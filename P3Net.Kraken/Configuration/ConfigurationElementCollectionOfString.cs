/*
 * Copyright © 2017 P3Net
 * All Rights Reserved
 */
using System;
using System.Configuration;
using System.Linq;

namespace P3Net.Kraken.Configuration
{
    /// <summary>Provides a generic collection for configuration elements that use a string key.</summary>    
    /// <typeparam name="TElement">The type of element.</typeparam>
    /// <remarks>
    /// To support keys other than string use <see cref="ConfigurationElementCollection{TElement, TKey}"/>.
    /// </remarks>
    public abstract class ConfigurationElementCollection<TElement> : ConfigurationElementCollection<TElement, string> where TElement : ConfigurationElement, new()
    {
    }
}
