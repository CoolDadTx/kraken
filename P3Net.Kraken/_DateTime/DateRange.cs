/*
 * Copyright © 2010 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Diagnostics.CodeAnalysis;

namespace P3Net.Kraken
{
    /// <summary>Represents a date range.</summary>
    /// <remarks>
    /// Time is ignored.
    /// </remarks>
    public struct DateRange : IEquatable<DateRange>
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="DateRange"/> structure.</summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <remarks>
        /// If <paramref name="startDate"/> is greater than <paramref name="endDate"/> then the values are reversed.
        /// </remarks>
        public DateRange ( DateTime startDate, DateTime endDate ) : this(new Date(startDate), new Date(endDate))
        {
        }

        /// <summary>Initializes an instance of the <see cref="DateRange"/> structure.</summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <remarks>
        /// If <paramref name="startDate"/> is greater than <paramref name="endDate"/> then the values are reversed.
        /// </remarks>
        public DateRange ( Date startDate, Date endDate ) : this()
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

        /// <summary>Gets the duration, in days, between the start and end dates.</summary>
        public int Duration
        {
            get { return End.Difference(Start); }
        }

        /// <summary>Gets the end date.</summary>
        public Date End { get; private set; }

        /// <summary>Gets the start date.</summary>
        public Date Start { get; private set; }

        /// <summary>Defines an empty data range.</summary>
        public static readonly DateRange Empty = new DateRange();

        #region Methods

        /// <summary>Determines if a date is within the date range.</summary>
        /// <param name="date">The date.</param>
        /// <returns><see langword="true"/> if the date is within the range.</returns>
        public bool Contains ( Date date )
        {
            return date.IsBetween(Start, End);
        }

        /// <summary></summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public override bool Equals ( object obj )
        {
            return Equals((DateRange)obj);
        }

        /// <summary>Determines if two values are equal.</summary>
        /// <param name="other">The other to compare against.</param>
        /// <returns><see langword="true"/> if they are equal.</returns>
        public bool Equals ( DateRange other )
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

        /// <summary>Gets the intersection of two ranges.</summary>
        /// <param name="other">The other range.</param>
        /// <seealso cref="Join"/>
        /// <seealso cref="Overlaps"/>
        /// <remarks>
        /// The intersection is the date range that contains both of the ranges.  If there is no overlap then it returns <see cref="Empty"/>.
        /// </remarks>
        public DateRange Intersect ( DateRange other )
        {
            if (this == other)
                return this;

            //If they don't overlap then the intersection is empty
            if (!Overlaps(other))
                return Empty;

            //Find the largest Start and the smallest End
            var start = (Start > other.Start) ? Start : other.Start;
            var end = (End < other.End) ? End : other.End;

            return new DateRange(start, end);
        }

        /// <summary>Joins this range with another range to produce a new range.</summary>
        /// <param name="other">The other range.</param>
        /// <returns>The new range that contains both ranges.</returns>
        /// <seealso cref="Intersect"/>
        /// <seealso cref="Overlaps"/>
        public DateRange Join ( DateRange other )
        {
            if (this == other)
                return this;

            //Find the smallest start and the largest end
            var start = (Start <= other.Start) ? Start : other.Start;
            var end = (End >= other.End) ? End : other.End;

            return new DateRange(start, end);
        }

        /// <summary>Determines if two ranges overlap.</summary>
        /// <param name="other">The other range.</param>
        /// <returns><see langword="true"/> if the ranges overlap.</returns>
        /// <seealso cref="Intersect"/>
        /// <seealso cref="Join"/>
        public bool Overlaps ( DateRange other )
        {
            if (this == other)
                return true;

            //Possible cases
            // 1) source is before other
            // 2) source contains other's start (or other is contained in source)
            // 3) source is inside other
            // 4) source contains other's end
            // 5) source is after other
            if (Contains(other.Start) || other.Contains(Start))
                return true;

            return false;
        }

        /// <summary>Gets the string representation as a date range.</summary>
        /// <returns>The string representation.</returns>
        public override string ToString ()
        {
            return String.Format("{0} - {1}", Start.ToShortDateString(), End.ToShortDateString());
        }
        #endregion     
   
        #region Operators

        /// <summary>Equality operator.</summary>
        /// <param name="left">First object.</param>
        /// <param name="right">Second object.</param>
        /// <returns><see langword="true"/> if they are equal</returns>
        public static bool operator== ( DateRange left, DateRange right )
        {
            return left.Equals(right);
        }

        /// <summary>Inequality operator.</summary>
        /// <param name="left">First object.</param>
        /// <param name="right">Second object.</param>
        /// <returns><see langword="true"/> if they are not equal</returns>
        public static bool operator!= ( DateRange left, DateRange right )
        {
            return !left.Equals(right);
        }
        #endregion
    }
}
