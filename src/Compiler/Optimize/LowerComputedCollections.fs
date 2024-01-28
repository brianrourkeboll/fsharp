// Copyright (c) Microsoft Corporation.  All Rights Reserved.  See License.txt in the project root for license information.

module internal FSharp.Compiler.LowerComputedCollectionExpressions

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

module Const =
    /// Constant 0.
    [<return: Struct>]
    let (|Zero|_|) expr =
        match expr with
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

/// start..finish
/// start..step..finish
[<return: Struct>]
let (|IntegralRange|_|) g expr =
    match expr with
    | ValApp g g.range_int32_op_vref ([], [start; step; finish], _) -> ValueSome (start, step, finish)
    | ValApp g g.range_int64_op_vref ([], [start; step; finish], _) -> ValueSome (start, step, finish)
    | ValApp g g.range_uint64_op_vref ([], [start; step; finish], _) -> ValueSome (start, step, finish)
    | ValApp g g.range_uint32_op_vref ([], [start; step; finish], _) -> ValueSome (start, step, finish)
    | ValApp g g.range_nativeint_op_vref ([], [start; step; finish], _) -> ValueSome (start, step, finish)
    | ValApp g g.range_unativeint_op_vref ([], [start; step; finish], _) -> ValueSome (start, step, finish)
    | ValApp g g.range_int16_op_vref ([], [start; step; finish], _) -> ValueSome (start, step, finish)
    | ValApp g g.range_uint16_op_vref ([], [start; step; finish], _) -> ValueSome (start, step, finish)
    | ValApp g g.range_sbyte_op_vref ([], [start; step; finish], _) -> ValueSome (start, step, finish)
    | ValApp g g.range_byte_op_vref ([], [start; step; finish], _) -> ValueSome (start, step, finish)
    | ValApp g g.range_char_op_vref ([], [start; step; finish], _) -> ValueSome (start, step, finish)
    | _ -> ValueNone

/// 5..1
/// 1..-5
/// 1..-1..5
/// 5..2..1
[<return: Struct>]
let (|EmptyRange|_|) (start, step, finish) =
    match start, step, finish with
    | Expr.Const (value = Const.Int32 start), Expr.Const (value = Const.Int32 step), Expr.Const (value = Const.Int32 finish) when finish < start && step > 0 || finish > start && step < 0 -> ValueSome EmptyRange
    | Expr.Const (value = Const.Int64 start), Expr.Const (value = Const.Int64 step), Expr.Const (value = Const.Int64 finish) when finish < start && step > 0L || finish > start && step < 0L -> ValueSome EmptyRange
    | Expr.Const (value = Const.UInt64 start), Expr.Const (value = Const.UInt64 _), Expr.Const (value = Const.UInt64 finish) when finish < start -> ValueSome EmptyRange
    | Expr.Const (value = Const.UInt32 start), Expr.Const (value = Const.UInt32 _), Expr.Const (value = Const.UInt32 finish) when finish < start -> ValueSome EmptyRange
    | Expr.Const (value = Const.IntPtr start), Expr.Const (value = Const.IntPtr step), Expr.Const (value = Const.IntPtr finish) when finish < start && step > 0L || finish > start && step < 0L -> ValueSome EmptyRange
    | Expr.Const (value = Const.UIntPtr start), Expr.Const (value = Const.UIntPtr _), Expr.Const (value = Const.UIntPtr finish) when finish < start -> ValueSome EmptyRange
    | Expr.Const (value = Const.Int16 start), Expr.Const (value = Const.Int16 step), Expr.Const (value = Const.Int16 finish) when finish < start && step > 0s || finish > start && step < 0s -> ValueSome EmptyRange
    | Expr.Const (value = Const.UInt16 start), Expr.Const (value = Const.UInt16 _), Expr.Const (value = Const.UInt16 finish) when finish < start -> ValueSome EmptyRange
    | Expr.Const (value = Const.SByte start), Expr.Const (value = Const.SByte step), Expr.Const (value = Const.SByte finish) when finish < start && step > 0y || finish > start && step < 0y -> ValueSome EmptyRange
    | Expr.Const (value = Const.Byte start), Expr.Const (value = Const.Byte _), Expr.Const (value = Const.Byte finish) when finish < start -> ValueSome EmptyRange
    | Expr.Const (value = Const.Char start), Expr.Const (value = Const.Char step), Expr.Const (value = Const.Char finish) when finish < start && step > '\000' || finish > start && step < '\000' -> ValueSome EmptyRange
    | _ -> ValueNone

