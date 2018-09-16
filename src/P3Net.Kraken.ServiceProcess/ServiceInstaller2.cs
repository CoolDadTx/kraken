/*
 * Copyright © 2007 Michael L Taylor
 * All Rights Reserved
  */
using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;

namespace P3Net.Kraken.ServiceProcess
{
	/// <summary>Provides extended support for installing services.</summary>
	public class ServiceInstaller2 : ServiceInstaller
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="ServiceInstaller2"/> class.</summary>
        public ServiceInstaller2 ()
        {
            FailureActions = new ServiceFailureActions();
        }
        #endregion

        #region Public Members

        /// <summary>Gets or sets the arguments to pass to the service.</summary>
		/// <remarks>
		/// The arguments are appended to the service's binary during installation.
		/// </remarks>
		[Category("Data")]
		[DefaultValue("")]
		[Description("The arguments to pass to the service.")]
		public string Arguments
		{
			get { return m_arguments ?? ""; }
			set { m_arguments = value; }
		}

		/// <summary>Gets or sets the error control mode to use for the service.</summary>
		/// <value>The default value is <see cref="ServiceErrorControlMode.Normal"/>.</value>
		[Category("Behavior")]
		[DefaultValue(typeof(ServiceErrorControlMode), "Normal")]
		[Description("The error control mode for the service.")]
		public ServiceErrorControlMode ErrorControl { get; set; }

		/// <summary>Gets the action(s) to take if the service fails.</summary>
		/// <remarks>
		/// Although there is no defined limit to the number of actions Windows
		/// only displays the first three.
		/// </remarks>
		[Category("Behavior")]
		[Description("The action(s) to take if the service fails.")]
		public ServiceFailureActions FailureActions { get; set; }

		/// <summary>Gets or sets the load order group of the service.</summary>
		/// <value>The default value is an empty string.</value>
		/// <remarks>
		/// Only the groups defined by the system are valid.				
		/// </remarks>
		[Category("Behavior")]
		[DefaultValue("")]
		[Description("The load order group of the service.")]
		public string LoadOrderGroup
		{
			get { return m_loadOrderGroup ?? ""; }
			set { m_loadOrderGroup = value; }
		}

		/// <summary>Gets or sets the tag within the load order group for the service.</summary>
		/// <value>The default value is 0.</value>
		/// <remarks>
		/// This property is only meaningful for services that start at boot or system time.
		/// </remarks>
		[Category("Behavior")]
		[CLSCompliant(false)]
		[DefaultValue(0)]
		[Description("The tag order within the load order group.")]
		public uint Tag { get; set; }

		#region Methods

		/// <summary>Called to install the service component.</summary>
		/// <param name="stateSaver">Dictionary to use for saving state.</param>
		public override void Install ( System.Collections.IDictionary stateSaver )
		{
			//Install the service
			base.Install(stateSaver);

			base.Context.LogMessage("Updating service '" + ServiceName + "' configuration");

			//Unfortunately we don't have the service handle available to us so
			//we'll have to fetch it manually
            using (var svc = new ServiceController(ServiceName))
            {
                if (svc == null)
                    throw new InstallException("Service '" + ServiceName + "' not found.");

                //Make the basic setting changes
                UpdateBasicSettings(svc);

                //Make the advanced setting changes
                UpdateAdvancedSettings(svc);
            };
		}		
		#endregion

		#endregion

		#region Private Members

		#region Methods

		private void UpdateBasicSettings ( ServiceController service )
		{
			//Build up the parameters
			string path = null;
			if (Arguments.Length > 0)
			{
				//The easiest solution is to attach it to the path but SI wasn't designed for
				//that so we have to change it after the fact
				path = "\"" + Context.Parameters["assemblypath"] + "\" " + Arguments;				
			};

			uint error = (ErrorControl == ServiceErrorControlMode.Normal) ? NativeMethods.SERVICE_NO_CHANGE : (uint)ErrorControl;

			var group = (LoadOrderGroup.Length > 0) ? LoadOrderGroup : null;
			var tag = IntPtr.Zero;

			try
			{
				NativeMethods.DWORD_STRUCT dwTag;
				if ((Tag > 0) && (group != null))
				{
					dwTag = new NativeMethods.DWORD_STRUCT(Tag);

					//Have to allocate space for it
					tag = Marshal.AllocHGlobal(Marshal.SizeOf(dwTag));
					Marshal.StructureToPtr(dwTag, tag, false);
				};

				//We don't want to leave the handle laying around so trap it
				using (SafeHandle handle = service.ServiceHandle)
				{
					//Update
					if (!NativeMethods.ChangeServiceConfig(handle,
								NativeMethods.SERVICE_NO_CHANGE, NativeMethods.SERVICE_NO_CHANGE, error,
								path, group, tag, null, null, null, null))
						throw new Win32Exception(Marshal.GetLastWin32Error());
				};
			} finally
			{
				if (tag != IntPtr.Zero)
					Marshal.FreeHGlobal(tag);
			};
		}

		private void UpdateAdvancedSettings ( ServiceController service )
		{
			//Set up the failure actions
			if (FailureActions.HasNondefaultValues())
			{
				NativeMethods.SERVICE_FAILURE_ACTIONS sfa = new NativeMethods.SERVICE_FAILURE_ACTIONS();
				try
				{
					//Build the SFA structure
					sfa = new NativeMethods.SERVICE_FAILURE_ACTIONS(FailureActions);

					//We don't want to leave the handle laying around so trap it
					using (SafeHandle handle = service.ServiceHandle)
					{
						//Update the configuration
						if (!NativeMethods.ChangeServiceConfig2(handle, NativeMethods.SERVICE_CONFIG_INFOLEVEL.FAILURE_ACTIONS, ref sfa))
							throw new Win32Exception(Marshal.GetLastWin32Error());
					};
				} finally
				{
					//Clean up
					sfa.Clear();
				};
			};

			Context.LogMessage("Updated service '" + ServiceName + "' configuration");
		}
		#endregion
        		
		private string m_arguments;
		private string m_loadOrderGroup;
		
		#endregion 
	}
}
