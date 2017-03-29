/*
 * Provides support for working with raw WIN32 messages.
 *
 * Copyright © 2005 Ata Lab, Federal Aviation Administration
 * All Rights Reserved
 * 
 * $Header: /DotNET/Kraken/Source/P3Net.Kraken.UI/Messages/Win32Message.cs 3     11/04/05 8:22a Michael $
 */
#region Imports

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace P3Net.Kraken.WinForms.Native
{
	/// <summary>Provides support for working with raw Win32 messages.</summary>
	/// <preliminary/>
	[CodeNotDocumented]
	[CodeNotTested]
	public static class Win32Message
	{
		#region Public Members

		#region Delegates

		/// <summary>Called after a thread processes a message sent using <see cref="M:SendMessageCallback"/>.</summary>
		/// <param name="handle">The window that processed the message.</param>
		/// <param name="message">The message that was sent.</param>
		/// <param name="data">The application-defined data that was sent.</param>
		/// <param name="result">The result from processing the message.</param>
		/// <remarks>
		/// Care must be taken to ensure that <paramref name="data"/> is not garbage collected before the callback occurs.
		/// </remarks>
		public delegate void SendAsyncCallback ( IntPtr handle, int message, IntPtr data, IntPtr result );
		#endregion

		#region Methods

		#region PostMessage

		/// <summary>Posts a message to the message queue of the given control and returns.</summary>
		/// <param name="control">The control to receive the message.</param>
		/// <param name="message">The message to send.</param>
		/// <param name="wParam">The WPARAM for the message.</param>
		/// <param name="lParam">The LPARAM for the message.</param>
		/// <returns><see langword="true"/> if the message was posted to the queue or <see langword="false"/> otherwise.</returns>
		/// <remarks>
		/// The message is posted to the message queue of the control and then the method returns.  The message may
		/// or may not be processed prior to this method returning.
		/// <para/>
		/// If <paramref name="wParam"/> or <paramref name="lParam"/> are reference types then care must be taken
		/// to ensure the garbage collector does not collect them while the method is being called.  Use <see cref="GCHandle"/>
		/// where appropriate.
		/// <para/>
		/// If <paramref name="control"/> is <see langword="null"/> then the message is broadcast to all
		/// top level windows.		
		/// </remarks>
		/// <exception cref="Win32Exception">An error occurred posting the message.</exception>
		/// <seealso cref="M:SendMessage"/>
		/// <seealso cref="M:PostThreadMessage"/>		
		public static bool PostMessage ( Control control, WindowMessage message, IntPtr wParam, IntPtr lParam )
		{
			//Build a structure to avoid having to deal with GC issues
			Message msg = MakeMessage(control, (int)message, wParam, lParam);
			return PostMessage(msg);
		}

		/// <summary>Posts a message to the message queue of the given control and returns.</summary>
		/// <param name="control">The control to receive the message.</param>
		/// <param name="message">The message to send.</param>
		/// <param name="wParam">The WPARAM for the message.</param>
		/// <param name="lParam">The LPARAM for the message.</param>
		/// <returns><see langword="true"/> if the message was posted to the queue or <see langword="false"/> otherwise.</returns>
		/// <remarks>
		/// The message is posted to the message queue of the control and then the method returns.  The message may
		/// or may not be processed prior to this method returning.
		/// <para/>
		/// If <paramref name="wParam"/> or <paramref name="lParam"/> are reference types then care must be taken
		/// to ensure the garbage collector does not collect them while the method is being called.  Use <see cref="GCHandle"/>
		/// where appropriate.
		/// <para/>
		/// If <paramref name="control"/> is <see langword="null"/> then the message is broadcast to all
		/// top level windows.		
		/// </remarks>
		/// <exception cref="Win32Exception">An error occurred posting the message.</exception>
		/// <seealso cref="M:SendMessage"/>
		/// <seealso cref="M:PostThreadMessage"/>		
		public static bool PostMessage ( Control control, int message, IntPtr wParam, IntPtr lParam )
		{
			//Build a structure to avoid having to deal with GC issues
			Message msg = MakeMessage(control, message, wParam, lParam);
			return PostMessage(msg);
		}

		/// <summary>Posts a message to the message queue of the given control and returns.</summary>
		/// <param name="window">The window to receive the message.</param>
		/// <param name="message">The message to send.</param>
		/// <param name="wParam">The WPARAM for the message.</param>
		/// <param name="lParam">The LPARAM for the message.</param>
		/// <returns><see langword="true"/> if the message was posted to the queue or <see langword="false"/> otherwise.</returns>
		/// <remarks>
		/// The message is posted to the message queue of the control and then the method returns.  The message may
		/// or may not be processed prior to this method returning.
		/// <para/>
		/// If <paramref name="wParam"/> or <paramref name="lParam"/> are reference types then care must be taken
		/// to ensure the garbage collector does not collect them while the method is being called.  Use <see cref="GCHandle"/>
		/// where appropriate.
		/// <para/>
		/// If <paramref name="window"/> is -1 then the message is broadcast to all top level windows.  If 
		/// <paramref name="window"/> is 0 then the message is posted to the current thread's message queue.
		/// </remarks>
		/// <exception cref="Win32Exception">An error occurred posting the message.</exception>
		/// <seealso cref="M:SendMessage"/>
		/// <seealso cref="M:PostThreadMessage"/>		
		public static bool PostMessage ( IntPtr window, WindowMessage message, IntPtr wParam, IntPtr lParam )
		{
			//Build a structure to avoid having to deal with GC issues
			Message msg = MakeMessage(window, (int)message, wParam, lParam);
			return PostMessage(msg);
		}

		/// <summary>Posts a message to the message queue of the given control and returns.</summary>
		/// <param name="window">The window to receive the message.</param>
		/// <param name="message">The message to send.</param>
		/// <param name="wParam">The WPARAM for the message.</param>
		/// <param name="lParam">The LPARAM for the message.</param>
		/// <returns><see langword="true"/> if the message was posted to the queue or <see langword="false"/> otherwise.</returns>
		/// <remarks>
		/// The message is posted to the message queue of the control and then the method returns.  The message may
		/// or may not be processed prior to this method returning.
		/// <para/>
		/// If <paramref name="wParam"/> or <paramref name="lParam"/> are reference types then care must be taken
		/// to ensure the garbage collector does not collect them while the method is being called.  Use <see cref="GCHandle"/>
		/// where appropriate.
		/// <para/>
		/// If <paramref name="window"/> is -1 then the message is broadcast to all top level windows.  If 
		/// <paramref name="window"/> is 0 then the message is posted to the current thread's message queue.
		/// </remarks>
		/// <exception cref="Win32Exception">An error occurred posting the message.</exception>
		/// <seealso cref="M:SendMessage"/>
		/// <seealso cref="M:PostThreadMessage"/>		
		public static bool PostMessage ( IntPtr window, int message, IntPtr wParam, IntPtr lParam )
		{
			//Build a structure to avoid having to deal with GC issues
			Message msg = MakeMessage(window, message, wParam, lParam);
			return PostMessage(msg);
		}

		/// <summary>Posts a message to the message queue of the given control and returns.</summary>
		/// <param name="message">The message to send.</param>
		/// <returns><see langword="true"/> if the message was posted to the queue or <see langword="false"/> otherwise.</returns>
		/// <remarks>
		/// The message is posted to the message queue of the control and then the method returns.  The message may
		/// or may not be processed prior to this method returning.
		/// <para/>
		/// Since the method will not wait for the message to be processed care must be taken to ensure that no parameters
		/// passed in the message are garbage collected or altered until the message has been handled.
		/// <para/>
		/// If the target handle is -1 then the message is posted to all top-level windows.  If the target handle is 0 then
		/// the message is posted to the current thread's message queue.
		/// </remarks>
		/// <exception cref="Win32Exception">An error occurred posting the message.</exception>
		/// <seealso cref="M:SendMessage"/>
		/// <seealso cref="M:PostThreadMessage"/>		
		public static bool PostMessage ( Message message )
		{
			if (InteropPostMessage(message.HWnd, message.Msg, message.WParam, message.LParam))
				return true;

			int nRes = Marshal.GetLastWin32Error();
			if (nRes != 0)
				throw new Win32Exception(Marshal.GetLastWin32Error());
			return false;
		}
		#endregion

		#region PostThreadMessage

		/// <summary>Posts a message to the message queue of the specified thread.</summary>
		/// <param name="threadId">The identifier of the thread to post the message to.</param>
		/// <param name="message">The message to send.</param>
		/// <param name="wParam">The WPARAM for the message.</param>
		/// <param name="lParam">The LPARAM for the message.</param>
		/// <returns><see langword="true"/> if the message was posted to the queue or <see langword="false"/> otherwise.</returns>
		/// <remarks>
		/// The message is posted to the message queue of the specified thread and then the method returns.  The message may
		/// or may not be processed prior to this method returning.  If the thread has not yet created a message queue or if the
		/// thread is not on the same desktop as the current thread then an error will occur.
		/// <para/>
		/// If <paramref name="wParam"/> or <paramref name="lParam"/> are reference types then care must be taken
		/// to ensure the garbage collector does not collect them while the method is being called.  Use <see cref="GCHandle"/>
		/// where appropriate.		
		/// </remarks>		
		/// <exception cref="ArgumentException">
		/// <paramref name="threadId"/> is invalid or refers to a thread on a different desktop.
		/// <para>-or-</para>
		/// The thread with the specified identifier does not have a message queue.
		/// </exception>
		/// <seealso cref="M:SendMessage"/>
		/// <seealso cref="M:PostMessage"/>	
		[CLSCompliant(false)]
		public static bool PostThreadMessage ( uint threadId, WindowMessage message, IntPtr wParam, IntPtr lParam )
		{
			return PostThreadMessage(threadId, (int)message, wParam, lParam);
		}

		/// <summary>Posts a message to the message queue of the specified thread.</summary>
		/// <param name="threadId">The identifier of the thread to post the message to.</param>
		/// <param name="message">The message to send.</param>
		/// <param name="wParam">The WPARAM for the message.</param>
		/// <param name="lParam">The LPARAM for the message.</param>
		/// <returns><see langword="true"/> if the message was posted to the queue or <see langword="false"/> otherwise.</returns>
		/// <remarks>
		/// The message is posted to the message queue of the specified thread and then the method returns.  The message may
		/// or may not be processed prior to this method returning.  If the thread has not yet created a message queue or if the
		/// thread is not on the same desktop as the current thread then an error will occur.
		/// <para/>
		/// If <paramref name="wParam"/> or <paramref name="lParam"/> are reference types then care must be taken
		/// to ensure the garbage collector does not collect them while the method is being called.  Use <see cref="GCHandle"/>
		/// where appropriate.		
		/// </remarks>		
		/// <exception cref="ArgumentException">
		/// <paramref name="threadId"/> is invalid or refers to a thread on a different desktop.
		/// <para>-or-</para>
		/// The thread with the specified identifier does not have a message queue.
		/// </exception>
		/// <seealso cref="M:SendMessage"/>
		/// <seealso cref="M:PostMessage"/>	
		[CLSCompliant(false)]
		public static bool PostThreadMessage ( uint threadId, int message, IntPtr wParam, IntPtr lParam )
		{
			if (InteropPostThreadMessage(threadId, message, wParam, lParam))
				return true;

			//Something went wrong
			switch (Marshal.GetLastWin32Error())
			{
				//case : throw new ArgumentException("The thread does not have a message queue.", "threadId");
				case 1444: throw new ArgumentException("The thread identifier is invalid or the thread does not have a message queue.", "threadId");
			};

			int nRes = Marshal.GetLastWin32Error();
			if (nRes != 0)
				throw new Win32Exception(Marshal.GetLastWin32Error());
			return false;
		}
		#endregion

		#region SendMessage

		/// <summary>Sends a message to the specified control and waits for a response.</summary>
		/// <param name="control">The control to send the message to.</param>
		/// <param name="message">The message to send.</param>
		/// <param name="wParam">The WPARAM for the message.</param>
		/// <param name="lParam">The LPARAM for the message.</param>
		/// <returns>The result of the message processing from the control.</returns>
		/// <remarks>
		/// If the control is on the same thread as the sender then the message procedure is called directly 
		/// otherwise the calling thread will block until the target control processes the message.  If the
		/// target control is locked then the caller will block indefinitely.
		/// <para/>
		/// If <paramref name="wParam"/> or <paramref name="lParam"/> are reference types then care must be taken
		/// to ensure the garbage collector does not collect them while the method is being called.  Use <see cref="GCHandle"/>
		/// where appropriate.
		/// <para/>
		/// If <paramref name="control"/> is <see langword="null"/> then the message is broadcast to all
		/// top level windows.
		/// </remarks>
		/// <seealso cref="M:PostMessage"/>
		/// <seealso cref="M:SendMessageCallback"/>
		/// <seealso cref="M:SendMessageTimeout"/>
		public static IntPtr SendMessage ( Control control, WindowMessage message, IntPtr wParam, IntPtr lParam )
		{
			//Build a structure to avoid having to deal with GC issues
			Message msg = MakeMessage(control, (int)message, wParam, lParam);
			return SendMessage(msg);
		}

		/// <summary>Sends a message to the specified control and waits for a response.</summary>
		/// <param name="control">The control to send the message to.</param>
		/// <param name="message">The message to send.</param>
		/// <param name="wParam">The WPARAM for the message.</param>
		/// <param name="lParam">The LPARAM for the message.</param>
		/// <returns>The result of the message processing from the control.</returns>
		/// <remarks>
		/// If the control is on the same thread as the sender then the message procedure is called directly 
		/// otherwise the calling thread will block until the target control processes the message.  If the
		/// target control is locked then the caller will block indefinitely.
		/// <para/>
		/// If <paramref name="wParam"/> or <paramref name="lParam"/> are reference types then care must be taken
		/// to ensure the garbage collector does not collect them while the method is being called.  Use <see cref="GCHandle"/>
		/// where appropriate.
		/// <para/>
		/// If <paramref name="control"/> is <see langword="null"/> then the message is broadcast to all
		/// top level windows.
		/// </remarks>
		/// <seealso cref="M:PostMessage"/>
		/// <seealso cref="M:SendMessageCallback"/>
		/// <seealso cref="M:SendMessageTimeout"/>
		public static IntPtr SendMessage ( Control control, int message, IntPtr wParam, IntPtr lParam )
		{
			//Build a structure to avoid having to deal with GC issues
			Message msg = MakeMessage(control, message, wParam, lParam);
			return SendMessage(msg);
		}

		/// <summary>Sends a message to the specified control and waits for a response.</summary>
		/// <param name="window">The window to send the message to.</param>
		/// <param name="message">The message to send.</param>
		/// <param name="wParam">The WPARAM for the message.</param>
		/// <param name="lParam">The LPARAM for the message.</param>
		/// <returns>The result of the message processing from the control.</returns>
		/// <remarks>
		/// If the control is on the same thread as the sender then the message procedure is called directly 
		/// otherwise the calling thread will block until the target control processes the message.  If the
		/// target control is locked then the caller will block indefinitely.
		/// <para/>
		/// If <paramref name="wParam"/> or <paramref name="lParam"/> are reference types then care must be taken
		/// to ensure the garbage collector does not collect them while the method is being called.  Use <see cref="GCHandle"/>
		/// where appropriate.
		/// <para/>
		/// If <paramref name="window"/> is -1 then the message is broadcast to all top level windows.
		/// </remarks>
		/// <seealso cref="M:PostMessage"/>
		/// <seealso cref="M:SendMessageCallback"/>
		/// <seealso cref="M:SendMessageTimeout"/>
		public static IntPtr SendMessage ( IntPtr window, WindowMessage message, IntPtr wParam, IntPtr lParam )
		{
			//Build a structure to avoid having to deal with GC issues
			Message msg = MakeMessage(window, (int)message, wParam, lParam);
			return SendMessage(msg);
		}

		/// <summary>Sends a message to the specified control and waits for a response.</summary>
		/// <param name="window">The window to send the message to.</param>
		/// <param name="message">The message to send.</param>
		/// <param name="wParam">The WPARAM for the message.</param>
		/// <param name="lParam">The LPARAM for the message.</param>
		/// <returns>The result of the message processing from the control.</returns>
		/// <remarks>
		/// If the control is on the same thread as the sender then the message procedure is called directly 
		/// otherwise the calling thread will block until the target control processes the message.  If the
		/// target control is locked then the caller will block indefinitely.
		/// <para/>
		/// If <paramref name="wParam"/> or <paramref name="lParam"/> are reference types then care must be taken
		/// to ensure the garbage collector does not collect them while the method is being called.  Use <see cref="GCHandle"/>
		/// where appropriate.
		/// <para/>
		/// If <paramref name="window"/> is -1 then the message is broadcast to all top level windows.
		/// </remarks>
		/// <seealso cref="M:PostMessage"/>
		/// <seealso cref="M:SendMessageCallback"/>
		/// <seealso cref="M:SendMessageTimeout"/>
		public static IntPtr SendMessage ( IntPtr window, int message, IntPtr wParam, IntPtr lParam )
		{
			//Build a structure to avoid having to deal with GC issues
			Message msg = MakeMessage(window, message, wParam, lParam);
			return SendMessage(msg);
		}

		/// <summary>Sends a message to the specified control and waits for a response.</summary>
		/// <param name="message">The message to send.</param>
		/// <returns>The result of the message processing from the control.</returns>
		/// <remarks>
		/// If the control is on the same thread as the sender then the message procedure is called directly 
		/// otherwise the calling thread will block until the target control processes the message.  If the
		/// target control is locked then the caller will block indefinitely.
		/// <para/>
		/// If the window is -1 then the message is broadcast to all top level windows.
		/// </remarks>
		/// <seealso cref="M:PostMessage"/>
		/// <seealso cref="M:SendMessageCallback"/>
		/// <seealso cref="M:SendMessageTimeout"/>
		/// <seealso cref="M:SendNotifyMessage"/>
		public static IntPtr SendMessage ( Message message )
		{
			message.Result = InteropSendMessage(message.HWnd, message.Msg, message.WParam, message.LParam);
			return message.Result;
		}
		#endregion

		#region SendMessageCallback

		/// <summary>Sends a message to the specified control and returns.  The specified callback is called once
		/// the message has been handled.</summary>
		/// <param name="control">The control to send the message to.</param>
		/// <param name="message">The message to send.</param>
		/// <param name="wParam">The WPARAM for the message.</param>
		/// <param name="lParam">The LPARAM for the message.</param>
		/// <param name="callback">The method to call once the message has been processed.</param>
		/// <param name="appData">Application-defined data to pass to the callback.</param>
		/// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
		/// <remarks>
		/// The message is sent to the window procedure of the control and then returns immediately.  Once the message
		/// is processed by the control <paramref name="callback"/> will be called.
		/// <para/>
		/// If <paramref name="wParam"/>, <paramref name="lParam"/> or <paramref name="appData"/> are reference types then care 
		/// must be taken to ensure the garbage collector does not collect them while the method is being called.  Use 
		/// <see cref="GCHandle"/> where appropriate.
		/// <para/>
		/// If <paramref name="control"/> is <see langword="null"/> then the message is broadcast to all
		/// top level windows.  In this case <paramref name="callback"/> is called once per window.
		/// </remarks>
		/// <seealso cref="M:PostMessage"/>
		/// <seealso cref="M:SendMessage"/>
		/// <seealso cref="M:SendMessageTimeout"/>
		public static bool SendMessageCallback ( Control control, WindowMessage message, IntPtr wParam, IntPtr lParam,
												   SendAsyncCallback callback, IntPtr appData )
		{
			//Build a structure to avoid having to deal with GC issues
			Message msg = MakeMessage(control, (int)message, wParam, lParam);
			return SendMessageCallback(msg, callback, appData);
		}

		/// <summary>Sends a message to the specified control and returns.  The specified callback is called once
		/// the message has been handled.</summary>
		/// <param name="control">The control to send the message to.</param>
		/// <param name="message">The message to send.</param>
		/// <param name="wParam">The WPARAM for the message.</param>
		/// <param name="lParam">The LPARAM for the message.</param>
		/// <param name="callback">The method to call once the message has been processed.</param>
		/// <param name="appData">Application-defined data to pass to the callback.</param>
		/// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
		/// <remarks>
		/// The message is sent to the window procedure of the control and then returns immediately.  Once the message
		/// is processed by the control <paramref name="callback"/> will be called.
		/// <para/>
		/// If <paramref name="wParam"/>, <paramref name="lParam"/> or <paramref name="appData"/> are reference types then care 
		/// must be taken to ensure the garbage collector does not collect them while the method is being called.  Use 
		/// <see cref="GCHandle"/> where appropriate.
		/// <para/>
		/// If <paramref name="control"/> is <see langword="null"/> then the message is broadcast to all
		/// top level windows.  In this case <paramref name="callback"/> is called once per window.
		/// </remarks>
		/// <seealso cref="M:PostMessage"/>
		/// <seealso cref="M:SendMessage"/>
		/// <seealso cref="M:SendMessageTimeout"/>
		public static bool SendMessageCallback ( Control control, int message, IntPtr wParam, IntPtr lParam,
												   SendAsyncCallback callback, IntPtr appData )
		{
			//Build a structure to avoid having to deal with GC issues
			Message msg = MakeMessage(control, message, wParam, lParam);
			return SendMessageCallback(msg, callback, appData);
		}

		/// <summary>Sends a message to the specified control and returns.  The specified callback is called once
		/// the message has been handled.</summary>
		/// <param name="window">The window to send the message to.</param>
		/// <param name="message">The message to send.</param>
		/// <param name="wParam">The WPARAM for the message.</param>
		/// <param name="lParam">The LPARAM for the message.</param>
		/// <param name="callback">The method to call once the message has been processed.</param>
		/// <param name="appData">Application-defined data to pass to the callback.</param>
		/// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
		/// <remarks>
		/// The message is sent to the window procedure of the control and then returns immediately.  Once the message
		/// is processed by the control <paramref name="callback"/> will be called.
		/// <para/>
		/// If <paramref name="wParam"/>, <paramref name="lParam"/> or <paramref name="appData"/> are reference types then care 
		/// must be taken to ensure the garbage collector does not collect them while the method is being called.  Use 
		/// <see cref="GCHandle"/> where appropriate.
		/// <para/>
		/// If <paramref name="window"/> is -1 then the message is broadcast to all
		/// top level windows.  In this case <paramref name="callback"/> is called once per window.
		/// </remarks>
		/// <seealso cref="M:PostMessage"/>
		/// <seealso cref="M:SendMessage"/>
		/// <seealso cref="M:SendMessageTimeout"/>
		public static bool SendMessageCallback ( IntPtr window, WindowMessage message, IntPtr wParam, IntPtr lParam,
												   SendAsyncCallback callback, IntPtr appData )
		{
			//Build a structure to avoid having to deal with GC issues
			Message msg = MakeMessage(window, (int)message, wParam, lParam);
			return SendMessageCallback(msg, callback, appData);
		}

		/// <summary>Sends a message to the specified control and returns.  The specified callback is called once
		/// the message has been handled.</summary>
		/// <param name="window">The window to send the message to.</param>
		/// <param name="message">The message to send.</param>
		/// <param name="wParam">The WPARAM for the message.</param>
		/// <param name="lParam">The LPARAM for the message.</param>
		/// <param name="callback">The method to call once the message has been processed.</param>
		/// <param name="appData">Application-defined data to pass to the callback.</param>
		/// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
		/// <remarks>
		/// The message is sent to the window procedure of the control and then returns immediately.  Once the message
		/// is processed by the control <paramref name="callback"/> will be called.
		/// <para/>
		/// If <paramref name="wParam"/>, <paramref name="lParam"/> or <paramref name="appData"/> are reference types then care 
		/// must be taken to ensure the garbage collector does not collect them while the method is being called.  Use 
		/// <see cref="GCHandle"/> where appropriate.
		/// <para/>
		/// If <paramref name="window"/> is -1 then the message is broadcast to all
		/// top level windows.  In this case <paramref name="callback"/> is called once per window.
		/// </remarks>
		/// <seealso cref="M:PostMessage"/>
		/// <seealso cref="M:SendMessage"/>
		/// <seealso cref="M:SendMessageTimeout"/>
		public static bool SendMessageCallback ( IntPtr window, int message, IntPtr wParam, IntPtr lParam,
												   SendAsyncCallback callback, IntPtr appData )
		{
			//Build a structure to avoid having to deal with GC issues
			Message msg = MakeMessage(window, message, wParam, lParam);
			return SendMessageCallback(msg, callback, appData);
		}

		/// <summary>Sends a message to the specified control and returns.  The specified callback is called once
		/// the message has been handled.</summary>
		/// <param name="message">The message to send.</param>
		/// <param name="callback">The method to call once the message has been processed.</param>
		/// <param name="appData">Application-defined data to pass to the callback.</param>
		/// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
		/// <remarks>
		/// The message is sent to the window procedure of the control and then returns immediately.  Once the message
		/// is processed by the control <paramref name="callback"/> will be called.
		/// <para/>
		/// If <b>wParam</b>, <b>lParam</b> or <paramref name="appData"/> are reference types then care 
		/// must be taken to ensure the garbage collector does not collect them while the method is being called.  Use 
		/// <see cref="GCHandle"/> where appropriate.
		/// <para/>
		/// If the window is -1 then the message is broadcast to all top level windows.  In this case 
		/// <paramref name="callback"/> is called once per window.
		/// </remarks>
		/// <seealso cref="M:PostMessage"/>
		/// <seealso cref="M:SendMessage"/>
		/// <seealso cref="M:SendMessageTimeout"/>
		public static bool SendMessageCallback ( Message message, SendAsyncCallback callback, IntPtr appData )
		{
			if (InteropSendMessageCallback(message.HWnd, message.Msg, message.WParam, message.LParam, callback, appData))
				return true;

			int nRes = Marshal.GetLastWin32Error();
			if (nRes != 0)
				throw new Win32Exception(nRes);

			return false;
		}
		#endregion

		#region SendMessageTimeout

		/// <summary>Sends a message to the specified control and waits for a response up to the specified
		/// timeout.</summary>
		/// <param name="control">The control to send the message to.</param>
		/// <param name="message">The message to send.</param>
		/// <param name="wParam">The WPARAM for the message.</param>
		/// <param name="lParam">The LPARAM for the message.</param>
		/// <param name="flags">Flags used to control the message handling.</param>
		/// <param name="timeout">The timeout, in milliseconds, for the call.</param>
		/// <param name="result">The result of the message being sent.</param>
		/// <returns>The result of the message processing from the control.</returns>
		/// <exception cref="ApplicationException">Timed out waiting for response.</exception>
		/// <exception cref="Win32Exception">An error occurred sending the message.</exception>
		/// <remarks>
		/// If the control is on the same thread as the sender then the message procedure is called directly 
		/// and the method blocks until it completes.  If the control is on a separate thread then the window procedure
		/// is called and the method blocks until either the message is handled or the timeout expires.
		/// <para/>
		/// If <paramref name="wParam"/> or <paramref name="lParam"/> are reference types then care must be taken
		/// to ensure the garbage collector does not collect them while the method is being called.  Use <see cref="GCHandle"/>
		/// where appropriate.
		/// <para/>
		/// If <paramref name="control"/> is <see langword="null"/> then the message is broadcast to all
		/// top level windows.  In this case the timeout applies to each window that is sent the message.
		/// </remarks>
		/// <seealso cref="M:PostMessage"/>
		/// <seealso cref="M:SendMessageCallback"/>
		/// <seealso cref="M:SendMessage"/>
		public static bool SendMessageTimeout ( Control control, WindowMessage message, IntPtr wParam, IntPtr lParam,
												  SendMessageTimeoutOptions flags, int timeout, out IntPtr result )
		{
			//Build a structure to avoid having to deal with GC issues
			Message msg = MakeMessage(control, (int)message, wParam, lParam);
			return SendMessageTimeout(msg, flags, timeout, out result);
		}

		/// <summary>Sends a message to the specified control and waits for a response up to the specified
		/// timeout.</summary>
		/// <param name="control">The control to send the message to.</param>
		/// <param name="message">The message to send.</param>
		/// <param name="wParam">The WPARAM for the message.</param>
		/// <param name="lParam">The LPARAM for the message.</param>
		/// <param name="flags">Flags used to control the message handling.</param>
		/// <param name="timeout">The timeout, in milliseconds, for the call.</param>
		/// <param name="result">The result of the message being sent.</param>
		/// <returns>The result of the message processing from the control.</returns>
		/// <exception cref="ApplicationException">Timed out waiting for response.</exception>
		/// <exception cref="Win32Exception">An error occurred sending the message.</exception>
		/// <remarks>
		/// If the control is on the same thread as the sender then the message procedure is called directly 
		/// and the method blocks until it completes.  If the control is on a separate thread then the window procedure
		/// is called and the method blocks until either the message is handled or the timeout expires.
		/// <para/>
		/// If <paramref name="wParam"/> or <paramref name="lParam"/> are reference types then care must be taken
		/// to ensure the garbage collector does not collect them while the method is being called.  Use <see cref="GCHandle"/>
		/// where appropriate.
		/// <para/>
		/// If <paramref name="control"/> is <see langword="null"/> then the message is broadcast to all
		/// top level windows.  In this case the timeout applies to each window that is sent the message.
		/// </remarks>
		/// <seealso cref="M:PostMessage"/>
		/// <seealso cref="M:SendMessageCallback"/>
		/// <seealso cref="M:SendMessage"/>
		public static bool SendMessageTimeout ( Control control, int message, IntPtr wParam, IntPtr lParam,
												  SendMessageTimeoutOptions flags, int timeout, out IntPtr result )
		{
			//Build a structure to avoid having to deal with GC issues
			Message msg = MakeMessage(control, message, wParam, lParam);
			return SendMessageTimeout(msg, flags, timeout, out result);
		}

		/// <summary>Sends a message to the specified control and waits for a response up to the specified
		/// timeout.</summary>
		/// <param name="control">The control to send the message to.</param>
		/// <param name="message">The message to send.</param>
		/// <param name="wParam">The WPARAM for the message.</param>
		/// <param name="lParam">The LPARAM for the message.</param>
		/// <param name="flags">Flags used to control the message handling.</param>
		/// <param name="timeout">The timeout for the call.</param>
		/// <param name="result">The result of the message being sent.</param>
		/// <returns>The result of the message processing from the control.</returns>
		/// <exception cref="ApplicationException">Timed out waiting for response.</exception>
		/// <exception cref="Win32Exception">An error occurred sending the message.</exception>
		/// <remarks>
		/// If the control is on the same thread as the sender then the message procedure is called directly 
		/// and the method blocks until it completes.  If the control is on a separate thread then the window procedure
		/// is called and the method blocks until either the message is handled or the timeout expires.
		/// <para/>
		/// If <paramref name="wParam"/> or <paramref name="lParam"/> are reference types then care must be taken
		/// to ensure the garbage collector does not collect them while the method is being called.  Use <see cref="GCHandle"/>
		/// where appropriate.
		/// <para/>
		/// If <paramref name="control"/> is <see langword="null"/> then the message is broadcast to all
		/// top level windows.  In this case the timeout applies to each window that is sent the message.
		/// </remarks>
		/// <seealso cref="M:PostMessage"/>
		/// <seealso cref="M:SendMessageCallback"/>
		/// <seealso cref="M:SendMessage"/>
		public static bool SendMessageTimeout ( Control control, WindowMessage message, IntPtr wParam, IntPtr lParam,
												SendMessageTimeoutOptions flags, TimeSpan timeout, out IntPtr result )
		{
			//Build a structure to avoid having to deal with GC issues
			Message msg = MakeMessage(control, (int)message, wParam, lParam);
			return SendMessageTimeout(msg, flags, Convert.ToInt32(timeout.TotalMilliseconds), out result);
		}

		/// <summary>Sends a message to the specified control and waits for a response up to the specified
		/// timeout.</summary>
		/// <param name="control">The control to send the message to.</param>
		/// <param name="message">The message to send.</param>
		/// <param name="wParam">The WPARAM for the message.</param>
		/// <param name="lParam">The LPARAM for the message.</param>
		/// <param name="flags">Flags used to control the message handling.</param>
		/// <param name="timeout">The timeout for the call.</param>
		/// <param name="result">The result of the message being sent.</param>
		/// <returns>The result of the message processing from the control.</returns>
		/// <exception cref="ApplicationException">Timed out waiting for response.</exception>
		/// <exception cref="Win32Exception">An error occurred sending the message.</exception>
		/// <remarks>
		/// If the control is on the same thread as the sender then the message procedure is called directly 
		/// and the method blocks until it completes.  If the control is on a separate thread then the window procedure
		/// is called and the method blocks until either the message is handled or the timeout expires.
		/// <para/>
		/// If <paramref name="wParam"/> or <paramref name="lParam"/> are reference types then care must be taken
		/// to ensure the garbage collector does not collect them while the method is being called.  Use <see cref="GCHandle"/>
		/// where appropriate.
		/// <para/>
		/// If <paramref name="control"/> is <see langword="null"/> then the message is broadcast to all
		/// top level windows.  In this case the timeout applies to each window that is sent the message.
		/// </remarks>
		/// <seealso cref="M:PostMessage"/>
		/// <seealso cref="M:SendMessageCallback"/>
		/// <seealso cref="M:SendMessage"/>
		public static bool SendMessageTimeout ( Control control, int message, IntPtr wParam, IntPtr lParam,
												SendMessageTimeoutOptions flags, TimeSpan timeout, out IntPtr result )
		{
			//Build a structure to avoid having to deal with GC issues
			Message msg = MakeMessage(control, message, wParam, lParam);
			return SendMessageTimeout(msg, flags, Convert.ToInt32(timeout.TotalMilliseconds), out result);
		}

		/// <summary>Sends a message to the specified window and waits for a response up to the specified
		/// timeout.</summary>
		/// <param name="window">The window to send the message to.</param>
		/// <param name="message">The message to send.</param>
		/// <param name="wParam">The WPARAM for the message.</param>
		/// <param name="lParam">The LPARAM for the message.</param>
		/// <param name="flags">Flags used to control the message handling.</param>
		/// <param name="timeout">The timeout, in milliseconds, for the call.</param>
		/// <param name="result">The result of the message being sent.</param>
		/// <returns>The result of the message processing from the control.</returns>
		/// <exception cref="ApplicationException">Timed out waiting for response.</exception>
		/// <exception cref="Win32Exception">An error occurred sending the message.</exception>
		/// <remarks>
		/// If the window is on the same thread as the sender then the message procedure is called directly 
		/// and the method blocks until it completes.  If the window is on a separate thread then the window procedure
		/// is called and the method blocks until either the message is handled or the timeout expires.
		/// <para/>
		/// If <paramref name="wParam"/> or <paramref name="lParam"/> are reference types then care must be taken
		/// to ensure the garbage collector does not collect them while the method is being called.  Use <see cref="GCHandle"/>
		/// where appropriate.
		/// <para/>
		/// If <paramref name="window"/> is -1 then the message is broadcast to all
		/// top level windows.  In this case the timeout applies to each window that is sent the message.
		/// </remarks>
		/// <seealso cref="M:PostMessage"/>
		/// <seealso cref="M:SendMessageCallback"/>
		/// <seealso cref="M:SendMessage"/>
		public static bool SendMessageTimeout ( IntPtr window, WindowMessage message, IntPtr wParam, IntPtr lParam,
												  SendMessageTimeoutOptions flags, int timeout, out IntPtr result )
		{
			//Build a structure to avoid having to deal with GC issues
			Message msg = MakeMessage(window, (int)message, wParam, lParam);
			return SendMessageTimeout(msg, flags, timeout, out result);
		}

		/// <summary>Sends a message to the specified window and waits for a response up to the specified
		/// timeout.</summary>
		/// <param name="window">The window to send the message to.</param>
		/// <param name="message">The message to send.</param>
		/// <param name="wParam">The WPARAM for the message.</param>
		/// <param name="lParam">The LPARAM for the message.</param>
		/// <param name="flags">Flags used to control the message handling.</param>
		/// <param name="timeout">The timeout, in milliseconds, for the call.</param>
		/// <param name="result">The result of the message being sent.</param>
		/// <returns>The result of the message processing from the control.</returns>
		/// <exception cref="ApplicationException">Timed out waiting for response.</exception>
		/// <exception cref="Win32Exception">An error occurred sending the message.</exception>
		/// <remarks>
		/// If the window is on the same thread as the sender then the message procedure is called directly 
		/// and the method blocks until it completes.  If the window is on a separate thread then the window procedure
		/// is called and the method blocks until either the message is handled or the timeout expires.
		/// <para/>
		/// If <paramref name="wParam"/> or <paramref name="lParam"/> are reference types then care must be taken
		/// to ensure the garbage collector does not collect them while the method is being called.  Use <see cref="GCHandle"/>
		/// where appropriate.
		/// <para/>
		/// If <paramref name="window"/> is -1 then the message is broadcast to all
		/// top level windows.  In this case the timeout applies to each window that is sent the message.
		/// </remarks>
		/// <seealso cref="M:PostMessage"/>
		/// <seealso cref="M:SendMessageCallback"/>
		/// <seealso cref="M:SendMessage"/>
		public static bool SendMessageTimeout ( IntPtr window, int message, IntPtr wParam, IntPtr lParam,
												  SendMessageTimeoutOptions flags, int timeout, out IntPtr result )
		{
			//Build a structure to avoid having to deal with GC issues
			Message msg = MakeMessage(window, message, wParam, lParam);
			return SendMessageTimeout(msg, flags, timeout, out result);
		}
		
		/// <summary>Sends a message to the specified window and waits for a response up to the specified
		/// timeout.</summary>
		/// <param name="window">The window to send the message to.</param>
		/// <param name="message">The message to send.</param>
		/// <param name="wParam">The WPARAM for the message.</param>
		/// <param name="lParam">The LPARAM for the message.</param>
		/// <param name="flags">Flags used to control the message handling.</param>
		/// <param name="timeout">The timeout, in milliseconds, for the call.</param>
		/// <param name="result">The result of the message being sent.</param>
		/// <returns>The result of the message processing from the control.</returns>
		/// <exception cref="ApplicationException">Timed out waiting for response.</exception>
		/// <exception cref="Win32Exception">An error occurred sending the message.</exception>
		/// <remarks>
		/// If the window is on the same thread as the sender then the message procedure is called directly 
		/// and the method blocks until it completes.  If the window is on a separate thread then the window procedure
		/// is called and the method blocks until either the message is handled or the timeout expires.
		/// <para/>
		/// If <paramref name="wParam"/> or <paramref name="lParam"/> are reference types then care must be taken
		/// to ensure the garbage collector does not collect them while the method is being called.  Use <see cref="GCHandle"/>
		/// where appropriate.
		/// <para/>
		/// If <paramref name="window"/> is -1 then the message is broadcast to all
		/// top level windows.  In this case the timeout applies to each window that is sent the message.
		/// </remarks>
		/// <seealso cref="M:PostMessage"/>
		/// <seealso cref="M:SendMessageCallback"/>
		/// <seealso cref="M:SendMessage"/>
		public static bool SendMessageTimeout ( IntPtr window, WindowMessage message, IntPtr wParam, IntPtr lParam,
												SendMessageTimeoutOptions flags, TimeSpan timeout, out IntPtr result )
		{
			//Build a structure to avoid having to deal with GC issues
			Message msg = MakeMessage(window, (int)message, wParam, lParam);
			return SendMessageTimeout(msg, flags, Convert.ToInt32(timeout.TotalMilliseconds), out result);
		}

		/// <summary>Sends a message to the specified window and waits for a response up to the specified
		/// timeout.</summary>
		/// <param name="window">The window to send the message to.</param>
		/// <param name="message">The message to send.</param>
		/// <param name="wParam">The WPARAM for the message.</param>
		/// <param name="lParam">The LPARAM for the message.</param>
		/// <param name="flags">Flags used to control the message handling.</param>
		/// <param name="timeout">The timeout, in milliseconds, for the call.</param>
		/// <param name="result">The result of the message being sent.</param>
		/// <returns>The result of the message processing from the control.</returns>
		/// <exception cref="ApplicationException">Timed out waiting for response.</exception>
		/// <exception cref="Win32Exception">An error occurred sending the message.</exception>
		/// <remarks>
		/// If the window is on the same thread as the sender then the message procedure is called directly 
		/// and the method blocks until it completes.  If the window is on a separate thread then the window procedure
		/// is called and the method blocks until either the message is handled or the timeout expires.
		/// <para/>
		/// If <paramref name="wParam"/> or <paramref name="lParam"/> are reference types then care must be taken
		/// to ensure the garbage collector does not collect them while the method is being called.  Use <see cref="GCHandle"/>
		/// where appropriate.
		/// <para/>
		/// If <paramref name="window"/> is -1 then the message is broadcast to all
		/// top level windows.  In this case the timeout applies to each window that is sent the message.
		/// </remarks>
		/// <seealso cref="M:PostMessage"/>
		/// <seealso cref="M:SendMessageCallback"/>
		/// <seealso cref="M:SendMessage"/>
		public static bool SendMessageTimeout ( IntPtr window, int message, IntPtr wParam, IntPtr lParam,
												SendMessageTimeoutOptions flags, TimeSpan timeout, out IntPtr result )
		{
			//Build a structure to avoid having to deal with GC issues
			Message msg = MakeMessage(window, message, wParam, lParam);
			return SendMessageTimeout(msg, flags, Convert.ToInt32(timeout.TotalMilliseconds), out result);
		}

		/// <summary>Sends a message to the specified control and waits for a response up to the specified
		/// timeout.</summary>
		/// <param name="message">The message to send.</param>
		/// <param name="flags">Flags used to control the message handling.</param>
		/// <param name="timeout">The timeout, in milliseconds, for the call.</param>
		/// <param name="result">The result of the message being sent.</param>
		/// <returns>The result of the message processing from the control.</returns>
		/// <exception cref="ApplicationException">Timed out waiting for response.</exception>
		/// <exception cref="Win32Exception">An error occurred sending the message.</exception>
		/// <remarks>
		/// If the window is on the same thread as the sender then the message procedure is called directly 
		/// and the method blocks until it completes.  If the window is on a separate thread then the window procedure
		/// is called and the method blocks until either the message is handled or the timeout expires.
		/// <para/>
		/// If <b>wParam</b> or <b>lParam</b> are reference types then care must be taken
		/// to ensure the garbage collector does not collect them while the method is being called.  Use <see cref="GCHandle"/>
		/// where appropriate.
		/// <para/>
		/// If the window is -1 then the message is broadcast to all
		/// top level windows.  In this case the timeout applies to each window that is sent the message.
		/// </remarks>
		/// <seealso cref="M:PostMessage"/>
		/// <seealso cref="M:SendMessageCallback"/>
		/// <seealso cref="M:SendMessage"/>
		public static bool SendMessageTimeout ( Message message, SendMessageTimeoutOptions flags, int timeout, out IntPtr result )
		{
			if (InteropSendMessageTimeout(message.HWnd, message.Msg, message.WParam, message.LParam,
									      (uint)flags, (uint)timeout, out result))
				return true;

			int nRes = Marshal.GetLastWin32Error();
			if (nRes != 0)
				throw new Win32Exception(nRes);

			//Follow ReaderWriterLock format of exception
			throw new ApplicationException("Timeout waiting for message to be sent.");
		}

		/// <summary>Sends a message to the specified control and waits for a response up to the specified
		/// timeout.</summary>
		/// <param name="message">The message to send.</param>
		/// <param name="flags">Flags used to control the message handling.</param>
		/// <param name="timeout">The timeout, in milliseconds, for the call.</param>
		/// <param name="result">The result of the message being sent.</param>
		/// <returns>The result of the message processing from the control.</returns>
		/// <exception cref="ApplicationException">Timed out waiting for response.</exception>
		/// <exception cref="Win32Exception">An error occurred sending the message.</exception>
		/// <remarks>
		/// If the window is on the same thread as the sender then the message procedure is called directly 
		/// and the method blocks until it completes.  If the window is on a separate thread then the window procedure
		/// is called and the method blocks until either the message is handled or the timeout expires.
		/// <para/>
		/// If <b>wParam</b> or <b>lParam</b> are reference types then care must be taken
		/// to ensure the garbage collector does not collect them while the method is being called.  Use <see cref="GCHandle"/>
		/// where appropriate.
		/// <para/>
		/// If the window is -1 then the message is broadcast to all
		/// top level windows.  In this case the timeout applies to each window that is sent the message.
		/// </remarks>
		/// <seealso cref="M:PostMessage"/>
		/// <seealso cref="M:SendMessageCallback"/>
		/// <seealso cref="M:SendMessage"/>
		public static bool SendMessageTimeout ( Message message, SendMessageTimeoutOptions flags, TimeSpan timeout, out IntPtr result )
		{ return SendMessageTimeout(message, flags, Convert.ToInt32(timeout.TotalMilliseconds), out result); }
		#endregion

		#region SendNotifyMessage

		/// <summary>Sends a message to the specified control.</summary>
		/// <param name="control">The control to send the message to.</param>
		/// <param name="message">The message to send.</param>
		/// <param name="wParam">The WPARAM for the message.</param>
		/// <param name="lParam">The LPARAM for the message.</param>
		/// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
		/// <exception cref="Win32Exception">An error occurred sending the message.</exception>
		/// <remarks>
		/// If the control is on the same thread as the sender then the message procedure is called directly 
		/// and the method blocks.  If the control is on a separate thread then the message is sent to the window
		/// procedure and the method returns.
		/// <para/>
		/// If <paramref name="wParam"/> or <paramref name="lParam"/> are reference types then care must be taken
		/// to ensure the garbage collector does not collect them while the method is being called.  Use <see cref="GCHandle"/>
		/// where appropriate.
		/// <para/>
		/// If <paramref name="control"/> is <see langword="null"/> then the message is broadcast to all
		/// top level windows.
		/// </remarks>
		/// <seealso cref="M:SendMessage"/>
		public static bool SendNotifyMessage ( Control control, WindowMessage message, IntPtr wParam, IntPtr lParam )
		{
			//Build a structure to avoid having to deal with GC issues
			Message msg = MakeMessage(control, (int)message, wParam, lParam);
			return SendNotifyMessage(msg);
		}

		/// <summary>Sends a message to the specified control.</summary>
		/// <param name="control">The control to send the message to.</param>
		/// <param name="message">The message to send.</param>
		/// <param name="wParam">The WPARAM for the message.</param>
		/// <param name="lParam">The LPARAM for the message.</param>
		/// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
		/// <exception cref="Win32Exception">An error occurred sending the message.</exception>
		/// <remarks>
		/// If the control is on the same thread as the sender then the message procedure is called directly 
		/// and the method blocks.  If the control is on a separate thread then the message is sent to the window
		/// procedure and the method returns.
		/// <para/>
		/// If <paramref name="wParam"/> or <paramref name="lParam"/> are reference types then care must be taken
		/// to ensure the garbage collector does not collect them while the method is being called.  Use <see cref="GCHandle"/>
		/// where appropriate.
		/// <para/>
		/// If <paramref name="control"/> is <see langword="null"/> then the message is broadcast to all
		/// top level windows.
		/// </remarks>
		/// <seealso cref="M:SendMessage"/>
		public static bool SendNotifyMessage ( Control control, int message, IntPtr wParam, IntPtr lParam )
		{
			//Build a structure to avoid having to deal with GC issues
			Message msg = MakeMessage(control, message, wParam, lParam);
			return SendNotifyMessage(msg);
		}

		/// <summary>Sends a message to the specified control.</summary>
		/// <param name="window">The window to send the message to.</param>
		/// <param name="message">The message to send.</param>
		/// <param name="wParam">The WPARAM for the message.</param>
		/// <param name="lParam">The LPARAM for the message.</param>
		/// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
		/// <exception cref="Win32Exception">An error occurred sending the message.</exception>
		/// <remarks>
		/// If the control is on the same thread as the sender then the message procedure is called directly 
		/// and the method blocks.  If the control is on a separate thread then the message is sent to the window
		/// procedure and the method returns.
		/// <para/>
		/// If <paramref name="wParam"/> or <paramref name="lParam"/> are reference types then care must be taken
		/// to ensure the garbage collector does not collect them while the method is being called.  Use <see cref="GCHandle"/>
		/// where appropriate.
		/// <para/>
		/// If <paramref name="window"/> is -1 then the message is broadcast to all top level windows.
		/// </remarks>
		/// <seealso cref="M:SendMessage"/>
		public static bool SendNotifyMessage ( IntPtr window, WindowMessage message, IntPtr wParam, IntPtr lParam )
		{
			//Build a structure to avoid having to deal with GC issues
			Message msg = MakeMessage(window, (int)message, wParam, lParam);
			return SendNotifyMessage(msg);
		}

		/// <summary>Sends a message to the specified control.</summary>
		/// <param name="window">The window to send the message to.</param>
		/// <param name="message">The message to send.</param>
		/// <param name="wParam">The WPARAM for the message.</param>
		/// <param name="lParam">The LPARAM for the message.</param>
		/// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
		/// <exception cref="Win32Exception">An error occurred sending the message.</exception>
		/// <remarks>
		/// If the control is on the same thread as the sender then the message procedure is called directly 
		/// and the method blocks.  If the control is on a separate thread then the message is sent to the window
		/// procedure and the method returns.
		/// <para/>
		/// If <paramref name="wParam"/> or <paramref name="lParam"/> are reference types then care must be taken
		/// to ensure the garbage collector does not collect them while the method is being called.  Use <see cref="GCHandle"/>
		/// where appropriate.
		/// <para/>
		/// If <paramref name="window"/> is -1 then the message is broadcast to all top level windows.
		/// </remarks>
		/// <seealso cref="M:SendMessage"/>
		public static bool SendNotifyMessage ( IntPtr window, int message, IntPtr wParam, IntPtr lParam )
		{
			//Build a structure to avoid having to deal with GC issues
			Message msg = MakeMessage(window, message, wParam, lParam);
			return SendNotifyMessage(msg);
		}

		/// <summary>Sends a message to the specified control.</summary>
		/// <param name="message">The message to send.</param>
		/// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
		/// <exception cref="Win32Exception">An error occurred sending the message.</exception>
		/// <remarks>
		/// If the control is on the same thread as the sender then the message procedure is called directly 
		/// and the method blocks.  If the control is on a separate thread then the message is sent to the window
		/// procedure and the method returns.
		/// <para/>
		/// If the window is -1 then the message is broadcast to all top level windows.
		/// </remarks>
		/// <seealso cref="M:SendMessage"/>
		public static bool SendNotifyMessage ( Message message )
		{
			if (InteropSendNotifyMessage(message.HWnd, message.Msg, message.WParam, message.LParam))
				return true;

			int nRes = Marshal.GetLastWin32Error();
			if (nRes != 0)
				throw new Win32Exception(nRes);

			return false;
		}
		#endregion

		#endregion

		#endregion //Public Members

		#region Private Members

		#region Methods

		//Used to create a message structure to help avoid the issues with GC
		private static Message MakeMessage ( Control control, int message, IntPtr wParam, IntPtr lParam )
		{
			Message msg = new Message();
			msg.HWnd = (control != null) ? control.Handle : new IntPtr(-1);
			msg.Msg = message;
			msg.WParam = wParam;
			msg.LParam = lParam;

			return msg;
		}

		//Used to create a message structure to help avoid the issues with GC
		private static Message MakeMessage ( IntPtr handle, int message, IntPtr wParam, IntPtr lParam )
		{
			Message msg = new Message();
			msg.HWnd = handle;
			msg.Msg = message;
			msg.WParam = wParam;
			msg.LParam = lParam;

			return msg;
		}
		#endregion

		#region Imports

		[DllImport("user32.dll", EntryPoint = "PostMessage", SetLastError = true)]
		private static extern bool InteropPostMessage ( IntPtr window, int message, IntPtr wParam, IntPtr lParam );

		[DllImport("user32.dll", EntryPoint = "PostThreadMessage", SetLastError = true)]
		private static extern bool InteropPostThreadMessage ( uint threadId, int message, IntPtr wParam, IntPtr lParam );

		[DllImport("user32.dll", EntryPoint = "SendMessage")]
		private static extern IntPtr InteropSendMessage ( IntPtr window, int message, IntPtr wParam, IntPtr lParam );

		[DllImport("user32.dll", EntryPoint = "SendMessageCallback", SetLastError = true)]
		private static extern bool InteropSendMessageCallback ( IntPtr window, int message, IntPtr wParam, IntPtr lParam,
																SendAsyncCallback callback, IntPtr dwData );

		[DllImport("user32.dll", EntryPoint = "SendMessageTimeout", SetLastError = true)]
		private static extern bool InteropSendMessageTimeout ( IntPtr window, int message, IntPtr wParam, IntPtr lParam,
															   uint flags, uint timeout, out IntPtr result );

		[DllImport("user32.dll", EntryPoint = "SendNotifyMessage", SetLastError = true)]
		private static extern bool InteropSendNotifyMessage ( IntPtr window, int message, IntPtr wParam, IntPtr lParam );
		#endregion

		#endregion //Private Members
	}
}
