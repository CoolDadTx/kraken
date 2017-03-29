using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken
{
    public partial class DateTests
    {
        #region Comparison (Date)
        
        [TestMethod]
        public void CompareTo_IsLess ()
        {
            var left = new Date(2013, 1, 10);
            var right = new Date(2013, 2, 10);

            var actual = left.CompareTo(right);

            actual.Should().BeLessThan(0);
        }

        [TestMethod]
        public void CompareTo_IsGreater ()
        {
            var left = new Date(2013, 3, 10);
            var right = new Date(2013, 2, 10);

            var actual = left.CompareTo(right);

            actual.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void CompareTo_AreEqual ()
        {
            var left = new Date(2013, 1, 10);
            var right = new Date(2013, 1, 10);

            var actual = left.CompareTo(right);

            actual.Should().Be(0);
        }
        
        [TestMethod]
        public void OpGreaterThan_IsTrue ()
        {
            var left = new Date(2013, 1, 10);
            var right = new Date(2012, 5, 10);

            var actual = left > right;

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void OpGreaterThanOrEqual_IsTrue ()
        {
            var left = new Date(2013, 1, 10);
            var right = left;

            var actual = left >= right;

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void OpLessThan_IsFalse ()
        {
            var left = new Date(2013, 1, 10);
            var right = new Date(2012, 5, 10);

            var actual = left < right;

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void OpLessThanOrEqual_IsTrue ()
        {
            var left = new Date(2012, 1, 10);
            var right = new Date(2013, 5, 10);

            var actual = left <= right;

            actual.Should().BeTrue();
        }
        #endregion

        #region Comparison (DateTime)

        [TestMethod]
        public void CompareTo_DateTime_IsLess ()
        {
            var left = new Date(2013, 1, 10);
            var right = new DateTime(2013, 2, 10);

            var actual = left.CompareTo(right);

            actual.Should().BeLessThan(0);
        }

        [TestMethod]
        public void CompareTo_DateTime_IsGreater ()
        {
            var left = new Date(2013, 3, 10);
            var right = new DateTime(2013, 2, 10);

            var actual = left.CompareTo(right);

            actual.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void CompareTo_DateTime_AreEqual ()
        {
            var left = new Date(2013, 1, 10);
            var right = left.At(new TimeSpan(4, 5, 6));

            var actual = left.CompareTo(right);

            actual.Should().Be(0);
        }

        [TestMethod]
        public void OpGreaterThan_DateDateTime_IsTrue ()
        {
            var left = new Date(2013, 1, 10);
            var right = new DateTime(2012, 5, 10);

            var actual = left > right;

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void OpGreaterThan_DateTimeDate_IsTrue ()
        {
            var left = new DateTime(2013, 1, 10);
            var right = new Date(2012, 5, 10);

            var actual = left > right;

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void OpGreaterThanOrEqual_DateDateTime_IsTrue ()
        {
            var left = new Date(2013, 1, 10);
            var right = left.At(new TimeSpan(6, 7, 8));

            var actual = left >= right;

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void OpGreaterThanOrEqual_DateTimeDate_IsTrue ()
        {
            var left = new DateTime(2013, 1, 10, 6, 7, 8);
            var right = new Date(left);

            var actual = left >= right;

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void OpLessThan_DateDateTime_IsFalse ()
        {
            var left = new Date(2013, 1, 10);
            var right = new DateTime(2012, 5, 10);

            var actual = left < right;

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void OpLessThan_DateTimeDate_IsFalse ()
        {
            var left = new DateTime(2013, 1, 10);
            var right = new Date(2012, 5, 10);

            var actual = left < right;

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void OpLessThanOrEqual_DateDateTime_IsTrue ()
        {
            var left = new Date(2012, 1, 10);
            var right = new DateTime(2013, 5, 10);

            var actual = left <= right;

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void OpLessThanOrEqual_DateTimeDate_IsTrue ()
        {
            var left = new DateTime(2012, 1, 10, 4, 5, 6);
            var right = new Date(2013, 5, 10);

            var actual = left <= right;

            actual.Should().BeTrue();
        }
        #endregion

        #region Comparison (Generic)

        [TestMethod]
        public void CompareTo_IsNull ()
        {
            var target = new Date(2013, 3, 4) as IComparable;

            var actual = target.CompareTo(null);

            actual.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void CompareTo_IsDate ()
        {
            var target = new Date(2013, 3, 4) as IComparable;

            var actual = target.CompareTo(new Date(2013, 5, 6));

            actual.Should().BeLessThan(0);
        }

        [TestMethod]
        public void CompareTo_IsNotDate ()
        {
            var target = new Date(2013, 3, 4) as IComparable;

            Action action = () => target.CompareTo(45);

            action.ShouldThrowArgumentException();
        }
        #endregion
    }
}
