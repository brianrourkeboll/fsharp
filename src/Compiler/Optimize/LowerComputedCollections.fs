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

/// Note: this assumes that an empty range has already been checked for
/// (otherwise the conversion operations here might overflow).
[<return: Struct>]
let (|ConstCount|_|) (start, step, finish) =
    match start, step, finish with
    | Expr.Const (value = Const.Int32 start), Expr.Const (value = Const.Int32 step), Expr.Const (value = Const.Int32 finish) -> ValueSome (Const.Int32 (int ((bigint finish - bigint start) / bigint step + 1I)))
    | Expr.Const (value = Const.Int64 start), Expr.Const (value = Const.Int64 step), Expr.Const (value = Const.Int64 finish) -> ValueSome (Const.Int64 (int64 ((bigint finish - bigint start) / bigint step + 1I)))
    | Expr.Const (value = Const.UInt64 start), Expr.Const (value = Const.UInt64 step), Expr.Const (value = Const.UInt64 finish) -> ValueSome (Const.UInt64 (uint64 ((bigint finish - bigint start) / bigint step + 1I)))
    | Expr.Const (value = Const.UInt32 start), Expr.Const (value = Const.UInt32 step), Expr.Const (value = Const.UInt32 finish) -> ValueSome (Const.UInt32 (uint32 ((bigint finish - bigint start) / bigint step + 1I)))
    | Expr.Const (value = Const.IntPtr start), Expr.Const (value = Const.IntPtr step), Expr.Const (value = Const.IntPtr finish) -> ValueSome (Const.IntPtr (int64 ((bigint finish - bigint start) / bigint step + 1I)))
    | Expr.Const (value = Const.UIntPtr start), Expr.Const (value = Const.UIntPtr step), Expr.Const (value = Const.UIntPtr finish) -> ValueSome (Const.UIntPtr (uint64 ((bigint finish - bigint start) / bigint step + 1I)))
    | Expr.Const (value = Const.Int16 start), Expr.Const (value = Const.Int16 step), Expr.Const (value = Const.Int16 finish) -> ValueSome (Const.Int16 (int16 ((bigint finish - bigint start) / bigint step + 1I)))
    | Expr.Const (value = Const.UInt16 start), Expr.Const (value = Const.UInt16 step), Expr.Const (value = Const.UInt16 finish) -> ValueSome (Const.UInt16 ((finish - start) / step + 1us))
    | Expr.Const (value = Const.SByte start), Expr.Const (value = Const.SByte step), Expr.Const (value = Const.SByte finish) -> ValueSome (Const.SByte ((finish - start) / step + 1y))
    | Expr.Const (value = Const.Byte start), Expr.Const (value = Const.Byte step), Expr.Const (value = Const.Byte finish) -> ValueSome (Const.Byte ((finish - start) / step + 1uy))
    | Expr.Const (value = Const.Char start), Expr.Const (value = Const.Char step), Expr.Const (value = Const.Char finish) -> ValueSome (Const.Char (char (uint16 (finish - start) / uint16 step) + '\001'))
    | _ -> ValueNone

