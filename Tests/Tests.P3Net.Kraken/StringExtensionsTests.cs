#region Imports

using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken
{
    [TestClass]
    public partial class StringExtensionsTests : UnitTest
    {
        #region AsNullIfEmpty

        [TestMethod]
        public void AsNullIfEmpty_WithNonempty ()
        {
            var expected = "Hello";

            var actual = expected.AsNullIfEmpty();

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AsNullIfEmpty_WithEmpty ()
        {
            var expected = "";

            var actual = expected.AsNullIfEmpty();

            //Assert
            actual.Should().BeNull();
        }

        [TestMethod]
        public void AsNullIfEmpty_WithNull ()
        {
            string expected = null;

            var actual = expected.AsNullIfEmpty();

            //Assert
            actual.Should().BeNull();
        }
        #endregion

        #region FormatWith

        [TestMethod]
        public void FormatWith_OneArgument ()
        {
            var target = "Hello {0} World";
            var arg = 10;
            var expected = String.Format(target, arg);

            var actual = target.FormatWith(arg);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void FormatWith_TwoArguments ()
        {
            var target = "Hello {0} World {1}";
            var arg1 = 10;
            var arg2 = DateTime.Now;

            var expected = String.Format(target, arg1, arg2);

            var actual = target.FormatWith(arg1, arg2);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void FormatWith_ThreeArguments ()
        {
            var target = "Hello {0} World {1} {2}";
            var arg1 = 10;
            var arg2 = DateTime.Now;
            var arg3 = 4.567;

            var expected = String.Format(target, arg1, arg2, arg3);

            var actual = target.FormatWith(arg1, arg2, arg3);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void FormatWith_ManyArguments ()
        {
            var target = "{0},{1},{2},{3}";
            var arg1 = 10;
            var arg2 = DateTime.Now;
            var arg3 = 34.564;
            var arg4 = true;

            var expected = String.Format(target, arg1, arg2, arg3, arg4);

            var actual = target.FormatWith(arg1, arg2, arg3, arg4);

            actual.Should().Be(expected);
        }
        #endregion

        #region GetComparer

        [TestMethod]
        public void GetComparer_CurrentCulture ()
        {
            //Act
            var actual = StringExtensions.GetComparer(StringComparison.CurrentCulture);
            
            //Assert
            actual.Should().Be(StringComparer.CurrentCulture);
        }

        [TestMethod]
        public void GetComparer_CurrentCultureIgnoreCase ()
        {
            //Act
            var actual = StringExtensions.GetComparer(StringComparison.CurrentCultureIgnoreCase);

            //Assert
            actual.Should().Be(StringComparer.CurrentCultureIgnoreCase);
        }

        [TestMethod]
        public void GetComparer_InvariantCulture ()
        {
            //Act
            var actual = StringExtensions.GetComparer(StringComparison.InvariantCulture);

            //Assert
            actual.Should().Be(StringComparer.InvariantCulture);
        }

        [TestMethod]
        public void GetComparer_InvariantCultureIgnoreCase ()
        {
            //Act
            var actual = StringExtensions.GetComparer(StringComparison.InvariantCultureIgnoreCase);

            //Assert
            actual.Should().Be(StringComparer.InvariantCultureIgnoreCase);
        }

        [TestMethod]
        public void GetComparer_Ordinal ()
        {
            //Act
            var actual = StringExtensions.GetComparer(StringComparison.Ordinal);

            //Assert
            actual.Should().Be(StringComparer.Ordinal);
        }

        [TestMethod]
        public void GetComparer_OrdinalIgnoreCase ()
        {
            //Act
            var actual = StringExtensions.GetComparer(StringComparison.OrdinalIgnoreCase);

            //Assert
            actual.Should().Be(StringComparer.OrdinalIgnoreCase);
        }

        [TestMethod]
        public void GetComparer_NotValid ()
        {
            Action action = () => StringExtensions.GetComparer((StringComparison)1000);

            action.ShouldThrowArgumentOutOfRangeException();
        }
        #endregion

        #region RemoveAll

        [TestMethod]
        public void RemoveAll_Char ()
        {
            var target = "Hello | World";
            var expected = "Hello  World";

            var actual = target.RemoveAll('|');

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void RemoveAll_CharAndIndex ()
        {
            var target = "| Hello | World";
            var expected = "| Hello  World";

            var actual = target.RemoveAll('|', 1);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void RemoveAll_CharAndComparison ()
        {
            var target = "A Hello a World";
            var expected = "A Hello  World";

            var actual = target.RemoveAll('a', StringComparison.CurrentCulture);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void RemoveAll_CharIndexAndComparison ()
        {
            var target = "Aa Hello a World";
            var expected = "Aa Hello  World";

            var actual = target.RemoveAll('a', 2, StringComparison.CurrentCulture);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void RemoveAll_CharListIndexAndComparison ()
        {
            var target = "A Hello B World C";
            var expected = "A Hello  World ";

            var actual = target.RemoveAll(new[] { 'A', 'B', 'C' }, 1, StringComparison.CurrentCulture);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void RemoveAll_String ()
        {
            var target = "Hello || World";
            var expected = "Hello  World";

            var actual = target.RemoveAll("||");

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void RemoveAll_StringAndIndex ()
        {
            var target = "|| Hello || World";
            var expected = "|| Hello  World";

            var actual = target.RemoveAll("||", 1);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void RemoveAll_StringAndComparison ()
        {
            var target = "Ab Hello ab World";
            var expected = "Ab Hello  World";

            var actual = target.RemoveAll("ab", StringComparison.CurrentCulture);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void RemoveAll_StringIndexAndComparison ()
        {
            var target = "Ab Hello ab World";
            var expected = "Ab Hello  World";

            var actual = target.RemoveAll("ab", 2, StringComparison.CurrentCulture);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void RemoveAll_StringListIndexAndComparison ()
        {
            var target = "AB Hello CD World EF";
            var expected = "AB Hello  World ";

            var actual = target.RemoveAll(new[] { "AB", "CD", "EF" }, 1, StringComparison.CurrentCulture);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void RemoveAll_CharListIsNull ()
        {
            Action action = () => "Hello World".RemoveAll((char[])null, 0, StringComparison.CurrentCulture);

            action.ShouldThrowArgumentNullException();
        }

        [TestMethod]
        public void RemoveAll_StringIsNull ()
        {
            Action action = () => "Hello World".RemoveAll((string)null);

            action.ShouldThrowArgumentNullException();
        }

        [TestMethod]
        public void RemoveAll_StringIsEmpty ()
        {
            Action action = () => "Hello World".RemoveAll("");

            action.ShouldThrowArgumentException();
        }

        [TestMethod]
        public void RemoveAll_StringListIsNull ()
        {
            Action action = () => "Hello World".RemoveAll((string[])null, 0, StringComparison.CurrentCulture);

            action.ShouldThrowArgumentNullException();
        }
        #endregion

        #region TrimmedValueOrEmpty

        [TestMethod]
        public void TrimmedValueOrEmpty_StringHasValue ()
        {
            //Act
            var input = "Hello";
            var actual = input.TrimmedValueOrEmpty();

            //Assert
            actual.Should().Be(input);
        }

        [TestMethod]
        public void TrimmedValueOrEmpty_StringIsEmpty ()
        {
            //Act
            var input = "";
            var actual = input.TrimmedValueOrEmpty();

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void TrimmedValueOrEmpty_StringIsNull ()
        {
            //Act
            string input = null;
            var actual = input.TrimmedValueOrEmpty();

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void TrimmedValueOrEmpty_StringWithSpaces ()
        {
            //Act
            string input = "  first   ";
            var actual = input.TrimmedValueOrEmpty();

            //Assert
            actual.Should().Be("first");
        }
        #endregion

        #region ValueOrEmpty

        [TestMethod]
        public void ValueOrEmpty_StringHasValue ()
        {
            //Act
            var input = "Hello";
            var actual = input.ValueOrEmpty();

            //Assert
            actual.Should().Be(input);
        }

        [TestMethod]
        public void ValueOrEmpty_StringIsEmpty ()
        {
            //Act
            var input = "";
            var actual = input.ValueOrEmpty();

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void ValueOrEmpty_StringIsNull ()
        {
            //Act
            string input = null;
            var actual = input.ValueOrEmpty();

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void ValueOrEmpty_StringWithSpaces ()
        {
            //Act
            string input = "  first";
            var actual = input.ValueOrEmpty();

            //Assert
            actual.Should().Be(input);
        }
        #endregion
    }
}
