/*
 * Defines the flags that can be used with SendMessageTimeout
 *
 * Copyright © 2005 Michael L Taylor ($COMPANY$)
 * All Rights Reserved
 * 
 * $Header: /DotNET/Kraken/Source/P3Net.Kraken.UI/Messages/SendMessageTimeoutFlags.cs 1     10/16/05 12:15a Michael $
 */
#region Imports

using System;
#endregion

namespace P3Net.Kraken.WinForms.Native
{
	/// <summary>Defines the options that can be used with <see cref="M:SendMessageTimeout"/>.</summary>
	[Flags]
	public enum SendMessageTimeoutOptions
	{
		/// <summary>The calling thread does not block requests while waiting for the function to return.</summary>
		None = 0,

		/// <summary>The calling thread blocks further requests until the function returns.</summary>
		Block = 1,

		/// <summary>Aborts if the receiving window appears to be hung.</summary>
		AbortIfHung = 2,

		/// <summary>Does not time out if the receiving window is not responding.</summary>
		NoTimeout = 8,
	}
}
