using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Text;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.Text
{
    [TestClass]
    public class StringComparisonExtensionsTests
    {        
        #region ToComparer

        [TestMethod]
        public void ToComparer_CurrentCulture ()
        {
            var actual = StringComparison.CurrentCulture.ToComparer();

            actual.Should().Be(StringComparer.CurrentCulture);
        }

        [TestMethod]
        public void ToComparer_CurrentCultureIgnoreCase ()
        {
            var actual = StringComparison.CurrentCultureIgnoreCase.ToComparer();

            actual.Should().Be(StringComparer.CurrentCultureIgnoreCase);
        }

        [TestMethod]
        public void ToComparer_InvariantCulture ()
        {
            var actual = StringComparison.InvariantCulture.ToComparer();

            actual.Should().Be(StringComparer.InvariantCulture);
        }

        [TestMethod]
        public void ToComparer_InvariantCultureIgnoreCase ()
        {
            var actual = StringComparison.InvariantCultureIgnoreCase.ToComparer();

            actual.Should().Be(StringComparer.InvariantCultureIgnoreCase);
        }

        [TestMethod]
        public void ToComparer_Ordinal ()
        {
            var actual = StringComparison.Ordinal.ToComparer();

            actual.Should().Be(StringComparer.Ordinal);
        }

        [TestMethod]
        public void ToComparer_OrdinalIgnoreCase ()
        {
            var actual = StringComparison.OrdinalIgnoreCase.ToComparer();

            actual.Should().Be(StringComparer.OrdinalIgnoreCase);
        }

        [TestMethod]
        public void ToCompare_BadComparison ()
        {
            Action action = () => ((StringComparison)100).ToComparer();

            action.ShouldThrowArgumentOutOfRangeException();
        }
        #endregion
    }
}
