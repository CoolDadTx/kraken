/*
 * Copyright (c) 2007 by Michael Taylor
 * All rights reserved.
 */
using System;
using System.IO;
using System.ServiceProcess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken;
using P3Net.Kraken.ServiceProcess;

namespace Tests.P3Net.Kraken
{
    [TestClass]
    [Ignore]
    public sealed class ServiceControllerExtensionsTest : BaseServiceTest
    {
        #region Tests

        [TestMethod]
        public void GetConfiguration_BasicInfo ()
        {
            //Act
            var actual = GetServiceConfiguration("W32Time");

            //Assert
            actual.Arguments.Should().ContainEquivalentOf("-k");
            actual.BinaryPath.Should().ContainEquivalentOf(Environment.SystemDirectory + @"\svchost.exe");
            actual.LoadOrderGroup.Should().BeEmpty();
            actual.StartType.Should().Be(ServiceStartMode.Automatic);
            actual.DelayStart.Should().BeTrue();
            actual.ErrorControl.Should().Be(ServiceErrorControlMode.Normal);
        }

        [TestMethod]
        public void GetConfiguration_Description ()
        {
            //Act
            var actual = GetServiceConfiguration("W32Time");

            //Assert
            actual.Description.Should().NotBeEmpty();
        }

        [TestMethod]
        public void GetConfiguration_FailureActions ()
        {
            //Act
            var actual = GetServiceConfiguration("W32Time");

            //Assert
            actual.FailureActions.HasActionsDefined.Should().BeTrue();
            actual.FailureActions.Actions[0].ActionType.Should().NotBe(ServiceActionMode.None);
        }

        [TestMethod]
        public void GetConfiguration_LoadOrder ()
        {
            //Act
            var actual = GetServiceConfiguration("Beep");

            //Assert
            actual.LoadOrderGroup.Should().NotBeEmpty();
            actual.TagId.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void GetConfiguration_StandardInfo ()
        {
            //Act
            var actual = GetServiceConfiguration("W32Time");

            //Assert            
            actual.DependentServices.Should().BeEmpty();
            actual.DisplayName.Should().BeEquivalentTo("Windows Time");
            actual.ServiceName.Should().BeEquivalentTo("W32Time");
            actual.ServicesDependentOn.Should().BeEmpty();
        }
        #endregion

        #region Private Members

        private ServiceConfigurationInfo GetServiceConfiguration ( string serviceName )
        {
            using (var svc = new ServiceController(serviceName))
            {
                if (svc == null)
                {
                    Assert.Inconclusive(serviceName + " service does not exist.");
                    return null;
                };

                return svc.GetConfiguration();
            };
        }
        #endregion
    }
}
