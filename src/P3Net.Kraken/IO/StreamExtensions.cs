/*
 * Copyright © 2009 Michael Taylor
 * All Rights Reserved
 * 
 */
#region Imports

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

using P3Net.Kraken.Diagnostics;
#endregion

namespace P3Net.Kraken.IO
{
    /// <summary>Provides extension methods for <see cref="Stream">Streams</see>.</summary>
    /// <example>
    /// <code lang="C#">
    ///  Employee LoadEmployee ( Stream stream )
    ///  {
    ///      var employee = new Employee();
    ///      
    ///      employee.Id = stream.ReadInt32();
    ///      employee.FirstName = stream.ReadCompressedString();
    ///      employee.LastName = stream.ReadCompressedString();
    ///      employee.PayRate = stream.ReadDouble();
    ///      
    ///      return employee;
    ///  }
    ///  
    /// void SaveEmployee ( Stream stream, Employee employee )
    /// {
    ///    stream.Write(employee.Id);
    ///    stream.WriteCompressedStream(employee.FirstName);
    ///    stream.WriteCompressedStream(employee.LastName);
    ///    stream.Write(employee.PayRate);
    /// }
    /// </code>
    /// </example>
    public static class StreamExtensions
    {
        #region Public Members

        /// <summary>Reads a boolean value.</summary>
        /// <param name="source">The stream to use.</param>
        /// <returns>The read value.</returns>
        /// <remarks>
        /// The boolean value is a single byte.
        /// </remarks>
        /// <exception cref="EndOfStreamException">The end of stream was reached.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static bool ReadBoolean ( this Stream source )
        {
            if (source.Read(DataBuffer, 0, 1) == 0)
                throw new EndOfStreamException();

            return BitConverter.ToBoolean(DataBuffer, 0);
        }

        /// <summary>Reads an array of bytes.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>The read value.</returns>
        /// <remarks>
        /// The method loops through the stream until <paramref name="count"/> bytes are read.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than zero.</exception>
        /// <exception cref="EndOfStreamException">The end of stream was reached.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static byte[] ReadBytes ( this Stream source, int count )
        {
            Verify.Argument("count", count).IsGreaterThanOrEqualToZero();

            if (count == 0)
                return new byte[0];

            byte[] data = ReadBytesCore(source, count);

            //Since we are returning the array we need to make a copy of it if it happens to be the shared one
            if (data == DataBuffer)
            {
                byte[] newData = new byte[count];
                Buffer.BlockCopy(data, 0, newData, 0, count);

                data = newData; 
            };

            return data;
        }

        #region ReadChar
        
        /// <summary>Reads a character.</summary>
        /// <param name="source">The stream to use.</param>
        /// <returns>The read value.</returns>
        /// <exception cref="EndOfStreamException">The end of stream was reached.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static char ReadChar ( this Stream source )
        {
            return ReadChar(source, null);
        }

        /// <summary>Reads a character.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="encoding">The encoding to use.  If <see langword="null"/> is specified then the default is used.</param>
        /// <returns>The read value.</returns>
        /// <exception cref="EndOfStreamException">The end of stream was reached.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static char ReadChar ( this Stream source, Encoding encoding )
        {
            int value = ReadCharCore(source, encoding ?? Encoding.Default);
            if (value == -1)
                throw new EndOfStreamException();

            return (char)value;
        }
        #endregion

        #region ReadCompressedString
        
        /// <summary>Reads a compressed string.</summary>
        /// <param name="source">The stream to use.</param>
        /// <returns>The read value.</returns>
        /// <remarks>
        /// Compressed strings are length prefixed.  The length is 1, 2 or 4 bytes based upon the string length.  The length is in characters.
        /// </remarks>
        /// <exception cref="EndOfStreamException">The end of stream was reached.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static string ReadCompressedString ( this Stream source )
        {
            return ReadCompressedString(source, null);
        }

