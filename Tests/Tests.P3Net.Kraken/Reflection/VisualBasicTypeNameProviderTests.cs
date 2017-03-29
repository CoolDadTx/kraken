using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Reflection;
using FluentAssertions;

namespace Tests.P3Net.Kraken.Reflection
{
    [TestClass]
    public class VisualBasicTypeNameProviderTests
    {
        #region Primitives

        [TestMethod]
        public void GetTypeName_Bool()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(bool));

            actual.Should().Be("Boolean");
        }

        [TestMethod]
        public void GetTypeName_Char()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(char));

            actual.Should().Be("Char");
        }

        [TestMethod]
        public void GetTypeName_String()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(string));

            actual.Should().Be("String");
        }

        [TestMethod]
        public void GetTypeName_Float()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(float));

            actual.Should().Be("Single");
        }

        [TestMethod]
        public void GetTypeName_Double()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(double));

            actual.Should().Be("Double");
        }

        [TestMethod]
        public void GetTypeName_Decimal()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(decimal));

            actual.Should().Be("Decimal");
        }

        [TestMethod]
        public void GetTypeName_Byte()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(byte));

            actual.Should().Be("Byte");
        }

        [TestMethod]
        public void GetTypeName_UShort()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(ushort));

            actual.Should().Be("UShort");
        }

        [TestMethod]
        public void GetTypeName_UInt()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(uint));

            actual.Should().Be("UInteger");
        }

        [TestMethod]
        public void GetTypeName_ULong()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(ulong));

            actual.Should().Be("ULong");
        }

        [TestMethod]
        public void GetTypeName_SByte()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(sbyte));

            actual.Should().Be("SByte");
        }

        [TestMethod]
        public void GetTypeName_Short()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(short));

            actual.Should().Be("Short");
        }

        [TestMethod]
        public void GetTypeName_Int()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(int));

            actual.Should().Be("Integer");
        }

        [TestMethod]
        public void GetTypeName_Long()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(long));

            actual.Should().Be("Long");
        }

        [TestMethod]
        public void GetTypeName_Object()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(object));

            actual.Should().Be("Object");
        }

        [TestMethod]
        public void GetTypeName_Void()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(void));

            actual.Should().Be("");
        }
        #endregion

        #region Nullable Primitives

        [TestMethod]
        public void GetTypeName_NullableChar()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(char?));

            actual.Should().Be("Char?");
        }

        [TestMethod]
        public void GetTypeName_NullableFloat()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(float?));

            actual.Should().Be("Single?");
        }

        [TestMethod]
        public void GetTypeName_NullableDouble()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(double?));

            actual.Should().Be("Double?");
        }

        [TestMethod]
        public void GetTypeName_NullableDecimal()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(decimal?));

            actual.Should().Be("Decimal?");
        }

        [TestMethod]
        public void GetTypeName_NullableByte()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(byte?));

            actual.Should().Be("Byte?");
        }

        [TestMethod]
        public void GetTypeName_NullableUShort()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(ushort?));

            actual.Should().Be("UShort?");
        }

        [TestMethod]
        public void GetTypeName_NullableUInt()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(uint?));

            actual.Should().Be("UInteger?");
        }

        [TestMethod]
        public void GetTypeName_NullableULong()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(ulong?));

            actual.Should().Be("ULong?");
        }

        [TestMethod]
        public void GetTypeName_NullableSByte()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(sbyte?));

            actual.Should().Be("SByte?");
        }

        [TestMethod]
        public void GetTypeName_NullableShort()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(short?));

            actual.Should().Be("Short?");
        }

        [TestMethod]
        public void GetTypeName_NullableInt()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(int?));

            actual.Should().Be("Integer?");
        }

        [TestMethod]
        public void GetTypeName_NullableLong()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(long?));

            actual.Should().Be("Long?");
        }
        #endregion

        #region Custom Types

        [TestMethod]
        public void GetTypeName_RefType()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(SampleClass));

            actual.Should().Be("SampleClass");
        }

        [TestMethod]
        public void GetTypeName_ValueType()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(SampleStruct));

            actual.Should().Be("SampleStruct");
        }

        [TestMethod]
        public void GetTypeName_NullableValueType()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(SampleStruct?));

            actual.Should().Be("SampleStruct?");
        }
        #endregion

        #region Generics

        [TestMethod]
        public void GetTypeName_GenericType()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(List<int>));

            actual.Should().Be("List(Of Integer)");
        }

        [TestMethod]
        public void GetTypeName_GenericTypeWithMultipleParameters()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(Dictionary<string, double>));

            actual.Should().Be("Dictionary(Of String, Of Double)");
        }

        [TestMethod]
        public void GetTypeName_GenericTypeOfGenericType()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(List<IEnumerable<int>>));

            actual.Should().Be("List(Of IEnumerable(Of Integer))");
        }
        #endregion

        #region Others

        [TestMethod]
        public void GetTypeName_RefSimpleParameter()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(ISample).GetMethod("MethodWithSimpleRefParameter").GetParameters().First().ParameterType);

            actual.Should().Be("SampleClass");
        }

        [TestMethod]
        public void GetTypeName_PointerSimpleParameter()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(char*));

            actual.Should().Be("Char");
        }

        [TestMethod]
        public void GetTypeName_RefGenericParameter()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(ISample).GetMethod("MethodWithGenericRefParameter").GetParameters().First().ParameterType);

            actual.Should().Be("IEnumerable(Of Integer)");
        }

        [TestMethod]
        public void GetTypeName_OutBooleanParameter()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(ISample).GetMethod("MethodWithOutBooleanParameter").GetParameters().First().ParameterType);

            actual.Should().Be("Boolean");
        }
        #endregion

        #region Arrays

        [TestMethod]
        public void GetTypeName_ArrayOfPrimitives()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(int[]));

            actual.Should().Be("Integer()");
        }

        [TestMethod]
        public void GetTypeName_ArrayOfRefTypes()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(SampleClass[]));

            actual.Should().Be("SampleClass()");
        }

        [TestMethod]
        public void GetTypeName_ArrayOfGenericTypes()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(List<int>[]));

            actual.Should().Be("List(Of Integer)()");
        }

        [TestMethod]
        public void GetTypeName_MultidimensionalArrayOfPrimitives()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(int[,]));

            actual.Should().Be("Integer(,)");
        }

        [TestMethod]
        public void GetTypeName_MultidimensionalArrayOfRefTypes()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(SampleClass[, ,]));

            actual.Should().Be("SampleClass(,,)");
        }

        [TestMethod]
        public void GetTypeName_MultidimensionalArrayOfGenericTypes()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(IEnumerable<double>[, ,]));

            actual.Should().Be("IEnumerable(Of Double)(,,)");
        }

        [TestMethod]
        public void GetTypeName_JaggedArrayOfPrimitives()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(int[][]));

            actual.Should().Be("Integer()()");
        }

        [TestMethod]
        public void GetTypeName_JaggedArrayOfGenericTypes()
        {
            var target = new VisualBasicTypeNameProvider();
            var actual = target.GetTypeName(typeof(IEnumerable<double>[][]));

            actual.Should().Be("IEnumerable(Of Double)()()");
        }
        #endregion

        #region IncludeNamespace

        [TestMethod]
        public void IncludeNamespace_IsSet_IgnoreForAlias()
        {
            var target = new VisualBasicTypeNameProvider();
            target.IncludeNamespace = true;

            var str = target.GetTypeName(typeof(int));

            str.Should().Be("Integer");
        }

        [TestMethod]
        public void IncludeNamespace_IsSet_Works()
        {
            var target = new VisualBasicTypeNameProvider();
            target.IncludeNamespace = true;

            var actualType = this.GetType();
            var str = target.GetTypeName(actualType);

            str.Should().Be(actualType.Namespace + "." + actualType.Name);
        }

        [TestMethod]
        public void IncludeNamespace_WithGenericType()
        {
            var target = new VisualBasicTypeNameProvider() { IncludeNamespace = true };
            var actual = target.GetTypeName(typeof(List<int>));

            actual.Should().Be("System.Collections.Generic.List(Of Integer)");
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
