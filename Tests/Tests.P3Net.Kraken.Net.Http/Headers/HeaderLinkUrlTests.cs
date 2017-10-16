using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

using P3Net.Kraken.Net.Http.Headers;

namespace Tests.P3Net.Kraken.Net.Http.Headers
{
    [TestClass]
    public class HeaderLinkUrlTests
    {
        #region Ctor

        [TestMethod]
        public void Ctor_StringUrlIsValid ()
        {
            var expected = "http://www.google.com";

            var actual = new HeaderLinkUrl(expected);

            actual.Url.ToString().TrimEnd('/').Should().BeEquivalentTo(expected.TrimEnd('/'));
        }

        [TestMethod]
        public void Ctor_StringUrlIsRelative ()
        {
            var expected = "Testing\abc";

            Action action = () => new HeaderLinkUrl(expected);

            action.ShouldThrow<UriFormatException>();
        }

        [TestMethod]
        public void Ctor_StringUrlIsNull ()
        {
            Action action = () => new HeaderLinkUrl((string)null);

            action.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void Ctor_StringUrlIsEmpty ()
        {
            Action action = () => new HeaderLinkUrl("");

            action.ShouldThrow<UriFormatException>();
        }

        [TestMethod]
        public void Ctor_StringUrlIsBadFormat ()
        {
            Action action = () => new HeaderLinkUrl(@"d:Fdfa:fagaga:24252:q5q35q:aafdadfa:");

            action.ShouldThrow<FormatException>();
        }

        [TestMethod]
        public void Ctor_UrlIsValid ()
        {
            var expected = "http://www.google.com";

            var actual = new HeaderLinkUrl(expected);

            actual.Url.ToString().TrimEnd('/').Should().BeEquivalentTo(expected.TrimEnd('/'));
        }

        [TestMethod]
        public void Ctor_UrlIsRelative ()
        {
            var expected = new Uri("Testing", UriKind.Relative);

            Action action = () => new HeaderLinkUrl(expected);

            action.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void Ctor_UrlIsNull ()
        {
            Action action = () => new HeaderLinkUrl((Uri)null);

            action.ShouldThrow<ArgumentNullException>();
        }
        #endregion

        #region Parse

        [TestMethod]
        public void Parse_StringUrlIsValid ()
        {
            var expected = "http://www.google.com";

            var actual = HeaderLinkUrl.Parse($"<{expected}>");

            actual.Url.ToString().TrimEnd('/').Should().BeEquivalentTo(expected.TrimEnd('/'));
        }

        [TestMethod]
        public void Parse_StringUrlWithRelation ()
        {
            var expectedUrl = "http://www.google.com";
            var expectedRelation = "next";
            
            var actual = HeaderLinkUrl.Parse($"<{expectedUrl}>;rel=\"{expectedRelation}\"");

            actual.Url.ToString().TrimEnd('/').Should().BeEquivalentTo(expectedUrl.TrimEnd('/'));
            actual.Relation.Should().Be(expectedRelation);
        }

        [TestMethod]
        public void Parse_StringUrlWithMultipleRelation ()
        {
            var expectedUrl = "http://www.google.com";
            var expectedRelations = new[] { StandardLinkTypes.Next, StandardLinkTypes.Alternate };

            var actual = HeaderLinkUrl.Parse($"<{expectedUrl}>;rel=\"{String.Join(" ", expectedRelations)}\"");

            actual.Url.ToString().TrimEnd('/').Should().BeEquivalentTo(expectedUrl.TrimEnd('/'));
            actual.Relations.Should().Contain(expectedRelations);
        }

        [TestMethod]
        public void Parse_StringUrlIsBad ()
        {
            var expected = "</Testing>";

            Action action = () => HeaderLinkUrl.Parse(expected);

            action.ShouldThrow<FormatException>();
        }

        [TestMethod]
        public void Parse_StringUrlIsNull ()
        {
            Action action = () => HeaderLinkUrl.Parse((string)null);

            action.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void Parse_StringUrlIsEmpty ()
        {
            Action action = () => HeaderLinkUrl.Parse("");

            action.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void Parse_StringUrlIsBadFormat ()
        {
            Action action = () => HeaderLinkUrl.Parse(@"D:\/\Temp\/f\test.dat");

            action.ShouldThrow<FormatException>();
        }
        #endregion

        #region TryParse

        [TestMethod]
        public void TryParse_StringUrlIsValid ()
        {
            var expected = "http://www.google.com";

            HeaderLinkUrl actual;
            var actualResult = HeaderLinkUrl.TryParse($"<{expected}>", out actual);

            actualResult.Should().BeTrue();
            actual.Url.ToString().TrimEnd('/').Should().BeEquivalentTo(expected.TrimEnd('/'));
        }

        [TestMethod]
        public void TryParse_StringUrlWithUnknownParameter ()
        {
            var expectedUrl = "http://www.google.com";
            HeaderLinkUrl actual;
            var actualResult = HeaderLinkUrl.TryParse($"<{expectedUrl}>;abc=\"aadf\"", out actual);

            actualResult.Should().BeTrue();
            actual.Url.ToString().TrimEnd('/').Should().BeEquivalentTo(expectedUrl.TrimEnd('/'));
        }

        [TestMethod]
        public void TryParse_StringUrlIsBadFormat ()
        {
            HeaderLinkUrl actual;
            var actualResult = HeaderLinkUrl.TryParse($"http://www.google.com", out actual);

            actualResult.Should().BeFalse();
        }

        [TestMethod]
        public void TryParse_StringUrlIsRelative ()
        {
            var expected = "/Testing";

            HeaderLinkUrl actual;
            var actualResult = HeaderLinkUrl.TryParse(expected, out actual);

            actualResult.Should().BeFalse();            
        }

        [TestMethod]
        public void TryParse_StringUrlIsNull ()
        {
            HeaderLinkUrl actual;
            var actualResult = HeaderLinkUrl.TryParse((string)null, out actual);

            actualResult.Should().BeFalse();
        }

        [TestMethod]
        public void TryParse_StringUrlIsEmpty ()
        {
            HeaderLinkUrl actual;
            var actualResult = HeaderLinkUrl.TryParse("", out actual);

            actualResult.Should().BeFalse();
        }        
        #endregion
    }
}
