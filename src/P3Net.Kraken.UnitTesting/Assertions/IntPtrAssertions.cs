/*
 * Copyright © Michael Taylor
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace P3Net.Kraken.UnitTesting
{
    /// <summary>Provides assertions for <see cref="IntPtr"/>.</summary>
    public class IntPtrAssertions
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="IntPtrAssertions"/> class.</summary>
        /// <param name="value">The value.</param>
        protected internal IntPtrAssertions ( IntPtr value )
        {
            Subject = value;
        }
        #endregion

        #region Public Members

        #region Attributes

        /// <summary>Gets the subject of the assertion.</summary>
        public IntPtr Subject { get; private set; }
        #endregion

        #region Methods

        #region Be
        
        /// <summary>Asserts that the pointer is a specific value.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The new constraint.</returns>
        public AndConstraint<IntPtrAssertions> Be ( IntPtr value )
        {
            return Subject.Should().Be(value.ToInt64(), "");
        }

        /// <summary>Asserts that the pointer is a specific value.</summary>
        /// <param name="value">The value.</param>
        /// <param name="reason">The reason for the failure.</param>
        /// <param name="reasonArgs">The optional reason arguments.</param>
        /// <returns>The new constraint.</returns>
        public AndConstraint<IntPtrAssertions> Be ( IntPtr value, string reason, params object[] reasonArgs )
        {
            return Subject.Should().Be(value.ToInt64(), reason, reasonArgs);
        }

        /// <summary>Asserts that the pointer is a specific value.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The new constraint.</returns>
        public AndConstraint<IntPtrAssertions> Be ( int value )
        {
            return Be(value, "");
        }

        /// <summary>Asserts that the pointer is a specific value.</summary>
        /// <param name="value">The value.</param>
        /// <param name="reason">The reason for the failure.</param>
        /// <param name="reasonArgs">The optional reason arguments.</param>
        /// <returns>The new constraint.</returns>
        public AndConstraint<IntPtrAssertions> Be ( int value, string reason, params object[] reasonArgs )
        {
            Execute.Assertion.ForCondition(Subject.ToInt64() == value).BecauseOf(reason, reasonArgs).FailWith("Expected {0}{reason}, but found {1}.", new object[] { value, this.Subject });
            return new AndConstraint<IntPtrAssertions>(this);
        }

        /// <summary>Asserts that the pointer is a specific value.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The new constraint.</returns>
        public AndConstraint<IntPtrAssertions> Be ( long value )
        {
            return Be(value, "");
        }

        /// <summary>Asserts that the pointer is a specific value.</summary>
        /// <param name="value">The value.</param>
        /// <param name="reason">The reason for the failure.</param>
        /// <param name="reasonArgs">The optional reason arguments.</param>
        /// <returns>The new constraint.</returns>
        public AndConstraint<IntPtrAssertions> Be ( long value, string reason, params object[] reasonArgs )
        {
            Execute.Assertion.ForCondition(Subject.ToInt64() == value).BecauseOf(reason, reasonArgs).FailWith("Expected {0}{reason}, but found {1}.", new object[] { value, this.Subject });
            return new AndConstraint<IntPtrAssertions>(this);
        }
        #endregion

        #region NotBZero

        /// <summary>Asserts that the pointer is not zero.</summary>
        /// <returns>The new constraint.</returns>
        public AndConstraint<IntPtrAssertions> NotBeZero ()
        {
            return Subject.Should().NotBeZero("");
        }

        /// <summary>Asserts that the pointer is not zero.</summary>
        /// <param name="reason">The reason for the failure.</param>
        /// <param name="reasonArgs">The optional reason arguments.</param>
        /// <returns>The new constraint.</returns>
        public AndConstraint<IntPtrAssertions> NotBeZero ( string reason, params object[] reasonArgs )
        {
            Execute.Assertion.ForCondition(Subject != IntPtr.Zero).BecauseOf(reason, reasonArgs).FailWith("Expected not zero {reason}, but found zero.");
            return new AndConstraint<IntPtrAssertions>(this);
        }
        #endregion

        #region BeZero
        
        /// <summary>Asserts that the pointer is zero.</summary>
        /// <returns>The new constraint.</returns>
        public AndConstraint<IntPtrAssertions> BeZero ()
        {
            return Subject.Should().Be(IntPtr.Zero);
        }

        /// <summary>Asserts that the pointer is zero.</summary>
        /// <param name="reason">The reason for the failure.</param>
        /// <param name="reasonArgs">The optional reason arguments.</param>
        /// <returns>The new constraint.</returns>
        public AndConstraint<IntPtrAssertions> BeZero ( string reason, params object[] reasonArgs )
        {
            return Subject.Should().Be(IntPtr.Zero, reason, reasonArgs);
        }
        #endregion

        #endregion

        #endregion
    }
}
