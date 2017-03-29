/*
 * Copyright © 2007 Michael L Taylor
 * All Rights Reserved
 * 
 * $Header: $
 */
#region Imports

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

namespace P3Net.Kraken.Web.UI
{
	/// <summary>An <see cref="ImageButton"/> that displays a client-side confirmation message.</summary>
	/// <remarks>
	/// At this time the <see cref="ImageButton.OnClientClick">OnClientClick</see> property is ignored.
	/// <para/>
	/// The <b>ConfirmButton</b> available in the AJAX Toolkit provides more advanced functionality.  Use
	/// that control instead when using AJAX in a site.
	/// </remarks>
	/// <preliminary/>	
	[CodeNotAnalyzed]
	[CodeNotTested]
	[ToolboxData("<{0}:ConfirmImageButton runat=\"server\"></{0}:ConfirmImageButton>")]
	public class ConfirmImageButton : ImageButton
	{
		#region Public Members

		#region Attributes

		/// <summary>Gets or sets the confirmation message to display when the button is clicked.</summary>
		[Category("Appearance")]
		[Description("The message to display prior to posting back.")]
		[DefaultValue("Are you sure?")]
		public string ConfirmMessage
		{
			[DebuggerStepThrough]
			get
			{
				object obj = ViewState["ConfirmMessage"];
				return (obj != null) ? (string)obj : "Are you sure?";
			}

			[DebuggerStepThrough]
			set
			{
				value = (value != null) ? value.Trim() : "";
				if (value.Length > 0)
					ViewState["ConfirmMessage"] = value;
				else
					ViewState.Remove("ConfirmMessage");
			}
		}
		#endregion

		#endregion  //Public Members

		#region Protected Members

		#region Methods

		/// <summary>Prepares the control for rendering.</summary>
		/// <param name="e">The event arguments.</param>
		protected override void OnPreRender ( EventArgs e )
		{
			base.OnPreRender(e);

			//Add the confirmation attribute
			if ((ConfirmMessage.Length > 0) && (Page != null) && Visible)
				Attributes.Add("onclick", "return confirm('" + Page.Server.HtmlEncode(ConfirmMessage) + "');");
		}
		#endregion

		#endregion //Protected Members
	}
}
