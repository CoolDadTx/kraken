/*
 * $description$
 * 
 * Copyright © $year$ Michael L Taylor
 * All Rights Reserved
 * 
 * $Header: $
 */
#region Imports

using System;
using System.Diagnostics;
using System.Runtime.Serialization;
#endregion

#if share_ready
namespace P3Net.Kraken.Net
{
	#region ShareAlreadyExistsException

	/// <summary>Exception that occurs when a share already exists.</summary>
	[Serializable]
	public sealed class ShareAlreadyExistsException : ShareException
	{
		#region Construction

		/// <summary>Initializes an instance of the <see cref="ShareAlreadyExistsException"/> class.</summary>
		/// <param name="shareName">The name of the share.</param>
		public ShareAlreadyExistsException ( string shareName ) : this(shareName, "The share already exists.", null)
		{ /* Do nothing */ }

		/// <summary>Initializes an instance of the <see cref="ShareAlreadyExistsException"/> class.</summary>
		/// <param name="shareName">The name of the share.</param>
		/// <param name="message">The message to display.</param>
		public ShareAlreadyExistsException ( string shareName, string message ) : this(shareName, message, null)
		{ /* Do nothing */ }

		/// <summary>Initializes an instance of the <see cref="ShareAlreadyExistsException"/> class.</summary>
		/// <param name="shareName">The name of the share.</param>
		/// <param name="innerException">The inner exception.</param>
		public ShareAlreadyExistsException ( string shareName, Exception innerException ) 
				: this(shareName, "The share already exists.", null)
		{ /* Do nothing */ }

		/// <summary>Initializes an instance of the <see cref="ShareAlreadyExistsException"/> class.</summary>
		/// <param name="shareName">The name of the share.</param>
		/// <param name="message">The message to display.</param>
		/// <param name="innerException">The inner exception.</param>
		public ShareAlreadyExistsException ( string shareName, string message, Exception innerException ) 
				: base(message, 22, innerException)
		{
			m_strName = (shareName != null) ? shareName.Trim() : "";
		}

		/// <summary>Initializes an instance of the <see cref="ShareException"/> class.</summary>
		/// <param name="info">The serialization information.</param>
		/// <param name="context">The streaming context.</param>
		public ShareAlreadyExistsException ( SerializationInfo info, StreamingContext context ) : base(info, context)
		{
			m_strName = info.GetString("Name");
		}
		#endregion //Construction

		#region Public Members

		#region Attributes

		/// <summary>Gets the name of the share.</summary>
		public string Name
		{
			[DebuggerStepThrough]
			get { return m_strName ?? ""; }
		}
		#endregion

		#region Methods

		/// <summary>Gets the serialized data for the exception.</summary>
		/// <param name="info">The serialization information.</param>
		/// <param name="context">The streaming context.</param>
		public override void GetObjectData ( SerializationInfo info, StreamingContext context )
		{
			base.GetObjectData(info, context);

			info.AddValue("Name", m_strName);
		}		
		#endregion

		#endregion //Public Members

		#region Private Members

		#region Data

		private string m_strName;
		#endregion

		#endregion //Private Members
	}
	#endregion 

	#region ShareException

	/// <summary>General class for exceptions related to network shares.</summary>
	[Serializable]
	public class ShareException : Exception
	{
		#region Construction

		/// <summary>Initializes an instance of the <see cref="ShareException"/> class.</summary>
		protected ShareException ()
		{ /* Do nothing */ }

		/// <summary>Initializes an instance of the <see cref="ShareException"/> class.</summary>
		/// <param name="errorCode">The error code for the exception.</param>
		protected ShareException ( int errorCode )
		{
			m_ErrorCode = errorCode;
		}

		/// <summary>Initializes an instance of the <see cref="ShareException"/> class.</summary>
		/// <param name="message">The message to display.</param>
		public ShareException ( string message ) : base(message)
		{ /* Do nothing */ }

		/// <summary>Initializes an instance of the <see cref="ShareException"/> class.</summary>
		/// <param name="message">The message to display.</param>
		/// <param name="errorCode">The error code for the exception.</param>
		public ShareException ( string message, int errorCode ) : base(message)
		{
			m_ErrorCode = errorCode;
		}

		/// <summary>Initializes an instance of the <see cref="ShareException"/> class.</summary>
		/// <param name="message">The message to display.</param>
		/// <param name="innerException">The inner exception.</param>
		public ShareException ( string message, Exception innerException ) : base(message, innerException)
		{ /* Do nothing */ }

		/// <summary>Initializes an instance of the <see cref="ShareException"/> class.</summary>
		/// <param name="message">The message to display.</param>
		/// <param name="errorCode">The error code for the exception.</param>
		/// <param name="innerException">The inner exception.</param>
		public ShareException ( string message, int errorCode, Exception innerException ) : base(message, innerException)
		{
			m_ErrorCode = errorCode;
		}

		/// <summary>Initializes an instance of the <see cref="ShareException"/> class.</summary>
		/// <param name="info">The serialization information.</param>
		/// <param name="context">The streaming context.</param>
		protected ShareException ( SerializationInfo info, StreamingContext context ) : base(info, context)
		{
			m_ErrorCode = info.GetInt32("ErrorCode");
		}
		#endregion //Construction

		#region Public Members

		#region Attributes

		/// <summary>Gets the error code for the exception, if any.</summary>
		public int ErrorCode
		{
			[DebuggerStepThrough]
			get { return m_ErrorCode; }
		}
		#endregion

		#region Methods

		/// <summary>Gets the serialized data for the exception.</summary>
		/// <param name="info">The serialization information.</param>
		/// <param name="context">The streaming context.</param>
		public override void GetObjectData ( SerializationInfo info, StreamingContext context )
		{
			base.GetObjectData(info, context);

			info.AddValue("ErrorCode", m_ErrorCode);
		}
		#endregion

		#endregion //Public Members

		#region Private Members

		#region Data

		private int m_ErrorCode;
		#endregion

		#endregion //Private Members
	}
	#endregion
}
#endif