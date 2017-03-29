/*
 * Copyright © 2013 Michael Taylor
 * All Rights Reserved
 * 
 * From code copyright Federation of State Medical Boards
 */
using System;

using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken
{
    // Handles date verification
    internal static class DateVerification
    {
        /// <summary>Verifies the month is valid.</summary>
        /// <param name="value">The value.</param>
        /// <param name="name">The argument name, for errors.</param>
        /// <remarks>
        /// The month is assumed to be the full <see cref="DateTime"/> range.
        /// </remarks>
        public static void ValidateMonth ( int value, string name )
        {
            Verify.Argument(name, value).IsBetween(Dates.MinimumMonth, Dates.MaximumMonth);
        }

        /// <summary>Verifies the year is valid for <see cref="DateTime"/>.</summary>
        /// <param name="value">The value.</param>
        /// <param name="name">The argument name, for errors.</param>
        /// <remarks>
        /// The year is assumed to be the full <see cref="DateTime"/> range.
        /// </remarks>
        public static void ValidateYear ( int value, string name )
        {
            Verify.Argument(name, value).IsBetween(Dates.MinimumYear, Dates.MaximumYear);
        }
    }
}
