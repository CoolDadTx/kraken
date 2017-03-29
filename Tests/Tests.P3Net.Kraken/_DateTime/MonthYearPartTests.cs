using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken
{
    [TestClass]
    public class MonthYearPartTests
    {
        #region Ctor
        
        [TestMethod]
        public void Ctor_IsValid ()
        {
            var expectedYear = 2000;
            var expectedMonth = 10;

            var target = new MonthYearPart(expectedMonth, expectedYear);

            target.Year.Should().Be(expectedYear);
            target.Month.Should().Be(expectedMonth);
        }

        [TestMethod]
        public void Ctor_YearTooSmall ()
        {
            Action action = () => new MonthYearPart(1, 0);

            action.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void Ctor_YearTooLarge()
        {
            Action action = () => new MonthYearPart(1, Dates.MaximumYear + 1);

            action.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void Ctor_MonthTooSmall ()
        {
            Action action = () => new MonthYearPart(0, 2013);

            action.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void Ctor_MonthTooLarge ()
        {
            Action action = () => new MonthYearPart(14, 2013);

            action.ShouldThrowArgumentOutOfRangeException();
        }
        #endregion

        #region Day

        [TestMethod]
        public void Day_IsValid ()
        {
            var expectedYear = 2013;
            var expectedMonth = 2;
            var expectedDay = 28;

            var target = new MonthYearPart(expectedMonth, expectedYear).Day(expectedDay);

            target.Month.Should().Be(expectedMonth);
            target.Day.Should().Be(expectedDay);
            target.Year.Should().Be(expectedYear);
        }

        [TestMethod]
        public void Day_TooSmall ()
        {
            Action action = () => new MonthYearPart(1, 2000).Day(0);

            action.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void Day_TooLarge ()
        {
            Action action = () => new MonthYearPart(1, 2000).Day(400);

            action.ShouldThrowArgumentOutOfRangeException();
        }
        #endregion

        [TestMethod]
        public void FirstDayOfMonth_IsValid ()
        {
            var target = new MonthYearPart(4, 2013);
            var actual = target.FirstDayOfMonth();

            actual.Year.Should().Be(target.Year);
            actual.Month.Should().Be(target.Month);
            actual.Day.Should().Be(1);
        }

        [TestMethod]
        public void LastDayOfMonth_IsValid ()
        {
            var target = new MonthYearPart(5, 2013);
            
            var actual = target.LastDayOfMonth();

            actual.Year.Should().Be(target.Year);
            actual.Month.Should().Be(target.Month);
            actual.Day.Should().Be(31);
        }
        
        #region IEquatable (Type)

        [TestMethod]
        public void OpEqual_IsTrue ()
        {
            var left = new MonthYearPart(5, 2010);
            var right = new MonthYearPart(5, 2010);

            var actual = left == right;

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void OpNotEqual_IsTrue ()
        {
            var left = new MonthYearPart(4, 2000);
            var right = new MonthYearPart(4, 2010);

            var actual = left != right;

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Equals_Type_IsTrue ()
        {
            var actual = new MonthYearPart(4, 2013).Equals(new MonthYearPart(4, 2013));

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Equals_Type_IsFalse ()
        {
            var actual = new MonthYearPart(5, 2012).Equals(new MonthYearPart(4, 2013));

            actual.Should().BeFalse();
        }
        
        #endregion

        #region IEquatable (General)
        
        [TestMethod]
        public void Equals_Object_IsNull ()
        {
            var actual = new MonthYearPart(4, 2013).Equals(null);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void Equals_Object_IsType ()
        {
            object target = new MonthYearPart(4, 2013);
            var actual = new MonthYearPart(4, 2013).Equals(target);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Equals_Object_IsNotType ()
        {
            var actual = new MonthYearPart(4, 2000).Equals("Hello");

            actual.Should().BeFalse();
        }
        #endregion        
    }
}
