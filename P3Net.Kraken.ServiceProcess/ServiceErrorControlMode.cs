/*
 * Copyright © 2007 Michael L Taylor
 * All Rights Reserved
 * 
 * $Header: $
 */
#region Imports

using System;
using System.Diagnostics;

using P3Net.Kraken;
#endregion

namespace P3Net.Kraken.ServiceProcess
{
	/// <summary>Defines the error control settings for a service during system startup.</summary>
	public enum ServiceErrorControlMode
	{
		/// <summary>Errors are ignored during startup.</summary>
		Ignore = 0,

		/// <summary>An event log entry is generated and startup continues.</summary>
		Normal = 1,

		/// <summary>An event log entry is generated.  If the last-known-good configuration is being started
		/// then startup continues otherwise the system is restarted with the last-known-good configuration.
		/// </summary>
		Severe = 2,

		/// <summary>An event log entry is generated.  If the last-known-good configuration is being started
		/// then startup fails otherwise the system is restarted with the last-known-good configuration.
		/// </summary>
		Critical = 3,
	}
}
