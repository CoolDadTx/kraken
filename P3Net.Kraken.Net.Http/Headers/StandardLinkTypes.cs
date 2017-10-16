/*
 * Copyright © 2016 Michael Taylor
 * All Rights Reserved
 * 
 * Portions copyright Federation of State Medical Boards
 */
using System;

namespace P3Net.Kraken.Net.Http.Headers
{
    /// <summary>Defines some standard link types.</summary>
    public static class StandardLinkTypes
    {
        /// <summary>Provides a link for specifying an alternative link (i.e. stylesheet).</summary>
        public const string Alternate = "alternate";

        /// <summary>Provides a link for specifying an author.</summary>
        public const string Author = "author";
        
        /// <summary>Provides a link for getting to the first of a set of items.</summary>
        public const string First = "first";

        /// <summary>Provides a link for getting help.</summary>
        public const string Help = "help";

        /// <summary>Provides a link for getting an icon.</summary>
        public const string Icon = "icon";

        /// <summary>Provides a link for getting to the last of a set of items.</summary>
        public const string Last = "last";

        /// <summary>Provides a link for getting license information.</summary>
        public const string License = "license";

        /// <summary>Provides a link for getting an application manifest.</summary>
        public const string Manifest = "manifest";

        /// <summary>Provides a link for getting to the next of a set of items.</summary>
        public const string Next = "next";

        /// <summary>Provides a link specifying the content to pre-load.</summary>
        public const string Preload = "preload";

        /// <summary>Provides a link for getting to the previous of a set of items.</summary>
        public const string Previous = "prev";

        /// <summary>Provides a link for search information.</summary>
        public const string Search = "search";

        /// <summary>Provides a link for specifying a stylesheet.</summary>
        public const string Stylesheet = "stylesheet";
    }
}