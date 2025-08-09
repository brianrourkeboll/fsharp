// Copyright (c) Microsoft Corporation.  All Rights Reserved.  See License.txt in the project root for license information.

module Language.SpreadTests

open FSharp.Test.Compiler
open Xunit

module TypeSpreads =
    module Records =
        [<Fact>]
        let ``Record spread compiles`` () =
            FSharp """
            type R1 = { A : int; B : string }
            type R2 = { X : int; Y : string }
            type R3 = { ...R1; C : float }
            type R4 = { ...R2; D : float }
            type R5 = { ...R1; ...R2; E : float }

            let _ : R3 = { A = 3; B = "lol"; C = 3.14 }
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
            |> withSingleDiagnostic (Error 37, Line 3, Col 34, Line 3, Col 39, "Duplicate definition of field 'A'")

        [<Fact>]
        let ``No dupes allowed, any order`` () =
            FSharp """
            type R1 = { A : int; B : string }
            type R2 = { ...R1; A : int }
            """
            |> typecheck
            |> shouldFail
            |> withSingleDiagnostic (Error 37, Line 3, Col 32, Line 3, Col 33, "Duplicate definition of field 'A'")

        [<Fact>]
        let ``No dupes allowed, multiple`` () =
            FSharp """
            type R1 = { A : int; B : string }
            type R2 = { ...R1; A : int; ...R1; A : int }
            """
            |> typecheck
            |> shouldFail
            |> withDiagnostics [
                Error 37, Line 3, Col 32, Line 3, Col 33, "Duplicate definition of field 'A'"
                Error 37, Line 3, Col 41, Line 3, Col 46, "Duplicate definition of field 'A'"
                Error 37, Line 3, Col 48, Line 3, Col 49, "Duplicate definition of field 'A'"
                Error 37, Line 3, Col 41, Line 3, Col 46, "Duplicate definition of field 'B'"
            ]

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

        [<Fact>]
        let ``Generic record spreads, single type parameter, inferred`` () =
            FSharp """
            type R1<'a> = { A : 'a; B : string }
            type R2<'a> = { X : 'a; Y : string }
            type R3<'a> = { ...R1<'a>; ...R2<'a> }

            let _ : R3<_> = { A = 3; B = "lol"; X = 4; Y = "haha" }
            """
            |> typecheck
            |> shouldSucceed

        [<Fact>]
        let ``Generic record spreads, single type parameter, annotated`` () =
            FSharp """
            type R1<'a> = { A : 'a; B : string }
            type R2<'a> = { X : 'a; Y : string }
            type R3<'a> = { ...R1<'a>; ...R2<'a> }

            let _ : R3<int> = { A = 3; B = "lol"; X = 4; Y = "haha" }
            """
            |> typecheck
            |> shouldSucceed

        [<Fact>]
        let ``Generic record spreads, multiple type parameters`` () =
            FSharp """
            type R1<'a> = { A : 'a; B : string }
            type R2<'a> = { X : 'a; Y : string }
            type R3<'a, 'b> = { ...R1<'a>; ...R2<'b> }

            let _ : R3<_, _> = { A = 3; B = "lol"; X = 3.14; Y = "haha" }
            """
            |> typecheck
            |> shouldSucceed

        [<Fact>]
        let ``Generic record spreads, single type parameter, not in scope, not allowed`` () =
            FSharp """
            type R1<'a> = { A : 'a; B : string }
            type R2<'a> = { X : 'a; Y : string }
            type R3<'a> = { ...R1<'a>; ...R2<'b> }
            type R4 = { ...R1<'a>; ...R2<'b> }
            """
            |> typecheck
            |> shouldFail
            |> withDiagnostics [
                Error 39, Line 4, Col 46, Line 4, Col 48, "The type parameter 'b is not defined."
                Error 39, Line 5, Col 31, Line 5, Col 33, "The type parameter 'a is not defined."
                Error 39, Line 5, Col 42, Line 5, Col 44, "The type parameter 'b is not defined."
            ]

module ExpressionSpreads =
    module AnonymousRecords =
        [<Fact>]
        let ``Spreading into an anonymous record works`` () =
            FSharp """
            type R1 =
                { A : int
                  B : string }
                member _.Lol = 99

            type R2 = { ...R1; C : string }

            let r1 = { A = 3; B = "4" }
            let r2 = { A = 5; B = "6"; C = "haha" }

            let _ = {| ...r1; ...r2 |}
            """
            |> typecheck
            |> shouldSucceed

        [<Fact>]
        let ``Union of the fields from right to left`` () =
            FSharp """
            type R1 =
                { A : int
                  B : string }
                member _.Lol = 99

            type R2 = { A : int; B : int; C : string }

            let r1 = { A = 3; B = "4" }
            let r2 = { A = 5; B = 6; C = "haha" }

            let _ : {| A : int; B : int; C : string |} = {| ...r1; ...r2 |}
            let _ : {| A : int; B : string; C : string |} = {| ...r2; ...r1 |}
            """
            |> typecheck
            |> shouldSucceed
