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
        #region Int16

        #region Read

        [TestMethod]
        public void ReadInt16_ValueExists ()
        {
            short expected = 45;
            var data = BitConverter.GetBytes(expected);
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadInt16();

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(EndOfStreamException))]
        public void ReadInt16_ValueNotExists ()
        {
            var target = new MemoryStream();

            //Act
            target.ReadInt16();
        }
        #endregion

        #region Roundtrip

        [TestMethod]
        public void RoundTrip_Int16 ()
        {
            short expected = 45;
            var target = new MemoryStream();

            //Act
            target.Write(expected);
            target.Seek(0, SeekOrigin.Begin);
            var actual = target.ReadInt16();

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Write

        [TestMethod]
        public void Write_Int16 ()
        {
            short data = 123;
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
        
        #region Int32

        #region Read

        [TestMethod]
        public void ReadInt32_ValueExists ()
        {
            int expected = 123456;
            var data = BitConverter.GetBytes(expected);
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadInt32();

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(EndOfStreamException))]
        public void ReadInt32_ValueNotExists ()
        {
            var target = new MemoryStream();

            //Act
            target.ReadInt32();
        }
        #endregion

        #region Roundtrip

        [TestMethod]
        public void RoundTrip_Int32 ()
        {
            int expected = 12345;
            var target = new MemoryStream();

            //Act
            target.Write(expected);
            target.Seek(0, SeekOrigin.Begin);
            var actual = target.ReadInt32();

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Write

        [TestMethod]
        public void Write_Int32 ()
        {
            int data = 12345;
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

        #region Int64

        #region Read

        [TestMethod]
        public void ReadInt64_ValueExists ()
        {
            long expected = 1234567890;
            var data = BitConverter.GetBytes(expected);
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadInt64();

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(EndOfStreamException))]
        public void ReadInt64_ValueNotExists ()
        {
            var target = new MemoryStream();

            //Act
            target.ReadInt64();
        }
        #endregion

        #region Roundtrip

        [TestMethod]
        public void RoundTrip_Int64 ()
        {
            long expected = 12345678;
            var target = new MemoryStream();

            //Act
            target.Write(expected);
            target.Seek(0, SeekOrigin.Begin);
            var actual = target.ReadInt64();

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Write

        [TestMethod]
        public void Write_Int64 ()
        {
            long data = 123456789;
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

        #region SByte

        #region Read

        [TestMethod]
        public void ReadSByte_ValueExists ()
        {
            sbyte expected = -10;
            var data = BitConverter.GetBytes(expected);
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadSByte();

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(EndOfStreamException))]
        public void ReadSByte_ValueNotExists ()
        {
            var target = new MemoryStream();

            //Act
            target.ReadSByte();
        }
        #endregion

        #region Roundtrip

        [TestMethod]
        public void RoundTrip_SByte ()
        {
            sbyte expected = 123;
            var target = new MemoryStream();

            //Act
            target.Write(expected);
            target.Seek(0, SeekOrigin.Begin);
            var actual = target.ReadSByte();

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Write

        [TestMethod]
        public void Write_SByte ()
        {
            sbyte data = -100;
            var expected = new byte[] { (byte)data };
            var target = new MemoryStream();

            //Act
            target.Write(data);
            var actual = target.ToArray();

            //Assert
            actual.Should().ContainInOrder(expected);
        }
        #endregion

        #endregion

        #region UInt16

        #region Read

        [TestMethod]
        public void ReadUInt16_ValueExists ()
        {
            ushort expected = 45;
            var data = BitConverter.GetBytes(expected);
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadUInt16();

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(EndOfStreamException))]
        public void ReadUInt16_ValueNotExists ()
        {
            var target = new MemoryStream();

            //Act
            target.ReadUInt16();
        }
        #endregion

        #region Roundtrip

        [TestMethod]
        public void RoundTrip_UInt16 ()
        {
            ushort expected = 45;
            var target = new MemoryStream();

            //Act
            target.Write(expected);
            target.Seek(0, SeekOrigin.Begin);
            var actual = target.ReadUInt16();

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Write

        [TestMethod]
        public void Write_UInt16 ()
        {
            ushort data = 123;
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

        #region UInt32

        #region Read

        [TestMethod]
        public void ReadUInt32_ValueExists ()
        {
            uint expected = 123456;
            var data = BitConverter.GetBytes(expected);
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadUInt32();

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(EndOfStreamException))]
        public void ReadUInt32_ValueNotExists ()
        {
            var target = new MemoryStream();

            //Act
            target.ReadUInt32();
        }
        #endregion

        #region Roundtrip

        [TestMethod]
        public void RoundTrip_UInt32 ()
        {
            uint expected = 12345;
            var target = new MemoryStream();

            //Act
            target.Write(expected);
            target.Seek(0, SeekOrigin.Begin);
            var actual = target.ReadUInt32();

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Write

        [TestMethod]
        public void Write_UInt32 ()
        {
            uint data = 12345;
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

        #region UInt64

        #region Read

        [TestMethod]
        public void ReadUInt64_ValueExists ()
        {
            ulong expected = 1234567890;
            var data = BitConverter.GetBytes(expected);
            var target = new MemoryStream(data);

            //Act
            var actual = target.ReadUInt64();

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(EndOfStreamException))]
        public void ReadUInt64_ValueNotExists ()
        {
            var target = new MemoryStream();

            //Act
            target.ReadUInt64();
        }
        #endregion

        #region Roundtrip

        [TestMethod]
        public void RoundTrip_UInt64 ()
        {
            ulong expected = 12345678;
            var target = new MemoryStream();

            //Act
            target.Write(expected);
            target.Seek(0, SeekOrigin.Begin);
            var actual = target.ReadUInt64();

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Write

        [TestMethod]
        public void Write_UInt64 ()
        {
            ulong data = 123456789;
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
