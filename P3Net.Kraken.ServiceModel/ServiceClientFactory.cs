/*
 * Copyright © 2011 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.ServiceModel
{
    /// <summary>Factory for creating WCF service clients.</summary>
    public static class ServiceClientFactory
    {
        #region CreateAndWrap

        /// <summary>Creates a service client instance and wraps it.</summary>
        /// <typeparam name="TChannel">The service interface.</typeparam>
        /// <returns>The wrapped instance.</returns>
        public static ServiceClientWrapper<TChannel> CreateAndWrap<TChannel> () where TChannel : class
        {
            return new ServiceClientWrapper<TChannel>();
        }

        /// <summary>Creates a service client instance and wraps it.</summary>
        /// <typeparam name="TChannel">The service interface.</typeparam>
        /// <param name="callbackInstance">The instance callback.</param>
        /// <returns>The wrapped instance.</returns>                
        public static ServiceClientWrapper<TChannel> CreateAndWrap<TChannel> ( InstanceContext callbackInstance ) where TChannel : class
        {
            return new ServiceClientWrapper<TChannel>(callbackInstance);
        }

        /// <summary>Creates a service client instance and wraps it.</summary>
        /// <typeparam name="TChannel">The service interface.</typeparam>
        /// <param name="endpointConfigurationName">The endpoint name.</param>
        /// <returns>The wrapped instance.</returns>        
        public static ServiceClientWrapper<TChannel> CreateAndWrap<TChannel> ( string endpointConfigurationName ) where TChannel : class
        {
            return new ServiceClientWrapper<TChannel>(endpointConfigurationName);
        }

        /// <summary>Creates a service client instance and wraps it.</summary>
        /// <typeparam name="TChannel">The service interface.</typeparam>
        /// <param name="endpoint">The service endpoint.</param>
        /// <returns>The wrapped instance.</returns>        
        public static ServiceClientWrapper<TChannel> CreateAndWrap<TChannel> ( ServiceEndpoint endpoint ) where TChannel : class
        {
            return new ServiceClientWrapper<TChannel>(endpoint);
        }

        /// <summary>Creates a service client instance and wraps it.</summary>
        /// <typeparam name="TChannel">The service interface.</typeparam>
        /// <param name="callbackInstance">The instance callback.</param>
        /// <param name="endpoint">The service endpoint.</param>
        /// <returns>The wrapped instance.</returns>        
        public static ServiceClientWrapper<TChannel> CreateAndWrap<TChannel> ( InstanceContext callbackInstance,
                                    ServiceEndpoint endpoint ) where TChannel : class
        {
            return new ServiceClientWrapper<TChannel>(callbackInstance, endpoint);
        }

        /// <summary>Creates a service client instance and wraps it.</summary>
        /// <typeparam name="TChannel">The service interface.</typeparam>
        /// <param name="callbackInstance">The instance callback.</param>
        /// <param name="endpointConfigurationName">The endpoint name.</param>
        /// <returns>The wrapped instance.</returns>        
        public static ServiceClientWrapper<TChannel> CreateAndWrap<TChannel> ( InstanceContext callbackInstance,
                                    string endpointConfigurationName ) where TChannel : class
        {
            return new ServiceClientWrapper<TChannel>(callbackInstance, endpointConfigurationName);
        }

        /// <summary>Creates a service client instance and wraps it.</summary>
        /// <typeparam name="TChannel">The service interface.</typeparam>
        /// <param name="endpointConfigurationName">The endpoint name.</param>
        /// <param name="remoteAddress">The endpoint address.</param>
        /// <returns>The wrapped instance.</returns>        
        public static ServiceClientWrapper<TChannel> CreateAndWrap<TChannel> ( string endpointConfigurationName,
                                    EndpointAddress remoteAddress ) where TChannel : class
        {
            return new ServiceClientWrapper<TChannel>(endpointConfigurationName, remoteAddress);
        }

        /// <summary>Creates a service client instance and wraps it.</summary>
        /// <typeparam name="TChannel">The service interface.</typeparam>
        /// <param name="endpointConfigurationName">The endpoint name.</param>
        /// <param name="remoteAddress">The endpoint address.</param>
        /// <returns>The wrapped instance.</returns>        
        public static ServiceClientWrapper<TChannel> CreateAndWrap<TChannel> ( string endpointConfigurationName,
                                    string remoteAddress ) where TChannel : class
        {
            return new ServiceClientWrapper<TChannel>(endpointConfigurationName, remoteAddress);
        }

        /// <summary>Creates a service client instance and wraps it.</summary>
        /// <typeparam name="TChannel">The service interface.</typeparam>
        /// <param name="binding">The binding to use.</param>
        /// <param name="remoteAddress">The endpoint address.</param>
        /// <returns>The wrapped instance.</returns>        
        public static ServiceClientWrapper<TChannel> CreateAndWrap<TChannel> ( Binding binding, EndpointAddress remoteAddress ) where TChannel : class
        {
            return new ServiceClientWrapper<TChannel>(binding, remoteAddress);
        }

        /// <summary>Creates a service client instance and wraps it.</summary>
        /// <typeparam name="TChannel">The service interface.</typeparam>
        /// <param name="callbackInstance">The instance callback.</param>
        /// <param name="binding">The binding to use.</param>
        /// <param name="remoteAddress">The endpoint address.</param>
        /// <returns>The wrapped instance.</returns>        
        public static ServiceClientWrapper<TChannel> CreateAndWrap<TChannel> ( InstanceContext callbackInstance,
                                            Binding binding, EndpointAddress remoteAddress ) where TChannel : class
        {
            return new ServiceClientWrapper<TChannel>(callbackInstance, binding, remoteAddress);
        }

        /// <summary>Creates a service client instance and wraps it.</summary>
        /// <typeparam name="TChannel">The service interface.</typeparam>
        /// <param name="callbackInstance">The instance callback.</param>
        /// <param name="endpointConfigurationName">The endpoint name.</param>
        /// <param name="remoteAddress">The endpoint address.</param>
        /// <returns>The wrapped instance.</returns>        
        public static ServiceClientWrapper<TChannel> CreateAndWrap<TChannel> ( InstanceContext callbackInstance,
                                            string endpointConfigurationName, EndpointAddress remoteAddress ) where TChannel : class
        {
            return new ServiceClientWrapper<TChannel>(callbackInstance, endpointConfigurationName, remoteAddress);
        }

        /// <summary>Creates a service client instance and wraps it.</summary>
        /// <typeparam name="TChannel">The service interface.</typeparam>
        /// <param name="callbackInstance">The instance callback.</param>
        /// <param name="endpointConfigurationName">The endpoint name.</param>
        /// <param name="remoteAddress">The endpoint address.</param>
        /// <returns>The wrapped instance.</returns>        
        public static ServiceClientWrapper<TChannel> CreateAndWrap<TChannel> ( InstanceContext callbackInstance,
                                            string endpointConfigurationName, string remoteAddress ) where TChannel : class
        {
            return new ServiceClientWrapper<TChannel>(callbackInstance, endpointConfigurationName, remoteAddress);
        }
        #endregion

        #region InvokeMethod

        /// <summary>Invokes a service method.</summary>
        /// <typeparam name="TChannel">The service interface.</typeparam>
        /// <param name="invocation">The function to execute.</param>
        /// <exception cref="ArgumentNullException"><paramref name="invocation"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method creates a service client, invokes a method and closes the client.  It is useful for invoking a single
        /// method without the boiler plate code.  Do not use this method if multiple methods need to be called
        /// on the same service instance.
        /// </remarks>
        public static void InvokeMethod<TChannel> ( Action<TChannel> invocation ) where TChannel : class
        {
            Verify.Argument("invocation", invocation).IsNotNull();

            using (var proxy = CreateAndWrap<TChannel>())
            {
                invocation(proxy.Client);
            };
        }

        /// <summary>Invokes a service method.</summary>
        /// <typeparam name="TChannel">The service interface.</typeparam>
        /// <param name="initializer">The initializer to create the proxy.  If this is <see langword="null"/> then the default initializer is used.</param>
        /// <param name="invocation">The function to execute.</param>
        /// <exception cref="ArgumentNullException"><paramref name="invocation"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method creates a service client, invokes a method and closes the client.  It is useful for invoking a single
        /// method without the boiler plate code.  Do not use this method if multiple methods need to be called
        /// on the same service instance.
        /// </remarks>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static void InvokeMethod<TChannel> ( Action<TChannel> invocation, Func<ServiceClientWrapper<TChannel>> initializer 
                                                  ) where TChannel : class
        {
            Verify.Argument("invocation", invocation).IsNotNull();

            Func<ServiceClientWrapper<TChannel>> init = initializer ?? (() => CreateAndWrap<TChannel>());

            using (var proxy = init())
            {
                invocation(proxy.Client);
            };
        }

        /// <summary>Invokes a service method.</summary>
        /// <typeparam name="TChannel">The service interface.</typeparam>
        /// <typeparam name="TResult">The return obj from the method.</typeparam>
        /// <param name="invocation">The function to execute.</param>
        /// <returns>The result of the method invocation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="invocation"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method creates a service client, invokes a method and closes the client.  It is useful for invoking a single
        /// method without the boiler plate code.  Do not use this method if multiple methods need to be called
        /// on the same service instance.
        /// </remarks>
        public static TResult InvokeMethod<TChannel, TResult> ( Func<TChannel, TResult> invocation ) where TChannel : class
        {
            Verify.Argument("invocation", invocation).IsNotNull();

            using (var proxy = CreateAndWrap<TChannel>())
            {
                return invocation(proxy.Client);
            };
        }

        /// <summary>Invokes a service method.</summary>
        /// <typeparam name="TChannel">The service interface.</typeparam>        
        /// <typeparam name="TResult">The return obj from the method.</typeparam>
        /// <param name="initializer">The initializer to create the proxy.  If <see langword="null"/> then the default initializer is used.</param>
        /// <param name="invocation">The function to execute.</param>
        /// <returns>The result of the method invocation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="invocation"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method creates a service client, invokes a method and closes the client.  It is useful for invoking a single
        /// method without the boiler plate code.  Do not use this method if multiple methods need to be called
        /// on the same service instance.
        /// </remarks>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static TResult InvokeMethod<TChannel, TResult> ( Func<TChannel, TResult> invocation, Func<ServiceClientWrapper<TChannel>> initializer 
                                                              ) where TChannel : class
        {
            Verify.Argument("invocation", invocation).IsNotNull();

            Func<ServiceClientWrapper<TChannel>> init = initializer ?? (() => CreateAndWrap<TChannel>());

            using (var proxy = init())
            {
                return invocation(proxy.Client);
            };
        }
        #endregion
    }
}
