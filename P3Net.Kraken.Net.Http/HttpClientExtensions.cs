/*
 * Copyright © 2017 Michael Taylor
 * All Rights Reserved
 * 
 * Portions copyright Federation of State Medical Boards
 */
using System;
using System.Net.Http;
using System.Net.Http.Headers;

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
    }
}
