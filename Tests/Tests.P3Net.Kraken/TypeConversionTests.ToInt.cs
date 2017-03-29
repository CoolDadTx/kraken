using System;
using System.Collections.Generic;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken;

namespace Tests.P3Net.Kraken
{
    public partial class TypeConversionTests
    {
        #region ToInt16OrDefault

        #region String

        [TestMethod]
        public void ToInt16OrDefault_WithString_IsInRange ()
        {
            //Actual
            var actual = TypeConversion.ToInt16OrDefault("1000");

            //Assert
            actual.Should().Be(1000);
        }

        [TestMethod]
        public void ToInt16OrDefault_WithString_IsNull ()
        {
            //Actual
            var actual = TypeConversion.ToInt16OrDefault((string)null);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToInt16OrDefault_WithString_IsEmpty ()
        {
            //Actual
            var actual = TypeConversion.ToInt16OrDefault("");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ToInt16OrDefault_WithString_IsOutOfRange ()
        {
            //Actual
            TypeConversion.ToInt16OrDefault((Int16.MaxValue + 1).ToString());
        }

        [TestMethod]
        public void ToInt16OrDefault_WithString_CustomDefault ()
        {
            //Actual
            short expected = 10;
            var actual = TypeConversion.ToInt16OrDefault("", expected);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Object

        [TestMethod]
        public void ToInt16OrDefault_WithObject_IsNumeric ()
        {
            object value = 1000;

            //Actual
            var actual = TypeConversion.ToInt16OrDefault(value);

            //Assert
            actual.Should().Be(1000);
        }

        [TestMethod]
        public void ToInt16OrDefault_WithObject_IsNull ()
        {
            object value = null;

            //Actual
            var actual = TypeConversion.ToInt16OrDefault(value);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToInt16OrDefault_WithObject_IsDBNull ()
        {
            //Actual
            var actual = TypeConversion.ToInt16OrDefault(DBNull.Value);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToInt16OrDefault_WithObject_IsString ()
        {
            object value = "5";

            //Actual
            var actual = TypeConversion.ToInt16OrDefault(value);

            //Assert
            actual.Should().Be(5);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ToInt16OrDefault_WithObject_IsNotValid ()
        {
            object value = "blah";

            //Actual
            TypeConversion.ToInt16OrDefault(value);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ToInt16OrDefault_WithObject_IsOutOfRange ()
        {
            object value = Int32.MaxValue;

            //Actual
            TypeConversion.ToInt16OrDefault(value);
        }

        [TestMethod]
        public void ToInt16OrDefault_WithObject_CustomDefault ()
        {
            //Actual
            short expected = 45;
            var actual = TypeConversion.ToInt16OrDefault(DBNull.Value, expected);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #endregion

        #region ToInt32OrDefault

        #region String

        [TestMethod]
        public void ToInt32OrDefault_WithString_IsInRange ()
        {
            //Actual
            var actual = TypeConversion.ToInt32OrDefault("100000");

            //Assert
            actual.Should().Be(100000);
        }

        [TestMethod]
        public void ToInt32OrDefault_WithString_IsNull ()
        {
            //Actual
            var actual = TypeConversion.ToInt32OrDefault((string)null);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToInt32OrDefault_WithString_IsEmpty ()
        {
            //Actual
            var actual = TypeConversion.ToInt32OrDefault("");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ToInt32OrDefault_WithString_IsOutOfRange ()
        {
            //Actual
            TypeConversion.ToInt16OrDefault(Int64.MaxValue.ToString());
        }

        [TestMethod]
        public void ToInt32OrDefault_WithString_CustomDefault ()
        {
            //Actual
            var expected = 1234;
            var actual = TypeConversion.ToInt32OrDefault("", expected);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Object

        [TestMethod]
        public void ToInt32OrDefault_WithObject_IsNumeric ()
        {
            object value = 100000;

            //Actual
            var actual = TypeConversion.ToInt32OrDefault(value);

            //Assert
            actual.Should().Be(100000);
        }

        [TestMethod]
        public void ToInt32OrDefault_WithObject_IsNull ()
        {
            object value = null;

            //Actual
            var actual = TypeConversion.ToInt32OrDefault(value);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToInt32OrDefault_WithObject_IsDBNull ()
        {
            //Actual
            var actual = TypeConversion.ToInt32OrDefault(DBNull.Value);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToInt32OrDefault_WithObject_IsString ()
        {
            object value = "5";

            //Actual
            var actual = TypeConversion.ToInt32OrDefault(value);

            //Assert
            actual.Should().Be(5);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ToInt32OrDefault_WithObject_IsNotValid ()
        {
            object value = "blah";

            //Actual
            TypeConversion.ToInt32OrDefault(value);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ToInt32OrDefault_WithObject_IsOutOfRange ()
        {
            object value = Int64.MaxValue;

            //Actual
            TypeConversion.ToInt32OrDefault(value);
        }

        [TestMethod]
        public void ToInt32OrDefault_WithObject_CustomDefault ()
        {
            //Actual
            var expected = 9876;
            var actual = TypeConversion.ToInt32OrDefault(DBNull.Value, expected);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #endregion

        #region ToInt64OrDefault

        #region String

        [TestMethod]
        public void ToInt64OrDefault_WithString_IsInRange ()
        {
            //Actual
            var actual = TypeConversion.ToInt64OrDefault("10000000");

            //Assert
            actual.Should().Be(10000000);
        }

        [TestMethod]
        public void ToInt64OrDefault_WithString_IsNull ()
        {
            //Actual
            var actual = TypeConversion.ToInt64OrDefault((string)null);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToInt64OrDefault_WithString_IsEmpty ()
        {
            //Actual
            var actual = TypeConversion.ToInt64OrDefault("");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ToInt64OrDefault_WithString_IsOutOfRange ()
        {
            //Actual
            TypeConversion.ToInt64OrDefault(Int64.MaxValue.ToString() + "0");
        }

        [TestMethod]
        public void ToInt64OrDefault_WithString_CustomDefault ()
        {
            //Actual
            long expected = 1234567;
            var actual = TypeConversion.ToInt64OrDefault("", expected);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Object

        [TestMethod]
        public void ToInt64OrDefault_WithObject_IsNumeric ()
        {
            object value = 100000000;

            //Actual
            var actual = TypeConversion.ToInt64OrDefault(value);

            //Assert
            actual.Should().Be(100000000);
        }

        [TestMethod]
        public void ToInt64OrDefault_WithObject_IsNull ()
        {
            object value = null;

            //Actual
            var actual = TypeConversion.ToInt64OrDefault(value);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToInt64OrDefault_WithObject_IsDBNull ()
        {
            //Actual
            var actual = TypeConversion.ToInt64OrDefault(DBNull.Value);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToInt64OrDefault_WithObject_IsString ()
        {
            object value = "5";

            //Actual
            var actual = TypeConversion.ToInt64OrDefault(value);

            //Assert
            actual.Should().Be(5);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ToInt64OrDefault_WithObject_IsNotValid ()
        {
            object value = "blah";

            //Actual
            TypeConversion.ToInt64OrDefault(value);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ToInt64OrDefault_WithObject_IsOutOfRange ()
        {
            object value = UInt64.MaxValue;

            //Actual
            TypeConversion.ToInt64OrDefault(value);
        }

        [TestMethod]
        public void ToInt64OrDefault_WithObject_CustomDefault ()
        {
            //Actual
            long expected = 987654;
            var actual = TypeConversion.ToInt64OrDefault(DBNull.Value, expected);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #endregion

        #region ToSByteOrDefault

        #region String

        [TestMethod]
        public void ToSByteOrDefault_WithString_IsInRange ()
        {
            //Actual
            var actual = TypeConversion.ToSByteOrDefault("-10");

            //Assert
            actual.Should().Be(-10);
        }

        [TestMethod]
        public void ToSByteOrDefault_WithString_IsNull ()
        {
            //Actual
            var actual = TypeConversion.ToSByteOrDefault((string)null);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToSByteOrDefault_WithString_IsEmpty ()
        {
            //Actual
            var actual = TypeConversion.ToSByteOrDefault("");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ToSByteOrDefault_WithString_IsOutOfRange ()
        {
            //Actual
            TypeConversion.ToSByteOrDefault("1000");
        }

        [TestMethod]
        public void ToSByteOrDefault_WithString_CustomDefault ()
        {
            //Actual
            sbyte expected = -23;
            var actual = TypeConversion.ToSByteOrDefault("", expected);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Object

        [TestMethod]
        public void ToSByteOrDefault_WithObject_IsNumeric ()
        {
            object value = 10;

            //Actual
            var actual = TypeConversion.ToSByteOrDefault(value);

            //Assert
            actual.Should().Be(10);
        }

        [TestMethod]
        public void ToSByteOrDefault_WithObject_IsNull ()
        {
            object value = null;

            //Actual
            var actual = TypeConversion.ToSByteOrDefault(value);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToSByteOrDefault_WithObject_IsDBNull ()
        {
            //Actual
            var actual = TypeConversion.ToSByteOrDefault(DBNull.Value);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToSByteOrDefault_WithObject_IsString ()
        {
            object value = "5";

            //Actual
            var actual = TypeConversion.ToSByteOrDefault(value);

            //Assert
            actual.Should().Be(5);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ToSByteOrDefault_WithObject_IsNotValid ()
        {
            object value = "blah";

            //Actual
            TypeConversion.ToSByteOrDefault(value);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ToSByteOrDefault_WithObject_IsOutOfRange ()
        {
            object value = Int32.MaxValue;

            //Actual
            TypeConversion.ToSByteOrDefault(value);
        }

        [TestMethod]
        public void ToSByteOrDefault_WithObject_CustomDefault ()
        {
            //Actual
            sbyte expected = -45;
            var actual = TypeConversion.ToSByteOrDefault(DBNull.Value, expected);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #endregion
    }
}
