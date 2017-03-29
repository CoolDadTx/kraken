/*
 * Copyright (c) 2005 by Michael Taylor
 * All rights reserved.
 *
 */
#region Imports

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken.Win32;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken
{
    [TestClass]
    public partial class SystemTimeTests : UnitTest
    {
        #region Tests

        #region ToString

        [TestMethod]
        public void ToString_Now ()
        {
            DateTime dt = DateTime.Now;

            SystemTime st = new SystemTime(dt);
            Assert.AreEqual(dt.ToString(), st.ToString());
        }

        [TestMethod]
        public void ToString_Random ()
        {
            DateTime dt = new DateTime(2004, 1, 24, 5, 16, 42);

            SystemTime st = new SystemTime(dt);
            Assert.AreEqual(dt.ToString(), st.ToString());
        }
        #endregion

        #region ToDateTime

        [TestMethod]
        public void ToDateTime_Now ()
        {
            //Can't rely on Now because the seconds may be too precise for SystemTime.
            DateTime dtNow = DateTime.Now;
            DateTime dt = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, dtNow.Hour, dtNow.Minute, dtNow.Second, 10);

            SystemTime st = new SystemTime(dt);
            Assert.AreEqual(dt, st.ToDateTime());
        }

        [TestMethod]
        public void ToDateTime_Random ()
        {
            DateTime dt = new DateTime(2004, 1, 24, 5, 16, 42);

            SystemTime st = new SystemTime(dt);
            Assert.AreEqual(dt, st.ToDateTime());
        }
        #endregion

        #endregion

        #region Private Members

        private void VerifyTime ( SystemTime st, DateTime dt )
        {
            VerifyTime(st, dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond, (int)dt.DayOfWeek);
        }

        private void VerifyTime ( SystemTime st, int year, int month, int day,
                                  int hour, int minute, int second, int millisecond,
                                  int dow )
        {
            Assert.AreEqual(st.wYear, (ushort)year);
            Assert.AreEqual(st.wMonth, (ushort)month);
            Assert.AreEqual(st.wDay, (ushort)day);

            Assert.AreEqual(st.wHour, (ushort)hour);
            Assert.AreEqual(st.wMinute, (ushort)minute);
            Assert.AreEqual(st.wSecond, (ushort)second);
            Assert.AreEqual(st.wMilliseconds, (ushort)millisecond);

            Assert.AreEqual(st.wDayOfWeek, (ushort)dow);
        }
        #endregion
    }
}
