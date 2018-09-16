/*
 * Copyright © 2005 Michael Taylor
 * All rights reserved.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.IO
{
    /// <summary>Provides support for working with paths.</summary>	
    public static class PathExtensions
    {
        #region Public Members

        #region Constants

        /// <summary>Gets the currently defined maximum path length.</summary>
        public static readonly int MaximumPath = 260;

        #endregion

        #region BuildPath

        /// <summary>Builds a path given the subpaths.</summary>
        /// <param name="path1">The first path.</param>
        /// <param name="path2">The second path.</param>
        /// <returns>The combined paths.</returns>
        /// <exception cref="ArgumentException"><paramref name="path2"/> contains an absolute path.</exception>
        /// <remarks>
        /// This method builds a path given the path components.  Unlike <see cref="O:Path.Combine"/> all paths after the first path are assumed
        /// to be subpaths.  <see langword="null"/> and empty paths are ignored.  Absolute paths after the first path will cause an exception.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( )
        ///			{
        ///				Console.WriteLine(PathExtensions.BuildPath(@"c:\", @"Windows"));
        ///				Console.WriteLine(PathExtensions.BuildPath(@"c:\Windows\", @"\System32"));
        ///				Console.WriteLine(PathExtensions.BuildPath(@"c:", @""));
        ///			}
        ///		}
        /// 
        ///		/* Output is
        ///		 	c:\Windows
        ///		 	c:\Windows\System32
        ///		  	c:
        ///		 */
        /// </code>
        /// </example>                        
        [ExcludeFromCodeCoverage]
        public static string BuildPath ( string path1, string path2 )
        {            
            return BuildPathCore(new string[] { path1 ?? "", path2 ?? "" });
        }
        
        /// <summary>Builds a path given the subpaths.</summary>
        /// <param name="path1">The first path.</param>
        /// <param name="path2">The second path.</param>
        /// <param name="path3">The third path.</param>
        /// <returns>The combined paths.</returns>
        /// <exception cref="ArgumentException"><paramref name="path2"/> or <paramref name="path3"/> contains an absolute path.</exception>
        /// <remarks>
        /// This method builds a path given the path components.  Unlike <see cref="O:Path.Combine"/> all paths after the first path are assumed
        /// to be relative.  <see langword="null"/> and empty paths are ignored.  Absolute paths after the first path will cause an exception.
        /// </remarks>
        /// <example>
        /// Refer to <see cref="M:BuildPath(System.String,System.String)"/> for an example.
        /// </example>
        [ExcludeFromCodeCoverage]
        public static string BuildPath ( string path1, string path2, string path3 )
        {
            return BuildPathCore(new string[] { path1 ?? "", path2 ?? "", path3 ?? "" });
        }        
        
        /// <summary>Builds a path given the subpaths.</summary>
        /// <param name="path1">The first path.</param>
        /// <param name="path2">The second path.</param>
        /// <param name="path3">The third path.</param>
        /// <param name="paths">Additional paths.</param>
        /// <returns>The combined paths.</returns>
        /// <exception cref="ArgumentException">One or more of the paths (other than the first) contain an absolute path.</exception>
        /// <remarks>
        /// This method builds a path given the path components.  Unlike <see cref="O:Path.Combine"/> all paths after the first path are assumed
        /// to be relative.  <see langword="null"/> and empty paths are ignored.  Absolute paths after the first path will cause an exception.
        /// </remarks>
        /// <example>
        /// Refer to <see cref="M:BuildPath(System.String,System.String)"/> for an example.
        /// </example>
        public static string BuildPath ( string path1, string path2, string path3, params string[] paths )
        {
            var list = new List<string>() { path1, path2, path3 };

            if (paths != null)
                list.AddRange(paths);

            return BuildPathCore(list);
        }                
        #endregion
        
        /// <summary>Compacts a path name to fit into a specific length.</summary>
        /// <param name="path">The path to compact.</param>
        /// <param name="maximumLength">The maximum length of the string.</param>
        /// <returns>The compacted string with an ellipsis added.</returns>
        /// <remarks>
        /// The maximum length must include the ellipsis.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maximumLength"/> is less than one.</exception>
        /// <example>
        /// <code lang="C#">
        ///     Console.WriteLine(PathExtensions.CompactPath(@"C:\Windows\SomeFile.txt", 10); // C:\Wind...
        ///     Console.WriteLine(PathExtensions.CompactPath(@"C:\Windows\SomeFile.txt", 20); // C:\Wind...
        /// </code>
        /// </example>
        [SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "maximumLength+1")]
        public static string CompactPath ( string path, int maximumLength )
        {
            Verify.Argument("maximumLength", maximumLength).IsGreaterThanZero();

            if (String.IsNullOrEmpty(path))
                return "";

            var output = new StringBuilder(MaximumPath);
            SafeNativeMethods.PathCompactPathExW(output, path, (uint)(maximumLength + 1), 0);

            return output.ToString();
        }

        /// <summary>Gets the common path shared by two paths.</summary>
        /// <param name="path1">The first path.</param>
        /// <param name="path2">The second path.</param>
        /// <returns>The common path, if any, or an empty string otherwise.</returns>
        /// <example>
        /// <code lang="C#">
        /// Console.WriteLine(PathExtensions.GetCommonPath(@"C:\Windows", @"C:\Windows\System32"));  // C:\Windows
        /// Console.WriteLine(PathExtensions.GetCommonPath(@"C:\Windows\System32", @"C:\Windows\Temp")); //C:\Windows
        /// Console.WriteLine(PathExtensions.GetCommonPath(@"C:\Windows\System32", @"C:\Windows\Temp")); //C:\Windows
        /// Console.WriteLine(PathExtensions.GetCommonPath(@"C:\Temp", @"D:\Temp")); // (empty)
        /// </code>
        /// </example>
        public static string GetCommonPath ( string path1, string path2 )
        {
            if (String.IsNullOrEmpty(path1) || String.IsNullOrEmpty(path2))
                return "";

            var commonPath = new StringBuilder(MaximumPath);
            if (SafeNativeMethods.PathCommonPrefixW(path1, path2, commonPath) == 0)
                return "";

            return commonPath.ToString();
        }
          
        /// <summary>Gets the full path of <paramref name="pathName"/> when it is relative to <paramref name="basePath"/>.</summary>
        /// <param name="basePath">The path to use as the root path.</param>
        /// <param name="pathName">The path to obtain the full path of.</param>
        /// <returns>The relative path from <paramref name="basePath"/> to <paramref name="pathName"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="basePath"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="basePath"/> is empty or invalid.</exception>
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
        ///									  PathExtensions.GetFullPath(data.RootPath, data.PathName));
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
        /// </example>
        public static string GetFullPath ( string basePath, string pathName )
        {
            Verify.Argument("basePath", basePath).IsNotNullOrEmpty();

            if (String.IsNullOrEmpty(pathName))
                return basePath;
                        
            //There is a chance that another thread could mess this up...
            string old = Environment.CurrentDirectory;
            try
            {
                Environment.CurrentDirectory = basePath;
                return Path.GetFullPath(pathName);
            } finally
            {
                Environment.CurrentDirectory = old;
            }
        }
        
        /// <summary>Gets the relative path from <paramref name="basePath"/> to <paramref name="pathName"/>.</summary>
        /// <param name="basePath">The path to use as the root path.</param>
        /// <param name="pathName">The path to obtain the relative path of.</param>
        /// <returns>The relative path from <paramref name="basePath"/> to <paramref name="pathName"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="basePath"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="basePath"/> is empty or invalid.</exception>
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
        ///									  PathExtensions.GetRelativePath(data.RootPath, data.PathName));
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
        /// </example>
        public static string GetRelativePath ( string basePath, string pathName )
        {
            Verify.Argument("basePath", basePath).IsNotNullOrEmpty();
            pathName = pathName ?? "";

            //Determine if we are dealing with files or directories (use heuristics)
            string file = Path.GetFileName(basePath);
            var fromAttribute = file.Contains(".") ? FileAttributes.Normal : FileAttributes.Directory;

            file = Path.GetFileName(pathName);
            var toAttribute = file.Contains(".") ? FileAttributes.Normal : FileAttributes.Directory;

            System.Text.StringBuilder bldr = new System.Text.StringBuilder();
            bldr.EnsureCapacity(260);
            if (!SafeNativeMethods.PathRelativePathToW(bldr, basePath, fromAttribute, pathName, toAttribute))
                return pathName;

            return bldr.ToString();
        }

        /// <summary>Determines if a path is a relative path.</summary>
        /// <param name="path">The path to check.</param>
        /// <returns><see langword="true"/> if the path is relative or <see langword="false"/> otherwise.</returns>
        /// <example>
        /// <code lang="C#">
        ///     Console.WriteLine(PathExtensions.IsRelative(@"..\Temp") ? "Yes" : "No");  //Yes
        ///     Console.WriteLine(PathExtensions.IsRelative(@"Temp") ? "Yes" : "No");     //Yes
        ///     Console.WriteLine(PathExtensions.IsRelative(@"C:\Temp") ? "Yes" : "No");  //No
        /// </code>
        /// </example>
        public static bool IsRelative ( string path )
        {
            if (String.IsNullOrEmpty(path))
                return false;

            return SafeNativeMethods.PathIsRelativeW(path);
        }

        /// <summary>Determines if a path is in UNC format.</summary>
        /// <param name="path">The path to check.</param>
        /// <returns><see langword="true"/> if the path is in UNC format or <see langword="false"/> otherwise.</returns>
        /// <example>
        /// <code lang="C#">
        ///     Console.WriteLine(PathExtensions.IsUnc(@"\\server\path") ? "Yes" : "No");  //Yes
        ///     Console.WriteLine(PathExtensions.IsUnc(@"C:\Temp") ? "Yes" : "No");  //No
        /// </code>
        /// </example>
        public static bool IsUnc ( string path )
        {
            if (String.IsNullOrEmpty(path))
                return false;

            return SafeNativeMethods.PathIsUNCW(path);
        }
        #endregion
        
        #region Private Members
        
        private static string BuildPathCore ( IEnumerable<string> paths )
        {
            var bldr = new System.Text.StringBuilder();

            bool endsWithSlash = false;
            foreach (var path in paths)
            {
                if (String.IsNullOrEmpty(path))
                    continue;

                //Handle ending slash
                bool beginsWithSlash = path.StartsWith(@"\");
                if (endsWithSlash && beginsWithSlash)
                    bldr.Append(path.Substring(1));
                else if (endsWithSlash || beginsWithSlash)
                    bldr.Append(path);
                else
                {
                    if (bldr.Length > 0)
                        bldr.Append(@"\");

                    bldr.Append(path);
                };

                endsWithSlash = path.EndsWith(@"\");
            };

            if (endsWithSlash)
                return bldr.ToString(0, bldr.Length - 1);
            else
                return bldr.ToString();
        }          
        #endregion
    }
}
