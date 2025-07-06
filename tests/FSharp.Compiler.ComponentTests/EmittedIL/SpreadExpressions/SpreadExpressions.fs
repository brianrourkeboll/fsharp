namespace EmittedIL

open FSharp.Test
open FSharp.Test.Compiler
open Xunit

module SpreadExpressions =
    let verifyCompilation compilation =
        compilation
        |> asExe
        |> withEmbeddedPdb
        |> withEmbedAllSource
        |> ignoreWarnings
        |> verifyILBaseline

    [<Theory; FileInlineData("TypeSpreads.fs", Realsig = BooleanOptions.True, Optimize = BooleanOptions.True)>]
    let TypeSpreads_fs compilation = 
        compilation
        |> getCompilation
        |> verifyCompilation

    [<Theory; FileInlineData("SpreadNominalRecordIntoAnonymousRecord.fs", Realsig = BooleanOptions.True, Optimize = BooleanOptions.True)>]
    let SpreadNominalRecordIntoAnonymousRecord_fs compilation = 
        compilation
        |> getCompilation
        |> verifyCompilation
