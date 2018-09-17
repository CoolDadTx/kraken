/*
 * Copyright © 2014 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web.UI;
using System.Web.UI.WebControls;

namespace P3Net.Kraken.Web.UI.WebControls
{
    /// <summary>Provides extension methods for <see cref="WebControl"/>.</summary>
    public static class WebControlExtensions
    {
        /// <summary>Renders a control as hidden.</summary>
        /// <param name="source">The source value.</param>
        /// <remarks>
        /// This method renders the control as hidden.  Client code can show the control.  This differs from
        /// <see cref="Control.Visible"/> in that an invisible control is not rendered at all.
        /// </remarks>
        public static void Hide ( this WebControl source )
        {
            source.Style.Add(HtmlTextWriterStyle.Display, "none");
        }
    }
}
