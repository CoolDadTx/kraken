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
        #region TryGetValueAsBoolean

        [TestMethod]
        public void TryGetValueAsBoolean_KeyIsValidAndSet ()
        {
            //Arrange                    
            bool expected = true;
            var target = new Dictionary<string, object>() { { "Key1", expected } };

            //Act        
            bool actualResult;
            var actualReturn = target.TryGetValueAsBoolean("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeTrue();
            actualResult.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetValueAsBoolean_KeyIsValidAndNull ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act            
            bool actualResult;
            var actualReturn = target.TryGetValueAsBoolean("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsBoolean_KeyValueIsInvalid ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };

            //Act            
            bool actualResult;
            var actualReturn = target.TryGetValueAsBoolean("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsBoolean_KeyIsMissing ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>();

            //Act            
            bool actualResult;
            var actualReturn = target.TryGetValueAsBoolean("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryGetValueAsBoolean_KeyIsNull ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>();

            //Act            
            bool actualResult;
            target.TryGetValueAsBoolean(null, out actualResult);
        }
        #endregion

        #region TryGetValueAsByte

        [TestMethod]
        public void TryGetValueAsByte_KeyIsValidAndSet ()
        {
            //Arrange                    
            byte expected = 5;
            var target = new Dictionary<string, object>() { { "Key1", expected } };

            //Act            
            byte actualResult;
            var actualReturn = target.TryGetValueAsByte("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeTrue();
            actualResult.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetValueAsByte_KeyIsValidAndNull ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act            
            byte actualResult;
            var actualReturn = target.TryGetValueAsByte("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }
        
        [TestMethod]
        public void TryGetValueAsByte_KeyValueIsInvalid ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };

            //Act            
            byte actualResult;
            var actualReturn = target.TryGetValueAsByte("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsByte_KeyIsMissing ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>();

            //Act            
            byte actualResult;
            var actualReturn = target.TryGetValueAsByte("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryGetValueAsByte_KeyIsNull ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>();

            //Act            
            byte actualResult;
            target.TryGetValueAsByte(null, out actualResult);
        }
        #endregion

        #region TryGetValueAsChar

        [TestMethod]
        public void TryGetValueAsChar_KeyIsValidAndSet ()
        {
            //Arrange                    
            var expected = 'D';
            var target = new Dictionary<string, object>() { { "Key1", expected } };

            //Act            
            char actualResult;
            var actualReturn = target.TryGetValueAsChar("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeTrue();
            actualResult.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetValueAsChar_KeyIsValidAndNull ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act            
            char actualResult;
            var actualReturn = target.TryGetValueAsChar("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }
        
        [TestMethod]
        public void TryGetValueAsChar_KeyValueIsInvalid ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>() { { "Key1", 45.7 } };

            //Act            
            char actualResult;
            var actualReturn = target.TryGetValueAsChar("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsChar_KeyIsMissing ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>();

            //Act            
            char actualResult;
            var actualReturn = target.TryGetValueAsChar("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryGetValueAsChar_KeyIsNull ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>();

            //Act            
            char actualResult;
            target.TryGetValueAsChar(null, out actualResult);
        }
        #endregion

        #region TryGetValueAsDateTime

        [TestMethod]
        public void TryGetValueAsDateTime_KeyIsValidAndSet ()
        {
            //Arrange                    
            var expected = new DateTime(2012, 4, 19, 12, 34, 56);
            var target = new Dictionary<string, object>() { { "Key1", expected } };

            //Act            
            DateTime actualResult;
            var actualReturn = target.TryGetValueAsDateTime("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeTrue();
            actualResult.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetValueAsDateTime_KeyIsValidAndNull ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act            
            DateTime actualResult;
            var actualReturn = target.TryGetValueAsDateTime("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsDateTime_KeyValueIsInvalid ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };

            //Act            
            DateTime actualResult;
            var actualReturn = target.TryGetValueAsDateTime("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsDateTime_KeyIsMissing ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>();

            //Act            
            DateTime actualResult;
            var actualReturn = target.TryGetValueAsDateTime("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryGetValueAsDateTime_KeyIsNull ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>();

            //Act            
            DateTime actualResult;
            target.TryGetValueAsDateTime(null, out actualResult);
        }
        #endregion

        #region TryGetValueAsDecimal

        [TestMethod]
        public void TryGetValueAsDecimal_KeyIsValidAndSet ()
        {
            //Arrange                    
            var expected = 123456789M;
            var target = new Dictionary<string, object>() { { "Key1", expected } };

            //Act            
            decimal actualResult;
            var actualReturn = target.TryGetValueAsDecimal("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeTrue();
            actualResult.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetValueAsDecimal_KeyIsValidAndNull ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act            
            decimal actualResult;
            var actualReturn = target.TryGetValueAsDecimal("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsDecimal_KeyValueIsInvalid ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };

            //Act            
            decimal actualResult;
            var actualReturn = target.TryGetValueAsDecimal("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsDecimal_KeyIsMissing ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>();

            //Act            
            decimal actualResult;
            var actualReturn = target.TryGetValueAsDecimal("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryGetValueAsDecimal_KeyIsNull ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>();

            //Act            
            decimal actualResult;
            target.TryGetValueAsDecimal(null, out actualResult);
        }
        #endregion

        #region TryGetValueAsDouble

        [TestMethod]
        public void TryGetValueAsDouble_KeyIsValidAndSet ()
        {
            //Arrange                    
            var expected = 1234.5678;            
            var target = new Dictionary<string, object>() { { "Key1", expected } };
            
            //Act            
            double actualResult;
            var actualReturn = target.TryGetValueAsDouble("Key1", out actualResult);
            
            //Assert
            actualReturn.Should().BeTrue();
            actualResult.Should().BeApproximately(expected);
        }
        
        [TestMethod]
        public void TryGetValueAsDouble_KeyIsValidAndNull ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act            
            double actualResult;
            var actualReturn = target.TryGetValueAsDouble("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }
        
        [TestMethod]
        public void TryGetValueAsDouble_KeyValueIsInvalid ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };

            //Act            
            double actualResult;
            var actualReturn = target.TryGetValueAsDouble("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsDouble_KeyIsMissing ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>();

            //Act            
            double actualResult;
            var actualReturn = target.TryGetValueAsDouble("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryGetValueAsDouble_KeyIsNull ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>();

            //Act            
            double actualResult;
            target.TryGetValueAsDouble(null, out actualResult);
        }
        #endregion

        #region TryGetValueAsInt16

        [TestMethod]
        public void TryGetValueAsInt16_KeyIsValidAndSet ()
        {
            //Arrange                    
            short expected = 45;
            var target = new Dictionary<string, object>() { { "Key1", expected } };

            //Act            
            short actualResult;
            var actualReturn = target.TryGetValueAsInt16("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeTrue();
            actualResult.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetValueAsInt16_KeyIsValidAndNull ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act            
            short actualResult;
            var actualReturn = target.TryGetValueAsInt16("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }
        
        [TestMethod]
        public void TryGetValueAsInt16_KeyValueIsInvalid ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };

            //Act            
            short actualResult;
            var actualReturn = target.TryGetValueAsInt16("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsInt16_KeyIsMissing ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>();

            //Act            
            short actualResult;
            var actualReturn = target.TryGetValueAsInt16("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryGetValueAsInt16_KeyIsNull ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>();

            //Act            
            short actualResult;
            target.TryGetValueAsInt16(null, out actualResult);
        }
        #endregion

        #region TryGetValueAsInt32

        [TestMethod]
        public void TryGetValueAsInt32_KeyIsValidAndSet ()
        {
            //Arrange                    
            int expected = 1234;
            var target = new Dictionary<string, object>() { { "Key1", expected } };

            //Act            
            int actualResult;
            var actualReturn = target.TryGetValueAsInt32("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeTrue();
            actualResult.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetValueAsInt32_KeyIsValidAndNull ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act            
            int actualResult;
            var actualReturn = target.TryGetValueAsInt32("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsInt32_KeyValueIsInvalid ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };

            //Act            
            int actualResult;
            var actualReturn = target.TryGetValueAsInt32("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsInt32_KeyIsMissing ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>();

            //Act            
            int actualResult;
            var actualReturn = target.TryGetValueAsInt32("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryGetValueAsInt32_KeyIsNull ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>();

            //Act            
            int actualResult;
            target.TryGetValueAsInt32(null, out actualResult);
        }
        #endregion

        #region TryGetValueAsInt64
        
        [TestMethod]
        public void TryGetValueAsInt64_KeyIsValidAndSet ()
        {
            //Arrange                    
            var expected = 123456789L;
            var target = new Dictionary<string, object>() { { "Key1", expected } };

            //Act            
            long actualResult;
            var actualReturn = target.TryGetValueAsInt64("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeTrue();
            actualResult.Should().Be(expected);
        }
        
        [TestMethod]
        public void TryGetValueAsInt64_KeyIsValidAndNull ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act            
            long actualResult;
            var actualReturn = target.TryGetValueAsInt64("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsInt64_KeyValueIsInvalid ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };            

            //Act            
            long actualResult;
            var actualReturn = target.TryGetValueAsInt64("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsInt64_KeyIsMissing ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>();

            //Act            
            long actualResult;
            var actualReturn = target.TryGetValueAsInt64("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryGetValueAsInt64_KeyIsNull ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>();

            //Act            
            long actualResult;
            var actualReturn = target.TryGetValueAsInt64(null, out actualResult);
        }
        #endregion

        #region TryGetValueAsSByte
        
        [TestMethod]
        public void TryGetValueAsSByte_KeyIsValidAndSet ()
        {
            //Arrange
            sbyte expected = -45;
            var target = new Dictionary<string, object>() { { "Key1", expected } };

            //Act            
            sbyte actualResult;
            var actualReturn = target.TryGetValueAsSByte("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeTrue();
            actualResult.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetValueAsSByte_KeyIsValidAndNull ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act            
            sbyte actualResult;
            var actualReturn = target.TryGetValueAsSByte("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsSByte_KeyValueIsInvalid ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };

            //Act            
            sbyte actualResult;
            var actualReturn = target.TryGetValueAsSByte("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsSByte_KeyIsMissing ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>();

            //Act            
            sbyte actualResult;
            var actualReturn = target.TryGetValueAsSByte("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryGetValueAsSByte_KeyIsNull ()
        {
            //Arrange                    
            var target = new Dictionary<string, object>();

            //Act            
            sbyte actualResult;
            target.TryGetValueAsSByte(null, out actualResult);
        }
        #endregion

        #region TryGetValueAsSingle

        [TestMethod]
        public void TryGetValueAsSingle_KeyIsValidAndSet ()
        {            
            //Arrange
            float expected = 9876.0F;
            var target = new Dictionary<string, object>() { { "Key1", expected } };

            //Act            
            float actualResult;
            var actualReturn = target.TryGetValueAsSingle("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeTrue();
            actualResult.Should().BeApproximately(expected);            
        }

        [TestMethod]
        public void TryGetValueAsSingle_KeyIsValidAndNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act            
            float actualResult;
            var actualReturn = target.TryGetValueAsSingle("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }
        
        [TestMethod]
        public void TryGetValueAsSingle_KeyIsMissing ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act            
            float actualResult;
            var actualReturn = target.TryGetValueAsSingle("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryGetValueAsSingle_KeyIsNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act            
            float actualResult;
            target.TryGetValueAsSingle(null, out actualResult);
        }

        [TestMethod]
        public void TryGetValueAsSingle_KeyValueIsInvalid ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };

            //Act            
            float actualResult;
            var actualReturn = target.TryGetValueAsSingle("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }
        #endregion

        #region TryGetValueAsString

        [TestMethod]
        public void TryGetValueAsString_KeyIsValidAndSet ()
        {            
            //Arrange
            var expected = "Hello";
            var target = new Dictionary<string, object>() { { "Key1", expected } };

            //Act            
            string actualResult;
            var actualReturn = target.TryGetValueAsString("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeTrue();
            actualResult.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetValueAsString_KeyIsValidAndNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act            
            string actualResult;
            var actualReturn = target.TryGetValueAsString("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeTrue();
            actualResult.Should().BeEmpty();
        }
        
        [TestMethod]
        public void TryGetValueAsString_KeyIsMissing ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act            
            string actualResult;
            var actualReturn = target.TryGetValueAsString("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryGetValueAsString_KeyIsNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act            
            string actualResult;
            target.TryGetValueAsString(null, out actualResult);
        }
        
        [TestMethod]
        public void TryGetValueAsString_KeyIsNotStringSucceeds ()
        {
            //Arrange
            int expected = 45;
            var target = new Dictionary<string, object>() { { "Key1", expected } };

            //Act            
            string actualResult;
            var actualReturn = target.TryGetValueAsString("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeTrue();
            actualResult.Should().Be(expected.ToString());
        }   
        #endregion

        #region TryGetValueAsUInt16

        [TestMethod]
        public void TryGetValueAsUInt16_KeyIsValidAndSet ()
        {            
            //Arrange
            ushort expected = 65432;
            var target = new Dictionary<string, object>() { { "Key1", expected } };

            //Act            
            ushort actualResult;
            var actualReturn = target.TryGetValueAsUInt16("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeTrue();
            actualResult.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetValueAsUInt16_KeyIsValidAndNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act            
            ushort actualResult;
            var actualReturn = target.TryGetValueAsUInt16("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsUInt16_KeyValueIsInvalid ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };

            //Act            
            ushort actualResult;
            var actualReturn = target.TryGetValueAsUInt16("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsUInt16_KeyIsMissing ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act            
            ushort actualResult;
            var actualReturn = target.TryGetValueAsUInt16("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryGetValueAsUInt16_KeyIsNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act            
            ushort actualResult;
            target.TryGetValueAsUInt16(null, out actualResult);
        }
        #endregion

        #region TryGetValueAsUInt32

        [TestMethod]
        public void TryGetValueAsUInt32_KeyIsValidAndSet ()
        {            
            //Arrange
            uint expected = 3456789U;
            var target = new Dictionary<string, object>() { { "Key1", expected } };

            //Act            
            uint actualResult;
            var actualReturn = target.TryGetValueAsUInt32("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeTrue();
            actualResult.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetValueAsUInt32_KeyIsValidAndNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act            
            uint actualResult;
            var actualReturn = target.TryGetValueAsUInt32("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }
        
        [TestMethod]
        public void TryGetValueAsUInt32_KeyValueIsInvalid ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };

            //Act            
            uint actualResult;
            var actualReturn = target.TryGetValueAsUInt32("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsUInt32_KeyIsMissing ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act            
            uint actualResult;
            var actualReturn = target.TryGetValueAsUInt32("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryGetValueAsUInt32_KeyIsNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act            
            uint actualResult;
            target.TryGetValueAsUInt32(null, out actualResult);
        }
        #endregion

        #region TryGetValueAsUInt64

        [TestMethod]
        public void TryGetValueAsUInt64_KeyIsValidAndSet ()
        {            
            //Arrange
            ulong expected = 567890123UL;
            var target = new Dictionary<string, object>() { { "Key1", expected } };

            //Act            
            ulong actualResult;
            var actualReturn = target.TryGetValueAsUInt64("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeTrue();
            actualResult.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetValueAsUInt64_KeyIsValidAndNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", null } };

            //Act            
            ulong actualResult;
            var actualReturn = target.TryGetValueAsUInt64("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsUInt64_KeyValueIsInvalid ()
        {
            //Arrange
            var target = new Dictionary<string, object>() { { "Key1", "Bad" } };

            //Act            
            ulong actualResult;
            var actualReturn = target.TryGetValueAsUInt64("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsUInt64_KeyIsMissing ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act            
            ulong actualResult;
            var actualReturn = target.TryGetValueAsUInt64("Key1", out actualResult);

            //Assert
            actualReturn.Should().BeFalse();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryGetValueAsUInt64_KeyIsNull ()
        {
            //Arrange
            var target = new Dictionary<string, object>();

            //Act            
            ulong actualResult;
            target.TryGetValueAsUInt64(null, out actualResult);
        }
        #endregion
    }
}
