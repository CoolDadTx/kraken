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
    /// <summary>Represents a time range.</summary>
    [Serializable]
    public struct TimeRange : IEquatable<TimeRange>
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="TimeRange"/> structure.</summary>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <remarks>
        /// If <paramref name="startTime"/> is greater than <paramref name="endTime"/> then the times are reversed.
        /// </remarks>
        public TimeRange ( TimeSpan startTime, TimeSpan endTime ) : this()
        {
            //Silently handle a mismatched time interval
            if (startTime > endTime)
            {
                Start = endTime;
                End = startTime;
            } else
            {
                Start = startTime;
                End = endTime;
            };
        }
        #endregion

        #region Public Members

        /// <summary>Gets the duration of the range.</summary>
        public TimeSpan Duration 
        {
            get { return End - Start; }
        }

        /// <summary>Gets the end time.</summary>
        public TimeSpan End { get; private set; }

        /// <summary>Gets the start time.</summary>
        public TimeSpan Start { get; private set; }

        #region Methods

        /// <summary></summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public override bool Equals ( object obj )
        {
            return Equals((TimeRange)obj);
        }

        /// <summary>Determines if two values are equal.</summary>
        /// <param name="other">The other to compare against.</param>
        /// <returns><see langword="true"/> if they are equal.</returns>
        public bool Equals ( TimeRange other )
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
        public static bool operator == ( TimeRange left, TimeRange right )
        {
            return left.Equals(right);
        }

        /// <summary>Inequality operator.</summary>
        /// <param name="left">First object.</param>
        /// <param name="right">Second object.</param>
        /// <returns><see langword="true"/> if they are not equal</returns>
        public static bool operator != ( TimeRange left, TimeRange right )
        {
            return !left.Equals(right);
        }
        #endregion

        #endregion
    }
}
