#region Impots

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken.IO;
#endregion

namespace Tests.P3Net.Kraken.IO
{
    public partial class StreamExtensionsTests
    {
        #region Compressed

        #region Read

        [TestMethod]
        public void ReadCompressedString_DefaultEncoding_SmallString ()
        {           
            var expected = "Hello";
            var data = CreateCompressedString(expected, Encoding.Default);
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadCompressedString();

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ReadCompressedString_DefaultEncoding_MediumString ()
        {
            var expected = new string('a', SByte.MaxValue + 10);
            var data = CreateCompressedString(expected, Encoding.Default);
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadCompressedString();

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ReadCompressedString_DefaultEncoding_LargeString ()
        {
            var expected = new string('x', Int16.MaxValue + 10);
            var data = CreateCompressedString(expected, Encoding.Default);
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadCompressedString();

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ReadCompressedString_CustomEncoding_StringExists ()
        {
            var expected = "Hello";
            var data = CreateCompressedString(expected, Encoding.Unicode);
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadCompressedString(Encoding.Unicode);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ReadCompressedString_DefaultEncoding_StringIsEmpty ()
        {
            var expected = "";
            var data = CreateCompressedString(expected, Encoding.Default);
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadCompressedString();

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void ReadCompressedString_MaxValue ()
        {
            var target = new MemoryStream();
            target.WriteBytes(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //Act
            target.ReadCompressedString();
        }
        #endregion

        #region Roundtrip

        [TestMethod]
        public void RoundTrip_CompressedString ()
        {
            var expected = "Later";
            var target = new MemoryStream();

            //Act
            target.WriteCompressedString(expected);
            target.Seek(0, SeekOrigin.Begin);
            var actual = target.ReadCompressedString();

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Write

        [TestMethod]
        public void WriteCompressedString_DefaultEncoding_SmallString ()
        {
            var data = "Hello";
            var expected = CreateCompressedString(data, Encoding.Unicode);
            var expectedWritten = expected.Length;
            var target = new MemoryStream();

            //Act
            var actualWritten = target.WriteCompressedString(data, Encoding.Unicode);
            var actual = target.ToArray();

            //Assert
            actualWritten.Should().Be(expectedWritten);
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void WriteCompressedString_DefaultEncoding_MediumString ()
        {
            var data = new string('a', SByte.MaxValue + 10);
            var expected = CreateCompressedString(data, Encoding.Unicode);
            var expectedWritten = expected.Length;
            var target = new MemoryStream();

            //Act
            var actualWritten = target.WriteCompressedString(data, Encoding.Unicode);
            var actual = target.ToArray();

            //Assert
            actualWritten.Should().Be(expectedWritten);
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void WriteCompressedString_DefaultEncoding_LargeString ()
        {
            var data = new string('x', Int16.MaxValue + 10);
            var expected = CreateCompressedString(data, Encoding.Unicode);
            var expectedWritten = expected.Length;
            var target = new MemoryStream();

            //Act
            var actualWritten = target.WriteCompressedString(data, Encoding.Unicode);
            var actual = target.ToArray();

            //Assert
            actualWritten.Should().Be(expectedWritten);
            //actual.Should().ContainInOrder(expected); - locks up
            for (int index = 0; index < actualWritten; ++index)
                actual[index].Should().Be(expected[index]);
        }

        [TestMethod]
        public void WriteCompressedString_CustomEncoding ()
        {
            var data = "Hello";
            var expected = CreateCompressedString(data, Encoding.Unicode);
            var expectedWritten = expected.Length;
            var target = new MemoryStream();

            //Act
            var actualWritten = target.WriteCompressedString(data, Encoding.Unicode);
            var actual = target.ToArray();

            //Assert
            actualWritten.Should().Be(expectedWritten);
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void WriteCompressedString_StringIsEmpty ()
        {
            var expected = CreateCompressedString("", Encoding.Default);
            var target = new MemoryStream();

            //Act
            var actualWritten = target.WriteCompressedString("");
            var actual = target.ToArray();

            //Assert
            actualWritten.Should().Be(expected.Length);
            actual.Should().ContainInOrder(actual);
        }

        [TestMethod]
        public void WriteCompressedString_StringIsNull ()
        {
            string data = null;
            var expected = CreateCompressedString("", Encoding.Default);
            var target = new MemoryStream();

            //Act
            var actualWritten = target.WriteCompressedString(data);
            var actual = target.ToArray();

            //Assert
            actualWritten.Should().Be(expected.Length);
            actual.Should().ContainInOrder(actual);
        }
        #endregion

        #endregion
        
        #region Fixed

        #region Read

        [TestMethod]
        public void ReadFixedString_ValueExists ()
        {
            var expected = "World";
            var length = expected.Length;
            var data = CreateFixedString(expected, Encoding.Default, length);
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadFixedString(length);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ReadFixedString_StringIsSmaller ()
        {
            var expected = "World";
            var length = 20;
            var data = CreateFixedString(expected, Encoding.Default, length);
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadFixedString(length);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(EndOfStreamException))]
        public void ReadFixedString_ValueNotExists ()
        {
            var target = new MemoryStream();

            //Act
            target.ReadFixedString(10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReadFixedString_CountIsNegative ()
        {
            var target = new MemoryStream();

            //Add
            target.ReadFixedString(-1);
        }

        [TestMethod]
        public void ReadFixedString_StringIsSmaller_UsingCustomFill ()
        {
            var expected = "World";
            var length = 20;
            var data = CreateFixedString(expected, Encoding.Default, length, '-');
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadFixedString(length, '-');

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ReadFixedString_CustomEncoding_StringIsSmaller_UsingCustomFill ()
        {
            var expected = "World";
            var length = 20;
            var data = CreateFixedString(expected, Encoding.Unicode, length, '-');
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadFixedString(length, '-', Encoding.Unicode);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Roundtrip

        [TestMethod]
        public void RoundTrip_FixedString ()
        {
            var length = 40;
            var expected = "Hello";
            var target = new MemoryStream();

            //Act
            target.WriteFixedString(expected, length);
            target.Seek(0, SeekOrigin.Begin);
            var actual = target.ReadFixedString(length);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Write

        [TestMethod]
        public void WriteFixedString_Value ()
        {
            var data = "World";
            var length = data.Length;
            var expected = CreateFixedString(data, Encoding.Default, length);
            var target = new MemoryStream();

            //Act
            var actualWritten = target.WriteFixedString(data, length);
            var actual = target.ToArray();

            //Assert
            actualWritten.Should().Be(expected.Length);
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void WriteFixedString_StringIsSmaller ()
        {
            var data = "World";
            var length = 20;
            var expected = CreateFixedString(data, Encoding.Default, length);
            var target = new MemoryStream();

            //Act
            var actualWritten = target.WriteFixedString(data, length);
            var actual = target.ToArray();

            //Assert
            actualWritten.Should().Be(expected.Length);
            actual.Should().ContainInOrder(expected);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WriteFixedString_CountIsNegative ()
        {
            var target = new MemoryStream();

            //Add
            target.WriteFixedString("", -1);
        }

        [TestMethod]
        public void WriteFixedString_StringIsSmaller_UsingCustomFill ()
        {
            var data = "World";
            var length = 20;
            var expected = CreateFixedString(data, Encoding.Default, length, '-');
            var target = new MemoryStream();

            //Act
            var actualWritten = target.WriteFixedString(data, length, '-');
            var actual = target.ToArray();

            //Assert
            actualWritten.Should().Be(expected.Length);
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void WriteFixedString_CustomEncoding_StringIsSmaller_UsingCustomFill ()
        {
            var data = "World";
            var length = 20;
            var expected = CreateFixedString(data, Encoding.Unicode, length, '-');
            var target = new MemoryStream();

            //Act
            var actualWritten = target.WriteFixedString(data, length, '-', Encoding.Unicode);
            var actual = target.ToArray();

            //Assert
            actualWritten.Should().Be(expected.Length);
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void WriteFixedString_StringIsNull ()
        {
            var expected = new byte[] { 0, 0, 0, 0, 0 };
            var target = new MemoryStream();

            //Act
            var actualWritten = target.WriteFixedString(null, expected.Length, '\0');
            var actual = target.ToArray();

            //Assert
            actualWritten.Should().Be(expected.Length);
            actual.Should().ContainInOrder(expected);
        }
        #endregion

        #endregion

        #region Length Prefixed

        #region Read

        [TestMethod]
        public void ReadLengthPrefixedString_One_StringExists ()
        {
            var expected = "Hello";
            var data = CreateLengthPrefixedString(expected, StringLengthPrefix.One, Encoding.Default);
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadLengthPrefixedString(StringLengthPrefix.One);

            //Assert
            actual.Should().Be(expected);        
        }

        [TestMethod]
        public void ReadLengthPrefixedString_Two_StringExists ()
        {
            var expected = "Hello";
            var data = CreateLengthPrefixedString(expected, StringLengthPrefix.Two, Encoding.Default);
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadLengthPrefixedString(StringLengthPrefix.Two);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ReadLengthPrefixedString_Four_StringExists ()
        {
            var expected = "Hello";
            var data = CreateLengthPrefixedString(expected, StringLengthPrefix.Four, Encoding.Default);
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadLengthPrefixedString(StringLengthPrefix.Four);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ReadLengthPrefixedString_CustomEncoding_StringExists ()
        {
            var expected = "Hello";
            var data = CreateLengthPrefixedString(expected, StringLengthPrefix.Four, Encoding.Unicode);
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadLengthPrefixedString(StringLengthPrefix.Four, Encoding.Unicode);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ReadLengthPrefixedString_StringIsEmpty ()
        {
            var expected = "";
            var data = CreateLengthPrefixedString(expected, StringLengthPrefix.Four, Encoding.Default);
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadLengthPrefixedString(StringLengthPrefix.Four);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReadLengthPrefixedString_LengthIsInvalid ()
        {
            var target = new MemoryStream();

            //Act
            target.ReadLengthPrefixedString((StringLengthPrefix)20);
        }
        #endregion

        #region Roundtrip

        [TestMethod]
        public void RoundTrip_LengthPrefixedString ()
        {
            var length = StringLengthPrefix.Two;
            var expected = "Hello";
            var target = new MemoryStream();

            //Act
            target.WriteLengthPrefixedString(expected, length);
            target.Seek(0, SeekOrigin.Begin);
            var actual = target.ReadLengthPrefixedString(length);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Write

        [TestMethod]
        public void WriteLengthPrefixedString_One ()
        {
            var data = "Hello";
            var expected = CreateLengthPrefixedString(data, StringLengthPrefix.One, Encoding.Default);            
            var target = new MemoryStream();

            //Act
            var actualWritten = target.WriteLengthPrefixedString(data, StringLengthPrefix.One);
            var actual = target.ToArray();

            //Assert
            actualWritten.Should().Be(expected.Length);
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void WriteLengthPrefixedString_Two ()
        {
            var data = "Hello";
            var expected = CreateLengthPrefixedString(data, StringLengthPrefix.Two, Encoding.Default);
            var target = new MemoryStream();

            //Act
            var actualWritten = target.WriteLengthPrefixedString(data, StringLengthPrefix.Two);
            var actual = target.ToArray();

            //Assert
            actualWritten.Should().Be(expected.Length);
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void WriteLengthPrefixedString_Four ()
        {
            var data = "Hello";
            var expected = CreateLengthPrefixedString(data, StringLengthPrefix.Four, Encoding.Default);
            var target = new MemoryStream();

            //Act
            var actualWritten = target.WriteLengthPrefixedString(data, StringLengthPrefix.Four);
            var actual = target.ToArray();

            //Assert
            actualWritten.Should().Be(expected.Length);
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void WriteLengthPrefixedString_CustomEncoding ()
        {
            var data = "Hello";
            var expected = CreateLengthPrefixedString(data, StringLengthPrefix.Four, Encoding.Unicode);
            var target = new MemoryStream();

            //Act
            var actualWritten = target.WriteLengthPrefixedString(data, StringLengthPrefix.Four, Encoding.Unicode);
            var actual = target.ToArray();

            //Assert
            actualWritten.Should().Be(expected.Length);
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void WriteLengthPrefixedString_StringIsEmpty ()
        {
            var data = "";
            var expected = CreateLengthPrefixedString(data, StringLengthPrefix.One, Encoding.Default);
            var target = new MemoryStream();

            //Act
            var actualWritten = target.WriteLengthPrefixedString(data, StringLengthPrefix.One);
            var actual = target.ToArray();

            //Assert
            actualWritten.Should().Be(expected.Length);
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void WriteLengthPrefixedString_StringIsNull ()
        {
            string data = null;
            var expected = CreateLengthPrefixedString("", StringLengthPrefix.One, Encoding.Default);
            var target = new MemoryStream();

            //Act
            var actualWritten = target.WriteLengthPrefixedString(data, StringLengthPrefix.One);
            var actual = target.ToArray();

            //Assert
            actualWritten.Should().Be(expected.Length);
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WriteLengthPrefixedString_LengthIsInvalid ()
        {
            var target = new MemoryStream();

            //Act
            target.WriteLengthPrefixedString("", (StringLengthPrefix)20);
        }
        #endregion

        #endregion
        
        #region Null Terminated

        #region Read

        [TestMethod]
        public void ReadNullTerminatedString_DefaultEncoding_StringExists ()
        {
            var expected = "Hello";
            var data = CreateNullTerminatedString(expected, Encoding.Default);
            var target = new MemoryStream(data);
            
            //Act
            var actual = target.ReadNullTerminatedString();

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ReadNullTerminatedString_CustomEncoding_StringExists ()
        {
            var expected = "Hello";
            var data = CreateNullTerminatedString(expected, Encoding.Unicode);
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadNullTerminatedString(Encoding.Unicode);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ReadNullTerminatedString_DefaultEncoding_StringIsEmpty ()
        {
            var expected = "";
            var data = CreateNullTerminatedString(expected, Encoding.Default);
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadNullTerminatedString();

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(EndOfStreamException))]
        public void ReadNullTerminatedString_NullIsMissing ()
        {
            var target = new MemoryStream(new byte[] { 65, 66, 67 });

            //Act
            target.ReadNullTerminatedString();
        }
        #endregion

        #region Roundtrip

        [TestMethod]
        public void RoundTrip_NullTerminatedString ()
        {
            var expected = "Hello";
            var target = new MemoryStream();

            //Act
            target.WriteNullTerminatedString(expected);
            target.Seek(0, SeekOrigin.Begin);
            var actual = target.ReadNullTerminatedString();

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Write

        [TestMethod]
        public void WriteNullTerminatedString_DefaultEncoding ()
        {
            var data = "Hello";
            var expected = CreateNullTerminatedString(data, Encoding.Default);
            var target = new MemoryStream();

            //Act
            var actualWritten = target.WriteNullTerminatedString(data);
            var actual = target.ToArray();

            //Assert
            actualWritten.Should().Be(expected.Length);
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void WriteNullTerminatedString_CustomEncoding ()
        {
            var data = "Hello";
            var expected = CreateNullTerminatedString(data, Encoding.Unicode);
            var target = new MemoryStream();

            //Act
            var actualWritten = target.WriteNullTerminatedString(data, Encoding.Unicode);
            var actual = target.ToArray();

            //Assert
            actualWritten.Should().Be(expected.Length);
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void WriteNullTerminatedString_DefaultEncoding_StringIsEmpty ()
        {
            var data = "";
            var expected = CreateNullTerminatedString(data, Encoding.Default);
            var target = new MemoryStream();

            //Act
            var actualWritten = target.WriteNullTerminatedString(data);
            var actual = target.ToArray();

            //Assert
            actualWritten.Should().Be(expected.Length);
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void WriteNullTerminatedString_DefaultEncoding_StringIsNull ()
        {
            string data = null;
            var expected = CreateNullTerminatedString(data, Encoding.Default);
            var target = new MemoryStream();

            //Act
            var actualWritten = target.WriteNullTerminatedString(data);
            var actual = target.ToArray();

            //Assert
            actualWritten.Should().Be(expected.Length);
            actual.Should().ContainInOrder(expected);
        }
        #endregion

        #endregion                     
    }
}
