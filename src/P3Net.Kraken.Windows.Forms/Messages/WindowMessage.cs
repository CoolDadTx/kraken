/*
 * Defines the different system messages available for native calls.
 *
 * Copyright © 2005 Michael L Taylor ($COMPANY$)
 * All Rights Reserved
 * 
 * $Header: /DotNET/Kraken/Source/P3Net.Kraken.UI/Messages/WindowMessages.cs 2     11/04/05 8:22a Michael $
 */
#region Imports

using System;
using System.Diagnostics.CodeAnalysis;
#endregion

namespace P3Net.Kraken.WinForms.Native
{
	/// <summary>Defines the different WM_ messages defined by Windows.</summary>	
	public enum WindowMessage
	{
		/// <summary>WM_NULL.</summary>
		Null	= 0x0000,

		/// <summary>WM_CREATE.</summary>
		Create = 0x0001,

		/// <summary>WM_DESTROY.</summary>
		Destroy = 0x0002,

		/// <summary>WM_MOVE.</summary>
		Move = 0x0003,

		/// <summary>WM_SIZE.</summary>
		Size = 0x0005,

		/// <summary>WM_ACTIVATE.</summary>
		Activate = 0x0006,

		/// <summary>WM_SETFOCUS.</summary>
		SetFocus = 0x007,

		/// <summary>WM_KILLFOCUS.</summary>
		KillFocus = 0x0008,

		/// <summary>WM_ENABLE.</summary>
		Enable = 0x000A,

		/// <summary>WM_SETREDRAW.</summary>
		SetRedraw = 0x000B,

		/// <summary>WM_SETTEXT.</summary>
		SetText = 0x000C,

		/// <summary>WM_GETTEXT.</summary>
		GetText = 0x000D,

		/// <summary>WM_GETTEXTLENGTH.</summary>
		GetTextLength = 0x000E,

		/// <summary>WM_PAINT.</summary>
		Paint = 0x000F,

		/// <summary>WM_CLOSE.</summary>
		Close = 0x0010,

		/// <summary>WM_QUERYENDSESSION.
		/// <para/>
		/// Not supported on Windows CE.
		/// </summary>
		QueryEndSession = 0x0011,

		/// <summary>WM_QUIT.</summary>
		Quit = 0x0012,

		/// <summary>WM_QUERYOPEN.
		/// <para/>
		/// Not supported on Windows CE.
		/// </summary>
		QueryOpen = 0x0013,

		/// <summary>WM_ERASEBKGND.</summary>
		EraseBkgnd = 0x0014,

		/// <summary>WM_SYSCOLORCHANGE.</summary>
		SysColorChange = 0x0015,

		/// <summary>WM_ENDSESSION.
		/// <para/>
		/// Not supported on Windows CE.
		/// </summary>
		EndSession = 0x0016,

		/// <summary>WM_SHOWWINDOW.</summary>
		ShowWindow = 0x0018,

		/// <summary>WM_SETTINCHANGE.  WM_WININICHANGE prior to Windows 95.</summary>
		SettingChange = 0x001A,		
		
		/// <summary>WM_DEVMODECHANGE.</summary>
		DevModeChange = 0x001B,

		/// <summary>WM_ACTIVEAPP.</summary>
		ActiveApp = 0x001C,

		/// <summary>WM_FONTCHANGE.</summary>
		FontChange = 0x001D,

		/// <summary>WM_TIMECHANGE.</summary>
		TimeChange = 0x001E,

		/// <summary>WM_CANCELMODE.</summary>
		CancelMode = 0x001F,

		/// <summary>WM_SETCURSOR.</summary>
		SetCursor = 0x0020,

		/// <summary>WM_MOUSEACTIVATE.</summary>
		MouseActivate = 0x0021,

		/// <summary>WM_CHILDACTIVATE.</summary>
		ChildActivate = 0x0022,

		/// <summary>WM_QUEUESYNC.</summary>
		QueueSync = 0x0023,

