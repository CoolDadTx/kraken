#region Imports

using System;
using System.Collections.Generic;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken;
#endregion

namespace Tests.P3Net.Kraken
{
    public partial class TypeConversionTests
    {
        #region ToByteOrDefault

        #region String

        [TestMethod]
        public void ToByteOrDefault_WithString_IsInRange ()
        {
            //Act
            var actual = TypeConversion.ToByteOrDefault("10");

            //Assert
            actual.Should().Be(10);
        }

        [TestMethod]
        public void ToByteOrDefault_WithString_IsNull ()
        {
            //Act
            var actual = TypeConversion.ToByteOrDefault((string)null);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToByteOrDefault_WithString_IsEmpty ()
        {
            //Act
            var actual = TypeConversion.ToByteOrDefault("");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ToByteOrDefault_WithString_IsOutOfRange ()
        {
            //Act
            TypeConversion.ToByteOrDefault("1000");
        }

        [TestMethod]
        public void ToByteOrDefault_WithString_CustomDefault ()
        {
            //Act
            byte expected = 10;
            var actual = TypeConversion.ToByteOrDefault("", expected);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Object

        [TestMethod]
        public void ToByteOrDefault_WithObject_IsNumeric ()
        {
            object value = 10;

            //Act
            var actual = TypeConversion.ToByteOrDefault(value);

            //Assert
            actual.Should().Be(10);
        }

        [TestMethod]
        public void ToByteOrDefault_WithObject_IsNull ()
        {
            object value = null;

            //Act
            var actual = TypeConversion.ToByteOrDefault(value);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToByteOrDefault_WithObject_IsDBNull ()
        {
            //Act
            var actual = TypeConversion.ToByteOrDefault(DBNull.Value);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToByteOrDefault_WithObject_IsString ()
        {
            object value = "5";

            //Act
            var actual = TypeConversion.ToByteOrDefault(value);

            //Assert
            actual.Should().Be(5);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ToByteOrDefault_WithObject_IsNotValid ()
        {
            object value = "blah";

            //Act
            TypeConversion.ToByteOrDefault(value);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ToByteOrDefault_WithObject_IsOutOfRange ()
        {
            object value = Int32.MaxValue;

            //Act
            TypeConversion.ToByteOrDefault(value);
        }

        [TestMethod]
        public void ToByteOrDefault_WithObject_CustomDefault ()
        {
            //Act
            byte expected = 40;
            var actual = TypeConversion.ToByteOrDefault(DBNull.Value, expected);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #endregion

        #region ToUInt16OrDefault

        #region String

        [TestMethod]
        public void ToUInt16OrDefault_WithString_IsInRange ()
        {
            //Act
            var actual = TypeConversion.ToUInt16OrDefault("1000");

            //Assert
            actual.Should().Be(1000);
        }

        [TestMethod]
        public void ToUInt16OrDefault_WithString_IsNull ()
        {
            //Act
            var actual = TypeConversion.ToUInt16OrDefault((string)null);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToUInt16OrDefault_WithString_IsEmpty ()
        {
            //Act
            var actual = TypeConversion.ToUInt16OrDefault("");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ToUInt16OrDefault_WithString_IsOutOfRange ()
        {
            //Act
            TypeConversion.ToUInt16OrDefault((UInt16.MaxValue + 1).ToString());
        }

        [TestMethod]
        public void ToUInt16OrDefault_WithString_CustomDefault ()
        {
            //Act
            ushort expected = 34;
            var actual = TypeConversion.ToUInt16OrDefault("", expected);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Object

        [TestMethod]
        public void ToUInt16OrDefault_WithObject_IsNumeric ()
        {
            object value = 1000;

            //Act
            var actual = TypeConversion.ToUInt16OrDefault(value);

            //Assert
            actual.Should().Be(1000);
        }

        [TestMethod]
        public void ToUInt16OrDefault_WithObject_IsNull ()
        {
            object value = null;

            //Act
            var actual = TypeConversion.ToUInt16OrDefault(value);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToUInt16OrDefault_WithObject_IsDBNull ()
        {
            //Act
            var actual = TypeConversion.ToUInt16OrDefault(DBNull.Value);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToUInt16OrDefault_WithObject_IsString ()
        {
            object value = "5";

            //Act
            var actual = TypeConversion.ToUInt16OrDefault(value);

            //Assert
            actual.Should().Be(5);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ToUInt16OrDefault_WithObject_IsNotValid ()
        {
            object value = "blah";

            //Act
            TypeConversion.ToUInt16OrDefault(value);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ToUInt16OrDefault_WithObject_IsOutOfRange ()
        {
            object value = Int32.MaxValue;

            //Act
            TypeConversion.ToUInt16OrDefault(value);
        }

        [TestMethod]
        public void ToUInt16OrDefault_WithObject_CustomDefault ()
        {
            //Act
            ushort expected = 35;
            var actual = TypeConversion.ToUInt16OrDefault(DBNull.Value, expected);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #endregion

        #region ToUInt32OrDefault

        #region String

        [TestMethod]
        public void ToUInt32OrDefault_WithString_IsInRange ()
        {
            //Act
            var actual = TypeConversion.ToUInt32OrDefault("100000");

            //Assert
            actual.Should().Be(100000);
        }

        [TestMethod]
        public void ToUInt32OrDefault_WithString_IsNull ()
        {
            //Act
            var actual = TypeConversion.ToUInt32OrDefault((string)null);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToUInt32OrDefault_WithString_IsEmpty ()
        {
            //Act
            var actual = TypeConversion.ToUInt32OrDefault("");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ToUInt32OrDefault_WithString_IsOutOfRange ()
        {
            //Act
            TypeConversion.ToUInt32OrDefault(Int64.MaxValue.ToString());
        }

        [TestMethod]
        public void ToUInt32OrDefault_WithString_UsingHex ()
        {
            uint expected = 0x456;

            //Act
            var actual = TypeConversion.ToUInt32OrDefault("0x456");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToUInt32OrDefault_WithString_CustomDefault ()
        {
            //Act
            uint expected = 1234;
            var actual = TypeConversion.ToUInt32OrDefault("", expected);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Object

        [TestMethod]
        public void ToUInt32OrDefault_WithObject_IsNumeric ()
        {
            object value = 100000;

            //Act
            var actual = TypeConversion.ToUInt32OrDefault(value);

            //Assert
            actual.Should().Be(100000);
        }

        [TestMethod]
        public void ToUInt32OrDefault_WithObject_IsNull ()
        {
            object value = null;

            //Act
            var actual = TypeConversion.ToUInt32OrDefault(value);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToUInt32OrDefault_WithObject_IsDBNull ()
        {
            //Act
            var actual = TypeConversion.ToUInt32OrDefault(DBNull.Value);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToUInt32OrDefault_WithObject_IsString ()
        {
            object value = "5";

            //Act
            var actual = TypeConversion.ToUInt32OrDefault(value);

            //Assert
            actual.Should().Be(5);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ToUInt32OrDefault_WithObject_IsNotValid ()
        {
            object value = "blah";

            //Act
            TypeConversion.ToUInt32OrDefault(value);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ToUInt32OrDefault_WithObject_IsOutOfRange ()
        {
            object value = Int64.MaxValue;

            //Act
            TypeConversion.ToUInt32OrDefault(value);
        }

        [TestMethod]
        public void ToUInt32OrDefault_WithObject_CustomDefault ()
        {
            //Act
            uint expected = 43;
            var actual = TypeConversion.ToUInt32OrDefault(DBNull.Value, expected);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #endregion

        #region ToUInt64OrDefault

        #region String

        [TestMethod]
        public void ToUInt64OrDefault_WithString_IsInRange ()
        {
            //Act
            var actual = TypeConversion.ToUInt64OrDefault("10000000");

            //Assert
            actual.Should().Be(10000000);
        }

        [TestMethod]
        public void ToUInt64OrDefault_WithString_IsNull ()
        {
            //Act
            var actual = TypeConversion.ToUInt64OrDefault((string)null);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToUInt64OrDefault_WithString_IsEmpty ()
        {
            //Act
            var actual = TypeConversion.ToUInt64OrDefault("");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ToUInt64OrDefault_WithString_IsOutOfRange ()
        {
            //Act
            TypeConversion.ToUInt64OrDefault(UInt64.MaxValue.ToString() + "0");
        }

        [TestMethod]
        public void ToUInt64OrDefault_WithString_UsingHex ()
        {
            ulong expected = 0x456789;

            //Act
            var actual = TypeConversion.ToUInt64OrDefault("0X456789");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToUInt64OrDefault_WithString_CustomDefault ()
        {
            //Act
            ulong expected = 12345;
            var actual = TypeConversion.ToUInt64OrDefault("", expected);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Object

        [TestMethod]
        public void ToUInt64OrDefault_WithObject_IsNumeric ()
        {
            object value = 100000000;

            //Act
            var actual = TypeConversion.ToUInt64OrDefault(value);

            //Assert
            actual.Should().Be(100000000);
        }

        [TestMethod]
        public void ToUInt64OrDefault_WithObject_IsNull ()
        {
            object value = null;

            //Act
            var actual = TypeConversion.ToUInt64OrDefault(value);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToUInt64OrDefault_WithObject_IsDBNull ()
        {
            //Act
            var actual = TypeConversion.ToUInt64OrDefault(DBNull.Value);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToUInt64OrDefault_WithObject_IsString ()
        {
            object value = "5";

            //Act
            var actual = TypeConversion.ToUInt64OrDefault(value);

            //Assert
            actual.Should().Be(5);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ToUInt64OrDefault_WithObject_IsNotValid ()
        {
            object value = "blah";

            //Act
            TypeConversion.ToUInt64OrDefault(value);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ToUInt64OrDefault_WithObject_IsOutOfRange ()
        {
            object value = Double.MaxValue;

            //Act
            TypeConversion.ToUInt64OrDefault(value);
        }

        [TestMethod]
        public void ToUInt64OrDefault_WithObject_CustomDefault ()
        {
            //Act
            uint expected = 987654;
            var actual = TypeConversion.ToUInt64OrDefault(DBNull.Value, expected);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #endregion
    }
}
