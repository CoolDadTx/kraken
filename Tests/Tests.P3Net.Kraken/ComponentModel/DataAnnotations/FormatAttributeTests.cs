using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.ComponentModel.DataAnnotations;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.ComponentModel.DataAnnotations
{
    [TestClass]
    public class FormatAttributeTests : UnitTest
    {
        #region Ctor

        [TestMethod]
        public void Ctor_FormatIsCorrect ()
        {
            var expected = "ssn";

            var target = new FormatAttribute(expected);

            target.Format.Should().Be(expected);
        }

        [TestMethod]
        public void Ctor_FormatIsNull ()
        {
            Action action = () => new FormatAttribute(null);

            action.ShouldThrowArgumentNullException();
        }

        [TestMethod]
        public void Ctor_FormatIsEmpty ()
        {
            Action action = () => new FormatAttribute("");

            action.ShouldThrowArgumentException();
        }
        #endregion

        [TestMethod]
        public void Example_CanBeSet ()
        {
            var expected = "xxx-xx-xxxx";

            var target = new FormatAttribute("ssn");
            target.Example = expected;

            target.Example.Should().Be(expected);
        }

        [TestMethod]
        public void Example_CanBeNull ()
        {
            var target = new FormatAttribute("ssn");
            target.Example = null;

            target.Example.Should().BeEmpty();
        }

        [TestMethod]
        public void Example_CanBeEmpty ()
        {
            var target = new FormatAttribute("ssn");
            target.Example = "";

            target.Example.Should().BeEmpty();
        }
    }
}
