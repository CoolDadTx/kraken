/*
 * Copyright © 2016 Michael Taylor
 * All Rights Reserved
 * 
 * Portions copyright Federation of State Medical Boards
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace P3Net.Kraken.Net.Http.Headers
{
    /// <summary>Provides extension methods for <see cref="HttpResponseHeaders"/>.</summary>
    public static class HttpResponseHeadersExtensions
    {
        /// <summary>Gets the URLs from the Link HTTP header, if any.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The list of URLs.</returns>
        /// <remarks>
        /// If there are multiple link headers then they are concatenated.
        /// <para />
        /// Multiple links with the same relation are not filtered out.
        /// </remarks>
        public static IEnumerable<HeaderLinkUrl> GetLink ( this HttpResponseHeaders source )
        {
            IEnumerable<string> values;
            if (source.TryGetValues(StandardHeaders.Links, out values) && (values?.Any() ?? false))
            {
                foreach (var value in values)
                {
                    if (String.IsNullOrEmpty(value))
                        continue;

                    //Links are separated by commas
                    var links = value.Split(',');
                    foreach (var link in links)
                    {
                        if (String.IsNullOrEmpty(link))
                            continue;

                        HeaderLinkUrl url;
                        if (HeaderLinkUrl.TryParse(link, out url))
                            yield return url;
                    };
                };
            };
        }

        /// <summary>Sets the Link HTTP header to the given urls.</summary>
        /// <param name="source">The source.</param>
        /// <param name="links">The list of URLs.</param>
        /// <remarks>
        /// Any existing Link header is replaced with the new values.
        /// <para />
        /// Multiple links with the same relation are not filtered out.
        /// </remarks>
        public static void SetLink ( this HttpResponseHeaders source, IEnumerable<HeaderLinkUrl> links )
        {
            //Remove any existing link header
            source.Remove(StandardHeaders.Links);

            //Links are 
            source.Add(StandardHeaders.Links, String.Join(",", links));
        }
    }
}
