/*
 * Copyright © 2007 Michael L Taylor
 * All Rights Reserved
 * 
 * $Header: $
 */
#region Imports

using System;
using System.Diagnostics;
using System.ServiceProcess;

using P3Net.Kraken;
#endregion

namespace Tests.P3Net.Kraken
{
	/// <summary>Base class for testing services.</summary>
	public abstract class TestServiceBase : ServiceBase
	{
		#region Construction

		/// <summary>Initializes an instance of the <see cref="TestServiceBase"/> class.</summary>
		/// <param name="serviceName">The service name.</param>
		/// <param name="displayName">The display name of the service.</param>
		protected TestServiceBase ( string serviceName, string displayName )
		{
			ServiceName = serviceName;

			m_strDisplay = displayName;
		}

		/// <summary>Initializes an instance of the <see cref="TestServiceBase"/> class.</summary>
		/// <param name="serviceName">The service name.</param>
		protected TestServiceBase ( string serviceName )
		{
			ServiceName = serviceName;
		}
		#endregion //Construction

		#region Public Members

		#region Attributes

		/// <summary>Gets or sets the display name of the service.</summary>
		public string DisplayName
		{
			[DebuggerStepThrough]
			get { return m_strDisplay ?? ""; }

			[DebuggerStepThrough]
			set { m_strDisplay = (value != null) ? value.Trim() : ""; }
		}
		#endregion

		#endregion //Public Members

		#region Private Members

		#region Data

		private string m_strDisplay;
		#endregion

		#endregion //Private Members
	}
}