		/// <summary>WM_GETMINMAXINFO.</summary>
		GetMinMaxInfo = 0x0024,

		/// <summary>WM_PAINTICON.</summary>
		PaintIcon = 0x0026,

		/// <summary>WM_ICONERASEBKGND.</summary>
		IconEraseBkgnd = 0x0027,

		/// <summary>WM_NEXTDLGCTL.</summary>
		NextDlgCtl = 0x0028,
		
		/// <summary>WM_SPOOLERSTATUS.</summary>
		SpoolerStatus = 0x002A,

		/// <summary>WM_DRAWITEM.</summary>
		DrawItem = 0x002B,

		/// <summary>WM_MEASUREITEM.</summary>
		MeasureItem = 0x002C,

		/// <summary>WM_DELETEITEM.</summary>
		DeleteItem = 0x002D,

		/// <summary>WM_VKEYTOITEM.</summary>
		VKeyToItem = 0x002E,

		/// <summary>WM_CHARTOITEM.</summary>
		CharToItem = 0x002F,

		/// <summary>WM_SETFONT.</summary>
		SetFont = 0x0030,

		/// <summary>WM_GETFONT.</summary>
		GetFont = 0x0031,

		/// <summary>WM_SETHOTKEY.</summary>
		SetHotKey = 0x0032,

		/// <summary>WM_GETHOTKEY.</summary>
		GetHotKey = 0x0033,

		/// <summary>WM_QUERYDRAGICON.</summary>
		QueryDragIcon = 0x0037,

		/// <summary>WM_COMPAREITEM.</summary>
		CompareItem = 0x0039,

		/// <summary>WM_GETOBJECT.
		/// <para/>
		/// Supported on Windows ME and Windows 2000 or later.
		/// </summary>
		GetObject = 0x0003D,

		/// <summary>WM_COMPACTING.</summary>
		Compacting = 0x0041,

		/// <summary>WM_WINDOWPOSCHANGING.</summary>
		WindowPosChanging = 0x0046,

		/// <summary>WM_WINDOWPOSCHANGED.</summary>
		WindowPosChanged = 0x0047,

		/// <summary>WM_POWER.</summary>
		Power = 0x0048,

		/// <summary>WM_COPYDATA.</summary>
		CopyData = 0x004A,

		/// <summary>WM_CANCELJOURNAL.</summary>
		CancelJournal = 0x004B,

		/// <summary>WM_NOTIFY.</summary>
		Notify = 0x004E,

		/// <summary>WM_INPUTLANGCHANGEREQUEST.</summary>
		InputLangChangeRequest = 0x0050,

		/// <summary>WM_INPUTLANGCHANGE.</summary>
		InputLangChange = 0x0051,
				
		/// <summary>WM_TCARD.</summary>
		TCard = 0x0052,

		/// <summary>WM_HELP.</summary>
		Help = 0x0053,

		/// <summary>WM_USERCHANGED.</summary>
		UserChanged = 0x0054,

		/// <summary>WM_NOTIFYFORMAT.</summary>
		NotifyFormat = 0x0055,

		/// <summary>WM_CONTEXTMENU.</summary>
		ContextMenu = 0x007B,

		/// <summary>WM_STYLECHANGING.</summary>
		StyleChanging = 0x007C,

		/// <summary>WM_STYLECHANGED.</summary>
		StyleChanged = 0x007D,

		/// <summary>WM_DISPLAYCHANGE.</summary>
		DisplayChange = 0x007E,

		/// <summary>WM_GETICON.</summary>
		GetIcon = 0x007F,

		/// <summary>WM_SETICON.</summary>
		SetIcon = 0x0080,

		/// <summary>WM_NCCREATE.</summary>
		NcCreate = 0x0081,

		/// <summary>WM_NCDESTROY.</summary>
		NcDestroy = 0x0082,

		/// <summary>WM_NCCALCSIZE.</summary>
		NcCalcSize = 0x0083,

		/// <summary>WM_NCHITTEST.</summary>
		NcHitTest = 0x0084,

