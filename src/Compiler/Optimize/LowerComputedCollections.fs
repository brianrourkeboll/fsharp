// Copyright (c) Microsoft Corporation.  All Rights Reserved.  See License.txt in the project root for license information.

module internal FSharp.Compiler.LowerComputedCollectionExpressions

open System
open FSharp.Compiler.AbstractIL.IL
open FSharp.Compiler.AccessibilityLogic
open FSharp.Compiler.DiagnosticsLogger
open FSharp.Compiler.InfoReader
open FSharp.Compiler.LowerSequenceExpressions
open FSharp.Compiler.MethodCalls
open FSharp.Compiler.Syntax
open FSharp.Compiler.TcGlobals
open FSharp.Compiler.TypeRelations
open FSharp.Compiler.TypedTree
open FSharp.Compiler.TypedTreeBasics
open FSharp.Compiler.TypedTreeOps
open FSharp.Compiler.TypeHierarchy

/// Build the 'test and dispose' part of a 'use' statement
let BuildDisposableCleanup tcVal (g: TcGlobals) infoReader m (v: Val) =
    let disposeMethod = 
        match GetIntrinsicMethInfosOfType infoReader (Some "Dispose") AccessibleFromSomewhere AllowMultiIntfInstantiations.Yes IgnoreOverrides m g.system_IDisposable_ty with
        | [x] -> x
        | _ -> error(InternalError(FSComp.SR.tcCouldNotFindIDisposable(), m))
    // For struct types the test is simpler
    if isStructTy g v.Type then
        assert (TypeFeasiblySubsumesType 0 g infoReader.amap m g.system_IDisposable_ty CanCoerce v.Type)
        // We can use NeverMutates here because the variable is going out of scope, there is no need to take a defensive
        // copy of it.
        let disposeExpr, _ = BuildMethodCall tcVal g infoReader.amap NeverMutates m false disposeMethod NormalValUse [] [exprForVal v.Range v] [] None
        //callNonOverloadedILMethod g infoReader.amap m "Dispose" g.system_IDisposable_ty [exprForVal v.Range v]
        
        disposeExpr
    else
        let disposeObjVar, disposeObjExpr = mkCompGenLocal m "objectToDispose" g.system_IDisposable_ty
        let disposeExpr, _ = BuildMethodCall tcVal g infoReader.amap PossiblyMutates m false disposeMethod NormalValUse [] [disposeObjExpr] [] None
        let inputExpr = mkCoerceExpr(exprForVal v.Range v, g.obj_ty, m, v.Type)
        mkIsInstConditional g m g.system_IDisposable_ty inputExpr disposeObjVar disposeExpr (mkUnit g m)

let mkCallCollectorMethod tcVal (g: TcGlobals) infoReader m name collExpr args =
    let listCollectorTy = tyOfExpr g collExpr
    let addMethod = 
        match GetIntrinsicMethInfosOfType infoReader (Some name) AccessibleFromSomewhere AllowMultiIntfInstantiations.Yes IgnoreOverrides m listCollectorTy with
        | [x] -> x
        | _ -> error(InternalError("no " + name + " method found on Collector", m))
    let expr, _ = BuildMethodCall tcVal g infoReader.amap DefinitelyMutates m false addMethod NormalValUse [] [collExpr] args None
    expr

let mkCallCollectorAdd tcVal (g: TcGlobals) infoReader m collExpr arg =
    mkCallCollectorMethod tcVal g infoReader m "Add" collExpr [arg]

let mkCallCollectorAddMany tcVal (g: TcGlobals) infoReader m collExpr arg =
    mkCallCollectorMethod tcVal g infoReader m "AddMany" collExpr [arg]

let mkCallCollectorAddManyAndClose tcVal (g: TcGlobals) infoReader m collExpr arg =
    mkCallCollectorMethod tcVal g infoReader m "AddManyAndClose" collExpr [arg]

let mkCallCollectorClose tcVal (g: TcGlobals) infoReader m collExpr =
    mkCallCollectorMethod tcVal g infoReader m "Close" collExpr []

