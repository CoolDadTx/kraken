/*
 * Copyright © 2014 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace P3Net.Kraken
{
    /// <summary>Provides extension methods for <see cref="IComparable{T}"/>.</summary>
    public static class ComparableExtensions
    {
        /// <summary>Determines if a value is between two values, inclusive.</summary>
        /// <param name="source">The source value.</param>
        /// <param name="minimumValue">The minimum value.</param>
        /// <param name="maximumValue">The Maximum value.</param>
        /// <returns><see langword="true"/> if the value is between the two values, inclusive.</returns>
        public static bool Between<T> ( this IComparable<T> source, T minimumValue, T maximumValue )
        {
            return source.CompareTo(minimumValue) >= 0 && source.CompareTo(maximumValue) <= 0;
        }

        /// <summary>Determines if a value is between two values, exclusive.</summary>
        /// <param name="source">The source value.</param>
        /// <param name="minimumValue">The minimum value.</param>
        /// <param name="maximumValue">The Maximum value.</param>
        /// <returns><see langword="true"/> if the value is between the two values, exclusive.</returns>
        public static bool BetweenExclusive<T> ( this IComparable<T> source, T minimumValue, T maximumValue )
        {
            return source.CompareTo(minimumValue) > 0 && source.CompareTo(maximumValue) < 0;
        }
    }
}
