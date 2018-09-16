/*
 * Provides support for working with bit flags.
 *
 * Copyright © 2006 Michael Taylor
 * All Rights Reserved 
 */
#region Imports

using System;
using System.Diagnostics;

#endregion

namespace P3Net.Kraken
{
    /// <summary>Provides support for working with bit masks.</summary>
    public static class Bitmask
    {
        #region AllBitsSet

        /// <summary>Determines if all bits are set in a value.</summary>
        /// <param name="value">The value to check.</param>
        /// <param name="mask">The mask to look for.</param>
        /// <returns><see langword="true"/> if all the bits in the mask are set.</returns>
        /// <example>Refer to <see cref="M:AllBitsSet(Int32,Int32)">AllBitsSet</see> for an example.</example>
        [CLSCompliant(false)]
        public static bool AllBitsSet ( sbyte value, sbyte mask )
        {
            return (value & mask) == mask;
        }

        /// <summary>Determines if all bits are set in a value.</summary>
        /// <param name="value">The value to check.</param>
        /// <param name="mask">The mask to look for.</param>
        /// <returns><see langword="true"/> if all the bits in the mask are set.</returns>
        /// <example>Refer to <see cref="M:AllBitsSet(Int32,Int32)">AllBitsSet</see> for an example.</example>
        public static bool AllBitsSet ( short value, short mask )
        {
            return (value & mask) == mask;
        }

        /// <summary>Determines if all bits are set in a value.</summary>
        /// <param name="value">The value to check.</param>
        /// <param name="mask">The mask to look for.</param>
        /// <returns><see langword="true"/> if all the bits in the mask are set.</returns>
        /// <example>
        /// <code lang="C#">
        ///		static void Main ( )
        ///		{
        ///			int value = 0xFFFF0000;
        ///			int mask;
        /// 
        ///			mask = 0x00000000;
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, AllBitsSet = {2}", value, mask, Bitmask.AllBitsSet(value, mask));
        ///			mask = 0x0000FFFF;
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, AllBitsSet = {2}", value, mask, Bitmask.AllBitsSet(value, mask));
        ///			mask = 0x000F0000;
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, AllBitsSet = {2}", value, mask, Bitmask.AllBitsSet(value, mask));
        ///			mask = 0xFFFF0000;
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, AllBitsSet = {2}", value, mask, Bitmask.AllBitsSet(value, mask));
        ///		}
        /// 
        ///		//Output is:
        ///		//	Value = 0xFFFF0000, Mask = 0x00000000, AllBitsSet = 0
        ///		//	Value = 0xFFFF0000, Mask = 0x0000FFFF, AllBitsSet = 0
        ///		//	Value = 0xFFFF0000, Mask = 0x000F0000, AllBitsSet = 1
        ///		//	Value = 0xFFFF0000, Mask = 0xFFFF0000, AllBitsSet = 1
        /// </code>
        /// <code lang="VB">
        ///		Shared Sub Main ( )
        ///		
        ///			Dim value As Integer = 0xFFFF0000
        ///			Dim mask As Integer
        /// 
        ///			mask = 16#00000000
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, AllBitsSet = {2}", value, mask, Bitmask.AllBitsSet(value, mask))
        ///			mask = 16#0000FFFF
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, AllBitsSet = {2}", value, mask, Bitmask.AllBitsSet(value, mask))
        ///			mask = 16#000F0000
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, AllBitsSet = {2}", value, mask, Bitmask.AllBitsSet(value, mask))
        ///			mask = 16#FFFF0000
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, AllBitsSet = {2}", value, mask, Bitmask.AllBitsSet(value, mask))
        ///		End Sub
        /// 
        ///		'Output is:
        ///		'	Value = 0xFFFF0000, Mask = 0x00000000, AllBitsSet = 0
        ///		'	Value = 0xFFFF0000, Mask = 0x0000FFFF, AllBitsSet = 0
        ///		'	Value = 0xFFFF0000, Mask = 0x000F0000, AllBitsSet = 1
        ///		'	Value = 0xFFFF0000, Mask = 0xFFFF0000, AllBitsSet = 1
        /// </code>
        /// </example>
        public static bool AllBitsSet ( int value, int mask )
        {
            return (value & mask) == mask;
        }

