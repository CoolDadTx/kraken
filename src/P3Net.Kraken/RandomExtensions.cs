/*
 * Copyright © 2017 Michael Taylor
 * All Rights Reserved
 */
using System;

using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken
{
    /// <summary>Provides extensions for <see cref="Random"/>.</summary>
    public static class RandomExtensions
    {
        #region Date

        /// <summary>Generates a random date between 1/1/1900 and today.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The date.</returns>
        public static Date NextDate ( this Random source ) => source.NextDate(Dates.January(1, 1900), Date.Today());

        /// <summary>Generates a random date between the specified date and today.</summary>
        /// <param name="source">The source.</param>
        /// <param name="minimumValue">The minimum date value.</param>
        /// <returns>The date.</returns>
        public static Date NextDate ( this Random source, Date minimumValue ) => source.NextDate(minimumValue, Date.Today());

        /// <summary>Generates a random date between the given date range.</summary>
        /// <param name="source">The source.</param>
        /// <param name="minimumValue">The minimum date value.</param>
        /// <param name="maximumValue">The maximum date value.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maximumValue"/> is greater than or equal to <paramref name="minimumValue"/>.</exception>
        public static Date NextDate ( this Random source, Date minimumValue, Date maximumValue )
        {
            Verify.Argument(nameof(maximumValue)).WithValue(maximumValue).IsGreaterThanOrEqualTo(minimumValue);

            var difference = minimumValue.Difference(maximumValue);
            if (difference == 0)
                return minimumValue;

            var offset = source.Next(0, difference);

            return minimumValue.AddDays(offset);
        }
        #endregion        
    }
}

