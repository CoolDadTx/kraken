/*
 * Provides support for working with file systems. 
 *
 * Copyright (c) 2005 Michael L. Taylor ($COMPANY$)
 * All rights reserved.
 *
 * $Header: /DotNET/Kraken/Source/P3Net.Kraken.IO/FileSystemHelper.cs 10    11/04/05 8:22a Michael $
 */
#region Imports

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text.RegularExpressions;

using P3Net.Kraken;
#endregion

namespace P3Net.Kraken.IO
{
	/// <summary>Provides additional support for file system objects.</summary>
	[CodeNotAnalyzed]
	[CodeNotTested]
	public static class FileSystemHelper
	{
        #region Public Members
        
        #region Methods
        
        #region BuildPath
        
        /// <summary>Builds a path given the subpaths.</summary>
        /// <param name="path1">The first path.</param>
        /// <param name="path2">The second path.</param>
        /// <returns>The combined paths.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="path1"/> or <paramref name="path2"/> is <see langword="null"/>.</exception>        
		/// <example>
		/// <code lang="C#">
		///		public class App
		///		{
		///			static void Main ( )
		///			{
		///				Console.WriteLine(FileSystemHelper.BuildPath(@"c:\", @"Windows"));
		///				Console.WriteLine(FileSystemHelper.BuildPath(@"c:\Windows\", @"\System32"));
		///				Console.WriteLine(FileSystemHelper.BuildPath(@"c:", @""));
		///			}
		///		}
		/// 
		///		/* Output is
		///		 	c:\Windows
		///		 	c:\Windows\System32
		///		  	c:
		///		 */
		/// </code>
		/// <code lang="Visual Basic">
		///		Public Class App
		///			Shared Sub Main
		/// 
		///				Console.WriteLine(FileSystemHelper.BuildPath("c:\", "Windows"))
		///				Console.WriteLine(FileSystemHelper.BuildPath("c:\Windows\", "\System32"))
		///				Console.WriteLine(FileSystemHelper.BuildPath("c:", ""))
		///			End Sub		
		///		End Class
		/// 
		///		' Output is
		///		' 	c:\Windows
		///		' 	c:\Windows\System32
		///		'  	c:
		/// </code>
		/// </example>
        [DebuggerStepThrough]
        public static string BuildPath ( string path1, string path2 )
        {
            if (path1 == null)
                throw new ArgumentNullException("path1");
            if (path2 == null)
                throw new ArgumentNullException("path2");
                                
            return Path.Combine(path1, path2);
        }
        
        /// <summary>Builds a path given the subpaths.</summary>
        /// <param name="path1">The first path.</param>
        /// <param name="path2">The second path.</param>
        /// <param name="path3">The third path.</param>
        /// <returns>The combined paths.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="path1"/>, <paramref name="path2"/> or <paramref name="path3"/> is <see langword="null"/>.</exception>        
		/// <example>
		/// Refer to <see cref="M:P3Net.Kraken.IO.FileSystemHelper.BuildPath(System.String,System.String)"/> for an example.
		/// </example>
        [DebuggerStepThrough]
        public static string BuildPath ( string path1, string path2, string path3 )
        {
			if (path1 == null)
				throw new ArgumentNullException("path1");
			if (path2 == null)
				throw new ArgumentNullException("path2");
            if (path3 == null)
                throw new ArgumentNullException("path3");
        
            return Path.Combine(BuildPath(path1, path2), path3);
        }        
        
        /// <summary>Builds a path given the subpaths.</summary>
        /// <param name="path1">The first path.</param>
        /// <param name="path2">The second path.</param>
        /// <param name="path3">The third path.</param>
        /// <param name="paths">Additional paths.</param>
        /// <returns>The combined paths.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="path1"/>, <paramref name="path2"/> or <paramref name="path3"/> is <see langword="null"/>.
		/// <para>-or-</para>
		/// one of the elements of <paramref name="paths"/> is <see langword="null"/>.
		/// </exception>   
		/// <example>
		/// Refer to <see cref="M:P3Net.Kraken.IO.FileSystemHelper.BuildPath(System.String,System.String)"/> for an example.
		/// </example>
        public static string BuildPath ( string path1, string path2, string path3, params string[] paths )
        {
            string strPath = BuildPath(path1, path2, path3);
            foreach(string path in paths)
                strPath = Path.Combine(strPath, path);
                
            return strPath;                
        }                
        #endregion
        
        #region DirectoryExists
        
        /// <summary>Determines if the given directory exists.</summary>
        /// <param name="path">The path to check.</param>
		/// <returns><see langword="true"/> if it exists or <see langword="false"/> otherwise.</returns>
        /// <remarks>Provides equivalent functionality to <see cref="Directory.Exists"/> but
        /// provided for consistency with the rest of the methods.</remarks>
		/// <exception cref="ArgumentNullException"><paramref name="path"/> is <see langword="null"/>.</exception>
		/// <example>
		/// <code lang="C#">
		///		public class App
		///		{
		///			static void Main ( )
		///			{
		///				if (FileSystemHelper.DirectoryExists(@"C:\Windows\System32"))
		///					Console.WriteLine("System directory exists.");
		///			}
		///		}
		/// </code>
		/// <code lang="Visual Basic">
		///		Public Class App
		/// 
		///			Shared Sub Main
		///			
		///				If FileSystemHelper.DirectoryExists("C:\Windows\System32") Then
		///					Console.WriteLine("System directory exists.")
		///				End If
		///			End Sub
		///		End Class
		/// </code>
		/// </example>
        public static bool DirectoryExists ( string path )
        {
			if (path == null)
				throw new ArgumentNullException("path");

            return (path.Length > 0) ? Directory.Exists(path) : false;
        }
        
