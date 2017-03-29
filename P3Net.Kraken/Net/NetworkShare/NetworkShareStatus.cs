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

#if share_ready
namespace P3Net.Kraken.Net
{
	/// <summary>Defines the status of a network share.</summary>
	[CodeNotAnalyzed]
	public enum NetworkShareStatus
	{
		/// <summary>Unknown status.</summary>
		Unknown = 0,

		/// <summary>Share is ok.</summary>
		Ok,

		/// <summary>An error occurred with the share.</summary>
		Error,

		/// <summary>The share has been degraded.</summary>
		Degraded,

		/// <summary>The device associated with the share is predicting a failure.</summary>
		PredictingFailure,

		/// <summary>The device is starting.</summary>
		Starting,

		/// <summary>The device is stopping.</summary>
		Stopping,

		/// <summary>The device is in the middle of a non-operational service request.</summary>
		Service,

		/// <summary>The device is busy.</summary>
		Stressed,

		/// <summary>The device has encountered an unrecoverable error.</summary>
		Nonrecovery,

		/// <summary>No contact with the device can be made.</summary>
		NoContact,

		/// <summary>The share has lost communication with the device.</summary>
		LostCommunications,
	}

	internal static class NetworkShareStatusHelper
	{
		#region Public Members

		#region Methods

		/// <summary>Converts a string to its enumerated equivalent.</summary>
		/// <param name="value">The string to convert.</param>
		/// <returns>The network status.</returns>
		/// <remarks>
		/// These values are defined by WMI.
		/// </remarks>
		public static NetworkShareStatus Parse ( string value )
		{
			value = (value != null) ? value.Trim() : "";

			switch (value.ToUpper())
			{
				case "OK": return NetworkShareStatus.Ok;
				case "ERROR": return NetworkShareStatus.Error;
				case "DEGRADED": return NetworkShareStatus.Degraded;
				case "PRED FAIL": return NetworkShareStatus.PredictingFailure;
				case "STARTING": return NetworkShareStatus.Starting;
				case "STOPPING": return NetworkShareStatus.Stopping;
				case "SERVICE": return NetworkShareStatus.Service;
				case "STRESSED": return NetworkShareStatus.Stressed;
				case "NONRECOVER": return NetworkShareStatus.Nonrecovery;
				case "NOCONTACT": return NetworkShareStatus.NoContact;
				case "LOSTCOMM": return NetworkShareStatus.LostCommunications;
			};

			return NetworkShareStatus.Unknown;
		}
		#endregion

		#endregion //Public Members
	}
}
#endif