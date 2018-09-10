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
    public partial class StringExtensionsTests
    {
        [TestMethod]
        public void Combine_AllStringsAreEmpty ()
        {
            //Act
            var actual = StringExtensions.Combine("", "", "");

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void Combine_SeparatorIsNotEmptyArgumentsAre ()
        {
            //Act
            var actual = StringExtensions.Combine(", ", "", "");

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void Combine_SeparatorIsEmpty ()
        {
            //Act
            var actual = StringExtensions.Combine("", "13", "AB", "+-");

            //Assert
            actual.Should().Be("13AB+-");
        }
        
        [TestMethod]
        public void Combine_SeparatorIsNull ()
        {
            Action action = () => StringExtensions.Combine(null, "1", "3");

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Combine_FirstArgumentIsEmpty ()
        {
            //Act
            var actual = StringExtensions.Combine(", ", "", "A", "1");

            //Assert
            actual.Should().Be("A, 1");
        }

        [TestMethod]
        public void Combine_LastArgumentIsEmpty ()
        {
            //Act
            var actual = StringExtensions.Combine(", ", "ABC", "123", "");

            //Assert
            actual.Should().Be("ABC, 123");
        }
        
        [TestMethod]        
        public void Combine_OneArgumentIsNull ()
        {
            //Act
            var actual = StringExtensions.Combine(", ", "A", null, "1");

            //Assert
            actual.Should().Be("A, 1");
        }

        [TestMethod]
        public void Combine_SingleArgument ()
        {            
            var actual = StringExtensions.Combine(", ", "ABC");

            //Assert
            actual.Should().Be("ABC");
        }

        [TestMethod]
        public void Combine_FirstArgumentContainsSeparator ()
        {
            //Act
            var actual = StringExtensions.Combine(";", ";ABC", "123");

            //Assert
            actual.Should().Be(";ABC;123");
        }

        [TestMethod]
        public void Combine_LastArgumentContainsSeparator ()
        {
            //Act
            var actual = StringExtensions.Combine(";", "ABC", ";123");

            //Assert
            actual.Should().Be("ABC;123");
        }

        [TestMethod]
        public void Combine_AllArgumentsContainsSeparator ()
        {
            //Act
            var actual = StringExtensions.Combine(";", ";ABC", ";123");

            //Assert
            actual.Should().Be(";ABC;123");
        }

        [TestMethod]
        public void Combine_ArgumentsHaveSeparatorOnEachSide ()
        {
            //Act
            var actual = StringExtensions.Combine(";", ";ABC;", ";123;");

            //Assert
            actual.Should().Be(";ABC;123;");
        }

        [TestMethod]
        public void Combine_ArgumentIsEnumerable ()
        {
            //Arrange
            var arguments = new List<string>() { "ABC", "123", "DEF", "456" };

            //Act
            var actual = StringExtensions.Combine(" | ", arguments);

            //Assert
            actual.Should().Be("ABC | 123 | DEF | 456");
        }

        [TestMethod]
        public void Combine_EnumerableArgumentIsNull ( )
        {
            List<string> arguments = null;

            //Act
            var actual = StringExtensions.Combine(" | ", arguments);

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void Combine_VariableArgumentIsNull ()
        {
            //Act
            var actual = StringExtensions.Combine(" | ");

            //Assert
            actual.Should().BeEmpty();
        }
    }
}
