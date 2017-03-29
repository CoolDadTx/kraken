#region Impots

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken.IO;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken.IO
{
    public partial class StreamExtensionsTests
    {
        #region Boolean
        
        #region Read

        [TestMethod]
        public void ReadBoolean_ValueExists ()
        {
            var data = BitConverter.GetBytes(true);
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadBoolean();

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        [ExpectedException(typeof(EndOfStreamException))]
        public void ReadBoolean_ValueNotExists ()
        {
            var target = new MemoryStream();

            //Act
            target.ReadBoolean();
        }
        #endregion

        #region RoundTrip

        [TestMethod]
        public void RoundTrip_Boolean ()
        {
            var expected = true;
            var target = new MemoryStream();

            //Act
            target.Write(expected);
            target.Seek(0, SeekOrigin.Begin);
            var actual = target.ReadBoolean();

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Write

        [TestMethod]
        public void Write_Boolean ()
        {
            var expected = BitConverter.GetBytes(true);
            var target = new MemoryStream();

            //Act
            target.Write(true);
            var actual = target.ToArray();

            //Assert
            actual.Should().ContainInOrder(expected);
        }
        #endregion

        #endregion

        #region Bytes

        #region Read

        [TestMethod]
        public void ReadBytes_ValueExists ()
        {
            var expected = new byte[] { 1, 2, 3, 4 };
            var target = new MemoryStream(expected);

            //Act
            var actual = target.ReadBytes(expected.Length);

            //Assert
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(EndOfStreamException))]
        public void ReadBytes_ValueNotExists ()
        {
            var target = new MemoryStream();

            //Act
            target.ReadBytes(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReadBytes_CountIsNegative ()
        {
            var target = new MemoryStream();

            //Act
            target.ReadBytes(-1);
        }

        [TestMethod]
        public void ReadBytes_CountIsZero ()
        {
            var target = new MemoryStream(10);

            //Act
            var actual = target.ReadBytes(0);

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void ReadBytes_CountIsLarge ()
        {
            var expected = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 13, 14, 15 };
            var target = new MemoryStream(expected);

            //Act
            var actual = target.ReadBytes(expected.Length);

            //Assert
            actual.Should().ContainInOrder(expected);
        }
        #endregion

        #region RoundTrip

        [TestMethod]
        public void RoundTrip_Bytes ()
        {
            var expected = new byte[] { 1, 2, 3, 4 };
            var target = new MemoryStream();

            //Act
            target.WriteBytes(expected);
            target.Seek(0, SeekOrigin.Begin);
            var actual = target.ReadBytes(expected.Length);

            //Assert
            actual.Should().ContainInOrder(expected);
        }
        #endregion

        #region Write

        [TestMethod]
        public void WriteBytes_Value ()
        {
            var expected = new byte[] { 1, 2, 3, 4 };
            var target = new MemoryStream();

            //Act
            target.WriteBytes(expected);
            var actual = target.ToArray();

            //Assert
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void WriteBytes_ValueIsNull ()
        {
            var target = new MemoryStream();

            //Act
            target.WriteBytes(null);
            var actual = target.ToArray();

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void WriteBytes_ValueIsEmpty ()
        {
            var target = new MemoryStream();

            //Act
            target.WriteBytes(new byte[0]);
            var actual = target.ToArray();

            //Assert
            actual.Should().BeEmpty();
        }
        #endregion

        #endregion

        #region Char

        #region Read

        [TestMethod]
        public void ReadChar_ValueExists ()
        {
            var expected = 'c';
            var data = Encoding.Default.GetBytes(expected.ToString());
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadChar();

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ReadChar_CustomEncoding_ValueExists ()
        {
            var expected = 'c';
            var data = Encoding.Unicode.GetBytes(expected.ToString());
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadChar();

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(EndOfStreamException))]
        public void ReadChar_ValueNotExists ()
        {
            var target = new MemoryStream();

            //Act
            target.ReadChar();
        }
        #endregion

        #region Roundtrip

        [TestMethod]
        public void RoundTrip_Char ()
        {
            var expected = 'z';
            var target = new MemoryStream();

            //Act
            target.Write(expected);
            target.Seek(0, SeekOrigin.Begin);
            var actual = target.ReadChar();

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Write

        [TestMethod]
        public void Write_CharWithDefaultEncoding ()
        {
            var data = 'c';
            var expected = Encoding.Default.GetBytes(data.ToString());
            var target = new MemoryStream();

            //Act
            target.Write(data);
            var actual = target.ToArray();

            //Assert
            actual.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void Write_CharWithCustomEncoding ()
        {
            var data = 'c';
            var expected = Encoding.Unicode.GetBytes(data.ToString());
            var target = new MemoryStream();

            //Act
            target.Write(data, Encoding.Unicode);
            var actual = target.ToArray();

            //Assert
            actual.Should().ContainInOrder(expected);
        }
        #endregion

        #endregion

        #region Double

        #region Read

        [TestMethod]
        public void ReadDouble_ValueExists ()
        {
            var expected = 123.456;
            var data = BitConverter.GetBytes(expected);
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadDouble();

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(EndOfStreamException))]
        public void ReadDouble_ValueNotExists ()
        {
            var target = new MemoryStream();

            //Act
            target.ReadDouble();
        }
        #endregion

        #region Roundtrip

        [TestMethod]
        public void RoundTrip_Double ()
        {
            var expected = 123.456;
            var target = new MemoryStream();

            //Act
            target.Write(expected);
            target.Seek(0, SeekOrigin.Begin);
            var actual = target.ReadDouble();

            //Assert
            actual.Should().BeApproximately(expected);
        }
        #endregion

        #region Write

        [TestMethod]
        public void Write_Double ()
        {
            var data = 123.456;
            var expected = BitConverter.GetBytes(data);
            var target = new MemoryStream();

            //Act
            target.Write(data);
            var actual = target.ToArray();

            //Assert
            actual.Should().ContainInOrder(expected);
        }
        #endregion

        #endregion

        #region Single

        #region Read

        [TestMethod]
        public void ReadSingle_ValueExists ()
        {
            var expected = 123.45F;
            var data = BitConverter.GetBytes(expected);
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadSingle();

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(EndOfStreamException))]
        public void ReadSingle_ValueNotExists ()
        {
            var target = new MemoryStream();

            //Act
            target.ReadSingle();
        }
        #endregion

        #region Roundtrip

        [TestMethod]
        public void RoundTrip_Single ()
        {
            float expected = 123.45F;
            var target = new MemoryStream();

            //Act
            target.Write(expected);
            target.Seek(0, SeekOrigin.Begin);
            var actual = target.ReadSingle();

            //Assert
            actual.Should().BeApproximately(expected);
        }
        #endregion

        #region Write

        [TestMethod]
        public void Write_Single ()
        {
            float data = 123.45F;
            var expected = BitConverter.GetBytes(data);
            var target = new MemoryStream();

            //Act
            target.Write(data);
            var actual = target.ToArray();

            //Assert
            actual.Should().ContainInOrder(expected);
        }
        #endregion

        #endregion               
    }
}
