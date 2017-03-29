/*
 * Copyright © 2011 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description; 

namespace P3Net.Kraken.ServiceModel
{
    /// <summary>Provides a wrapper around a WCF service client.</summary>
    /// <typeparam name="TChannel">The service interface.</typeparam>
    /// <remarks>
    /// The wrapper can be safely used with a <see langword="using"/> block.  WCF exceptions
    /// result in the client being aborted if necessary.
    /// </remarks>
    public class ServiceClientWrapper<TChannel> : ClientBase<TChannel>, IDisposable where TChannel : class
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="ServiceClientWrapper{T}"/> class.</summary>
        public ServiceClientWrapper ()
        {
        }

        /// <summary>Initializes an instance of the <see cref="ServiceClientWrapper{T}"/> class.</summary>
        /// <param name="callbackInstance">The instance callback.</param>
        public ServiceClientWrapper ( InstanceContext callbackInstance ) : base(callbackInstance)
        {
        }

        /// <summary>Initializes an instance of the <see cref="ServiceClientWrapper{T}"/> class.</summary>
        /// <param name="endpointConfigurationName">The endpoint name.</param>
        public ServiceClientWrapper ( string endpointConfigurationName ) : base(endpointConfigurationName)
        {
        }

        /// <summary>Initializes an instance of the <see cref="ServiceClientWrapper{T}"/> class.</summary>
        /// <param name="endpoint">The service endpoint.</param>
        public ServiceClientWrapper ( ServiceEndpoint endpoint ) : base(endpoint)
        {
        }

        /// <summary>Initializes an instance of the <see cref="ServiceClientWrapper{T}"/> class.</summary>
        /// <param name="callbackInstance">The instance callback.</param>
        /// <param name="endpoint">The service endpoint.</param>
        public ServiceClientWrapper ( InstanceContext callbackInstance, ServiceEndpoint endpoint )
                                            : base(callbackInstance, endpoint)
        {
        }

        /// <summary>Initializes an instance of the <see cref="ServiceClientWrapper{T}"/> class.</summary>
        /// <param name="callbackInstance">The instance callback.</param>
        /// <param name="endpointConfigurationName">The endpoint name.</param>
        public ServiceClientWrapper ( InstanceContext callbackInstance, string endpointConfigurationName )
                                            : base(callbackInstance, endpointConfigurationName)
        {
        }

        /// <summary>Initializes an instance of the <see cref="ServiceClientWrapper{T}"/> class.</summary>        
        /// <param name="endpointConfigurationName">The endpoint name.</param>
        /// <param name="remoteAddress">The endpoint address.</param>
        public ServiceClientWrapper ( string endpointConfigurationName, EndpointAddress remoteAddress )
                                            : base(endpointConfigurationName, remoteAddress)
        {
        }

        /// <summary>Initializes an instance of the <see cref="ServiceClientWrapper{T}"/> class.</summary>        
        /// <param name="endpointConfigurationName">The endpoint name.</param>
        /// <param name="remoteAddress">The endpoint address.</param>
        public ServiceClientWrapper ( string endpointConfigurationName, string remoteAddress )
                                            : base(endpointConfigurationName, remoteAddress)
        {
        }

        /// <summary>Initializes an instance of the <see cref="ServiceClientWrapper{T}"/> class.</summary>        
        /// <param name="binding">The binding to use.</param>
        /// <param name="remoteAddress">The endpoint address.</param>
        public ServiceClientWrapper ( Binding binding, EndpointAddress remoteAddress )
                                        : base(binding, remoteAddress)
        {
        }

        /// <summary>Initializes an instance of the <see cref="ServiceClientWrapper{T}"/> class.</summary>        
        /// <param name="callbackInstance">The instance callback.</param>
        /// <param name="binding">The binding to use.</param>
        /// <param name="remoteAddress">The endpoint address.</param>
        public ServiceClientWrapper ( InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress )
                                        : base(callbackInstance, binding, remoteAddress)
        {
        }

        /// <summary>Initializes an instance of the <see cref="ServiceClientWrapper{T}"/> class.</summary>        
        /// <param name="callbackInstance">The instance callback.</param>
        /// <param name="endpointConfigurationName">The endpoint name.</param>
        /// <param name="remoteAddress">The endpoint address.</param>
        public ServiceClientWrapper ( InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress )
                                        : base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
        }

        /// <summary>Initializes an instance of the <see cref="ServiceClientWrapper{T}"/> class.</summary>        
        /// <param name="callbackInstance">The instance callback.</param>
        /// <param name="endpointConfigurationName">The endpoint name.</param>
        /// <param name="remoteAddress">The endpoint address.</param>
        public ServiceClientWrapper ( InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress )
                                        : base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
        }
        #endregion
        
        #region Public Members

        #region Attributes
        
        /// <summary>Gets the wrapped client.</summary>
        public TChannel Client
        {
            get { return Channel; }
        }
        #endregion

        #region Methods
        
        /// <summary>Closes the client if it is open.</summary>
        /// <remarks>
        /// This method properly closes the client even in the face of aborted connections.
        /// </remarks>
        public new void Close ( )
        {
            ((IDisposable)this).Dispose();
        }
        #endregion

        #region Operators

        /// <summary>Implicit conversion to <typeparamref name="TChannel"/>.</summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>The underlying client.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates")]
        public static implicit operator TChannel ( ServiceClientWrapper<TChannel> value )
        {
            return value.Client;
        }
        #endregion

        #endregion

        #region Protected Members

        /// <summary>Disposes of the object.</summary>
        /// <param name="disposing"><see langword="true"/> if disposing.</param>
        /// <remarks>
        /// The default implementation closes the client if it is not already closed.  If the client is
        /// in an invalid state then the connection is aborted.
        /// </remarks>
        protected virtual void Dispose ( bool disposing )
        {
            try
            {
                if (State != CommunicationState.Closed)
                    base.Close();
            } catch (CommunicationException)
            {
                base.Abort();
            } catch (TimeoutException)
            {
                base.Abort();
            } catch (Exception)
            {
                base.Abort();
                throw;
            };        
        }
        #endregion

        #region Private Members
        
        void IDisposable.Dispose ()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion        
    }
}
