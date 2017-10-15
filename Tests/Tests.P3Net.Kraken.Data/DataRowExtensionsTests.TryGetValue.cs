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
        #region TryGetBooleanValue

        [TestMethod]
        public void TryGetBooleanValue_ColumnIsValidAndSet ()
        {
            //Arrange                    
            bool expected = true;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(bool), expected));

            //Act            
            var actual = CallTryGetValue<bool>("Column1", target.TryGetBooleanValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetBooleanValue_ColumnIsValidAndNull ()
        {
            //Arrange                    
            var target = CreateRow(new ColumnDefinition("Column1", typeof(bool)));

            //Act            
            var actual = CallTryGetValue<bool>("Column1", target.TryGetBooleanValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetBooleanValue_ColumnValueIsInvalid ()
        {
            //Arrange                    
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            //Act            
            var actual = CallTryGetValue<bool>("Column1", target.TryGetBooleanValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetBooleanValue_ColumnIsMissing ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<bool>("Column1", target.TryGetBooleanValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetBooleanValue_ColumnIsNull ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<bool>(null, target.TryGetBooleanValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]        
        public void TryGetBooleanValue_ColumnIsEmpty ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<bool>("", target.TryGetBooleanValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetBooleanValue_ColumnIsYes ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Yes"));

            //Act
            var actual = CallTryGetValue<bool>("Column1", target.TryGetBooleanValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().BeTrue();
            
        }

        [TestMethod]
        public void TryGetBooleanValue_ColumnIsNo ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "No"));

            //Act
            var actual = CallTryGetValue<bool>("Column1", target.TryGetBooleanValue);

            //Assert            
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetBooleanValue_ColumnIsInt8 ()
        {
            var target = CreateRow(new ColumnDefinition("Column1", typeof(sbyte), 10));

            var actual = CallTryGetValue<bool>("Column1", target.TryGetBooleanValue);

            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().BeTrue();
        }

        [TestMethod]
        public void TryGetBooleanValue_ColumnIsInt16 ()
        {
            var target = CreateRow(new ColumnDefinition("Column1", typeof(short), 310));

            var actual = CallTryGetValue<bool>("Column1", target.TryGetBooleanValue);

            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().BeTrue();
        }

        [TestMethod]
        public void TryGetBooleanValue_ColumnIsInt32 ()
        {
            var target = CreateRow(new ColumnDefinition("Column1", typeof(int), 515110));

            var actual = CallTryGetValue<bool>("Column1", target.TryGetBooleanValue);

            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().BeTrue();
        }

        [TestMethod]
        public void TryGetBooleanValue_ColumnIsInt64 ()
        {
            var target = CreateRow(new ColumnDefinition("Column1", typeof(long), 57477810));

            var actual = CallTryGetValue<bool>("Column1", target.TryGetBooleanValue);

            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().BeTrue();
        }

        [TestMethod]
        public void TryGetBooleanValue_ColumnIsUInt8 ()
        {
            var target = CreateRow(new ColumnDefinition("Column1", typeof(byte), 10));

            var actual = CallTryGetValue<bool>("Column1", target.TryGetBooleanValue);

            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().BeTrue();
        }

        [TestMethod]
        public void TryGetBooleanValue_ColumnIsUInt16 ()
        {
            var target = CreateRow(new ColumnDefinition("Column1", typeof(ushort), 310));

            var actual = CallTryGetValue<bool>("Column1", target.TryGetBooleanValue);

            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().BeTrue();
        }

        [TestMethod]
        public void TryGetBooleanValue_ColumnIsUInt32 ()
        {
            var target = CreateRow(new ColumnDefinition("Column1", typeof(uint), 515110));

            var actual = CallTryGetValue<bool>("Column1", target.TryGetBooleanValue);

            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().BeTrue();
        }

        [TestMethod]
        public void TryGetBooleanValue_ColumnIsUInt64 ()
        {
            var target = CreateRow(new ColumnDefinition("Column1", typeof(ulong), 57477810));

            var actual = CallTryGetValue<bool>("Column1", target.TryGetBooleanValue);

            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().BeTrue();
        }
        #endregion

        #region TryGetByteValue

        [TestMethod]
        public void TryGetByteValue_ColumnIsValidAndSet ()
        {
            //Arrange                    
            byte expected = 5;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(byte), expected));

            //Act            
            var actual = CallTryGetValue<byte>("Column1", target.TryGetByteValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetByteValue_ColumnIsValidAndNull ()
        {
            //Arrange                    
            var target = CreateRow(new ColumnDefinition("Column1", typeof(byte)));

            //Act            
            var actual = CallTryGetValue<byte>("Column1", target.TryGetByteValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(0);
        }
        
        [TestMethod]
        public void TryGetByteValue_ColumnValueIsInvalid ()
        {
            //Arrange                    
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            //Act            
            var actual = CallTryGetValue<byte>("Column1", target.TryGetByteValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetByteValue_ColumnIsMissing ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<byte>("Column1", target.TryGetByteValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetByteValue_ColumnIsNull ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<byte>(null, target.TryGetByteValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetByteValue_ColumnIsEmpty ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<byte>("", target.TryGetByteValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetByteValue_ColumnValueIsTooLarge ()
        {
            //Arrange                    
            ushort expected = Byte.MaxValue + 1;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(ushort), expected));

            //Act            
            var actual = CallTryGetValue<byte>("Column1", target.TryGetByteValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetByteValue_ColumnIsLargerButValueFits ()
        {
            //Arrange                    
            byte expected = Byte.MaxValue - 1;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(ushort), expected));

            //Act            
            var actual = CallTryGetValue<byte>("Column1", target.TryGetByteValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }
        #endregion

        #region TryGetCharValue

        [TestMethod]
        public void TryGetCharValue_ColumnIsValidAndSet ()
        {
            //Arrange                    
            var expected = 'D';
            var target = CreateRow(new ColumnDefinition("Column1", typeof(char), expected));

            //Act            
            var actual = CallTryGetValue<char>("Column1", target.TryGetCharValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetCharValue_ColumnIsValidAndNull ()
        {
            //Arrange                    
            var target = CreateRow(new ColumnDefinition("Column1", typeof(char)));

            //Act            
            var actual = CallTryGetValue<char>("Column1", target.TryGetCharValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(default(char));
        }
        
        [TestMethod]
        public void TryGetCharValue_ColumnValueIsInvalid ()
        {
            //Arrange                    
            var target = CreateRow(new ColumnDefinition("Column1", typeof(int), 45));

            //Act            
            var actual = CallTryGetValue<char>("Column1", target.TryGetCharValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetCharValue_ColumnIsMissing ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<char>("Column1", target.TryGetCharValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetCharValue_ColumnIsNull ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<char>(null, target.TryGetCharValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetCharValue_ColumnIsEmpty ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<char>("", target.TryGetCharValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }
        #endregion

        #region TryGetDateValue

        [TestMethod]
        public void TryGetDateValue_ColumnIsValidAndSet ()
        {
            //Arrange                    
            var expected = Dates.May(10, 1998);
            var target = CreateRow(new ColumnDefinition("Column1", typeof(Date), expected));

            //Act            
            var actual = CallTryGetValue<Date>("Column1", target.TryGetDateValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetDateValue_ColumnIsValidAndNull ()
        {
            //Arrange                    
            var target = CreateRow(new ColumnDefinition("Column1", typeof(Date)));

            //Act            
            var actual = CallTryGetValue<Date>("Column1", target.TryGetDateValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(Date.None);
        }

        [TestMethod]
        public void TryGetDateValue_ColumnValueIsInvalid ()
        {
            //Arrange                    
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            //Act            
            var actual = CallTryGetValue<Date>("Column1", target.TryGetDateValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetDateValue_ColumnIsMissing ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<Date>("Column1", target.TryGetDateValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetDateValue_ColumnIsNull ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<Date>(null, target.TryGetDateValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetDateValue_ColumnIsEmpty ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<Date>("", target.TryGetDateValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }
        #endregion

        #region TryGetDateTimeValue

        [TestMethod]
        public void TryGetDateTimeValue_ColumnIsValidAndSet ()
        {
            //Arrange                    
            var expected = new DateTime(2012, 4, 19, 12, 34, 56);
            var target = CreateRow(new ColumnDefinition("Column1", typeof(DateTime), expected));

            //Act            
            var actual = CallTryGetValue<DateTime>("Column1", target.TryGetDateTimeValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetDateTimeValue_ColumnIsValidAndNull ()
        {
            //Arrange                    
            var target = CreateRow(new ColumnDefinition("Column1", typeof(DateTime)));

            //Act            
            var actual = CallTryGetValue<DateTime>("Column1", target.TryGetDateTimeValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(DateTime.MinValue);
        }

        [TestMethod]
        public void TryGetDateTimeValue_ColumnValueIsInvalid ()
        {
            //Arrange                    
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            //Act            
            var actual = CallTryGetValue<DateTime>("Column1", target.TryGetDateTimeValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetDateTimeValue_ColumnIsMissing ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<DateTime>("Column1", target.TryGetDateTimeValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetDateTimeValue_ColumnIsNull ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<DateTime>(null, target.TryGetDateTimeValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetDateTimeValue_ColumnIsEmpty ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<DateTime>("", target.TryGetDateTimeValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }
        #endregion

        #region TryGetDecimalValue

        [TestMethod]
        public void TryGetDecimalValue_ColumnIsValidAndSet ()
        {
            //Arrange                    
            var expected = 123456789M;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(decimal), expected));

            //Act            
            var actual = CallTryGetValue<decimal>("Column1", target.TryGetDecimalValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetDecimalValue_ColumnIsValidAndNull ()
        {
            //Arrange                    
            var target = CreateRow(new ColumnDefinition("Column1", typeof(decimal)));

            //Act            
            var actual = CallTryGetValue<decimal>("Column1", target.TryGetDecimalValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(0);
        }

        [TestMethod]
        public void TryGetDecimalValue_ColumnValueIsInvalid ()
        {
            //Arrange                    
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            //Act            
            var actual = CallTryGetValue<decimal>("Column1", target.TryGetDecimalValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetDecimalValue_ColumnIsMissing ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<decimal>("Column1", target.TryGetDecimalValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetDecimalValue_ColumnIsNull ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<decimal>(null, target.TryGetDecimalValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetDecimalValue_ColumnIsEmpty ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<decimal>("", target.TryGetDecimalValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetDecimalValue_ColumnIsSmaller ()
        {
            //Arrange                    
            int expected = 34;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(int), expected));

            //Act            
            var actual = CallTryGetValue<decimal>("Column1", target.TryGetDecimalValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }
        #endregion

        #region TryGetDoubleValue

        [TestMethod]
        public void TryGetDoubleValue_ColumnIsValidAndSet ()
        {
            //Arrange                    
            var expected = 1234.5678;            
            var target = CreateRow(new ColumnDefinition("Column1", typeof(double), expected));
            
            //Act            
            var actual = CallTryGetValue<double>("Column1", target.TryGetDoubleValue);
            
            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().BeApproximately(expected);
        }
        
        [TestMethod]
        public void TryGetDoubleValue_ColumnIsValidAndNull ()
        {
            //Arrange                    
            var target = CreateRow(new ColumnDefinition("Column1", typeof(double)));

            //Act            
            var actual = CallTryGetValue<double>("Column1", target.TryGetDoubleValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().BeExactly(0);
        }
        
        [TestMethod]
        public void TryGetDoubleValue_ColumnValueIsInvalid ()
        {
            //Arrange                    
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            //Act            
            var actual = CallTryGetValue<double>("Column1", target.TryGetDoubleValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetDoubleValue_ColumnIsMissing ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<double>("Column1", target.TryGetDoubleValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetDoubleValue_ColumnIsNull ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<double>(null, target.TryGetDoubleValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();    
        }

        [TestMethod]
        public void TryGetDoubleValue_ColumnIsEmpty ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<double>("", target.TryGetDoubleValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();    
        }

        [TestMethod]
        public void TryGetDoubleValue_ColumnIsSmaller ()
        {
            //Arrange                    
            int expected = 34;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(int), expected));

            //Act            
            var actual = CallTryGetValue<double>("Column1", target.TryGetDoubleValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().BeExactly(expected);
        }
        #endregion

        #region TryGetInt16Value

        [TestMethod]
        public void TryGetInt16Value_ColumnIsValidAndSet ()
        {
            //Arrange                    
            short expected = 45;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(short), expected));

            //Act            
            var actual = CallTryGetValue<short>("Column1", target.TryGetInt16Value);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetInt16Value_ColumnIsValidAndNull ()
        {
            //Arrange                    
            var target = CreateRow(new ColumnDefinition("Column1", typeof(short)));

            //Act            
            var actual = CallTryGetValue<short>("Column1", target.TryGetInt16Value);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(0);
        }
        
        [TestMethod]
        public void TryGetInt16Value_ColumnValueIsInvalid ()
        {
            //Arrange                    
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            //Act            
            var actual = CallTryGetValue<short>("Column1", target.TryGetInt16Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetInt16Value_ColumnIsMissing ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<short>("Column1", target.TryGetInt16Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetInt16Value_ColumnIsNull ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<short>(null, target.TryGetInt16Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();            
        }

        [TestMethod]
        public void TryGetInt16Value_ColumnIsEmpty ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<short>("", target.TryGetInt16Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetInt16Value_ColumnIsSmaller ()
        {
            //Arrange                    
            sbyte expected = 45;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(sbyte), expected));

            //Act            
            var actual = CallTryGetValue<short>("Column1", target.TryGetInt16Value);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetInt16Value_ColumnIsLargerButFits ()
        {
            //Arrange                    
            short expected = Int16.MaxValue - 1;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(int), expected));

            //Act            
            var actual = CallTryGetValue<short>("Column1", target.TryGetInt16Value);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetInt16Value_ColumnIsLargerAndDoesNotFit ()
        {
            //Arrange                    
            int expected = Int16.MaxValue + 1;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(int), expected));

            //Act            
            var actual = CallTryGetValue<short>("Column1", target.TryGetInt16Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();            
        }
        #endregion

        #region TryGetInt32Value

        [TestMethod]
        public void TryGetInt32Value_ColumnIsValidAndSet ()
        {
            //Arrange                    
            int expected = 1234;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(int), expected));

            //Act            
            var actual = CallTryGetValue<int>("Column1", target.TryGetInt32Value);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetInt32Value_ColumnIsValidAndNull ()
        {
            //Arrange                    
            var target = CreateRow(new ColumnDefinition("Column1", typeof(int)));

            //Act            
            var actual = CallTryGetValue<int>("Column1", target.TryGetInt32Value);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(0);
        }

        [TestMethod]
        public void TryGetInt32Value_ColumnValueIsInvalid ()
        {
            //Arrange                    
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            //Act            
            var actual = CallTryGetValue<int>("Column1", target.TryGetInt32Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetInt32Value_ColumnIsMissing ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<int>("Column1", target.TryGetInt32Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetInt32Value_ColumnIsNull ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<int>(null, target.TryGetInt32Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetInt32Value_ColumnIsEmpty ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<int>("", target.TryGetInt32Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetInt32Value_ColumnIsSmaller ()
        {
            //Arrange                    
            short expected = 4321;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(short), expected));

            //Act            
            var actual = CallTryGetValue<int>("Column1", target.TryGetInt32Value);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetInt32Value_ColumnIsLargerButFits ()
        {
            //Arrange                    
            int expected = Int32.MaxValue - 1;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(long), expected));

            //Act            
            var actual = CallTryGetValue<int>("Column1", target.TryGetInt32Value);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetInt32Value_ColumnIsLargerAndDoesNotFit ()
        {
            //Arrange                    
            long expected = Int32.MaxValue + 1L;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(long), expected));

            //Act            
            var actual = CallTryGetValue<int>("Column1", target.TryGetInt32Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }
        #endregion

        #region TryGetInt64Value
        
        [TestMethod]
        public void TryGetInt64Value_ColumnIsValidAndSet ()
        {
            //Arrange                    
            var expected = 123456789L;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(long), expected));

            //Act            
            var actual = CallTryGetValue<long>("Column1", target.TryGetInt64Value);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }
        
        [TestMethod]
        public void TryGetInt64Value_ColumnIsValidAndNull ()
        {
            //Arrange                    
            var target = CreateRow(new ColumnDefinition("Column1", typeof(long)));

            //Act            
            var actual = CallTryGetValue<long>("Column1", target.TryGetInt64Value);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(0);
        }

        [TestMethod]
        public void TryGetInt64Value_ColumnValueIsInvalid ()
        {
            //Arrange                    
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));            

            //Act            
            var actual = CallTryGetValue<long>("Column1", target.TryGetInt64Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetInt64Value_ColumnIsMissing ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<long>("Column1", target.TryGetInt64Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetInt64Value_ColumnIsNull ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<long>(null, target.TryGetInt64Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetInt64Value_ColumnIsEmpty ()
        {
            //Arrange                    
            var target = CreateRow(new ColumnDefinition("Column1", typeof(long)));

            //Act            
            var actual = CallTryGetValue<long>("", target.TryGetInt64Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetInt64Value_ColumnIsSmaller ()
        {
            //Arrange                    
            int expected = 654321;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(int), expected));

            //Act            
            var actual = CallTryGetValue<long>("Column1", target.TryGetInt64Value);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }
        #endregion

        #region TryGetSByteValue
        
        [TestMethod]
        public void TryGetSByteValue_ColumnIsValidAndSet ()
        {
            //Arrange
            sbyte expected = -45;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(sbyte), expected));

            //Act            
            var actual = CallTryGetValue<sbyte>("Column1", target.TryGetSByteValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetSByteValue_ColumnIsValidAndNull ()
        {
            //Arrange                    
            var target = CreateRow(new ColumnDefinition("Column1", typeof(sbyte)));

            //Act            
            var actual = CallTryGetValue<sbyte>("Column1", target.TryGetSByteValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(0);
        }

        [TestMethod]
        public void TryGetSByteValue_ColumnValueIsInvalid ()
        {
            //Arrange                    
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            //Act            
            var actual = CallTryGetValue<sbyte>("Column1", target.TryGetSByteValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetSByteValue_ColumnIsMissing ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<sbyte>("Column1", target.TryGetSByteValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetSByteValue_ColumnIsNull ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<sbyte>(null, target.TryGetSByteValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetSByteValue_ColumnIsEmpty ()
        {
            //Arrange                    
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<sbyte>("", target.TryGetSByteValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetSByteValue_ColumnIsLargerButFits ()
        {
            //Arrange                    
            sbyte expected = -100;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(short), expected));
          
            //Act            
            var actual = CallTryGetValue<sbyte>("Column1", target.TryGetSByteValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetSByteValue_ColumnIsLargerAndDoesNotFit ()
        {
            //Arrange                    
            int expected = SByte.MaxValue + 1;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(int), expected));

            //Act            
            var actual = CallTryGetValue<sbyte>("Column1", target.TryGetSByteValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }
        #endregion

        #region TryGetSingleValue

        [TestMethod]
        public void TryGetSingleValue_ColumnIsValidAndSet ()
        {            
            //Arrange
            float expected = 9876.0F;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(float), expected));

            //Act            
            var actual = CallTryGetValue<float>("Column1", target.TryGetSingleValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().BeApproximately(expected);            
        }

        [TestMethod]
        public void TryGetSingleValue_ColumnIsValidAndNull ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(float)));

            //Act            
            var actual = CallTryGetValue<float>("Column1", target.TryGetSingleValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().BeExactly(0);
        }
        
        [TestMethod]
        public void TryGetSingleValue_ColumnIsMissing ()
        {
            //Arrange
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<float>("Column1", target.TryGetSingleValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetSingleValue_ColumnIsNull ()
        {
            //Arrange
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<float>(null, target.TryGetSingleValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetSingleValue_ColumnIsEmpty ()
        {
            //Arrange
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<float>("", target.TryGetSingleValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetSingleValue_ColumnIsSmaller ()
        {
            //Arrange
            int expected = 54321;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(int), expected));

            //Act            
            var actual = CallTryGetValue<float>("Column1", target.TryGetSingleValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().BeExactly(expected);  
        }

        [TestMethod]
        public void TryGetSingleValue_ColumnIsLargerButFits ()
        {            
            //Arrange
            float expected = 9876.5432F;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(double), expected));

            //Act            
            var actual = CallTryGetValue<float>("Column1", target.TryGetSingleValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().BeApproximately(expected);  
        }

        [TestMethod]
        public void TryGetSingleValue_ColumnIsLargerAndDoesNotFit ()
        {
            //Arrange
            double expected = 10E100;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(double), expected));

            //Act            
            var actual = CallTryGetValue<float>("Column1", target.TryGetSingleValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }
        #endregion

        #region TryGetStringValue

        [TestMethod]
        public void TryGetStringValue_ColumnIsValidAndSet ()
        {            
            //Arrange
            var expected = "Hello";
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), expected));

            //Act            
            var actual = CallTryGetValue<string>("Column1", target.TryGetStringValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetStringValue_ColumnIsValidAndNull ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string)));

            //Act            
            var actual = CallTryGetValue<string>("Column1", target.TryGetStringValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().BeEmpty();
        }
        
        [TestMethod]
        public void TryGetStringValue_ColumnIsMissing ()
        {
            //Arrange
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<string>("Column1", target.TryGetStringValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetStringValue_ColumnIsNull ()
        {
            //Arrange
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<string>(null, target.TryGetStringValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetStringValue_ColumnIsEmpty ()
        {
            //Arrange
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<string>("", target.TryGetStringValue);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetStringValue_ColumnIsNotStringSucceeds ()
        {
            //Arrange
            int expected = 45;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(int), expected));

            //Act            
            var actual = CallTryGetValue<string>("Column1", target.TryGetStringValue);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected.ToString());
        }   
        #endregion

        #region TryGetUInt16Value

        [TestMethod]
        public void TryGetUInt16Value_ColumnIsValidAndSet ()
        {            
            //Arrange
            ushort expected = 65432;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(ushort), expected));

            //Act            
            var actual = CallTryGetValue<ushort>("Column1", target.TryGetUInt16Value);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetUInt16Value_ColumnIsValidAndNull ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(ushort)));

            //Act            
            var actual = CallTryGetValue<ushort>("Column1", target.TryGetUInt16Value);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(0);
        }

        [TestMethod]
        public void TryGetUInt16Value_ColumnValueIsInvalid ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            //Act            
            var actual = CallTryGetValue<ushort>("Column1", target.TryGetUInt16Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetUInt16Value_ColumnIsMissing ()
        {
            //Arrange
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<ushort>("Column1", target.TryGetUInt16Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetUInt16Value_ColumnIsNull ()
        {
            //Arrange
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<ushort>(null, target.TryGetUInt16Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetUInt16Value_ColumnIsEmpty ()
        {
            //Arrange
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<ushort>("", target.TryGetUInt16Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetUInt16Value_ColumnIsSmaller ()
        {
            //Arrange
            byte expected = 34;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(byte), expected));

            //Act            
            var actual = CallTryGetValue<ushort>("Column1", target.TryGetUInt16Value);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetUInt16Value_ColumnIsLargerButFits ()
        {            
            //Arrange
            ushort expected = UInt16.MaxValue - 1;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(uint), expected));

            //Act            
            var actual = CallTryGetValue<ushort>("Column1", target.TryGetUInt16Value);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetUInt16Value_ColumnIsLargerAndDoesNotFit ()
        {
            //Arrange
            uint expected = UInt16.MaxValue + 1;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(uint), expected));

            //Act            
            var actual = CallTryGetValue<ushort>("Column1", target.TryGetUInt16Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }
        #endregion

        #region TryGetUInt32Value

        [TestMethod]
        public void TryGetUInt32Value_ColumnIsValidAndSet ()
        {            
            //Arrange
            uint expected = 3456789U;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(uint), expected));

            //Act            
            var actual = CallTryGetValue<uint>("Column1", target.TryGetUInt32Value);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetUInt32Value_ColumnIsValidAndNull ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(uint)));

            //Act            
            var actual = CallTryGetValue<uint>("Column1", target.TryGetUInt32Value);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(0);
        }
        
        [TestMethod]
        public void TryGetUInt32Value_ColumnValueIsInvalid ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            //Act            
            var actual = CallTryGetValue<uint>("Column1", target.TryGetUInt32Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetUInt32Value_ColumnIsMissing ()
        {
            //Arrange
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<uint>("Column1", target.TryGetUInt32Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetUInt32Value_ColumnIsNull ()
        {
            //Arrange
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<uint>(null, target.TryGetUInt32Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetUInt32Value_ColumnIsEmpty ()
        {
            //Arrange
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<uint>("", target.TryGetUInt32Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetUInt32Value_ColumnIsSmaller ()
        {
            //Arrange
            ushort expected = 65432;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(ushort), expected));

            //Act            
            var actual = CallTryGetValue<uint>("Column1", target.TryGetUInt32Value);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetUInt32Value_ColumnIsLargerButFits ()
        {
            //Arrange
            uint expected = UInt32.MaxValue - 1;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(ulong), expected));

            //Act            
            var actual = CallTryGetValue<uint>("Column1", target.TryGetUInt32Value);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetUInt32Value_ColumnIsLargerAndDoesNotFit ()
        {
            //Arrange
            ulong expected = UInt32.MaxValue + 1L;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(ulong), expected));

            //Act            
            var actual = CallTryGetValue<uint>("Column1", target.TryGetUInt32Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }
        #endregion

        #region TryGetUInt64Value

        [TestMethod]
        public void TryGetUInt64Value_ColumnIsValidAndSet ()
        {            
            //Arrange
            ulong expected = 567890123UL;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(ulong), expected));

            //Act            
            var actual = CallTryGetValue<ulong>("Column1", target.TryGetUInt64Value);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetUInt64Value_ColumnIsValidAndNull ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(ulong)));

            //Act            
            var actual = CallTryGetValue<ulong>("Column1", target.TryGetUInt64Value);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(0);
        }

        [TestMethod]
        public void TryGetUInt64Value_ColumnValueIsInvalid ()
        {
            //Arrange
            var target = CreateRow(new ColumnDefinition("Column1", typeof(string), "Bad"));

            //Act            
            var actual = CallTryGetValue<ulong>("Column1", target.TryGetUInt64Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetUInt64Value_ColumnIsMissing ()
        {
            //Arrange
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<ulong>("Column1", target.TryGetUInt64Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetUInt64Value_ColumnIsNull ()
        {
            //Arrange
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<ulong>(null, target.TryGetUInt64Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetUInt64Value_ColumnIsEmpty ()
        {
            //Arrange
            var target = CreateRow();

            //Act            
            var actual = CallTryGetValue<ulong>("", target.TryGetUInt64Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetUInt64Value_ColumnIsSmaller ()
        {
            //Arrange
            uint expected = 3456789U;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(uint), expected));

            //Act            
            var actual = CallTryGetValue<ulong>("Column1", target.TryGetUInt64Value);

            //Assert
            actual.ReturnValue.Should().BeTrue();
            actual.Result.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetUInt64Value_ColumnIsLargerAndDoesNotFit ()
        {
            //Arrange
            var expected = 9876.5432;
            var target = CreateRow(new ColumnDefinition("Column1", typeof(double), expected));

            //Act            
            var actual = CallTryGetValue<ulong>("Column1", target.TryGetUInt64Value);

            //Assert
            actual.ReturnValue.Should().BeFalse();
        }
        #endregion

        #region Private Members
        
        private struct TryGetValueTestResult<T>
        {
            public T Result;
            public bool ReturnValue;
        }
        
        private delegate bool TryGetValueDelegate<T> ( string columnName, out T result );
        private static TryGetValueTestResult<T> CallTryGetValue<T> ( string columnName, TryGetValueDelegate<T> callFunction )
        {
            T actualResult;
            var actualReturn = callFunction(columnName, out actualResult);

            return new TryGetValueTestResult<T>() { Result = actualResult, ReturnValue = actualReturn };
        }
        #endregion
    }
}
