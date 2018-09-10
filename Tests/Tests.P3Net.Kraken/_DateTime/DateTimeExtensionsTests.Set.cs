using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken
{
    public partial class DateTimeExtensionsTests
    {
        #region SetDate

        [TestMethod]
        public void SetDate_DateTime_DateIsTypical ()
        {
            var target = new DateTime(2011, 10, 26, 12, 34, 56);
            var newDate = new Date(2010, 9, 25);
            var expected = new DateTime(2010, 9, 25, 12, 34, 56);

            //Act
            var actual = target.SetDate(newDate);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void SetDate_Int32_DateIsTypical ()
        {
            var target = new DateTime(2011, 10, 26, 12, 34, 56);
            var newDate = new DateTime(2010, 9, 25);
            var expected = new DateTime(2010, 9, 25, 12, 34, 56);

            //Act
            var actual = target.SetDate(newDate.Year, newDate.Month, newDate.Day);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void SetDate_Int32_YearIsInvalid ()
        {
            Action action = () => new DateTime(2011, 10, 26).SetDate(10000, 1, 1);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void SetDate_Int32_MonthIsInvalid ()
        {
            Action action = () => new DateTime(2011, 10, 26).SetDate(2010, 50, 1);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void SetDate_Int32_DayIsInvalid ()
        {
            Action action = () => new DateTime(2011, 10, 26).SetDate(2010, 1, 76);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }
        #endregion

        #region SetTimeOfDay

        [TestMethod]
        public void SetTimeOfDay_DateTime_DateIsTypical ()
        {
            var target = new DateTime(2011, 10, 26, 12, 34, 56);
            var newTime = new TimeSpan(0, 10, 11, 12, 34);
            var expected = new DateTime(2011, 10, 26, 10, 11, 12, 34);

            //Act
            var actual = target.SetTimeOfDay(newTime);

            //Assert
            actual.Should().Be(expected);
        }
        
        [TestMethod]
        public void SetTimeOfDay_Int32_DateIsTypical ()
        {
            var target = new DateTime(2011, 10, 26, 12, 34, 56);
            var newTime = new TimeSpan(0, 10, 11, 12, 34);
            var expected = new DateTime(2011, 10, 26, 10, 11, 12, 34);

            //Act
            var actual = target.SetTimeOfDay(newTime.Hours, newTime.Minutes, newTime.Seconds, newTime.Milliseconds);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void SetTimeOfDay_Int32_NoMiliseconds ()
        {
            var target = new DateTime(2011, 10, 26, 12, 34, 56);
            var newTime = new TimeSpan(0, 10, 11, 12);
            var expected = new DateTime(2011, 10, 26, 10, 11, 12);

            //Act
            var actual = target.SetTimeOfDay(newTime.Hours, newTime.Minutes, newTime.Seconds);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion
    }
}