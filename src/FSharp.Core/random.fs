// Copyright (c) Microsoft Corporation.  All Rights Reserved.  See License.txt in the project root for license information.

namespace Microsoft.FSharp.Core

open System
open System.Runtime.CompilerServices

[<Sealed>]
type internal ThreadSafeRandom() =
    inherit Random()

    [<DefaultValue>]
    [<ThreadStatic>]
    static val mutable private random: Random

    [<MethodImpl(MethodImplOptions.NoInlining)>]
    static member private Create() =
        ThreadSafeRandom.random <- Random()
        ThreadSafeRandom.random

    static member private LocalRandom =
        match ThreadSafeRandom.random with
        | null -> ThreadSafeRandom.Create()
        | random -> random

    override _.Next() = ThreadSafeRandom.LocalRandom.Next()
    override _.Next maxValue = ThreadSafeRandom.LocalRandom.Next maxValue
    override _.Next(minValue, maxValue) = ThreadSafeRandom.LocalRandom.Next(minValue, maxValue)
    override _.NextDouble() = ThreadSafeRandom.LocalRandom.NextDouble()
    override _.NextBytes(buffer: byte array) = ThreadSafeRandom.LocalRandom.NextBytes buffer
    override _.Sample() = raise (NotSupportedException())

[<AutoOpen>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module internal ThreadSafeRandom =
    // Avoid the static init check overhead on each access
    // that would happen if this were a static member val in a class.
    // See: https://github.com/dotnet/fsharp/issues/6454
    let private shared = ThreadSafeRandom()

    type Random with
        static member internal Shared: Random = shared

[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Random =
    let inline nextInt () = Random.Shared.Next()
    let inline int maxValue = Random.Shared.Next maxValue
    let inline intInRange minValue maxValue = Random.Shared.Next(minValue, maxValue)
    let inline nextFloat () = Random.Shared.NextDouble()

    let bytes count =
        if count < 0 then invalidArgOutOfRange (nameof count) count "TODO" 0
        let bytes = Microsoft.FSharp.Primitives.Basics.Array.zeroCreateUnchecked count
        Random.Shared.NextBytes bytes
        bytes

    let inline fill (buffer: byte array) = Random.Shared.NextBytes buffer
