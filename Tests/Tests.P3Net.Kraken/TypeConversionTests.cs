using System;
using System.Collections.Generic;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken
{
    [TestClass]
    public partial class TypeConversionTests : UnitTest
    {
        #region ToBooleanOrDefault

        #region String
        
        [TestMethod]
        public void ToBooleanOrDefault_WithString_IsOne ()
        {
            //Actual
            var actual = TypeConversion.ToBooleanOrDefault("1");

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void ToBooleanOrDefault_WithString_IsNegativeOne ()
        {
            //Actual
            var actual = TypeConversion.ToBooleanOrDefault("-1");

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void ToBooleanOrDefault_WithString_IsZero ()
        {
            //Actual
            var actual = TypeConversion.ToBooleanOrDefault("0");

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void ToBooleanOrDefault_WithString_IsTrue ()
        {
            //Actual
            var actual = TypeConversion.ToBooleanOrDefault("True");

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void ToBooleanOrDefault_WithString_IsFalse ()
        {
            //Actual
            var actual = TypeConversion.ToBooleanOrDefault("false");

            //Assert
            actual.Should().BeFalse();
        }
        
        [TestMethod]
        public void ToBooleanOrDefault_WithString_IsNonZero ()
        {
            //Actual
            var actual = TypeConversion.ToBooleanOrDefault("5");

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void ToBooleanOrDefault_WithString_IsNull ()
        {
            //Actual
            var actual = TypeConversion.ToBooleanOrDefault((string)null);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void ToBooleanOrDefault_WithString_IsEmpty ()
        {
            //Actual
            var actual = TypeConversion.ToBooleanOrDefault("");

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ToBooleanOrDefault_WithString_IsInvalid ()
        {
            //Actual
            TypeConversion.ToBooleanOrDefault("blah");
        }

        [TestMethod]
        public void ToBooleanOrDefault_WithString_CustomDefault ()
        {
            //Actual            
            var actual = TypeConversion.ToBooleanOrDefault("", true);

            //Assert
            actual.Should().BeTrue();
        }
        #endregion

        #region Object
        
        [TestMethod]
        public void ToBooleanOrDefault_WithObject_IsOne ()
        {
            object value = 1;

            //Actual
            var actual = TypeConversion.ToBooleanOrDefault(value);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void ToBooleanOrDefault_WithObject_IsZero ()
        {
            object value = 0;

            //Actual
            var actual = TypeConversion.ToBooleanOrDefault(value);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void ToBooleanOrDefault_WithObject_IsNull ()
        {
            object value = null;

            //Actual
            var actual = TypeConversion.ToBooleanOrDefault(value);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void ToBooleanOrDefault_WithObject_IsDBNull ()
        {
            //Actual
            var actual = TypeConversion.ToBooleanOrDefault(DBNull.Value);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void ToBooleanOrDefault_WithObject_IsString ()
        {
            object value = "1";

            //Actual
            var actual = TypeConversion.ToBooleanOrDefault(value);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void ToBooleanOrDefault_WithObject_CustomDefault ()
        {
            //Actual
            var actual = TypeConversion.ToBooleanOrDefault(DBNull.Value, true);

            //Assert
            actual.Should().BeTrue();
        }
        #endregion

        #endregion

        #region ToCharOrDefault

        #region String

        [TestMethod]
        public void ToCharOrDefault_WithString_IsInRange ()
        {
            //Actual
            var actual = TypeConversion.ToCharOrDefault("A");

            //Assert
            actual.Should().Be('A');
        }

        [TestMethod]
        public void ToCharOrDefault_WithString_IsNull ()
        {
            //Actual
            var actual = TypeConversion.ToCharOrDefault((string)null);

            //Assert
            actual.Should().Be(default(char));
        }

        [TestMethod]
        public void ToCharOrDefault_WithString_IsEmpty ()
        {
            //Actual
            var actual = TypeConversion.ToCharOrDefault("");

            //Assert
            actual.Should().Be(default(char));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ToCharOrDefault_WithString_IsOutOfRange ()
        {
            //Actual
            TypeConversion.ToCharOrDefault("ABC");
        }

        [TestMethod]
        public void ToCharOrDefault_WithString_CustomDefault ()
        {
            //Actual
            var expected = 'C';
            var actual = TypeConversion.ToCharOrDefault("", expected);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Object
        
        [TestMethod]
        public void ToCharOrDefault_WithObject_IsNull ()
        {
            object value = null;

            //Actual
            var actual = TypeConversion.ToCharOrDefault(value);

            //Assert
            actual.Should().Be(default(char));
        }

        [TestMethod]
        public void ToCharOrDefault_WithObject_IsDBNull ()
        {
            //Actual
            var actual = TypeConversion.ToCharOrDefault(DBNull.Value);

            //Assert
            actual.Should().Be(default(char));
        }

        [TestMethod]
        public void ToCharOrDefault_WithObject_IsString ()
        {
            object value = "5";

            //Actual
            var actual = TypeConversion.ToCharOrDefault(value);

            //Assert
            actual.Should().Be('5');
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ToCharOrDefault_WithObject_IsNotValid ()
        {
            object value = "blah";

            //Actual
            TypeConversion.ToCharOrDefault(value);
        }

        [TestMethod]
        public void ToCharOrDefault_WithObject_CustomDefault ()
        {
            //Actual
            var expected = 'Z';
            var actual = TypeConversion.ToCharOrDefault(DBNull.Value, expected);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #endregion

        #region ToDateTimeOrDefault

        #region String

        [TestMethod]
        public void ToDateTimeOrDefault_WithString_IsValid ()
        {
            var expected = new DateTime(2011, 1, 2);

            //Actual
            var actual = TypeConversion.ToDateTimeOrDefault(expected.ToShortDateString());

            //Assert
            actual.Should().BeDate(expected);
        }

        [TestMethod]
        public void ToDateTimeOrDefault_WithString_IsNull ()
        {
            //Actual
            var actual = TypeConversion.ToDateTimeOrDefault((string)null);

            //Assert
            actual.Should().Be(default(DateTime));
        }

        [TestMethod]
        public void ToDateTimeOrDefault_WithString_IsEmpty ()
        {
            //Actual
            var actual = TypeConversion.ToDateTimeOrDefault("");

            //Assert
            actual.Should().Be(default(DateTime));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ToDateTimeOrDefault_WithString_IsBad ()
        {
            //Actual
            TypeConversion.ToDateTimeOrDefault("super");
        }

        [TestMethod]
        public void ToDateTimeOrDefault_WithString_CustomDefault ()
        {
            //Actual
            var expected = new DateTime(2012, 4, 16, 12, 34, 56);
            var actual = TypeConversion.ToDateTimeOrDefault("", expected);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion
        
        #region String (Exact)

        [TestMethod]
        public void ToDateTimeOrDefault_WithExactString_IsValid ()
        {
            var expected = new DateTime(2011, 1, 2);

            //Actual
            var actual = TypeConversion.ToDateTimeOrDefault("1/2/2011", "M/d/yyyy");

            //Assert
            actual.Should().BeDate(expected);
        }

        [TestMethod]
        public void ToDateTimeOrDefault_WithExactString_IsNull ()
        {
            //Actual
            var actual = TypeConversion.ToDateTimeOrDefault((string)null, "MM/dd/yyyy");

            //Assert
            actual.Should().Be(default(DateTime));
        }

        [TestMethod]
        public void ToDateTimeOrDefault_WithExactString_IsEmpty ()
        {
            //Actual
            var actual = TypeConversion.ToDateTimeOrDefault("", "MM/dd/yyyy");

            //Assert
            actual.Should().Be(default(DateTime));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ToDateTimeOrDefault_WithExactString_IsBad ()
        {
            //Actual
            TypeConversion.ToDateTimeOrDefault("1/2/2011", "MM/dd/yyyy hh:mm:ss");
        }

        [TestMethod]
        public void ToDateTimeOrDefault_WithExactString_CustomDefault ()
        {
            //Actual
            var expected = new DateTime(2012, 4, 16, 12, 34, 56);
            var actual = TypeConversion.ToDateTimeOrDefault("", "MM/dd/yyyy", expected);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Object

        [TestMethod]
        public void ToDateTimeOrDefault_WithObject_IsNull ()
        {
            object value = null;

            //Actual
            var actual = TypeConversion.ToDateTimeOrDefault(value);

            //Assert
            actual.Should().Be(default(DateTime));
        }

        [TestMethod]
        public void ToDateTimeOrDefault_WithObject_IsDBNull ()
        {
            //Actual
            var actual = TypeConversion.ToDateTimeOrDefault(DBNull.Value);

            //Assert
            actual.Should().Be(default(DateTime));
        }

        [TestMethod]
        public void ToDateTimeOrDefault_WithObject_IsString ()
        {
            var expected = new DateTime(2010, 5, 6);
            object value = expected.ToShortDateString();

            //Actual
            var actual = TypeConversion.ToDateTimeOrDefault(value);

            //Assert
            actual.Should().BeDate(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ToDateTimeOrDefault_WithObject_IsNotValid ()
        {
            object value = "blah";

            //Actual
            TypeConversion.ToDateTimeOrDefault(value);
        }

        [TestMethod]
        public void ToDateTimeOrDefault_WithObject_CustomDefault ()
        {
            //Actual
            var expected = new DateTime(2012, 4, 16, 12, 34, 56);
            var actual = TypeConversion.ToDateTimeOrDefault(DBNull.Value, expected);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #endregion

        #region ToDecimalOrDefault

        #region String

        [TestMethod]
        public void ToDecimalOrDefault_WithString_IsInRange ()
        {
            //Actual
            var actual = TypeConversion.ToDecimalOrDefault("10000000");

            //Assert
            actual.Should().Be(10000000);
        }

        [TestMethod]
        public void ToDecimalOrDefault_WithString_IsNull ()
        {
            //Actual
            var actual = TypeConversion.ToDecimalOrDefault((string)null);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToDecimalOrDefault_WithString_IsEmpty ()
        {
            //Actual
            var actual = TypeConversion.ToDecimalOrDefault("");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ToDecimalOrDefault_WithString_IsOutOfRange ()
        {
            //Actual
            TypeConversion.ToInt64OrDefault(Decimal.MaxValue.ToString() + "0");
        }

        [TestMethod]
        public void ToDecimalOrDefault_WithString_CustomDefault ()
        {
            //Actual
            var expected = 12345678M;
            var actual = TypeConversion.ToDecimalOrDefault("", expected);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Object

        [TestMethod]
        public void ToDecimalOrDefault_WithObject_IsNumeric ()
        {
            object value = 100000000;

            //Actual
            var actual = TypeConversion.ToDecimalOrDefault(value);

            //Assert
            actual.Should().Be(100000000);
        }

        [TestMethod]
        public void ToDecimalOrDefault_WithObject_IsNull ()
        {
            object value = null;

            //Actual
            var actual = TypeConversion.ToDecimalOrDefault(value);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToDecimalOrDefault_WithObject_IsDBNull ()
        {
            //Actual
            var actual = TypeConversion.ToDecimalOrDefault(DBNull.Value);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void ToDecimalOrDefault_WithObject_IsString ()
        {
            object value = "5";

            //Actual
            var actual = TypeConversion.ToDecimalOrDefault(value);

            //Assert
            actual.Should().Be(5);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ToDecimalOrDefault_WithObject_IsNotValid ()
        {
            object value = "blah";

            //Actual
            TypeConversion.ToDecimalOrDefault(value);
        }

        [TestMethod]
        public void ToDecimalOrDefault_WithObject_CustomDefault ()
        {
            //Actual
            var expected = 9876543M;
            var actual = TypeConversion.ToDecimalOrDefault(DBNull.Value, expected);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #endregion

        #region ToStringOrEmpty
      
        [TestMethod]
        public void ToStringOrEmpty_WithObject_IsString ()
        {
            object expected = "abc";

            //Actual
            var actual = TypeConversion.ToStringOrEmpty(expected);

            //Assert
            actual.Should().Be((string)expected);
        }

        [TestMethod]
        public void ToStringOrEmpty_WithObject_IsNull ()
        {
            //Actual
            var actual = TypeConversion.ToStringOrEmpty(null);

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void ToStringOrEmpty_WithObject_IsDBNull ()
        {
            //Actual
            var actual = TypeConversion.ToStringOrEmpty(DBNull.Value);

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void ToStringOrEmpty_WithObject_IsNumeric ()
        {
            //Actual
            var actual = TypeConversion.ToStringOrEmpty(45);

            //Assert
            actual.Should().Be("45");
        }

        [TestMethod]
        public void ToStringOrEmpty_WithObject_CustomDefault ()
        {
            //Actual
            var expected = "None";
            var actual = TypeConversion.ToStringOrEmpty(DBNull.Value, expected);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region TryConvertToBoolean

        [TestMethod]
        public void TryConvertToBoolean_WithNull ()
        {
            object target = null;

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeFalse();
            actualResult.Should().BeFalse();
        }

        [TestMethod]
        public void TryConvertToBoolean_WithDBNull ()
        {
            object target = DBNull.Value;

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeFalse();
            actualResult.Should().BeFalse();
        }

        [TestMethod]
        public void TryConvertToBoolean_WithBoolean_True ()
        {
            var target = true;

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeTrue();
            actualResult.Should().BeTrue();
        }

        [TestMethod]
        public void TryConvertToBoolean_WithBoolean_False ()
        {
            var target = false;

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeFalse();
            actualResult.Should().BeTrue();
        }

        [TestMethod]
        public void TryConvertToBoolean_WithString_Empty ()
        {
            var target = "";

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeFalse();
            actualResult.Should().BeFalse();
        }

        [TestMethod]
        public void TryConvertToBoolean_WithString_0 ()
        {
            var target = "0";

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeFalse();
            actualResult.Should().BeTrue();
        }

        [TestMethod]
        public void TryConvertToBoolean_WithString_1 ()
        {
            var target = "1";

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeTrue();
            actualResult.Should().BeTrue();
        }

        [TestMethod]
        public void TryConvertToBoolean_WithString_False ()
        {
            var target = "false";

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeFalse();
            actualResult.Should().BeTrue();
        }

        [TestMethod]
        public void TryConvertToBoolean_WithString_True ()
        {
            var target = "true";

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeTrue();
            actualResult.Should().BeTrue();
        }

        [TestMethod]
        public void TryConvertToBoolean_WithString_Yes ()
        {
            var target = "yes";

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeTrue();
            actualResult.Should().BeTrue();
        }

        [TestMethod]
        public void TryConvertToBoolean_WithString_No ()
        {
            var target = "no";

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeFalse();
            actualResult.Should().BeTrue();
        }

        [TestMethod]
        public void TryConvertToBoolean_WithInt64_0 ()
        {
            var target = 0L;

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeFalse();
            actualResult.Should().BeTrue();
        }

        [TestMethod]
        public void TryConvertToBoolean_WithInt64_1 ()
        {
            var target = 1L;

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeTrue();
            actualResult.Should().BeTrue();
        }

        [TestMethod]
        public void TryConvertToBoolean_WithInt32_0 ()
        {
            var target = 0;

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeFalse();
            actualResult.Should().BeTrue();
        }

        [TestMethod]
        public void TryConvertToBoolean_WithInt32_1 ()
        {
            var target = 1;

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeTrue();
            actualResult.Should().BeTrue();
        }

        [TestMethod]
        public void TryConvertToBoolean_WithInt16_0 ()
        {
            var target = (short)0;

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeFalse();
            actualResult.Should().BeTrue();
        }

        [TestMethod]
        public void TryConvertToBoolean_WithInt16_1 ()
        {
            var target = (short)1;

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeTrue();
            actualResult.Should().BeTrue();
        }

        [TestMethod]
        public void TryConvertToBoolean_WithInt8_0 ()
        {
            var target = (sbyte)0;

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeFalse();
            actualResult.Should().BeTrue();
        }

        [TestMethod]
        public void TryConvertToBoolean_WithInt8_1 ()
        {
            var target = (sbyte)1;

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeTrue();
            actualResult.Should().BeTrue();
        }

        [TestMethod]
        public void TryConvertToBoolean_WithUInt64_0 ()
        {
            var target = 0UL;

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeFalse();
            actualResult.Should().BeTrue();
        }

        [TestMethod]
        public void TryConvertToBoolean_WithUInt64_1 ()
        {
            var target = 1UL;

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeTrue();
            actualResult.Should().BeTrue();
        }

        [TestMethod]
        public void TryConvertToBoolean_WithUInt32_0 ()
        {
            var target = 0U;

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeFalse();
            actualResult.Should().BeTrue();
        }

        [TestMethod]
        public void TryConvertToBoolean_WithUInt32_1 ()
        {
            var target = 1U;

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeTrue();
            actualResult.Should().BeTrue();
        }

        [TestMethod]
        public void TryConvertToBoolean_WithUInt16_0 ()
        {
            var target = (ushort)0;

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeFalse();
            actualResult.Should().BeTrue();
        }

        [TestMethod]
        public void TryConvertToBoolean_WithUInt16_1 ()
        {
            var target = (ushort)1;

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeTrue();
            actualResult.Should().BeTrue();
        }

        [TestMethod]
        public void TryConvertToBoolean_WithUInt8_0 ()
        {
            var target = (byte)0;

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeFalse();
            actualResult.Should().BeTrue();
        }

        [TestMethod]
        public void TryConvertToBoolean_WithUInt8_1 ()
        {
            var target = (byte)1;

            var actualResult = TypeConversion.TryConvertToBoolean(target, out bool actual);

            actual.Should().BeTrue();
            actualResult.Should().BeTrue();
        }
        #endregion
    }
}
