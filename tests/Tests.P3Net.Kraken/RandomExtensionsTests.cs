/*
 * Copyright © 2017 Michael Taylor
 * https://www.michaeltaylorp3.net
 * All Rights Reserved
 */
using System;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken
{
    [TestClass]
    public class RandomExtensionsTests : UnitTest
    {
        #region NextDate

        [TestMethod]
        public void NextDate_NoDates ()
        {
            var actual = Random.NextDate();

            ((DateTime)actual).Should().NotBeAfter(Date.Today());
        }

        [TestMethod]
        public void NextDate_MinDate ()
        {
            var expectedMin = Dates.January(1, 2000);
            var actual = Random.NextDate(expectedMin);

            ((DateTime)actual).Should().NotBeBefore(expectedMin);
            ((DateTime)actual).Should().NotBeAfter(Date.Today());
        }

        [TestMethod]
        public void NextDate_WithDateRange ()
        {
            var expectedMin = Dates.January(1, 2000);
            var expectedMax = Dates.December(1, 2000);
            var actual = Random.NextDate(expectedMin, expectedMax);

            ((DateTime)actual).Should().NotBeBefore(expectedMin);
            ((DateTime)actual).Should().NotBeAfter(expectedMax);
        }

        [TestMethod]
        public void NextDate_MinIsMinDate ()
        {
            var expectedMin = Date.MinValue;
            var expectedMax = Dates.December(1, 2000);
            var actual = Random.NextDate(expectedMin, expectedMax);

            ((DateTime)actual).Should().NotBeBefore(expectedMin);
            ((DateTime)actual).Should().NotBeAfter(expectedMax);
        }

        [TestMethod]
        public void NextDate_MaxIsMaxDate ()
        {
            var expectedMin = Dates.January(1, 2000);
            var expectedMax = Date.MaxValue;
            var actual = Random.NextDate(expectedMin, expectedMax);

            ((DateTime)actual).Should().NotBeBefore(expectedMin);
            ((DateTime)actual).Should().NotBeAfter(expectedMax);
        }
        #endregion
    }
}
