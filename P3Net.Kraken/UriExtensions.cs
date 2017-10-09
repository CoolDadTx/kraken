/*
 * Copyright © 2017 P3Net
 * All Rights Reserved
 * 
 * Portions copyright Federation of State Medical Boards
 */
using System;

namespace P3Net.Kraken
{
    /// <summary>Provides extensions for working with <see cref="Uri"/>.</summary>
    /// <preliminary />
    public static class UriExtensions
    {
        /// <summary>Joins a URI with a relative path.</summary>
        /// <param name="source">The source.</param>
        /// <param name="relativeUrl">The relative path to add.</param>
        /// <returns>The new URL.</returns>
        /// <remarks>
        /// Do not include any query string information on the source URL. If <paramref name="relativeUrl"/> is empty then
        /// source is returned.
        /// <para />
        /// Unlike the standard <see cref="Uri"/> methods, this method will combine partial paths to produce a full path (i.e. "tempuri.org/baseapp", "api/resource" would yield "tempuri.org/baseapp/api/resource").
        /// </remarks>
        public static Uri Join ( this Uri source, string relativeUrl )
        {
            if (String.IsNullOrEmpty(relativeUrl))
                return source;

            var newUri = source.AbsoluteUri;

            var hasLeftSlash = newUri.EndsWith("/");
            var hasRightSlash = relativeUrl.StartsWith("/");

            if (hasLeftSlash && hasRightSlash)
                newUri += relativeUrl.Substring(1);
            else if (hasLeftSlash || hasRightSlash)
                newUri += relativeUrl;
            else
                newUri += "/" + relativeUrl;

            return new Uri(newUri);
        }
    }
}
