/*
 * Copyright © Michael L. Taylor
 * All Rights Reserved
 * 
 */
using System;
using System.Web.UI.WebControls;

using P3Net.Kraken;

namespace P3Net.Kraken.Web.UI.WebControls
{
    /// <summary>Provides methods for working with web controls.</summary>    
    public static class ListControlExtensions
    {
        #region ListControlFill

        /// <summary>Fills a list control with the values from an enumeration.</summary>
        /// <typeparam name="T">The enumerated type to fill the list with.</typeparam>
        /// <param name="source">The control to populate.</param>
        /// <remarks>
        /// This method uses <see cref="EnumExtensions.GetFormattedNames{T}"/> to build the list of names and values.
        /// </remarks>
        /// <returns>The control.</returns>
        /// <example>
        /// <code lang="C#">
        ///    protected override void OnLoad ( EventArgs e )
        ///    {
        ///       if (!IsPostBack)
        ///       {
        ///          ddlTags..ListControlFill&lt;HtmlTextWriterTag&gt;();
        ///       };
        ///    }
        /// </code>
        /// <code lang="VB">
        ///    Protected Overrides Sub OnLoad ( e As EventArgs )
        ///       If Not IsPostBack Then
        ///          ddlTags.ListControlFill(Of HtmlTextWriterTag)()
        ///       End If
        ///    End Sub
        /// </code>
        /// </example>
        public static ListControl ListControlFill<T> ( this ListControl source ) where T : struct
        {
            InternalListControlFill<T>(source, null);

            return source;
        }

        /// <summary>Fills a list control with the values from an enumeration.</summary>
        /// <typeparam name="T">The enumerated type to fill the list with.</typeparam>
        /// <param name="initialValue">The initial value of the control.</param>
        /// <param name="source">The control to populate.</param>
        /// <remarks>
        /// This method uses <see cref="EnumExtensions.GetFormattedNames{T}"/> to build the list of names and values.
        /// </remarks>
        /// <returns>The control.</returns>
        /// <example>
        /// <code lang="C#">
        ///    protected override void OnLoad ( EventArgs e )
        ///    {
        ///       if (!IsPostBack)
        ///       {
        ///          ddlTags.ListControlFill&lt;HtmlTextWriterTag&gt;(HtmlTextWriterTag.Br);
        ///       };
        ///    }
        /// </code>
        /// <code lang="VB">
        ///    Protected Overrides Sub OnLoad ( e As EventArgs )
        ///       If Not IsPostBack Then
        ///          ddlTags.ListControlFill(Of HtmlTextWriterTag)(HtmlTextWriterTag.Br)
        ///       End If
        ///    End Sub
        /// </code>
        /// </example>
        public static ListControl ListControlFill<T> ( this ListControl source, T initialValue ) where T : struct
        {
            InternalListControlFill<T>(source, initialValue);

            return source;
        }
        #endregion

        #region ListControlSelect

        /// <summary>Selects the given value in the list control.</summary>
        /// <param name="source">The control to select from.</param>
        /// <param name="value">The value to select.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>		
        /// <example>
        /// <code lang="C#">
        ///    protected override void OnLoad ( EventArgs e )
        ///    {
        ///       if (!IsPostBack)
        ///       {
        ///          ddlTags.ListControlSelect(HtmlTextWriterTag.Body);
        ///       };
        ///    }
        /// </code>
        /// <code lang="VB">
        ///    Protected Overrides Sub OnLoad ( e As EventArgs )
        ///       If Not IsPostBack Then
        ///          ddlTags.ListControlSelect(HtmlTextWriterTag.Body)
        ///       End If
        ///    End Sub
        /// </code>
        /// </example>
        public static bool ListControlSelect ( this ListControl source, object value )
        {
            string strValue = (value != null) ? value.ToString() : "";

            //Find it
            for (int nIdx = 0; nIdx < source.Items.Count; ++nIdx)
            {
                if (source.Items[nIdx].Value == strValue)
                {
                    source.SelectedIndex = nIdx;
                    return true;
                };
            };

            return false;
        }
        #endregion

        #region Private Members

        private static void InternalListControlFill<T> ( ListControl control, T? initialValue ) where T : struct
        {			
            bool bHasInit = initialValue.HasValue;
        
            //Get the name-value pairs
            var pairs = EnumExtensions.GetFormattedNames<T>();

            //Append the values
            int index = -1;
            foreach (var pair in pairs)
            {
                control.Items.Add(new ListItem(pair.Item2, pair.Item1.ToString()));
                if (bHasInit && pair.Item2.Equals(initialValue.Value))
                    index = control.Items.Count - 1;
            };

            if (index >= 0)
                control.SelectedIndex = index;
        }

        #endregion
    }
}
