/*
 * Wrapper around SYSTEMTIME structure. 
 * Based off the work done by Michael Brumm at http://www.michaelbrumm.com/simpletimezone.html
 *
 * Copyright (c) 2004 Michael L. Taylor ($COMPANY$)
 * Portions copyright (c) Michael R. Brumm
 * All rights reserved.
 * 
 * $Header: /DotNET/Kraken/Source/P3Net.Kraken.Utils/SystemTime.cs 5     10/26/05 7:59a Michael $
 */
using System;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.InteropServices;

namespace P3Net.Kraken.Win32
{
	/// <summary>A wrapper around the Win32 SYSTEMTIME structure.</summary>
	/// <remarks>
	/// This structure uses the most common calendar settings: 12 months, 7 days, etc.  Variations
	/// will need to account for this.
	/// <para/>
	/// Do not add and subtract values of this type to obtain relative values.  Convert to <see cref="DateTime"/> 
	/// and then use the provided methods.
	/// <para/>
	/// Based off the work done by Michael Brumm at http://www.michaelbrumm.com/simpletimezone.html
	/// </remarks>
	[CLSCompliant(false)]
	[CodeNotAnalyzed]
	[CodeNotTested]
	[StructLayout(LayoutKind.Sequential)]
	public struct SystemTime
	{
		#region Construction
		
		/// <summary>Initializes the object.</summary>
		/// <param name="dt">The <see cref="DateTime"/> value to initialize with.</param>
		public SystemTime ( DateTime dt )
		{
		    if ((dt.Year < MinimumYears) || (dt.Year > MaximumYears))
		        throw new ArgumentOutOfRangeException("dt", dt.Year, 
		                           String.Format("Year must be between {0} and {1}.", MinimumYears, MaximumYears));
		    else
                wYear = (ushort)dt.Year;
                
		    if ((dt.Month < MinimumMonths) || (dt.Month > MaximumMonths))
		        throw new ArgumentOutOfRangeException("dt", dt.Month, 
		                           String.Format("Month must be between {0} and {1}.", MinimumMonths, MaximumMonths));
		    else                
                wMonth = (ushort)dt.Month;
                
		    if ((dt.Day < MinimumDays) || (dt.Day > MaximumDays))
		        throw new ArgumentOutOfRangeException("dt", dt.Day, 
		                           String.Format("Day must be between {0} and {1}.", MinimumDays, MaximumDays));
		    else                
                wDay = (ushort)dt.Day;
                
		    if ((dt.DayOfWeek < DayOfWeek.Sunday) || (dt.DayOfWeek > DayOfWeek.Saturday))
		        throw new ArgumentOutOfRangeException("dt", dt.DayOfWeek, 
		                           "DayOfWeek must be between Sunday and Saturday.");
		    else                
                wDayOfWeek = (ushort)dt.DayOfWeek;
            
		    if ((dt.Hour < MinimumHours) || (dt.Hour > MaximumHours))
		        throw new ArgumentOutOfRangeException("dt", dt.Hour, 
		                           String.Format("Hour must be between {0} and {1}.", MinimumHours, MaximumHours));
		    else            
                wHour = (ushort)dt.Hour;
                
		    if ((dt.Minute < MinimumMinutes) || (dt.Minute > MaximumMinutes))
		        throw new ArgumentOutOfRangeException("dt", dt.Minute, 
		                           String.Format("Minute must be between {0} and {1}.", MinimumMinutes, MaximumMinutes));
		    else                
                wMinute = (ushort)dt.Minute;

		    if ((dt.Second < MinimumSeconds) || (dt.Second > MaximumSeconds))
		        throw new ArgumentOutOfRangeException("dt", dt.Second, 
		                           String.Format("Second must be between {0} and {1}.", MinimumSeconds, MaximumSeconds));
		    else
                wSecond = (ushort)dt.Second;
                
		    if ((dt.Millisecond < MinimumMilliseconds) || (dt.Millisecond > MaximumMilliseconds))
		        throw new ArgumentOutOfRangeException("dt", dt.Millisecond, 
		                           String.Format("Year must be between {0} and {1}.", MinimumMilliseconds, MaximumMilliseconds));
		    else                
                wMilliseconds = (ushort)dt.Millisecond;
		}
		#endregion
		
		#region Public Members
		
		#region Attributes
		
		/// <summary>The year (1601 - 30827).</summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public ushort wYear;
        
        /// <summary>The month (1 - 12).</summary>
        /// <value>January is 1 and December is 12.  Note that some calendars have 13 months.</value>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public ushort wMonth;
        
