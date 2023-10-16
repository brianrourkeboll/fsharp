// Copyright (c) Microsoft Corporation.  All Rights Reserved.  See License.txt in the project root for license information.

namespace FSharp.Compiler.EditorServices

open System.Collections.Generic
open System.Runtime.CompilerServices
open Internal.Utilities.Library
open FSharp.Compiler.CodeAnalysis
open FSharp.Compiler.Symbols
open FSharp.Compiler.Syntax
open FSharp.Compiler.Syntax.PrettyNaming
open FSharp.Compiler.Text
open FSharp.Compiler.Text.Range

module UnusedOpens =

    let symbolHash =
        HashIdentity.FromFunctions (fun (x: FSharpSymbol) -> x.GetEffectivelySameAsHash()) (fun x y -> x.IsEffectivelySameAs(y))

    /// Represents one namespace or module opened by an 'open' statement
    type OpenedModule(entity: FSharpEntity, isNestedAutoOpen: bool) =

        /// Compute an indexed table of the set of symbols revealed by 'open', on-demand
        let revealedSymbols: Lazy<HashSet<FSharpSymbol>> =
            lazy
                let symbols: FSharpSymbol[] =
                    [|
                        for ent in entity.NestedEntities do
                            ent

                            if ent.IsFSharpRecord then
                                for rf in ent.FSharpFields do
                                    rf

                            if ent.IsFSharpUnion && not (ent.HasAttribute<RequireQualifiedAccessAttribute>()) then
                                for unionCase in ent.UnionCases do
                                    unionCase

                            if ent.HasAttribute<ExtensionAttribute>() then
                                for fv in ent.MembersFunctionsAndValues do
                                    // fv.IsExtensionMember is always false for C# extension methods returning by `MembersFunctionsAndValues`,
                                    // so we have to check Extension attribute instead.
                                    // (note: fv.IsExtensionMember has proper value for symbols returning by GetAllUsesOfAllSymbolsInFile though)
                                    if fv.HasAttribute<ExtensionAttribute>() then
                                        fv

                        for apCase in entity.ActivePatternCases do
                            apCase

                        // The IsNamespace and IsFSharpModule cases are handled by looking at DeclaringEntity below
                        if not entity.IsNamespace && not entity.IsFSharpModule then
                            for fv in entity.MembersFunctionsAndValues do
                                fv
                    |]

                HashSet<_>(symbols, symbolHash)

        member _.Entity = entity
        member _.IsNestedAutoOpen = isNestedAutoOpen
        member _.RevealedSymbolsContains(symbol) = revealedSymbols.Force().Contains symbol

    type OpenedModuleGroup =
        {
            OpenedModules: OpenedModule[]
        }

        static member Create(modul: FSharpEntity) =
            let rec getModuleAndItsAutoOpens (isNestedAutoOpen: bool) (modul: FSharpEntity) =
                [|
                    yield OpenedModule(modul, isNestedAutoOpen)
                    for ent in modul.NestedEntities do
                        if ent.IsFSharpModule && ent.HasAttribute<AutoOpenAttribute>() then
                            yield! getModuleAndItsAutoOpens true ent
                |]

            {
                OpenedModules = getModuleAndItsAutoOpens false modul
            }

    /// Represents a single open statement
    type OpenStatement =
        {
            /// All namespaces, modules and types which this open declaration effectively opens, including the AutoOpen ones
            OpenedGroups: OpenedModuleGroup list

            /// The range of open statement itself
            Range: range

            /// The scope on which this open declaration is applied
            AppliedScope: range
        }

    /// Gets the open statements, their scopes and their resolutions
    let getOpenStatements (openDeclarations: FSharpOpenDeclaration[]) : OpenStatement[] =
        openDeclarations
        |> Array.choose (fun openDecl ->
            if openDecl.IsOwnNamespace then
                None
            else
                match openDecl.LongId, openDecl.Range with
                | firstId :: _, Some range ->
                    if firstId.idText = MangledGlobalName then
                        None
                    else
                        let openedModulesAndTypes =
                            List.concat [ openDecl.Modules; openDecl.Types |> List.map (fun ty -> ty.TypeDefinition) ]

                        Some
                            {
                                OpenedGroups = openedModulesAndTypes |> List.map OpenedModuleGroup.Create
                                Range = range
                                AppliedScope = openDecl.AppliedScope
                            }
                | _ -> None)

    /// Only consider symbol uses which are the first part of a long ident, i.e. with no qualifying identifiers
    let filterSymbolUses (getSourceLineStr: int -> string) (symbolUses: seq<FSharpSymbolUse>) =
        symbolUses
        |> Seq.filter (fun (su: FSharpSymbolUse) ->
            match su.Symbol with
            | :? FSharpMemberOrFunctionOrValue as fv when fv.IsExtensionMember ->
                // Extension members should be taken into account even though they have a prefix (as they do most of the time)
                true

            | :? FSharpMemberOrFunctionOrValue as fv when not fv.IsModuleValueOrMember ->
                // Local values can be ignored
                false

            | :? FSharpMemberOrFunctionOrValue when su.IsFromDefinition ->
                // Value definitions should be ignored
                false

            | :? FSharpGenericParameter ->
                // Generic parameters can be ignored, they never come into scope via 'open'
                false

            | :? FSharpUnionCase when su.IsFromDefinition -> false

            | :? FSharpField as field when field.DeclaringEntity.IsSome && field.DeclaringEntity.Value.IsFSharpRecord ->
                // Record fields are used in name resolution
                true

            | :? FSharpField as field when field.IsUnionCaseField -> false

            | _ ->
                // For the rest of symbols we pick only those which are the first part of a long ident, because it's they which are
                // contained in opened namespaces / modules. For example, we pick `IO` from long ident `IO.File.OpenWrite` because
                // it's `open System` which really brings it into scope.
                let partialName =
                    QuickParse.GetPartialLongNameEx(getSourceLineStr su.Range.StartLine, su.Range.EndColumn - 1)

                List.isEmpty partialName.QualifyingIdents)
        |> Array.ofSeq

    /// Split symbol uses into cases that are easy to handle (via DeclaringEntity) and those that don't have a good DeclaringEntity
    let splitSymbolUses (symbolUses: FSharpSymbolUse[]) =
        symbolUses
        |> Array.partition (fun symbolUse ->
            let symbol = symbolUse.Symbol

            match symbol with
            | :? FSharpMemberOrFunctionOrValue as f ->
                match f.DeclaringEntity with
                | Some ent when ent.IsNamespace || ent.IsFSharpModule -> true
                | _ -> false
            | _ -> false)

    /// Given an 'open' statement, find fresh modules/namespaces referred to by that statement where there is some use of a revealed symbol
    /// in the scope of the 'open' is from that module.
    ///
    /// Performance will be roughly NumberOfOpenStatements x NumberOfSymbolUses
    let isOpenStatementUsed
        (symbolUses2: FSharpSymbolUse[])
        (symbolUsesRangesByDeclaringEntity: Dictionary<FSharpEntity, range list>)
        (usedModules: Dictionary<FSharpEntity, range list>)
        (openStatement: OpenStatement)
        =

        // Don't re-check modules whose symbols are already known to have been used
        let openedGroupsToExamine =
            openStatement.OpenedGroups
            |> List.choose (fun openedGroup ->
                let openedEntitiesToExamine =
                    openedGroup.OpenedModules
                    |> Array.filter (fun openedEntity ->
                        not (
                            usedModules.BagExistsValueForKey(
                                openedEntity.Entity,
                                fun scope -> rangeContainsRange scope openStatement.AppliedScope
                            )
                        ))

                match openedEntitiesToExamine with
                | [||] -> None
                | _ when openedEntitiesToExamine |> Array.exists (fun x -> not x.IsNestedAutoOpen) ->
                    Some
                        {
                            OpenedModules = openedEntitiesToExamine
                        }
                | _ -> None)

        // Find the opened groups that are used by some symbol use
        let newlyUsedOpenedGroups =
            openedGroupsToExamine
            |> List.filter (fun openedGroup ->
                openedGroup.OpenedModules
                |> Array.exists (fun openedEntity ->
                    symbolUsesRangesByDeclaringEntity.BagExistsValueForKey(
                        openedEntity.Entity,
                        fun symbolUseRange ->
                            rangeContainsRange openStatement.AppliedScope symbolUseRange
                            && Position.posGt symbolUseRange.Start openStatement.Range.End
                    )
                    ||

                    symbolUses2
                    |> Array.exists (fun symbolUse ->
                        rangeContainsRange openStatement.AppliedScope symbolUse.Range
                        && Position.posGt symbolUse.Range.Start openStatement.Range.End
                        && openedEntity.RevealedSymbolsContains symbolUse.Symbol)))

        // Return them as interim used entities
        let newlyOpenedModules =
            newlyUsedOpenedGroups
            |> List.collect (fun openedGroup -> openedGroup.OpenedModules |> List.ofArray)

        for openedModule in newlyOpenedModules do
            let scopes =
                match usedModules.TryGetValue openedModule.Entity with
                | true, scopes -> openStatement.AppliedScope :: scopes
                | _ -> [ openStatement.AppliedScope ]

            usedModules[openedModule.Entity] <- scopes

        not newlyOpenedModules.IsEmpty

    /// Incrementally filter out the open statements one by one. Filter those whose contents are referred to somewhere in the symbol uses.
    /// Async to allow cancellation.
    let rec filterOpenStatementsIncremental
        symbolUses2
        (symbolUsesRangesByDeclaringEntity: Dictionary<FSharpEntity, range list>)
        (openStatements: OpenStatement list)
        (usedModules: Dictionary<FSharpEntity, range list>)
        acc
        =
        async {
            match openStatements with
            | openStatement :: rest ->
                if isOpenStatementUsed symbolUses2 symbolUsesRangesByDeclaringEntity usedModules openStatement then
                    return! filterOpenStatementsIncremental symbolUses2 symbolUsesRangesByDeclaringEntity rest usedModules acc
                else
                    // The open statement has not been used, include it in the results
                    return!
                        filterOpenStatementsIncremental
                            symbolUses2
                            symbolUsesRangesByDeclaringEntity
                            rest
                            usedModules
                            (openStatement :: acc)
            | [] -> return List.rev acc
        }

    let entityHash =
        HashIdentity.FromFunctions (fun (x: FSharpEntity) -> x.GetEffectivelySameAsHash()) (fun x y -> x.IsEffectivelySameAs(y))

    /// Filter out the open statements whose contents are referred to somewhere in the symbol uses.
    /// Async to allow cancellation.
    let filterOpenStatements (symbolUses1: FSharpSymbolUse[], symbolUses2: FSharpSymbolUse[]) openStatements =
        async {
            // the key is a namespace or module, the value is a list of FSharpSymbolUse range of symbols defined in the
            // namespace or module. So, it's just symbol uses ranges grouped by namespace or module where they are _defined_.
            let symbolUsesRangesByDeclaringEntity =
                Dictionary<FSharpEntity, range list>(entityHash)

            for symbolUse in symbolUses1 do
                match symbolUse.Symbol with
                | :? FSharpMemberOrFunctionOrValue as f ->
                    match f.DeclaringEntity with
                    | Some entity when entity.IsNamespace || entity.IsFSharpModule ->
                        symbolUsesRangesByDeclaringEntity.BagAdd(entity, symbolUse.Range)
                    | _ -> ()
                | _ -> ()

            let! results =
                filterOpenStatementsIncremental
                    symbolUses2
                    symbolUsesRangesByDeclaringEntity
                    (List.ofArray openStatements)
                    (Dictionary(entityHash))
                    []

            return results |> List.map (fun os -> os.Range)
        }

    /// Get the open statements whose contents are not referred to anywhere in the symbol uses.
    /// Async to allow cancellation.
    let getUnusedOpens (checkFileResults: FSharpCheckFileResults, getSourceLineStr: int -> string) : Async<range list> =
        async {
            let! ct = Async.CancellationToken
            let symbolUses = checkFileResults.GetAllUsesOfAllSymbolsInFile(ct)
            let symbolUses = filterSymbolUses getSourceLineStr symbolUses
            let symbolUses = splitSymbolUses symbolUses
            let openStatements = getOpenStatements checkFileResults.OpenDeclarations
            return! filterOpenStatements symbolUses openStatements
        }

