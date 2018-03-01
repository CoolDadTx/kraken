/*
 * Copyright © 2012 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Types;

namespace P3Net.Kraken.UnitTesting
{
    /// <summary>Provides extension methods for <see cref="PropertyInfoAssertions"/>.</summary>
    public static class PropertyInfoAssertionsExtensions
    {
        #region Be

        /// <summary>Asserts that a method matches the expected value.</summary>
        /// <param name="source">The source value.</param>
        /// <param name="expected">The expected method.</param>
        /// <returns>The constraint.</returns>
        public static AndConstraint<PropertyInfoAssertions> Be ( this PropertyInfoAssertions source, PropertyInfo expected )
        {
            return Be(source, expected, "");
        }

        /// <summary>Asserts that a method matches the expected value.</summary>
        /// <param name="source">The source value.</param>
        /// <param name="expected">The expected method.</param>
        /// <param name="reason">The reason.</param>
        /// <param name="reasonArgs">The reason arguments.</param>
        /// <returns>The constraint.</returns>
        public static AndConstraint<PropertyInfoAssertions> Be ( this PropertyInfoAssertions source, PropertyInfo expected, string reason, params object[] reasonArgs )
        {
            var actual = source.Subject;

            Execute.Assertion.ForCondition(Object.ReferenceEquals(actual, expected))
                            .BecauseOf(reason, reasonArgs)
                            .FailWith("Expected '{0}'{reason}, but found {1}.", ((expected != null) ? expected.Name : "(null)"), ((actual != null) ? actual.Name : "(null)" ));

            return new AndConstraint<PropertyInfoAssertions>(source);
        }
        #endregion

        #region BeNullFixed

        /// <summary>Asserts that a method is <see langword="null"/>.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        public static AndConstraint<PropertyInfoAssertions> BeNullFixed ( this PropertyInfoAssertions source )
        {
            return BeNullFixed(source, "");
        }

        /// <summary>Asserts that a method is <see langword="null"/>.</summary>
        /// <param name="source">The source.</param>
        /// <param name="reason">The reason.</param>
        /// <param name="reasonArgs">The reason arguments.</param>
        /// <returns>The constraint.</returns>
        public static AndConstraint<PropertyInfoAssertions> BeNullFixed ( this PropertyInfoAssertions source, string reason, params object[] reasonArgs )
        {
            var actual = source.Subject;

            Execute.Assertion.ForCondition(actual == null)
                                .BecauseOf(reason, reasonArgs)
                                .FailWith("Expected null{reason}, but found {0}.", ((actual != null) ? actual.Name : "(null)"));

            return new AndConstraint<PropertyInfoAssertions>(source);
        }
        #endregion

        #region NotBeNullFixed

        /// <summary>Asserts that a method is not <see langword="null"/>.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        public static AndConstraint<PropertyInfoAssertions> NotBeNullFixed ( this PropertyInfoAssertions source )
        {
            return NotBeNullFixed(source, "");
        }

        /// <summary>Asserts that a method is not <see langword="null"/>.</summary>
        /// <param name="source">The source.</param>
        /// <param name="reason">The reason.</param>
        /// <param name="reasonArgs">The reason arguments.</param>
        /// <returns>The constraint.</returns>
        public static AndConstraint<PropertyInfoAssertions> NotBeNullFixed ( this PropertyInfoAssertions source, string reason, params object[] reasonArgs )
        {
            var actual = source.Subject;

            Execute.Assertion.ForCondition(actual != null)
                                .BecauseOf(reason, reasonArgs)
                                .FailWith("Expected not null{reason}, but found null.");

            return new AndConstraint<PropertyInfoAssertions>(source);
        }
        #endregion
    }
}
