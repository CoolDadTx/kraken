using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using P3Net.Kraken;
using P3Net.Kraken.Diagnostics;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.Diagnostics
{
    [TestClass]
    public partial class VerifyTests : UnitTest
    {
        #region Argument

        [TestMethod]
        public void Argument_WithName ()
        {
            var expectedName = "Test";

            var target = Verify.Argument(expectedName);

            target.Name.Should().Be(expectedName);
        }

        [TestMethod]
        public void Argument_WithNameAndValue ()
        {
            var expectedName = "Test";
            var expectedValue = Dates.February(4, 2000);

            var target = Verify.Argument(expectedName, expectedValue);

            target.Argument.Name.Should().Be(expectedName);
            target.Argument.Value.Should().Be(expectedValue);
        }
        #endregion
    }
}
