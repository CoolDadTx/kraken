using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

using P3Net.Kraken.Diagnostics;
using P3Net.Kraken;

namespace Tests.P3Net.Kraken.Diagnostics
{
    [TestClass]
    public class DateArgumentConstraintExtensionsTests
    {
        [TestMethod]
        public void IsNotNone_IsTrue ()
        {
            var target = new ArgumentConstraint<Date>(new Argument<Date>("arg", Dates.January(4, 2000)));

            target.IsNotNone();
        }

        [TestMethod]
        public void IsNotNone_IsFalse ()
        {
            var target = new ArgumentConstraint<Date>(new Argument<Date>("arg", Date.None));

            Action action = () => target.IsNotNone();

            action.ShouldThrow<ArgumentException>();
        }
    }
}
