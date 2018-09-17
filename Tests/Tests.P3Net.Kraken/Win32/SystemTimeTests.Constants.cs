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
		#region Days

		/// <summary>Tests the constant ranges.</summary>
		[TestMethod]
		public void DaysConstant_Range ( )
		{
			Assert.IsTrue(SystemTime.MaximumDays == 31, "Breaking change made.");
			Assert.IsTrue(SystemTime.MinimumDays == 0, "Breaking change made.");
		}
		#endregion

		#region Hours

		/// <summary>Tests the constant ranges.</summary>
		[TestMethod]
		public void HoursConstant_Range ( )
		{
			Assert.IsTrue(SystemTime.MaximumHours == 23, "Breaking change made.");
			Assert.IsTrue(SystemTime.MinimumHours == 0, "Breaking change made.");
		}
		#endregion
		
		#region Milliseconds

		/// <summary>Tests the constant ranges.</summary>
		[TestMethod]
		public void MillisecondsConstant_Range ( )
		{
			Assert.IsTrue(SystemTime.MaximumMilliseconds == 999, "Breaking change made.");
			Assert.IsTrue(SystemTime.MinimumMilliseconds == 0, "Breaking change made.");
		}
		#endregion

		#region Minutes

		/// <summary>Tests the constant ranges.</summary>
		[TestMethod]
		public void MinutesConstant_Range ( )
		{
			Assert.IsTrue(SystemTime.MaximumMinutes == 59, "Breaking change made.");
			Assert.IsTrue(SystemTime.MinimumMinutes == 0, "Breaking change made.");
		}
		#endregion

		#region Months

		/// <summary>Tests the constant ranges.</summary>
		[TestMethod]
		public void MonthsConstant_Range ( )
		{
			Assert.IsTrue(SystemTime.MaximumMonths == 12, "Breaking change made.");
			Assert.IsTrue(SystemTime.MinimumMonths == 1, "Breaking change made.");
		}
		#endregion

		#region Seconds

		/// <summary>Tests the constant ranges.</summary>
		[TestMethod]
		public void SecondsConstant_Range ( )
		{
			Assert.IsTrue(SystemTime.MaximumSeconds == 59, "Breaking change made.");
			Assert.IsTrue(SystemTime.MinimumSeconds == 0, "Breaking change made.");
		}
		#endregion

		#region Years

		/// <summary>Tests the constant ranges.</summary>
		[TestMethod]
		public void YearsConstant_Range ( )
		{
			Assert.IsTrue(SystemTime.MaximumYears == 30827, "Breaking change made.");
			Assert.IsTrue(SystemTime.MinimumYears == 1601, "Breaking change made.");
		}
		#endregion
	}
}
#endif