let LowerComputedListOrArraySeqExpr tcVal g amap m collectorTy overallSeqExpr =
    let infoReader = InfoReader(g, amap)
    let collVal, collExpr = mkMutableCompGenLocal m "@collector" collectorTy
    //let collExpr = mkValAddr m false (mkLocalValRef collVal)
    let rec ConvertSeqExprCode isUninteresting isTailcall expr =
        match expr with
        | SeqYield g (e, m) -> 
            let exprR = mkCallCollectorAdd tcVal g infoReader m collExpr e
            Result.Ok (false, exprR)

        | SeqDelay g (delayedExpr, _elemTy) ->
            ConvertSeqExprCode isUninteresting isTailcall delayedExpr

        | SeqAppend g (e1, e2, m) ->
            let res1 = ConvertSeqExprCode false false e1
            let res2 = ConvertSeqExprCode false isTailcall e2
            match res1, res2 with 
            | Result.Ok (_, e1R), Result.Ok (closed2, e2R) -> 
                let exprR = mkSequential m e1R e2R
                Result.Ok (closed2, exprR)
            | Result.Error msg, _ | _, Result.Error msg -> Result.Error msg

        | SeqWhile g (guardExpr, bodyExpr, spWhile, m) ->
            let resBody = ConvertSeqExprCode false false bodyExpr
            match resBody with 
            | Result.Ok (_, bodyExprR) ->
                let exprR = mkWhile g (spWhile, NoSpecialWhileLoopMarker, guardExpr, bodyExprR, m)
                Result.Ok (false, exprR)
            | Result.Error msg -> Result.Error msg

        | SeqUsing g (resource, v, bodyExpr, _elemTy, spBind, m) ->
            let resBody = ConvertSeqExprCode false false bodyExpr
            match resBody with 
            | Result.Ok (_, bodyExprR) ->
                // printfn "found Seq.using"
                let cleanupE = BuildDisposableCleanup tcVal g infoReader m v
                let exprR = 
                    mkLet spBind m v resource
                        (mkTryFinally g (bodyExprR, cleanupE, m, tyOfExpr g bodyExpr, DebugPointAtTry.No, DebugPointAtFinally.No))
                Result.Ok (false, exprR)
            | Result.Error msg -> Result.Error msg

        | SeqForEach g (inp, v, bodyExpr, _genElemTy, mFor, mIn, spIn) ->
            let resBody = ConvertSeqExprCode false false bodyExpr
            match resBody with 
            | Result.Ok (_, bodyExprR) ->
                // printfn "found Seq.for"
                let inpElemTy = v.Type
                let inpEnumTy = mkIEnumeratorTy g inpElemTy
                let enumv, enumve = mkCompGenLocal m "enum" inpEnumTy
                let guardExpr = callNonOverloadedILMethod g amap m "MoveNext" inpEnumTy [enumve]
                let cleanupE = BuildDisposableCleanup tcVal g infoReader m enumv

                // A debug point should get emitted prior to both the evaluation of 'inp' and the call to GetEnumerator
                let addForDebugPoint e = Expr.DebugPoint(DebugPointAtLeafExpr.Yes mFor, e)

                let spInAsWhile = match spIn with DebugPointAtInOrTo.Yes m -> DebugPointAtWhile.Yes m | DebugPointAtInOrTo.No -> DebugPointAtWhile.No

                let exprR =
                    mkInvisibleLet mFor enumv (callNonOverloadedILMethod g amap mFor "GetEnumerator" (mkSeqTy g inpElemTy) [inp])
                        (mkTryFinally g 
                            (mkWhile g (spInAsWhile, NoSpecialWhileLoopMarker, guardExpr, 
                                (mkInvisibleLet mIn v 
                                    (callNonOverloadedILMethod g amap mIn "get_Current" inpEnumTy [enumve]))
                                    bodyExprR, mIn), 
                            cleanupE,
                            mFor, tyOfExpr g bodyExpr, DebugPointAtTry.No, DebugPointAtFinally.No))
                    |> addForDebugPoint
                Result.Ok (false, exprR)
            | Result.Error msg -> Result.Error msg

        | SeqTryFinally g (bodyExpr, compensation, spTry, spFinally, m) ->
            let resBody = ConvertSeqExprCode false false bodyExpr
            match resBody with 
            | Result.Ok (_, bodyExprR) ->
                let exprR =
                    mkTryFinally g (bodyExprR, compensation, m, tyOfExpr g bodyExpr, spTry, spFinally)
                Result.Ok (false, exprR)
            | Result.Error msg -> Result.Error msg

        | SeqEmpty g m ->
            let exprR = mkUnit g m
            Result.Ok(false, exprR)

        | Expr.Sequential (x1, bodyExpr, NormalSeq, m) ->
            let resBody = ConvertSeqExprCode isUninteresting isTailcall bodyExpr
            match resBody with 
            | Result.Ok (closed, bodyExprR) ->
                let exprR = Expr.Sequential (x1, bodyExprR, NormalSeq, m)
                Result.Ok(closed, exprR)
            | Result.Error msg -> Result.Error msg

        | Expr.Let (bind, bodyExpr, m, _) ->
            let resBody = ConvertSeqExprCode isUninteresting isTailcall bodyExpr
            match resBody with 
            | Result.Ok (closed, bodyExprR) ->
                let exprR = mkLetBind m bind bodyExprR
                Result.Ok(closed, exprR)
            | Result.Error msg -> Result.Error msg

        | Expr.LetRec (binds, bodyExpr, m, _) ->
            let resBody = ConvertSeqExprCode isUninteresting isTailcall bodyExpr
            match resBody with 
            | Result.Ok (closed, bodyExprR) ->
                let exprR = mkLetRecBinds m binds bodyExprR
                Result.Ok(closed, exprR)
            | Result.Error msg -> Result.Error msg

        | Expr.Match (spBind, mExpr, pt, targets, m, ty) ->
            // lower all the targets. abandon if any fail to lower
            let resTargets =
                targets |> Array.map (fun (TTarget(vs, targetExpr, flags)) -> 
                    match ConvertSeqExprCode false false targetExpr with 
                    | Result.Ok (_, targetExprR) -> 
                        Result.Ok (TTarget(vs, targetExprR, flags))
                    | Result.Error msg -> Result.Error msg )

            if resTargets |> Array.forall (function Result.Ok _ -> true | _ -> false) then
                let tglArray = Array.map (function Result.Ok v -> v | _ -> failwith "unreachable") resTargets

                let exprR = primMkMatch (spBind, mExpr, pt, tglArray, m, ty)
                Result.Ok(false, exprR)
            else
                resTargets |> Array.pick (function Result.Error msg -> Some (Result.Error msg) | _ -> None)

        | Expr.DebugPoint(dp, innerExpr) ->
            let resInnerExpr = ConvertSeqExprCode isUninteresting isTailcall innerExpr
            match resInnerExpr with 
            | Result.Ok (flag, innerExprR) ->
                let exprR = Expr.DebugPoint(dp, innerExprR)
                Result.Ok (flag, exprR)
            | Result.Error msg -> Result.Error msg

        // yield! e ---> (for x in e -> x)

        | arbitrarySeqExpr ->
            let m = arbitrarySeqExpr.Range
            if isUninteresting then
                // printfn "FAILED - not worth compiling an unrecognized Seq.toList at %s " (stringOfRange m)
                Result.Error ()
            else
                // If we're the final in a sequential chain then we can AddMany, Close and return
                if isTailcall then 
                    let exprR = mkCallCollectorAddManyAndClose tcVal (g: TcGlobals) infoReader m collExpr arbitrarySeqExpr
                    // Return 'true' to indicate the collector was closed and the overall result of the expression is the result
                    Result.Ok(true, exprR)
                else
                    let exprR = mkCallCollectorAddMany tcVal (g: TcGlobals) infoReader m collExpr arbitrarySeqExpr
                    Result.Ok(false, exprR)


    // Perform conversion
    match ConvertSeqExprCode true true overallSeqExpr with 
    | Result.Ok (closed, overallSeqExprR) ->
        mkInvisibleLet m collVal (mkDefault (m, collectorTy)) 
            (if closed then 
                // If we ended with AddManyAndClose then we're done
                overallSeqExprR
             else
                mkSequential m
                    overallSeqExprR
                    (mkCallCollectorClose tcVal g infoReader m collExpr))
        |> Some
    | Result.Error () -> 
        None

