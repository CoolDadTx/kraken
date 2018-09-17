using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Text.Substitution;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.Text.Substitution
{
    [TestClass]
    public class TextSubstitutionEngineTests : UnitTest
    {
        #region Ctor

        [TestMethod]
        public void Ctor_DelimitersAreValid ( )
        {
            //Act
            var target = new TextSubstitutionEngine("<", ">");

            //Assert
            target.StartDelimiter.Should().Be("<");
            target.EndDelimiter.Should().Be(">");
        }

        [TestMethod]
        public void Ctor_StartDelimiterIsNull ()
        {            
            Action action = () => new TextSubstitutionEngine(null, ">");

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Ctor_StartDelimiterIsEmpty ()
        {
            Action action = () => new TextSubstitutionEngine("", ">");

            action.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void Ctor_EndDelimiterIsNull ()
        {
            Action action = () => new TextSubstitutionEngine("<", null);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_EndDelimiterIsEmpty ()
        {
            new TextSubstitutionEngine("<", "");
        }

        [TestMethod]
        public void Ctor_CustomComparison ()
        {
            //Act
            var target = new TextSubstitutionEngine("<", ">", StringComparison.CurrentCulture);

            //Assert
            target.DefaultComparison.Should().Be(StringComparison.CurrentCulture);
        }
        #endregion

        #region Process

        [TestMethod]
        public void Process_ArgumentIsValid ()
        {
            //Arrange
            var input = "Hello <Name>!";
            var expected = "Hello Bob!";
            var target = new TextSubstitutionEngine("<", ">");
            target.Rules.Add(new SimpleTextSubstitutionRule("Name", "Bob"));             

            //Act            
            var actual = target.Process(input);
            
            //Assert
            actual.Should().Be(expected);
        }            
        
        [TestMethod]
        public void Process_ArgumentIsEmpty ()
        {
            //Arrange
            var target = new TextSubstitutionEngine("<", ">");
            target.Rules.Add(new SimpleTextSubstitutionRule("simple", "complex"));

            //Act
            var actual = target.Process("");

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void Process_ArgumentIsNull ()
        {
            //Arrange
            var target = new TextSubstitutionEngine("<", ">");
            target.Rules.Add(new SimpleTextSubstitutionRule("simple", "complex"));

            //Act
            var actual = target.Process(null);

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void Process_MultipleTokens ()
        {
            //Arrange
            var input = "Begin <first> and <second> End";
            var expected = "Begin one and two End";
            var target = new TextSubstitutionEngine("<", ">");
            target.Rules.Add(new SimpleTextSubstitutionRule("first", "one"));
            target.Rules.Add(new SimpleTextSubstitutionRule("second", "two"));

            //Act            
            var actual = target.Process(input);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Process_TokenAtStart ()
        {
            //Arrange
            var input = "<first> End";
            var expected = "one End";
            var target = new TextSubstitutionEngine("<", ">");
            target.Rules.Add(new SimpleTextSubstitutionRule("first", "one"));

            //Act            
            var actual = target.Process(input);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Process_TokenAtEnd ()
        {
            //Arrange
            var input = "Begin <first>";
            var expected = "Begin one";
            var target = new TextSubstitutionEngine("<", ">");
            target.Rules.Add(new SimpleTextSubstitutionRule("first", "one"));

            //Act            
            var actual = target.Process(input);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Process_TokenHasSpacesInDelimiter ()
        {
            //Arrange
            var input = "Begin <first  > End";
            var expected = "Begin one End";
            var target = new TextSubstitutionEngine("<", ">");
            target.Rules.Add(new SimpleTextSubstitutionRule("first", "one"));

            //Act            
            var actual = target.Process(input);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Process_TokenNotDefined ()
        {
            //Arrange
            var input = "Begin <third> End";
            var expected = input;
            var target = new TextSubstitutionEngine("<", ">");
            target.Rules.Add(new SimpleTextSubstitutionRule("first", "one"));

            //Act
            var actual = target.Process(input);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Process_TokenNotDefinedWithSpaces ()
        {
            //Arrange
            var input = "Begin <  third  > End";
            var expected = input;
            var target = new TextSubstitutionEngine("<", ">");
            target.Rules.Add(new SimpleTextSubstitutionRule("first", "one"));

            //Act
            var actual = target.Process(input);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Process_StartDelimiterWithNoEnd ()
        {
            //Arrange
            var input = "Begin <first End";
            var expected = input;
            var target = new TextSubstitutionEngine("<", ">");
            target.Rules.Add(new SimpleTextSubstitutionRule("first", "one"));

            //Act
            var actual = target.Process(input);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Process_EndDelimiterWithNoStart ()
        {
            //Arrange
            var input = "Begin first> End";
            var expected = input;
            var target = new TextSubstitutionEngine("<", ">");
            target.Rules.Add(new SimpleTextSubstitutionRule("first", "one"));

            //Act
            var actual = target.Process(input);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Process_ReplacementValueHasDelimiter ()
        {
            //Arrange
            var input = "Begin <first> End";
            var expected = "Begin <second> End";
            var target = new TextSubstitutionEngine("<", ">");
            target.Rules.Add(new SimpleTextSubstitutionRule("first", "<second>"));
            target.Rules.Add(new SimpleTextSubstitutionRule("second", "two"));

            //Act
            var actual = target.Process(input);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Process_EmptyDelimiter ()
        {
            //Arrange
            var input = "Begin <> End";
            var expected = input;
            var target = new TextSubstitutionEngine("<", ">");
            target.Rules.Add(new SimpleTextSubstitutionRule("first", "one"));

            //Act
            var actual = target.Process(input);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Process_DelimiterUsingWrongCaseWithDefaultComparison ()
        {
            //Arrange
            var input = "Begin <first> End";
            var expected = "Begin one End";
            var target = new TextSubstitutionEngine("<", ">");
            target.Rules.Add(new SimpleTextSubstitutionRule("First", "one"));

            //Act
            var actual = target.Process(input);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Process_UsingCustomComparison ()
        {
            //Arrange
            var input = "Begin <first> End";
            var expected = input;
            var target = new TextSubstitutionEngine("<", ">", StringComparison.CurrentCulture);
            target.Rules.Add(new SimpleTextSubstitutionRule("First", "one"));

            //Act
            var actual = target.Process(input);

            //Assert
            actual.Should().Be(expected);            
        }
        #endregion
    }
}