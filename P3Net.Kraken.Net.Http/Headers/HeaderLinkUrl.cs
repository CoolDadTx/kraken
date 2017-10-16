/*
 * Copyright © 2016 Michael Taylor
 * All Rights Reserved
 * 
 * Portions Copyright Federation of State Medical Boards
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.Net.Http.Headers
{
    /// <summary>Represents a link URL in a Link header.</summary>
    /// <remarks>
    /// This type currently only supports links with the following parameters.
    /// <list type="bullet">
    ///     <item>rel</item>
    /// </list>
    /// <para />
    /// Multiple relations are supported.
    /// </remarks>
    /// <preliminary />
    public class HeaderLinkUrl
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="HeaderLinkUrl"/> class.</summary>
        /// <param name="url">The URL to use.</param>
        /// <exception cref="ArgumentNullException"><paramref name="url"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="url"/> is empty.</exception>        
        /// <exception cref="UriFormatException"><paramref name="url"/> is not an absolute URL.</exception>
        public HeaderLinkUrl ( string url )
        {
            Url = new Uri(url, UriKind.Absolute);
        }

        /// <summary>Initializes an instance of the <see cref="HeaderLinkUrl"/> class.</summary>
        /// <param name="url">The URL to use.</param>
        /// <exception cref="ArgumentNullException"><paramref name="url"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="url"/> is not an absolute URL.</exception>
        public HeaderLinkUrl ( Uri url )
        {
            Verify.Argument(nameof(url)).WithValue(url).IsNotNull().And.Is(u => u.IsAbsoluteUri, "Must be an absolute URI");

            Url = url;
        }

        /// <summary>Parses a formatted string to a link URL.</summary>
        /// <param name="value">The string to parse.</param>
        /// <returns>The link URL.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is empty.</exception>
        /// <exception cref="FormatException">Value is not properly formatted.</exception>
        /// <remarks>
        /// The string is expected to be formatted as required for the LINK header.
        /// </remarks>
        public static HeaderLinkUrl Parse ( string value )
        {
            Verify.Argument(nameof(value)).WithValue(value).IsNotNullOrEmpty();

            HeaderLinkUrl url;
            if (!TryParse(value, out url))
                throw new FormatException("Link is not properly formatted.");

            return url;
        }

        /// <summary>Attempts to parse a formatted string to a link URL.</summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="result">The result.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        /// <remarks>
        /// The string is expected to be formatted as required for the LINK header.
        /// </remarks>
        public static bool TryParse ( string value, out HeaderLinkUrl result )
        {
            result = null;

            if (String.IsNullOrEmpty(value))
                return false;

            //Format is <{url}>[; rel="{rel}">
            var match = s_re.Match(value);
            if (!match.Success)
                return false;

            var str = match.Groups["url"]?.Value;
            Uri uri;
            if (String.IsNullOrEmpty(str) || !Uri.TryCreate(str, UriKind.Absolute, out uri))
                return false;

            var url = new HeaderLinkUrl(uri);

            //Parse the relations, if any
            var relationValue = match.Groups["relation"]?.Value;
            if (!String.IsNullOrEmpty(relationValue))
            {
                var relations = relationValue.Split(s_relationDelimiters, StringSplitOptions.RemoveEmptyEntries);
                url.Relations.AddRange(relations);
            };

            result = url;
            return true;
        }
        #endregion

        /// <summary>Gets or sets the relationship.</summary>
        /// <value>When setting this property, any existing relations are cleared.</value>
        public string Relation
        {
            get => Relations.FirstOrDefault();
            set {
                var count = Relations.Count;

                if (count > 1)
                {
                    Relations.Clear();
                    Relations.Add(value);
                } else if (count == 1)
                    Relations[0] = value;
                else
                    Relations.Add(value);
            }
        }

        /// <summary>Gets the list of relations associated with the link.</summary>
        public List<string> Relations { get; } = new List<string>();

        /// <summary>Gets the URL.</summary>        
        public Uri Url { get; private set; }

        /// <summary>Gets the string value.</summary>
        /// <returns>The string representation.</returns>
        public override string ToString ()
        {
            if (!String.IsNullOrEmpty(Relation))
                return $"<{Url}>; rel=\"{Relation}\"";

            return $"<{Url}>";
        }

        #region Private Members

        private static readonly Regex s_re = new Regex("<(\"|')?(?<url>.*)(\"|')?>\\s*(;\\s*rel=(\"|')(?<relation>.*)(\"|'))?", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        private static readonly char[] s_relationDelimiters = new char[] { ' ', '\t' };

        #endregion
    }
}