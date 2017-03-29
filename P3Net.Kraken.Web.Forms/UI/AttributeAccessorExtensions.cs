/*
 * Copyright © 2013 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace P3Net.Kraken.Web.UI
{
    /// <summary>Provides extension methods for <see cref="IAttributeAccessor"/>.</summary>
    public static class AttributeAccessorExtensions
    {
        /// <summary>Attaches a client-side click handler to a control.</summary>
        /// <param name="source">The source value.</param>
        /// <param name="handler">The Javascript handler.</param>
        public static void OnClientClick ( this IAttributeAccessor source, string handler )
        {
            source.SetAttribute("onclick", handler);            
        }
    }
}
