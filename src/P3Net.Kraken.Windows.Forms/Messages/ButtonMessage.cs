/*
 * Copyright © 2005 Michael Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.Diagnostics.CodeAnalysis;
#endregion

namespace P3Net.Kraken.WinForms.Native
{
    /// <summary>Defines the different messages for a button control.</summary>
    [SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue")]
    public enum ButtonMessage
    {
        /// <summary>
        /// BCM_GETIDEALSIZE.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Unused.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Pointer to SIZE structure to receive ideal size.</description>
        ///		</item>
        /// </list>
        /// <para/>
        /// Supported by Windows XP and later.  Requires v6 of CommClt32.dll.
        /// </summary>
        GetIdealSize = 0x1601,

        /// <summary>
        /// BCM_GETIMAGELIST. 
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Unused.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Pointer to a BUTTON_IMAGELIST structure.</description>
        ///		</item>
        /// </list>
        /// <para/>
        /// Supported by Windows XP and later.  Requires v6 of CommClt32.dll.
        /// </summary>
        GetImageList = 0x1603,

        /// <summary>
        /// BCM_GETTEXTMARGIN.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Unused.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Pointer to a RECT containing the margins to use for drawing.</description>
        ///		</item>
        /// </list>
        /// <para/>
        /// Supported by Windows XP and later.  Requires v6 of CommClt32.dll.
        /// </summary>
        GetTextMargin = 0x1605,

        /// <summary>
        /// BCM_SETIMAGELIST.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Unused.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Pointer to a BUTTON_IMAGELIST containing the image list.</description>
        ///		</item>
        /// </list>
        /// <para/>
        /// Supported by Windows XP and later.  Requires v6 of CommClt32.dll.
        /// </summary>
        SetImageList = 0x1602,

        /// <summary>	
        /// BCM_SETTEXTMARGIN.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Unused.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Pointer to RECT containing margins to use for drawing.</description>
        ///		</item>
        /// </list>
        /// <para/>
        /// Supported by Windows XP and later.  Requires v6 of CommClt32.dll.
        /// </summary>
        SetTextMargin = 0x1604,

        /// <summary>
        /// BM_CLICK.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Unused.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Unused.</description>
        ///		</item>
        /// </list>
        /// </summary>
        Click = 0x00F5,

        /// <summary>
        /// BM_GETCHECK.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Unused.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Unused.</description>
        ///		</item>
        /// </list>
        /// <para/>
        /// Returns one of the following.
        /// <list type="table">
        ///		<item>
        ///			<term>Value</term>
        ///			<description>Meaning</description>
        ///		</item>
        ///		<item>
        ///			<term>0</term>
        ///			<description>Button is unchecked.</description>
        ///		</item>
        ///		<item>
        ///			<term>1</term>
        ///			<description>Button is checked.</description>
        ///		</item>
        ///		<item>
        ///			<term>2</term>
        ///			<description>Button is indeterminate.</description>
        ///		</item>		
        /// </list>
        /// </summary>
        GetCheck = 0x00F0,

        /// <summary>
        /// BM_GETIMAGE.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>The type of the image.  0 for bitmap, 1 for icon.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Unused.</description>
        ///		</item>
        /// </list>
        /// <para/>
        /// Returns the handle to the image.
        /// </summary>
        GetImage = 0x00F6,

        /// <summary>
        /// BM_GETSTATE.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Unused.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Unused.</description>
        ///		</item>
        /// </list>
        /// <para/>
        /// Refer to <b>BM_GETSTATE</b> for return values.
        /// </summary>
        GetState = 0x00F2,

        /// <summary>
        /// BM_SETCHECK.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>The new check state.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Unused.</description>
        ///		</item>
        /// </list>
        /// </summary>
        SetCheck = 0x00F1,

        /// <summary>
        /// BM_SETIMAGE.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Specifies the type of the image.  0 for bitmap, 1 for icon.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Handle to the new image.</description>
        ///		</item>
        /// </list>
        /// <para/>
        /// Returns the handle to the previous image, if any.
        /// </summary>
        SetImage = 0x00F7,

        /// <summary>
        /// BM_SETSTATE.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption><see langword="true"/> to highlight the word.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Unused.</description>
        ///		</item>
        /// </list>
        /// </summary>
        SetState = 0x00F3,

        /// <summary>
        /// BM_SETSTYLE.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Contains the new button style.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Contains the redraw flag.</description>
        ///		</item>
        /// </list>
        /// </summary>
        SetStyle = 0x00F4,
    }
}