module SimplifyNames =
    type SimplifiableRange = { Range: range; RelativeName: string }

    let getPlidLength (plid: string list) =
        (plid |> List.sumBy String.length) + plid.Length

    let getSimplifiableNames (checkFileResults: FSharpCheckFileResults, getSourceLineStr: int -> string) =
        async {
            let result = ResizeArray()
            let! ct = Async.CancellationToken

            let symbolUses =
                checkFileResults.GetAllUsesOfAllSymbolsInFile(ct)
                |> Seq.choose (fun symbolUse ->
                    if symbolUse.IsFromOpenStatement || symbolUse.IsFromDefinition then
                        None
                    else
                        let lineStr = getSourceLineStr symbolUse.Range.StartLine
                        // for `System.DateTime.Now` it returns ([|"System"; "DateTime"|], "Now")
                        let partialName =
                            QuickParse.GetPartialLongNameEx(lineStr, symbolUse.Range.EndColumn - 1)
                        // `symbolUse.Range.Start` does not point to the start of plid, it points to start of `name`,
                        // so we have to calculate plid's start ourselves.
                        let plidStartCol =
                            symbolUse.Range.EndColumn
                            - partialName.PartialIdent.Length
                            - (getPlidLength partialName.QualifyingIdents)

                        if partialName.PartialIdent = "" || List.isEmpty partialName.QualifyingIdents then
                            None
                        else
                            Some(symbolUse, partialName.QualifyingIdents, plidStartCol, partialName.PartialIdent))
                |> Seq.groupBy (fun (symbolUse, _, plidStartCol, _) -> symbolUse.Range.StartLine, plidStartCol)
                |> Seq.map (fun (_, xs) -> xs |> Seq.maxBy (fun (symbolUse, _, _, _) -> symbolUse.Range.EndColumn))

            for symbolUse, plid, plidStartCol, name in symbolUses do
                let posAtStartOfName =
                    let r = symbolUse.Range

                    if r.StartLine = r.EndLine then
                        Position.mkPos r.StartLine (r.EndColumn - name.Length)
                    else
                        r.Start

                let getNecessaryPlid (plid: string list) : string list =
                    let rec loop (rest: string list) (current: string list) =
                        match rest with
                        | [] -> current
                        | headIdent :: restPlid ->
                            let res =
                                checkFileResults.IsRelativeNameResolvableFromSymbol(posAtStartOfName, current, symbolUse.Symbol)

                            if res then
                                current
                            else
                                loop restPlid (headIdent :: current)

                    loop (List.rev plid) []

                let necessaryPlid = getNecessaryPlid plid

                match necessaryPlid with
                | necessaryPlid when necessaryPlid = plid -> ()
                | necessaryPlid ->
                    let r = symbolUse.Range

                    let necessaryPlidStartCol =
                        r.EndColumn - name.Length - (getPlidLength necessaryPlid)

                    let unnecessaryRange =
                        withStartEnd (Position.mkPos r.StartLine plidStartCol) (Position.mkPos r.EndLine necessaryPlidStartCol) r

                    let relativeName = (String.concat "." plid) + "." + name

                    result.Add(
                        {
                            Range = unnecessaryRange
                            RelativeName = relativeName
                        }
                    )

            return (result :> seq<_>)
        }

