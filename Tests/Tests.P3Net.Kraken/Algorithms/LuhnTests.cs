using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.UnitTesting;
using P3Net.Kraken.Algorithms;

namespace Tests.P3Net.Kraken.Algorithms
{
    [TestClass]
    public class LuhnTests : UnitTest
    {
        #region CalculateCheckDigits

        [TestMethod]
        public void CalculateCheckDigits_CC1 ()
        {            
            var expected = 1;
            var actual = Luhn.CalculateCheckDigit("37144963539843");

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void CalculateCheckDigits_CC2 ()
        {
            var expected = 7;
            var actual = Luhn.CalculateCheckDigit("601111111111111");

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void CalculateCheckDigits_CC3 ()
        {
            var expected = 1;
            var actual = Luhn.CalculateCheckDigit("411111111111111");

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void CalculateCheckDigits_Simple1 ()
        {
            var expected = 9;
            var actual = Luhn.CalculateCheckDigit("456396012200199");

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void CalculateCheckDigits_Zeroes ()
        {
            var expected = 0;
            var actual = Luhn.CalculateCheckDigit("000000");

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void CalculateCheckDigits_NullFails ()
        {
            Action action = () => Luhn.CalculateCheckDigit(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void CalculateCheckDigits_EmptyFails ()
        {
            Action action = () => Luhn.CalculateCheckDigit("");

            action.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CalculateCheckDigits_BadCharsFail ()
        {
            Action action = () => Luhn.CalculateCheckDigit("123ABC456");

            action.Should().Throw<ArgumentException>();
        }
        #endregion

        #region IsValid

        [TestMethod]
        public void IsValid_WithCC1 ()
        {
            var actual = Luhn.IsValid("371449635398431");

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsValid_WithCC2 ()
        {
            var actual = Luhn.IsValid("6011111111111117");

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsValid_WithCC3 ()
        {
            var actual = Luhn.IsValid("4111111111111111");

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsValid_WithAllZeroes ()
        {
            var actual = Luhn.IsValid("000000000000000");

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsValid_WithBadCheckDigit ()
        {
            var actual = Luhn.IsValid("3714496353984315");

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsValid_WithNull ()
        {
            var actual = Luhn.IsValid(null);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsValid_WithEmpty ()
        {
            var actual = Luhn.IsValid("");

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsValid_WithBadChars ()
        {
            var actual = Luhn.IsValid("123ABC456");

            actual.Should().BeFalse();
        }
        #endregion
    }
}
