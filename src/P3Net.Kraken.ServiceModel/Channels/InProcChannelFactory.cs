/*
 * Copied from code - copyright iDesign, WCF Essentials
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace P3Net.Kraken.ServiceModel.Channels
{
    /// <summary>Provides an in-process channel factory for hosting services.</summary>    
    /// <remarks>
    /// The default instance (<see cref="Default"/>) provides a shared appdomain instance of the factory
    /// that can be used.  Alternatively a factory can be created and hosted for a limited time.
    /// </remarks>
    public sealed class InProcChannelFactory : IDisposable
    {
        #region Construction
                
        /// <summary>Creates an instance of the <see cref="InProcChannelFactory"/> class.</summary>
        public InProcChannelFactory ( ) : this("")
        { }

        /// <summary>Creates an instance of the <see cref="InProcChannelFactory"/> class.</summary>
        /// <param name="endpoint">The endpoint to use.</param>
        public InProcChannelFactory ( string endpoint )
        {
            //Append endpoint as needed
            var basePath = DefaultBaseAddress;
            if (!String.IsNullOrEmpty(endpoint))
                basePath += endpoint;
            if (!basePath.EndsWith("/"))
                basePath += "/";

            //Set endpoint and binding information
            BaseAddress = new Uri(basePath);
            Binding = new NetNamedPipeBinding() { TransactionFlow = true };                                    
        }
        #endregion

        #region Attributes

        /// <summary>Gets the base address of the factory.</summary>
        public Uri BaseAddress { get; private set; }

        /// <summary>Gets the binding for the factory.</summary>
        public Binding Binding { get; private set; }

        /// <summary>Gets the default instance of the factory.</summary>
        public static InProcChannelFactory Default
        {
            get { return s_defaultInstance.Value; }
        }

        /// <summary>Defines the default base address if no endpoint is specified.</summary>
        public static readonly string DefaultBaseAddress = "net.pipe://localhost/";
        #endregion

        /// <summary>Closes all the hosts in the factory.</summary>
        public void CloseAll ( )
        {
            Dispose(true);
        }

        /// <summary>Creates an instance of a service in the factory.</summary>
        /// <typeparam name="TService">The service implementation.</typeparam>
        /// <typeparam name="TInterface">The service interface.</typeparam>
        /// <returns>The interface instance.</returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public TInterface CreateInstance<TService, TInterface> ()
            where TInterface : class
            where TService : TInterface
        {
            var hostRecord = GetHostRecord<TService, TInterface>();
            return ChannelFactory<TInterface>.CreateChannel(Binding, new EndpointAddress(hostRecord.Address));
        }

        /// <summary>Closes all the hosts in the factory.</summary>
        public void Dispose ()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        #region Private Members

        #region Types

        private struct HostRecord
        {
            public HostRecord ( ServiceHost host, string address )
            {
                Host = host;
                Address = address;
            }

            public readonly ServiceHost Host;
            public readonly string Address;
        }
        #endregion

        [ExcludeFromCodeCoverage]
        private static InProcChannelFactory CreateDefaultInstance ( )
        {
            var factory = new InProcChannelFactory();

            //Hook into the domain unload so we clean up
            AppDomain.CurrentDomain.ProcessExit += (o,e) => factory.CloseAll();

            return factory;
        }

        [ExcludeFromCodeCoverage]
        private void Dispose ( bool disposing )
        {
            if (disposing && (m_hosts != null))
            {
                foreach (var pair in m_hosts)
                    pair.Value.Host.Close();
            };
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        [ExcludeFromCodeCoverage]
        private HostRecord GetHostRecord<TSERVICE, TINTERFACE> ()
            where TINTERFACE : class 
            where TSERVICE : TINTERFACE
        {
            HostRecord hostRecord;
            if (m_hosts.ContainsKey(typeof(TSERVICE)))
            {
                hostRecord = m_hosts[typeof(TSERVICE)];
            } else
            {
                var host = new ServiceHost(typeof(TSERVICE), BaseAddress);
                string address = BaseAddress.ToString() + Guid.NewGuid().ToString();
                hostRecord = new HostRecord(host, address);
                m_hosts.Add(typeof(TSERVICE), hostRecord);
                host.AddServiceEndpoint(typeof(TINTERFACE), Binding, address);
                host.Open();
            };

            return hostRecord;
        }

        #region Data

        private static readonly Lazy<InProcChannelFactory> s_defaultInstance = new Lazy<InProcChannelFactory>(CreateDefaultInstance, true);

        private readonly Dictionary<Type, HostRecord> m_hosts = new Dictionary<Type, HostRecord>();
        #endregion

        #endregion        
    }
}
