/*
 * Displays a "not implemented" box.
 *
 * Copyright (c) 2005 Michael L. Taylor ($COMPANY$)
 * All Rights Reserved
 *
 * $Header: /DotNET/Kraken/Source/P3Net.Kraken.UI/Dialogs/NotImplementedBox.cs 7     10/26/05 7:59a Michael $
 */
#region Imports

using System;
using System.Diagnostics;
using System.Windows.Forms;

using P3Net.Kraken;
#endregion

namespace P3Net.Kraken.WinForms
{
	/// <summary>Displays a simple message box with the words Not Implemented.</summary>
	[CodeNotTested]
	public static class NotImplementedBox
	{
		#region Public Members
		
		#region Methods
		
		#region Show

        /// <summary>Displays a "not implemented" message box.</summary>
        /// <param name="caption">The title of the message box.</param>
		/// <example>
		/// <code lang="C#">
		///		using System;
		///		using System.Windows.Forms;
		/// 
		///		public class MainForm : System.Windows.Forms.Form
		///		{
		///			private void OnFileOpen ( object sender, EventArgs e )
		///			{
		///				NotImplementedBox.Show("File/Open");
		///			}
		///		}
		/// </code>
		/// <code lang="Visual Basic">
		///		Imports System
		///		Imports System.Windows.Forms
		/// 
		///		Public Class MainForm
		///			Inherits Form
		/// 
		///			Private Sub OnFileOpen ( ByVal sender As Object, ByVal e As EventArgs )
		/// 
		///				NotImplementedBox.Show("File/Open")
		///			End Sub
		///		End Class
		/// </code>
		/// </example>
        [DebuggerStepThrough]
        public static void Show ( string caption )
        {
            MessageBoxEx.Show("Not Implemented Yet!", caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        /// <summary>Displays a "not implemented" message box.</summary>
        /// <param name="owner">The owning window.</param>
        /// <param name="caption">The title of the message box.</param>		
		/// <example>
		/// <code lang="C#">
		///		using System;
		///		using System.Windows.Forms;
		/// 
		///		public class MainForm : System.Windows.Forms.Form
		///		{
		///			private void OnFileOpen ( object sender, EventArgs e )
		///			{
		///				NotImplementedBox.Show(this, "File/Open");
		///			}
		///		}
		/// </code>
		/// <code lang="Visual Basic">
		///		Imports System
		///		Imports System.Windows.Forms
		/// 
		///		Public Class MainForm
		///			Inherits Form
		/// 
		///			Private Sub OnFileOpen ( ByVal sender As Object, ByVal e As EventArgs )
		/// 
		///				NotImplementedBox.Show(Me, "File/Open")
		///			End Sub
		///		End Class
		/// </code>
		/// </example>
        [DebuggerStepThrough]
		public static void Show ( System.Windows.Forms.IWin32Window owner, string caption )
		{
	        MessageBoxEx.Show(owner, "Not Implemented Yet!", caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		#endregion 
		
		#endregion
		
		#endregion //Public Members
	}
}
