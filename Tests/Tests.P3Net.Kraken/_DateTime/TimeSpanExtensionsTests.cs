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
    public partial class TimeSpanExtensionsTests : UnitTest
    {
        #region InRange

        [TestMethod]
        public void InRange_TimeBetweenStartAndEnd ()
        {
            var start = new TimeSpan(1, 2, 3);
            var target = new TimeSpan(12, 34, 56);
            var end = new TimeSpan(23, 59, 59);

            //Act
            var actual = target.InRange(start, end);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void InRange_TimeEqualsStart ()
        {
            var start = new TimeSpan(1, 2, 3);
            var target = start;
            var end = new TimeSpan(23, 59, 59);

            //Act
            var actual = target.InRange(start, end);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void InRange_TimeEqualsEnd ()
        {
            var start = new TimeSpan(1, 2, 3);            
            var end = new TimeSpan(23, 59, 59);
            var target = end;

            //Act
            var actual = target.InRange(start, end);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void InRange_TimeBeforeStart ()
        {
            var target = new TimeSpan(1, 2, 3);
            var start = new TimeSpan(12, 34, 56);            
            var end = new TimeSpan(23, 59, 59);
            
            //Act
            var actual = target.InRange(start, end);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void InRange_TimeAfterEnd ()
        {
            var start = new TimeSpan(1, 2, 3);
            var end = new TimeSpan(12, 34, 56);
            var target = new TimeSpan(23, 59, 59);
            
            //Act
            var actual = target.InRange(start, end);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region IsValidTimeOfDay

        [TestMethod]
        public void IsValidTimeOfDay_Midnight ()
        {
            var target = new TimeSpan();

            //Act
            var actual = target.IsValidTimeOfDay();

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsValidTimeOfDay_Noon ()
        {
            var target = new TimeSpan(12, 0, 0);

            //Act
            var actual = target.IsValidTimeOfDay();

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsValidTimeOfDay_MillisecondBeforeMidnight ()
        {
            var target = TimeSpanExtensions.MaximumTimeOfDay;

            //Act
            var actual = target.IsValidTimeOfDay();

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsValidTimeOfDay_24Hours ()
        {
            var target = new TimeSpan(24, 0, 0);

            //Act
            var actual = target.IsValidTimeOfDay();

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsValidTimeOfDay_After24Hours ()
        {
            var target = new TimeSpan(24, 0, 1);

            //Act
            var actual = target.IsValidTimeOfDay();

            //Assert
            actual.Should().BeFalse();
        }
        #endregion
    }
}
