using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken;

namespace Tests.P3Net.Kraken
{
    public partial class StringExtensionsTests
    {
        #region EnsureEndsWith

        #region Char

        [TestMethod]
        public void EnsureEndsWith_Char_IsTrue ()
        {
            var target = "Test/";

            var actual = target.EnsureEndsWith('/');

            actual.Should().Be(target);
        }

        [TestMethod]
        public void EnsureEndsWith_Char_IsFalse ()
        {
            var target = "Test";

            var actual = target.EnsureEndsWith('/');

            actual.Should().Be(target + "/");
        }

        [TestMethod]
        public void EnsureEndsWith_Char_IsCaseSensitive ()
        {
            var target = "TestA";

            var actual = target.EnsureEndsWith('a');

            actual.Should().Be(target + 'a');
        }

        [TestMethod]
        public void EnsureEndsWith_Char_WithComparison ()
        {
            var target = "TestA";

            var actual = target.EnsureEndsWith('a', StringComparison.CurrentCultureIgnoreCase);

            actual.Should().Be(target);
        }

        [TestMethod]
        public void EnsureEndsWith_Char_WithCulture ()
        {
            var target = "Test/";
            var culture = CultureInfo.InvariantCulture;

            var actual = target.EnsureEndsWith('/', true, culture);

            actual.Should().Be(target);
        }

        [TestMethod]
        public void EnsureEndsWith_Char_WithNullCulture ()
        {
            var target = "Test/";
            
            var actual = target.EnsureEndsWith('/', true, null);

            actual.Should().Be(target);
        }

        [TestMethod]
        public void EnsureEndsWith_Char_SourceIsNull ()
        {
            var target = (string)null;

            var actual = target.EnsureEndsWith("/", true, null);

            actual.Should().Be("/");
        }

        [TestMethod]
        public void EnsureEndsWith_Char_SourceIsEmpty ()
        {
            var actual = "".EnsureEndsWith("/", true, null);

            actual.Should().Be("/");
        }
        #endregion

        #region String

        [TestMethod]
        public void EnsureEndsWith_String_IsTrue ()
        {
            var target = "Test/";

            var actual = target.EnsureEndsWith("/");

            actual.Should().Be(target);
        }

        [TestMethod]
        public void EnsureEndsWith_String_IsFalse ()
        {
            var target = "Test";

            var actual = target.EnsureEndsWith("/");

            actual.Should().Be(target + "/");
        }

        [TestMethod]
        public void EnsureEndsWith_String_IsCaseSensitive ()
        {
            var target = "TestA";

            var actual = target.EnsureEndsWith("a");

            actual.Should().Be(target + "a");
        }

        [TestMethod]
        public void EnsureEndsWith_String_WithComparison ()
        {
            var target = "TestA";

            var actual = target.EnsureEndsWith("a", StringComparison.CurrentCultureIgnoreCase);

            actual.Should().Be(target);
        }

        [TestMethod]
        public void EnsureEndsWith_String_WithCulture ()
        {
            var target = "Test/";
            var culture = CultureInfo.InvariantCulture;

            var actual = target.EnsureEndsWith("/", true, culture);

            actual.Should().Be(target);
        }

        [TestMethod]
        public void EnsureEndsWith_String_WithNullCulture ()
        {
            var target = "Test/";

            var actual = target.EnsureEndsWith("/", true, null);

            actual.Should().Be(target);
        }

        [TestMethod]
        public void EnsureEndsWith_String_SourceIsNull ()
        {
            var target = (string)null;

            var actual = target.EnsureEndsWith("[]");

            actual.Should().Be("[]");
        }

        [TestMethod]
        public void EnsureEndsWith_String_SourceIsEmpty ()
        {
            var actual = "".EnsureEndsWith("[]");

            actual.Should().Be("[]");
        }

        [TestMethod]
        public void EnsureEndsWith_String_WithCulture_IsFalse ()
        {
            var target = "Test";
            var culture = CultureInfo.InvariantCulture;

            var actual = target.EnsureEndsWith("/", true, culture);

            actual.Should().Be(target + "/");
        }

        [TestMethod]
        public void EnsureEndsWith_String_SourceIsNullWithComparison ()
        {
            var target = (string)null;

            var actual = target.EnsureEndsWith("[]", StringComparison.CurrentCultureIgnoreCase);

            actual.Should().Be("[]");
        }

        [TestMethod]
        public void EnsureEndsWith_StringWithComparison_IsTrue ()
        {
            var target = "Test[]";

            var actual = target.EnsureEndsWith("[]", StringComparison.CurrentCultureIgnoreCase);

            actual.Should().Be(target);
        }