        /// <summary>Determines if the given child directory exists.</summary>
        /// <param name="path">The parent path to check.</param>
        /// <param name="child">The child path.</param>
		/// <returns><see langword="true"/> if it exists or <see langword="false"/> otherwise.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="path"/> or <paramref name="child"/> is <see langword="null"/>.</exception>
		/// <example>
		/// <code lang="C#">
		///		public class App
		///		{
		///			static void Main ( )
		///			{
		///				if (FileSystemHelper.DirectoryExists(@"C:\Windows", "System32"))
		///					Console.WriteLine("System directory exists.");
		///			}
		///		}
		/// </code>
		/// <code lang="Visual Basic">
		///		Public Class App
		/// 
		///			Shared Sub Main
		///			
		///				If FileSystemHelper.DirectoryExists("C:\Windows", "System32") Then
		///					Console.WriteLine("System directory exists.")
		///				End If
		///			End Sub
		///		End Class
		/// </code>
		/// </example>
        public static bool DirectoryExists ( string path, string child )
        {
			if (path == null)
				throw new ArgumentNullException("path");
			if (child == null)
				throw new ArgumentNullException("child");

            return (path.Length > 0) ? Directory.Exists(BuildPath(path, child)) : false;
        }
        #endregion

		#region FileExists

		/// <summary>Determines if the given file exists.</summary>
		/// <param name="fileName">The file to check.</param>
		/// <returns><see langword="true"/> if it exists or <see langword="false"/> otherwise.</returns>
		/// <remarks>Provides equivalent functionality to <see cref="File.Exists"/> but
		/// provided for consistency with the rest of the methods.</remarks>
		/// <exception cref="ArgumentNullException"><paramref name="fileName"/> is <see langword="null"/>.</exception>
		/// <example>
		/// <code lang="C#">
		///		public class App
		///		{
		///			static void Main ( )
		///			{
		///				string[] files = new string[] { 
		///					@"C:\Windows\System32.msvcrt.dll", @"C:\Windows\Explorer.exe", @"C:\Temp\test.txt", @"" };
		///		
		///				foreach (string file in files)
		///				{
		///					if (FileSystem.Helper.FileExists(file))
		///						Console.WriteLine(String.Concat(file, " exists"));
		///					else
		///						Console.WriteLine(String.Concat(file, " does not exist"));
		///				};
		///			}
		///		}
		/// 
		///		/* Output is
		///				C:\Windows\System32.msvcrt.dll exists
		///				C:\Windows\Explorer.exe exists
		///				C:\Temp\test.txt does not exist
		///				 does not exist
		///		 */
		/// </code>
		/// <code lang="Visual Basic">
		///		Public Class App
		/// 
		///			Shared Sub Main
		///			
		///				Dim files() As String = { _
		///					"C:\Windows\System32.msvcrt.dll", "C:\Windows\Explorer.exe", "C:\Temp\test.txt", "" }
		///		
		///				For Each file As String In files
		///					If FileSystem.Helper.FileExists(file) Then
		///						Console.WriteLine(String.Concat(file, " exists"))
		///					Else
		///						Console.WriteLine(String.Concat(file, " does not exist"))
		///				Next
		///			End Sub
		///		End Class
		/// 
		///		' Output is
		///		'		C:\Windows\System32.msvcrt.dll exists
		///		'		C:\Windows\Explorer.exe exists
		///		'		C:\Temp\test.txt does not exist
		///		'		 does not exist		
		/// </code>
		/// </example>
		public static bool FileExists ( string fileName )
		{
			if (fileName == null)
				throw new ArgumentNullException("fileName");

			return (fileName.Length > 0) ? Directory.Exists(fileName) : false;
		}

		/// <summary>Determines if the given file exists in the specified directory.</summary>
		/// <param name="path">The parent path to check.</param>
		/// <param name="fileName">The file to check.</param>
		/// <returns><see langword="true"/> if it exists or <see langword="false"/> otherwise.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="path"/> or <paramref name="fileName"/> is <see langword="null"/>.</exception>
		/// <example>
		/// Refer to <see cref="M:P3Net.Kraken.IO.FileSystemHelper.FileExists(System.String)"/> for an example.
		/// </example>
		public static bool FileExists ( string path, string fileName )
		{
			if (path == null)
				throw new ArgumentNullException("path");
			if (fileName == null)
				throw new ArgumentNullException("fileName");

			return (path.Length > 0) ? File.Exists(BuildPath(path, fileName)) : false;
		}
		#endregion

        #region GetBaseDirectoryName
        
