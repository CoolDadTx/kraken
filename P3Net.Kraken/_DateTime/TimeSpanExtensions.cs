/*
 * Copyright © 2010 Michael Taylor
 * All Rights Reserved
 */
#region Imports

using System;

#endregion

namespace P3Net.Kraken
{
    /// <summary>Extension methods for <see cref="TimeSpan"/>.</summary>        
    public static class TimeSpanExtensions
    {
        #region Data

        /// <summary>Specifies the maximum time of day.</summary>
        public static readonly TimeSpan MaximumTimeOfDay = new TimeSpan(1, 0, 0, 0).Subtract(new TimeSpan(0, 0, 0, 0, 1));
        #endregion

        #region Methods

        #region Add...
        
        /// <summary>Adds days to a <see cref="TimeSpan"/>.</summary>
        /// <param name="source">The source.</param>
        /// <param name="days">The days to add.</param>
        /// <returns>The new value.</returns>
        /// <exception cref="OverflowException">The new time would be either too large or too small.</exception>
        /// <remarks>
        /// <paramref name="days"/> can be positive to add or negative to subtract.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// DateTime UpdateTime ( DateTime value, int days, int hours, int minutes, int seconds, int milliseconds )
        /// {
        ///    var newTime = new TimeSpan();
        ///    
        ///    if (days != 0)
        ///       newTime = newTime.AddDays(days);
        ///    if (hours != 0)
        ///       newTime = newTime.AddHours(hours);
        ///    if (minutes != 0)
        ///       newTime = newTime.AddMinutes(minutes);
        ///    if (seconds != 0)
        ///       newTime = newTime.AddSeconds(seconds);
        ///    if (milliseconds != 0)
        ///       newTime = newTime.AddMilliseconds(milliseconds);
        ///       
        ///    return newTime;
        /// }
        /// </code>
        /// </example>
        public static TimeSpan AddDays ( this TimeSpan source, int days )
        {            
            return source.Add(new TimeSpan(days, 0, 0, 0));
        }

        /// <summary>Adds hours to a <see cref="TimeSpan"/>.</summary>
        /// <param name="source">The source.</param>
        /// <param name="hours">The hours to add.</param>
        /// <returns>The new value.</returns>
        /// <exception cref="OverflowException">The new time would be either too large or too small.</exception>
        /// <remarks>
        /// <paramref name="hours"/> can be positive to add or negative to subtract.
        /// </remarks>
        /// <example>Refer to <see cref="AddDays"/> for an example.</example>
        public static TimeSpan AddHours ( this TimeSpan source, int hours )
        {
            return source.Add(new TimeSpan(hours, 0, 0));
        }

        /// <summary>Adds milliseconds to a <see cref="TimeSpan"/>.</summary>
        /// <param name="source">The source.</param>
        /// <param name="milliseconds">The milliseconds to add.</param>
        /// <returns>The new value.</returns>
        /// <exception cref="OverflowException">The new time would be either too large or too small.</exception>
        /// <remarks>
        /// <paramref name="milliseconds"/> can be positive to add hours or negative to subtract.
        /// </remarks>
        /// <example>Refer to <see cref="AddDays"/> for an example.</example>
        public static TimeSpan AddMilliseconds ( this TimeSpan source, int milliseconds )
        {
            return source.Add(new TimeSpan(0, 0, 0, 0, milliseconds));
        }

        /// <summary>Adds minutes to a <see cref="TimeSpan"/>.</summary>
        /// <param name="source">The source.</param>
        /// <param name="minutes">The minutes to add.</param>
        /// <returns>The new value.</returns>
        /// <exception cref="OverflowException">The new time would be either too large or too small.</exception>
        /// <remarks>
        /// <paramref name="minutes"/> can be positive to add or negative to subtract.
        /// </remarks>
        /// <example>Refer to <see cref="AddDays"/> for an example.</example>
        public static TimeSpan AddMinutes ( this TimeSpan source, int minutes )
        {
            return source.Add(new TimeSpan(0, minutes, 0));
        }

        /// <summary>Adds seconds to a <see cref="TimeSpan"/>.</summary>
        /// <param name="source">The source.</param>
        /// <param name="seconds">The seconds to add.</param>
        /// <returns>The new value.</returns>
        /// <exception cref="OverflowException">The new time would be either too large or too small.</exception>
        /// <remarks>
        /// <paramref name="seconds"/> can be positive to add hours or negative to subtract.
        /// </remarks>
        /// <example>Refer to <see cref="AddDays"/> for an example.</example>
        public static TimeSpan AddSeconds ( this TimeSpan source, int seconds )
        {
            return source.Add(new TimeSpan(0, 0, seconds));
        }

        /// <summary>Adds ticks to a <see cref="TimeSpan"/>.</summary>
        /// <param name="source">The source.</param>
        /// <param name="ticks">The ticks to add.</param>
        /// <returns>The new value.</returns>
        /// <exception cref="OverflowException">The new time would be either too large or too small.</exception>
        /// <remarks>
        /// <paramref name="ticks"/> can be positive to add hours or negative to subtract.
        /// </remarks>
        /// <example>Refer to <see cref="AddDays"/> for an example.</example>
        public static TimeSpan AddTicks ( this TimeSpan source, long ticks )
        {
            return source.Add(new TimeSpan(ticks));
        }
        #endregion

        /// <summary>Determines if a value is in the given range.</summary>
        /// <param name="source">The value to check.</param>
        /// <param name="minimumValue">The minimum value.</param>
        /// <param name="maximumValue">The maximum value.</param>
        /// <returns><see langword="true"/> if the value is in range or <see langword="false"/> otherwise.</returns>
        public static bool InRange ( this TimeSpan source, TimeSpan minimumValue, TimeSpan maximumValue )
        {
            return source >= minimumValue && source <= maximumValue;
        }

        /// <summary>Determines if a time is a valid time of day.</summary>
        /// <param name="source">The source.</param>
        /// <returns><see langword="true"/> if the time is a valid time of day.</returns>        
        public static bool IsValidTimeOfDay ( this TimeSpan source )
        {
            //Valid TODs are 0..(midnight - 1)
            return InRange(source, TimeSpan.Zero, MaximumTimeOfDay);
        }
        #endregion
    }
}
