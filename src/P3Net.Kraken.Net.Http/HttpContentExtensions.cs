/*
 * Copyright © 2017 Michael Taylor
 * All Rights Reserved
 * 
 * Portions copyright Federation of State Medical Boards
 */
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace P3Net.Kraken.Net.Http
{
    /// <summary>Provides extension methods for <see cref="HttpContent"/>.</summary>
    public static class HttpContentExtensions
    {
        /// <summary>Attempts to convert HTTP content to a JSON string.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The content as a string.</returns>
        /// <remarks>
        /// If the content is not convertible to a string (eg. byte array) then nothing is returned.
        /// </remarks>
        public static async Task<string> ToJsonAsync ( this HttpContent source )
        {
            if (source == null)
                return null;

            if (!(source is StringContent))
            {
                if (source is ByteArrayContent)
                {
                    var data = new ContentDescriptor()
                    {
                        Type = "byte[]",
                        Length = source.Headers.ContentLength ?? -1
                    };

                    return await ToJsonAsync(data).ConfigureAwait(false);
                } else if (source is StreamContent)
                {
                    var data = new ContentDescriptor()
                    {
                        Type = "Stream",
                        Length = source.Headers.ContentLength ?? -1
                    };

                    return await ToJsonAsync(data).ConfigureAwait(false);
                } else if (source is MultipartContent mpc)
                {
                    var data = new ContentDescriptor()
                    {
                        Type = "MultiPart",
                        Length = source.Headers.ContentLength ?? -1
                    };

                    var children = from child in mpc
                                   select child.ToJsonAsync();
                    data.Data = await Task.WhenAll(children).ConfigureAwait(false);

                    return await ToJsonAsync(data).ConfigureAwait(false);
                };
            };

            //Try to read the data as a string
            var value = await source.ReadAsStringAsync().ConfigureAwait(false);

            //Is this JSON?
            var mediaType = source.Headers.ContentType?.MediaType ?? "";
            if ((mediaType.IndexOf("json", StringComparison.OrdinalIgnoreCase) >= 0) || value.TrimStart().StartsWith("{"))
                return value;

            //Possibly XML or something else
            return "{ \"value\": " + "\"" + value + "\" }";
        }

        #region Private Members
        
        private static Task<string> ToJsonAsync<T> ( T data )
        {
            return Task.Run(() => JsonConvert.SerializeObject(data));
        }

        private struct ContentDescriptor
        {
            public string Type { get; set; }
            public long Length { get; set; }
            public string[] Data { get; set; }
        }
        #endregion
    }
}