        /// <summary>Gets the base directory of a path.</summary>
        /// <param name="pathName">The path to search.</param>
        /// <returns>The base directory of <paramref name="pathName"/>.</returns>
        /// <remarks>
        /// The base directory is the last directory in the path.  For example the base directory of C:\temp\test.txt
        /// is 'temp' and the base directory of c:\temp is 'temp'.
        /// </remarks>
		/// <exception cref="ArgumentNullException"><paramref name="pathName"/> is <see langword="null"/>.</exception>
		/// <example>
		///	<code lang="C#">
		///		public class App
		///		{
		///			static void Main ( )
		///			{
		///				Console.WriteLine(FileSystemHelper.GetBaseDirectoryName(@"C:\Windows\System32"));
		///				Console.WriteLine(FileSystemHelper.GetBaseDirectoryName(@"C:\Windows\"));
		///				Console.WriteLine(FileSystemHelper.GetBaseDirectoryName(@"C:\Windows"));
		///			}
		///		}
		/// 
		///		/*Output is
		///			System32
		///		
		///			Windows
		///		*/
		/// </code>
		/// <code lang="Visual Basic">
		///		Public Class App
		///		
		///			Shared Sub Main ( )
		/// 
		///				Console.WriteLine(FileSystemHelper.GetBaseDirectoryName("C:\Windows\System32"))
		///				Console.WriteLine(FileSystemHelper.GetBaseDirectoryName("C:\Windows\"))
		///				Console.WriteLine(FileSystemHelper.GetBaseDirectoryName("C:\Windows"))
		///			End Sub
		///		End Class
		/// 
		///		'Output is
		///		'	System32
		///		'
		///		'	Windows
		/// </code>
		/// </example>
        public static string GetBaseDirectoryName ( string pathName )
        {
			if (pathName == null)
				throw new ArgumentNullException("pathName");

            string dir = Path.GetDirectoryName(pathName);
            int nPos = dir.LastIndexOfAny(new char[] { Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar });
            if (nPos >= 0)
                return dir.Substring(nPos + 1); 
                
            return "";                
        }
        #endregion
        
        #region GetDirectories
        
        /// <summary>Gets the child directories of the given directory.</summary>
        /// <param name="pathName">The path of the directory.</param>
        /// <returns>The child directories or an empty string if the directory has no children.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="pathName"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method will not throw an exception if the current user does not have permission to
        /// retrieve the directory names or if the directory does not exist.
        /// </remarks>
		/// <example>
		/// Refer to <see cref="M:P3Net.Kraken.IO.FileSystemHelper.GetDirectories(System.String, System.String)"/> for an example.
		/// </example>
        public static string[] GetDirectories ( string pathName )
        {
            return GetDirectories(pathName, "");
        }
        
        /// <summary>Gets the child directories of the given directory.</summary>
        /// <param name="pathName">The path of the directory.</param>
        /// <param name="searchPattern">The pattern of child directories to find.</param>
        /// <returns>The child directories or an empty string if the directory has no children.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="pathName"/> or <paramref name="searchPattern"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="pathName"/> is empty or <paramref name="searchPattern"/> is invalid.</exception>
        /// <remarks>
        /// This method will not throw an exception if the current user does not have permission to
        /// retrieve the directory names or if the directory does not exist.
        /// </remarks>
		/// <example>
		/// <code lang="C#">
		///		public class App
		///		{
		///			static void Main ( )
		///			{
		///				string[] dirs = FileSystemHelper.GetDirectories(@"C:\Windows\System32", "I*");
		///				foreach (string dir in dirs)
		///					Console.WriteLine(dir);
		///			}
		///		}
		/// 
		///		/* Typical output
		/// 
		///			IAS
		///			ICSXML
		///			IME
		///			INETSRV
		///		 */
		/// </code>
		/// <code lang="Visual Basic">
		///		Public Class App
		/// 
		///			Shared Sub Main
		/// 
		///				Dim dirs() As String = FileSystemHelper.GetDirectories("C:\Windows\System32", "I*")
		/// 
		///				For Each dir As String In dirs
		///					Console.WriteLine(dir)
		///				Next
		///			End Sub
		///		End Class
		/// 
		///		' Typical output
		///		'
		///		'	IAS
		///		'	ICSXML
		///		'	IME
		///		'	INETSRV
		/// </code>
		/// </example>
        public static string[] GetDirectories ( string pathName, string searchPattern )
        {
            if (pathName == null)
                throw new ArgumentNullException("pathName");
            pathName = pathName.Trim();
                
            if (searchPattern == null)
                throw new ArgumentNullException("searchPattern");
            searchPattern = searchPattern.Trim();

			if (pathName.Length == 0)
				return new string[0];
                
            //Get the directories, handling security exceptions
            try
            {                
                if (Directory.Exists(pathName))
                    if (searchPattern.Length > 0)
                        return Directory.GetDirectories(pathName, searchPattern);
                      
                    return Directory.GetDirectories(pathName);                        
            } catch (SecurityException)
            { /* Ignore */ 
            } catch (UnauthorizedAccessException)
            { /* Ignore */ };
            
            return new string[0];             
        }
        #endregion
        
