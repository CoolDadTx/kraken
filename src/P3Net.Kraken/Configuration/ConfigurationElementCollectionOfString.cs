/*
 * Copyright © 2017 P3Net
 * All Rights Reserved
 */
#if NET_FRAMEWORK

using System;
using System.Configuration;

namespace P3Net.Kraken.Configuration
{
    /// <summary>Provides a generic collection for configuration elements that use a string key.</summary>    
    /// <typeparam name="TElement">The type of element.</typeparam>
    /// <remarks>
    /// To support keys other than string use <see cref="ConfigurationElementCollection{TKey, TElement}"/>.
    /// </remarks>
    public abstract class ConfigurationElementCollection<TElement> : ConfigurationElementCollection<string, TElement> where TElement : ConfigurationElement, new()
    {
    }
}
#endif