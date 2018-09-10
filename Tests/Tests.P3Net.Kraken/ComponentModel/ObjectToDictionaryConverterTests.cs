using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

using P3Net.Kraken.ComponentModel;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.ComponentModel
{
    [TestClass]
    public class ObjectToDictionaryConverterTests : UnitTest
    {
        [TestMethod]
        public void ToDictionary_WithObject ()
        {
            var value = new { First = 1, Second = 2, Third = 3 };

            var actual = ObjectToDictionaryConverter.ToDictionary(value);

            //Assert
            actual.Should().Contain("First", 1);
            actual.Should().Contain("Second", 2);
            actual.Should().Contain("Third", 3);
        }

        [TestMethod]
        public void ToDictionary_WithEmptyObject ()
        {
            var value = new { };

            var actual = ObjectToDictionaryConverter.ToDictionary(value);

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void ToDictionary_WithNull ()
        {
            Action action = () => ObjectToDictionaryConverter.ToDictionary(null);

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
