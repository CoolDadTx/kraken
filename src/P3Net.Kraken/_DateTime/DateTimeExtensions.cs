/*
 * Copyright © 2010 Michael Taylor
 * All Rights Reserved
 */
using System;

namespace P3Net.Kraken
{
    /// <summary>Extension methods for <see cref="DateTime"/>.</summary>    
    public static class DateTimeExtensions
    {       
        /// <summary>Gets a value indicating the end of a day.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The date with the time set to the last possible value for the day.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="source"/> is <see cref="DateTime.MaxValue"/>.</exception>
        /// <example>Refer to <see cref="StartOfDay"/> for an example.</example>
        /// <seealso cref="StartOfDay" />        
        public static DateTime EndOfDay ( this DateTime source )
        {
            return source.Date.SetTimeOfDay(TimeSpanExtensions.MaximumTimeOfDay);
        }

        /// <summary>Determines if a value is in the given date range.</summary>
        /// <param name="source">The value to check.</param>
        /// <param name="minimumValue">The minimum value.</param>
        /// <param name="maximumValue">The maximum value.</param>
        /// <returns><see langword="true"/> if the value is in range or <see langword="false"/> otherwise.</returns>        
        /// <remarks>
        /// Only the dates are considered for purposes of comparison.
        /// </remarks>
        /// <example>Refer to <see cref="O:InRange"/> for an example.</example>
        /// <seealso cref="O:InRange"/>
        /// <seealso cref="O:IsSameDay"/>
        public static bool InDateRange ( this DateTime source, DateTime minimumValue, DateTime maximumValue )
        {
            return new Date(source).IsBetween(new Date(minimumValue), new Date(maximumValue));
        }

        /// <summary>Determines if a value is in the given date range.</summary>
        /// <param name="source">The value to check.</param>
        /// <param name="minimumValue">The minimum value.</param>
        /// <param name="maximumValue">The maximum value.</param>
        /// <returns><see langword="true"/> if the value is in range or <see langword="false"/> otherwise.</returns>        
        /// <example>Refer to <see cref="O:InRange"/> for an example.</example>
        /// <seealso cref="O:InRange"/>
        /// <seealso cref="O:IsSameDay"/>
        public static bool InDateRange ( this DateTime source, Date minimumValue, Date maximumValue )
        {
            return new Date(source).IsBetween(minimumValue, maximumValue);
        }

        /// <summary>Determines if a value is in the given datetime range.</summary>
        /// <param name="source">The value to check.</param>
        /// <param name="minimumValue">The minimum value.</param>
        /// <param name="maximumValue">The maximum value.</param>
        /// <returns><see langword="true"/> if the value is in range or <see langword="false"/> otherwise.</returns>
        /// <example>
        /// <code lang="C#">
        /// void IsDateInRange ( DateTime value, DateTime start, DateTime end )
        /// {
        ///    if (value.InDateRange(start, end))
        ///       Console.WriteLine("Value is in date range.");
        ///    else
        ///       Console.WriteLine("Value is not in date range.");
        ///       
        ///    if (value.InRange(start, end))
        ///       Console.WriteLine("Value is in range.");
        ///    else
        ///       Console.WriteLine("Value is not in range.");
        /// }
        /// 
        /// //  Value = 10/25/2011 12:34:56
        /// //  Start = 10/25/2011  23:45:56
        /// //  End   =  10/26/2011 12:34:56
        /// //     Value is in date range
        /// //     Value is not in range
        /// </code>
        /// </example>
        /// <seealso cref="O:InDateRange"/>
        /// <seealso cref="O:IsSameDay"/>
        public static bool InRange ( this DateTime source, DateTime minimumValue, DateTime maximumValue )
        {
            return source >= minimumValue && source <= maximumValue;
        }

        /// <summary>Determines if one date is the day after another date.</summary>
        /// <param name="source">The source.</param>
        /// <param name="value">The value to check.</param>
        /// <returns><see langword="true"/> if <paramref name="source"/> is the day before <paramref name="value"/>.</returns>
        /// <example>Refer to <see cref="O:IsSameDay"/> for an example.</example>
        /// <seealso cref="O:IsSameDay"/>
        /// <seealso cref="O:IsPreviousDay"/>
        public static bool IsNextDay ( this DateTime source, DateTime value )
        {
            return IsNextDay(source, new Date(value));
        }

