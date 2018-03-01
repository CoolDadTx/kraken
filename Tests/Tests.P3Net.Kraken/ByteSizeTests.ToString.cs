using System;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

using P3Net.Kraken;

namespace Tests.P3Net.Kraken
{
    public partial class ByteSizeTests
    {              
        [TestMethod]
        public void ToLongString_Default ()
        {
            var expected = "1.95 Kilobytes";
            var target = new ByteSize(2000);

            //Act
            var actual = target.ToLongString();

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToShortString_Default ()
        {
            var expected = "2 KB";
            var target = new ByteSize(2000);

            //Act
            var actual = target.ToShortString();

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToString_Default ()
        {
            var expected = "1.95 KB";
            var target = new ByteSize(2000);

            //Act
            var actual = target.ToString();
            
            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToString_WithNullFormat ()
        {
            var expected = "1.95 KB";
            var target = new ByteSize(2000);

            //Act
            var actual = target.ToString((string)null);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToString_WithFormatProvider ()
        {
            var expected = "3 GB";
            var target = ByteSize.FromGigabytes(3);

            //Act
            var actual = target.ToString(NumberFormatInfo.CurrentInfo);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToString_WithFormatStringAndProvider ()
        {
            var expected = "1.95";
            var target = new ByteSize(2000);

            //Act
            var actual = target.ToString("kkk", NumberFormatInfo.CurrentInfo);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToString_WithNullFormatStringAndProvider ()
        {
            var expected = "1.95 KB";
            var target = new ByteSize(2000);

            //Act
            var actual = target.ToString(null, NumberFormatInfo.CurrentInfo);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToString_WithZero ()
        {
            var expected = "0";
            var target = new ByteSize(0);

            //Act
            var actual = target.ToString("b");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToString_InBytes ()
        {
            var expected = 100L;
            var target = new ByteSize(100);

            //Act
            var actual = target.ToString("b");

            //Assert
            actual.Should().Be(expected.ToString());
        }

        [TestMethod]
        public void ToString_InGigabytes ()
        {
            var expected = "3";
            var target = ByteSize.FromGigabytes(3);

            //Act
            var actual = target.ToString("g");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToString_InKilobytes ()
        {
            var expected = "1.95";
            var target = new ByteSize(2000);

            //Act
            var actual = target.ToString("kkk");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToString_InMegabytes ()
        {
            var expected = "1.9";
            var target = ByteSize.FromMegabytes(1.9);

            //Act
            var actual = target.ToString("mm");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToString_InTerabytes ()
        {
            var expected = "1";
            var target = ByteSize.FromTerabytes(1);

            //Act
            var actual = target.ToString("t");

            //Assert
            actual.Should().Be(expected);
        }
        
        [TestMethod]
        public void ToString_InBestValueOfBytes ()
        {
            var expected = "123";
            var target = new ByteSize(123);

            //Act
            var actual = target.ToString("ff");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToString_InBestValueOfKilobytes ()
        {
            var expected = "2";
            var target = ByteSize.FromKilobytes(2);

            //Act
            var actual = target.ToString("ff");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToString_InBestValueOfMegabytes ()
        {
            var expected = "1.9";
            var target = ByteSize.FromMegabytes(1.9);

            //Act
            var actual = target.ToString("ff");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToString_InBestValueOfGigabytes ()
        {
            var expected = "3";
            var target = ByteSize.FromGigabytes(3);

            //Act
            var actual = target.ToString("ff");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToString_InBestValueOfTerabytes ()
        {
            var expected = "4";
            var target = ByteSize.FromTerabytes(4);

            //Act
            var actual = target.ToString("ff");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToString_UnitLetterLower ()
        {
            var expected = "m";
            var target = ByteSize.FromMegabytes(1.9);

            //Act
            var actual = target.ToString("u");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToString_UnitLetterUpper ()
        {
            var expected = "G";
            var target = ByteSize.FromGigabytes(1.9);

            //Act
            var actual = target.ToString("U");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToString_UnitAbbreviationLower ()
        {
            var expected = "mb";
            var target = ByteSize.FromMegabytes(1.9);

            //Act
            var actual = target.ToString("uu");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToString_UnitAbbreviationUpper ()
        {
            var expected = "TB";
            var target = ByteSize.FromTerabytes(2);

            //Act
            var actual = target.ToString("UU");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToString_UnitFullLower ()
        {
            var expected = "megabytes";
            var target = ByteSize.FromMegabytes(1.9);

            //Act
            var actual = target.ToString("uuu");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToString_UnitFullUpper ()
        {
            var expected = "Terabytes";
            var target = ByteSize.FromTerabytes(2);

            //Act
            var actual = target.ToString("UUU");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToString_WithComma ()
        {
            var expected = "1,025";
            var target = new ByteSize(1025);

            //Act
            var actual = target.ToString(",b");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToString_UnitAndSize ()
        {
            var expected = "1.9 TB";
            var target = ByteSize.FromTerabytes(1.9);

            //Act
            var actual = target.ToString("tt UU");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToString_WithQuotedString ()
        {
            var expected = "File1 n is 2 KB t in size";
            var target = ByteSize.FromKilobytes(2);

            //Act
            var actual = target.ToString("\"File1 \\n is\" kk UU \\t \'in size\'");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToString_BadEscapeSequence ()
        {
            Action action = () => new ByteSize(10).ToString("Hello \\");

            action.Should().Throw<FormatException>();
        }

        [TestMethod]
        public void ToString_BadEscapeSequenceInQuote ()
        {
            Action action = () => new ByteSize(10).ToString("\"Hello \\");

            action.Should().Throw<FormatException>();
        }

        [TestMethod]
        public void ToString_MissingClosingQuote()
        {
            Action action = () => new ByteSize(10).ToString("\"Hello");

            action.Should().Throw<FormatException>();
        }
    }
}
