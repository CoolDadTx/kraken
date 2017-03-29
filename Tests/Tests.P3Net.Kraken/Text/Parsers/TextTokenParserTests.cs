using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Text;
using P3Net.Kraken.Text.Parsers;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.Text.Parsers
{
    [TestClass]
    public class TextTokenParserTests : UnitTest
    {
        #region Ctor
        
        [TestMethod]
        public void Ctor_StartIsNull ()
        {               
            Action action = () => new TextTokenParser(null, "}");

            action.ShouldThrowArgumentNullException();
        }

        [TestMethod]
        public void Ctor_StartIsEmpty ()
        {
            Action action = () => new TextTokenParser("", "}");

            action.ShouldThrowArgumentException();
        }

        [TestMethod]
        public void Ctor_EndIsNull ()
        {
            Action action = () => new TextTokenParser("{", null);

            action.ShouldThrowArgumentNullException();
        }

        [TestMethod]
        public void Ctor_EndIsEmpty ()
        {
            Action action = () => new TextTokenParser("{", "");

            action.ShouldThrowArgumentException();
        }
        #endregion

        #region Parse

        [TestMethod]
        public void Parse_InputIsNull ()
        {
            //Act
            var target = new TextTokenParser("{", "}");
            var actual = target.Parse(null).ToList();

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void Parse_InputIsEmpty ()
        {
            //Act
            var target = new TextTokenParser("{", "}");
            var actual = target.Parse("").ToList();

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void Parse_NoDelimitedText ()
        {
            //Arrange            
            var input = "Some text";
            var expected = new TextTokenInfo[] {            
                                new TextTokenInfo(TextTokenKind.Text, "Some text", 0)
                };

            var target = new TextTokenParser("{", "}");
            
            //Act
            var actual = target.Parse(input).ToList();

            //Assert
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void Parse_MixedText ()
        {
            //Arrange
            var input = "Begin <first> and <second> End";
            var target = new TextTokenParser("<", ">");
            var expected = new TextTokenInfo[] {            
                                new TextTokenInfo(TextTokenKind.Text, "Begin ", 0),
                                new TextTokenInfo(TextTokenKind.DelimitedText, "first", 6),
                                new TextTokenInfo(TextTokenKind.Text, " and ", 13),
                                new TextTokenInfo(TextTokenKind.DelimitedText, "second", 18),
                                new TextTokenInfo(TextTokenKind.Text, " End", 26),
                };
            
            //Act
            var actual = target.Parse(input).ToList();

            //Assert
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void Parse_DelimiterAtStart ()
        {
            //Arrange
            var input = "<first> End";
            var target = new TextTokenParser("<", ">");
            var expected = new TextTokenInfo[] {
                                new TextTokenInfo(TextTokenKind.DelimitedText, "first", 0),
                                new TextTokenInfo(TextTokenKind.Text, " End", 7)
                };

            //Act
            var actual = target.Parse(input).ToList();

            //Assert
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void Parse_DelimiterAtEnd ()
        {
            //Arrange
            var input = "Begin <first>";
            var target = new TextTokenParser("<", ">");
            var expected = new TextTokenInfo[] {
                                new TextTokenInfo(TextTokenKind.Text, "Begin ", 0),
                                new TextTokenInfo(TextTokenKind.DelimitedText, "first", 6)
                };

            //Act
            var actual = target.Parse(input).ToList();

            //Assert
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void Parser_DelimitedTextWithSpaces ()
        {
            //Arrange
            var input = "Begin <first  > End";            
            var target = new TextTokenParser("<", ">");
            var expected = new TextTokenInfo[] {
                                new TextTokenInfo(TextTokenKind.Text, "Begin ", 0),
                                new TextTokenInfo(TextTokenKind.DelimitedText, "first  ", 6),
                                new TextTokenInfo(TextTokenKind.Text, " End", 15)
                };

            //Act
            var actual = target.Parse(input).ToList();

            //Assert
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void Parse_StartDelimiterWithNoEnd ()
        {
            //Arrange
            var input = "Begin <first End";            
            var target = new TextTokenParser("<", ">");
            var expected = new TextTokenInfo[] {
                                new TextTokenInfo(TextTokenKind.Text, input, 0)
                };

            //Act
            var actual = target.Parse(input).ToList();

            //Assert
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void Parse_EndDelimiterWithNoStart ()
        {
            //Arrange
            var input = "Begin first> End";
            var target = new TextTokenParser("<", ">");
            var expected = new TextTokenInfo[] {
                                new TextTokenInfo(TextTokenKind.Text, input, 0)
                };

            //Act
            var actual = target.Parse(input).ToList();

            //Assert
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void Parse_EmptyDelimiter ()
        {
            //Arrange
            var input = "Begin <> End";
            var target = new TextTokenParser("<", ">");
            var expected = new TextTokenInfo[] {
                                new TextTokenInfo(TextTokenKind.Text, "Begin ", 0),
                                new TextTokenInfo(TextTokenKind.DelimitedText, "", 6),
                                new TextTokenInfo(TextTokenKind.Text, " End", 8)
                };

            //Act
            var actual = target.Parse(input).ToList();

            //Assert
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void Parse_TooManyStartDelimiters ()
        {
            //Arrange
            var input = "Begin <4 < 5> End";
            var target = new TextTokenParser("<", ">");
            var expected = new TextTokenInfo[] {
                                new TextTokenInfo(TextTokenKind.Text, "Begin ", 0),
                                new TextTokenInfo(TextTokenKind.DelimitedText, "4 < 5", 6),
                                new TextTokenInfo(TextTokenKind.Text, " End", 13)
                };

            //Act
            var actual = target.Parse(input).ToList();

            //Assert
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void Parse_TooManyEndDelimiters ()
        {
            //Arrange
            var input = "Begin <4 > 5> End";
            var target = new TextTokenParser("<", ">");
            var expected = new TextTokenInfo[] {
                                new TextTokenInfo(TextTokenKind.Text, "Begin ", 0),
                                new TextTokenInfo(TextTokenKind.DelimitedText, "4 ", 6),
                                new TextTokenInfo(TextTokenKind.Text, " 5> End", 10)
                };

            //Act
            var actual = target.Parse(input).ToList();

            //Assert
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void Parse_SpacesAtFront ()
        {
            //Arrange
            var input = "    Begin <target> End";
            var target = new TextTokenParser("<", ">");
            var expected = new TextTokenInfo[] {
                                new TextTokenInfo(TextTokenKind.Text, "    Begin ", 0),
                                new TextTokenInfo(TextTokenKind.DelimitedText, "target", 10),
                                new TextTokenInfo(TextTokenKind.Text, " End", 18)
                };

            //Act
            var actual = target.Parse(input).ToList();

            //Assert
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void Parse_DelimitersSideBySide ()
        {
            //Arrange
            var input = "Begin <first><second> End";
            var target = new TextTokenParser("<", ">");
            var expected = new TextTokenInfo[] {
                                new TextTokenInfo(TextTokenKind.Text, "Begin ", 0),
                                new TextTokenInfo(TextTokenKind.DelimitedText, "first", 6),
                                new TextTokenInfo(TextTokenKind.DelimitedText, "second", 13),
                                new TextTokenInfo(TextTokenKind.Text, " End", 21)
                };

            //Act
            var actual = target.Parse(input).ToList();

            //Assert
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void Parse_StartDelimiterAtEndOfInput ()
        {
            //Arrange
            var input = "Begin <";
            var target = new TextTokenParser("<", ">");
            var expected = new TextTokenInfo[] {
                                new TextTokenInfo(TextTokenKind.Text, "Begin <", 0)
                };

            //Act
            var actual = target.Parse(input).ToList();

            //Assert
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void Parse_DelimiterHasMultipleCharacters ()
        {
            //Arrange
            var input = "Begin <!-- comment -->";
            var target = new TextTokenParser("<!--", "-->");
            var expected = new TextTokenInfo[] {
                                new TextTokenInfo(TextTokenKind.Text, "Begin ", 0),
                                new TextTokenInfo(TextTokenKind.DelimitedText, " comment ", 6)
                };

            //Act
            var actual = target.Parse(input).ToList();

            //Assert
            actual.Should().ContainInOrder(expected);
        }
        #endregion
    }
}