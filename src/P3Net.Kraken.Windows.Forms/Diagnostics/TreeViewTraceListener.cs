/*
 * This file implements a trace listener attached to a treeview.
 *
 * Copyright © 2003 Michael L Taylor
 * All rights reserved
 */
#region Imports

using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows.Forms;

#endregion

namespace P3Net.Kraken.Diagnostics
{
	/// <summary>Provides a trace listener attached to a treeview.</summary>
	/// <remarks>
	/// All trace messages are sent to the treeview.  To clear the control call <see cref="Clear"/>.  Indentation
	/// controls the level at which messages appear.
	/// </remarks>
	[CodeNotTested]
	public class TreeViewTraceListener : DefaultTraceListener
	{
		#region Construction and Destruction
		/// <summary>Initializes the object with no owner.</summary>
		/// <remarks>
		/// Until the owner is set this listener will do nothing.
		/// </remarks>
		public TreeViewTraceListener ( )
		{ /* Do nothing */ }

		/// <summary>Initializes the object with the owning TreeView.</summary>
		public TreeViewTraceListener ( TreeView owner )
		{
			Owner = owner;
		}
		#endregion

		#region Attributes
		
		/// <summary>The TreeView associated with the listener.</summary>
		/// <value>Gets or sets the owning TreeView that listener messages are sent to.</value>
		public TreeView Owner 
		{
			get { return m_Owner; }
			set	{ m_Owner = value; }
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
		///				TreeViewTraceListener listener = new TreeViewTraceListener();
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
		///				Dim listener As New TreeViewTraceListener()
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
				m_Owner.Nodes.Clear();
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

		/// <summary>Gets the image index of specified category, if any.</summary>
		/// <param name="category">Category to retrieve image for</param>
		/// <returns>The image index or -1 if there is no associated image.</returns>
		public int GetCategory ( string category )
		{ 
			if (m_Categories == null)
				return -1;

			return (int)m_Categories[category]; 
		}

		/// <summary>Sets the image index of a category.</summary>
		/// <param name="category">Category to set.</param>
		/// <param name="index">Image index to use.</param>
		/// <exception cref="ArgumentNullException"><paramref name="category"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.</exception>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.ArgumentOutOfRangeException.#ctor(System.String,System.Object,System.String)")]
		public void SetCategory ( string category, int index )
		{
			if (category == null)
				throw new ArgumentNullException("category");
			if (index < 0)
				throw new ArgumentOutOfRangeException("index", index, "Index is less than zero.");

			if (m_Categories == null)
				m_Categories = new ListDictionary();

			m_Categories.Add(category, index);
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
		{
			Write(message, null);
		}

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

			//Send it to the control
			if (message != null)
				AddNode(m_Owner, Trace.IndentLevel, message, category);
		}

		/// <summary>Writes the value of the given object's ToString() method to the listener.</summary>
		/// <param name="o">The object to write to the listener.</param>		
		public override void WriteLine ( object o )
		{
			if (o != null)
				WriteLine(o.ToString(), null);
		}

		/// <summary>Writes the message to the listener.</summary>
		/// <param name="message">The message to write to the listener.</param>
		public override void WriteLine ( string message )
		{
			WriteLine(message, null);
		}

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

			//Send it to the control
			if (message != null)
				AddNode(m_Owner, Trace.IndentLevel, message, category);
		}
		#endregion

		#region Private Methods

		//Async handler for adding a node to a TreeView
		delegate void AddNodeDelegate ( TreeView tree, int depth, string message, string category );
		private void AddNode ( TreeView tree, int depth, string message, string category )
		{
			if (tree.InvokeRequired)
			{
				//Not on the UI thread so post the request
				AddNodeDelegate del = new AddNodeDelegate(AddNode);
				tree.FindForm().BeginInvoke(del, new object[] {tree, depth, message, category});
			} else
			{
				try
				{
					m_Owner.BeginUpdate();

					//Move to the correct node collection given the current node and the depth
					TreeNodeCollection coll = MoveToDepth(depth);		

					//Add the new message					
					TreeNode child = coll.Add(message);						
					TreeNode parent = child.Parent;
					while (parent != null)
					{
						parent.Expand();
						parent = parent.Parent;
					};

					//Set the category image as needed
					int nIdx = -1;
					if (String.IsNullOrEmpty(category)) 
						nIdx = GetCategory(category);
					child.ImageIndex = child.SelectedImageIndex = nIdx;
				} finally
				{
					m_Owner.EndUpdate();
				};
			};
		}
		
		//MODIFIED: MLT - 2/4/06 CA1822 - Make static
		//Gets the last node of a collection		
		private static TreeNode GetLastNode ( TreeNodeCollection coll )
		{
			//If the collection is empty
			if (coll.Count == 0)
				return null;

			//Return the last element in this collection
			return coll[coll.Count - 1];
		}

		//Move to the appropriate node
		private TreeNodeCollection MoveToDepth ( int depth )
		{	
			//Start at the root (depth = 0)
			int nActual = 0;
			TreeNodeCollection coll = m_Owner.Nodes;
			
			//If the actual depth is less than our desired depth
			while (nActual < depth)
			{
				//We need to go to the next level so get the last node at this level
				TreeNode last = GetLastNode(coll);
				if (last == null)
				{
					//There are no nodes at this level so create a new node 
					last = coll.Add("");
				};

				//Move to the next level
				++nActual;
				coll = last.Nodes;
			};

			//Finally we're at the correct depth, return the collection
			return coll;
		}
		#endregion

		#region Private Data		
		//The owning TreeView where messages will go
		private TreeView m_Owner;

		//Mapping of categories to image indice
		private ListDictionary m_Categories;
		#endregion
	}
}
