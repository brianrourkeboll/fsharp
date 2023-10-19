﻿// Copyright (c) Microsoft Corporation.  All Rights Reserved.  See License.txt in the project root for license information.

namespace Microsoft.VisualStudio.FSharp.Editor

open System.Composition
open System.Collections.Immutable
open System.Threading
open System.Threading.Tasks
open FSharp.Compiler.EditorServices
open FSharp.Compiler.Text
open Microsoft.CodeAnalysis
open Microsoft.CodeAnalysis.ExternalAccess.FSharp.Diagnostics
open CancellableTasks

// This interface is not defined in Microsoft.CodeAnalysis.ExternalAccess.FSharp.Diagnostics
// and so we are not currently exporting the type below as an implementation of it
// using [<Export(typeof<IFSharpUnnecessaryParenthesesDiagnosticAnalyzer>)>], since it would not be recognized.
type IFSharpUnnecessaryParenthesesDiagnosticAnalyzer = inherit IFSharpDocumentDiagnosticAnalyzer

[<Sealed>]
type internal UnnecessaryParenthesesDiagnosticAnalyzer [<ImportingConstructor>] () =
    static let completedTask = Task.FromResult ImmutableArray.Empty

    static let descriptor =
        let title = "Parentheses can be removed."

        DiagnosticDescriptor(
            "IDE0047",
            title,
            title,
            "Style",
            DiagnosticSeverity.Hidden,
            isEnabledByDefault = true,
            description = null,
            helpLinkUri = null)

    static member GetDiagnostics(document: Document) =
        cancellableTask {
            let! parseResults = document.GetFSharpParseResultsAsync(nameof UnnecessaryParenthesesDiagnosticAnalyzer)
            let! cancellationToken = CancellableTask.getCancellationToken ()
            let! sourceText = document.GetTextAsync cancellationToken
            let getLineString line = sourceText.Lines[Line.toZ line].ToString()
            let! unnecessaryParentheses = UnnecessaryParentheses.getUnnecessaryParentheses getLineString parseResults.ParseTree
            return
                unnecessaryParentheses
                |> Seq.map (fun range -> Diagnostic.Create(descriptor, RoslynHelpers.RangeToLocation(range, sourceText, document.FilePath)))
                |> Seq.toImmutableArray
        }

    interface IFSharpUnnecessaryParenthesesDiagnosticAnalyzer with
        member _.AnalyzeSemanticsAsync(document: Document, cancellationToken: CancellationToken) =
            ignore (document, cancellationToken)
            completedTask

        member _.AnalyzeSyntaxAsync(document: Document, cancellationToken: CancellationToken) =
            UnnecessaryParenthesesDiagnosticAnalyzer.GetDiagnostics document
            |> CancellableTask.start cancellationToken