/// 1..5 → (5 - 1) / 1 + 1 → 5
/// 1..2..5 → (5 - 1) / 2 + 1 → 3
/// -1..-2..-5 → (-5 - -1) / -2 + 1 → 3
[<return: Struct>]
let (|ConstCount|_|) (start, step, finish) =
    match start, step, finish with
    | Expr.Const (value = Const.Int32 start), Expr.Const (value = Const.Int32 step), Expr.Const (value = Const.Int32 finish) -> ValueSome (Const.Int32 ((finish - start) / step + 1))
    | Expr.Const (value = Const.Int64 start), Expr.Const (value = Const.Int64 step), Expr.Const (value = Const.Int64 finish) -> ValueSome (Const.Int64  ((finish - start) / step + 1L))
    | Expr.Const (value = Const.UInt64 start), Expr.Const (value = Const.UInt64 step), Expr.Const (value = Const.UInt64 finish) -> ValueSome (Const.UInt64 ((finish - start) / step + 1UL))
    | Expr.Const (value = Const.UInt32 start), Expr.Const (value = Const.UInt32 step), Expr.Const (value = Const.UInt32 finish) -> ValueSome (Const.UInt32 ((finish - start) / step + 1u))
    | Expr.Const (value = Const.IntPtr start), Expr.Const (value = Const.IntPtr step), Expr.Const (value = Const.IntPtr finish) -> ValueSome (Const.IntPtr ((finish - start) / step + 1L))
    | Expr.Const (value = Const.UIntPtr start), Expr.Const (value = Const.UIntPtr step), Expr.Const (value = Const.UIntPtr finish) -> ValueSome (Const.UIntPtr ((finish - start) / step + 1UL))
    | Expr.Const (value = Const.Int16 start), Expr.Const (value = Const.Int16 step), Expr.Const (value = Const.Int16 finish) -> ValueSome (Const.Int16 ((finish - start) / step + 1s))
    | Expr.Const (value = Const.UInt16 start), Expr.Const (value = Const.UInt16 step), Expr.Const (value = Const.UInt16 finish) -> ValueSome (Const.UInt16 ((finish - start) / step + 1us))
    | Expr.Const (value = Const.SByte start), Expr.Const (value = Const.SByte step), Expr.Const (value = Const.SByte finish) -> ValueSome (Const.SByte ((finish - start) / step + 1y))
    | Expr.Const (value = Const.Byte start), Expr.Const (value = Const.Byte step), Expr.Const (value = Const.Byte finish) -> ValueSome (Const.Byte ((finish - start) / step + 1uy))
    | Expr.Const (value = Const.Char start), Expr.Const (value = Const.Char step), Expr.Const (value = Const.Char finish) -> ValueSome (Const.Char (char (uint16 (finish - start) / uint16 step) + '\001'))
    | _ -> ValueNone

