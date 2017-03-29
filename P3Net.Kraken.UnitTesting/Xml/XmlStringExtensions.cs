/*
 * Copyright © 2012 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace P3Net.Kraken.UnitTesting.Xml
{
    /// <summary>Provides helper methods for working with XML.</summary>
    public static class XmlStringExtensions
    {
        /// <summary>Gets an attribute value given an XML element.</summary>
        /// <param name="xmlElement">The XML element.</param>
        /// <param name="attribute">The attribute name.</param>
        /// <returns>The attribute value or <see langword="null"/> if it does not exist.</returns>
        public static string GetAttributeValueFromElement ( string xmlElement, string attribute )
        {
            var xml = XElement.Parse(xmlElement);
            var attr = xml.Attribute(attribute);

            return (attr != null) ? attr.Value : null;
        }

        /// <summary>Gets the content (inner HTML) given an XML element.</summary>
        /// <param name="xmlElement">The XML element.</param>
        /// <returns>The content of the element.</returns>
        public static string GetContentFromElement ( string xmlElement )
        {
            var xml = XElement.Parse(xmlElement);
                        
            return xml.Value;
        }
    }
}
