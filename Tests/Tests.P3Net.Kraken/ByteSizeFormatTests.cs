using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

using P3Net.Kraken;

namespace Tests.P3Net.Kraken
{
    [TestClass]
    public class ByteSizeFormatTests
    {
        [TestMethod]
        public void Format_WithNullFormatString ()
        {
            var target = ByteSize.FromBytes(1200);

            var expected = ByteSizeFormat.Format(ByteSizeFormat.DefaultFormatString, target, null);

            var actual = ByteSizeFormat.Format(null, target, null);

            actual.Should().Be(expected);
        }
    }
}
