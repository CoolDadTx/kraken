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
    public class SimpleTextSubstitutionRuleTests : UnitTest
    {
        #region CanProcess

        [TestMethod]
        public void Process_UsingStringAndMatches ()
        {
            //Arrange
            var context = new TextSubstitutionContext(new TextSubstitutionOptions(), "source");
            var target = new SimpleTextSubstitutionRule("source", "target");

            //Act
            var actual = target.CanProcess(context);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void CanProcess_UsingStringAndDoesNotMatch ()
        {
            //Arrange
            var context = new TextSubstitutionContext(new TextSubstitutionOptions(), "source");
            var target = new SimpleTextSubstitutionRule("target", "source");

            //Act
            var actual = target.CanProcess(context);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void CanProcess_UsingStringThatDiffersByCaseWithCaseSensitiveComparison ()
        {
            //Arrange
            var context = new TextSubstitutionContext(new TextSubstitutionOptions() { Comparison = StringComparison.CurrentCulture }, "Source");
            var target = new SimpleTextSubstitutionRule("source", "target");

            //Act
            var actual = target.CanProcess(context);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void CanProcess_UsingStringThatDiffersByCaseWithCaseInsensitiveComparison ()
        {
            //Arrange
            var context = new TextSubstitutionContext(new TextSubstitutionOptions() { Comparison = StringComparison.CurrentCultureIgnoreCase }, "Source");
            var target = new SimpleTextSubstitutionRule("source", "target");

            //Act
            var actual = target.CanProcess(context);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void CanProcess_UsingEmptyMatchingText ()
        {
            //Arrange
            var context = new TextSubstitutionContext(new TextSubstitutionOptions(), "source");
            var target = new SimpleTextSubstitutionRule("", "target");

            //Act
            var actual = target.CanProcess(context);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void CanProcess_UsingNullString ()
        {
            //Arrange
            var context = new TextSubstitutionContext(new TextSubstitutionOptions(), "source");
            var target = new SimpleTextSubstitutionRule(null, "target");

            //Act
            var actual = target.CanProcess(context);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region Process

        [TestMethod]
        public void Process_SimpleString ()
        {
            //Arrange
            var expected = "target";
            var context = new TextSubstitutionContext(new TextSubstitutionOptions(), "source");
            var target = new SimpleTextSubstitutionRule("source", "target");

            //Act
            var actual = target.Process(context);

            //Assert
            actual.Should().Be(expected);
        }
        
        [TestMethod]
        public void Process_UsingEmptyReplacementText ()
        {
            //Arrange
            var context = new TextSubstitutionContext(new TextSubstitutionOptions(), "source");
            var target = new SimpleTextSubstitutionRule("source", "");

            //Act
            var actual = target.Process(context);

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void Process_UsingNullReplacementString ()
        {
            //Arrange
            var context = new TextSubstitutionContext(new TextSubstitutionOptions(), "source");
            var target = new SimpleTextSubstitutionRule("source", null);

            //Act
            var actual = target.Process(context);

            //Assert
            actual.Should().BeEmpty();
        }
        
        [TestMethod]
        public void Process_EmptyValueRemovesToken ()
        {
            //Arrange
            var context = new TextSubstitutionContext(new TextSubstitutionOptions(), "source");
            var target = new SimpleTextSubstitutionRule("source", "");

            //Act
            var actual = target.Process(context);

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void Process_NullValueRemovesToken ()
        {
            //Arrange
            var context = new TextSubstitutionContext(new TextSubstitutionOptions(), "source");
            var target = new SimpleTextSubstitutionRule("source", null);

            //Act
            var actual = target.Process(context);

            //Assert
            actual.Should().BeEmpty();
        }
        
        #endregion
    }
}