        #region GetDirectorySize
        
        /// <summary>Gets the size of a directory and its children.</summary>
        /// <param name="pathName">The path to retrieve the size for.</param>
        /// <returns>The size of the directory in bytes.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="pathName"/> is <see langword="null"/>.</exception>
		/// <example>
		/// Refer to <see cref="M:P3Net.Kraken.IO.FileSystemHelper.GetDirectorySize(System.String, System.Boolean)"/> for an example.
		/// </example>
        [DebuggerStepThrough]
        public static long GetDirectorySize ( string pathName )
        {
            return GetDirectorySize(pathName, true);
        }
        
        /// <summary>Gets the size of a directory and its children.</summary>
        /// <param name="pathName">The path to retrieve the size for.</param>
		/// <param name="includeChildren"><see langword="true"/> to include child directories as well.</param>
        /// <returns>The size of the directory in bytes.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="pathName"/> is <see langword="null"/>.</exception>
		/// <example>
		/// <code lang="C#">
		///		public class App
		///		{
		///			static void Main ( )
		///			{
		///				Console.WriteLine("System32 without children: {0} bytes", FileSystemHelper.GetDirectorySize(@"C:\Windows\System32", false));
		///				Console.WriteLine("System32 with children: {0} bytes", FileSystemHelper.GetDirectorySize(@"C:\Windows\System32", true));
		///			}
		///		}
		/// 
		///		/* Typical output
		/// 
		///			System32 without children: 509273362 bytes
		///			System32 with children: 1060451977 bytes
		///		 */
		/// </code>
		/// <code lang="Visual Basic">
		///		Public Class App
		/// 
		///			Shared Sub Main
		///				
		///				Console.WriteLine("System32 without children: {0} bytes", FileSystemHelper.GetDirectorySize("C:\Windows\System32", False))
		///				Console.WriteLine("System32 with children: {0} bytes", FileSystemHelper.GetDirectorySize("@C:\Windows\System32", True))
		///			End Sub
		///		End Class
		/// 
		///		' Typical output
		///		'	
		///		'	System32 without children: 509273362 bytes
		///		'	System32 with children: 1060451977 bytes
		/// </code>
		/// </example>
        public static long GetDirectorySize ( string pathName, bool includeChildren )
        {
            if (pathName == null)
                throw new ArgumentNullException(nameof(pathName));
            pathName = pathName.Trim();
			if (pathName.Length == 0)
				return 0L;
            
            //We are going to use a stack to enumerate the directories because we don't want
            //to blow the runtime stack with a recursive call and furthermore the stack won't shrink
            //once allocated so we don't want to waste memory either
            var dirStack = new Stack<DirectoryInfo>();
            var dir = new DirectoryInfo(pathName);
            if (!dir.Exists)
                return 0;
            dirStack.Push(dir);

            long nSize = 0;
                        
            //While there is data on the stack
            while (dirStack.Count > 0)
            {
                //Pop the next directory
                dir = (DirectoryInfo)dirStack.Pop();
                
                //Get the files in the directory (may throw a security exception)
                FileInfo[] files = SafeGetFiles(dir);
                foreach(FileInfo file in files)
                    nSize += file.Length;
            
                //Children?
                if (includeChildren)
                {   
                    DirectoryInfo[] children = SafeGetDirs(dir);
                    foreach(DirectoryInfo child in children)
                        dirStack.Push(child);
                };
            };
                        
            return nSize;
        }
        #endregion

        #region GetFiles
        
        /// <summary>Gets the files of the given directory.</summary>
        /// <param name="pathName">The path of the directory.</param>
        /// <returns>The files or an empty string if the directory has no files.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="pathName"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method will not throw an exception if the current user does not have permission to
        /// retrieve the file names or if the directory does not exist.
        /// </remarks>
		/// <example>
		/// Refer to <see cref="M:P3Net.Kraken.IO.FileSystemHelper.GetFiles(System.String, System.String)"/> for an example.
		/// </example>
        public static string[] GetFiles ( string pathName )
        {
            return GetFiles(pathName, "");
        }
        
