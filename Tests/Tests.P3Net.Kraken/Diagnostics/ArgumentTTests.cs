using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

using P3Net.Kraken;
using P3Net.Kraken.Diagnostics;

namespace Tests.P3Net.Kraken.Diagnostics
{
    [TestClass]
    public class ArgumentTTests
    {
        [TestMethod]
        public void Ctor_WithNameAndValue ()
        {
            var expectedName = "Test";
            var expectedValue = Dates.February(1, 1970);

            var target = new Argument<Date>(expectedName, expectedValue);

            target.Name.Should().Be(expectedName);
            target.Value.Should().Be(expectedValue);
        }

        [TestMethod]
        public void Ctor_WithEmptyNameAndValue ()
        {
            var expectedValue = Dates.March(10, 1978);

            var target = new Argument<Date>("", expectedValue);

            target.Name.Should().BeEmpty();
            target.Value.Should().Be(expectedValue);
        }

        [TestMethod]
        public void Ctor_WithNullNameAndValue ()
        {
            var expectedValue = Dates.June(10, 1989);

            var target = new Argument<Date>(null, expectedValue);

            target.Name.Should().BeEmpty();
            target.Value.Should().Be(expectedValue);
        }
    }
}
