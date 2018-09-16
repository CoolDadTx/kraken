/* 
 * Copyright © 2011 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Diagnostics.CodeAnalysis;

using FluentAssertions.Execution;
using FluentAssertions.Specialized;

namespace P3Net.Kraken.UnitTesting
{
    /// <summary>Provides additional assertions for exception assertions.</summary>
    [ExcludeFromCodeCoverage]
    public static class ExceptionAssertionExtensions
    {
        #region ContainingMessage

        /// <summary>Asserts that the message contains some text.</summary>
        /// <typeparam name="T">The exception type.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="expected">The expected message.  Only a portion of the entire message needs to match.</param>
        /// <returns>The assertion.</returns>
        public static ExceptionAssertions<T> ContainingMessage<T> ( this ExceptionAssertions<T> source, string expected ) where T : Exception
        {
            //Ensure it is wrapped with *
            return source.WithMessage(EnsureWrappedWithWildcards(expected));
        }

        /// <summary>Asserts that the message contains some text.</summary>
        /// <typeparam name="T">The exception type.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="expected">The expected message.  Only a portion of the entire message needs to match.</param>
        /// <param name="reason">The reason.</param>
        /// <param name="reasonArgs">The reason arguments.</param>
        /// <returns>The assertion.</returns>
        public static ExceptionAssertions<T> ContainingMessage<T> ( this ExceptionAssertions<T> source, string expected,
                                                        string reason, params object[] reasonArgs ) where T : Exception
        {
            return source.WithMessage(EnsureWrappedWithWildcards(expected), reason, reasonArgs);
        }
        #endregion

        #region WithParameter
        
        /// <summary>Asserts that the parameter name matches the expected value.</summary>
        /// <typeparam name="T">The exception type.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="expected">The expected name.</param>
        /// <returns>The assertion.</returns>
        public static ExceptionAssertions<T> WithParameter<T> ( this ExceptionAssertions<T> source, string expected ) where T : ArgumentException
        {
            var e = source.And as ArgumentException;

            Execute.Assertion.ForCondition(String.Compare(e.ParamName, expected, true) == 0)
                                .FailWith("Expected parameter '{0}', but found '{1}'", expected, e.ParamName);

            return source;            
        }

        /// <summary>Asserts that the parameter name matches the expected value.</summary>
        /// <typeparam name="T">The exception type.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="expected">The expected name.</param>
        /// <param name="reason">The reason.</param>
        /// <param name="reasonArgs">The reason arguments.</param>
        /// <returns>The assertion.</returns>
        public static ExceptionAssertions<T> WithParameter<T> ( this ExceptionAssertions<T> source, string expected, 
                                                        string reason, params object[] reasonArgs ) where T : ArgumentException
        {
            var e = source.And as ArgumentException;

            Execute.Assertion.ForCondition(String.Compare(e.ParamName, expected, true) == 0)
                                .BecauseOf(reason, reasonArgs)
                                .FailWith("Expected parameter '{0}', but found '{1}'", expected, e.ParamName);

            return source;            
        }
        #endregion

        #region Private Members

        private static string EnsureWrappedWithWildcards ( string value )
        {
            return value.EnsureStartsWith("*").EnsureEndsWith("*");
        }
        #endregion
    }
}
