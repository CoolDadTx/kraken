using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken;

namespace Tests.P3Net.Kraken
{
    [TestClass]
    public class ComparableExtensionsTests
    {
        #region Between

        [TestMethod]
        public void Between_IsBelowMinimum ()
        {
            var target = -10;

            var actual = target.Between(1, 100);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void Between_IsMinimum ()
        {
            var target = 1;

            var actual = target.Between(1, 100);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Between_IsTrue ()
        {
            var target = 50;

            var actual = target.Between(1, 100);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Between_IsAboveMaximum ()
        {
            var target = 101;

            var actual = target.Between(1, 100);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void Between_IsMaximum ()
        {
            var target = 100;

            var actual = target.Between(1, 100);

            actual.Should().BeTrue();
        }
        #endregion

        #region BetweenExclusive

        [TestMethod]
        public void BetweenExclusive_IsBelowMinimum ()
        {
            var target = -10;

            var actual = target.BetweenExclusive(1, 100);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void BetweenExclusive_IsMinimum ()
        {
            var target = 1;

            var actual = target.BetweenExclusive(1, 100);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void BetweenExclusive_IsTrue ()
        {
            var target = 50;

            var actual = target.BetweenExclusive(1, 100);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void BetweenExclusive_IsAboveMaximum ()
        {
            var target = 101;

            var actual = target.BetweenExclusive(1, 100);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void BetweenExclusive_IsMaximum ()
        {
            var target = 100;

            var actual = target.BetweenExclusive(1, 100);

            actual.Should().BeFalse();
        }
        #endregion
    }
}
