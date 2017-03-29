#region Imports

using System;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Xml.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using P3Net.Kraken;
using P3Net.Kraken.Data;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken.Data
{
    [TestClass]
    public class DbTypeMapperTests : UnitTest
    {
        #region ToClrType

        [TestMethod]
        public void ToClrType_WithAnsiString ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.AnsiString);

            actual.Should().Be(typeof(string));
        }

        [TestMethod]
        public void ToClrType_WithAnsiStringFixedLength ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.AnsiStringFixedLength);

            actual.Should().Be(typeof(string));
        }
            
        [TestMethod]
        public void ToClrType_WithBinary ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.Binary);

            actual.Should().Be(typeof(byte[]));
        }

        [TestMethod]
        public void ToClrType_WithByte ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.Byte);

            actual.Should().Be(typeof(byte));
        }

        [TestMethod]
        public void ToClrType_WithBoolean ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.Boolean);

            actual.Should().Be(typeof(bool));
        }

        [TestMethod]
        public void ToClrType_WithCurrency ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.Currency);

            actual.Should().Be(typeof(Money));
        }

        [TestMethod]
        public void ToClrType_WithDate ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.Date);

            actual.Should().Be(typeof(Date));
        }

        [TestMethod]
        public void ToClrType_WithDateTime ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.DateTime);

            actual.Should().Be(typeof(DateTime));
        }
        
        [TestMethod]
        public void ToClrType_WithDateTime2 ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.DateTime2);

            actual.Should().Be(typeof(DateTime));
        }

        [TestMethod]
        public void ToClrType_WithDateTimeOffset ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.DateTimeOffset);

            actual.Should().Be(typeof(DateTimeOffset));
        }

        [TestMethod]
        public void ToClrType_WithDecimal ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.Decimal);

            actual.Should().Be(typeof(decimal));
        }

        [TestMethod]
        public void ToClrType_WithDouble ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.Double);

            actual.Should().Be(typeof(double));
        }

        [TestMethod]
        public void ToClrType_WithGuid ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.Guid);

            actual.Should().Be(typeof(Guid));
        }

        [TestMethod]
        public void ToClrType_WithInt16 ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.Int16);

            actual.Should().Be(typeof(short));
        }

        [TestMethod]
        public void ToClrType_WithInt32 ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.Int32);

            actual.Should().Be(typeof(int));
        }

        [TestMethod]
        public void ToClrType_WithInt64 ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.Int64);

            actual.Should().Be(typeof(long));
        }

        [TestMethod]
        public void ToClrType_WithObject ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.Object);

            actual.Should().Be(typeof(object));
        }

        [TestMethod]
        public void ToClrType_WithSByte ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.SByte);

            actual.Should().Be(typeof(sbyte));
        }

        [TestMethod]
        public void ToClrType_WithSingle ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.Single);

            actual.Should().Be(typeof(float));
        }

        [TestMethod]
        public void ToClrType_WithString ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.String);

            actual.Should().Be(typeof(string));
        }

        [TestMethod]
        public void ToClrType_WithStringFixedLength ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.StringFixedLength);

            actual.Should().Be(typeof(string));
        }

        [TestMethod]
        public void ToClrType_WithTime ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.Time);

            actual.Should().Be(typeof(TimeSpan));
        }

        [TestMethod]
        public void ToClrType_WithUInt16 ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.UInt16);

            actual.Should().Be(typeof(ushort));
        }

        [TestMethod]
        public void ToClrType_WithUInt32 ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.UInt32);

            actual.Should().Be(typeof(uint));
        }

        [TestMethod]
        public void ToClrType_WithUInt64 ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.UInt64);

            actual.Should().Be(typeof(ulong));
        }

        [TestMethod]
        public void ToClrType_WithVarNumeric ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.VarNumeric);

            actual.Should().Be(typeof(decimal));
        }

        [TestMethod]
        public void ToClrType_WithXml ()
        {
            var actual = DbTypeMapper.ToClrType(DbType.Xml);

            actual.Should().Be(typeof(XElement));
        }

        [TestMethod]
        public void ToClrType_WithUnknownType ()
        {
            var actual = DbTypeMapper.ToClrType((DbType)(-1));

            actual.Should().Be(typeof(object));
        }
        #endregion

        #region ToDbType

        [TestMethod]
        public void ToDbType_WithBinary ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(Binary));

            //Assert
            actual.Should().Be(DbType.Binary);
        }

        [TestMethod]
        public void ToDbType_WithBoolean ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(bool));

            //Assert
            actual.Should().Be(DbType.Boolean);
        }
        
        [TestMethod]
        public void ToDbType_WithByte ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(byte));

            //Assert
            actual.Should().Be(DbType.Byte);
        }        

        [TestMethod]
        public void ToDbType_WithByteArray ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(byte[]));

            //Assert
            actual.Should().Be(DbType.Binary);
        }

        [TestMethod]
        public void ToDbType_WithChar ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(char));

            //Assert
            actual.Should().Be(DbType.String);
        }

        [TestMethod]
        public void ToDbType_WithCharArray ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(char[]));

            //Assert
            actual.Should().Be(DbType.String);
        }

        [TestMethod]
        public void ToDbType_WithDate ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(Date));

            //Assert
            actual.Should().Be(DbType.Date);
        }

        [TestMethod]
        public void ToDbType_WithDateTime ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(DateTime));

            //Assert
            actual.Should().Be(DbType.DateTime);
        }

        [TestMethod]
        public void ToDbType_WithDateTimeOffset ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(DateTimeOffset));

            //Assert
            actual.Should().Be(DbType.DateTimeOffset);
        }

        [TestMethod]
        public void ToDbType_WithDecimal ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(decimal));

            //Assert
            actual.Should().Be(DbType.Decimal);
        }

        [TestMethod]
        public void ToDbType_WithDouble ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(double));

            //Assert
            actual.Should().Be(DbType.Double);
        }

        [TestMethod]
        public void ToDbType_WithGuid ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(Guid));

            //Assert
            actual.Should().Be(DbType.Guid);
        }

        [TestMethod]
        public void ToDbType_WithInt16 ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(short));

            //Assert
            actual.Should().Be(DbType.Int16);
        }

        [TestMethod]
        public void ToDbType_WithInt32 ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(int));

            //Assert
            actual.Should().Be(DbType.Int32);
        }

        [TestMethod]
        public void ToDbType_WithInt64 ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(long));

            //Assert
            actual.Should().Be(DbType.Int64);
        }

        [TestMethod]
        public void ToDbType_WithMoney ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(Money));

            //Assert
            actual.Should().Be(DbType.Currency);
        }

        [TestMethod]
        public void ToDbType_WithNullableBoolean ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(bool?));

            //Assert
            actual.Should().Be(DbType.Boolean);
        }

        [TestMethod]
        public void ToDbType_WithNullableByte ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(byte?));

            //Assert
            actual.Should().Be(DbType.Byte);
        }

        [TestMethod]
        public void ToDbType_WithNullableChar ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(char?));

            //Assert
            actual.Should().Be(DbType.String);
        }

        [TestMethod]
        public void ToDbType_WithNullableDateTime ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(DateTime?));

            //Assert
            actual.Should().Be(DbType.DateTime);
        }

        [TestMethod]
        public void ToDbType_WithNullableDateTimeOffset ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(DateTimeOffset?));

            //Assert
            actual.Should().Be(DbType.DateTimeOffset);
        }

        [TestMethod]
        public void ToDbType_WithNullableDecimal ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(decimal?));

            //Assert
            actual.Should().Be(DbType.Decimal);
        }

        [TestMethod]
        public void ToDbType_WithNullableDouble ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(double?));

            //Assert
            actual.Should().Be(DbType.Double);
        }

        [TestMethod]
        public void ToDbType_WithNullableGuid ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(Guid?));

            //Assert
            actual.Should().Be(DbType.Guid);
        }

        [TestMethod]
        public void ToDbType_WithNullableInt16 ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(short?));

            //Assert
            actual.Should().Be(DbType.Int16);
        }

        [TestMethod]
        public void ToDbType_WithNullableInt32 ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(int?));

            //Assert
            actual.Should().Be(DbType.Int32);
        }

        [TestMethod]
        public void ToDbType_WithNullableInt64 ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(long?));

            //Assert
            actual.Should().Be(DbType.Int64);
        }

        [TestMethod]
        public void ToDbType_WithNullableSByte ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(sbyte?));

            //Assert
            actual.Should().Be(DbType.SByte);
        }

        [TestMethod]
        public void ToDbType_WithNullableSingle ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(float?));

            //Assert
            actual.Should().Be(DbType.Single);
        }

        [TestMethod]
        public void ToDbType_WithNullableTimeSpan ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(TimeSpan?));

            //Assert
            actual.Should().Be(DbType.Time);
        }

        [TestMethod]
        public void ToDbType_WithNullableUInt16 ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(ushort?));

            //Assert
            actual.Should().Be(DbType.UInt16);
        }

        [TestMethod]
        public void ToDbType_WithNullableUInt32 ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(uint?));

            //Assert
            actual.Should().Be(DbType.UInt32);
        }

        [TestMethod]
        public void ToDbType_WithNullableUInt64 ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(ulong?));

            //Assert
            actual.Should().Be(DbType.UInt64);
        }    

        [TestMethod]
        public void ToDbType_WithSByte ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(sbyte));

            //Assert
            actual.Should().Be(DbType.SByte);
        }

        [TestMethod]
        public void ToDbType_WithSingle ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(float));

            //Assert
            actual.Should().Be(DbType.Single);
        }

        [TestMethod]
        public void ToDbType_WithString ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(string));

            //Assert
            actual.Should().Be(DbType.String);
        }

        [TestMethod]
        public void ToDbType_WithTimeSpan ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(TimeSpan));

            //Assert
            actual.Should().Be(DbType.Time);
        }

        [TestMethod]
        public void ToDbType_WithUInt16 ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(ushort));

            //Assert
            actual.Should().Be(DbType.UInt16);
        }

        [TestMethod]
        public void ToDbType_WithUInt32 ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(uint));

            //Assert
            actual.Should().Be(DbType.UInt32);
        }

        [TestMethod]
        public void ToDbType_WithUInt64 ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(ulong));

            //Assert
            actual.Should().Be(DbType.UInt64);
        }        

        [TestMethod]
        public void ToDbType_WithXElement ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(XElement));

            //Assert
            actual.Should().Be(DbType.Xml);
        }

        [TestMethod]
        public void ToDbType_WithUnknownType ()
        {
            var actual = DbTypeMapper.ToDbType(typeof(DbTypeMapperTests));

            //Assert
            actual.Should().Be(DbType.Object);
        }
        #endregion
    }
}
