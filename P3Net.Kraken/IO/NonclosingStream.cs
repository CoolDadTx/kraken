/*
 * Copyright © 2008 Michael Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.IO;

using P3Net.Kraken.Diagnostics;
#endregion

namespace P3Net.Kraken.IO
{
    /// <summary>Provides a wrapper around a stream that ignores requests to close it.</summary>
    /// <remarks>
    /// This type is designed for use in cases where a stream must be used by another type (such as <see cref="BinaryReader"/> or <see cref="BinaryWriter"/>
    /// and the type will automatically close the stream even if it is still needed.  
    /// </remarks>    
    /// <example>
    /// <code lang="C#">
    /// void SaveEmployee ( Stream stream, Employee employee )
    /// {
    ///    SaveEmployeeCore(new NonclosingStream(stream), employee);
    /// }
    /// 
    /// void SaveEmployeeCore ( Stream stream, Employee employee )
    /// {
    ///    using (var writer = new BinaryWriter(stream))
    ///    [
    ///       writer.Write(employee.Id);
    ///       ...
    ///    };
    /// }
    /// </code>
    /// </example>
    public sealed class NonclosingStream : Stream
    {
        #region Construction
        
        /// <summary>Initializes an instance of the <see cref="NonclosingStream"/>  class.</summary>
        /// <param name="stream">The stream to wrap.</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <see langword="null"/>.</exception>
        public NonclosingStream ( Stream stream )
        {
            Verify.Argument("stream", stream).IsNotNull();

            InnerStream = stream;
        }
        #endregion

        #region Public Members

        #region Attributes
        
        /// <summary>Determines if the stream can be read.</summary>
        public override bool CanRead
        {
            get { return InnerStream.CanRead; }
        }

        /// <summary>Determines if the stream can be seeked.</summary>
        public override bool CanSeek
        {
            get { return InnerStream.CanSeek; }
        }

        /// <summary>Determines if the stream can be written.</summary>
        public override bool CanWrite
        {
            get { return InnerStream.CanWrite; }
        }

        /// <summary>Determines the length of the stream.</summary>
        /// <exception cref="NotSupportedException">The stream does not support seeking.</exception>
        public override long Length
        {
            get { return InnerStream.Length; }
        }

        /// <summary>Gets or sets the current position in the stream.</summary>
        /// <exception cref="NotSupportedException">The stream does not support seeking.</exception>
        public override long Position
        {
            get { return InnerStream.Position; }
            set { InnerStream.Position = value; }
        }
        #endregion

        #region Methods
        
        /// <summary>Flushes any writes to the stream.</summary>
        public override void Flush ()
        {
            InnerStream.Flush();
        }
      
        /// <summary>Reads a block of data from the stream.</summary>
        /// <param name="buffer">The buffer to store the data.</param>
        /// <param name="offset">The offset within the buffer to start with.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>The number of bytes read.</returns>
        /// <exception cref="ArgumentException"><paramref name="offset"/> and <paramref name="count"/> are larger than the buffer length.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="buffer"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/> or <paramref name="count"/> is negative.</exception>
        /// <exception cref="NotSupportedException">The stream does not support reading.</exception>
        public override int Read ( byte[] buffer, int offset, int count )
        {
            return InnerStream.Read(buffer, offset, count);
        }

        /// <summary>Seeks within the stream.</summary>
        /// <param name="offset">The offset from the origin.</param>
        /// <param name="origin">The origin to start with.</param>
        /// <returns>The new position.</returns>
        /// <exception cref="NotSupportedException">The stream does not support seeking.</exception>
        public override long Seek ( long offset, SeekOrigin origin )
        {
            return InnerStream.Seek(offset, origin);
        }

        /// <summary>Sets the length of the stream.</summary>
        /// <param name="value">The new length.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
        /// <exception cref="NotSupportedException">The stream does not support seeking and writing.</exception>
        public override void SetLength ( long value )
        {
            InnerStream.SetLength(value);
        }

        /// <summary>Writes a block of data to the stream.</summary>
        /// <param name="buffer">The buffer to write.</param>
        /// <param name="offset">The offset within the buffer to start with.</param>
        /// <param name="count">The number of bytes to write.</param>
        /// <exception cref="ArgumentException"><paramref name="offset"/> and <paramref name="count"/> are larger than the buffer length.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="buffer"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/> or <paramref name="count"/> is negative.</exception>
        /// <exception cref="NotSupportedException">The stream does not support writing.</exception>        
        public override void Write ( byte[] buffer, int offset, int count )
        {
            InnerStream.Write(buffer, offset, count);
        }
        #endregion

        #endregion

        #region Private Members

        private Stream InnerStream { get; set; }
        #endregion
    }
}
