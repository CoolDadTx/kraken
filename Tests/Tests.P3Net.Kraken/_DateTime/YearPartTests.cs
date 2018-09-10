using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken
{
    [TestClass]
    public class YearPartTests
    {
        #region Ctor
        
        [TestMethod]
        public void Ctor_IsValid ()
        {
            var expected = 2000;

            var target = new YearPart(expected);

            target.Year.Should().Be(expected);
        }

        [TestMethod]
        public void Ctor_IsTooSmall ()
        {
            Action action = () => new YearPart(0);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void Ctor_IsTooLarge()
        {
            Action action = () => new YearPart(Dates.MaximumYear + 1);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }
        #endregion

        #region DayOfYear

        [TestMethod]
        public void DayOfYear_IsValid ()
        {
            var expectedYear = 2013;
            
            var target = new YearPart(expectedYear).DayOfYear(28);

            target.Month.Should().Be(1);
            target.Day.Should().Be(28);
            target.Year.Should().Be(expectedYear);
        }

        [TestMethod]
        public void DayOfYear_TooSmall ()
        {
            Action action = () => new YearPart(2000).DayOfYear(0);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void DayOfYear_TooLarge ()
        {
            Action action = () => new YearPart(2000).DayOfYear(400);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }
        #endregion

        #region FirstDayOfMonth

        [TestMethod]
        public void FirstDayOfMonth_IsValid ()
        {
            var target = new YearPart(2013);
            var expectedMonth = 5;

            var actual = target.FirstDayOfMonth(expectedMonth);

            actual.Year.Should().Be(target.Year);
            actual.Month.Should().Be(expectedMonth);
            actual.Day.Should().Be(1);
        }

        [TestMethod]
        public void FirstDayOfMonth_IsTooSmall ()
        {
            Action action = () => new YearPart(2013).FirstDayOfMonth(0);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void FirstDayOfMonth_IsTooLarge ()
        {
            Action action = () => new YearPart(2013).FirstDayOfMonth(14);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }
        #endregion

        #region LastDayOfMonth

        [TestMethod]
        public void LastDayOfMonth_IsValid ()
        {
            var target = new YearPart(2013);
            var expectedMonth = 5;

            var actual = target.LastDayOfMonth(expectedMonth);

            actual.Year.Should().Be(target.Year);
            actual.Month.Should().Be(expectedMonth);
            actual.Day.Should().Be(31);
        }

        [TestMethod]
        public void LastDayOfMonth_IsTooSmall ()
        {
            Action action = () => new YearPart(2013).LastDayOfMonth(0);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void LastDayOfMonth_IsTooLarge ()
        {
            Action action = () => new YearPart(2013).LastDayOfMonth(14);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }
        #endregion

        #region Month

        [TestMethod]
        public void Month_IsValid ()
        {
            var target = new YearPart(2000);
            var expectedMonth = Months.April;

            var actual = target.Month(expectedMonth);

            actual.Year.Should().Be(target.Year);
            actual.Month.Should().Be(expectedMonth);
        }

        [TestMethod]
        public void Month_TooSmall ()
        {
            Action action = () => new YearPart(2000).Month(0);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void Month_TooLarge ()
        {
            Action action = () => new YearPart(2000).Month(14);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }
        #endregion

        #region Months

        [TestMethod]
        public void January_NoDay ()
        {
            var expectedYear = 2010;
            
            var actual = new YearPart(expectedYear).January();

            actual.Year.Should().Be(expectedYear);
            actual.Month.Should().Be(Months.January);
        }

        [TestMethod]
        public void January_WithDay ()
        {
            var expectedYear = 2010;
            var expectedDay = 20;

            var actual = new YearPart(expectedYear).January(expectedDay);

            actual.Year.Should().Be(expectedYear);
            actual.Month.Should().Be(Months.January);
            actual.Day.Should().Be(expectedDay);
        }

        [TestMethod]
        public void February_NoDay ()
        {
            var expectedYear = 2010;

            var actual = new YearPart(expectedYear).February();

            actual.Year.Should().Be(expectedYear);
            actual.Month.Should().Be(Months.February);
        }

        [TestMethod]
        public void February_WithDay ()
        {
            var expectedYear = 2010;
            var expectedDay = 20;

            var actual = new YearPart(expectedYear).February(expectedDay);

            actual.Year.Should().Be(expectedYear);
            actual.Month.Should().Be(Months.February);
            actual.Day.Should().Be(expectedDay);
        }

        [TestMethod]
        public void March_NoDay ()
        {
            var expectedYear = 2010;

            var actual = new YearPart(expectedYear).March();

            actual.Year.Should().Be(expectedYear);
            actual.Month.Should().Be(Months.March);
        }

        [TestMethod]
        public void March_WithDay ()
        {
            var expectedYear = 2010;
            var expectedDay = 20;

            var actual = new YearPart(expectedYear).March(expectedDay);

            actual.Year.Should().Be(expectedYear);
            actual.Month.Should().Be(Months.March);
            actual.Day.Should().Be(expectedDay);
        }

        [TestMethod]
        public void April_NoDay ()
        {
            var expectedYear = 2010;

            var actual = new YearPart(expectedYear).April();

            actual.Year.Should().Be(expectedYear);
            actual.Month.Should().Be(Months.April);
        }

        [TestMethod]
        public void April_WithDay ()
        {
            var expectedYear = 2010;
            var expectedDay = 20;

            var actual = new YearPart(expectedYear).April(expectedDay);

            actual.Year.Should().Be(expectedYear);
            actual.Month.Should().Be(Months.April);
            actual.Day.Should().Be(expectedDay);
        }

        [TestMethod]
        public void May_NoDay ()
        {
            var expectedYear = 2010;

            var actual = new YearPart(expectedYear).May();

            actual.Year.Should().Be(expectedYear);
            actual.Month.Should().Be(Months.May);
        }

        [TestMethod]
        public void May_WithDay ()
        {
            var expectedYear = 2010;
            var expectedDay = 20;

            var actual = new YearPart(expectedYear).May(expectedDay);
            
            actual.Year.Should().Be(expectedYear);
            actual.Month.Should().Be(Months.May);
            actual.Day.Should().Be(expectedDay);
        }

        [TestMethod]
        public void June_NoDay ()
        {
            var expectedYear = 2010;

            var actual = new YearPart(expectedYear).June();

            actual.Year.Should().Be(expectedYear);
            actual.Month.Should().Be(Months.June);
        }

        [TestMethod]
        public void June_WithDay ()
        {
            var expectedYear = 2010;
            var expectedDay = 20;

            var actual = new YearPart(expectedYear).June(expectedDay);

            actual.Year.Should().Be(expectedYear);
            actual.Month.Should().Be(Months.June);
            actual.Day.Should().Be(expectedDay);
        }

        [TestMethod]
        public void July_NoDay ()
        {
            var expectedYear = 2010;

            var actual = new YearPart(expectedYear).July();

            actual.Year.Should().Be(expectedYear);
            actual.Month.Should().Be(Months.July);
        }

        [TestMethod]
        public void July_WithDay ()
        {
            var expectedYear = 2010;
            var expectedDay = 20;

            var actual = new YearPart(expectedYear).July(expectedDay);

            actual.Year.Should().Be(expectedYear);
            actual.Month.Should().Be(Months.July);
            actual.Day.Should().Be(expectedDay);
        }

        [TestMethod]
        public void August_NoDay ()
        {
            var expectedYear = 2010;

            var actual = new YearPart(expectedYear).August();

            actual.Year.Should().Be(expectedYear);
            actual.Month.Should().Be(Months.August);
        }

        [TestMethod]
        public void August_WithDay ()
        {
            var expectedYear = 2010;
            var expectedDay = 20;

            var actual = new YearPart(expectedYear).August(expectedDay);

            actual.Year.Should().Be(expectedYear);
            actual.Month.Should().Be(Months.August);
            actual.Day.Should().Be(expectedDay);
        }

        [TestMethod]
        public void September_NoDay ()
        {
            var expectedYear = 2010;

            var actual = new YearPart(expectedYear).September();

            actual.Year.Should().Be(expectedYear);
            actual.Month.Should().Be(Months.September);
        }

        [TestMethod]
        public void September_WithDay ()
        {
            var expectedYear = 2010;
            var expectedDay = 20;

            var actual = new YearPart(expectedYear).September(expectedDay);

            actual.Year.Should().Be(expectedYear);
            actual.Month.Should().Be(Months.September);
            actual.Day.Should().Be(expectedDay);
        }

        [TestMethod]
        public void October_NoDay ()
        {
            var expectedYear = 2010;

            var actual = new YearPart(expectedYear).October();

            actual.Year.Should().Be(expectedYear);
            actual.Month.Should().Be(Months.October);
        }

        [TestMethod]
        public void October_WithDay ()
        {
            var expectedYear = 2010;
            var expectedDay = 20;

            var actual = new YearPart(expectedYear).October(expectedDay);

            actual.Year.Should().Be(expectedYear);
            actual.Month.Should().Be(Months.October);
            actual.Day.Should().Be(expectedDay);
        }

        [TestMethod]
        public void November_NoDay ()
        {
            var expectedYear = 2010;

            var actual = new YearPart(expectedYear).November();

            actual.Year.Should().Be(expectedYear);
            actual.Month.Should().Be(Months.November);
        }

        [TestMethod]
        public void November_WithDay ()
        {
            var expectedYear = 2010;
            var expectedDay = 20;

            var actual = new YearPart(expectedYear).November(expectedDay);

            actual.Year.Should().Be(expectedYear);
            actual.Month.Should().Be(Months.November);
            actual.Day.Should().Be(expectedDay);
        }

        [TestMethod]
        public void December_NoDay ()
        {
            var expectedYear = 2010;

            var actual = new YearPart(expectedYear).December();

            actual.Year.Should().Be(expectedYear);
            actual.Month.Should().Be(Months.December);
        }

        [TestMethod]
        public void December_WithDay ()
        {
            var expectedYear = 2010;
            var expectedDay = 20;

            var actual = new YearPart(expectedYear).December(expectedDay);

            actual.Year.Should().Be(expectedYear);
            actual.Month.Should().Be(Months.December);
            actual.Day.Should().Be(expectedDay);
        }
        #endregion

        #region IEquatable

        [TestMethod]
        public void OpEqual_IsTrue ()
        {
            var left = new YearPart(2010);
            var right = new YearPart(2010);

            var actual = left == right;

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void OpNotEqual_IsTrue ()
        {
            var left = new YearPart(2000);
            var right = new YearPart(2010);

            var actual = left != right;

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Equals_Object_IsNull ()
        {
            var actual = new YearPart(2013).Equals(null);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void Equals_Object_IsType ()
        {
            object target = new YearPart(2013);
            var actual = new YearPart(2013).Equals(target);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Equals_Object_IsNotType ()
        {
            var actual = new YearPart(2000).Equals("Hello");

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void Equals_Type_IsTrue ()
        {
            var actual = new YearPart(2013).Equals(new YearPart(2013));

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Equals_Type_IsFalse ()
        {
            var actual = new YearPart(2012).Equals(new YearPart(2013));

            actual.Should().BeFalse();
        }
        #endregion
    }
}
