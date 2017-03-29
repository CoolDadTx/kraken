/*
 * Copyright © 2012 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.ComponentModel
{
    /// <summary>Converts an object to a dictionary.</summary>
    public static class ObjectToDictionaryConverter
    {
        /// <summary>Converts an object to a dictionary of key-value pairs.</summary>
        /// <param name="values">The value to convert.</param>
        /// <returns>A dictionary with the object properties as the keys and the property values as the key values.</returns>
        /// <remarks>
        /// This method is primarily useful for converting an anonymous type to a set of key-value pairs.  It does not
        /// attempt to determine if the value is already a dictionary.  Each public property is converted to a key.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/>.</exception>
        public static IDictionary<string, object> ToDictionary ( object values )
        {
            Verify.Argument("values", values).IsNotNull();

            var items = new Dictionary<string, object>();

            var properties = TypeDescriptor.GetProperties(values);
            foreach (PropertyDescriptor property in properties)
            {
                items[property.Name] = property.GetValue(values);
            };

            return items;
        }
    }
}