        [TestMethod]
        public void EnsureEndsWith_StringWithComparison_IsFalse ()
        {
            var target = "Test";

            var actual = target.EnsureEndsWith("[]", StringComparison.CurrentCultureIgnoreCase);

            actual.Should().Be(target + "[]");
        }
        #endregion

        #endregion

        #region EnsureStartsWith

        #region Char

        [TestMethod]
        public void EnsureStartsWith_Char_IsTrue ()
        {
            var target = "/Test";

            var actual = target.EnsureStartsWith('/');

            actual.Should().Be(target);
        }

        [TestMethod]
        public void EnsureStartsWith_Char_IsFalse ()
        {
            var target = "Test";

            var actual = target.EnsureStartsWith('/');

            actual.Should().Be("/" + target);
        }

        [TestMethod]
        public void EnsureStartsWith_Char_IsCaseSensitive ()
        {
            var target = "ATest";

            var actual = target.EnsureStartsWith('a');

            actual.Should().Be("a" + target);
        }

        [TestMethod]
        public void EnsureStartsWith_Char_WithComparison ()
        {
            var target = "ATest";

            var actual = target.EnsureStartsWith('a', StringComparison.CurrentCultureIgnoreCase);

            actual.Should().Be(target);
        }

        [TestMethod]
        public void EnsureStartsWith_Char_WithCulture ()
        {
            var target = "/Test";
            var culture = CultureInfo.InvariantCulture;

            var actual = target.EnsureStartsWith('/', true, culture);

            actual.Should().Be(target);
        }

        [TestMethod]
        public void EnsureStartsWith_Char_WithNullCulture ()
        {
            var target = "/Test";

            var actual = target.EnsureStartsWith('/', true, null);

            actual.Should().Be(target);
        }

        [TestMethod]
        public void EnsureStartsWith_Char_SourceIsNull ()
        {
            var target = (string)null;

            var actual = target.EnsureStartsWith('/');

            actual.Should().Be("/");
        }

        [TestMethod]
        public void EnsureStartsWith_Char_SourceIsEmpty ()
        {
            var actual = "".EnsureStartsWith('/');

            actual.Should().Be("/");
        }
        #endregion

        #region String

        [TestMethod]
        public void EnsureStartsWith_String_IsTrue ()
        {
            var target = "/Test";

            var actual = target.EnsureStartsWith("/");

            actual.Should().Be(target);
        }

        [TestMethod]
        public void EnsureStartsWith_String_IsFalse ()
        {
            var target = "Test";

            var actual = target.EnsureStartsWith("/");

            actual.Should().Be("/" + target);
        }

        [TestMethod]
        public void EnsureStartsWith_String_IsCaseSensitive ()
        {
            var target = "ATest";

            var actual = target.EnsureStartsWith("a");

            actual.Should().Be("a" + target);
        }

        [TestMethod]
        public void EnsureStartsWith_String_WithComparison ()
        {
            var target = "ATest";

            var actual = target.EnsureStartsWith("a", StringComparison.CurrentCultureIgnoreCase);

            actual.Should().Be(target);
        }

        [TestMethod]
        public void EnsureStartsWith_String_WithCulture ()
        {
            var target = "/Test";
            var culture = CultureInfo.InvariantCulture;

            var actual = target.EnsureStartsWith("/", true, culture);

            actual.Should().Be(target);
        }

        [TestMethod]
        public void EnsureStartsWith_String_WithNullCulture ()
        {
            var target = "/Test";

            var actual = target.EnsureStartsWith("/", true, null);

            actual.Should().Be(target);
        }

        [TestMethod]
        public void EnsureStartsWith_String_SourceIsNull ()
        {
            var target = (string)null;

            var actual = target.EnsureStartsWith("[]");

            actual.Should().Be("[]");
        }

        [TestMethod]
        public void EnsureStartsWith_String_SourceIsEmpty ()
        {
            var actual = "".EnsureStartsWith("[]");

            actual.Should().Be("[]");
        }

        [TestMethod]
        public void EnsureStartsWith_String_SourceIsNullWithComparison ()
        {
            var target = (string)null;

            var actual = target.EnsureStartsWith("[]", StringComparison.CurrentCultureIgnoreCase);

            actual.Should().Be("[]");
        }

        [TestMethod]
        public void EnsureStartsWith_StringWithComparison_IsTrue ()
        {
            var target = "[]Test";

            var actual = target.EnsureStartsWith("[]", StringComparison.CurrentCultureIgnoreCase);

            actual.Should().Be(target);
        }

        [TestMethod]
        public void EnsureStartsWith_StringWithComparison_IsFalse ()
        {
            var target = "Test";

            var actual = target.EnsureStartsWith("[]", StringComparison.CurrentCultureIgnoreCase);

            actual.Should().Be("[]" + target);
        }
        #endregion

        #endregion
    }
}