let (|OptionalCoerce|) expr = 
    match expr with
    | Expr.Op (TOp.Coerce, _, [arg], _) -> arg
    | _ -> expr

// Making 'seq' optional means this kicks in for FSharp.Core, see TcArrayOrListComputedExpression
// which only adds a 'seq' call outside of FSharp.Core
[<return: Struct>]
let (|OptionalSeq|_|) g amap expr =
    match expr with
    // use 'seq { ... }' as an indicator
    | Seq g (e, elemTy) -> 
        ValueSome (e, elemTy)
    | _ -> 
    // search for the relevant element type
    match tyOfExpr g expr with
    | SeqElemTy g amap expr.Range elemTy ->
        ValueSome (expr, elemTy)
    | _ -> ValueNone

[<return: Struct>]
let (|SeqToList|_|) g expr =
    match expr with
    | ValApp g g.seq_to_list_vref (_, [seqExpr], m) -> ValueSome (seqExpr, m)
    | _ -> ValueNone

[<return: Struct>]
let (|SeqToArray|_|) g expr =
    match expr with
    | ValApp g g.seq_to_array_vref (_, [seqExpr], m) -> ValueSome (seqExpr, m)
    | _ -> ValueNone

[<RequireQualifiedAccess>]
module IntegralConst =
    /// Constant 0.
    [<return: Struct>]
    let (|Zero|_|) expr =
        match expr with
        | Const.Zero
        | Const.Int32 0
        | Const.Int64 0L
        | Const.UInt64 0UL
        | Const.UInt32 0u
        | Const.IntPtr 0L
        | Const.UIntPtr 0UL
        | Const.Int16 0s
        | Const.UInt16 0us
        | Const.SByte 0y
        | Const.Byte 0uy
        | Const.Char '\000' -> ValueSome Zero
        | _ -> ValueNone

    /// Constant 1.
    [<return: Struct>]
    let (|One|_|) expr =
        match expr with
        | Const.Int32 1
        | Const.Int64 1L
        | Const.UInt64 1UL
        | Const.UInt32 1u
        | Const.IntPtr 1L
        | Const.UIntPtr 1UL
        | Const.Int16 1s
        | Const.UInt16 1us
        | Const.SByte 1y
        | Const.Byte 1uy
        | Const.Char '\001' -> ValueSome One
        | _ -> ValueNone

    /// Constant -1.
    [<return: Struct>]
    let (|MinusOne|_|) expr =
        match expr with
        | Const.Int32 -1
        | Const.Int64 -1L
        | Const.IntPtr -1L
        | Const.Int16 -1s
        | Const.SByte -1y -> ValueSome MinusOne
        | _ -> ValueNone

    /// Positive constant.
    [<return: Struct>]
    let (|Positive|_|) expr =
        match expr with
        | Const.Int32 v when v > 0 -> ValueSome Positive
        | Const.Int64 v when v > 0L -> ValueSome Positive
        | Const.IntPtr v when v > 0L -> ValueSome Positive
        | Const.Int16 v when v > 0s -> ValueSome Positive
        | Const.SByte v when v > 0y -> ValueSome Positive
        | Const.UInt64 v when v > 0UL -> ValueSome Positive
        | Const.UInt32 v when v > 0u -> ValueSome Positive
        | Const.UIntPtr v when v > 0UL -> ValueSome Positive
        | Const.UInt16 v when v > 0us -> ValueSome Positive
        | Const.Byte v when v > 0uy -> ValueSome Positive
        | Const.Char v when v > '\000' -> ValueSome Positive
        | _ -> ValueNone

    let abs expr =
        match expr with
        | Const.Int32 Int32.MinValue -> Const.UInt32 (uint Int32.MaxValue + 1u)
        | Const.Int64 Int64.MinValue -> Const.UInt64 (uint64 Int64.MaxValue + 1UL)
        | Const.IntPtr Int64.MinValue -> Const.UIntPtr (uint64 Int64.MaxValue + 1UL)
        | Const.Int16 Int16.MinValue -> Const.UInt16 (uint16 Int16.MaxValue + 1us)
        | Const.SByte SByte.MinValue -> Const.Byte (byte SByte.MaxValue + 1uy)
        | Const.Int32 v -> Const.Int32 (abs v)
        | Const.Int64 v -> Const.Int64 (abs v)
        | Const.IntPtr v -> Const.IntPtr (abs v)
        | Const.Int16 v -> Const.Int16 (abs v)
        | Const.SByte v -> Const.SByte (abs v)
        | _ -> expr

