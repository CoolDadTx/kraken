#region Imports

using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken;
#endregion

namespace Tests.P3Net.Kraken
{    
    public partial class StringExtensionsTests
    {
        #region IsAlpha

        [TestMethod]
        public void IsAlpha_OnlyContainsLetters()
        {
            var actual = "ABC".IsAlpha();

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsAlpha_ContainsLettersAndDigits ()
        {
            var actual = "A1B2".IsAlpha();

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsAlpha_ContainsLettersAndSymbols ()
        {
            var actual = "_ABC".IsAlpha();

            //Assert
            actual.Should().BeFalse();
        }
       
        [TestMethod]
        public void IsAlpha_EmptySource ()
        {
            var actual = "".IsAlpha();
            
            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region IsAlphaNumeric

        [TestMethod]
        public void IsAlphaNumeric_OnlyContainsLetters ()
        {
            var actual = "ABC".IsAlphaNumeric();

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsAlphaNumeric_ContainsLettersAndDigits ()
        {
            var actual = "A1B2".IsAlphaNumeric();

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsAlphaNumeric_ContainsLettersAndSymbols ()
        {
            var actual = "_ABC".IsAlphaNumeric();

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsAlphaNumeric_EmptySource ()
        {
            var actual = "".IsAlphaNumeric();

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region IsEmpty

        [TestMethod]
        public void IsEmpty_NotEmptyIsFalse ()
        {
            var target = "Hello";

            var actual = target.IsEmpty();

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsEmpty_WhitespaceIsFalse ()
        {
            var target = "  ";

            var actual = target.IsEmpty();

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsEmpty_EmptyIsTrue ()
        {
            var target = "";

            var actual = target.IsEmpty();

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsEmpty_NullIsTrue ()
        {
            string target = null;

            var actual = target.IsEmpty();

            //Assert
            actual.Should().BeTrue();
        }
        #endregion

        #region IsIdentifier

        [TestMethod]
        public void IsIdentifier_StartsWithLetterAndIsValid ()
        {
            var actual = "someValue".IsIdentifier();

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsIdentifier_StartsWithLetterAndContainsSymbol ()
        {
            var actual = "some+Value".IsIdentifier();

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsIdentifier_StartsWithUnderscoreAndIsValid ()
        {
            var actual = "_id".IsIdentifier();

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsIdentifier_StartsWithUnderscoreAndContainsSymbols ()
        {
            var actual = "_id-Value".IsIdentifier();

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsIdentifier_StartsWithDigit ()
        {
            var actual = "1Grade".IsIdentifier();

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsIdentifier_ContainsLettersAndDigits ()
        {
            var actual = "grade1".IsIdentifier();

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsIdentifier_ContainsLettersDigitsAndUnderscores ()
        {
            var actual = "grade_1".IsIdentifier();

            //Assert
            actual.Should().BeTrue();
        }
        
        [TestMethod]
        public void IsIdentifier_EmptySource ()
        {
            var actual = "".IsIdentifier();

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region IsNumeric

        [TestMethod]
        public void IsNumeric_OnlyContainsDigits ()
        {
            var actual = "123".IsNumeric();

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsNumeric_ContainsFixedPointValue ()
        {
            var actual = "12.34".IsNumeric();

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsNumeric_ContainsExponentialValue ()
        {
            var actual = "12E10".IsNumeric();

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsNumeric_ContainsLettersAndDigits ()
        {
            var actual = "A1B2".IsNumeric();

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsNumeric_ContainsDigitsAndSymbols ()
        {
            var actual = "1_2_3".IsNumeric();

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsNumeric_EmptySource ()
        {
            var actual = "".IsNumeric();

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region IsWhitespace

        [TestMethod]
        public void IsWhitespace_NotEmptyIsFalse ()
        {
            var target = "Hello";

            var actual = target.IsWhitespace();

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsWhitespace_WhitespaceIsTrue ()
        {
            var target = "  ";

            var actual = target.IsWhitespace();

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsWhitespace_EmptyIsTrue ()
        {
            var target = "";

            var actual = target.IsWhitespace();

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsWhitespace_NullIsTrue ()
        {
            string target = null;

            var actual = target.IsWhitespace();

            //Assert
            actual.Should().BeTrue();
        }
        #endregion
    }
}
