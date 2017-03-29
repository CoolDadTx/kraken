/*
 * Copyright © 2013 Michael Taylor
 * All Rights Reserved
 * 
 * From code copyright Federation of State Medical Boards
 */
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken
{
    /// <summary>Represents a date (without a time component).</summary>
    [Serializable]
    [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Date", Justification="Name makes the most sense.")]    
    public struct Date : IComparable<Date>, IComparable<DateTime>, IEquatable<Date>, IEquatable<DateTime>, IFormattable, IComparable
    {
        #region Constructors

        /// <summary>Initializes an instance of the <see cref="Date"/> structure.</summary>
        /// <param name="value">The date/time to initialize with.</param>
        public Date ( DateTime value )
        {
            m_dt = value.Date;
        }

        /// <summary>Initializes an instance of the <see cref="Date"/> structure.</summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="year"/> is less than 1 or greater than 9999.
        /// <para>-or-</para>
        /// <paramref name="month"/> is less than 1 or greater than 12.
        /// <para>-or-</para>
        /// <paramref name="day"/> is less than 1 or greater than the number of days in the month.
        /// </exception>
        public Date ( int year, int month, int day )
        {
            m_dt = new DateTime(year, month, day);
        }
                
        /// <summary>Converts from a <see cref="Date"/> to a <see cref="DateTime"/>.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The converted value.</returns>
        public static implicit operator DateTime ( Date value )
        {
            return value.m_dt;
        }

        /// <summary>Converts from a <see cref="DateTime"/> to a <see cref="Date"/>.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The converted value.</returns>
        public static explicit operator Date ( DateTime value )
        {
            return new Date(value);
        }         
   
        /// <summary>Creates a date given a year and day of year.</summary>
        /// <param name="year">The year.</param>
        /// <param name="dayOfYear">The day of the year.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="dayOfYear"/> is less than 1 or greater than the number of days in the year.</exception>
        public static Date FromDayOfYear ( int year, int dayOfYear )
        {
            Verify.Argument("dayOfYear", dayOfYear).IsGreaterThanZero();
            var dt = new Date(year, 1, 1).AddDays(dayOfYear - 1);

            if (dt.Year != year)
                throw new ArgumentOutOfRangeException("dayOfYear", "Day of year is less than 1 or greater than the number of days in the year.");

            return dt;
        }
        #endregion

        #region Attributes

        /// <summary>Represents the largest date.</summary>
        public static readonly Date MaxValue = new Date(DateTime.MaxValue);

        /// <summary>Represents the smallest date.</summary>
        public static readonly Date MinValue = new Date(DateTime.MinValue);

        /// <summary>Represents no date.</summary>
        public static readonly Date None = new Date(DateTime.MinValue);

        /// <summary>Gets the day.</summary>
        public int Day
        {
            get { return m_dt.Day; }
        }
                        
        /// <summary>Gets the day of the week.</summary>
        public DayOfWeek DayOfWeek
        {
            get { return m_dt.DayOfWeek; }
        }

        /// <summary>Gets the day of the year.</summary>
        public int DayOfYear
        {
            get { return m_dt.DayOfYear; }
        }

        /// <summary>Determines if the date is a leap date.</summary>
        public bool IsLeapDay
        {
            get { return IsLeapYear && Month == Months.February && Day == 29; }
        }

        /// <summary>Determines if the date is in a leap year.</summary>
        public bool IsLeapYear
        {
            get { return DateTime.IsLeapYear(Year); }         
        }

        /// <summary>Determines if a date has been set.</summary>
        /// <returns><see langword="true"/> if the date is not <see cref="Date.None"/>.</returns>
        public bool IsSet
        {
            get { return this != Date.None; }
        }

        /// <summary>Gets the month.</summary>
        public int Month
        {
            get { return m_dt.Month; }
        }

        /// <summary>Get the year.</summary>
        public int Year
        {
            get { return m_dt.Year; }
        }        
        #endregion

        #region Methods

        /// <summary>Adds a number of days to a date.</summary>
        /// <param name="value">The number of days (can be positive or negative).</param>
        /// <returns>The new date.</returns>
        public Date AddDays ( int value )
        {
            return new Date(m_dt.AddDays(value));
        }

        /// <summary>Adds a number of months to a date.</summary>
        /// <param name="value">The number of months (can be positive or negative).</param>
        /// <returns>The new date.</returns>
        public Date AddMonths ( int value )
        {
            return new Date(m_dt.AddMonths(value));
        }

        /// <summary>Adds a number of weeks to a date.</summary>
        /// <param name="value">The number of weeks (can be positive or negative).</param>
        /// <returns>The new date.</returns>
        public Date AddWeeks ( int value )
        {
            return AddDays(value * 7);
        }

        /// <summary>Adds a number of years to a date.</summary>
        /// <param name="value">The number of years (can be positive or negative).</param>
        /// <returns>The new date.</returns>
        public Date AddYears ( int value )
        {
            return new Date(m_dt.AddYears(value));
        }
        
        /// <summary>Adds time to the date.</summary>
        /// <param name="time">The time.</param>
        /// <returns>The new date/time.</returns>
        public DateTime At ( TimeSpan time )
        {
            return m_dt.Add(time);
        }

        /// <summary>Adds time to the date.</summary>
        /// <param name="hours">The hour.</param>
        /// <param name="minutes">The minutes.</param>
        /// <param name="seconds">The seconds.</param>
        /// <returns>The new date/time.</returns>
        public DateTime At ( int hours, int minutes, int seconds )
        {
            return m_dt.Add(new TimeSpan(hours, minutes, seconds));
        }        
        
        #region Difference

        /// <summary>Determines the difference, in days, between two dates.</summary>
        /// <param name="value">The value to compare against.</param>
        /// <returns>The number of days between two dates.  Can be positive or negative.</returns>
        /// <seealso cref="O:DifferenceAbsolute"/>
        public int Difference ( Date value )
        {
            var diff = value.m_dt - m_dt;

            return (int)diff.TotalDays;
        }

        /// <summary>Determines the difference, in days, between two dates.</summary>
        /// <param name="value">The value to compare against.</param>
        /// <returns>The number of days between two dates.  Can be positive or negative.</returns>
        /// <seealso cref="O:DifferenceAbsolute"/>
        public int Difference ( DateTime value )
        {
            var diff = value.ToDate().m_dt - m_dt;

            return (int)diff.TotalDays;
        }
        #endregion

        #region DifferenceAbsolute

        /// <summary>Determines the number of days between two dates.</summary>
        /// <param name="value">The value to compare against.</param>
        /// <returns>The number of days between two dates.  The number is always positive irrelevant of the order.</returns>
        /// <seealso cref="O:Difference"/>
        public int DifferenceAbsolute ( Date value )
        {
            return Math.Abs(Difference(value));
        }

        /// <summary>Determines the number of days between two dates.</summary>
        /// <param name="value">The value to compare against.</param>
        /// <returns>The number of days between two dates.  The number is always positive irrelevant of the order.</returns>
        /// <seealso cref="O:Difference"/>
        public int DifferenceAbsolute ( DateTime value )
        {
            return Math.Abs(Difference(value));
        }
        #endregion

        /// <summary>Gets the first day of the month.</summary>
        /// <returns>The date.</returns>
        public Date FirstDayOfMonth ( )
        {
            return new Date(Year, Month, 1);
        }

        #region IsBetween

        /// <summary>Determines if the date falls between two dates, inclusive.</summary>
        /// <param name="left">The start date.</param>
        /// <param name="right">The end date.</param>
        /// <returns><see langword="true"/> if the date is on or between the two dates.</returns>
        public bool IsBetween ( Date left, Date right )
        {
            return (this >= left) && (this <= right);
        }

        /// <summary>Determines if the date falls between two dates, inclusive.</summary>
        /// <param name="left">The start date.</param>
        /// <param name="right">The end date.</param>
        /// <returns><see langword="true"/> if the date is on or between the two dates.</returns>
        public bool IsBetween ( DateTime left, Date right )
        {
            return (this >= left) && (this <= right);
        }

        /// <summary>Determines if the date falls between two dates, inclusive.</summary>
        /// <param name="left">The start date.</param>
        /// <param name="right">The end date.</param>
        /// <returns><see langword="true"/> if the date is on or between the two dates.</returns>
        public bool IsBetween ( Date left, DateTime right )
        {
            return (this >= left) && (this <= right);
        }

        /// <summary>Determines if the date falls between two dates, inclusive.</summary>
        /// <param name="left">The start date.</param>
        /// <param name="right">The end date.</param>
        /// <returns><see langword="true"/> if the date is on or between the two dates.</returns>
        public bool IsBetween ( DateTime left, DateTime right )
        {
            return (this >= left) && (this <= right);
        }
        #endregion

        /// <summary>Gets the last day of the month.</summary>
        /// <returns>The date.</returns>
        public Date LastDayOfMonth ( )
        {
            return new Date(Year, Month, DateTime.DaysInMonth(Year, Month));
        }

        /// <summary>Gets the previous month.</summary>
        /// <returns>The new date.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The value is too small.</exception>
        public Date LastMonth ( )
        {
            return AddMonths(-1);
        }

        /// <summary>Gets the next month.</summary>
        /// <returns>The new date.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The value is too large.</exception>
        public Date NextMonth ( )
        {
            return AddMonths(1);
        }        

        #region Parse

        /// <summary>Parses a date string.</summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>The date.</returns>
        /// <remarks>
        /// Any time component of the string is ignored.
        /// </remarks>
        public static Date Parse ( string value )
        {
            return new Date(DateTime.Parse(value));
        }

        /// <summary>Parses a date string.</summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>The date.</returns>
        /// <remarks>
        /// Any time component of the string is ignored.
        /// </remarks>
        public static Date Parse ( string value, IFormatProvider formatProvider )
        {
            return new Date(DateTime.Parse(value, formatProvider));
        }

        /// <summary>Parses a date string.</summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="styles">The styles to use.</param>
        /// <returns>The date.</returns>
        /// <remarks>
        /// Any time component of the string is ignored.
        /// </remarks>
        public static Date Parse ( string value, IFormatProvider formatProvider, System.Globalization.DateTimeStyles styles )
        {
            return new Date(DateTime.Parse(value, formatProvider, styles));
        }
        #endregion

        #region ParseExact

        /// <summary>Parses a date string.</summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="format">The format string.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>The date.</returns>
        /// <remarks>
        /// Any time component of the string is ignored.
        /// </remarks>
        public static Date ParseExact ( string value, string format, IFormatProvider formatProvider )
        {
            return new Date(DateTime.ParseExact(value, format, formatProvider));
        }

        /// <summary>Parses a date string.</summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="formats">The format strings to use.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="styles">The styles to use.</param>
        /// <returns>The date.</returns>
        /// <remarks>
        /// Any time component of the string is ignored.
        /// </remarks>
        public static Date ParseExact ( string value, string[] formats, IFormatProvider formatProvider, DateTimeStyles styles )
        {
            return new Date(DateTime.ParseExact(value, formats, formatProvider, styles));
        }

        /// <summary>Parses a date string.</summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="format">The format string.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="styles">The styles to use.</param>
        /// <returns>The date.</returns>
        /// <remarks>
        /// Any time component of the string is ignored.
        /// </remarks>
        public static Date ParseExact ( string value, string format, IFormatProvider formatProvider, DateTimeStyles styles )
        {
            return new Date(DateTime.ParseExact(value, format, formatProvider, styles));
        }
        #endregion        

        /// <summary>Gets today's date.</summary>
        /// <returns>Today's date.</returns>
        public static Date Today ()
        {
            return DateTime.Today.ToDate();
        }

        /// <summary>Gets the day after the date.</summary>
        /// <returns>The new date.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The value is too large.</exception>
        public Date Tomorrow ()
        {
            return AddDays(1);
        }

        /// <summary>Converts the date to a nullable <see cref="DateTime"/>.</summary>
        /// <returns>The <see cref="DateTime"/> if the date is set or <see langword="null"/> otherwise.</returns>
        public DateTime? ToNullableDateTime ()
        {
            return IsSet ? this : (DateTime?)null;
        }

        #region ToString

        /// <summary>Gets the long string format of the date.</summary>
        /// <returns>The string.</returns>        
        public string ToLongDateString ()
        {
            return m_dt.ToLongDateString();
        }

        /// <summary>Gets the short string format of the date.</summary>
        /// <returns>The string.</returns>
        public string ToShortDateString ()
        {
            return m_dt.ToShortDateString();
        }

        /// <summary>Gets the string representation of the object.</summary>
        /// <returns>The string.</returns>
        public override string ToString ()
        {
            return ToLongDateString();
        }
        #endregion

        #region TryParse

        /// <summary>Parses a date string.</summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="result">The result.</param>
        /// <returns><see langword="true"/> if successful.</returns>
        /// <remarks>
        /// Any time component of the string is ignored.
        /// </remarks>
        public static bool TryParse ( string value, out Date result )
        {
            DateTime output;
            if (DateTime.TryParse(value, out output))
            {
                result = new Date(output);
                return true;
            };

            result = Date.None;
            return false;
        }

        /// <summary>Parses a date string.</summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="styles">The styles to use.</param>
        /// <param name="result">The result.</param>
        /// <returns><see langword="true"/> if successful.</returns>
        /// <remarks>
        /// Any time component of the string is ignored.
        /// </remarks>
        public static bool TryParse ( string value, IFormatProvider formatProvider, DateTimeStyles styles, out Date result )
        {
            DateTime output;
            if (DateTime.TryParse(value, formatProvider, styles, out output))
            {
                result = new Date(output);
                return true;
            };

            result = Date.None;
            return false;
        }
        #endregion

        #region TryParseExact

        /// <summary>Parses a date string.</summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="format">The format string.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="styles">The styles to use.</param>
        /// <param name="result">The result.</param>
        /// <returns><see langword="true"/> if successful.</returns>
        /// <remarks>
        /// Any time component of the string is ignored.
        /// </remarks>
        public static bool TryParseExact ( string value, string format, IFormatProvider formatProvider, DateTimeStyles styles, out Date result )
        {
            DateTime output;
            if (DateTime.TryParseExact(value, format, formatProvider, styles, out output))
            {
                result = new Date(output);
                return true;
            };

            result = Date.None;
            return false;
        }

        /// <summary>Parses a date string.</summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="formats">The format strings.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="styles">The styles to use.</param>
        /// <param name="result">The result.</param>
        /// <returns><see langword="true"/> if successful.</returns>
        /// <remarks>
        /// Any time component of the string is ignored.
        /// </remarks>
        public static bool TryParseExact ( string value, string[] formats, IFormatProvider formatProvider, DateTimeStyles styles, out Date result )
        {
            DateTime output;
            if (DateTime.TryParseExact(value, formats, formatProvider, styles, out output))
            {
                result = new Date(output);
                return true;
            };

            result = Date.None;return false;
        }
        #endregion

        /// <summary>Gets the day before the date.</summary>
        /// <returns>The new date.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The value is too small.</exception>
        public Date Yesterday ()
        {
            return AddDays(-1);
        }
        #endregion

        #region Infrastructure

        /// <summary>Gets the hash code.</summary>
        /// <returns>The hash code.</returns>
        [ExcludeFromCodeCoverage]
        public override int GetHashCode ()
        {
            return m_dt.GetHashCode();
        }

        /// <summary>Determines if two objects are equal.</summary>
        /// <param name="obj">The value to compare against.</param>
        /// <returns><see langword="true"/> if the objects are equal.</returns>
        public override bool Equals ( object obj )
        {
            if (!(obj is Date))
                return false;

            return base.Equals((Date)obj);
        }        
        #endregion

        #region IComparable<Date> Members

        /// <summary>Compares two dates for ordering.</summary>
        /// <param name="other">The value to compare against.</param>
        /// <returns>The comparison result.</returns>
        public int CompareTo ( Date other )
        {
            return m_dt.CompareTo(other.m_dt);
        }

        /// <summary>Compares two dates for ordering.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> is greater than <paramref name="right"/>.</returns>
        public static bool operator > ( Date left, Date right )
        {
            return left.m_dt > right.m_dt;
        }

        /// <summary>Compares two dates for ordering.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref name="right"/>.</returns>
        public static bool operator >= ( Date left, Date right )
        {
            return left.m_dt >= right.m_dt;
        }

        /// <summary>Compares two dates for ordering.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>.</returns>
        public static bool operator < ( Date left, Date right )
        {
            return left.m_dt < right.m_dt;
        }

        /// <summary>Compares two dates for ordering.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref name="right"/>.</returns>
        public static bool operator <= ( Date left, Date right )
        {
            return left.m_dt <= right.m_dt;
        }
        #endregion

        #region IComparable<DateTime> Members

        /// <summary>Compares two dates for ordering.</summary>
        /// <param name="other">The value to compare against.</param>
        /// <returns>The comparison result.</returns>
        public int CompareTo ( DateTime other )
        {
            return m_dt.CompareTo(other.Date);
        }

        /// <summary>Compares a date and date/time for ordering.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> is greater than <paramref name="right"/>.</returns>
        public static bool operator > ( Date left, DateTime right )
        {
            return left.m_dt > right.Date;
        }

        /// <summary>Compares a date and date/time for ordering.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref name="right"/>.</returns>
        public static bool operator >= ( Date left, DateTime right )
        {
            return left.m_dt >= right.Date;
        }

        /// <summary>Compares a date and date/time for ordering.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> is greater than <paramref name="right"/>.</returns>
        public static bool operator > ( DateTime left, Date right )
        {
            return left.Date > right.m_dt;
        }

        /// <summary>Compares a date and date/time for ordering.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref name="right"/>.</returns>
        public static bool operator >= ( DateTime left, Date right )
        {
            return left.Date >= right.m_dt;
        }

        /// <summary>Compares a date and date/time for ordering.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>.</returns>
        public static bool operator < ( Date left, DateTime right )
        {
            return left.m_dt < right.Date;
        }

        /// <summary>Compares a date and date/time for ordering.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref name="right"/>.</returns>
        public static bool operator <= ( Date left, DateTime right )
        {
            return left.m_dt <= right.Date;
        }

        /// <summary>Compares a date and date/time for ordering.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>.</returns>
        public static bool operator < ( DateTime left, Date right )
        {
            return left.Date < right.m_dt;
        }

        /// <summary>Compares a date and date/time for ordering.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref name="right"/>.</returns>
        public static bool operator <= ( DateTime left, Date right )
        {
            return left.Date <= right.m_dt;
        }
        #endregion

        #region IComparable Members

        int IComparable.CompareTo ( object obj )
        {
            if (obj == null)
                return 1;

            if (!(obj is Date))
                throw new ArgumentException("Object must be Date.");

            return CompareTo((Date)obj);
        }
        #endregion

        #region IEquatable<Date> Members

        /// <summary>Determines if two objects are equal.</summary>
        /// <param name="other">The value to compare against.</param>
        /// <returns><see langword="true"/> if the objects are equal.</returns>
        public bool Equals ( Date other )
        {
            return m_dt.Equals(other.m_dt);
        }

        /// <summary>Determines if two objects are equal.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if the objects are equal.</returns>
        public static bool operator== ( Date left, Date right )
        {
            return left.m_dt == right.m_dt;
        }

        /// <summary>Determines if two objects are not equal.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if the objects are not equal.</returns>
        public static bool operator!= ( Date left, Date right )
        {
            return left.m_dt != right.m_dt;
        }        
        #endregion

        #region IEquatable<DateTime> Members

        /// <summary>Determines if a date is equal to a date and time.</summary>
        /// <param name="other">The value to compare against.</param>
        /// <returns><see langword="true"/> if the objects are equal.</returns>
        /// <remarks>
        /// Only the date portion of the date/time is taken into account.
        /// </remarks>
        public bool Equals ( DateTime other )
        {
            return m_dt.Date == other.Date;
        }

        /// <summary>Determines if a date is equal to a date and time.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if the objects are equal.</returns>
        /// <remarks>
        /// Only the date portion of the date/time is taken into account.
        /// </remarks>
        public static bool operator == ( Date left, DateTime right )
        {
            return left.Equals(new Date(right));
        }

        /// <summary>Determines if a date is equal to a date and time.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if the objects are equal.</returns>
        /// <remarks>
        /// Only the date portion of the date/time is taken into account.
        /// </remarks>
        public static bool operator == ( DateTime left, Date right )
        {
            return new Date(left).Equals(right);
        }

        /// <summary>Determines if a date is equal to a date and time.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if the objects are not equal.</returns>
        /// <remarks>
        /// Only the date portion of the date/time is taken into account.
        /// </remarks>
        public static bool operator != ( Date left, DateTime right )
        {
            return !left.Equals(new Date(right));
        }

        /// <summary>Determines if a date is equal to a date and time.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if the objects are not equal.</returns>
        /// <remarks>
        /// Only the date portion of the date/time is taken into account.
        /// </remarks>
        public static bool operator != ( DateTime left, Date right )
        {
            return !new Date(left).Equals(right);
        }
        #endregion
                
        #region IFormattable Members

        /// <summary>Gets the formatted string.</summary>
        /// <param name="format">The format string.</param>
        /// <returns>The string result.</returns>
        /// <remarks>
        /// If a time specifier is included then the time will be treated as midnight.
        /// </remarks>
        public string ToString ( string format )
        {
            return m_dt.ToString(format);
        }

        /// <summary>Gets the formatted string.</summary>
        /// <param name="format">The format string.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <returns>The string result.</returns>
        /// <remarks>
        /// If a time specifier is included then the time will be treated as midnight.
        /// </remarks>
        public string ToString ( string format, IFormatProvider formatProvider )
        {
            return m_dt.ToString(format, formatProvider);
        }
        #endregion

        #region Private Members
        
        private readonly DateTime m_dt;
        #endregion
    }
}