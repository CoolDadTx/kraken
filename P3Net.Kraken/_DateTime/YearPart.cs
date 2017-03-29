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
    /// <summary>Represents a year part of a date.</summary>
    /// <remarks>
    /// This type supports the standard <see cref="DateTime"/> range and not the range defined by the current calendar.
    /// </remarks>
    /// <seealso cref="Dates"/>
    public struct YearPart : IEquatable<YearPart>
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="YearPart"/> structure.</summary>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        public YearPart ( int year ) 
        {
            DateVerification.ValidateYear(year, "year");

            m_value = year;
        }
        #endregion

        #region Public Members

        #region Attributes
        
        /// <summary>Gets the year value.</summary>
        public int Year 
        {
            get { return m_value; }
        }
        #endregion

        /// <summary>Gets a date given the day of the year.</summary>
        /// <param name="value">The day of the year.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is less than 1 or greater than the number of days in the year.</exception>
        public Date DayOfYear ( int value )
        {
            return Date.FromDayOfYear(Year, value);
        }

        /// <summary>Gets the first day of the month.</summary>
        /// <param name="month">The month.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="month"/> is less than 1 or greater than 12.</exception>
        public Date FirstDayOfMonth ( int month )
        {
            return Month(month).FirstDayOfMonth();
        }

        /// <summary>Gets the last day of the month.</summary>
        /// <param name="month">The month.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="month"/> is less than 1 or greater than 12.</exception>
        public Date LastDayOfMonth ( int month )
        {
            return Month(month).LastDayOfMonth();
        }

        /// <summary>Gets a month of the year.</summary>
        /// <param name="value">The month.</param>
        /// <returns>The month and year part.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is less than 1 or greater than 12.</exception>
        public MonthYearPart Month ( int value )
        {
            DateVerification.ValidateMonth(value, "value");

            return new MonthYearPart(value, Year);
        }        
        
        #region Named Months to MonthYear

        /// <summary>January of the year.</summary>
        /// <returns>The month and year part.</returns>
        public MonthYearPart January ( )
        {
            return new MonthYearPart(Months.January, Year);
        }

        /// <summary>Gets a day in January of the year.</summary>
        /// <param name="day">The day.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="day"/> is less than 1 or greater than the number of days in the month.</exception>
        public Date January ( int day )
        {
            return new Date(Year, Months.January, day);
        }

        /// <summary>February of the year.</summary>
        /// <returns>The month and year part.</returns>
        public MonthYearPart February ( )
        {
            return new MonthYearPart(Months.February, Year);
        }

        /// <summary>Gets a day in February of the year.</summary>
        /// <param name="day">The day.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="day"/> is less than 1 or greater than the number of days in the month.</exception>
        public Date February ( int day )
        {
            return new Date(Year, Months.February, day);
        }

        /// <summary>March of the year.</summary>
        /// <returns>The month and year part.</returns>
        public MonthYearPart March ( )
        {
            return new MonthYearPart(Months.March, Year);
        }

        /// <summary>Gets a day in March of the year.</summary>
        /// <param name="day">The day.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="day"/> is less than 1 or greater than the number of days in the month.</exception>
        public Date March ( int day )
        {
            return new Date(Year, Months.March, day);
        }

        /// <summary>April of the year.</summary>
        /// <returns>The month and year part.</returns>
        public MonthYearPart April ( )
        {
            return new MonthYearPart(Months.April, Year);
        }

        /// <summary>Gets a day in April of the year.</summary>
        /// <param name="day">The day.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="day"/> is less than 1 or greater than the number of days in the month.</exception>
        public Date April ( int day )
        {
            return new Date(Year, Months.April, day);
        }

        /// <summary>May of the year.</summary>
        /// <returns>The month and year part.</returns>
        public MonthYearPart May ( )
        {
            return new MonthYearPart(Months.May, Year);
        }

        /// <summary>Gets a day in May of the year.</summary>
        /// <param name="day">The day.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="day"/> is less than 1 or greater than the number of days in the month.</exception>
        public Date May ( int day )
        {
            return new Date(Year, Months.May, day);
        }

        /// <summary>June of the year.</summary>
        /// <returns>The month and year part.</returns>
        public MonthYearPart June ( )
        {
            return new MonthYearPart(Months.June, Year);
        }

        /// <summary>Gets a day in June of the year.</summary>
        /// <param name="day">The day.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="day"/> is less than 1 or greater than the number of days in the month.</exception>
        public Date June ( int day )
        {
            return new Date(Year, Months.June, day);
        }

        /// <summary>July of the year.</summary>
        /// <returns>The month and year part.</returns>
        public MonthYearPart July ( )
        {
            return new MonthYearPart(Months.July, Year);
        }

        /// <summary>Gets a day in July of the year.</summary>
        /// <param name="day">The day.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="day"/> is less than 1 or greater than the number of days in the month.</exception>
        public Date July ( int day )
        {
            return new Date(Year, Months.July, day);
        }

        /// <summary>August of the year.</summary>
        /// <returns>The month and year part.</returns>
        public MonthYearPart August ( )
        {
            return new MonthYearPart(Months.August, Year);
        }

        /// <summary>Gets a day in August of the year.</summary>
        /// <param name="day">The day.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="day"/> is less than 1 or greater than the number of days in the month.</exception>
        public Date August ( int day )
        {
            return new Date(Year, Months.August, day);
        }

        /// <summary>September of the year.</summary>
        /// <returns>The month and year part.</returns>
        public MonthYearPart September ( )
        {
            return new MonthYearPart(Months.September, Year);
        }

        /// <summary>Gets a day in September of the year.</summary>
        /// <param name="day">The day.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="day"/> is less than 1 or greater than the number of days in the month.</exception>
        public Date September ( int day )
        {
            return new Date(Year, Months.September, day);
        }

        /// <summary>October of the year.</summary>
        /// <returns>The month and year part.</returns>
        public MonthYearPart October ( )
        {
            return new MonthYearPart(Months.October, Year);
        }

        /// <summary>Gets a day in October of the year.</summary>
        /// <param name="day">The day.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="day"/> is less than 1 or greater than the number of days in the month.</exception>
        public Date October ( int day )
        {
            return new Date(Year, Months.October, day);
        }

        /// <summary>November of the year.</summary>
        /// <returns>The month and year part.</returns>
        public MonthYearPart November ( )
        {
            return new MonthYearPart(Months.November, Year);
        }

        /// <summary>Gets a day in November of the year.</summary>
        /// <param name="day">The day.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="day"/> is less than 1 or greater than the number of days in the month.</exception>
        public Date November ( int day )
        {
            return new Date(Year, Months.November, day);
        }

        /// <summary>December of the year.</summary>
        /// <returns>The month and year part.</returns>
        public MonthYearPart December ( )
        {
            return new MonthYearPart(Months.December, Year);
        }

        /// <summary>Gets a day in December of the year.</summary>
        /// <param name="day">The day.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="day"/> is less than 1 or greater than the number of days in the month.</exception>
        public Date December ( int day )
        {
            return new Date(Year, Months.December, day);
        }
        #endregion

        #endregion

        #region Infrastructure

        /// <summary>Determines if two objects are equal.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if they are equal.</returns>
        public static bool operator == ( YearPart left, YearPart right )
        {
            return left.Equals(right);
        }

        /// <summary>Determines if two objects are equal.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if they are not equal.</returns>
        public static bool operator != ( YearPart left, YearPart right )
        {
            return !left.Equals(right);
        }

        /// <summary>Determines if two instances are equal.</summary>
        /// <param name="obj">The value.</param>
        /// <returns><see langword="true"/> if they are equal.</returns>
        public override bool Equals ( object obj )
        {
            if (!(obj is YearPart))
                return false;

            return Equals((YearPart)obj);
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

        #region IEquatable<YearPart> Members

        /// <summary>Determines if two instances are equal.</summary>
        /// <param name="other">The value.</param>
        /// <returns><see langword="true"/> if they are equal.</returns>
        public bool Equals ( YearPart other )
        {
            return m_value == other.m_value;
        }
        #endregion

        #region Private Members

        private readonly int m_value;
        #endregion
    }
}