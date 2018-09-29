/*
 * Copyright (c) 2007 by Michael Taylor
 * All rights reserved.
 */
#region Imports

using System;
using System.Configuration.Install;
using System.ServiceProcess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken;
using P3Net.Kraken.ServiceProcess;
#endregion

namespace Tests.P3Net.Kraken
{
    //Ignore these tests as they require admin privileges
 //   [Ignore]
	//[TestClass]
	//public sealed class ServiceInstaller2Test : BaseServiceTest
	//{
	//	#region Tests

	//	#region Normal

	//	/// <summary>Tests that service can install without any additional options.</summary>
	//	[TestMethod]
	//	public void ServiceInstallSuccess ()
	//	{
	//		AssertAdministrator();
	//		AssertServiceNotExists("SimpleService");

	//		using (TestInstallContext<ServiceInstaller2> context = PrepareInstallerEx(typeof(SimpleService)))
	//		{
	//			context.Install();
                                
	//			using (var svc = AssertServiceExists("SimpleService"))
	//			{ };
	//		};
	//	}
	//	#endregion

	//	#region Standard Settings

	//	/// <summary>Tests that simple service arguments can be specified during installation.</summary>
	//	[TestMethod]
	//	public void ServiceSimpleArgumentsSuccess ( )
	//	{
	//		AssertAdministrator();
	//		AssertServiceNotExists("SimpleService");

	//		using (var context = PrepareInstallerEx(typeof(SimpleService)))
	//		{
	//			context.Installer.Arguments = "someArg";
	//			context.Installer.ErrorControl = ServiceErrorControlMode.Severe;
				
	//			context.Install();

	//			using (var svc = AssertServiceExists("SimpleService"))
	//			{
 //                   var info = svc.GetConfiguration();
 //                   info.Arguments.Should().Be(context.Installer.Arguments, "Service argument does not match.");
	//				info.ErrorControl.Should().Be(context.Installer.ErrorControl, "Error control does not match.");
	//			};
	//		};
	//	}

	//	/// <summary>Tests that service arguments can be specified during installation.</summary>
	//	[TestMethod]
	//	public void ServiceComplexArgumentsSuccess ()
	//	{
	//		AssertAdministrator();
	//		AssertServiceNotExists("SimpleService");

	//		using (var context = PrepareInstallerEx(typeof(SimpleService)))
	//		{
	//			context.Installer.Arguments = "someArg anotherArg andAnother";
	//			context.Install();

	//			using (var svc = AssertServiceExists("SimpleService"))
	//			{
 //                   var info = svc.GetConfiguration();
	//				info.Arguments.Should().Be(context.Installer.Arguments, "Service argument is not valid.");
	//			};
	//		};
	//	}

	//	/// <summary>Tests that service load order and tag can be specified during installation.</summary>
	//	[TestMethod]
	//	public void ServiceLoadOrderSuccess ()
	//	{
	//		AssertAdministrator();
	//		AssertServiceNotExists("SimpleService");

	//		using (var context = PrepareInstallerEx(typeof(SimpleService)))
	//		{
	//			context.Installer.LoadOrderGroup = "Event Log";
	//			context.Installer.Tag = 1;

	//			context.Install();

	//			using (var svc = AssertServiceExists("SimpleService"))
	//			{
 //                   var info = svc.GetConfiguration();

	//				info.LoadOrderGroup.Should().Be(context.Installer.LoadOrderGroup, "Load order group does not match.");
	//				info.TagId.Should().Be(context.Installer.Tag, "Tag does not match.");
	//			};
	//		};
	//	}
	//	#endregion

	//	#region Failure Actions

	//	/// <summary>Tests that service can install with a simple failure settings.</summary>
	//	[TestMethod]
	//	public void ServiceSimpleFailureSettingsSuccess ()
	//	{
	//		AssertAdministrator();
	//		AssertServiceNotExists("SimpleService");

	//		using (var context = PrepareInstallerEx(typeof(SimpleService)))
	//		{
	//			context.Installer.FailureActions.Command = "someFailureCommand";
	//			context.Installer.FailureActions.RebootMessage = "A reboot message";
	//			context.Installer.FailureActions.ResetPeriod = new TimeSpan(1, 0, 0, 0);
	//			context.Installer.FailureActions.Actions[0] = new ServiceAction(ServiceActionMode.Restart, new TimeSpan(0, 0, 1, 0));							

