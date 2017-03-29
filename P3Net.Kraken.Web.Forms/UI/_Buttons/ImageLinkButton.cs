/*
 * Copyright © Michael L Taylor
 * All Rights Reserved
 * 
 * $History: $
 */
#region Imports

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;

using P3Net.Kraken;
#endregion

namespace P3Net.Kraken.Web.UI
{
	/// <summary>Provides a link button that can display an image, text or both.</summary>
	/// <preliminary />
	[CodeNotAnalyzed]
	[CodeNotTested]
	[ToolboxData("<{0}:ImageLinkButton runat=\"server\"></{0}:ImageLinkButton>")]
	public class ImageLinkButton : LinkButton
	{
		#region Public Members

		#region Attributes

		/// <summary>Gets or sets the mode used to display the link.</summary>
		[Category("Appearance")]
		[Description("The mode used to display the link.")]
		[DefaultValue(0)]
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

		/// <summary>Gets or sets the image to be displayed.</summary>
		[Category("Appearance")]
		[Description("The image to display.")]
		[DefaultValue("")]
		[Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
		public string ImageUrl
		{
			[DebuggerStepThrough]
			get
			{
				string obj = ViewState["ImageUrl"] as string;
				return (obj != null) ? obj : "";
			}

			[DebuggerStepThrough]
			set { ViewState["ImageUrl"] = value; }
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

		/// <summary>Overridden to render the control contents.</summary>
		/// <param name="writer">The writer to use.</param>
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

				//Render a space as needed
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
				img.EnableViewState = false;

				img.RenderControl(writer);
			};

			//If displaying the text and align is right
			if (bDisplayText && (align == TextAlign.Right))
			{
				//Render a space as needed
				if (bDisplayImage)
					writer.Write("&nbsp;");

				writer.Write(this.Text);
			};
		}
		#endregion

		#endregion //Protected Members
	}
}
