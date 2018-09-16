/*
 * Copyright © 2010 Michael Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.Diagnostics.CodeAnalysis;

#endregion

namespace P3Net.Kraken
{
    /// <summary>Represents a date-time range.</summary>
    public struct DateTimeRange : IEquatable<DateTimeRange>
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="DateTimeRange"/> structure.</summary>
        /// <param name="startDate">The start datetime.</param>
        /// <param name="endDate">The end datetime.</param>
        /// <remarks>
        /// If <paramref name="startDate"/> is greater than <paramref name="endDate"/> then the values are reversed.
        /// </remarks>
        public DateTimeRange ( DateTime startDate, DateTime endDate ) : this()
        {
            //Silently handle a mismatched date intervals
            if (startDate > endDate)
            {
                Start = endDate;
                End = startDate;
            } else
            {
                Start = startDate;
                End = endDate;
            };
        }
        #endregion

        #region Public Members

        /// <summary>Gets the duration between the start and end dates.</summary>
        public TimeSpan Duration
        {
            get { return End - Start; }
        }

        /// <summary>Gets the end date-time.</summary>
        public DateTime End { get; private set; }

        /// <summary>Gets the start date-time.</summary>
        public DateTime Start { get; private set; }

        #region Methods

        /// <summary></summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public override bool Equals ( object obj )
        {
            return Equals((DateTimeRange)obj);
        }

        /// <summary>Determines if two values are equal.</summary>
        /// <param name="other">The other to compare against.</param>
        /// <returns><see langword="true"/> if they are equal.</returns>
        public bool Equals ( DateTimeRange other )
        {
            return Start == other.Start && End == other.End;
        }

        /// <summary>Gets the hash code.</summary>
        /// <returns>The hash code.</returns>
        [ExcludeFromCodeCoverage]
        public override int GetHashCode ()
        {
            return Start.GetHashCode() | End.GetHashCode();
        }

        /// <summary>Gets the string representation as a time range.</summary>
        /// <returns>The string representation.</returns>
        public override string ToString ()
        {
            return String.Format("{0} - {1}", Start, End);
        }
        #endregion

        #region Operators

        /// <summary>Equality operator.</summary>
        /// <param name="left">First object.</param>
        /// <param name="right">Second object.</param>
        /// <returns><see langword="true"/> if they are equal</returns>
        public static bool operator == ( DateTimeRange left, DateTimeRange right )
        {
            return left.Equals(right);
        }

        /// <summary>Inequality operator.</summary>
        /// <param name="left">First object.</param>
        /// <param name="right">Second object.</param>
        /// <returns><see langword="true"/> if they are not equal</returns>
        public static bool operator != ( DateTimeRange left, DateTimeRange right )
        {
            return !left.Equals(right);
        }
        #endregion

        #endregion
    }
}