        /// <summary>Gets the files of the given directory.</summary>
        /// <param name="pathName">The path of the directory.</param>
        /// <param name="searchPattern">The pattern of files to find.</param>
        /// <returns>The files or an empty string if the directory has no files.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="pathName"/> or <paramref name="searchPattern"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method will not throw an exception if the current user does not have permission to
        /// retrieve the file names or if the directory does not exist.
        /// </remarks>
		/// <example>
		/// <code lang="C#">
		///		public class App
		///		{
		///			static void Main ( )
		///			{
		///				string[] files = FileSystemHelper.GetFiles(@"C:\Windows\System32", "*.cpl");
		///				foreach(string file in files)
		///				{
		///					Console.WriteLine(file);
		///				};
		///			}
		///		}
		/// 
		///		/* Typical output
		///		
		///			access.cpl
		///			appwiz.cpl
		///			desk.cpl
		///			...
		///		 */
		/// </code>
		/// <code lang="Visual Basic">
		///		Public Class App
		/// 
		///			Shared Sub Main
		/// 
		///				Dim files() As String = FileSystemHelper.GetFiles("C:\Windows\System32", "*.cpl")
		/// 
		///				For Each file As String in files
		///					Console.WriteLine(file)
		///				Next
		///			End Sub
		///		End Class
		/// 
		///		' Typical output
		///		'
		///		'	access.cpl
		///		'	appwiz.cpl
		///		'	desk.cpl
		///		'	...
		/// </code>
		/// </example>
        public static string[] GetFiles ( string pathName, string searchPattern )
        {
            if (pathName == null)
                throw new ArgumentNullException("pathName");
            pathName = pathName.Trim();
                
            if (searchPattern == null)
                throw new ArgumentNullException("searchPattern");
            searchPattern = searchPattern.Trim();

			if (pathName.Length == 0)
				return new string[0];
                
            //Get the files, handling security exceptions
            try
            {                
                if (Directory.Exists(pathName))
                    if (searchPattern.Length > 0)
                        return Directory.GetFiles(pathName, searchPattern);
                      
                    return Directory.GetFiles(pathName);                        
            } catch (SecurityException)
            { /* Ignore */ 
            } catch (UnauthorizedAccessException)
            { /* Ignore */ };
            
            return new string[0];             
        }
        #endregion

		#region GetFullPath

		/// <summary>Gets the full path of <paramref name="pathName"/> when it is relative to <paramref name="basePath"/>.</summary>
		/// <param name="basePath">The path to use as the root path.</param>
		/// <param name="pathName">The path to obtain the full path of.</param>
		/// <returns>The relative path from <paramref name="basePath"/> to <paramref name="pathName"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="basePath"/> or <paramref name="pathName"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="basePath"/> or <paramref name="pathName"/> is empty or invalid.</exception>
		/// <example>
		/// <code lang="C#">
		///		struct PathData
		///		{
		///			public string RootPath;
		///			public string PathName;
		/// 
		///			public PathData ( string root, string path )
		///			{
		///				RootPath = path;
		///				PathName = path;
		///			}
		///		}
		/// 
		///		class App
		///		{
		///			static void Main ( )
		///			{
		///				PathData[] arr = new PathData[] {
		///					new PathData(@"C:\Windows", @".\System32"),
		///					new PathData(@"C:\Temp", @"..\Windows"),
		///					new PathData(@"C:\Windows", @"D:\"),
		///					new PathData(@"C:\Windows\System32, @".")
		///				};
		/// 
		///				foreach(PathData data in arr)
		///					Console.WriteLine("Full path from '{0}' to '{1}' is: {2}", data.RootPath, data.PathName,
		///									  FileSystemHelper.GetFullPath(data.RootPath, data.PathName));
		///			}
		///		}
		/// 
		///		/* Output is
		/// 
		///		    Full path from 'C:\Windows' to '.\System32' is: C:\Windows\System32
		///			Full path from 'C:\Temp' to '..\Windows' is: C:\Windows
		///			Full path from 'C:\Windows' to 'D:\' is: D:\
		///			Full path from 'C:\Windows\System32' to '.' is: C:\Windows\System32
		///		 */
		/// </code>
		/// <code lang="Visual Basic">
		///		Structure PathData
		///			Public RootPath As String
		///			Public PathName As String
		/// 
		///			Public Sub New ( root As String, path As String )		
		///				RootPath = path
		///				PathName = path
		///			End Sub
		///		End Structure
		/// 
		///		Class App
		/// 
		///			Shared Sub Main ( )
		///			
		///				Dim arr() As PathData = {
		///					New PathData("C:\Windows", ".\System32"),
		///					New PathData("C:\Temp", "..\Windows"),
		///					New PathData("C:\Windows", "D:\"),
		///					New PathData("C:\Windows\System32, ".")
		///				}
		/// 
		///				For Each data As PathData In arr
		///					Console.WriteLine("Full path from '{0}' to '{1}' is: {2}", data.RootPath, data.PathName,
		///									  FileSystemHelper.GetFullPath(data.RootPath, data.PathName))
		///				Next
		///			End Sub
		///		End Class
		/// 
		///		' Output is
		///		'
		///		'   Full path from 'C:\Windows' to '.\System32' is: C:\Windows\System32
		///		'	Full path from 'C:\Temp' to '..\Windows' is: C:\Windows
		///		'	Full path from 'C:\Windows' to 'D:\' is: D:\
		///		'	Full path from 'C:\Windows\System32' to '.' is: C:\Windows\System32
		///		 
		/// </code>
		/// </example>
		public static string GetFullPath ( string basePath, string pathName )
		{
			if (basePath == null)
				throw new ArgumentNullException("basePath");
			basePath = basePath.Trim();
			if (pathName == null)
				throw new ArgumentNullException("pathName");
			pathName = pathName.Trim();
			if (basePath.Length == 0)
				return pathName;
			else if (pathName.Length == 0)
				return "";

			//There is a chance that another thread could mess this up...
			string strOld = Environment.CurrentDirectory;
			try
			{
				Environment.CurrentDirectory = basePath;
				return Path.GetFullPath(pathName);
			} finally
			{
				Environment.CurrentDirectory = strOld;
			}
		}
		#endregion

