/*
 * Copyright © 2007 Michael L Taylor
 * All Rights Reserved
 * 
 * $Header: $
 */
#region Imports

using System;
using System.Diagnostics;
using System.Security.Principal;

using P3Net.Kraken;
#endregion

#if share_ready
namespace P3Net.Kraken.Net
{
	/// <summary>Represents a network share.</summary>
	/// <remarks>
	/// Share-level security is not supported by any version of Windows from 2000 on and
	/// therefore is not supported by this type.
	/// </remarks>
	/// <preliminary/>
	[CodeNotAnalyzed]
	[CodeNotDocumented]
	[CodeNotTested]
	public sealed class NetworkShareInfo
	{
		#region Construction

		/// <summary>Initializes an instance of the <see cref="NetworkShareInfo"/> class.</summary>
		internal NetworkShareInfo ()
		{ }

		/// <summary>Initializes an instance of the <see cref="NetworkShareInfo"/> class.</summary>
		/// <param name="machineName">The machine associated with the share.</param>
		internal NetworkShareInfo ( string machineName )
		{
			m_strMachine = (machineName != null) ? machineName.Trim() : "";
		}
		#endregion //Construction

		#region Public Members

		#region Attributes

		/// <summary>Gets or sets the description of the share.</summary>
		public string Description
		{
			[DebuggerStepThrough]
			get { return m_strDescription ?? ""; }

			[DebuggerStepThrough]
			set { m_strDescription = (value != null) ? value.Trim() : ""; }
		}
	
		/// <summary>Gets or sets the maximum number of users who can use the share at once.</summary>
		/// <value>The default is 0xFFFFFFFF representing no limit.</value>
		/// <exception cref="ArgumentOutOfRangeException">When setting the property and the value is less than -1 or greater than Int32.MaxValue.</exception>
		public long MaximumUsers
		{
			[DebuggerStepThrough]
			get { return m_UsersMax; }

			[DebuggerStepThrough]
			set 
			{
				ValidationHelper.ThrowIfArgumentOutOfRange<long>(m_UsersMax, "MaximumUsers", -1, Int32.MaxValue);

				m_UsersMax = value; 
			}
		}

		/// <summary>Gets the name of the share.</summary>
		public string Name
		{
			[DebuggerStepThrough]
			get { return m_strName ?? ""; }

			[DebuggerStepThrough]
			internal set
			{
				m_strName = ValidationHelper.ThrowIfArgumentStringEmpty(value, "Name");
			}
		}

		/// <summary>Gets the path on the server for the share.</summary>
		public string Path
		{
			[DebuggerStepThrough]
			get { return m_strPath ?? ""; }

			[DebuggerStepThrough]
			internal set
			{
				m_strPath = ValidationHelper.ThrowIfArgumentStringEmpty(value, "Path");
			}
		}

		/// <summary>Gets the type of the share.</summary>
		public NetworkShareTypes ShareType
		{
			[DebuggerStepThrough]
			get { return m_Type; }

			[DebuggerStepThrough]
			internal set { m_Type = value; }
		}
		#endregion

		#region Methods

		/// <summary>Gets the current status of the share.</summary>
		/// <returns>The status of the share.</returns>
		public NetworkShareStatus GetStatus ()
		{
			return NetworkShare.GetShareStatus(m_strMachine, m_strName);
		}

		/// <summary>Refreshes the share properties.</summary>
		public void Refresh ()
		{
			//Retrieve new values
			NetworkShareInfo info = NetworkShare.GetShare(Name);

			//Copy them
			m_strDescription = info.Description;
			m_strPath = info.Path;
			m_Type = info.ShareType;
			m_UsersMax = info.MaximumUsers;
		}
		#endregion

		#endregion //Public Members

		#region Private Members

		#region Data

		private string m_strMachine;
		private string m_strName;

		private NetworkShareTypes m_Type = NetworkShareTypes.DiskDrive;
		private string m_strDescription;
        private long m_UsersMax = NetworkShare.UnlimitedUsers;
		
		private string m_strPath;
		#endregion

		#endregion //Private Members
	}
}
#endif