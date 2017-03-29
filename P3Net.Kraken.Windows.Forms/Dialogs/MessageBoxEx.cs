/*
 * Provides extended support for message boxes.
 *
 * Copyright © 2005 Michael Taylor ($COMPANY$)
 * All Rights Reserved
 * 
 * $Header: $
 */
#region Imports

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows.Forms;

using P3Net.Kraken.Diagnostics;
#endregion

namespace P3Net.Kraken.WinForms
{
    /// <summary>Provides extended support for message boxes.</summary>
    /// <remarks>
    /// <see cref="MessageBoxEx"/> provides a mirror image of <see cref="MessageBox"/> with 
    /// some additional functionality.  <see cref="MessageBoxEx"/> contains properties to 
    /// specify the default values for many of the message box parameters.  Applications should use
    /// these properties to set the application-wide defaults for message boxes.  Every default value
    /// can be overridden as needed without changing the application-wide defaults.  By using default values
    /// a consistent user interface is presented to the user while keeping maintenance at a minimum.  In all
    /// cases it is recommended that explicit values be used when invoking a message box when the assumed defaults
    /// are not accurate.  For example an error message box should always specify the error icon instead of assuming
    /// that the application-wide settings will suffice.
    /// </remarks>
    [CodeNotTested]
    [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase")]
    public static class MessageBoxEx
    {
        #region Construction

        static MessageBoxEx ()
        {
            DefaultButtons = MessageBoxButtons.OK;
            DefaultIcon = MessageBoxIcon.None;
        }
        #endregion

        #region Public Members

        #region Attributes

        /// <summary>Gets or sets the default buttons to display in the message box.</summary>
        /// <value>The default icon is <see cref="MessageBoxButtons.OK"/>.</value>
        public static MessageBoxButtons DefaultButtons { get; set; }

        /// <summary>Gets or sets the default icon to display in the message box.</summary>
        /// <value>The default icon is <see cref="MessageBoxIcon.None"/>.</value>
        /// <remarks>
        /// In general this property should be left at its default.  For applications that normally only
        /// display errors, warnings or other specific messages this can be changed to save some time.  It
        /// is recommended that an icon always be specified when calling one of the <see cref="M:Show"/> methods
        /// when displaying errors, warnings and questions.
        /// </remarks>
        public static MessageBoxIcon DefaultIcon { get; set; }

        /// <summary>Gets or sets the default owner for the message box.</summary>
        /// <value>The default is <see langword="null"/>.</value>
        /// <remarks>
        /// Applications should set this property to the main window.  The owner should be explicitly
        /// specified when a window other than the main window should be used, such as in child forms.
        /// </remarks>
        public static IWin32Window DefaultOwner { get; set; }

        /// <summary>Gets or sets the default title to use for the message box.</summary>
        /// <value>The default is an empty string.</value>
        /// <remarks>
        /// If no title is specified then the title will be based on the type of message displayed.  Normally
        /// this property should be set to the application's name.
        /// </remarks>
        public static string DefaultTitle
        {
            get { return m_strDefTitle; }
            set { m_strDefTitle = (value != null) ? value : ""; }
        }
        #endregion

        #region Methods

        #region DisplayError

        /// <summary>Displays an error message box.</summary>
        /// <param name="message">The message to display.</param>
        /// <remarks>
        /// The message box will use the <see cref="MessageBoxIcon.Error"/> icon and have an <b>OK</b> button.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				try
        ///				{
        ///					throw new Exception("An unknown error occurred.");
        ///				} catch (Exception e)
        ///				{
        ///					MessageBoxEx.DisplayError(e.Message);
        ///				};
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( )
        ///			
        ///				Try
        ///				
        ///					Throw New Exception("An unknown error occurred.")
        ///				Catch e As Exception
        ///				
        ///					MessageBoxEx.DisplayError(e.Message)
        ///				End Try
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static void DisplayError ( string message )
        { DisplayError(DefaultOwner, message, DefaultTitle); }

        /// <summary>Displays an error message box.</summary>
        /// <param name="owner">The owner of the message box.</param>
        /// <param name="message">The message to display.</param>
        /// <remarks>
        /// The message box will use the <see cref="MessageBoxIcon.Error"/> icon and have an <b>OK</b> button.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				try
        ///				{
        ///					throw new Exception(null, "An unknown error occurred.");
        ///				} catch (Exception e)
        ///				{
        ///					MessageBoxEx.DisplayError(e.Message);
        ///				};
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( )
        ///			
        ///				Try
        ///				
        ///					Throw New Exception(Nothing, "An unknown error occurred.")
        ///				Catch e As Exception
        ///				
        ///					MessageBoxEx.DisplayError(e.Message)
        ///				End Try
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static void DisplayError ( IWin32Window owner, string message )
        { DisplayError(owner, message, DefaultTitle); }

        /// <summary>Displays an error message box.</summary>
        /// <param name="error">The exception to display.</param>
        /// <remarks>
        /// The message box will use the <see cref="MessageBoxIcon.Error"/> icon and have an <b>OK</b> button.
        /// <para/>
        /// Only the <see cref="Exception.Message"/> value is displayed.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				try
        ///				{
        ///					throw new Exception("An unknown error occurred.");
        ///				} catch (Exception e)
        ///				{
        ///					MessageBoxEx.DisplayError(e);
        ///				};
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( )
        ///			
        ///				Try
        ///				
        ///					Throw New Exception("An unknown error occurred.")
        ///				Catch e As Exception
        ///				
        ///					MessageBoxEx.DisplayError(e)
        ///				End Try
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static void DisplayError ( Exception error )
        {
            Verify.Argument("error", error).IsNotNull();

            DisplayError(DefaultOwner, error.Message, DefaultTitle); 
        }

        /// <summary>Displays an error message box.</summary>
        /// <param name="owner">The owner of the message box.</param>
        /// <param name="error">The exception to display.</param>
        /// <remarks>
        /// The message box will use the <see cref="MessageBoxIcon.Error"/> icon and have an <b>OK</b> button.
        /// <para/>
        /// Only the <see cref="Exception.Message"/> value is displayed.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				try
        ///				{
        ///					throw new Exception("An unknown error occurred.");
        ///				} catch (Exception e)
        ///				{
        ///					MessageBoxEx.DisplayError(null, e);
        ///				};
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( )
        ///			
        ///				Try
        ///				
        ///					Throw New Exception("An unknown error occurred.")
        ///				Catch e As Exception
        ///				
        ///					MessageBoxEx.DisplayError(Nothing, e)
        ///				End Try
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static void DisplayError ( IWin32Window owner, Exception error )
        {
            Verify.Argument("error", error).IsNotNull();

            DisplayError(owner, error.Message, DefaultTitle); 
        }

        /// <summary>Displays an error message box.</summary>
        /// <param name="message">The message to display.</param>
        /// <param name="title">The title to display.</param>
        /// <remarks>
        /// The message box will use the <see cref="MessageBoxIcon.Error"/> icon and have an <b>OK</b> button.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				try
        ///				{
        ///					throw new Exception("An unknown error occurred.");
        ///				} catch (Exception e)
        ///				{
        ///					MessageBoxEx.DisplayError(e.Message, "Error");
        ///				};
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( )
        ///			
        ///				Try
        ///				
        ///					Throw New Exception("An unknown error occurred.")
        ///				Catch e As Exception
        ///				
        ///					MessageBoxEx.DisplayError(e.Message, "Error")
        ///				End Try
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static void DisplayError ( string message, string title )
        { DisplayError(DefaultOwner, message, title); }
                
        /// <summary>Displays an error message box.</summary>
        /// <param name="error">The exception to display.</param>
        /// <param name="title">The title to display.</param>
        /// <remarks>
        /// The message box will use the <see cref="MessageBoxIcon.Error"/> icon and have an <b>OK</b> button.
        /// <para/>
        /// Only the <see cref="Exception.Message"/> value is displayed.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				try
        ///				{
        ///					throw new Exception("An unknown error occurred.");
        ///				} catch (Exception e)
        ///				{
        ///					MessageBoxEx.DisplayError(e, "Error");
        ///				};
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( )
        ///			
        ///				Try
        ///				
        ///					Throw New Exception("An unknown error occurred.")
        ///				Catch e As Exception
        ///				
        ///					MessageBoxEx.DisplayError(e, "Error")
        ///				End Try
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static void DisplayError ( Exception error, string title )
        {
            Verify.Argument("error", error).IsNotNull();

            DisplayError(DefaultOwner, error.Message, title); 
        }

        /// <summary>Displays an error message box.</summary>
        /// <param name="owner">The owner of the message box.</param>
        /// <param name="error">The exception to display.</param>
        /// <param name="title">The title to display.</param>
        /// <remarks>
        /// The message box will use the <see cref="MessageBoxIcon.Error"/> icon and have an <b>OK</b> button.
        /// <para/>
        /// Only the <see cref="Exception.Message"/> value is displayed.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				try
        ///				{
        ///					throw new Exception("An unknown error occurred.");
        ///				} catch (Exception e)
        ///				{
        ///					MessageBoxEx.DisplayError(null, e, "Error");
        ///				};
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( )
        ///			
        ///				Try
        ///				
        ///					Throw New Exception("An unknown error occurred.")
        ///				Catch e As Exception
        ///				
        ///					MessageBoxEx.DisplayError(Nothing, e, "Error")
        ///				End Try
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static void DisplayError ( IWin32Window owner, Exception error, string title )
        {
            Verify.Argument("error", error).IsNotNull();
            DisplayError(owner, error.Message, title); 
        }

        /// <summary>Displays an error message box.</summary>
        /// <param name="owner">The owner of the message box.</param>
        /// <param name="message">The message to display.</param>
        /// <param name="title">The title to display.</param>
        /// <remarks>
        /// The message box will use the <see cref="MessageBoxIcon.Error"/> icon and have an <b>OK</b> button.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				try
        ///				{
        ///					throw new Exception("An unknown error occurred.");
        ///				} catch (Exception e)
        ///				{
        ///					MessageBoxEx.DisplayError(null, e.Message, "Error");
        ///				};
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( )
        ///			
        ///				Try
        ///				
        ///					Throw New Exception("An unknown error occurred.")
        ///				Catch e As Exception
        ///				
        ///					MessageBoxEx.DisplayError(Nothing, e.Message, "Error")
        ///				End Try
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static void DisplayError ( IWin32Window owner, string message, string title )
        {
            MessageBoxEx.Show(owner, message, title, MessageBoxButtons.OK, MessageBoxIcon.Error); 
        }
        #endregion

        #region DisplayQuestion

        /// <summary>Displays a message box containing a question.</summary>
        /// <param name="message">The message to display.</param>
        /// <returns><see langword="true"/> if the user clicked <b>Yes</b> or <see langword="false"/> otherwise.</returns>
        /// <remarks>
        /// The icon is set to <see cref="MessageBoxIcon.Question"/> and the button is set to <see cref="MessageBoxButtons.YesNo"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					if (!MessageBoxEx.DisplayQuestion("Do you want to enter some values?"))
        ///						return;
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					If Not MessageBoxEx.DisplayQuestion("Do you want to enter some values?") Then
        ///						Exit Sub
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static bool DisplayQuestion ( string message )
        { return DisplayQuestion(DefaultOwner, message, DefaultTitle, false) == DialogResult.Yes; }

        /// <summary>Displays a message box containing a question.</summary>
        /// <param name="owner">The window owning the message box.</param>
        /// <param name="message">The message to display.</param>
        /// <returns><see langword="true"/> if the user clicked <b>Yes</b> or <see langword="false"/> otherwise.</returns>
        /// <remarks>
        /// The icon is set to <see cref="MessageBoxIcon.Question"/> and the button is set to <see cref="MessageBoxButtons.YesNo"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					if (!MessageBoxEx.DisplayQuestion(null, "Do you want to enter some values?"))
        ///						return;
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					If Not MessageBoxEx.DisplayQuestion(Nothing, "Do you want to enter some values?") Then
        ///						Exit Sub
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static bool DisplayQuestion ( IWin32Window owner, string message )
        { return DisplayQuestion(owner, message, DefaultTitle, false) == DialogResult.Yes; }

        /// <summary>Displays a message box containing a question.</summary>
        /// <param name="message">The message to display.</param>
        /// <param name="showCancel"><see langword="true"/> to show the <b>Cancel</b> button.</param>
        /// <returns>The results of the invocation.</returns>
        /// <remarks>
        /// The icon is set to <see cref="MessageBoxIcon.Question"/>.  If <paramref name="showCancel"/> is <see langword="true"/> then
        /// the button is set to <see cref="MessageBoxButtons.YesNoCancel"/> otherwise it is set to <see cref="MessageBoxButtons.YesNo"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					if (MessageBoxEx.DisplayQuestion("Do you want to enter some values?", true) == DialogResult.No)
        ///						return;
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					If MessageBoxEx.DisplayQuestion("Do you want to enter some values?", True) = DialogResult.No Then
        ///						Exit Sub
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static DialogResult DisplayQuestion ( string message, bool showCancel )
        { return DisplayQuestion(DefaultOwner, message, DefaultTitle, showCancel); }

        /// <summary>Displays a message box containing a question.</summary>
        /// <param name="owner">The window owning the message box.</param>
        /// <param name="message">The message to display.</param>
        /// <param name="showCancel"><see langword="true"/> to show the <b>Cancel</b> button.</param>
        /// <returns>The results of the invocation.</returns>
        /// <remarks>
        /// The icon is set to <see cref="MessageBoxIcon.Question"/>.  If <paramref name="showCancel"/> is <see langword="true"/> then
        /// the button is set to <see cref="MessageBoxButtons.YesNoCancel"/> otherwise it is set to <see cref="MessageBoxButtons.YesNo"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					if (MessageBoxEx.DisplayQuestion(null, "Do you want to enter some values?", true) == DialogResult.No)
        ///						return;
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					If MessageBoxEx.DisplayQuestion(Nothing, "Do you want to enter some values?", True) = DialogResult.No Then
        ///						Exit Sub
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static DialogResult DisplayQuestion ( IWin32Window owner, string message, bool showCancel )
        { return DisplayQuestion(owner, message, DefaultTitle, showCancel); }

        /// <summary>Displays a message box containing a question.</summary>
        /// <param name="message">The message to display.</param>
        /// <param name="title">The title to display.</param>
        /// <returns><see langword="true"/> if the user clicked <b>Yes</b> or <see langword="false"/> otherwise.</returns>
        /// <remarks>
        /// The icon is set to <see cref="MessageBoxIcon.Question"/> and the button is set to <see cref="MessageBoxButtons.YesNo"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					if (!MessageBoxEx.DisplayQuestion("Do you want to enter some values?", "Question"))
        ///						return;
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					If Not MessageBoxEx.DisplayQuestion("Do you want to enter some values?", "Question") Then
        ///						Exit Sub
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static bool DisplayQuestion ( string message, string title )
        { return DisplayQuestion(DefaultOwner, message, title, false) == DialogResult.Yes; }

        /// <summary>Displays a message box containing a question.</summary>
        /// <param name="owner">The window owning the message box.</param>
        /// <param name="message">The message to display.</param>
        /// <param name="title">The title to display.</param>
        /// <returns><see langword="true"/> if the user clicked <b>Yes</b> or <see langword="false"/> otherwise.</returns>
        /// <remarks>
        /// The icon is set to <see cref="MessageBoxIcon.Question"/> and the button is set to <see cref="MessageBoxButtons.YesNo"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					if (!MessageBoxEx.DisplayQuestion(null, "Do you want to enter some values?", "Question")
        ///						return;
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					If Not MessageBoxEx.DisplayQuestion(Nothing, "Do you want to enter some values?", "Question") Then
        ///						Exit Sub
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static bool DisplayQuestion ( IWin32Window owner, string message, string title )
        { return DisplayQuestion(owner, message, title, false) == DialogResult.Yes; }

        /// <summary>Displays a message box containing a question.</summary>
        /// <param name="message">The message to display.</param>
        /// <param name="title">The title to display.</param>
        /// <param name="showCancel"><see langword="true"/> to show the <b>Cancel</b> button.</param>
        /// <returns>The results of the invocation.</returns>
        /// <remarks>
        /// The icon is set to <see cref="MessageBoxIcon.Question"/>.  If <paramref name="showCancel"/> is <see langword="true"/> then
        /// the button is set to <see cref="MessageBoxButtons.YesNoCancel"/> otherwise it is set to <see cref="MessageBoxButtons.YesNo"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					if (MessageBoxEx.DisplayQuestion("Do you want to enter some values?", "Question", true) == DialogResult.No)
        ///						return;
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					If MessageBoxEx.DisplayQuestion("Do you want to enter some values?", "Question", True) = DialogResult.No Then
        ///						Exit Sub
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static DialogResult DisplayQuestion ( string message, string title, bool showCancel )
        { return DisplayQuestion(DefaultOwner, message, title, showCancel); }		

        /// <summary>Displays a message box containing a question.</summary>
        /// <param name="owner">The window owning the message box.</param>
        /// <param name="message">The message to display.</param>
        /// <param name="title">The title to display.</param>
        /// <param name="showCancel"><see langword="true"/> to show the <b>Cancel</b> button.</param>
        /// <returns>The results of the invocation.</returns>
        /// <remarks>
        /// The icon is set to <see cref="MessageBoxIcon.Question"/>.  If <paramref name="showCancel"/> is <see langword="true"/> then
        /// the button is set to <see cref="MessageBoxButtons.YesNoCancel"/> otherwise it is set to <see cref="MessageBoxButtons.YesNo"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					if (MessageBoxEx.DisplayQuestion(null, "Do you want to enter some values?", "Question", true) == DialogResult.No)
        ///						return;
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					If MessageBoxEx.DisplayQuestion(Nothing, "Do you want to enter some values?", "Question", True) = DialogResult.No Then
        ///						Exit Sub
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static DialogResult DisplayQuestion ( IWin32Window owner, string message, string title, bool showCancel )
        {
            MessageBoxButtons btns = showCancel ? MessageBoxButtons.YesNoCancel : MessageBoxButtons.YesNo;
            return MessageBoxEx.Show(owner, message, title, btns, MessageBoxIcon.Question); 
        }		
        #endregion

        #region DisplayWarning

        /// <summary>Displays a warning message box.</summary>
        /// <param name="message">The message to display.</param>
        /// <remarks>
        /// The message box will use the <see cref="MessageBoxIcon.Warning"/> icon and have an <b>OK</b> button.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					MessageBoxEx.DisplayWarning("No arguments were specified.");
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					MessageBoxEx.DisplayWarning("No arguments were specified.")
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static void DisplayWarning ( string message )
        { DisplayWarning(DefaultOwner, message, DefaultTitle); }

        /// <summary>Displays a warning message box.</summary>
        /// <param name="owner">The owner of the message box.</param>
        /// <param name="message">The message to display.</param>
        /// <remarks>
        /// The message box will use the <see cref="MessageBoxIcon.Warning"/> icon and have an <b>OK</b> button.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					MessageBoxEx.DisplayWarning(null, "No arguments were specified.");
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					MessageBoxEx.DisplayWarning(Nothing, "No arguments were specified.")
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static void DisplayWarning ( IWin32Window owner, string message )
        { DisplayWarning(owner, message, DefaultTitle); }

        /// <summary>Displays a warning message box.</summary>
        /// <param name="message">The message to display.</param>
        /// <param name="title">The title to display.</param>
        /// <remarks>
        /// The message box will use the <see cref="MessageBoxIcon.Warning"/> icon and have an <b>OK</b> button.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					MessageBoxEx.DisplayWarning("No arguments were specified.", "Warning");
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					MessageBoxEx.DisplayWarning("No arguments were specified.", "Warning")
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static void DisplayWarning ( string message, string title )
        { DisplayWarning(DefaultOwner, message, title); }

        /// <summary>Displays a warning message box.</summary>
        /// <param name="owner">The owner of the message box.</param>
        /// <param name="message">The message to display.</param>
        /// <param name="title">The title to display.</param>
        /// <remarks>
        /// The message box will use the <see cref="MessageBoxIcon.Warning"/> icon and have an <b>OK</b> button.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					MessageBoxEx.DisplayWarning(null, "No arguments were specified.", "Warning");
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					MessageBoxEx.DisplayWarning(Nothing, "No arguments were specified.", "Warning")
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static void DisplayWarning ( IWin32Window owner, string message, string title )
        { MessageBoxEx.Show(owner, message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        #endregion

        #region Show

        /// <summary>Displays a message box.</summary>
        /// <param name="message">The message to display.</param>
        /// <returns>The results of the invocation.</returns>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					MessageBoxEx.Show("No arguments were specified.");
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					MessageBoxEx.Show("No arguments were specified.")
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static DialogResult Show ( string message )
        { 
            return MessageBox.Show(DefaultOwner, message, DefaultTitle, DefaultButtons, DefaultIcon,
                                   MessageBoxDefaultButton.Button1, GetOptions(DefaultOwner)); 
        }

        /// <summary>Displays a message box.</summary>
        /// <param name="message">The message to display.</param>
        /// <param name="title">The title to use.</param>
        /// <returns>The results of the invocation.</returns>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					MessageBoxEx.Show("No arguments were specified.", "Warning");
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					MessageBoxEx.Show("No arguments were specified.", "Warning")
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static DialogResult Show ( string message, string title )
        { 
            return MessageBox.Show(DefaultOwner, message, title, DefaultButtons, DefaultIcon,
                                   MessageBoxDefaultButton.Button1, GetOptions(DefaultOwner)); 
        }

        /// <summary>Displays a message box.</summary>
        /// <param name="message">The message to display.</param>		
        /// <param name="buttons">The buttons to display on the message box.</param>
        /// <returns>The results of the invocation.</returns>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					MessageBoxEx.Show("No arguments were specified.", MessageBoxButtons.OK);
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					MessageBoxEx.Show("No arguments were specified.", MessageBoxButtons.OK)
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static DialogResult Show ( string message, MessageBoxButtons buttons )
        {
            return MessageBox.Show(DefaultOwner, message, DefaultTitle, buttons, DefaultIcon,
                                   MessageBoxDefaultButton.Button1, GetOptions(DefaultOwner)); 
        }

        /// <summary>Displays a message box.</summary>
        /// <param name="message">The message to display.</param>		
        /// <param name="title">The title to display.</param>
        /// <param name="buttons">The buttons to display on the message box.</param>
        /// <returns>The results of the invocation.</returns>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					MessageBoxEx.Show("No arguments were specified.", "Warning", MessageBoxButtons.OK);
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					MessageBoxEx.Show("No arguments were specified.", "Warning", MessageBoxButtons.OK)
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static DialogResult Show ( string message, string title, MessageBoxButtons buttons )
        {
            return MessageBox.Show(DefaultOwner, message, title, buttons, DefaultIcon,
                                   MessageBoxDefaultButton.Button1, GetOptions(DefaultOwner)); 
        }

        /// <summary>Displays a message box.</summary>
        /// <param name="message">The message to display.</param>		
        /// <param name="icon">The icon to display on the message box.</param>
        /// <returns>The results of the invocation.</returns>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					MessageBoxEx.Show("No arguments were specified.", MessageBoxIcon.Warning);
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					MessageBoxEx.Show("No arguments were specified.", MessageBoxIcon.Warning)
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static DialogResult Show ( string message, MessageBoxIcon icon )
        {
            return MessageBox.Show(DefaultOwner, message, DefaultTitle, DefaultButtons, icon,
                                   MessageBoxDefaultButton.Button1, GetOptions(DefaultOwner)); 
        }

        /// <summary>Displays a message box.</summary>
        /// <param name="message">The message to display.</param>		
        /// <param name="title">The title to display.</param>
        /// <param name="icon">The icon to display on the message box.</param>
        /// <returns>The results of the invocation.</returns>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					MessageBoxEx.Show("No arguments were specified.", "Warning", MessageBoxIcon.Warning);
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					MessageBoxEx.Show("No arguments were specified.", "Warning", MessageBoxIcon.Warning)
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static DialogResult Show ( string message, string title, MessageBoxIcon icon )
        {
            return MessageBox.Show(DefaultOwner, message, title, DefaultButtons, icon,
                                   MessageBoxDefaultButton.Button1, GetOptions(DefaultOwner)); 
        }

        /// <summary>Displays a message box.</summary>
        /// <param name="message">The message to display.</param>		
        /// <param name="buttons">The buttons to display on the message box.</param>
        /// <param name="icon">The icon to display on the message box.</param>
        /// <returns>The results of the invocation.</returns>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					MessageBoxEx.Show("No arguments were specified.", MessageBoxButtons.Ok, MessageBoxIcon.Warning);
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					MessageBoxEx.Show("No arguments were specified.", MessageBoxButtons.Ok, MessageBoxIcon.Warning)
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static DialogResult Show ( string message, MessageBoxButtons buttons, MessageBoxIcon icon )
        {
            return MessageBox.Show(DefaultOwner, message, DefaultTitle, buttons, icon,
                                   MessageBoxDefaultButton.Button1, GetOptions(DefaultOwner)); 
        }

        /// <summary>Displays a message box.</summary>
        /// <param name="message">The message to display.</param>		
        /// <param name="title">The title to display.</param>
        /// <param name="buttons">The buttons to display on the message box.</param>
        /// <param name="icon">The icon to display on the message box.</param>
        /// <returns>The results of the invocation.</returns>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					MessageBoxEx.Show("No arguments were specified.", "Warning", MessageBoxButtons.Ok, MessageBoxIcon.Warning);
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					MessageBoxEx.Show("No arguments were specified.", "Warning", MessageBoxButtons.Ok, MessageBoxIcon.Warning)
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static DialogResult Show ( string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon )
        {
            return MessageBox.Show(DefaultOwner, message, title, buttons, icon,
                                   MessageBoxDefaultButton.Button1, GetOptions(DefaultOwner)); 
        }

        /// <summary>Displays a message box.</summary>
        /// <param name="message">The message to display.</param>		
        /// <param name="buttons">The buttons to display on the message box.</param>
        /// <param name="icon">The icon to display on the message box.</param>
        /// <param name="defaultButton">The default button.</param>
        /// <returns>The results of the invocation.</returns>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					MessageBoxEx.Show("No arguments were specified.", MessageBoxButtons.Ok, 
        ///							MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					MessageBoxEx.Show("No arguments were specified.", MessageBoxButtons.Ok, 
        ///							MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static DialogResult Show ( string message, MessageBoxButtons buttons,
                                          MessageBoxIcon icon, MessageBoxDefaultButton defaultButton )
        { 
            return MessageBox.Show(DefaultOwner, message, DefaultTitle, buttons, icon, defaultButton,
                                   GetOptions(DefaultOwner)); 
        }

        /// <summary>Displays a message box.</summary>
        /// <param name="message">The message to display.</param>		
        /// <param name="title">The title to display.</param>
        /// <param name="buttons">The buttons to display on the message box.</param>
        /// <param name="icon">The icon to display on the message box.</param>
        /// <param name="defaultButton">The default button.</param>
        /// <returns>The results of the invocation.</returns>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					MessageBoxEx.Show("No arguments were specified.", "Warning", MessageBoxButtons.Ok, 
        ///							MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					MessageBoxEx.Show("No arguments were specified.", "Warning", MessageBoxButtons.Ok, 
        ///							MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static DialogResult Show ( string message, string title, MessageBoxButtons buttons,
                                          MessageBoxIcon icon, MessageBoxDefaultButton defaultButton )
        {
            return MessageBox.Show(DefaultOwner, message, title, buttons, icon, defaultButton,
                                   GetOptions(DefaultOwner)); 
        }

        /// <summary>Displays a message box.</summary>
        /// <param name="owner">The owning window.</param>
        /// <param name="message">The message to display.</param>		
        /// <returns>The results of the invocation.</returns>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					MessageBoxEx.Show(null, "No arguments were specified.");
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					MessageBoxEx.Show(Nothing, "No arguments were specified.")
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static DialogResult Show ( IWin32Window owner, string message )
        { 
            return MessageBox.Show(owner, message, DefaultTitle, DefaultButtons, DefaultIcon,
                                   MessageBoxDefaultButton.Button1, GetOptions(owner)); 
        }

        /// <summary>Displays a message box.</summary>
        /// <param name="owner">The owning window.</param>
        /// <param name="message">The message to display.</param>		
        /// <param name="title">The title to display.</param>
        /// <returns>The results of the invocation.</returns>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					MessageBoxEx.Show(null, "No arguments were specified.", "Warning");
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					MessageBoxEx.Show(Nothing, "No arguments were specified.", "Warning")
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static DialogResult Show ( IWin32Window owner, string message, string title )
        {
            return MessageBox.Show(owner, message, title, DefaultButtons, DefaultIcon,
                                   MessageBoxDefaultButton.Button1, GetOptions(owner));
        }

        /// <summary>Displays a message box.</summary>
        /// <param name="owner">The owning window.</param>
        /// <param name="message">The message to display.</param>		
        /// <param name="buttons">The buttons to display on the message box.</param>
        /// <returns>The results of the invocation.</returns>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					MessageBoxEx.Show(null, "No arguments were specified.", MessageBoxButtons.Ok);
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					MessageBoxEx.Show(Nothing, "No arguments were specified.", MessageBoxButtons.Ok)
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static DialogResult Show ( IWin32Window owner, string message, MessageBoxButtons buttons )
        {
            return MessageBox.Show(owner, message, DefaultTitle, buttons, DefaultIcon,
                                   MessageBoxDefaultButton.Button1, GetOptions(owner));
        }

        /// <summary>Displays a message box.</summary>
        /// <param name="owner">The owning window.</param>
        /// <param name="message">The message to display.</param>		
        /// <param name="title">The title to display.</param>
        /// <param name="buttons">The buttons to display on the message box.</param>
        /// <returns>The results of the invocation.</returns>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					MessageBoxEx.Show(null, "No arguments were specified.", "Warning", MessageBoxButtons.Ok);
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					MessageBoxEx.Show(Nothing, "No arguments were specified.", "Warning", MessageBoxButtons.Ok)
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static DialogResult Show ( IWin32Window owner, string message, string title, MessageBoxButtons buttons )
        { 
            return MessageBox.Show(owner, message, title, buttons, DefaultIcon,
                                   MessageBoxDefaultButton.Button1, GetOptions(owner));
        }

        /// <summary>Displays a message box.</summary>
        /// <param name="owner">The owning window.</param>
        /// <param name="message">The message to display.</param>		
        /// <param name="icon">The icon to display on the message box.</param>
        /// <returns>The results of the invocation.</returns>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					MessageBoxEx.Show(null, "No arguments were specified.", MessageBoxIcon.Warning);
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					MessageBoxEx.Show(Nothing, "No arguments were specified.", MessageBoxIcon.Warning)
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static DialogResult Show ( IWin32Window owner, string message, MessageBoxIcon icon )
        { 
            return MessageBox.Show(owner, message, DefaultTitle, DefaultButtons, icon,
                                   MessageBoxDefaultButton.Button1, GetOptions(owner));
        }

        /// <summary>Displays a message box.</summary>
        /// <param name="owner">The owning window.</param>
        /// <param name="message">The message to display.</param>		
        /// <param name="title">The title to display.</param>
        /// <param name="icon">The icon to display on the message box.</param>
        /// <returns>The results of the invocation.</returns>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					MessageBoxEx.Show(null, "No arguments were specified.", "Warning", MessageBoxIcon.Warning);
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					MessageBoxEx.Show(Nothing, "No arguments were specified.", "Warning", MessageBoxIcon.Warning)
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static DialogResult Show ( IWin32Window owner, string message, string title, MessageBoxIcon icon )
        { 
            return MessageBox.Show(owner, message, title, DefaultButtons, icon,
                                   MessageBoxDefaultButton.Button1, GetOptions(owner));
        }

        /// <summary>Displays a message box.</summary>
        /// <param name="owner">The owning window.</param>
        /// <param name="message">The message to display.</param>		
        /// <param name="buttons">The buttons to display on the message box.</param>
        /// <param name="icon">The icon to display on the message box.</param>
        /// <returns>The results of the invocation.</returns>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					MessageBoxEx.Show(null, "No arguments were specified.", MessageBoxButtons.Ok, 
        ///							MessageBoxIcon.Warning);
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					MessageBoxEx.Show(Nothing, "No arguments were specified.", MessageBoxButtons.Ok, 
        ///							MessageBoxIcon.Warning)
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static DialogResult Show ( IWin32Window owner, string message, MessageBoxButtons buttons, MessageBoxIcon icon )
        { 
            return MessageBox.Show(owner, message, DefaultTitle, buttons, icon,
                                   MessageBoxDefaultButton.Button1, GetOptions(owner));
        }

        /// <summary>Displays a message box.</summary>
        /// <param name="owner">The owning window.</param>
        /// <param name="message">The message to display.</param>		
        /// <param name="title">The title to display.</param>
        /// <param name="buttons">The buttons to display on the message box.</param>
        /// <param name="icon">The icon to display on the message box.</param>
        /// <returns>The results of the invocation.</returns>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					MessageBoxEx.Show(null, "No arguments were specified.", "Warning", MessageBoxButtons.Ok, 
        ///							MessageBoxIcon.Warning);
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					MessageBoxEx.Show(Nothing, "No arguments were specified.", "Warning", MessageBoxButtons.Ok, 
        ///							MessageBoxIcon.Warning)
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static DialogResult Show ( IWin32Window owner, string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon )
        {
            return MessageBox.Show(owner, message, title, buttons, icon,
                                   MessageBoxDefaultButton.Button1, GetOptions(owner));
        }

        /// <summary>Displays a message box.</summary>
        /// <param name="owner">The owning window.</param>
        /// <param name="message">The message to display.</param>		
        /// <param name="buttons">The buttons to display on the message box.</param>
        /// <param name="icon">The icon to display on the message box.</param>
        /// <param name="defaultButton">The default button.</param>
        /// <returns>The results of the invocation.</returns>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					MessageBoxEx.Show(null, "No arguments were specified.", MessageBoxButtons.Ok, 
        ///							MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					MessageBoxEx.Show(Nothing, "No arguments were specified.", MessageBoxButtons.Ok, 
        ///							MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static DialogResult Show ( IWin32Window owner, string message, MessageBoxButtons buttons,
                                          MessageBoxIcon icon, MessageBoxDefaultButton defaultButton )
        {
            return MessageBox.Show(owner, message, DefaultTitle, buttons, icon, defaultButton,
                                   GetOptions(owner));
        }

        /// <summary>Displays a message box.</summary>
        /// <param name="owner">The owning window.</param>
        /// <param name="message">The message to display.</param>		
        /// <param name="title">The title to display.</param>
        /// <param name="buttons">The buttons to display on the message box.</param>
        /// <param name="icon">The icon to display on the message box.</param>
        /// <param name="defaultButton">The default button.</param>
        /// <returns>The results of the invocation.</returns>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///					MessageBoxEx.Show(null, "No arguments were specified.", "Warning", MessageBoxButtons.Ok, 
        ///							MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					MessageBoxEx.Show(Nothing, "No arguments were specified.", "Warning", MessageBoxButtons.Ok, 
        ///							MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static DialogResult Show ( IWin32Window owner, string message, string title, MessageBoxButtons buttons,
                                          MessageBoxIcon icon, MessageBoxDefaultButton defaultButton )
        {
            return MessageBox.Show(owner, message, title, buttons, icon, defaultButton,
                                   GetOptions(owner));
        }

        /// <summary>Displays a message box.</summary>
        /// <param name="message">The message to display.</param>
        /// <param name="settings">The settings to use for the display.</param>
        /// <returns>The results of the invocation.</returns>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( string[] args )
        ///			{
        ///				if (args.Length == 0)
        ///				{
        ///					MessageBoxSettings settings = new MessageBoxSettings();
        ///					settings.Buttons = MessageBoxButtons.Ok;
        ///					settings.Icon = MessageBoxIcon.Warning;
        /// 
        ///					MessageBoxEx.Show("No arguments were specified.", settings);
        ///				};
        ///			}
        ///		}
        /// </code>
        /// <code lang="VB">
        ///		Class App
        ///		
        ///			Shared Sub Main ( ByVal args() As String )
        ///			
        ///				If args.Length = 0 Then
        ///					Dim settings As New MessageBoxSettings()
        ///					
        ///					settings.Buttons = MessageBoxButtons.Ok
        ///					settings.Icon = MessageBoxIcon.Warning
        ///					MessageBoxEx.Show("No arguments were specified.", settings)
        ///				End If
        ///			End Sub
        ///		End Class
        /// </code>
        /// </example>
        public static DialogResult Show ( string message, MessageBoxSettings settings )
        {
            return MessageBox.Show(settings.Owner, message, settings.Title, settings.Buttons, 
                                   settings.Icon, settings.DefaultButton, settings.Options);
        }
        #endregion

        #endregion

        #endregion //Public Members

        #region Private Members

        #region Methods

        private static MessageBoxOptions GetOptions ( IWin32Window owner )
        {
            MessageBoxOptions options = 0;
            bool bRTL = false;

            if (owner != null)
            {
                Control ctrl = Control.FromChildHandle(owner.Handle);
                while (ctrl != null)
                {
                    if (ctrl.RightToLeft == RightToLeft.Inherit)
                    {
                        ctrl = ctrl.Parent;
                    }
                    else if (ctrl.RightToLeft == RightToLeft.Yes)
                    {
                        bRTL = true;
                        break;
                    } else
                    {
                        break;
                    };
                };

                if (ctrl == null)
                    bRTL = CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft;
            } else
                bRTL = CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft;

            if (bRTL)
                options |= (MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);

            return options;
        }
        #endregion

        #region Data

        private static string m_strDefTitle = "";
        #endregion

        #endregion //Private Members
    }

    /// <summary>Provides the settings to use for displaying a message box.</summary>
    /// <seealso cref="MessageBoxEx"/>
    [CodeNotTested]
    public struct MessageBoxSettings
    {
        #region Construction
        
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dummy")]
        private MessageBoxSettings ( bool dummy )
        {
            m_Owner = null;
            m_strTitle = null;
            m_Buttons = null;
            m_Icon = null;
            m_DefButton = MessageBoxDefaultButton.Button1;
            m_Options = 0;
        }

        /// <summary>Initializes an instance of the <see cref="MessageBoxSettings"/> structure.</summary>
        /// <param name="title">The title to display.</param>
        public MessageBoxSettings ( string title ) : this(null, title)
        { /* Do nothing */ }

        /// <summary>Initializes an instance of the <see cref="MessageBoxSettings"/> structure.</summary>
        /// <param name="owner">The owning wndow.</param>
        /// <param name="title">The title to display.</param>
        public MessageBoxSettings ( IWin32Window owner, string title ) : this(false)
        {
            Owner = owner;
            Title = title;
        }

        /// <summary>Initializes an instance of the <see cref="MessageBoxSettings"/> structure.</summary>
        /// <param name="title">The title to display.</param>
        /// <param name="buttons">The buttons to display.</param>
        public MessageBoxSettings ( string title, MessageBoxButtons buttons ) : this(null, title, buttons)
        { /* Do nothing */ }

        /// <summary>Initializes an instance of the <see cref="MessageBoxSettings"/> structure.</summary>
        /// <param name="owner">The owning wndow.</param>
        /// <param name="title">The title to display.</param>
        /// <param name="buttons">The buttons to display.</param>
        public MessageBoxSettings ( IWin32Window owner, string title, MessageBoxButtons buttons ) : this(owner, title)
        {
            Buttons = buttons;
        }

        /// <summary>Initializes an instance of the <see cref="MessageBoxSettings"/> structure.</summary>
        /// <param name="title">The title to display.</param>
        /// <param name="buttons">The buttons to display.</param>
        /// <param name="icon">The icon to display.</param>
        public MessageBoxSettings ( string title, MessageBoxButtons buttons, MessageBoxIcon icon )
                            : this(null, title, buttons, icon)
        { /* Do nothing */ }

        /// <summary>Initializes an instance of the <see cref="MessageBoxSettings"/> structure.</summary>
        /// <param name="owner">The owning wndow.</param>
        /// <param name="title">The title to display.</param>
        /// <param name="buttons">The buttons to display.</param>
        /// <param name="icon">The icon to display.</param>
        public MessageBoxSettings ( IWin32Window owner, string title, MessageBoxButtons buttons, MessageBoxIcon icon )
                        : this(owner, title, buttons)
        {
            Icon = icon;
        }

        /// <summary>Initializes an instance of the <see cref="MessageBoxSettings"/> structure.</summary>
        /// <param name="title">The title to display.</param>
        /// <param name="buttons">The buttons to display.</param>
        /// <param name="defaultButton">The default button.</param>
        public MessageBoxSettings ( string title, MessageBoxButtons buttons, MessageBoxDefaultButton defaultButton )
                            : this(null, title, buttons, defaultButton)
        { /* Do nothing */ }

        /// <summary>Initializes an instance of the <see cref="MessageBoxSettings"/> structure.</summary>
        /// <param name="owner">The owning wndow.</param>
        /// <param name="title">The title to display.</param>
        /// <param name="buttons">The buttons to display.</param>
        /// <param name="defaultButton">The default button.</param>
        public MessageBoxSettings ( IWin32Window owner, string title, MessageBoxButtons buttons, MessageBoxDefaultButton defaultButton )
                            : this(owner, title, buttons)
        {
            DefaultButton = defaultButton;
        }

        /// <summary>Initializes an instance of the <see cref="MessageBoxSettings"/> structure.</summary>
        /// <param name="title">The title to display.</param>
        /// <param name="buttons">The buttons to display.</param>
        /// <param name="icon">The icon to display.</param>
        /// <param name="defaultButton">The default button.</param>
        public MessageBoxSettings ( string title, MessageBoxButtons buttons, MessageBoxIcon icon, 
                                    MessageBoxDefaultButton defaultButton ) : this(null, title, buttons, icon, defaultButton)
        { /* Do nothing */ }

        /// <summary>Initializes an instance of the <see cref="MessageBoxSettings"/> structure.</summary>
        /// <param name="owner">The owning wndow.</param>
        /// <param name="title">The title to display.</param>
        /// <param name="buttons">The buttons to display.</param>
        /// <param name="icon">The icon to display.</param>
        /// <param name="defaultButton">The default button.</param>
        public MessageBoxSettings ( IWin32Window owner, string title, MessageBoxButtons buttons, MessageBoxIcon icon,
                                    MessageBoxDefaultButton defaultButton ) : this(false)
        {
            Owner = owner;
            Title = title;

            Buttons = buttons;
            Icon = icon;
            DefaultButton = defaultButton;
        }
        #endregion

        #region Public Members

        #region Attributes

        /// <summary>Gets or sets the buttons to display on the message box.</summary>
        public MessageBoxButtons Buttons
        {
            [DebuggerStepThrough]
            get { return m_Buttons.HasValue ? m_Buttons.Value : MessageBoxEx.DefaultButtons; }

            [DebuggerStepThrough]
            set { m_Buttons = value; }
        }

        /// <summary>Gets or sets the default button of the message box.</summary>
        public MessageBoxDefaultButton DefaultButton
        {
            [DebuggerStepThrough]
            get { return m_DefButton; }

            [DebuggerStepThrough]
            set { m_DefButton = value; }
        }

        /// <summary>Gets or sets the icon to display on the message box.</summary>
        public MessageBoxIcon Icon
        {
            [DebuggerStepThrough]
            get { return m_Icon.HasValue ? m_Icon.Value : MessageBoxEx.DefaultIcon; }

            [DebuggerStepThrough]
            set { m_Icon = value; }
        }

        /// <summary>Gets or sets the options for the message box.</summary>
        public MessageBoxOptions Options
        {
            [DebuggerStepThrough]
            get { return m_Options; }

            [DebuggerStepThrough]
            set { m_Options = value; }
        }

        /// <summary>Gets or sets the owner of the message box.</summary>
        public IWin32Window Owner
        {
            [DebuggerStepThrough]
            get { return m_Owner ?? MessageBoxEx.DefaultOwner; }

            [DebuggerStepThrough]
            set { m_Owner = value; }
        }

        /// <summary>Gets or sets the title of the message box.</summary>
        public string Title
        {
            [DebuggerStepThrough]
            get { return m_strTitle ?? MessageBoxEx.DefaultTitle; }

            [DebuggerStepThrough]
            set { m_strTitle = value; }
        }
        #endregion

        #region Methods

        /// <summary>Compares two values for equality.</summary>
        /// <param name="obj">The object to compare against.</param>
        /// <returns><see langword="true"/> if they are equal or <see langword="false"/> otherwise.</returns>
        public override bool  Equals(object obj)
        {
            return this == (MessageBoxSettings)obj;
        }

        /// <summary>Gets the hashcode for the structure.</summary>
        /// <returns>The hashcode for the structure.</returns>
        public override int GetHashCode ( )
        {
            int code = m_Buttons.GetHashCode() | m_DefButton.GetHashCode() |
                       m_Options.GetHashCode();
            if (m_Icon.HasValue)
                code |= m_Icon.GetHashCode();
            if (m_Owner != null)
                code |= m_Owner.GetHashCode();
            if (m_strTitle != null)
                code |= m_strTitle.GetHashCode();

            return code;
        }

        /// <summary>Compares two values for equality.</summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns><see langword="true"/> if they are equal or <see langword="false"/> otherwise.</returns>
        public static bool operator== ( MessageBoxSettings value1, MessageBoxSettings value2 )
        {
            return (value1.m_Buttons == value2.m_Buttons) && (value1.m_DefButton == value2.m_DefButton) &&
                   (value1.m_Icon == value2.m_Icon) && (value1.m_Options == value2.m_Options) &&
                   (value1.m_Owner == value2.m_Owner) && (value1.m_strTitle == value2.m_strTitle);
        }

        /// <summary>Compares two values for inequality.</summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns><see langword="true"/> if they are not equal or <see langword="false"/> otherwise.</returns>
        public static bool operator != ( MessageBoxSettings value1, MessageBoxSettings value2 )
        {
            return !(value1 == value2);
        }
        #endregion

        #endregion //Public Members

        #region Private Members

        #region Data

        private IWin32Window m_Owner;
        private string m_strTitle;
        private MessageBoxButtons? m_Buttons;
        private MessageBoxIcon? m_Icon;
        private MessageBoxDefaultButton m_DefButton;

        private MessageBoxOptions m_Options;
        #endregion

        #endregion //Private Members
    }
}
