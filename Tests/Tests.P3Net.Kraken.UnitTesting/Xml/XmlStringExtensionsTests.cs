using System;
using System.Xml;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

using P3Net.Kraken.UnitTesting;
using P3Net.Kraken.UnitTesting.Xml;

namespace Tests.P3Net.Kraken.UnitTesting.Xml
{
    [TestClass]
    public class XmlStringExtensionsTests : UnitTest
    {
        #region GetAttributeValueFromElement

        [TestMethod]
        public void GetAttributeValueFromElement_AttributeExists ()
        {
            var expectedAttribute = "attribute1";
            var expectedValue = "5";
            var xml = String.Format("<element {0}=\"{1}\" />", expectedAttribute, expectedValue);

            var actual = XmlStringExtensions.GetAttributeValueFromElement(xml, expectedAttribute);

            //Assert
            actual.Should().Be(expectedValue);
        }

        [TestMethod]
        public void GetAttributeValueFromElement_AttributeDoesNotExists ()
        {
            var xml = "<element attribute1=\"5\" attribute2=\"10\" />";

            var actual = XmlStringExtensions.GetAttributeValueFromElement(xml, "attribute3");

            //Assert
            actual.Should().BeNull();
        }

        [TestMethod]
        public void GetAttributeValueFromElement_AttributeIsEmpty ()
        {
            var xml = "<element attribute1=\"5\" attribute2=\"\" />";

            var actual = XmlStringExtensions.GetAttributeValueFromElement(xml, "attribute2");

            //Assert
            actual.Should().BeEmpty();
        }
        #endregion

        #region GetContentFromElement

        [TestMethod]
        public void GetContentFromElement_ContentExists ()
        {
            var expectedContent = "Some Content";
            var xml = String.Format("<element>{0}</element>", expectedContent);

            var actual = XmlStringExtensions.GetContentFromElement(xml);

            //Assert
            actual.Should().Be(expectedContent);
        }

        [TestMethod]
        public void GetContentFromElement_IsEmpty ()
        {
            var xml = "<element />";

            var actual = XmlStringExtensions.GetContentFromElement(xml);

            //Assert
            actual.Should().BeEmpty();
        }
        #endregion
    }
}
