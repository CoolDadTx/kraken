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

        #region EnsureSurroundedWith

        [TestMethod]
        public void EnsureSurroundedWith_NeedsBoth ()
        {
            var target = "Field1";
            var delimiter = "@";
            var expected = delimiter + target + delimiter;

            var actual = target.EnsureSurroundedWith(delimiter);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void EnsureSurroundedWith_NeedsPrefix ()
        {
            var delimiter = "@";
            var target = "Field1" + delimiter;            
            var expected = delimiter + target;

            var actual = target.EnsureSurroundedWith(delimiter);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void EnsureSurroundedWith_NeedsSuffix ()
        {
            var delimiter = "@";
            var target = delimiter + "Field1";
            var expected = target + delimiter;

            var actual = target.EnsureSurroundedWith(delimiter);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void EnsureSurroundedWith_NeedsNeither ()
        {
            var delimiter = "@";
            var target = delimiter + "Field1" + delimiter;
            var expected = target;

            var actual = target.EnsureSurroundedWith(delimiter);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void EnsureSurroundedWith_TargetIsNull ()
        {
            var delimiter = "@";
            var expected = delimiter + delimiter;

            var actual = ((string)null).EnsureSurroundedWith(delimiter);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void EnsureSurroundedWith_TargetIsEmpty ()
        {
            var delimiter = "@";
            var expected = delimiter + delimiter;

            var actual = "".EnsureSurroundedWith(delimiter);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void EnsureSurroundedWith_DelimiterIsNull ()
        {
            var target = "Field1";
            var expected = target;

            var actual = target.EnsureSurroundedWith(null);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void EnsureSurroundedWith_DelimiterIsEmpty ()
        {
            var target = "Field1";
            var expected = target;

            var actual = target.EnsureSurroundedWith("");

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void EnsureSurroundedWith_WithComparison_DiffersByCaseWithSensitive ()
        {
            var target = "XField1X";
            var delimiter = "x";
            var expected = delimiter + target + delimiter;

            var actual = target.EnsureSurroundedWith(delimiter, StringComparison.CurrentCulture);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void EnsureSurroundedWith_WithComparison_DiffersByCaseWithInsensitive ()
        {
            var target = "XField1X";
            var delimiter = "x";
            var expected = target;

            var actual = target.EnsureSurroundedWith(delimiter, StringComparison.CurrentCultureIgnoreCase);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void EnsureSurroundedWith_WithCulture_DiffersByCaseWithSensitive ()
        {
            var target = "XField1X";
            var delimiter = "x";
            var expected = delimiter + target + delimiter;

            var actual = target.EnsureSurroundedWith(delimiter, false, CultureInfo.CurrentCulture);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void EnsureSurroundedWith_WithCulture_DiffersByCaseWithInsensitive ()
        {
            var target = "XField1X";
            var delimiter = "x";
            var expected = target;

            var actual = target.EnsureSurroundedWith(delimiter, true, CultureInfo.CurrentCulture);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void EnsureSurroundedWith_CharDelimiter_NeedsBoth ()
        {
            var target = "Field1";
            var delimiter = '@';
            var expected = delimiter + target + delimiter;

            var actual = target.EnsureSurroundedWith(delimiter);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void EnsureSurroundedWith_CharDelimiter_NeedsPrefix ()
        {
            var delimiter = '@';
            var target = "Field1" + delimiter;
            var expected = delimiter + target;

            var actual = target.EnsureSurroundedWith(delimiter);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void EnsureSurroundedWith_CharDelimiter_NeedsSuffix ()
        {
            var delimiter = '@';
            var target = delimiter + "Field1";
            var expected = target + delimiter;

            var actual = target.EnsureSurroundedWith(delimiter);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void EnsureSurroundedWith_CharDelimiter_NeedsNeither ()
        {
            var delimiter = '@';
            var target = delimiter + "Field1" + delimiter;
            var expected = target;

            var actual = target.EnsureSurroundedWith(delimiter);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void EnsureSurroundedWith_CharDelimiter_TargetIsNull ()
        {
            var delimiter = '@';
            var expected = delimiter.ToString() + delimiter.ToString();

            var actual = ((string)null).EnsureSurroundedWith(delimiter);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void EnsureSurroundedWith_CharDelimiter_TargetIsEmpty ()
        {
            var delimiter = '@';
            var expected = delimiter.ToString() + delimiter.ToString();

            var actual = "".EnsureSurroundedWith(delimiter);

            actual.Should().Be(expected);
        }
        
        [TestMethod]
        public void EnsureSurroundedWith_CharDelimiter_WithComparison_DiffersByCaseWithSensitive ()
        {
            var target = "XField1X";
            var delimiter = 'x';
            var expected = delimiter + target + delimiter;

            var actual = target.EnsureSurroundedWith(delimiter, StringComparison.CurrentCulture);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void EnsureSurroundedWith_CharDelimiter_WithComparison_DiffersByCaseWithInsensitive ()
        {
            var target = "XField1X";
            var delimiter = 'x';
            var expected = target;

            var actual = target.EnsureSurroundedWith(delimiter, StringComparison.CurrentCultureIgnoreCase);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void EnsureSurroundedWith_CharDelimiter_WithCulture_DiffersByCaseWithSensitive ()
        {
            var target = "XField1X";
            var delimiter = 'x';
            var expected = delimiter + target + delimiter;

            var actual = target.EnsureSurroundedWith(delimiter, false, CultureInfo.CurrentCulture);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void EnsureSurroundedWith_CharDelimiter_WithCulture_DiffersByCaseWithInsensitive ()
        {
            var target = "XField1X";
            var delimiter = 'x';
            var expected = target;

            var actual = target.EnsureSurroundedWith(delimiter, true, CultureInfo.CurrentCulture);

            actual.Should().Be(expected);
        }
        #endregion
    }
}
