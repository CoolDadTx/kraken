/*
 * Copyright © 2014 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace P3Net.Kraken.Text
{
    /// <summary>Provides extension methods for <see cref="StringComparison"/>.</summary>
    public static class StringComparisonExtensions
    {                
        /// <summary>Gets the corresponding <see cref="StringComparer"/> instance.</summary>
        /// <param name="source">The source value.</param>
        /// <returns>The comparer.</returns>
        public static StringComparer ToComparer ( this StringComparison source )
        {
            switch (source)
            {
                case StringComparison.CurrentCulture: return StringComparer.CurrentCulture;
                case StringComparison.CurrentCultureIgnoreCase: return StringComparer.CurrentCultureIgnoreCase;

                case StringComparison.InvariantCulture: return StringComparer.InvariantCulture;
                case StringComparison.InvariantCultureIgnoreCase: return StringComparer.InvariantCultureIgnoreCase;

                case StringComparison.Ordinal: return StringComparer.Ordinal;
                case StringComparison.OrdinalIgnoreCase: return StringComparer.OrdinalIgnoreCase;
            };

            throw new ArgumentOutOfRangeException("source", "Unknown comparison value.");
        }
    }
}
