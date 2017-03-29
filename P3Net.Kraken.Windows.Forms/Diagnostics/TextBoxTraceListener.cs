/*
 * This file implements a trace listener attached to a multi-line text box.
 *
 * Copyright © 2003 Michael L Taylor ($COMPANY$)
 * All rights reserved
 *
 * $Header: $
 */
#region Imports

using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

using TaylorSoft;
#endregion

namespace P3Net.Kraken.Diagnostics
{
	/// <summary>Provides a trace listener attached to a multi-line text box.</summary>
	/// <remarks>
	/// All trace messages are sent to the text box.  To clear the textbox call <see cref="Clear"/>.
	/// </remarks>
	[CodeNotTested]
	public class TextBoxTraceListener : DefaultTraceListener
	{
		#region Construction and Destruction

		/// <summary>Initializes a new instance of the <see cref="TextBoxTraceListener"/> class.</summary>
		/// <remarks>
		/// Until the owner is set this listener will do nothing.
		/// </remarks>
		public TextBoxTraceListener ( )
		{ /* Do nothing */ }

		/// <summary>Initializes a new instance of the <see cref="TextBoxTraceListener"/> class.</summary>
		/// <param name="owner">The owning text box.</param>
		/// <remarks>
		/// The owner must be a multi-line TextBoxBase or an assertion will occur.
		/// </remarks>
		public TextBoxTraceListener ( TextBoxBase owner )
		{
			Owner = owner;
		}
		#endregion

		#region Attributes
		
		/// <summary>The TextBox associated with the listener.</summary>
		/// <value>Gets or sets the owning <see cref="TextBoxBase"/> that listener messages are sent to.
		/// The TextBoxBase must be multiline enabled.</value>
		public TextBoxBase Owner 
		{
			[DebuggerStepThrough]
			get { return m_Owner; }

			[DebuggerStepThrough]
			set
			{
				//Verify the owner is multi-line
				Debug.Assert(value.Multiline, "The owning TextBox must be multilined");

				//Set the owner
				m_Owner = value;
			}
		}
		#endregion

		#region Public Methods

		/// <summary>Clears the contents of the listener.</summary>
		/// <example>
		/// <code lang="C#">
		///		using System;
		///		using System.Diagnostics;
		/// 
		///		public class App
		///		{
		///			public static void Main ( string[] args )
		///			{
		///				TextBoxTraceListener listener = new TextBoxTraceListener();
		///				Debug.Listeners.Add(listener);
		///				
		///				Debug.WriteLine("Message 1");
		///				Debug.WriteLine("Message 2");
		///				Debug.WriteLine("Message 3");
		/// 
		///				listener.Clear();
		///			}
		///		}
		/// </code>
		/// <code lang="VB">
		///		Imports System
		///		Imports System.Diagnostics
		/// 
		///		Class App
		///		
		///			Shared Sub Main ( )
		///			
		///				Dim listener As New TextBoxTraceListener()
		///				Debug.Listeners.Add(listener)
		///				
		///				Debug.WriteLine("Message 1")
		///				Debug.WriteLine("Message 2")
		///				Debug.WriteLine("Message 3")
		/// 
		///				listener.Clear()
		///			Sub
		///		End Class
		/// </code>
		/// </example>
		public void Clear ( )
		{
			if (m_Owner != null)
				m_Owner.Clear();
		}

		/// <summary>Resets the listener.  It must be reinitialized.</summary>
		public override void Close ( )
		{
			m_Owner = null;
		}

		/// <summary>Flushes the listener.</summary>
		public override void Flush ( )
		{
			if (m_Owner != null)
				m_Owner.Refresh();
		}

		/// <summary>Writes the value of the given object's ToString() method to the listener.</summary>
		/// <param name="o">The object to write to the listener.</param>
		public override void Write ( object o )
		{			
			if (o != null)
				Write(o.ToString(), null);
		}

		/// <summary>Writes the message to the listener.</summary>
		/// <param name="message">The message to write to the listener.</param>
		public override void Write ( string message )
		{ Write(message, null); }

		/// <summary>Writes the category name and the value of the given object's ToString() method to the listener.</summary>		
		/// <param name="o">The object to write to the listener.</param>
		/// <param name="category">The category of the message.</param>
		/// <remarks>
		/// The message is written as {category}: {0}
		/// </remarks>
		public override void Write ( object o, string category )
		{
			if (o != null)
				Write(o.ToString(), category);
		}

		/// <summary>Writes the category name and the message to the listener.</summary>		
		/// <param name="message">The message to write to the listener.</param>
		/// <param name="category">The category of the message.</param>
		/// <remarks>
		/// The message is written as {category}: {message}
		/// </remarks>
		public override void Write ( string message, string category )
		{
			if (m_Owner == null)
				return;

			//Format the text
			if (message != null)
			{
				StringBuilder bldr = new StringBuilder();
				if (category != null)
					bldr.AppendFormat("{0}{1}: {2}", GetIndentString(), category, message);
				else
					bldr.AppendFormat("{0}{1}", GetIndentString(), message);

				//Send it to the control
				AppendControlText(m_Owner, bldr.ToString());
			};
		}

		/// <summary>Writes the value of the given object's ToString() method to the listener.</summary>
		/// <param name="o">The object to write to the listener.</param>
		public override void WriteLine ( object o )
		{
			if (o != null)
				WriteLine(o.ToString(), null);
		}

		/// <summary>Writes the message to the listener.</summary>
		/// <param name="message">The message to write.</param>
		public override void WriteLine ( string message )
		{ WriteLine(message, null); }

		/// <summary>Writes the category name and the value of the given object's ToString() method to the listener.</summary>		
		/// <param name="o">The object to write to the listener.</param>
		/// <param name="category">The category of the message.</param>
		/// <remarks>
		/// The message is written as {category}: {0}
		/// </remarks>
		public override void WriteLine ( object o, string category )
		{
			if (o != null)
				WriteLine(o.ToString(), category);
		}

		/// <summary>Writes the category name and the message to the listener.</summary>		
		/// <param name="message">The message to write to the listener.</param>
		/// <param name="category">The category of the message.</param>
		/// <remarks>
		/// The message is written as {category}: {message}
		/// </remarks>
		public override void WriteLine ( string message, string category )
		{
			if (m_Owner == null)
				return;

			//Format the text
			if (message != null)
			{
				StringBuilder bldr = new StringBuilder();
				if (category != null)
					bldr.AppendFormat("{0}{1}: {2}\r\n", GetIndentString(), category, message);
				else
					bldr.AppendFormat("{0}{1}\r\n", GetIndentString(), message);

				//Send it to the control
				AppendControlText(m_Owner, bldr.ToString());
			};
		}
		#endregion

		#region Private Methods
		
		//Async handler for writing to TextBox
		delegate void AppendControlTextDelegate ( TextBoxBase box, string message );
		private static void AppendControlText ( TextBoxBase box, string message )
		{
			if (box.InvokeRequired)
			{
				//Not on the UI thread so post the request
				AppendControlTextDelegate del = new AppendControlTextDelegate(AppendControlText);
				box.FindForm().BeginInvoke(del, new object[] {box, message});
			} else
				box.AppendText(message);
		}

		//MODIFIED: MLT - 2/4/06 CA1822 - Make static
		private static string GetIndentString ( )
		{
			return new String(' ', Trace.IndentSize * Trace.IndentLevel);
		}
		#endregion

		#region Private Data

		//The owning TextBoxBase where messages will go
		private TextBoxBase m_Owner;
		#endregion
	}
}
