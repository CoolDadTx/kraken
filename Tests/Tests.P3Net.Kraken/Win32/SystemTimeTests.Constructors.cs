/*
 * Copyright (c) 2005 by Michael Taylor
 * All rights reserved.
 */
#if NET_FRAMEWORK

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken.Win32;

namespace Tests.P3Net.Kraken
{
	public partial class SystemTimeTests
	{
		#region Default

		/// <summary>Tests the default constructor.</summary>
		[TestMethod]
		public void Default ( )
		{
			SystemTime sysTime = new SystemTime();

			//As a struct the default constructor zero-inits so...
			VerifyTime(sysTime, 0, 0, 0, 0, 0, 0, 0, 0);
		}
		#endregion

		#region DateTime

		/// <summary>Tests the DateTime constructor with a valid value.</summary>
		[TestMethod]
		public void DateTime_Valid ( )
		{
			DateTime dt = new DateTime(2004, 12, 25, 8, 14, 32, 90);
			SystemTime sysTime = new SystemTime(dt);

			VerifyTime(sysTime, dt);
		}

		/// <summary>Tests the DateTime constructor with a valid value.</summary>
		[TestMethod]
		public void DateTime_Valid2 ( )
		{
			DateTime dt = new DateTime(1990, 1, 12, 23, 41, 13, 56);
			SystemTime sysTime = new SystemTime(dt);

			VerifyTime(sysTime, dt);
		}

		/// <summary>Tests the DateTime constructor with an invalid value.</summary>
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		[TestMethod]
		public void DateTime_InvalidMinYear ( )
		{
			DateTime dt = new DateTime(1600, 1, 1, 0, 0, 0, 0);
			SystemTime sysTime = new SystemTime(dt);						
		}

		/// <summary>Tests the DateTime constructor with an invalid value.</summary>
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		[TestMethod]
		public void DateTime_InvalidMaxYear ( )
		{
			DateTime dt = new DateTime(30827, 1, 1, 0, 0, 0, 0);
			SystemTime sysTime = new SystemTime(dt);
		}

		/// <summary>Tests the DateTime constructor with an invalid value.</summary>
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		[TestMethod]
		public void DateTime_InvalidMinMonth ( )
		{
			DateTime dt = new DateTime(2000, 0, 1, 0, 0, 0, 0);
			SystemTime sysTime = new SystemTime(dt);
		}

		/// <summary>Tests the DateTime constructor with an invalid value.</summary>
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		[TestMethod]
		public void DateTime_InvalidMaxMonth ( )
		{
			DateTime dt = new DateTime(2000, 13, 1, 0, 0, 0, 0);
			SystemTime sysTime = new SystemTime(dt);
		}

		/// <summary>Tests the DateTime constructor with an invalid value.</summary>
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		[TestMethod]
		public void DateTime_InvalidMinDay ( )
		{
			DateTime dt = new DateTime(2000, 1, 0, 0, 0, 0, 0);
			SystemTime sysTime = new SystemTime(dt);
		}

		/// <summary>Tests the DateTime constructor with an invalid value.</summary>
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		[TestMethod]
		public void DateTime_InvalidMaxDay ( )
		{
			DateTime dt = new DateTime(2000, 1, 32, 0, 0, 0, 0);
			SystemTime sysTime = new SystemTime(dt);
		}

		/// <summary>Tests the DateTime constructor with an invalid value.</summary>
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		[TestMethod]
		public void DateTime_InvalidMinHour ( )
		{
			DateTime dt = new DateTime(2000, 1, 1, -1, 0, 0, 0);
			SystemTime sysTime = new SystemTime(dt);
		}

		/// <summary>Tests the DateTime constructor with an invalid value.</summary>
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		[TestMethod]
		public void DateTime_InvalidMaxHour ( )
		{
			DateTime dt = new DateTime(2000, 1, 1, 25, 0, 0, 0);
			SystemTime sysTime = new SystemTime(dt);
		}

		/// <summary>Tests the DateTime constructor with an invalid value.</summary>
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		[TestMethod]
		public void DateTime_InvalidMinMinute ( )
		{
			DateTime dt = new DateTime(2000, 1, 1, 0, -1, 0, 0);
			SystemTime sysTime = new SystemTime(dt);
		}

		/// <summary>Tests the DateTime constructor with an invalid value.</summary>
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		[TestMethod]
		public void DateTime_InvalidMaxMinute ( )
		{
			DateTime dt = new DateTime(2000, 1, 1, 0, 60, 0, 0);
			SystemTime sysTime = new SystemTime(dt);
		}

		/// <summary>Tests the DateTime constructor with an invalid value.</summary>
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		[TestMethod]
		public void DateTime_InvalidMinSecond ( )
		{
			DateTime dt = new DateTime(2000, 1, 1, 0, 0, -1, 0);
			SystemTime sysTime = new SystemTime(dt);
		}

		/// <summary>Tests the DateTime constructor with an invalid value.</summary>
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		[TestMethod]
		public void DateTime_InvalidMaxSecond ( )
		{
			DateTime dt = new DateTime(2000, 1, 1, 0, 0, 60, 0);
			SystemTime sysTime = new SystemTime(dt);
		}

		/// <summary>Tests the DateTime constructor with an invalid value.</summary>
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		[TestMethod]
		public void DateTime_InvalidMinMillisecond ( )
		{
			DateTime dt = new DateTime(2000, 1, 1, 0, 0, -1, 0);
			SystemTime sysTime = new SystemTime(dt);
		}

		/// <summary>Tests the DateTime constructor with an invalid value.</summary>
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		[TestMethod]
		public void DateTime_InvalidMaxMillisecond ( )
		{
			DateTime dt = new DateTime(2000, 1, 1, 0, 0, 100, 0);
			SystemTime sysTime = new SystemTime(dt);
		}
		#endregion		
	}
}
#endif