#region Imports

using System;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken
{
    [TestClass]
    public class BitmaskTests : UnitTest
    {
        #region AllBitsSet

        #region SByte
                
        [TestMethod]
        public void AllBitsSet_SByteHasAllBitsSet ()
        {
            sbyte target = 0x11;

            //Act
            var actual = Bitmask.AllBitsSet(target, (sbyte)0x11);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void AllBitsSet_SByteHasSomeBitsSet ()
        {
            sbyte target = 0x01;

            //Act
            var actual = Bitmask.AllBitsSet(target, (sbyte)0x11);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region Int16

        [TestMethod]
        public void AllBitsSet_Int16HasAllBitsSet ()
        {
            short target = 0x0101;

            //Act
            var actual = Bitmask.AllBitsSet(target, (short)0x0101);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void AllBitsSet_Int16HasSomeBitsSet ()
        {
            short target = 0x0001;

            //Act
            var actual = Bitmask.AllBitsSet(target, (short)0x0101);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region Int32
              
        [TestMethod]
        public void AllBitsSet_Int32HasAllBitsSet ()
        {
            int target = 0x00010100;

            //Act
            var actual = Bitmask.AllBitsSet(target, 0x00010100);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void AllBitsSet_Int32HasSomeBitsSet ()
        {
            int target = 0x00010000;

            //Act
            var actual = Bitmask.AllBitsSet(target, 0x00011000);

            //Assert
            actual.Should().BeFalse();
        }        
        #endregion

        #region Int64

        [TestMethod]
        public void AllBitsSet_Int64HasAllBitsSet ()
        {
            long target = 0x00010100;

            //Act
            var actual = Bitmask.AllBitsSet(target, 0x00010100);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void AllBitsSet_Int64HasSomeBitsSet ()
        {
            long target = 0x00010000;

            //Act
            var actual = Bitmask.AllBitsSet(target, 0x00011000);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region Byte

        [TestMethod]
        public void AllBitsSet_ByteHasAllBitsSet ()
        {
            byte target = 0x11;

            //Act
            var actual = Bitmask.AllBitsSet(target, (byte)0x11);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void AllBitsSet_ByteHasSomeBitsSet ()
        {
            sbyte target = 0x01;

            //Act
            var actual = Bitmask.AllBitsSet(target, (byte)0x11);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region UInt16

        [TestMethod]
        public void AllBitsSet_UInt16HasAllBitsSet ()
        {
            ushort target = 0x0101;

            //Act
            var actual = Bitmask.AllBitsSet(target, (ushort)0x0101);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void AllBitsSet_UInt16HasSomeBitsSet ()
        {
            ushort target = 0x0001;

            //Act
            var actual = Bitmask.AllBitsSet(target, (ushort)0x0101);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region UInt32
      
        [TestMethod]
        public void AllBitsSet_UInt32HasAllBitsSet ()
        {
            uint target = 0x00010100;

            //Act
            var actual = Bitmask.AllBitsSet(target, 0x00010100);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void AllBitsSet_UInt32HasSomeBitsSet ()
        {
            uint target = 0x00010000;

            //Act
            var actual = Bitmask.AllBitsSet(target, 0x00011000);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region UInt64

        [TestMethod]
        public void AllBitsSet_UInt64HasAllBitsSet ()
        {
            ulong target = 0x00010100;

            //Act
            var actual = Bitmask.AllBitsSet(target, 0x00010100);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void AllBitsSet_UInt64HasSomeBitsSet ()
        {
            ulong target = 0x00010000;

            //Act
            var actual = Bitmask.AllBitsSet(target, 0x00011000);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #endregion

        #region AnyBitSet

        #region SByte

        [TestMethod]
        public void AnyBitSet_SByteHasSomeBitsSet ()
        {
            sbyte target = 0x01;

            //Act
            var actual = Bitmask.AnyBitSet(target, (sbyte)0x11);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void AnyBitSet_SByteHasNoBitsSet ()
        {
            sbyte target = 0x01;

            //Act
            var actual = Bitmask.AnyBitSet(target, (sbyte)0x10);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region Int16

        [TestMethod]
        public void AnyBitSet_Int16HasSomeBitsSet ()
        {
            short target = 0x0101;

            //Act
            var actual = Bitmask.AnyBitSet(target, (short)0x0001);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void AnyBitSet_Int16HasNoBitsSet ()
        {
            short target = 0x0001;

            //Act
            var actual = Bitmask.AnyBitSet(target, (short)0x0100);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region Int32

        [TestMethod]
        public void AnyBitSet_Int32HasSomeBitsSet ()
        {
            int target = 0x00010100;

            //Act
            var actual = Bitmask.AnyBitSet(target, 0x00010000);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void AnyBitSet_Int32HasNoBitsSet ()
        {
            int target = 0x00010000;

            //Act
            var actual = Bitmask.AnyBitSet(target, 0x00001000);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region Int64

        [TestMethod]
        public void AnyBitSet_Int64HasSomeBitsSet ()
        {
            long target = 0x00010100;

            //Act
            var actual = Bitmask.AnyBitSet(target, 0x00010000);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void AnyBitSet_Int64HasNoBitsSet ()
        {
            long target = 0x00010000;

            //Act
            var actual = Bitmask.AnyBitSet(target, 0x00001000);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region Byte

        [TestMethod]
        public void AnyBitSet_ByteHasSomeBitsSet ()
        {
            byte target = 0x10;

            //Act
            var actual = Bitmask.AnyBitSet(target, (byte)0x11);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void AnyBitSet_ByteHasNoBitsSet ()
        {
            sbyte target = 0x01;

            //Act
            var actual = Bitmask.AnyBitSet(target, (byte)0x10);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region UInt16

        [TestMethod]
        public void AnyBitSet_UInt16HasSomeBitsSet ()
        {
            ushort target = 0x0101;

            //Act
            var actual = Bitmask.AnyBitSet(target, (ushort)0x0100);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void AnyBitSet_UInt16HasNoBitsSet ()
        {
            ushort target = 0x0001;

            //Act
            var actual = Bitmask.AnyBitSet(target, (ushort)0x0100);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region UInt32

        [TestMethod]
        public void AnyBitSet_UInt32HasSomeBitsSet ()
        {
            uint target = 0x00010100;

            //Act
            var actual = Bitmask.AnyBitSet(target, 0x00010000);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void AnyBitSet_UInt32HasNoBitsSet ()
        {
            uint target = 0x00010000;

            //Act
            var actual = Bitmask.AnyBitSet(target, 0x00001000);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region UInt64

        [TestMethod]
        public void AnyBitSet_UInt64HasSomeBitsSet ()
        {
            ulong target = 0x00010100;

            //Act
            var actual = Bitmask.AnyBitSet(target, 0x00010000);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void AnyBitSet_UInt64HasNoBitsSet ()
        {
            ulong target = 0x00010000;

            //Act
            var actual = Bitmask.AnyBitSet(target, 0x00001000);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #endregion

        #region ClearBits

        #region SByte

        [TestMethod]
        public void ClearBits_SByteNoBitsWereSet ()
        {
            sbyte expected = 0x10;
            sbyte target = 0x10;
            sbyte mask = 0x01;

            //Act
            var actual = Bitmask.ClearBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ClearBits_SByteSomeBitsWereSet ()
        {
            sbyte expected = 0x00;
            sbyte target = 0x10;
            sbyte mask = 0x11;

            //Act
            var actual = Bitmask.ClearBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Int16

        [TestMethod]
        public void ClearBits_Int16NoBitsWereSet ()
        {
            short expected = 0x1100;
            short target = 0x1100;
            short mask = 0x0011;

            //Act
            var actual = Bitmask.ClearBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ClearBits_Int16SomeBitsWereSet ()
        {
            short expected = 0x0100;
            short target = 0x1100;
            short mask = 0x1000;

            //Act
            var actual = Bitmask.ClearBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Int32
        
        [TestMethod]
        public void ClearBits_Int32NoBitsWereSet ()
        {
            int expected = 0x11110000;
            int target = 0x11110000;
            int mask = 0x00001111;

            //Act
            var actual = Bitmask.ClearBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ClearBits_Int32SomeBitsWereSet ()
        {
            int expected = 0x00110000;
            int target = 0x11110000;
            int mask = 0x11000000;

            //Act
            var actual = Bitmask.ClearBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Int64

        [TestMethod]
        public void ClearBits_Int64NoBitsWereSet ()
        {
            long expected = 0x1111000011110000;
            long target = 0x1111000011110000;
            long mask = 0x0000111100001111;

            //Act
            var actual = Bitmask.ClearBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ClearBits_Int64SomeBitsWereSet ()
        {
            long expected = 0x0011000000110000;
            long target = 0x1111000011110000;
            long mask = 0x1100000011000000;

            //Act
            var actual = Bitmask.ClearBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Byte

        [TestMethod]
        public void ClearBits_ByteNoBitsWereSet ()
        {
            byte expected = 0x10;
            byte target = 0x10;
            byte mask = 0x01;

            //Act
            var actual = Bitmask.ClearBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ClearBits_ByteSomeBitsWereSet ()
        {
            byte expected = 0x00;
            byte target = 0x10;
            byte mask = 0x11;

            //Act
            var actual = Bitmask.ClearBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region UInt16

        [TestMethod]
        public void ClearBits_UInt16NoBitsWereSet ()
        {
            ushort expected = 0x1100;
            ushort target = 0x1100;
            ushort mask = 0x0011;

            //Act
            var actual = Bitmask.ClearBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ClearBits_UInt16SomeBitsWereSet ()
        {
            ushort expected = 0x0100;
            ushort target = 0x1100;
            ushort mask = 0x1000;

            //Act
            var actual = Bitmask.ClearBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region UInt32

        [TestMethod]
        public void ClearBits_UInt32NoBitsWereSet ()
        {
            uint expected = 0x11110000;
            uint target = 0x11110000;
            uint mask = 0x00001111;

            //Act
            var actual = Bitmask.ClearBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ClearBits_UInt32SomeBitsWereSet ()
        {
            uint expected = 0x00110000;
            uint target = 0x11110000;
            uint mask = 0x11000000;

            //Act
            var actual = Bitmask.ClearBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region UInt64

        [TestMethod]
        public void ClearBits_UInt64NoBitsWereSet ()
        {
            ulong expected = 0x1111000011110000;
            ulong target = 0x1111000011110000;
            ulong mask = 0x0000111100001111;

            //Act
            var actual = Bitmask.ClearBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ClearBits_UInt64SomeBitsWereSet ()
        {
            ulong expected = 0x0011000000110000;
            ulong target = 0x1111000011110000;
            ulong mask = 0x1100000011000000;

            //Act
            var actual = Bitmask.ClearBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #endregion

        #region SetBits

        #region SByte

        [TestMethod]
        public void SetBits_SByteNoBitsWereSet ()
        {
            sbyte expected = 0x11;
            sbyte target = 0x10;
            sbyte mask = 0x01;

            //Act
            var actual = Bitmask.SetBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void SetBits_SByteSomeBitsWereSet ()
        {
            sbyte expected = 0x11;
            sbyte target = 0x10;
            sbyte mask = 0x11;

            //Act
            var actual = Bitmask.SetBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Int16

        [TestMethod]
        public void SetBits_Int16NoBitsWereSet ()
        {
            short expected = 0x1011;
            short target = 0x1000;
            short mask = 0x0011;

            //Act
            var actual = Bitmask.SetBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void SetBits_Int16SomeBitsWereSet ()
        {
            short expected = 0x1100;
            short target = 0x1100;
            short mask = 0x1000;

            //Act
            var actual = Bitmask.SetBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Int32

        [TestMethod]
        public void SetBits_Int32NoBitsWereSet ()
        {
            int expected = 0x10101111;
            int target =   0x10100000;
            int mask =     0x00001111;

            //Act
            var actual = Bitmask.SetBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void SetBits_Int32SomeBitsWereSet ()
        {
            int expected = 0x11110000;
            int target = 0x11110000;
            int mask = 0x11000000;

            //Act
            var actual = Bitmask.SetBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Int64

        [TestMethod]
        public void SetBits_Int64NoBitsWereSet ()
        {
            long expected = 0x1111011011111100;
            long target =   0x1111000011110000;
            long mask =     0x0000011000001100;

            //Act
            var actual = Bitmask.SetBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void SetBits_Int64SomeBitsWereSet ()
        {
            long expected = 0x1111000011110000;
            long target =   0x1111000011110000;
            long mask =     0x1100000011000000;

            //Act
            var actual = Bitmask.SetBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Byte

        [TestMethod]
        public void SetBits_ByteNoBitsWereSet ()
        {
            byte expected = 0x11;
            byte target = 0x10;
            byte mask = 0x01;

            //Act
            var actual = Bitmask.SetBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void SetBits_ByteSomeBitsWereSet ()
        {
            byte expected = 0x11;
            byte target = 0x10;
            byte mask = 0x11;

            //Act
            var actual = Bitmask.SetBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region UInt16

        [TestMethod]
        public void SetBits_UInt16NoBitsWereSet ()
        {
            ushort expected = 0x1011;
            ushort target =   0x1000;
            ushort mask =     0x0011;

            //Act
            var actual = Bitmask.SetBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void SetBits_UInt16SomeBitsWereSet ()
        {
            ushort expected = 0x1100;
            ushort target =   0x1100;
            ushort mask =     0x1000;

            //Act
            var actual = Bitmask.SetBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region UInt32

        [TestMethod]
        public void SetBits_UInt32NoBitsWereSet ()
        {
            uint expected = 0x10101111;
            uint target =   0x10100000;
            uint mask =     0x00001111;

            //Act
            var actual = Bitmask.SetBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void SetBits_UInt32SomeBitsWereSet ()
        {
            uint expected = 0x11110000;
            uint target =   0x11110000;
            uint mask =     0x11000000;

            //Act
            var actual = Bitmask.SetBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region UInt64

        [TestMethod]
        public void SetBits_UInt64NoBitsWereSet ()
        {
            ulong expected = 0x1010111110101111;
            ulong target =   0x1010000010100000;
            ulong mask =     0x0000111100001111;

            //Act
            var actual = Bitmask.SetBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void SetBits_UInt64SomeBitsWereSet ()
        {
            ulong expected = 0x1111000011110000;
            ulong target =   0x1111000011110000;
            ulong mask =     0x1100000011000000;

            //Act
            var actual = Bitmask.SetBits(target, mask);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #endregion
    }
}
