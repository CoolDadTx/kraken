/* 
 * Copyright © 2011 Michael Taylor
 */
using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using FluentAssertions.Specialized;

namespace P3Net.Kraken.UnitTesting
{
    /// <summary>Provides additional assertions for objects.</summary>
    public static class AssertionExtensions
    {
        /// <summary>Gets the assertion object for <see cref="IntPtr"/>.</summary>
        /// <param name="source">The source.</param>
        /// <returns>An assertion object.</returns>
        public static IntPtrAssertions Should ( this IntPtr source )
        {
            return new IntPtrAssertions(source);
        }

        #region ShouldThrowArgumentException

        /// <summary>Asserts that an action throws an argument exception.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The assertion.</returns>
        public static ExceptionAssertions<ArgumentException> ShouldThrowArgumentException ( this Action source )
        {
            return source.ShouldThrow<ArgumentException>();
        }

        /// <summary>Asserts that an action throws an argument exception.</summary>
        /// <param name="source">The source.</param>
        /// <param name="reason">The reason message.</param>
        /// <returns>The assertion.</returns>
        public static ExceptionAssertions<ArgumentException> ShouldThrowArgumentException ( this Action source, string reason )
        {
            return source.ShouldThrow<ArgumentException>(reason);
        }

        /// <summary>Asserts that an action throws an argument exception.</summary>
        /// <param name="source">The source.</param>
        /// <param name="reason">The reason message.</param>
        /// <param name="reasonArgs">The reason arguments.</param>
        /// <returns>The assertion.</returns>
        public static ExceptionAssertions<ArgumentException> ShouldThrowArgumentException ( this Action source, string reason, params object[] reasonArgs )
        {
            return source.ShouldThrow<ArgumentException>(reason, reasonArgs);
        }
        #endregion

        #region ShouldThrowArgumentNullException

        /// <summary>Asserts that an action throws an argument exception.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The assertion.</returns>
        public static ExceptionAssertions<ArgumentNullException> ShouldThrowArgumentNullException ( this Action source )
        {
            return source.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>Asserts that an action throws an argument exception.</summary>
        /// <param name="source">The source.</param>
        /// <param name="reason">The reason message.</param>
        /// <returns>The assertion.</returns>
        public static ExceptionAssertions<ArgumentNullException> ShouldThrowArgumentNullException ( this Action source, string reason )
        {
            return source.ShouldThrow<ArgumentNullException>(reason);
        }

        /// <summary>Asserts that an action throws an argument exception.</summary>
        /// <param name="source">The source.</param>
        /// <param name="reason">The reason message.</param>
        /// <param name="reasonArgs">The reason arguments.</param>
        /// <returns>The assertion.</returns>
        public static ExceptionAssertions<ArgumentNullException> ShouldThrowArgumentNullException ( this Action source, string reason, params object[] reasonArgs )
        {
            return source.ShouldThrow<ArgumentNullException>(reason, reasonArgs);
        }
        #endregion

        #region ShouldThrowArgumentOutOfRangeException

        /// <summary>Asserts that an action throws an argument out of range exception.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The assertion.</returns>
        public static ExceptionAssertions<ArgumentOutOfRangeException> ShouldThrowArgumentOutOfRangeException ( this Action source )
        {
            return source.ShouldThrow<ArgumentOutOfRangeException>();
        }

        /// <summary>Asserts that an action throws an argument out of range exception.</summary>
        /// <param name="source">The source.</param>
        /// <param name="reason">The reason message.</param>
        /// <returns>The assertion.</returns>
        public static ExceptionAssertions<ArgumentOutOfRangeException> ShouldThrowArgumentOutOfRangeException ( this Action source, string reason )
        {
            return source.ShouldThrow<ArgumentOutOfRangeException>(reason);
        }

        /// <summary>Asserts that an action throws an argument out of range exception.</summary>
        /// <param name="source">The source.</param>
        /// <param name="reason">The reason message.</param>
        /// <param name="reasonArgs">The reason arguments.</param>
        /// <returns>The assertion.</returns>
        public static ExceptionAssertions<ArgumentOutOfRangeException> ShouldThrowArgumentOutOfRangeException ( this Action source, string reason, params object[] reasonArgs )
        {
            return source.ShouldThrow<ArgumentOutOfRangeException>(reason, reasonArgs);
        }
        #endregion
    }
}
