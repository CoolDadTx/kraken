/*
 * Copyright © 2016 Michael Taylor
 * All Rights Reserved
 * 
 * Portions copyright Federation of State Medical Boards
 */
using System;

namespace P3Net.Kraken.Net.Http.Headers
{
    /// <summary>Provides a list of standard header names.</summary>
    public static class StandardHeaders
    {
        /// <summary>The list of links.</summary>
        public const string Links = "Link";

        /// <summary>The count of pages that are available.</summary>
        public const string PageCount = "X-Page-Count";

        /// <summary>The count of items that are available.</summary>
        public const string TotalCount = "X-Total-Count";
    }
}
