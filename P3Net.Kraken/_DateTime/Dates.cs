/*
 * Copyright © 2013 Michael Taylor
 * All Rights Reserved
 * 
 * From code copyright Federation of State Medical Boards
 */
using System;

namespace P3Net.Kraken
{
    /// <summary>Provides access to methods to create dates.</summary>
    public static class Dates
    {
        #region Fields

        /// <summary>Gets the maximum month.  This is currently 12.</summary>
        public static readonly int MaximumMonth = DateTime.MaxValue.Month;

        /// <summary>Gets the maximum year.  This is currently 9999.</summary>
        public static readonly int MaximumYear = DateTime.MaxValue.Year;

        /// <summary>Gets the minimum month.  This is currently 1.</summary>
        public static readonly int MinimumMonth = DateTime.MinValue.Month;

        /// <summary>Gets the minimum year.  This is currently 1.</summary>
        public static readonly int MinimumYear = DateTime.MinValue.Year;        

        #endregion

        #region Methods
        
        /// <summary>Creates a month part.</summary>
        /// <param name="value">The month number.</param>
        /// <returns>The month part.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is less than 1 or greater than 12.</exception>
        public static MonthPart Month ( int value )
        { return new MonthPart(value); }

        /// <summary>Creates a year part.</summary>
        /// <param name="value">The year.</param>
        /// <returns>The year part.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is less than 1 or greater than 9999.</exception>
        public static YearPart Year ( int value )
        {
            return new YearPart(value);
        }
        #endregion

        #region Months

        /// <summary>Creates a month part for January.</summary>
        /// <returns>The month part.</returns>
        public static MonthPart January ()
        {
            return new MonthPart(Months.January);
        }

        /// <summary>Creates a month part for February.</summary>
        /// <returns>The month part.</returns>
        public static MonthPart February ()
        {
            return new MonthPart(Months.February);
        }

        /// <summary>Creates a month part for March.</summary>
        /// <returns>The month part.</returns>
        public static MonthPart March ()
        {
            return new MonthPart(Months.March);
        }

        /// <summary>Creates a month part for April.</summary>
        /// <returns>The month part.</returns>
        public static MonthPart April ()
        {
            return new MonthPart(Months.April);
        }

        /// <summary>Creates a month part for May.</summary>
        /// <returns>The month part.</returns>
        public static MonthPart May ()
        {
            return new MonthPart(Months.May);
        }

        /// <summary>Creates a month part for June.</summary>
        /// <returns>The month part.</returns>
        public static MonthPart June ()
        {
            return new MonthPart(Months.June);
        }

        /// <summary>Creates a month part for July.</summary>
        /// <returns>The month part.</returns>
        public static MonthPart July ()
        {
            return new MonthPart(Months.July);
        }

        /// <summary>Creates a month part for August.</summary>
        /// <returns>The month part.</returns>
        public static MonthPart August ()
        {
            return new MonthPart(Months.August);
        }

        /// <summary>Creates a month part for September.</summary>
        /// <returns>The month part.</returns>
        public static MonthPart September ()
        {
            return new MonthPart(Months.September);
        }

        /// <summary>Creates a month part for October.</summary>
        /// <returns>The month part.</returns>
        public static MonthPart October ()
        {
            return new MonthPart(Months.October);
        }

        /// <summary>Creates a month part for November.</summary>
        /// <returns>The month part.</returns>
        public static MonthPart November ()
        {
            return new MonthPart(Months.November);
        }

        /// <summary>Creates a month part for December.</summary>
        /// <returns>The month part.</returns>
        public static MonthPart December ()
        {
            return new MonthPart(Months.December);
        }

        /// <summary>Creates a date given a day and year.</summary>
        /// <param name="day">The day.</param>
        /// <param name="year">The year.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="day"/> is less than 1 or greater than the days in the month.</exception>
        public static Date January ( int day, int year )
        {
            return new Date(year, Months.January, day);
        }

        /// <summary>Creates a date given a day and year.</summary>
        /// <param name="day">The day.</param>
        /// <param name="year">The year.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="day"/> is less than 1 or greater than the days in the month.</exception>
        public static Date February ( int day, int year )
        {
            return new Date(year, Months.February, day);
        }

        /// <summary>Creates a date given a day and year.</summary>
        /// <param name="day">The day.</param>
        /// <param name="year">The year.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="day"/> is less than 1 or greater than the days in the month.</exception>
        public static Date March ( int day, int year )
        {
            return new Date(year, Months.March, day);
        }

        /// <summary>Creates a date given a day and year.</summary>
        /// <param name="day">The day.</param>
        /// <param name="year">The year.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="day"/> is less than 1 or greater than the days in the month.</exception>
        public static Date April ( int day, int year )
        {
            return new Date(year, Months.April, day);
        }

        /// <summary>Creates a date given a day and year.</summary>
        /// <param name="day">The day.</param>
        /// <param name="year">The year.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="day"/> is less than 1 or greater than the days in the month.</exception>
        public static Date May ( int day, int year )
        {
            return new Date(year, Months.May, day);
        }

        /// <summary>Creates a date given a day and year.</summary>
        /// <param name="day">The day.</param>
        /// <param name="year">The year.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="day"/> is less than 1 or greater than the days in the month.</exception>
        public static Date June ( int day, int year )
        {
            return new Date(year, Months.June, day);
        }

        /// <summary>Creates a date given a day and year.</summary>
        /// <param name="day">The day.</param>
        /// <param name="year">The year.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="day"/> is less than 1 or greater than the days in the month.</exception>
        public static Date July ( int day, int year )
        {
            return new Date(year, Months.July, day);
        }

        /// <summary>Creates a date given a day and year.</summary>
        /// <param name="day">The day.</param>
        /// <param name="year">The year.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="day"/> is less than 1 or greater than the days in the month.</exception>
        public static Date August ( int day, int year )
        {
            return new Date(year, Months.August, day);
        }

        /// <summary>Creates a date given a day and year.</summary>
        /// <param name="day">The day.</param>
        /// <param name="year">The year.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="day"/> is less than 1 or greater than the days in the month.</exception>
        public static Date September ( int day, int year )
        {
            return new Date(year, Months.September, day);
        }

        /// <summary>Creates a date given a day and year.</summary>
        /// <param name="day">The day.</param>
        /// <param name="year">The year.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="day"/> is less than 1 or greater than the days in the month.</exception>
        public static Date October ( int day, int year )
        {
            return new Date(year, Months.October, day);
        }

        /// <summary>Creates a date given a day and year.</summary>
        /// <param name="day">The day.</param>
        /// <param name="year">The year.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="day"/> is less than 1 or greater than the days in the month.</exception>
        public static Date November ( int day, int year )
        {
            return new Date(year, Months.November, day);
        }

        /// <summary>Creates a date given a day and year.</summary>
        /// <param name="day">The day.</param>
        /// <param name="year">The year.</param>
        /// <returns>The date.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="year"/> is less than 1 or greater than 9999.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="day"/> is less than 1 or greater than the days in the month.</exception>
        public static Date December ( int day, int year )
        {
            return new Date(year, Months.December, day);
        }
        #endregion
    }    
}
