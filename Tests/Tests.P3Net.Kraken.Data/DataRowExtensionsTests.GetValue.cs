#region Imports

using System;
using System.Data;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using P3Net.Kraken;
using P3Net.Kraken.Data;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken.Data
{
    public partial class DataRowExtensionsTests
    {
        #region GetBooleanValueOrDefault

        [TestMethod]
        public void GetBooleanValueOrDefault_ColumnIsValidAndSet ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(bool), true));

            //Act
            var actual = target.GetBooleanValueOrDefault("Column1");

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void GetBooleanValueOrDefault_ColumnIsValidAndNull ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(bool)));

            //Act
            var actual = target.GetBooleanValueOrDefault("Column1");

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void GetBooleanValueOrDefault_ColumnValueIsNullUseCustomDefault ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(bool)));

            //Act
            var actual = target.GetBooleanValueOrDefault("Column1", true);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void GetBooleanValueOrDefault_ColumnValueIsInvalid ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            //Act
            var actual = target.GetBooleanValueOrDefault("Column1");

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetBooleanValueOrDefault_ColumnIsMissing ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetBooleanValueOrDefault("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetBooleanValueOrDefault_ColumnIsNull ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetBooleanValueOrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetBooleanValueOrDefault_ColumnIsEmpty ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetBooleanValueOrDefault("");
        }

        [TestMethod]
        public void GetBooleanValueOrDefault_ColumnIsYes ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Yes"));

            //Act
            var actual = target.GetBooleanValueOrDefault("Column1");

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void GetBooleanValueOrDefault_ColumnIsNo ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "No"));

            //Act
            var actual = target.GetBooleanValueOrDefault("Column1", true);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void GetBooleanValueOrDefault_ColumnIsInt8 ()
        {
            var target = CreateRow(new ColumnDefinition("Column1", typeof(sbyte), 4));

            var actual = target.GetBooleanValueOrDefault("Column1");

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void GetBooleanValueOrDefault_ColumnIsInt16 ()
        {
            var target = CreateRow(new ColumnDefinition("Column1", typeof(short), 4));

            var actual = target.GetBooleanValueOrDefault("Column1");

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void GetBooleanValueOrDefault_ColumnIsInt32 ()
        {
            var target = CreateRow(new ColumnDefinition("Column1", typeof(int), 654321));

            var actual = target.GetBooleanValueOrDefault("Column1");

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void GetBooleanValueOrDefault_ColumnIsInt64 ()
        {
            var target = CreateRow(new ColumnDefinition("Column1", typeof(long), 567890123));

            var actual = target.GetBooleanValueOrDefault("Column1");

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void GetBooleanValueOrDefault_ColumnIsUInt8 ()
        {
            var target = CreateRow(new ColumnDefinition("Column1", typeof(byte), 4));

            var actual = target.GetBooleanValueOrDefault("Column1");

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void GetBooleanValueOrDefault_ColumnIsUInt16 ()
        {
            var target = CreateRow(new ColumnDefinition("Column1", typeof(ushort), 4));

            var actual = target.GetBooleanValueOrDefault("Column1");

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void GetBooleanValueOrDefault_ColumnIsUInt32 ()
        {
            var target = CreateRow(new ColumnDefinition("Column1", typeof(uint), 654321));

            var actual = target.GetBooleanValueOrDefault("Column1");

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void GetBooleanValueOrDefault_ColumnIsUInt64 ()
        {
            var target = CreateRow(new ColumnDefinition("Column1", typeof(ulong), 567890123));

            var actual = target.GetBooleanValueOrDefault("Column1");

            actual.Should().BeTrue();
        }
        #endregion

        #region GetByteValueOrDefault

        [TestMethod]
        public void GetByteValueOrDefault_ColumnIsValidAndSet ()
        {
            //Arrange
            byte expected = 5;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(byte), expected));

            //Act
            var actual = target.GetByteValueOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetByteValueOrDefault_ColumnIsValidAndNull ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(byte)));

            //Act
            var actual = target.GetByteValueOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void GetByteValueOrDefault_ColumnValueIsNullUseCustomDefault ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(byte)));

            //Act
            byte expected = 123;
            var actual = target.GetByteValueOrDefault("Column1", expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetByteValueOrDefault_ColumnValueIsInvalid ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            //Act
            var actual = target.GetByteValueOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetByteValueOrDefault_ColumnIsMissing ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetByteValueOrDefault("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetByteValueOrDefault_ColumnIsNull ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetByteValueOrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetByteValueOrDefault_ColumnIsEmpty ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetByteValueOrDefault("");
        }

        [TestMethod]
        public void GetByteValueOrDefault_ColumnValueIsTooLarge ()
        {
            //Arrange
            ushort expected = Byte.MaxValue + 1;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(ushort), expected));

            //Act
            var actual = target.GetByteValueOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void GetByteValueOrDefault_ColumnIsLargerButValueFits ()
        {
            //Arrange
            byte expected = Byte.MaxValue;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(ushort), expected));

            //Act
            var actual = target.GetByteValueOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region GetCharValueOrDefault

        [TestMethod]
        public void GetCharValueOrDefault_ColumnIsValidAndSet ()
        {
            //Arrange
            var expected = 'D';
            var target = CreateRow(new ColumnDefinition("Column1", typeof(char), expected));

            //Act
            var actual = target.GetCharValueOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetCharValueOrDefault_ColumnIsValidAndNull ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(char)));

            //Act
            var actual = target.GetCharValueOrDefault("Column1");

            //Assert
            actual.Should().Be(default(char));
        }

        [TestMethod]
        public void GetCharValueOrDefault_ColumnValueIsNullUseCustomDefault ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(char)));

            //Act
            var expected = 'X';
            var actual = target.GetCharValueOrDefault("Column1", expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetCharValueOrDefault_ColumnValueIsInvalid ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            //Act
            var actual = target.GetCharValueOrDefault("Column1");

            //Assert
            actual.Should().Be(default(char));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCharValueOrDefault_ColumnIsMissing ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetCharValueOrDefault("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetCharValueOrDefault_ColumnIsNull ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetCharValueOrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCharValueOrDefault_ColumnIsEmpty ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetCharValueOrDefault("");
        }
        #endregion

        #region GetDateValueOrDefault

        [TestMethod]
        public void GetDateValueOrDefault_ColumnIsValidAndSet ()
        {
            var expected = Dates.March(4, 1980);
            var target = CreateRow(new ColumnDefinition("Column1", typeof(Date), expected));

            var actual = target.GetDateValueOrDefault("Column1");

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetDateValueOrDefault_ColumnIsValidAndNull ()
        {
            var target = CreateRow(new ColumnDefinition("Column1", typeof(Date)));

            var actual = target.GetDateValueOrDefault("Column1");

            actual.Should().Be(Date.None);
        }

        [TestMethod]
        public void GetDateValueOrDefault_ColumnValueIsNullUseCustomDefault ()
        {
            var target = CreateRow(new ColumnDefinition("Column1", typeof(Date)));

            var expected = Dates.April(10, 1976);
            var actual = target.GetDateValueOrDefault("Column1", expected);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetDateValueOrDefault_ColumnValueIsInvalid ()
        {
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            var actual = target.GetDateValueOrDefault("Column1");

            actual.Should().Be(Date.None);
        }

        [TestMethod]
        public void GetDateValueOrDefault_ColumnIsMissing ()
        {
            var target = CreateRow();

            Action action = () => target.GetDateValueOrDefault("Column1");

            action.ShouldThrowArgumentException();
        }

        [TestMethod]
        public void GetDateValueOrDefault_ColumnIsNull ()
        {
            var target = CreateRow();

            Action action = () => target.GetDateValueOrDefault(null);

            action.ShouldThrowArgumentNullException();
        }

        [TestMethod]
        public void GetDateValueOrDefault_ColumnIsEmpty ()
        {
            var target = CreateRow();

            Action action = () => target.GetDateValueOrDefault("");

            action.ShouldThrowArgumentException();
        }
        #endregion

        #region GetDateTimeValueOrDefault

        [TestMethod]
        public void GetDateTimeValueOrDefault_ColumnIsValidAndSet ()
        {
            //Arrange
            var expected = new DateTime(2012, 4, 17, 12, 34, 56);
            var target = CreateRow(new ColumnDefinition("Column1", typeof(DateTime), expected));

            //Act
            var actual = target.GetDateTimeValueOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetDateTimeValueOrDefault_ColumnIsValidAndNull ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(DateTime)));

            //Act
            var actual = target.GetDateTimeValueOrDefault("Column1");

            //Assert
            actual.Should().Be(DateTime.MinValue);
        }

        [TestMethod]
        public void GetDateTimeValueOrDefault_ColumnValueIsNullUseCustomDefault ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(DateTime)));

            //Act
            var expected = new DateTime(2012, 4, 17, 12, 34, 56);
            var actual = target.GetDateTimeValueOrDefault("Column1", expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetDateTimeValueOrDefault_ColumnValueIsInvalid ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            //Act
            var actual = target.GetDateTimeValueOrDefault("Column1");

            //Assert
            actual.Should().Be(DateTime.MinValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDateTimeValueOrDefault_ColumnIsMissing ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetDateTimeValueOrDefault("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetDateTimeValueOrDefault_ColumnIsNull ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetDateTimeValueOrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDateTimeValueOrDefault_ColumnIsEmpty ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetDateTimeValueOrDefault("");
        }
        #endregion

        #region GetDecimalValueOrDefault

        [TestMethod]
        public void GetDecimalValueOrDefault_ColumnIsValidAndSet ()
        {
            //Arrange
            decimal expected = 123456789M;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(decimal), expected));

            //Act
            var actual = target.GetDecimalValueOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetDecimalValueOrDefault_ColumnIsValidAndNull ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(decimal)));

            //Act
            var actual = target.GetDecimalValueOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void GetDecimalValueOrDefault_ColumnValueIsNullUseCustomDefault ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(decimal)));

            //Act
            var expected = 76543M;
            var actual = target.GetDecimalValueOrDefault("Column1", expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetDecimalValueOrDefault_ColumnValueIsInvalid ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            //Act
            var actual = target.GetDecimalValueOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDecimalValueOrDefault_ColumnIsMissing ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetDecimalValueOrDefault("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetDecimalValueOrDefault_ColumnIsNull ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetDecimalValueOrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDecimalValueOrDefault_ColumnIsEmpty ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetDecimalValueOrDefault("");
        }

        [TestMethod]
        public void GetDecimalValueOrDefault_ColumnIsSmaller ()
        {
            //Arrange
            int expected = 34;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(int), expected));

            //Act
            var actual = target.GetDecimalValueOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region GetDoubleValueOrDefault

        [TestMethod]
        public void GetDoubleValueOrDefault_ColumnIsValidAndSet ()
        {
            //Arrange
            double expected = 1234.5678;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(double), expected));

            //Act
            var actual = target.GetDoubleValueOrDefault("Column1");

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        public void GetDoubleValueOrDefault_ColumnIsValidAndNull ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(double)));

            //Act
            var actual = target.GetDoubleValueOrDefault("Column1");

            //Assert
            actual.Should().BeExactly(0);
        }

        [TestMethod]
        public void GetDoubleValueOrDefault_ColumnValueIsNullUseCustomDefault ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(double)));

            //Act
            var expected = 567.89;
            var actual = target.GetDoubleValueOrDefault("Column1", expected);

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        public void GetDoubleValueOrDefault_ColumnValueIsInvalid ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            //Act
            var actual = target.GetDoubleValueOrDefault("Column1");

            //Assert
            actual.Should().BeExactly(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDoubleValueOrDefault_ColumnIsMissing ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetDoubleValueOrDefault("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetDoubleValueOrDefault_ColumnIsNull ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetDoubleValueOrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDoubleValueOrDefault_ColumnIsEmpty ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetDoubleValueOrDefault("");
        }

        [TestMethod]
        public void GetDoubleValueOrDefault_ColumnIsSmaller ()
        {
            //Arrange
            int expected = 34;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(int), expected));

            //Act
            var actual = target.GetDoubleValueOrDefault("Column1");

            //Assert
            actual.Should().BeExactly(expected);
        }
        #endregion

        #region GetInt16ValueOrDefault

        [TestMethod]
        public void GetInt16ValueOrDefault_ColumnIsValidAndSet ()
        {
            //Arrange
            short expected = 45;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(short), expected));

            //Act
            var actual = target.GetInt16ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetInt16ValueOrDefault_ColumnIsValidAndNull ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(short)));

            //Act
            var actual = target.GetInt16ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void GetInt16ValueOrDefault_ColumnValueIsNullUseCustomDefault ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(short)));

            //Act
            short expected = 7654;
            var actual = target.GetInt16ValueOrDefault("Column1", expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetInt16ValueOrDefault_ColumnValueIsInvalid ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            //Act
            var actual = target.GetInt16ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInt16ValueOrDefault_ColumnIsMissing ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetInt16ValueOrDefault("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetInt16ValueOrDefault_ColumnIsNull ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetInt16ValueOrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInt16ValueOrDefault_ColumnIsEmpty ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetInt16ValueOrDefault("");
        }

        [TestMethod]
        public void GetInt16ValueOrDefault_ColumnIsSmaller ()
        {
            //Arrange
            sbyte expected = 34;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(sbyte), expected));

            //Act
            var actual = target.GetInt16ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetInt16ValueOrDefault_ColumnIsLargerButFits ()
        {
            //Arrange
            short expected = Int16.MaxValue;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(int), expected));

            //Act
            var actual = target.GetInt16ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetInt16ValueOrDefault_ColumnIsLargerAndDoesNotFit ()
        {
            //Arrange
            int expected = Int16.MaxValue + 1;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(int), expected));

            //Act
            var actual = target.GetInt16ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }
        #endregion

        #region GetInt32ValueOrDefault

        [TestMethod]
        public void GetInt32ValueOrDefault_ColumnIsValidAndSet ()
        {
            //Arrange
            int expected = 1234;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(int), expected));

            //Act
            var actual = target.GetInt32ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetInt32ValueOrDefault_ColumnIsValidAndNull ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(int)));

            //Act
            var actual = target.GetInt32ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void GetInt32ValueOrDefault_ColumnValueIsNullUseCustomDefault ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(int)));

            //Act
            var expected = 97532;
            var actual = target.GetInt32ValueOrDefault("Column1", expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetInt32ValueOrDefault_ColumnValueIsInvalid ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            //Act
            var actual = target.GetInt32ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInt32ValueOrDefault_ColumnIsMissing ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetInt32ValueOrDefault("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetInt32ValueOrDefault_ColumnIsNull ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetInt32ValueOrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInt32ValueOrDefault_ColumnIsEmpty ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetInt32ValueOrDefault("");
        }

        [TestMethod]
        public void GetInt32ValueOrDefault_ColumnIsSmaller ()
        {
            //Arrange
            short expected = Int16.MaxValue;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(short), expected));

            //Act
            var actual = target.GetInt32ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetInt32ValueOrDefault_ColumnIsLargerButFits ()
        {
            //Arrange
            int expected = Int32.MaxValue - 1;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(int), expected));

            //Act
            var actual = target.GetInt32ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetInt32ValueOrDefault_ColumnIsLargerAndDoesNotFit ()
        {
            //Arrange
            long expected = Int32.MaxValue + 1L;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(long), expected));

            //Act
            var actual = target.GetInt32ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }
        #endregion

        #region GetInt64ValueOrDefault

        [TestMethod]
        public void GetInt64ValueOrDefault_ColumnIsValidAndSet ()
        {
            //Arrange
            long expected = 123456789L;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(long), expected));

            //Act
            var actual = target.GetInt64ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetInt64ValueOrDefault_ColumnIsValidAndNull ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(long)));

            //Act
            var actual = target.GetInt64ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void GetInt64ValueOrDefault_ColumnValueIsNullUseCustomDefault ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(long)));

            //Act
            var expected = 9753108L;
            var actual = target.GetInt64ValueOrDefault("Column1", expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetInt64ValueOrDefault_ColumnValueIsInvalid ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            //Act
            var actual = target.GetInt64ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInt64ValueOrDefault_ColumnIsMissing ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetInt64ValueOrDefault("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetInt64ValueOrDefault_ColumnIsNull ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetInt64ValueOrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInt64ValueOrDefault_ColumnIsEmpty ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetInt64ValueOrDefault("");
        }

        [TestMethod]
        public void GetInt64ValueOrDefault_ColumnIsSmaller ()
        {
            //Arrange
            int expected = 654321;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(int), expected));

            //Act
            var actual = target.GetInt64ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region GetSByteValueOrDefault

        [TestMethod]
        public void GetSByteValueOrDefault_ColumnIsValidAndSet ()
        {
            //Arrange
            sbyte expected = -45;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(sbyte), expected));

            //Act
            var actual = target.GetSByteValueOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetSByteValueOrDefault_ColumnIsValidAndNull ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(sbyte)));

            //Act
            var actual = target.GetSByteValueOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void GetSByteValueOrDefault_ColumnValueIsNullUseCustomDefault ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(sbyte)));

            //Act
            sbyte expected = -89;
            var actual = target.GetSByteValueOrDefault("Column1", expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetSByteValueOrDefault_ColumnValueIsInvalid ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            //Act
            var actual = target.GetSByteValueOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetSByteValueOrDefault_ColumnIsMissing ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetSByteValueOrDefault("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetSByteValueOrDefault_ColumnIsNull ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetSByteValueOrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetSByteValueOrDefault_ColumnIsEmpty ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetSByteValueOrDefault("");
        }

        [TestMethod]
        public void GetSByteValueOrDefault_ColumnIsLargerButFits ()
        {
            //Arrange
            sbyte expected = -100;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(short), expected));

            //Act
            var actual = target.GetSByteValueOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetSByteValueOrDefault_ColumnIsLargerAndDoesNotFit ()
        {
            //Arrange
            int expected = SByte.MaxValue + 1;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(int), expected));

            //Act
            var actual = target.GetSByteValueOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }
        #endregion

        #region GetSingleValueOrDefault

        [TestMethod]
        public void GetSingleValueOrDefault_ColumnIsValidAndSet ()
        {
            //Arrange
            var expected = 9876.0F;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(float), expected));

            //Act
            var actual = target.GetSingleValueOrDefault("Column1");

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        public void GetSingleValueOrDefault_ColumnIsValidAndNull ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(float)));

            //Act
            var actual = target.GetSingleValueOrDefault("Column1");

            //Assert
            actual.Should().BeExactly(0);
        }

        [TestMethod]
        public void GetSingleValueOrDefault_ColumnValueIsNullUseCustomDefault ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(float)));

            //Act
            var expected = 7531.246F;
            var actual = target.GetSingleValueOrDefault("Column1", expected);

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        public void GetSingleValueOrDefault_ColumnValueIsInvalid ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            //Act
            var actual = target.GetSingleValueOrDefault("Column1");

            //Assert
            actual.Should().BeExactly(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetSingleValueOrDefault_ColumnIsMissing ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetSingleValueOrDefault("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetSingleValueOrDefault_ColumnIsNull ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetSingleValueOrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetSingleValueOrDefault_ColumnIsEmpty ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetSingleValueOrDefault("");
        }

        [TestMethod]
        public void GetSingleValueOrDefault_ColumnIsSmaller ()
        {
            //Arrange
            int expected = 54321;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(int), expected));

            //Act
            var actual = target.GetSingleValueOrDefault("Column1");

            //Assert
            actual.Should().BeExactly(expected);
        }

        [TestMethod]
        public void GetSingleValueOrDefault_ColumnIsLargerButFits ()
        {
            //Arrange
            float expected = 9876.5432F;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(double), expected));

            //Act
            var actual = target.GetSingleValueOrDefault("Column1");

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        public void GetSingleValueOrDefault_ColumnIsLargerAndDoesNotFit ()
        {
            //Arrange
            var expected = 10E100;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(double), expected));

            //Act
            var actual = target.GetSingleValueOrDefault("Column1");

            //Assert
            actual.Should().BeExactly(0);
        }
        #endregion

        #region GetStringValueOrDefault

        [TestMethod]
        public void GetStringValueOrDefault_ColumnIsValidAndSet ()
        {
            //Arrange
            var expected = "Hello";
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), expected));

            //Act
            var actual = target.GetStringValueOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetStringValueOrDefault_ColumnIsValidAndNull ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string)));

            //Act
            var actual = target.GetStringValueOrDefault("Column1");

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void GetStringValueOrDefault_ColumnValueIsNullUseCustomDefault ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string)));

            //Act
            var expected = "unknown";
            var actual = target.GetStringValueOrDefault("Column1", expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetStringValueOrDefault_ColumnIsMissing ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetStringValueOrDefault("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetStringValueOrDefault_ColumnIsNull ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetStringValueOrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetStringValueOrDefault_ColumnIsEmpty ()
        {
            //Arrange            
            var target = CreateRow();

            //Act
            target.GetStringValueOrDefault("");
        }

        [TestMethod]
        public void GetStringValueOrDefault_ColumnIsNotStringSucceeds ()
        {
            //Arrange
            var expected = 45;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(int), expected));

            //Act
            var actual = target.GetStringValueOrDefault("Column1");

            //Assert
            actual.Should().Be(expected.ToString());
        }
        #endregion

        #region GetUInt16ValueOrDefault

        [TestMethod]
        public void GetUInt16ValueOrDefault_ColumnIsValidAndSet ()
        {
            //Arrange
            ushort expected = 65432;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(ushort), expected));

            //Act
            var actual = target.GetUInt16ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetUInt16ValueOrDefault_ColumnIsValidAndNull ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(ushort)));

            //Act
            var actual = target.GetUInt16ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void GetUInt16ValueOrDefault_ColumnValueIsNullUseCustomDefault ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(ushort)));

            //Act
            ushort expected = 43210;
            var actual = target.GetUInt16ValueOrDefault("Column1", expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetUInt16ValueOrDefault_ColumnValueIsInvalid ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            //Act
            var actual = target.GetUInt16ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetUInt16ValueOrDefault_ColumnIsMissing ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetUInt16ValueOrDefault("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetUInt16ValueOrDefault_ColumnIsNull ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetUInt16ValueOrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetUInt16ValueOrDefault_ColumnIsEmpty ()
        {
            //Arrange            
            var target = CreateRow();

            //Act
            target.GetUInt16ValueOrDefault("");
        }

        [TestMethod]
        public void GetUInt16ValueOrDefault_ColumnIsSmaller ()
        {
            //Arrange
            byte expected = 34;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(byte), expected));

            //Act
            var actual = target.GetUInt16ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetUInt16ValueOrDefault_ColumnIsLargerButFits ()
        {
            //Arrange
            ushort expected = UInt16.MaxValue - 1;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(uint), expected));

            //Act
            var actual = target.GetUInt16ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetUInt16ValueOrDefault_ColumnIsLargerAndDoesNotFit ()
        {
            //Arrange
            uint expected = UInt16.MaxValue + 1;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(uint), expected));

            //Act
            var actual = target.GetUInt16ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }
        #endregion

        #region GetUInt32ValueOrDefault

        [TestMethod]
        public void GetUInt32ValueOrDefault_ColumnIsValidAndSet ()
        {
            //Arrange
            var expected = 3456789U;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(uint), expected));

            //Act
            var actual = target.GetUInt32ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetUInt32ValueOrDefault_ColumnIsValidAndNull ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(uint)));

            //Act
            var actual = target.GetUInt32ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void GetUInt32ValueOrDefault_ColumnValueIsNullUseCustomDefault ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(uint)));

            //Act
            var expected = 3456789U;
            var actual = target.GetUInt32ValueOrDefault("Column1", expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetUInt32ValueOrDefault_ColumnValueIsInvalid ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            //Act
            var actual = target.GetUInt32ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetUInt32ValueOrDefault_ColumnIsMissing ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetUInt32ValueOrDefault("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetUInt32ValueOrDefault_ColumnIsNull ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetUInt32ValueOrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetUInt32ValueOrDefault_ColumnIsEmpty ()
        {
            //Arrange            
            var target = CreateRow();

            //Act
            target.GetUInt32ValueOrDefault("");
        }

        [TestMethod]
        public void GetUInt32ValueOrDefault_ColumnIsSmaller ()
        {
            //Arrange
            ushort expected = 65432;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(ushort), expected));

            //Act
            var actual = target.GetUInt32ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetUInt32ValueOrDefault_ColumnIsLargerButFits ()
        {
            //Arrange
            uint expected = UInt32.MaxValue - 1;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(ulong), expected));

            //Act
            var actual = target.GetUInt32ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetUInt32ValueOrDefault_ColumnIsLargerAndDoesNotFit ()
        {
            //Arrange
            ulong expected = UInt32.MaxValue + 1L;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(ulong), expected));

            //Act
            var actual = target.GetUInt32ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }
        #endregion

        #region GetUInt64ValueOrDefault

        [TestMethod]
        public void GetUInt64ValueOrDefault_ColumnIsValidAndSet ()
        {
            //Arrange
            ulong expected = 567890123UL;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(ulong), expected));

            //Act
            var actual = target.GetUInt64ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetUInt64ValueOrDefault_ColumnIsValidAndNull ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(ulong)));

            //Act
            var actual = target.GetUInt64ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void GetUInt64ValueOrDefault_ColumnValueIsNullUseCustomDefault ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(ulong)));

            //Act
            var expected = 98765432UL;
            var actual = target.GetUInt64ValueOrDefault("Column1", expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetUInt64ValueOrDefault_ColumnValueIsInvalid ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            //Act
            var actual = target.GetUInt64ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetUInt64ValueOrDefault_ColumnIsMissing ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetUInt64ValueOrDefault("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetUInt64ValueOrDefault_ColumnIsNull ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetUInt64ValueOrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetUInt64ValueOrDefault_ColumnIsEmpty ()
        {
            //Arrange
            var target = CreateRow();

            //Act
            target.GetUInt64ValueOrDefault("");
        }

        [TestMethod]
        public void GetUInt64ValueOrDefault_ColumnIsSmaller ()
        {
            //Arrange
            uint expected = 3456789U;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(uint), expected));

            //Act
            var actual = target.GetUInt64ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetUInt64ValueOrDefault_ColumnIsLargerAndDoesNotFit ()
        {
            //Arrange
            double expected = 9876.5432;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(double), expected));

            //Act
            var actual = target.GetUInt64ValueOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }
        #endregion
    }
}
