using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken;

namespace Tests.P3Net.Kraken
{
    public partial class DateTimeExtensionsTests
    {
        #region InDateRange (DateTime)
        
        [TestMethod]
        public void InDateRange_ValueBetweenStartAndEnd ()
        {
            var start = new DateTime(2011, 10, 25, 12, 34, 56);
            var target = new DateTime(2011, 10, 25, 23, 45, 56);
            var end = new DateTime(2011, 10, 26, 1, 2, 3);
            
            //Act            
            var actual = target.InDateRange(start, end);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void InDateRange_ValueBeforeStartOnDifferentDay ()
        {
            var start = new DateTime(2011, 10, 25, 12, 34, 56);
            var target = new DateTime(2011, 10, 24, 23, 45, 56);
            var end = new DateTime(2011, 10, 26, 1, 2, 3);

            //Act            
            var actual = target.InDateRange(start, end);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void InDateRange_ValueBeforeStartOnSameDay ()
        {
            var start = new DateTime(2011, 10, 25, 12, 34, 56);
            var target = new DateTime(2011, 10, 25, 1, 2, 3);
            var end = new DateTime(2011, 10, 26, 1, 2, 3);

            //Act            
            var actual = target.InDateRange(start, end);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void InDateRange_ValueAfterEndOnSameDay ()
        {
            var start = new DateTime(2011, 10, 25, 12, 34, 56);
            var target = new DateTime(2011, 10, 26, 3, 4, 5);
            var end = new DateTime(2011, 10, 26, 1, 2, 3);

            //Act            
            var actual = target.InDateRange(start, end);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void InDateRange_ValueAfterEndOnDifferentDay ()
        {
            var start = new DateTime(2011, 10, 25, 12, 34, 56);
            var target = new DateTime(2011, 10, 27, 1, 2, 3);
            var end = new DateTime(2011, 10, 26, 1, 2, 3);

            //Act            
            var actual = target.InDateRange(start, end);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region InDateRange (Date)

        [TestMethod]
        public void InDateRange_Date_ValueBetweenStartAndEnd ()
        {
            var start = new Date(2011, 10, 25);
            var target = new DateTime(2011, 10, 25, 23, 45, 56);
            var end = new Date(2011, 10, 26);

            //Act            
            var actual = target.InDateRange(start, end);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void InDateRange_Date_ValueBeforeStartOnDifferentDay ()
        {
            var start = new Date(2011, 10, 25);
            var target = new DateTime(2011, 10, 24, 23, 45, 56);
            var end = new Date(2011, 10, 26);

            //Act            
            var actual = target.InDateRange(start, end);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void InDateRange_Date_ValueBeforeStartOnSameDay ()
        {
            var start = new Date(2011, 10, 25);
            var target = new DateTime(2011, 10, 25, 1, 2, 3);
            var end = new Date(2011, 10, 26);

            //Act            
            var actual = target.InDateRange(start, end);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void InDateRange_Date_ValueAfterEndOnSameDay ()
        {
            var start = new Date(2011, 10, 25);
            var target = new DateTime(2011, 10, 26, 3, 4, 5);
            var end = new Date(2011, 10, 26);

            //Act            
            var actual = target.InDateRange(start, end);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void InDateRange_Date_ValueAfterEndOnDifferentDay ()
        {
            var start = new Date(2011, 10, 25);
            var target = new DateTime(2011, 10, 27, 1, 2, 3);
            var end = new Date(2011, 10, 26);

            //Act            
            var actual = target.InDateRange(start, end);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region InRange (DateTime)

        [TestMethod]
        public void InRange_ValueBetweenStartAndEnd ()
        {
            var start = new DateTime(2011, 10, 25, 12, 34, 56);
            var target = new DateTime(2011, 10, 25, 23, 45, 56);
            var end = new DateTime(2011, 10, 26, 1, 2, 3);

            //Act            
            var actual = target.InRange(start, end);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void InRange_ValueBeforeStartOnDifferentDay ()
        {
            var start = new DateTime(2011, 10, 25, 12, 34, 56);
            var target = new DateTime(2011, 10, 24, 23, 45, 56);
            var end = new DateTime(2011, 10, 26, 1, 2, 3);

            //Act            
            var actual = target.InRange(start, end);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void InRange_ValueBeforeStartOnSameDay ()
        {
            var start = new DateTime(2011, 10, 25, 12, 34, 56);
            var target = new DateTime(2011, 10, 25, 1, 2, 3);
            var end = new DateTime(2011, 10, 26, 1, 2, 3);

            //Act            
            var actual = target.InRange(start, end);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void InRange_ValueAfterEndOnSameDay ()
        {
            var start = new DateTime(2011, 10, 25, 12, 34, 56);
            var target = new DateTime(2011, 10, 26, 3, 4, 5);
            var end = new DateTime(2011, 10, 26, 1, 2, 3);

            //Act            
            var actual = target.InRange(start, end);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void InRange_ValueAfterEndOnDifferentDay ()
        {
            var start = new DateTime(2011, 10, 25, 12, 34, 56);
            var target = new DateTime(2011, 10, 27, 1, 2, 3);
            var end = new DateTime(2011, 10, 26, 1, 2, 3);

            //Act            
            var actual = target.InRange(start, end);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region InRange (Date)

        [TestMethod]
        public void InRange_Date_ValueBetweenStartAndEnd ()
        {
            var start = new Date(2011, 10, 25);
            var target = new DateTime(2011, 10, 25, 23, 45, 56);
            var end = new Date(2011, 10, 26);

            //Act            
            var actual = target.InRange(start, end);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void InRange_Date_ValueBeforeStartOnDifferentDay ()
        {
            var start = new Date(2011, 10, 25);
            var target = new DateTime(2011, 10, 24, 23, 45, 56);
            var end = new Date(2011, 10, 26);

            //Act            
            var actual = target.InRange(start, end);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void InRange_Date_ValueBeforeStartOnSameDay ()
        {
            var start = new Date(2011, 10, 25);
            var target = start.At(new TimeSpan(1, 2, 3));
            var end = new Date(2011, 10, 26);

            //Act            
            var actual = target.InRange(start, end);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void InRange_Date_ValueAfterEndOnSameDay ()
        {
            var start = new Date(2011, 10, 25);
            var target = new DateTime(2011, 10, 26, 3, 4, 5);
            var end = new Date(2011, 10, 26);

            //Act            
            var actual = target.InRange(start, end);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void InRange_Date_ValueAfterEndOnDifferentDay ()
        {
            var start = new Date(2011, 10, 25);
            var target = new DateTime(2011, 10, 27, 1, 2, 3);
            var end = new Date(2011, 10, 26);

            //Act            
            var actual = target.InRange(start, end);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion
    }
}
