// Copyright (c) Microsoft Corporation.  All Rights Reserved.  See License.txt in the project root for license information.

namespace Microsoft.FSharp.Core

open System

[<AutoOpen>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module internal ThreadSafeRandom =
    type Random with
        static member internal Shared : Random

/// Convenience functions for generating pseudo-random numbers.
[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Random =
    val inline nextInt : unit -> int
    val inline int : maxValue:int -> int
    val inline intInRange : minValue:int -> maxValue:int -> int
    val inline nextFloat : unit -> float
    val bytes : count:int -> byte array
    val inline fill : buffer:byte array -> unit