        /// <summary>Determines if all bits are set in a value.</summary>
        /// <param name="value">The value to check.</param>
        /// <param name="mask">The mask to look for.</param>
        /// <returns><see langword="true"/> if all the bits in the mask are set.</returns>
        /// <example>Refer to <see cref="M:AllBitsSet(Int32,Int32)">AllBitsSet</see> for an example.</example>
        public static bool AllBitsSet ( long value, long mask )
        {
            return (value & mask) == mask;
        }

        /// <summary>Determines if all bits are set in a value.</summary>
        /// <param name="value">The value to check.</param>
        /// <param name="mask">The mask to look for.</param>
        /// <returns><see langword="true"/> if all the bits in the mask are set.</returns>
        /// <example>
        /// Refer to <see cref="M:AllBitsSet(System.Int32,System.Int32)"/> for an example.
        /// </example>
        public static bool AllBitsSet ( byte value, byte mask )
        {
            return (value & mask) == mask;
        }

        /// <summary>Determines if all bits are set in a value.</summary>
        /// <param name="value">The value to check.</param>
        /// <param name="mask">The mask to look for.</param>
        /// <returns><see langword="true"/> if all the bits in the mask are set.</returns>
        /// <example>
        /// Refer to <see cref="M:AllBitsSet(System.Int32,System.Int32)"/> for an example.
        /// </example>
        [CLSCompliant(false)]
        public static bool AllBitsSet ( ushort value, ushort mask )
        {
            return (value & mask) == mask;
        }

        /// <summary>Determines if all bits are set in a value.</summary>
        /// <param name="value">The value to check.</param>
        /// <param name="mask">The mask to look for.</param>
        /// <returns><see langword="true"/> if all the bits in the mask are set.</returns>
        /// <example>
        /// Refer to <see cref="M:AllBitsSet(System.Int32,System.Int32)"/> for an example.
        /// </example>
        [CLSCompliant(false)]
        public static bool AllBitsSet ( uint value, uint mask )
        {
            return (value & mask) == mask;
        }

        /// <summary>Determines if all bits are set in a value.</summary>
        /// <param name="value">The value to check.</param>
        /// <param name="mask">The mask to look for.</param>
        /// <returns><see langword="true"/> if all the bits in the mask are set.</returns>
        /// <example>
        /// Refer to <see cref="M:AllBitsSet(System.Int32,System.Int32)"/> for an example.
        /// </example>
        [CLSCompliant(false)]
        public static bool AllBitsSet ( ulong value, ulong mask )
        {
            return (value & mask) == mask;
        }
        #endregion

        #region AnyBitSet

        /// <summary>Determines if any bit is set in a value.</summary>
        /// <param name="value">The value to check.</param>
        /// <param name="mask">The mask to look for.</param>
        /// <returns><see langword="true"/> if all the bits in the mask are set.</returns>
        /// <example>Refer to <see cref="M:AnyBitSet(Int32,Int32)">AnyBitSet</see> for an example.</example>
        [CLSCompliant(false)]
        public static bool AnyBitSet ( sbyte value, sbyte mask )
        {
            return (value & mask) != 0;
        }

        /// <summary>Determines if any bit is set in a value.</summary>
        /// <param name="value">The value to check.</param>
        /// <param name="mask">The mask to look for.</param>
        /// <returns><see langword="true"/> if all the bits in the mask are set.</returns>
        /// <example>Refer to <see cref="M:AnyBitSet(Int32,Int32)">AnyBitSet</see> for an example.</example>
        public static bool AnyBitSet ( short value, short mask )
        {
            return (value & mask) != 0;
        }

