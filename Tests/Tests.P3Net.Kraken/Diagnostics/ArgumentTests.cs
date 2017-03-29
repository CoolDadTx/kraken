using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

using P3Net.Kraken.Diagnostics;
using P3Net.Kraken;

namespace Tests.P3Net.Kraken.Diagnostics
{
    [TestClass]
    public class ArgumentTests
    {
        #region Ctor
        
        [TestMethod]
        public void Ctor_WithName ()
        {
            var expectedName = "Test";

            var target = new Argument(expectedName);

            target.Name.Should().Be(expectedName);
        }

        [TestMethod]
        public void Ctor_WithEmptyName ()
        {
            var target = new Argument("");

            target.Name.Should().BeEmpty();
        }

        [TestMethod]
        public void Ctor_WithNullName ()
        {
            var target = new Argument((string)null);

            target.Name.Should().BeEmpty();
        }
        #endregion
    }
}
