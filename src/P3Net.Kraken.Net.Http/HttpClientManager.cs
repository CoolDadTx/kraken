/*
 * Copyright © 2017 Michael Taylor
 * All Rights Reserved
 * 
 * Portions copyright Federation of State Medical Boards
 */
using System;
using System.Net.Http;
using System.Runtime.Caching;

using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.Net.Http
{
    /// <summary>Provides a factory for getting <see cref="HttpClient"/> instances.</summary>
    /// <remarks>
    /// <see cref="HttpClient"/> is thread safe for the most part but changing properties on the class is not. The factory
    /// provides a thread-safe pool of <see cref="HttpClient"/> objects to use. Each client is identified by a unique name allowing
    /// an application to create clients by name and reuse them throughout the application without having to worry about the lifetime.
    /// <para />
    /// Some important notes:
    /// <list type="numbered">
    /// <item>The handler is associated with the client and their lifetimes are connected.</item>
    /// <item>It is assumed that applications will not change the <see cref="HttpClient"/> properties or that of the handler after it is created.</item>
    /// <item>The factory does not clean up the clients on shutdown since the process will handle this automatically.</item>
    /// </list>
    /// </remarks>
    public class HttpClientManager
    {
        /// <summary>Provides a default instance that can be used across an entire application.</summary>
        public static readonly HttpClientManager Default = new HttpClientManager();

        /// <summary>Gets or sets the default expiration interval for a client.</summary>
        /// <value>The default is 10 minutes.</value>
        public TimeSpan DefaultExpirationInterval { get; set; } = new TimeSpan(0, 10, 0);

        /// <summary>Clears the factory of all clients.</summary>
        public void Clear ()
        { 
            lock (s_clients)
            {
                var keys = s_clients.GetValues(null).Keys;

                foreach (var key in keys)
                {
                    var client = s_clients.Remove(key) as HttpClient;
                    SafeDispose(client);
                };
            };            
        }

        /// <summary>Determines if a client exists.</summary>
        /// <param name="clientName">The name of the client.</param>
        /// <returns><see langword="true"/> if the client exists or <see langword="false"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="clientName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="clientName"/> is empty.</exception>
        public bool Exists ( string clientName )
        {
            Verify.Argument(nameof(clientName)).WithValue(clientName).IsNotNullOrEmpty();

            return s_clients.Contains(clientName);
        }


        /// <summary>Gets a client.</summary>
        /// <param name="clientName">The name of the client.</param>
        /// <param name="clientUri">The client URL.</param>
        /// <returns>The client.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="clientName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="clientName"/> is empty.</exception>
        /// <exception cref="UriFormatException"><paramref name="clientUri"/> is not a valid URL.</exception>
        public HttpClient Get ( string clientName, string clientUri )
        {
            Verify.Argument(nameof(clientName)).WithValue(clientName).IsNotNullOrEmpty();

            return GetCore(clientName, () => CreateCore(new Uri(clientUri, UriKind.Absolute), null));
        }

        /// <summary>Gets a client.</summary>
        /// <param name="clientName">The name of the client.</param>
        /// <param name="clientUri">The client URL.</param>
        /// <returns>The client.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="clientName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="clientName"/> is empty.</exception>
        public HttpClient Get ( string clientName, Uri clientUri )
        {
            Verify.Argument(nameof(clientName)).WithValue(clientName).IsNotNullOrEmpty();

            return GetCore(clientName, () => CreateCore(clientUri, null));
        }

        /// <summary>Gets a client.</summary>
        /// <param name="clientName">The name of the client.</param>
        /// <param name="clientUri">The client URL.</param>
        /// <param name="handler">The optional message handler.</param>
        /// <returns>The client.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="clientName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="clientName"/> is empty.</exception>
        /// <exception cref="UriFormatException"><paramref name="clientUri"/> is not a valid URL.</exception>
        public HttpClient Get ( string clientName, string clientUri, HttpMessageHandler handler )
        {
            Verify.Argument(nameof(clientName)).WithValue(clientName).IsNotNullOrEmpty();

            return GetCore(clientName, () => CreateCore(new Uri(clientUri, UriKind.Absolute), handler));
        }

        /// <summary>Gets a client.</summary>
        /// <param name="clientName">The name of the client.</param>
        /// <param name="clientUri">The client URL.</param>
        /// <param name="handler">The optional message handler.</param>
        /// <returns>The client.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="clientName"/> or <paramref name="clientUri"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="clientName"/> is empty.</exception>
        public HttpClient Get ( string clientName, Uri clientUri, HttpMessageHandler handler )
        {
            Verify.Argument(nameof(clientName)).WithValue(clientName).IsNotNullOrEmpty();
            Verify.Argument(nameof(clientUri)).WithValue(clientUri).IsNotNull();

            return GetCore(clientName, () => CreateCore(clientUri, handler));
        }

        /// <summary>Gets a client.</summary>
        /// <param name="clientName">The name of the client.</param>
        /// <param name="creator">The function to create the client.</param>
        /// <returns>The client.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="clientName"/> or <paramref name="creator "/>is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="clientName"/> is empty.</exception>
        public HttpClient Get ( string clientName, Func<HttpClient> creator )
        {
            Verify.Argument(nameof(clientName)).WithValue(clientName).IsNotNullOrEmpty();
            Verify.Argument(nameof(creator)).WithValue(creator).IsNotNull();

            return GetCore(clientName, creator);
        }

        /// <summary>Gets all the clients.</summary>
        /// <returns>The clients.</returns>        
        public HttpClient[] GetAll ()
        {
            lock (s_clients)
            {
                var values = s_clients.GetValues(null).Values;

                var clients = new HttpClient[values.Count];
                clients.CopyTo(clients, 0);

                return clients;
            };
        }

        /// <summary>Removes a client.</summary>
        /// <param name="clientName">The client name.</param>
        /// <remarks>
        /// The client is cleaned up and removed from the factory.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="clientName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="clientName"/> is empty.</exception>        
        public void Remove ( string clientName )
        {
            Verify.Argument(nameof(clientName)).WithValue(clientName).IsNotNullOrEmpty();

            var client = s_clients.Remove(clientName) as HttpClient;
            SafeDispose(client);
        }
        
        #region Private Members

        private HttpClient CreateCore ( Uri clientUri, HttpMessageHandler handler )
        {
            var client = (handler != null) ? new HttpClient(handler) : new HttpClient();
            client.BaseAddress = clientUri;

            return client;
        }

        private HttpClient GetCore ( string clientName, Func<HttpClient> creator )
        {
            var client = s_clients.Get(clientName) as HttpClient;
            if (client != null)
                return client;

            var policy = new CacheItemPolicy() {
                SlidingExpiration = DefaultExpirationInterval,                
            };

            //AddOrGetExisting returns null if the item isn't in the cache yet
            client = creator();
            s_clients.AddOrGetExisting(clientName, client, policy);
            return client;
        }

        private void SafeDispose ( HttpClient client )
        {
            try
            {
                client?.Dispose();
            } catch
            { /* Ignore exceptions */ };
        }

        private readonly MemoryCache s_clients = new MemoryCache(nameof(HttpClientManager));
        #endregion
    }
}