        /// <summary>Determines if one date is the day after another date.</summary>
        /// <param name="source">The source.</param>
        /// <param name="value">The value to check.</param>
        /// <returns><see langword="true"/> if <paramref name="source"/> is the day before <paramref name="value"/>.</returns>
        /// <example>Refer to <see cref="O:IsSameDay"/> for an example.</example>
        /// <seealso cref="O:IsSameDay"/>
        /// <seealso cref="O:IsPreviousDay"/>
        public static bool IsNextDay ( this DateTime source, Date value )
        {
            if (source == DateTime.MaxValue)
                return false;

            return new Date(source).Tomorrow() == value;
        }

        /// <summary>Determines if one date is the day before another date.</summary>
        /// <param name="source">The source.</param>
        /// <param name="value">The value to check.</param>
        /// <returns><see langword="true"/> if the source is the day after <paramref name="value"/>.</returns>
        /// <example>Refer to <see cref="O:IsSameDay"/> for an example.</example>
        /// <seealso cref="O:IsNextDay"/>
        /// <seealso cref="O:IsSameDay"/>
        public static bool IsPreviousDay ( this DateTime source, DateTime value )
        {
            return IsPreviousDay(source, new Date(value));
        }

        /// <summary>Determines if one date is the day before another date.</summary>
        /// <param name="source">The source.</param>
        /// <param name="value">The value to check.</param>
        /// <returns><see langword="true"/> if the source is the day after <paramref name="value"/>.</returns>
        /// <example>Refer to <see cref="O:IsSameDay"/> for an example.</example>
        /// <seealso cref="O:IsNextDay"/>
        /// <seealso cref="O:IsSameDay"/>
        public static bool IsPreviousDay ( this DateTime source, Date value )
        {
            if (source == DateTime.MinValue)
                return false;

            return new Date(source).Yesterday() == value;
        }

        /// <summary>Determines if two values are the same day.</summary>
        /// <param name="source">The source.</param>
        /// <param name="value">The value to check.</param>
        /// <returns><see langword="true"/> if the datetime values are the same day.</returns>
        /// <example>
        /// <code lang="C#">
        /// void DetermineIfClose ( DateTime value1, DateTime value2 )
        /// {
        ///    Console.WriteLine("Is same day: {0}", value1.IsSameDay(value2) ? "Yes" : "No");
        ///    Console.WriteLine("Is previous day: {0}", value1.IsPreviousDay(value2) ? "Yes" : "No");
        ///    Console.WriteLine("Is next day: {0}", value1.IsNextDay(value2) ? "Yes" : "No");
        /// }
        ///         
        /// DetermineIfClose(new DateTime(2011, 10, 25), new DateTime(2011, 10, 26);  // No, No, Yes
        /// DetermineIfClose(new DateTime(2011, 10, 26), new DateTime(2011, 10, 25);  // No, Yes, No
        /// DetermineIfClose(new DateTime(2011, 10, 25), new DateTime(2011, 10, 25);  // Yes, No, No
        /// </code>
        /// </example>
        /// <seealso cref="O:IsNextDay"/>
        /// <seealso cref="O:IsPreviousDay"/>
        public static bool IsSameDay ( this DateTime source, DateTime value )
        {
            return IsSameDay(source, new Date(value));
        }

        /// <summary>Determines if two values are the same day.</summary>
        /// <param name="source">The source.</param>
        /// <param name="value">The value to check.</param>
        /// <returns><see langword="true"/> if the datetime values are the same day.</returns>
        /// <example>
        /// <code lang="C#">
        /// void DetermineIfClose ( DateTime value1, DateTime value2 )
        /// {
        ///    Console.WriteLine("Is same day: {0}", value1.IsSameDay(value2) ? "Yes" : "No");
        ///    Console.WriteLine("Is previous day: {0}", value1.IsPreviousDay(value2) ? "Yes" : "No");
        ///    Console.WriteLine("Is next day: {0}", value1.IsNextDay(value2) ? "Yes" : "No");
        /// }
        ///         
        /// DetermineIfClose(new DateTime(2011, 10, 25), new DateTime(2011, 10, 26);  // No, No, Yes
        /// DetermineIfClose(new DateTime(2011, 10, 26), new DateTime(2011, 10, 25);  // No, Yes, No
        /// DetermineIfClose(new DateTime(2011, 10, 25), new DateTime(2011, 10, 25);  // Yes, No, No
        /// </code>
        /// </example>
        /// <seealso cref="O:IsNextDay"/>
        /// <seealso cref="O:IsPreviousDay"/>
        public static bool IsSameDay ( this DateTime source, Date value )
        {
            return new Date(source) == value;
        }

