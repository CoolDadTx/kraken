/*
 * Copyright © 2017 Michael Taylor
 * All Rights Reserved
 * 
 * Portions copyright Federation of State Medical Boards
 */
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.Net.Http
{
    /// <summary>Provides extension methods for <see cref="HttpClient"/>.</summary>
    public static class HttpClientExtensions
    {
        /// <summary>Ensures the client allows JSON as a response if it is not already specified.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The client.</returns>
        public static HttpClient EnsureJsonAccepted ( this HttpClient source )
        {
            var acceptTypes = source.DefaultRequestHeaders.Accept;

            var jsonType = new MediaTypeWithQualityHeaderValue("application/json");
            if (!acceptTypes.Contains(jsonType))
                acceptTypes.Add(jsonType);

            return source;
        }

        #region GetAsync<T>
        
        /// <summary>Makes an asynchronous GET request.</summary>
        /// <typeparam name="T">The expected result.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="requestUri">The URI to send to.</param>
        /// <returns>The results.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="requestUri"/> is null or empty.</exception>
        /// <preliminary />
        public static Task<T> GetJsonAsync<T> ( this HttpClient source, string requestUri ) 
                                => GetJsonAsync<T>(source, new Uri(requestUri), CancellationToken.None);

        /// <summary>Makes an asynchronous GET request.</summary>
        /// <typeparam name="T">The expected result.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="requestUri">The URI to send to.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The results.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="requestUri"/> is null or empty.</exception>
        /// <preliminary />
        public static Task<T> GetJsonAsync<T> ( this HttpClient source, string requestUri, CancellationToken cancellationToken )
                                    => GetJsonAsync<T>(source, new Uri(requestUri), cancellationToken);

        /// <summary>Makes an asynchronous GET request.</summary>
        /// <typeparam name="T">The expected result.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="requestUri">The URI to send to.</param>
        /// <returns>The results.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="requestUri"/> is null or empty.</exception>
        /// <preliminary />
        public static Task<T> GetJsonAsync<T> ( this HttpClient source, Uri requestUri )
                                => GetJsonAsync<T>(source, requestUri, CancellationToken.None);

        /// <summary>Makes an asynchronous GET request.</summary>
        /// <typeparam name="T">The expected result.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="requestUri">The URI to send to.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The results.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="requestUri"/> is null or empty.</exception>
        /// <preliminary />
        public static Task<T> GetJsonAsync<T> ( this HttpClient source, Uri requestUri, CancellationToken cancellationToken )
        {
            Verify.Argument(nameof(requestUri)).WithValue(requestUri).IsNotNull();

            return source.GetJsonAsync<T>(requestUri.ToString(), cancellationToken);
        }        
        #endregion

        #region PostJsonAsync

        /// <summary>Posts data as a JSON object.</summary>
        /// <typeparam name="TRequest">The type of the data being sent.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="requestUri">The URI to send to.</param>
        /// <param name="content">The data to send.</param>
        /// <returns>The response.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="requestUri"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentException"><paramref name="requestUri"/> is empty.</exception>
        /// <preliminary />
        public static Task<HttpResponseMessage> PostJsonAsync<TRequest> ( this HttpClient source, string requestUri, TRequest content )
                                => PostJsonAsync<TRequest>(source, new Uri(requestUri), content, CancellationToken.None);

        /// <summary>Posts data as a JSON object.</summary>
        /// <typeparam name="TRequest">The type of the data being sent.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="requestUri">The URI to send to.</param>
        /// <param name="content">The data to send.</param>
        /// <returns>The response.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="requestUri"/> is <see langword="null" />.</exception>
        /// <preliminary />
        public static Task<HttpResponseMessage> PostJsonAsync<TRequest> ( this HttpClient source, Uri requestUri, TRequest content )
                                        => PostJsonAsync<TRequest>(source, requestUri, content, CancellationToken.None);

        /// <summary>Posts data as a JSON object.</summary>
        /// <typeparam name="TRequest">The type of the data being sent.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="requestUri">The URI to send to.</param>
        /// <param name="content">The data to send.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="requestUri"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentException"><paramref name="requestUri"/> is empty.</exception>
        /// <preliminary />
        public static Task<HttpResponseMessage> PostJsonAsync<TRequest> ( this HttpClient source, string requestUri, TRequest content, CancellationToken cancellationToken )
                                    => PostJsonAsync<TRequest>(source, new Uri(requestUri), content, null, cancellationToken);

        /// <summary>Posts data as a JSON object.</summary>
        /// <typeparam name="TRequest">The type of the data being sent.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="requestUri">The URI to send to.</param>
        /// <param name="content">The data to send.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="requestUri"/> is <see langword="null" />.</exception>
        /// <preliminary />
        public static Task<HttpResponseMessage> PostJsonAsync<TRequest> ( this HttpClient source, Uri requestUri, TRequest content, CancellationToken cancellationToken )
                                    => PostJsonAsync<TRequest>(source, requestUri, content, null, cancellationToken);

        /// <summary>Posts data as a JSON object.</summary>
        /// <typeparam name="TRequest">The type of the data being sent.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="requestUri">The URI to send to.</param>
        /// <param name="content">The data to send.</param>
        /// <param name="serializerSettings">The optional serializer settings.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="requestUri"/> is <see langword="null" />.</exception>
        /// <preliminary />
        public static async Task<HttpResponseMessage> PostJsonAsync<TRequest> ( this HttpClient source, Uri requestUri, TRequest content
                                                                              , JsonSerializerSettings serializerSettings, CancellationToken cancellationToken )
        {
            Verify.Argument(nameof(requestUri)).WithValue(requestUri).IsNotNull();

            var data = await Task.Run(() => serializerSettings != null ? JsonConvert.SerializeObject(content, serializerSettings) : JsonConvert.SerializeObject(content)).ConfigureAwait(false);

            return await source.PostAsync(requestUri, new StringContent(data, Encoding.UTF8, "application/json"), cancellationToken).ConfigureAwait(false);
        }
        #endregion
    }
}