	//			context.Install();

	//			using (var svc = AssertServiceExists("SimpleService"))
	//			{
 //                   var info = svc.GetConfiguration();

	//				//Verify the settings
	//				info.FailureActions.Command.Should().Be(context.Installer.FailureActions.Command,  "Failure command doesn't match.");
	//				info.FailureActions.RebootMessage.Should().Be(context.Installer.FailureActions.RebootMessage, "Reboot message doesn't match.");
	//				info.FailureActions.ResetPeriod.Should().Be(context.Installer.FailureActions.ResetPeriod, "Reset period doesn't match.");

	//				VerifyActions(context.Installer.FailureActions.Actions, info.FailureActions.Actions);
	//			};
	//		};
	//	}

	//	/// <summary>Tests that service can install with two failure actions.</summary>
	//	[TestMethod]
	//	public void ServiceFailureActionTwoSuccess ()
	//	{
	//		AssertAdministrator();
	//		AssertServiceNotExists("SimpleService");

	//		using (var context = PrepareInstallerEx(typeof(SimpleService)))
	//		{
	//			//We change the order here just to make sure it works
	//			context.Installer.FailureActions.Actions[1] = new ServiceAction(ServiceActionMode.Restart, new TimeSpan(0, 0, 1, 0));
	//			context.Installer.FailureActions.Actions[0] = new ServiceAction(ServiceActionMode.RunCommand, new TimeSpan(0, 1, 0, 0));
	//			context.Install();

	//			using (var svc = AssertServiceExists("SimpleService"))
	//			{
 //                   var info = svc.GetConfiguration();

	//				//Verify the settings
	//				VerifyActions(context.Installer.FailureActions.Actions, info.FailureActions.Actions);
	//			};
	//		};
	//	}

	//	/// <summary>Tests that service will succeed with three failure actions.</summary>
	//	[TestMethod]
	//	public void ServiceFailureActionThreeSuccess ()
	//	{
	//		AssertAdministrator();
	//		AssertServiceNotExists("SimpleService");

	//		using (var context = PrepareInstallerEx(typeof(SimpleService)))
	//		{
	//			context.Installer.FailureActions.Actions[0] = new ServiceAction(ServiceActionMode.Restart, new TimeSpan(0, 0, 1, 0));
	//			context.Installer.FailureActions.Actions[1] = new ServiceAction(ServiceActionMode.RunCommand, new TimeSpan(0, 1, 0, 0));
	//			context.Installer.FailureActions.Actions[2] = new ServiceAction(ServiceActionMode.None, 5000);
	//			context.Install();

	//			using (var svc = AssertServiceExists("SimpleService"))
	//			{
 //                   var info = svc.GetConfiguration();

	//				//Verify the settings
	//				VerifyActions(context.Installer.FailureActions.Actions, info.FailureActions.Actions);
	//			};
	//		};
	//	}
	//	#endregion

	//	#endregion

	//	#region Protected Members

	//	protected override void OnInitializeTest ()
	//	{
	//		RegisterServiceForUninstall("SimpleService");

	//		base.OnInitializeTest();
	//	}
	//	#endregion

	//	#region Private Members

	//	//Simple service to test against
	//	class SimpleService : TestServiceBase
	//	{
	//		public SimpleService () : base("SimpleService", "SimpleService")
	//		{ }
	//	}

	//	private void VerifyActions ( ServiceAction[] expectedActions, ServiceAction[] actualActions )
	//	{			
	//		Assert.AreEqual(expectedActions.Length, actualActions.Length, "Action lengths do not match.");
			
	//		for (int nIdx = 0; nIdx < expectedActions.Length; ++nIdx)
	//		{
	//			Assert.AreEqual(expectedActions[nIdx].ActionType, actualActions[nIdx].ActionType, "Action types do not match for action " + nIdx.ToString());
	//			Assert.AreEqual(expectedActions[nIdx].Delay, actualActions[nIdx].Delay, "Delays do not match for action " + nIdx.ToString());
	//		};					
	//	}
	//	#endregion 
	//}
}
