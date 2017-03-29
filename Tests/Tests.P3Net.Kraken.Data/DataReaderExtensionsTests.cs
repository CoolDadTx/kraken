using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Data;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.Data
{
    [TestClass]
    public partial class DataReaderExtensionsTests : UnitTest
    {       
        #region FieldExists

        [TestMethod]
        public void FieldExists_ColumnDoesExist ()
        {
            //Arrange
            var schema = new DataTable();
            schema.Columns.AddRange(new DataColumn[] { 
                new DataColumn("Column1", typeof(int)),
                new DataColumn("Column2", typeof(string))
            });
            var target = new TestDataReader(schema);

            //Act
            var actual = target.FieldExists("Column2");

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void FieldExists_FieldDoesNotExist ()
        {
            //Arrange
            var schema = new DataTable();
            schema.Columns.Add(new DataColumn("Column1", typeof(int)));
            var target = new TestDataReader(schema);

            //Act
            var actual = target.FieldExists("Column2");

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FieldExists_ColumnIsNull ()
        {
            //Arrange
            var target = new TestDataReader(new DataTable());

            //Act
            var actual = target.FieldExists(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FieldExists_ColumnIsEmpty ()
        {
            //Arrange
            var target = new TestDataReader(new DataTable());

            //Act
            var actual = target.FieldExists("");
        }

        [TestMethod]
        public void FieldExists_FieldExistsWithDifferentCase ()
        {
            //Arrange
            var schema = new DataTable();
            schema.Columns.Add(new DataColumn("Column1", typeof(int)));
            var target = new TestDataReader(schema);

            //Act
            var actual = target.FieldExists("cOlUmN1");

            //Assert
            actual.Should().BeTrue();
        }
        #endregion

        #region GetDataTypeName

        [TestMethod]
        public void GetDataTypeName_ColumnExists ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int));
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetDataTypeName("Column1");

            //Assert
            actual.Should().Be("Int32");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDataTypeName_ColumnDoesNotExist ()
        {
            //Arrange         
            var schema = new DataTable()
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetDataTypeName("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetDataTypeName_ColumnIsNull ()
        {
            //Arrange
            var target = new TestDataReader(new DataTable());

            //Act
            target.Read();
            target.GetDataTypeName(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDataTypeName_ColumnIsEmpty ()
        {
            //Arrange
            var target = new TestDataReader(new DataTable());

            //Act
            target.Read();
            target.GetDataTypeName("");
        }
        #endregion

        #region GetFieldType

        [TestMethod]
        public void GetFieldType_ColumnExists ()
        {
            //Arrange
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(int));
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetFieldType("Column1");

            //Assert
            actual.Should().Be(typeof(int));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFieldType_ColumnDoesNotExist ()
        {
            //Arrange         
            var schema = new DataTable()
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.GetFieldType("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetFieldType_ColumnIsNull ()
        {
            //Arrange
            var target = new TestDataReader(new DataTable());

            //Act
            target.Read();
            target.GetFieldType(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFieldType_ColumnIsEmpty ()
        {
            //Arrange
            var target = new TestDataReader(new DataTable());

            //Act
            target.Read();
            target.GetFieldType("");
        }
        #endregion

        #region GetNames

        [TestMethod]
        public void GetNames_HasColumns ()
        {
            //Arrange
            var expected = new string[] { "Column1", "Column2", "Column3" };
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(sbyte))
                            .AddColumn("Column2", typeof(string))
                            .AddColumn("Column3", typeof(bool));
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.GetNames();

            //Assert
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void GetNames_NoColumns ()
        {
            //Arrange
            var target = new TestDataReader(new DataTable());

            //Act
            target.Read();
            var actual = target.GetNames();

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetNames_IsClosed ()
        {
            var target = new TestDataReader(new DataTable());

            //Act
            target.Close();
            target.GetNames();
        }
        #endregion

        #region IsDBNull

        [TestMethod]
        public void IsDBNull_ColumnExists ()
        {
            //Arrange            
            var schema = new DataTable()
                            .AddColumn("Column1", typeof(long))
                            .InsertRow(45);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            var actual = target.IsDBNull("Column1");

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsDBNull_ColumnDoesNotExist ()
        {
            //Arrange         
            var schema = new DataTable()
                            .InsertRow(null);
            var target = new TestDataReader(schema);

            //Act
            target.Read();
            target.IsDBNull("Column1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsDBNull_ColumnIsNull ()
        {
            //Arrange
            var target = new TestDataReader(new DataTable());

            //Act
            target.Read();
            target.IsDBNull(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsDBNull_ColumnIsEmpty ()
        {
            //Arrange
            var target = new TestDataReader(new DataTable());

            //Act
            target.Read();
            target.IsDBNull("");
        }
        #endregion

        #region Private Members

        //This class exists because the DTR returns ArgumentOutOfRangeException exceptions when the ordinal is wrong rather than the
        //correct IndexOutOfRangeException
        private sealed class TestDataReader : IDataReader
        {
            public TestDataReader ( DataTable table )
            {
                m_reader = table.CreateDataReader();
            }

            public TestDataReader ( DataSet dataset )
            {
                m_reader = dataset.CreateDataReader();
            }

            public TestDataReader ( DataTableReader reader )
            {
                m_reader = reader;
            }
                        
            #region IDataReader Members

            public void Close () 
            {
                m_reader.Close(); 
            }
            
            public int Depth 
            {
                get { return m_reader.Depth; }
            }

            public DataTable GetSchemaTable ()
            {
                return m_reader.GetSchemaTable();
            }

            public bool IsClosed
            {
                get { return m_reader.IsClosed; }
            }

            public bool NextResult ()
            {
                return m_reader.NextResult();
            }

            public bool Read ()
            {
                return m_reader.Read();
            }

            public int RecordsAffected
            {
                get { return m_reader.RecordsAffected; }
            }
                        
            public void Dispose ()
            {
                m_reader.Dispose();
            }

            public int FieldCount
            {
                get { return m_reader.FieldCount; }
            }

            public bool GetBoolean ( int i )
            {
                return FilterOutOfRangeException(m_reader.GetBoolean, i);
            }

            public byte GetByte ( int i )
            {
                return FilterOutOfRangeException(m_reader.GetByte, i);
            }

            public long GetBytes ( int i, long fieldOffset, byte[] buffer, int bufferoffset, int length )
            {
                try
                {
                    return m_reader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
                } catch (ArgumentOutOfRangeException e)
                {
                    throw new IndexOutOfRangeException(e.Message);
                };
            }

            public char GetChar ( int i )
            {
                return FilterOutOfRangeException(m_reader.GetChar, i);
            }

            public long GetChars ( int i, long fieldoffset, char[] buffer, int bufferoffset, int length )
            {
                try
                {
                    return m_reader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
                } catch (ArgumentOutOfRangeException e)
                {
                    throw new IndexOutOfRangeException(e.Message);
                };
            }

            public IDataReader GetData ( int i )
            {
                return FilterOutOfRangeException(m_reader.GetData, i);
            }

            public string GetDataTypeName ( int i )
            {
                return FilterOutOfRangeException(m_reader.GetDataTypeName, i);
            }

            public DateTime GetDateTime ( int i )
            {
                return FilterOutOfRangeException(m_reader.GetDateTime, i);
            }

            public decimal GetDecimal ( int i )
            {
                return FilterOutOfRangeException(m_reader.GetDecimal, i);
            }

            public double GetDouble ( int i )
            {
                return FilterOutOfRangeException(m_reader.GetDouble, i);
            }

            public Type GetFieldType ( int i )
            {
                return FilterOutOfRangeException(m_reader.GetFieldType, i);
            }

            public float GetFloat ( int i )
            {
                return FilterOutOfRangeException(m_reader.GetFloat, i);
            }

            public Guid GetGuid ( int i )
            {
                return FilterOutOfRangeException(m_reader.GetGuid, i);
            }

            public short GetInt16 ( int i )
            {
                return FilterOutOfRangeException(m_reader.GetInt16, i);
            }

            public int GetInt32 ( int i )
            {
                return FilterOutOfRangeException(m_reader.GetInt32, i);
            }

            public long GetInt64 ( int i )
            {
                return FilterOutOfRangeException(m_reader.GetInt64, i);
            }

            public string GetName ( int i )
            {
                return FilterOutOfRangeException(m_reader.GetName, i);
            }

            public int GetOrdinal ( string name )
            {
                return m_reader.GetOrdinal(name);
            }

            public string GetString ( int i )
            {
                return FilterOutOfRangeException(m_reader.GetString, i);
            }

            public object GetValue ( int i )
            {
                return FilterOutOfRangeException(m_reader.GetValue, i);
            }

            public int GetValues ( object[] values )
            {
                return m_reader.GetValues(values);
            }

            public bool IsDBNull ( int i )
            {
                return FilterOutOfRangeException(m_reader.IsDBNull, i);
            }

            public object this[string name]
            {
                get { return m_reader[name]; }
            }

            public object this[int i]
            {
                get
                {
                    try
                    {
                        return m_reader[i];
                    } catch (ArgumentOutOfRangeException e)
                    {
                        throw new IndexOutOfRangeException(e.Message);
                    };
                }
            }
            #endregion

            private TResult FilterOutOfRangeException<TResult> ( Func<int, TResult> action, int i )
            {
                try
                {
                    return action(i);
                } catch (ArgumentOutOfRangeException e)
                {
                    throw new IndexOutOfRangeException(e.Message);
                };
            }

            private DataTableReader m_reader;
        }
        #endregion
    }
}