module UnusedDeclarations =
    let isPotentiallyUnusedDeclaration (symbol: FSharpSymbol) : bool =
        match symbol with

        // Determining that a record, DU or module is used anywhere requires inspecting all their enclosed entities (fields, cases and func / vals)
        // for usages, which is too expensive to do. Hence we never gray them out.
        | :? FSharpEntity as e when
            e.IsFSharpRecord
            || e.IsFSharpUnion
            || e.IsInterface
            || e.IsFSharpModule
            || e.IsClass
            || e.IsNamespace
            ->
            false

        // FCS returns inconsistent results for override members; we're skipping these symbols.
        | :? FSharpMemberOrFunctionOrValue as f when
            f.IsOverrideOrExplicitInterfaceImplementation
            || f.IsBaseValue
            || f.IsConstructor
            ->
            false

        // Usage of DU case parameters does not give any meaningful feedback; we never gray them out.
        | :? FSharpParameter -> false
        | _ -> true

    let getUnusedDeclarationRanges (symbolsUses: seq<FSharpSymbolUse>) (isScript: bool) =
        let usages =
            let usages =
                symbolsUses
                |> Seq.choose (fun su ->
                    if not su.IsFromDefinition then
                        su.Symbol.DeclarationLocation
                    else
                        None)

            HashSet(usages)

        symbolsUses
        |> Seq.distinctBy (fun su -> su.Range) // Account for "hidden" uses, like a val in a member val definition. These aren't relevant
        |> Seq.choose (fun (su: FSharpSymbolUse) ->
            if
                su.IsFromDefinition
                && su.Symbol.DeclarationLocation.IsSome
                && (isScript || su.IsPrivateToFile)
                && not (su.Symbol.DisplayName.StartsWith "_")
                && isPotentiallyUnusedDeclaration su.Symbol
            then
                Some(su, usages.Contains su.Symbol.DeclarationLocation.Value)
            else
                None)
        |> Seq.groupBy (fun (defSu, _) -> defSu.Range)
        |> Seq.filter (fun (_, defSus) -> defSus |> Seq.forall (fun (_, isUsed) -> not isUsed))
        |> Seq.map (fun (m, _) -> m)

    let getUnusedDeclarations (checkFileResults: FSharpCheckFileResults, isScriptFile: bool) =
        async {
            let! ct = Async.CancellationToken
            let allSymbolUsesInFile = checkFileResults.GetAllUsesOfAllSymbolsInFile(ct)
            let unusedRanges = getUnusedDeclarationRanges allSymbolUsesInFile isScriptFile
            return unusedRanges
        }

