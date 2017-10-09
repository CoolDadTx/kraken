/*
 * Copyright © 2007 Michael L Taylor
 * All Rights Reserved
 */
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.ServiceProcess;

using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.ServiceProcess
{
    /// <summary>Provides extension methods for service controllers.</summary>    
    public static class ServiceControllerExtensions
    {
        /// <summary>Gets the configuration information for a service.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The service configuration.</returns>
        /// <example>
        /// <code lang="C#">
        /// public static void main ( string[] args )
        /// {
        ///    foreach (string arg in args)
        ///    {
        ///       using (var svc = ServiceController.GetService(args))
        ///       {
        ///          if (svc != null)
        ///             PrintServiceDetails(svc.GetConfiguration());
        ///       };
        ///    };
        /// }
        /// </code>
        /// </example>
        public static ServiceConfigurationInfo GetConfiguration ( this ServiceController source )
        {
            return new ServiceConfigurationInfo(source.ServiceName, source.MachineName);
        }

        /// <summary>Gets the service with the given name.</summary>
        /// <param name="serviceName">The name of the service to retrieve.</param>
        /// <returns>The service controller if found or <see langword="null"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="serviceName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="serviceName"/> is empty or invalid.</exception>
        /// <example>
        /// <code lang="C#">
        /// public static void main ( string[] args )
        /// {
        ///    foreach (string arg in args)
        ///    {
        ///       using (var svc = ServiceControllerExtensions.GetService(args))
        ///       {
        ///          if (svc != null)
        ///             PrintServiceDetails(svc);
        ///       };
        ///    };
        /// }
        /// </code>
        /// </example>
        public static ServiceController GetService ( string serviceName )
        {
            return GetService(serviceName, null);
        }

        /// <summary>Gets the service with the given name.</summary>
        /// <param name="serviceName">The name of the service to retrieve.</param>
        /// <param name="machineName">The name of the machine to connect to.</param>
        /// <returns>The service controller if found or <see langword="null"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="serviceName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="serviceName"/> is empty or invalid.</exception>
        /// <example>
        /// See <see cref="M:GetService(String)"/> for an example.
        /// </example>
        public static ServiceController GetService ( string serviceName, string machineName )
        {
            Verify.Argument("serviceName", serviceName).IsNotNullOrEmpty();

            if (String.IsNullOrEmpty(machineName))
                machineName = ".";

            //To avoid an exception, enumerate the services
            foreach (var ctrl in ServiceController.GetServices(machineName))
            {
                if (String.Compare(ctrl.ServiceName, serviceName, StringComparison.OrdinalIgnoreCase) == 0)
                    return ctrl;
            };

            return null;
        }
    }
}
