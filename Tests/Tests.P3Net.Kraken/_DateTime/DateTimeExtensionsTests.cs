using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken
{
    [TestClass]
    public partial class DateTimeExtensionsTests : UnitTest
    {
        #region EndOfDay
        
        [TestMethod]
        public void EndOfDay_MidDay ()
        {
            var target = new DateTime(2011, 10, 26, 13, 23, 45);
            var expected = new Date(target).At(new TimeSpan(23, 59, 59));

            //Act            
            var actual = target.EndOfDay();

            //Assert
            actual.Should().BeCloseTo(expected, 1000);
        }
        #endregion

        #region NextDay

        [TestMethod]
        public void NextDay_ValueIsTypical ()
        {
            var target = new DateTime(2011, 10, 26, 12, 34, 56);
            var expected = new DateTime(2011, 10, 27);

            //Act
            var actual = target.NextDay();

            //Assert
            actual.Year.Should().Be(expected.Year);
            actual.Month.Should().Be(expected.Month);
            actual.Day.Should().Be(expected.Day);
        }

        [TestMethod]        
        public void NextDay_ValueIsMax ()
        {
            Action action = () => DateTime.MaxValue.NextDay();

            action.ShouldThrowArgumentOutOfRangeException();
        }
        #endregion

        #region NextDaySameTime

        [TestMethod]
        public void NextDaySameTime_ValueIsTypical ()
        {
            var target = new DateTime(2011, 10, 26, 12, 34, 56);
            var expected = new DateTime(2011, 10, 27, 12, 34, 56);

            //Act
            var actual = target.NextDaySameTime();

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]        
        public void NextDaySameTime_ValueIsMax ()
        {
            Action action = () => DateTime.MaxValue.NextDaySameTime();

            action.ShouldThrowArgumentOutOfRangeException();
        }
        #endregion

        #region PreviousDay

        [TestMethod]
        public void PreviousDay_ValueIsTypical ()
        {
            var target = new DateTime(2011, 10, 26, 12, 34, 56);
            var expected = new DateTime(2011, 10, 25);

            //Act
            var actual = target.PreviousDay();

            //Assert
            actual.Year.Should().Be(expected.Year);
            actual.Month.Should().Be(expected.Month);
            actual.Day.Should().Be(expected.Day);
        }

        [TestMethod]
        public void PreviousDay_ValueIsMin ()
        {
            Action action = () => DateTime.MinValue.PreviousDay();

            action.ShouldThrowArgumentOutOfRangeException();
        }
        #endregion

        #region PreviousDaySameTime

        [TestMethod]
        public void PreviousDaySameTime_ValueIsTypical ()
        {
            var target = new DateTime(2011, 10, 26, 12, 34, 56);
            var expected = new DateTime(2011, 10, 25, 12, 34, 56);

            //Act
            var actual = target.PreviousDaySameTime();

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PreviousDaySameTime_ValueIsMin ()
        {
            Action action = () => DateTime.MinValue.PreviousDaySameTime();

            action.ShouldThrowArgumentOutOfRangeException();
        }
        #endregion

        #region StartOfDay

        [TestMethod]
        public void StartOfDay_MidDay ()
        {
            var target = new DateTime(2011, 10, 26, 13, 23, 45);
            var expected = new DateTime(2011, 10, 26);

            //Act            
            var actual = target.StartOfDay();

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region ToDate

        [TestMethod]
        public void ToDate_IsValid ()
        {
            var expected = new Date(2013, 3, 4);

            var target = new DateTime(expected.Year, expected.Month, expected.Day, 12, 1, 2);
            var actual = target.ToDate();

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToDate_IsMinValue ()
        {
            var actual = DateTime.MinValue.ToDate();

            actual.Should().Be(Date.None);
        }

        [TestMethod]
        public void ToDate_NullableIsValid ()
        {
            var expected = new Date(2013, 3, 4);

            DateTime? target = new DateTime(expected.Year, expected.Month, expected.Day, 12, 1, 2);
            var actual = target.ToDate();

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToDate_NullableIsNull ()
        {
            DateTime? target = null;

            var actual = target.ToDate();

            actual.Should().Be(Date.None);
        }
        #endregion

        #region ToNullableDateTime

        [TestMethod]
        public void ToNullableDateTime_IsNotNull ()
        {
            var target = new DateTime(2013, 4, 5);
            var actual = target.ToNullableDateTime();

            actual.Should().HaveValue();
        }

        [TestMethod]
        public void ToNullableDateTime_IsMinValue ()
        {
            var actual = DateTime.MinValue.ToNullableDateTime();

            actual.Should().NotHaveValue();
        }
        #endregion
    }
}