/*
 * Copyright © 2011 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace P3Net.Kraken.UnitTesting
{
    /// <summary>Provides additional DateTime assertions.</summary>
    public static class DateTimeAssertionsExtensions
    {
        #region BeOnDate
        
        /// <summary>Asserts that a value is the same date as another, ignoring time.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="expected">The expected value.</param>
        /// <returns>The constraint.</returns>
        public static AndConstraint<DateTimeAssertions> BeDate ( this DateTimeAssertions source, DateTime expected )
        {
            return source.HaveYear(expected.Year).And.HaveMonth(expected.Month).And.HaveDay(expected.Day);
        }

        /// <summary>Asserts that a value is the same date as another, ignoring time.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="reason">The reason for the failure.</param>
        /// <param name="reasonArgs">The optional reason arguments.</param>
        /// <returns>The constraint.</returns>
        public static AndConstraint<DateTimeAssertions> BeDate ( this DateTimeAssertions source, DateTime expected, string reason, params object[] reasonArgs )
        {
            var actual = source.Subject.GetValueOrDefault();
            Execute.Assertion.ForCondition(actual.Year == expected.Year && actual.Month == expected.Month && actual.Day == expected.Day)
                                .BecauseOf(reason, reasonArgs)
                                .FailWith("Expected {0} {reason}, but found {1}.", expected.ToString("MM/dd/yyyy"), actual.ToString("MM/dd/yyyy"));
            return new AndConstraint<DateTimeAssertions>(source);
        }
        #endregion
    }
}