		#region GetRelativePath

		/// <summary>Gets the relative path from <paramref name="basePath"/> to <paramref name="pathName"/>.</summary>
		/// <param name="basePath">The path to use as the root path.</param>
		/// <param name="pathName">The path to obtain the relative path of.</param>
		/// <returns>The relative path from <paramref name="basePath"/> to <paramref name="pathName"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="basePath"/> or <paramref name="pathName"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="basePath"/> or <paramref name="pathName"/> is empty or invalid.</exception>
		/// <example>
		/// <code lang="C#">
		///		struct PathData
		///		{
		///			public string RootPath;
		///			public string PathName;
		/// 
		///			public PathData ( string root, string path )
		///			{
		///				RootPath = path;
		///				PathName = path;
		///			}
		///		}
		/// 
		///		class App
		///		{
		///			static void Main ( )
		///			{
		///				PathData[] arr = new PathData[] {
		///					new PathData(@"C:\Windows", @"C:\Windows\System32"),
		///					new PathData(@"C:\Temp", @"C:\Windows"),
		///					new PathData(@"C:\Windows", @"D:\"),
		///					new PathData(@"C:\Windows\System32, @"C:\Windows\System32")
		///				};
		/// 
		///				foreach(PathData data in arr)
		///					Console.WriteLine("Relative path from '{0}' to '{1}' is: {2}", data.RootPath, data.PathName,
		///									  FileSystemHelper.GetRelativePath(data.RootPath, data.PathName));
		///			}
		///		}
		/// 
		///		/* Output is
		/// 
		///		    Relative path from 'C:\Windows' to 'C:\Windows\System32' is: .\System32
		///			Relative path from 'C:\Temp' to 'C:\Windows' is: ..\Windows
		///			Relative path from 'C:\Windows' to 'D:\' is: D:\
		///			Relative path from 'C:\Windows\System32' to 'C:\Windows\System32' is: .
		///		 */
		/// </code>
		/// <code lang="Visual Basic">
		///		Structure PathData
		///			Public RootPath As String
		///			Public PathName As String
		/// 
		///			Public Sub New ( root As String, path As String )		
		///				RootPath = path
		///				PathName = path
		///			End Sub
		///		End Structure
		/// 
		///		Class App
		/// 
		///			Shared Sub Main ( )
		///			
		///				Dim arr() As PathData = {
		///					New PathData("C:\Windows", "C:\Windows\System32"),
		///					New PathData("C:\Temp", "C:\Windows"),
		///					New PathData("C:\Windows", "D:\"),
		///					New PathData("C:\Windows\System32, "C:\Windows\System32")
		///				}
		/// 
		///				For Each data As PathData In arr
		///					Console.WriteLine("Relative path from '{0}' to '{1}' is: {2}", data.RootPath, data.PathName,
		///									  FileSystemHelper.GetRelativePath(data.RootPath, data.PathName))
		///				Next
		///			End Sub
		///		End Class
		/// 
		///		' Output is
		///		'
		///		'   Relative path from 'C:\Windows' to 'C:\Windows\System32' is: .\System32
		///		'	Relative path from 'C:\Temp' to 'C:\Windows' is: ..\Windows
		///		'	Relative path from 'C:\Windows' to 'D:\' is: D:\
		///		'	Relative path from 'C:\Windows\System32' to 'C:\Windows\System32' is: .
		///		 
		/// </code>
		/// </example>
		public static string GetRelativePath ( string basePath, string pathName )
		{
			if (basePath == null)
				throw new ArgumentNullException("basePath");
			basePath = basePath.Trim();
			if (pathName == null)
				throw new ArgumentNullException("pathName");
			pathName = pathName.Trim();
			if (basePath.Length == 0)
				return pathName;
			else if (pathName.Length == 0)
				return "";

			//Determine if we are dealing with files or directories (use heuristics)
			string strFile = Path.GetFileName(basePath);
			int nFromAttr = strFile.Contains(".") ? (int)FileAttributes.Normal : (int)FileAttributes.Directory;

			strFile = Path.GetFileName(pathName);
			int nToAttr = strFile.Contains(".") ? (int)FileAttributes.Normal : (int)FileAttributes.Directory;

			System.Text.StringBuilder bldr = new System.Text.StringBuilder();
			bldr.EnsureCapacity(MaximumPathLength);
			if (!PathRelativePathToW(bldr, basePath, nFromAttr, pathName, nToAttr))
				return pathName;

			return bldr.ToString();
		}
		#endregion

		#region IsValid...

