// Copyright (c) Microsoft Corporation.  All Rights Reserved.  See License.txt in the project root for license information.

namespace Microsoft.VisualStudio.FSharp.Editor

open System.Collections.Immutable
open System.Composition
open Microsoft.CodeAnalysis.CodeFixes
open Microsoft.CodeAnalysis.Text
open CancellableTasks

[<ExportCodeFixProvider(FSharpConstants.FSharpLanguageName, Name = CodeFix.ReplaceQuotedStringWithNameof); Shared; Sealed>]
type internal FSharpReplaceQuotedStringWithNameofCodeFixProvider [<ImportingConstructor>] () =
    inherit CodeFixProvider()

    static let title = SR.ReplaceQuotedStringWithNameof()
    static let fixableDiagnosticIds = ImmutableArray.Create "FS9999"

    override _.FixableDiagnosticIds = fixableDiagnosticIds

    override this.RegisterCodeFixesAsync context = context.RegisterFsharpFix this

    override this.GetFixAllProvider() = this.RegisterFsharpFixAll()

    interface IFSharpCodeFixProvider with
        member _.GetCodeFixIfAppliesAsync context =
            assert (context.Span.Length >= 3) // "…"

            CancellableTask.singleton (
                if context.Diagnostics.IsEmpty then
                    ValueNone
                else
                    assert not context.Diagnostics.IsEmpty

                    let diag = context.Diagnostics[0]
                    let replacement = diag.Properties["replacement"]

                    ValueSome
                        {
                            Name = CodeFix.ReplaceQuotedStringWithNameof
                            Message = title
                            Changes = [ TextChange(context.Span, replacement) ]
                        }
            )