        /// <summary>Reads a compressed string.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="encoding">The encoding to use.  If <see langword="null"/> is specified then the default is used.</param>
        /// <returns>The read value.</returns>
        /// <remarks>
        /// Compressed strings are length prefixed.  The length is 1, 2 or 4 bytes based upon the string length.  The length is in characters.
        /// </remarks>
        /// <exception cref="EndOfStreamException">The end of stream was reached.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static string ReadCompressedString ( this Stream source, Encoding encoding )
        {
            encoding = encoding ?? Encoding.Default;

            //Get the compressed length first            
            int len = ReadCompressedInt32(source);
            var builder = new StringBuilder(len);

            for (int index = 0; index < len; ++index)
                builder.Append((char)ReadCharCore(source, encoding));

            return builder.ToString();
        }
        #endregion
               
        /// <summary>Reads a double precision real value.</summary>
        /// <param name="source">The stream to use.</param>
        /// <returns>The read value.</returns>
        /// <exception cref="EndOfStreamException">The end of stream was reached.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static double ReadDouble ( this Stream source )
        {
            byte[] data = ReadBytesCore(source, 8);
            return BitConverter.ToDouble(data, 0);
        }

        #region ReadFixedString
        
        /// <summary>Reads a fixed-size string value.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="count">The number of characters to read.</param>
        /// <returns>The read value.  Any NULL terminators or spaces are removed from the end of the string.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than zero.</exception>
        /// <exception cref="EndOfStreamException">The end of stream was reached.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static string ReadFixedString ( this Stream source, int count )
        {
            return ReadFixedString(source, count, ' ', null);
        }

        /// <summary>Reads a fixed-size string value.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="count">The number of characters to read.</param>
        /// <param name="fillChar">The character that was used to pad the end of the string.</param>
        /// <returns>The read value.  Any NULL terminators or fill characters are removed from the end of the string.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than zero.</exception>
        /// <exception cref="EndOfStreamException">The end of stream was reached.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "char")]
        public static string ReadFixedString ( this Stream source, int count, char fillChar )
        {
            return ReadFixedString(source, count, fillChar, null);
        }

        /// <summary>Reads a fixed-size string value.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="count">The number of characters to read.</param>
        /// <param name="encoding">The encoding to use.  If <see langword="null"/> is specified then the default is used.</param>
        /// <returns>The read value.  Any NULL terminators or spaces are removed from the end of the string.</returns>
        /// <remarks>
        /// The number of bytes read is dependent upon the encoding.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than zero.</exception>
        /// <exception cref="EndOfStreamException">The end of stream was reached.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static string ReadFixedString ( this Stream source, int count, Encoding encoding )
        {
            return ReadFixedString(source, count, ' ', encoding);
        }

        /// <summary>Reads a fixed-size string value.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="count">The number of characters to read.</param>
        /// <param name="fillChar">The character that was used to pad the string.</param>
        /// <param name="encoding">The encoding to use.  If <see langword="null"/> is specified then the default is used.</param>
        /// <returns>The read value.  Any NULL terminators or fill characters are removed from the end of the string.</returns>
        /// <remarks>
        /// The number of bytes read is dependent upon the encoding.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than zero.</exception>
        /// <exception cref="EndOfStreamException">The end of stream was reached.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "char")]
        public static string ReadFixedString ( this Stream source, int count, char fillChar, Encoding encoding )
        {
            Verify.Argument("count", count).IsGreaterThanOrEqualToZero();

            encoding = encoding ?? Encoding.Default;

            //Start reading characters until we get to count
            StringBuilder bldr = new StringBuilder(count);
            for (int index = 0; index < count; ++index)
            {
                int value = ReadCharCore(source, encoding);
                if (value == -1)
                    throw new EndOfStreamException();

                bldr.Append((char)value);
            };

            return bldr.ToString().Trim(fillChar, '\0');
        }
        #endregion

