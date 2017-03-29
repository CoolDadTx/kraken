#region Imports

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using Moq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken
{
    [TestClass]
    public class TimeRangeTests : UnitTest
    {
        #region Ctor

        [TestMethod]
        public void Ctor_StartAndEndAreValid ()
        {
            var start = new TimeSpan(1, 2, 3);
            var end = new TimeSpan(10, 11, 12);

            //Act            
            var target = new TimeRange(start, end);

            //Assert
            target.Start.Should().Be(start);
            target.End.Should().Be(end);
        }

        [TestMethod]
        public void Ctor_StartAndEndAreReversed ()
        {
            var start = new TimeSpan(1, 2, 3);
            var end = new TimeSpan(10, 11, 12);

            //Act            
            var target = new TimeRange(end, start);

            //Assert
            target.Start.Should().Be(start);
            target.End.Should().Be(end);
        }
        #endregion

        #region Attributes

        [TestMethod]
        public void Duration_StartAndEndAreValid ()
        {
            var start = new TimeSpan(1, 2, 3);
            var end = new TimeSpan(10, 11, 12);
            var expected = end - start;

            //Act            
            var target = new TimeRange(start, end);
            
            //Assert
            target.Duration.Should().Be(expected);
        }

        [TestMethod]
        public void Duration_StartAndEndAreReversed ()
        {
            var start = new TimeSpan(1, 2, 3);
            var end = new TimeSpan(10, 11, 12);
            var expected = end - start;

            //Act            
            var target = new TimeRange(end, start);

            //Assert
            target.Duration.Should().Be(expected);
        }
        #endregion

        #region Equals

        [TestMethod]
        public void Equals_SameValueIsEqual ()
        {
            //Act            
            var target = new TimeRange(new TimeSpan(1, 2, 3), new TimeSpan(10, 11, 12));
            var actual = target.Equals(target);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Equals_IdenticalRangesAreEqual ()
        {
            var start = new TimeSpan(1, 2, 3);
            var end = new TimeSpan(10, 11, 12);

            //Act            
            var target1 = new TimeRange(start, end);
            var target2 = new TimeRange(start, end);
            var actual = target1.Equals(target2);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Equals_DifferentRangesAreDifferent ()
        {
            //Act            
            var target1 = new TimeRange(new TimeSpan(1, 2, 3), new TimeSpan(10, 11, 12));
            var target2 = new TimeRange(new TimeSpan(2, 3, 4), new TimeSpan(11, 12, 13));
            var actual = target1.Equals(target2);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion        

        #region Operators

        [TestMethod]
        public void OpEqual_AreSame ()
        {
            var start = new TimeSpan(12, 34, 56);
            var end = new TimeSpan(14, 56, 23);

            var left = new TimeRange(start, end);
            var right = new TimeRange(start, end);

            //Act
            var actual = left == right;

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void OpEqual_AreDifferent ()
        {
            var start = new TimeSpan(12, 34, 56);
            var end = new TimeSpan(14, 56, 23);

            var left = new TimeRange(start, end);
            var right = new TimeRange(start.Add(new TimeSpan(1, 0, 0)), end);

            //Act
            var actual = left == right;

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void OpNotEqual_AreSame ()
        {
            var start = new TimeSpan(12, 34, 56);
            var end = new TimeSpan(14, 56, 23);

            var left = new TimeRange(start, end);
            var right = new TimeRange(start, end);

            //Act
            var actual = left != right;

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void OpNotEqual_AreDifferent ()
        {
            var start = new TimeSpan(12, 34, 56);
            var end = new TimeSpan(14, 56, 23);

            var left = new TimeRange(start, end);
            var right = new TimeRange(start.Add(new TimeSpan(1, 0, 0)), end);

            //Act
            var actual = left != right;

            //Assert
            actual.Should().BeTrue();
        }
        #endregion

        #region ToString

        [TestMethod]
        public void ToString_IsValid ()
        {
            var start = new TimeSpan(1, 2, 3);
            var end = new TimeSpan(10, 11, 12);
            var expected = String.Format("{0} - {1}", start, end);

            //Act
            var target = new TimeRange(start, end);
            var actual = target.ToString();

            //Assert
            actual.Should().Be(expected);
        }
        #endregion
    }
}