        /// <summary>Determines if any bit is set in a value.</summary>
        /// <param name="value">The value to check.</param>
        /// <param name="mask">The mask to look for.</param>
        /// <returns><see langword="true"/> if all the bits in the mask are set.</returns>
        /// <example>
        /// <code lang="C#">
        ///		static void Main ( )
        ///		{
        ///			int value = 0xFFFF0000;
        ///			int mask;
        /// 
        ///			mask = 0x00000000;
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, AllBitsSet = {2}", value, mask, Bitmask.AnyBitSet(value, mask));
        ///			mask = 0x0000FFFF;
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, AllBitsSet = {2}", value, mask, Bitmask.AnyBitSet(value, mask));
        ///			mask = 0x000F0000;
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, AllBitsSet = {2}", value, mask, Bitmask.AnyBitSet(value, mask));
        ///			mask = 0xFFFF0000;
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, AllBitsSet = {2}", value, mask, Bitmask.AnyBitSet(value, mask));
        ///		}
        /// 
        ///		//Output is:
        ///		//	Value = 0xFFFF0000, Mask = 0x00000000, AnyBitSet = 0
        ///		//	Value = 0xFFFF0000, Mask = 0x0000FFFF, AnyBitSet = 0
        ///		//	Value = 0xFFFF0000, Mask = 0x000F0000, AnyBitSet = 1
        ///		//	Value = 0xFFFF0000, Mask = 0xFFFF0000, AnyBitSet = 1
        /// </code>
        /// <code lang="VB">
        ///		Shared Sub Main ( )
        ///		
        ///			Dim value As Integer = 0xFFFF0000
        ///			Dim mask As Integer
        /// 
        ///			mask = 16#00000000
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, AllBitsSet = {2}", value, mask, Bitmask.AnyBitSet(value, mask))
        ///			mask = 16#0000FFFF
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, AllBitsSet = {2}", value, mask, Bitmask.AnyBitSet(value, mask))
        ///			mask = 16#000F0000
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, AllBitsSet = {2}", value, mask, Bitmask.AnyBitSet(value, mask))
        ///			mask = 16#FFFF0000
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, AllBitsSet = {2}", value, mask, Bitmask.AnyBitSet(value, mask))
        ///		End Sub
        /// 
        ///		'Output is:
        ///		'	Value = 0xFFFF0000, Mask = 0x00000000, AnyBitSet = 0
        ///		'	Value = 0xFFFF0000, Mask = 0x0000FFFF, AnyBitSet = 0
        ///		'	Value = 0xFFFF0000, Mask = 0x000F0000, AnyBitSet = 1
        ///		'	Value = 0xFFFF0000, Mask = 0xFFFF0000, AnyBitSet = 1
        /// </code>
        /// </example>
        public static bool AnyBitSet ( int value, int mask )
        {
            return (value & mask) != 0;
        }

        /// <summary>Determines if any bit is set in a value.</summary>
        /// <param name="value">The value to check.</param>
        /// <param name="mask">The mask to look for.</param>
        /// <returns><see langword="true"/> if all the bits in the mask are set.</returns>
        /// <example>Refer to <see cref="M:AnyBitSet(Int32,Int32)">AnyBitSet</see> for an example.</example>
        public static bool AnyBitSet ( long value, long mask )
        {
            return (value & mask) != 0;
        }

        /// <summary>Determines if any bit is set in a value.</summary>
        /// <param name="value">The value to check.</param>
        /// <param name="mask">The mask to look for.</param>
        /// <returns><see langword="true"/> if all the bits in the mask are set.</returns>
        /// <example>
        /// Refer to <see cref="M:AnyBitSet(System.Int32,System.Int32)"/> for an example.
        /// </example>
        public static bool AnyBitSet ( byte value, byte mask )
        {
            return (value & mask) != 0;
        }

        /// <summary>Determines if any bit is set in a value.</summary>
        /// <param name="value">The value to check.</param>
        /// <param name="mask">The mask to look for.</param>
        /// <returns><see langword="true"/> if all the bits in the mask are set.</returns>
        /// <example>
        /// Refer to <see cref="M:AnyBitSet(System.Int32,System.Int32)"/> for an example.
        /// </example>
        [CLSCompliant(false)]
        public static bool AnyBitSet ( ushort value, ushort mask )
        {
            return (value & mask) != 0;
        }

