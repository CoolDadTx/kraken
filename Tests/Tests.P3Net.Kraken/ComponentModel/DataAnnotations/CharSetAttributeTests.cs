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
    public class CharSetAttributeTests : UnitTest
    {
        [TestMethod]
        public void Ctor_CharsetIsCorrect ()
        {
            var expected = CharSet.Unicode;

            var target = new CharSetAttribute(expected);

            target.CharSet.Should().Be(expected);
        }
    }
}
