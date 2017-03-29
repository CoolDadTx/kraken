/*
 * Copyright © 2013 Michael Taylor
 * All Rights Reserved
 * 
 * From code copyright Federation of State Medical Boards
 */
using System;
using System.Diagnostics.CodeAnalysis;

namespace P3Net.Kraken
{
    /// <summary>Represents the month part of a date.</summary>
    /// <remarks>
    /// This type supports the standard <see cref="DateTime"/> range and not the range defined by the current calendar.
    /// </remarks>
    /// <seealso cref="Dates"/>
    public struct MonthPart : IEquatable<MonthPart>
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="MonthPart"/> structure.</summary>
        /// <param name="month">The month (1 - 12).</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="month"/> is less than 1 or greater than 12.</exception>
        public MonthPart ( int month )
        {
            DateVerification.ValidateMonth(month, "month");

            m_month = month;
        }
        #endregion

        #region Public Members

        /// <summary>Gets the month number.</summary>
        public int Month 
        {
            get { return m_month; }
        }

        /// <summary>Gets a month and year part for a date.</summary>
        /// <param name="year">The year.</param>
        /// <returns>The month and year part.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        public MonthYearPart OfYear ( int year )
        {
            DateVerification.ValidateYear(year, "year");

            return new MonthYearPart(Month, year);
        }        
        #endregion

        #region Infrastructure

        /// <summary>Determines if two objects are equal.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if they are equal.</returns>
        public static bool operator== ( MonthPart left, MonthPart right )
        {
            return left.Equals(right);
        }

        /// <summary>Determines if two objects are equal.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if they are not equal.</returns>
        public static bool operator != ( MonthPart left, MonthPart right )
        {
            return !left.Equals(right);
        }

        /// <summary>Determines if two instances are equal.</summary>
        /// <param name="obj">The value.</param>
        /// <returns><see langword="true"/> if they are equal.</returns>
        public override bool Equals ( object obj )
        {            
            if (!(obj is MonthPart))
                return false;

            return Equals((MonthPart)obj);
        }

        /// <summary>Gets the hash code.</summary>
        /// <returns>The hash code.</returns>
        [ExcludeFromCodeCoverage]
        public override int GetHashCode ()
        {
            return m_month.GetHashCode();
        }

        /// <summary>Gets the string representation.</summary>
        /// <returns>A string.</returns>
        [ExcludeFromCodeCoverage]
        public override string ToString ()
        {
            return m_month.ToString();
        }
        #endregion

        #region IEquatable<MonthPart> Members

        /// <summary>Determines if two instances are equal.</summary>
        /// <param name="other">The value.</param>
        /// <returns><see langword="true"/> if they are equal.</returns>
        public bool Equals ( MonthPart other )
        {
            return m_month == other.m_month;
        }
        #endregion

        #region Private Members

        private readonly int m_month;
        #endregion        
    }
}