        /// <summary>Determines if a date is a weekend date.</summary>
        /// <param name="source">The source.</param>
        /// <returns><see langword="true"/> if it is a weekend.</returns>        
        /// <remarks>
        /// Uses the US standard for determining weekends.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// new DateTime(2011, 10, 22).IsWeekend()  // Saturday - true
        /// new DateTime(2011, 10, 21).IsWeekend()  // Friday - false
        /// </code>
        /// </example>
        public static bool IsWeekend ( this DateTime source )
        {
            //Currently we assume Saturday and Sunday are weekends
            return source.DayOfWeek == DayOfWeek.Saturday || source.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>Gets the start of the next day.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The start of the next day.</returns>        
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="source"/> is <see cref="DateTime.MaxValue"/>.</exception>        
        /// <example>Refer to <see cref="StartOfDay"/> for an example.</example>
        /// <seealso cref="PreviousDay"/>
        /// <seealso cref="NextDaySameTime"/>
        public static Date NextDay ( this DateTime source )
        {
            return new Date(source).Tomorrow();
        }

        /// <summary>Gets the same time next day.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The next day at the same time.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="source"/> is <see cref="DateTime.MaxValue"/>.</exception>
        /// <example>Refer to <see cref="StartOfDay"/> for an example.</example>
        /// <seealso cref="PreviousDaySameTime"/>
        /// <seealso cref="NextDay"/>        
        public static DateTime NextDaySameTime ( this DateTime source )
        {
            return source.AddDays(1);
        }

        /// <summary>Gets the start of the previous day.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The start of the previous day.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="source"/> is <see cref="DateTime.MinValue"/>.</exception>
        /// <example>Refer to <see cref="StartOfDay"/> for an example.</example>
        /// <seealso cref="NextDay"/>
        /// <seealso cref="PreviousDaySameTime"/>
        public static Date PreviousDay ( this DateTime source )
        {
            return new Date(source).Yesterday();
        }

        /// <summary>Gets the same time on the previous day.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The previous day at the same time.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="source"/> is <see cref="DateTime.MinValue"/>.</exception>
        /// <example>Refer to <see cref="StartOfDay"/> for an example.</example>
        /// <seealso cref="NextDaySameTime"/>
        /// <seealso cref="PreviousDay"/>
        public static DateTime PreviousDaySameTime ( this DateTime source )
        {
            return source.AddDays(-1);
        }

        #region SetDate
        
        /// <summary>Sets the date of a datetime value.  The time does not change.</summary>
        /// <param name="source">The source.</param>
        /// <param name="value">The new date.</param>
        /// <returns>The new datetime.</returns>
        /// <example>
        /// <code lang="C#">
        /// public DateTime UpdateChangeDate ( DateTime original, DateTime newDate )
        /// {
        ///    return original.SetDate(newDate);
        /// }
        /// </code>
        /// </example>
        /// <seealso cref="O:SetTimeOfDay"/>        
        public static DateTime SetDate ( this DateTime source, DateTime value )
        {
            return new Date(value).At(source.TimeOfDay);
        }

        /// <summary>Sets the date of a datetime value.  The time does not change.</summary>
        /// <param name="source">The source.</param>
        /// <param name="value">The new date.</param>
        /// <returns>The new datetime.</returns>
        /// <example>
        /// <code lang="C#">
        /// public DateTime UpdateChangeDate ( DateTime original, DateTime newDate )
        /// {
        ///    return original.SetDate(newDate);
        /// }
        /// </code>
        /// </example>
        /// <seealso cref="O:SetTimeOfDay"/>        
        public static DateTime SetDate ( this DateTime source, Date value )
        {
            return value.At(source.TimeOfDay);
        }

        /// <summary>Sets the date of a datetime value.  The time does not change.</summary>
        /// <param name="source">The source.</param>
        /// <param name="year">The new year.</param>
        /// <param name="month">The new month.</param>
        /// <param name="day">The new day.</param>
        /// <returns>The new datetime.</returns>
        /// <exception cref="ArgumentOutOfRangeException">One of the parameters is out of range.</exception>
        /// <example>Refer to <see cref="O:SetDate(DateTime, DateTime)"/> for an example.</example>
        /// <seealso cref="O:SetTimeOfDay"/>
        public static DateTime SetDate ( this DateTime source, int year, int month, int day )
        {
            return new Date(year, month, day).At(source.TimeOfDay);
        }
        #endregion

        #region SetTimeOfDay
        
        /// <summary>Sets the time of a datetime value.  The date does not change.</summary>
        /// <param name="source">The source.</param>
        /// <param name="value">The new time.</param>
        /// <returns>The new datetime.</returns>
        /// <example>
        /// <code lang="C#">
        /// public DateTime ResetToNoon ( DateTime original )
        /// {
        ///    return original.SetTimeOfDay(new TimeSpan(12, 0, 0));
        /// }
        /// </code>
        /// </example>
        /// <seealso cref="O:SetDate"/>
        public static DateTime SetTimeOfDay ( this DateTime source, TimeSpan value )
        {
            return new Date(source).At(value);
        }

        /// <summary>Sets the time of a datetime value.  The date does not change.</summary>
        /// <param name="source">The source.</param>
        /// <param name="hour">The new hour.</param>
        /// <param name="minute">The new minute.</param>
        /// <param name="second">The new second.</param>
        /// <exception cref="ArgumentOutOfRangeException">One of the parameters is out of range.</exception>
        /// <returns>The new datetime.</returns>
        /// <example>Refer to <see cref="O:SetTimeOfDay(DateTime, TimeSpan)"/> for an example.</example>        
        /// <seealso cref="O:SetDate"/>
        public static DateTime SetTimeOfDay ( this DateTime source, int hour, int minute, int second)
        {
            return SetTimeOfDay(source, hour, minute, second, 0);
        }

        /// <summary>Sets the time of a datetime value.  The date does not change.</summary>
        /// <param name="source">The source.</param>
        /// <param name="hour">The new hour.</param>
        /// <param name="minute">The new minute.</param>
        /// <param name="second">The new second.</param>
        /// <param name="millisecond">The new millisecond.</param>
        /// <exception cref="ArgumentOutOfRangeException">One of the parameters is out of range.</exception>
        /// <returns>The new datetime.</returns>
        /// <example>Refer to <see cref="O:SetTimeOfDay(DateTime, TimeSpan)"/> for an example.</example>        
        /// <seealso cref="O:SetDate"/>
        public static DateTime SetTimeOfDay ( this DateTime source, int hour, int minute, int second, int millisecond )
        {
            return new Date(source).At(new TimeSpan(0, hour, minute, second, millisecond));
        }
        #endregion

        /// <summary>Gets a value indicating the start of a day.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The start of the day with the time cleared.</returns>
        /// <example>
        /// <code lang="C#">
        /// void PrintDateInformation ( DateTime value )
        /// {
        ///    Console.WriteLine("Start of Day: {0}", value.StartOfDay());
        ///    Console.WriteLine("End of Day: {0}", value.EndOfDay());
        ///    Console.WriteLine("Next Day: {0}", value.NextDay());
        ///    Console.WriteLine("Next Day at same time: {0}", value.NextDaySameTime());
        ///    Console.WriteLine("Previous Day: {0}", value.PreviousDay());
        ///    Console.WriteLine("Previous Day at same time: {0}", value.PreviousDaySameTime());
        /// }
        /// 
        /// // 10/25/2011 12:34:56 prints 
        /// //   Start of Day: 10/25/2011 0:0:0
        /// //   End of Day: 10/25/2011 23:59:59.999
        /// //   Next Day: 10/26/2011
        /// //   Next Day at same time: 10/26/2011 12:34:56
        /// //   Previous Day: 10/24/2011
        /// //   Previous Day at same time: 10/24/2011 12:34:56
        /// </code>
        /// </example>
        /// <seealso cref="EndOfDay"/>        
        public static DateTime StartOfDay ( this DateTime source )
        {
            return source.Date;
        }

        /// <summary>Converts a <see cref="DateTime"/> to a <see cref="Date"/>.</summary>
        /// <param name="source">The source value.</param>
        /// <returns>The date.</returns>
        public static Date ToDate ( this DateTime source )
        {
            return new Date(source);
        }

        /// <summary>Converts a <see cref="DateTime"/> to a <see cref="Date"/>.</summary>
        /// <param name="source">The source value.</param>
        /// <returns>The date.</returns>
        public static Date ToDate ( this DateTime? source )
        {
            return source.HasValue ? new Date(source.Value) : Date.None;
        }

        /// <summary>Converts a <see cref="DateTime"/> to a nullable <see cref="DateTime"/>.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="DateTime"/> if the value is not the minimum or <see langword="null"/> otherwise.</returns>
        public static DateTime? ToNullableDateTime ( this DateTime source )
        {
            return (source != DateTime.MinValue) ? source : (DateTime?)null;
        }
    }
}
