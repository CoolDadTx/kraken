/*
 * Copyright (c) 2007 by Michael Taylor
 * All rights reserved.
 */
#region Imports

using System;
using System.Configuration.Install;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken;
using P3Net.Kraken.ServiceProcess;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken
{
	public abstract class BaseServiceTest : UnitTest
	{
		#region Protected Members
                
        #region Init/Cleanup

        protected override void OnInitializeTest ()
		{
			base.OnInitializeTest();

			CleanServices();
		}

		protected override void OnUninitializeTest ()
		{
			base.OnUninitializeTest();

			CleanServices();
		}
		#endregion

		/// <summary>Asserts that a service exists.</summary>
		/// <param name="serviceName">The name of the service to check for.</param>
		/// <returns>The service instance, if found.</returns>
		protected ServiceController AssertServiceExists ( string serviceName )
		{
			//Error if it doesn't
			ServiceController svc = GetService(serviceName);
            if (svc != null)
                return svc;

			Assert.Fail("Service does not exist.");
			return null;
		}

		/// <summary>Asserts that a service does not exist.</summary>
		/// <param name="serviceName">The name of the service to check for.</param>
		protected void AssertServiceNotExists ( string serviceName )
		{
			//Inconclusive it does
			using (ServiceController svc = GetService(serviceName))
			{
				if (svc != null)
					Assert.Inconclusive("Service already exists.");
			};
		}

		/// <summary>Uninstalls any services that were installed during this interval.</summary>
		protected virtual void CleanServices ()
		{
			//Force a clean up of the service, if needed
			foreach (string service in m_ServiceCleanup.Keys)
			{
				using (ServiceController svc = GetService(service))
				{
					if (svc != null)
					{
						//Use interop code as it is quicker
						using (SafeHandle handle = svc.ServiceHandle)
						{
							DeleteService(handle);
						};
					};
				};
			};

			m_ServiceCleanup.Clear();
		}

		/// <summary>Gets a specific service.</summary>
		/// <param name="serviceName">The name of the service to get.</param>
		/// <returns>The service instance, if found.</returns>
		protected ServiceController GetService ( string serviceName )
		{
			foreach (ServiceController svc in ServiceController.GetServices())
			{
				if (String.Compare(svc.ServiceName, serviceName, true) == 0)
					return svc;
			};

			return null;
		}

		/// <summary>Prepares an installer for a service.</summary>
		/// <param name="serviceType">The type of the service.</param>
		/// <returns>The installer context to use.</returns>
		protected virtual TestInstallContext<ServiceInstaller> PrepareInstaller ( Type serviceType )
		{
			TestServiceBase svc = (TestServiceBase)Activator.CreateInstance(serviceType);

			//Create a new service installer
			ServiceInstaller serviceInstaller = new ServiceInstaller();

			serviceInstaller.DisplayName = svc.DisplayName;
			serviceInstaller.ServiceName = svc.ServiceName;
			serviceInstaller.StartType = ServiceStartMode.Disabled;

			//The parent process
			ServiceProcessInstaller processInstaller = new ServiceProcessInstaller();
			processInstaller.Account = ServiceAccount.LocalService;

			//The real installer
			Installer projectInstaller = new Installer();
			projectInstaller.Installers.AddRange(new Installer[] { processInstaller, serviceInstaller });

			string strAssembly = System.Reflection.Assembly.GetExecutingAssembly().Location;

			//Add the service to the list of things to potentially be uninstalled
			RegisterServiceForUninstall(svc.ServiceName);

			return new TestInstallContext<ServiceInstaller>(projectInstaller, serviceInstaller, strAssembly);
		}

		/// <summary>Prepares an installer for a service.</summary>
		/// <param name="serviceType">The type of the service.</param>
		/// <returns>The installer context to use.</returns>
		protected virtual TestInstallContext<ServiceInstaller2> PrepareInstallerEx ( Type serviceType )
		{
			TestServiceBase svc = (TestServiceBase)Activator.CreateInstance(serviceType);

			//Create a new service installer
			ServiceInstaller2 serviceInstaller = new ServiceInstaller2();

			serviceInstaller.DisplayName = svc.DisplayName;
			serviceInstaller.ServiceName = svc.ServiceName;
			serviceInstaller.StartType = ServiceStartMode.Disabled;

			//The parent process
			ServiceProcessInstaller processInstaller = new ServiceProcessInstaller();
			processInstaller.Account = ServiceAccount.LocalService;

			//The real installer
			Installer projectInstaller = new Installer();
			projectInstaller.Installers.AddRange(new Installer[] { processInstaller, serviceInstaller });

			string strAssembly = System.Reflection.Assembly.GetExecutingAssembly().Location;

			//Add the service to the list of things to potentially be uninstalled
			RegisterServiceForUninstall(svc.ServiceName);

			return new TestInstallContext<ServiceInstaller2>(projectInstaller, serviceInstaller, strAssembly);
		}

		/// <summary>Registers a service to be uninstalled when the test completes.</summary>
		/// <param name="serviceName">The name of the service to uninstall.</param>
		protected void RegisterServiceForUninstall ( string serviceName )
		{
			m_ServiceCleanup[serviceName] = serviceName;
		}

		/// <summary>Unregisters a service from uninstall when the test completes.</summary>
		/// <param name="serviceName">The name of the service to remove.</param>
		protected void UnregisterServiceForUninstall ( string serviceName )
		{
			m_ServiceCleanup.Remove(serviceName);
		}
		#endregion

		#region Private Members

		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool DeleteService ( SafeHandle hService );

		private SortedList<string, string> m_ServiceCleanup = new SortedList<string, string>();
		#endregion 
	}
}