        /// <summary>Determines if any bit is set in a value.</summary>
        /// <param name="value">The value to check.</param>
        /// <param name="mask">The mask to look for.</param>
        /// <returns><see langword="true"/> if all the bits in the mask are set.</returns>
        /// <example>
        /// Refer to <see cref="M:AnyBitSet(System.Int32,System.Int32)"/> for an example.
        /// </example>
        [CLSCompliant(false)]
        public static bool AnyBitSet ( uint value, uint mask )
        {
            return (value & mask) != 0;
        }

        /// <summary>Determines if any bit is set in a value.</summary>
        /// <param name="value">The value to check.</param>
        /// <param name="mask">The mask to look for.</param>
        /// <returns><see langword="true"/> if all the bits in the mask are set.</returns>
        /// <example>
        /// Refer to <see cref="M:AnyBitSet(System.Int32,System.Int32)"/> for an example.
        /// </example>
        [CLSCompliant(false)]
        public static bool AnyBitSet ( ulong value, ulong mask )
        {
            return (value & mask) != 0;
        }
        #endregion

        #region ClearBits

        /// <summary>Clears a set of bits in a value.</summary>
        /// <param name="value">The value to mask.</param>
        /// <param name="mask">The bit(s) to set.</param>
        /// <returns>The new value.</returns>
        /// <example>
        /// Refer to <see cref="M:ClearBits(System.Int32,System.Int32)"/> for an example.
        /// </example>
        [CLSCompliant(false)]
        public static sbyte ClearBits ( sbyte value, sbyte mask )
        {
            return value &= (sbyte)~mask;
        }

        /// <summary>Clears a set of bits in a value.</summary>
        /// <param name="value">The value to mask.</param>
        /// <param name="mask">The bit(s) to set.</param>
        /// <returns>The new value.</returns>
        /// <example>
        /// Refer to <see cref="M:ClearBits(System.Int32,System.Int32)"/> for an example.
        /// </example>
        public static short ClearBits ( short value, short mask )
        {
            return value &= (short)~mask;
        }

        /// <summary>Clears a set of bits in a value.</summary>
        /// <param name="value">The value to change.</param>
        /// <param name="mask">The bit(s) to clear.</param>
        /// <returns>The new value.</returns>
        /// <example>
        /// <code lang="C#">
        ///		static void Main ( )
        ///		{
        ///			int value = 0x0F0F0F0F;
        ///			int mask;
        /// 
        ///			mask = 0xF0F0F0F0;
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, Result = 0x{2:X}", value, mask, Bitmask.ClearBits(value, mask));
        /// 
        ///			mask = 0x00000000FFFFFFFF;
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, Result = 0x{2:X}", value, mask, Bitmask.ClearBits(value, mask));
        /// 
        ///			mask = 0x0000000000000000;
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, Result = 0x{2:X}", value, mask, Bitmask.ClearBits(value, mask));
        ///		}
        /// 
        ///		//Output is:
        ///		//	Value = 0x0F0F0F0F, Mask = 0xF0F0F0F0, Result = 0x0F0F0F0F
        ///		//	Value = 0x0F0F0F0F, Mask = 0x0000FFFF, Result = 0x0F0F0000
        ///		//	Value = 0x0F0F0F0F, Mask = 0x00000000, Result = 0x0F0F0F0F
        /// </code>
        /// <code lang="VB">
        ///		Shared Sub Main ( )
        ///		
        ///			Dim value As Integer = 0x0F0F0F0F
        ///			Dim mask As Integer
        /// 
        ///			mask = 0xF0F0F0F0
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, Result = 0x{2:X}", value, mask, Bitmask.SetBits(value, mask))
        /// 
        ///			mask = 0x0000FFFF
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, Result = 0x{2:X}", value, mask, Bitmask.SetBits(value, mask))
        /// 
        ///			mask = 0x00000000
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, Result = 0x{2:X}", value, mask, Bitmask.SetBits(value, mask))
        ///		End Sub
        /// 
        ///		'Output is:
        ///		'	Value = 0x0F0F0F0F, Mask = 0xF0F0F0F0, Result = 0x0F0F0F0F
        ///		'	Value = 0x0F0F0F0F, Mask = 0x0000FFFF, Result = 0x0F0F0000
        ///		'	Value = 0x0F0F0F0F, Mask = 0x00000000, Result = 0x0F0F0F0F
        /// </code>
        /// </example>
        public static int ClearBits ( int value, int mask )
        {
            return value &= ~mask;
        }

