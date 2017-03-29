using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Diagnostics;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.Diagnostics
{
    [TestClass]
    public class StringArgumentConstraintExtensionsTests : UnitTest
    {
        #region HasLengthBetween

        [TestMethod]
        public void HasLengthBetween_IsTrue ()
        {            
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "Hello"));

            target.HasLengthBetween(1, 10);
        }

        [TestMethod]
        public void HasLengthBetween_IsTooLong ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "Hello"));

            Action work = () => target.HasLengthBetween(1, 3);

            work.ShouldThrowArgumentException();
        }

        [TestMethod]
        public void HasLengthBetween_IsTooShort ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "Hello"));

            Action work = () => target.HasLengthBetween(10, 20);

            work.ShouldThrowArgumentException();
        }

        [TestMethod]
        public void HasLengthBetween_WithMessage ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "Hello"));
            var expectedMessage = "Testing";

            Action work = () => target.HasLengthBetween(10, 20, expectedMessage);

            work.ShouldThrowArgumentException().ContainingMessage(expectedMessage);
        }

        [TestMethod]
        public void HasLengthBetween_IsNull ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", null));

            Action work = () => target.HasLengthBetween(1, 10);

            work.ShouldThrowArgumentException();
        }
        #endregion

        #region HasMaximumLength

        [TestMethod]
        public void HasMaximumLength_IsTrue ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "Hello"));

            target.HasMaximumLength(10);
        }

        [TestMethod]
        public void HasMaximumLength_IsFalse ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "Hello"));

            Action work = () => target.HasMaximumLength(3);

            work.ShouldThrowArgumentException();
        }
        
        [TestMethod]
        public void HasMaximumLength_WithMessage ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "Hello"));
            var expectedMessage = "Testing";

            Action work = () => target.HasMaximumLength(3, expectedMessage);

            work.ShouldThrowArgumentException().ContainingMessage(expectedMessage);
        }

        [TestMethod]
        public void HasMaximumLength_IsNull ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", null));

            target.HasMaximumLength(1);            
        }
        #endregion

        #region HasMinimumLength

        [TestMethod]
        public void HasMinimumLength_IsTrue ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "Hello"));

            target.HasMinimumLength(3);
        }

        [TestMethod]
        public void HasMinimumLength_IsFalse ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "Hello"));

            Action work = () => target.HasMinimumLength(10);

            work.ShouldThrowArgumentException();
        }

        [TestMethod]
        public void HasMinimumLength_WithMessage ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "Hello"));
            var expectedMessage = "Testing";

            Action work = () => target.HasMinimumLength(10, expectedMessage);

            work.ShouldThrowArgumentException().ContainingMessage(expectedMessage);
        }

        [TestMethod]
        public void HasMinimumLength_IsNull ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", null));

            Action work = () => target.HasMinimumLength(1);

            work.ShouldThrowArgumentException();
        }
        #endregion

        #region IsAlpha

        [TestMethod]
        public void IsAlpha_HasAllLetters ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "ABC"));

            target.IsAlpha();
        }

        [TestMethod]
        public void IsAlpha_HasMixed ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "123ABC"));

            Action work = () => target.IsAlpha();

            work.ShouldThrowArgumentException();
        }

        [TestMethod]
        public void IsAlpha_HasNoLetters ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "123"));

            Action work = () => target.IsAlpha();

            work.ShouldThrowArgumentException();
        }

        [TestMethod]
        public void IsAlpha_HasMixedWithMessage ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "123ABC"));
            var expectedMessage = "Testing";

            Action work = () => target.IsAlpha(expectedMessage);

            work.ShouldThrowArgumentException().ContainingMessage(expectedMessage);
        }

        [TestMethod]
        public void IsAlpha_HasNoLettersWithMessage ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "123"));
            var expectedMessage = "Testing";

            Action work = () => target.IsAlpha(expectedMessage);

            work.ShouldThrowArgumentException().ContainingMessage(expectedMessage);
        }

        [TestMethod]
        public void IsAlpha_HasLettersWithMessage ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "ABC"));

            target.IsAlpha("Testing");
        }
        #endregion

        #region IsAlphaNumeric

        [TestMethod]
        public void IsAlphaNumeric_HasAllDigits ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "1234"));

            target.IsAlphanumeric();
        }

        [TestMethod]
        public void IsAlphaNumeric_HasAllLetters ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "ABC"));

            target.IsAlphanumeric();
        }

        [TestMethod]
        public void IsAlphaNumeric_HasAllLettersAndDigits ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "123ABC"));

            target.IsAlphanumeric();
        }

        [TestMethod]
        public void IsAlphaNumeric_HasMixed ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "123_ABC"));

            Action work = () => target.IsAlphanumeric();

            work.ShouldThrowArgumentException();
        }

        [TestMethod]
        public void IsAlphaNumeric_HasMixedWithMessage ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "123_ABC"));
            var expectedMessage = "Testing";

            Action work = () => target.IsAlphanumeric(expectedMessage);

            work.ShouldThrowArgumentException().ContainingMessage(expectedMessage);
        }

        [TestMethod]
        public void IsAlphaNumeric_HasLettersAndDigitsWithMessage ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "123ABC"));

            target.IsAlphanumeric("Testing");
        }
        #endregion

        #region IsNotNullOrEmpty

        [TestMethod]
        public void IsNotNullOrEmpty_IsFalse ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "Hello"));

            target.IsNotNullOrEmpty();
        }

        [TestMethod]
        public void IsNotNullOrEmpty_IsEmpty ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", ""));

            Action work = () => target.IsNotNullOrEmpty();

            work.ShouldThrowArgumentException();
        }

        [TestMethod]
        public void IsNotNullOrEmpty_IsNull ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", null));

            Action work = () => target.IsNotNullOrEmpty();

            work.ShouldThrowArgumentNullException();
        }

        [TestMethod]
        public void IsNotNullOrEmpty_IsNull_WithMessage ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", null));
            var expectedMessage = "Testing";

            Action work = () => target.IsNotNullOrEmpty(expectedMessage);

            work.ShouldThrowArgumentException().ContainingMessage(expectedMessage);
        }

        [TestMethod]
        public void IsNotNullOrEmpty_IsEmpty_WithMessage ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", ""));
            var expectedMessage = "Testing";

            Action work = () => target.IsNotNullOrEmpty(expectedMessage);

            work.ShouldThrowArgumentException().ContainingMessage(expectedMessage);
        }

        [TestMethod]
        public void IsNotNullOrEmpty_IsTrue_WithMessage ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "Not"));

            target.IsNotNullOrEmpty("Testing");
        }
        #endregion

        #region IsNotNullOrWhiteSpace

        [TestMethod]
        public void IsNotNullOrWhitespace_IsFalse ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "Hello"));

            target.IsNotNullOrWhiteSpace();
        }

        [TestMethod]
        public void IsNotNullOrWhitespace_IsEmpty ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", ""));

            Action work = () => target.IsNotNullOrWhiteSpace();

            work.ShouldThrowArgumentException();
        }

        [TestMethod]
        public void IsNotNullOrWhitespace_IsWhitespace ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "    "));

            Action work = () => target.IsNotNullOrWhiteSpace();

            work.ShouldThrowArgumentException();
        }

        [TestMethod]
        public void IsNotNullOrWhitespace_IsNull ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", null));

            Action work = () => target.IsNotNullOrWhiteSpace();

            work.ShouldThrowArgumentNullException();
        }

        [TestMethod]
        public void IsNotNullOrWhitespace_IsNull_WithMessage ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", null));
            var expectedMessage = "Testing";

            Action work = () => target.IsNotNullOrWhiteSpace(expectedMessage);

            work.ShouldThrowArgumentException().ContainingMessage(expectedMessage);
        }

        [TestMethod]
        public void IsNotNullOrWhitespace_IsEmpty_WithMessage ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", ""));
            var expectedMessage = "Testing";

            Action work = () => target.IsNotNullOrWhiteSpace(expectedMessage);

            work.ShouldThrowArgumentException().ContainingMessage(expectedMessage);
        }

        [TestMethod]
        public void IsNotNullOrWhitespace_IsTrue_WithMessage ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "Not"));

            target.IsNotNullOrWhiteSpace("Testing");
        }
        #endregion

        #region IsNumeric

        [TestMethod]
        public void IsNumeric_HasAllDigits ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "1234"));

            target.IsNumeric();
        }

        [TestMethod]
        public void IsNumeric_HasMixed ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "123ABC"));

            Action work = () => target.IsNumeric();

            work.ShouldThrowArgumentException();
        }

        [TestMethod]
        public void IsNumeric_HasNoDigits ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "ABC"));

            Action work = () => target.IsNumeric();

            work.ShouldThrowArgumentException();
        }

        [TestMethod]
        public void IsNumeric_HasMixedWithMessage ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "123ABC"));
            var expectedMessage = "Testing";

            Action work = () => target.IsNumeric(expectedMessage);

            work.ShouldThrowArgumentException().ContainingMessage(expectedMessage);
        }

        [TestMethod]
        public void IsNumeric_HasNoDigitsWithMessage ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "ABC"));
            var expectedMessage = "Testing";

            Action work = () => target.IsNumeric(expectedMessage);

            work.ShouldThrowArgumentException().ContainingMessage(expectedMessage);
        }

        [TestMethod]
        public void IsNumeric_HasDigitsWithMessage ()
        {
            var target = new ArgumentConstraint<string>(new Argument<string>("a", "123"));

            target.IsNumeric("Testing");
        }
        #endregion
    }
}
