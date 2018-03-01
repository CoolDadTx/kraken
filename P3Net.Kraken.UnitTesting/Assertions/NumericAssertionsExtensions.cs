/*
 * Copyright © 2011 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Numeric;
using FluentAssertions.Primitives;

namespace P3Net.Kraken.UnitTesting
{
    /// <summary>Provides additional numerical assertions.</summary>
    public static class NumericAssertionsExtensions
    {
        #region BeApproximately 
              
        #region Decimal

        /// <summary>Asserts that a value is approximately equal to the expected value.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="expected">The expected value.</param>
        /// <returns>The constraint.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Nested type is reasonable.")]
        public static AndConstraint<NumericAssertions<decimal>> BeApproximately ( this NumericAssertions<decimal> source, decimal expected )
        {
            return source.BeInRange(expected, expected);
        }

        /// <summary>Asserts that a value is approximately equal to the expected value.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="reason">The reason for the failure.</param>
        /// <param name="reasonArgs">The optional reason arguments.</param>        
        /// <returns>The constraint.</returns>
        [ExcludeFromCodeCoverage]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Nested type is reasonable.")]
        public static AndConstraint<NumericAssertions<decimal>> BeApproximately ( this NumericAssertions<decimal> source, decimal expected,
                                                    string reason, params object[] reasonArgs )
        {
            return source.BeInRange(expected, expected, reason, reasonArgs);
        }
        #endregion

        #region Double
        
        /// <summary>Asserts that a value is approximately equal to the expected value.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="expected">The expected value.</param>
        /// <returns>The constraint.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Nested type is reasonable.")]
        public static AndConstraint<NumericAssertions<double>> BeApproximately ( this NumericAssertions<double> source, double expected )
        {
            return source.BeApproximately(expected, Double.Epsilon, "");
        }

        /// <summary>Asserts that a value is approximately equal to the expected value.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="reason">The reason for the failure.</param>
        /// <param name="reasonArgs">The optional reason arguments.</param>
        /// <returns>The constraint.</returns>
        [ExcludeFromCodeCoverage]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Nested type is reasonable.")]
        public static AndConstraint<NumericAssertions<double>> BeApproximately ( this NumericAssertions<double> source, 
                                    double expected, string reason, params object[] reasonArgs )
        {
            return source.BeApproximately(expected, Double.Epsilon, reason, reasonArgs);
        }
        #endregion

        #region Float

        /// <summary>Asserts that a value is approximately equal to the expected value.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="expected">The expected value.</param>
        /// <returns>The constraint.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Nested type is reasonable.")]
        public static AndConstraint<NumericAssertions<float>> BeApproximately ( this NumericAssertions<float> source, float expected )
        {
            return source.BeApproximately(expected, Single.Epsilon, "");
        }

        /// <summary>Asserts that a value is approximately equal to the expected value.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="reason">The reason for the failure.</param>
        /// <param name="reasonArgs">The optional reason arguments.</param>        
        /// <returns>The constraint.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Nested type is reasonable.")]
        public static AndConstraint<NumericAssertions<float>> BeApproximately ( this NumericAssertions<float> source, float expected,
                                    string reason, params object[] reasonArgs )
        {
            return source.BeApproximately(expected, Single.Epsilon, reason, reasonArgs);
        }       
        #endregion
        
        #endregion

        #region BeExactly

        #region Decimal

        /// <summary>Asserts that a value is exactly a given integral value.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="expected">The expected value.</param>
        /// <returns>The constraint.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Nested type is reasonable.")]
        public static AndConstraint<NumericAssertions<decimal>> BeExactly ( this NumericAssertions<decimal> source, int expected )
        {
            return source.BeInRange(expected, expected);
        }

        /// <summary>Asserts that a value is exactly a given integral value.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="reason">The reason for the failure.</param>
        /// <param name="reasonArgs">The optional reason arguments.</param>        
        /// <returns>The constraint.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Nested type is reasonable.")]
        public static AndConstraint<NumericAssertions<decimal>> BeExactly ( this NumericAssertions<decimal> source, int expected,
                                                    string reason, params object[] reasonArgs )
        {
            return source.BeInRange(expected, expected, reason, reasonArgs);
        }
        #endregion

        #region Double
        
        /// <summary>Asserts that a value is exactly a given integral value.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="expected">The expected integral value.</param>
        /// <returns>The constraint.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Nested type is reasonable.")]
        public static AndConstraint<NumericAssertions<double>> BeExactly ( this NumericAssertions<double> source, int expected )
        {
            return BeExactly(source, expected, "");
        }

        /// <summary>Asserts that a value is exactly a given integral value.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="expected">The expected integral value.</param>
        /// <param name="reason">The reason for the failure.</param>
        /// <param name="reasonArgs">The optional reason arguments.</param>        
        /// <returns>The constraint.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Nested type is reasonable.")]
        public static AndConstraint<NumericAssertions<double>> BeExactly ( this NumericAssertions<double> source, int expected,
                                        string reason, params object[] reasonArgs )
        {
            return source.BeInRange(expected, expected, reason, reasonArgs);
        }
        #endregion

        #region Single
        
        /// <summary>Asserts that a value is exactly a given integral value.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="expected">The expected integral value.</param>
        /// <returns>The constraint.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Nested type is reasonable.")]
        public static AndConstraint<NumericAssertions<float>> BeExactly ( this NumericAssertions<float> source, int expected )
        {
            return BeExactly(source, expected, "");
        }

        /// <summary>Asserts that a value is exactly a given integral value.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="expected">The expected integral value.</param>
        /// <param name="reason">The reason for the failure.</param>
        /// <param name="reasonArgs">The optional reason arguments.</param>        
        /// <returns>The constraint.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Nested type is reasonable.")]
        public static AndConstraint<NumericAssertions<float>> BeExactly ( this NumericAssertions<float> source, int expected,
                            string reason, params object[] reasonArgs )
        {
            return source.BeInRange(expected, expected, reason, reasonArgs);
        }
        #endregion

        #endregion
    }
}
