using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken
{
    [TestClass]
    public partial class DateTests
    {
        #region Ctor
        
        [TestMethod]
        public void Ctor_WithDateTime ()
        {
            int expectedYear = 2013, expectedMonth = 4, expectedDay = 20;

            var target = new Date(new DateTime(expectedYear, expectedMonth, expectedDay, 20, 10, 5));

            target.Year.Should().Be(expectedYear);
            target.Month.Should().Be(expectedMonth);
            target.Day.Should().Be(expectedDay);
        }

        [TestMethod]
        public void Ctor_WithYearMonthDay ()
        {
            int expectedYear = 2013, expectedMonth = 4, expectedDay = 20;

            var target = new Date(expectedYear, expectedMonth, expectedDay);

            target.Year.Should().Be(expectedYear);
            target.Month.Should().Be(expectedMonth);
            target.Day.Should().Be(expectedDay);
        }
        #endregion

        #region Conversion
        
        [TestMethod]
        public void OpToDateTime_IsValid ()
        {            
            var expected = new Date(2013, 10, 12);

            DateTime actual = expected;

            actual.Year.Should().Be(expected.Year);
            actual.Month.Should().Be(expected.Month);
            actual.Day.Should().Be(expected.Day);
            actual.TimeOfDay.Should().Be(TimeSpan.Zero);
            
        }

        [TestMethod]
        public void OpFromDateTime_IsValid ()
        {
            var expected = new DateTime(2013, 10, 12);

            Date actual = (Date)expected;

            actual.Year.Should().Be(expected.Year);
            actual.Month.Should().Be(expected.Month);
            actual.Day.Should().Be(expected.Day);
        }
        
        [TestMethod]
        public void FromDayOfYear_IsValid ()
        {
            int expectedYear = 2013, expectedMonth = 5, expectedDay = 6;            
            var expectedDOY = new DateTime(expectedYear, expectedMonth, expectedDay).DayOfYear;

            var target = Date.FromDayOfYear(expectedYear, expectedDOY);

            target.Year.Should().Be(expectedYear);
            target.Month.Should().Be(expectedMonth);
            target.Day.Should().Be(expectedDay);
        }

        [TestMethod]        
        public void FromDayOfYear_DayOfYearTooSmall ()
        {
            Action action = () => Date.FromDayOfYear(2013, 0);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]        
        public void FromDayOfYear_DayOfYearTooLarge ()
        {
            Action action = () => Date.FromDayOfYear(2013, 370);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }
        #endregion

        #region Attributes

        [TestMethod]
        public void DayOfWeek_IsValid ()
        {
            var expectedDayOfWeek = DayOfWeek.Monday;

            var target = new Date(2013, 3, 25);

            target.DayOfWeek.Should().Be(expectedDayOfWeek);
        }

        [TestMethod]
        public void DayOfYear_IsValid ()
        {
            var expectedDayOfYear = 84;        

            var target = new Date(2013, 3, 25);

            target.DayOfYear.Should().Be(expectedDayOfYear);
        }

        [TestMethod]
        public void IsLeapDay_IsTrue ()
        {
            var target = new Date(2012, 2, 29);

            target.IsLeapDay.Should().BeTrue();
        }

        [TestMethod]
        public void IsLeapDay_IsLeapYearButNotDay ()
        {
            var target = new Date(2012, 2, 28);

            target.IsLeapDay.Should().BeFalse();
        }

        [TestMethod]
        public void IsLeapDay_IsFalse ()
        {
            var target = new Date(2013, 2, 28);

            target.IsLeapDay.Should().BeFalse();
        }

        [TestMethod]
        public void IsLeapYear_IsTrue ()
        {
            var target = new Date(2012, 4, 2);

            target.IsLeapYear.Should().BeTrue();
        }

        [TestMethod]
        public void IsLeapYear_IsFalse ()
        {
            var target = new Date(2013, 2, 28);

            target.IsLeapYear.Should().BeFalse();
        }

        [TestMethod]
        public void IsSet_IsTrue ()
        {
            var target = new Date(2013, 1, 1);

            target.IsSet.Should().BeTrue();
        }

        [TestMethod]
        public void IsSet_IsFalse ()
        {
            Date.None.IsSet.Should().BeFalse();
        }
        #endregion

        #region Add...

        [TestMethod]
        public void AddDays_Positive ()
        {
            var target = new Date(2013, 3, 25);
            var expected = new Date(2013, 3, 27);

            var actual = target.AddDays(2);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AddDays_Negative ()
        {
            var target = new Date(2013, 3, 27);
            var expected = new Date(2013, 3, 25);

            var actual = target.AddDays(-2);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AddMonths_Positive ()
        {
            var target = new Date(2013, 3, 25);
            var expected = new Date(2013, 6, 25);

            var actual = target.AddMonths(3);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AddMonths_Negative ()
        {
            var target = new Date(2013, 6, 25);
            var expected = new Date(2013, 3, 25);

            var actual = target.AddMonths(-3);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AddWeeks_Positive ()
        {
            var target = new Date(2013, 3, 1);
            var expected = new Date(2013, 3, 22);

            var actual = target.AddWeeks(3);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AddWeeks_Negative ()
        {
            var target = new Date(2013, 3, 25);
            var expected = new Date(2013, 3, 4);

            var actual = target.AddWeeks(-3);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AddYears_Positive ()
        {
            var target = new Date(2013, 3, 25);
            var expected = new Date(2016, 3, 25);

            var actual = target.AddYears(3);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AddYears_Negative ()
        {
            var target = new Date(2013, 3, 25);
            var expected = new Date(2010, 3, 25);

            var actual = target.AddYears(-3);

            actual.Should().Be(expected);
        }
        #endregion

        [TestMethod]
        public void At_IsValid ()
        {
            var expected = new TimeSpan(20, 10, 5);
            var target = new Date(2013, 4, 5);

            var actual = target.At(expected);

            actual.TimeOfDay.Should().Be(expected);
        }

        [TestMethod]
        public void At_Int32_IsValid ()
        {
            var expected = new TimeSpan(20, 10, 5);
            var target = new Date(2013, 4, 5);

            var actual = target.At(expected.Hours, expected.Minutes, expected.Seconds);

            actual.TimeOfDay.Should().Be(expected);
        }

        #region Difference

        [TestMethod]
        public void Difference_BeginIsEarlier()
        {
            var begin = new Date(2013, 2, 1);
            var end = new Date(2013, 2, 20);
            var expected = 19;

            var actual = begin.Difference(end);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Difference_BeginIsLater()
        {
            var begin = new Date(2013, 2, 20);
            var end = new Date(2013, 2, 2);
            var expected = -18;

            var actual = begin.Difference(end);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Difference_AreSame()
        {
            var begin = new Date(2013, 2, 20);
            var end = begin;

            var actual = begin.Difference(end);

            actual.Should().Be(0);
        }

        [TestMethod]
        public void Difference_DateTime_BeginIsEarlier()
        {
            var begin = new Date(2013, 2, 1);
            var end = new DateTime(2013, 2, 20);
            var expected = 19;

            var actual = begin.Difference(end);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Difference_DateTime_BeginIsLater()
        {
            var begin = new Date(2013, 2, 20);
            var end = new DateTime(2013, 2, 2);
            var expected = -18;

            var actual = begin.Difference(end);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Difference_DateTime_AreSame()
        {
            var begin = new Date(2013, 2, 20);
            DateTime end = begin;

            var actual = begin.Difference(end);

            actual.Should().Be(0);
        }

        [TestMethod]
        public void Difference_Date_DifferentYears()
        {
            var begin = new Date(2013, 1, 1);
            var end = new Date(2014, 1, 1);

            var actual = begin.Difference(end);
            actual.Should().Be(365);
        }

        [TestMethod]
        public void Difference_Date_DifferentYears2()
        {
            var begin = new Date(2015, 1, 1);
            var end = new Date(2014, 1, 1);

            var actual = begin.Difference(end);
            actual.Should().Be(-365);
        }

        [TestMethod]
        public void Difference_DateTime_DifferentYears()
        {
            var begin = new Date(2013, 1, 1);
            var end = new DateTime(2014, 1, 1);

            var actual = begin.Difference(end);
            actual.Should().Be(365);
        }

        [TestMethod]
        public void Difference_DateTime_DifferentYears2()
        {
            var begin = new Date(2015, 1, 1);
            var end = new DateTime(2014, 1, 1);

            var actual = begin.Difference(end);
            actual.Should().Be(-365);
        }

        [TestMethod]
        public void Difference_Date_Close()
        {
            var begin = new Date(2014, 1, 1);
            var end = new Date(2014, 1, 2);

            var actual = begin.Difference(end);
            actual.Should().Be(1);
        }

        [TestMethod]
        public void Difference_DateTime_Close()
        {
            var begin = new Date(2013, 1, 2);
            var end = new DateTime(2013, 1, 1);

            var actual = begin.Difference(end);
            actual.Should().Be(-1);
        }

        [TestMethod]
        public void Difference_DateTime_SameExceptTime()
        {
            var begin = new Date(2013, 1, 2);
            var end = new DateTime(2013, 1, 2, 12, 11, 10);

            var actual = begin.Difference(end);
            actual.Should().Be(0);
        }
        #endregion

        #region DifferenceAbsolute

        [TestMethod]
        public void DifferenceAbsolute_BeginIsEarlier()
        {
            var begin = new Date(2013, 2, 1);
            var end = new Date(2013, 2, 20);
            var expected = 19;

            var actual = begin.DifferenceAbsolute(end);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void DifferenceAbsolute_BeginIsLater()
        {
            var begin = new Date(2013, 2, 20);
            var end = new Date(2013, 2, 2);
            var expected = 18;

            var actual = begin.DifferenceAbsolute(end);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void DifferenceAbsolute_AreSame()
        {
            var begin = new Date(2013, 2, 20);
            var end = begin;

            var actual = begin.DifferenceAbsolute(end);

            actual.Should().Be(0);
        }

        [TestMethod]
        public void DifferenceAbsolute_DateTime_BeginIsEarlier()
        {
            var begin = new Date(2013, 2, 1);
            var end = new DateTime(2013, 2, 20);
            var expected = 19;

            var actual = begin.DifferenceAbsolute(end);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void DifferenceAbsolute_DateTime_BeginIsLater()
        {
            var begin = new Date(2013, 2, 20);
            var end = new DateTime(2013, 2, 2);
            var expected = 18;

            var actual = begin.DifferenceAbsolute(end);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void DifferenceAbsolute_DateTime_AreSame()
        {
            var begin = new Date(2013, 2, 20);
            DateTime end = begin;

            var actual = begin.DifferenceAbsolute(end);

            actual.Should().Be(0);
        }

        [TestMethod]
        public void DifferenceAbsolute_Date_DifferentYears()
        {
            var begin = new Date(2013, 1, 1);
            var end = new Date(2014, 1, 1);

            var actual = begin.DifferenceAbsolute(end);
            actual.Should().Be(365);
        }

        [TestMethod]
        public void DifferenceAbsolute_Date_DifferentYears2()
        {
            var begin = new Date(2015, 1, 1);
            var end = new Date(2014, 1, 1);

            var actual = begin.DifferenceAbsolute(end);
            actual.Should().Be(365);
        }

        [TestMethod]
        public void DifferenceAbsolute_DateTime_DifferentYears()
        {
            var begin = new Date(2013, 1, 1);
            var end = new DateTime(2014, 1, 1);

            var actual = begin.DifferenceAbsolute(end);
            actual.Should().Be(365);
        }

        [TestMethod]
        public void DifferenceAbsolute_DateTime_DifferentYears2()
        {
            var begin = new Date(2015, 1, 1);
            var end = new DateTime(2014, 1, 1);

            var actual = begin.DifferenceAbsolute(end);
            actual.Should().Be(365);
        }

        [TestMethod]
        public void DifferenceAbsolute_Date_Close()
        {
            var begin = new Date(2014, 1, 1);
            var end = new Date(2014, 1, 2);

            var actual = begin.DifferenceAbsolute(end);
            actual.Should().Be(1);
        }

        [TestMethod]
        public void DifferenceAbsolute_DateTime_Close()
        {
            var begin = new Date(2013, 1, 2);
            var end = new DateTime(2013, 1, 1);

            var actual = begin.DifferenceAbsolute(end);
            actual.Should().Be(1);
        }

        [TestMethod]
        public void DifferenceAbsolute_DateTime_SameExceptTime()
        {
            var begin = new Date(2013, 1, 2);
            var end = new DateTime(2013, 1, 2, 12, 11, 10);

            var actual = begin.DifferenceAbsolute(end);
            actual.Should().Be(0);
        }
        #endregion

        [TestMethod]
        public void FirstDayOfMonth_IsValid ()
        {
            var target = new Date(2013, 1, 25);

            var actual = target.FirstDayOfMonth();

            actual.Year.Should().Be(target.Year);
            actual.Month.Should().Be(target.Month);
            actual.Day.Should().Be(1);
        }

        #region IsBetween

        #region Date/Date

        [TestMethod]
        public void IsBetween_IsTrue ()
        {
            var begin = new Date(2013, 2, 1);
            var end = new Date(2013, 3, 1);

            var actual = new Date(2013, 2, 15).IsBetween(begin, end);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsBetween_IsBegin ()
        {
            var begin = new Date(2013, 2, 1);
            var end = new Date(2013, 3, 1);

            var actual = begin.IsBetween(begin, end);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsBetween_IsEnd ()
        {
            var begin = new Date(2013, 2, 1);
            var end = new Date(2013, 3, 1);

            var actual = end.IsBetween(begin, end);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsBetween_IsFalse ()
        {
            var begin = new Date(2013, 2, 1);
            var end = new Date(2013, 3, 1);

            var actual = new Date(2013, 4, 1).IsBetween(begin, end);

            actual.Should().BeFalse();
        }
        #endregion

        #region Date/DateTime

        [TestMethod]
        public void IsBetween_DateDateTime_IsTrue ()
        {
            var begin = new Date(2013, 2, 1);
            var end = new DateTime(2013, 3, 1);

            var actual = new Date(2013, 2, 15).IsBetween(begin, end);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsBetween_DateDateTime_IsBegin ()
        {
            var begin = new Date(2013, 2, 1);
            var end = new DateTime(2013, 3, 1);

            var actual = begin.IsBetween(begin, end);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsBetween_DateDateTime_IsEnd ()
        {
            var begin = new Date(2013, 2, 1);
            var end = new DateTime(2013, 3, 1);

            var actual = new Date(end).IsBetween(begin, end);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsBetween_DateDateTime_IsAfterEnd ()
        {
            var begin = new Date(2013, 2, 1);
            var end = new DateTime(2013, 3, 1);

            var actual = new Date(2013, 4, 1).IsBetween(begin, end);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsBetween_DateDateTime_IsBeforeStart ()
        {
            var begin = new Date(2013, 2, 1);
            var end = new DateTime(2013, 3, 1);

            var actual = new Date(2013, 1, 1).IsBetween(begin, end);

            actual.Should().BeFalse();
        }
        #endregion

        #region DateTime/Date

        [TestMethod]
        public void IsBetween_DateTimeDate_IsTrue ()
        {
            var begin = new DateTime(2013, 2, 1);
            var end = new Date(2013, 3, 1);

            var actual = new Date(2013, 2, 15).IsBetween(begin, end);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsBetween_DateTimeDate_IsBegin ()
        {
            var begin = new DateTime(2013, 2, 1);
            var end = new Date(2013, 3, 1);

            var actual = new Date(begin).IsBetween(begin, end);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsBetween_DateTimeDate_IsEnd ()
        {
            var begin = new DateTime(2013, 2, 1);
            var end = new Date(2013, 3, 1);

            var actual = end.IsBetween(begin, end);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsBetween_DateTimeDate_IsAfterEnd ()
        {
            var begin = new DateTime(2013, 2, 1);
            var end = new Date(2013, 3, 1);

            var actual = new Date(2013, 4, 1).IsBetween(begin, end);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsBetween_DateTimeDate_IsBeforeStart ()
        {
            var begin = new DateTime(2013, 2, 1);
            var end = new Date(2013, 3, 1);

            var actual = new Date(2013, 1, 1).IsBetween(begin, end);

            actual.Should().BeFalse();
        }
        #endregion

        #region DateTime/DateTime

        [TestMethod]
        public void IsBetween_DateTimeDateTime_IsTrue ()
        {
            var begin = new DateTime(2013, 2, 1);
            var end = new DateTime(2013, 3, 1);

            var actual = new Date(2013, 2, 15).IsBetween(begin, end);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsBetween_DateTimeDateTime_IsBegin ()
        {
            var begin = new DateTime(2013, 2, 1);
            var end = new DateTime(2013, 3, 1);

            var actual = new Date(begin).IsBetween(begin, end);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsBetween_DateTimeDateTime_IsEnd ()
        {
            var begin = new DateTime(2013, 2, 1);
            var end = new DateTime(2013, 3, 1);

            var actual = new Date(end).IsBetween(begin, end);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsBetween_DateTimeDateTime_IsAfterEnd ()
        {
            var begin = new DateTime(2013, 2, 1);
            var end = new DateTime(2013, 3, 1);

            var actual = new Date(2013, 4, 1).IsBetween(begin, end);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsBetween_DateTimeDateTime_IsBeforeStart ()
        {
            var begin = new DateTime(2013, 2, 1);
            var end = new DateTime(2013, 3, 1);

            var actual = new Date(2013, 1, 1).IsBetween(begin, end);

            actual.Should().BeFalse();
        }
        #endregion

        #endregion

        [TestMethod]
        public void LastDayOfMonth_IsValid ()
        {
            var target = new Date(2013, 1, 25);

            var actual = target.LastDayOfMonth();

            actual.Year.Should().Be(target.Year);
            actual.Month.Should().Be(target.Month);
            actual.Day.Should().Be(31);
        }

        [TestMethod]
        public void LastMonth_IsValid ()
        {
            var target = new Date(2013, 4, 20);

            var actual = target.LastMonth();

            actual.Year.Should().Be(target.Year);
            actual.Month.Should().Be(3);
            actual.Day.Should().Be(target.Day);
        }

        [TestMethod]
        public void NextMonth_IsValid ()
        {
            var target = new Date(2013, 4, 20);

            var actual = target.NextMonth();

            actual.Year.Should().Be(target.Year);
            actual.Month.Should().Be(5);
            actual.Day.Should().Be(target.Day);
        }

        [TestMethod]
        public void Today_IsValid ()
        {
            var today = DateTime.Today;

            var actual = Date.Today();

            actual.Day.Should().Be(today.Day);
            actual.Month.Should().Be(today.Month);
            actual.Year.Should().Be(today.Year);
        }

        [TestMethod]
        public void Tomorrow_IsValid ()
        {
            var target = new Date(2013, 4, 20);

            var actual = target.Tomorrow();

            actual.Should().Be(target.AddDays(1));
        }

        #region ToNullableDateTime

        [TestMethod]
        public void ToNullableDateTime_IsNotNull ()
        {
            var expected = new DateTime(2013, 2, 3);

            var target = new Date(expected);
            var actual = target.ToNullableDateTime();

            actual.HasValue.Should().BeTrue();
            actual.Value.Should().Be(expected);
        }

        [TestMethod]
        public void ToNullableDateTime_IsNull ()
        {
            var actual = Date.None.ToNullableDateTime();

            actual.HasValue.Should().BeFalse();
        }
        #endregion   

        #region ToString...

        [TestMethod]
        public void ToLongDateString ()
        {
            var target = new Date(2013, 1, 4);

            var actual = target.ToLongDateString();

            actual.Should().Contain("January");
            actual.Should().Contain("4");
            actual.Should().Contain("2013");
        }

        [TestMethod]
        public void ToShortDateString ()
        {
            var target = new Date(2013, 5, 4);

            var actual = target.ToShortDateString();

            actual.Should().Contain("5");
            actual.Should().Contain("4");
            actual.Should().Contain("2013");
        }

        [TestMethod]
        public void ToString_NoFormat ()
        {
            var target = new Date(2013, 1, 4);

            var actual = target.ToString();

            actual.Should().Contain("January");
            actual.Should().Contain("4");
            actual.Should().Contain("2013");
        }

        [TestMethod]
        public void ToString_WithFormat ()
        {
            var target = new Date(2013, 1, 4);

            var actual = target.ToString("dd MM yyyy");

            actual.Should().Contain("04 01 2013");
        }

        [TestMethod]
        public void ToString_WithFormatAndProvider ()
        {
            var target = new Date(2013, 1, 4);

            var actual = target.ToString("dd MM yyyy", System.Globalization.DateTimeFormatInfo.CurrentInfo);

            actual.Should().Be("04 01 2013");
        }
        #endregion

        [TestMethod]
        public void Yesterday_IsValid ()
        {
            var target = new Date(2013, 4, 20);

            var actual = target.Yesterday();

            actual.Should().Be(target.AddDays(-1));
        }
    }
}