		/// <summary>WM_NCPAINT.</summary>
		NcPaint = 0x0085,

		/// <summary>WM_NCACTIVATE.</summary>
		NcActivate = 0x0086,

		/// <summary>WM_GETDLGCODE.</summary>
		GetDlgCode = 0x0087,

		/// <summary>WM_SYNCPAINT.
		/// <para/>
		/// Not supported on Windows CE.
		/// </summary>
		SyncPaint = 0x0088,

		/// <summary>WM_NCMOUSEMOVE.</summary>
		NcMouseMove = 0x00A0,

		/// <summary>WM_NCLBUTTONDOWN.</summary>
		NcLButtonDown = 0x00A1,

		/// <summary>WM_NCLBUTTONUP.</summary>
		NcLButtonUp = 0x00A2,

		/// <summary>WM_NCLBUTTONDBLCLK.</summary>
		NcLButtonDblClk = 0x00A3,

		/// <summary>WM_NCRBUTTONDOWN.</summary>
		NcRButtonDown = 0x00A4,

		/// <summary>WM_NCRBUTTONUP.</summary>
		NcRButtonUp = 0x00A5,

		/// <summary>WM_NCRBUTTONDBLCLK.</summary>
		NcRButtonDblClk = 0x00A6,

		/// <summary>WM_NCMBUTTONDOWN.</summary>
		NcMButtonDown = 0x00A7,

		/// <summary>WM_NCMBUTTONUP.</summary>
		NcMButtonUp = 0x00A8,

		/// <summary>WM_NCMBUTTONDBLCLK.</summary>
		NcMButtonDblClk = 0x00A9,

		/// <summary>WM_NCXBUTTONDOWN.
		/// <para/>
		/// Supported on Windows 2000 or later.
		/// </summary>
		NcXButtonDown = 0x00AB,

		/// <summary>WM_NCXBUTTONUP.
		/// <para/>
		/// Supported on Windows 2000 or later.
		/// </summary>
		NcXButtonUp = 0x00AC,