        /// <summary>The day of the week (0 - 6).</summary>
        /// <value>Sunday is 0 and Saturday is 6.</value>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public ushort wDayOfWeek;
        
        /// <summary>The day of the month (0 - 31).</summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public ushort wDay;
        
        /// <summary>The hour (0 - 23).</summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public ushort wHour;
        
        /// <summary>The minutes (0 - 59).</summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public ushort wMinute;
        
        /// <summary>The seconds (0 - 59).</summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public ushort wSecond;
        
        /// <summary>The milliseconds (0 - 999).</summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public ushort wMilliseconds;		
		#endregion
		
		#region Constants
		
		/// <summary>The maximum days this type supports.</summary>
		public const ushort MaximumDays = 31;
		
		/// <summary>The maximum hours this type supports.</summary>
		public const ushort MaximumHours = 23;
		
		/// <summary>The maximum milliseconds this type supports.</summary>
		public const ushort MaximumMilliseconds = 999;
		
		/// <summary>The maximum minutes this type supports.</summary>
		public const ushort MaximumMinutes = 59;
		
		/// <summary>The maximum months this type supports.</summary>
		public const ushort MaximumMonths = 12;
		
		/// <summary>The maximum seconds this type supports.</summary>
		public const ushort MaximumSeconds = 59;
		
		/// <summary>The maximum years this type supports.</summary>
		public const ushort MaximumYears = 30827;		
		
		/// <summary>The minimum days this type supports.</summary>
		public const ushort MinimumDays = 0;
		
		/// <summary>The minimum hours this type supports.</summary>
		public const ushort MinimumHours = 0;
		
		/// <summary>The minimum milliseconds this type supports.</summary>
		public const ushort MinimumMilliseconds = 0;		
		
		/// <summary>The minimum minutes this type supports.</summary>
		public const ushort MinimumMinutes = 0;
		
		/// <summary>The minimum months this type supports.</summary>
		public const ushort MinimumMonths = 1;
		
		/// <summary>The minimum seconds this type supports.</summary>
		public const ushort MinimumSeconds = 0;
		
		/// <summary>The minimum years this type supports.</summary>
		public const ushort MinimumYears = 1601;
		#endregion
		
		#region Methods

		/// <summary>Compares two times for equality.</summary>
		/// <param name="obj">The second time to compare.</param>		
		/// <returns><see langword="true"/> if they are equal or <see langword="false"/> otherwise.</returns>
		public override bool Equals ( object obj )
		{
			SystemTime time = (SystemTime)obj;
			return this == time;
		}

		/// <summary>Gets the hashcode for the time.</summary>
		/// <returns>The hash code.</returns>
		public override int GetHashCode ( )
		{
			return base.GetHashCode();
		}

		/// <summary>Converts to a <see cref="DateTime"/>.</summary>
		/// <returns>Returns the data as a <see cref="DateTime"/>.</returns>
		[DebuggerStepThrough]
		public DateTime ToDateTime ( )
		{
		    return new DateTime(wYear, wMonth, wDay, wHour, wMinute, wSecond, wMilliseconds);
		}
		
		/// <summary>Converts to a string.</summary>
		/// <returns>Returns the data as a string.</returns>
		[DebuggerStepThrough]
        public override string ToString ( )
        {
            DateTime dt = ToDateTime();
            return dt.ToString();
        }

		/// <summary>Determines whether two instances are equal.</summary>
		/// <param name="value1">The first value.</param>
		/// <param name="value2">The second value.</param>
		/// <returns><see langword="true"/> if the instances are equal or <see langword="false"/> otherwise.</returns>
		public static bool operator== ( SystemTime value1, SystemTime value2 )
		{
			return (value1.wYear == value2.wYear) && (value1.wMonth == value2.wMonth) && (value1.wDay == value2.wDay) &&
				   (value1.wHour == value2.wHour) && (value1.wMinute == value2.wMinute) && (value1.wSecond == value2.wSecond) &&
				   (value1.wMilliseconds == value2.wMilliseconds);
		}

		/// <summary>Determines whether two instances are unequal.</summary>
		/// <param name="value1">The first value.</param>
		/// <param name="value2">The second value.</param>
		/// <returns><see langword="true"/> if the instances are not equal or <see langword="false"/> otherwise.</returns>
		public static bool operator != ( SystemTime value1, SystemTime value2 )
		{ return !(value1 == value2); }

		#endregion
		
		#endregion //Public Members
	}
}