module UnnecessaryParentheses =
    /// Represents the precedence of a binary expression.
    type Precedence =
        /// <-
        | Set

        /// :=
        | ColonEquals

        /// ,
        | Comma

        /// or, ||
        ///
        /// Refers to the exact operators or and ||.
        /// Instances with leading dots or question marks or trailing characters are parsed as Bar instead.
        | BarBar

        /// &, &&
        ///
        /// Refers to the exact operators & and &&.
        /// Instances with leading dots or question marks or trailing characters are parsed as Amp instead.
        | AmpAmp

        /// :?>
        | Downcast

        /// :>
        | Upcast

        /// =…
        | Eq

        /// |…
        | Bar

        /// &…
        | Amp

        /// $…
        | Dollar

        /// >…
        | Greater

        /// <…
        | Less

        /// !=…
        | BangEq

        /// ^…
        | Hat

        /// @…
        | At

        /// ::
        | Cons

        /// :?
        | TypeTest

        /// -…
        | Sub

        /// +…
        | Add

        /// %…
        | Mod

        /// /…
        | Div

        /// *…
        | Mul

        /// **…
        | Exp

        /// - x
        | UnaryPrefix

        /// f x
        | Apply

        /// -x, ! x
        | High

        // x.y
        | Dot

    module Precedence =
        let sameKind prec1 prec2 = prec1 = prec2

        let compare prec1 prec2 =
            match prec1, prec2 with
            | Dot, Dot -> 0
            | Dot, _ -> 1
            | _, Dot -> -1

            | High, High -> 0
            | High, _ -> 1
            | _, High -> -1

            | Apply, Apply -> 0
            | Apply, _ -> 1
            | _, Apply -> -1

            | UnaryPrefix, UnaryPrefix -> 0
            | UnaryPrefix, _ -> 1
            | _, UnaryPrefix -> -1

            | Exp, Exp -> 0
            | Exp, _ -> 1
            | _, Exp -> -1

            | (Mod | Div | Mul), (Mod | Div | Mul) -> 0
            | (Mod | Div | Mul), _ -> 1
            | _, (Mod | Div | Mul) -> -1

            | (Sub | Add), (Sub | Add) -> 0
            | (Sub | Add), _ -> 1
            | _, (Sub | Add) -> -1

            | TypeTest, TypeTest -> 0
            | TypeTest, _ -> 1
            | _, TypeTest -> -1

            | Cons, Cons -> 0
            | Cons, _ -> 1
            | _, Cons -> -1

            | (Hat | At), (Hat | At) -> 0
            | (Hat | At), _ -> 1
            | _, (Hat | At) -> -1

            | (Eq | Bar | Amp | Dollar | Greater | Less | BangEq), (Eq | Bar | Amp | Dollar | Greater | Less | BangEq) -> 0
            | (Eq | Bar | Amp | Dollar | Greater | Less | BangEq), _ -> 1
            | _, (Eq | Bar | Amp | Dollar | Greater | Less | BangEq) -> -1

            | (Downcast | Upcast), (Downcast | Upcast) -> 0
            | (Downcast | Upcast), _ -> 1
            | _, (Downcast | Upcast) -> -1

            | AmpAmp, AmpAmp -> 0
            | AmpAmp, _ -> 1
            | _, AmpAmp -> -1

            | BarBar, BarBar -> 0
            | BarBar, _ -> 1
            | _, BarBar -> -1

            | Comma, Comma -> 0
            | Comma, _ -> 1
            | _, Comma -> -1

            | ColonEquals, ColonEquals -> 0
            | ColonEquals, _ -> 1
            | _, ColonEquals -> -1

            | Set, Set -> 0

    /// Associativity/association.
    type Assoc =
        /// Non-associative or no association.
        | Non

        /// Left-associative or left-hand association.
        | Left

        /// Right-associative or right-hand association.
        | Right

    module Assoc =
        let ofPrecedence precedence =
            match precedence with
            | Set -> Non
            | ColonEquals -> Right
            | Comma -> Non
            | BarBar -> Left
            | AmpAmp -> Left
            | Upcast
            | Downcast -> Right
            | Eq
            | Bar
            | Amp
            | Dollar
            | Greater
            | Less
            | BangEq -> Left
            | At
            | Hat -> Right
            | Cons -> Right
            | TypeTest -> Non
            | Add
            | Sub -> Left
            | Mul
            | Div
            | Mod -> Left
            | Exp -> Right
            | UnaryPrefix -> Left
            | Apply -> Left
            | High -> Left
            | Dot -> Left

    module SynExpr =
        open FSharp.Compiler.SyntaxTrivia

        /// Matches if the given expression represents a high-precedence
        /// function application, e.g.,
        ///
        /// f x
        ///
        /// (+) x y
        [<return: Struct>]
        let (|HighPrecedenceApp|_|) expr =
            match expr with
            | SynExpr.App (isInfix = false; funcExpr = SynExpr.Ident _)
            | SynExpr.App (isInfix = false; funcExpr = SynExpr.LongIdent _)
            | SynExpr.App (isInfix = false; funcExpr = SynExpr.App(isInfix = false)) -> ValueSome HighPrecedenceApp
            | _ -> ValueNone

        module FuncExpr =
            /// Matches when the given funcExpr is a direct application
            /// of a symbolic operator, e.g., -, _not_ (~-).
            [<return: Struct>]
            let (|SymbolicOperator|_|) funcExpr =
                match funcExpr with
                | SynExpr.LongIdent(longDotId = SynLongIdent (trivia = trivia)) ->
                    let rec tryPick =
                        function
                        | [] -> ValueNone
                        | Some (IdentTrivia.OriginalNotation op) :: _ -> ValueSome op
                        | _ :: rest -> tryPick rest

                    tryPick trivia
                | _ -> ValueNone

        open System

        [<return: Struct>]
        let (|PrefixApp|_|) expr : Precedence voption =
            match expr with
            | SynExpr.App (isInfix = false; funcExpr = funcExpr & FuncExpr.SymbolicOperator op; argExpr = argExpr) ->
                if funcExpr.Range.IsAdjacentTo argExpr.Range then
                    ValueSome High
                else
                    assert (op.Length > 0)

                    match op[0] with
                    | '!'
                    | '~' -> ValueSome High
                    | _ -> ValueSome UnaryPrefix

            | _ -> ValueNone

        /// Tries to parse the given original notation as a symbolic infix operator.
        [<return: Struct>]
        let (|SymbolPrec|_|) (originalNotation: string) =
            // Trim any leading dots or question marks from the given symbolic operator.
            // Leading dots or question marks have no effect on operator precedence or associativity
            // with the exception of &, &&, and ||.
            let ignoredLeadingChars = ".?".AsSpan()
            let trimmed = originalNotation.AsSpan().TrimStart ignoredLeadingChars
            assert (trimmed.Length > 0)

            match trimmed[0], originalNotation with
            | _, ":=" -> ValueSome ColonEquals
            | _, ("||" | "or") -> ValueSome BarBar
            | _, ("&" | "&&") -> ValueSome AmpAmp
            | '|', _ -> ValueSome Bar
            | '&', _ -> ValueSome Amp
            | '<', _ -> ValueSome Less
            | '>', _ -> ValueSome Greater
            | '=', _ -> ValueSome Eq
            | '$', _ -> ValueSome Dollar
            | '!', _ when trimmed.Length > 1 && trimmed[1] = '=' -> ValueSome BangEq
            | '^', _ -> ValueSome Hat
            | '@', _ -> ValueSome At
            | _, "::" -> ValueSome Cons
            | '+', _ -> ValueSome Add
            | '-', _ -> ValueSome Sub
            | '/', _ -> ValueSome Div
            | '%', _ -> ValueSome Mod
            | '*', _ when trimmed.Length > 1 && trimmed[1] = '*' -> ValueSome Exp
            | '*', _ -> ValueSome Mul
            | _ -> ValueNone

        /// Any expressions in which the removal of parens would
        /// lead to something like the following that would be
        /// confused by the parser with a type parameter application:
        ///
        /// x<y>z
        ///
        /// x<y,y>z
        [<return: Struct>]
        let rec (|ConfusableWithTypeApp|_|) synExpr =
            match synExpr with
            | SynExpr.Paren(expr = ConfusableWithTypeApp)
            | SynExpr.App(funcExpr = ConfusableWithTypeApp)
            | SynExpr.App (isInfix = true; funcExpr = FuncExpr.SymbolicOperator (SymbolPrec Greater); argExpr = ConfusableWithTypeApp) ->
                ValueSome ConfusableWithTypeApp
            | SynExpr.App (isInfix = true; funcExpr = funcExpr & FuncExpr.SymbolicOperator (SymbolPrec Less); argExpr = argExpr) when
                argExpr.Range.IsAdjacentTo funcExpr.Range
                ->
                ValueSome ConfusableWithTypeApp
            | SynExpr.Tuple (exprs = exprs) ->
                let rec anyButLast =
                    function
                    | _ :: []
                    | [] -> ValueNone
                    | ConfusableWithTypeApp :: _ -> ValueSome ConfusableWithTypeApp
                    | _ :: tail -> anyButLast tail

                anyButLast exprs
            | _ -> ValueNone

        /// Matches when the expression represents the infix application of a symbolic operator.
        ///
        /// (x λ y) ρ z
        ///
        /// x λ (y ρ z)
        [<return: Struct>]
        let (|InfixApp|_|) synExpr : struct (Precedence * Assoc) voption =
            match synExpr with
            | SynExpr.App(funcExpr = SynExpr.App (isInfix = true; funcExpr = FuncExpr.SymbolicOperator (SymbolPrec prec))) ->
                ValueSome(prec, Right)
            | SynExpr.App (isInfix = true; funcExpr = FuncExpr.SymbolicOperator (SymbolPrec prec)) -> ValueSome(prec, Left)
            | SynExpr.Upcast _ -> ValueSome(Upcast, Left)
            | SynExpr.Downcast _ -> ValueSome(Upcast, Left)
            | SynExpr.TypeTest _ -> ValueSome(TypeTest, Left)
            | _ -> ValueNone

        /// Matches if the two expressions refer to the same object.
        [<return: Struct>]
        let inline (|Is|_|) (inner1: SynExpr) (inner2: SynExpr) =
            if obj.ReferenceEquals(inner1, inner2) then
                ValueSome Is
            else
                ValueNone

        [<return: Struct>]
        let (|Outer|_|) inner outer : struct (Precedence * Assoc) voption =
            match outer with
            | SynExpr.Tuple(exprs = SynExpr.Paren(expr = Is inner) :: _) -> ValueSome(Comma, Left)
            | SynExpr.Tuple _ -> ValueSome(Comma, Right)
            | InfixApp (Cons, side) -> ValueSome(Cons, side)
            | SynExpr.Assert _
            | SynExpr.Lazy _
            | SynExpr.InferredUpcast _
            | SynExpr.InferredDowncast _ -> ValueSome(Apply, Non)
            | PrefixApp prec -> ValueSome(prec, Left)
            | InfixApp (prec, side) -> ValueSome(prec, side)
            | SynExpr.App(argExpr = SynExpr.ComputationExpr _) -> ValueSome(UnaryPrefix, Left)
            | SynExpr.App(funcExpr = SynExpr.Paren(expr = SynExpr.App _)) -> ValueSome(Apply, Left)
            | SynExpr.App _ -> ValueSome(Apply, Non)
            | SynExpr.DotSet(targetExpr = SynExpr.Paren(expr = Is inner)) -> ValueSome(Dot, Left)
            | SynExpr.DotSet(rhsExpr = SynExpr.Paren(expr = Is inner)) -> ValueSome(Set, Right)
            | SynExpr.DotIndexedSet(objectExpr = SynExpr.Paren(expr = Is inner))
            | SynExpr.DotNamedIndexedPropertySet(targetExpr = SynExpr.Paren(expr = Is inner)) -> ValueSome(Dot, Left)
            | SynExpr.DotIndexedSet(valueExpr = SynExpr.Paren(expr = Is inner))
            | SynExpr.DotNamedIndexedPropertySet(rhsExpr = SynExpr.Paren(expr = Is inner)) -> ValueSome(Set, Right)
            | SynExpr.LongIdentSet(expr = SynExpr.Paren(expr = Is inner)) -> ValueSome(Set, Right)
            | SynExpr.Set _ -> ValueSome(Set, Non)
            | SynExpr.DotGet _ -> ValueSome(Dot, Left)
            | SynExpr.DotIndexedGet(objectExpr = SynExpr.Paren(expr = Is inner)) -> ValueSome(Dot, Left)
            | _ -> ValueNone

        [<return: Struct>]
        let (|Inner|_|) expr : Precedence voption =
            match expr with
            | SynExpr.Tuple(isStruct = false) -> ValueSome Comma
            | SynExpr.DotGet _
            | SynExpr.DotIndexedGet _ -> ValueSome Dot
            | PrefixApp prec -> ValueSome prec
            | InfixApp (prec, _) -> ValueSome prec
            | SynExpr.App _
            | SynExpr.Assert _
            | SynExpr.Lazy _
            | SynExpr.For _
            | SynExpr.ForEach _
            | SynExpr.While _
            | SynExpr.Do _
            | SynExpr.New _
            | SynExpr.InferredUpcast _
            | SynExpr.InferredDowncast _ -> ValueSome Apply
            | SynExpr.DotIndexedSet _
            | SynExpr.DotNamedIndexedPropertySet _
            | SynExpr.DotSet _ -> ValueSome Set
            | _ -> ValueNone

        let parenthesesNeededBetween outer inner =
            match outer, inner with
            | ConfusableWithTypeApp, _ -> true

            | Outer inner (outerPrecedence, side), Inner innerPrecedence ->
                match Precedence.compare outerPrecedence innerPrecedence with
                | 0 ->
                    match side, Assoc.ofPrecedence innerPrecedence with
                    | Non, _
                    | _, Non
                    | Left, Right -> true
                    | Right, Right
                    | Left, Left -> false
                    | Right, Left ->
                        not (Precedence.sameKind outerPrecedence innerPrecedence)
                        || match innerPrecedence with
                           | Div
                           | Mod
                           | Sub -> true
                           | _ -> false
                | c -> c > 0

            | Outer inner (_, Right), (SynExpr.Sequential _ | SynExpr.LetOrUse(trivia = { InKeyword = None })) -> true
            | Outer inner (_, Right), _ -> false

            // (^a : (static member M : ^b -> ^c) x)
            | _, SynExpr.TraitCall _ -> true

            | SynExpr.WhileBang(whileExpr = SynExpr.Paren (expr = whileExpr)), SynExpr.Typed _
            | SynExpr.While(whileExpr = SynExpr.Paren (expr = whileExpr)), SynExpr.Typed _ -> obj.ReferenceEquals(whileExpr, inner)

            | SynExpr.Typed _, SynExpr.Typed _
            | SynExpr.For _, SynExpr.Typed _
            | SynExpr.ArrayOrList _, SynExpr.Typed _
            | SynExpr.ArrayOrListComputed _, SynExpr.Typed _
            | SynExpr.IndexRange _, SynExpr.Typed _
            | SynExpr.IndexFromEnd _, SynExpr.Typed _
            | SynExpr.ComputationExpr _, SynExpr.Typed _
            | SynExpr.Lambda _, SynExpr.Typed _
            | SynExpr.Assert _, SynExpr.Typed _
            | SynExpr.App _, SynExpr.Typed _
            | SynExpr.Lazy _, SynExpr.Typed _
            | SynExpr.LongIdentSet _, SynExpr.Typed _
            | SynExpr.DotSet _, SynExpr.Typed _
            | SynExpr.Set _, SynExpr.Typed _
            | SynExpr.DotIndexedSet _, SynExpr.Typed _
            | SynExpr.NamedIndexedPropertySet _, SynExpr.Typed _
            | SynExpr.Upcast _, SynExpr.Typed _
            | SynExpr.Downcast _, SynExpr.Typed _
            | SynExpr.AddressOf _, SynExpr.Typed _
            | SynExpr.JoinIn _, SynExpr.Typed _ -> true

            | _, SynExpr.Paren _
            | _, SynExpr.Quote _
            | _, SynExpr.Const _
            | _, SynExpr.Typed _
            | _, SynExpr.Tuple(isStruct = true)
            | _, SynExpr.AnonRecd _
            | _, SynExpr.ArrayOrList _
            | _, SynExpr.Record _
            | _, SynExpr.ObjExpr _
            | _, SynExpr.ArrayOrListComputed _
            | _, SynExpr.ComputationExpr _
            | _, SynExpr.TypeApp _
            | _, SynExpr.Ident _
            | _, SynExpr.LongIdent _
            | _, SynExpr.DotGet _
            | _, SynExpr.DotLambda _
            | _, SynExpr.DotIndexedGet _
            | _, SynExpr.Null _
            | _, SynExpr.AddressOf _
            | _, SynExpr.InterpolatedString _ -> false

            | SynExpr.Paren(rightParenRange = Some _), _
            | SynExpr.Quote _, _
            | SynExpr.Typed _, _
            | SynExpr.AnonRecd _, _
            | SynExpr.Record _, _
            | SynExpr.ObjExpr _, _
            | SynExpr.While _, _
            | SynExpr.WhileBang _, _
            | SynExpr.For _, _
            | SynExpr.ForEach _, _
            | SynExpr.Lambda _, _
            | SynExpr.MatchLambda _, _
            | SynExpr.Match _, _
            | SynExpr.MatchBang _, _
            | SynExpr.LetOrUse _, _
            | SynExpr.LetOrUseBang _, _
            | SynExpr.Do _, _
            | SynExpr.DoBang _, _
            | SynExpr.YieldOrReturn _, _
            | SynExpr.YieldOrReturnFrom _, _
            | SynExpr.IfThenElse _, _
            | SynExpr.TryWith _, _
            | SynExpr.TryFinally _, _
            | SynExpr.InterpolatedString _, _ -> false

            | _ -> true

    module SynPat =
        let parenthesesNeededBetween outer inner =
            match outer, inner with
            // (x :: xs) :: ys
            | SynPat.ListCons(lhsPat = SynPat.Paren (pat = lhs)), SynPat.ListCons _ -> obj.ReferenceEquals(lhs, inner)

            // A as (B | C)
            // A as (B & C)
            // x as (y, z)
            | SynPat.As(rhsPat = SynPat.Paren (pat = rhs)), SynPat.Or _
            | SynPat.As(rhsPat = SynPat.Paren (pat = rhs)), SynPat.Ands _
            | SynPat.As(rhsPat = SynPat.Paren (pat = rhs)), SynPat.Tuple(isStruct = false) -> obj.ReferenceEquals(rhs, inner)

            // (A | B) :: xs
            // (A & B) :: xs
            // (x as y) :: xs
            | SynPat.ListCons _, SynPat.Or _
            | SynPat.ListCons _, SynPat.Ands _
            | SynPat.ListCons _, SynPat.As _

            // Pattern (x : int)
            // Pattern ([<Attr>] x)
            // Pattern (:? int)
            // Pattern (A :: _)
            // Pattern (A | B)
            // Pattern (A & B)
            // Pattern (A as B)
            // Pattern (A, B)
            // Pattern1 (Pattern2 (x = A))
            // Pattern1 (Pattern2 x y)
            | SynPat.LongIdent _, SynPat.Typed _
            | SynPat.LongIdent _, SynPat.Attrib _
            | SynPat.LongIdent _, SynPat.IsInst _
            | SynPat.LongIdent _, SynPat.ListCons _
            | SynPat.LongIdent _, SynPat.Or _
            | SynPat.LongIdent _, SynPat.Ands _
            | SynPat.LongIdent _, SynPat.As _
            | SynPat.LongIdent _, SynPat.Tuple(isStruct = false)
            | SynPat.LongIdent _, SynPat.LongIdent(argPats = SynArgPats.NamePatPairs _)
            | SynPat.LongIdent _, SynPat.LongIdent(argPats = SynArgPats.Pats (_ :: _))

            // A | (B as C)
            // A & (B as C)
            // A, (B as C)
            | SynPat.Or _, SynPat.As _
            | SynPat.Ands _, SynPat.As _
            | SynPat.Tuple _, SynPat.As _

            // x, (y, z)
            | SynPat.Tuple _, SynPat.Tuple(isStruct = false)

            // A, (B | C)
            // A & (B | C)
            | SynPat.Tuple _, SynPat.Or _
            | SynPat.Ands _, SynPat.Or _

            // (x : int) | x
            // (x : int) & y
            | SynPat.Or _, SynPat.Typed _
            | SynPat.Ands _, SynPat.Typed _

            // let () = …
            // member _.M() = …
            | SynPat.Paren _, SynPat.Const (SynConst.Unit, _)
            | SynPat.LongIdent _, SynPat.Const (SynConst.Unit, _) -> true

            | _, SynPat.Const _
            | _, SynPat.Wild _
            | _, SynPat.Named _
            | _, SynPat.Typed _
            | _, SynPat.LongIdent(argPats = SynArgPats.Pats [])
            | _, SynPat.Tuple(isStruct = true)
            | _, SynPat.Paren _
            | _, SynPat.ArrayOrList _
            | _, SynPat.Record _
            | _, SynPat.Null _
            | _, SynPat.OptionalVal _
            | _, SynPat.IsInst _
            | _, SynPat.QuoteExpr _

            | SynPat.Or _, _
            | SynPat.ListCons _, _
            | SynPat.Ands _, _
            | SynPat.As _, _
            | SynPat.LongIdent _, _
            | SynPat.Tuple _, _
            | SynPat.Paren _, _
            | SynPat.ArrayOrList _, _
            | SynPat.Record _, _ -> false

            | _ -> true

    /// Represents the range of a control-flow construct or part thereof.
    [<NoComparison; NoEquality>]
    type ControlFlowPart =
        /// match … with … -> …
        ///
        /// match! … with … -> …
        ///
        /// function … -> …
        ///
        /// try … with … -> …
        ///
        /// try … finally …
        | MatchOrTry of range

        /// |, ->, finally, with (of try-with)
        | BarOrArrowOrFinallyOrWith of range

        /// if … then … else …
        | IfThenElse of range

        /// then, else
        | ThenOrElse of range

    module ControlFlowPart =
        /// A comparer that considers the first control flow part
        /// equal to the second if both end on the same line,
        /// the first ends before the second begins on that line,
        /// and both are of kinds that would be syntactically ambiguous
        /// when in such a position and unseparated by parentheses.
        /// Falls back to range comparison otherwise.
        let comparer =
            { new IComparer<ControlFlowPart> with
                member _.Compare(x, y) =
                    match x, y with
                    | MatchOrTry exprRange, BarOrArrowOrFinallyOrWith delimiterRange
                    | IfThenElse exprRange, ThenOrElse delimiterRange when
                        exprRange.EndLine = delimiterRange.EndLine
                        && exprRange.EndColumn < delimiterRange.StartColumn
                        ->
                        0

                    | (MatchOrTry x | BarOrArrowOrFinallyOrWith x | IfThenElse x | ThenOrElse x),
                      (MatchOrTry y | BarOrArrowOrFinallyOrWith y | IfThenElse y | ThenOrElse y) -> Range.rangeOrder.Compare(x, y)
            }

    open System

