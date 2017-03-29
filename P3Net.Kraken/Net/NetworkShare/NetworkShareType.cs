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
	/// <summary>Defines the different types of network shares.</summary>
	[CodeNotAnalyzed]
	[Flags]
	public enum NetworkShareTypes
	{
		/// <summary>A share for a disk drive.</summary>
		DiskDrive = 0,

		/// <summary>A share for a printer queue.</summary>
		Printer = 1,

		/// <summary>A share to a device.</summary>
		Device = 2,

		/// <summary>A share for IPC.</summary>
		InterprocessCommunction = 3,	

		/// <summary>Identifies an administrative share.</summary>
		Administrative = -2147483647,		//0x80000000
	}
}
#endif