let LowerComputedListOrArrayExpr tcVal (g: TcGlobals) amap overallExpr =
    // If ListCollector is in FSharp.Core then this optimization kicks in
    if g.ListCollector_tcr.CanDeref then
        /// Make a call to invalidArg when step is 0.
        let mkCallInvalidArgStepCannotBeZero m overallSeqExprTy =
            mkCallInvalidArg
                g
                m
                overallSeqExprTy
                (Expr.Const (Const.String "step", Text.Range.range0, g.string_ty))
                (Expr.Const (Const.String (SR.GetString "stepCannotBeZero"), Text.Range.range0, g.string_ty))

        /// Make an expression holding the count/length
        /// to initialize the collection with.
        let mkCount ty start step finish =
            match step with
            | Expr.Const (value = Const.One) ->
                let diff = mkAsmExpr ([AI_sub], [], [finish; start], [ty], Text.Range.range0)
                mkAsmExpr ([AI_add], [], [diff; step], [ty], Text.Range.range0)
            | _notOne ->
                let diff = mkAsmExpr ([AI_sub], [], [finish; start], [ty], Text.Range.range0)
                let quotient = mkAsmExpr ([AI_div], [], [diff; step], [ty], Text.Range.range0)
                mkAsmExpr ([AI_add], [], [quotient; step], [ty], Text.Range.range0)

        /// Make a lambda expression to pass into
        /// the Array.init/List.init call.
        let mkInitializer ty start step =
            match step with
            | Expr.Const (value = Const.One) ->
                let v, e = mkCompGenLocal Text.Range.range0 "i" g.int32_ty
                let body = mkAsmExpr ([AI_add], [], [start; e], [ty], Text.Range.range0)
                mkLambda Text.Range.range0 v (body, ty)
            | _notOne ->
                let v, e = mkCompGenLocal Text.Range.range0 "i" g.int32_ty
                let mulByStep = mkAsmExpr ([AI_mul], [], [e; step], [ty], Text.Range.range0)
                let body = mkAsmExpr ([AI_add], [], [mulByStep; start], [ty], Text.Range.range0)
                mkLambda Text.Range.range0 v (body, ty)

        /// Make an expression that initializes a list
        /// with the values from start through finish.
        let mkListInit m ty start step finish =
            match step with
            // [start..finish] → if finish < start then [] else List.init (finish - start + 1) ((+) start)
            // [start..2..finish] → if finish < start then [] else List.init ((finish - start) / 2 + 1) ((*) 2 >> (+) start)
            | Expr.Const (value = Const.Positive) ->
                mkCond
                    DebugPointAtBinding.NoneAtInvisible
                    m
                    (mkListTy g ty)
                    (mkILAsmClt g Text.Range.range0 finish start)
                    (mkNil g Text.Range.range0 ty)
                    (mkCallListInit g Text.Range.range0 ty (mkCount ty start step finish) (mkInitializer ty start step))

            // [start..-2..finish] → if start < finish then [] else List.init ((finish - start) / -2 + 1) ((*) -2 >> (+) start)
            | Expr.Const (value = _negative) ->
                mkCond
                    DebugPointAtBinding.NoneAtInvisible
                    m
                    (mkListTy g ty)
                    (mkILAsmClt g Text.Range.range0 start finish)
                    (mkNil g Text.Range.range0 ty)
                    (mkCallListInit g Text.Range.range0 ty (mkCount ty start step finish) (mkInitializer ty start step))

            // [start..step..finish] → let count = (finish - start) / step + 1 in if count = 0 then [] else List.init count ((*) step >> (+) start)
            | _notConst ->
                let listTy = mkListTy g ty
                let count = mkCount ty start step finish

                let init =
                    mkCompGenLetIn Text.Range.range0 (nameof count) ty count (fun (_, count) ->
                        mkCond
                            DebugPointAtBinding.NoneAtInvisible
                            m
                            listTy
                            (mkILAsmCeq g Text.Range.range0 count (mkZero g Text.Range.range0))
                            (mkNil g Text.Range.range0 ty)
                            (mkCallListInit g Text.Range.range0 ty count (mkInitializer ty start step)))

                mkCond
                    DebugPointAtBinding.NoneAtInvisible
                    m
                    listTy
                    (mkILAsmCeq g Text.Range.range0 step (mkZero g Text.Range.range0))
                    (mkCallInvalidArgStepCannotBeZero m listTy)
                    init

        /// Make an expression that initializes an array
        /// with the values from start through finish.
        let mkArrayInit m ty start step finish =
            match step with
            // [|start..finish|] → if finish < start then [||] else Array.init (finish - start + 1) ((+) start)
            // [|start..2..finish|] → if finish < start then [||] else Array.init ((finish - start) / 2 + 1) ((*) 2 >> (+) start)
            | Expr.Const (value = Const.Positive) ->
                mkCond
                    DebugPointAtBinding.NoneAtInvisible
                    m
                    (mkArrayType g ty)
                    (mkILAsmClt g Text.Range.range0 finish start)
                    (mkArray (ty, [], Text.Range.range0))
                    (mkCallArrayInit g Text.Range.range0 ty (mkCount ty start step finish) (mkInitializer ty start step))

            // [|start..-2..finish|] → if start < finish then [||] else Array.init ((finish - start) / -2 + 1) ((*) -2 >> (+) start)
            | Expr.Const (value = _negative) ->
                mkCond
                    DebugPointAtBinding.NoneAtInvisible
                    m
                    (mkArrayType g ty)
                    (mkILAsmClt g Text.Range.range0 start finish)
                    (mkArray (ty, [], Text.Range.range0))
                    (mkCallArrayInit g Text.Range.range0 ty (mkCount ty start step finish) (mkInitializer ty start step))

            // [|start..step..finish|] → let count = (finish - start) / step + 1 in if count = 0 then [||] else Array.init count ((*) step >> (+) start)
            | _notConst ->
                let arrayTy = mkArrayType g  ty
                let count = mkCount ty start step finish

                let init =
                    mkCompGenLetIn Text.Range.range0 (nameof count) ty count (fun (_, count) ->
                        mkCond
                            DebugPointAtBinding.NoneAtInvisible
                            m
                            arrayTy
                            (mkILAsmCeq g Text.Range.range0 count (mkZero g Text.Range.range0))
                            (mkArray (ty, [], Text.Range.range0))
                            (mkCallArrayInit g Text.Range.range0 ty count (mkInitializer ty start step)))

                mkCond
                    DebugPointAtBinding.NoneAtInvisible
                    m
                    arrayTy
                    (mkILAsmCeq g Text.Range.range0 step (mkZero g Text.Range.range0))
                    (mkCallInvalidArgStepCannotBeZero m arrayTy)
                    init

        /// Bind start, step, and finish exprs to local variables if needed.
        ///
        /// E.g.:
        ///
        ///     [start..finishExpr] →
        ///         let finish = finishExpr in
        ///         if finish < start then [] else List.init (finish - start + 1) ((+) start)
        ///
        ///     [startExpr..finish] →
        ///         let start = startExpr in
        ///         if finish < start then [] else List.init (finish - start + 1) ((+) start)
        let mkLetBindingsIfNeeded ty start step finish mkInitExpr =
            match start, step, finish with
            | (Expr.Const _ | Expr.Val _), (Expr.Const _ | Expr.Val _), (Expr.Const _ | Expr.Val _) ->
                mkInitExpr ty start step finish

            | (Expr.Const _ | Expr.Val _), (Expr.Const _ | Expr.Val _), _ ->
                mkCompGenLetIn Text.Range.range0 (nameof finish) ty finish (fun (_, finish) ->
                    mkInitExpr ty start step finish)

            | _, (Expr.Const _ | Expr.Val _), (Expr.Const _ | Expr.Val _) ->
                mkCompGenLetIn Text.Range.range0 (nameof start) ty start (fun (_, start) ->
                    mkInitExpr ty start step finish)

            | (Expr.Const _ | Expr.Val _), _, (Expr.Const _ | Expr.Val _) ->
                mkCompGenLetIn Text.Range.range0 (nameof step) ty step (fun (_, step) ->
                    mkInitExpr ty start step finish)

            | _, (Expr.Const _ | Expr.Val _), _ ->
                mkCompGenLetIn Text.Range.range0 (nameof start) ty start (fun (_, start) ->
                    mkCompGenLetIn Text.Range.range0 (nameof finish) ty finish (fun (_, finish) ->
                        mkInitExpr ty start step finish))

            | (Expr.Const _ | Expr.Val _), _, _ ->
                mkCompGenLetIn Text.Range.range0 (nameof step) ty step (fun (_, step) ->
                    mkCompGenLetIn Text.Range.range0 (nameof finish) ty finish (fun (_, finish) ->
                        mkInitExpr ty start step finish))

            | _, _, (Expr.Const _ | Expr.Val _) ->
                mkCompGenLetIn Text.Range.range0 (nameof start) ty start (fun (_, start) ->
                    mkCompGenLetIn Text.Range.range0 (nameof step) ty step (fun (_, step) ->
                        mkInitExpr ty start step finish))

            | _, _, _ ->
                mkCompGenLetIn Text.Range.range0 (nameof start) ty start (fun (_, start) ->
                    mkCompGenLetIn Text.Range.range0 (nameof step) ty step (fun (_, step) ->
                        mkCompGenLetIn Text.Range.range0 (nameof finish) ty finish (fun (_, finish) ->
                            mkInitExpr ty start step finish)))

        match overallExpr with
        // […]
        | SeqToList g (OptionalCoerce (OptionalSeq g amap (overallSeqExpr, overallElemTy)), m) ->
            match overallSeqExpr with
            // [start..0..finish]
            | IntegralRange g (_, Expr.Const (value = Const.Zero), _) ->
                Some (mkCallInvalidArgStepCannotBeZero m (tyOfExpr g overallSeqExpr))

            // [5..1] → []
            | IntegralRange g EmptyRange ->
                Some (mkNil g m overallElemTy)

            // [1..5] → List.init 5 ((+) 5)
            | IntegralRange g ((start, step, _) & ConstCount count)
                ->
                Some (mkCallListInit g Text.Range.range0 overallElemTy (Expr.Const (count, Text.Range.range0, overallElemTy)) (mkInitializer overallElemTy start step))

            // [start..finish] → if finish < start then [] else List.init (finish - start + 1) ((+) start)
            // [start..step..finish] → let count = (finish - start) / step + 1 in if count = 0 then [] else List.init count ((*) step >> (+) start)
            | IntegralRange g (start, step, finish) ->
                Some (mkLetBindingsIfNeeded overallElemTy start step finish (mkListInit m))

            // [(* Anything more complex. *)]
            | _ ->
                let collectorTy = g.mk_ListCollector_ty overallElemTy
                LowerComputedListOrArraySeqExpr tcVal g amap m collectorTy overallSeqExpr
        
        // [|…|]
        | SeqToArray g (OptionalCoerce (OptionalSeq g amap (overallSeqExpr, overallElemTy)), m) ->
            match overallSeqExpr with
            // [|start..0..finish|]
            | IntegralRange g (_, Expr.Const (value = Const.Zero), _) ->
                Some (mkCallInvalidArgStepCannotBeZero m (tyOfExpr g overallSeqExpr))

            // [|5..1|] → [||]
            | IntegralRange g EmptyRange ->
                Some (mkArray (overallElemTy, [], m))

            // [|1..5|] → Array.init 5 ((+) 5)
            | IntegralRange g ((start, step, _) & ConstCount count)
                ->
                Some (mkCallArrayInit g Text.Range.range0 overallElemTy (Expr.Const (count, Text.Range.range0, overallElemTy)) (mkInitializer overallElemTy start step))

            // [|start..finish|] → if finish < start then [||] else Array.init (finish - start + 1) ((+) start)
            // [|start..step..finish|] → let count = (finish - start) / step + 1 in if count = 0 then [||] else Array.init count ((*) step >> (+) start)
            | IntegralRange g (start, step, finish) ->
                Some (mkLetBindingsIfNeeded overallElemTy start step finish (mkArrayInit m))

            // [|(* Anything more complex. *)|]
            | _ ->
                let collectorTy = g.mk_ArrayCollector_ty overallElemTy
                LowerComputedListOrArraySeqExpr tcVal g amap m collectorTy overallSeqExpr

        | _ -> None
    else
        None
