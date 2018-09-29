/*
 * Copyright © 2018 Michael Taylor
 * https://www.michaeltaylorp3.net
 * All Rights Reserved
 */
using System;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken
{    
    public partial class StringExtensionsTests
    {
        [TestMethod]        
        public void Truncate_LengthIsLonger ( )
        {
            var target = "Hello World";
            var expected = "Hello";
            var length = expected.Length;

            var actual = target.Truncate(length);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Truncate_LengthIsShorter ()
        {
            var target = "Hi";

            var actual = target.Truncate(10);

            actual.Should().Be(target);
        }

        [TestMethod]
        public void Truncate_LengthIsZero ()
        {
            var target = "Hello World";

            var actual = target.Truncate(0);

            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void Truncate_LengthIsNegative ()
        {
            var target = "Hello World";
            
            Action action = () => target.Truncate(-1);

            action.Should().Throw<ArgumentOutOfRangeException>().WithParameter("length");
        }

        [TestMethod]
        public void Truncate_WithIndicator_LengthIsLonger ()
        {
            var target = "Hello World";            
            var indicator = "...";
            var expected = "Hello...";
            var length = expected.Length;
            
            var actual = target.Truncate(length, indicator);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Truncate_WithIndicator_LengthIsShorter ()
        {
            var target = "Hello World";
            var indicator = "...";

            var actual = target.Truncate(20, indicator);

            actual.Should().Be(target);
        }

        [TestMethod]
        public void Truncate_WithIndicator_IndicatorIsNull ()
        {
            var target = "Hello World";
            var expected = "Hello";
            var length = expected.Length;

            var actual = target.Truncate(length, null);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Truncate_WithIndicator_IndicatorIsEmpty ()
        {
            var target = "Hello World";
            var expected = "Hello";
            var length = expected.Length;

            var actual = target.Truncate(length, "");

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Truncate_WithIndicator_LengthIsIndicatorLength ()
        {
            var target = "Hello World";            
            var indicator = "...";
            var length = indicator.Length;

            var actual = target.Truncate(length, indicator);

            actual.Should().Be(indicator);
        }

        [TestMethod]
        public void Truncate_WithIndicator_LengthTooShort ()
        {
            var target = "Hello World";

            Action action = () => target.Truncate(2, "...");

            action.Should().Throw<ArgumentOutOfRangeException>().WithParameter("length");
        }

        [TestMethod]
        public void Truncate_WithIndicator_IndicatorInFront ()
        {
            var target = "Hello World";
            var expected = "...Hello";
            var indicator = "...";
            var length = expected.Length;

            var actual = target.Truncate(length, indicator, false);

            actual.Should().Be(expected);
        }
    }
}
