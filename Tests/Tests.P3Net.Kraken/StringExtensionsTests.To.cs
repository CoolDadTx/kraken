#region Imports

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken;
#endregion

namespace Tests.P3Net.Kraken
{    
    public partial class StringExtensionsTests
    {
        #region ToCamel

        [TestMethod]
        public void ToCamel_LowerCaseStart()
        {
            //Act
            var actual = "abc".ToCamel();

            //Assert
            actual.Should().Be("abc");
        }

        [TestMethod]
        public void ToCamel_UpperCaseStart ()
        {
            //Act
            var actual = "Abc".ToCamel();

            //Assert
            actual.Should().Be("abc");
        }

        [TestMethod]
        public void ToCamel_SourceIsEmpty ()
        {
            //Act
            var actual = "".ToCamel();

            //Assert
            actual.Should().BeEmpty();
        }
        #endregion

        #region ToPascal

        [TestMethod]
        public void ToPascal_LowerCaseStart ()
        {
            //Act
            var actual = "abc".ToPascal();

            //Assert
            actual.Should().Be("Abc");
        }

        [TestMethod]
        public void ToPascal_UpperCaseStart ()
        {
            //Act
            var actual = "Abc".ToPascal();

            //Assert
            actual.Should().Be("Abc");
        }

        [TestMethod]
        public void ToPascal_SourceIsEmpty ()
        {
            //Act
            var actual = "".ToPascal();

            //Assert
            actual.Should().BeEmpty();
        }
        #endregion

        #region ToTitleCase

        [TestMethod]
        public void ToTitleCase_Works ()
        {
            var target = "Hello World";
            var expected = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(target);

            var actual = target.ToTitleCase();

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToTitleCase_WithCulture ()
        {
            var target = "Hello World";
            var culture = CultureInfo.InvariantCulture;
            var expected = culture.TextInfo.ToTitleCase(target);

            var actual = target.ToTitleCase(culture);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToTitleCase_WithNullCulture ()
        {
            var target = "Hello World";
            var expected = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(target);

            var actual = target.ToTitleCase(null);

            actual.Should().Be(expected);
        }
        #endregion

        #region ToUserFriendly

        [TestMethod]
        public void ToUserFriendly_LowerCaseStart ()
        {
            //Act
            var actual = "item".ToUserFriendly();

            //Assert
            actual.Should().Be("Item");
        }

        [TestMethod]
        public void ToUserFriendly_UpperCaseStart ()
        {
            //Act
            var actual = "Item".ToUserFriendly();

            //Assert
            actual.Should().Be("Item");
        }

        [TestMethod]
        public void ToUserFriendly_WithMultipleWords ()
        {
            //Act
            var actual = "FirstGrade".ToUserFriendly();

            //Assert
            actual.Should().Be("First Grade");
        }
        [TestMethod]
        public void ToUserFriendly_WithNumber ()
        {
            //Act
            var actual = "Item1".ToUserFriendly();

            //Assert
            actual.Should().Be("Item 1");
        }

        [TestMethod]
        public void ToUserFriendly_WithMultipleWordsAndNumbers ()
        {
            //Act
            var actual = "PartCode123".ToUserFriendly();

            //Assert
            actual.Should().Be("Part Code 123");
        }

        [TestMethod]
        public void ToUserFriendly_WithNumbersAndLowercaseWords ()
        {
            //Act
            var actual = "remote1user".ToUserFriendly();

            //Assert
            actual.Should().Be("Remote 1 User");
        }

        [TestMethod]
        public void ToUserFriendly_WithUnderscore ()
        {
            //Act
            var actual = "Aircraft_Type".ToUserFriendly();

            //Assert
            actual.Should().Be("Aircraft Type");
        }

        [TestMethod]
        public void ToUserFriendly_WithUnderscoreAndLowercase ()
        {
            //Act
            var actual = "Aircraft_type".ToUserFriendly();

            //Assert
            actual.Should().Be("Aircraft Type");
        }

        [TestMethod]
        public void ToUserFriendly_WithMultipleUnderscores ()
        {
            //Act
            var actual = "Aircraft___Type".ToUserFriendly();

            //Assert
            actual.Should().Be("Aircraft Type");
        }

        [TestMethod]
        public void ToUserFriendly_WithAcronym ()
        {
            //Act
            var actual = "ICAOCode".ToUserFriendly();

            //Assert
            actual.Should().Be("ICAO Code");
        }

        [TestMethod]
        public void ToUserFriendly_WithAcronymAndNumber ()
        {
            //Act
            var actual = "ICAO1code".ToUserFriendly();

            //Assert
            actual.Should().Be("ICAO 1 Code");
        }

        [TestMethod]
        public void ToUserFriendly_NoUpperWordBoundary_WithMultipleUpperWords ()
        {
            //Act
            var actual = "Part1Number".ToUserFriendly(false);

            //Assert
            actual.Should().Be("Part 1 Number");
        }

        [TestMethod]
        public void ToUserFriendly_NoUpperWordBoundary_WithMultipleLowerWords ()
        {
            //Act
            var actual = "Part1number".ToUserFriendly(false);

            //Assert
            actual.Should().Be("Part 1 number");
        }

        [TestMethod]
        public void ToUserFriendly_NoUpperWordBoundary_StartsWithLowercaseLetter ()
        {
            //Act
            var actual = "firstItem".ToUserFriendly(false);

            //Assert
            actual.Should().Be("first Item");
        }

        [TestMethod]
        public void ToUserFriendly_NoUpperWordBound_WithSpaceAndLowercaseLetter ()
        {
            //Act
            var actual = "Item_one".ToUserFriendly(false);

            //Assert
            actual.Should().Be("Item one");
        }

        [TestMethod]
        public void ToUserFriendly_WithPeriod ()
        {
            //Act
            var actual = "Doctype.Form".ToUserFriendly();

            //Assert
            actual.Should().Be("Doctype Form");
        }

        [TestMethod]
        public void ToUserFriendly_SourceIsEmpty ()
        {
            //Act
            var actual = "".ToUserFriendly();

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void ToUserFriendly_SourceHasLeadingSpaces ()
        {
            //Act
            var actual = "  Item1".ToUserFriendly();

            //Assert
            actual.Should().Be("Item 1");
        }

        [TestMethod]
        public void ToUserFriendly_SourceStartsWithDigit ()
        {
            //Act
            var actual = "1stItem".ToUserFriendly();

            //Assert
            actual.Should().Be("1 St Item");
        }

        [TestMethod]
        public void ToUserFriendly_WithSpaceAndNumber ()
        {
            //Act
            var actual = "Item_1".ToUserFriendly();

            //Assert
            actual.Should().Be("Item 1");
        }

        [TestMethod]
        public void ToUserFriendly_WithSpaceAndLowercaseLetter ()
        {
            //Act
            var actual = "Item_one".ToUserFriendly();

            //Assert
            actual.Should().Be("Item One");
        }
        #endregion
    }
}
