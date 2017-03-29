#region Imports

using System;
using System.Collections.Generic;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Collections;
using P3Net.Kraken.UnitTesting;

#endregion

namespace Tests.P3Net.Kraken.Collections
{
    public partial class ObjectDictionaryExtensionsTests
    {
        #region GetValueAsBoolean

        [TestMethod]
        public void GetValueAsBoolean_KeyIsValidAndSet ()
        {            
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", true } };
            
            //Act
            var actual = target.GetValueAsBoolean("Key1");

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void GetValueAsBoolean_KeyIsValidAndNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act
            var actual = target.GetValueAsBoolean("Key1");

            //Assert
            actual.Should().BeFalse();
        }
        
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void GetValueAsBoolean_KeyValueIsInvalid ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };

            //Act
            target.GetValueAsBoolean("Key1");            
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetValueAsBoolean_KeyIsMissing ()
        {
            //Arrange
            var target = new Dictionary<string, object>();     

            //Act
            target.GetValueAsBoolean("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetValueAsBoolean_KeyIsNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsBoolean(null);
        }
        #endregion

        #region GetValueAsByte

        [TestMethod]
        public void GetValueAsByte_KeyIsValidAndSet ()
        {
            //Arrange
            byte expected = 5;
            var target = new Dictionary<string, object>() { { "Key1", expected } };

            //Act
            var actual = target.GetValueAsByte("Key1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetValueAsByte_KeyIsValidAndNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act
            var actual = target.GetValueAsByte("Key1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void GetValueAsByte_KeyValueIsInvalid ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };

            //Act
            target.GetValueAsByte("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetValueAsByte_KeyIsMissing ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsByte("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetValueAsByte_KeyIsNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsByte(null);
        }
        #endregion

        #region GetValueAsChar

        [TestMethod]
        public void GetValueAsChar_KeyIsValidAndSet ()
        {
            //Arrange
            var expected = 'D';
            var target = new Dictionary<string, object>() { { "Key1", expected } };

            //Act
            var actual = target.GetValueAsChar("Key1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetValueAsChar_KeyIsValidAndNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", null } };
            
            //Act
            var actual = target.GetValueAsChar("Key1");

            //Assert
            actual.Should().Be(default(char));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void GetValueAsChar_KeyValueIsInvalid ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };
            
            //Act
            target.GetValueAsChar("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetValueAsChar_KeyIsMissing ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsChar("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetValueAsChar_KeyIsNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsChar(null);
        }
        #endregion

        #region GetValueAsDateTime

        [TestMethod]
        public void GetValueAsDateTime_KeyIsValidAndSet ()
        {
            //Arrange
            var expected = new DateTime(2012, 4, 17, 12, 34, 56);
            var target = new Dictionary<string, object>() { { "Key1", expected } };

            //Act
            var actual = target.GetValueAsDateTime("Key1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetValueAsDateTime_KeyIsValidAndNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act
            var actual = target.GetValueAsDateTime("Key1");

            //Assert
            actual.Should().Be(DateTime.MinValue);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void GetValueAsDateTime_KeyValueIsInvalid ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };

            //Act
            target.GetValueAsDateTime("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetValueAsDateTime_KeyIsMissing ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsDateTime("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetValueAsDateTime_KeyIsNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsDateTime(null);
        }
        #endregion

        #region GetValueAsDecimal

        [TestMethod]
        public void GetValueAsDecimal_KeyIsValidAndSet ()
        {
            //Arrange
            decimal expected = 123456789M;
            var target = new Dictionary<string, object>() { { "Key1", expected } };

            //Act
            var actual = target.GetValueAsDecimal("Key1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetValueAsDecimal_KeyIsValidAndNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act
            var actual = target.GetValueAsDecimal("Key1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void GetValueAsDecimal_KeyValueIsInvalid ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };

            //Act
            target.GetValueAsDecimal("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetValueAsDecimal_KeyIsMissing ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsDecimal("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetValueAsDecimal_KeyIsNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsDecimal(null);
        }
        #endregion

        #region GetValueAsDouble

        [TestMethod]
        public void GetValueAsDouble_KeyIsValidAndSet ()
        {
            //Arrange
            double expected = 1234.5678;
            var target = new Dictionary<string, object>() { { "Key1", expected } };
            
            //Act
            var actual = target.GetValueAsDouble("Key1");

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        public void GetValueAsDouble_KeyIsValidAndNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act
            var actual = target.GetValueAsDouble("Key1");

            //Assert
            actual.Should().BeExactly(0);
        }
        
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void GetValueAsDouble_KeyValueIsInvalid ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };

            //Act
            target.GetValueAsDouble("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetValueAsDouble_KeyIsMissing ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsDouble("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetValueAsDouble_KeyIsNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsDouble(null);
        }
        #endregion

        #region GetValueAsInt16

        [TestMethod]
        public void GetValueAsInt16_KeyIsValidAndSet ()
        {
            //Arrange
            short expected = 45;
            var target = new Dictionary<string, object>() { { "Key1", expected } };

            //Act
            var actual = target.GetValueAsInt16("Key1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetValueAsInt16_KeyIsValidAndNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", null } };
            
            //Act
            var actual = target.GetValueAsInt16("Key1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void GetValueAsInt16_KeyValueIsInvalid ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };

            //Act
            target.GetValueAsInt16("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetValueAsInt16_KeyIsMissing ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsInt16("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetValueAsInt16_KeyIsNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsInt16(null);
        }
        #endregion

        #region GetValueAsInt32

        [TestMethod]
        public void GetValueAsInt32_KeyIsValidAndSet ()
        {
            //Arrange
            int expected = 1234;
            var target = new Dictionary<string, object>() { { "Key1", expected } };
            
            //Act
            var actual = target.GetValueAsInt32("Key1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetValueAsInt32_KeyIsValidAndNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act
            var actual = target.GetValueAsInt32("Key1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void GetValueAsInt32_KeyValueIsInvalid ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };

            //Act
            target.GetValueAsInt32("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetValueAsInt32_KeyIsMissing ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsInt32("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetValueAsInt32_KeyIsNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsInt32(null);
        }
        #endregion

        #region GetValueAsInt64

        [TestMethod]
        public void GetValueAsInt64_KeyIsValidAndSet ()
        {
            //Arrange
            long expected = 123456789L;
            var target = new Dictionary<string, object>() { { "Key1", expected } };
            
            //Act
            var actual = target.GetValueAsInt64("Key1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetValueAsInt64_KeyIsValidAndNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", null } };
            
            //Act
            var actual = target.GetValueAsInt64("Key1");

            //Assert
            actual.Should().Be(0);
        }
        
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void GetValueAsInt64_KeyValueIsInvalid ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };

            //Act
            target.GetValueAsInt64("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetValueAsInt64_KeyIsMissing ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsInt64("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetValueAsInt64_KeyIsNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsInt64(null);
        }
        #endregion

        #region GetValueAsSByte

        [TestMethod]
        public void GetValueAsSByte_KeyIsValidAndSet ()
        {
            //Arrange
            sbyte expected = -45;
            var target = new Dictionary<string, object>() { { "Key1", expected } };
            
            //Act
            var actual = target.GetValueAsSByte("Key1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetValueAsSByte_KeyIsValidAndNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act
            var actual = target.GetValueAsSByte("Key1");

            //Assert
            actual.Should().Be(0);
        }
        
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void GetValueAsSByte_KeyValueIsInvalid ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };

            //Act
            target.GetValueAsSByte("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetValueAsSByte_KeyIsMissing ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsSByte("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetValueAsSByte_KeyIsNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsSByte(null);
        }
        #endregion

        #region GetValueAsSingle

        [TestMethod]
        public void GetValueAsSingle_KeyIsValidAndSet ()
        {
            //Arrange
            var expected = 9876.0F;
            var target = new Dictionary<string, object>() { { "Key1", expected } };
            
            //Act
            var actual = target.GetValueAsSingle("Key1");

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        public void GetValueAsSingle_KeyIsValidAndNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act
            var actual = target.GetValueAsSingle("Key1");

            //Assert
            actual.Should().BeExactly(0);
        }
        
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void GetValueAsSingle_KeyValueIsInvalid ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };

            //Act
            target.GetValueAsSingle("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetValueAsSingle_KeyIsMissing ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsSingle("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetValueAsSingle_KeyIsNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsSingle(null);
        }
        #endregion

        #region GetValueAsString

        [TestMethod]
        public void GetValueAsString_KeyIsValidAndSet ()
        {
            //Arrange
            var expected = "Hello";
            var target = new Dictionary<string, object>() { { "Key1", expected } };
            
            //Act
            var actual = target.GetValueAsString("Key1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetValueAsString_KeyIsValidAndNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act
            var actual = target.GetValueAsString("Key1");

            //Assert
            actual.Should().BeEmpty();
        }
        
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetValueAsString_KeyIsMissing ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsString("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetValueAsString_KeyIsNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsString(null);
        }

        [TestMethod]
        public void GetValueAsString_KeyIsNotStringSucceeds ()
        {
            //Arrange
            var expected = 45;
            var target = new Dictionary<string, object>() { { "Key1", expected } };
            
            //Act
            var actual = target.GetValueAsString("Key1");

            //Assert
            actual.Should().Be(expected.ToString());
        }                
        #endregion

        #region GetValueAsUInt16

        [TestMethod]
        public void GetValueAsUInt16_KeyIsValidAndSet ()
        {
            //Arrange
            ushort expected = 65432;
            var target = new Dictionary<string, object>() { { "Key1", expected } };

            //Act
            var actual = target.GetValueAsUInt16("Key1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetValueAsUInt16_KeyIsValidAndNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act
            var actual = target.GetValueAsUInt16("Key1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void GetValueAsUInt16_KeyValueIsInvalid ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };

            //Act
            target.GetValueAsUInt16("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetValueAsUInt16_KeyIsMissing ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsUInt16("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetValueAsUInt16_KeyIsNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsUInt16(null);
        }
        #endregion

        #region GetValueAsUInt32

        [TestMethod]
        public void GetValueAsUInt32_KeyIsValidAndSet ()
        {
            //Arrange
            var expected = 3456789U;
            var target = new Dictionary<string, object>() { { "Key1", expected } };

            //Act
            var actual = target.GetValueAsUInt32("Key1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetValueAsUInt32_KeyIsValidAndNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act
            var actual = target.GetValueAsUInt32("Key1");

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void GetValueAsUInt32_KeyValueIsInvalid ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };

            //Act
            target.GetValueAsUInt32("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetValueAsUInt32_KeyIsMissing ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsUInt32("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetValueAsUInt32_KeyIsNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsUInt32(null);
        }
        #endregion

        #region GetValueAsUInt64

        [TestMethod]
        public void GetValueAsUInt64_KeyIsValidAndSet ()
        {
            //Arrange
            ulong expected = 567890123UL;
            var target = new Dictionary<string, object>() { { "Key1", expected } };

            //Act
            var actual = target.GetValueAsUInt64("Key1");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetValueAsUInt64_KeyIsValidAndNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act
            var actual = target.GetValueAsUInt64("Key1");

            //Assert
            actual.Should().Be(0);
        }
        
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void GetValueAsUInt64_KeyValueIsInvalid ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };

            //Act
            target.GetValueAsUInt64("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetValueAsUInt64_KeyIsMissing ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsUInt64("Key1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetValueAsUInt64_KeyIsNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act
            target.GetValueAsUInt64(null);
        }
        #endregion
    }
}