		/// <summary>Determines if the given name is a valid file name.</summary>
		/// <param name="fileName">The name to validate.</param>
		/// <returns><see langword="true"/> if the name is a valid filename or <see langword="false"/> otherwise.</returns>
		/// <example>
		/// <code lang="C#">
		///		public class App
		///		{
		///			static void Main ( )
		///			{
		///				string[] files = new string[] {
		///						@"msvcrt.dll", @"C:\Windows\explorer.exe", @"C:\temp\test|a", @"" };
		/// 
		///				foreach (string file in files)
		///					Console.WriteLine(String.Format({0} is valid: {1}, file, FileSystemHelper.IsValidFileName(file)));
		///			}
		///		}
		/// 
		///		/*Output is
		///			msvcrt.dll is valid: 1
		///		    C:\Windows\explorer.exe is valid: 0
		///			C:\temp\test|a is valid: 0
		///          is valid: 0
		///		*/
		/// </code>
		/// <code lang="Visual Basic">
		///		Public Class App
		///		
		///			Shared Sub Main ( )
		/// 
		///				Dim files() As String { "msvcrt.dll", "C:\Windows\explorer.exe", "C:\temp\test|a", "" }
		/// 
		///				For Each file As String In files
		///					Console.WriteLine(String.Format({0} is valid: {1}, file, FileSystemHelper.IsValidFileName(file)))
		///				Next
		///			End Sub
		///		End Class
		/// 
		///		'Output is
		///		'	msvcrt.dll is valid: 1
		///		'   C:\Windows\explorer.exe is valid: 0
		///		'	C:\temp\test|a is valid: 0
		///     '    is valid: 0
		/// </code>
		/// </example>
		/// <remarks>
		/// A valid file name does not mean the file exists or that it even represents a valid path.  A valid file name is
		/// strictly a string that meets the requirements for a properly formatted file name.
		/// </remarks>
		public static bool IsValidFileName ( string fileName )
		{
			return ValidateFileName(fileName) == null;
		}

		/// <summary>Determines if the given name is a valid, local path name.</summary>
		/// <param name="path">The name to validate.</param>
		/// <returns><see langword="true"/> if the name is a valid path or <see langword="false"/> otherwise.</returns>
		/// <example>
		/// <code lang="C#">
		///		public class App
		///		{
		///			static void Main ( )
		///			{
		///				string[] paths = new string[] {
		///						@"C:\Windows\System32", @"C:\Windows", @"C://W:", @"" };
		/// 
		///				foreach (string path in paths)
		///					Console.WriteLine(String.Format({0} is valid: {1}, path, FileSystemHelper.IsValidPathName(path)));
		///			}
		///		}
		/// 
		///		/*Output is
		///			C:\Windows\System32 is valid: 1
		///		    C:\Windows is valid: 1
		///			C://W: is valid: 0
		///          is valid: 0
		///		*/
		/// </code>
		/// <code lang="Visual Basic">
		///		Public Class App
		///		
		///			Shared Sub Main ( )
		/// 
		///				Dim paths() As String { "C:\Windows\System32", "C:\Windows", "C://W:", "" }
		/// 
		///				For Each path As String In paths
		///					Console.WriteLine(String.Format({0} is valid: {1}, path, FileSystemHelper.IsValidFileName(path)))
		///				Next
		///			End Sub
		///		End Class
		/// 
		///		'Output is
		///		'	C:\Windows\System32 is valid: 1
		///		'   C:\Windows is valid: 1
		///		'	C://W: is valid: 0
		///     '    is valid: 0
		/// </code>
		/// </example>
		/// <remarks>
		/// A valid path does not mean the path exists.  A valid path is strictly a string that meets the 
		/// requirements for a properly formatted path.
		/// </remarks>
		public static bool IsValidPathName ( string path )
		{
			return ValidatePathName(path) == null;
		}
		#endregion 

		#region ValidateFileName

		/// <summary>Determines if a file name is valid and if not returns the appropriate exception.</summary>
		/// <param name="fileName">The file name to validate.</param>
		/// <returns><see langword="null"/> if valid or an exception representing the error otherwise.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="fileName"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="fileName"/> is empty or contains invalid characters.</exception>
		/// <exception cref="PathTooLongException">The file name exceeds the limit defined by the file system.</exception>
		/// <exception cref="FormatException"><paramref name="fileName"/> is in an incorrect format.</exception>
		/// <remarks>
		/// Only file names are supported.  File names with paths are invalid.
		/// </remarks>
		public static Exception ValidateFileName ( string fileName )
		{
			if (fileName == null)
				return new ArgumentNullException("fileName");
			fileName = fileName.Trim();
			if (fileName.Length == 0)
				return new ArgumentException("File name is empty.", "fileName");

			if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
				return new ArgumentException("File name contains invalid characters.", "fileName");

			if (fileName.Length > MaximumFileNameLength)
				return new PathTooLongException("The file name is too long.");

			//Validate format
			/*Regex re = new Regex(PathNameExpression);
			if (!re.IsMatch(path))
				return new FormatException("Path is not in the correct format.");
			*/
			return null;
		}
		#endregion

		#region ValidatePathName

