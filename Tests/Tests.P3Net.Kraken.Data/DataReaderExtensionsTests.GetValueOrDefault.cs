#region Imports

using System;
using System.Data;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Data;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken.Data
{
    public partial class DataReaderExtensionsTests
    {
        #region GetBooleanOrDefault

        [TestMethod]
        public void GetBooleanOrDefault_ColumnValueIsSet ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(bool))
                            .InsertRow(true);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetBooleanOrDefault("Column1");

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void GetBooleanOrDefault_ColumnValueIsNull ()
        {
            //Arrange         
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(bool))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetBooleanOrDefault("Column1");

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void GetBooleanOrDefault_IsNullWithCustomDefault ()
        {
            //Arrange         
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(bool))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetBooleanOrDefault("Column1", true);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetBooleanOrDefault_ColumnIsMissing ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(bool))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetBooleanOrDefault("Column2");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetBooleanOrDefault_ColumnIsNull ()
        {
            //Arrange
            var target = new TestDataReader(new DataTable());

            //Act
            target.Read();
            target.GetBooleanOrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetBooleanOrDefault_ColumnIsEmpty ()
        {
            //Arrange
            var target = new TestDataReader(new DataTable());

            //Act
            target.Read();
            target.GetBooleanOrDefault("");
        }

        [TestMethod]
        public void GetBooleanOrDefault_WithOrdinal_ColumnValueIsSet ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(bool))
                            .InsertRow(true);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetBooleanOrDefault(0);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void GetBooleanOrDefault_WithOrdinal_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(bool))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetBooleanOrDefault(0, true);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetBooleanOrDefault_WithOrdinal_TooSmall ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetBooleanOrDefault(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetBooleanOrDefault_WithOrdinal_TooLarge ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(bool))
                            .InsertRow(true);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetBooleanOrDefault(1);
        }
        #endregion

        #region GetByteOrDefault

        [TestMethod]
        public void GetByteOrDefault_ColumnValueIsSet ()
        {
            //Arrange
            byte expected = 5;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(byte))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetByteOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetByteOrDefault_ColumnValueIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(byte))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetByteOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetByteOrDefault_ColumnIsMissing ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(byte))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetByteOrDefault("Column2");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetByteOrDefault_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetByteOrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetByteOrDefault_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetByteOrDefault("");
        }

        [TestMethod]
        public void GetByteOrDefault_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(byte))
                            .InsertRow();
            byte expected = 45;
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetByteOrDefault("Column1", expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetByteOrDefault_WithOrdinal_ColumnValueIsSet ()
        {
            //Arrange
            byte expected = 4;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(byte))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetByteOrDefault(0);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetByteOrDefault_WithOrdinal_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(byte))
                            .InsertRow();
            byte expected = 45;
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetByteOrDefault(0, expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetByteOrDefault_WithOrdinal_TooSmall ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetByteOrDefault(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetByteOrDefault_WithOrdinal_TooLarge ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(byte))
                            .InsertRow(0);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetByteOrDefault(1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetByteOrDefault_ColumnTypeIsLarger ()
        {
            //Arrange
            short expected = 1234;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(short))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetByteOrDefault(0);
        }
        #endregion

        #region GetCharOrDefault

        [TestMethod]
        public void GetCharOrDefault_ColumnValueIsSet ()
        {
            //Arrange
            var expected = 'D';
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(char))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetCharOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetCharOrDefault_ColumnValueIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(char))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetCharOrDefault("Column1");

            //Assert
            actual.Should().Be(default(char));
        }

        [TestMethod]
        public void GetCharOrDefault_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(char))
                            .InsertRow();
            var expected = 'X';
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetCharOrDefault("Column1", expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCharOrDefault_ColumnIsMissing ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(char))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetCharOrDefault("Column2");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetCharOrDefault_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetCharOrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCharOrDefault_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetCharOrDefault("");
        }

        [TestMethod]
        public void GetCharOrDefault_WithOrdinal_ColumnValueIsSet ()
        {
            //Arrange
            var expected = 'Z';
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(char))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetCharOrDefault(0);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetCharOrDefault_WithOrdinal_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(char))
                            .InsertRow();
            var expected = 'C';
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetCharOrDefault(0, expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetCharOrDefault_WithOrdinal_TooSmall ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetCharOrDefault(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetCharOrDefault_WithOrdinal_TooLarge ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(char))
                            .InsertRow('A');
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetCharOrDefault(1);
        }
        #endregion

        #region GetDateTimeOrDefault

        [TestMethod]
        public void GetDateTimeOrDefault_ColumnValueIsSet ()
        {
            //Arrange
            var expected = new DateTime(2011, 12, 20, 1, 2, 3);
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(DateTime))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetDateTimeOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetDateTimeOrDefault_ColumnValueIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(DateTime))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetDateTimeOrDefault("Column1");

            //Assert
            actual.Should().Be(default(DateTime));
        }

        [TestMethod]
        public void GetDateTimeOrDefault_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(byte))
                            .InsertRow();
            var expected = new DateTime(2012, 4, 17, 12, 34, 56);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetDateTimeOrDefault("Column1", expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDateTimeOrDefault_ColumnIsMissing ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(DateTime))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDateTimeOrDefault("Column2");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetDateTimeOrDefault_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDateTimeOrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDateTimeOrDefault_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDateTimeOrDefault("");
        }

        [TestMethod]
        public void GetDateTimeOrDefault_WithOrdinal_ColumnValueIsSet ()
        {
            //Arrange
            var expected = new DateTime(2011, 12, 20, 1, 2, 3);
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(DateTime))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetDateTimeOrDefault(0);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetDateTimeOrDefault_WithOrdinal_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(DateTime))
                            .InsertRow();
            var expected = new DateTime(2012, 4, 17, 12, 34, 56);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetDateTimeOrDefault(0, expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetDateTimeOrDefault_WithOrdinal_TooSmall ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDateTimeOrDefault(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetDateTimeOrDefault_WithOrdinal_TooLarge ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(decimal))
                            .InsertRow(0);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDateTimeOrDefault(1);
        }
        #endregion

        #region GetDecimalOrDefault

        [TestMethod]
        public void GetDecimalOrDefault_ColumnValueIsSet ()
        {
            //Arrange
            decimal expected = 123456789M;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(decimal))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetDecimalOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetDecimalOrDefault_ColumnValueIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(decimal))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetDecimalOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void GetDecimalOrDefault_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(decimal))
                            .InsertRow();
            var expected = 12345678M;
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetDecimalOrDefault("Column1", expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDecimalOrDefault_ColumnIsMissing ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(decimal))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDecimalOrDefault("Column2");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetDecimalOrDefault_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDecimalOrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDecimalOrDefault_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDecimalOrDefault("");
        }

        [TestMethod]
        public void GetDecimalOrDefault_WithOrdinal_ColumnValueIsSet ()
        {
            //Arrange
            var expected = 12345678M;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(decimal))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetDecimalOrDefault(0);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetDecimalOrDefault_WithOrdinal_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(decimal))
                            .InsertRow();
            var expected = 1234567M;
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetDecimalOrDefault(0, expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetDecimalOrDefault_WithOrdinal_TooSmall ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDecimalOrDefault(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetDecimalOrDefault_WithOrdinal_TooLarge ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(decimal))
                            .InsertRow(0);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDecimalOrDefault(1);
        }

        [TestMethod]
        public void GetDecimalOrDefault_ColumnTypeIsSmaller ()
        {
            //Arrange
            int expected = 12345;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetDecimalOrDefault(0);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region GetDoubleOrDefault

        [TestMethod]
        public void GetDoubleOrDefault_ColumnValueIsSet ()
        {
            //Arrange
            double expected = 1234.5678;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(double))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetDoubleOrDefault("Column1");

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        public void GetDoubleOrDefault_ColumnValueIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(double))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetDoubleOrDefault("Column1");

            //Assert
            actual.Should().BeExactly(0);
        }

        [TestMethod]
        public void GetDoubleOrDefault_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(double))
                            .InsertRow();
            var expected = 1234.5678;
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetDoubleOrDefault("Column1", expected);

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDoubleOrDefault_ColumnIsMissing ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(double))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDoubleOrDefault("Column2");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetDoubleOrDefault_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDoubleOrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDoubleOrDefault_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDoubleOrDefault("");
        }

        [TestMethod]
        public void GetDoubleOrDefault_WithOrdinal_ColumnValueIsSet ()
        {
            //Arrange
            var expected = 123.456;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(double))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetDoubleOrDefault(0);

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        public void GetDoubleOrDefault_WithOrdinal_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(double))
                            .InsertRow();
            var expected = 1234.5678;
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetDoubleOrDefault(0, expected);

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetDoubuleOrDefault_WithOrdinal_TooSmall ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDoubleOrDefault(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetDoubleOrDefault_WithOrdinal_TooLarge ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(double))
                            .InsertRow(0);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDoubleOrDefault(1);
        }

        [TestMethod]
        public void GetDoubleOrDefault_ColumnTypeIsSmaller ()
        {
            //Arrange
            int expected = 12345;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetDoubleOrDefault(0);

            //Assert
            actual.Should().BeExactly(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetDoubleOrDefault_ColumnTypeIsLarger ()
        {
            //Arrange
            decimal expected = 1234;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(decimal))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDoubleOrDefault(0);
        }
        #endregion

        #region GetGuidOrDefault

        [TestMethod]
        public void GetGuidOrDefault_ColumnValueIsSet ()
        {
            //Arrange
            var expected = Guid.NewGuid();
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(Guid))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetGuidOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetGuidOrDefault_ColumnValueIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(Guid))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetGuidOrDefault("Column1");

            //Assert
            actual.Should().Be(Guid.Empty);
        }

        [TestMethod]
        public void GetGuidOrDefault_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(Guid))
                            .InsertRow();
            var expected = Guid.NewGuid();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetGuidOrDefault("Column1", expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetGuidOrDefault_ColumnIsMissing ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(DateTime))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetGuidOrDefault("Column2");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetGuidOrDefault_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetGuidOrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetGuidOrDefault_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetGuidOrDefault("");
        }

        [TestMethod]
        public void GetGuidOrDefault_WithOrdinal_ColumnValueIsSet ()
        {
            //Arrange
            var expected = Guid.NewGuid();
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(Guid))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetGuidOrDefault(0);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetGuidOrDefault_WithOrdinal_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(Guid))
                            .InsertRow();
            var expected = Guid.NewGuid();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetGuidOrDefault(0, expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetGuidOrDefault_WithOrdinal_TooSmall ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetGuidOrDefault(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetGuidOrDefault_WithOrdinal_TooLarge ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(decimal))
                            .InsertRow(0);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetGuidOrDefault(1);
        }
        #endregion

        #region GetInt16OrDefault

        [TestMethod]
        public void GetInt16OrDefault_ColumnValueIsSet ()
        {
            //Arrange
            short expected = 45;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(short))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetInt16OrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetInt16OrDefault_ColumnValueIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(short))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetInt16OrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void GetInt16OrDefault_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(short))
                            .InsertRow();
            short expected = 123;
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetInt16OrDefault("Column1", expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInt16OrDefault_ColumnIsMissing ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(short))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt16OrDefault("Column2");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetInt16OrDefault_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt16OrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInt16OrDefault_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt16OrDefault("");
        }

        [TestMethod]
        public void GetInt16OrDefault_WithOrdinal_ColumnValueIsSet ()
        {
            //Arrange
            short expected = 4;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(short))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetInt16OrDefault(0);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetInt16OrDefault_WithOrdinal_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(short))
                            .InsertRow();
            short expected = 123;
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetInt16OrDefault(0, expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetInt16OrDefault_WithOrdinal_TooSmall ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt16OrDefault(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetInt16OrDefault_WithOrdinal_TooLarge ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(short))
                            .InsertRow(0);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt16OrDefault(1);
        }

        [TestMethod]
        public void GetInt16OrDefault_ColumnTypeIsSmaller ()
        {
            //Arrange
            byte expected = 123;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(byte))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetInt16OrDefault(0);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetInt16OrDefault_ColumnTypeIsLarger ()
        {
            //Arrange
            int expected = 1234;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt16OrDefault(0);
        }
        #endregion

        #region GetInt32OrDefault

        [TestMethod]
        public void GetInt32OrDefault_ColumnValueIsSet ()
        {
            //Arrange
            int expected = 1234;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetInt32OrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetInt32OrDefault_ColumnValueIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetInt32OrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void GetInt32OrDefault_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var expected = 1234;
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetInt32OrDefault("Column1", expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInt32OrDefault_ColumnIsMissing ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt32OrDefault("Column2");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetInt32OrDefault_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt32OrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInt32OrDefault_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt32OrDefault("");
        }

        [TestMethod]
        public void GetInt32OrDefault_WithOrdinal_ColumnValueIsSet ()
        {
            //Arrange
            var expected = 4567;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetInt32OrDefault(0);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetInt32OrDefault_WithOrdinal_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var expected = 123456;
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetInt32OrDefault(0, expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetInt32OrDefault_WithOrdinal_TooSmall ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt32OrDefault(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetInt32OrDefault_WithOrdinal_TooLarge ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(0);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt32OrDefault(1);
        }

        [TestMethod]
        public void GetInt32OrDefault_ColumnTypeIsSmaller ()
        {
            //Arrange
            short expected = 1234;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(short))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetInt32OrDefault(0);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetInt32OrDefault_ColumnTypeIsLarger ()
        {
            //Arrange
            long expected = 1234;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(long))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt32OrDefault(0);
        }
        #endregion

        #region GetInt64OrDefault

        [TestMethod]
        public void GetInt64OrDefault_ColumnValueIsSet ()
        {
            //Arrange
            long expected = 123456789L;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(long))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetInt64OrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetInt64OrDefault_ColumnValueIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(long))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetInt64OrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void GetInt64OrDefault_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(long))
                            .InsertRow();
            var expected = 12345678;
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetInt64OrDefault("Column1", expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInt64OrDefault_ColumnIsMissing ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(long))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt64OrDefault("Column2");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetInt64OrDefault_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt64OrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInt64OrDefault_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt64OrDefault("");
        }

        [TestMethod]
        public void GetInt64OrDefault_WithOrdinal_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(long))
                            .InsertRow();
            var expected = 12345678L;
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetInt64OrDefault(0, expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetInt64OrDefault_WithOrdinal_TooSmall ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt64OrDefault(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetInt64OrDefault_WithOrdinal_TooLarge ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(long))
                            .InsertRow(0);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt64OrDefault(1);
        }

        [TestMethod]
        public void GetInt64OrDefault_ColumnTypeIsSmaller ()
        {
            //Arrange
            int expected = 123456;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetInt64OrDefault(0);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetInt64OrDefault_ColumnTypeIsLarger ()
        {
            //Arrange
            float expected = 1234;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(float))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt64OrDefault(0);
        }
        #endregion

        #region GetSByteOrDefault

        [TestMethod]
        public void GetSByteOrDefault_ColumnValueIsSet ()
        {
            //Arrange
            byte expected = 45;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(byte))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetSByteOrDefault("Column1");

            //Assert
            actual.Should().Be(Convert.ToSByte(expected));
        }

        [TestMethod]
        public void GetSByteOrDefault_ColumnValueIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(byte))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetSByteOrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void GetSByteOrDefault_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(byte))
                            .InsertRow();
            sbyte expected = -10;
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetSByteOrDefault("Column1", expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetSByteOrDefault_ColumnIsMissing ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(byte))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetSByteOrDefault("Column2");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetSByteOrDefault_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetSByteOrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetSByteOrDefault_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetSByteOrDefault("");
        }

        [TestMethod]
        public void GetSByteOrDefault_WithOrdinal_ColumnValueIsSet ()
        {
            //Arrange
            byte expected = 67;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(byte))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetSByteOrDefault(0);

            //Assert
            actual.Should().Be(Convert.ToSByte(expected));
        }

        [TestMethod]
        public void GetSByteOrDefault_WithOrdinal_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(byte))
                            .InsertRow();
            sbyte expected = -10;
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetSByteOrDefault(0, expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetSByteOrDefault_WithOrdinal_TooSmall ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetSByteOrDefault(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetSByteOrDefault_WithOrdinal_TooLarge ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(byte))
                            .InsertRow(0);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetSByteOrDefault(1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetSByteOrDefault_ColumnTypeIsLarger ()
        {
            //Arrange
            short expected = 1234;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(short))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetSByteOrDefault(0);
        }
        #endregion

        #region GetSingleOrDefault

        [TestMethod]
        public void GetSingleOrDefault_ColumnValueIsSet ()
        {
            //Arrange
            var expected = 9876.0F;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(float))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetSingleOrDefault("Column1");

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        public void GetSingleOrDefault_ColumnValueIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(float))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetSingleOrDefault("Column1");

            //Assert
            actual.Should().BeExactly(0);
        }

        [TestMethod]
        public void GetSingleOrDefault_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(float))
                            .InsertRow();
            var expected = 1234.56F;
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetSingleOrDefault("Column1", expected);

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetSingleOrDefault_ColumnIsMissing ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(float))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetSingleOrDefault("Column2");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetSingleOrDefault_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetSingleOrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetSingleOrDefault_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetSingleOrDefault("");
        }

        [TestMethod]
        public void GetSingleOrDefault_WithOrdinal_ColumnValueIsSet ()
        {
            //Arrange
            var expected = 12.34F;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(float))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetSingleOrDefault(0);

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        public void GetSingleOrDefault_WithOrdinal_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(float))
                            .InsertRow();
            var expected = 123.456F;
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetSingleOrDefault(0, expected);

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetSingleOrDefault_WithOrdinal_TooSmall ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetSingleOrDefault(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetSingleOrDefault_WithOrdinal_TooLarge ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(float))
                            .InsertRow(0);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetSingleOrDefault(1);
        }

        [TestMethod]
        public void GetSingleOrDefault_ColumnTypeIsSmaller ()
        {
            //Arrange
            int expected = 12345;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetSingleOrDefault(0);

            //Assert
            actual.Should().BeExactly(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetSingleOrDefault_ColumnTypeIsLarger ()
        {
            //Arrange
            double expected = 1234;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(double))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetSingleOrDefault(0);
        }
        #endregion

        #region GetStringOrDefault

        [TestMethod]
        public void GetStringOrDefault_ColumnValueIsSet ()
        {
            //Arrange
            var expected = "Hello";
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(string))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetStringOrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetStringOrDefault_ColumnValueIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(string))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetStringOrDefault("Column1");

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void GetStringOrDefault_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(string))
                            .InsertRow();
            var expected = "None";
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetStringOrDefault("Column1", expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetStringOrDefault_ColumnIsMissing ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(string))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetStringOrDefault("Column2");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetStringOrDefault_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetStringOrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetStringOrDefault_ColumnIsEmpty ()
        {
            //Arrange            
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetStringOrDefault("");
        }

        [TestMethod]
        public void GetStringOrDefault_WithOrdinal_ColumnValueIsSet ()
        {
            //Arrange
            var expected = "Hello";
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(string))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetStringOrDefault(0);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetStringOrDefault_WithOrdinal_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(string))
                            .InsertRow();
            var expected = "None";
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetStringOrDefault(0, expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetStringOrDefault_WithOrdinal_TooSmall ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetStringOrDefault(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetStringOrDefault_WithOrdinal_TooLarge ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(string))
                            .InsertRow(0);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetStringOrDefault(1);
        }
        #endregion

        #region GetUInt16OrDefault

        [TestMethod]
        public void GetUInt16OrDefault_ColumnValueIsSet ()
        {
            //Arrange
            ushort expected = 3456;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(short))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetUInt16OrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetUInt16OrDefault_ColumnValueIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(short))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetUInt16OrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void GetUInt16OrDefault_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(ushort))
                            .InsertRow();
            ushort expected = 123;
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetUInt16OrDefault("Column1", expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetUInt16OrDefault_ColumnIsMissing ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(short))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt16OrDefault("Column2");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetUInt16OrDefault_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt16OrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetUInt16OrDefault_ColumnIsEmpty ()
        {
            //Arrange            
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt16OrDefault("");
        }

        [TestMethod]
        public void GetUInt16OrDefault_WithOrdinal_ColumnValueIsSet ()
        {
            //Arrange
            ushort expected = 45;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(short))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetUInt16OrDefault(0);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetUInt16OrDefault_WithOrdinal_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(ushort))
                            .InsertRow();
            ushort expected = 45;
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetUInt16OrDefault(0, expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetUInt16OrDefault_WithOrdinal_TooSmall ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt16OrDefault(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetUInt16OrDefault_WithOrdinal_TooLarge ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(short))
                            .InsertRow(0);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt16OrDefault(1);
        }

        [TestMethod]
        public void GetUInt16OrDefault_ColumnTypeIsSmaller ()
        {
            //Arrange
            byte expected = 123;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(byte))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetUInt16OrDefault(0);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetUInt16OrDefault_ColumnTypeIsLarger ()
        {
            //Arrange
            int expected = 1234;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt16OrDefault(0);
        }
        #endregion

        #region GetUInt32OrDefault

        [TestMethod]
        public void GetUInt32OrDefault_ColumnValueIsSet ()
        {
            //Arrange
            var expected = 3456789U;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetUInt32OrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetUInt32OrDefault_ColumnValueIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetUInt32OrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void GetUInt32OrDefault_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(uint))
                            .InsertRow();
            var expected = 12345U;
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetUInt32OrDefault("Column1", expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetUInt32OrDefault_ColumnIsMissing ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt32OrDefault("Column2");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetUInt32OrDefault_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt32OrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetUInt32OrDefault_ColumnIsEmpty ()
        {
            //Arrange            
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt32OrDefault("");
        }

        [TestMethod]
        public void GetUInt32OrDefault_WithOrdinal_ColumnValueIsSet ()
        {
            //Arrange
            var expected = 4567U;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetUInt32OrDefault(0);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetUInt32OrDefault_WithOrdinal_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(uint))
                            .InsertRow();
            uint expected = 123456;
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetUInt32OrDefault(0, expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetUInt32OrDefault_WithOrdinal_TooSmall ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt32OrDefault(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetUInt32OrDefault_WithOrdinal_TooLarge ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(0);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt32OrDefault(1);
        }

        [TestMethod]
        public void GetUInt32OrDefault_ColumnTypeIsSmaller ()
        {
            //Arrange
            short expected = 1234;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(short))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetUInt32OrDefault(0);

            //Assert
            actual.Should().Be(Convert.ToUInt32(expected));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetUInt32OrDefault_ColumnTypeIsLarger ()
        {
            //Arrange
            long expected = 1234;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(long))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt32OrDefault(0);
        }
        #endregion

        #region GetUInt64OrDefault

        [TestMethod]
        public void GetUInt64OrDefault_ColumnValueIsSet ()
        {
            //Arrange
            ulong expected = 567890123UL;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(long))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetUInt64OrDefault("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetUInt64OrDefault_ColumnValueIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(long))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetUInt64OrDefault("Column1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void GetUInt64OrDefault_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(ulong))
                            .InsertRow();
            var expected = 12345678UL;
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetUInt64OrDefault("Column1", expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetUInt64OrDefault_ColumnIsMissing ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(long))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt64OrDefault("Column2");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetUInt64OrDefault_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt64OrDefault(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetUInt64OrDefault_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt64OrDefault("");
        }

        [TestMethod]
        public void GetUInt64OrDefault_WithOrdinal_ColumnValueIsSet ()
        {
            //Arrange
            var expected = 456789UL;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(long))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetUInt64OrDefault(0);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetUInt64OrDefault_WithOrdinal_IsNullWithCustomDefault ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(ulong))
                            .InsertRow();
            var expected = 123456UL;
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetUInt64OrDefault(0, expected);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetUInt64OrDefault_WithOrdinal_TooSmall ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt64OrDefault(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetUInt64OrDefault_WithOrdinal_TooLarge ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(long))
                            .InsertRow(0);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt64OrDefault(1);
        }

        [TestMethod]
        public void GetUInt164OrDefault_ColumnTypeIsSmaller ()
        {
            //Arrange
            int expected = 1234;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetUInt64OrDefault(0);

            //Assert
            actual.Should().Be(Convert.ToUInt64(expected));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetUInt64OrDefault_ColumnTypeIsLarger ()
        {
            //Arrange
            float expected = 1234;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(float))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt64OrDefault(0);
        }
        #endregion
    }
}