        /// <summary>Reads a 16-bit signed integer value.</summary>
        /// <param name="source">The stream to use.</param>
        /// <returns>The read value.</returns>
        /// <exception cref="EndOfStreamException">The end of stream was reached.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static short ReadInt16 ( this Stream source )
        {
            byte[] data = ReadBytesCore(source, 2);
            return BitConverter.ToInt16(data, 0);
        }

        /// <summary>Reads a 32-bit signed integer value.</summary>
        /// <param name="source">The stream to use.</param>
        /// <returns>The read value.</returns>
        /// <exception cref="EndOfStreamException">The end of stream was reached.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static int ReadInt32 ( this Stream source )
        {
            byte[] data = ReadBytesCore(source, 4);
            return BitConverter.ToInt32(data, 0);
        }

        /// <summary>Reads a 64-bit signed integer value.</summary>
        /// <param name="source">The stream to use.</param>
        /// <returns>The read value.</returns>
        /// <exception cref="EndOfStreamException">The end of stream was reached.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static long ReadInt64 ( this Stream source )
        {
            byte[] data = ReadBytesCore(source, 8);
            return BitConverter.ToInt64(data, 0);
        }
        
        #region ReadLengthPrefixedString

        /// <summary>Reads a string that is length prefixed..</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="length">The length.</param>
        /// <returns>The read value.</returns>
        /// <remarks>
        /// The method reads the length followed by the string.  The length is in characters.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="length"/> is invalid.</exception>
        /// <exception cref="EndOfStreamException">The end of stream was reached.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static string ReadLengthPrefixedString ( this Stream source, StringLengthPrefix length )
        {
            return ReadLengthPrefixedString(source, length, null);
        }
      
        /// <summary>Reads a string that is length prefixed.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="encoding">The encoding to use.  If <see langword="null"/> is specified then the default is used.</param>
        /// <param name="length">The length.</param>
        /// <returns>The read value.</returns>
        /// <remarks>
        /// The method reads the 4-byte length followed by the string.  The length is in characters.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="length"/> is invalid.</exception>
        /// <exception cref="EndOfStreamException">The end of stream was reached.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static string ReadLengthPrefixedString ( this Stream source, StringLengthPrefix length, Encoding encoding )
        {           
            //Read the length
            int len = 0;
            switch (length)
            {
                case StringLengthPrefix.One: len = ReadSByte(source); break;
                case StringLengthPrefix.Two: len = ReadInt16(source); break;
                case StringLengthPrefix.Four: len = ReadInt32(source); break;
                default: throw new ArgumentOutOfRangeException("length", "The length is invalid.");
            };

            //Read the (now) fixed size string
            return ReadFixedString(source, len, encoding ?? Encoding.Default);
        }
        #endregion

        #region ReadNullTerminatedString

        /// <summary>Reads a NULL terminated string.</summary>
        /// <param name="source">The stream to use.</param>
        /// <returns>The read value.</returns>
        /// <remarks>
        /// The method reads the stream until it finds a NULL terminator.  C and C++ normally use these types of strings.
        /// </remarks>
        /// <exception cref="EndOfStreamException">The end of stream was reached.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static string ReadNullTerminatedString ( this Stream source )
        {
            return ReadNullTerminatedString(source, null);
        }

        /// <summary>Reads a NULL terminated string.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="encoding">The encoding to use.  If <see langword="null"/> is specified then the default is used.</param>
        /// <returns>The read value.</returns>
        /// <remarks>
        /// The method reads the stream until it finds a NULL terminator.  C and C++ normally use these types of strings.
        /// </remarks>
        /// <exception cref="EndOfStreamException">The end of stream was reached.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static string ReadNullTerminatedString ( this Stream source, Encoding encoding )
        {
            encoding = encoding ?? Encoding.Default;

            //Since we don't know the length of the string (or even the characters)
            //we have to read a byte at a time until we find a NULL character (not byte)
            StringBuilder bldr = new StringBuilder();
            int ch = ReadCharCore(source, encoding);
            while (ch != 0)
            {
                if (ch == -1)
                    throw new EndOfStreamException();

                bldr.Append((char)ch);

                ch = ReadCharCore(source, encoding);
            };

            return bldr.ToString();
        }
        #endregion

