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
	/// <summary>A <see cref="HyperLink"/> that can display both an image and text.</summary>
	/// <preliminary/>
	[CodeNotAnalyzed]
	[CodeNotTested]
	[ToolboxData("<{0}:ImageLink runat=\"server\"></{0}:ImageLink>")]
	public class ImageLink : HyperLink
	{
		#region Public Members

		#region Attributes

		/// <summary>Gets or sets the mode used to display the link.</summary>
		[Category("Appearance")]
		[Description("The mode used to display the link.")]
		[DefaultValue(typeof(ImageLinkDisplayMode), "ImageAndText")]
		public ImageLinkDisplayMode DisplayMode
		{
			[DebuggerStepThrough]
			get 
			{
				object obj = ViewState["DisplayMode"];
				return (obj != null) ? (ImageLinkDisplayMode)obj : ImageLinkDisplayMode.ImageAndText;
			}

			[DebuggerStepThrough]
			set
			{
				ViewState["DisplayMode"] = value;
			}
		}

		/// <summary>Gets or sets the alignment of the text.</summary>
		[Category("Appearance")]
		[Description("The alignment of the text.")]
		[DefaultValue(typeof(TextAlign), "Right")]
		public TextAlign TextAlign
		{
			[DebuggerStepThrough]
			get
			{
				object obj = ViewState["TextAlign"];
				return (obj != null) ? (TextAlign)obj : TextAlign.Right;
			}

			[DebuggerStepThrough]
			set { ViewState["TextAlign"] = value; }
		}
		#endregion

		#endregion //Public Members

		#region Protected Members

		#region Methods

		/// <summary>Renders the control contents.</summary>
		/// <param name="writer">The writer to render to.</param>
		protected override void RenderContents ( HtmlTextWriter writer )
		{
			//Override the default hyperlink behavior by generating an image
			//only if the mode is set 			
			bool bDisplayImage = false, bDisplayText = false;
			switch (DisplayMode)
			{
				case ImageLinkDisplayMode.Image: bDisplayImage = true; break;
				case ImageLinkDisplayMode.ImageAndText: bDisplayImage = bDisplayText = true; break;
				case ImageLinkDisplayMode.Text: bDisplayText = true; break;
			};
			TextAlign align = TextAlign;

			//If displaying the text and text align is left
			if (bDisplayText && (align == TextAlign.Left))
			{
				//Write out the text
				writer.Write(this.Text);

				//Insert a space
				if (bDisplayImage)
					writer.Write("&nbsp;");
			};

			//If displaying the image then write it out now
			if (bDisplayImage && (ImageUrl.Length > 0))
			{
				//Render the image
				Image img = new Image();
				img.ImageUrl = this.ResolveClientUrl(ImageUrl);
				string text = (ToolTip.Length > 0) ? ToolTip : Text;
				img.ToolTip = img.AlternateText = text;
				img.Visible = this.Visible;
				img.EnableViewState = false;

				img.RenderControl(writer);
			};

			//If displaying the text and align is right
			if (bDisplayText && (align == TextAlign.Right))
			{
				//Insert a space
				if (bDisplayImage)
					writer.Write("&nbsp;");

				writer.Write(this.Text);
			};
		}
		#endregion 

		#endregion //Protected Members
	}

	/// <summary>Specifies the modes used to render an <see cref="ImageLink"/>.</summary>
	/// <preliminary/>
	public enum ImageLinkDisplayMode
	{
		/// <summary>Displays both the image and the text.</summary>
		ImageAndText = 0,

		/// <summary>Displays the image only.</summary>
		Image = 1,

		/// <summary>Displays the text only.</summary>
		Text = 2,
	}
}