/// Note: this assumes that an empty range has already been checked for
/// (otherwise the conversion operations here might overflow).
[<return: Struct>]
let (|ConstCount|_|) (start, step, finish) =
    match start, step, finish with
    // This will cause an overflow exception to be raised at runtime, which we need for parity with the library implementation.
    | Expr.Const (value = Const.Int64 Int64.MinValue), Expr.Const (value = Const.Int64 1L), Expr.Const (value = Const.Int64 Int64.MaxValue)
    | Expr.Const (value = Const.Int64 Int64.MaxValue), Expr.Const (value = Const.Int64 -1L), Expr.Const (value = Const.Int64 Int64.MinValue)
    | Expr.Const (value = Const.UInt64 UInt64.MinValue), Expr.Const (value = Const.UInt64 1UL), Expr.Const (value = Const.UInt64 UInt64.MaxValue) -> ValueSome (Const.UInt64 UInt64.MaxValue)

    | Expr.Const (value = Const.IntPtr Int64.MinValue), Expr.Const (value = Const.IntPtr 1L), Expr.Const (value = Const.IntPtr Int64.MaxValue)
    | Expr.Const (value = Const.IntPtr Int64.MaxValue), Expr.Const (value = Const.IntPtr -1L), Expr.Const (value = Const.IntPtr Int64.MinValue)
    | Expr.Const (value = Const.UIntPtr UInt64.MinValue), Expr.Const (value = Const.UIntPtr 1UL), Expr.Const (value = Const.UIntPtr UInt64.MaxValue) -> ValueSome (Const.UIntPtr UInt64.MaxValue)

    // We must special-case a step of Int64.MinValue, since we cannot call abs on it.
    | Expr.Const (value = Const.Int64 start), Expr.Const (value = Const.Int64 Int64.MinValue), Expr.Const (value = Const.Int64 finish) -> ValueSome (Const.UInt64 ((uint64 start - uint64 finish) / (uint64 Int64.MaxValue + 1UL) + 1UL))
    | Expr.Const (value = Const.IntPtr start), Expr.Const (value = Const.IntPtr Int64.MinValue), Expr.Const (value = Const.IntPtr finish) -> ValueSome (Const.UIntPtr ((uint64 start - uint64 finish) / (uint64 Int64.MaxValue + 1UL) + 1UL))

    | Expr.Const (value = Const.Int64 start), Expr.Const (value = Const.Int64 step), Expr.Const (value = Const.Int64 finish) when start <= finish -> ValueSome (Const.UInt64 ((uint64 finish - uint64 start) / uint64 (abs step) + 1UL))
    | Expr.Const (value = Const.Int64 start), Expr.Const (value = Const.Int64 step), Expr.Const (value = Const.Int64 finish) -> ValueSome (Const.UInt64 ((uint64 start - uint64 finish) / uint64 (abs step) + 1UL))

    | Expr.Const (value = Const.IntPtr start), Expr.Const (value = Const.IntPtr step), Expr.Const (value = Const.IntPtr finish) when start <= finish -> ValueSome (Const.UIntPtr ((uint64 finish - uint64 start) / uint64 (abs step) + 1UL))
    | Expr.Const (value = Const.IntPtr start), Expr.Const (value = Const.IntPtr step), Expr.Const (value = Const.IntPtr finish) -> ValueSome (Const.UIntPtr ((uint64 start - uint64 finish) / uint64 (abs step) + 1UL))

    | Expr.Const (value = Const.Int32 start), Expr.Const (value = Const.Int32 step), Expr.Const (value = Const.Int32 finish) when start <= finish -> ValueSome (Const.UInt32 (uint32 ((uint64 finish - uint64 start) / uint64 (abs (int64 step)) + 1UL)))
    | Expr.Const (value = Const.Int32 start), Expr.Const (value = Const.Int32 step), Expr.Const (value = Const.Int32 finish) -> ValueSome (Const.UInt32 (uint32 ((uint64 start - uint64 finish) / uint64 (abs (int64 step)) + 1UL)))

    | Expr.Const (value = Const.Int16 start), Expr.Const (value = Const.Int16 step), Expr.Const (value = Const.Int16 finish) when start <= finish -> ValueSome (Const.UInt16 (uint16 ((uint64 finish - uint64 start) / uint64 (abs (int64 step)) + 1UL)))
    | Expr.Const (value = Const.Int16 start), Expr.Const (value = Const.Int16 step), Expr.Const (value = Const.Int16 finish) -> ValueSome (Const.UInt16 (uint16 ((uint64 start - uint64 finish) / uint64 (abs (int64 step)) + 1UL)))

    | Expr.Const (value = Const.SByte start), Expr.Const (value = Const.SByte step), Expr.Const (value = Const.SByte finish) when start <= finish -> ValueSome (Const.Byte (byte ((uint64 finish - uint64 start) / uint64 (abs (int64 step)) + 1UL)))
    | Expr.Const (value = Const.SByte start), Expr.Const (value = Const.SByte step), Expr.Const (value = Const.SByte finish) -> ValueSome (Const.Byte (byte ((uint64 start - uint64 finish) / uint64 (abs (int64 step)) + 1UL)))

    | Expr.Const (value = Const.UIntPtr start), Expr.Const (value = Const.UIntPtr step), Expr.Const (value = Const.UIntPtr finish) -> ValueSome (Const.UIntPtr ((finish - start) / step + 1UL))
    | Expr.Const (value = Const.UInt64 start), Expr.Const (value = Const.UInt64 step), Expr.Const (value = Const.UInt64 finish) -> ValueSome (Const.UInt64 ((finish - start) / step + 1UL))
    | Expr.Const (value = Const.UInt32 start), Expr.Const (value = Const.UInt32 step), Expr.Const (value = Const.UInt32 finish) -> ValueSome (Const.UInt32 ((finish - start) / step + 1u))
    | Expr.Const (value = Const.UInt16 start), Expr.Const (value = Const.UInt16 step), Expr.Const (value = Const.UInt16 finish) -> ValueSome (Const.UInt16 ((finish - start) / step + 1us))
    | Expr.Const (value = Const.Byte start), Expr.Const (value = Const.Byte step), Expr.Const (value = Const.Byte finish) -> ValueSome (Const.Byte ((finish - start) / step + 1uy))
    | Expr.Const (value = Const.Char start), Expr.Const (value = Const.Char step), Expr.Const (value = Const.Char finish) -> ValueSome (Const.Char (char (uint16 (finish - start) / uint16 step) + '\001'))

    | _ -> ValueNone

/// Bind start, step, and finish exprs to local variables if needed, e.g.,
///
///     [start..finishExpr] → let finish = finishExpr in …
///
///     [startExpr..finish] → let start = startExpr in …
///
///     [startExpr..finishExpr] → let start = startExpr in let finish = finishExpr in …
let mkLetBindingsIfNeeded m ty start step finish mkInitExpr =
    match start, step, finish with
    | (Expr.Const _ | Expr.Val _), (Expr.Const _ | Expr.Val _), (Expr.Const _ | Expr.Val _) ->
        mkInitExpr start step finish

    | (Expr.Const _ | Expr.Val _), (Expr.Const _ | Expr.Val _), _ ->
        mkCompGenLetIn m (nameof finish) ty finish (fun (_, finish) ->
            mkInitExpr start step finish)

    | _, (Expr.Const _ | Expr.Val _), (Expr.Const _ | Expr.Val _) ->
        mkCompGenLetIn m (nameof start) ty start (fun (_, start) ->
            mkInitExpr start step finish)

    | (Expr.Const _ | Expr.Val _), _, (Expr.Const _ | Expr.Val _) ->
        mkCompGenLetIn m (nameof step) ty step (fun (_, step) ->
            mkInitExpr start step finish)

    | _, (Expr.Const _ | Expr.Val _), _ ->
        mkCompGenLetIn m (nameof start) ty start (fun (_, start) ->
            mkCompGenLetIn m (nameof finish) ty finish (fun (_, finish) ->
                mkInitExpr start step finish))

    | (Expr.Const _ | Expr.Val _), _, _ ->
        mkCompGenLetIn m (nameof step) ty step (fun (_, step) ->
            mkCompGenLetIn m (nameof finish) ty finish (fun (_, finish) ->
                mkInitExpr start step finish))

    | _, _, (Expr.Const _ | Expr.Val _) ->
        mkCompGenLetIn m (nameof start) ty start (fun (_, start) ->
            mkCompGenLetIn m (nameof step) ty step (fun (_, step) ->
                mkInitExpr start step finish))

    | _, _, _ ->
        mkCompGenLetIn m (nameof start) ty start (fun (_, start) ->
            mkCompGenLetIn m (nameof step) ty step (fun (_, step) ->
                mkCompGenLetIn m (nameof finish) ty finish (fun (_, finish) ->
                    mkInitExpr start step finish)))

