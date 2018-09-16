/*
 * Copyright © 2012 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;

namespace P3Net.Kraken.ServiceProcess
{
    /// <summary>Represents the configuration information about a service.</summary>
    [ExcludeFromCodeCoverage]
    public class ServiceConfigurationInfo
    {
        #region Construction

        internal ServiceConfigurationInfo ( string serviceName, string machineName )
        {
            m_serviceName = serviceName;
            m_machineName = String.IsNullOrEmpty(machineName) ? "." : machineName;
        }
        #endregion

        #region Public Members

        /// <summary>Gets any arguments specified for the service.</summary>
        public string Arguments
        {
            get { return BasicConfiguration.Arguments ?? ""; }
        }

        /// <summary>Gets the path and filename of the service.</summary>
        public string BinaryPath
        {
            get { return BasicConfiguration.BinaryPath ?? ""; }
        }

        /// <summary>Gets the services that depend on this service.</summary>
        public IEnumerable<ServiceConfigurationInfo> DependentServices
        {
            get { return StandardConfiguration.DependentServices ?? Enumerable.Empty<ServiceConfigurationInfo>(); }
        }

        /// <summary>Gets whether a service is delay started when set to automatic startup.</summary>
        public bool DelayStart
        {
            get { return ExtendedConfiguration.DelayStart; }
        }

        /// <summary>Gets the description of the service.</summary>
        public string Description
        {
            get { return ExtendedConfiguration.Description ?? ""; }
        }

        /// <summary>Gets the display name of the service.</summary>
        public string DisplayName
        {
            get { return StandardConfiguration.DisplayName ?? ""; }
        }

        /// <summary>Gets the error control mode for the service.</summary>
        public ServiceErrorControlMode ErrorControl
        {
            get { return BasicConfiguration.ErrorControl; }
        }

        /// <summary>Gets the failure actions for the service.</summary>
        public ServiceFailureActions FailureActions
        {
            get { return ExtendedConfiguration.FailureActions ?? new ServiceFailureActions(); }
        }

        /// <summary>Gets the load order group of the service.</summary>
        public string LoadOrderGroup
        {
            get { return BasicConfiguration.LoadOrderGroup ?? ""; }
        }

        /// <summary>Gets the name of the machine hosting the service.</summary>
        public string MachineName
        {
            get { return m_machineName ?? "."; }
        }

        /// <summary>Gets the name of the service.</summary>
        public string ServiceName
        {
            get { return m_serviceName ?? ""; }
        }

        /// <summary>Gets the services that this service depends on.</summary>
        public IEnumerable<ServiceConfigurationInfo> ServicesDependentOn
        {
            get { return StandardConfiguration.ServicesDependingOn ?? Enumerable.Empty<ServiceConfigurationInfo>(); }
        }

        /// <summary>Gets the type of the service.</summary>
        public ServiceType ServiceType
        {
            get { return StandardConfiguration.Type; }
        }

        /// <summary>Gets the startup mode for the service.</summary>
        public ServiceStartMode StartType
        {
            get { return BasicConfiguration.StartMode; }
        }

        /// <summary>Gets the unique tag for the service in the load order group.</summary>
        [CLSCompliant(false)]
        public uint TagId
        {
            get { return BasicConfiguration.Tag; }            
        }

        #region Methods

        /// <summary>Refreshes the configuration information.</summary>
        public void Refresh ()
        {
            m_configStandard = null;
            m_configBasic = null;
            m_configExtended = null;
        }

        /// <summary>Gets the string representation.</summary>
        /// <returns>The object as a string.</returns>
        public override string ToString ()
        {
            return ServiceName;
        }        
        #endregion

        #endregion

        #region Private Members

        private BasicServiceConfiguation BasicConfiguration
        {
            get
            {
                if (m_configBasic == null)
                    m_configBasic = new BasicServiceConfiguation(ServiceName, MachineName);

                return m_configBasic;
            }
        }

        private ExtendedServiceConfiguration ExtendedConfiguration
        {
            get
            {
                if (m_configExtended == null)
                    m_configExtended = new ExtendedServiceConfiguration(ServiceName, MachineName);

                return m_configExtended;
            }
        }

        private StandardServiceConfiguration StandardConfiguration
        {
            get
            {
                if (m_configStandard == null)
                    m_configStandard = new StandardServiceConfiguration(ServiceName, MachineName);

                return m_configStandard;
            }
        }

        #region Types

        private sealed class BasicServiceConfiguation
        {
            public BasicServiceConfiguation ( string serviceName, string machineName )
            {
                using (var ctrl = new ServiceController(serviceName, machineName))
                {
                    LoadData(ctrl.ServiceHandle);
                };
            }

            public string Arguments { get; set; }
            public string BinaryPath { get; set; }
            public string LoadOrderGroup { get; set; }

            public ServiceErrorControlMode ErrorControl { get; set; }
            public ServiceStartMode StartMode { get; set; }

            public uint Tag { get; set; }

            private void LoadData ( SafeHandle handle )
            {
                //We'll allocate a large buffer to handle the memory requirements
                uint dwBytesNeeded;
                uint sizeBuffer = 4096;
                IntPtr pBuffer = IntPtr.Zero;
                try
                {
                    while (true)
                    {
                        //Try and query the config info
                        pBuffer = Marshal.AllocHGlobal((int)sizeBuffer);
                        if (NativeMethods.QueryServiceConfig(handle, pBuffer, sizeBuffer, out dwBytesNeeded))
                            break;

                        //For any error other than insufficient buffer, throw an exception
                        int error = Marshal.GetLastWin32Error();
                        if (error != NativeMethods.ERROR_INSUFFICIENT_BUFFER)
                            throw new Win32Exception(error);

                        //Allocate a new buffer of the appropriate size and try again
                        Marshal.FreeHGlobal(pBuffer);
                        pBuffer = IntPtr.Zero;
                        sizeBuffer = dwBytesNeeded;
                    };

                    //We now have the config info so populate the fields				
                    NativeMethods.QUERY_SERVICE_CONFIG config = (NativeMethods.QUERY_SERVICE_CONFIG)
                            Marshal.PtrToStructure(pBuffer, typeof(NativeMethods.QUERY_SERVICE_CONFIG));

                    ErrorControl = (ServiceErrorControlMode)config.dwErrorControl;
                    StartMode = (ServiceStartMode)config.dwStartType;
                    Tag = config.dwTagId;

                    //Strings are a little harder as we have to copy them from the buffer back to our real string
                    string path = Marshal.PtrToStringUni(config.lpBinaryPathName);
                    LoadOrderGroup = Marshal.PtrToStringUni(config.lpLoadOrderGroup);

                    //Finally, we need to split the binary from the arguments we'll just look for the first space, barring quotes
                    if (path.StartsWith("\""))
                    {
                        //Find the next quote
                        int pos = path.IndexOf('\"', 1);
                        BinaryPath = path.Substring(1, pos - 1).Trim();

                        if (pos < path.Length)
                            Arguments = path.Substring(pos + 1).Trim();
                    } else
                    {
                        var tokens = path.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                        BinaryPath = tokens[0].Trim();
                        if (tokens.Length > 1)
                            Arguments = tokens[1].Trim();
                    };
                } finally
                {
                    if (pBuffer != IntPtr.Zero)
                        Marshal.FreeHGlobal(pBuffer);
                };
            }
        };

        private sealed class ExtendedServiceConfiguration
        {
            public ExtendedServiceConfiguration ( string serviceName, string machineName )
            {
                using (var ctrl = new ServiceController(serviceName, machineName))
                {
                    QueryDescription(ctrl.ServiceHandle);
                    QueryDelayStart(ctrl.ServiceHandle);
                    QueryFailureActions(ctrl.ServiceHandle);
                };
            }

            public ServiceFailureActions FailureActions { get; set; }
            public bool DelayStart { get; set; }
            public string Description { get; set; }

            private void QueryDelayStart ( SafeHandle handle )
            {
                uint sizeNeeded;
                uint sizeBuffer = 1024;
                IntPtr pBuffer = IntPtr.Zero;
                var info = new NativeMethods.SERVICE_DELAYED_AUTO_START_INFO();

                try
                {
                    while (true)
                    {
                        //Allocate buffer for the structure and the string
                        pBuffer = Marshal.AllocHGlobal(Marshal.SizeOf(info) + (int)sizeBuffer);

                        //Get the description 
                        if (NativeMethods.QueryServiceConfig2(handle, NativeMethods.SERVICE_CONFIG_INFOLEVEL.DELAYED_AUTO_START_INFO,
                            pBuffer, sizeBuffer, out sizeNeeded))
                            break;

                        //If we failed for any reason other than insufficient buffer space
                        int error = Marshal.GetLastWin32Error();
                        if (error != NativeMethods.ERROR_INSUFFICIENT_BUFFER)
                            throw new Win32Exception(error);

                        //Prepare to allocate a new buffer
                        Marshal.FreeHGlobal(pBuffer);
                        pBuffer = IntPtr.Zero;
                        sizeBuffer = sizeNeeded;
                    };

                    //Store the string 
                    info = (NativeMethods.SERVICE_DELAYED_AUTO_START_INFO)Marshal.PtrToStructure(pBuffer, typeof(NativeMethods.SERVICE_DELAYED_AUTO_START_INFO));
                    DelayStart = info.fDelayedAutoStart;
                } finally
                {
                    //Clean up
                    if (pBuffer != IntPtr.Zero)
                        Marshal.FreeHGlobal(pBuffer);
                };
            }

            private void QueryDescription ( SafeHandle handle )
            {
                uint sizeNeeded;
                uint sizeBuffer = 1024;
                IntPtr pBuffer = IntPtr.Zero;
                var descript = new NativeMethods.DESCRIPTION_STRUCT();

                try
                {
                    while (true)
                    {
                        //Allocate buffer for the structure and the string
                        pBuffer = Marshal.AllocHGlobal(Marshal.SizeOf(descript) + (int)sizeBuffer);

                        //Get the description 
                        if (NativeMethods.QueryServiceConfig2(handle, NativeMethods.SERVICE_CONFIG_INFOLEVEL.DESCRIPTION,
                            pBuffer, sizeBuffer, out sizeNeeded))
                            break;

                        //If we failed for any reason other than insufficient buffer space
                        int error = Marshal.GetLastWin32Error();
                        if (error != NativeMethods.ERROR_INSUFFICIENT_BUFFER)
                            throw new Win32Exception(error);

                        //Prepare to allocate a new buffer
                        Marshal.FreeHGlobal(pBuffer);
                        pBuffer = IntPtr.Zero;
                        sizeBuffer = sizeNeeded;
                    };

                    //Store the string 
                    descript = (NativeMethods.DESCRIPTION_STRUCT)Marshal.PtrToStructure(pBuffer, typeof(NativeMethods.DESCRIPTION_STRUCT));
                    Description = Marshal.PtrToStringUni(descript.strValue);
                } finally
                {
                    //Clean up
                    if (pBuffer != IntPtr.Zero)
                        Marshal.FreeHGlobal(pBuffer);
                };
            }

            private void QueryFailureActions ( SafeHandle handle )
            {
                uint sizeNeeded;
                uint sizeBuffer = 4096;
                IntPtr pBuffer = IntPtr.Zero;

                try
                {
                    while (true)
                    {
                        //Allocate buffer
                        pBuffer = Marshal.AllocHGlobal((int)sizeBuffer);

                        //Get the description first as it is the easiest
                        if (NativeMethods.QueryServiceConfig2(handle, NativeMethods.SERVICE_CONFIG_INFOLEVEL.FAILURE_ACTIONS,
                            pBuffer, sizeBuffer, out sizeNeeded))
                            break;

                        //If we failed for any reason other than insufficient buffer space
                        int error = Marshal.GetLastWin32Error();
                        if (error != NativeMethods.ERROR_INSUFFICIENT_BUFFER)
                            throw new Win32Exception(error);

                        //Prepare to allocate a new buffer
                        Marshal.FreeHGlobal(pBuffer);
                        sizeBuffer = sizeNeeded;
                    };

                    //Build the failure list
                    var failure = (NativeMethods.SERVICE_FAILURE_ACTIONS)Marshal.PtrToStructure(pBuffer, typeof(NativeMethods.SERVICE_FAILURE_ACTIONS));
                    FailureActions = new ServiceFailureActions(failure);
                } finally
                {
                    //Clean up
                    if (pBuffer != IntPtr.Zero)
                        Marshal.FreeHGlobal(pBuffer);
                };
            }
        }

        private sealed class StandardServiceConfiguration
        {
            public StandardServiceConfiguration ( string serviceName, string machineName )
            {
                using (var ctrl = new ServiceController(serviceName, machineName))
                {
                    DisplayName = ctrl.DisplayName;
                    Type = ctrl.ServiceType;

                    ServicesDependingOn = (from sd in ctrl.ServicesDependedOn
                                           select new ServiceConfigurationInfo(sd.ServiceName, machineName)).ToList();

                    DependentServices = (from sd in ctrl.DependentServices
                                         select new ServiceConfigurationInfo(sd.ServiceName, machineName)).ToList();
                };
            }

            public string DisplayName { get; set; }
            public ServiceType Type { get; set; }

            public List<ServiceConfigurationInfo> ServicesDependingOn { get; private set; }
            public List<ServiceConfigurationInfo> DependentServices { get; private set; }
        }
        #endregion

        #region Data

        private string m_serviceName;
        private string m_machineName;

        private StandardServiceConfiguration m_configStandard;
        private BasicServiceConfiguation m_configBasic;
        private ExtendedServiceConfiguration m_configExtended;
        #endregion

        #endregion
    }
}

