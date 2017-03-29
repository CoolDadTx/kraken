using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken.Threading;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.Threading
{
    [TestClass]
    public class InterlockTests : UnitTest
    {
        #region And
        
        [TestMethod]
        public void And_Int32Value ()
        {
            int target      = 0x0F0F0F0F;
            int updateValue = 0x0FFF0000;
            int expected    = 0x0F0F0000;

            //Act
            var actual = Interlock.And(ref target, updateValue);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void And_Int64Value ()
        {
            long target      = 0x0F0F0F0F0F0F0F0F;
            long updateValue = 0x0FFFFFFF00000000;
            long expected    = 0x0F0F0F0F00000000;

            //Act
            var actual = Interlock.And(ref target, updateValue);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Or

        [TestMethod]
        public void Or_Int32Value ()
        {
            int target      = 0x0F0F0F0F;
            int updateValue = 0x0FFF0000;
            int expected    = 0x0FFF0F0F;

            //Act
            var actual = Interlock.Or(ref target, updateValue);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Or_Int64Value ()
        {
            long target      = 0x0F0F0F0F00000000;
            long updateValue = 0x000000000F0F0F0F;
            long expected    = 0x0F0F0F0F0F0F0F0F;

            //Act
            var actual = Interlock.Or(ref target, updateValue);
            
            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Xor

        [TestMethod]
        public void Xor_Int32Value ()
        {
            int target      = 0x0F0F0F0F;
            int updateValue = 0x0F00000F;
            int expected    = 0x000F0F00;

            //Act
            var actual = Interlock.Xor(ref target, updateValue);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Xor_Int64Value ()
        {
            long target      = 0x0F0F0F0F00000000;
            long updateValue = 0x0FFFFFFF0F0F0F0F;
            long expected    = 0x00F0F0F00F0F0F0F;

            //Act
            var actual = Interlock.Xor(ref target, updateValue);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion
    }
}