module List =
    /// Makes an expression that will build a list from an integral range.
    /// Expects a constant zero step to have already been checked for.
    let mkFromIntegralRange tcVal g amap m overallElemTy overallSeqExpr start step finish =
        match start, step, finish with
        // [5..1] → []
        // [1..-1..5] → []
        | EmptyRange -> mkNil g m overallElemTy

        // [1..5]
        // [1..2..5]
        // [start..finish]
        // [start..step..finish]
        | _, _, _ ->
            let collectorTy = g.mk_ListCollector_ty overallElemTy

            mkLetBindingsIfNeeded m overallElemTy start step finish (fun start step finish ->
                mkCompGenLetMutableIn m "collector" collectorTy (mkDefault (m, collectorTy)) (fun (_, collector) ->
                    mkCompGenLetMutableIn m "loopVar" overallElemTy start (fun (loopVarVal, loopVar) ->
                        let reader = InfoReader (g, amap)

                        let body = mkCallCollectorAdd tcVal g reader m collector loopVar

                        let loop =
                            mkOptimizedRangeLoop
                                g
                                (m, m, m, DebugPointAtWhile.No)
                                (overallElemTy, overallSeqExpr)
                                (start, step, finish)
                                (loopVarVal, loopVar)
                                body

                        let close = mkCallCollectorClose tcVal g reader m collector

                        mkSequential m loop close
                    )
                )
            )

