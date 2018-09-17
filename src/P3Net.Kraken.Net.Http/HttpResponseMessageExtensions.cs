/*
 * Copyright © 2017 Michael Taylor
 * All Rights Reserved
 * 
 * Portions copyright Federation of State Medical Boards
 */
using System;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace P3Net.Kraken.Net.Http
{
    /// <summary>Provides extension methods for <see cref="HttpResponseMessage"/>.</summary>
    public static class HttpResponseMessageExtensions
    {
        /// <summary>Deserialize the message as JSON.</summary>
        /// <typeparam name="T">The desired type</typeparam>
        /// <param name="source">The source.</param>   
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>
        /// The message is first verified to ensure it is valid. If the media type is not a valid JSON media type then it throws an exception.
        /// </remarks>
        public static async Task<T> DeserializeJsonAsync<T> ( this HttpResponseMessage source, CancellationToken cancellationToken )
        {
            source.ThrowIfError();
            
            //Handle 204 responses (no content) or 200 with no body
            if (source.StatusCode == System.Net.HttpStatusCode.NoContent || source.Content == null)
                return default(T);

            if (!source.IsJsonResponse())
                throw new Exception("Unsupported media type - cannot deserialize JSON");

            var str = await source.Content.ReadAsStringAsync().ConfigureAwait(false);
            cancellationToken.ThrowIfCancellationRequested();

            //Handle 200 with no data
            if (String.IsNullOrEmpty(str))
                return default(T);

            return JsonConvert.DeserializeObject<T>(str);
        }

        /// <summary>Gets the message error.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The exception, if any.</returns>
        /// <remarks>
        /// WARNING: This method will clear the Content value.
        /// </remarks>
        public static Exception GetError ( this HttpResponseMessage source )
        {
            try
            {
                source.EnsureSuccessStatusCode();
            } catch (HttpRequestException e)
            {
                return e;
            };

            return null;
        }

        /// <summary>Handles the response as a JSON string of the provided type.</summary>
        /// <typeparam name="T">The expected type.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>The typed response.</returns>
        /// <exception cref="Exception">Deserialization failed.</exception>
        /// <remarks>
        /// The underlying response is automatically disposed when the method completes.
        /// </remarks>
        /// <preliminary />
        public static Task<T> HandleJsonAsync<T> ( this HttpResponseMessage source ) => HandleJsonAsync<T>(source, CancellationToken.None);

        /// <summary>Handles the response as a JSON string of the provided type.</summary>
        /// <typeparam name="T">The expected type.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The typed response.</returns>
        /// <exception cref="Exception">Deserialization failed.</exception>
        /// <remarks>
        /// The underlying response is automatically disposed when the method completes.
        /// </remarks>
        /// <preliminary />
        public static async Task<T> HandleJsonAsync<T> ( this HttpResponseMessage source, CancellationToken cancellationToken )
        {
            using (source)
            {
                return await source.DeserializeJsonAsync<T>(cancellationToken).ConfigureAwait(false);
            };
        }

        /// <summary>Handles the response as a JSON string of the provided type.</summary>
        /// <typeparam name="T">The expected type.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>The typed response.</returns>
        /// <exception cref="Exception">Deserialization failed.</exception>
        /// <remarks>
        /// The underlying response is automatically disposed when the method completes.
        /// </remarks>
        /// <preliminary />
        public static Task<T> HandleJsonAsync<T> ( this Task<HttpResponseMessage> source ) => HandleJsonAsync<T>(source, CancellationToken.None);

        /// <summary>Handles the response as a JSON string of the provided type.</summary>
        /// <typeparam name="T">The expected type.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The typed response.</returns>
        /// <exception cref="Exception">Deserialization failed.</exception>
        /// <remarks>
        /// The underlying response is automatically disposed when the method completes.
        /// </remarks>
        /// <preliminary />
        public static async Task<T> HandleJsonAsync<T> ( this Task<HttpResponseMessage> source, CancellationToken cancellationToken )
        {
            using (var response = await source)
            {
                return await response.DeserializeJsonAsync<T>(cancellationToken).ConfigureAwait(false);
            };
        }

        /// <summary>Determines if the content is a JSON response.</summary>
        /// <param name="source">The source.</param>
        /// <returns><see langword="true"/> if the content is JSON.</returns>
        public static bool IsJsonResponse ( this HttpResponseMessage source )
                            => source.Content?.Headers.ContentType.MediaType.IndexOf("json", StringComparison.OrdinalIgnoreCase) >= 0;

        /// <summary>Throws an exception if the message indicates an error.</summary>
        /// <param name="source">The source.</param>
        /// <remarks>
        /// The response is checked to see if there is a non-success code returned. If so then it attempts
        /// to get the content body containing the error, if possible. If it is unable to locate any useful
        /// error information then the standard HttpResponseException is thrown with the error information.
        /// </remarks>
        public static void ThrowIfError ( this HttpResponseMessage source ) => ThrowIfErrorAsync(source).Wait();

        /// <summary>Throws an exception if the message indicates an error.</summary>
        /// <param name="source">The source.</param>
        /// <remarks>
        /// The response is checked to see if there is a non-success code returned. If so then it attempts
        /// to get the content body containing the error, if possible. If it is unable to locate any useful
        /// error information then the standard HttpResponseException is thrown with the error information.
        /// </remarks>
        public static async Task ThrowIfErrorAsync ( this HttpResponseMessage source )
        {
            if (source.IsSuccessStatusCode)
                return;

            Exception error = null;

            //If there is content
            if (source.Content != null)
            {
                var content = await TryGetErrorMessageAsync(source).ConfigureAwait(false);
                if (!String.IsNullOrEmpty(content))
                    error = new HttpRequestException(content, source.GetError());                
            };

            if (error != null)
            {
                //Clean up before the throw
                source.Content.Dispose();
                throw error;
            };

            //Default
            source.EnsureSuccessStatusCode();
        }       

        /// <summary>Attempts to get the error message associated with the response.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The error message, if any.</returns>
        public static async Task<string> TryGetErrorMessageAsync ( this HttpResponseMessage source )
        {
            //Start with the reason phrase
            var message = source.ReasonPhrase;

            try
            {
                //If we can read the content as a string
                if (source.Content != null)
                {
                    var content = await source.Content.ReadAsStringAsync().ConfigureAwait(false);

                    //If the content is JSON
                    if (source.IsJsonResponse())
                    {
                        //Try and parse the content into a JSON object we can examine
                        var data = JsonConvert.DeserializeObject(content);
                        if (data is string str)
                        {
                            //The JSON is actually just a string so use it
                            content = str;
                        } else if (data != null)
                        {
                            //Look for a "message" property, set the content to it if successful
                            var propMessage = FindMessageProperty(data);
                            if (!String.IsNullOrEmpty(propMessage))
                                content = propMessage;
                        };
                    };

                    if (!String.IsNullOrEmpty(content))
                    {
                        //Read the content as a string and use it, if set                        
                        message = content;
                    };
                };
            } catch
            { /* Ignore errors */ };

            return message;
        }

        #region Private Members

        private static string FindMessageProperty ( object value )
        {
            var type = value.GetType();
            var flags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.FlattenHierarchy;

            var prop = type.GetProperty("message", flags) ?? type.GetProperty("errormessage", flags);
            return prop?.GetValue(value) as string;
        }
        #endregion
    }
}
