using System;
using System.Collections.Generic;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken
{
    public partial class TypeConversionTests
    {
        #region Byte
        
        [TestMethod]
        public void Coerce_ToByteFromByte ()
        {
            byte expected = 10;

            //Act
            var actual = TypeConversion.Coerce<byte>(expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Coerce_ToByteFromSByte ()
        {
            sbyte expected = 10;

            //Act
            var actual = TypeConversion.Coerce<byte>(expected);

            //Assert
            actual.Should().Be((byte)expected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void Coerce_ToByteFromLarger ()
        {
            int expected = 10;

            //Act
            TypeConversion.Coerce<byte>(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Coerce_ToByteFromOther ()
        {
            //Act
            TypeConversion.Coerce<byte>("456");
        }
        #endregion

        #region Decimal

        [TestMethod]
        public void Coerce_ToDecimalFromByte ()
        {
            byte expected = 10;

            //Act
            var actual = TypeConversion.Coerce<decimal>(expected);

            //Assert
            actual.Should().BeExactly((int)expected);
        }

        [TestMethod]
        public void Coerce_ToDecimalFromSByte ()
        {
            sbyte expected = 10;

            //Act
            var actual = TypeConversion.Coerce<decimal>(expected);

            //Assert
            actual.Should().BeExactly((int)expected);
        }

        [TestMethod]
        public void Coerce_ToDecimalFromInt16 ()
        {
            short expected = 10;

            //Act
            var actual = TypeConversion.Coerce<decimal>(expected);

            //Assert
            actual.Should().BeExactly((int)expected);
        }

        [TestMethod]
        public void Coerce_ToDecimalFromUInt16 ()
        {
            ushort expected = 10;

            //Act
            var actual = TypeConversion.Coerce<decimal>(expected);

            //Assert
            actual.Should().BeExactly((int)expected);
        }

        [TestMethod]
        public void Coerce_ToDecimalFromInt32 ()
        {
            int expected = 10;

            //Act
            var actual = TypeConversion.Coerce<decimal>(expected);

            //Assert
            actual.Should().BeExactly((int)expected);
        }

        [TestMethod]
        public void Coerce_ToDecimalFromUInt32 ()
        {
            uint expected = 10;

            //Act
            var actual = TypeConversion.Coerce<decimal>(expected);

            //Assert
            actual.Should().BeExactly((int)expected);
        }

        [TestMethod]
        public void Coerce_ToDecimalFromInt64 ()
        {
            long expected = 10;

            //Act
            var actual = TypeConversion.Coerce<decimal>(expected);

            //Assert
            actual.Should().BeExactly((int)expected);
        }

        [TestMethod]
        public void Coerce_ToDecimalFromUInt64 ()
        {
            ulong expected = 10;

            //Act
            var actual = TypeConversion.Coerce<decimal>(expected);

            //Assert
            actual.Should().BeExactly((int)expected);
        }

        [TestMethod]
        public void Coerce_ToDecimalFromSingle ()
        {
            float expected = 10;

            //Act
            var actual = TypeConversion.Coerce<decimal>(expected);

            //Assert
            actual.Should().BeApproximately((decimal)expected);
        }

        [TestMethod]
        public void Coerce_ToDecimalFromDouble ()
        {
            double expected = 10;

            //Act
            var actual = TypeConversion.Coerce<decimal>(expected);

            //Assert
            actual.Should().BeApproximately((decimal)expected);
        }

        [TestMethod]
        public void Coerce_ToDecimalFromDecimal ()
        {
            decimal expected = 10;

            //Act
            var actual = TypeConversion.Coerce<decimal>(expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Coerce_ToDecimalFromOther ()
        {
            //Act
            TypeConversion.Coerce<decimal>("456");
        }
        #endregion

        #region Double

        [TestMethod]
        public void Coerce_ToDoubleFromByte ()
        {
            byte expected = 10;

            //Act
            var actual = TypeConversion.Coerce<double>(expected);

            //Assert
            actual.Should().BeExactly(expected);
        }

        [TestMethod]
        public void Coerce_ToDoubleFromSByte ()
        {
            sbyte expected = 10;

            //Act
            var actual = TypeConversion.Coerce<double>(expected);

            //Assert
            actual.Should().BeExactly(expected);
        }

        [TestMethod]
        public void Coerce_ToDoubleFromInt16 ()
        {
            short expected = 10;

            //Act
            var actual = TypeConversion.Coerce<double>(expected);

            //Assert
            actual.Should().BeExactly(expected);
        }

        [TestMethod]
        public void Coerce_ToDoubleFromUInt16 ()
        {
            ushort expected = 10;

            //Act
            var actual = TypeConversion.Coerce<double>(expected);

            //Assert
            actual.Should().BeExactly(expected);
        }

        [TestMethod]
        public void Coerce_ToDoubleFromInt32 ()
        {
            int expected = 10;

            //Act
            var actual = TypeConversion.Coerce<double>(expected);

            //Assert
            actual.Should().BeExactly(expected);
        }

        [TestMethod]
        public void Coerce_ToDoubleFromUInt32 ()
        {
            uint expected = 10;

            //Act
            var actual = TypeConversion.Coerce<double>(expected);

            //Assert
            actual.Should().BeExactly((int)expected);
        }

        [TestMethod]
        public void Coerce_ToDoubleFromInt64 ()
        {
            long expected = 10;

            //Act
            var actual = TypeConversion.Coerce<double>(expected);

            //Assert
            actual.Should().BeExactly((int)expected);
        }

        [TestMethod]
        public void Coerce_ToDoubleFromUInt64 ()
        {
            ulong expected = 10;

            //Act
            var actual = TypeConversion.Coerce<double>(expected);

            //Assert
            actual.Should().BeExactly((int)expected);
        }

        [TestMethod]
        public void Coerce_ToDoubleFromSingle ()
        {
            float expected = 10;

            //Act
            var actual = TypeConversion.Coerce<double>(expected);

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        public void Coerce_ToDoubleFromDouble ()
        {
            double expected = 10;

            //Act
            var actual = TypeConversion.Coerce<double>(expected);

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void Coerce_ToDoubleFromLarger ()
        {
            decimal expected = 10;

            //Act
            TypeConversion.Coerce<double>(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Coerce_ToDoubleFromOther ()
        {
            //Act
            TypeConversion.Coerce<double>("456");
        }
        #endregion

        #region Int16

        [TestMethod]
        public void Coerce_ToInt16FromByte ()
        {
            byte expected = 10;

            //Act
            var actual = TypeConversion.Coerce<short>(expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Coerce_ToInt16FromSByte ()
        {
            sbyte expected = 10;

            //Act
            var actual = TypeConversion.Coerce<short>(expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void Coerce_ToInt16FromUInt16 ()
        {
            ushort expected = 10;

            //Act
            TypeConversion.Coerce<short>(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void Coerce_ToInt16FromLarger ()
        {
            int expected = 10;

            //Act
            TypeConversion.Coerce<short>(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Coerce_ToInt16FromOther ()
        {
            //Act
            TypeConversion.Coerce<short>("456");
        }
        #endregion

        #region Int32

        [TestMethod]
        public void Coerce_ToInt32FromByte ()
        {
            byte expected = 10;

            //Act
            var actual = TypeConversion.Coerce<int>(expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Coerce_ToInt32FromSByte ()
        {
            sbyte expected = 10;

            //Act
            var actual = TypeConversion.Coerce<int>(expected);

            //Assert
            actual.Should().Be(expected);        
        }

        [TestMethod]
        public void Coerce_ToInt32FromInt16 ()
        {
            short expected = 10;

            //Act
            var actual = TypeConversion.Coerce<int>(expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Coerce_ToInt32FromUInt16 ()
        {
            ushort expected = 10;

            //Act
            var actual = TypeConversion.Coerce<int>(expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Coerce_ToInt32FromInt32 ()
        {
            int expected = 10;

            //Act
            var actual = TypeConversion.Coerce<int>(expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void Coerce_ToInt32FromUInt32 ()
        {
            uint expected = 10;

            //Act
            TypeConversion.Coerce<int>(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void Coerce_ToInt32FromLarger ()
        {
            long expected = 10;

            //Act
            TypeConversion.Coerce<int>(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Coerce_ToInt32FromOther ()
        {
            //Act
            TypeConversion.Coerce<int>("456");
        }
        #endregion

        #region Int64

        [TestMethod]
        public void Coerce_ToInt64FromByte ()
        {
            byte expected = 10;

            //Act
            var actual = TypeConversion.Coerce<long>(expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Coerce_ToInt64FromSByte ()
        {
            sbyte expected = 10;

            //Act
            var actual = TypeConversion.Coerce<long>(expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Coerce_ToInt64FromInt16 ()
        {
            short expected = 10;

            //Act
            var actual = TypeConversion.Coerce<long>(expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Coerce_ToInt64FromUInt16 ()
        {
            ushort expected = 10;

            //Act
            var actual = TypeConversion.Coerce<long>(expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Coerce_ToInt64FromInt32 ()
        {
            int expected = 10;

            //Act
            var actual = TypeConversion.Coerce<long>(expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Coerce_ToInt64FromUInt32 ()
        {
            uint expected = 10;

            //Act
            var actual = TypeConversion.Coerce<long>(expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Coerce_ToInt64FromInt64 ()
        {
            long expected = 10;

            //Act
            var actual = TypeConversion.Coerce<long>(expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void Coerce_ToInt64FromUInt64 ()
        {
            ulong expected = 10;

            //Act
            TypeConversion.Coerce<long>(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void Coerce_ToInt64FromLarger ()
        {
            decimal expected = 10;

            //Act
            TypeConversion.Coerce<long>(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Coerce_ToInt64FromOther ()
        {
            //Act
            TypeConversion.Coerce<long>("456");
        }
        #endregion

        #region SByte

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void Coerce_ToSByteFromByte ()
        {
            byte expected = 10;

            //Act
            TypeConversion.Coerce<sbyte>(expected);
        }

        [TestMethod]
        public void Coerce_ToSByteFromSByte ()
        {
            sbyte expected = 10;

            //Act
            var actual = TypeConversion.Coerce<sbyte>(expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void Coerce_ToSByteFromLarger ()
        {
            int expected = 10;

            //Act
            TypeConversion.Coerce<byte>(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Coerce_ToSByteFromOther ()
        {
            //Act
            TypeConversion.Coerce<sbyte>("456");
        }
        #endregion

        #region Single

        [TestMethod]
        public void Coerce_ToSingleFromByte ()
        {
            byte expected = 10;

            //Act
            var actual = TypeConversion.Coerce<float>(expected);

            //Assert
            actual.Should().BeExactly(expected);
        }

        [TestMethod]
        public void Coerce_ToSingleFromSByte ()
        {
            sbyte expected = 10;

            //Act
            var actual = TypeConversion.Coerce<float>(expected);

            //Assert
            actual.Should().BeExactly(expected);
        }

        [TestMethod]
        public void Coerce_ToSingleFromInt16 ()
        {
            short expected = 10;

            //Act
            var actual = TypeConversion.Coerce<float>(expected);

            //Assert
            actual.Should().BeExactly(expected);
        }

        [TestMethod]
        public void Coerce_ToSingleFromUInt16 ()
        {
            ushort expected = 10;

            //Act
            var actual = TypeConversion.Coerce<float>(expected);

            //Assert
            actual.Should().BeExactly(expected);
        }

        [TestMethod]
        public void Coerce_ToSingleFromInt32 ()
        {
            int expected = 10;

            //Act
            var actual = TypeConversion.Coerce<float>(expected);

            //Assert
            actual.Should().BeExactly(expected);
        }

        [TestMethod]
        public void Coerce_ToSingleFromUInt32 ()
        {
            uint expected = 10;

            //Act
            var actual = TypeConversion.Coerce<float>(expected);

            //Assert
            actual.Should().BeExactly((int)expected);
        }

        [TestMethod]
        public void Coerce_ToSingleFromInt64 ()
        {
            long expected = 10;

            //Act
            var actual = TypeConversion.Coerce<float>(expected);

            //Assert
            actual.Should().BeExactly((int)expected);
        }

        [TestMethod]
        public void Coerce_ToSingleFromUInt64 ()
        {
            ulong expected = 10;

            //Act
            var actual = TypeConversion.Coerce<float>(expected);

            //Assert
            actual.Should().BeExactly((int)expected);
        }

        [TestMethod]
        public void Coerce_ToSingleFromSingle ()
        {
            float expected = 10;

            //Act
            var actual = TypeConversion.Coerce<float>(expected);

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void Coerce_ToSingleFromDouble ()
        {
            double expected = 10;

            //Act
            TypeConversion.Coerce<float>(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void Coerce_ToSingleeFromLarger ()
        {
            decimal expected = 10;

            //Act
            TypeConversion.Coerce<float>(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Coerce_ToSingleFromOther ()
        {
            //Act
            TypeConversion.Coerce<float>("456");
        }
        #endregion

        #region UInt16

        [TestMethod]
        public void Coerce_ToUInt16FromByte ()
        {
            byte expected = 10;

            //Act
            var actual = TypeConversion.Coerce<ushort>(expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Coerce_ToUInt16FromSByte ()
        {
            sbyte expected = 10;

            //Act
            var actual = TypeConversion.Coerce<ushort>(expected);

            //Assert
            actual.Should().Be((ushort)expected);
        }

        [TestMethod]
        public void Coerce_ToUInt16FromInt16 ()
        {
            short expected = 10;

            //Act
            var actual = TypeConversion.Coerce<ushort>(expected);

            //Assert
            actual.Should().Be((ushort)expected);
        }

        [TestMethod]
        public void Coerce_ToUInt16FromUInt16 ()
        {
            ushort expected = 10;

            //Act
            var actual = TypeConversion.Coerce<ushort>(expected);

            //Assert
            actual.Should().Be((ushort)expected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void Coerce_ToUInt16FromLarger ()
        {
            int expected = 10;

            //Act
            TypeConversion.Coerce<ushort>(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Coerce_ToUInt16FromOther ()
        {
            //Act
            TypeConversion.Coerce<ushort>("456");
        }
        #endregion

        #region UInt32

        [TestMethod]
        public void Coerce_ToUInt32FromByte ()
        {
            byte expected = 10;

            //Act
            var actual = TypeConversion.Coerce<uint>(expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Coerce_ToUInt32FromSByte ()
        {
            sbyte expected = 10;

            //Act
            var actual = TypeConversion.Coerce<uint>(expected);

            //Assert
            actual.Should().Be((uint)expected);
        }

        [TestMethod]
        public void Coerce_ToUInt32FromInt16 ()
        {
            short expected = 10;

            //Act
            var actual = TypeConversion.Coerce<uint>(expected);

            //Assert
            actual.Should().Be((uint)expected);
        }

        [TestMethod]
        public void Coerce_ToUInt32FromUInt16 ()
        {
            ushort expected = 10;

            //Act
            var actual = TypeConversion.Coerce<uint>(expected);

            //Assert
            actual.Should().Be((uint)expected);
        }

        [TestMethod]
        public void Coerce_ToUInt32FromInt32 ()
        {
            int expected = 10;

            //Act
            var actual = TypeConversion.Coerce<uint>(expected);

            //Assert
            actual.Should().Be((uint)expected);
        }

        [TestMethod]
        public void Coerce_ToUInt32FromUInt32 ()
        {
            uint expected = 10;

            //Act
            var actual = TypeConversion.Coerce<uint>(expected);

            //Assert
            actual.Should().Be((uint)expected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void Coerce_ToUInt32FromLarger ()
        {
            long expected = 10;

            //Act
            TypeConversion.Coerce<uint>(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Coerce_ToUInt32FromOther ()
        {
            //Act
            TypeConversion.Coerce<uint>("456");
        }
        #endregion

        #region UInt64

        [TestMethod]
        public void Coerce_ToUInt64FromByte ()
        {
            byte expected = 10;

            //Act
            var actual = TypeConversion.Coerce<ulong>(expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Coerce_ToUInt64FromSByte ()
        {
            sbyte expected = 10;

            //Act
            var actual = TypeConversion.Coerce<ulong>(expected);

            //Assert
            actual.Should().Be((ulong)expected);
        }

        [TestMethod]
        public void Coerce_ToUInt64FromInt16 ()
        {
            short expected = 10;

            //Act
            var actual = TypeConversion.Coerce<ulong>(expected);

            //Assert
            actual.Should().Be((ulong)expected);
        }

        [TestMethod]
        public void Coerce_ToUInt64FromUInt16 ()
        {
            ushort expected = 10;

            //Act
            var actual = TypeConversion.Coerce<ulong>(expected);

            //Assert
            actual.Should().Be((ulong)expected);
        }

        [TestMethod]
        public void Coerce_ToUInt64FromInt32 ()
        {
            int expected = 10;

            //Act
            var actual = TypeConversion.Coerce<ulong>(expected);

            //Assert
            actual.Should().Be((ulong)expected);
        }

        [TestMethod]
        public void Coerce_ToUInt64FromUInt32 ()
        {
            uint expected = 10;

            //Act
            var actual = TypeConversion.Coerce<ulong>(expected);

            //Assert
            actual.Should().Be((ulong)expected);
        }

        [TestMethod]
        public void Coerce_ToUInt64FromInt64 ()
        {
            long expected = 10;

            //Act
            var actual = TypeConversion.Coerce<ulong>(expected);

            //Assert
            actual.Should().Be((ulong)expected);
        }

        [TestMethod]
        public void Coerce_ToUInt64FromUInt64 ()
        {
            ulong expected = 10;

            //Act
            var actual = TypeConversion.Coerce<ulong>(expected);

            //Assert
            actual.Should().Be((ulong)expected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void Coerce_ToUInt64FromLarger ()
        {
            decimal expected = 10;

            //Act
            TypeConversion.Coerce<ulong>(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Coerce_ToUInt64FromOther ()
        {
            //Act
            TypeConversion.Coerce<ulong>("456");
        }
        #endregion
    }
}