module Array =
    /// Makes an expression that will build an array from an integral range.
    /// Expects a constant zero step to have already been checked for.
    let mkFromIntegralRange g m overallElemTy overallSeqExpr start step finish =
        let arrayTy = mkArrayType g overallElemTy

        //let mkZero ty =
        //    let underlyingTy = stripMeasuresFromTy g ty
        //    if typeEquiv g underlyingTy g.int32_ty then Expr.Const (Const.Int32 0, m, ty)
        //    elif typeEquiv g underlyingTy g.int64_ty then Expr.Const (Const.Int64 0L, m, ty)
        //    elif typeEquiv g underlyingTy g.uint64_ty then Expr.Const (Const.UInt64 0UL, m, ty)
        //    elif typeEquiv g underlyingTy g.uint32_ty then Expr.Const (Const.UInt32 0u, m, ty)
        //    elif typeEquiv g underlyingTy g.nativeint_ty then Expr.Const (Const.IntPtr 0L, m, ty)
        //    elif typeEquiv g underlyingTy g.unativeint_ty then Expr.Const (Const.UIntPtr 0UL, m, ty)
        //    elif typeEquiv g underlyingTy g.int16_ty then Expr.Const (Const.Int16 0s, m, ty)
        //    elif typeEquiv g underlyingTy g.uint16_ty then Expr.Const (Const.UInt16 0us, m, ty)
        //    elif typeEquiv g underlyingTy g.sbyte_ty then Expr.Const (Const.SByte 0y, m, ty)
        //    elif typeEquiv g underlyingTy g.byte_ty then Expr.Const (Const.Byte 0uy, m, ty)
        //    elif typeEquiv g underlyingTy g.char_ty then Expr.Const (Const.Char '\000', m, ty)
        //    else error (InternalError ($"Unrecognized integral type '{ty}'.", m))
        let mkZero ty =
            let underlyingTy = stripMeasuresFromTy g ty
            if typeEquiv g underlyingTy g.int32_ty then mkCoerceIfNeeded g ty g.int32_ty (Expr.Const (Const.Int32 0, m, ty))
            elif typeEquiv g underlyingTy g.int64_ty then mkCoerceIfNeeded g ty g.int64_ty (Expr.Const (Const.Int64 0L, m, ty))
            elif typeEquiv g underlyingTy g.uint64_ty then mkCoerceIfNeeded g ty g.uint64_ty (Expr.Const (Const.UInt64 0UL, m, ty))
            elif typeEquiv g underlyingTy g.uint32_ty then mkCoerceIfNeeded g ty g.uint32_ty (Expr.Const (Const.UInt32 0u, m, ty))
            elif typeEquiv g underlyingTy g.nativeint_ty then mkCoerceIfNeeded g ty g.nativeint_ty (Expr.Const (Const.IntPtr 0L, m, ty))
            elif typeEquiv g underlyingTy g.unativeint_ty then mkCoerceIfNeeded g ty g.unativeint_ty (Expr.Const (Const.UIntPtr 0UL, m, ty))
            elif typeEquiv g underlyingTy g.int16_ty then mkCoerceIfNeeded g ty g.int16_ty (Expr.Const (Const.Int16 0s, m, ty))
            elif typeEquiv g underlyingTy g.uint16_ty then mkCoerceIfNeeded g ty g.uint16_ty (Expr.Const (Const.UInt16 0us, m, ty))
            elif typeEquiv g underlyingTy g.sbyte_ty then mkCoerceIfNeeded g ty g.sbyte_ty (Expr.Const (Const.SByte 0y, m, ty))
            elif typeEquiv g underlyingTy g.byte_ty then mkCoerceIfNeeded g ty g.byte_ty (Expr.Const (Const.Byte 0uy, m, ty))
            elif typeEquiv g underlyingTy g.char_ty then mkCoerceIfNeeded g ty g.char_ty (Expr.Const (Const.Char '\000', m, ty))
            else error (InternalError ($"Unrecognized integral type '{ty}'.", m))

        let mkOne ty =
            let underlyingTy = stripMeasuresFromTy g ty
            if typeEquiv g underlyingTy g.int32_ty then Expr.Const (Const.Int32 1, m, ty)
            elif typeEquiv g underlyingTy g.int64_ty then Expr.Const (Const.Int64 1L, m, ty)
            elif typeEquiv g underlyingTy g.uint64_ty then Expr.Const (Const.UInt64 1UL, m, ty)
            elif typeEquiv g underlyingTy g.uint32_ty then Expr.Const (Const.UInt32 1u, m, ty)
            elif typeEquiv g underlyingTy g.nativeint_ty then Expr.Const (Const.IntPtr 1L, m, ty)
            elif typeEquiv g underlyingTy g.unativeint_ty then Expr.Const (Const.UIntPtr 1UL, m, ty)
            elif typeEquiv g underlyingTy g.int16_ty then Expr.Const (Const.Int16 1s, m, ty)
            elif typeEquiv g underlyingTy g.uint16_ty then Expr.Const (Const.UInt16 1us, m, ty)
            elif typeEquiv g underlyingTy g.sbyte_ty then Expr.Const (Const.SByte 1y, m, ty)
            elif typeEquiv g underlyingTy g.byte_ty then Expr.Const (Const.Byte 1uy, m, ty)
            elif typeEquiv g underlyingTy g.char_ty then Expr.Const (Const.Char '\001', m, ty)
            else error (InternalError ($"Unrecognized integral type '{ty}'.", m))

        let mkMinValue ty =
            let underlyingTy = stripMeasuresFromTy g ty
            if typeEquiv g underlyingTy g.int32_ty then Expr.Const (Const.Int32 Int32.MinValue, m, ty)
            elif typeEquiv g underlyingTy g.int64_ty then Expr.Const (Const.Int64 Int64.MinValue, m, ty)
            elif typeEquiv g underlyingTy g.uint64_ty then Expr.Const (Const.UInt64 UInt64.MinValue, m, ty)
            elif typeEquiv g underlyingTy g.uint32_ty then Expr.Const (Const.UInt32 UInt32.MinValue, m, ty)
            elif typeEquiv g underlyingTy g.nativeint_ty then Expr.Const (Const.IntPtr Int64.MinValue, m, ty)
            elif typeEquiv g underlyingTy g.unativeint_ty then Expr.Const (Const.UIntPtr UInt64.MinValue, m, ty)
            elif typeEquiv g underlyingTy g.int16_ty then Expr.Const (Const.Int16 Int16.MinValue, m, ty)
            elif typeEquiv g underlyingTy g.uint16_ty then Expr.Const (Const.UInt16 UInt16.MinValue, m, ty)
            elif typeEquiv g underlyingTy g.sbyte_ty then Expr.Const (Const.SByte SByte.MinValue, m, ty)
            elif typeEquiv g underlyingTy g.byte_ty then Expr.Const (Const.Byte Byte.MinValue, m, ty)
            elif typeEquiv g underlyingTy g.char_ty then Expr.Const (Const.Char '\000', m, ty)
            else error (InternalError ($"Unrecognized integral type '{ty}'.", m))

        let mkMaxValuePlusOneAsUnsigned originalTy destTy =
            let underlyingTy = stripMeasuresFromTy g originalTy
            if typeEquiv g underlyingTy g.int32_ty then Expr.Const (Const.UInt64 (uint64 (uint Int32.MaxValue + 1u)), m, destTy)
            elif typeEquiv g underlyingTy g.int64_ty then Expr.Const (Const.UInt64 (uint64 Int64.MaxValue + 1UL), m, destTy)
            elif typeEquiv g underlyingTy g.nativeint_ty then Expr.Const (Const.UIntPtr (uint64 Int64.MaxValue + 1UL), m, destTy)
            elif typeEquiv g underlyingTy g.int16_ty then Expr.Const (Const.UInt64 (uint64 (uint16 Int16.MaxValue + 1us)), m, destTy)
            elif typeEquiv g underlyingTy g.sbyte_ty then Expr.Const (Const.UInt64 (uint64 (byte SByte.MaxValue + 1uy)), m, destTy)
            else error (InternalError ($"Unrecognized signed integral type '{originalTy}'.", m))

        /// Trigger an overflow exception at runtime if count might not fit in a native int without overflow.
        let convToNativeIntWithOverflow m count =
            let ty = stripMeasuresFromTy g overallElemTy

            // The total count for any of these could be greater than 2⁶³ - 1.
            if typeEquiv g ty g.int64_ty || typeEquiv g ty g.uint64_ty || typeEquiv g ty g.nativeint_ty || typeEquiv g ty g.unativeint_ty then
                mkAsmExpr ([AI_conv_ovf_un DT_I], [], [count], [g.nativeint_ty], m)
            else
                count

        let mkInitializer count start step =
            let ilTy =
                let ty = stripMeasuresFromTy g overallElemTy
                if typeEquiv g ty g.int32_ty then g.ilg.typ_Int32
                elif typeEquiv g ty g.int64_ty then g.ilg.typ_Int64
                elif typeEquiv g ty g.uint64_ty then g.ilg.typ_UInt64
                elif typeEquiv g ty g.uint32_ty then g.ilg.typ_UInt32
                elif typeEquiv g ty g.nativeint_ty then g.ilg.typ_IntPtr
                elif typeEquiv g ty g.unativeint_ty then g.ilg.typ_UIntPtr
                elif typeEquiv g ty g.int16_ty then g.ilg.typ_Int16
                elif typeEquiv g ty g.uint16_ty then g.ilg.typ_UInt16
                elif typeEquiv g ty g.sbyte_ty then g.ilg.typ_SByte
                elif typeEquiv g ty g.byte_ty then g.ilg.typ_Byte
                elif typeEquiv g ty g.char_ty then g.ilg.typ_Char
                else error (InternalError ($"Unrecognized integral type '{overallElemTy}'.", m))

            /// (# "newarr !0" type ('T) count : 'T array #)
            let mkNewArray count =
                mkAsmExpr
                    (
                        [I_newarr (ILArrayShape.SingleDimensional, ilTy)],
                        [],
                        [convToNativeIntWithOverflow m count],
                        [arrayTy],
                        m
                    )

            mkCompGenLetIn m "array" arrayTy (mkNewArray count) (fun (_, array) ->
                mkCompGenLetMutableIn m "i" g.int32_ty (mkZero g.int32_ty) (fun (iVal, i) ->
                    mkCompGenLetMutableIn m "loopVar" overallElemTy start (fun (loopVarVal, loopVar) ->
                        // array[i] <- loopVar
                        let setArrSubI = mkAsmExpr ([I_stelem_any (ILArrayShape.SingleDimensional, ilTy)], [], [array; i; loopVar], [], m)

                        // loopVar <- loopVar + step
                        let incrV = mkValSet m (mkLocalValRef loopVarVal) (mkAsmExpr ([AI_add], [], [loopVar; step], [overallElemTy], m))

                        // i <- i + 1
                        let incrI = mkValSet m (mkLocalValRef iVal) (mkAsmExpr ([AI_add], [], [i; mkOne g.int32_ty], [g.int32_ty], m))

                        let body = mkSequentials g m [setArrSubI; incrV; incrI]

                        let guard = mkILAsmClt g m i (mkLdlen g m array)

                        let loop =
                            mkWhile
                                g
                                (
                                    DebugPointAtWhile.No,
                                    NoSpecialWhileLoopMarker,
                                    guard,
                                    body,
                                    m
                                )

                        // while i < array.Length do <body> done
                        // array
                        mkSequential m loop array
                    )
                )
            )

        let mkSignednessAppropriateClt ty e1 e2 =
            if isSignedIntegerTy g ty then
                mkILAsmClt g m e1 e2
            else
                mkAsmExpr ([AI_clt_un], [], [e1; e2], [g.bool_ty], m)

        match start, step, finish with
        // [|5..1|] → [||]
        // [|1..-1..5|] → [||]
        | EmptyRange -> mkArray (overallElemTy, [], m)

        // [|1..5|]
        // [|1..2..5|]
        | ConstCount count -> mkInitializer (Expr.Const (count, m, overallElemTy)) start step

        // [|start..finish|]
        // [|start..step..finish|]
        | _, _, _ ->
            /// Make an expression to compute the size of the array.
            let mkCount m rangeExpr ty start step finish =
                /// Unsigned diff: e1 - e2.
                /// Expects that e1 >= e2.
                let mkDiff e1 e2 =
                    if isSignedIntegerTy g ty && not (typeEquiv g (stripMeasuresFromTy g ty) g.nativeint_ty) then
                        let mkWiden e = mkAsmExpr ([AI_conv DT_I8], [], [e], [g.uint64_ty], m)
                        mkAsmExpr ([AI_sub], [], [mkWiden e1; mkWiden e2], [g.uint64_ty], m)
                    else
                        mkAsmExpr ([AI_sub], [], [e1; e2], [ty], m)

                /// diff / step
                let mkQuotient diff step = mkAsmExpr ([AI_div_un], [], [diff; step], [tyOfExpr g diff], m)

                /// (diff / step + 1)
                let mkAddOne pseudoCount =
                    // For parity with the behavior of (..) and (.. ..) in FSharp.Core,
                    // we want an overflow exception to be raised at runtime
                    // instead of returning a 0 count here.
                    let shouldRaiseOverflowExnAtRuntime =
                        let ty = stripMeasuresFromTy g overallElemTy

                        typeEquiv g ty g.int64_ty
                        || typeEquiv g ty g.uint64_ty
                        || typeEquiv g ty g.nativeint_ty
                        || typeEquiv g ty g.unativeint_ty

                    let ty = tyOfExpr g pseudoCount

                    if shouldRaiseOverflowExnAtRuntime then
                        mkAsmExpr ([AI_add_ovf_un], [], [pseudoCount; mkOne ty], [ty], m)
                    else
                        mkAsmExpr ([AI_add], [], [pseudoCount; mkOne ty], [ty], m)

                match step with
                // step = 1:
                //     if finish < start then 0 else finish - start + 1
                | Expr.Const (value = IntegralConst.One) ->
                    let diff = mkDiff finish start
                    let diffTy = tyOfExpr g diff

                    mkCond
                        DebugPointAtBinding.NoneAtInvisible
                        m
                        diffTy
                        (mkSignednessAppropriateClt overallElemTy finish start)
                        (mkZero diffTy)
                        (mkAddOne diff)

                // (Only possible for signed types.)
                //
                // step = -1:
                //     if start < finish then 0 else start - finish + 1
                | Expr.Const (value = IntegralConst.MinusOne) ->
                    let diff = mkDiff finish start
                    let diffTy = tyOfExpr g diff

                    mkCond
                        DebugPointAtBinding.NoneAtInvisible
                        m
                        diffTy
                        (mkSignednessAppropriateClt overallElemTy start finish)
                        (mkZero diffTy)
                        (mkAddOne diff)

                // 0 < step:
                //     if finish < start then 0 else (finish - start) / step + 1
                | Expr.Const (value = IntegralConst.Positive) ->
                    let diff = mkDiff finish start
                    let diffTy = tyOfExpr g diff

                    mkCond
                        DebugPointAtBinding.NoneAtInvisible
                        m
                        diffTy
                        (mkSignednessAppropriateClt overallElemTy finish start)
                        (mkZero diffTy)
                        (mkAddOne (mkQuotient diff step))

                // (Only possible for signed types.)
                //
                // step < 0:
                //     if start < finish then 0 else (start - finish) / abs step + 1
                | Expr.Const (value = negativeStep) ->
                    let diff = mkDiff start finish
                    let diffTy = tyOfExpr g diff

                    mkCond
                        DebugPointAtBinding.NoneAtInvisible
                        m
                        diffTy
                        (mkSignednessAppropriateClt overallElemTy start finish)
                        (mkZero diffTy)
                        (mkAddOne (mkQuotient diff (Expr.Const (IntegralConst.abs negativeStep, m, diffTy))))

                // Arbitrary non-constant step.
                //
                //     if step = 0 then
                //         ignore ((.. ..) start step finish) // Throws.
                //     if 0 < step then
                //         if finish < start then 0 else (finish - start) / step + 1
                //     else // step < 0
                //         if start < finish then 0 else (finish - start) / step + 1
                | _notConst ->
                    // Use the potentially-evaluated-and-bound start, step, and finish.
                    let rangeExpr =
                        match rangeExpr with
                        | Expr.App (funcExpr, formalType, tyargs, _, m) -> Expr.App (funcExpr, formalType, tyargs, [start; step; finish], m)
                        | _ -> rangeExpr

                    // Let the range call throw the appropriate localized
                    // exception at runtime if step is zero:
                    //
                    //     if step = 0 then ignore ((.. ..) start step finish)
                    let throwIfStepIsZero =
                        let callAndIgnoreRangeExpr =
                            mkSequential
                                m
                                rangeExpr
                                (mkUnit g m)

                        mkCond
                            DebugPointAtBinding.NoneAtInvisible
                            m
                            g.unit_ty
                            (mkILAsmCeq g m step (mkZero overallElemTy))
                            callAndIgnoreRangeExpr
                            (mkUnit g m)

                    let count =
                        if isSignedIntegerTy g ty then
                            let positiveStep =
                                let diff = mkDiff finish start
                                let diffTy = tyOfExpr g diff

                                mkCond
                                    DebugPointAtBinding.NoneAtInvisible
                                    m
                                    diffTy
                                    (mkSignednessAppropriateClt overallElemTy finish start)
                                    (mkZero diffTy)
                                    (mkAddOne (mkQuotient diff step))

                            let negativeStep =
                                let diff = mkDiff start finish
                                let diffTy = tyOfExpr g diff

                                let step =
                                    mkCond
                                        DebugPointAtBinding.NoneAtInvisible
                                        m
                                        diffTy
                                        (mkILAsmCeq g m step (mkMinValue overallElemTy))
                                        (mkMaxValuePlusOneAsUnsigned overallElemTy diffTy)
                                        (mkAsmExpr ([AI_neg], [], [step], [diffTy], m))

                                mkCond
                                    DebugPointAtBinding.NoneAtInvisible
                                    m
                                    diffTy
                                    (mkSignednessAppropriateClt overallElemTy start finish)
                                    (mkZero diffTy)
                                    (mkAddOne (mkQuotient diff step))

                            mkCond
                                DebugPointAtBinding.NoneAtInvisible
                                m
                                (tyOfExpr g positiveStep)
                                (mkSignednessAppropriateClt overallElemTy (mkZero overallElemTy) step)
                                positiveStep
                                negativeStep
                        else // Unsigned.
                            let diff = mkDiff finish start
                            let diffTy = tyOfExpr g diff

                            mkCond
                                DebugPointAtBinding.NoneAtInvisible
                                m
                                ty
                                (mkSignednessAppropriateClt overallElemTy finish start)
                                (mkZero diffTy)
                                (mkAddOne (mkQuotient diff step))

                    mkSequential m throwIfStepIsZero count

            mkLetBindingsIfNeeded m overallElemTy start step finish (fun start step finish ->
                let count = mkCount m overallSeqExpr overallElemTy start step finish
                let countTy = tyOfExpr g count

                mkCompGenLetIn m "count" countTy count (fun (_, count) ->
                    // count < 1
                    let countLtOne = mkSignednessAppropriateClt countTy count (mkOne countTy)

                    // [||]
                    let empty = mkArray (overallElemTy, [], m)

                    let initialize = mkInitializer count start step

                    // if count < 1 then [||] else <initialize>
                    mkCond
                        DebugPointAtBinding.NoneAtInvisible
                        m
                        arrayTy
                        countLtOne
                        empty
                        initialize
                )
            )

