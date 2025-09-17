




.assembly extern runtime { }
.assembly extern FSharp.Core { }
.assembly extern netstandard
{
  .publickeytoken = (CC 7B 13 FF CD 2D DD 51 )                         
  .ver 2:1:0:0
}
.assembly assembly
{
  .custom instance void [FSharp.Core]Microsoft.FSharp.Core.FSharpInterfaceDataVersionAttribute::.ctor(int32,
                                                                                                      int32,
                                                                                                      int32) = ( 01 00 02 00 00 00 00 00 00 00 00 00 00 00 00 00 ) 

  
  

  .hash algorithm 0x00008004
  .ver 0:0:0:0
}
.module assembly.exe

.imagebase {value}
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0003       
.corflags 0x00000001    





.class public abstract auto ansi sealed assembly
       extends [runtime]System.Object
{
  .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 07 00 00 00 00 00 ) 
  .class auto ansi serializable sealed nested public R1
         extends [runtime]System.Object
         implements class [runtime]System.IEquatable`1<class assembly/R1>,
                    [runtime]System.Collections.IStructuralEquatable,
                    class [runtime]System.IComparable`1<class assembly/R1>,
                    [runtime]System.IComparable,
                    [runtime]System.Collections.IStructuralComparable
  {
    .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 02 00 00 00 00 00 ) 
    .field assembly int32 A@
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
    .field assembly string B@
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
    .method public hidebysig specialname instance int32  get_A() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  ldfld      int32 assembly/R1::A@
      IL_0006:  ret
    } 

    .method public hidebysig specialname instance string  get_B() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  ldfld      string assembly/R1::B@
      IL_0006:  ret
    } 

    .method public specialname rtspecialname instance void  .ctor(int32 a, string b) cil managed
    {
      .custom instance void [runtime]System.Diagnostics.CodeAnalysis.DynamicDependencyAttribute::.ctor(valuetype [runtime]System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes,
                                                                                                              class [runtime]System.Type) = ( 01 00 60 06 00 00 29 53 70 72 65 61 64 4E 6F 6D   
                                                                                                                                                     69 6E 61 6C 52 65 63 6F 72 64 49 6E 74 6F 41 6E   
                                                                                                                                                     6F 6E 79 6D 6F 75 73 52 65 63 6F 72 64 2B 52 31   
                                                                                                                                                     00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  call       instance void [runtime]System.Object::.ctor()
      IL_0006:  ldarg.0
      IL_0007:  ldarg.1
      IL_0008:  stfld      int32 assembly/R1::A@
      IL_000d:  ldarg.0
      IL_000e:  ldarg.2
      IL_000f:  stfld      string assembly/R1::B@
      IL_0014:  ret
    } 

    .method public strict virtual instance string ToString() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldstr      "%+A"
      IL_0005:  newobj     instance void class [FSharp.Core]Microsoft.FSharp.Core.PrintfFormat`5<class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class assembly/R1,string>,class [FSharp.Core]Microsoft.FSharp.Core.Unit,string,string,class assembly/R1>::.ctor(string)
      IL_000a:  call       !!0 [FSharp.Core]Microsoft.FSharp.Core.ExtraTopLevelOperators::PrintFormatToString<class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class assembly/R1,string>>(class [FSharp.Core]Microsoft.FSharp.Core.PrintfFormat`4<!!0,class [FSharp.Core]Microsoft.FSharp.Core.Unit,string,string>)
      IL_000f:  ldarg.0
      IL_0010:  callvirt   instance !1 class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class assembly/R1,string>::Invoke(!0)
      IL_0015:  ret
    } 

    .method public hidebysig virtual final instance int32  CompareTo(class assembly/R1 obj) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  5
      .locals init (int32 V_0,
               class [runtime]System.Collections.IComparer V_1,
               int32 V_2,
               int32 V_3)
      IL_0000:  ldarg.0
      IL_0001:  brfalse.s  IL_004a

      IL_0003:  ldarg.1
      IL_0004:  brfalse.s  IL_0048

      IL_0006:  call       class [runtime]System.Collections.IComparer [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::get_GenericComparer()
      IL_000b:  stloc.1
      IL_000c:  ldarg.0
      IL_000d:  ldfld      int32 assembly/R1::A@
      IL_0012:  stloc.2
      IL_0013:  ldarg.1
      IL_0014:  ldfld      int32 assembly/R1::A@
      IL_0019:  stloc.3
      IL_001a:  ldloc.2
      IL_001b:  ldloc.3
      IL_001c:  cgt
      IL_001e:  ldloc.2
      IL_001f:  ldloc.3
      IL_0020:  clt
      IL_0022:  sub
      IL_0023:  stloc.0
      IL_0024:  ldloc.0
      IL_0025:  ldc.i4.0
      IL_0026:  bge.s      IL_002a

      IL_0028:  ldloc.0
      IL_0029:  ret

      IL_002a:  ldloc.0
      IL_002b:  ldc.i4.0
      IL_002c:  ble.s      IL_0030

      IL_002e:  ldloc.0
      IL_002f:  ret

      IL_0030:  call       class [runtime]System.Collections.IComparer [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::get_GenericComparer()
      IL_0035:  stloc.1
      IL_0036:  ldarg.0
      IL_0037:  ldfld      string assembly/R1::B@
      IL_003c:  ldarg.1
      IL_003d:  ldfld      string assembly/R1::B@
      IL_0042:  call       int32 [netstandard]System.String::CompareOrdinal(string,
                                                                            string)
      IL_0047:  ret

      IL_0048:  ldc.i4.1
      IL_0049:  ret

      IL_004a:  ldarg.1
      IL_004b:  brfalse.s  IL_004f

      IL_004d:  ldc.i4.m1
      IL_004e:  ret

      IL_004f:  ldc.i4.0
      IL_0050:  ret
    } 

    .method public hidebysig virtual final instance int32  CompareTo(object obj) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  ldarg.1
      IL_0002:  unbox.any  assembly/R1
      IL_0007:  callvirt   instance int32 assembly/R1::CompareTo(class assembly/R1)
      IL_000c:  ret
    } 

    .method public hidebysig virtual final instance int32  CompareTo(object obj, class [runtime]System.Collections.IComparer comp) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  5
      .locals init (class assembly/R1 V_0,
               int32 V_1,
               int32 V_2,
               int32 V_3)
      IL_0000:  ldarg.1
      IL_0001:  unbox.any  assembly/R1
      IL_0006:  stloc.0
      IL_0007:  ldarg.0
      IL_0008:  brfalse.s  IL_004a

      IL_000a:  ldarg.1
      IL_000b:  unbox.any  assembly/R1
      IL_0010:  brfalse.s  IL_0048

      IL_0012:  ldarg.0
      IL_0013:  ldfld      int32 assembly/R1::A@
      IL_0018:  stloc.2
      IL_0019:  ldloc.0
      IL_001a:  ldfld      int32 assembly/R1::A@
      IL_001f:  stloc.3
      IL_0020:  ldloc.2
      IL_0021:  ldloc.3
      IL_0022:  cgt
      IL_0024:  ldloc.2
      IL_0025:  ldloc.3
      IL_0026:  clt
      IL_0028:  sub
      IL_0029:  stloc.1
      IL_002a:  ldloc.1
      IL_002b:  ldc.i4.0
      IL_002c:  bge.s      IL_0030

      IL_002e:  ldloc.1
      IL_002f:  ret

      IL_0030:  ldloc.1
      IL_0031:  ldc.i4.0
      IL_0032:  ble.s      IL_0036

      IL_0034:  ldloc.1
      IL_0035:  ret

      IL_0036:  ldarg.0
      IL_0037:  ldfld      string assembly/R1::B@
      IL_003c:  ldloc.0
      IL_003d:  ldfld      string assembly/R1::B@
      IL_0042:  call       int32 [netstandard]System.String::CompareOrdinal(string,
                                                                            string)
      IL_0047:  ret

      IL_0048:  ldc.i4.1
      IL_0049:  ret

      IL_004a:  ldarg.1
      IL_004b:  unbox.any  assembly/R1
      IL_0050:  brfalse.s  IL_0054

      IL_0052:  ldc.i4.m1
      IL_0053:  ret

      IL_0054:  ldc.i4.0
      IL_0055:  ret
    } 

    .method public hidebysig virtual final instance int32  GetHashCode(class [runtime]System.Collections.IEqualityComparer comp) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  7
      .locals init (int32 V_0,
               string V_1)
      IL_0000:  ldarg.0
      IL_0001:  brfalse.s  IL_0040

      IL_0003:  ldc.i4.0
      IL_0004:  stloc.0
      IL_0005:  ldc.i4     0x9e3779b9
      IL_000a:  ldarg.0
      IL_000b:  ldfld      string assembly/R1::B@
      IL_0010:  stloc.1
      IL_0011:  ldloc.1
      IL_0012:  brtrue.s   IL_0018

      IL_0014:  ldc.i4.0
      IL_0015:  nop
      IL_0016:  br.s       IL_001f

      IL_0018:  ldloc.1
      IL_0019:  callvirt   instance int32 [netstandard]System.Object::GetHashCode()
      IL_001e:  nop
      IL_001f:  ldloc.0
      IL_0020:  ldc.i4.6
      IL_0021:  shl
      IL_0022:  ldloc.0
      IL_0023:  ldc.i4.2
      IL_0024:  shr
      IL_0025:  add
      IL_0026:  add
      IL_0027:  add
      IL_0028:  stloc.0
      IL_0029:  ldc.i4     0x9e3779b9
      IL_002e:  ldarg.0
      IL_002f:  ldfld      int32 assembly/R1::A@
      IL_0034:  ldloc.0
      IL_0035:  ldc.i4.6
      IL_0036:  shl
      IL_0037:  ldloc.0
      IL_0038:  ldc.i4.2
      IL_0039:  shr
      IL_003a:  add
      IL_003b:  add
      IL_003c:  add
      IL_003d:  stloc.0
      IL_003e:  ldloc.0
      IL_003f:  ret

      IL_0040:  ldc.i4.0
      IL_0041:  ret
    } 

    .method public hidebysig virtual final instance int32  GetHashCode() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  call       class [runtime]System.Collections.IEqualityComparer [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::get_GenericEqualityComparer()
      IL_0006:  callvirt   instance int32 assembly/R1::GetHashCode(class [runtime]System.Collections.IEqualityComparer)
      IL_000b:  ret
    } 

    .method public hidebysig instance bool Equals(class assembly/R1 obj, class [runtime]System.Collections.IEqualityComparer comp) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  brfalse.s  IL_002a

      IL_0003:  ldarg.1
      IL_0004:  brfalse.s  IL_0028

      IL_0006:  ldarg.0
      IL_0007:  ldfld      int32 assembly/R1::A@
      IL_000c:  ldarg.1
      IL_000d:  ldfld      int32 assembly/R1::A@
      IL_0012:  bne.un.s   IL_0026

      IL_0014:  ldarg.0
      IL_0015:  ldfld      string assembly/R1::B@
      IL_001a:  ldarg.1
      IL_001b:  ldfld      string assembly/R1::B@
      IL_0020:  call       bool [netstandard]System.String::Equals(string,
                                                                   string)
      IL_0025:  ret

      IL_0026:  ldc.i4.0
      IL_0027:  ret

      IL_0028:  ldc.i4.0
      IL_0029:  ret

      IL_002a:  ldarg.1
      IL_002b:  ldnull
      IL_002c:  cgt.un
      IL_002e:  ldc.i4.0
      IL_002f:  ceq
      IL_0031:  ret
    } 

    .method public hidebysig virtual final instance bool  Equals(object obj, class [runtime]System.Collections.IEqualityComparer comp) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  5
      .locals init (class assembly/R1 V_0)
      IL_0000:  ldarg.1
      IL_0001:  isinst     assembly/R1
      IL_0006:  stloc.0
      IL_0007:  ldloc.0
      IL_0008:  brfalse.s  IL_0013

      IL_000a:  ldarg.0
      IL_000b:  ldloc.0
      IL_000c:  ldarg.2
      IL_000d:  callvirt   instance bool assembly/R1::Equals(class assembly/R1,
                                                                                           class [runtime]System.Collections.IEqualityComparer)
      IL_0012:  ret

      IL_0013:  ldc.i4.0
      IL_0014:  ret
    } 

    .method public hidebysig specialname instance int32  get_Lol() cil managed
    {
      
      .maxstack  8
      IL_0000:  ldc.i4.s   99
      IL_0002:  ret
    } 

    .method public hidebysig virtual final instance bool  Equals(class assembly/R1 obj) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  brfalse.s  IL_002a

      IL_0003:  ldarg.1
      IL_0004:  brfalse.s  IL_0028

      IL_0006:  ldarg.0
      IL_0007:  ldfld      int32 assembly/R1::A@
      IL_000c:  ldarg.1
      IL_000d:  ldfld      int32 assembly/R1::A@
      IL_0012:  bne.un.s   IL_0026

      IL_0014:  ldarg.0
      IL_0015:  ldfld      string assembly/R1::B@
      IL_001a:  ldarg.1
      IL_001b:  ldfld      string assembly/R1::B@
      IL_0020:  call       bool [netstandard]System.String::Equals(string,
                                                                   string)
      IL_0025:  ret

      IL_0026:  ldc.i4.0
      IL_0027:  ret

      IL_0028:  ldc.i4.0
      IL_0029:  ret

      IL_002a:  ldarg.1
      IL_002b:  ldnull
      IL_002c:  cgt.un
      IL_002e:  ldc.i4.0
      IL_002f:  ceq
      IL_0031:  ret
    } 

    .method public hidebysig virtual final instance bool  Equals(object obj) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  4
      .locals init (class assembly/R1 V_0)
      IL_0000:  ldarg.1
      IL_0001:  isinst     assembly/R1
      IL_0006:  stloc.0
      IL_0007:  ldloc.0
      IL_0008:  brfalse.s  IL_0012

      IL_000a:  ldarg.0
      IL_000b:  ldloc.0
      IL_000c:  callvirt   instance bool assembly/R1::Equals(class assembly/R1)
      IL_0011:  ret

      IL_0012:  ldc.i4.0
      IL_0013:  ret
    } 

    .property instance int32 A()
    {
      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags,
                                                                                                  int32) = ( 01 00 04 00 00 00 00 00 00 00 00 00 ) 
      .get instance int32 assembly/R1::get_A()
    } 
    .property instance string B()
    {
      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags,
                                                                                                  int32) = ( 01 00 04 00 00 00 01 00 00 00 00 00 ) 
      .get instance string assembly/R1::get_B()
    } 
    .property instance int32 Lol()
    {
      .get instance int32 assembly/R1::get_Lol()
    } 
  } 

  .class auto ansi serializable sealed nested public R2
         extends [runtime]System.Object
         implements class [runtime]System.IEquatable`1<class assembly/R2>,
                    [runtime]System.Collections.IStructuralEquatable,
                    class [runtime]System.IComparable`1<class assembly/R2>,
                    [runtime]System.IComparable,
                    [runtime]System.Collections.IStructuralComparable
  {
    .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 02 00 00 00 00 00 ) 
    .field assembly int32 A@
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
    .field assembly string B@
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
    .field assembly string C@
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
    .method public hidebysig specialname instance int32  get_A() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  ldfld      int32 assembly/R2::A@
      IL_0006:  ret
    } 

    .method public hidebysig specialname instance string  get_B() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  ldfld      string assembly/R2::B@
      IL_0006:  ret
    } 

    .method public hidebysig specialname instance string  get_C() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  ldfld      string assembly/R2::C@
      IL_0006:  ret
    } 

    .method public specialname rtspecialname 
            instance void  .ctor(int32 a,
                                 string b,
                                 string c) cil managed
    {
      .custom instance void [runtime]System.Diagnostics.CodeAnalysis.DynamicDependencyAttribute::.ctor(valuetype [runtime]System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes,
                                                                                                              class [runtime]System.Type) = ( 01 00 60 06 00 00 29 53 70 72 65 61 64 4E 6F 6D   
                                                                                                                                                     69 6E 61 6C 52 65 63 6F 72 64 49 6E 74 6F 41 6E   
                                                                                                                                                     6F 6E 79 6D 6F 75 73 52 65 63 6F 72 64 2B 52 32   
                                                                                                                                                     00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  call       instance void [runtime]System.Object::.ctor()
      IL_0006:  ldarg.0
      IL_0007:  ldarg.1
      IL_0008:  stfld      int32 assembly/R2::A@
      IL_000d:  ldarg.0
      IL_000e:  ldarg.2
      IL_000f:  stfld      string assembly/R2::B@
      IL_0014:  ldarg.0
      IL_0015:  ldarg.3
      IL_0016:  stfld      string assembly/R2::C@
      IL_001b:  ret
    } 

    .method public strict virtual instance string ToString() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldstr      "%+A"
      IL_0005:  newobj     instance void class [FSharp.Core]Microsoft.FSharp.Core.PrintfFormat`5<class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class assembly/R2,string>,class [FSharp.Core]Microsoft.FSharp.Core.Unit,string,string,class assembly/R2>::.ctor(string)
      IL_000a:  call       !!0 [FSharp.Core]Microsoft.FSharp.Core.ExtraTopLevelOperators::PrintFormatToString<class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class assembly/R2,string>>(class [FSharp.Core]Microsoft.FSharp.Core.PrintfFormat`4<!!0,class [FSharp.Core]Microsoft.FSharp.Core.Unit,string,string>)
      IL_000f:  ldarg.0
      IL_0010:  callvirt   instance !1 class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class assembly/R2,string>::Invoke(!0)
      IL_0015:  ret
    } 

    .method public hidebysig virtual final instance int32  CompareTo(class assembly/R2 obj) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  5
      .locals init (int32 V_0,
               class [runtime]System.Collections.IComparer V_1,
               int32 V_2,
               int32 V_3)
      IL_0000:  ldarg.0
      IL_0001:  brfalse.s  IL_006e

      IL_0003:  ldarg.1
      IL_0004:  brfalse.s  IL_006c

      IL_0006:  call       class [runtime]System.Collections.IComparer [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::get_GenericComparer()
      IL_000b:  stloc.1
      IL_000c:  ldarg.0
      IL_000d:  ldfld      int32 assembly/R2::A@
      IL_0012:  stloc.2
      IL_0013:  ldarg.1
      IL_0014:  ldfld      int32 assembly/R2::A@
      IL_0019:  stloc.3
      IL_001a:  ldloc.2
      IL_001b:  ldloc.3
      IL_001c:  cgt
      IL_001e:  ldloc.2
      IL_001f:  ldloc.3
      IL_0020:  clt
      IL_0022:  sub
      IL_0023:  stloc.0
      IL_0024:  ldloc.0
      IL_0025:  ldc.i4.0
      IL_0026:  bge.s      IL_002a

      IL_0028:  ldloc.0
      IL_0029:  ret

      IL_002a:  ldloc.0
      IL_002b:  ldc.i4.0
      IL_002c:  ble.s      IL_0030

      IL_002e:  ldloc.0
      IL_002f:  ret

      IL_0030:  call       class [runtime]System.Collections.IComparer [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::get_GenericComparer()
      IL_0035:  stloc.1
      IL_0036:  ldarg.0
      IL_0037:  ldfld      string assembly/R2::B@
      IL_003c:  ldarg.1
      IL_003d:  ldfld      string assembly/R2::B@
      IL_0042:  call       int32 [netstandard]System.String::CompareOrdinal(string,
                                                                            string)
      IL_0047:  stloc.2
      IL_0048:  ldloc.2
      IL_0049:  ldc.i4.0
      IL_004a:  bge.s      IL_004e

      IL_004c:  ldloc.2
      IL_004d:  ret

      IL_004e:  ldloc.2
      IL_004f:  ldc.i4.0
      IL_0050:  ble.s      IL_0054

      IL_0052:  ldloc.2
      IL_0053:  ret

      IL_0054:  call       class [runtime]System.Collections.IComparer [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::get_GenericComparer()
      IL_0059:  stloc.1
      IL_005a:  ldarg.0
      IL_005b:  ldfld      string assembly/R2::C@
      IL_0060:  ldarg.1
      IL_0061:  ldfld      string assembly/R2::C@
      IL_0066:  call       int32 [netstandard]System.String::CompareOrdinal(string,
                                                                            string)
      IL_006b:  ret

      IL_006c:  ldc.i4.1
      IL_006d:  ret

      IL_006e:  ldarg.1
      IL_006f:  brfalse.s  IL_0073

      IL_0071:  ldc.i4.m1
      IL_0072:  ret

      IL_0073:  ldc.i4.0
      IL_0074:  ret
    } 

    .method public hidebysig virtual final instance int32  CompareTo(object obj) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  ldarg.1
      IL_0002:  unbox.any  assembly/R2
      IL_0007:  callvirt   instance int32 assembly/R2::CompareTo(class assembly/R2)
      IL_000c:  ret
    } 

    .method public hidebysig virtual final instance int32  CompareTo(object obj, class [runtime]System.Collections.IComparer comp) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  5
      .locals init (class assembly/R2 V_0,
               int32 V_1,
               int32 V_2,
               int32 V_3)
      IL_0000:  ldarg.1
      IL_0001:  unbox.any  assembly/R2
      IL_0006:  stloc.0
      IL_0007:  ldarg.0
      IL_0008:  brfalse.s  IL_0068

      IL_000a:  ldarg.1
      IL_000b:  unbox.any  assembly/R2
      IL_0010:  brfalse.s  IL_0066

      IL_0012:  ldarg.0
      IL_0013:  ldfld      int32 assembly/R2::A@
      IL_0018:  stloc.2
      IL_0019:  ldloc.0
      IL_001a:  ldfld      int32 assembly/R2::A@
      IL_001f:  stloc.3
      IL_0020:  ldloc.2
      IL_0021:  ldloc.3
      IL_0022:  cgt
      IL_0024:  ldloc.2
      IL_0025:  ldloc.3
      IL_0026:  clt
      IL_0028:  sub
      IL_0029:  stloc.1
      IL_002a:  ldloc.1
      IL_002b:  ldc.i4.0
      IL_002c:  bge.s      IL_0030

      IL_002e:  ldloc.1
      IL_002f:  ret

      IL_0030:  ldloc.1
      IL_0031:  ldc.i4.0
      IL_0032:  ble.s      IL_0036

      IL_0034:  ldloc.1
      IL_0035:  ret

      IL_0036:  ldarg.0
      IL_0037:  ldfld      string assembly/R2::B@
      IL_003c:  ldloc.0
      IL_003d:  ldfld      string assembly/R2::B@
      IL_0042:  call       int32 [netstandard]System.String::CompareOrdinal(string,
                                                                            string)
      IL_0047:  stloc.2
      IL_0048:  ldloc.2
      IL_0049:  ldc.i4.0
      IL_004a:  bge.s      IL_004e

      IL_004c:  ldloc.2
      IL_004d:  ret

      IL_004e:  ldloc.2
      IL_004f:  ldc.i4.0
      IL_0050:  ble.s      IL_0054

      IL_0052:  ldloc.2
      IL_0053:  ret

      IL_0054:  ldarg.0
      IL_0055:  ldfld      string assembly/R2::C@
      IL_005a:  ldloc.0
      IL_005b:  ldfld      string assembly/R2::C@
      IL_0060:  call       int32 [netstandard]System.String::CompareOrdinal(string,
                                                                            string)
      IL_0065:  ret

      IL_0066:  ldc.i4.1
      IL_0067:  ret

      IL_0068:  ldarg.1
      IL_0069:  unbox.any  assembly/R2
      IL_006e:  brfalse.s  IL_0072

      IL_0070:  ldc.i4.m1
      IL_0071:  ret

      IL_0072:  ldc.i4.0
      IL_0073:  ret
    } 

    .method public hidebysig virtual final instance int32  GetHashCode(class [runtime]System.Collections.IEqualityComparer comp) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  7
      .locals init (int32 V_0,
               string V_1)
      IL_0000:  ldarg.0
      IL_0001:  brfalse.s  IL_0064

      IL_0003:  ldc.i4.0
      IL_0004:  stloc.0
      IL_0005:  ldc.i4     0x9e3779b9
      IL_000a:  ldarg.0
      IL_000b:  ldfld      string assembly/R2::C@
      IL_0010:  stloc.1
      IL_0011:  ldloc.1
      IL_0012:  brtrue.s   IL_0018

      IL_0014:  ldc.i4.0
      IL_0015:  nop
      IL_0016:  br.s       IL_001f

      IL_0018:  ldloc.1
      IL_0019:  callvirt   instance int32 [netstandard]System.Object::GetHashCode()
      IL_001e:  nop
      IL_001f:  ldloc.0
      IL_0020:  ldc.i4.6
      IL_0021:  shl
      IL_0022:  ldloc.0
      IL_0023:  ldc.i4.2
      IL_0024:  shr
      IL_0025:  add
      IL_0026:  add
      IL_0027:  add
      IL_0028:  stloc.0
      IL_0029:  ldc.i4     0x9e3779b9
      IL_002e:  ldarg.0
      IL_002f:  ldfld      string assembly/R2::B@
      IL_0034:  stloc.1
      IL_0035:  ldloc.1
      IL_0036:  brtrue.s   IL_003c

      IL_0038:  ldc.i4.0
      IL_0039:  nop
      IL_003a:  br.s       IL_0043

      IL_003c:  ldloc.1
      IL_003d:  callvirt   instance int32 [netstandard]System.Object::GetHashCode()
      IL_0042:  nop
      IL_0043:  ldloc.0
      IL_0044:  ldc.i4.6
      IL_0045:  shl
      IL_0046:  ldloc.0
      IL_0047:  ldc.i4.2
      IL_0048:  shr
      IL_0049:  add
      IL_004a:  add
      IL_004b:  add
      IL_004c:  stloc.0
      IL_004d:  ldc.i4     0x9e3779b9
      IL_0052:  ldarg.0
      IL_0053:  ldfld      int32 assembly/R2::A@
      IL_0058:  ldloc.0
      IL_0059:  ldc.i4.6
      IL_005a:  shl
      IL_005b:  ldloc.0
      IL_005c:  ldc.i4.2
      IL_005d:  shr
      IL_005e:  add
      IL_005f:  add
      IL_0060:  add
      IL_0061:  stloc.0
      IL_0062:  ldloc.0
      IL_0063:  ret

      IL_0064:  ldc.i4.0
      IL_0065:  ret
    } 

    .method public hidebysig virtual final instance int32  GetHashCode() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  call       class [runtime]System.Collections.IEqualityComparer [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::get_GenericEqualityComparer()
      IL_0006:  callvirt   instance int32 assembly/R2::GetHashCode(class [runtime]System.Collections.IEqualityComparer)
      IL_000b:  ret
    } 

    .method public hidebysig instance bool Equals(class assembly/R2 obj, class [runtime]System.Collections.IEqualityComparer comp) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  4
      IL_0000:  ldarg.0
      IL_0001:  brfalse.s  IL_003f

      IL_0003:  ldarg.1
      IL_0004:  brfalse.s  IL_003d

      IL_0006:  ldarg.0
      IL_0007:  ldfld      int32 assembly/R2::A@
      IL_000c:  ldarg.1
      IL_000d:  ldfld      int32 assembly/R2::A@
      IL_0012:  bne.un.s   IL_003b

      IL_0014:  ldarg.0
      IL_0015:  ldfld      string assembly/R2::B@
      IL_001a:  ldarg.1
      IL_001b:  ldfld      string assembly/R2::B@
      IL_0020:  call       bool [netstandard]System.String::Equals(string,
                                                                   string)
      IL_0025:  brfalse.s  IL_0039

      IL_0027:  ldarg.0
      IL_0028:  ldfld      string assembly/R2::C@
      IL_002d:  ldarg.1
      IL_002e:  ldfld      string assembly/R2::C@
      IL_0033:  call       bool [netstandard]System.String::Equals(string,
                                                                   string)
      IL_0038:  ret

      IL_0039:  ldc.i4.0
      IL_003a:  ret

      IL_003b:  ldc.i4.0
      IL_003c:  ret

      IL_003d:  ldc.i4.0
      IL_003e:  ret

      IL_003f:  ldarg.1
      IL_0040:  ldnull
      IL_0041:  cgt.un
      IL_0043:  ldc.i4.0
      IL_0044:  ceq
      IL_0046:  ret
    } 

    .method public hidebysig virtual final instance bool  Equals(object obj, class [runtime]System.Collections.IEqualityComparer comp) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  5
      .locals init (class assembly/R2 V_0)
      IL_0000:  ldarg.1
      IL_0001:  isinst     assembly/R2
      IL_0006:  stloc.0
      IL_0007:  ldloc.0
      IL_0008:  brfalse.s  IL_0013

      IL_000a:  ldarg.0
      IL_000b:  ldloc.0
      IL_000c:  ldarg.2
      IL_000d:  callvirt   instance bool assembly/R2::Equals(class assembly/R2,
                                                                                           class [runtime]System.Collections.IEqualityComparer)
      IL_0012:  ret

      IL_0013:  ldc.i4.0
      IL_0014:  ret
    } 

    .method public hidebysig virtual final instance bool  Equals(class assembly/R2 obj) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  4
      IL_0000:  ldarg.0
      IL_0001:  brfalse.s  IL_003f

      IL_0003:  ldarg.1
      IL_0004:  brfalse.s  IL_003d

      IL_0006:  ldarg.0
      IL_0007:  ldfld      int32 assembly/R2::A@
      IL_000c:  ldarg.1
      IL_000d:  ldfld      int32 assembly/R2::A@
      IL_0012:  bne.un.s   IL_003b

      IL_0014:  ldarg.0
      IL_0015:  ldfld      string assembly/R2::B@
      IL_001a:  ldarg.1
      IL_001b:  ldfld      string assembly/R2::B@
      IL_0020:  call       bool [netstandard]System.String::Equals(string,
                                                                   string)
      IL_0025:  brfalse.s  IL_0039

      IL_0027:  ldarg.0
      IL_0028:  ldfld      string assembly/R2::C@
      IL_002d:  ldarg.1
      IL_002e:  ldfld      string assembly/R2::C@
      IL_0033:  call       bool [netstandard]System.String::Equals(string,
                                                                   string)
      IL_0038:  ret

      IL_0039:  ldc.i4.0
      IL_003a:  ret

      IL_003b:  ldc.i4.0
      IL_003c:  ret

      IL_003d:  ldc.i4.0
      IL_003e:  ret

      IL_003f:  ldarg.1
      IL_0040:  ldnull
      IL_0041:  cgt.un
      IL_0043:  ldc.i4.0
      IL_0044:  ceq
      IL_0046:  ret
    } 

    .method public hidebysig virtual final instance bool  Equals(object obj) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  4
      .locals init (class assembly/R2 V_0)
      IL_0000:  ldarg.1
      IL_0001:  isinst     assembly/R2
      IL_0006:  stloc.0
      IL_0007:  ldloc.0
      IL_0008:  brfalse.s  IL_0012

      IL_000a:  ldarg.0
      IL_000b:  ldloc.0
      IL_000c:  callvirt   instance bool assembly/R2::Equals(class assembly/R2)
      IL_0011:  ret

      IL_0012:  ldc.i4.0
      IL_0013:  ret
    } 

    .property instance int32 A()
    {
      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags,
                                                                                                  int32) = ( 01 00 04 00 00 00 00 00 00 00 00 00 ) 
      .get instance int32 assembly/R2::get_A()
    } 
    .property instance string B()
    {
      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags,
                                                                                                  int32) = ( 01 00 04 00 00 00 01 00 00 00 00 00 ) 
      .get instance string assembly/R2::get_B()
    } 
    .property instance string C()
    {
      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags,
                                                                                                  int32) = ( 01 00 04 00 00 00 02 00 00 00 00 00 ) 
      .get instance string assembly/R2::get_C()
    } 
  } 

  .field static assembly class assembly/R1 r1@8
  .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
  .field static assembly class assembly/R2 r2@9
  .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
  .field static assembly class '<>f__AnonymousType3073944080`3'<int32,string,string> r1AndR2@11
  .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
  .method public specialname static class assembly/R1 get_r1() cil managed
  {
    
    .maxstack  8
    IL_0000:  ldsfld     class assembly/R1 assembly::r1@8
    IL_0005:  ret
  } 

  .method public specialname static class assembly/R2 get_r2() cil managed
  {
    
    .maxstack  8
    IL_0000:  ldsfld     class assembly/R2 assembly::r2@9
    IL_0005:  ret
  } 

  .method public specialname static class '<>f__AnonymousType3073944080`3'<int32,string,string> get_r1AndR2() cil managed
  {
    
    .maxstack  8
    IL_0000:  ldsfld     class '<>f__AnonymousType3073944080`3'<int32,string,string> assembly::r1AndR2@11
    IL_0005:  ret
  } 

  .method private specialname rtspecialname static void  .cctor() cil managed
  {
    
    .maxstack  8
    IL_0000:  ldc.i4.0
    IL_0001:  stsfld     int32 '<StartupCode$assembly>'.$assembly::init@
    IL_0006:  ldsfld     int32 '<StartupCode$assembly>'.$assembly::init@
    IL_000b:  pop
    IL_000c:  ret
  } 

  .method assembly specialname static void staticInitialization@() cil managed
  {
    
    .maxstack  5
    IL_0000:  ldc.i4.3
    IL_0001:  ldstr      "4"
    IL_0006:  newobj     instance void assembly/R1::.ctor(int32,
                                                                                        string)
    IL_000b:  stsfld     class assembly/R1 assembly::r1@8
    IL_0010:  ldc.i4.5
    IL_0011:  ldstr      "6"
    IL_0016:  ldstr      "haha"
    IL_001b:  newobj     instance void assembly/R2::.ctor(int32,
                                                                                        string,
                                                                                        string)
    IL_0020:  stsfld     class assembly/R2 assembly::r2@9
    IL_0025:  call       class assembly/R2 assembly::get_r2()
    IL_002a:  ldfld      int32 assembly/R2::A@
    IL_002f:  call       class assembly/R2 assembly::get_r2()
    IL_0034:  ldfld      string assembly/R2::B@
    IL_0039:  call       class assembly/R2 assembly::get_r2()
    IL_003e:  ldfld      string assembly/R2::C@
    IL_0043:  newobj     instance void class '<>f__AnonymousType3073944080`3'<int32,string,string>::.ctor(!0,
                                                                                                          !1,
                                                                                                          !2)
    IL_0048:  stsfld     class '<>f__AnonymousType3073944080`3'<int32,string,string> assembly::r1AndR2@11
    IL_004d:  ret
  } 

  .property class assembly/R1
          r1()
  {
    .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 09 00 00 00 00 00 ) 
    .get class assembly/R1 assembly::get_r1()
  } 
  .property class assembly/R2
          r2()
  {
    .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 09 00 00 00 00 00 ) 
    .get class assembly/R2 assembly::get_r2()
  } 
  .property class '<>f__AnonymousType3073944080`3'<int32,string,string>
          r1AndR2()
  {
    .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 09 00 00 00 00 00 ) 
    .get class '<>f__AnonymousType3073944080`3'<int32,string,string> assembly::get_r1AndR2()
  } 
} 

