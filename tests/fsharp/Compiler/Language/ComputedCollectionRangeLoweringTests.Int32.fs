﻿// Copyright (c) Microsoft Corporation.  All Rights Reserved.  See License.txt in the project root for license information.

namespace FSharp.Compiler.UnitTests.ComputedCollectionRangeLoweringTests

open NUnit.Framework
open FSharp.Test

[<TestFixture>]
module Int32 =
    module Array =
        /// [|start..finish|]
        module Range =
            [<Test>]
            let ``Lone RangeInt32 with const args when start > finish lowers to empty array`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions([|"--optimize+"|],
                    """
                    module Test

                    let test () = [|10..1|]
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
                      .method public static int32[]  test() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  call       !!0[] [runtime]System.Array::Empty<int32>()
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
            let ``Lone RangeInt32 with const args lowers to call to init`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions([|"--optimize+"|],
                    """
                    module Test

                    let test () = [|1..257|]
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
                             extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,int32>
                      {
                        .field static assembly initonly class Test/test@1 @_instance
                        .method assembly specialname rtspecialname 
                                instance void  .ctor() cil managed
                        {
                          .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                          .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,int32>::.ctor()
                          IL_0006:  ret
                        } 
                    
                        .method public strict virtual instance int32 
                                Invoke(int32 i) cil managed
                        {
                          
                          .maxstack  8
                          IL_0000:  ldc.i4.1
                          IL_0001:  ldarg.1
                          IL_0002:  add
                          IL_0003:  ret
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
                    
                      .method public static int32[]  test() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4     0x101
                        IL_0005:  ldsfld     class Test/test@1 Test/test@1::@_instance
                        IL_000a:  tail.
                        IL_000c:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::Initialize<int32>(int32,
                                                                                                                            class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,!!0>)
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
            let ``Lone RangeInt32 with dynamic args lowers to call to init`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions([|"--optimize+"|],
                    """
                    module Test

                    let test start finish = [|start..finish|]
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
                             extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,int32>
                      {
                        .field public int32 start
                        .method assembly specialname rtspecialname 
                                instance void  .ctor(int32 start) cil managed
                        {
                          .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                          .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,int32>::.ctor()
                          IL_0006:  ldarg.0
                          IL_0007:  ldarg.1
                          IL_0008:  stfld      int32 Test/test@1::start
                          IL_000d:  ret
                        } 
                    
                        .method public strict virtual instance int32 
                                Invoke(int32 i) cil managed
                        {
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  ldfld      int32 Test/test@1::start
                          IL_0006:  ldarg.1
                          IL_0007:  add
                          IL_0008:  ret
                        } 
                    
                      } 
                    
                      .method public static int32[]  test(int32 start,
                                                          int32 finish) cil managed
                      {
                        .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationArgumentCountsAttribute::.ctor(int32[]) = ( 01 00 02 00 00 00 01 00 00 00 01 00 00 00 00 00 ) 
                        
                        .maxstack  8
                        IL_0000:  ldarg.1
                        IL_0001:  ldarg.0
                        IL_0002:  bge.s      IL_000a
                    
                        IL_0004:  call       !!0[] [runtime]System.Array::Empty<int32>()
                        IL_0009:  ret
                    
                        IL_000a:  ldarg.1
                        IL_000b:  ldarg.0
                        IL_000c:  sub
                        IL_000d:  ldc.i4.1
                        IL_000e:  add
                        IL_000f:  ldarg.0
                        IL_0010:  newobj     instance void Test/test@1::.ctor(int32)
                        IL_0015:  tail.
                        IL_0017:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::Initialize<int32>(int32,
                                                                                                                            class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,!!0>)
                        IL_001c:  ret
                      } 
                    
                    } 
                    
                    .class private abstract auto ansi sealed '<StartupCode$assembly>'.$Test
                           extends [runtime]System.Object
                    {
                    }
                    """
                    ]))

            [<Test>]
            let ``Lone RangeInt32 with dynamic args that are complex exprs stores those in vars before calling init`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions([|"--optimize+"|],
                    """
                    module Test

                    let a () = (Array.zeroCreate 10).Length
                    let b () = (Array.zeroCreate 20).Length

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
                             extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,int32>
                      {
                        .field public int32 start
                        .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
                        .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                        .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                        .method assembly specialname rtspecialname 
                                instance void  .ctor(int32 start) cil managed
                        {
                          .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                          .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,int32>::.ctor()
                          IL_0006:  ldarg.0
                          IL_0007:  ldarg.1
                          IL_0008:  stfld      int32 Test/test@1::start
                          IL_000d:  ret
                        } 
                    
                        .method public strict virtual instance int32 
                                Invoke(int32 i) cil managed
                        {
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  ldfld      int32 Test/test@1::start
                          IL_0006:  ldarg.1
                          IL_0007:  add
                          IL_0008:  ret
                        } 
                    
                      } 
                    
                      .method public static int32  a() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4.s   10
                        IL_0002:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0007:  ldlen
                        IL_0008:  conv.i4
                        IL_0009:  ret
                      } 
                    
                      .method public static int32  b() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4.s   20
                        IL_0002:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0007:  ldlen
                        IL_0008:  conv.i4
                        IL_0009:  ret
                      } 
                    
                      .method public static int32[]  test() cil managed
                      {
                        
                        .maxstack  4
                        .locals init (int32 V_0,
                                 int32 V_1)
                        IL_0000:  ldc.i4.s   10
                        IL_0002:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0007:  ldlen
                        IL_0008:  conv.i4
                        IL_0009:  stloc.0
                        IL_000a:  ldc.i4.s   20
                        IL_000c:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0011:  ldlen
                        IL_0012:  conv.i4
                        IL_0013:  stloc.1
                        IL_0014:  ldloc.1
                        IL_0015:  ldloc.0
                        IL_0016:  bge.s      IL_001e
                    
                        IL_0018:  call       !!0[] [runtime]System.Array::Empty<int32>()
                        IL_001d:  ret
                    
                        IL_001e:  ldloc.1
                        IL_001f:  ldloc.0
                        IL_0020:  sub
                        IL_0021:  ldc.i4.1
                        IL_0022:  add
                        IL_0023:  ldloc.0
                        IL_0024:  newobj     instance void Test/test@1::.ctor(int32)
                        IL_0029:  tail.
                        IL_002b:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::Initialize<int32>(int32,
                                                                                                                            class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,!!0>)
                        IL_0030:  ret
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
            let ``Lone RangeInt32 with const args when step = 0 lowers to call to invalidArg`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions([|"--optimize+"|],
                    """
                    module Test

                    let test () = [|1..0..5|]
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
                      .method public static int32[]  test() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldstr      "step"
                        IL_0005:  ldstr      "The step of a range cannot be zero."
                        IL_000a:  tail.
                        IL_000c:  call       !!0 [FSharp.Core]Microsoft.FSharp.Core.Operators::InvalidArg<int32[]>(string,
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
            let ``Lone RangeInt32 with const args when (finish - start) / step + 1 ≤ 0 lowers to empty array`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions([|"--optimize+"|],
                    """
                    module Test

                    let test () = [|1..-1..5|]
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
                      .method public static int32[]  test() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  call       !!0[] [runtime]System.Array::Empty<int32>()
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
            let ``Lone RangeInt32 with const args lowers to call to init`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions([|"--optimize+"|],
                    """
                    module Test

                    let test () = [|1..2..257|]
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
                             extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,int32>
                      {
                        .field static assembly initonly class Test/test@1 @_instance
                        .method assembly specialname rtspecialname 
                                instance void  .ctor() cil managed
                        {
                          .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                          .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,int32>::.ctor()
                          IL_0006:  ret
                        } 
                    
                        .method public strict virtual instance int32 
                                Invoke(int32 i) cil managed
                        {
                          
                          .maxstack  8
                          IL_0000:  ldarg.1
                          IL_0001:  ldc.i4.2
                          IL_0002:  mul
                          IL_0003:  ldc.i4.1
                          IL_0004:  add
                          IL_0005:  ret
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
                    
                      .method public static int32[]  test() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4     0x81
                        IL_0005:  ldsfld     class Test/test@1 Test/test@1::@_instance
                        IL_000a:  tail.
                        IL_000c:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::Initialize<int32>(int32,
                                                                                                                            class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,!!0>)
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
            let ``Lone RangeInt32 with dynamic args lowers to call to init`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions([|"--optimize+"|],
                    """
                    module Test

                    let test start step finish = [|start..step..finish|]
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
                             extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,int32>
                      {
                        .field public int32 start
                        .field public int32 step
                        .method assembly specialname rtspecialname 
                                instance void  .ctor(int32 start,
                                                     int32 step) cil managed
                        {
                          .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                          .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,int32>::.ctor()
                          IL_0006:  ldarg.0
                          IL_0007:  ldarg.1
                          IL_0008:  stfld      int32 Test/test@1::start
                          IL_000d:  ldarg.0
                          IL_000e:  ldarg.2
                          IL_000f:  stfld      int32 Test/test@1::step
                          IL_0014:  ret
                        } 
                    
                        .method public strict virtual instance int32 
                                Invoke(int32 i) cil managed
                        {
                          
                          .maxstack  8
                          IL_0000:  ldarg.1
                          IL_0001:  ldarg.0
                          IL_0002:  ldfld      int32 Test/test@1::step
                          IL_0007:  mul
                          IL_0008:  ldarg.0
                          IL_0009:  ldfld      int32 Test/test@1::start
                          IL_000e:  add
                          IL_000f:  ret
                        } 
                    
                      } 
                    
                      .method public static int32[]  test(int32 start,
                                                          int32 step,
                                                          int32 finish) cil managed
                      {
                        .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationArgumentCountsAttribute::.ctor(int32[]) = ( 01 00 03 00 00 00 01 00 00 00 01 00 00 00 01 00 
                                                                                                                                        00 00 00 00 ) 
                        
                        .maxstack  5
                        .locals init (int32 V_0)
                        IL_0000:  ldarg.1
                        IL_0001:  brtrue.s   IL_0015
                    
                        IL_0003:  ldstr      "step"
                        IL_0008:  ldstr      "The step of a range cannot be zero."
                        IL_000d:  tail.
                        IL_000f:  call       !!0 [FSharp.Core]Microsoft.FSharp.Core.Operators::InvalidArg<int32[]>(string,
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
                    
                        IL_0020:  call       !!0[] [runtime]System.Array::Empty<int32>()
                        IL_0025:  ret
                    
                        IL_0026:  ldloc.0
                        IL_0027:  ldarg.0
                        IL_0028:  ldarg.1
                        IL_0029:  newobj     instance void Test/test@1::.ctor(int32,
                                                                              int32)
                        IL_002e:  tail.
                        IL_0030:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::Initialize<int32>(int32,
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
            let ``Lone RangeInt32 with dynamic args that are complex exprs stores those in vars before calling init`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions([|"--optimize+"|],
                    """
                    module Test

                    let a () = (Array.zeroCreate 10).Length
                    let b () = (Array.zeroCreate 20).Length
                    let c () = (Array.zeroCreate 300).Length

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
                             extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,int32>
                      {
                        .field public int32 start
                        .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
                        .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                        .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                        .field public int32 step
                        .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
                        .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                        .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                        .method assembly specialname rtspecialname 
                                instance void  .ctor(int32 start,
                                                     int32 step) cil managed
                        {
                          .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                          .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,int32>::.ctor()
                          IL_0006:  ldarg.0
                          IL_0007:  ldarg.1
                          IL_0008:  stfld      int32 Test/test@1::start
                          IL_000d:  ldarg.0
                          IL_000e:  ldarg.2
                          IL_000f:  stfld      int32 Test/test@1::step
                          IL_0014:  ret
                        } 
                    
                        .method public strict virtual instance int32 
                                Invoke(int32 i) cil managed
                        {
                          
                          .maxstack  8
                          IL_0000:  ldarg.1
                          IL_0001:  ldarg.0
                          IL_0002:  ldfld      int32 Test/test@1::step
                          IL_0007:  mul
                          IL_0008:  ldarg.0
                          IL_0009:  ldfld      int32 Test/test@1::start
                          IL_000e:  add
                          IL_000f:  ret
                        } 
                    
                      } 
                    
                      .method public static int32  a() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4.s   10
                        IL_0002:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0007:  ldlen
                        IL_0008:  conv.i4
                        IL_0009:  ret
                      } 
                    
                      .method public static int32  b() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4.s   20
                        IL_0002:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0007:  ldlen
                        IL_0008:  conv.i4
                        IL_0009:  ret
                      } 
                    
                      .method public static int32  c() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4     0x12c
                        IL_0005:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_000a:  ldlen
                        IL_000b:  conv.i4
                        IL_000c:  ret
                      } 
                    
                      .method public static int32[]  test() cil managed
                      {
                        
                        .maxstack  5
                        .locals init (int32 V_0,
                                 int32 V_1,
                                 int32 V_2,
                                 int32 V_3)
                        IL_0000:  ldc.i4.s   10
                        IL_0002:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0007:  ldlen
                        IL_0008:  conv.i4
                        IL_0009:  stloc.0
                        IL_000a:  ldc.i4.s   20
                        IL_000c:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0011:  ldlen
                        IL_0012:  conv.i4
                        IL_0013:  stloc.1
                        IL_0014:  ldc.i4     0x12c
                        IL_0019:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_001e:  ldlen
                        IL_001f:  conv.i4
                        IL_0020:  stloc.2
                        IL_0021:  ldloc.1
                        IL_0022:  brtrue.s   IL_0036
                    
                        IL_0024:  ldstr      "step"
                        IL_0029:  ldstr      "The step of a range cannot be zero."
                        IL_002e:  tail.
                        IL_0030:  call       !!0 [FSharp.Core]Microsoft.FSharp.Core.Operators::InvalidArg<int32[]>(string,
                                                                                                                   string)
                        IL_0035:  ret
                    
                        IL_0036:  ldloc.2
                        IL_0037:  ldloc.0
                        IL_0038:  sub
                        IL_0039:  ldloc.1
                        IL_003a:  div
                        IL_003b:  ldloc.1
                        IL_003c:  add
                        IL_003d:  stloc.3
                        IL_003e:  ldloc.3
                        IL_003f:  brtrue.s   IL_0047
                    
                        IL_0041:  call       !!0[] [runtime]System.Array::Empty<int32>()
                        IL_0046:  ret
                    
                        IL_0047:  ldloc.3
                        IL_0048:  ldloc.0
                        IL_0049:  ldloc.1
                        IL_004a:  newobj     instance void Test/test@1::.ctor(int32,
                                                                              int32)
                        IL_004f:  tail.
                        IL_0051:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::Initialize<int32>(int32,
                                                                                                                            class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,!!0>)
                        IL_0056:  ret
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
            let ``Lone RangeInt32 with const args when start > finish lowers to empty list`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions([|"--optimize+"|],
                    """
                    module Test

                    let test () = [10..1]
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
                      .method public static class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<int32> 
                              test() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  call       class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<!0> class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<int32>::get_Empty()
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
            let ``Lone RangeInt32 with const args lowers to call to init`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions([|"--optimize+"|],
                    """
                    module Test

                    let test () = [1..101]
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
                             extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,int32>
                      {
                        .field static assembly initonly class Test/test@1 @_instance
                        .method assembly specialname rtspecialname 
                                instance void  .ctor() cil managed
                        {
                          .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                          .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,int32>::.ctor()
                          IL_0006:  ret
                        } 
                    
                        .method public strict virtual instance int32 
                                Invoke(int32 i) cil managed
                        {
                          
                          .maxstack  8
                          IL_0000:  ldc.i4.1
                          IL_0001:  ldarg.1
                          IL_0002:  add
                          IL_0003:  ret
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
                    
                      .method public static class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<int32> 
                              test() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4.s   101
                        IL_0002:  ldsfld     class Test/test@1 Test/test@1::@_instance
                        IL_0007:  tail.
                        IL_0009:  call       class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<!!0> [FSharp.Core]Microsoft.FSharp.Collections.ListModule::Initialize<int32>(int32,
                                                                                                                                                                                       class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,!!0>)
                        IL_000e:  ret
                      } 
                    
                    } 
                    
                    .class private abstract auto ansi sealed '<StartupCode$assembly>'.$Test
                           extends [runtime]System.Object
                    {
                    }
                    """
                    ]))

            [<Test>]
            let ``Lone RangeInt32 with dynamic args lowers to call to init``() =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions([|"--optimize+"|],
                    """
                    module Test

                    let test start finish = [start..finish]
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
                             extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,int32>
                      {
                        .field public int32 start
                        .method assembly specialname rtspecialname 
                                instance void  .ctor(int32 start) cil managed
                        {
                          .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                          .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,int32>::.ctor()
                          IL_0006:  ldarg.0
                          IL_0007:  ldarg.1
                          IL_0008:  stfld      int32 Test/test@1::start
                          IL_000d:  ret
                        } 
                    
                        .method public strict virtual instance int32 
                                Invoke(int32 i) cil managed
                        {
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  ldfld      int32 Test/test@1::start
                          IL_0006:  ldarg.1
                          IL_0007:  add
                          IL_0008:  ret
                        } 
                    
                      } 
                    
                      .method public static class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<int32> 
                              test(int32 start,
                                   int32 finish) cil managed
                      {
                        .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationArgumentCountsAttribute::.ctor(int32[]) = ( 01 00 02 00 00 00 01 00 00 00 01 00 00 00 00 00 ) 
                        
                        .maxstack  8
                        IL_0000:  ldarg.1
                        IL_0001:  ldarg.0
                        IL_0002:  bge.s      IL_000a
                    
                        IL_0004:  call       class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<!0> class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<int32>::get_Empty()
                        IL_0009:  ret
                    
                        IL_000a:  ldarg.1
                        IL_000b:  ldarg.0
                        IL_000c:  sub
                        IL_000d:  ldc.i4.1
                        IL_000e:  add
                        IL_000f:  ldarg.0
                        IL_0010:  newobj     instance void Test/test@1::.ctor(int32)
                        IL_0015:  tail.
                        IL_0017:  call       class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<!!0> [FSharp.Core]Microsoft.FSharp.Collections.ListModule::Initialize<int32>(int32,
                                                                                                                                                                                       class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,!!0>)
                        IL_001c:  ret
                      } 
                    
                    } 
                    
                    .class private abstract auto ansi sealed '<StartupCode$assembly>'.$Test
                           extends [runtime]System.Object
                    {
                    }
                    """
                    ]))

            [<Test>]
            let ``Lone RangeInt32 with dynamic args that are complex exprs stores those in vars before calling init`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions([|"--optimize+"|],
                    """
                    module Test

                    let a () = (Array.zeroCreate 10).Length
                    let b () = (Array.zeroCreate 20).Length

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
                             extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,int32>
                      {
                        .field public int32 start
                        .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
                        .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                        .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                        .method assembly specialname rtspecialname 
                                instance void  .ctor(int32 start) cil managed
                        {
                          .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                          .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,int32>::.ctor()
                          IL_0006:  ldarg.0
                          IL_0007:  ldarg.1
                          IL_0008:  stfld      int32 Test/test@1::start
                          IL_000d:  ret
                        } 
                    
                        .method public strict virtual instance int32 
                                Invoke(int32 i) cil managed
                        {
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  ldfld      int32 Test/test@1::start
                          IL_0006:  ldarg.1
                          IL_0007:  add
                          IL_0008:  ret
                        } 
                    
                      } 
                    
                      .method public static int32  a() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4.s   10
                        IL_0002:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0007:  ldlen
                        IL_0008:  conv.i4
                        IL_0009:  ret
                      } 
                    
                      .method public static int32  b() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4.s   20
                        IL_0002:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0007:  ldlen
                        IL_0008:  conv.i4
                        IL_0009:  ret
                      } 
                    
                      .method public static class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<int32> 
                              test() cil managed
                      {
                        
                        .maxstack  4
                        .locals init (int32 V_0,
                                 int32 V_1)
                        IL_0000:  ldc.i4.s   10
                        IL_0002:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0007:  ldlen
                        IL_0008:  conv.i4
                        IL_0009:  stloc.0
                        IL_000a:  ldc.i4.s   20
                        IL_000c:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0011:  ldlen
                        IL_0012:  conv.i4
                        IL_0013:  stloc.1
                        IL_0014:  ldloc.1
                        IL_0015:  ldloc.0
                        IL_0016:  bge.s      IL_001e
                    
                        IL_0018:  call       class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<!0> class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<int32>::get_Empty()
                        IL_001d:  ret
                    
                        IL_001e:  ldloc.1
                        IL_001f:  ldloc.0
                        IL_0020:  sub
                        IL_0021:  ldc.i4.1
                        IL_0022:  add
                        IL_0023:  ldloc.0
                        IL_0024:  newobj     instance void Test/test@1::.ctor(int32)
                        IL_0029:  tail.
                        IL_002b:  call       class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<!!0> [FSharp.Core]Microsoft.FSharp.Collections.ListModule::Initialize<int32>(int32,
                                                                                                                                                                                       class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,!!0>)
                        IL_0030:  ret
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
            let ``Lone RangeInt32 with const args when step = 0 lowers to call to invalidArg`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions(["--optimize+"],
                    """
                    module Test

                    let test () = [1..0..5]
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
                      .method public static class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<int32> 
                              test() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldstr      "step"
                        IL_0005:  ldstr      "The step of a range cannot be zero."
                        IL_000a:  tail.
                        IL_000c:  call       !!0 [FSharp.Core]Microsoft.FSharp.Core.Operators::InvalidArg<class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<int32>>(string,
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
            let ``Lone RangeInt32 with const args when (finish - start) / step + 1 ≤ 0 lowers to empty array`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions(["--optimize+"],
                    """
                    module Test

                    let test () = [1..-1..5]
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
                      .method public static class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<int32> 
                              test() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  call       class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<!0> class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<int32>::get_Empty()
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
            let ``Lone RangeInt32 with const args lowers to call to init`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions(["--optimize+"],
                    """
                    module Test

                    let test () = [1..2..257]
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
                             extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,int32>
                      {
                        .field static assembly initonly class Test/test@1 @_instance
                        .method assembly specialname rtspecialname 
                                instance void  .ctor() cil managed
                        {
                          .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                          .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,int32>::.ctor()
                          IL_0006:  ret
                        } 
                    
                        .method public strict virtual instance int32 
                                Invoke(int32 i) cil managed
                        {
                          
                          .maxstack  8
                          IL_0000:  ldarg.1
                          IL_0001:  ldc.i4.2
                          IL_0002:  mul
                          IL_0003:  ldc.i4.1
                          IL_0004:  add
                          IL_0005:  ret
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
                    
                      .method public static class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<int32> 
                              test() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4     0x81
                        IL_0005:  ldsfld     class Test/test@1 Test/test@1::@_instance
                        IL_000a:  tail.
                        IL_000c:  call       class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<!!0> [FSharp.Core]Microsoft.FSharp.Collections.ListModule::Initialize<int32>(int32,
                                                                                                                                                                                       class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,!!0>)
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
            let ``Lone RangeInt32 with dynamic args lowers to call to init`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions(["--optimize+"],
                    """
                    module Test

                    let test start step finish = [start..step..finish]
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
                             extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,int32>
                      {
                        .field public int32 start
                        .field public int32 step
                        .method assembly specialname rtspecialname 
                                instance void  .ctor(int32 start,
                                                     int32 step) cil managed
                        {
                          .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                          .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,int32>::.ctor()
                          IL_0006:  ldarg.0
                          IL_0007:  ldarg.1
                          IL_0008:  stfld      int32 Test/test@1::start
                          IL_000d:  ldarg.0
                          IL_000e:  ldarg.2
                          IL_000f:  stfld      int32 Test/test@1::step
                          IL_0014:  ret
                        } 
                    
                        .method public strict virtual instance int32 
                                Invoke(int32 i) cil managed
                        {
                          
                          .maxstack  8
                          IL_0000:  ldarg.1
                          IL_0001:  ldarg.0
                          IL_0002:  ldfld      int32 Test/test@1::step
                          IL_0007:  mul
                          IL_0008:  ldarg.0
                          IL_0009:  ldfld      int32 Test/test@1::start
                          IL_000e:  add
                          IL_000f:  ret
                        } 
                    
                      } 
                    
                      .method public static class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<int32> 
                              test(int32 start,
                                   int32 step,
                                   int32 finish) cil managed
                      {
                        .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationArgumentCountsAttribute::.ctor(int32[]) = ( 01 00 03 00 00 00 01 00 00 00 01 00 00 00 01 00 
                                                                                                                                        00 00 00 00 ) 
                        
                        .maxstack  5
                        .locals init (int32 V_0)
                        IL_0000:  ldarg.1
                        IL_0001:  brtrue.s   IL_0015
                    
                        IL_0003:  ldstr      "step"
                        IL_0008:  ldstr      "The step of a range cannot be zero."
                        IL_000d:  tail.
                        IL_000f:  call       !!0 [FSharp.Core]Microsoft.FSharp.Core.Operators::InvalidArg<class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<int32>>(string,
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
                    
                        IL_0020:  call       class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<!0> class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<int32>::get_Empty()
                        IL_0025:  ret
                    
                        IL_0026:  ldloc.0
                        IL_0027:  ldarg.0
                        IL_0028:  ldarg.1
                        IL_0029:  newobj     instance void Test/test@1::.ctor(int32,
                                                                              int32)
                        IL_002e:  tail.
                        IL_0030:  call       class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<!!0> [FSharp.Core]Microsoft.FSharp.Collections.ListModule::Initialize<int32>(int32,
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
            let ``Lone RangeInt32 with dynamic args that are complex exprs stores those in vars before calling init`` () =
                CompilerAssert.CompileLibraryAndVerifyILWithOptions(["--optimize+"],
                    """
                    module Test

                    let a () = (Array.zeroCreate 10).Length
                    let b () = (Array.zeroCreate 20).Length
                    let c () = (Array.zeroCreate 300).Length

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
                             extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,int32>
                      {
                        .field public int32 start
                        .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
                        .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                        .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                        .field public int32 step
                        .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
                        .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                        .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                        .method assembly specialname rtspecialname 
                                instance void  .ctor(int32 start,
                                                     int32 step) cil managed
                        {
                          .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
                          .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
                          
                          .maxstack  8
                          IL_0000:  ldarg.0
                          IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,int32>::.ctor()
                          IL_0006:  ldarg.0
                          IL_0007:  ldarg.1
                          IL_0008:  stfld      int32 Test/test@1::start
                          IL_000d:  ldarg.0
                          IL_000e:  ldarg.2
                          IL_000f:  stfld      int32 Test/test@1::step
                          IL_0014:  ret
                        } 
                    
                        .method public strict virtual instance int32 
                                Invoke(int32 i) cil managed
                        {
                          
                          .maxstack  8
                          IL_0000:  ldarg.1
                          IL_0001:  ldarg.0
                          IL_0002:  ldfld      int32 Test/test@1::step
                          IL_0007:  mul
                          IL_0008:  ldarg.0
                          IL_0009:  ldfld      int32 Test/test@1::start
                          IL_000e:  add
                          IL_000f:  ret
                        } 
                    
                      } 
                    
                      .method public static int32  a() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4.s   10
                        IL_0002:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0007:  ldlen
                        IL_0008:  conv.i4
                        IL_0009:  ret
                      } 
                    
                      .method public static int32  b() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4.s   20
                        IL_0002:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0007:  ldlen
                        IL_0008:  conv.i4
                        IL_0009:  ret
                      } 
                    
                      .method public static int32  c() cil managed
                      {
                        
                        .maxstack  8
                        IL_0000:  ldc.i4     0x12c
                        IL_0005:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_000a:  ldlen
                        IL_000b:  conv.i4
                        IL_000c:  ret
                      } 
                    
                      .method public static class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<int32> 
                              test() cil managed
                      {
                        
                        .maxstack  5
                        .locals init (int32 V_0,
                                 int32 V_1,
                                 int32 V_2,
                                 int32 V_3)
                        IL_0000:  ldc.i4.s   10
                        IL_0002:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0007:  ldlen
                        IL_0008:  conv.i4
                        IL_0009:  stloc.0
                        IL_000a:  ldc.i4.s   20
                        IL_000c:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_0011:  ldlen
                        IL_0012:  conv.i4
                        IL_0013:  stloc.1
                        IL_0014:  ldc.i4     0x12c
                        IL_0019:  call       !!0[] [FSharp.Core]Microsoft.FSharp.Collections.ArrayModule::ZeroCreate<object>(int32)
                        IL_001e:  ldlen
                        IL_001f:  conv.i4
                        IL_0020:  stloc.2
                        IL_0021:  ldloc.1
                        IL_0022:  brtrue.s   IL_0036
                    
                        IL_0024:  ldstr      "step"
                        IL_0029:  ldstr      "The step of a range cannot be zero."
                        IL_002e:  tail.
                        IL_0030:  call       !!0 [FSharp.Core]Microsoft.FSharp.Core.Operators::InvalidArg<class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<int32>>(string,
                                                                                                                                                                               string)
                        IL_0035:  ret
                    
                        IL_0036:  ldloc.2
                        IL_0037:  ldloc.0
                        IL_0038:  sub
                        IL_0039:  ldloc.1
                        IL_003a:  div
                        IL_003b:  ldloc.1
                        IL_003c:  add
                        IL_003d:  stloc.3
                        IL_003e:  ldloc.3
                        IL_003f:  brtrue.s   IL_0047
                    
                        IL_0041:  call       class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<!0> class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<int32>::get_Empty()
                        IL_0046:  ret
                    
                        IL_0047:  ldloc.3
                        IL_0048:  ldloc.0
                        IL_0049:  ldloc.1
                        IL_004a:  newobj     instance void Test/test@1::.ctor(int32,
                                                                              int32)
                        IL_004f:  tail.
                        IL_0051:  call       class [FSharp.Core]Microsoft.FSharp.Collections.FSharpList`1<!!0> [FSharp.Core]Microsoft.FSharp.Collections.ListModule::Initialize<int32>(int32,
                                                                                                                                                                                       class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<int32,!!0>)
                        IL_0056:  ret
                      } 
                    
                    } 
                    
                    .class private abstract auto ansi sealed '<StartupCode$assembly>'.$Test
                           extends [runtime]System.Object
                    {
                    }
                    """
                    ]))
