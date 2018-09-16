/*
 * Copyright © 2005 Michael Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;		//Docs

#endregion

namespace P3Net.Kraken.WinForms.Native
{
    /// <summary>Defines the messages used for combo box controls.</summary>
    [SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue")]
    public enum ComboBoxMessage
    {
        /// <summary>
        /// CB_ADDSTRING.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Unused.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>The string to be added or the item data (if using owner-draw).</description>
        ///		</item>
        /// </list>
        /// <para/>
        /// Returns the zero-based index of the newly added item or -1 if an error occurs.  Returns -2 if there is
        /// insufficient space.
        /// </summary>
        AddString = 0x0143,

        /// <summary>
        /// CB_DELETESTRING.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Zero-based index of string to delete.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Unused.</description>
        ///		</item>
        /// </list>
        /// <para/>
        /// Returns the number of items remaining in the string.  Returns -1 if <i>wParam</i> is
        /// invalid.
        /// </summary>
        DeleteString = 0x0144,
        
        /// <summary>
        /// CB_DIR.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Attributes of the items to be added.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>The path to use for finding the directories or files to add.</description>
        ///		</item>
        /// </list>
        /// <para/>
        /// Returns the zero-based index of the last item added to the list or -1 if an error occurs.  If there
        /// is insufficient space to store the item then -2 is returned.
        /// </summary>
        Dir = 0x0145,

        /// <summary>
        /// CB_FINDSTRING.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Zero-based index of the item preceding the place to begin searching.  -1 for the entire list.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>The string to find.  The search is case insensitive.</description>
        ///		</item>
        /// </list>
        /// <para/>
        /// Returns the zero-based index of the matching item or -1 if no item is found.
        /// </summary>
        FindString = 0x014C,

        /// <summary>
        /// CB_FINDSTRINGEXACT.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Zero-based index of the item preceding the place to begin searching.  -1 for the entire list.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>The string to find.  The search is case insensitive.</description>
        ///		</item>
        /// </list>
        /// <para/>
        /// Returns the zero-based index of the matching item or -1 if no item is found.
        /// </summary>
        FindStringExact = 0x0158,

        /// <summary>
        /// CB_GETCOMBOBOXINFO.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Unused.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Pointer to a <b>COMBOBOXINFO</b> structure to hold the information.</description>
        ///		</item>
        /// </list>
        /// <para/>
        /// Supported in Windows XP and later.
        /// </summary>
        GetComboBoxInfo = 0x0164,

        /// <summary>
        /// CB_GETCOUNT.
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
        /// Returns the number of items in the list box or -1 on error.
        /// </summary>
        GetCount = 0x0146,

        /// <summary>
        /// CB_GETCURSEL.
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
        /// Returns the zero-based index of the currently selected item or -1 if no item is selected.
        /// </summary>
        GetCurSel = 0x0147,

        /// <summary>
        /// CB_GETDROPPEDCONTROLRECT.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Unused.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Pointer to the <b>RECT</b> structure containing the screen coordinates.</description>
        ///		</item>
        /// </list>
        /// </summary>
        GetDroppedControlRect = 0x0152,

        /// <summary>
        /// CB_GETDROPPEDSTATE.
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
        /// Returns <see langword="true"/> if the list box is visible or <see langword="false"/> otherwise.
        /// </summary>
        GetDroppedState = 0x0157,

        /// <summary>
        /// CB_GETDROPPEDWIDTH.
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
        /// Returns the width in pixels or -1 on error.
        /// </summary>
        GetDroppedWidth = 0x015F,

        /// <summary>
        /// CB_GETEDISEL.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Pointer to <see cref="uint"/> that receives the starting position of the selection.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Pointer to <see cref="uint"/> that receives the ending position of the selection.</description>
        ///		</item>
        /// </list>
        /// <para/>
        /// Returns the starting position in the low-order word and the ending position in the high-order word.
        /// </summary>
        GetEditSel = 0x0140,

        /// <summary>
        /// CB_GETEXTENDEDUI.
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
        /// Returns <see langword="true"/> if the control has the extended user interface or <see langword="false"/> otherwise.
        /// </summary>
        GetExtendedUI = 0x0156,

        /// <summary>
        /// CB_GETHORIZONTALEXTENT.
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
        /// Returns the scrollable width in pixels.
        /// </summary>
        GetHorizontalExtent = 0x015D,

        /// <summary>
        /// CB_GETITEMDATA.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>The zero-based index of the item.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Unused.</description>
        ///		</item>
        /// </list>
        /// <para/>
        /// Returns the data associated with the item or -1 if an error occurs.
        /// </summary>
        GetItemData = 0x0150,

        /// <summary>
        /// CB_GETITEMHEIGHT.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>-1 to get height of selection field.  Specify 0 to get the height of the list items for combo boxes
        ///			that do not have <see cref="ComboBox.DrawMode">DrawMode</see> set to <see cref="DrawMode.OwnerDrawVariable"/>.
        ///			Specify the zero-based index of the item to get the height of when <see cref="ComboBox.DrawMode">DrawMode</see> is set
        ///			to <see cref="DrawMode.OwnerDrawVariable"/>.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Unused.</description>
        ///		</item>
        /// </list>
        /// </summary>
        GetItemHeight = 0x0154,

        /// <summary>
        /// CB_GETLBTEXT.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Zero-based index of the item to retrieve.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Pointer to string to receive the item.  The string must be large enough to
        ///			hold the item text.</description>
        ///		</item>
        /// </list>
        /// <para/>
        /// Returns the length of the string or -1 on error.
        /// </summary>
        GetLBText = 0x0148,		 

        /// <summary>
        /// CB_GETLBTEXTLEN.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Zero-based index of the item to retrieve.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Unused.</description>
        ///		</item>
        /// </list>
        /// <para/>
        /// Returns the length of the string or -1 on error.
        /// </summary>
        GetLBTextLen = 0x0149,		 

        /// <summary>
        /// CB_GETLOCALE.
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
        /// Returns the current locale.  The country/region code is in the high-order word and the
        /// language identifier is in the low-order word.
        /// </summary>
        GetLocale = 0x015A,		 

        /// <summary>
        /// CB_GETMINVISIBLE.
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
        /// The minimum number of visible items.
        /// <para/>
        /// Supported under Windows XP and later.  Requires v6.0 of ComCtl32.
        /// </summary>
        GetMinVisible = 0x1702,		 

        /// <summary>
        /// CB_GETTOPINDEX.
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
        /// Returns the index of the first visible item in the list box.
        /// </summary>
        GetTopIndex = 0x015B,		 

        /// <summary>
        /// CB_INITSTORAGE.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>The number of items to add.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>The amount of memory to allocate for the items, in bytes.</description>
        ///		</item>
        /// </list>
        /// <para/>
        /// Returns the amount of memory allocated for all the items or -2 if the request fails.
        /// </summary>
        InitStorage = 0x0161,		 

        /// <summary>
        /// CB_INSERTSTRING.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>The zero-based index identifying where to insert the item.  -1 to insert at the end of the list.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>The string to be inserted.</description>
        ///		</item>
        /// </list>
        /// <para/>
        /// Returns the zero-based index where the item was inserted or -1 on error.  Returns -2 if there is not enough
        /// space.
        /// </summary>
        InsertString = 0x014A,		 

        /// <summary>
        /// CB_LIMITTEXT.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Specifies the number of characters that can be entered.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Unused.</description>
        ///		</item>
        /// </list>
        /// </summary>
        LimitText = 0x0141,		 

        /// <summary>
        /// CB_RESETCONTENT.
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
        ResetContent = 0x014B,		 

        /// <summary>
        /// CB_SELECTSTRING.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Zero-based index of the item preceding the location to begin searching.  Specify -1
        ///			to search the entire list.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>The string to find.  The case is insensitive.</description>
        ///		</item>
        /// </list>
        /// <para/>
        /// Returns the zero-based index of the selected item or -1 if the item is not found.
        /// </summary>
        SelectString = 0x014D,		
 
        /// <summary>
        /// CB_SETCURSEL.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>The zero-based index of the string to select.  Specify -1 to remove the current selection.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Unused.</description>
        ///		</item>
        /// </list>
        /// <para/>
        /// Returns the index of the selected item or -1 on error.
        /// </summary>
        SetCurSel = 0x014E,		
 
        /// <summary>
        /// CB_SETDROPPEDWIDTH.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>The width of the list box, in pixels.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Unused.</description>
        ///		</item>
        /// </list>
        /// <para/>
        /// Returns the new width.
        /// </summary>
        SetDroppedWidth = 0x0160,		
 
        /// <summary>
        /// CB_SETEDITSEL.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Unused.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>The low-order word is the starting position.  Specify -1 to remove selection.  The
        ///			high-order word specifies the ending position.  Specify -1 to select all items from the starting
        ///			position to the end of the edit box.</description>
        ///		</item>
        /// </list>
        /// </summary>
        SetEditSel = 0x0142,		 

        /// <summary>
        /// CB_SETEXTENDEDUI.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption><see langword="true"/> to use the extended UI or <see langword="false"/> otherwise.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Unused.</description>
        ///		</item>
        /// </list>
        /// </summary>
        SetExtendedUI = 0x0155,		
 
        /// <summary>
        /// CB_SETHORIZONTALEXTENT.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>The scrollable width of the list box, in pixels.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Unused.</description>
        ///		</item>
        /// </list>
        /// </summary>
        SetHorizontalExtent = 0x015E,		
 
        /// <summary>
        /// CB_SETITEMDATA.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Zero-based index of the item.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>The data to associate with the item.</description>
        ///		</item>
        /// </list>
        /// </summary>
        SetItemData = 0x0151,	
     
        /// <summary>
        /// CB_SETITEMHEIGHT.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>-1 to set the height of the selection field.  0 to set the list items height for 
        ///			controls with <see cref="ComboBox.DrawMode">DrawMode</see> set to <see cref="DrawMode.OwnerDrawVariable"/>.  The
        ///			zero-based index of the specified item otherwise.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>The height, in pixels.</description>
        ///		</item>
        /// </list>
        /// </summary>
        SetItemHeight = 0x153,		 

        /// <summary>
        /// CB_SETLOCALE.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>The locale to use.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Unused.</description>
        ///		</item>
        /// </list>
        /// <para/>
        /// Returns the previous locale.  Returns -1 if <i>wParam</i> is invalid or not installed.
        /// </summary>
        SetLocale = 0x159,		 

        /// <summary>
        /// CB_SETMINVISIBLE.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>The minimum number of visible items.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Unused.</description>
        ///		</item>
        /// </list>
        /// <para/>
        /// Supported under Windows XP and later.  Requires v6.0 of ComCtl32.
        /// </summary>
        SetMinVisible = 0x1701,		 

        /// <summary>
        /// CB_SETTOPINDEX.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>The zero-based index of the item.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Unused.</description>
        ///		</item>
        /// </list>
        /// </summary>
        SetTopIndex = 0x15C,		 

        /// <summary>
        /// CB_SHOWDROPDOWN.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption><see langword="true"/> to show the list box or <see langword="false"/> to hide it.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Unused.</description>
        ///		</item>
        /// </list>
        /// </summary>
        ShowDropDown = 0x14F,		 
    }
}