        /// <summary>Reads a signed byte value.</summary>
        /// <param name="source">The stream to use.</param>
        /// <returns>The read value.</returns>
        /// <exception cref="EndOfStreamException">The end of stream was reached.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        [CLSCompliant(false)]
        public static sbyte ReadSByte ( this Stream source )
        {
            int value = source.ReadByte();
            if (value == -1)
                throw new EndOfStreamException();

            return (sbyte)value;
        }

        /// <summary>Reads a single precision real value.</summary>
        /// <param name="source">The stream to use.</param>
        /// <returns>The read value.</returns>
        /// <exception cref="EndOfStreamException">The end of stream was reached.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static float ReadSingle ( this Stream source )
        {
            byte[] data = ReadBytesCore(source, 4);
            return BitConverter.ToSingle(data, 0);
        }

        /// <summary>Reads a 16-bit unsigned integer value.</summary>
        /// <param name="source">The stream to use.</param>
        /// <returns>The read value.</returns>
        /// <exception cref="EndOfStreamException">The end of stream was reached.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        [CLSCompliant(false)]
        public static ushort ReadUInt16 ( this Stream source )
        {
            byte[] data = ReadBytesCore(source, 2);
            return BitConverter.ToUInt16(data, 0);
        }

        /// <summary>Reads a 32-bit unsigned integer value.</summary>
        /// <param name="source">The stream to use.</param>
        /// <returns>The read value.</returns>
        /// <exception cref="EndOfStreamException">The end of stream was reached.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        [CLSCompliant(false)]
        public static uint ReadUInt32 ( this Stream source )
        {
            byte[] data = ReadBytesCore(source, 4);
            return BitConverter.ToUInt32(data, 0);
        }

        /// <summary>Reads a 64-bit unsigned integer value.</summary>
        /// <param name="source">The stream to use.</param>
        /// <returns>The read value.</returns>
        /// <exception cref="EndOfStreamException">The end of stream was reached.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        [CLSCompliant(false)]
        public static ulong ReadUInt64 ( this Stream source )
        {
            byte[] data = ReadBytesCore(source, 8);
            return BitConverter.ToUInt64(data, 0);
        }
       
        #region Write

        /// <summary>Writes a boolean value.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="value">The value to write.</param>        
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void Write ( this Stream source, bool value )
        {
            byte[] data = BitConverter.GetBytes(value);
            source.Write(data, 0, data.Length);
        }

        /// <summary>Writes a character value.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="value">The value to write.</param>        
        /// <returns>The number of bytes written.</returns>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static int Write ( this Stream source, char value )
        {
            return Write(source, value, null);
        }
       
        /// <summary>Writes a character value.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="value">The value to write.</param>        
        /// <param name="encoding">The encoding to use.  If <see langword="null"/> is specified then the default is used.</param>
        /// <returns>The number of bytes written.</returns>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static int Write ( this Stream source, char value, Encoding encoding )
        {
            //Could use a fixed buffer here but we won't worry about it right now
            byte[] data = (encoding ?? Encoding.Default).GetBytes(new char[] { value }, 0, 1);
            source.Write(data, 0, data.Length);

            return data.Length;
        }
        
        /// <summary>Writes a double precision real value.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="value">The value to write.</param>        
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void Write ( this Stream source, double value )
        {
            byte[] data = BitConverter.GetBytes(value);
            source.Write(data, 0, data.Length);
        }

        /// <summary>Writes a 16-bit signed integer value.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="value">The value to write.</param>        
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void Write ( this Stream source, short value )
        {
            byte[] data = BitConverter.GetBytes(value);
            source.Write(data, 0, data.Length);
        }

