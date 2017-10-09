/*
 * Copyright © 2007 Michael L Taylor
 * All Rights Reserved
 */
using System;

namespace P3Net.Kraken.ServiceProcess
{
	/// <summary>Defines the action to take when a service fails.</summary>
	public enum ServiceActionMode
	{
		/// <summary>Take no action.</summary>
		None = 0,

		/// <summary>Restart the service.</summary>
		Restart,

		/// <summary>Reboot the computer.</summary>
		Reboot,

		/// <summary>Run a command.</summary>
		RunCommand,
	}
}
