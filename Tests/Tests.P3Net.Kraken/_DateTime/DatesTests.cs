using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken
{
    [TestClass]
    public class DatesTests
    {
        #region Month
        
        [TestMethod]
        public void Month_IsValid ()
        {
            var expectedMonth = 10;

            var target = Dates.Month(expectedMonth);

            target.Month.Should().Be(expectedMonth);            
        }

        [TestMethod]
        public void Month_IsTooSmall ()
        {
            Action action = () => Dates.Month(0);

            action.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void Month_IsTooLarge ()
        {
            Action action = () => Dates.Month(20);

            action.ShouldThrowArgumentOutOfRangeException();
        }
        #endregion

        #region Months

        [TestMethod]
        public void January_IsValid ()
        {
            Dates.January().Month.Should().Be(Months.January);
        }

        [TestMethod]
        public void January_WithDayYear ()
        {
            var expectedDay = 10;
            var expectedYear = 2013;

            var target = Dates.January(expectedDay, expectedYear);

            target.Day.Should().Be(expectedDay);            
            target.Year.Should().Be(expectedYear);
            target.Month.Should().Be(Months.January);
        }
        
        [TestMethod]
        public void February_IsValid ()
        {
            Dates.February().Month.Should().Be(Months.February);
        }

        [TestMethod]
        public void February_WithDayYear ()
        {
            var expectedDay = 10;
            var expectedYear = 2013;

            var target = Dates.February(expectedDay, expectedYear);

            target.Day.Should().Be(expectedDay);
            target.Year.Should().Be(expectedYear);
            target.Month.Should().Be(Months.February);
        }

        [TestMethod]
        public void March_IsValid ()
        {
            Dates.March().Month.Should().Be(Months.March);
        }

        [TestMethod]
        public void March_WithDayYear ()
        {
            var expectedDay = 10;
            var expectedYear = 2013;

            var target = Dates.March(expectedDay, expectedYear);

            target.Day.Should().Be(expectedDay);
            target.Year.Should().Be(expectedYear);
            target.Month.Should().Be(Months.March);
        }

        [TestMethod]
        public void April_IsValid ()
        {
            Dates.April().Month.Should().Be(Months.April);
        }

        [TestMethod]
        public void April_WithDayYear ()
        {
            var expectedDay = 10;
            var expectedYear = 2013;

            var target = Dates.April(expectedDay, expectedYear);

            target.Day.Should().Be(expectedDay);
            target.Year.Should().Be(expectedYear);
            target.Month.Should().Be(Months.April);
        }

        [TestMethod]
        public void May_IsValid ()
        {
            Dates.May().Month.Should().Be(Months.May);
        }

        [TestMethod]
        public void May_WithDayYear ()
        {
            var expectedDay = 10;
            var expectedYear = 2013;

            var target = Dates.May(expectedDay, expectedYear);

            target.Day.Should().Be(expectedDay);
            target.Year.Should().Be(expectedYear);
            target.Month.Should().Be(Months.May);
        }

        [TestMethod]
        public void June_IsValid ()
        {
            Dates.June().Month.Should().Be(Months.June);
        }

        [TestMethod]
        public void June_WithDayYear ()
        {
            var expectedDay = 10;
            var expectedYear = 2013;

            var target = Dates.June(expectedDay, expectedYear);

            target.Day.Should().Be(expectedDay);
            target.Year.Should().Be(expectedYear);
            target.Month.Should().Be(Months.June);
        }

        [TestMethod]
        public void July_IsValid ()
        {
            Dates.July().Month.Should().Be(Months.July);
        }

        [TestMethod]
        public void July_WithDayYear ()
        {
            var expectedDay = 10;
            var expectedYear = 2013;

            var target = Dates.July(expectedDay, expectedYear);

            target.Day.Should().Be(expectedDay);
            target.Year.Should().Be(expectedYear);
            target.Month.Should().Be(Months.July);
        }

        [TestMethod]
        public void August_IsValid ()
        {
            Dates.August().Month.Should().Be(Months.August);
        }

        [TestMethod]
        public void August_WithDayYear ()
        {
            var expectedDay = 10;
            var expectedYear = 2013;

            var target = Dates.August(expectedDay, expectedYear);

            target.Day.Should().Be(expectedDay);
            target.Year.Should().Be(expectedYear);
            target.Month.Should().Be(Months.August);
        }

        [TestMethod]
        public void September_IsValid ()
        {
            Dates.September().Month.Should().Be(Months.September);
        }

        [TestMethod]
        public void September_WithDayYear ()
        {
            var expectedDay = 10;
            var expectedYear = 2013;

            var target = Dates.September(expectedDay, expectedYear);

            target.Day.Should().Be(expectedDay);
            target.Year.Should().Be(expectedYear);
            target.Month.Should().Be(Months.September);
        }

        [TestMethod]
        public void October_IsValid ()
        {
            Dates.October().Month.Should().Be(Months.October);
        }

        [TestMethod]
        public void October_WithDayYear ()
        {
            var expectedDay = 10;
            var expectedYear = 2013;

            var target = Dates.October(expectedDay, expectedYear);

            target.Day.Should().Be(expectedDay);
            target.Year.Should().Be(expectedYear);
            target.Month.Should().Be(Months.October);
        }

        [TestMethod]
        public void November_IsValid ()
        {
            Dates.November().Month.Should().Be(Months.November);
        }

        [TestMethod]
        public void November_WithDayYear ()
        {
            var expectedDay = 10;
            var expectedYear = 2013;

            var target = Dates.November(expectedDay, expectedYear);

            target.Day.Should().Be(expectedDay);
            target.Year.Should().Be(expectedYear);
            target.Month.Should().Be(Months.November);
        }

        [TestMethod]
        public void December_IsValid ()
        {
            Dates.December().Month.Should().Be(Months.December);
        }

        [TestMethod]
        public void December_WithDayYear ()
        {
            var expectedDay = 10;
            var expectedYear = 2013;

            var target = Dates.December(expectedDay, expectedYear);

            target.Day.Should().Be(expectedDay);
            target.Year.Should().Be(expectedYear);
            target.Month.Should().Be(Months.December);
        }
        #endregion

        #region Year

        [TestMethod]
        public void Year_IsValid ()
        {
            var expected = 2013;

            var target = Dates.Year(expected);

            target.Year.Should().Be(expected);
        }

        [TestMethod]
        public void Year_IsTooSmall ()
        {
            Action action = () => Dates.Year(Dates.MinimumYear - 1);

            action.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]        
        public void Year_IsTooLarge ()
        {
            Action action = () => Dates.Year(Dates.MaximumYear + 1);

            action.ShouldThrowArgumentOutOfRangeException();
        }
        #endregion
    }
}
