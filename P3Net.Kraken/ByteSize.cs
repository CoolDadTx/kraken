/*
 * Copyright © 2012 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace P3Net.Kraken
{
    /// <summary>Represents a byte size.</summary>
    /// <remarks>
    /// This type can be used to convert between various byte sizes and to store a size for display.
    /// </remarks>
    [Serializable]
    public struct ByteSize : IEquatable<ByteSize>, IComparable<ByteSize>, ISerializable, IFormattable, IComparable
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="ByteSize"/> structure.</summary>
        /// <param name="bytes">The size, in bytes.</param>
        public ByteSize ( long bytes ) : this()
        {
            Bytes = bytes;
        }

        /// <summary>Creates a new instance with the given size in bytes.</summary>
        /// <param name="value">The size in bytes.</param>
        /// <returns>The new instance.</returns>
        public static ByteSize FromBytes ( long value )
        {
            return new ByteSize(value);
        }

        /// <summary>Creates a new instance with the given size in gigabytes.</summary>
        /// <param name="value">The size in gigabytes.</param>
        /// <returns>The new instance.</returns>
        public static ByteSize FromGigabytes ( double value )
        {
            return new ByteSize((long)(value * BytesInGigabytes));
        }

        /// <summary>Creates a new instance with the given size in kilobytes.</summary>
        /// <param name="value">The size in kilobytes.</param>
        /// <returns>The new instance.</returns>
        public static ByteSize FromKilobytes ( double value )
        {
            return new ByteSize((long)(value * BytesInKilobytes));
        }

        /// <summary>Creates a new instance with the given size in megabytes.</summary>
        /// <param name="value">The size in megabytes.</param>
        /// <returns>The new instance.</returns>
        public static ByteSize FromMegabytes ( double value )
        {
            return new ByteSize((long)(value * BytesInMegabytes));
        }

        /// <summary>Creates a new instance with the given size in terabytes.</summary>
        /// <param name="value">The size in terabytes.</param>
        /// <returns>The new instance.</returns>
        public static ByteSize FromTerabytes ( double value )
        {
            return new ByteSize((long)(value * BytesInTerabytes));
        }

        [ExcludeFromCodeCoverage]
        ByteSize ( SerializationInfo info, StreamingContext context ) : this()
        {
            Bytes = info.GetInt64("bytes");
        }
        #endregion

        #region Public Members

        #region Constants
        
        /// <summary>Gets the number of bytes in a gigabyte.</summary>
        public const long BytesInGigabytes = BytesInMegabytes * 1024;

        /// <summary>Gets the number of bytes in a kilobyte.</summary>
        public const long BytesInKilobytes = 1024;

        /// <summary>Gets the number of bytes in a megabyte.</summary>
        public const long BytesInMegabytes = BytesInKilobytes * 1024;

        /// <summary>Gets the number of bytes in a terabyte.</summary>
        public const long BytesInTerabytes = BytesInGigabytes * 1024;

        /// <summary>Gets a byte size of zero.</summary>
        public static readonly ByteSize Zero = new ByteSize(0);
        #endregion

        #region Attributes

        /// <summary>Gets the size in bytes.</summary>
        public long Bytes { get; private set; }

        /// <summary>Gets the size in gigabytes.</summary>
        public double Gigabytes
        {
            get { return (double)Bytes / BytesInGigabytes; }
        }

        /// <summary>Gets the size in kilobytes.</summary>
        public double Kilobytes 
        {
            get { return (double)Bytes / BytesInKilobytes; }
        }

        /// <summary>Gets the size in megabytes.</summary>
        public double Megabytes
        {
            get { return (double)Bytes / BytesInMegabytes; }
        }

        /// <summary>Gets the size in terabytes.</summary>
        public double Terabytes
        {
            get { return (double)Bytes / BytesInTerabytes; }
        }        
        #endregion

        #region Methods

        #region Add...

        /// <summary>Adds two objects and returns the new object.</summary>
        /// <param name="value">The value to add.</param>
        /// <returns>The new size.</returns>
        public ByteSize Add ( ByteSize value )
        {
            return new ByteSize(Bytes + value.Bytes);
        }

        /// <summary>Adds bytes to the object and returns the new object.</summary>
        /// <param name="value">The value to add.</param>
        /// <returns>The new size.</returns>
        public ByteSize AddBytes ( long value )
        {
            return new ByteSize(Bytes + value);
        }

        /// <summary>Adds gigabytes to the object and returns the new object.</summary>
        /// <param name="value">The value to add.</param>
        /// <returns>The new size.</returns>
        public ByteSize AddGigabytes ( long value )
        {
            return new ByteSize(Bytes + (value * BytesInGigabytes));
        }

        /// <summary>Adds kilobytes to the object and returns the new object.</summary>
        /// <param name="value">The value to add.</param>
        /// <returns>The new size.</returns>
        public ByteSize AddKilobytes ( long value )
        {
            return new ByteSize(Bytes + (value * BytesInKilobytes));
        }

        /// <summary>Adds megabytes to the object and returns the new object.</summary>
        /// <param name="value">The value to add.</param>
        /// <returns>The new size.</returns>
        public ByteSize AddMegabytes ( long value )
        {
            return new ByteSize(Bytes + (value * BytesInMegabytes));
        }

        /// <summary>Adds terabytes to the object and returns the new object.</summary>
        /// <param name="value">The value to add.</param>
        /// <returns>The new size.</returns>
        public ByteSize AddTerabytes ( long value )
        {
            return new ByteSize(Bytes + (value * BytesInTerabytes));
        }
        #endregion

        /// <summary>Compares two instances.</summary>
        /// <param name="other">The value to compare against.</param>
        /// <returns>-1 if the object is less than the argument, 0 if they are equal and 1 if the object is greater.</returns>        
        public int CompareTo ( ByteSize other )
        {
            return Bytes.CompareTo(other.Bytes);
        }

        #region Subtract...

        /// <summary>Subtracts two objects and returns the new object.</summary>
        /// <param name="value">The value to subtract.</param>
        /// <returns>The new size.</returns>
        public ByteSize Subtract ( ByteSize value )
        {
            return new ByteSize(Bytes - value.Bytes);
        }

        /// <summary>Subtracts bytes to the object and returns the new object.</summary>
        /// <param name="value">The value to subtract.</param>
        /// <returns>The new size.</returns>
        public ByteSize SubtractBytes ( long value )
        {
            return new ByteSize(Bytes - value);
        }

        /// <summary>Subtracts gigabytes to the object and returns the new object.</summary>
        /// <param name="value">The value to subtract.</param>
        /// <returns>The new size.</returns>
        public ByteSize SubtractGigabytes ( long value )
        {
            return new ByteSize(Bytes - (value * BytesInGigabytes));
        }

        /// <summary>Subtracts kilobytes to the object and returns the new object.</summary>
        /// <param name="value">The value to subtract.</param>
        /// <returns>The new size.</returns>
        public ByteSize SubtractKilobytes ( long value )
        {
            return new ByteSize(Bytes - (value * BytesInKilobytes));
        }

        /// <summary>Subtracts megabytes to the object and returns the new object.</summary>
        /// <param name="value">The value to subtract.</param>
        /// <returns>The new size.</returns>
        public ByteSize SubtractMegabytes ( long value )
        {
            return new ByteSize(Bytes - (value * BytesInMegabytes));
        }

        /// <summary>Subtracts terabytes to the object and returns the new object.</summary>
        /// <param name="value">The value to subtract.</param>
        /// <returns>The new size.</returns>
        public ByteSize SubtractTerabytes ( long value )
        {
            return new ByteSize(Bytes - (value * BytesInTerabytes));
        }
        #endregion

        #endregion

        #region Operators

        /// <summary>Less than operator.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if the left is less than the right.</returns>
        public static bool operator< ( ByteSize left, ByteSize right )
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>Less than or equal operator.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if the left is less than or equal to the right.</returns>
        public static bool operator<= ( ByteSize left, ByteSize right )
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>Greater than operator.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if the left is greater than the right.</returns>
        public static bool operator > ( ByteSize left, ByteSize right )
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>Greater than or equal operator.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><see langword="true"/> if the left is greater than or equal to the right.</returns>
        public static bool operator >= ( ByteSize left, ByteSize right )
        {
            return left.CompareTo(right) >= 0;
        }

        /// <summary>Determines if the two objects are equal.</summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if the values are equal.</returns>
        public static bool operator == ( ByteSize left, ByteSize right )
        {
            return left.Equals(right);
        }

        /// <summary>Determines if the two objects are equal.</summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if the values are equal.</returns>
        public static bool operator != ( ByteSize left, ByteSize right )
        {
            return !left.Equals(right);
        }

        /// <summary>Adds two values.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The updated value.</returns>
        public static ByteSize operator+ ( ByteSize left, ByteSize right )
        {
            return left.Add(right);
        }

        /// <summary>Adds two values.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The updated value.</returns>
        public static ByteSize operator+ ( ByteSize left, long right )
        {
            return left.AddBytes(right);
        }

        /// <summary>Subtracts two values.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The updated value.</returns>
        public static ByteSize operator- ( ByteSize left, ByteSize right )
        {
            return left.Subtract(right);
        }

        /// <summary>Subtracts two values.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The updated value.</returns>
        public static ByteSize operator- ( ByteSize left, long right )
        {
            return left.SubtractBytes(right);
        }
        #endregion

        #endregion

        #region Infrastructure
        
        /// <summary>Determines if the two objects are equal.</summary>
        /// <param name="obj">The value to compare against.</param>
        /// <returns><see langword="true"/> if the values are equal.</returns>        
        [ExcludeFromCodeCoverage]
        public override bool Equals ( object obj )
        {
            return Equals((ByteSize)obj);
        }

        /// <summary>Determines if the two objects are equal.</summary>
        /// <param name="other">The value to compare against.</param>
        /// <returns><see langword="true"/> if the values are equal.</returns>
        public bool Equals ( ByteSize other )
        {
            return Bytes == other.Bytes;
        }

        /// <summary>Gets the hash code.</summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode ()
        {
            return Bytes.GetHashCode();
        }

        #region ToString

        /// <summary>Gets the value formatted as a long string.</summary>
        /// <returns>The string equivalent.</returns>
        /// <remarks>
        /// The long string uses the format string: ",fff UUU"
        /// </remarks>
        public string ToLongString ()
        {
            return ByteSizeFormat.Format(",fff UUU", this, null);
        }

        /// <summary>Gets the value formatted as a short string.</summary>
        /// <returns>The string equivalent.</returns>
        /// <remarks>
        /// The short string uses the format string: ",f UU"
        /// </remarks>
        public string ToShortString ()
        {
            return ByteSizeFormat.Format(",f UU", this, null);
        }

        /// <summary>Gets the string representation.</summary>
        /// <returns>The string equivalent.</returns>
        /// <remarks>
        /// The format string is: ",fff UU"
        /// </remarks>
        public override string ToString ()
        {
            return ByteSizeFormat.Format(ByteSizeFormat.DefaultFormatString, this, null);
        }

        /// <summary>Gets the string representation.</summary>
        /// <param name="format">The format string.</param>
        /// <returns>The string equivalent.</returns>
        /// <remarks>
        /// The supported format strings are listed below.
        /// <list type="table">
        ///     <item>
        ///         <term>Specifier</term>
        ///         <description>Description</description>
        ///     </item>
        ///     <item>
        ///         <term>b, k, m, g, or t (case insensitive)</term>
        ///         <description>Displays the value in the given unit as a whole integer.</description>
        ///     </item>
        ///     <item>
        ///         <term>bb, kk, mm, gg or tt (case insensitive)</term>
        ///         <description>Displays the value in the given unit as a fixed point decimal with 1 digit of precision.</description>
        ///     </item>
        ///     <item>
        ///         <term>bbb, kkk, mmm, ggg or ttt (case insensitive)</term>
        ///         <description>Displays the value in the given unit as a fixed point decimal with 2 digits of precision.</description>
        ///     </item>
        ///     <item>
        ///         <term>f or F</term>
        ///         <description>Displays the value using the best unit as a whole number.</description>
        ///     </item>        
        ///     <item>
        ///         <term>ff or FF</term>
        ///         <description>Displays the value using the best unit as a fixed point decimal with 1 digit of precision.</description>
        ///     </item>        
        ///     <item>
        ///         <term>fff or FFF</term>
        ///         <description>Displays the value using the best unit as a fixed point decimal with 2 digits of precision.</description>
        ///     </item>        
        ///     <item>
        ///         <term>u and U</term>
        ///         <description>Displays the best unit as a lower or uppercase single letter (b, k, etc).</description>
        ///     </item>
        ///     <item>
        ///         <term>uu and UU</term>
        ///         <description>Displays the best unit as a lower or uppercase abbreviation (by, kb, etc).</description>
        ///     </item>
        ///     <item>
        ///         <term>uuu and UUU</term>
        ///         <description>Displays the best unit as a lower or uppercase word (bytes, kilobytes, etc).</description>
        ///     </item>
        /// </list>
        /// </remarks>
        public string ToString ( string format )
        {
            return ByteSizeFormat.Format(format, this, null);
        }

        /// <summary>Gets the string representation.</summary>
        /// <param name="provider">The format provider.</param>        
        /// <returns>The string equivalent.</returns>
        public string ToString ( IFormatProvider provider )
        {
            return ByteSizeFormat.Format(ByteSizeFormat.DefaultFormatString, this, provider);
        }

        /// <summary>Gets the string representation.</summary>
        /// <param name="format">The format string.</param>
        /// <param name="provider">The format provider.</param>        
        /// <returns>The string equivalent.</returns>
        public string ToString ( string format, IFormatProvider provider )
        {
            return ByteSizeFormat.Format(format, this, provider);
        }
        #endregion                           
        
        #endregion

        #region ISerializable Members

        [ExcludeFromCodeCoverage]
        void ISerializable.GetObjectData ( SerializationInfo info, StreamingContext context )
        {
            info.AddValue("bytes", Bytes);
        }
        #endregion
        
        #region IComparable Members

        [ExcludeFromCodeCoverage]
        int IComparable.CompareTo ( object obj )
        {
            return this.CompareTo((ByteSize)obj);
        }
        #endregion

        #region Private Members

        
        #endregion
    }
}
