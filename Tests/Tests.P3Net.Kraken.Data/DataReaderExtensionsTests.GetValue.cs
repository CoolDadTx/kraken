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
    public partial class DataReaderExtensionsTests
    {
        #region GetBoolean

        [TestMethod]
        public void GetBoolean_ColumnExists ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(bool))
                            .InsertRow(true);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetBoolean("Column1");

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetBoolean_ColumnDoesNotExist ()
        {
            //Arrange         
            var schema = new DataTable()
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetBoolean("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetBoolean_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetBoolean(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetBoolean_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetBoolean("");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetBoolean_ColumnIsIntegerType ()
        {
            //Arrange            
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(4);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetBoolean("Column1");
        }
        #endregion

        #region GetByte

        [TestMethod]
        public void GetByte_ColumnExists ()
        {
            //Arrange
            byte expected = 12;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(byte))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetByte("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetByte_ColumnDoesNotExist ()
        {
            //Arrange         
            var schema = new DataTable()
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetByte("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetByte_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetByte(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetByte_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetByte("");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetByte_ColumnIsLargerType ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(uint))
                            .InsertRow(12);

            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetByte("Column1");
        }
        #endregion

        #region GetBytes

        [TestMethod]
        public void GetBytes_ColumnExists ()
        {
            //Arrange
            var expected = new byte[] { 1, 2, 3, 4 };
            byte[] actual = new byte[expected.Length];
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(byte[]))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetBytes("Column1", 0, actual, 0, actual.Length);

            //Assert
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetBytes_ColumnDoesNotExist ()
        {
            //Arrange         
            var schema = new DataTable()
                            .InsertRow(null);
            var target = new TestDataReader(schema);
            var actual = new byte[4];

            //Act
            target.Read();
            target.GetBytes("Column1", 0, actual, 0, actual.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetBytes_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);
            var actual = new byte[4];

            //Act
            target.Read();
            target.GetBytes(null, 0, actual, 0, actual.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetBytes_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);
            var actual = new byte[4];

            //Act
            target.Read();
            target.GetBytes("", 0, actual, 0, actual.Length);
        }
        #endregion

        #region GetChar

        [TestMethod]
        public void GetChar_ColumnExists ()
        {
            //Arrange
            var expected = 'Z';
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(char))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetChar("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetChar_ColumnDoesNotExist ()
        {
            //Arrange         
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetChar("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetChar_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetChar(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetChar_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetChar("");
        }
        #endregion

        #region GetChars

        [TestMethod]
        public void GetChars_ColumnExists ()
        {
            //Arrange
            var expected = new char[] { 'H', 'e', 'l', 'l', 'o' };
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(char[]))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);
            var actual = new char[expected.Length];

            //Act
            target.Read();
            target.GetChars("Column1", 0, actual, 0, actual.Length);

            //Assert
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetChars_ColumnDoesNotExist ()
        {
            //Arrange         
            var schema = new DataTable()
                            .InsertRow(null);
            var target = new TestDataReader(schema);
            var actual = new char[4];

            //Act
            target.Read();
            target.GetChars("Column1", 0, actual, 0, actual.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetChars_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);
            var actual = new char[4];

            //Act
            target.Read();
            target.GetChars(null, 0, actual, 0, actual.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetChars_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);
            var actual = new char[4];

            //Act
            target.Read();
            target.GetChars("", 0, actual, 0, actual.Length);
        }
        #endregion

        #region GetDate

        [TestMethod]
        public void GetDate_ColumnExists ()
        {
            var expected = Dates.February(4, 1987);

            //Have to use DT because the reader will fail on a "Date"
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(DateTime))
                            .InsertRow((DateTime)expected);
            var target = new TestDataReader(schema);

            target.Read();
            var actual = target.GetDate("Column1");

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetDate_ColumnDoesNotExist ()
        {
            var schema = new DataTable()
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            target.Read();
            Action action = () => target.GetDate("Column1");
            action.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void GetDate_ColumnIsNull ()
        {
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            target.Read();
            Action action = () => target.GetDate(null);
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void GetDate_ColumnIsEmpty ()
        {
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            target.Read();
            Action action = () => target.GetDate("");
            action.Should().Throw<ArgumentException>();
        }
        #endregion

        #region GetDateTime

        [TestMethod]
        public void GetDateTime_ColumnExists ()
        {
            //Arrange
            var expected = new DateTime(2011, 12, 20, 1, 2, 3);
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(DateTime))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetDateTime("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDateTime_ColumnDoesNotExist ()
        {
            //Arrange         
            var schema = new DataTable()
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDateTime("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetDateTime_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDateTime(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDateTime_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDateTime("");
        }
        #endregion

        #region GetDecimal

        [TestMethod]
        public void GetDecimal_ColumnExists ()
        {
            //Arrange
            var expected = 12345678M;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(decimal))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetDecimal("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDecimal_ColumnDoesNotExist ()
        {
            //Arrange         
            var schema = new DataTable()
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDecimal("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetDecimal_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDecimal(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDecimal_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDecimal("");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetDecimal_ColumnIsSmallerType ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(1234);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDecimal("Column1");
        }
        #endregion

        #region GetDouble

        [TestMethod]
        public void GetDouble_ColumnExists ()
        {
            //Arrange
            var expected = 123.456;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(double))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetDouble("Column1");

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDouble_ColumnDoesNotExist ()
        {
            //Arrange         
            var schema = new DataTable()
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDouble("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetDouble_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDouble(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDouble_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDouble("");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetDouble_ColumnIsSmallerType ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(float))
                            .InsertRow(123.456F);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDouble("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetDouble_ColumnIsIntegralType ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(123);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDouble("Column1");
        }
        #endregion

        #region GetGuid

        [TestMethod]
        public void GetGuid_ColumnExists ()
        {
            //Arrange
            var expected = Guid.NewGuid();
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(Guid))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetGuid("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetGuid_ColumnDoesNotExist ()
        {
            //Arrange         
            var schema = new DataTable()
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetGuid("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetGuid_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetGuid(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetGuid_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetGuid("");
        }
        #endregion

        #region GetInt16

        [TestMethod]
        public void GetInt16_ColumnExists ()
        {
            //Arrange
            short expected = 1234;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(short))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetInt16("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInt16_ColumnDoesNotExist ()
        {
            //Arrange         
            var schema = new DataTable()
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt16("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetInt16_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt16(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInt16_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt16("");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetInt16_ColumnIsSmallerType ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(byte))
                            .InsertRow(12);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt16("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetInt16_ColumnIsLargerType ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(12345);

            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt16("Column1");
        }
        #endregion

        #region GetInt32

        [TestMethod]
        public void GetInt32_ColumnExists ()
        {
            //Arrange
            var expected = 123456;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetInt32("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInt32_ColumnDoesNotExist ()
        {
            //Arrange         
            var schema = new DataTable()
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt32("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetInt32_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt32(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInt32_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt32("");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetInt32_ColumnIsSmallerType ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(short))
                            .InsertRow(12);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt32("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetInt32_ColumnIsLargerType ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(long))
                            .InsertRow(12345);

            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt32("Column1");
        }
        #endregion

        #region GetInt64

        [TestMethod]
        public void GetInt64_ColumnExists ()
        {
            //Arrange
            var expected = 1234567L;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(long))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetInt64("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInt64_ColumnDoesNotExist ()
        {
            //Arrange         
            var schema = new DataTable()
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt64("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetInt64_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt64(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInt64_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt64("");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetInt64_ColumnIsSmallerType ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(byte))
                            .InsertRow(12);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetInt64("Column1");
        }
        #endregion

        #region GetSByte

        [TestMethod]
        public void GetSByte_ColumnExists ()
        {
            //Arrange
            byte expected = 34;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(byte))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetSByte("Column1");

            //Assert
            actual.Should().Be(Convert.ToSByte(expected));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetSByte_ColumnDoesNotExist ()
        {
            //Arrange         
            var schema = new DataTable()
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetSByte("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetSByte_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetSByte(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetSByte_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetSByte("");
        }

        [TestMethod]
        public void GetSByte_WithOrdinal_ColumnExists ()
        {
            //Arrange
            byte expected = 34;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(byte))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetSByte(0);

            //Assert
            actual.Should().Be(Convert.ToSByte(expected));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetSByte_WithOrdinal_ColumnValueIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(byte))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetSByte(0);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetSByte_WithOrdinal_TooSmall ()
        {
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetSByte(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetSByte_WithOrdinal_TooLarge ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(bool))
                            .InsertRow(true);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetSByte(1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetSByte_ColumnIsLargerType ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(12);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetSByte("Column1");
        }
        #endregion

        #region GetSingle

        [TestMethod]
        public void GetSingle_ColumnExists ()
        {
            //Arrange
            var expected = 12.34F;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(float))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetSingle("Column1");

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetSingle_ColumnDoesNotExist ()
        {
            //Arrange         
            var schema = new DataTable()
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetSingle("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetSingle_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetSingle(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetSingle_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetSingle("");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetSingle_ColumnIsLargerType ()
        {
            //Arrange            
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(double))
                            .InsertRow(123.456);

            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetSingle("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetSingle_ColumnIsIntegralType ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(123);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetSingle("Column1");
        }

        [TestMethod]
        public void GetSingle_WithOrdinal_Isvalid ()
        {
            //Arrange
            var expected = 123.456F;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(float))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetSingle(0);

            //Assert
            actual.Should().BeApproximately(expected);
        }
        #endregion

        #region GetString

        [TestMethod]
        public void GetString_ColumnExists ()
        {
            //Arrange
            var expected = "Goodbye";
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(string))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetString("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetString_ColumnDoesNotExist ()
        {
            //Arrange         
            var schema = new DataTable()
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetString("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetString_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetString(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetString_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetString("");
        }
        #endregion

        #region GetUInt16

        [TestMethod]
        public void GetUInt16_ColumnExists ()
        {
            //Arrange
            ushort expected = 123;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(short))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetUInt16("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetUInt16_ColumnDoesNotExist ()
        {
            //Arrange         
            var schema = new DataTable()
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt16("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetUInt16_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt16(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetUInt16_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt16("");
        }

        [TestMethod]
        public void GetUInt16_WithOrdinal_ColumnExists ()
        {
            //Arrange
            ushort expected = 34;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(short))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetUInt16(0);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetUInt16_WithOrdinal_TooSmall ()
        {
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt16(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetUInt16_WithOrdinal_TooLarge ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(short))
                            .InsertRow(45);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt16(1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetUInt16_WithOrdinal_ColumnValueIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(short))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt16(0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetUInt16_ColumnIsSmallerType ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(byte))
                            .InsertRow(12);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt16("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetUInt16_ColumnIsLargerType ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(123);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt16("Column1");
        }
        #endregion

        #region GetUInt32

        [TestMethod]
        public void GetUInt32_ColumnExists ()
        {
            //Arrange
            uint expected = 1234;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetUInt32("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetUInt32_ColumnDoesNotExist ()
        {
            //Arrange         
            var schema = new DataTable()
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt32("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetUInt32_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt32(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetUInt32_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt32("");
        }

        [TestMethod]
        public void GetUInt32_WithOrdinal_ColumnExists ()
        {
            //Arrange
            uint expected = 3456;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetUInt32(0);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetUInt32_WithOrdinal_TooSmall ()
        {
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt32(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetUInt32_WithOrdinal_TooLarge ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(45);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt32(1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetUInt32_WithOrdinal_ColumnValueIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt32(0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetUInt32_ColumnIsSmallerType ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(short))
                            .InsertRow(123);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt32("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetUInt32_ColumnIsLargerType ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(long))
                            .InsertRow(123);

            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt32("Column1");
        }
        #endregion

        #region GetUInt64

        [TestMethod]
        public void GetUInt64_ColumnExists ()
        {
            //Arrange
            ulong expected = 98765432;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(long))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetUInt64("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetUInt64_ColumnDoesNotExist ()
        {
            //Arrange         
            var schema = new DataTable()
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt64("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetUInt64_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt64(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetUInt64_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt64("");
        }

        [TestMethod]
        public void GetUInt64_WithOrdinal_ColumnExists ()
        {
            //Arrange
            ulong expected = 3456789;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(long))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetUInt64(0);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetUInt64_WithOrdinal_TooSmall ()
        {
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt64(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetUInt64_WithOrdinal_TooLarge ()
        {
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(long))
                            .InsertRow(45);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt64(1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetUInt64_WithOrdinal_ColumnValueIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(long))
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt64(0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void GetUInt64_ColumnIsSmallerType ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(123);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetUInt64("Column1");
        }
        #endregion

        #region GetValue

        [TestMethod]
        public void GetValue_ColumnExists ()
        {
            //Arrange
            var expected = 12345;
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int))
                            .InsertRow(expected);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetValue("Column1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetValue_ColumnDoesNotExist ()
        {
            //Arrange         
            var schema = new DataTable()
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetValue("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetValue_ColumnIsNull ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetValue(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetValue_ColumnIsEmpty ()
        {
            //Arrange
            var schema = new DataTable()
                            .InsertRow();
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetValue("");
        }
        #endregion        
    }
}