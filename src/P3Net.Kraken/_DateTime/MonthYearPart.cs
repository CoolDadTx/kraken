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
    /// <summary>Represents a month and year part of a date.</summary>
    /// <remarks>
    /// This type supports the standard <see cref="DateTime"/> range and not the range defined by the current calendar.
    /// </remarks>
    /// <seealso cref="Dates"/>
    public struct MonthYearPart : IEquatable<MonthYearPart>
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="MonthYearPart"/> structure.</summary>
        /// <param name="month">The month number (1 - 12).</param>
        /// <param name="year">The year.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="month"/> is less than 1 or greater than 12.
        /// <para>-or-</para>
        /// <paramref name="year"/> is less than 1 or greater than 9999.
        /// </exception>
        public MonthYearPart ( int month, int year )
        {
            DateVerification.ValidateMonth(month, "month");
            DateVerification.ValidateYear(year, "year");

            m_value = (month & 0x000000FF) << 16 | (year & 0xFFFF);
        }
        #endregion

        #region Public Members

        #region Attributes
        
        /// <summary>Gets the month number.</summary>
        public int Month 
        {
            get { return (m_value >> 16) & 0x000000FF; }
        }

        /// <summary>Gets the year.</summary>
        public int Year 
        {
            get { return m_value & 0x0000FFFF; }
        }
        #endregion

        /// <summary>Creates a date given a day.</summary>
        /// <param name="value">The day number.</param>
        /// <returns>The new date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is less than 1 or greater than the number of days in the month.</exception>
        public Date Day ( int value )
        {
            return new Date(Year, Month, value);
        }

        /// <summary>Gets the first day of the month.</summary>
        /// <returns>The date.</returns>
        public Date FirstDayOfMonth ( )
        {
            return new Date(Year, Month, 1);
        }

        /// <summary>Gets the last day of the month.</summary>
        /// <returns>The date.</returns>
        public Date LastDayOfMonth ( )
        {
            return new Date(Year, Month, 1).LastDayOfMonth();
        }
        #endregion

        #region Infrastructure

        /// <summary>Determines if two objects are equal.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if they are equal.</returns>
        public static bool operator == ( MonthYearPart left, MonthYearPart right )
        {
            return left.Equals(right);
        }

        /// <summary>Determines if two objects are equal.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if they are not equal.</returns>
        public static bool operator != ( MonthYearPart left, MonthYearPart right )
        {
            return !left.Equals(right);
        }

        /// <summary>Determines if two instances are equal.</summary>
        /// <param name="obj">The value.</param>
        /// <returns><see langword="true"/> if they are equal.</returns>
        public override bool Equals ( object obj )
        {
            if (!(obj is MonthYearPart))
                return false;

            return Equals((MonthYearPart)obj);
        }

        /// <summary>Gets the hash code.</summary>
        /// <returns>The hash code.</returns>
        [ExcludeFromCodeCoverage]
        public override int GetHashCode ()
        {
            return m_value.GetHashCode();
        }

        /// <summary>Gets the string representation.</summary>
        /// <returns>A string.</returns>
        [ExcludeFromCodeCoverage]
        public override string ToString ()
        {
            return m_value.ToString();
        }
        #endregion

        #region IEquatable<MonthYearPart> Members

        /// <summary>Determines if two instances are equal.</summary>
        /// <param name="other">The value.</param>
        /// <returns><see langword="true"/> if they are equal.</returns>
        public bool Equals ( MonthYearPart other )
        {
            return m_value == other.m_value;
        }
        #endregion

        #region Private Members

        // 00MMYYYY where each part is in hex
        private readonly int m_value;
        #endregion
    }
}