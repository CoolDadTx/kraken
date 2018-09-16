/*
 * Copyright © 2008 Michael L Taylor
 * All Rights Reserved
 * 
 * $Header: $
 */
#region Imports

using System;
using System.IO;
using System.Text;

using P3Net.Kraken.Diagnostics;
#endregion

namespace P3Net.Kraken.IO
{
	/// <summary>Provides a string exposed as a stream.</summary>
	/// <remarks>
	/// The stream is not writable.
	/// <para/>
	/// Portions of this code from <i>.NET Matters, MSDN Magazine, July 2005</i>.	
	/// </remarks>
	/// <example>
	/// <code lang="C#">
	/// static void Main ( string[] arguments )
	/// {
	///    StringBuilder bldr = new StringBuilder();
	///    foreach(string argument in arguments)
	///       bldr.Append(argument + " ");
	///       
	///    using(StringStream stream = new StringStream(bldr.ToString()))
	///    {
	///       byte[] data;
	///       
	///       using(EncryptStream encryptor = new EncryptStream(stream))
	///       {
	///          data = encryptor.Encrypt();
	///       };
	///    };
	/// }
	/// </code>
	/// <code lang="VB">
	/// Shared Sub Main ( arguments() As String )
	///    Dim bldr As New StringBuilder()
	///    
	///    For Each argument As String In arguments
	///       bldr.Append(argument &amp; " ")
	///    Next
	///    
	///    Using stream As New StringStream(bldr.ToString())
	///       Dim data() As Byte
	///       
	///       Using encryptor As New EncryptStream(stream)
	///          data = encryptor.Encrypt()
	///       End Using
	///    End Using
	/// End Sub
	/// </code>
	/// </example>
	[CodeNotAnalyzed]
	[CodeNotTested]
	public sealed class StringStream : Stream
	{
		#region Construction

		/// <summary>Initializes an instance of the <see cref="StringStream"/> class.</summary>
		/// <param name="value">The string to wrap.</param>		
		public StringStream ( string value ) : this(value, null)
		{ }

		/// <summary>Initializes an instance of the <see cref="StringStream"/> class.</summary>
		/// <param name="value">The string to wrap.</param>
		/// <param name="encoding">The encoding to use for the stream.</param>
		public StringStream ( string value, Encoding encoding )
		{
			value = value ?? "";

			//If using default encoding then we're OK
			if ((encoding == null) || (String.Compare(encoding.EncodingName, Encoding.Unicode.EncodingName, StringComparison.OrdinalIgnoreCase) == 0))
			{
				m_useDefaultEncoding = true;

				m_value = value ?? "";
				m_len = m_value.Length * 2;
			} else
			{				
				m_buffer = encoding.GetBytes(value);
				m_len = m_buffer.Length;				
			};
		}
		#endregion

		#region Public Members

		#region Attributes

		/// <summary>Determines if the stream can be read.</summary>
		/// <value>Always returns <see langword="true"/>.</value>
		public override bool CanRead
		{
			get { return true; }
		}

		/// <summary>Determines if the stream supports seeking.</summary>
		/// <value>Always returns <see langword="true"/>.</value>
		public override bool CanSeek
		{
			get { return true; }
		}

		/// <summary>Determines if the stream can be written.</summary>
		/// <value>Always returns <see langword="false"/>.</value>
		public override bool CanWrite
		{
			get { return false; }
		}

		/// <summary>Gets the length of the stream.</summary>
		public override long Length
		{
			get { return m_len; }
		}

		/// <summary>Gets or sets the current position within the stream.</summary>
		/// <exception cref="ArgumentOutOfRangeException">When setting the property and the value is less than 0 or greater than the length of the stream.</exception>
		public override long Position
		{
			get { return m_pos; }

			set
			{
                Verify.Argument("value", value).IsBetween(0, Length);

				m_pos = (int)value;
			}
		}
		#endregion

		/// <summary>This method is not supported.</summary>
		public override void Flush ( )
		{
			throw new NotSupportedException();
		}

		/// <summary>Reads from the stream.</summary>
		/// <param name="buffer">The buffer to fill.</param>
		/// <param name="offset">The offset within the buffer to write.</param>
		/// <param name="count">The number of bytes to read.</param>
		/// <returns>The number of bytes read.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="buffer"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/> or <paramref name="count"/> is negative.</exception>
		/// <exception cref="ArgumentException">The sum of <paramref name="offset"/> and <paramref name="count"/> is larger than the buffer length.</exception>
		public override int Read ( byte[] buffer, int offset, int count )
		{
            Verify.Argument("buffer", buffer).IsNotNull();
            Verify.Argument("offset", offset).IsGreaterThanOrEqualToZero();
            Verify.Argument("count", count).IsGreaterThanOrEqualToZero();
			if (buffer.Length < offset + count)
				throw new ArgumentException("Offset plus count is larger than the buffer length.", "buffer");
			
			int read = 0;
			if (m_useDefaultEncoding)
			{
				while (read < count)
				{
					if (m_pos >= Length)
						return read;

					char ch = m_value[m_pos / 2];
					buffer[offset + read] = (byte)((m_pos % 2 == 0) ? ch & 0xFF : (ch >> 8) & 0xFF);
					++m_pos;
					++read;
				};
			} else
			{
				read = Math.Min(count, (int)(Length - m_pos));

				//Copy the buffer
				Buffer.BlockCopy(m_buffer, m_pos, buffer, offset, read);
			};

			return read;
		}

		/// <summary>Reads a single byte from the stream.</summary>
		/// <returns>The next byte from the stream.</returns>
		public override int ReadByte ( )
		{			
			if (m_pos < 0)
				throw new InvalidOperationException("Beyond the end of the file.");

			if (m_pos < Length)
			{
				int value = -1;

				if (m_useDefaultEncoding)
				{
					char ch = m_value[m_pos / 2];
					value = (byte)((m_pos % 2 == 0) ? ch & 0xFF : (ch >> 8) & 0xFF);
				} else
					value = m_buffer[m_pos];

				++m_pos;
				return value;
			} else
				return -1;
		}

		/// <summary>Seeks to a new position in the stream.</summary>
		/// <param name="offset">The offset to seek from the origin.</param>
		/// <param name="origin">The starting position for the seek.</param>
		/// <returns>The new position.</returns>
		/// <exception cref="ArgumentOutOfRangeException">The new position would be less than 0 or greater than the length of the stream.</exception>
		public override long Seek ( long offset, SeekOrigin origin )
		{
			switch (origin)
			{
				case SeekOrigin.Begin: Position = offset; break;
				case SeekOrigin.Current: Position += offset; break;
				case SeekOrigin.End: Position = Length + offset; break;
			};

			return Position;
		}

		/// <summary>This method is not supported.</summary>
		/// <param name="value">The new length.</param>
		/// <exception cref="NotSupportedException">Always thrown.</exception>
		public override void SetLength ( long value )
		{
			throw new NotSupportedException("Cannot set the length of the stream.");
		}

		/// <summary>This method is not supported.</summary>
		/// <param name="buffer">The data to write.</param>
		/// <param name="offset">The offset within the data to begin writing.</param>
		/// <param name="count">The number of bytes to write.</param>
		/// <exception cref="NotSupportedException">Always thrown.</exception>
		public override void Write ( byte[] buffer, int offset, int count )
		{
			throw new NotSupportedException("Stream does not support writing.");
		}

		#endregion //Public Members
				
		#region Private Members

		private readonly string m_value;

		private readonly byte[] m_buffer;
		private readonly bool m_useDefaultEncoding;

		private int m_pos;
		private int m_len;

		#endregion //Private Members							
	}
}