.class private abstract auto ansi sealed '<StartupCode$assembly>'.$assembly
       extends [runtime]System.Object
{
  .field static assembly int32 init@
  .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
  .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
  .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
  .method public static void  main@() cil managed
  {
    .entrypoint
    
    .maxstack  8
    IL_0000:  call       void assembly::staticInitialization@()
    IL_0005:  ret
  } 

} 

.class public auto ansi serializable sealed beforefieldinit '<>f__AnonymousType3073944080`3'<'<A>j__TPar','<B>j__TPar','<C>j__TPar'>
       extends [runtime]System.Object
       implements [runtime]System.Collections.IStructuralComparable,
                  [runtime]System.IComparable,
                  class [runtime]System.IComparable`1<class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>>,
                  [runtime]System.Collections.IStructuralEquatable,
                  class [runtime]System.IEquatable`1<class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>>
{
  .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
  .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 02 00 00 00 00 00 ) 
  .field private !'<A>j__TPar' A@
  .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
  .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
  .field private !'<B>j__TPar' B@
  .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
  .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
  .field private !'<C>j__TPar' C@
  .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
  .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
  .method public specialname rtspecialname 
          instance void  .ctor(!'<A>j__TPar' A,
                               !'<B>j__TPar' B,
                               !'<C>j__TPar' C) cil managed
  {
    .custom instance void [runtime]System.Diagnostics.CodeAnalysis.DynamicDependencyAttribute::.ctor(valuetype [runtime]System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes,
                                                                                                            class [runtime]System.Type) = ( 01 00 60 06 00 00 1E 3C 3E 66 5F 5F 41 6E 6F 6E   
                                                                                                                                                   79 6D 6F 75 73 54 79 70 65 33 30 37 33 39 34 34   
                                                                                                                                                   30 38 30 60 33 00 00 )                            
    
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [runtime]System.Object::.ctor()
    IL_0006:  ldarg.0
    IL_0007:  ldarg.1
    IL_0008:  stfld      !0 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::A@
    IL_000d:  ldarg.0
    IL_000e:  ldarg.2
    IL_000f:  stfld      !1 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::B@
    IL_0014:  ldarg.0
    IL_0015:  ldarg.3
    IL_0016:  stfld      !2 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::C@
    IL_001b:  ret
  } 

  .method public hidebysig specialname instance !'<A>j__TPar' get_A() cil managed
  {
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
    
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  ldfld      !0 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::A@
    IL_0006:  ret
  } 

  .method public hidebysig specialname instance !'<B>j__TPar' get_B() cil managed
  {
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
    
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  ldfld      !1 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::B@
    IL_0006:  ret
  } 

  .method public hidebysig specialname instance !'<C>j__TPar' get_C() cil managed
  {
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
    
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  ldfld      !2 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::C@
    IL_0006:  ret
  } 

  .method public strict virtual instance string ToString() cil managed
  {
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    
    .maxstack  8
    IL_0000:  ldstr      "%+A"
    IL_0005:  newobj     instance void class [FSharp.Core]Microsoft.FSharp.Core.PrintfFormat`5<class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>,string>,class [FSharp.Core]Microsoft.FSharp.Core.Unit,string,string,class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>>::.ctor(string)
    IL_000a:  call       !!0 [FSharp.Core]Microsoft.FSharp.Core.ExtraTopLevelOperators::PrintFormatToString<class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>,string>>(class [FSharp.Core]Microsoft.FSharp.Core.PrintfFormat`4<!!0,class [FSharp.Core]Microsoft.FSharp.Core.Unit,string,string>)
    IL_000f:  ldarg.0
    IL_0010:  callvirt   instance !1 class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>,string>::Invoke(!0)
    IL_0015:  ret
  } 

  .method public hidebysig virtual final instance int32  CompareTo(class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'> obj) cil managed
  {
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    
    .maxstack  5
    .locals init (int32 V_0,
             int32 V_1)
    IL_0000:  ldarg.0
    IL_0001:  brfalse.s  IL_0067

    IL_0003:  ldarg.1
    IL_0004:  brfalse.s  IL_0065

    IL_0006:  call       class [runtime]System.Collections.IComparer [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::get_GenericComparer()
    IL_000b:  ldarg.0
    IL_000c:  ldfld      !0 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::A@
    IL_0011:  ldarg.1
    IL_0012:  ldfld      !0 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::A@
    IL_0017:  call       int32 [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::GenericComparisonWithComparer<!'<A>j__TPar'>(class [runtime]System.Collections.IComparer,
                                                                                                                                   !!0,
                                                                                                                                   !!0)
    IL_001c:  stloc.0
    IL_001d:  ldloc.0
    IL_001e:  ldc.i4.0
    IL_001f:  bge.s      IL_0023

    IL_0021:  ldloc.0
    IL_0022:  ret

    IL_0023:  ldloc.0
    IL_0024:  ldc.i4.0
    IL_0025:  ble.s      IL_0029

    IL_0027:  ldloc.0
    IL_0028:  ret

    IL_0029:  call       class [runtime]System.Collections.IComparer [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::get_GenericComparer()
    IL_002e:  ldarg.0
    IL_002f:  ldfld      !1 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::B@
    IL_0034:  ldarg.1
    IL_0035:  ldfld      !1 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::B@
    IL_003a:  call       int32 [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::GenericComparisonWithComparer<!'<B>j__TPar'>(class [runtime]System.Collections.IComparer,
                                                                                                                                   !!0,
                                                                                                                                   !!0)
    IL_003f:  stloc.1
    IL_0040:  ldloc.1
    IL_0041:  ldc.i4.0
    IL_0042:  bge.s      IL_0046

    IL_0044:  ldloc.1
    IL_0045:  ret

    IL_0046:  ldloc.1
    IL_0047:  ldc.i4.0
    IL_0048:  ble.s      IL_004c

    IL_004a:  ldloc.1
    IL_004b:  ret

    IL_004c:  call       class [runtime]System.Collections.IComparer [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::get_GenericComparer()
    IL_0051:  ldarg.0
    IL_0052:  ldfld      !2 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::C@
    IL_0057:  ldarg.1
    IL_0058:  ldfld      !2 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::C@
    IL_005d:  tail.
    IL_005f:  call       int32 [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::GenericComparisonWithComparer<!'<C>j__TPar'>(class [runtime]System.Collections.IComparer,
                                                                                                                                   !!0,
                                                                                                                                   !!0)
    IL_0064:  ret

    IL_0065:  ldc.i4.1
    IL_0066:  ret

    IL_0067:  ldarg.1
    IL_0068:  brfalse.s  IL_006c

    IL_006a:  ldc.i4.m1
    IL_006b:  ret

    IL_006c:  ldc.i4.0
    IL_006d:  ret
  } 

  .method public hidebysig virtual final instance int32  CompareTo(object obj) cil managed
  {
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  ldarg.1
    IL_0002:  unbox.any  class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>
    IL_0007:  tail.
    IL_0009:  callvirt   instance int32 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::CompareTo(class '<>f__AnonymousType3073944080`3'<!0,!1,!2>)
    IL_000e:  ret
  } 

  .method public hidebysig virtual final instance int32  CompareTo(object obj, class [runtime]System.Collections.IComparer comp) cil managed
  {
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    
    .maxstack  5
    .locals init (class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'> V_0,
             class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'> V_1,
             int32 V_2,
             int32 V_3)
    IL_0000:  ldarg.1
    IL_0001:  unbox.any  class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>
    IL_0006:  stloc.0
    IL_0007:  ldloc.0
    IL_0008:  stloc.1
    IL_0009:  ldarg.0
    IL_000a:  brfalse.s  IL_0069

    IL_000c:  ldarg.1
    IL_000d:  unbox.any  class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>
    IL_0012:  brfalse.s  IL_0067

    IL_0014:  ldarg.2
    IL_0015:  ldarg.0
    IL_0016:  ldfld      !0 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::A@
    IL_001b:  ldloc.1
    IL_001c:  ldfld      !0 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::A@
    IL_0021:  call       int32 [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::GenericComparisonWithComparer<!'<A>j__TPar'>(class [runtime]System.Collections.IComparer,
                                                                                                                                   !!0,
                                                                                                                                   !!0)
    IL_0026:  stloc.2
    IL_0027:  ldloc.2
    IL_0028:  ldc.i4.0
    IL_0029:  bge.s      IL_002d

    IL_002b:  ldloc.2
    IL_002c:  ret

    IL_002d:  ldloc.2
    IL_002e:  ldc.i4.0
    IL_002f:  ble.s      IL_0033

    IL_0031:  ldloc.2
    IL_0032:  ret

    IL_0033:  ldarg.2
    IL_0034:  ldarg.0
    IL_0035:  ldfld      !1 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::B@
    IL_003a:  ldloc.1
    IL_003b:  ldfld      !1 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::B@
    IL_0040:  call       int32 [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::GenericComparisonWithComparer<!'<B>j__TPar'>(class [runtime]System.Collections.IComparer,
                                                                                                                                   !!0,
                                                                                                                                   !!0)
    IL_0045:  stloc.3
    IL_0046:  ldloc.3
    IL_0047:  ldc.i4.0
    IL_0048:  bge.s      IL_004c

    IL_004a:  ldloc.3
    IL_004b:  ret

    IL_004c:  ldloc.3
    IL_004d:  ldc.i4.0
    IL_004e:  ble.s      IL_0052

    IL_0050:  ldloc.3
    IL_0051:  ret

    IL_0052:  ldarg.2
    IL_0053:  ldarg.0
    IL_0054:  ldfld      !2 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::C@
    IL_0059:  ldloc.1
    IL_005a:  ldfld      !2 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::C@
    IL_005f:  tail.
    IL_0061:  call       int32 [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::GenericComparisonWithComparer<!'<C>j__TPar'>(class [runtime]System.Collections.IComparer,
                                                                                                                                   !!0,
                                                                                                                                   !!0)
    IL_0066:  ret

    IL_0067:  ldc.i4.1
    IL_0068:  ret

    IL_0069:  ldarg.1
    IL_006a:  unbox.any  class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>
    IL_006f:  brfalse.s  IL_0073

    IL_0071:  ldc.i4.m1
    IL_0072:  ret

    IL_0073:  ldc.i4.0
    IL_0074:  ret
  } 

  .method public hidebysig virtual final instance int32  GetHashCode(class [runtime]System.Collections.IEqualityComparer comp) cil managed
  {
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    
    .maxstack  7
    .locals init (int32 V_0)
    IL_0000:  ldarg.0
    IL_0001:  brfalse.s  IL_0058

    IL_0003:  ldc.i4.0
    IL_0004:  stloc.0
    IL_0005:  ldc.i4     0x9e3779b9
    IL_000a:  ldarg.1
    IL_000b:  ldarg.0
    IL_000c:  ldfld      !2 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::C@
    IL_0011:  call       int32 [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::GenericHashWithComparer<!'<C>j__TPar'>(class [runtime]System.Collections.IEqualityComparer,
                                                                                                                             !!0)
    IL_0016:  ldloc.0
    IL_0017:  ldc.i4.6
    IL_0018:  shl
    IL_0019:  ldloc.0
    IL_001a:  ldc.i4.2
    IL_001b:  shr
    IL_001c:  add
    IL_001d:  add
    IL_001e:  add
    IL_001f:  stloc.0
    IL_0020:  ldc.i4     0x9e3779b9
    IL_0025:  ldarg.1
    IL_0026:  ldarg.0
    IL_0027:  ldfld      !1 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::B@
    IL_002c:  call       int32 [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::GenericHashWithComparer<!'<B>j__TPar'>(class [runtime]System.Collections.IEqualityComparer,
                                                                                                                             !!0)
    IL_0031:  ldloc.0
    IL_0032:  ldc.i4.6
    IL_0033:  shl
    IL_0034:  ldloc.0
    IL_0035:  ldc.i4.2
    IL_0036:  shr
    IL_0037:  add
    IL_0038:  add
    IL_0039:  add
    IL_003a:  stloc.0
    IL_003b:  ldc.i4     0x9e3779b9
    IL_0040:  ldarg.1
    IL_0041:  ldarg.0
    IL_0042:  ldfld      !0 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::A@
    IL_0047:  call       int32 [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::GenericHashWithComparer<!'<A>j__TPar'>(class [runtime]System.Collections.IEqualityComparer,
                                                                                                                             !!0)
    IL_004c:  ldloc.0
    IL_004d:  ldc.i4.6
    IL_004e:  shl
    IL_004f:  ldloc.0
    IL_0050:  ldc.i4.2
    IL_0051:  shr
    IL_0052:  add
    IL_0053:  add
    IL_0054:  add
    IL_0055:  stloc.0
    IL_0056:  ldloc.0
    IL_0057:  ret

    IL_0058:  ldc.i4.0
    IL_0059:  ret
  } 

  .method public hidebysig virtual final instance int32  GetHashCode() cil managed
  {
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       class [runtime]System.Collections.IEqualityComparer [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::get_GenericEqualityComparer()
    IL_0006:  tail.
    IL_0008:  callvirt   instance int32 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::GetHashCode(class [runtime]System.Collections.IEqualityComparer)
    IL_000d:  ret
  } 

  .method public hidebysig instance bool Equals(class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'> obj, class [runtime]System.Collections.IEqualityComparer comp) cil managed
  {
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    
    .maxstack  5
    .locals init (class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'> V_0)
    IL_0000:  ldarg.0
    IL_0001:  brfalse.s  IL_004b

    IL_0003:  ldarg.1
    IL_0004:  brfalse.s  IL_0049

    IL_0006:  ldarg.1
    IL_0007:  stloc.0
    IL_0008:  ldarg.2
    IL_0009:  ldarg.0
    IL_000a:  ldfld      !0 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::A@
    IL_000f:  ldloc.0
    IL_0010:  ldfld      !0 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::A@
    IL_0015:  call       bool [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::GenericEqualityWithComparer<!'<A>j__TPar'>(class [runtime]System.Collections.IEqualityComparer,
                                                                                                                                !!0,
                                                                                                                                !!0)
    IL_001a:  brfalse.s  IL_0047

    IL_001c:  ldarg.2
    IL_001d:  ldarg.0
    IL_001e:  ldfld      !1 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::B@
    IL_0023:  ldloc.0
    IL_0024:  ldfld      !1 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::B@
    IL_0029:  call       bool [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::GenericEqualityWithComparer<!'<B>j__TPar'>(class [runtime]System.Collections.IEqualityComparer,
                                                                                                                                !!0,
                                                                                                                                !!0)
    IL_002e:  brfalse.s  IL_0045

    IL_0030:  ldarg.2
    IL_0031:  ldarg.0
    IL_0032:  ldfld      !2 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::C@
    IL_0037:  ldloc.0
    IL_0038:  ldfld      !2 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::C@
    IL_003d:  tail.
    IL_003f:  call       bool [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::GenericEqualityWithComparer<!'<C>j__TPar'>(class [runtime]System.Collections.IEqualityComparer,
                                                                                                                                !!0,
                                                                                                                                !!0)
    IL_0044:  ret

    IL_0045:  ldc.i4.0
    IL_0046:  ret

    IL_0047:  ldc.i4.0
    IL_0048:  ret

    IL_0049:  ldc.i4.0
    IL_004a:  ret

    IL_004b:  ldarg.1
    IL_004c:  ldnull
    IL_004d:  cgt.un
    IL_004f:  ldc.i4.0
    IL_0050:  ceq
    IL_0052:  ret
  } 

  .method public hidebysig virtual final instance bool  Equals(object obj, class [runtime]System.Collections.IEqualityComparer comp) cil managed
  {
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    
    .maxstack  5
    .locals init (class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'> V_0)
    IL_0000:  ldarg.1
    IL_0001:  isinst     class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>
    IL_0006:  stloc.0
    IL_0007:  ldloc.0
    IL_0008:  brfalse.s  IL_0015

    IL_000a:  ldarg.0
    IL_000b:  ldloc.0
    IL_000c:  ldarg.2
    IL_000d:  tail.
    IL_000f:  callvirt   instance bool class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::Equals(class '<>f__AnonymousType3073944080`3'<!0,!1,!2>,
                                                                                                                                 class [runtime]System.Collections.IEqualityComparer)
    IL_0014:  ret

    IL_0015:  ldc.i4.0
    IL_0016:  ret
  } 

  .method public hidebysig virtual final instance bool  Equals(class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'> obj) cil managed
  {
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    
    .maxstack  4
    IL_0000:  ldarg.0
    IL_0001:  brfalse.s  IL_0046

    IL_0003:  ldarg.1
    IL_0004:  brfalse.s  IL_0044

    IL_0006:  ldarg.0
    IL_0007:  ldfld      !0 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::A@
    IL_000c:  ldarg.1
    IL_000d:  ldfld      !0 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::A@
    IL_0012:  call       bool [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::GenericEqualityER<!'<A>j__TPar'>(!!0,
                                                                                                                      !!0)
    IL_0017:  brfalse.s  IL_0042

    IL_0019:  ldarg.0
    IL_001a:  ldfld      !1 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::B@
    IL_001f:  ldarg.1
    IL_0020:  ldfld      !1 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::B@
    IL_0025:  call       bool [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::GenericEqualityER<!'<B>j__TPar'>(!!0,
                                                                                                                      !!0)
    IL_002a:  brfalse.s  IL_0040

    IL_002c:  ldarg.0
    IL_002d:  ldfld      !2 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::C@
    IL_0032:  ldarg.1
    IL_0033:  ldfld      !2 class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::C@
    IL_0038:  tail.
    IL_003a:  call       bool [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::GenericEqualityER<!'<C>j__TPar'>(!!0,
                                                                                                                      !!0)
    IL_003f:  ret

    IL_0040:  ldc.i4.0
    IL_0041:  ret

    IL_0042:  ldc.i4.0
    IL_0043:  ret

    IL_0044:  ldc.i4.0
    IL_0045:  ret

    IL_0046:  ldarg.1
    IL_0047:  ldnull
    IL_0048:  cgt.un
    IL_004a:  ldc.i4.0
    IL_004b:  ceq
    IL_004d:  ret
  } 

  .method public hidebysig virtual final instance bool  Equals(object obj) cil managed
  {
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    
    .maxstack  4
    .locals init (class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'> V_0)
    IL_0000:  ldarg.1
    IL_0001:  isinst     class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>
    IL_0006:  stloc.0
    IL_0007:  ldloc.0
    IL_0008:  brfalse.s  IL_0014

    IL_000a:  ldarg.0
    IL_000b:  ldloc.0
    IL_000c:  tail.
    IL_000e:  callvirt   instance bool class '<>f__AnonymousType3073944080`3'<!'<A>j__TPar',!'<B>j__TPar',!'<C>j__TPar'>::Equals(class '<>f__AnonymousType3073944080`3'<!0,!1,!2>)
    IL_0013:  ret

    IL_0014:  ldc.i4.0
    IL_0015:  ret
  } 

  .property instance !'<A>j__TPar' A()
  {
    .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags,
                                                                                                int32) = ( 01 00 04 00 00 00 00 00 00 00 00 00 ) 
    .get instance !'<A>j__TPar' '<>f__AnonymousType3073944080`3'::get_A()
  } 
  .property instance !'<B>j__TPar' B()
  {
    .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags,
                                                                                                int32) = ( 01 00 04 00 00 00 01 00 00 00 00 00 ) 
    .get instance !'<B>j__TPar' '<>f__AnonymousType3073944080`3'::get_B()
  } 
  .property instance !'<C>j__TPar' C()
  {
    .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags,
                                                                                                int32) = ( 01 00 04 00 00 00 02 00 00 00 00 00 ) 
    .get instance !'<C>j__TPar' '<>f__AnonymousType3073944080`3'::get_C()
  } 
} 






