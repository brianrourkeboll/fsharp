// Copyright (c) Microsoft Corporation.  All Rights Reserved.  See License.txt in the project root for license information.

module Language.SpreadTests

open FSharp.Test.Compiler
open Xunit

module TypeSpreads =
    module Records =
        module RightmostWins = ()
        module DuplicateFieldHandling = ()
        module GenericTypeParameters = ()

        module Accessibility =
            [<Fact>]
            let ``Private into public compiles`` () =
                FSharp
                    """
                    type private R1 = { A : int; B : string }
                    type R2 = { ...R1 }
                    """
                |> typecheck
                |> shouldSucceed

        module Mutability =
            [<Fact>]
            let ``Mutability is brought over`` () =
                FSharp
                    """
                    type private R1 = { A : int; mutable B : string }
                    type R2 = { ...R1 }
                    let r2 : R2 = { A = 1; B = "3" }
                    r2.B <- "99"
                    """
                |> typecheck
                |> shouldSucceed

        module StaticFields = ()
        module NonRecordSource = ()

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
        let ``Record spread compiles, different order`` () =
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
        let ``No explicit duplicate fields allowed`` () =
            FSharp """
            type R = { A : int; A : string }
            """
            |> typecheck
            |> shouldFail
            |> withSingleDiagnostic (Error 37, Line 2, Col 33, Line 2, Col 34, "Duplicate definition of field 'A'")

        [<Fact>]
        let ``No warning for explicit field shadowing spread field`` () =
            FSharp """
            type R1 = { A : int }
            type R2 = { ...R1; A : string }
            """
            |> typecheck
            |> shouldSucceed

        [<Fact>]
        let ``Warning for spread field shadowing explicit field`` () =
            FSharp """
            type R1 = { A : int }
            type R2 = { A : int; ...R1 }
            """
            |> typecheck
            |> shouldFail
            |> withSingleDiagnostic (Warning 37, Line 3, Col 34, Line 3, Col 39, "Duplicate definition of field 'A'")

        [<Fact>]
        let ``No dupes allowed, multiple`` () =
            FSharp """
            type R1 = { A : int; B : string }
            type R2 = { ...R1; A : int; ...R1; A : int }
            """
            |> typecheck
            |> shouldFail
            |> withDiagnostics [
                Warning 37, Line 3, Col 41, Line 3, Col 46, "Duplicate definition of field 'A'"
                Warning 37, Line 3, Col 41, Line 3, Col 46, "Duplicate definition of field 'B'"
                Error 37, Line 3, Col 48, Line 3, Col 49, "Duplicate definition of field 'A'"
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

        // Just like
        // type R1<[<Measure>] 'a> = { A : int<'a> }
        // type R2<'a> = { X : R1<'a> }
        [<Fact>]
        let ``Generic record spread, measure attribute on source, required on spread destination`` () =
            FSharp """
            type R1<[<Measure>] 'a> = { A : int<'a> }
            type R2<'a> = { ...R1<'a> }
            """
            |> typecheck
            |> shouldFail
            |> withSingleDiagnostic (Error 702, Line 3, Col 35, Line 3, Col 37, "Expected unit-of-measure parameter, not type parameter. Explicit unit-of-measure parameters must be marked with the [<Measure>] attribute.")

        [<Fact>]
        let ``Generic record spread, measure attribute on source, measure on spread destination, OK`` () =
            FSharp """
            type R1<[<Measure>] 'a> = { A : int<'a> }
            type R2<[<Measure>] 'a> = { ...R1<'a> }
            """
            |> typecheck
            |> shouldSucceed

        // Just like
        // type R1<'a when 'a : comparison> = { A : 'a }
        // type R2<'a> = { X : R1<'a> }
        [<Fact>]
        let ``Generic record spread, constraint on source, required on spread destination`` () =
            FSharp """
            type R1<'a when 'a : comparison> = { A : 'a }
            type R2<'a> = { ...R1<'a> }
            """
            |> typecheck
            |> shouldFail
            |> withSingleDiagnostic (Error 1, Line 3, Col 32, Line 3, Col 38, "A type parameter is missing a constraint 'when 'a: comparison'")

        [<Fact>]
        let ``Generic record spread, constraint on source, constraint on spread destination, OK`` () =
            FSharp """
            type R1<'a when 'a : comparison> = { A : 'a }
            type R2<'a when 'a : comparison> = { ...R1<'a> }
            """
            |> typecheck
            |> shouldSucceed

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

        [<Fact>]
        let ``With`` () =
            FSharp
                """
                type R1 = { A : int; B : string }
                type R2 = { X : int; Y : string }
                type R3 = { ...R1; ...R2; E : float }

                let r1 : R1 = { A = 3; B = "lol" }
                let r2 : R2 = { X = 4; Y = "ha" }
                let r3 : R3 = { ...r1; ...r2; E = 3.14 }
                let r3' = {| r3 with E = 5.0; ...r1 |}
                """
            |> typecheck
            |> shouldSucceed

    module Records =
        module RecordToRecord =
            [<Fact>]
            let ``Can spread a record into a record`` () =
                FSharp
                    """
                    type R1 = { A : int; B : string }
                    type R2 = { X : int; Y : string }
                    type R3 = { ...R1; C : float }
                    type R4 = { ...R2; D : float }
                    type R5 = { ...R1; ...R2; E : float }

                    let r1 : R1 = { A = 3; B = "lol" }
                    let r2 : R2 = { X = 4; Y = "ha" }
                    let r3 : R3 = { ...r1; C = 3.14 }
                    let r4 : R4 = { ...r2; D = 3.14 }
                    let r5 : R5 = { ...r1; ...r2; E = 3.14 }
                    """
                |> typecheck
                |> shouldSucceed

            [<Fact>]
            let ``With`` () =
                FSharp
                    """
                    type R1 = { A : int; B : string }
                    type R2 = { X : int; Y : string }
                    type R3 = { ...R1; ...R2; E : float }

                    let r1 : R1 = { A = 3; B = "lol" }
                    let r2 : R2 = { X = 4; Y = "ha" }
                    let r3 : R3 = { ...r1; ...r2; E = 3.14 }
                    let r3' : R3 = { r3 with E = 5.0; ...r1 }
                    """
                |> typecheck
                |> shouldSucceed

        module AnonymousRecordToRecord = ()
        module ObjToRecord = ()
