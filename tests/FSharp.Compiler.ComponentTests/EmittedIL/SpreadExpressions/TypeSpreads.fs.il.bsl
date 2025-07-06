




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
                                                                                                              class [runtime]System.Type) = ( 01 00 60 06 00 00 0E 54 79 70 65 53 70 72 65 61   
                                                                                                                                                     64 73 2B 52 31 00 00 )                            
      
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
    .field assembly int32 X@
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
    .field assembly string Y@
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
    .method public hidebysig specialname instance int32  get_X() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  ldfld      int32 assembly/R2::X@
      IL_0006:  ret
    } 

    .method public hidebysig specialname instance string  get_Y() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  ldfld      string assembly/R2::Y@
      IL_0006:  ret
    } 

    .method public specialname rtspecialname instance void  .ctor(int32 x, string y) cil managed
    {
      .custom instance void [runtime]System.Diagnostics.CodeAnalysis.DynamicDependencyAttribute::.ctor(valuetype [runtime]System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes,
                                                                                                              class [runtime]System.Type) = ( 01 00 60 06 00 00 0E 54 79 70 65 53 70 72 65 61   
                                                                                                                                                     64 73 2B 52 32 00 00 )                            
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  call       instance void [runtime]System.Object::.ctor()
      IL_0006:  ldarg.0
      IL_0007:  ldarg.1
      IL_0008:  stfld      int32 assembly/R2::X@
      IL_000d:  ldarg.0
      IL_000e:  ldarg.2
      IL_000f:  stfld      string assembly/R2::Y@
      IL_0014:  ret
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
      IL_0001:  brfalse.s  IL_004a

      IL_0003:  ldarg.1
      IL_0004:  brfalse.s  IL_0048

      IL_0006:  call       class [runtime]System.Collections.IComparer [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::get_GenericComparer()
      IL_000b:  stloc.1
      IL_000c:  ldarg.0
      IL_000d:  ldfld      int32 assembly/R2::X@
      IL_0012:  stloc.2
      IL_0013:  ldarg.1
      IL_0014:  ldfld      int32 assembly/R2::X@
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
      IL_0037:  ldfld      string assembly/R2::Y@
      IL_003c:  ldarg.1
      IL_003d:  ldfld      string assembly/R2::Y@
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
      IL_0008:  brfalse.s  IL_004a

      IL_000a:  ldarg.1
      IL_000b:  unbox.any  assembly/R2
      IL_0010:  brfalse.s  IL_0048

      IL_0012:  ldarg.0
      IL_0013:  ldfld      int32 assembly/R2::X@
      IL_0018:  stloc.2
      IL_0019:  ldloc.0
      IL_001a:  ldfld      int32 assembly/R2::X@
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
      IL_0037:  ldfld      string assembly/R2::Y@
      IL_003c:  ldloc.0
      IL_003d:  ldfld      string assembly/R2::Y@
      IL_0042:  call       int32 [netstandard]System.String::CompareOrdinal(string,
                                                                            string)
      IL_0047:  ret

      IL_0048:  ldc.i4.1
      IL_0049:  ret

      IL_004a:  ldarg.1
      IL_004b:  unbox.any  assembly/R2
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
      IL_000b:  ldfld      string assembly/R2::Y@
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
      IL_002f:  ldfld      int32 assembly/R2::X@
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
      IL_0006:  callvirt   instance int32 assembly/R2::GetHashCode(class [runtime]System.Collections.IEqualityComparer)
      IL_000b:  ret
    } 

    .method public hidebysig instance bool Equals(class assembly/R2 obj, class [runtime]System.Collections.IEqualityComparer comp) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  brfalse.s  IL_002a

      IL_0003:  ldarg.1
      IL_0004:  brfalse.s  IL_0028

      IL_0006:  ldarg.0
      IL_0007:  ldfld      int32 assembly/R2::X@
      IL_000c:  ldarg.1
      IL_000d:  ldfld      int32 assembly/R2::X@
      IL_0012:  bne.un.s   IL_0026

      IL_0014:  ldarg.0
      IL_0015:  ldfld      string assembly/R2::Y@
      IL_001a:  ldarg.1
      IL_001b:  ldfld      string assembly/R2::Y@
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
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  brfalse.s  IL_002a

      IL_0003:  ldarg.1
      IL_0004:  brfalse.s  IL_0028

      IL_0006:  ldarg.0
      IL_0007:  ldfld      int32 assembly/R2::X@
      IL_000c:  ldarg.1
      IL_000d:  ldfld      int32 assembly/R2::X@
      IL_0012:  bne.un.s   IL_0026

      IL_0014:  ldarg.0
      IL_0015:  ldfld      string assembly/R2::Y@
      IL_001a:  ldarg.1
      IL_001b:  ldfld      string assembly/R2::Y@
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

    .property instance int32 X()
    {
      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags,
                                                                                                  int32) = ( 01 00 04 00 00 00 00 00 00 00 00 00 ) 
      .get instance int32 assembly/R2::get_X()
    } 
    .property instance string Y()
    {
      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags,
                                                                                                  int32) = ( 01 00 04 00 00 00 01 00 00 00 00 00 ) 
      .get instance string assembly/R2::get_Y()
    } 
  } 

  .class auto ansi serializable sealed nested public R3
         extends [runtime]System.Object
         implements class [runtime]System.IEquatable`1<class assembly/R3>,
                    [runtime]System.Collections.IStructuralEquatable,
                    class [runtime]System.IComparable`1<class assembly/R3>,
                    [runtime]System.IComparable,
                    [runtime]System.Collections.IStructuralComparable
  {
    .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 02 00 00 00 00 00 ) 
    .class auto ansi serializable sealed nested assembly beforefieldinit clo@3
           extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class [FSharp.Core]Microsoft.FSharp.Core.Unit,int32>
    {
      .field public class assembly/R3 this
      .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      .field public class assembly/R3 obj
      .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      .method assembly specialname rtspecialname instance void  .ctor(class assembly/R3 this, class assembly/R3 obj) cil managed
      {
        .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
        .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
        
        .maxstack  8
        IL_0000:  ldarg.0
        IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class [FSharp.Core]Microsoft.FSharp.Core.Unit,int32>::.ctor()
        IL_0006:  ldarg.0
        IL_0007:  ldarg.1
        IL_0008:  stfld      class assembly/R3 assembly/R3/clo@3::this
        IL_000d:  ldarg.0
        IL_000e:  ldarg.2
        IL_000f:  stfld      class assembly/R3 assembly/R3/clo@3::obj
        IL_0014:  ret
      } 

      .method public strict virtual instance int32 Invoke(class [FSharp.Core]Microsoft.FSharp.Core.Unit unitVar) cil managed
      {
        
        .maxstack  7
        .locals init (int32 V_0,
                 class [runtime]System.Collections.IComparer V_1,
                 int32 V_2,
                 int32 V_3,
                 float64 V_4,
                 float64 V_5)
        IL_0000:  ldarg.0
        IL_0001:  ldfld      class assembly/R3 assembly/R3/clo@3::obj
        IL_0006:  brfalse    IL_00b8

        IL_000b:  call       class [runtime]System.Collections.IComparer [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::get_GenericComparer()
        IL_0010:  stloc.1
        IL_0011:  ldarg.0
        IL_0012:  ldfld      class assembly/R3 assembly/R3/clo@3::this
        IL_0017:  ldfld      int32 assembly/R3::A@
        IL_001c:  stloc.2
        IL_001d:  ldarg.0
        IL_001e:  ldfld      class assembly/R3 assembly/R3/clo@3::obj
        IL_0023:  ldfld      int32 assembly/R3::A@
        IL_0028:  stloc.3
        IL_0029:  ldloc.2
        IL_002a:  ldloc.3
        IL_002b:  cgt
        IL_002d:  ldloc.2
        IL_002e:  ldloc.3
        IL_002f:  clt
        IL_0031:  sub
        IL_0032:  stloc.0
        IL_0033:  ldloc.0
        IL_0034:  ldc.i4.0
        IL_0035:  bge.s      IL_0039

        IL_0037:  ldloc.0
        IL_0038:  ret

        IL_0039:  ldloc.0
        IL_003a:  ldc.i4.0
        IL_003b:  ble.s      IL_003f

        IL_003d:  ldloc.0
        IL_003e:  ret

        IL_003f:  call       class [runtime]System.Collections.IComparer [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::get_GenericComparer()
        IL_0044:  stloc.1
        IL_0045:  ldarg.0
        IL_0046:  ldfld      class assembly/R3 assembly/R3/clo@3::this
        IL_004b:  ldfld      string assembly/R3::B@
        IL_0050:  ldarg.0
        IL_0051:  ldfld      class assembly/R3 assembly/R3/clo@3::obj
        IL_0056:  ldfld      string assembly/R3::B@
        IL_005b:  call       int32 [netstandard]System.String::CompareOrdinal(string,
                                                                              string)
        IL_0060:  stloc.2
        IL_0061:  ldloc.2
        IL_0062:  ldc.i4.0
        IL_0063:  bge.s      IL_0067

        IL_0065:  ldloc.2
        IL_0066:  ret

        IL_0067:  ldloc.2
        IL_0068:  ldc.i4.0
        IL_0069:  ble.s      IL_006d

        IL_006b:  ldloc.2
        IL_006c:  ret

        IL_006d:  call       class [runtime]System.Collections.IComparer [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::get_GenericComparer()
        IL_0072:  stloc.1
        IL_0073:  ldarg.0
        IL_0074:  ldfld      class assembly/R3 assembly/R3/clo@3::this
        IL_0079:  ldfld      float64 assembly/R3::C@
        IL_007e:  stloc.s    V_4
        IL_0080:  ldarg.0
        IL_0081:  ldfld      class assembly/R3 assembly/R3/clo@3::obj
        IL_0086:  ldfld      float64 assembly/R3::C@
        IL_008b:  stloc.s    V_5
        IL_008d:  ldloc.s    V_4
        IL_008f:  ldloc.s    V_5
        IL_0091:  clt
        IL_0093:  brfalse.s  IL_0097

        IL_0095:  ldc.i4.m1
        IL_0096:  ret

        IL_0097:  ldloc.s    V_4
        IL_0099:  ldloc.s    V_5
        IL_009b:  cgt
        IL_009d:  brfalse.s  IL_00a1

        IL_009f:  ldc.i4.1
        IL_00a0:  ret

        IL_00a1:  ldloc.s    V_4
        IL_00a3:  ldloc.s    V_5
        IL_00a5:  ceq
        IL_00a7:  brfalse.s  IL_00ab

        IL_00a9:  ldc.i4.0
        IL_00aa:  ret

        IL_00ab:  ldloc.1
        IL_00ac:  ldloc.s    V_4
        IL_00ae:  ldloc.s    V_5
        IL_00b0:  tail.
        IL_00b2:  call       int32 [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives/HashCompare::GenericComparisonWithComparerIntrinsic<float64>(class [runtime]System.Collections.IComparer,
                                                                                                                                                      !!0,
                                                                                                                                                      !!0)
        IL_00b7:  ret

        IL_00b8:  ldc.i4.1
        IL_00b9:  ret
      } 

    } 

    .class auto ansi serializable sealed nested assembly beforefieldinit 'clo@3-1'
           extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class [FSharp.Core]Microsoft.FSharp.Core.Unit,int32>
    {
      .field public class assembly/R3 this
      .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      .field public object obj
      .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      .field public class [runtime]System.Collections.IComparer comp
      .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      .field public class assembly/R3 objTemp
      .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      .method assembly specialname rtspecialname 
              instance void  .ctor(class assembly/R3 this,
                                   object obj,
                                   class [runtime]System.Collections.IComparer comp,
                                   class assembly/R3 objTemp) cil managed
      {
        .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
        .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
        
        .maxstack  8
        IL_0000:  ldarg.0
        IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class [FSharp.Core]Microsoft.FSharp.Core.Unit,int32>::.ctor()
        IL_0006:  ldarg.0
        IL_0007:  ldarg.1
        IL_0008:  stfld      class assembly/R3 assembly/R3/'clo@3-1'::this
        IL_000d:  ldarg.0
        IL_000e:  ldarg.2
        IL_000f:  stfld      object assembly/R3/'clo@3-1'::obj
        IL_0014:  ldarg.0
        IL_0015:  ldarg.3
        IL_0016:  stfld      class [runtime]System.Collections.IComparer assembly/R3/'clo@3-1'::comp
        IL_001b:  ldarg.0
        IL_001c:  ldarg.s    objTemp
        IL_001e:  stfld      class assembly/R3 assembly/R3/'clo@3-1'::objTemp
        IL_0023:  ret
      } 

      .method public strict virtual instance int32 Invoke(class [FSharp.Core]Microsoft.FSharp.Core.Unit unitVar) cil managed
      {
        
        .maxstack  7
        .locals init (int32 V_0,
                 int32 V_1,
                 int32 V_2,
                 float64 V_3,
                 float64 V_4)
        IL_0000:  ldarg.0
        IL_0001:  ldfld      object assembly/R3/'clo@3-1'::obj
        IL_0006:  unbox.any  assembly/R3
        IL_000b:  brfalse    IL_00ab

        IL_0010:  ldarg.0
        IL_0011:  ldfld      class assembly/R3 assembly/R3/'clo@3-1'::this
        IL_0016:  ldfld      int32 assembly/R3::A@
        IL_001b:  stloc.1
        IL_001c:  ldarg.0
        IL_001d:  ldfld      class assembly/R3 assembly/R3/'clo@3-1'::objTemp
        IL_0022:  ldfld      int32 assembly/R3::A@
        IL_0027:  stloc.2
        IL_0028:  ldloc.1
        IL_0029:  ldloc.2
        IL_002a:  cgt
        IL_002c:  ldloc.1
        IL_002d:  ldloc.2
        IL_002e:  clt
        IL_0030:  sub
        IL_0031:  stloc.0
        IL_0032:  ldloc.0
        IL_0033:  ldc.i4.0
        IL_0034:  bge.s      IL_0038

        IL_0036:  ldloc.0
        IL_0037:  ret

        IL_0038:  ldloc.0
        IL_0039:  ldc.i4.0
        IL_003a:  ble.s      IL_003e

        IL_003c:  ldloc.0
        IL_003d:  ret

        IL_003e:  ldarg.0
        IL_003f:  ldfld      class assembly/R3 assembly/R3/'clo@3-1'::this
        IL_0044:  ldfld      string assembly/R3::B@
        IL_0049:  ldarg.0
        IL_004a:  ldfld      class assembly/R3 assembly/R3/'clo@3-1'::objTemp
        IL_004f:  ldfld      string assembly/R3::B@
        IL_0054:  call       int32 [netstandard]System.String::CompareOrdinal(string,
                                                                              string)
        IL_0059:  stloc.1
        IL_005a:  ldloc.1
        IL_005b:  ldc.i4.0
        IL_005c:  bge.s      IL_0060

        IL_005e:  ldloc.1
        IL_005f:  ret

        IL_0060:  ldloc.1
        IL_0061:  ldc.i4.0
        IL_0062:  ble.s      IL_0066

        IL_0064:  ldloc.1
        IL_0065:  ret

        IL_0066:  ldarg.0
        IL_0067:  ldfld      class assembly/R3 assembly/R3/'clo@3-1'::this
        IL_006c:  ldfld      float64 assembly/R3::C@
        IL_0071:  stloc.3
        IL_0072:  ldarg.0
        IL_0073:  ldfld      class assembly/R3 assembly/R3/'clo@3-1'::objTemp
        IL_0078:  ldfld      float64 assembly/R3::C@
        IL_007d:  stloc.s    V_4
        IL_007f:  ldloc.3
        IL_0080:  ldloc.s    V_4
        IL_0082:  clt
        IL_0084:  brfalse.s  IL_0088

        IL_0086:  ldc.i4.m1
        IL_0087:  ret

        IL_0088:  ldloc.3
        IL_0089:  ldloc.s    V_4
        IL_008b:  cgt
        IL_008d:  brfalse.s  IL_0091

        IL_008f:  ldc.i4.1
        IL_0090:  ret

        IL_0091:  ldloc.3
        IL_0092:  ldloc.s    V_4
        IL_0094:  ceq
        IL_0096:  brfalse.s  IL_009a

        IL_0098:  ldc.i4.0
        IL_0099:  ret

        IL_009a:  ldarg.0
        IL_009b:  ldfld      class [runtime]System.Collections.IComparer assembly/R3/'clo@3-1'::comp
        IL_00a0:  ldloc.3
        IL_00a1:  ldloc.s    V_4
        IL_00a3:  tail.
        IL_00a5:  call       int32 [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives/HashCompare::GenericComparisonWithComparerIntrinsic<float64>(class [runtime]System.Collections.IComparer,
                                                                                                                                                      !!0,
                                                                                                                                                      !!0)
        IL_00aa:  ret

        IL_00ab:  ldc.i4.1
        IL_00ac:  ret
      } 

    } 

    .field assembly int32 A@
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
    .field assembly string B@
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
    .field assembly float64 C@
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
    .method public hidebysig specialname instance int32  get_A() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  ldfld      int32 assembly/R3::A@
      IL_0006:  ret
    } 

    .method public hidebysig specialname instance string  get_B() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  ldfld      string assembly/R3::B@
      IL_0006:  ret
    } 

    .method public hidebysig specialname instance float64  get_C() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  ldfld      float64 assembly/R3::C@
      IL_0006:  ret
    } 

    .method public specialname rtspecialname 
            instance void  .ctor(int32 a,
                                 string b,
                                 float64 c) cil managed
    {
      .custom instance void [runtime]System.Diagnostics.CodeAnalysis.DynamicDependencyAttribute::.ctor(valuetype [runtime]System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes,
                                                                                                              class [runtime]System.Type) = ( 01 00 60 06 00 00 0E 54 79 70 65 53 70 72 65 61   
                                                                                                                                                     64 73 2B 52 33 00 00 )                            
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  call       instance void [runtime]System.Object::.ctor()
      IL_0006:  ldarg.0
      IL_0007:  ldarg.1
      IL_0008:  stfld      int32 assembly/R3::A@
      IL_000d:  ldarg.0
      IL_000e:  ldarg.2
      IL_000f:  stfld      string assembly/R3::B@
      IL_0014:  ldarg.0
      IL_0015:  ldarg.3
      IL_0016:  stfld      float64 assembly/R3::C@
      IL_001b:  ret
    } 

    .method public strict virtual instance string ToString() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldstr      "%+A"
      IL_0005:  newobj     instance void class [FSharp.Core]Microsoft.FSharp.Core.PrintfFormat`5<class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class assembly/R3,string>,class [FSharp.Core]Microsoft.FSharp.Core.Unit,string,string,class assembly/R3>::.ctor(string)
      IL_000a:  call       !!0 [FSharp.Core]Microsoft.FSharp.Core.ExtraTopLevelOperators::PrintFormatToString<class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class assembly/R3,string>>(class [FSharp.Core]Microsoft.FSharp.Core.PrintfFormat`4<!!0,class [FSharp.Core]Microsoft.FSharp.Core.Unit,string,string>)
      IL_000f:  ldarg.0
      IL_0010:  callvirt   instance !1 class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class assembly/R3,string>::Invoke(!0)
      IL_0015:  ret
    } 

    .method public hidebysig virtual final instance int32  CompareTo(class assembly/R3 obj) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  4
      .locals init (class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class [FSharp.Core]Microsoft.FSharp.Core.Unit,int32> V_0)
      IL_0000:  ldarg.0
      IL_0001:  brfalse.s  IL_0015

      IL_0003:  ldarg.0
      IL_0004:  ldarg.1
      IL_0005:  newobj     instance void assembly/R3/clo@3::.ctor(class assembly/R3,
                                                                     class assembly/R3)
      IL_000a:  stloc.0
      IL_000b:  ldloc.0
      IL_000c:  ldnull
      IL_000d:  tail.
      IL_000f:  callvirt   instance !1 class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class [FSharp.Core]Microsoft.FSharp.Core.Unit,int32>::Invoke(!0)
      IL_0014:  ret

      IL_0015:  ldarg.1
      IL_0016:  brfalse.s  IL_001a

      IL_0018:  ldc.i4.m1
      IL_0019:  ret

      IL_001a:  ldc.i4.0
      IL_001b:  ret
    } 

    .method public hidebysig virtual final instance int32  CompareTo(object obj) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  ldarg.1
      IL_0002:  unbox.any  assembly/R3
      IL_0007:  tail.
      IL_0009:  callvirt   instance int32 assembly/R3::CompareTo(class assembly/R3)
      IL_000e:  ret
    } 

    .method public hidebysig virtual final instance int32  CompareTo(object obj, class [runtime]System.Collections.IComparer comp) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  6
      .locals init (class assembly/R3 V_0,
               class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class [FSharp.Core]Microsoft.FSharp.Core.Unit,int32> V_1)
      IL_0000:  ldarg.1
      IL_0001:  unbox.any  assembly/R3
      IL_0006:  stloc.0
      IL_0007:  ldarg.0
      IL_0008:  brfalse.s  IL_001e

      IL_000a:  ldarg.0
      IL_000b:  ldarg.1
      IL_000c:  ldarg.2
      IL_000d:  ldloc.0
      IL_000e:  newobj     instance void assembly/R3/'clo@3-1'::.ctor(class assembly/R3,
                                                                         object,
                                                                         class [runtime]System.Collections.IComparer,
                                                                         class assembly/R3)
      IL_0013:  stloc.1
      IL_0014:  ldloc.1
      IL_0015:  ldnull
      IL_0016:  tail.
      IL_0018:  callvirt   instance !1 class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class [FSharp.Core]Microsoft.FSharp.Core.Unit,int32>::Invoke(!0)
      IL_001d:  ret

      IL_001e:  ldarg.1
      IL_001f:  unbox.any  assembly/R3
      IL_0024:  brfalse.s  IL_0028

      IL_0026:  ldc.i4.m1
      IL_0027:  ret

      IL_0028:  ldc.i4.0
      IL_0029:  ret
    } 

    .method public hidebysig virtual final instance int32  GetHashCode(class [runtime]System.Collections.IEqualityComparer comp) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  7
      .locals init (int32 V_0,
               string V_1)
      IL_0000:  ldarg.0
      IL_0001:  brfalse.s  IL_005b

      IL_0003:  ldc.i4.0
      IL_0004:  stloc.0
      IL_0005:  ldc.i4     0x9e3779b9
      IL_000a:  ldarg.1
      IL_000b:  ldarg.0
      IL_000c:  ldfld      float64 assembly/R3::C@
      IL_0011:  call       int32 [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives/HashCompare::GenericHashWithComparerIntrinsic<float64>(class [runtime]System.Collections.IEqualityComparer,
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
      IL_0025:  ldarg.0
      IL_0026:  ldfld      string assembly/R3::B@
      IL_002b:  stloc.1
      IL_002c:  ldloc.1
      IL_002d:  brtrue.s   IL_0033

      IL_002f:  ldc.i4.0
      IL_0030:  nop
      IL_0031:  br.s       IL_003a

      IL_0033:  ldloc.1
      IL_0034:  callvirt   instance int32 [netstandard]System.Object::GetHashCode()
      IL_0039:  nop
      IL_003a:  ldloc.0
      IL_003b:  ldc.i4.6
      IL_003c:  shl
      IL_003d:  ldloc.0
      IL_003e:  ldc.i4.2
      IL_003f:  shr
      IL_0040:  add
      IL_0041:  add
      IL_0042:  add
      IL_0043:  stloc.0
      IL_0044:  ldc.i4     0x9e3779b9
      IL_0049:  ldarg.0
      IL_004a:  ldfld      int32 assembly/R3::A@
      IL_004f:  ldloc.0
      IL_0050:  ldc.i4.6
      IL_0051:  shl
      IL_0052:  ldloc.0
      IL_0053:  ldc.i4.2
      IL_0054:  shr
      IL_0055:  add
      IL_0056:  add
      IL_0057:  add
      IL_0058:  stloc.0
      IL_0059:  ldloc.0
      IL_005a:  ret

      IL_005b:  ldc.i4.0
      IL_005c:  ret
    } 

    .method public hidebysig virtual final instance int32  GetHashCode() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  call       class [runtime]System.Collections.IEqualityComparer [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::get_GenericEqualityComparer()
      IL_0006:  callvirt   instance int32 assembly/R3::GetHashCode(class [runtime]System.Collections.IEqualityComparer)
      IL_000b:  ret
    } 

    .method public hidebysig instance bool Equals(class assembly/R3 obj, class [runtime]System.Collections.IEqualityComparer comp) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  4
      IL_0000:  ldarg.0
      IL_0001:  brfalse.s  IL_003c

      IL_0003:  ldarg.1
      IL_0004:  brfalse.s  IL_003a

      IL_0006:  ldarg.0
      IL_0007:  ldfld      int32 assembly/R3::A@
      IL_000c:  ldarg.1
      IL_000d:  ldfld      int32 assembly/R3::A@
      IL_0012:  bne.un.s   IL_0038

      IL_0014:  ldarg.0
      IL_0015:  ldfld      string assembly/R3::B@
      IL_001a:  ldarg.1
      IL_001b:  ldfld      string assembly/R3::B@
      IL_0020:  call       bool [netstandard]System.String::Equals(string,
                                                                   string)
      IL_0025:  brfalse.s  IL_0036

      IL_0027:  ldarg.0
      IL_0028:  ldfld      float64 assembly/R3::C@
      IL_002d:  ldarg.1
      IL_002e:  ldfld      float64 assembly/R3::C@
      IL_0033:  ceq
      IL_0035:  ret

      IL_0036:  ldc.i4.0
      IL_0037:  ret

      IL_0038:  ldc.i4.0
      IL_0039:  ret

      IL_003a:  ldc.i4.0
      IL_003b:  ret

      IL_003c:  ldarg.1
      IL_003d:  ldnull
      IL_003e:  cgt.un
      IL_0040:  ldc.i4.0
      IL_0041:  ceq
      IL_0043:  ret
    } 

    .method public hidebysig virtual final instance bool  Equals(object obj, class [runtime]System.Collections.IEqualityComparer comp) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  5
      .locals init (class assembly/R3 V_0)
      IL_0000:  ldarg.1
      IL_0001:  isinst     assembly/R3
      IL_0006:  stloc.0
      IL_0007:  ldloc.0
      IL_0008:  brfalse.s  IL_0013

      IL_000a:  ldarg.0
      IL_000b:  ldloc.0
      IL_000c:  ldarg.2
      IL_000d:  callvirt   instance bool assembly/R3::Equals(class assembly/R3,
                                                                class [runtime]System.Collections.IEqualityComparer)
      IL_0012:  ret

      IL_0013:  ldc.i4.0
      IL_0014:  ret
    } 

    .method public hidebysig virtual final instance bool  Equals(class assembly/R3 obj) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  4
      .locals init (float64 V_0,
               float64 V_1)
      IL_0000:  ldarg.0
      IL_0001:  brfalse.s  IL_0051

      IL_0003:  ldarg.1
      IL_0004:  brfalse.s  IL_004f

      IL_0006:  ldarg.0
      IL_0007:  ldfld      int32 assembly/R3::A@
      IL_000c:  ldarg.1
      IL_000d:  ldfld      int32 assembly/R3::A@
      IL_0012:  bne.un.s   IL_004d

      IL_0014:  ldarg.0
      IL_0015:  ldfld      string assembly/R3::B@
      IL_001a:  ldarg.1
      IL_001b:  ldfld      string assembly/R3::B@
      IL_0020:  call       bool [netstandard]System.String::Equals(string,
                                                                   string)
      IL_0025:  brfalse.s  IL_004b

      IL_0027:  ldarg.0
      IL_0028:  ldfld      float64 assembly/R3::C@
      IL_002d:  stloc.0
      IL_002e:  ldarg.1
      IL_002f:  ldfld      float64 assembly/R3::C@
      IL_0034:  stloc.1
      IL_0035:  ldloc.0
      IL_0036:  ldloc.1
      IL_0037:  ceq
      IL_0039:  brfalse.s  IL_003d

      IL_003b:  ldc.i4.1
      IL_003c:  ret

      IL_003d:  ldloc.0
      IL_003e:  ldloc.0
      IL_003f:  beq.s      IL_0049

      IL_0041:  ldloc.1
      IL_0042:  ldloc.1
      IL_0043:  ceq
      IL_0045:  ldc.i4.0
      IL_0046:  ceq
      IL_0048:  ret

      IL_0049:  ldc.i4.0
      IL_004a:  ret

      IL_004b:  ldc.i4.0
      IL_004c:  ret

      IL_004d:  ldc.i4.0
      IL_004e:  ret

      IL_004f:  ldc.i4.0
      IL_0050:  ret

      IL_0051:  ldarg.1
      IL_0052:  ldnull
      IL_0053:  cgt.un
      IL_0055:  ldc.i4.0
      IL_0056:  ceq
      IL_0058:  ret
    } 

    .method public hidebysig virtual final instance bool  Equals(object obj) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  4
      .locals init (class assembly/R3 V_0)
      IL_0000:  ldarg.1
      IL_0001:  isinst     assembly/R3
      IL_0006:  stloc.0
      IL_0007:  ldloc.0
      IL_0008:  brfalse.s  IL_0012

      IL_000a:  ldarg.0
      IL_000b:  ldloc.0
      IL_000c:  callvirt   instance bool assembly/R3::Equals(class assembly/R3)
      IL_0011:  ret

      IL_0012:  ldc.i4.0
      IL_0013:  ret
    } 

    .property instance int32 A()
    {
      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags,
                                                                                                  int32) = ( 01 00 04 00 00 00 00 00 00 00 00 00 ) 
      .get instance int32 assembly/R3::get_A()
    } 
    .property instance string B()
    {
      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags,
                                                                                                  int32) = ( 01 00 04 00 00 00 01 00 00 00 00 00 ) 
      .get instance string assembly/R3::get_B()
    } 
    .property instance float64 C()
    {
      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags,
                                                                                                  int32) = ( 01 00 04 00 00 00 02 00 00 00 00 00 ) 
      .get instance float64 assembly/R3::get_C()
    } 
  } 

  .class auto ansi serializable sealed nested public R4
         extends [runtime]System.Object
         implements class [runtime]System.IEquatable`1<class assembly/R4>,
                    [runtime]System.Collections.IStructuralEquatable,
                    class [runtime]System.IComparable`1<class assembly/R4>,
                    [runtime]System.IComparable,
                    [runtime]System.Collections.IStructuralComparable
  {
    .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 02 00 00 00 00 00 ) 
    .class auto ansi serializable sealed nested assembly beforefieldinit 'clo@4-2'
           extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class [FSharp.Core]Microsoft.FSharp.Core.Unit,int32>
    {
      .field public class assembly/R4 this
      .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      .field public class assembly/R4 obj
      .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      .method assembly specialname rtspecialname instance void  .ctor(class assembly/R4 this, class assembly/R4 obj) cil managed
      {
        .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
        .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
        
        .maxstack  8
        IL_0000:  ldarg.0
        IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class [FSharp.Core]Microsoft.FSharp.Core.Unit,int32>::.ctor()
        IL_0006:  ldarg.0
        IL_0007:  ldarg.1
        IL_0008:  stfld      class assembly/R4 assembly/R4/'clo@4-2'::this
        IL_000d:  ldarg.0
        IL_000e:  ldarg.2
        IL_000f:  stfld      class assembly/R4 assembly/R4/'clo@4-2'::obj
        IL_0014:  ret
      } 

      .method public strict virtual instance int32 Invoke(class [FSharp.Core]Microsoft.FSharp.Core.Unit unitVar) cil managed
      {
        
        .maxstack  7
        .locals init (int32 V_0,
                 class [runtime]System.Collections.IComparer V_1,
                 int32 V_2,
                 int32 V_3,
                 float64 V_4,
                 float64 V_5)
        IL_0000:  ldarg.0
        IL_0001:  ldfld      class assembly/R4 assembly/R4/'clo@4-2'::obj
        IL_0006:  brfalse    IL_00b8

        IL_000b:  call       class [runtime]System.Collections.IComparer [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::get_GenericComparer()
        IL_0010:  stloc.1
        IL_0011:  ldarg.0
        IL_0012:  ldfld      class assembly/R4 assembly/R4/'clo@4-2'::this
        IL_0017:  ldfld      int32 assembly/R4::X@
        IL_001c:  stloc.2
        IL_001d:  ldarg.0
        IL_001e:  ldfld      class assembly/R4 assembly/R4/'clo@4-2'::obj
        IL_0023:  ldfld      int32 assembly/R4::X@
        IL_0028:  stloc.3
        IL_0029:  ldloc.2
        IL_002a:  ldloc.3
        IL_002b:  cgt
        IL_002d:  ldloc.2
        IL_002e:  ldloc.3
        IL_002f:  clt
        IL_0031:  sub
        IL_0032:  stloc.0
        IL_0033:  ldloc.0
        IL_0034:  ldc.i4.0
        IL_0035:  bge.s      IL_0039

        IL_0037:  ldloc.0
        IL_0038:  ret

        IL_0039:  ldloc.0
        IL_003a:  ldc.i4.0
        IL_003b:  ble.s      IL_003f

        IL_003d:  ldloc.0
        IL_003e:  ret

        IL_003f:  call       class [runtime]System.Collections.IComparer [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::get_GenericComparer()
        IL_0044:  stloc.1
        IL_0045:  ldarg.0
        IL_0046:  ldfld      class assembly/R4 assembly/R4/'clo@4-2'::this
        IL_004b:  ldfld      string assembly/R4::Y@
        IL_0050:  ldarg.0
        IL_0051:  ldfld      class assembly/R4 assembly/R4/'clo@4-2'::obj
        IL_0056:  ldfld      string assembly/R4::Y@
        IL_005b:  call       int32 [netstandard]System.String::CompareOrdinal(string,
                                                                              string)
        IL_0060:  stloc.2
        IL_0061:  ldloc.2
        IL_0062:  ldc.i4.0
        IL_0063:  bge.s      IL_0067

        IL_0065:  ldloc.2
        IL_0066:  ret

        IL_0067:  ldloc.2
        IL_0068:  ldc.i4.0
        IL_0069:  ble.s      IL_006d

        IL_006b:  ldloc.2
        IL_006c:  ret

        IL_006d:  call       class [runtime]System.Collections.IComparer [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::get_GenericComparer()
        IL_0072:  stloc.1
        IL_0073:  ldarg.0
        IL_0074:  ldfld      class assembly/R4 assembly/R4/'clo@4-2'::this
        IL_0079:  ldfld      float64 assembly/R4::D@
        IL_007e:  stloc.s    V_4
        IL_0080:  ldarg.0
        IL_0081:  ldfld      class assembly/R4 assembly/R4/'clo@4-2'::obj
        IL_0086:  ldfld      float64 assembly/R4::D@
        IL_008b:  stloc.s    V_5
        IL_008d:  ldloc.s    V_4
        IL_008f:  ldloc.s    V_5
        IL_0091:  clt
        IL_0093:  brfalse.s  IL_0097

        IL_0095:  ldc.i4.m1
        IL_0096:  ret

        IL_0097:  ldloc.s    V_4
        IL_0099:  ldloc.s    V_5
        IL_009b:  cgt
        IL_009d:  brfalse.s  IL_00a1

        IL_009f:  ldc.i4.1
        IL_00a0:  ret

        IL_00a1:  ldloc.s    V_4
        IL_00a3:  ldloc.s    V_5
        IL_00a5:  ceq
        IL_00a7:  brfalse.s  IL_00ab

        IL_00a9:  ldc.i4.0
        IL_00aa:  ret

        IL_00ab:  ldloc.1
        IL_00ac:  ldloc.s    V_4
        IL_00ae:  ldloc.s    V_5
        IL_00b0:  tail.
        IL_00b2:  call       int32 [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives/HashCompare::GenericComparisonWithComparerIntrinsic<float64>(class [runtime]System.Collections.IComparer,
                                                                                                                                                      !!0,
                                                                                                                                                      !!0)
        IL_00b7:  ret

        IL_00b8:  ldc.i4.1
        IL_00b9:  ret
      } 

    } 

    .class auto ansi serializable sealed nested assembly beforefieldinit 'clo@4-3'
           extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class [FSharp.Core]Microsoft.FSharp.Core.Unit,int32>
    {
      .field public class assembly/R4 this
      .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      .field public object obj
      .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      .field public class [runtime]System.Collections.IComparer comp
      .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      .field public class assembly/R4 objTemp
      .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      .method assembly specialname rtspecialname 
              instance void  .ctor(class assembly/R4 this,
                                   object obj,
                                   class [runtime]System.Collections.IComparer comp,
                                   class assembly/R4 objTemp) cil managed
      {
        .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
        .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
        
        .maxstack  8
        IL_0000:  ldarg.0
        IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class [FSharp.Core]Microsoft.FSharp.Core.Unit,int32>::.ctor()
        IL_0006:  ldarg.0
        IL_0007:  ldarg.1
        IL_0008:  stfld      class assembly/R4 assembly/R4/'clo@4-3'::this
        IL_000d:  ldarg.0
        IL_000e:  ldarg.2
        IL_000f:  stfld      object assembly/R4/'clo@4-3'::obj
        IL_0014:  ldarg.0
        IL_0015:  ldarg.3
        IL_0016:  stfld      class [runtime]System.Collections.IComparer assembly/R4/'clo@4-3'::comp
        IL_001b:  ldarg.0
        IL_001c:  ldarg.s    objTemp
        IL_001e:  stfld      class assembly/R4 assembly/R4/'clo@4-3'::objTemp
        IL_0023:  ret
      } 

      .method public strict virtual instance int32 Invoke(class [FSharp.Core]Microsoft.FSharp.Core.Unit unitVar) cil managed
      {
        
        .maxstack  7
        .locals init (int32 V_0,
                 int32 V_1,
                 int32 V_2,
                 float64 V_3,
                 float64 V_4)
        IL_0000:  ldarg.0
        IL_0001:  ldfld      object assembly/R4/'clo@4-3'::obj
        IL_0006:  unbox.any  assembly/R4
        IL_000b:  brfalse    IL_00ab

        IL_0010:  ldarg.0
        IL_0011:  ldfld      class assembly/R4 assembly/R4/'clo@4-3'::this
        IL_0016:  ldfld      int32 assembly/R4::X@
        IL_001b:  stloc.1
        IL_001c:  ldarg.0
        IL_001d:  ldfld      class assembly/R4 assembly/R4/'clo@4-3'::objTemp
        IL_0022:  ldfld      int32 assembly/R4::X@
        IL_0027:  stloc.2
        IL_0028:  ldloc.1
        IL_0029:  ldloc.2
        IL_002a:  cgt
        IL_002c:  ldloc.1
        IL_002d:  ldloc.2
        IL_002e:  clt
        IL_0030:  sub
        IL_0031:  stloc.0
        IL_0032:  ldloc.0
        IL_0033:  ldc.i4.0
        IL_0034:  bge.s      IL_0038

        IL_0036:  ldloc.0
        IL_0037:  ret

        IL_0038:  ldloc.0
        IL_0039:  ldc.i4.0
        IL_003a:  ble.s      IL_003e

        IL_003c:  ldloc.0
        IL_003d:  ret

        IL_003e:  ldarg.0
        IL_003f:  ldfld      class assembly/R4 assembly/R4/'clo@4-3'::this
        IL_0044:  ldfld      string assembly/R4::Y@
        IL_0049:  ldarg.0
        IL_004a:  ldfld      class assembly/R4 assembly/R4/'clo@4-3'::objTemp
        IL_004f:  ldfld      string assembly/R4::Y@
        IL_0054:  call       int32 [netstandard]System.String::CompareOrdinal(string,
                                                                              string)
        IL_0059:  stloc.1
        IL_005a:  ldloc.1
        IL_005b:  ldc.i4.0
        IL_005c:  bge.s      IL_0060

        IL_005e:  ldloc.1
        IL_005f:  ret

        IL_0060:  ldloc.1
        IL_0061:  ldc.i4.0
        IL_0062:  ble.s      IL_0066

        IL_0064:  ldloc.1
        IL_0065:  ret

        IL_0066:  ldarg.0
        IL_0067:  ldfld      class assembly/R4 assembly/R4/'clo@4-3'::this
        IL_006c:  ldfld      float64 assembly/R4::D@
        IL_0071:  stloc.3
        IL_0072:  ldarg.0
        IL_0073:  ldfld      class assembly/R4 assembly/R4/'clo@4-3'::objTemp
        IL_0078:  ldfld      float64 assembly/R4::D@
        IL_007d:  stloc.s    V_4
        IL_007f:  ldloc.3
        IL_0080:  ldloc.s    V_4
        IL_0082:  clt
        IL_0084:  brfalse.s  IL_0088

        IL_0086:  ldc.i4.m1
        IL_0087:  ret

        IL_0088:  ldloc.3
        IL_0089:  ldloc.s    V_4
        IL_008b:  cgt
        IL_008d:  brfalse.s  IL_0091

        IL_008f:  ldc.i4.1
        IL_0090:  ret

        IL_0091:  ldloc.3
        IL_0092:  ldloc.s    V_4
        IL_0094:  ceq
        IL_0096:  brfalse.s  IL_009a

        IL_0098:  ldc.i4.0
        IL_0099:  ret

        IL_009a:  ldarg.0
        IL_009b:  ldfld      class [runtime]System.Collections.IComparer assembly/R4/'clo@4-3'::comp
        IL_00a0:  ldloc.3
        IL_00a1:  ldloc.s    V_4
        IL_00a3:  tail.
        IL_00a5:  call       int32 [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives/HashCompare::GenericComparisonWithComparerIntrinsic<float64>(class [runtime]System.Collections.IComparer,
                                                                                                                                                      !!0,
                                                                                                                                                      !!0)
        IL_00aa:  ret

        IL_00ab:  ldc.i4.1
        IL_00ac:  ret
      } 

    } 

    .field assembly int32 X@
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
    .field assembly string Y@
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
    .field assembly float64 D@
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
    .method public hidebysig specialname instance int32  get_X() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  ldfld      int32 assembly/R4::X@
      IL_0006:  ret
    } 

    .method public hidebysig specialname instance string  get_Y() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  ldfld      string assembly/R4::Y@
      IL_0006:  ret
    } 

    .method public hidebysig specialname instance float64  get_D() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  ldfld      float64 assembly/R4::D@
      IL_0006:  ret
    } 

    .method public specialname rtspecialname 
            instance void  .ctor(int32 x,
                                 string y,
                                 float64 d) cil managed
    {
      .custom instance void [runtime]System.Diagnostics.CodeAnalysis.DynamicDependencyAttribute::.ctor(valuetype [runtime]System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes,
                                                                                                              class [runtime]System.Type) = ( 01 00 60 06 00 00 0E 54 79 70 65 53 70 72 65 61   
                                                                                                                                                     64 73 2B 52 34 00 00 )                            
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  call       instance void [runtime]System.Object::.ctor()
      IL_0006:  ldarg.0
      IL_0007:  ldarg.1
      IL_0008:  stfld      int32 assembly/R4::X@
      IL_000d:  ldarg.0
      IL_000e:  ldarg.2
      IL_000f:  stfld      string assembly/R4::Y@
      IL_0014:  ldarg.0
      IL_0015:  ldarg.3
      IL_0016:  stfld      float64 assembly/R4::D@
      IL_001b:  ret
    } 

    .method public strict virtual instance string ToString() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldstr      "%+A"
      IL_0005:  newobj     instance void class [FSharp.Core]Microsoft.FSharp.Core.PrintfFormat`5<class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class assembly/R4,string>,class [FSharp.Core]Microsoft.FSharp.Core.Unit,string,string,class assembly/R4>::.ctor(string)
      IL_000a:  call       !!0 [FSharp.Core]Microsoft.FSharp.Core.ExtraTopLevelOperators::PrintFormatToString<class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class assembly/R4,string>>(class [FSharp.Core]Microsoft.FSharp.Core.PrintfFormat`4<!!0,class [FSharp.Core]Microsoft.FSharp.Core.Unit,string,string>)
      IL_000f:  ldarg.0
      IL_0010:  callvirt   instance !1 class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class assembly/R4,string>::Invoke(!0)
      IL_0015:  ret
    } 

    .method public hidebysig virtual final instance int32  CompareTo(class assembly/R4 obj) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  4
      .locals init (class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class [FSharp.Core]Microsoft.FSharp.Core.Unit,int32> V_0)
      IL_0000:  ldarg.0
      IL_0001:  brfalse.s  IL_0015

      IL_0003:  ldarg.0
      IL_0004:  ldarg.1
      IL_0005:  newobj     instance void assembly/R4/'clo@4-2'::.ctor(class assembly/R4,
                                                                         class assembly/R4)
      IL_000a:  stloc.0
      IL_000b:  ldloc.0
      IL_000c:  ldnull
      IL_000d:  tail.
      IL_000f:  callvirt   instance !1 class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class [FSharp.Core]Microsoft.FSharp.Core.Unit,int32>::Invoke(!0)
      IL_0014:  ret

      IL_0015:  ldarg.1
      IL_0016:  brfalse.s  IL_001a

      IL_0018:  ldc.i4.m1
      IL_0019:  ret

      IL_001a:  ldc.i4.0
      IL_001b:  ret
    } 

    .method public hidebysig virtual final instance int32  CompareTo(object obj) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  ldarg.1
      IL_0002:  unbox.any  assembly/R4
      IL_0007:  tail.
      IL_0009:  callvirt   instance int32 assembly/R4::CompareTo(class assembly/R4)
      IL_000e:  ret
    } 

    .method public hidebysig virtual final instance int32  CompareTo(object obj, class [runtime]System.Collections.IComparer comp) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  6
      .locals init (class assembly/R4 V_0,
               class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class [FSharp.Core]Microsoft.FSharp.Core.Unit,int32> V_1)
      IL_0000:  ldarg.1
      IL_0001:  unbox.any  assembly/R4
      IL_0006:  stloc.0
      IL_0007:  ldarg.0
      IL_0008:  brfalse.s  IL_001e

      IL_000a:  ldarg.0
      IL_000b:  ldarg.1
      IL_000c:  ldarg.2
      IL_000d:  ldloc.0
      IL_000e:  newobj     instance void assembly/R4/'clo@4-3'::.ctor(class assembly/R4,
                                                                         object,
                                                                         class [runtime]System.Collections.IComparer,
                                                                         class assembly/R4)
      IL_0013:  stloc.1
      IL_0014:  ldloc.1
      IL_0015:  ldnull
      IL_0016:  tail.
      IL_0018:  callvirt   instance !1 class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class [FSharp.Core]Microsoft.FSharp.Core.Unit,int32>::Invoke(!0)
      IL_001d:  ret

      IL_001e:  ldarg.1
      IL_001f:  unbox.any  assembly/R4
      IL_0024:  brfalse.s  IL_0028

      IL_0026:  ldc.i4.m1
      IL_0027:  ret

      IL_0028:  ldc.i4.0
      IL_0029:  ret
    } 

    .method public hidebysig virtual final instance int32  GetHashCode(class [runtime]System.Collections.IEqualityComparer comp) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  7
      .locals init (int32 V_0,
               string V_1)
      IL_0000:  ldarg.0
      IL_0001:  brfalse.s  IL_005b

      IL_0003:  ldc.i4.0
      IL_0004:  stloc.0
      IL_0005:  ldc.i4     0x9e3779b9
      IL_000a:  ldarg.1
      IL_000b:  ldarg.0
      IL_000c:  ldfld      float64 assembly/R4::D@
      IL_0011:  call       int32 [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives/HashCompare::GenericHashWithComparerIntrinsic<float64>(class [runtime]System.Collections.IEqualityComparer,
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
      IL_0025:  ldarg.0
      IL_0026:  ldfld      string assembly/R4::Y@
      IL_002b:  stloc.1
      IL_002c:  ldloc.1
      IL_002d:  brtrue.s   IL_0033

      IL_002f:  ldc.i4.0
      IL_0030:  nop
      IL_0031:  br.s       IL_003a

      IL_0033:  ldloc.1
      IL_0034:  callvirt   instance int32 [netstandard]System.Object::GetHashCode()
      IL_0039:  nop
      IL_003a:  ldloc.0
      IL_003b:  ldc.i4.6
      IL_003c:  shl
      IL_003d:  ldloc.0
      IL_003e:  ldc.i4.2
      IL_003f:  shr
      IL_0040:  add
      IL_0041:  add
      IL_0042:  add
      IL_0043:  stloc.0
      IL_0044:  ldc.i4     0x9e3779b9
      IL_0049:  ldarg.0
      IL_004a:  ldfld      int32 assembly/R4::X@
      IL_004f:  ldloc.0
      IL_0050:  ldc.i4.6
      IL_0051:  shl
      IL_0052:  ldloc.0
      IL_0053:  ldc.i4.2
      IL_0054:  shr
      IL_0055:  add
      IL_0056:  add
      IL_0057:  add
      IL_0058:  stloc.0
      IL_0059:  ldloc.0
      IL_005a:  ret

      IL_005b:  ldc.i4.0
      IL_005c:  ret
    } 

    .method public hidebysig virtual final instance int32  GetHashCode() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  call       class [runtime]System.Collections.IEqualityComparer [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::get_GenericEqualityComparer()
      IL_0006:  callvirt   instance int32 assembly/R4::GetHashCode(class [runtime]System.Collections.IEqualityComparer)
      IL_000b:  ret
    } 

    .method public hidebysig instance bool Equals(class assembly/R4 obj, class [runtime]System.Collections.IEqualityComparer comp) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  4
      IL_0000:  ldarg.0
      IL_0001:  brfalse.s  IL_003c

      IL_0003:  ldarg.1
      IL_0004:  brfalse.s  IL_003a

      IL_0006:  ldarg.0
      IL_0007:  ldfld      int32 assembly/R4::X@
      IL_000c:  ldarg.1
      IL_000d:  ldfld      int32 assembly/R4::X@
      IL_0012:  bne.un.s   IL_0038

      IL_0014:  ldarg.0
      IL_0015:  ldfld      string assembly/R4::Y@
      IL_001a:  ldarg.1
      IL_001b:  ldfld      string assembly/R4::Y@
      IL_0020:  call       bool [netstandard]System.String::Equals(string,
                                                                   string)
      IL_0025:  brfalse.s  IL_0036

      IL_0027:  ldarg.0
      IL_0028:  ldfld      float64 assembly/R4::D@
      IL_002d:  ldarg.1
      IL_002e:  ldfld      float64 assembly/R4::D@
      IL_0033:  ceq
      IL_0035:  ret

      IL_0036:  ldc.i4.0
      IL_0037:  ret

      IL_0038:  ldc.i4.0
      IL_0039:  ret

      IL_003a:  ldc.i4.0
      IL_003b:  ret

      IL_003c:  ldarg.1
      IL_003d:  ldnull
      IL_003e:  cgt.un
      IL_0040:  ldc.i4.0
      IL_0041:  ceq
      IL_0043:  ret
    } 

    .method public hidebysig virtual final instance bool  Equals(object obj, class [runtime]System.Collections.IEqualityComparer comp) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  5
      .locals init (class assembly/R4 V_0)
      IL_0000:  ldarg.1
      IL_0001:  isinst     assembly/R4
      IL_0006:  stloc.0
      IL_0007:  ldloc.0
      IL_0008:  brfalse.s  IL_0013

      IL_000a:  ldarg.0
      IL_000b:  ldloc.0
      IL_000c:  ldarg.2
      IL_000d:  callvirt   instance bool assembly/R4::Equals(class assembly/R4,
                                                                class [runtime]System.Collections.IEqualityComparer)
      IL_0012:  ret

      IL_0013:  ldc.i4.0
      IL_0014:  ret
    } 

    .method public hidebysig virtual final instance bool  Equals(class assembly/R4 obj) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  4
      .locals init (float64 V_0,
               float64 V_1)
      IL_0000:  ldarg.0
      IL_0001:  brfalse.s  IL_0051

      IL_0003:  ldarg.1
      IL_0004:  brfalse.s  IL_004f

      IL_0006:  ldarg.0
      IL_0007:  ldfld      int32 assembly/R4::X@
      IL_000c:  ldarg.1
      IL_000d:  ldfld      int32 assembly/R4::X@
      IL_0012:  bne.un.s   IL_004d

      IL_0014:  ldarg.0
      IL_0015:  ldfld      string assembly/R4::Y@
      IL_001a:  ldarg.1
      IL_001b:  ldfld      string assembly/R4::Y@
      IL_0020:  call       bool [netstandard]System.String::Equals(string,
                                                                   string)
      IL_0025:  brfalse.s  IL_004b

      IL_0027:  ldarg.0
      IL_0028:  ldfld      float64 assembly/R4::D@
      IL_002d:  stloc.0
      IL_002e:  ldarg.1
      IL_002f:  ldfld      float64 assembly/R4::D@
      IL_0034:  stloc.1
      IL_0035:  ldloc.0
      IL_0036:  ldloc.1
      IL_0037:  ceq
      IL_0039:  brfalse.s  IL_003d

      IL_003b:  ldc.i4.1
      IL_003c:  ret

      IL_003d:  ldloc.0
      IL_003e:  ldloc.0
      IL_003f:  beq.s      IL_0049

      IL_0041:  ldloc.1
      IL_0042:  ldloc.1
      IL_0043:  ceq
      IL_0045:  ldc.i4.0
      IL_0046:  ceq
      IL_0048:  ret

      IL_0049:  ldc.i4.0
      IL_004a:  ret

      IL_004b:  ldc.i4.0
      IL_004c:  ret

      IL_004d:  ldc.i4.0
      IL_004e:  ret

      IL_004f:  ldc.i4.0
      IL_0050:  ret

      IL_0051:  ldarg.1
      IL_0052:  ldnull
      IL_0053:  cgt.un
      IL_0055:  ldc.i4.0
      IL_0056:  ceq
      IL_0058:  ret
    } 

    .method public hidebysig virtual final instance bool  Equals(object obj) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  4
      .locals init (class assembly/R4 V_0)
      IL_0000:  ldarg.1
      IL_0001:  isinst     assembly/R4
      IL_0006:  stloc.0
      IL_0007:  ldloc.0
      IL_0008:  brfalse.s  IL_0012

      IL_000a:  ldarg.0
      IL_000b:  ldloc.0
      IL_000c:  callvirt   instance bool assembly/R4::Equals(class assembly/R4)
      IL_0011:  ret

      IL_0012:  ldc.i4.0
      IL_0013:  ret
    } 

    .property instance int32 X()
    {
      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags,
                                                                                                  int32) = ( 01 00 04 00 00 00 00 00 00 00 00 00 ) 
      .get instance int32 assembly/R4::get_X()
    } 
    .property instance string Y()
    {
      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags,
                                                                                                  int32) = ( 01 00 04 00 00 00 01 00 00 00 00 00 ) 
      .get instance string assembly/R4::get_Y()
    } 
    .property instance float64 D()
    {
      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags,
                                                                                                  int32) = ( 01 00 04 00 00 00 02 00 00 00 00 00 ) 
      .get instance float64 assembly/R4::get_D()
    } 
  } 

  .class auto ansi serializable sealed nested public R5
         extends [runtime]System.Object
         implements class [runtime]System.IEquatable`1<class assembly/R5>,
                    [runtime]System.Collections.IStructuralEquatable,
                    class [runtime]System.IComparable`1<class assembly/R5>,
                    [runtime]System.IComparable,
                    [runtime]System.Collections.IStructuralComparable
  {
    .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags) = ( 01 00 02 00 00 00 00 00 ) 
    .class auto ansi serializable sealed nested assembly beforefieldinit 'clo@5-4'
           extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class [FSharp.Core]Microsoft.FSharp.Core.Unit,int32>
    {
      .field public class assembly/R5 this
      .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      .field public class assembly/R5 obj
      .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      .field public int32 n
      .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      .method assembly specialname rtspecialname 
              instance void  .ctor(class assembly/R5 this,
                                   class assembly/R5 obj,
                                   int32 n) cil managed
      {
        .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
        .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
        
        .maxstack  8
        IL_0000:  ldarg.0
        IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class [FSharp.Core]Microsoft.FSharp.Core.Unit,int32>::.ctor()
        IL_0006:  ldarg.0
        IL_0007:  ldarg.1
        IL_0008:  stfld      class assembly/R5 assembly/R5/'clo@5-4'::this
        IL_000d:  ldarg.0
        IL_000e:  ldarg.2
        IL_000f:  stfld      class assembly/R5 assembly/R5/'clo@5-4'::obj
        IL_0014:  ldarg.0
        IL_0015:  ldarg.3
        IL_0016:  stfld      int32 assembly/R5/'clo@5-4'::n
        IL_001b:  ret
      } 

      .method public strict virtual instance int32 Invoke(class [FSharp.Core]Microsoft.FSharp.Core.Unit unitVar) cil managed
      {
        
        .maxstack  7
        .locals init (int32 V_0,
                 class [runtime]System.Collections.IComparer V_1,
                 int32 V_2,
                 int32 V_3,
                 float64 V_4,
                 float64 V_5)
        IL_0000:  ldarg.0
        IL_0001:  ldfld      int32 assembly/R5/'clo@5-4'::n
        IL_0006:  ldc.i4.0
        IL_0007:  ble.s      IL_0010

        IL_0009:  ldarg.0
        IL_000a:  ldfld      int32 assembly/R5/'clo@5-4'::n
        IL_000f:  ret

        IL_0010:  call       class [runtime]System.Collections.IComparer [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::get_GenericComparer()
        IL_0015:  stloc.1
        IL_0016:  ldarg.0
        IL_0017:  ldfld      class assembly/R5 assembly/R5/'clo@5-4'::this
        IL_001c:  ldfld      int32 assembly/R5::X@
        IL_0021:  stloc.2
        IL_0022:  ldarg.0
        IL_0023:  ldfld      class assembly/R5 assembly/R5/'clo@5-4'::obj
        IL_0028:  ldfld      int32 assembly/R5::X@
        IL_002d:  stloc.3
        IL_002e:  ldloc.2
        IL_002f:  ldloc.3
        IL_0030:  cgt
        IL_0032:  ldloc.2
        IL_0033:  ldloc.3
        IL_0034:  clt
        IL_0036:  sub
        IL_0037:  stloc.0
        IL_0038:  ldloc.0
        IL_0039:  ldc.i4.0
        IL_003a:  bge.s      IL_003e

        IL_003c:  ldloc.0
        IL_003d:  ret

        IL_003e:  ldloc.0
        IL_003f:  ldc.i4.0
        IL_0040:  ble.s      IL_0044

        IL_0042:  ldloc.0
        IL_0043:  ret

        IL_0044:  call       class [runtime]System.Collections.IComparer [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::get_GenericComparer()
        IL_0049:  stloc.1
        IL_004a:  ldarg.0
        IL_004b:  ldfld      class assembly/R5 assembly/R5/'clo@5-4'::this
        IL_0050:  ldfld      string assembly/R5::Y@
        IL_0055:  ldarg.0
        IL_0056:  ldfld      class assembly/R5 assembly/R5/'clo@5-4'::obj
        IL_005b:  ldfld      string assembly/R5::Y@
        IL_0060:  call       int32 [netstandard]System.String::CompareOrdinal(string,
                                                                              string)
        IL_0065:  stloc.2
        IL_0066:  ldloc.2
        IL_0067:  ldc.i4.0
        IL_0068:  bge.s      IL_006c

        IL_006a:  ldloc.2
        IL_006b:  ret

        IL_006c:  ldloc.2
        IL_006d:  ldc.i4.0
        IL_006e:  ble.s      IL_0072

        IL_0070:  ldloc.2
        IL_0071:  ret

        IL_0072:  call       class [runtime]System.Collections.IComparer [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::get_GenericComparer()
        IL_0077:  stloc.1
        IL_0078:  ldarg.0
        IL_0079:  ldfld      class assembly/R5 assembly/R5/'clo@5-4'::this
        IL_007e:  ldfld      float64 assembly/R5::E@
        IL_0083:  stloc.s    V_4
        IL_0085:  ldarg.0
        IL_0086:  ldfld      class assembly/R5 assembly/R5/'clo@5-4'::obj
        IL_008b:  ldfld      float64 assembly/R5::E@
        IL_0090:  stloc.s    V_5
        IL_0092:  ldloc.s    V_4
        IL_0094:  ldloc.s    V_5
        IL_0096:  clt
        IL_0098:  brfalse.s  IL_009c

        IL_009a:  ldc.i4.m1
        IL_009b:  ret

        IL_009c:  ldloc.s    V_4
        IL_009e:  ldloc.s    V_5
        IL_00a0:  cgt
        IL_00a2:  brfalse.s  IL_00a6

        IL_00a4:  ldc.i4.1
        IL_00a5:  ret

        IL_00a6:  ldloc.s    V_4
        IL_00a8:  ldloc.s    V_5
        IL_00aa:  ceq
        IL_00ac:  brfalse.s  IL_00b0

        IL_00ae:  ldc.i4.0
        IL_00af:  ret

        IL_00b0:  ldloc.1
        IL_00b1:  ldloc.s    V_4
        IL_00b3:  ldloc.s    V_5
        IL_00b5:  tail.
        IL_00b7:  call       int32 [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives/HashCompare::GenericComparisonWithComparerIntrinsic<float64>(class [runtime]System.Collections.IComparer,
                                                                                                                                                      !!0,
                                                                                                                                                      !!0)
        IL_00bc:  ret
      } 

    } 

    .class auto ansi serializable sealed nested assembly beforefieldinit 'clo@5-5'
           extends class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class [FSharp.Core]Microsoft.FSharp.Core.Unit,int32>
    {
      .field public class assembly/R5 this
      .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      .field public class [runtime]System.Collections.IComparer comp
      .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      .field public class assembly/R5 objTemp
      .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      .field public int32 n
      .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      .method assembly specialname rtspecialname 
              instance void  .ctor(class assembly/R5 this,
                                   class [runtime]System.Collections.IComparer comp,
                                   class assembly/R5 objTemp,
                                   int32 n) cil managed
      {
        .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
        .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
        
        .maxstack  8
        IL_0000:  ldarg.0
        IL_0001:  call       instance void class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class [FSharp.Core]Microsoft.FSharp.Core.Unit,int32>::.ctor()
        IL_0006:  ldarg.0
        IL_0007:  ldarg.1
        IL_0008:  stfld      class assembly/R5 assembly/R5/'clo@5-5'::this
        IL_000d:  ldarg.0
        IL_000e:  ldarg.2
        IL_000f:  stfld      class [runtime]System.Collections.IComparer assembly/R5/'clo@5-5'::comp
        IL_0014:  ldarg.0
        IL_0015:  ldarg.3
        IL_0016:  stfld      class assembly/R5 assembly/R5/'clo@5-5'::objTemp
        IL_001b:  ldarg.0
        IL_001c:  ldarg.s    n
        IL_001e:  stfld      int32 assembly/R5/'clo@5-5'::n
        IL_0023:  ret
      } 

      .method public strict virtual instance int32 Invoke(class [FSharp.Core]Microsoft.FSharp.Core.Unit unitVar) cil managed
      {
        
        .maxstack  7
        .locals init (int32 V_0,
                 int32 V_1,
                 int32 V_2,
                 float64 V_3,
                 float64 V_4)
        IL_0000:  ldarg.0
        IL_0001:  ldfld      int32 assembly/R5/'clo@5-5'::n
        IL_0006:  ldc.i4.0
        IL_0007:  ble.s      IL_0010

        IL_0009:  ldarg.0
        IL_000a:  ldfld      int32 assembly/R5/'clo@5-5'::n
        IL_000f:  ret

        IL_0010:  ldarg.0
        IL_0011:  ldfld      class assembly/R5 assembly/R5/'clo@5-5'::this
        IL_0016:  ldfld      int32 assembly/R5::X@
        IL_001b:  stloc.1
        IL_001c:  ldarg.0
        IL_001d:  ldfld      class assembly/R5 assembly/R5/'clo@5-5'::objTemp
        IL_0022:  ldfld      int32 assembly/R5::X@
        IL_0027:  stloc.2
        IL_0028:  ldloc.1
        IL_0029:  ldloc.2
        IL_002a:  cgt
        IL_002c:  ldloc.1
        IL_002d:  ldloc.2
        IL_002e:  clt
        IL_0030:  sub
        IL_0031:  stloc.0
        IL_0032:  ldloc.0
        IL_0033:  ldc.i4.0
        IL_0034:  bge.s      IL_0038

        IL_0036:  ldloc.0
        IL_0037:  ret

        IL_0038:  ldloc.0
        IL_0039:  ldc.i4.0
        IL_003a:  ble.s      IL_003e

        IL_003c:  ldloc.0
        IL_003d:  ret

        IL_003e:  ldarg.0
        IL_003f:  ldfld      class assembly/R5 assembly/R5/'clo@5-5'::this
        IL_0044:  ldfld      string assembly/R5::Y@
        IL_0049:  ldarg.0
        IL_004a:  ldfld      class assembly/R5 assembly/R5/'clo@5-5'::objTemp
        IL_004f:  ldfld      string assembly/R5::Y@
        IL_0054:  call       int32 [netstandard]System.String::CompareOrdinal(string,
                                                                              string)
        IL_0059:  stloc.1
        IL_005a:  ldloc.1
        IL_005b:  ldc.i4.0
        IL_005c:  bge.s      IL_0060

        IL_005e:  ldloc.1
        IL_005f:  ret

        IL_0060:  ldloc.1
        IL_0061:  ldc.i4.0
        IL_0062:  ble.s      IL_0066

        IL_0064:  ldloc.1
        IL_0065:  ret

        IL_0066:  ldarg.0
        IL_0067:  ldfld      class assembly/R5 assembly/R5/'clo@5-5'::this
        IL_006c:  ldfld      float64 assembly/R5::E@
        IL_0071:  stloc.3
        IL_0072:  ldarg.0
        IL_0073:  ldfld      class assembly/R5 assembly/R5/'clo@5-5'::objTemp
        IL_0078:  ldfld      float64 assembly/R5::E@
        IL_007d:  stloc.s    V_4
        IL_007f:  ldloc.3
        IL_0080:  ldloc.s    V_4
        IL_0082:  clt
        IL_0084:  brfalse.s  IL_0088

        IL_0086:  ldc.i4.m1
        IL_0087:  ret

        IL_0088:  ldloc.3
        IL_0089:  ldloc.s    V_4
        IL_008b:  cgt
        IL_008d:  brfalse.s  IL_0091

        IL_008f:  ldc.i4.1
        IL_0090:  ret

        IL_0091:  ldloc.3
        IL_0092:  ldloc.s    V_4
        IL_0094:  ceq
        IL_0096:  brfalse.s  IL_009a

        IL_0098:  ldc.i4.0
        IL_0099:  ret

        IL_009a:  ldarg.0
        IL_009b:  ldfld      class [runtime]System.Collections.IComparer assembly/R5/'clo@5-5'::comp
        IL_00a0:  ldloc.3
        IL_00a1:  ldloc.s    V_4
        IL_00a3:  tail.
        IL_00a5:  call       int32 [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives/HashCompare::GenericComparisonWithComparerIntrinsic<float64>(class [runtime]System.Collections.IComparer,
                                                                                                                                                      !!0,
                                                                                                                                                      !!0)
        IL_00aa:  ret
      } 

    } 

    .field assembly int32 A@
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
    .field assembly string B@
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
    .field assembly int32 X@
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
    .field assembly string Y@
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
    .field assembly float64 E@
    .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    .custom instance void [runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [runtime]System.Diagnostics.DebuggerBrowsableState) = ( 01 00 00 00 00 00 00 00 ) 
    .method public hidebysig specialname instance int32  get_A() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  ldfld      int32 assembly/R5::A@
      IL_0006:  ret
    } 

    .method public hidebysig specialname instance string  get_B() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  ldfld      string assembly/R5::B@
      IL_0006:  ret
    } 

    .method public hidebysig specialname instance int32  get_X() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  ldfld      int32 assembly/R5::X@
      IL_0006:  ret
    } 

    .method public hidebysig specialname instance string  get_Y() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  ldfld      string assembly/R5::Y@
      IL_0006:  ret
    } 

    .method public hidebysig specialname instance float64  get_E() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      .custom instance void [runtime]System.Diagnostics.DebuggerNonUserCodeAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  ldfld      float64 assembly/R5::E@
      IL_0006:  ret
    } 

    .method public specialname rtspecialname 
            instance void  .ctor(int32 a,
                                 string b,
                                 int32 x,
                                 string y,
                                 float64 e) cil managed
    {
      .custom instance void [runtime]System.Diagnostics.CodeAnalysis.DynamicDependencyAttribute::.ctor(valuetype [runtime]System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes,
                                                                                                              class [runtime]System.Type) = ( 01 00 60 06 00 00 0E 54 79 70 65 53 70 72 65 61   
                                                                                                                                                     64 73 2B 52 35 00 00 )                            
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  call       instance void [runtime]System.Object::.ctor()
      IL_0006:  ldarg.0
      IL_0007:  ldarg.1
      IL_0008:  stfld      int32 assembly/R5::A@
      IL_000d:  ldarg.0
      IL_000e:  ldarg.2
      IL_000f:  stfld      string assembly/R5::B@
      IL_0014:  ldarg.0
      IL_0015:  ldarg.3
      IL_0016:  stfld      int32 assembly/R5::X@
      IL_001b:  ldarg.0
      IL_001c:  ldarg.s    y
      IL_001e:  stfld      string assembly/R5::Y@
      IL_0023:  ldarg.0
      IL_0024:  ldarg.s    e
      IL_0026:  stfld      float64 assembly/R5::E@
      IL_002b:  ret
    } 

    .method public strict virtual instance string ToString() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldstr      "%+A"
      IL_0005:  newobj     instance void class [FSharp.Core]Microsoft.FSharp.Core.PrintfFormat`5<class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class assembly/R5,string>,class [FSharp.Core]Microsoft.FSharp.Core.Unit,string,string,class assembly/R5>::.ctor(string)
      IL_000a:  call       !!0 [FSharp.Core]Microsoft.FSharp.Core.ExtraTopLevelOperators::PrintFormatToString<class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class assembly/R5,string>>(class [FSharp.Core]Microsoft.FSharp.Core.PrintfFormat`4<!!0,class [FSharp.Core]Microsoft.FSharp.Core.Unit,string,string>)
      IL_000f:  ldarg.0
      IL_0010:  callvirt   instance !1 class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class assembly/R5,string>::Invoke(!0)
      IL_0015:  ret
    } 

    .method public hidebysig virtual final instance int32  CompareTo(class assembly/R5 obj) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  5
      .locals init (int32 V_0,
               class [runtime]System.Collections.IComparer V_1,
               int32 V_2,
               int32 V_3,
               class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class [FSharp.Core]Microsoft.FSharp.Core.Unit,int32> V_4)
      IL_0000:  ldarg.0
      IL_0001:  brfalse.s  IL_0065

      IL_0003:  ldarg.1
      IL_0004:  brfalse.s  IL_0063

      IL_0006:  call       class [runtime]System.Collections.IComparer [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::get_GenericComparer()
      IL_000b:  stloc.1
      IL_000c:  ldarg.0
      IL_000d:  ldfld      int32 assembly/R5::A@
      IL_0012:  stloc.2
      IL_0013:  ldarg.1
      IL_0014:  ldfld      int32 assembly/R5::A@
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
      IL_0037:  ldfld      string assembly/R5::B@
      IL_003c:  ldarg.1
      IL_003d:  ldfld      string assembly/R5::B@
      IL_0042:  call       int32 [netstandard]System.String::CompareOrdinal(string,
                                                                            string)
      IL_0047:  stloc.2
      IL_0048:  ldloc.2
      IL_0049:  ldc.i4.0
      IL_004a:  bge.s      IL_004e

      IL_004c:  ldloc.2
      IL_004d:  ret

      IL_004e:  ldarg.0
      IL_004f:  ldarg.1
      IL_0050:  ldloc.2
      IL_0051:  newobj     instance void assembly/R5/'clo@5-4'::.ctor(class assembly/R5,
                                                                         class assembly/R5,
                                                                         int32)
      IL_0056:  stloc.s    V_4
      IL_0058:  ldloc.s    V_4
      IL_005a:  ldnull
      IL_005b:  tail.
      IL_005d:  callvirt   instance !1 class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class [FSharp.Core]Microsoft.FSharp.Core.Unit,int32>::Invoke(!0)
      IL_0062:  ret

      IL_0063:  ldc.i4.1
      IL_0064:  ret

      IL_0065:  ldarg.1
      IL_0066:  brfalse.s  IL_006a

      IL_0068:  ldc.i4.m1
      IL_0069:  ret

      IL_006a:  ldc.i4.0
      IL_006b:  ret
    } 

    .method public hidebysig virtual final instance int32  CompareTo(object obj) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  ldarg.1
      IL_0002:  unbox.any  assembly/R5
      IL_0007:  tail.
      IL_0009:  callvirt   instance int32 assembly/R5::CompareTo(class assembly/R5)
      IL_000e:  ret
    } 

    .method public hidebysig virtual final instance int32  CompareTo(object obj, class [runtime]System.Collections.IComparer comp) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  6
      .locals init (class assembly/R5 V_0,
               int32 V_1,
               int32 V_2,
               int32 V_3,
               class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class [FSharp.Core]Microsoft.FSharp.Core.Unit,int32> V_4)
      IL_0000:  ldarg.1
      IL_0001:  unbox.any  assembly/R5
      IL_0006:  stloc.0
      IL_0007:  ldarg.0
      IL_0008:  brfalse.s  IL_0066

      IL_000a:  ldarg.1
      IL_000b:  unbox.any  assembly/R5
      IL_0010:  brfalse.s  IL_0064

      IL_0012:  ldarg.0
      IL_0013:  ldfld      int32 assembly/R5::A@
      IL_0018:  stloc.2
      IL_0019:  ldloc.0
      IL_001a:  ldfld      int32 assembly/R5::A@
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
      IL_0037:  ldfld      string assembly/R5::B@
      IL_003c:  ldloc.0
      IL_003d:  ldfld      string assembly/R5::B@
      IL_0042:  call       int32 [netstandard]System.String::CompareOrdinal(string,
                                                                            string)
      IL_0047:  stloc.2
      IL_0048:  ldloc.2
      IL_0049:  ldc.i4.0
      IL_004a:  bge.s      IL_004e

      IL_004c:  ldloc.2
      IL_004d:  ret

      IL_004e:  ldarg.0
      IL_004f:  ldarg.2
      IL_0050:  ldloc.0
      IL_0051:  ldloc.2
      IL_0052:  newobj     instance void assembly/R5/'clo@5-5'::.ctor(class assembly/R5,
                                                                         class [runtime]System.Collections.IComparer,
                                                                         class assembly/R5,
                                                                         int32)
      IL_0057:  stloc.s    V_4
      IL_0059:  ldloc.s    V_4
      IL_005b:  ldnull
      IL_005c:  tail.
      IL_005e:  callvirt   instance !1 class [FSharp.Core]Microsoft.FSharp.Core.FSharpFunc`2<class [FSharp.Core]Microsoft.FSharp.Core.Unit,int32>::Invoke(!0)
      IL_0063:  ret

      IL_0064:  ldc.i4.1
      IL_0065:  ret

      IL_0066:  ldarg.1
      IL_0067:  unbox.any  assembly/R5
      IL_006c:  brfalse.s  IL_0070

      IL_006e:  ldc.i4.m1
      IL_006f:  ret

      IL_0070:  ldc.i4.0
      IL_0071:  ret
    } 

    .method public hidebysig virtual final instance int32  GetHashCode(class [runtime]System.Collections.IEqualityComparer comp) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  7
      .locals init (int32 V_0,
               string V_1)
      IL_0000:  ldarg.0
      IL_0001:  brfalse    IL_0097

      IL_0006:  ldc.i4.0
      IL_0007:  stloc.0
      IL_0008:  ldc.i4     0x9e3779b9
      IL_000d:  ldarg.1
      IL_000e:  ldarg.0
      IL_000f:  ldfld      float64 assembly/R5::E@
      IL_0014:  call       int32 [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives/HashCompare::GenericHashWithComparerIntrinsic<float64>(class [runtime]System.Collections.IEqualityComparer,
                                                                                                                                              !!0)
      IL_0019:  ldloc.0
      IL_001a:  ldc.i4.6
      IL_001b:  shl
      IL_001c:  ldloc.0
      IL_001d:  ldc.i4.2
      IL_001e:  shr
      IL_001f:  add
      IL_0020:  add
      IL_0021:  add
      IL_0022:  stloc.0
      IL_0023:  ldc.i4     0x9e3779b9
      IL_0028:  ldarg.0
      IL_0029:  ldfld      string assembly/R5::Y@
      IL_002e:  stloc.1
      IL_002f:  ldloc.1
      IL_0030:  brtrue.s   IL_0036

      IL_0032:  ldc.i4.0
      IL_0033:  nop
      IL_0034:  br.s       IL_003d

      IL_0036:  ldloc.1
      IL_0037:  callvirt   instance int32 [netstandard]System.Object::GetHashCode()
      IL_003c:  nop
      IL_003d:  ldloc.0
      IL_003e:  ldc.i4.6
      IL_003f:  shl
      IL_0040:  ldloc.0
      IL_0041:  ldc.i4.2
      IL_0042:  shr
      IL_0043:  add
      IL_0044:  add
      IL_0045:  add
      IL_0046:  stloc.0
      IL_0047:  ldc.i4     0x9e3779b9
      IL_004c:  ldarg.0
      IL_004d:  ldfld      int32 assembly/R5::X@
      IL_0052:  ldloc.0
      IL_0053:  ldc.i4.6
      IL_0054:  shl
      IL_0055:  ldloc.0
      IL_0056:  ldc.i4.2
      IL_0057:  shr
      IL_0058:  add
      IL_0059:  add
      IL_005a:  add
      IL_005b:  stloc.0
      IL_005c:  ldc.i4     0x9e3779b9
      IL_0061:  ldarg.0
      IL_0062:  ldfld      string assembly/R5::B@
      IL_0067:  stloc.1
      IL_0068:  ldloc.1
      IL_0069:  brtrue.s   IL_006f

      IL_006b:  ldc.i4.0
      IL_006c:  nop
      IL_006d:  br.s       IL_0076

      IL_006f:  ldloc.1
      IL_0070:  callvirt   instance int32 [netstandard]System.Object::GetHashCode()
      IL_0075:  nop
      IL_0076:  ldloc.0
      IL_0077:  ldc.i4.6
      IL_0078:  shl
      IL_0079:  ldloc.0
      IL_007a:  ldc.i4.2
      IL_007b:  shr
      IL_007c:  add
      IL_007d:  add
      IL_007e:  add
      IL_007f:  stloc.0
      IL_0080:  ldc.i4     0x9e3779b9
      IL_0085:  ldarg.0
      IL_0086:  ldfld      int32 assembly/R5::A@
      IL_008b:  ldloc.0
      IL_008c:  ldc.i4.6
      IL_008d:  shl
      IL_008e:  ldloc.0
      IL_008f:  ldc.i4.2
      IL_0090:  shr
      IL_0091:  add
      IL_0092:  add
      IL_0093:  add
      IL_0094:  stloc.0
      IL_0095:  ldloc.0
      IL_0096:  ret

      IL_0097:  ldc.i4.0
      IL_0098:  ret
    } 

    .method public hidebysig virtual final instance int32  GetHashCode() cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  8
      IL_0000:  ldarg.0
      IL_0001:  call       class [runtime]System.Collections.IEqualityComparer [FSharp.Core]Microsoft.FSharp.Core.LanguagePrimitives::get_GenericEqualityComparer()
      IL_0006:  callvirt   instance int32 assembly/R5::GetHashCode(class [runtime]System.Collections.IEqualityComparer)
      IL_000b:  ret
    } 

    .method public hidebysig instance bool Equals(class assembly/R5 obj, class [runtime]System.Collections.IEqualityComparer comp) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  4
      IL_0000:  ldarg.0
      IL_0001:  brfalse.s  IL_0061

      IL_0003:  ldarg.1
      IL_0004:  brfalse.s  IL_005f

      IL_0006:  ldarg.0
      IL_0007:  ldfld      int32 assembly/R5::A@
      IL_000c:  ldarg.1
      IL_000d:  ldfld      int32 assembly/R5::A@
      IL_0012:  bne.un.s   IL_005d

      IL_0014:  ldarg.0
      IL_0015:  ldfld      string assembly/R5::B@
      IL_001a:  ldarg.1
      IL_001b:  ldfld      string assembly/R5::B@
      IL_0020:  call       bool [netstandard]System.String::Equals(string,
                                                                   string)
      IL_0025:  brfalse.s  IL_005b

      IL_0027:  ldarg.0
      IL_0028:  ldfld      int32 assembly/R5::X@
      IL_002d:  ldarg.1
      IL_002e:  ldfld      int32 assembly/R5::X@
      IL_0033:  bne.un.s   IL_0059

      IL_0035:  ldarg.0
      IL_0036:  ldfld      string assembly/R5::Y@
      IL_003b:  ldarg.1
      IL_003c:  ldfld      string assembly/R5::Y@
      IL_0041:  call       bool [netstandard]System.String::Equals(string,
                                                                   string)
      IL_0046:  brfalse.s  IL_0057

      IL_0048:  ldarg.0
      IL_0049:  ldfld      float64 assembly/R5::E@
      IL_004e:  ldarg.1
      IL_004f:  ldfld      float64 assembly/R5::E@
      IL_0054:  ceq
      IL_0056:  ret

      IL_0057:  ldc.i4.0
      IL_0058:  ret

      IL_0059:  ldc.i4.0
      IL_005a:  ret

      IL_005b:  ldc.i4.0
      IL_005c:  ret

      IL_005d:  ldc.i4.0
      IL_005e:  ret

      IL_005f:  ldc.i4.0
      IL_0060:  ret

      IL_0061:  ldarg.1
      IL_0062:  ldnull
      IL_0063:  cgt.un
      IL_0065:  ldc.i4.0
      IL_0066:  ceq
      IL_0068:  ret
    } 

    .method public hidebysig virtual final instance bool  Equals(object obj, class [runtime]System.Collections.IEqualityComparer comp) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  5
      .locals init (class assembly/R5 V_0)
      IL_0000:  ldarg.1
      IL_0001:  isinst     assembly/R5
      IL_0006:  stloc.0
      IL_0007:  ldloc.0
      IL_0008:  brfalse.s  IL_0013

      IL_000a:  ldarg.0
      IL_000b:  ldloc.0
      IL_000c:  ldarg.2
      IL_000d:  callvirt   instance bool assembly/R5::Equals(class assembly/R5,
                                                                class [runtime]System.Collections.IEqualityComparer)
      IL_0012:  ret

      IL_0013:  ldc.i4.0
      IL_0014:  ret
    } 

    .method public hidebysig virtual final instance bool  Equals(class assembly/R5 obj) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  4
      .locals init (float64 V_0,
               float64 V_1)
      IL_0000:  ldarg.0
      IL_0001:  brfalse    IL_007c

      IL_0006:  ldarg.1
      IL_0007:  brfalse    IL_007a

      IL_000c:  ldarg.0
      IL_000d:  ldfld      int32 assembly/R5::A@
      IL_0012:  ldarg.1
      IL_0013:  ldfld      int32 assembly/R5::A@
      IL_0018:  bne.un.s   IL_0078

      IL_001a:  ldarg.0
      IL_001b:  ldfld      string assembly/R5::B@
      IL_0020:  ldarg.1
      IL_0021:  ldfld      string assembly/R5::B@
      IL_0026:  call       bool [netstandard]System.String::Equals(string,
                                                                   string)
      IL_002b:  brfalse.s  IL_0076

      IL_002d:  ldarg.0
      IL_002e:  ldfld      int32 assembly/R5::X@
      IL_0033:  ldarg.1
      IL_0034:  ldfld      int32 assembly/R5::X@
      IL_0039:  bne.un.s   IL_0074

      IL_003b:  ldarg.0
      IL_003c:  ldfld      string assembly/R5::Y@
      IL_0041:  ldarg.1
      IL_0042:  ldfld      string assembly/R5::Y@
      IL_0047:  call       bool [netstandard]System.String::Equals(string,
                                                                   string)
      IL_004c:  brfalse.s  IL_0072

      IL_004e:  ldarg.0
      IL_004f:  ldfld      float64 assembly/R5::E@
      IL_0054:  stloc.0
      IL_0055:  ldarg.1
      IL_0056:  ldfld      float64 assembly/R5::E@
      IL_005b:  stloc.1
      IL_005c:  ldloc.0
      IL_005d:  ldloc.1
      IL_005e:  ceq
      IL_0060:  brfalse.s  IL_0064

      IL_0062:  ldc.i4.1
      IL_0063:  ret

      IL_0064:  ldloc.0
      IL_0065:  ldloc.0
      IL_0066:  beq.s      IL_0070

      IL_0068:  ldloc.1
      IL_0069:  ldloc.1
      IL_006a:  ceq
      IL_006c:  ldc.i4.0
      IL_006d:  ceq
      IL_006f:  ret

      IL_0070:  ldc.i4.0
      IL_0071:  ret

      IL_0072:  ldc.i4.0
      IL_0073:  ret

      IL_0074:  ldc.i4.0
      IL_0075:  ret

      IL_0076:  ldc.i4.0
      IL_0077:  ret

      IL_0078:  ldc.i4.0
      IL_0079:  ret

      IL_007a:  ldc.i4.0
      IL_007b:  ret

      IL_007c:  ldarg.1
      IL_007d:  ldnull
      IL_007e:  cgt.un
      IL_0080:  ldc.i4.0
      IL_0081:  ceq
      IL_0083:  ret
    } 

    .method public hidebysig virtual final instance bool  Equals(object obj) cil managed
    {
      .custom instance void [runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
      
      .maxstack  4
      .locals init (class assembly/R5 V_0)
      IL_0000:  ldarg.1
      IL_0001:  isinst     assembly/R5
      IL_0006:  stloc.0
      IL_0007:  ldloc.0
      IL_0008:  brfalse.s  IL_0012

      IL_000a:  ldarg.0
      IL_000b:  ldloc.0
      IL_000c:  callvirt   instance bool assembly/R5::Equals(class assembly/R5)
      IL_0011:  ret

      IL_0012:  ldc.i4.0
      IL_0013:  ret
    } 

    .property instance int32 A()
    {
      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags,
                                                                                                  int32) = ( 01 00 04 00 00 00 00 00 00 00 00 00 ) 
      .get instance int32 assembly/R5::get_A()
    } 
    .property instance string B()
    {
      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags,
                                                                                                  int32) = ( 01 00 04 00 00 00 01 00 00 00 00 00 ) 
      .get instance string assembly/R5::get_B()
    } 
    .property instance int32 X()
    {
      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags,
                                                                                                  int32) = ( 01 00 04 00 00 00 02 00 00 00 00 00 ) 
      .get instance int32 assembly/R5::get_X()
    } 
    .property instance string Y()
    {
      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags,
                                                                                                  int32) = ( 01 00 04 00 00 00 03 00 00 00 00 00 ) 
      .get instance string assembly/R5::get_Y()
    } 
    .property instance float64 E()
    {
      .custom instance void [FSharp.Core]Microsoft.FSharp.Core.CompilationMappingAttribute::.ctor(valuetype [FSharp.Core]Microsoft.FSharp.Core.SourceConstructFlags,
                                                                                                  int32) = ( 01 00 04 00 00 00 04 00 00 00 00 00 ) 
      .get instance float64 assembly/R5::get_E()
    } 
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
    
    .maxstack  7
    IL_0000:  ldc.i4.3
    IL_0001:  ldstr      "lol"
    IL_0006:  ldc.r8     3.1400000000000001
    IL_000f:  newobj     instance void assembly/R3::.ctor(int32,
                                                             string,
                                                             float64)
    IL_0014:  pop
    IL_0015:  ldc.i4.3
    IL_0016:  ldstr      "lol"
    IL_001b:  ldc.r8     3.1400000000000001
    IL_0024:  newobj     instance void assembly/R4::.ctor(int32,
                                                             string,
                                                             float64)
    IL_0029:  pop
    IL_002a:  ldc.i4.3
    IL_002b:  ldstr      "lol"
    IL_0030:  ldc.i4.3
    IL_0031:  ldstr      "lol"
    IL_0036:  ldc.r8     3.1400000000000001
    IL_003f:  newobj     instance void assembly/R5::.ctor(int32,
                                                             string,
                                                             int32,
                                                             string,
                                                             float64)
    IL_0044:  pop
    IL_0045:  ret
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