        /// <summary>Writes a 32-bit signed integer value.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="value">The value to write.</param>        
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void Write ( this Stream source, int value )
        {
            byte[] data = BitConverter.GetBytes(value);
            source.Write(data, 0, data.Length);
        }

        /// <summary>Writes a 64-bit signed integer value.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="value">The value to write.</param>        
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void Write ( this Stream source, long value )
        {
            byte[] data = BitConverter.GetBytes(value);
            source.Write(data, 0, data.Length);
        }

        /// <summary>Writes a signed byte value.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="value">The value to write.</param>        
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        [CLSCompliant(false)]
        public static void Write ( this Stream source, sbyte value )
        {
            DataBuffer[0] = (byte)value;
            source.Write(DataBuffer, 0, 1);
        }

        /// <summary>Writes a single precision real value.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="value">The value to write.</param>        
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void Write ( this Stream source, float value )
        {
            byte[] data = BitConverter.GetBytes(value);
            source.Write(data, 0, data.Length);
        }

        /// <summary>Writes a 16-bit unsigned integer value.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="value">The value to write.</param>        
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        [CLSCompliant(false)]
        public static void Write ( this Stream source, ushort value )
        {
            byte[] data = BitConverter.GetBytes(value);
            source.Write(data, 0, data.Length);
        }

        /// <summary>Writes a 32-bit unsigned integer value.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="value">The value to write.</param>        
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        [CLSCompliant(false)]
        public static void Write ( this Stream source, uint value )
        {
            byte[] data = BitConverter.GetBytes(value);
            source.Write(data, 0, data.Length);
        }

        /// <summary>Writes a 64-bit unsigned integer value.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="value">The value to write.</param>        
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        [CLSCompliant(false)]
        public static void Write ( this Stream source, ulong value )
        {
            byte[] data = BitConverter.GetBytes(value);
            source.Write(data, 0, data.Length);
        }
        #endregion
               
        /// <summary>Writes a byte array.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="value">The value to write.</param>                
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static void WriteBytes ( this Stream source, byte[] value )
        {
            if ((value != null) && (value.Length > 0))
                source.Write(value, 0, value.Length);
        }

        #region WriteCompressedString
        
        /// <summary>Writes a compressed string.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="value">The value to write.</param>        
        /// <returns>The number of bytes written.</returns>
        /// <remarks>
        /// Compressed strings are length-prefixed.  The prefix is 1, 2 or 4 bytes.  The length is in characters.
        /// </remarks>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static int WriteCompressedString ( this Stream source, string value )
        {
            return WriteCompressedString(source, value, null);
        }
                
        /// <summary>Writes a compressed string.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="value">The value to write.</param>        
        /// <param name="encoding">The encoding to use.  If <see langword="null"/> is specified then the default is used.</param>
        /// <returns>The number of bytes written.</returns>
        /// <remarks>
        /// Compressed strings are length-prefixed.  The prefix is 1, 2 or 4 bytes.  The length is in characters.
        /// </remarks>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static int WriteCompressedString ( this Stream source, string value, Encoding encoding )
        {
            value = value ?? "";
            
            //Write the string length first
            int len = WriteCompressedInt32(source, value.Length);

            byte[] data = (encoding ?? Encoding.Default).GetBytes(value);            
            source.Write(data, 0, data.Length);

            return data.Length + len;
        }
        #endregion
      
        #region WriteFixedString
        
        /// <summary>Writes a fixed-size string.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="value">The value to write.</param>   
        /// <param name="count">The number of characters to write.</param>
        /// <returns>The number of bytes written.</returns>
        /// <remarks>
        /// The string is written out up to <paramref name="count"/> bytes.  If any space remains then spaces are used to fill in the rest.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than zero.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static int WriteFixedString ( this Stream source, string value, int count )
        {
            return WriteFixedString(source, value, count, ' ', null);
        }

