/*
 * Copyright © 2016 Michael Taylor
 * All Rights Reserved
 * 
 * From code copyright Federation of State Medical Boards
 */
using System;

namespace P3Net.Kraken.Diagnostics
{
    /// <summary>Provides extension methods for <see cref="ArgumentConstraint{Date}"/>.</summary>
    public static class DateArgumentConstraintExtensions
    {
        /// <summary>Verifies the value is not <see cref="Date.None"/>.</summary>
        /// <param name="source">The source value.</param>
        /// <returns>The new constraint.</returns>
        public static AndArgumentConstraint<Date> IsNotNone ( this ArgumentConstraint<Date> source )
        {
            if (source.Argument.Value == Date.None)
                throw new ArgumentException("Date is not set.", source.Argument.Name);

            return new AndArgumentConstraint<Date>(source);
        }
    }
}
