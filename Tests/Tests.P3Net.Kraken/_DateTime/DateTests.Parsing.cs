using System;
using System.Globalization;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken
{    
    public partial class DateTests
    {
        #region Parse
        
        [TestMethod]
        public void Parse_IsValid ()
        {
            var expected = new Date(2013, 4, 10);

            var actual = Date.Parse("4/10/2013");

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Parse_TimeIsIgnored ()
        {
            var expected = new Date(2013, 4, 10);

            var actual = Date.Parse("4/10/2013 12:30:11 AM");

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Parse_WithFormatProvider ()
        {
            var expected = new Date(2013, 4, 10);

            var actual = Date.Parse("April 10, 2013", DateTimeFormatInfo.CurrentInfo);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Parse_WithFormatProviderAndStyle ()
        {
            var expected = new Date(2013, 4, 10);

            var actual = Date.Parse("   April 10,    2013", DateTimeFormatInfo.CurrentInfo, DateTimeStyles.AllowWhiteSpaces);

            actual.Should().Be(expected);
        }

        [TestMethod]        
        public void Parse_IsInvalid ()
        {
            Action action = () => Date.Parse("Hello");

            action.Should().Throw<FormatException>();
        }
        #endregion

        #region ParseExact

        [TestMethod]
        public void ParseExact_IsValid ()
        {
            var expected = new Date(2013, 3, 25);

            var actual = Date.ParseExact("03-25-2013", "MM-dd-yyyy", DateTimeFormatInfo.CurrentInfo);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ParseExact_WithStyles ()
        {
            var expected = new Date(2013, 3, 25);

            var actual = Date.ParseExact("   03-25-2013   ", "MM-dd-yyyy", DateTimeFormatInfo.CurrentInfo, DateTimeStyles.AllowWhiteSpaces);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ParseExact_WithMultipleFormats ()
        {
            var expected = new Date(2013, 3, 25);
            var formats = new string[] { "MM-dd-yyyy", "M-dd-yyyy", "yyyy-MM-dd" };

            var actual = Date.ParseExact("03-25-2013", formats, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.AllowWhiteSpaces);

            actual.Should().Be(expected);
        }

        [TestMethod]        
        public void ParseExact_IsInvalid ()
        {
            Action action = () => Date.ParseExact("03-25-2013", "yyyy-MM-dd", DateTimeFormatInfo.CurrentInfo);

            action.Should().Throw<FormatException>();
        }
        #endregion

        #region TryParse

        [TestMethod]
        public void TryParse_IsValid ()
        {
            var expected = new Date(2013, 4, 10);
            Date actual;

            var result = Date.TryParse("4/10/2013", out actual);

            result.Should().BeTrue();
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void TryParse_TimeIsIgnored ()
        {
            var expected = new Date(2013, 4, 10);
            Date actual;

            var result = Date.TryParse("4/10/2013 12:30:11 AM", out actual);

            result.Should().BeTrue();
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void TryParse_IsInvalid ()
        {
            Date actual;

            var result = Date.TryParse("xyz 10, 2013", out actual);

            result.Should().BeFalse();
        }

        [TestMethod]
        public void TryParse_WithFormatProviderAndStyle ()
        {
            var expected = new Date(2013, 4, 10);
            Date actual;

            var result = Date.TryParse("   April 10,    2013", DateTimeFormatInfo.CurrentInfo, DateTimeStyles.AllowWhiteSpaces, out actual);

            result.Should().BeTrue();
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void TryParse_WithFormatProviderAndStyle_IsInvalid ()
        {
            Date actual;

            var result = Date.TryParse("Hello", DateTimeFormatInfo.CurrentInfo, DateTimeStyles.AllowWhiteSpaces, out actual);

            result.Should().BeFalse();
        }
        #endregion

        #region TryParseExact

        [TestMethod]
        public void TryParseExact_IsValid ()
        {
            var expected = new Date(2013, 3, 25);
            Date actual;

            var result = Date.TryParseExact("  03-25-2013  ", "MM-d-yyyy", DateTimeFormatInfo.CurrentInfo, DateTimeStyles.AllowWhiteSpaces, out actual);

            result.Should().BeTrue();
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void TryParseExact_IsInvalid ()
        {
            var expected = new Date(2013, 3, 25);
            Date actual;

            var result = Date.TryParseExact("03-25-2013", "yyyy/MM/dd", DateTimeFormatInfo.CurrentInfo, DateTimeStyles.AllowWhiteSpaces, out actual);

            result.Should().BeFalse();
        }

        [TestMethod]
        public void TryParseExact_WithMultipleFormats ()
        {
            var expected = new Date(2013, 3, 25);
            var formats = new string[] { "MM-dd-yyyy", "M-dd-yyyy", "yyyy-MM-dd" };
            Date actual;

            var result = Date.TryParseExact("03-25-2013", formats, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.AllowWhiteSpaces, out actual);

            result.Should().BeTrue();
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void TryParseExact_WithMultipleFormats_IsInvalid ()
        {
            var formats = new string[] { "MM-dd-yyyy", "M-dd-yyyy", "yyyy-MM-dd" };
            Date actual;

            var result = Date.TryParseExact("Hello", formats, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.AllowWhiteSpaces, out actual);

            result.Should().BeFalse();
        }
        #endregion
    }
}
