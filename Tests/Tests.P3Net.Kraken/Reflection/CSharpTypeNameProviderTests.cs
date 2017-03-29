using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Reflection;
using FluentAssertions;

namespace Tests.P3Net.Kraken.Reflection
{
    [TestClass]
    public class CSharpTypeNameProviderTests
    {
        #region Primitives

        [TestMethod]
        public void GetTypeName_Bool()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(bool));

            actual.Should().Be("bool");
        }

        [TestMethod]
        public void GetTypeName_Char()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(char));

            actual.Should().Be("char");
        }

        [TestMethod]
        public void GetTypeName_String()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(string));

            actual.Should().Be("string");
        }

        [TestMethod]
        public void GetTypeName_Float()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(float));

            actual.Should().Be("float");
        }

        [TestMethod]
        public void GetTypeName_Double()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(double));

            actual.Should().Be("double");
        }

        [TestMethod]
        public void GetTypeName_Decimal()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(decimal));

            actual.Should().Be("decimal");
        }

        [TestMethod]
        public void GetTypeName_Byte()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(byte));

            actual.Should().Be("byte");
        }

        [TestMethod]
        public void GetTypeName_UShort()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(ushort));

            actual.Should().Be("ushort");
        }

        [TestMethod]
        public void GetTypeName_UInt()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(uint));

            actual.Should().Be("uint");
        }

        [TestMethod]
        public void GetTypeName_ULong()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(ulong));

            actual.Should().Be("ulong");
        }

        [TestMethod]
        public void GetTypeName_SByte()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(sbyte));

            actual.Should().Be("sbyte");
        }

        [TestMethod]
        public void GetTypeName_Short()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(short));

            actual.Should().Be("short");
        }

        [TestMethod]
        public void GetTypeName_Int()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(int));

            actual.Should().Be("int");
        }

        [TestMethod]
        public void GetTypeName_Long()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(long));

            actual.Should().Be("long");
        }

        [TestMethod]
        public void GetTypeName_Object()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(object));

            actual.Should().Be("object");
        }

        [TestMethod]
        public void GetTypeName_Void()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(void));

            actual.Should().Be("void");
        }
        #endregion

        #region Nullable Primitives

        [TestMethod]
        public void GetTypeName_NullableChar()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(char?));

            actual.Should().Be("char?");
        }

        [TestMethod]
        public void GetTypeName_NullableFloat()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(float?));

            actual.Should().Be("float?");
        }

        [TestMethod]
        public void GetTypeName_NullableDouble()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(double?));

            actual.Should().Be("double?");
        }

        [TestMethod]
        public void GetTypeName_NullableDecimal()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(decimal?));

            actual.Should().Be("decimal?");
        }

        [TestMethod]
        public void GetTypeName_NullableByte()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(byte?));

            actual.Should().Be("byte?");
        }

        [TestMethod]
        public void GetTypeName_NullableUShort()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(ushort?));

            actual.Should().Be("ushort?");
        }

        [TestMethod]
        public void GetTypeName_NullableUInt()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(uint?));

            actual.Should().Be("uint?");
        }

        [TestMethod]
        public void GetTypeName_NullableULong()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(ulong?));

            actual.Should().Be("ulong?");
        }

        [TestMethod]
        public void GetTypeName_NullableSByte()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(sbyte?));

            actual.Should().Be("sbyte?");
        }

        [TestMethod]
        public void GetTypeName_NullableShort()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(short?));

            actual.Should().Be("short?");
        }

        [TestMethod]
        public void GetTypeName_NullableInt()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(int?));

            actual.Should().Be("int?");
        }

        [TestMethod]
        public void GetTypeName_NullableLong()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(long?));

            actual.Should().Be("long?");
        }
        #endregion

        #region Custom Types

        [TestMethod]
        public void GetTypeName_RefType()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(SampleClass));

            actual.Should().Be("SampleClass");
        }

        [TestMethod]
        public void GetTypeName_ValueType()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(SampleStruct));

            actual.Should().Be("SampleStruct");
        }

        [TestMethod]
        public void GetTypeName_NullableValueType()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(SampleStruct?));

            actual.Should().Be("SampleStruct?");
        }
        #endregion

        #region Generics

        [TestMethod]
        public void GetTypeName_GenericType()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(List<int>));

            actual.Should().Be("List<int>");
        }

        [TestMethod]
        public void GetTypeName_GenericTypeWithMultipleParameters()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(Dictionary<string, double>));

            actual.Should().Be("Dictionary<string, double>");
        }

        [TestMethod]
        public void GetTypeName_GenericTypeOfGenericType()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(List<IEnumerable<int>>));

            actual.Should().Be("List<IEnumerable<int>>");
        }
        #endregion

        #region Others

        [TestMethod]
        public void GetTypeName_RefSimpleParameter()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(ISample).GetMethod("MethodWithSimpleRefParameter").GetParameters().First().ParameterType);

            actual.Should().Be("SampleClass");
        }

        [TestMethod]
        public void GetTypeName_PointerSimpleParameter()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(char*));

            actual.Should().Be("char*");
        }

        [TestMethod]
        public void GetTypeName_RefGenericParameter()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(ISample).GetMethod("MethodWithGenericRefParameter").GetParameters().First().ParameterType);

            actual.Should().Be("IEnumerable<int>");
        }

        [TestMethod]
        public void GetTypeName_OutBooleanParameter()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(ISample).GetMethod("MethodWithOutBooleanParameter").GetParameters().First().ParameterType);

            actual.Should().Be("bool");
        }
        #endregion

        #region Arrays

        [TestMethod]
        public void GetTypeName_ArrayOfPrimitives()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(int[]));

            actual.Should().Be("int[]");
        }

        [TestMethod]
        public void GetTypeName_ArrayOfRefTypes()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(SampleClass[]));

            actual.Should().Be("SampleClass[]");
        }

        [TestMethod]
        public void GetTypeName_ArrayOfGenericTypes()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(List<int>[]));

            actual.Should().Be("List<int>[]");
        }

        [TestMethod]
        public void GetTypeName_MultidimensionalArrayOfPrimitives()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(int[,]));

            actual.Should().Be("int[,]");
        }

        [TestMethod]
        public void GetTypeName_MultidimensionalArrayOfRefTypes()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(SampleClass[, ,]));

            actual.Should().Be("SampleClass[,,]");
        }

        [TestMethod]
        public void GetTypeName_MultidimensionalArrayOfGenericTypes()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(IEnumerable<double>[, ,]));

            actual.Should().Be("IEnumerable<double>[,,]");
        }

        [TestMethod]
        public void GetTypeName_JaggedArrayOfPrimitives()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(int[][]));

            actual.Should().Be("int[][]");
        }

        [TestMethod]
        public void GetTypeName_JaggedArrayOfGenericTypes()
        {
            var target = new CSharpTypeNameProvider();
            var actual = target.GetTypeName(typeof(IEnumerable<double>[][]));

            actual.Should().Be("IEnumerable<double>[][]");
        }
        #endregion

        #region IncludeNamespace

        [TestMethod]
        public void IncludeNamespace_IsSet_IgnoreForAlias()
        {
            var target = new CSharpTypeNameProvider();
            target.IncludeNamespace = true;

            var str = target.GetTypeName(typeof(int));

            str.Should().Be("int");
        }

        [TestMethod]
        public void IncludeNamespace_IsSet_Works()
        {
            var target = new CSharpTypeNameProvider();
            target.IncludeNamespace = true;

            var actualType = this.GetType();
            var str = target.GetTypeName(actualType);

            str.Should().Be(actualType.Namespace + "." + actualType.Name);
        }

        [TestMethod]
        public void IncludeNamespace_WithGenericType()
        {
            var target = new CSharpTypeNameProvider() { IncludeNamespace = true };
            var actual = target.GetTypeName(typeof(List<int>));

            actual.Should().Be("System.Collections.Generic.List<int>");
        }
        #endregion

        #region Private Members

        internal sealed class SampleClass
        { }

        internal struct SampleStruct
        { }

        internal interface ISample
        {
            void MethodWithSimpleRefParameter(ref SampleClass p);

            void MethodWithGenericRefParameter(ref IEnumerable<int> p);

            void MethodWithOutBooleanParameter(out bool p);
        }
        #endregion
    }
}
