using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken
{
    [TestClass]
    public class DateRangeTests : UnitTest
    {
        #region Ctor

        [TestMethod]
        public void Ctor_StartAndEndAreValid ()
        {
            var start = new DateTime(2011, 10, 26);
            var end = new DateTime(2011, 11, 26);

            //Act            
            var target = new DateRange(start, end);

            //Assert
            target.Start.Should().Be(new Date(start));
            target.End.Should().Be(new Date(end));
        }

        [TestMethod]
        public void Ctor_StartAndEndAreReversed ()
        {
            var start = new DateTime(2011, 11, 25);
            var end = new DateTime(2011, 10, 26);

            //Act            
            var target = new DateRange(end, start);

            //Assert
            target.Start.Should().Be(new Date(end.Date));
            target.End.Should().Be(new Date(start.Date));
        }

        [TestMethod]
        public void Ctor_Date_StartAndEndAreValid ()
        {
            var start = new Date(2011, 10, 26);
            var end = new Date(2011, 10, 26);

            //Act            
            var target = new DateRange(start, end);

            //Assert
            target.Start.Should().Be(start);
            target.End.Should().Be(end);
        }

        [TestMethod]
        public void Ctor_Date_StartAndEndAreReversed ()
        {
            var start = new Date(2011, 10, 25);
            var end = new Date(2011, 10, 26);

            //Act            
            var target = new DateRange(end, start);

            //Assert
            target.Start.Should().Be(start);
            target.End.Should().Be(end);
        }
        #endregion

        #region Attributes

        [TestMethod]
        public void Duration_StartAndEndAreValid ()
        {
            var start = new Date(2011, 10, 26);
            var end = new Date(2011, 10, 26);
            var expected = end.Difference(start);

            //Act            
            var target = new DateRange(start, end);
            
            //Assert
            target.Duration.Should().Be(expected);
        }

        [TestMethod]
        public void Duration_StartAndEndAreReversed ()
        {
            var start = new Date(2011, 10, 25);
            var end = new Date(2011, 10, 26);
            var expected = end.Difference(start);

            //Act            
            var target = new DateRange(end, start);

            //Assert
            target.Duration.Should().Be(expected);
        }
        #endregion

        #region Contains

        [TestMethod]
        public void Contains_IsTrue ()
        {
            var target = new DateRange(Dates.January(1, 2000), Dates.December(31, 2000));

            var actual = target.Contains(Dates.March(4, 2000));

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Contains_IsFalse ()
        {
            var target = new DateRange(Dates.January(1, 2000), Dates.December(31, 2000));

            var actual = target.Contains(Dates.March(4, 2001));

            actual.Should().BeFalse();
        }
        #endregion

        #region Equals

        [TestMethod]
        public void Equals_SameValueIsEqual ()
        {
            //Act            
            var target = new DateRange(new DateTime(2011, 10, 26, 1, 2, 3), new DateTime(2011, 10, 26, 10, 11, 12));
            var actual = target.Equals(target);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Equals_IdenticalRangesAreEqual ()
        {
            var start = new DateTime(2011, 10, 26, 1, 2, 3);
            var end = new DateTime(2011, 10, 26, 10, 11, 12);

            //Act            
            var target1 = new DateRange(start, end);
            var target2 = new DateRange(start, end);
            var actual = target1.Equals(target2);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Equals_DifferByTimesAreSame ()
        {
            //Act            
            var target1 = new DateRange(new DateTime(2011, 10, 23, 1, 2, 3), new DateTime(2011, 10, 26, 10, 11, 12));
            var target2 = new DateRange(new DateTime(2011, 10, 23, 2, 3, 4), new DateTime(2011, 10, 26, 11, 12, 13));
            var actual = target1.Equals(target2);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Equals_DifferentRangesAreDifferent ()
        {
            //Act            
            var target1 = new DateRange(new DateTime(2011, 10, 26, 1, 2, 3), new DateTime(2011, 10, 26, 10, 11, 12));
            var target2 = new DateRange(new DateTime(2011, 10, 25, 2, 3, 4), new DateTime(2011, 10, 25, 11, 12, 13));
            var actual = target1.Equals(target2);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion        

        #region Intersect

        [TestMethod]
        public void Intersect_LeftBeforeRight ()
        {
            var left = new DateRange(Dates.January(1, 2000), Dates.December(31, 2000));
            var right = new DateRange(Dates.January(1, 2001), Dates.December(31, 2001));

            var actual = left.Intersect(right);

            actual.Should().Be(DateRange.Empty);
        }

        [TestMethod]
        public void Intersect_LeftContainsRightStart ()
        {
            var left = new DateRange(Dates.January(1, 2000), Dates.December(31, 2000));
            var right = new DateRange(Dates.June(1, 2000), Dates.December(31, 2001));

            var actual = left.Intersect(right);

            actual.Start.Should().Be(right.Start);
            actual.End.Should().Be(left.End);
        }

        [TestMethod]
        public void Intersect_LeftContainsRight ()
        {
            var left = new DateRange(Dates.January(1, 2000), Dates.December(31, 2000));
            var right = new DateRange(Dates.June(1, 2000), Dates.December(1, 2000));

            var actual = left.Intersect(right);

            actual.Start.Should().Be(right.Start);
            actual.End.Should().Be(right.End);
        }

        [TestMethod]
        public void Intersect_LeftContainsRightEnd ()
        {
            var left = new DateRange(Dates.January(1, 2000), Dates.December(31, 2000));
            var right = new DateRange(Dates.June(1, 1999), Dates.March(1, 2000));

            var actual = left.Intersect(right);

            actual.Start.Should().Be(left.Start);
            actual.End.Should().Be(right.End);
        }

        [TestMethod]
        public void Intersect_LeftAfterRight ()
        {
            var left = new DateRange(Dates.January(1, 2002), Dates.December(31, 2002));
            var right = new DateRange(Dates.January(1, 2001), Dates.December(31, 2001));

            var actual = left.Intersect(right);

            actual.Should().Be(DateRange.Empty);
        }

        [TestMethod]
        public void Intersect_AreSame ()
        {
            var left = new DateRange(Dates.January(1, 2002), Dates.December(31, 2002));

            var actual = left.Intersect(left);

            actual.Should().Be(left);
        }
        #endregion

        #region Join

        [TestMethod]
        public void Join_LeftBeforeRight ()
        {
            var left = new DateRange(Dates.January(1, 2000), Dates.December(31, 2000));
            var right = new DateRange(Dates.January(1, 2001), Dates.December(31, 2001));

            var actual = left.Join(right);

            actual.Start.Should().Be(left.Start);
            actual.End.Should().Be(right.End);
        }

        [TestMethod]
        public void Join_LeftInsideRight ()
        {
            var left = new DateRange(Dates.January(1, 2000), Dates.December(31, 2000));
            var right = new DateRange(Dates.June(1, 1999), Dates.December(31, 2001));

            var actual = left.Join(right);

            actual.Start.Should().Be(right.Start);
            actual.End.Should().Be(right.End);
        }

        [TestMethod]
        public void Join_LeftContainsRightEnd ()
        {
            var left = new DateRange(Dates.January(1, 2000), Dates.December(31, 2000));
            var right = new DateRange(Dates.June(1, 1999), Dates.March(1, 2000));

            var actual = left.Join(right);

            actual.Start.Should().Be(right.Start);
            actual.End.Should().Be(left.End);
        }

        [TestMethod]
        public void Join_LeftAfterRight ()
        {
            var left = new DateRange(Dates.January(1, 2002), Dates.December(31, 2002));
            var right = new DateRange(Dates.January(1, 2001), Dates.December(31, 2001));

            var actual = left.Join(right);

            actual.Start.Should().Be(right.Start);
            actual.End.Should().Be(left.End);
        }

        [TestMethod]
        public void Join_AreSame ()
        {
            var left = new DateRange(Dates.January(1, 2002), Dates.December(31, 2002));

            var actual = left.Join(left);

            actual.Should().Be(left);
        }
        #endregion

        #region Overlaps

        [TestMethod]
        public void Overlaps_LeftBeforeRight ()
        {
            var left = new DateRange(Dates.January(1, 2000), Dates.December(31, 2000));
            var right = new DateRange(Dates.January(1, 2001), Dates.December(31, 2001));

            var actual = left.Overlaps(right);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void Overlaps_LeftContainsRightStart ()
        {
            var left = new DateRange(Dates.January(1, 2000), Dates.December(31, 2000));
            var right = new DateRange(Dates.June(1, 2000), Dates.December(31, 2001));

            var actual = left.Overlaps(right);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Overlaps_LeftInsideRight ()
        {
            var left = new DateRange(Dates.January(1, 2000), Dates.December(31, 2000));
            var right = new DateRange(Dates.June(1, 1999), Dates.December(31, 2001));

            var actual = left.Overlaps(right);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Overlaps_LeftContainsRightEnd ()
        {
            var left = new DateRange(Dates.January(1, 2000), Dates.December(31, 2000));
            var right = new DateRange(Dates.June(1, 1999), Dates.March(1, 2000));

            var actual = left.Overlaps(right);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Overlaps_LeftAfterRight ()
        {
            var left = new DateRange(Dates.January(1, 2002), Dates.December(31, 2002));
            var right = new DateRange(Dates.January(1, 2001), Dates.December(31, 2001));

            var actual = left.Overlaps(right);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void Overlaps_AreSame ()
        {
            var left = new DateRange(Dates.January(1, 2002), Dates.December(31, 2002));

            var actual = left.Overlaps(left);

            actual.Should().BeTrue();
        }
        #endregion

        #region Operators

        [TestMethod]
        public void OpEqual_AreSame ()
        {
            var start = new DateTime(2011, 10, 25);
            var end = new DateTime(2011, 10, 26);

            var left = new DateRange(start, end);
            var right = new DateRange(start, end);

            //Act
            var actual = left == right;

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void OpEqual_AreDifferent ()
        {
            var start = new DateTime(2011, 10, 25);
            var end = new DateTime(2011, 10, 26);

            var left = new DateRange(start, end);
            var right = new DateRange(start.AddDays(1), end);

            //Act
            var actual = left == right;

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void OpNotEqual_AreSame ()
        {
            var start = new DateTime(2011, 10, 25);
            var end = new DateTime(2011, 10, 26);

            var left = new DateRange(start, end);
            var right = new DateRange(start, end);

            //Act
            var actual = left != right;

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void OpNotEqual_AreDifferent ()
        {
            var start = new DateTime(2011, 10, 25);
            var end = new DateTime(2011, 10, 26);

            var left = new DateRange(start, end);
            var right = new DateRange(start.AddDays(1), end);

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
            var start = new DateTime(2011, 10, 26, 1, 2, 3);
            var end = new DateTime(2011, 10, 26, 10, 11, 12);
            var expected = String.Format("{0} - {1}", start.ToShortDateString(), end.ToShortDateString());

            //Act
            var target = new DateRange(start, end);
            var actual = target.ToString();

            //Assert
            actual.Should().Be(expected);
        }
        #endregion
    }
}