        /// <summary>Clears a set of bits in a value.</summary>
        /// <param name="value">The value to mask.</param>
        /// <param name="mask">The bit(s) to set.</param>
        /// <returns>The new value.</returns>
        /// <example>
        /// Refer to <see cref="M:ClearBits(System.Int32,System.Int32)"/> for an example.
        /// </example>
        public static long ClearBits ( long value, long mask )
        {
            return value &= ~mask;
        }

        /// <summary>Clears a set of bits in a value.</summary>
        /// <param name="value">The value to mask.</param>
        /// <param name="mask">The bit(s) to set.</param>
        /// <returns>The new value.</returns>
        /// <example>
        /// Refer to <see cref="M:ClearBits(System.Int32,System.Int32)"/> for an example.
        /// </example>
        public static byte ClearBits ( byte value, byte mask )
        {
            return value &= (byte)~mask;
        }

        /// <summary>Clears a set of bits in a value.</summary>
        /// <param name="value">The value to mask.</param>
        /// <param name="mask">The bit(s) to set.</param>
        /// <returns>The new value.</returns>
        /// <example>
        /// Refer to <see cref="M:ClearBits(System.Int32,System.Int32)"/> for an example.
        /// </example>
        [CLSCompliant(false)]
        public static ushort ClearBits ( ushort value, ushort mask )
        {
            return value &= (ushort)~mask;
        }

        /// <summary>Clears a set of bits in a value.</summary>
        /// <param name="value">The value to mask.</param>
        /// <param name="mask">The bit(s) to set.</param>
        /// <returns>The new value.</returns>
        /// <example>
        /// Refer to <see cref="M:ClearBits(System.Int32,System.Int32)"/> for an example.
        /// </example>
        [CLSCompliant(false)]
        public static uint ClearBits ( uint value, uint mask )
        {
            return value &= ~mask;
        }

        /// <summary>Clears a set of bits in a value.</summary>
        /// <param name="value">The value to change.</param>
        /// <param name="mask">The bit(s) to clear.</param>
        /// <returns>The new value.</returns>
        /// <example>
        /// Refer to <see cref="M:ClearBits(System.Int64,System.Int64)"/> for an example.
        /// </example>		
        [CLSCompliant(false)]
        public static ulong ClearBits ( ulong value, ulong mask )
        {
            return value &= ~mask;
        }
        #endregion

        #region SetBits

        /// <summary>Sets a bit mask value.</summary>
        /// <param name="value">The value to mask.</param>
        /// <param name="mask">The bit(s) to set.</param>
        /// <returns>The new value.</returns>
        /// <example>
        /// Refer to <see cref="M:SetBits(System.Int32,System.Int32)"/> for an example.
        /// </example>        
        [CLSCompliant(false)]
        public static sbyte SetBits ( sbyte value, sbyte mask )
        {
            return (sbyte)(value | mask);
        }

        /// <summary>Sets a bit mask value.</summary>
        /// <param name="value">The value to mask.</param>
        /// <param name="mask">The bit(s) to set.</param>
        /// <returns>The new value.</returns>
        /// <example>
        /// Refer to <see cref="M:SetBits(System.Int32,System.Int32)"/> for an example.
        /// </example>        
        public static short SetBits ( short value, short mask )
        {
            return (short)(value | mask);
        }

