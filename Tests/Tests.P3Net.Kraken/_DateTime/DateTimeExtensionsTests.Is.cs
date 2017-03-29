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
        #region IsNextDay (DateTime)

        [TestMethod]
        public void IsNextDay_SourceIsDayAfterValue ()
        {
            var source = new DateTime(2011, 10, 26, 12, 34, 56);
            var value = new DateTime(2011, 10, 25, 23, 12, 34);            

            //Act
            var actual = source.IsNextDay(value);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsNextDay_SourceIsDayBeforeValue ()
        {
            var source = new DateTime(2011, 10, 24, 12, 34, 56);
            var value = new DateTime(2011, 10, 25, 23, 12, 34);

            //Act
            var actual = source.IsNextDay(value);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsNextDay_SourceIsAfterValueByWeek ()
        {
            var source = new DateTime(2011, 10, 31, 12, 34, 56);
            var value = new DateTime(2011, 10, 25, 23, 12, 34);

            //Act
            var actual = source.IsNextDay(value);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsNextDay_SourceIsBeforeValueByWeek ()
        {
            var source = new DateTime(2011, 10, 18, 12, 34, 56);
            var value = new DateTime(2011, 10, 25, 23, 12, 34);

            //Act
            var actual = source.IsNextDay(value);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsNextDay_SourceIsMaxValue ()
        {
            var source = DateTime.MaxValue;
            var value = new DateTime(2011, 10, 25, 23, 12, 34);

            //Act
            var actual = source.IsNextDay(value);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region IsNextDay (Date)

        [TestMethod]
        public void IsNextDay_Date_SourceIsDayAfterValue ()
        {
            var source = new DateTime(2011, 10, 26, 12, 34, 56);
            var value = new Date(2011, 10, 25);

            //Act
            var actual = source.IsNextDay(value);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsNextDay_Date_SourceIsDayBeforeValue ()
        {
            var source = new DateTime(2011, 10, 24, 12, 34, 56);
            var value = new Date(2011, 10, 25);

            //Act
            var actual = source.IsNextDay(value);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsNextDay_Date_SourceIsAfterValueByWeek ()
        {
            var source = new DateTime(2011, 10, 31, 12, 34, 56);
            var value = new Date(2011, 10, 25);

            //Act
            var actual = source.IsNextDay(value);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsNextDay_Date_SourceIsBeforeValueByWeek ()
        {
            var source = new DateTime(2011, 10, 18, 12, 34, 56);
            var value = new Date(2011, 10, 25);

            //Act
            var actual = source.IsNextDay(value);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsNextDay_Date_SourceIsMaxValue ()
        {
            var source = DateTime.MaxValue;
            var value = new Date(2011, 10, 25);

            //Act
            var actual = source.IsNextDay(value);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region IsPreviousDay (DateTime)

        [TestMethod]
        public void IsPreviousDay_SourceIsDayAfterValue ()
        {
            var source = new DateTime(2011, 10, 26, 12, 34, 56);
            var value = new DateTime(2011, 10, 25, 23, 12, 34);

            //Act
            var actual = source.IsPreviousDay(value);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsPreviousDay_SourceIsDayBeforeValue ()
        {
            var source = new DateTime(2011, 10, 24, 12, 34, 56);
            var value = new DateTime(2011, 10, 25, 23, 12, 34);

            //Act
            var actual = source.IsPreviousDay(value);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsPreviousDay_SourceIsAfterValueByWeek ()
        {
            var source = new DateTime(2011, 10, 31, 12, 34, 56);
            var value = new DateTime(2011, 10, 25, 23, 12, 34);

            //Act
            var actual = source.IsPreviousDay(value);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsPreviousDay_SourceIsBeforeValueByWeek ()
        {
            var source = new DateTime(2011, 10, 18, 12, 34, 56);
            var value = new DateTime(2011, 10, 25, 23, 12, 34);

            //Act
            var actual = source.IsPreviousDay(value);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsPreviousDay_ValueIsMinValue ()
        {            
            var source = DateTime.MinValue;
            var value = new DateTime(2011, 10, 25, 23, 12, 34);

            //Act
            var actual = source.IsPreviousDay(value);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion
        
        #region IsPreviousDay (Date)

        [TestMethod]
        public void IsPreviousDay_Date_SourceIsDayAfterValue ()
        {
            var source = new DateTime(2011, 10, 26, 12, 34, 56);
            var value = new Date(2011, 10, 25);

            //Act
            var actual = source.IsPreviousDay(value);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsPreviousDay_Date_SourceIsDayBeforeValue ()
        {
            var source = new DateTime(2011, 10, 24, 12, 34, 56);
            var value = new Date(2011, 10, 25);

            //Act
            var actual = source.IsPreviousDay(value);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsPreviousDay_Date_SourceIsAfterValueByWeek ()
        {
            var source = new DateTime(2011, 10, 31, 12, 34, 56);
            var value = new Date(2011, 10, 25);

            //Act
            var actual = source.IsPreviousDay(value);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsPreviousDay_Date_SourceIsBeforeValueByWeek ()
        {
            var source = new DateTime(2011, 10, 18, 12, 34, 56);
            var value = new Date(2011, 10, 25);

            //Act
            var actual = source.IsPreviousDay(value);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsPreviousDay_Date_ValueIsMinValue ()
        {
            var source = DateTime.MinValue;
            var value = new Date(2013, 3, 4);

            //Act
            var actual = source.IsPreviousDay(value);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region IsSameDay (DateTime)

        [TestMethod]
        public void IsSameDay_SourceIsSameAsValue ()
        {
            var source = new DateTime(2011, 10, 26, 12, 34, 56);
            var value = source;

            //Act
            var actual = source.IsSameDay(value);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsSameDay_SourceIsSameDayAsValue ()
        {
            var source = new DateTime(2011, 10, 26, 12, 34, 56);
            var value = source.AddMinutes(50);

            //Act
            var actual = source.IsSameDay(value);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsSameDay_SourceIsDifferentDayAsValue ()
        {
            var source = new DateTime(2011, 10, 26, 12, 34, 56);
            var value = new DateTime(2011, 10, 25, 1, 2, 3);

            //Act
            var actual = source.IsSameDay(value);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region IsSameDay (Date)

        [TestMethod]
        public void IsSameDay_Date_SourceIsSameAsValue ()
        {
            var source = new DateTime(2011, 10, 26, 12, 34, 56);
            var value = new Date(source);

            //Act
            var actual = source.IsSameDay(value);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsSameDay_Date_SourceIsDifferentDayAsValue ()
        {
            var source = new DateTime(2011, 10, 26, 12, 34, 56);
            var value = new Date(2011, 10, 25);

            //Act
            var actual = source.IsSameDay(value);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region IsWeekend

        [TestMethod]
        public void IsWeekend_IsASaturday ()
        {
            var target = new DateTime(2011, 10, 29, 1, 2, 3);

            //Act
            var actual = target.IsWeekend();

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsWeekend_IsASunday ()
        {
            var target = new DateTime(2011, 10, 23, 1, 2, 3);

            //Act
            var actual = target.IsWeekend();

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsWeekend_IsAWeekday ()
        {
            var target = new DateTime(2011, 10, 26, 1, 2, 3);

            //Act
            var actual = target.IsWeekend();

            //Assert
            actual.Should().BeFalse();
        }
        #endregion
    }
}
