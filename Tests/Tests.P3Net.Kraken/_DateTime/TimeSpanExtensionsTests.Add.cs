#region Imports

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using Moq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken;
#endregion

namespace Tests.P3Net.Kraken
{
    public partial class TimeSpanExtensionsTests
    {
        #region AddDays

        [TestMethod]
        public void AddDays_AddValue ()
        {
            var target = new TimeSpan(1, 2, 3, 4);
            var expected = new TimeSpan(2, 2, 3, 4);

            //Act
            var actual = target.AddDays(1);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AddDays_SubtractValue ()
        {
            var target = new TimeSpan(4, 2, 3, 4);
            var expected = new TimeSpan(3, 2, 3, 4);

            //Act
            var actual = target.AddDays(-1);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AddDays_ValueTooLarge ()
        {
            Action action = () => TimeSpan.MaxValue.AddDays(1);

            action.Should().Throw<OverflowException>();
        }

        [TestMethod]
        public void AddDays_ValueTooSmall ()
        {
            Action action = () => TimeSpan.MinValue.AddDays(-1);

            action.Should().Throw<OverflowException>();
        }
        #endregion

        #region AddHours

        [TestMethod]
        public void AddHours_AddValue ()
        {
            var target = new TimeSpan(2, 3, 4);
            var expected = new TimeSpan(3, 3, 4);

            //Act
            var actual = target.AddHours(1);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AddHours_SubtractValue ()
        {
            var target = new TimeSpan(2, 2, 3);
            var expected = new TimeSpan(1, 2, 3);

            //Act
            var actual = target.AddHours(-1);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AddHours_ValueTooLarge ()
        {
            Action action = () => TimeSpan.MaxValue.AddHours(1);

            action.Should().Throw<OverflowException>();
        }

        [TestMethod]
        public void AddHours_ValueTooSmall ()
        {
            Action action = () => TimeSpan.MinValue.AddHours(-1);

            action.Should().Throw<OverflowException>();
        }
        #endregion

        #region AddMinutes

        [TestMethod]
        public void AddMinutes_AddValue ()
        {
            var target = new TimeSpan(1, 2, 3);
            var expected = new TimeSpan(1, 3, 3);

            //Act
            var actual = target.AddMinutes(1);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AddMinutes_SubtractValue ()
        {
            var target = new TimeSpan(2, 3, 4);
            var expected = new TimeSpan(2, 2, 4);

            //Act
            var actual = target.AddMinutes(-1);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AddMinutes_ValueTooLarge ()
        {
            Action action = () => TimeSpan.MaxValue.AddMinutes(1);

            action.Should().Throw<OverflowException>();
        }

        [TestMethod]
        public void AddMinutes_ValueTooSmall ()
        {
            Action action = () => TimeSpan.MinValue.AddMinutes(-1);

            action.Should().Throw<OverflowException>();
        }
        #endregion

        #region AddSeconds

        [TestMethod]
        public void AddSeconds_AddValue ()
        {
            var target = new TimeSpan(1, 2, 3);
            var expected = new TimeSpan(1, 2, 4);

            //Act
            var actual = target.AddSeconds(1);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AddSeconds_SubtractValue ()
        {
            var target = new TimeSpan(1, 2, 3);
            var expected = new TimeSpan(1, 2, 2);

            //Act
            var actual = target.AddSeconds(-1);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AddSeconds_ValueTooLarge ()
        {
            Action action = () => TimeSpan.MaxValue.AddSeconds(1);

            action.Should().Throw<OverflowException>();
        }

        [TestMethod]
        public void AddSeconds_ValueTooSmall ()
        {
            Action action = () => TimeSpan.MinValue.AddSeconds(-1);

            action.Should().Throw<OverflowException>();
        }
        #endregion

        #region AddMilliseconds

        [TestMethod]
        public void AddMilliseconds_AddValue ()
        {
            var target = new TimeSpan(1, 2, 3, 4, 5);
            var expected = new TimeSpan(1, 2, 3, 4, 6);

            //Act
            var actual = target.AddMilliseconds(1);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AddMilliseconds_SubtractValue ()
        {
            var target = new TimeSpan(1, 2, 3, 4, 5);
            var expected = new TimeSpan(1, 2, 3, 4, 4);

            //Act
            var actual = target.AddMilliseconds(-1);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AddMilliseconds_ValueTooLarge ()
        {
            Action action = () => TimeSpan.MaxValue.AddMilliseconds(1);

            action.Should().Throw<OverflowException>();
        }

        [TestMethod]
        public void AddMilliseconds_ValueTooSmall ()
        {
            Action action = () => TimeSpan.MinValue.AddMilliseconds(-1);

            action.Should().Throw<OverflowException>();
        }
        #endregion

        #region AddTicks

        [TestMethod]
        public void AddTicks_AddValue ()
        {
            var target = new TimeSpan(123456789);
            var expected = new TimeSpan(123456790);

            //Act
            var actual = target.AddTicks(1);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AddTicks_SubtractValue ()
        {
            var target = new TimeSpan(123456789);
            var expected = new TimeSpan(123456788);

            //Act
            var actual = target.AddTicks(-1);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AddTicks_ValueTooLarge ()
        {
            Action action = () => TimeSpan.MaxValue.AddTicks(1);

            action.Should().Throw<OverflowException>();
        }

        [TestMethod]
        public void AddTicks_ValueTooSmall ()
        {
            Action action = () => TimeSpan.MinValue.AddTicks(-1);

            action.Should().Throw<OverflowException>();
        }
        #endregion
    }
}