		/// <summary>WM_NCXBUTTONDBLCLK.
		/// <para/>
		/// Supported on Windows 2000 or later.
		/// </summary>
		[SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member")]
		NcXButtonDblClk = 0x00AD,

		/// <summary>WM_INPUT.
		/// <para/>
		/// Supported on Windows XP or later.
		/// </summary>
		Input = 0x00FF,

		/// <summary>WM_KEYDOWN.</summary>
		KeyDown = 0x0100,

		/// <summary>WM_KEYUP.</summary>
		KeyUp = 0x0101,

		/// <summary>WM_CHAR.</summary>
		Char = 0x0102,

		/// <summary>WM_DEADCHAR.</summary>
		DeadChar = 0x0103,

		/// <summary>WM_SYSKEYDOWN.</summary>
		SysKeyDown = 0x0104,

		/// <summary>WM_SYSKEYUP.</summary>
		SysKeyUp = 0x0105,

		/// <summary>WM_SYSCHAR.</summary>
		SysChar = 0x0106,
		
		/// <summary>WM_SYSDEADCHAR.</summary>
		SysDeadChar = 0x0107,

		/// <summary>WM_UNICHAR.
		/// <para/>
		/// Supported on Windows XP or later.
		/// </summary>
		UniChar = 0x0109,

		/// <summary>WM_IME_STARTCOMPOSITION.</summary>
		ImeStartComposition = 0x010D,

		/// <summary>WM_IME_ENDCOMPOSITION.</summary>
		ImeEndComposition = 0x010E,

		/// <summary>WM_IME_COMPOSITION.</summary>
		ImeComposition = 0x010F,

		/// <summary>WM_INITDIALOG.</summary>
		InitDialog = 0x0110,

		/// <summary>WM_COMMAND.</summary>
		Command = 0x0111,

		/// <summary>WM_SYSCOMMAND.</summary>
		SysCommand = 0x0112,

		/// <summary>WM_TIMER.</summary>
		Timer = 0x0113,

		/// <summary>WM_HSCROLL.</summary>
		HScroll = 0x0114,
		
		/// <summary>WM_VSCROLL.</summary>
		VScroll = 0x0115,

		/// <summary>WM_INITMENU.</summary>
		InitMenu = 0x0116,

		/// <summary>WM_INITMENUPOPUP.</summary>
		InitMenuPopup = 0x0117,

		/// <summary>WM_MENUSELECT.</summary>
		MenuSelect = 0x011F,

		/// <summary>WM_MENUCHAR.</summary>
		MenuChar = 0x0120,

		/// <summary>WM_ENTERIDLE.</summary>
		EnterIdle = 0x0121,

		/// <summary>WM_MENURBUTTONUP.
		/// <para/>
		/// Supported on Windows ME and Windows 2000 or later.
		/// </summary>
		MenuRButtonUp = 0x0122,

		/// <summary>WM_MENUDRAG.
		/// <para/>
		/// Supported on Windows ME and Windows 2000 or later.
		/// </summary>
		MenuDrag = 0x0123,

		/// <summary>WM_MENUGETOBJECT.
		/// <para/>
		/// Supported on Windows ME and Windows 2000 or later.
		/// </summary>
		MenuGetObject = 0x0124,

		/// <summary>WM_UNINITMENUPOPUP.
		/// <para/>
		/// Supported on Windows ME and Windows 2000 or later.
		/// </summary>
		UninitMenuPopup = 0x0125,

		/// <summary>WM_MENUCOMMAND.
		/// <para/>
		/// Supported on Windows ME and Windows 2000 or later.
		/// </summary>
		MenuCommand = 0x0126,

		/// <summary>WM_CHANGEUISTATE.
		/// <para/>
		/// Supported on Windows 2000 or later.
		/// </summary>
		ChangeUIState = 0x0127,

		/// <summary>WM_UPDATEUISTATE.
		/// <para/>
		/// Supported on Windows 2000 or later.
		/// </summary>
		UpdateUIState = 0x0128,

		/// <summary>WM_QUERYUISTATE.
		/// <para/>
		/// Supported on Windows 2000 or later.
		/// </summary>
		QueryUIState = 0x0129,

		/// <summary>WM_CTLCOLORMSGBOX.</summary>
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ctl")]
		CtlColorMsgBox = 0x0132,

		/// <summary>WM_CTLCOLOREDIT.</summary>
		CtlColorEdit = 0x0133,

		/// <summary>WM_CTLCOLORLISTBOX.</summary>
		CtlColorListBox = 0x0134,

		/// <summary>WM_CTLCOLORBTN.</summary>
		CtlColorBtn = 0x0135,

		/// <summary>WM_CTLCOLORDLG.</summary>
		CtlColorDlg = 0x0136,

		/// <summary>WM_CTLCOLORSCROLLBAR.</summary>
		CtlColorScrollBar = 0x0137,

		/// <summary>WM_CTLCOLORSTATIC.</summary>
		CtlColorStatic = 0x0138,

		/// <summary>WM_GETHMENU.</summary>
		GetHMenu = 0x01E1,

		/// <summary>WM_MOUSEMOVE.</summary>
		MouseMove = 0x0200,

		/// <summary>WM_LBUTTONDOWN.</summary>
		LButtonDown = 0x0201,

		/// <summary>WM_LBUTTONUP.</summary>
		LButtonUp = 0x0202,

		/// <summary>WM_LBUTTONDBLCLK.</summary>
		LButtonDblClk = 0x0203,

		/// <summary>WM_RBUTTONDOWN.</summary>
		RButtonDown = 0x0204,

		/// <summary>WM_RBUTTONUP.</summary>
		RButtonUp = 0x0205,

		/// <summary>WM_RBUTTONDBLCLK.</summary>
		RButtonDblClk = 0x0206,

		/// <summary>WM_MBUTTONDOWN.</summary>
		MButtonDown = 0x0207,

		/// <summary>WM_MBUTTONUP.</summary>
		MButtonUp = 0x0208,

		/// <summary>WM_MBUTTONDBLCLK.</summary>
		MButtonDblClk = 0x0209,

		/// <summary>WM_MOUSEWHEEL.</summary>
		MouseWheel = 0x020A,

		/// <summary>WM_XBUTTONDOWN.
		/// <para/>
		/// Supported on Windows 2000 or later.
		/// </summary>
		XButtonDown = 0x020B,

		/// <summary>WM_XBUTTONUP.
		/// <para/>
		/// Supported on Windows 2000 or later.
		/// </summary>
		XButtonUp = 0x020C,

		/// <summary>WM_XBUTTONDBLCLK.
		/// <para/>
		/// Supported on Windows 2000 or later.
		/// </summary>
		XButtonDblClk = 0x020D,

		/// <summary>WM_PARENTNOTIFY.</summary>
		ParentNotify = 0x0210,

		/// <summary>WM_ENTERMENULOOP.</summary>
		EnterMenuLoop = 0x0211,

		/// <summary>WM_EXITMENULOOP.</summary>
		ExitMenuLoop = 0x0212,

		/// <summary>WM_NEXTMENU.</summary>
		NextMenu = 0x0213,

		/// <summary>WM_SIZING.</summary>
		Sizing = 0x0214,

		/// <summary>WM_CAPTURECHANGED.</summary>
		CaptureChanged = 0x0215,

		/// <summary>WM_MOVING.</summary>
		Moving = 0x0216,

		/// <summary>WM_POWERBROADCAST.</summary>
		PowerBroadcast = 0x0218,

		/// <summary>WM_DEVICECHANGE.</summary>
		DeviceChange = 0x0219,

		/// <summary>WM_MDICREATE.</summary>
		MdiCreate = 0x0220,

		/// <summary>WM_MDIDESTROY.</summary>
		MdiDestroy = 0x0221,

		/// <summary>WM_MDIACTIVATE.</summary>
		MdiActivate = 0x0222,

		/// <summary>WM_MDIRESTORE.</summary>
		MdiRestore = 0x0223,

		/// <summary>WM_MDINEXT.</summary>
		MdiNext = 0x0224,

		/// <summary>WM_MDIMAXIMIZE.</summary>
		MdiMaximize = 0x0225,

		/// <summary>WM_MDITILE.</summary>
		MdiTile = 0x0226,

		/// <summary>WM_MDICASCADE.</summary>
		MdiCascade = 0x0227,

		/// <summary>WM_MDIICONARRANGE.</summary>
		MdiIconArrange = 0x0228,

		/// <summary>WM_MDIGETACTIVE.</summary>
		MdiGetActive = 0x0229,

		/// <summary>WM_MDISETMENU.</summary>
		MdiSetMenu = 0x0230,

		/// <summary>WM_ENTERSIZEMOVE.</summary>
		EnterSizeMove = 0x0231,

		/// <summary>WM_EXITSIZEMOVE.</summary>
		ExitSizeMove = 0x0232,

		/// <summary>WM_DROPFILES.</summary>
		DropFiles = 0x0233,

		/// <summary>WM_MDIREFRESHMENU.</summary>
		MdiRefreshMenu = 0x0234,

		/// <summary>WM_IME_SETCONTEXT.</summary>
		ImeSetContext = 0x0281,

		/// <summary>WM_IME_NOTIFY.</summary>
		ImeNotify = 0x0282,

		/// <summary>WM_IME_CONTROL.</summary>
		ImeControl = 0x0283,

		/// <summary>WM_IME_COMPOSITIONFULL.</summary>
		ImeCompositionFull = 0x0284,

		/// <summary>WM_IME_SELECT.</summary>
		ImeSelect = 0x0285,

		/// <summary>WM_IME_CHAR.</summary>
		ImeChar = 0x0286,

		/// <summary>WM_IME_REQUEST.
		/// <para/>
		/// Supported on Windows ME and Windows 2000 or later.
		/// </summary>
		ImeRequest = 0x0288,

		/// <summary>WM_IME_KEYDOWN.</summary>
		ImeKeyDown = 0x0290,

		/// <summary>WM_IME_KEYUP.</summary>
		ImeKeyUp = 0x0291,

		/// <summary>WM_NCMOUSEHOVER.
		/// <para/>
		/// Supported on Windows ME and Windows 2000 or later.
		/// </summary>
		NcMouseHover = 0x02A0,

		/// <summary>WM_MOUSEHOVER.
		/// <para/>
		/// Supported on Windows ME and Windows NT 4.0 or later.
		/// </summary>
		MouseHover = 0x02A1,

		/// <summary>WM_NCMOUSELEAVE.
		/// <para/>
		/// Supported on Windows ME and Windows 2000 or later.
		/// </summary>
		NcMouseLeave = 0x02A2,

		/// <summary>WM_MOUSELEAVE.
		/// <para/>
		/// Supported on Windows ME and Windows NT 4.0 or later.
		/// </summary>
		MouseLeave = 0x02A3,
			
		/// <summary>WM_WTSSESSION_CHANGE.
		/// <para/>
		/// Supported on Windows XP or later.
		/// </summary>
		WtsSessionChange= 0x02B1,

		/// <summary>WM_CUT.</summary>
		Cut = 0x0300,

		/// <summary>WM_COPY.</summary>
		Copy = 0x0301,

		/// <summary>WM_PASTE.</summary>
		Paste = 0x0302,

		/// <summary>WM_CLEAR.</summary>
		Clear = 0x0303,

		/// <summary>WM_UNDO.</summary>
		Undo = 0x0304,

		/// <summary>WM_RENDERFORMAT.</summary>
		RenderFormat = 0x0305,

		/// <summary>WM_RENDERALLFORMATS.</summary>
		RenderAllFormats = 0x0306,

		/// <summary>WM_DESTROYCLIPBOARD.</summary>
		DestroyClipboard = 0x0307,

		/// <summary>WM_DRAWCLIPBOARD.</summary>
		DrawClipboard = 0x0308,

		/// <summary>WM_PAINTCLIPBOARD.</summary>
		PaintClipboard = 0x0309,

		/// <summary>WM_VSCROLLCLIPBOARD.</summary>
		VScrollClipboard = 0x030A,

		/// <summary>WM_SIZECLIPBOARD.</summary>
		SizeClipboard = 0x030B,

		/// <summary>WM_ASKCBFORMATNAME.</summary>
		AskCbFormatName = 0x030C,

		/// <summary>WM_CHANGECBCHAIN.</summary>
		[SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cb")]
		ChangeCbChain = 0x030D,

		/// <summary>WM_HSCROLLCLIPBOARD.</summary>
		HScrollClipboard = 0x030E,

		/// <summary>WM_QUERYNEWPALETTE.</summary>
		QueryNewPalette = 0x030F,

		/// <summary>WM_PALETTEISCHANGING.</summary>
		PaletteIsChanging = 0x0310,

		/// <summary>WM_PALETTECHANGED.</summary>
		PaletteChanged = 0x0311,

		/// <summary>WM_HOTKEY.</summary>
		HotKey = 0x0312,

		/// <summary>WM_PRINT.</summary>
		Print = 0x0317,

		/// <summary>WM_PRINTCLIENT.</summary>
		PrintClient = 0x0318,

		/// <summary>WM_APPCOMMAND.
		/// <para/>
		/// Supported on Windows 2000 or later.
		/// </summary>
		AppCommand = 0x0319,

		/// <summary>WM_THEMECHANGED.
		/// <para/>
		/// Supported on Windows XP or later.
		/// </summary>
		ThemeChanged = 0x031A,

		/// <summary>WM_APP.</summary>
		App = 0x8000,

		/// <summary>WM_USER.</summary>
		User = 0x0400,
	}
}