        /// <summary>Sets a bit mask value.</summary>
        /// <param name="value">The value to mask.</param>
        /// <param name="mask">The bit(s) to set.</param>
        /// <returns>The new value.</returns>
        /// <example>
        /// <code lang="C#">
        ///		static void Main ( )
        ///		{
        ///			int value = 0x0F0F0F0F;
        ///			int mask;
        /// 
        ///			mask = 0xF0F0F0F0;
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, Result = 0x{2:X}", value, mask, Bitmask.SetBits(value, mask));
        /// 
        ///			mask = 0x0000FFFF;
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, Result = 0x{2:X}", value, mask, Bitmask.SetBits(value, mask));
        /// 
        ///			mask = 0x00000000;
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, Result = 0x{2:X}", value, mask, Bitmask.SetBits(value, mask));
        ///		}
        /// 
        ///		//Output is:
        ///		//	Value = 0x0F0F0F0F, Mask = 0xF0F0F0F0, Result = 0xFFFFFFFF
        ///		//	Value = 0x0F0F0F0F, Mask = 0x0000FFFF, Result = 0x0F0FFFFF
        ///		//	Value = 0x0F0F0F0F, Mask = 0x00000000, Result = 0x0F0F0F0F
        /// </code>
        /// <code lang="VB">
        ///		Shared Sub Main ( )
        ///		
        ///			Dim value As Integer = 0x0F0F0F0F
        ///			Dim mask As Iteger
        /// 
        ///			mask = 0xF0F0F0F0
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, Result = 0x{2:X}", value, mask, Bitmask.SetBits(value, mask))
        /// 
        ///			mask = 0x0000FFFF
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, Result = 0x{2:X}", value, mask, Bitmask.SetBits(value, mask))
        /// 
        ///			mask = 0x00000000
        ///			Console.WriteLine("Value = 0x{0:X}, Mask = 0x{1:X}, Result = 0x{2:X}", value, mask, Bitmask.SetBits(value, mask))
        ///		End Sub
        /// 
        ///		'Output is:
        ///		'	Value = 0x0F0F0F0F, Mask = 0xF0F0F0F0, Result = 0xFFFFFFFF
        ///		'	Value = 0x0F0F0F0F, Mask = 0x0000FFFF, Result = 0x0F0FFFFFF
        ///		'	Value = 0x0F0F0F0F, Mask = 0x00000000, Result = 0x0F0F0F0F
        /// </code>
        /// </example>
        public static int SetBits ( int value, int mask )
        {
            return value | mask;
        }

        /// <summary>Sets a bit mask value.</summary>
        /// <param name="value">The value to mask.</param>
        /// <param name="mask">The bit(s) to set.</param>
        /// <returns>The new value.</returns>
        /// <example>
        /// Refer to <see cref="M:SetBits(System.Int32,System.Int32)"/> for an example.
        /// </example>        
        public static long SetBits ( long value, long mask )
        {
            return value | mask;
        }

        /// <summary>Sets a bit mask value.</summary>
        /// <param name="value">The value to mask.</param>
        /// <param name="mask">The bit(s) to set.</param>
        /// <returns>The new value.</returns>
        /// <example>
        /// Refer to <see cref="M:SetBits(System.Int32,System.Int32)"/> for an example.
        /// </example>
        public static byte SetBits ( byte value, byte mask )
        {
            return (byte)(value | mask);
        }

        /// <summary>Sets a bit mask value.</summary>
        /// <param name="value">The value to mask.</param>
        /// <param name="mask">The bit(s) to set.</param>
        /// <returns>The new value.</returns>
        /// <example>
        /// Refer to <see cref="M:SetBits(System.Int32,System.Int32)"/> for an example.
        /// </example>
        [CLSCompliant(false)]
        public static ushort SetBits ( ushort value, ushort mask )
        {
            return (ushort)(value | mask);
        }

        /// <summary>Sets a bit mask value.</summary>
        /// <param name="value">The value to mask.</param>
        /// <param name="mask">The bit(s) to set.</param>
        /// <returns>The new value.</returns>
        /// <example>
        /// Refer to <see cref="M:SetBits(System.Int32,System.Int32)"/> for an example.
        /// </example>
        [CLSCompliant(false)]
        public static uint SetBits ( uint value, uint mask )
        {
            return value | mask;
        }

        /// <summary>Sets a bit mask value.</summary>
        /// <param name="value">The value to mask.</param>
        /// <param name="mask">The bit(s) to set.</param>
        /// <returns>The new value.</returns>
        /// <example>
        /// Refer to <see cref="M:SetBits(System.Int32,System.Int32)"/> for an example.
        /// </example>
        [CLSCompliant(false)]
        public static ulong SetBits ( ulong value, ulong mask )
        {
            return value | mask;
        }
        #endregion
    }
}
