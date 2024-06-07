// Copyright (c) Microsoft Corporation.  All Rights Reserved.  See License.txt in the project root for license information.

namespace Microsoft.VisualStudio.FSharp.Editor

open System
open System.Composition
open System.Collections.Generic
open System.Collections.Immutable
open System.Runtime.Caching
open System.Threading
open System.Threading.Tasks
open FSharp.Compiler.Syntax
open FSharp.Compiler.SyntaxTrivia
open FSharp.Compiler.Text
open Microsoft.CodeAnalysis
open Microsoft.CodeAnalysis.ExternalAccess.FSharp.Diagnostics
open CancellableTasks

// This interface is not defined in Microsoft.CodeAnalysis.ExternalAccess.FSharp.Diagnostics
// and so we are not currently exporting the type below as an implementation of it
// using [<Export(typeof<IFSharpUseNameofInsteadDiagnosticAnalyzer>)>], since it would not be recognized.
type IFSharpUseNameofInsteadDiagnosticAnalyzer =
    inherit IFSharpDocumentDiagnosticAnalyzer

[<NoEquality; NoComparison>]
type private DocumentData' =
    {
        Hash: int
        Diagnostics: ImmutableArray<Diagnostic>
    }

[<Sealed>]
type internal UseNameofInsteadDiagnosticAnalyzer [<ImportingConstructor>] () =
    static let completedTask = Task.FromResult ImmutableArray.Empty

    static let descriptor =
        let title = "Use nameof instead."

        DiagnosticDescriptor(
            "FS9999",
            title,
            title,
            "Style",
            DiagnosticSeverity.Hidden,
            isEnabledByDefault = true,
            description = null,
            helpLinkUri = null
        )

    static let cache =
        new MemoryCache $"FSharp.Editor.{nameof UseNameofInsteadDiagnosticAnalyzer}"

    static let semaphore = new SemaphoreSlim 3

    static member GetDiagnostics(document: Document) =
        cancellableTask {
            let! cancellationToken = CancellableTask.getCancellationToken ()
            let! textVersion = document.GetTextVersionAsync cancellationToken
            let textVersionHash = textVersion.GetHashCode()

            match! semaphore.WaitAsync(DefaultTuning.PerDocumentSavedDataSlidingWindow, cancellationToken) with
            | false -> return ImmutableArray.Empty
            | true ->
                try
                    let key = string document.Id

                    match cache.Get key with
                    | :? DocumentData' as data when data.Hash = textVersionHash -> return data.Diagnostics
                    | _ ->
                        let! parseResults = document.GetFSharpParseResultsAsync(nameof UseNameofInsteadDiagnosticAnalyzer)

                        let couldUseNameofInstead =
                            let comparer =
                                { new IEqualityComparer<struct (string * range)> with
                                    member _.Equals((id1, m1), (id2, m2)) =
                                        Range.equals m1 m2 && String.Equals(id1, id2, StringComparison.Ordinal)

                                    member _.GetHashCode pair = pair.GetHashCode()
                                }

                            (HashSet comparer, parseResults.ParseTree)
                            ||> ParsedInput.fold (fun acc path node ->
                                let rec isInScopeIn path (name: string) =
                                    let (|Ident|) (ident: Ident) = ident.idText

                                    let (|MatchingPat|_|) pats =
                                        let anyMatching =
                                            pats
                                            |> List.exists (function
                                                | SynPat.Named(
                                                    ident = SynIdent(trivia = Some(IdentTrivia.OriginalNotationWithParen(_, ident, _)))) ->
                                                    name.Length = ident.Length + 2
                                                    && name.StartsWith("(", StringComparison.Ordinal)
                                                    && name.EndsWith(")", StringComparison.Ordinal)
                                                    && name.AsSpan(1, name.Length - 2).SequenceEqual(ident.AsSpan())

                                                | SynPat.Named(ident = SynIdent(ident = Ident ident)) ->
                                                    String.Equals(name, ident, StringComparison.Ordinal)

                                                | _ -> false)

                                        if anyMatching then Some MatchingPat else None

                                    match path with
                                    | SyntaxNode.SynBinding(SynBinding(headPat = SynPat.LongIdent(argPats = SynArgPats.Pats MatchingPat))) :: _
                                    | SyntaxNode.SynExpr(SynExpr.Lambda(parsedData = Some(MatchingPat, _))) :: _ -> true

                                    | SyntaxNode.SynPat _ :: path
                                    | SyntaxNode.SynExpr _ :: path -> isInScopeIn path name

                                    | _ -> false

                                match node with
                                | SyntaxNode.SynExpr(SynExpr.Const(constant = SynConst.String(name, SynStringKind.Regular, m)))
                                | SyntaxNode.SynPat(SynPat.Const(constant = SynConst.String(name, SynStringKind.Regular, m))) when
                                    name |> isInScopeIn path
                                    ->
                                    ignore (acc.Add(name, m))
                                    acc

                                | _ -> acc)

                        let! diagnostics =
                            if couldUseNameofInstead.Count = 0 then
                                CancellableTask.singleton ImmutableArray.Empty
                            else
                                cancellableTask {
                                    let! sourceText = document.GetTextAsync cancellationToken
                                    let builder = ImmutableArray.CreateBuilder couldUseNameofInstead.Count

                                    for name, range in couldUseNameofInstead do
                                        builder.Add(
                                            Diagnostic.Create(
                                                descriptor,
                                                RoslynHelpers.RangeToLocation(range, sourceText, document.FilePath),
                                                ImmutableDictionary.CreateRange [| KeyValuePair("name", name) |]
                                            )
                                        )

                                    return builder.MoveToImmutable()
                                }

                        ignore (cache.Remove key)

                        cache.Set(
                            CacheItem(
                                key,
                                {
                                    Hash = textVersionHash
                                    Diagnostics = diagnostics
                                }
                            ),
                            CacheItemPolicy(SlidingExpiration = DefaultTuning.PerDocumentSavedDataSlidingWindow)
                        )

                        return diagnostics
                finally
                    ignore (semaphore.Release())
        }

    interface IFSharpUseNameofInsteadDiagnosticAnalyzer with
        member _.AnalyzeSemanticsAsync(document: Document, cancellationToken: CancellationToken) =
            ignore (document, cancellationToken)
            completedTask

        member _.AnalyzeSyntaxAsync(document: Document, cancellationToken: CancellationToken) =
            UseNameofInsteadDiagnosticAnalyzer.GetDiagnostics document
            |> CancellableTask.start cancellationToken
