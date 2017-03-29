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
    public class UnicodeAttributeTests : UnitTest
    {
        [TestMethod]
        public void Ctor_CharsetIsCorrect ()
        {
            var target = new UnicodeAttribute();

            target.CharSet.Should().Be(CharSet.Unicode);
        }
    }
}