        /// <summary>Writes a fixed-size string.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="value">The value to write.</param>   
        /// <param name="count">The number of characters to write.</param>
        /// <param name="fillChar">The character to use to fill in the remaining space.</param>
        /// <returns>The number of bytes written.</returns>
        /// <remarks>
        /// The string is written out up to <paramref name="count"/> bytes.  If any space remains then the <paramref name="fillChar"/>
        /// is used to fill in the rest.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than zero.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "char")]
        public static int WriteFixedString ( this Stream source, string value, int count, char fillChar )
        {
            return WriteFixedString(source, value, count, fillChar, null);
        }

        /// <summary>Writes a fixed-size string.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="value">The value to write.</param>   
        /// <param name="count">The number of characters to write.</param>
        /// <param name="encoding">The encoding to use.  If <see langword="null"/> is specified then the default is used.</param>
        /// <returns>The number of bytes written.</returns>
        /// <remarks>
        /// The string is written out up to <paramref name="count"/> characters.  If any space remains then spaces are used to fill in the rest.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than zero.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static int WriteFixedString ( this Stream source, string value, int count, Encoding encoding )
        {
            return WriteFixedString(source, value, count, ' ', encoding);
        }

        /// <summary>Writes a fixed-size string.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="value">The value to write.</param>   
        /// <param name="count">The number of characters to write.</param>
        /// <param name="fillChar">The character to use to fill in any remaining space.</param>
        /// <param name="encoding">The encoding to use.  If <see langword="null"/> is specified then the default is used.</param>
        /// <returns>The number of bytes written.</returns>
        /// <remarks>
        /// The string is written out up to <paramref name="count"/> characters.  If any space remains then the <paramref name="fillChar"/>
        /// is used to fill in the rest.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than zero.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "char")]
        public static int WriteFixedString ( this Stream source, string value, int count, char fillChar, Encoding encoding )
        {
            Verify.Argument("count", count).IsGreaterThanOrEqualToZero();

            value = value ?? "";

            //Build the final string that will be written - if it is too long it'll be dealt with later
            if (value.Length < count)
                value = value.PadRight(count, fillChar);

            //Write it out
            byte[] buffer = (encoding ?? Encoding.Default).GetBytes(value.ToCharArray(0, count));
            source.Write(buffer, 0, buffer.Length);

            return buffer.Length;
        }
        #endregion

        #region WriteLengthPrefixedString

        /// <summary>Writes a length prefixed string.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="value">The value to write.</param>   
        /// <param name="length">The length prefix to use.</param>
        /// <returns>The number of characters written.</returns>
        /// <remarks>
        /// The string length is written followed by the string.  The length is in characters.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="length"/> is invalid.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static int WriteLengthPrefixedString ( this Stream source, string value, StringLengthPrefix length )
        {
            return WriteLengthPrefixedString(source, value, length, null);
        }

        /// <summary>Writes a length prefixed string.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="value">The value to write.</param>   
        /// <param name="length">The length prefix to use.</param>
        /// <param name="encoding">The encoding to use.  If <see langword="null"/> is specified then the default is used.</param>
        /// <returns>The number of bytes written.</returns>
        /// <remarks>
        /// The string length is written followed by the string.  The length is in characters.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="length"/> is invalid.</exception>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static int WriteLengthPrefixedString ( this Stream source, string value, StringLengthPrefix length, Encoding encoding )
        {           
            value = value ?? "";

            int bytesWritten = 0;
            switch (length)
            {
                case StringLengthPrefix.One: source.Write((byte)value.Length); bytesWritten += 1; break;
                case StringLengthPrefix.Two: source.Write((short)value.Length); bytesWritten += 2; break;
                case StringLengthPrefix.Four: source.Write((int)value.Length); bytesWritten += 4; break;
                default: throw new ArgumentOutOfRangeException("length", "The length is invalid.");
            };

            return WriteFixedString(source, value, value.Length, encoding ?? Encoding.Default) + bytesWritten;
        }
        #endregion

        #region WriteNullTerminatedString