let LowerComputedListOrArrayExpr tcVal (g: TcGlobals) amap overallExpr =
    // If ListCollector is in FSharp.Core then this optimization kicks in
    if g.ListCollector_tcr.CanDeref then
        match overallExpr with
        // […]
        | SeqToList g (OptionalCoerce (OptionalSeq g amap (overallSeqExpr, overallElemTy)), m) ->
            match overallSeqExpr with
            // [start..0..finish] → let the default implementation raise an exception.
            | IntegralRange g (_, (_, Expr.Const (value = IntegralConst.Zero), _)) ->
                let collectorTy = g.mk_ListCollector_ty overallElemTy
                LowerComputedListOrArraySeqExpr tcVal g amap m collectorTy overallSeqExpr
                
            // [start..finish]
            // [start..step..finish]
            | IntegralRange g (_, (start, step, finish)) ->
                Some (List.mkFromIntegralRange tcVal g amap m overallElemTy overallSeqExpr start step finish)

            // [(* Anything more complex. *)]
            | _ ->
                let collectorTy = g.mk_ListCollector_ty overallElemTy
                LowerComputedListOrArraySeqExpr tcVal g amap m collectorTy overallSeqExpr

        // [|…|]
        | SeqToArray g (OptionalCoerce (OptionalSeq g amap (overallSeqExpr, overallElemTy)), m) ->
            match overallSeqExpr with
            // [|start..0..finish|] → let the default implementation raise an exception.
            | IntegralRange g (_, (_, Expr.Const (value = IntegralConst.Zero), _)) ->
                let collectorTy = g.mk_ArrayCollector_ty overallElemTy
                LowerComputedListOrArraySeqExpr tcVal g amap m collectorTy overallSeqExpr

            // [|start..finish|]
            // [|start..step..finish|]
            | IntegralRange g (_, (start, step, finish)) ->
                Some (Array.mkFromIntegralRange g m overallElemTy overallSeqExpr start step finish)

            // [|(* Anything more complex. *)|]
            | _ ->
                let collectorTy = g.mk_ArrayCollector_ty overallElemTy
                LowerComputedListOrArraySeqExpr tcVal g amap m collectorTy overallSeqExpr

        | _ -> None
    else
        None
