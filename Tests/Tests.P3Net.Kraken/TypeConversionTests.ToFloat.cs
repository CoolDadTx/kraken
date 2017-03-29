#region Imports

using System;
using System.Collections.Generic;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken
{
    public partial class TypeConversionTests
    {
        #region ToDoubleOrDefault

        #region String

        [TestMethod]
        public void ToDoubleOrDefault_WithString_IsInRange ()
        {
            //Actual
            var actual = TypeConversion.ToDoubleOrDefault("123.456");

            //Assert
            actual.Should().BeApproximately(123.456);
        }

        [TestMethod]
        public void ToDoubleOrDefault_WithString_IsNull ()
        {
            //Actual
            var actual = TypeConversion.ToDoubleOrDefault((string)null);

            //Assert
            actual.Should().BeExactly(0);
        }

        [TestMethod]
        public void ToDoubleOrDefault_WithString_IsEmpty ()
        {
            //Actual
            var actual = TypeConversion.ToDoubleOrDefault("");

            //Assert
            actual.Should().BeExactly(0);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ToDoubleOrDefault_WithString_IsOutOfRange ()
        {
            //Actual
            TypeConversion.ToDoubleOrDefault(Double.MaxValue.ToString() + "0");
        }

        [TestMethod]
        public void ToDoubleOrDefault_WithString_CustomDefault ()
        {
            //Actual
            var expected = 1234.5678;
            var actual = TypeConversion.ToDoubleOrDefault("", expected);

            //Assert
            actual.Should().BeApproximately(expected);
        }
        #endregion

        #region Object

        [TestMethod]
        public void ToDoubleOrDefault_WithObject_IsNumeric ()
        {
            object value = 123.456;

            //Actual
            var actual = TypeConversion.ToDoubleOrDefault(value);

            //Assert
            actual.Should().BeApproximately(123.456);
        }

        [TestMethod]
        public void ToDoubleOrDefault_WithObject_IsNull ()
        {
            object value = null;

            //Actual
            var actual = TypeConversion.ToDoubleOrDefault(value);

            //Assert
            actual.Should().BeApproximately(0);
        }

        [TestMethod]
        public void ToDoubleOrDefault_WithObject_IsDBNull ()
        {
            //Actual
            var actual = TypeConversion.ToDoubleOrDefault(DBNull.Value);

            //Assert
            actual.Should().BeApproximately(0);
        }

        [TestMethod]
        public void ToDoubleOrDefault_WithObject_IsString ()
        {
            object value = "5";

            //Actual
            var actual = TypeConversion.ToDoubleOrDefault(value);

            //Assert
            actual.Should().BeApproximately(5);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ToDoubleOrDefault_WithObject_IsNotValid ()
        {
            object value = "blah";

            //Actual
            TypeConversion.ToDoubleOrDefault(value);
        }

        [TestMethod]
        public void ToDoubleOrDefault_WithObject_CustomDefault ()
        {
            //Actual
            var expected = 1234.5678;
            var actual = TypeConversion.ToDoubleOrDefault(DBNull.Value, expected);

            //Assert
            actual.Should().BeApproximately(expected);
        }
        #endregion

        #endregion

        #region ToSingleOrDefault

        #region String

        [TestMethod]
        public void ToSingleOrDefault_WithString_IsInRange ()
        {
            //Actual
            var actual = TypeConversion.ToSingleOrDefault("123.456");

            //Assert
            actual.Should().BeApproximately(123.456F);
        }

        [TestMethod]
        public void ToSingleOrDefault_WithString_IsNull ()
        {
            //Actual
            var actual = TypeConversion.ToSingleOrDefault((string)null);

            //Assert
            actual.Should().BeExactly(0);
        }

        [TestMethod]
        public void ToSingleOrDefault_WithString_IsEmpty ()
        {
            //Actual
            var actual = TypeConversion.ToSingleOrDefault("");

            //Assert
            actual.Should().BeExactly(0);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ToSingleOrDefault_WithString_IsOutOfRange ()
        {
            //Actual
            TypeConversion.ToSingleOrDefault(Double.MaxValue.ToString());
        }

        [TestMethod]
        public void ToSingleOrDefault_WithString_CustomDefault ()
        {
            //Actual
            float expected = 45.67F;
            var actual = TypeConversion.ToSingleOrDefault("", expected);

            //Assert
            actual.Should().BeApproximately(expected);
        }
        #endregion

        #region Object

        [TestMethod]
        public void ToSingleOrDefault_WithObject_IsNumeric ()
        {
            object value = 123.456;

            //Actual
            var actual = TypeConversion.ToSingleOrDefault(value);

            //Assert
            actual.Should().BeApproximately(123.456F);
        }

        [TestMethod]
        public void ToSingleOrDefault_WithObject_IsNull ()
        {
            object value = null;

            //Actual
            var actual = TypeConversion.ToSingleOrDefault(value);

            //Assert
            actual.Should().BeExactly(0);
        }

        [TestMethod]
        public void ToSingleOrDefault_WithObject_IsDBNull ()
        {
            //Actual
            var actual = TypeConversion.ToSingleOrDefault(DBNull.Value);

            //Assert
            actual.Should().BeExactly(0);
        }

        [TestMethod]
        public void ToSingleOrDefault_WithObject_IsString ()
        {
            object value = "5";

            //Actual
            var actual = TypeConversion.ToSingleOrDefault(value);

            //Assert
            actual.Should().BeApproximately(5);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ToSingleOrDefault_WithObject_IsNotValid ()
        {
            object value = "blah";

            //Actual
            TypeConversion.ToSingleOrDefault(value);
        }

        [TestMethod]
        public void ToSingleOrDefault_WithObject_CustomDefault ()
        {
            //Actual
            float expected = 12.34F;
            var actual = TypeConversion.ToSingleOrDefault(DBNull.Value, expected);

            //Assert
            actual.Should().BeApproximately(expected);
        }
        #endregion

        #endregion
    }
}