        /// <summary>Writes a NULL terminated string.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="value">The value to write.</param>        
        /// <returns>The number of bytes written.</returns>
        /// <remarks>
        /// The string is written out followed by the NULL terminator.
        /// </remarks>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static int WriteNullTerminatedString ( this Stream source, string value )
        {
            return WriteNullTerminatedString(source, value, null);
        }

        /// <summary>Writes a NULL terminated string.</summary>
        /// <param name="source">The stream to use.</param>
        /// <param name="value">The value to write.</param>        
        /// <param name="encoding">The encoding to use.  If <see langword="null"/> is specified then the default is used.</param>
        /// <returns>The number of bytes written.</returns>
        /// <remarks>
        /// The string is written out followed by the NULL terminator.
        /// </remarks>
        /// <exception cref="IOException">An IO error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public static int WriteNullTerminatedString ( this Stream source, string value, Encoding encoding )
        {
            value = value ?? "";

            //Write the value plus the terminator
            byte[] data = (encoding ?? Encoding.Default).GetBytes(value + '\0');
            source.Write(data, 0, data.Length);

            return data.Length;
        }
        #endregion

        #endregion

        #region Private Members

        #region Methods

        private static byte[] ReadBytesCore ( Stream stream, int count )
        {
            //Use the shared buffer when we can
            var data = (count <= DataBuffer.Length) ? DataBuffer: new byte[count];

            int totalRead = 0;
            do
            {
                int read = stream.Read(data, totalRead, count - totalRead);
                if (read == 0 && count > 0)
                    throw new EndOfStreamException();

                totalRead += read;
            } while (totalRead != count);

            return data;
        }

        private static int ReadCharCore ( Stream stream, Encoding encoding )
        {
            //We replicate the BinaryReader code here although it doesn't properly handle MBCS                     
            int byteCount = encoding.IsSingleByte ? 1 : 2;
            for (int index = 0; index < byteCount; ++index)
            {
                int byteValue = stream.ReadByte();
                if (byteValue == -1)  //End of stream
                    return -1;
                else
                    DataBuffer[index] = (byte)byteValue;
            };

            //Convert
            return encoding.GetChars(DataBuffer, 0, byteCount)[0];            
        }

        //The integer is read as a 7-bit encoding value so we can detect EOS
        [ExcludeFromCodeCoverage]
        private static int ReadCompressedInt32 ( Stream stream )
        {
            int value = 0;  
            int bitShift = 0; 
            byte next; 
            
            do
            {
                //Shifted too far
                if (bitShift == 35)
                    throw new IOException("Bad value");

                next = (byte)stream.ReadByte();
                value |= (next & 127) << bitShift;
                bitShift += 7;
            } while ((next & 128) != 0);

            return value;
        }

        //The integer is written as a 7-bit encoding value so we can detect EOS
        private static int WriteCompressedInt32 ( Stream stream, int value )
        {
            int len = 0;
            uint num = (uint)value;
            while (num >= 128)
            {
                stream.WriteByte((byte)(num | 128));
                num >>= 7;
                ++len;
            };

            stream.WriteByte((byte)num);
            ++len;

            return len;
        }
        #endregion
                
        #region Data

        [ExcludeFromCodeCoverage]
        private static byte[] DataBuffer
        {
            get
            {
                //No threading issues here because this is per-thread!!!
                if (s_buffer == null)
                    s_buffer = new byte[8];

                return s_buffer;
            }
        }
        
        //This is per-thread to avoid conflicts
        [ThreadStatic]
        private static byte[] s_buffer;
        #endregion

        #endregion
    }

    /// <summary>String length prefix values.</summary>
    [SuppressMessage("Microsoft.Design", "CA1027:MarkEnumsWithFlags", Justification="Not a flag")]
    [SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Justification = "Does not make sense here.")]
    public enum StringLengthPrefix
    {
        /// <summary>One byte.</summary>
        One = 1,

        /// <summary>Two bytes.</summary>
        Two = 2,

        /// <summary>Four bytes.</summary>
        Four = 4,
    }
}
