// Copyright (c) Microsoft Corporation.  All Rights Reserved.  See License.txt in the project root for license information.

namespace FSharp.Compiler.UnitTests.ComputedCollectionRangeLoweringTests

open NUnit.Framework
open FSharp.Test

[<TestFixture>]
module UInt64 =
    module Array =
        /// [|start..finish|]
        module Range =
            [<Test>]
            let ``Lone RangeUInt64 with const args when start > finish lowers to empty array`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions([|"--optimize+"|],
                    """
                    module Test

                    let test () = [|10UL..1UL|]
                    """,
                    (fun verifier -> verifier.VerifyIL [
                    """
                    .assembly extern runtime { }
                    .assembly extern FSharp.Core { }
                    .assembly assembly
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.FSharpInterfaceDataVersionAttribute::.ctor(int32,
                                                                                                                          int32,
                                                                                                                          int32) = ( 01 00 02 00 00 00 00 00 00 00 00 00 00 00 00 00 ) 
                      .hash algorithm 0x00008004
                      .ver 0:0:0:0
                    }
                    .mresource public FSharpSignatureData.assembly
                    {
                      
                      
                    }
                    .mresource public FSharpOptimizationData.assembly
                    {
                      
                      
                    }
                    .module assembly.dll
                    
                    .imagebase {value}
                    .file alignment 0x00000200
                    .stackreserve 0x00100000
                    .subsystem 0x0003       
                    .corflags 0x00000001    
                    
                    
                    
                    
                    
                    .class public abstract auto ansi sealed Test
                           extends [runtime]System.Object
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 07 00 00 00 00 00 ) 
                      .method public static uint64[]  test() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  call       !!0[] [runtime]System.Array::Empty<uint64>()
                        IL_0005:  ret
                      } 
                    
                    } 
                    
                    .class private abstract auto ansi sealed '<StartupCode$assembly>'.$Test
                           extends [runtime]System.Object
                    {
                    }
                    """
                    ]))

            [<Test>]
            let ``Lone RangeUInt64 with const args lowers to call to init`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions([|"--optimize+"|],
                    """
                    module Test

                    let test () = [|1UL..257UL|]
                    """,
                    (fun verifier -> verifier.VerifyIL [
                    """
                    .assembly extern runtime { }
                    .assembly extern FSharp.Core { }
                    .assembly assembly
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.FSharpInterfaceDataVersionAttribute::.ctor(int32,
                                                                                                                          int32,
                                                                                                                          int32) = ( 01 00 02 00 00 00 00 00 00 00 00 00 00 00 00 00 ) 
                      .hash algorithm 0x00008004
                      .ver 0:0:0:0
                    }
                    .mresource public FSharpSignatureData.assembly
                    {
                      
                      
                    }
                    .mresource public FSharpOptimizationData.assembly
                    {
                      
                      
                    }
                    .module assembly.dll
                    
                    .imagebase {value}
                    .file alignment 0x00000200
                    .stackreserve 0x00100000
                    .subsystem 0x0003       
                    .corflags 0x00000001    
                    
                    
                    
                    
                    
                    .class public abstract auto ansi sealed Test
                           extends [runtime]System.Object
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 07 00 00 00 00 00 ) 
                      .class auto ansi serializable sealed nested assembly beforefieldinit test@1
                             extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,uint64>
                      {
                        .field static assembly initonly class Test/test@1 @_instance
                        .method assembly specialname rtspecialname 
                                instance void  .ctor() cil managed
                        {
                          .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                          .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,uint64>::.ctor()
                          IL_0006:  ret
                        } 
                    
                        .method public strict virtual instance uint64 
                                Invoke(int32 i) cil managed
                        {
                          
                          .maxstack  8
                          IL_0000:  ldc.i4.1
                          IL_0001:  conv.i8
                          IL_0002:  ldarg.1
                          IL_0003:  add
                          IL_0004:  ret
                        } 
                    
                        .method private specialname rtspecialname static 
                                void  .cctor() cil managed
                        {
                          
                          .maxstack  10
                          IL_0000:  newobj     instance void Test/test@1::.ctor()
                          IL_0005:  stsfld     class Test/test@1 Test/test@1::@_instance
                          IL_000a:  ret
                        } 
                    
                      } 
                    
                      .method public static uint64[]  test() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4     0x101
                        IL_0005:  conv.i8
                        IL_0006:  ldsfld     class Test/test@1 Test/test@1::@_instance
                        IL_000b:  tail.
                        IL_000d:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::Initialize<uint64>(int32,
                                                                                                                             class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,!!0>)
                        IL_0012:  ret
                      } 
                    
                    } 
                    
                    .class private abstract auto ansi sealed '<StartupCode$assembly>'.$Test
                           extends [runtime]System.Object
                    {
                    }
                    """
                    ]))

            [<Test>]
            let ``Lone RangeUInt64 with dynamic args lowers to call to init`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions([|"--optimize+"|],
                    """
                    module Test

                    let test (start : uint64) (finish : uint64) = [|start..finish|]
                    """,
                    (fun verifier -> verifier.VerifyIL [
                    """
                    .assembly extern runtime { }
                    .assembly extern FSharp.Core { }
                    .assembly assembly
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.FSharpInterfaceDataVersionAttribute::.ctor(int32,
                                                                                                                          int32,
                                                                                                                          int32) = ( 01 00 02 00 00 00 00 00 00 00 00 00 00 00 00 00 ) 
                      .hash algorithm 0x00008004
                      .ver 0:0:0:0
                    }
                    .mresource public FSharpSignatureData.assembly
                    {
                      
                      
                    }
                    .mresource public FSharpOptimizationData.assembly
                    {
                      
                      
                    }
                    .module assembly.dll
                    
                    .imagebase {value}
                    .file alignment 0x00000200
                    .stackreserve 0x00100000
                    .subsystem 0x0003       
                    .corflags 0x00000001    
                    
                    
                    
                    
                    
                    .class public abstract auto ansi sealed Test
                           extends [runtime]System.Object
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 07 00 00 00 00 00 ) 
                      .class auto ansi serializable sealed nested assembly beforefieldinit test@1
                             extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,uint64>
                      {
                        .field public uint64 start
                        .method assembly specialname rtspecialname 
                                instance void  .ctor(uint64 start) cil managed
                        {
                          .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                          .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,uint64>::.ctor()
                          IL_0006:  ldarg.0
                          IL_0007:  ldarg.1
                          IL_0008:  stfld      uint64 Test/test@1::start
                          IL_000d:  ret
                        } 
                    
                        .method public strict virtual instance uint64 
                                Invoke(int32 i) cil managed
                        {
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  ldfld      uint64 Test/test@1::start
                          IL_0006:  ldarg.1
                          IL_0007:  add
                          IL_0008:  ret
                        } 
                    
                      } 
                    
                      .method public static uint64[]  test(uint64 start,
                                                           uint64 finish) cil managed
                      {
                        .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationArgumentCountsAttribute::.ctor(int32[]) = ( 01 00 02 00 00 00 01 00 00 00 01 00 00 00 00 00 ) 
                        
                        .maxstack  8
                        IL_0000:  ldarg.1
                        IL_0001:  ldarg.0
                        IL_0002:  bge.s      IL_000a
                    
                        IL_0004:  call       !!0[] [runtime]System.Array::Empty<uint64>()
                        IL_0009:  ret
                    
                        IL_000a:  ldarg.1
                        IL_000b:  ldarg.0
                        IL_000c:  sub
                        IL_000d:  ldc.i4.1
                        IL_000e:  conv.i8
                        IL_000f:  add
                        IL_0010:  ldarg.0
                        IL_0011:  newobj     instance void Test/test@1::.ctor(uint64)
                        IL_0016:  tail.
                        IL_0018:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::Initialize<uint64>(int32,
                                                                                                                             class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,!!0>)
                        IL_001d:  ret
                      } 
                    
                    } 
                    
                    .class private abstract auto ansi sealed '<StartupCode$assembly>'.$Test
                           extends [runtime]System.Object
                    {
                    }
                    """
                    ]))

            [<Test>]
            let ``Lone RangeUInt64 with dynamic args that are complex exprs stores those in vars before calling init`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions([|"--optimize+"|],
                    """
                    module Test

                    let a () = uint64 (Array.zeroCreate 10).Length
                    let b () = uint64 (Array.zeroCreate 20).Length

                    let test () = [|a ()..b ()|]
                    """,
                    (fun verifier -> verifier.VerifyIL [
                    """
                    .assembly extern runtime { }
                    .assembly extern FSharp.Core { }
                    .assembly assembly
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.FSharpInterfaceDataVersionAttribute::.ctor(int32,
                                                                                                                          int32,
                                                                                                                          int32) = ( 01 00 02 00 00 00 00 00 00 00 00 00 00 00 00 00 ) 
                      .hash algorithm 0x00008004
                      .ver 0:0:0:0
                    }
                    .mresource public FSharpSignatureData.assembly
                    {
                      
                      
                    }
                    .mresource public FSharpOptimizationData.assembly
                    {
                      
                      
                    }
                    .module assembly.dll
                    
                    .imagebase {value}
                    .file alignment 0x00000200
                    .stackreserve 0x00100000
                    .subsystem 0x0003       
                    .corflags 0x00000001    
                    
                    
                    
                    
                    
                    .class public abstract auto ansi sealed Test
                           extends [runtime]System.Object
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 07 00 00 00 00 00 ) 
                      .class auto ansi serializable sealed nested assembly beforefieldinit test@1
                             extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,uint64>
                      {
                        .field public uint64 start
                        .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
                        .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                        .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                        .method assembly specialname rtspecialname 
                                instance void  .ctor(uint64 start) cil managed
                        {
                          .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                          .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,uint64>::.ctor()
                          IL_0006:  ldarg.0
                          IL_0007:  ldarg.1
                          IL_0008:  stfld      uint64 Test/test@1::start
                          IL_000d:  ret
                        } 
                    
                        .method public strict virtual instance uint64 
                                Invoke(int32 i) cil managed
                        {
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  ldfld      uint64 Test/test@1::start
                          IL_0006:  ldarg.1
                          IL_0007:  add
                          IL_0008:  ret
                        } 
                    
                      } 
                    
                      .method public static uint64  a() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4.s   10
                        IL_0002:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0007:  ldlen
                        IL_0008:  conv.i4
                        IL_0009:  conv.i8
                        IL_000a:  ret
                      } 
                    
                      .method public static uint64  b() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4.s   20
                        IL_0002:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0007:  ldlen
                        IL_0008:  conv.i4
                        IL_0009:  conv.i8
                        IL_000a:  ret
                      } 
                    
                      .method public static uint64[]  test() cil managed
                      {
                        
                        .maxstack  4
                        .locals init (uint64 V_0,
                                 uint64 V_1)
                        IL_0000:  ldc.i4.s   10
                        IL_0002:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0007:  ldlen
                        IL_0008:  conv.i4
                        IL_0009:  conv.i8
                        IL_000a:  stloc.0
                        IL_000b:  ldc.i4.s   20
                        IL_000d:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0012:  ldlen
                        IL_0013:  conv.i4
                        IL_0014:  conv.i8
                        IL_0015:  stloc.1
                        IL_0016:  ldloc.1
                        IL_0017:  ldloc.0
                        IL_0018:  bge.s      IL_0020
                    
                        IL_001a:  call       !!0[] [runtime]System.Array::Empty<uint64>()
                        IL_001f:  ret
                    
                        IL_0020:  ldloc.1
                        IL_0021:  ldloc.0
                        IL_0022:  sub
                        IL_0023:  ldc.i4.1
                        IL_0024:  conv.i8
                        IL_0025:  add
                        IL_0026:  ldloc.0
                        IL_0027:  newobj     instance void Test/test@1::.ctor(uint64)
                        IL_002c:  tail.
                        IL_002e:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::Initialize<uint64>(int32,
                                                                                                                             class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,!!0>)
                        IL_0033:  ret
                      } 
                    
                    } 
                    
                    .class private abstract auto ansi sealed '<StartupCode$assembly>'.$Test
                           extends [runtime]System.Object
                    {
                    }
                    """
                    ]))

        /// [|start..step..finish|]
        module RangeStep =
            [<Test>]
            let ``Lone RangeUInt64 with const args when step = 0 lowers to call to invalidArg`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions([|"--optimize+"|],
                    """
                    module Test

                    let test () = [|1UL..0UL..5UL|]
                    """,
                    (fun verifier -> verifier.VerifyIL [
                    """
                    .assembly extern runtime { }
                    .assembly extern FSharp.Core { }
                    .assembly assembly
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.FSharpInterfaceDataVersionAttribute::.ctor(int32,
                                                                                                                          int32,
                                                                                                                          int32) = ( 01 00 02 00 00 00 00 00 00 00 00 00 00 00 00 00 ) 
                      .hash algorithm 0x00008004
                      .ver 0:0:0:0
                    }
                    .mresource public FSharpSignatureData.assembly
                    {
                      
                      
                    }
                    .mresource public FSharpOptimizationData.assembly
                    {
                      
                      
                    }
                    .module assembly.dll
                    
                    .imagebase {value}
                    .file alignment 0x00000200
                    .stackreserve 0x00100000
                    .subsystem 0x0003       
                    .corflags 0x00000001    
                    
                    
                    
                    
                    
                    .class public abstract auto ansi sealed Test
                           extends [runtime]System.Object
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 07 00 00 00 00 00 ) 
                      .method public static uint64[]  test() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldstr      "step"
                        IL_0005:  ldstr      "The step of a range cannot be zero."
                        IL_000a:  tail.
                        IL_000c:  call       !!0 [FSharp.Core]Microsoft.FSharp.Core.Operators::InvalidArg<uint64[]>(string,
                                                                                                                    string)
                        IL_0011:  ret
                      } 
                    
                    } 
                    
                    .class private abstract auto ansi sealed '<StartupCode$assembly>'.$Test
                           extends [runtime]System.Object
                    {
                    }
                    """
                    ]))

            [<Test>]
            let ``Lone RangeUInt64 with const args when (finish - start) / step + 1 ≤ 0 lowers to empty array`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions([|"--optimize+"|],
                    """
                    module Test

                    let test () = [|5UL..1UL..1UL|]
                    """,
                    (fun verifier -> verifier.VerifyIL [
                    """
                    .assembly extern runtime { }
                    .assembly extern FSharp.Core { }
                    .assembly assembly
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.FSharpInterfaceDataVersionAttribute::.ctor(int32,
                                                                                                                          int32,
                                                                                                                          int32) = ( 01 00 02 00 00 00 00 00 00 00 00 00 00 00 00 00 ) 
                      .hash algorithm 0x00008004
                      .ver 0:0:0:0
                    }
                    .mresource public FSharpSignatureData.assembly
                    {
                      
                      
                    }
                    .mresource public FSharpOptimizationData.assembly
                    {
                      
                      
                    }
                    .module assembly.dll
                    
                    .imagebase {value}
                    .file alignment 0x00000200
                    .stackreserve 0x00100000
                    .subsystem 0x0003       
                    .corflags 0x00000001    
                    
                    
                    
                    
                    
                    .class public abstract auto ansi sealed Test
                           extends [runtime]System.Object
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 07 00 00 00 00 00 ) 
                      .method public static uint64[]  test() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  call       !!0[] [runtime]System.Array::Empty<uint64>()
                        IL_0005:  ret
                      } 
                    
                    } 
                    
                    .class private abstract auto ansi sealed '<StartupCode$assembly>'.$Test
                           extends [runtime]System.Object
                    {
                    }
                    """
                    ]))

            [<Test>]
            let ``Lone RangeUInt64 with const args lowers to call to init`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions([|"--optimize+"|],
                    """
                    module Test

                    let test () = [|1UL..2UL..257UL|]
                    """,
                    (fun verifier -> verifier.VerifyIL [
                    """
                    .assembly extern runtime { }
                    .assembly extern FSharp.Core { }
                    .assembly assembly
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.FSharpInterfaceDataVersionAttribute::.ctor(int32,
                                                                                                                          int32,
                                                                                                                          int32) = ( 01 00 02 00 00 00 00 00 00 00 00 00 00 00 00 00 ) 
                      .hash algorithm 0x00008004
                      .ver 0:0:0:0
                    }
                    .mresource public FSharpSignatureData.assembly
                    {
                      
                      
                    }
                    .mresource public FSharpOptimizationData.assembly
                    {
                      
                      
                    }
                    .module assembly.dll
                    
                    .imagebase {value}
                    .file alignment 0x00000200
                    .stackreserve 0x00100000
                    .subsystem 0x0003       
                    .corflags 0x00000001    
                    
                    
                    
                    
                    
                    .class public abstract auto ansi sealed Test
                           extends [runtime]System.Object
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 07 00 00 00 00 00 ) 
                      .class auto ansi serializable sealed nested assembly beforefieldinit test@1
                             extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,uint64>
                      {
                        .field static assembly initonly class Test/test@1 @_instance
                        .method assembly specialname rtspecialname 
                                instance void  .ctor() cil managed
                        {
                          .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                          .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,uint64>::.ctor()
                          IL_0006:  ret
                        } 
                    
                        .method public strict virtual instance uint64 
                                Invoke(int32 i) cil managed
                        {
                          
                          .maxstack  8
                          IL_0000:  ldarg.1
                          IL_0001:  ldc.i4.2
                          IL_0002:  conv.i8
                          IL_0003:  mul
                          IL_0004:  ldc.i4.1
                          IL_0005:  conv.i8
                          IL_0006:  add
                          IL_0007:  ret
                        } 
                    
                        .method private specialname rtspecialname static 
                                void  .cctor() cil managed
                        {
                          
                          .maxstack  10
                          IL_0000:  newobj     instance void Test/test@1::.ctor()
                          IL_0005:  stsfld     class Test/test@1 Test/test@1::@_instance
                          IL_000a:  ret
                        } 
                    
                      } 
                    
                      .method public static uint64[]  test() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4     0x81
                        IL_0005:  conv.i8
                        IL_0006:  ldsfld     class Test/test@1 Test/test@1::@_instance
                        IL_000b:  tail.
                        IL_000d:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::Initialize<uint64>(int32,
                                                                                                                             class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,!!0>)
                        IL_0012:  ret
                      } 
                    
                    } 
                    
                    .class private abstract auto ansi sealed '<StartupCode$assembly>'.$Test
                           extends [runtime]System.Object
                    {
                    }
                    """
                    ]))

            [<Test>]
            let ``Lone RangeUInt64 with dynamic args lowers to call to init`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions([|"--optimize+"|],
                    """
                    module Test

                    let test (start : uint64) (step : uint64) (finish : uint64) = [|start..step..finish|]
                    """,
                    (fun verifier -> verifier.VerifyIL [
                    """
                    .assembly extern runtime { }
                    .assembly extern FSharp.Core { }
                    .assembly assembly
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.FSharpInterfaceDataVersionAttribute::.ctor(int32,
                                                                                                                          int32,
                                                                                                                          int32) = ( 01 00 02 00 00 00 00 00 00 00 00 00 00 00 00 00 ) 
                      .hash algorithm 0x00008004
                      .ver 0:0:0:0
                    }
                    .mresource public FSharpSignatureData.assembly
                    {
                      
                      
                    }
                    .mresource public FSharpOptimizationData.assembly
                    {
                      
                      
                    }
                    .module assembly.dll
                    
                    .imagebase {value}
                    .file alignment 0x00000200
                    .stackreserve 0x00100000
                    .subsystem 0x0003       
                    .corflags 0x00000001    
                    
                    
                    
                    
                    
                    .class public abstract auto ansi sealed Test
                           extends [runtime]System.Object
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 07 00 00 00 00 00 ) 
                      .class auto ansi serializable sealed nested assembly beforefieldinit test@1
                             extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,uint64>
                      {
                        .field public uint64 start
                        .field public uint64 step
                        .method assembly specialname rtspecialname 
                                instance void  .ctor(uint64 start,
                                                     uint64 step) cil managed
                        {
                          .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                          .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,uint64>::.ctor()
                          IL_0006:  ldarg.0
                          IL_0007:  ldarg.1
                          IL_0008:  stfld      uint64 Test/test@1::start
                          IL_000d:  ldarg.0
                          IL_000e:  ldarg.2
                          IL_000f:  stfld      uint64 Test/test@1::step
                          IL_0014:  ret
                        } 
                    
                        .method public strict virtual instance uint64 
                                Invoke(int32 i) cil managed
                        {
                          
                          .maxstack  8
                          IL_0000:  ldarg.1
                          IL_0001:  ldarg.0
                          IL_0002:  ldfld      uint64 Test/test@1::step
                          IL_0007:  mul
                          IL_0008:  ldarg.0
                          IL_0009:  ldfld      uint64 Test/test@1::start
                          IL_000e:  add
                          IL_000f:  ret
                        } 
                    
                      } 
                    
                      .method public static uint64[]  test(uint64 start,
                                                           uint64 step,
                                                           uint64 finish) cil managed
                      {
                        .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationArgumentCountsAttribute::.ctor(int32[]) = ( 01 00 03 00 00 00 01 00 00 00 01 00 00 00 01 00 
                                                                                                                                        00 00 00 00 ) 
                        
                        .maxstack  5
                        .locals init (uint64 V_0)
                        IL_0000:  ldarg.1
                        IL_0001:  brtrue.s   IL_0015
                    
                        IL_0003:  ldstr      "step"
                        IL_0008:  ldstr      "The step of a range cannot be zero."
                        IL_000d:  tail.
                        IL_000f:  call       !!0 [FSharp.Core]Microsoft.FSharp.Core.Operators::InvalidArg<uint64[]>(string,
                                                                                                                    string)
                        IL_0014:  ret
                    
                        IL_0015:  ldarg.2
                        IL_0016:  ldarg.0
                        IL_0017:  sub
                        IL_0018:  ldarg.1
                        IL_0019:  div
                        IL_001a:  ldarg.1
                        IL_001b:  add
                        IL_001c:  stloc.0
                        IL_001d:  ldloc.0
                        IL_001e:  brtrue.s   IL_0026
                    
                        IL_0020:  call       !!0[] [runtime]System.Array::Empty<uint64>()
                        IL_0025:  ret
                    
                        IL_0026:  ldloc.0
                        IL_0027:  ldarg.0
                        IL_0028:  ldarg.1
                        IL_0029:  newobj     instance void Test/test@1::.ctor(uint64,
                                                                              uint64)
                        IL_002e:  tail.
                        IL_0030:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::Initialize<uint64>(int32,
                                                                                                                             class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,!!0>)
                        IL_0035:  ret
                      } 
                    
                    } 
                    
                    .class private abstract auto ansi sealed '<StartupCode$assembly>'.$Test
                           extends [runtime]System.Object
                    {
                    }
                    """
                    ]))

            [<Test>]
            let ``Lone RangeUInt64 with dynamic args that are complex exprs stores those in vars before calling init`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions([|"--optimize+"|],
                    """
                    module Test

                    let a () = uint64 (Array.zeroCreate 10).Length
                    let b () = uint64 (Array.zeroCreate 20).Length
                    let c () = uint64 (Array.zeroCreate 300).Length

                    let test () = [|a () .. b () .. c ()|]
                    """,
                    (fun verifier -> verifier.VerifyIL [
                    """
                    .assembly extern runtime { }
                    .assembly extern FSharp.Core { }
                    .assembly assembly
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.FSharpInterfaceDataVersionAttribute::.ctor(int32,
                                                                                                                          int32,
                                                                                                                          int32) = ( 01 00 02 00 00 00 00 00 00 00 00 00 00 00 00 00 ) 
                      .hash algorithm 0x00008004
                      .ver 0:0:0:0
                    }
                    .mresource public FSharpSignatureData.assembly
                    {
                      
                      
                    }
                    .mresource public FSharpOptimizationData.assembly
                    {
                      
                      
                    }
                    .module assembly.dll
                    
                    .imagebase {value}
                    .file alignment 0x00000200
                    .stackreserve 0x00100000
                    .subsystem 0x0003       
                    .corflags 0x00000001    
                    
                    
                    
                    
                    
                    .class public abstract auto ansi sealed Test
                           extends [runtime]System.Object
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 07 00 00 00 00 00 ) 
                      .class auto ansi serializable sealed nested assembly beforefieldinit test@1
                             extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,uint64>
                      {
                        .field public uint64 start
                        .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
                        .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                        .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                        .field public uint64 step
                        .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
                        .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                        .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                        .method assembly specialname rtspecialname 
                                instance void  .ctor(uint64 start,
                                                     uint64 step) cil managed
                        {
                          .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                          .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,uint64>::.ctor()
                          IL_0006:  ldarg.0
                          IL_0007:  ldarg.1
                          IL_0008:  stfld      uint64 Test/test@1::start
                          IL_000d:  ldarg.0
                          IL_000e:  ldarg.2
                          IL_000f:  stfld      uint64 Test/test@1::step
                          IL_0014:  ret
                        } 
                    
                        .method public strict virtual instance uint64 
                                Invoke(int32 i) cil managed
                        {
                          
                          .maxstack  8
                          IL_0000:  ldarg.1
                          IL_0001:  ldarg.0
                          IL_0002:  ldfld      uint64 Test/test@1::step
                          IL_0007:  mul
                          IL_0008:  ldarg.0
                          IL_0009:  ldfld      uint64 Test/test@1::start
                          IL_000e:  add
                          IL_000f:  ret
                        } 
                    
                      } 
                    
                      .method public static uint64  a() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4.s   10
                        IL_0002:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0007:  ldlen
                        IL_0008:  conv.i4
                        IL_0009:  conv.i8
                        IL_000a:  ret
                      } 
                    
                      .method public static uint64  b() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4.s   20
                        IL_0002:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0007:  ldlen
                        IL_0008:  conv.i4
                        IL_0009:  conv.i8
                        IL_000a:  ret
                      } 
                    
                      .method public static uint64  c() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4     0x12c
                        IL_0005:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_000a:  ldlen
                        IL_000b:  conv.i4
                        IL_000c:  conv.i8
                        IL_000d:  ret
                      } 
                    
                      .method public static uint64[]  test() cil managed
                      {
                        
                        .maxstack  5
                        .locals init (uint64 V_0,
                                 uint64 V_1,
                                 uint64 V_2,
                                 uint64 V_3)
                        IL_0000:  ldc.i4.s   10
                        IL_0002:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0007:  ldlen
                        IL_0008:  conv.i4
                        IL_0009:  conv.i8
                        IL_000a:  stloc.0
                        IL_000b:  ldc.i4.s   20
                        IL_000d:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0012:  ldlen
                        IL_0013:  conv.i4
                        IL_0014:  conv.i8
                        IL_0015:  stloc.1
                        IL_0016:  ldc.i4     0x12c
                        IL_001b:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0020:  ldlen
                        IL_0021:  conv.i4
                        IL_0022:  conv.i8
                        IL_0023:  stloc.2
                        IL_0024:  ldloc.1
                        IL_0025:  brtrue.s   IL_0039
                    
                        IL_0027:  ldstr      "step"
                        IL_002c:  ldstr      "The step of a range cannot be zero."
                        IL_0031:  tail.
                        IL_0033:  call       !!0 [FSharp.Core]Microsoft.FSharp.Core.Operators::InvalidArg<uint64[]>(string,
                                                                                                                    string)
                        IL_0038:  ret
                    
                        IL_0039:  ldloc.2
                        IL_003a:  ldloc.0
                        IL_003b:  sub
                        IL_003c:  ldloc.1
                        IL_003d:  div
                        IL_003e:  ldloc.1
                        IL_003f:  add
                        IL_0040:  stloc.3
                        IL_0041:  ldloc.3
                        IL_0042:  brtrue.s   IL_004a
                    
                        IL_0044:  call       !!0[] [runtime]System.Array::Empty<uint64>()
                        IL_0049:  ret
                    
                        IL_004a:  ldloc.3
                        IL_004b:  ldloc.0
                        IL_004c:  ldloc.1
                        IL_004d:  newobj     instance void Test/test@1::.ctor(uint64,
                                                                              uint64)
                        IL_0052:  tail.
                        IL_0054:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::Initialize<uint64>(int32,
                                                                                                                             class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,!!0>)
                        IL_0059:  ret
                      } 
                    
                    } 
                    
                    .class private abstract auto ansi sealed '<StartupCode$assembly>'.$Test
                           extends [runtime]System.Object
                    {
                    }
                    """
                    ]))

    module List =
        /// [start..finish]
        module Range =
            [<Test>]
            let ``Lone RangeUInt64 with const args when start > finish lowers to empty list`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions([|"--optimize+"|],
                    """
                    module Test

                    let test () = [10UL..1UL]
                    """,
                    (fun verifier -> verifier.VerifyIL [
                    """
                    .assembly extern runtime { }
                    .assembly extern FSharp.Core { }
                    .assembly assembly
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.FSharpInterfaceDataVersionAttribute::.ctor(int32,
                                                                                                                          int32,
                                                                                                                          int32) = ( 01 00 02 00 00 00 00 00 00 00 00 00 00 00 00 00 ) 
                      .hash algorithm 0x00008004
                      .ver 0:0:0:0
                    }
                    .mresource public FSharpSignatureData.assembly
                    {
                      
                      
                    }
                    .mresource public FSharpOptimizationData.assembly
                    {
                      
                      
                    }
                    .module assembly.dll
                    
                    .imagebase {value}
                    .file alignment 0x00000200
                    .stackreserve 0x00100000
                    .subsystem 0x0003       
                    .corflags 0x00000001    
                    
                    
                    
                    
                    
                    .class public abstract auto ansi sealed Test
                           extends [runtime]System.Object
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 07 00 00 00 00 00 ) 
                      .method public static class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<uint64> 
                              test() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  call       class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<!0> class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<uint64>::get_Empty()
                        IL_0005:  ret
                      } 
                    
                    } 
                    
                    .class private abstract auto ansi sealed '<StartupCode$assembly>'.$Test
                           extends [runtime]System.Object
                    {
                    }
                    """
                    ]))

            [<Test>]
            let ``Lone RangeUInt64 with const args lowers to call to init`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions([|"--optimize+"|],
                    """
                    module Test

                    let test () = [1UL..101UL]
                    """,
                    (fun verifier -> verifier.VerifyIL [
                    """
                    .assembly extern runtime { }
                    .assembly extern FSharp.Core { }
                    .assembly assembly
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.FSharpInterfaceDataVersionAttribute::.ctor(int32,
                                                                                                                          int32,
                                                                                                                          int32) = ( 01 00 02 00 00 00 00 00 00 00 00 00 00 00 00 00 ) 
                      .hash algorithm 0x00008004
                      .ver 0:0:0:0
                    }
                    .mresource public FSharpSignatureData.assembly
                    {
                      
                      
                    }
                    .mresource public FSharpOptimizationData.assembly
                    {
                      
                      
                    }
                    .module assembly.dll
                    
                    .imagebase {value}
                    .file alignment 0x00000200
                    .stackreserve 0x00100000
                    .subsystem 0x0003       
                    .corflags 0x00000001    
                    
                    
                    
                    
                    
                    .class public abstract auto ansi sealed Test
                           extends [runtime]System.Object
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 07 00 00 00 00 00 ) 
                      .class auto ansi serializable sealed nested assembly beforefieldinit test@1
                             extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,uint64>
                      {
                        .field static assembly initonly class Test/test@1 @_instance
                        .method assembly specialname rtspecialname 
                                instance void  .ctor() cil managed
                        {
                          .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                          .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,uint64>::.ctor()
                          IL_0006:  ret
                        } 
                    
                        .method public strict virtual instance uint64 
                                Invoke(int32 i) cil managed
                        {
                          
                          .maxstack  8
                          IL_0000:  ldc.i4.1
                          IL_0001:  conv.i8
                          IL_0002:  ldarg.1
                          IL_0003:  add
                          IL_0004:  ret
                        } 
                    
                        .method private specialname rtspecialname static 
                                void  .cctor() cil managed
                        {
                          
                          .maxstack  10
                          IL_0000:  newobj     instance void Test/test@1::.ctor()
                          IL_0005:  stsfld     class Test/test@1 Test/test@1::@_instance
                          IL_000a:  ret
                        } 
                    
                      } 
                    
                      .method public static class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<uint64> 
                              test() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4.s   101
                        IL_0002:  conv.i8
                        IL_0003:  ldsfld     class Test/test@1 Test/test@1::@_instance
                        IL_0008:  tail.
                        IL_000a:  call       class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<!!0> [FSharp.Core]Microsoft.FSharp.Collections.ListModule::Initialize<uint64>(int32,
                                                                                                                                                                                        class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,!!0>)
                        IL_000f:  ret
                      } 
                    
                    } 
                    
                    .class private abstract auto ansi sealed '<StartupCode$assembly>'.$Test
                           extends [runtime]System.Object
                    {
                    }
                    """
                    ]))

            [<Test>]
            let ``Lone RangeUInt64 with dynamic args lowers to call to init``() =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions([|"--optimize+"|],
                    """
                    module Test

                    let test (start : uint64) (finish : uint64) = [start..finish]
                    """,
                    (fun verifier -> verifier.VerifyIL [
                    """
                    .assembly extern runtime { }
                    .assembly extern FSharp.Core { }
                    .assembly assembly
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.FSharpInterfaceDataVersionAttribute::.ctor(int32,
                                                                                                                          int32,
                                                                                                                          int32) = ( 01 00 02 00 00 00 00 00 00 00 00 00 00 00 00 00 ) 
                      .hash algorithm 0x00008004
                      .ver 0:0:0:0
                    }
                    .mresource public FSharpSignatureData.assembly
                    {
                      
                      
                    }
                    .mresource public FSharpOptimizationData.assembly
                    {
                      
                      
                    }
                    .module assembly.dll
                    
                    .imagebase {value}
                    .file alignment 0x00000200
                    .stackreserve 0x00100000
                    .subsystem 0x0003       
                    .corflags 0x00000001    
                    
                    
                    
                    
                    
                    .class public abstract auto ansi sealed Test
                           extends [runtime]System.Object
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 07 00 00 00 00 00 ) 
                      .class auto ansi serializable sealed nested assembly beforefieldinit test@1
                             extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,uint64>
                      {
                        .field public uint64 start
                        .method assembly specialname rtspecialname 
                                instance void  .ctor(uint64 start) cil managed
                        {
                          .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                          .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,uint64>::.ctor()
                          IL_0006:  ldarg.0
                          IL_0007:  ldarg.1
                          IL_0008:  stfld      uint64 Test/test@1::start
                          IL_000d:  ret
                        } 
                    
                        .method public strict virtual instance uint64 
                                Invoke(int32 i) cil managed
                        {
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  ldfld      uint64 Test/test@1::start
                          IL_0006:  ldarg.1
                          IL_0007:  add
                          IL_0008:  ret
                        } 
                    
                      } 
                    
                      .method public static class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<uint64> 
                              test(uint64 start,
                                   uint64 finish) cil managed
                      {
                        .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationArgumentCountsAttribute::.ctor(int32[]) = ( 01 00 02 00 00 00 01 00 00 00 01 00 00 00 00 00 ) 
                        
                        .maxstack  8
                        IL_0000:  ldarg.1
                        IL_0001:  ldarg.0
                        IL_0002:  bge.s      IL_000a
                    
                        IL_0004:  call       class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<!0> class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<uint64>::get_Empty()
                        IL_0009:  ret
                    
                        IL_000a:  ldarg.1
                        IL_000b:  ldarg.0
                        IL_000c:  sub
                        IL_000d:  ldc.i4.1
                        IL_000e:  conv.i8
                        IL_000f:  add
                        IL_0010:  ldarg.0
                        IL_0011:  newobj     instance void Test/test@1::.ctor(uint64)
                        IL_0016:  tail.
                        IL_0018:  call       class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<!!0> [FSharp.Core]Microsoft.FSharp.Collections.ListModule::Initialize<uint64>(int32,
                                                                                                                                                                                        class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,!!0>)
                        IL_001d:  ret
                      } 
                    
                    } 
                    
                    .class private abstract auto ansi sealed '<StartupCode$assembly>'.$Test
                           extends [runtime]System.Object
                    {
                    }
                    """
                    ]))

            [<Test>]
            let ``Lone RangeUInt64 with dynamic args that are complex exprs stores those in vars before calling init`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions([|"--optimize+"|],
                    """
                    module Test

                    let a () = uint64 (Array.zeroCreate 10).Length
                    let b () = uint64 (Array.zeroCreate 20).Length

                    let test () = [a ()..b ()]
                    """,
                    (fun verifier -> verifier.VerifyIL [
                    """
                    .assembly extern runtime { }
                    .assembly extern FSharp.Core { }
                    .assembly assembly
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.FSharpInterfaceDataVersionAttribute::.ctor(int32,
                                                                                                                          int32,
                                                                                                                          int32) = ( 01 00 02 00 00 00 00 00 00 00 00 00 00 00 00 00 ) 
                      .hash algorithm 0x00008004
                      .ver 0:0:0:0
                    }
                    .mresource public FSharpSignatureData.assembly
                    {
                      
                      
                    }
                    .mresource public FSharpOptimizationData.assembly
                    {
                      
                      
                    }
                    .module assembly.dll
                    
                    .imagebase {value}
                    .file alignment 0x00000200
                    .stackreserve 0x00100000
                    .subsystem 0x0003       
                    .corflags 0x00000001    
                    
                    
                    
                    
                    
                    .class public abstract auto ansi sealed Test
                           extends [runtime]System.Object
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 07 00 00 00 00 00 ) 
                      .class auto ansi serializable sealed nested assembly beforefieldinit test@1
                             extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,uint64>
                      {
                        .field public uint64 start
                        .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
                        .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                        .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                        .method assembly specialname rtspecialname 
                                instance void  .ctor(uint64 start) cil managed
                        {
                          .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                          .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,uint64>::.ctor()
                          IL_0006:  ldarg.0
                          IL_0007:  ldarg.1
                          IL_0008:  stfld      uint64 Test/test@1::start
                          IL_000d:  ret
                        } 
                    
                        .method public strict virtual instance uint64 
                                Invoke(int32 i) cil managed
                        {
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  ldfld      uint64 Test/test@1::start
                          IL_0006:  ldarg.1
                          IL_0007:  add
                          IL_0008:  ret
                        } 
                    
                      } 
                    
                      .method public static uint64  a() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4.s   10
                        IL_0002:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0007:  ldlen
                        IL_0008:  conv.i4
                        IL_0009:  conv.i8
                        IL_000a:  ret
                      } 
                    
                      .method public static uint64  b() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4.s   20
                        IL_0002:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0007:  ldlen
                        IL_0008:  conv.i4
                        IL_0009:  conv.i8
                        IL_000a:  ret
                      } 
                    
                      .method public static class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<uint64> 
                              test() cil managed
                      {
                        
                        .maxstack  4
                        .locals init (uint64 V_0,
                                 uint64 V_1)
                        IL_0000:  ldc.i4.s   10
                        IL_0002:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0007:  ldlen
                        IL_0008:  conv.i4
                        IL_0009:  conv.i8
                        IL_000a:  stloc.0
                        IL_000b:  ldc.i4.s   20
                        IL_000d:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0012:  ldlen
                        IL_0013:  conv.i4
                        IL_0014:  conv.i8
                        IL_0015:  stloc.1
                        IL_0016:  ldloc.1
                        IL_0017:  ldloc.0
                        IL_0018:  bge.s      IL_0020
                    
                        IL_001a:  call       class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<!0> class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<uint64>::get_Empty()
                        IL_001f:  ret
                    
                        IL_0020:  ldloc.1
                        IL_0021:  ldloc.0
                        IL_0022:  sub
                        IL_0023:  ldc.i4.1
                        IL_0024:  conv.i8
                        IL_0025:  add
                        IL_0026:  ldloc.0
                        IL_0027:  newobj     instance void Test/test@1::.ctor(uint64)
                        IL_002c:  tail.
                        IL_002e:  call       class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<!!0> [FSharp.Core]Microsoft.FSharp.Collections.ListModule::Initialize<uint64>(int32,
                                                                                                                                                                                        class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,!!0>)
                        IL_0033:  ret
                      } 
                    
                    } 
                    
                    .class private abstract auto ansi sealed '<StartupCode$assembly>'.$Test
                           extends [runtime]System.Object
                    {
                    }
                    """
                    ]))

        /// [start..step..finish]
        module RangeStep =
            [<Test>]
            let ``Lone RangeUInt64 with const args when step = 0 lowers to call to invalidArg`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions(["--optimize+"],
                    """
                    module Test

                    let test () = [1UL..0UL..5UL]
                    """,
                    (fun verifier -> verifier.VerifyIL [
                    """
                    .assembly extern runtime { }
                    .assembly extern FSharp.Core { }
                    .assembly assembly
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.FSharpInterfaceDataVersionAttribute::.ctor(int32,
                                                                                                                          int32,
                                                                                                                          int32) = ( 01 00 02 00 00 00 00 00 00 00 00 00 00 00 00 00 ) 
                      .hash algorithm 0x00008004
                      .ver 0:0:0:0
                    }
                    .mresource public FSharpSignatureData.assembly
                    {
                      
                      
                    }
                    .mresource public FSharpOptimizationData.assembly
                    {
                      
                      
                    }
                    .module assembly.dll
                    
                    .imagebase {value}
                    .file alignment 0x00000200
                    .stackreserve 0x00100000
                    .subsystem 0x0003       
                    .corflags 0x00000001    
                    
                    
                    
                    
                    
                    .class public abstract auto ansi sealed Test
                           extends [runtime]System.Object
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 07 00 00 00 00 00 ) 
                      .method public static class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<uint64> 
                              test() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldstr      "step"
                        IL_0005:  ldstr      "The step of a range cannot be zero."
                        IL_000a:  tail.
                        IL_000c:  call       !!0 [FSharp.Core]Microsoft.FSharp.Core.Operators::InvalidArg<class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<uint64>>(string,
                                                                                                                                                                                string)
                        IL_0011:  ret
                      } 
                    
                    } 
                    
                    .class private abstract auto ansi sealed '<StartupCode$assembly>'.$Test
                           extends [runtime]System.Object
                    {
                    }
                    """
                    ]))

            [<Test>]
            let ``Lone RangeUInt64 with const args when (finish - start) / step + 1 ≤ 0 lowers to empty array`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions(["--optimize+"],
                    """
                    module Test

                    let test () = [5UL..1UL..1UL]
                    """,
                    (fun verifier -> verifier.VerifyIL [
                    """
                    .assembly extern runtime { }
                    .assembly extern FSharp.Core { }
                    .assembly assembly
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.FSharpInterfaceDataVersionAttribute::.ctor(int32,
                                                                                                                          int32,
                                                                                                                          int32) = ( 01 00 02 00 00 00 00 00 00 00 00 00 00 00 00 00 ) 
                      .hash algorithm 0x00008004
                      .ver 0:0:0:0
                    }
                    .mresource public FSharpSignatureData.assembly
                    {
                      
                      
                    }
                    .mresource public FSharpOptimizationData.assembly
                    {
                      
                      
                    }
                    .module assembly.dll
                    
                    .imagebase {value}
                    .file alignment 0x00000200
                    .stackreserve 0x00100000
                    .subsystem 0x0003       
                    .corflags 0x00000001    
                    
                    
                    
                    
                    
                    .class public abstract auto ansi sealed Test
                           extends [runtime]System.Object
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 07 00 00 00 00 00 ) 
                      .method public static class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<uint64> 
                              test() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  call       class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<!0> class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<uint64>::get_Empty()
                        IL_0005:  ret
                      } 
                    
                    } 
                    
                    .class private abstract auto ansi sealed '<StartupCode$assembly>'.$Test
                           extends [runtime]System.Object
                    {
                    }
                    """
                    ]))

            [<Test>]
            let ``Lone RangeUInt64 with const args lowers to call to init`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions(["--optimize+"],
                    """
                    module Test

                    let test () = [1UL..2UL..257UL]
                    """,
                    (fun verifier -> verifier.VerifyIL [
                    """
                    .assembly extern runtime { }
                    .assembly extern FSharp.Core { }
                    .assembly assembly
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.FSharpInterfaceDataVersionAttribute::.ctor(int32,
                                                                                                                          int32,
                                                                                                                          int32) = ( 01 00 02 00 00 00 00 00 00 00 00 00 00 00 00 00 ) 
                      .hash algorithm 0x00008004
                      .ver 0:0:0:0
                    }
                    .mresource public FSharpSignatureData.assembly
                    {
                      
                      
                    }
                    .mresource public FSharpOptimizationData.assembly
                    {
                      
                      
                    }
                    .module assembly.dll
                    
                    .imagebase {value}
                    .file alignment 0x00000200
                    .stackreserve 0x00100000
                    .subsystem 0x0003       
                    .corflags 0x00000001    
                    
                    
                    
                    
                    
                    .class public abstract auto ansi sealed Test
                           extends [runtime]System.Object
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 07 00 00 00 00 00 ) 
                      .class auto ansi serializable sealed nested assembly beforefieldinit test@1
                             extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,uint64>
                      {
                        .field static assembly initonly class Test/test@1 @_instance
                        .method assembly specialname rtspecialname 
                                instance void  .ctor() cil managed
                        {
                          .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                          .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,uint64>::.ctor()
                          IL_0006:  ret
                        } 
                    
                        .method public strict virtual instance uint64 
                                Invoke(int32 i) cil managed
                        {
                          
                          .maxstack  8
                          IL_0000:  ldarg.1
                          IL_0001:  ldc.i4.2
                          IL_0002:  conv.i8
                          IL_0003:  mul
                          IL_0004:  ldc.i4.1
                          IL_0005:  conv.i8
                          IL_0006:  add
                          IL_0007:  ret
                        } 
                    
                        .method private specialname rtspecialname static 
                                void  .cctor() cil managed
                        {
                          
                          .maxstack  10
                          IL_0000:  newobj     instance void Test/test@1::.ctor()
                          IL_0005:  stsfld     class Test/test@1 Test/test@1::@_instance
                          IL_000a:  ret
                        } 
                    
                      } 
                    
                      .method public static class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<uint64> 
                              test() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4     0x81
                        IL_0005:  conv.i8
                        IL_0006:  ldsfld     class Test/test@1 Test/test@1::@_instance
                        IL_000b:  tail.
                        IL_000d:  call       class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<!!0> [FSharp.Core]Microsoft.FSharp.Collections.ListModule::Initialize<uint64>(int32,
                                                                                                                                                                                        class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,!!0>)
                        IL_0012:  ret
                      } 
                    
                    } 
                    
                    .class private abstract auto ansi sealed '<StartupCode$assembly>'.$Test
                           extends [runtime]System.Object
                    {
                    }
                    """
                    ]))

            [<Test>]
            let ``Lone RangeUInt64 with dynamic args lowers to call to init`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions(["--optimize+"],
                    """
                    module Test

                    let test (start : uint64) (step : uint64) (finish : uint64) = [start..step..finish]
                    """,
                    (fun verifier -> verifier.VerifyIL [
                    """
                    .assembly extern runtime { }
                    .assembly extern FSharp.Core { }
                    .assembly assembly
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.FSharpInterfaceDataVersionAttribute::.ctor(int32,
                                                                                                                          int32,
                                                                                                                          int32) = ( 01 00 02 00 00 00 00 00 00 00 00 00 00 00 00 00 ) 
                      .hash algorithm 0x00008004
                      .ver 0:0:0:0
                    }
                    .mresource public FSharpSignatureData.assembly
                    {
                      
                      
                    }
                    .mresource public FSharpOptimizationData.assembly
                    {
                      
                      
                    }
                    .module assembly.dll
                    
                    .imagebase {value}
                    .file alignment 0x00000200
                    .stackreserve 0x00100000
                    .subsystem 0x0003       
                    .corflags 0x00000001    
                    
                    
                    
                    
                    
                    .class public abstract auto ansi sealed Test
                           extends [runtime]System.Object
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 07 00 00 00 00 00 ) 
                      .class auto ansi serializable sealed nested assembly beforefieldinit test@1
                             extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,uint64>
                      {
                        .field public uint64 start
                        .field public uint64 step
                        .method assembly specialname rtspecialname 
                                instance void  .ctor(uint64 start,
                                                     uint64 step) cil managed
                        {
                          .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                          .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,uint64>::.ctor()
                          IL_0006:  ldarg.0
                          IL_0007:  ldarg.1
                          IL_0008:  stfld      uint64 Test/test@1::start
                          IL_000d:  ldarg.0
                          IL_000e:  ldarg.2
                          IL_000f:  stfld      uint64 Test/test@1::step
                          IL_0014:  ret
                        } 
                    
                        .method public strict virtual instance uint64 
                                Invoke(int32 i) cil managed
                        {
                          
                          .maxstack  8
                          IL_0000:  ldarg.1
                          IL_0001:  ldarg.0
                          IL_0002:  ldfld      uint64 Test/test@1::step
                          IL_0007:  mul
                          IL_0008:  ldarg.0
                          IL_0009:  ldfld      uint64 Test/test@1::start
                          IL_000e:  add
                          IL_000f:  ret
                        } 
                    
                      } 
                    
                      .method public static class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<uint64> 
                              test(uint64 start,
                                   uint64 step,
                                   uint64 finish) cil managed
                      {
                        .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationArgumentCountsAttribute::.ctor(int32[]) = ( 01 00 03 00 00 00 01 00 00 00 01 00 00 00 01 00 
                                                                                                                                        00 00 00 00 ) 
                        
                        .maxstack  5
                        .locals init (uint64 V_0)
                        IL_0000:  ldarg.1
                        IL_0001:  brtrue.s   IL_0015
                    
                        IL_0003:  ldstr      "step"
                        IL_0008:  ldstr      "The step of a range cannot be zero."
                        IL_000d:  tail.
                        IL_000f:  call       !!0 [FSharp.Core]Microsoft.FSharp.Core.Operators::InvalidArg<class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<uint64>>(string,
                                                                                                                                                                                string)
                        IL_0014:  ret
                    
                        IL_0015:  ldarg.2
                        IL_0016:  ldarg.0
                        IL_0017:  sub
                        IL_0018:  ldarg.1
                        IL_0019:  div
                        IL_001a:  ldarg.1
                        IL_001b:  add
                        IL_001c:  stloc.0
                        IL_001d:  ldloc.0
                        IL_001e:  brtrue.s   IL_0026
                    
                        IL_0020:  call       class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<!0> class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<uint64>::get_Empty()
                        IL_0025:  ret
                    
                        IL_0026:  ldloc.0
                        IL_0027:  ldarg.0
                        IL_0028:  ldarg.1
                        IL_0029:  newobj     instance void Test/test@1::.ctor(uint64,
                                                                              uint64)
                        IL_002e:  tail.
                        IL_0030:  call       class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<!!0> [FSharp.Core]Microsoft.FSharp.Collections.ListModule::Initialize<uint64>(int32,
                                                                                                                                                                                        class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,!!0>)
                        IL_0035:  ret
                      } 
                    
                    } 
                    
                    .class private abstract auto ansi sealed '<StartupCode$assembly>'.$Test
                           extends [runtime]System.Object
                    {
                    }
                    """
                    ]))

            [<Test>]
            let ``Lone RangeUInt64 with dynamic args that are complex exprs stores those in vars before calling init`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions(["--optimize+"],
                    """
                    module Test

                    let a () = uint64 (Array.zeroCreate 10).Length
                    let b () = uint64 (Array.zeroCreate 20).Length
                    let c () = uint64 (Array.zeroCreate 300).Length

                    let test () = [a () .. b () .. c ()]
                    """,
                    (fun verifier -> verifier.VerifyIL [
                    """
                    .assembly extern runtime { }
                    .assembly extern FSharp.Core { }
                    .assembly assembly
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.FSharpInterfaceDataVersionAttribute::.ctor(int32,
                                                                                                                          int32,
                                                                                                                          int32) = ( 01 00 02 00 00 00 00 00 00 00 00 00 00 00 00 00 ) 
                      .hash algorithm 0x00008004
                      .ver 0:0:0:0
                    }
                    .mresource public FSharpSignatureData.assembly
                    {
                      
                      
                    }
                    .mresource public FSharpOptimizationData.assembly
                    {
                      
                      
                    }
                    .module assembly.dll
                    
                    .imagebase {value}
                    .file alignment 0x00000200
                    .stackreserve 0x00100000
                    .subsystem 0x0003       
                    .corflags 0x00000001    
                    
                    
                    
                    
                    
                    .class public abstract auto ansi sealed Test
                           extends [runtime]System.Object
                    {
                      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 07 00 00 00 00 00 ) 
                      .class auto ansi serializable sealed nested assembly beforefieldinit test@1
                             extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,uint64>
                      {
                        .field public uint64 start
                        .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
                        .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                        .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                        .field public uint64 step
                        .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
                        .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                        .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                        .method assembly specialname rtspecialname 
                                instance void  .ctor(uint64 start,
                                                     uint64 step) cil managed
                        {
                          .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                          .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,uint64>::.ctor()
                          IL_0006:  ldarg.0
                          IL_0007:  ldarg.1
                          IL_0008:  stfld      uint64 Test/test@1::start
                          IL_000d:  ldarg.0
                          IL_000e:  ldarg.2
                          IL_000f:  stfld      uint64 Test/test@1::step
                          IL_0014:  ret
                        } 
                    
                        .method public strict virtual instance uint64 
                                Invoke(int32 i) cil managed
                        {
                          
                          .maxstack  8
                          IL_0000:  ldarg.1
                          IL_0001:  ldarg.0
                          IL_0002:  ldfld      uint64 Test/test@1::step
                          IL_0007:  mul
                          IL_0008:  ldarg.0
                          IL_0009:  ldfld      uint64 Test/test@1::start
                          IL_000e:  add
                          IL_000f:  ret
                        } 
                    
                      } 
                    
                      .method public static uint64  a() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4.s   10
                        IL_0002:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0007:  ldlen
                        IL_0008:  conv.i4
                        IL_0009:  conv.i8
                        IL_000a:  ret
                      } 
                    
                      .method public static uint64  b() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4.s   20
                        IL_0002:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0007:  ldlen
                        IL_0008:  conv.i4
                        IL_0009:  conv.i8
                        IL_000a:  ret
                      } 
                    
                      .method public static uint64  c() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4     0x12c
                        IL_0005:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_000a:  ldlen
                        IL_000b:  conv.i4
                        IL_000c:  conv.i8
                        IL_000d:  ret
                      } 
                    
                      .method public static class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<uint64> 
                              test() cil managed
                      {
                        
                        .maxstack  5
                        .locals init (uint64 V_0,
                                 uint64 V_1,
                                 uint64 V_2,
                                 uint64 V_3)
                        IL_0000:  ldc.i4.s   10
                        IL_0002:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0007:  ldlen
                        IL_0008:  conv.i4
                        IL_0009:  conv.i8
                        IL_000a:  stloc.0
                        IL_000b:  ldc.i4.s   20
                        IL_000d:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0012:  ldlen
                        IL_0013:  conv.i4
                        IL_0014:  conv.i8
                        IL_0015:  stloc.1
                        IL_0016:  ldc.i4     0x12c
                        IL_001b:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0020:  ldlen
                        IL_0021:  conv.i4
                        IL_0022:  conv.i8
                        IL_0023:  stloc.2
                        IL_0024:  ldloc.1
                        IL_0025:  brtrue.s   IL_0039
                    
                        IL_0027:  ldstr      "step"
                        IL_002c:  ldstr      "The step of a range cannot be zero."
                        IL_0031:  tail.
                        IL_0033:  call       !!0 [FSharp.Core]Microsoft.FSharp.Core.Operators::InvalidArg<class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<uint64>>(string,
                                                                                                                                                                                string)
                        IL_0038:  ret
                    
                        IL_0039:  ldloc.2
                        IL_003a:  ldloc.0
                        IL_003b:  sub
                        IL_003c:  ldloc.1
                        IL_003d:  div
                        IL_003e:  ldloc.1
                        IL_003f:  add
                        IL_0040:  stloc.3
                        IL_0041:  ldloc.3
                        IL_0042:  brtrue.s   IL_004a
                    
                        IL_0044:  call       class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<!0> class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<uint64>::get_Empty()
                        IL_0049:  ret
                    
                        IL_004a:  ldloc.3
                        IL_004b:  ldloc.0
                        IL_004c:  ldloc.1
                        IL_004d:  newobj     instance void Test/test@1::.ctor(uint64,
                                                                              uint64)
                        IL_0052:  tail.
                        IL_0054:  call       class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<!!0> [FSharp.Core]Microsoft.FSharp.Collections.ListModule::Initialize<uint64>(int32,
                                                                                                                                                                                        class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,!!0>)
                        IL_0059:  ret
                      } 
                    
                    } 
                    
                    .class private abstract auto ansi sealed '<StartupCode$assembly>'.$Test
                           extends [runtime]System.Object
                    {
                    }
                    """
                    ]))