let LowerComputedListOrArrayExpr tcVal (g: TcGlobals) amap overallExpr =
    // If ListCollector is in FSharp.Core then this optimization kicks in
    if g.ListCollector_tcr.CanDeref then
        /// The zero range.
        let range0 = Text.Range.range0

        /// Make an expression holding the count
        /// to initialize the collection with.
        let mkCount ty start step finish =
            match step with
            // step = 1:
            //     finish - start + 1
            | Expr.Const (value = IntegralConst.One) ->
                let diff = mkAsmExpr ([AI_sub], [], [finish; start], [ty], range0)
                mkAsmExpr ([AI_add], [], [diff; mkOne g range0], [ty], range0)

            // step = -1:
            //     -(finish - start) + 1
            | Expr.Const (value = IntegralConst.MinusOne) ->
                let diff = mkAsmExpr ([AI_neg], [], [mkAsmExpr ([AI_sub], [], [finish; start], [ty], range0)], [ty], range0)
                mkAsmExpr ([AI_add], [], [diff; mkOne g range0], [ty], range0)

            // Arbitrary step:
            //     (finish - start) / step + 1
            | _notConstOne ->
                let count =
                    let diff = mkAsmExpr ([AI_sub], [], [finish; start], [ty], range0)
                    let quotient = mkAsmExpr ([AI_div], [], [diff; step], [ty], range0)
                    mkAsmExpr ([AI_add], [], [quotient; mkOne g range0], [ty], range0)

                // Let the default implementation throw the appropriate localized
                // exception at runtime if step is zero:
                //     if step = 0 then toList (range …) else (finish - start) / step + 1
                //     if step = 0 then toArray (range …) else (finish - start) / step + 1
                mkCond
                    DebugPointAtBinding.NoneAtInvisible
                    range0
                    ty
                    (mkILAsmCeq g range0 count (mkZero g range0))
                    overallExpr
                    count

        /// Triggers an overflow exception at runtime if count doesn't fit in a native int.
        let convToNativeIntWithOverflow overallElemTy count =
            if typeEquiv g overallElemTy g.int64_ty then mkAsmExpr ([AI_conv_ovf DT_I], [], [count], [g.nativeint_ty], range0)
            elif typeEquiv g overallElemTy g.uint64_ty then mkAsmExpr ([AI_conv_ovf_un DT_I], [], [count], [g.nativeint_ty], range0)
            else count

        /// Bind start, step, and finish exprs to local variables if needed, e.g.,
        ///
        ///     [start..finishExpr] → let finish = finishExpr in …
        ///
        ///     [startExpr..finish] → let start = startExpr in …
        ///
        ///     [startExpr..finishExpr] → let start = startExpr in let finish = finishExpr in …
        let mkLetBindingsIfNeeded ty start step finish mkInitExpr =
            match start, step, finish with
            | (Expr.Const _ | Expr.Val _), (Expr.Const _ | Expr.Val _), (Expr.Const _ | Expr.Val _) ->
                mkInitExpr start step finish

            | (Expr.Const _ | Expr.Val _), (Expr.Const _ | Expr.Val _), _ ->
                mkCompGenLetIn range0 (nameof finish) ty finish (fun (_, finish) ->
                    mkInitExpr start step finish)

            | _, (Expr.Const _ | Expr.Val _), (Expr.Const _ | Expr.Val _) ->
                mkCompGenLetIn range0 (nameof start) ty start (fun (_, start) ->
                    mkInitExpr start step finish)

            | (Expr.Const _ | Expr.Val _), _, (Expr.Const _ | Expr.Val _) ->
                mkCompGenLetIn range0 (nameof step) ty step (fun (_, step) ->
                    mkInitExpr start step finish)

            | _, (Expr.Const _ | Expr.Val _), _ ->
                mkCompGenLetIn range0 (nameof start) ty start (fun (_, start) ->
                    mkCompGenLetIn range0 (nameof finish) ty finish (fun (_, finish) ->
                        mkInitExpr start step finish))

            | (Expr.Const _ | Expr.Val _), _, _ ->
                mkCompGenLetIn range0 (nameof step) ty step (fun (_, step) ->
                    mkCompGenLetIn range0 (nameof finish) ty finish (fun (_, finish) ->
                        mkInitExpr start step finish))

            | _, _, (Expr.Const _ | Expr.Val _) ->
                mkCompGenLetIn range0 (nameof start) ty start (fun (_, start) ->
                    mkCompGenLetIn range0 (nameof step) ty step (fun (_, step) ->
                        mkInitExpr start step finish))

            | _, _, _ ->
                mkCompGenLetIn range0 (nameof start) ty start (fun (_, start) ->
                    mkCompGenLetIn range0 (nameof step) ty step (fun (_, step) ->
                        mkCompGenLetIn range0 (nameof finish) ty finish (fun (_, finish) ->
                            mkInitExpr start step finish)))

        let mkIlTy m ty =
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
            else error(InternalError($"Unrecognized integral type '{ty}'.", m))

        match overallExpr with
        // […]
        | SeqToList g (OptionalCoerce (OptionalSeq g amap (overallSeqExpr, overallElemTy)), m) ->
            match overallSeqExpr with
            // [start..0..finish] → let the default implementation raise an exception.
            | IntegralRange g (_, (_, Expr.Const (value = IntegralConst.Zero), _)) ->
                let collectorTy = g.mk_ListCollector_ty overallElemTy
                LowerComputedListOrArraySeqExpr tcVal g amap m collectorTy overallSeqExpr
                
            // [5..1] → []
            // [1..-1..5] → []
            | IntegralRange g (_, EmptyRange) ->
                Some (mkNil g m overallElemTy)

            // [start..finish]
            // [start..step..finish]
            | IntegralRange g (_, (start, step, finish)) ->
                // expr → expr :: null
                let mkFreshConsNoTail expr = mkCons g overallElemTy expr (mkNull range0 overallElemTy)

                // cons tail → cons.(::).1 <- tail
                let mkSetFreshConsTail cons tail = mkUnionCaseFieldSet (cons, g.cons_ucref, [overallElemTy], 1, tail, range0)

                let expr =
                    mkLetBindingsIfNeeded overallElemTy start step finish (fun start step finish ->
                        // let mutable list = []
                        mkCompGenLetIn range0 "list" (mkListTy g overallElemTy) (mkNil g range0 overallElemTy) (fun (listVal, list) ->
                            // let mutable i = 0
                            mkCompGenLetIn range0 "i" overallElemTy (mkZero g range0) (fun (iVal, i) ->
                                // let cons2 = (i + step) :: null
                                // list.(::).1 <- cons2
                                // list <- cons2
                                let setTail =
                                    let cons2 = mkFreshConsNoTail (mkAsmExpr ([AI_add], [], [i; step], [overallElemTy], range0))
                                    mkCompGenLetIn range0 (nameof cons2) (mkListTy g overallElemTy) cons2 (fun (_, cons2) ->
                                        mkSequential
                                            range0
                                            // list.(::).1 <- cons2
                                            (mkSetFreshConsTail list cons2)
                                            // list <- cons2
                                            (mkValSet range0 (mkLocalValRef listVal) cons2))

                                // i <- 1 + i
                                let incr = mkValSet range0 (mkLocalValRef iVal) (mkAsmExpr ([ AI_add ], [], [mkOne g m; i], [overallElemTy], range0))

                                let body = mkSequential range0 setTail incr

                                let loop =
                                    mkOptimizedRangeLoop
                                        g
                                        (overallElemTy, overallExpr)
                                        (m, DebugPointAtBinding.NoneAtInvisible, range0, range0, DebugPointAtWhile.No)
                                        (start, step, finish)
                                        body

                                // while … do <body> done
                                // list
                                mkSequential range0 loop list
                            )
                        )
                    )

                Some expr

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

            // [|5..1|] → [||]
            // [|1..-1..5|] → [||]
            | IntegralRange g (_, EmptyRange) ->
                Some (mkArray (overallElemTy, [], m))

            // [|1..5|]
            // [|1..2..5|]
            | IntegralRange g (_, (start, step, finish) & ConstCount count) ->
                // (# "newarr !0" type ('T) count : 'T array #)
                let arrayInit =
                    mkAsmExpr
                        (
                            [I_newarr (ILArrayShape.SingleDimensional, mkIlTy m overallElemTy)],
                            [],
                            [convToNativeIntWithOverflow overallElemTy (Expr.Const (count, range0, overallElemTy))],
                            [mkArrayType g overallElemTy],
                            range0
                        )

                let expr =
                    mkCompGenLetIn range0 "array" (mkArrayType g overallElemTy) arrayInit (fun (_, array) ->
                        mkCompGenLetIn range0 "i" g.int32_ty (mkZero g range0) (fun (iVal, i) ->
                            // array[i] <- start + i
                            let setArrSubI = mkCallArraySet g range0 overallElemTy array i (mkAsmExpr ([AI_add], [], [start; i], [overallElemTy], range0))

                            // i <- step + i
                            let incr = mkValSet range0 (mkLocalValRef iVal) (mkAsmExpr ([AI_add], [], [step; i], [g.int32_ty], range0))

                            let body = mkSequential range0 setArrSubI incr

                            let loop =
                                mkOptimizedRangeLoop
                                    g
                                    (overallElemTy, overallExpr)
                                    (m, DebugPointAtBinding.NoneAtInvisible, range0, range0, DebugPointAtWhile.No)
                                    (start, step, finish)
                                    body

                            // while … do <body> done
                            // array
                            mkSequential range0 loop array
                        )
                    )

                Some expr

            // [|start..finish|]
            // [|start..step..finish|]
            | IntegralRange g (_, (start, step, finish)) ->
                let expr =
                    mkLetBindingsIfNeeded overallElemTy start step finish (fun start step finish ->
                        mkCompGenLetIn range0 "count" g.int32_ty (mkCount overallElemTy start step finish) (fun (_, count) ->
                            // count < 1
                            let countLtOne = mkILAsmClt g range0 count (mkOne g range0)

                            // [||]
                            let empty = mkArray (overallElemTy, [], range0)

                            // (# "newarr !0" type ('T) count : 'T array #)
                            let arrayInit =
                                mkAsmExpr
                                    (
                                        [I_newarr (ILArrayShape.SingleDimensional, mkIlTy m overallElemTy)],
                                        [],
                                        [convToNativeIntWithOverflow overallElemTy count],
                                        [mkArrayType g overallElemTy],
                                        range0
                                    )

                            let initialize =
                                mkCompGenLetIn range0 "array" (mkArrayType g overallElemTy) arrayInit (fun (_, array) ->
                                    mkCompGenLetIn range0 "i" g.int32_ty (mkZero g range0) (fun (iVal, i) ->
                                        // array[i] <- start + i
                                        let setArrSubI = mkCallArraySet g range0 overallElemTy array i (mkAsmExpr ([AI_add], [], [start; i], [overallElemTy], range0))

                                        // i <- step + i
                                        let incr = mkValSet range0 (mkLocalValRef iVal) (mkAsmExpr ([AI_add], [], [step; i], [g.int32_ty], range0))

                                        let body = mkSequential range0 setArrSubI incr

                                        let loop =
                                            mkOptimizedRangeLoop
                                                g
                                                (overallElemTy, overallExpr)
                                                (m, DebugPointAtBinding.NoneAtInvisible, range0, range0, DebugPointAtWhile.No)
                                                (start, step, finish)
                                                body

                                        // while … do <body> done
                                        // array
                                        mkSequential range0 loop array
                                    )
                                )

                            // if count < 1 then [||] else <initialize>
                            mkCond
                                DebugPointAtBinding.NoneAtInvisible
                                range0
                                (mkArrayType g overallElemTy)
                                countLtOne
                                empty
                                initialize
                        )
                    )

                Some expr

            // [|(* Anything more complex. *)|]
            | _ ->
                let collectorTy = g.mk_ArrayCollector_ty overallElemTy
                LowerComputedListOrArraySeqExpr tcVal g amap m collectorTy overallSeqExpr

        | _ -> None
    else
        None
