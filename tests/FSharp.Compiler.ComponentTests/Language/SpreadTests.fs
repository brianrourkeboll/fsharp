// Copyright (c) Microsoft Corporation.  All Rights Reserved.  See License.txt in the project root for license information.

module Language.SpreadTests

open FSharp.Test.Compiler
open Xunit

[<Fact>]
let ``Record spread compiles`` () =
    FSharp """
    type R1 = { A : int; B : string }
    type R2 = { X : int; Y : string }
    type R3 = { ...R1; C : float }
    type R4 = { ...R2; D : float }
    type R5 = { ...R1; ...R2; E : float }

    let _ : R3 = { A = 3; B = "lol"; C = 3.14  }
    let _ : R4 = { X = 3; Y = "lol"; D = 3.14 }
    let _ : R5 = { A = 3; B = "lol"; X = 3; Y = "lol"; E = 3.14 }
    """
    |> typecheck
    |> shouldSucceed

[<Fact>]
let ``Order doesn't matter`` () =
    FSharp """
    type R1 = { A : int; B : string }
    type R2 = { X : int; Y : string }
    type R3 = { C : float; ...R1 }
    type R4 = { D : float; ...R2 }
    type R5 = { ...R2; E : float; ...R1  }

    let _ : R3 = { A = 3; B = "lol"; C = 3.14 }
    let _ : R4 = { X = 3; Y = "lol"; D = 3.14 }
    let _ : R5 = { A = 3; B = "lol"; X = 3; Y = "lol"; E = 3.14 }
    """
    |> typecheck
    |> shouldSucceed

[<Fact>]
let ``No dupes allowed`` () =
    FSharp """
    type R1 = { A : int; B : string }
    type R2 = { A : int; ...R1 }
    """
    |> typecheck
    |> shouldFail
    |> withSingleDiagnostic (Error 37, Line 3, Col 26, Line 3, Col 31, "Duplicate definition of field 'A'")

[<Fact>]
let ``Spreading class fields into a record type works`` () =
    FSharp """
    type C1 =
        val A : int
        new () = { A = 3 }

    type R1 = { ...C1 }

    let _ : R1 = { A = 3 }
    """
    |> typecheck
    |> shouldSucceed

[<Fact>]
let ``Spreading a record type that also has properties ignores the properties`` () =
    FSharp """
    type R1 =
        { A : int
          B : string }
        member _.Lol = 99

    type R2 = { ...R1; C : string }

    let _ : R2 = { A = 3; B = "3"; C = "asdf" }
    """
    |> typecheck
    |> shouldSucceed
