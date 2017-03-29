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
        #region char[], char

        [TestMethod]
        public void ReplaceAll_CharArrayWithChar_MixedString ()
        {
            var oldChar = new char[] { '1', '2', '3' };
            var newChar = '|';

            //Act
            var actual = "abc1def2ghi".ReplaceAll(oldChar, newChar);

            //Assert
            actual.Should().Be("abc|def|ghi");
        }

        [TestMethod]
        public void ReplaceAll_CharArrayWithChar_CharNotFound ()
        {
            var oldChar = new char[] { '1', '2', '3' };
            var newChar = '|';

            //Act
            var actual = "abcdefghi".ReplaceAll(oldChar, newChar);

            //Assert
            actual.Should().Be("abcdefghi");
        }

        [TestMethod]
        public void ReplaceAll_CharArrayWithChar_OldCharsIsEmpty ()
        {
            var oldChar = new char[] { };
            var newChar = '|';

            //Act
            var actual = "abcdefghi".ReplaceAll(oldChar, newChar);

            //Assert
            actual.Should().Be("abcdefghi");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReplaceAll_CharArrayWithChar_OldCharsIsNull ()
        {
            char[] oldChar = null;
            var newChar = '|';

            //Act
            "abcdefghi".ReplaceAll(oldChar, newChar);
        }
        
        [TestMethod]
        public void ReplaceAll_CharArrayWithChar_SourceIsEmpty ()
        {
            var oldChar = new char[] { 'A', 'B', 'C' };
            var newChar = '|';

            //Act
            var actual = "".ReplaceAll(oldChar, newChar);

            //Assert
            actual.Should().BeEmpty();
        }
        #endregion

        #region char[], char[]

        [TestMethod]
        public void ReplaceAll_CharArrayWithCharArray_MixedString ()
        {
            var oldChar = new char[] { '1', '2', '3' };
            var newChar = new char[] { '9', '8', '7' };

            //Act
            var actual = "abc1def2ghi3".ReplaceAll(oldChar, newChar);

            //Assert
            actual.Should().Be("abc9def8ghi7");
        }

        [TestMethod]
        public void ReplaceAll_CharArrayWithCharArray_NewContainsOldChars ()
        {
            var oldChar = new char[] { '1', '2', '3' };
            var newChar = new char[] { '2', '3', '7' };

            //Act
            var actual = "abc1def2ghi3".ReplaceAll(oldChar, newChar);

            //Assert
            actual.Should().Be("abc2def3ghi7");
        }

        [TestMethod]
        public void ReplaceAll_CharArrayWithCharArray_NoCharsFound ()
        {
            var oldChar = new char[] { '1', '2', '3' };
            var newChar = new char[] { '7', '8', '9' };

            //Act
            var actual = "abcdefghi".ReplaceAll(oldChar, newChar);

            //Assert
            actual.Should().Be("abcdefghi");
        }
        
        [TestMethod]
        public void ReplaceAll_CharArrayWithCharArray_OldCharsIsEmpty ()
        {
            var oldChar = new char[] { };
            var newChar = new char[] { '2', '3', '7' };

            //Act
            var actual = "abc1def2ghi3".ReplaceAll(oldChar, newChar);

            //Assert
            actual.Should().Be("abc1def2ghi3");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReplaceAll_CharArrayWithCharArray_OldCharsIsNull ()
        {
            char[] oldChar = null;
            var newChar = new char[] { '2', '3', '7' };

            //Act
            "abc1def2ghi3".ReplaceAll(oldChar, newChar);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReplaceAll_CharArrayWithCharArray_NewCharsTooSmall ()
        {
            var oldChar = new char[] { '1', '2', '3' };
            var newChar = new char[] { '9', '8' };

            //Act
            "abc1def2ghi3".ReplaceAll(oldChar, newChar);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReplaceAll_CharArrayWithCharArray_NewCharsIsNull ()
        {
            var oldChar = new char[] { '1', '2', '3' };
            char[] newChar = null;

            //Act
            "abc1def2ghi3".ReplaceAll(oldChar, newChar);
        }

        [TestMethod]
        public void ReplaceAll_CharArrayWithCharArray_SourceIsEmpty ()
        {
            var oldChar = new char[] { 'A', 'B', 'C' };
            var newChar = new char[] { '|', '|', '|' };

            //Act
            var actual = "".ReplaceAll(oldChar, newChar);

            //Assert
            actual.Should().BeEmpty();
        }
        #endregion

        #region IEnumerable<string>, string

        [TestMethod]
        public void ReplaceAll_EnumerableStringWithString_MixedString ()
        {
            var oldString = new string[] { "123", "456", "789" };
            var newString = "_|_";

            //Act
            var actual = "abc123def789ghi127".ReplaceAll(oldString, newString);

            //Assert
            actual.Should().Be("abc_|_def_|_ghi127");
        }

        [TestMethod]
        public void ReplaceAll_EnumerableStringWithString_StringNotFound ()
        {
            var oldString = new string[] { "123", "456", "789" };
            var newString = "{}";

            //Act
            var actual = "abcdefghi".ReplaceAll(oldString, newString);

            //Assert
            actual.Should().Be("abcdefghi");
        }

        [TestMethod]
        public void ReplaceAll_EnumerableStringWithString_OldStringIsEmpty ()
        {
            var oldString = new string[] { };
            var newString = "|";

            //Act
            var actual = "abcdefghi".ReplaceAll(oldString, newString);

            //Assert
            actual.Should().Be("abcdefghi");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReplaceAll_EnumerableStringWithString_OldStringIsNull ()
        {
            string[] oldString = null;
            var newString = "|";

            //Act
            "abcdefghi".ReplaceAll(oldString, newString);
        }
        
        [TestMethod]
        public void ReplaceAll_EnumerableStringWithString_NewStringIsNull ()
        {
            var oldString = new string[] { "123", "def", "789" };
            string newString = null;
            
            //Act
            var actual = "abcdefghi".ReplaceAll(oldString, newString);

            //Assert
            actual.Should().Be("abcghi");
        }

        [TestMethod]
        public void ReplaceAll_EnumerableStringWithString_SourceIsEmpty ()
        {
            var oldString = new string[] { "123", "456", "789" };
            var newString = "_|_";

            //Act
            var actual = "".ReplaceAll(oldString, newString);

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void ReplaceAll_EnumerableStringWithString_HasNullStrings ()
        {
            var oldString = new string[] { "123", null, "789" };
            string newString = null;

            //Act
            var actual = "ABC123DEF".ReplaceAll(oldString, newString);

            //Assert
            actual.Should().Be("ABCDEF");
        }
        #endregion

        #region IEnumerable<string>, IEnumerable<string>

        [TestMethod]
        public void ReplaceAll_EnumerableStringWithEnumerableString_MixedString ()
        {
            var oldString = new string[] { "123", "456", "789" };
            var newString = new string[] { "{}", "()", "[]" };

            //Act
            var actual = "abc123def456ghi789".ReplaceAll(oldString, newString);

            //Assert
            actual.Should().Be("abc{}def()ghi[]");
        }

        [TestMethod]
        public void ReplaceAll_EnumerableStringWithEnumerableString_NewContainsOldStrings ()
        {
            var oldString = new string[] { "123", "456", "789" };
            var newString = new string[] { "456", "789", "{}" };

            //Act
            var actual = "abc123def456ghi789".ReplaceAll(oldString, newString);

            //Assert
            actual.Should().Be("abc{}def{}ghi{}");
        }

        [TestMethod]
        public void ReplaceAll_EnumerableStringWithEnumerableString_NoStringFound ()
        {
            var oldString = new string[] { "123", "456", "789" };
            var newString = new string[] { "{}", "[]", "()" };

            //Act
            var actual = "abc124def458ghi780".ReplaceAll(oldString, newString);

            //Assert
            actual.Should().Be("abc124def458ghi780");
        }

        [TestMethod]
        public void ReplaceAll_EnumerableStringWithEnumerableString_OldStringIsEmpty ()
        {
            var oldString = new string[] { };
            var newString = new string[] { "{}", "[]" };

            //Act
            var actual = "abc1def2ghi3".ReplaceAll(oldString, newString);

            //Assert
            actual.Should().Be("abc1def2ghi3");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReplaceAll_EnumerableStringWithEnumerableString_OldStringIsNull ()
        {
            string[] oldString = null;
            var newString = new string[] { "{}", "[]" };

            //Act
            "abc1def2ghi3".ReplaceAll(oldString, newString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReplaceAll_EnumerableStringWithEnumerableString_NewStringTooSmall ()
        {
            var oldString = new string[] { "123", "456", "789" };
            var newString = new string[] { "{}", "[]" };

            //Act
            "abc1def2ghi3".ReplaceAll(oldString, newString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReplaceAll_EnumerableStringWithEnumerableString_NewStringIsNull ()
        {
            var oldString = new string[] { "123", "456", "789" };
            string[] newString = null;

            //Act
            "abc1def2ghi3".ReplaceAll(oldString, newString);
        }

        [TestMethod]
        public void ReplaceAll_EnumerableStringWithEnumerableString_SourceIsEmpty ()
        {
            var oldString = new string[] { "123", "456", "789" };
            var newString = new string[] { "ABC", "DEF", "GHI" };

            //Act
            var actual = "".ReplaceAll(oldString, newString);

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void ReplaceAll_EnumerableStringWithEnumerableString_HasNullStringForOldString ()
        {
            var oldString = new string[] { "123", null, "789" };
            var newString = new string[] { "_|_", "|_|", "_||" };

            //Act
            var actual = "ABC123DEF456GHI789JKL".ReplaceAll(oldString, newString);

            //Assert
            actual.Should().Be("ABC_|_DEF456GHI_||JKL");
        }

        [TestMethod]
        public void ReplaceAll_EnumerableStringWithEnumerableString_HasNullStringForNewString ()
        {
            var oldString = new string[] { "123", "456", "789" };
            var newString = new string[] { "_|_", "|_|", null };

            //Act
            var actual = "ABC123DEF789GHI".ReplaceAll(oldString, newString);

            //Assert
            actual.Should().Be("ABC_|_DEFGHI");
        }
        #endregion
    }
}