		/// <summary>Determines if a path is valid and if not returns the appropriate exception.</summary>
		/// <param name="path">The path to validate.</param>
		/// <returns><see langword="null"/> if valid or an exception representing the error otherwise.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="path"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="path"/> is empty or contains invalid characters.</exception>
		/// <exception cref="PathTooLongException">The path exceeds the limit defined by the file system.</exception>
		/// <exception cref="FormatException"><paramref name="path"/> is in an incorrect format.</exception>
		/// <remarks>
		/// Only full, local paths are supported.  UNC paths and relative paths are not supported.
		/// </remarks>
		/// <seealso cref="ValidateFileName"/>
		/// <seealso cref="IsValidPathName"/>
		public static Exception ValidatePathName ( string path )
		{
			if (path == null)
				return new ArgumentNullException("path");
			path = path.Trim();
			if (path.Length == 0)
				return new ArgumentException("Path is empty.", "path");

			if (path.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
				return new ArgumentException("Path contains invalid characters.", "path");

			if (path.Length > MaximumPathLength)
				return new PathTooLongException("The path is too long.");

			//Validate format
			/*Regex re = new Regex(PathNameExpression);
			if (!re.IsMatch(path))
				return new FormatException("Path is not in the correct format.");
			*/
			return null;
		}
		#endregion

		#endregion

		#region Data

		/*
		/// <summary>A regular expression representing the format of an absolute local path name.</summary>
		/// <remarks>
		/// The expression supports all valid local Windows paths and file names.  UNC paths are not supported.  The
		/// following groups are defined.
		/// <list type="table">
		///		<item>
		///			<term>Group</term>
		///			<description>Meaning</description>
		///		</item>
		///		<item>
		///			<term>Drive</term>
		///			<description>The drive letter.</description>
		///		</item>
		///		<item>
		///			<term>Directory</term>
		///			<description>The path within the drive, excluding the drive but including any file name.</description>
		///		</item>
		/// </list>
		/// </remarks>
		/// <seealso cref="RelativePathNameExpression"/>
		/// <seealso cref="PathNameExpression"/>
		public static readonly string AbsolutePathNameExpression = @"^(?<Drive>[a-zA-Z]):(?<Directory>(\\\s*(([^\\/:\*\?""<>|\s])+\s*))+)$";
		*/
		
		/// <summary>A regular expression representing the format of a file name.</summary>
		/// <remarks>
		/// The expression supports all valid Windows file names with or without extensions.  
		/// <list type="table">
		///		<item>
		///			<term>Group</term>
		///			<description>Meaning</description>
		///		</item>
		///		<item>
		///			<term>BaseFile</term>
		///			<description>The base file name.</description>
		///		</item>
		///		<item>
		///			<term>Extension</term>
		///			<description>The file extension.</description>
		///		</item>
		/// </list>
		/// </remarks>
		public static readonly string FileNameExpression = @"^((?<BaseFile>[^\\/:\*\?""<>|\.]+)[\.]?(?<Extension>[^\\/:\*\?""<>|\.]*))|((?<BaseFile>\s*)\.(?<Extension>[^\\/:\*\?""<>|\.]+))$";

		/// <summary>The maximum length for a directory component in Windows.</summary>
		public static readonly int MaximumDirectoryLength = 256;

		/// <summary>The maximum length for a drive compnent in Windows.</summary>
		public static readonly int MaximumDriveLength = 3;

		/// <summary>The maximum length for a file extension in Windows.</summary>
		public static readonly int MaximumExtensionLength = 256;

		/// <summary>The maximum length for a file name in Windows.</summary>
		public static readonly int MaximumFileNameLength = 256;

		/// <summary>The maximum length for a file path in Windows.</summary>
		public static readonly int MaximumPathLength = 260;
			
		/*
		/// <summary>A regular expression representing the format of a relative local path name.</summary>
		/// <remarks>
		/// The expression supports all valid local Windows paths and file names.  UNC paths are not supported.  The
		/// following groups are defined.
		/// <list type="table">
		///		<item>
		///			<term>Group</term>
		///			<description>Meaning</description>
		///		</item>
		///		<item>
		///			<term>Drive</term>
		///			<description>The drive letter.</description>
		///		</item>
		///		<item>
		///			<term>Directory</term>
		///			<description>The path within the drive, excluding the drive but including any file name.</description>
		///		</item>
		/// </list>
		/// </remarks>
		/// <seealso cref="AbsolutePathNameExpression"/>
		/// <seealso cref="PathNameExpression"/>
		public static readonly string RelativePathNameExpression = @"^(?<Drive>[a-zA-Z]):(?<Directory>(\\\s*(([^\\/:\*\?""<>|\s])+\s*))+)$";
		*/
		#endregion

		#endregion //Public Members

		#region Private Members

		#region Imports

		[DllImport("shlwapi.dll",ExactSpelling=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool PathRelativePathToW ( System.Text.StringBuilder pszPath, string pszFrom, int dwAttrFrom, string pszTo, int dwAttrTo );
		#endregion

		#region Methods

		//Gets the directories in a directory dealing with security violations as necessary
        private static DirectoryInfo[] SafeGetDirs ( DirectoryInfo dir )
        {
            try
            {
                return dir.GetDirectories();
            } catch (SecurityException)
            {
                return new DirectoryInfo[0];
            };                
        }        
        
        //Gets the files in a directory dealing with security violations as necessary
        private static FileInfo[] SafeGetFiles ( DirectoryInfo dir )
        {
            try
            {
                return dir.GetFiles();
            } catch (SecurityException)
            {
                return new FileInfo[0];
            };                
        }        
        #endregion

		#endregion //Private Members
	}
}