#if !NET7_0_OR_GREATER
    [<Sealed; AbstractClass; Extension>]
    type ReadOnlySpanExtensions =
        [<Extension>]
        static member IndexOfAnyExcept(span: ReadOnlySpan<char>, value0: char, value1: char) =
            let mutable i = 0
            let mutable found = false

            while not found && i < span.Length do
                let c = span[i]

                if c <> value0 && c <> value1 then
                    found <- true
                else
                    i <- i + 1

            if found then i else -1

        [<Extension>]
        static member IndexOfAnyExcept(span: ReadOnlySpan<char>, values: ReadOnlySpan<char>) =
            let mutable i = 0
            let mutable found = false

            while not found && i < span.Length do
                if values.IndexOf span[i] < 0 then
                    found <- true
                else
                    i <- i + 1

            if found then i else -1

        [<Extension>]
        static member LastIndexOfAnyInRange(span: ReadOnlySpan<char>, lowInclusive: char, highInclusive: char) =
            let mutable i = span.Length - 1
            let mutable found = false
            let range = highInclusive - lowInclusive

            while not found && i >= 0 do
                if span[i] - lowInclusive <= range then
                    found <- true
                else
                    i <- i - 1

            if found then i else -1
#endif

    let getUnnecessaryParentheses (getSourceLineStr: int -> string) (parsedInput: ParsedInput) : Async<range seq> =
        async {
            let ranges = HashSet Range.comparer

            let visitor =
                let controlFlowConstructParts = SortedDictionary ControlFlowPart.comparer
                let seen = HashSet EqualityComparer<obj>.Default

                // Add the key and value to the dictionary, wrapping the value in a set.
                // If the key already exists, add the value to the existing set.
                let add key value (d: SortedDictionary<_, _>) =
                    match d.TryGetValue key with
                    | false, _ ->
                        let values = HashSet Range.comparer
                        ignore (values.Add value)
                        d.Add(key, values)

                    | true, values -> ignore (values.Add value)

                // Indicates whether the parentheses with the given range
                // enclose an expression whose indentation would be invalid
                // in context if it were not surrounded by parentheses.
                let containsSensitiveIndentation (parenRange: range) =
                    let startLine = parenRange.StartLine
                    let endLine = parenRange.EndLine

                    if startLine = endLine then
                        false
                    else
                        let rec loop offsides lineNo startCol =
                            if lineNo <= endLine then
                                let line = getSourceLineStr lineNo

                                match offsides with
                                | ValueNone ->
                                    let i = line.AsSpan(startCol).IndexOfAnyExcept(' ', ')')

                                    if i >= 0 then
                                        loop (ValueSome(i + startCol)) (lineNo + 1) 0
                                    else
                                        loop offsides (lineNo + 1) 0

                                | ValueSome offsidesCol ->
                                    let i = line.AsSpan(0, min offsidesCol line.Length).IndexOfAnyExcept(' ', ')')

                                    if i >= 0 && i < offsidesCol then
                                        let slice = line.AsSpan(i, min (offsidesCol - i) (line.Length - i))
                                        let j = slice.IndexOfAnyExcept("*/%-+:^@><=!|0$.?".AsSpan())

                                        i + (if j >= 0 && slice[j] = ' ' then j else 0) < offsidesCol - 1
                                        || loop offsides (lineNo + 1) 0
                                    else
                                        loop offsides (lineNo + 1) 0
                            else
                                false

                        loop ValueNone startLine (parenRange.StartColumn + 1)

                { new SyntaxVisitorBase<obj>() with
                    member _.VisitExpr(path, _, defaultTraverse, expr) =
                        let (|StartsWithSymbol|_|) =
                            let (|TextStartsWith|) (m: range) =
                                let line = getSourceLineStr m.StartLine
                                line[m.StartColumn]

                            let (|StartsWith|) (s: string) = s[0]

                            function
                            | SynExpr.Quote _
                            | SynExpr.InterpolatedString _
                            | SynExpr.Const (SynConst.String(synStringKind = SynStringKind.Verbatim), _)
                            | SynExpr.Const (SynConst.Byte _, TextStartsWith '+')
                            | SynExpr.Const (SynConst.UInt16 _, TextStartsWith '+')
                            | SynExpr.Const (SynConst.UInt32 _, TextStartsWith '+')
                            | SynExpr.Const (SynConst.UInt64 _, TextStartsWith '+')
                            | SynExpr.Const (SynConst.UIntPtr _, TextStartsWith '+')
                            | SynExpr.Const (SynConst.SByte _, TextStartsWith ('-' | '+'))
                            | SynExpr.Const (SynConst.Int16 _, TextStartsWith ('-' | '+'))
                            | SynExpr.Const (SynConst.Int32 _, TextStartsWith ('-' | '+'))
                            | SynExpr.Const (SynConst.Int64 _, TextStartsWith ('-' | '+'))
                            | SynExpr.Const (SynConst.IntPtr _, TextStartsWith ('-' | '+'))
                            | SynExpr.Const (SynConst.Decimal _, TextStartsWith ('-' | '+'))
                            | SynExpr.Const (SynConst.Double _, TextStartsWith ('-' | '+'))
                            | SynExpr.Const (SynConst.Single _, TextStartsWith ('-' | '+'))
                            | SynExpr.Const (SynConst.Measure (_, TextStartsWith ('-' | '+'), _, _), _)
                            | SynExpr.Const (SynConst.UserNum (StartsWith ('-' | '+'), _), _) -> Some StartsWithSymbol
                            | _ -> None

                        let (|DotSafeNumericLiteral|_|) =
                            /// 1l, 0b1, 1e10, 1d…
                            let (|TextContainsLetter|_|) (m: range) =
                                let line = getSourceLineStr m.StartLine
                                let span = line.AsSpan(m.StartColumn, m.EndColumn - m.StartColumn)

                                if span.LastIndexOfAnyInRange('A', 'z') >= 0 then
                                    Some TextContainsLetter
                                else
                                    None

                            // 1.0…
                            let (|TextEndsWithNumber|_|) (m: range) =
                                let line = getSourceLineStr m.StartLine
                                let span = line.AsSpan(m.StartColumn, m.EndColumn - m.StartColumn)

                                if Char.IsDigit span[span.Length - 1] then
                                    Some TextEndsWithNumber
                                else
                                    None

                            function
                            | SynExpr.Const (SynConst.Byte _, _)
                            | SynExpr.Const (SynConst.UInt16 _, _)
                            | SynExpr.Const (SynConst.UInt32 _, _)
                            | SynExpr.Const (SynConst.UInt64 _, _)
                            | SynExpr.Const (SynConst.UIntPtr _, _)
                            | SynExpr.Const (SynConst.SByte _, _)
                            | SynExpr.Const (SynConst.Int16 _, _)
                            | SynExpr.Const (SynConst.Int32 _, TextContainsLetter)
                            | SynExpr.Const (SynConst.Int64 _, _)
                            | SynExpr.Const (SynConst.IntPtr _, _)
                            | SynExpr.Const (SynConst.Decimal _, _)
                            | SynExpr.Const (SynConst.Double _, (TextContainsLetter | TextEndsWithNumber))
                            | SynExpr.Const (SynConst.Single _, _)
                            | SynExpr.Const (SynConst.Measure _, _)
                            | SynExpr.Const (SynConst.UserNum _, _) -> Some DotSafeNumericLiteral
                            | _ -> None

                        match expr, path with
                        // Normally, we don't need parentheses around control flow construct input or
                        // result expressions, e.g.,
                        //
                        //     if (2 + 2 = 5) then (…)                       → if 2 + 2 = 5 then …
                        //     match (…) with … when (…) -> (…) | (…) -> (…) → match … with … when … -> … | … -> …
                        //
                        // Given a parenthesized control flow construct nested inside of another
                        // construct of like kind, we can always remove the parentheses _unless_
                        // the inner construct is on the same line as any of the outer construct's
                        // delimiters (then, else, |, ->, finally, with (of try-with)) and, if the parentheses were removed,
                        // the inner construct would syntactically adhere to that delimiter.
                        //
                        // Note that, owing to precedence rules, the inner construct
                        // could be syntactically nested arbitrarily deeply
                        // and need not be top-level to be problematic. Consider:
                        //
                        //     // Second argument of an infix operator.
                        //     if … (id <| if … then … else …) then …
                        //     match … with … -> (id <| match … with … -> … | … -> …) | … -> …
                        //
                        //     // Last element of a tuple.
                        //     if … (…, if … then … else …) then …
                        //     match … with … -> (…, match … with … -> … | … -> …) | … -> …
                        //
                        //     // Result of yet another if-then-else.
                        //     if … (if … then … else if … then … else …) then …
                        //
                        //     // Result of a match.
                        //     if … (match … with … -> if … then … else …) then …
                        //
                        //     // Etc., etc., etc.
                        | SynExpr.Paren (expr = inner; rightParenRange = Some _; range = range),
                          SyntaxNode.SynExpr (SynExpr.IfThenElse (trivia = trivia) as outer) :: _ ->
                            controlFlowConstructParts |> add (ThenOrElse trivia.ThenKeyword) range

                            match trivia.ElseKeyword with
                            | Some elseKeyword -> controlFlowConstructParts |> add (ThenOrElse elseKeyword) range
                            | None -> ()

                            if not (SynExpr.parenthesesNeededBetween outer inner) then
                                ignore (ranges.Add range)

                        // Try-finally has a similar problem.
                        | SynExpr.Paren (expr = inner; rightParenRange = Some _; range = range),
                          SyntaxNode.SynExpr (SynExpr.TryFinally (trivia = trivia) as outer) :: _ ->
                            controlFlowConstructParts
                            |> add (BarOrArrowOrFinallyOrWith trivia.FinallyKeyword) range

                            if not (SynExpr.parenthesesNeededBetween outer inner) then
                                ignore (ranges.Add range)

                        | SynExpr.Paren (range = range),
                          SyntaxNode.SynExpr (tryWith & SynExpr.TryWith (withCases = clauses; trivia = trivia)) :: _
                        | SynExpr.Paren (range = range),
                          SyntaxNode.SynMatchClause _ :: SyntaxNode.SynExpr (tryWith & SynExpr.TryWith (withCases = clauses; trivia = trivia)) :: _ ->
                            if seen.Add tryWith then
                                controlFlowConstructParts
                                |> add (BarOrArrowOrFinallyOrWith trivia.WithKeyword) range

                                for SynMatchClause (trivia = trivia) in clauses do
                                    match trivia.BarRange with
                                    | Some barRange -> controlFlowConstructParts |> add (BarOrArrowOrFinallyOrWith barRange) range
                                    | None -> ()

                                    match trivia.ArrowRange with
                                    | Some arrowRange -> controlFlowConstructParts |> add (BarOrArrowOrFinallyOrWith arrowRange) range
                                    | None -> ()

                            ignore (ranges.Add range)

                        // Match-clause-having constructs do, too.
                        | SynExpr.Paren (range = range),
                          SyntaxNode.SynMatchClause _ :: SyntaxNode.SynExpr (matchExpr & SynExpr.Match (clauses = clauses)) :: _
                        | SynExpr.Paren (range = range),
                          SyntaxNode.SynMatchClause _ :: SyntaxNode.SynExpr (matchExpr & SynExpr.MatchLambda (matchClauses = clauses)) :: _
                        | SynExpr.Paren (range = range),
                          SyntaxNode.SynMatchClause _ :: SyntaxNode.SynExpr (matchExpr & SynExpr.MatchBang (clauses = clauses)) :: _
                        | SynExpr.Paren (range = range),
                          SyntaxNode.SynExpr (SynExpr.YieldOrReturn _) :: SyntaxNode.SynMatchClause _ :: SyntaxNode.SynExpr (matchExpr & SynExpr.MatchBang (clauses = clauses)) :: _
                        | SynExpr.Paren (range = range),
                          SyntaxNode.SynExpr (SynExpr.YieldOrReturnFrom _) :: SyntaxNode.SynMatchClause _ :: SyntaxNode.SynExpr (matchExpr & SynExpr.MatchBang (clauses = clauses)) :: _ ->
                            if seen.Add matchExpr then
                                for SynMatchClause (trivia = trivia) in clauses do
                                    match trivia.BarRange with
                                    | Some barRange -> controlFlowConstructParts |> add (BarOrArrowOrFinallyOrWith barRange) range
                                    | None -> ()

                                    match trivia.ArrowRange with
                                    | Some arrowRange -> controlFlowConstructParts |> add (BarOrArrowOrFinallyOrWith arrowRange) range
                                    | None -> ()

                            ignore (ranges.Add range)

                        // If this if-then-else is nested inside of another
                        // and is on the same line as a then or else from the outer that
                        // it would directly precede and to which it would adhere
                        // if the parentheses were removed, the parentheses must stay.
                        | SynExpr.IfThenElse (range = range), _ ->
                            match controlFlowConstructParts.TryGetValue(IfThenElse range) with
                            | true, parenRanges ->
                                for parenRange in parenRanges do
                                    if Range.rangeContainsRange parenRange range then
                                        ignore (ranges.Remove parenRange)

                            | false, _ -> ()

                        // If this control flow construct is nested inside of another
                        // and is on the same line as a delimiter from the outer that
                        // it would directly precede and to which it would adhere
                        // if the parentheses were removed, the parentheses must stay.
                        | SynExpr.TryFinally (range = range), _
                        | SynExpr.Match (range = range), _
                        | SynExpr.MatchLambda (range = range), _
                        | SynExpr.MatchBang (range = range), _
                        | SynExpr.TryWith (range = range), _ ->
                            match controlFlowConstructParts.TryGetValue(MatchOrTry range) with
                            | true, parenRanges ->
                                for parenRange in parenRanges do
                                    if Range.rangeContainsRange parenRange range then
                                        ignore (ranges.Remove parenRange)

                            | false, _ -> ()

                        // Always need parens for trait calls, e.g.,
                        //
                        //     let inline f x = (^a : (static member Parse : string -> ^a) x)
                        | SynExpr.Paren(expr = SynExpr.TraitCall _), _ -> ()

                        // Parens are required here if the parenthesized expression
                        // would be invalid without its parentheses, e.g.,
                        //
                        //     let x = (x
                        //           + y)
                        | SynExpr.Paren (rightParenRange = Some _; range = parenRange), SyntaxNode.SynBinding _ :: _ when
                            containsSensitiveIndentation parenRange
                            ->
                            ()

                        // Parens are otherwise never required for bindings or for top-level expressions:
                        //
                        //     let x = (…)
                        //     _.member X = (…)
                        //     (printfn "Hello, world.")
                        | SynExpr.Paren (rightParenRange = Some _; range = range), SyntaxNode.SynBinding _ :: _
                        | SynExpr.Paren (rightParenRange = Some _; range = range), SyntaxNode.SynModule _ :: _ -> ignore (ranges.Add range)

                        // A high-precedence function application before a prefix op
                        // before another expression that starts with a symbol.
                        //
                        //     id -(-x)
                        //     id ~~~(-1y)
                        //     id -($"")
                        //     id -(@"")
                        //     id -(<@ () @>)
                        //     let (~+) _ = true in assert +($"{true}")
                        | SynExpr.Paren(expr = SynExpr.PrefixApp _ | StartsWithSymbol),
                          SyntaxNode.SynExpr (SynExpr.App _) :: SyntaxNode.SynExpr (SynExpr.HighPrecedenceApp | SynExpr.Assert _ | SynExpr.InferredUpcast _ | SynExpr.InferredDowncast _) :: _ ->
                            ()

                        // Parens are never required around suffixed or infixed numeric literals, e.g.,
                        //
                        //     (1l).ToString()
                        //     (1uy).ToString()
                        //     (0b1).ToString()
                        //     (1e10).ToString()
                        //     (1.0).ToString()
                        | SynExpr.Paren (expr = DotSafeNumericLiteral; range = range), _ -> ignore (ranges.Add range)

                        // Parens are required around bare decimal ints or doubles ending in dots, e.g.,
                        //
                        //     (1).ToString()
                        //     (1.).ToString()
                        | SynExpr.Paren(expr = SynExpr.Const(constant = SynConst.Int32 _ | SynConst.Double _)),
                          SyntaxNode.SynExpr (SynExpr.DotGet _) :: _ -> ()

                        // Parens are required in
                        //
                        //     join … on (… = …)
                        | SynExpr.Paren(expr = SynExpr.App _),
                          SyntaxNode.SynExpr (SynExpr.App _) :: SyntaxNode.SynExpr (SynExpr.JoinIn _) :: _ -> ()

                        // We can't remove parens when they're required for fluent calls:
                        //
                        //     x.M(y).N z
                        //     x.M(y).[z]
                        //     x.M(y)[z]
                        | SynExpr.Paren _,
                          SyntaxNode.SynExpr (SynExpr.App _) :: SyntaxNode.SynExpr (SynExpr.DotGet _ | SynExpr.DotIndexedGet _) :: _
                        | SynExpr.Paren _,
                          SyntaxNode.SynExpr (SynExpr.App _) :: SyntaxNode.SynExpr (SynExpr.App(argExpr = SynExpr.ArrayOrListComputed(isArray = false))) :: _ ->
                            ()

                        // Outer right:
                        //
                        //     (x :: y) :: z
                        //     (x + y) :: z
                        //     (x * y) :: z
                        //     …
                        | SynExpr.Paren (expr = inner; rightParenRange = Some _; range = range),
                          SyntaxNode.SynExpr (SynExpr.Tuple (isStruct = false; exprs = [ SynExpr.Paren _; _ ])) :: SyntaxNode.SynExpr (SynExpr.App(isInfix = true) as outer) :: _ ->
                            if
                                not (SynExpr.parenthesesNeededBetween outer inner)
                                && not (containsSensitiveIndentation range)
                            then
                                ignore (ranges.Add range)

                        // Outer left:
                        //
                        //     x :: (y :: z)
                        //     x :: (y + z)
                        //     x :: (y * z)
                        //     …
                        | SynExpr.Paren (expr = inner; rightParenRange = Some _; range = range) as argExpr,
                          SyntaxNode.SynExpr (SynExpr.Tuple (isStruct = false; exprs = [ _; SynExpr.Paren _ ])) :: SyntaxNode.SynExpr (SynExpr.App(isInfix = true) as outer) :: _ ->
                            if
                                not (
                                    SynExpr.parenthesesNeededBetween
                                        (SynExpr.App(ExprAtomicFlag.NonAtomic, false, outer, argExpr, outer.Range))
                                        inner
                                )
                                && not (containsSensitiveIndentation range)
                            then
                                ignore (ranges.Add range)

                        // Ordinary nested exprs.
                        | SynExpr.Paren (expr = inner; rightParenRange = Some _; range = range), SyntaxNode.SynExpr outer :: _ when
                            not (SynExpr.parenthesesNeededBetween outer inner)
                            && not (containsSensitiveIndentation range)
                            ->
                            ignore (ranges.Add range)

                        | _ -> ()

                        defaultTraverse expr

                    member _.VisitPat(path, defaultTraverse, pat) =
                        match pat, path with
                        // Parens are needed in:
                        //
                        //     let (Pattern …) = …
                        //     let! (x: …) = …
                        //     and! (x: …) = …
                        //     use! (x: …) = …
                        //     _.member M(x: …) = …
                        //     match … with (x: …) -> …
                        //     function (x: …) -> …
                        //     fun (x, y, …) -> …
                        //     fun (x: …) -> …
                        //     fun (Pattern …) -> …
                        | SynPat.Paren _, SyntaxNode.SynExpr (SynExpr.LetOrUseBang(pat = SynPat.Paren(pat = SynPat.Typed _))) :: _
                        | SynPat.Paren _, SyntaxNode.SynMatchClause (SynMatchClause(pat = SynPat.Paren(pat = SynPat.Typed _))) :: _
                        | SynPat.Paren(pat = SynPat.LongIdent _), SyntaxNode.SynBinding _ :: _
                        | SynPat.Paren(pat = SynPat.LongIdent _), SyntaxNode.SynExpr (SynExpr.Lambda _) :: _
                        | SynPat.Paren _, SyntaxNode.SynExpr (SynExpr.Lambda(args = SynSimplePats.SimplePats(pats = _ :: _ :: _))) :: _
                        | SynPat.Paren _,
                          SyntaxNode.SynExpr (SynExpr.Lambda(args = SynSimplePats.SimplePats(pats = [ SynSimplePat.Typed _ ]))) :: _ -> ()

                        // () is parsed as this in certain cases…
                        //
                        //     let () = …
                        //     for () in … do …
                        //     let! () = …
                        //     and! () = …
                        //     use! () = …
                        //     match … with () -> …
                        | SynPat.Paren (SynPat.Const (SynConst.Unit, _), _), SyntaxNode.SynBinding _ :: _
                        | SynPat.Paren (SynPat.Const (SynConst.Unit, _), _), SyntaxNode.SynExpr (SynExpr.ForEach _) :: _
                        | SynPat.Paren (SynPat.Const (SynConst.Unit, _), _), SyntaxNode.SynExpr (SynExpr.LetOrUseBang _) :: _
                        | SynPat.Paren (SynPat.Const (SynConst.Unit, _), _), SyntaxNode.SynMatchClause _ :: _ -> ()

                        // Parens are otherwise never needed in these cases:
                        //
                        //     let (x: …) = …
                        //     for (…) in (…) do …
                        //     let! (…) = …
                        //     and! (…) = …
                        //     use! (…) = …
                        //     match … with (…) -> …
                        //     function (…) -> …
                        //     function (Pattern …) -> …
                        //     fun (x) -> …
                        | SynPat.Paren (_, range), SyntaxNode.SynBinding _ :: _
                        | SynPat.Paren (_, range), SyntaxNode.SynExpr (SynExpr.ForEach _) :: _
                        | SynPat.Paren (_, range), SyntaxNode.SynExpr (SynExpr.LetOrUseBang _) :: _
                        | SynPat.Paren (_, range), SyntaxNode.SynMatchClause _ :: _
                        | SynPat.Paren (_, range),
                          SyntaxNode.SynExpr (SynExpr.Lambda(args = SynSimplePats.SimplePats(pats = [ SynSimplePat.Id _ ]))) :: _ ->
                            ignore (ranges.Add range)

                        // Nested patterns.
                        | SynPat.Paren (inner, range), SyntaxNode.SynPat outer :: _ when not (SynPat.parenthesesNeededBetween outer inner) ->
                            ignore (ranges.Add range)

                        | _ -> ()

                        defaultTraverse pat
                }

            // Traverse every node in the input.
            let pick _ _ _ diveResults =
                let rec loop =
                    function
                    | [] -> None
                    | (_, project) :: rest ->
                        ignore (project ())
                        loop rest

                loop diveResults

            let _ = SyntaxTraversal.traverseUntil pick parsedInput.Range.End visitor parsedInput

            return ranges
        }
