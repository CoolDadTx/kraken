/*
 * Copyright © 2013 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace P3Net.Kraken.Reflection
{
    /// <summary>Provides options for getting a type name from <see cref="TypeNameProvider"/>.</summary>
    public struct TypeNameProviderOptions
    {
        /// <summary>Gets or sets whether to include the namespace or not.</summary>
        /// <value>The default is <see langword="false"/>.</value>
        public bool IncludeNamespace { get; set; }
    }
}
