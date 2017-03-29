using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken
{
    [TestClass]
    public class MonthPartTests
    {
        #region Ctor
        
        [TestMethod]
        public void Ctor_IsValid ()
        {
            var expected = 10;

            var target = new MonthPart(expected);

            target.Month.Should().Be(expected);
        }

        [TestMethod]
        public void Ctor_IsTooSmall ()
        {
            Action action = () => new MonthPart(0);

            action.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void Ctor_IsTooLarge()
        {
            Action action = () => new MonthPart(14);

            action.ShouldThrowArgumentOutOfRangeException();
        }
        #endregion

        #region OfYear

        [TestMethod]
        public void OfYear_IsValid ()
        {
            var expectedYear = 2013;
            var expectedMonth = 4;

            var target = new MonthPart(expectedMonth).OfYear(expectedYear);

            target.Month.Should().Be(expectedMonth);
            target.Year.Should().Be(expectedYear);
        }

        [TestMethod]
        public void OfYear_TooSmall ()
        {
            Action action = () => new MonthPart(10).OfYear(Dates.MinimumYear - 1);

            action.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void OfYear_TooLarge ()
        {
            Action action = () => new MonthPart(10).OfYear(Dates.MaximumYear + 1);

            action.ShouldThrowArgumentOutOfRangeException();
        }
        #endregion

        #region IEquatable

        [TestMethod]
        public void OpEqual_IsTrue ()
        {
            var left = new MonthPart(4);
            var right = new MonthPart(4);

            var actual = left == right;

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void OpNotEqual_IsTrue ()
        {
            var left = new MonthPart(4);
            var right = new MonthPart(6);

            var actual = left != right;

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Equals_Object_IsNull ()
        {
            var actual = new MonthPart(4).Equals(null);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void Equals_Object_IsType ()
        {
            object target = new MonthPart(4);
            var actual = new MonthPart(4).Equals(target);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Equals_Object_IsNotType ()
        {
            var actual = new MonthPart(4).Equals("Hello");

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void Equals_Type_IsTrue ()
        {
            var actual = new MonthPart(5).Equals(new MonthPart(5));

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Equals_Type_IsFalse ()
        {
            var actual = new MonthPart(5).Equals(new MonthPart(6));

            actual.Should().BeFalse();
        }
        #endregion
    }
}
