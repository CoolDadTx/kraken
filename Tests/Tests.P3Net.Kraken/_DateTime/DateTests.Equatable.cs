using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken;

namespace Tests.P3Net.Kraken
{
    public partial class DateTests
    {
        #region IEquatable (Date)

        [TestMethod]
        public void Equals_Date_IsTrue ()
        {
            var target = new Date(2013, 4, 5);

            var actual = target.Equals(target);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void OpEqual_Date_IsFalse ()
        {
            var target = new Date(2013, 4, 5);

            var actual = target == new Date(2013, 4, 6);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void OpNotEqual_Date_IsFalse ()
        {
            var target = new Date(2013, 4, 5);

            var actual = target != new Date(2013, 4, 5);

            actual.Should().BeFalse();
        }
        #endregion

        #region IEquatable (DateTime)

        [TestMethod]
        public void Equals_DateTime_IsTrue ()
        {
            var target = new Date(2013, 4, 5);

            var actual = target.Equals(target.At(new TimeSpan(10, 12, 14)));

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void OpEqual_DateDateTime_IsFalse ()
        {
            var actual = new Date(2013, 4, 5) == new DateTime(2013, 4, 6);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void OpEqual_DateTimeDate_IsFalse ()
        {
            var actual = new DateTime(2013, 6, 7) == new Date(2013, 4, 5);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void OpNotEqual_DateDateTime_IsFalse ()
        {
            var actual = new Date(2013, 4, 5) != new DateTime(2013, 4, 5, 7, 8, 9);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void OpNotEqual_DateTimeDate_IsFalse ()
        {
            var target = new Date(2013, 4, 5);

            var actual = target.At(new TimeSpan(10, 12, 14)) != target;

            actual.Should().BeFalse();
        }
        #endregion

        #region IEquatable (General)

        [TestMethod]
        public void Equals_Object_IsNull ()
        {
            var target = new Date(2013, 4, 5);

            var actual = target.Equals(null);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void Equals_Object_IsType ()
        {
            var target = new Date(2013, 4, 5);

            var actual = target.Equals(new Date(2013, 4, 5));

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Equals_Object_IsNotType ()
        {
            var target = new Date(2013, 4, 5);

            var actual = target.Equals("Hello");

            actual.Should().BeFalse();
        }
        #endregion
    }
}
