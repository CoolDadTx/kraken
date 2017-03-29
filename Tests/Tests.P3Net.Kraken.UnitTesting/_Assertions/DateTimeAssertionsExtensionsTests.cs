using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.UnitTesting
{
    [TestClass]
    public class DateTimeAssertionsExtensionsTests : UnitTest
    {
        #region BeDate

        [TestMethod]
        public void BeDate_IsValid ()
        {
            var instance = new DateTime(2012, 6, 7);
            var target = instance.AddHours(5).AddMinutes(6).AddSeconds(30);
            
            target.Should().BeDate(instance);
        }

        [TestMethod]
        public void BeDate_IsInvalid ()
        {
            var instance = new DateTime(2012, 6, 7);
            var target = new DateTime(2012, 10, 12);

            Action action = () => instance.Should().BeDate(target);

            action.ShouldThrow<AssertFailedException>();
        }

        [TestMethod]
        public void BeDate_WithMessage ()
        {
            var instance = new DateTime(2012, 6, 7);
            var target = new DateTime(2012, 10, 12);
            var expectedMessage = "Should fail";

            Action action = () => instance.Should().BeDate(target, expectedMessage);

            action.ShouldThrow<AssertFailedException>().ContainingMessage(expectedMessage);
        }        
        #endregion        
    }
}
