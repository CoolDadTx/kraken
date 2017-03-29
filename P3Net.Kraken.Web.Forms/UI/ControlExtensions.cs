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
    /// <summary>Provides extension methods for <see cref="Control"/>.</summary>
    public static class ControlExtensions
    {
        /// <summary>Finds a control by its ID.</summary>
        /// <param name="source">The source value.</param>
        /// <param name="id">The ID of the control.</param>
        /// <returns>The control, if found.</returns>
        public static T FindControl<T> ( this Control source, string id ) where T: Control
        {
            return source.FindControl(id) as T;
        }
    }
}
