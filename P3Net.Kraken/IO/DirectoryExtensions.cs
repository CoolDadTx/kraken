/*
 * Copyright © 2005 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.IO
{
    /// <summary>Provides support for working with directories.</summary>
    public static class DirectoryExtensions
    {
        #region GetDirectorySize

        /// <summary>Gets the size of a directory and its children.</summary>
        /// <param name="path">The path to retrieve the size for.</param>
        /// <returns>The size of the directory in bytes.</returns>
        /// <remarks>
        /// Only files in the specified directory are included.        
        /// </remarks>
        /// <example>
        /// Refer to <see cref="M:GetDirectorySize(String, String, SearchOption)"/> for an example.
        /// </example>
        public static ByteSize GetDirectorySize ( string path )
        {
            return GetDirectorySize(path, null, SearchOption.TopDirectoryOnly);
        }

        /// <summary>Gets the size of a directory and its children.</summary>
        /// <param name="path">The path to retrieve the size for.</param>
        /// <param name="searchPattern">The optional search pattern to use.  Can be <see langword="null"/> or empty for all files.</param>
        /// <returns>The size of the directory in bytes.</returns>
        /// <remarks>
        /// Only files in the current directory matching the pattern are included.
        /// </remarks>
        /// <example>
        /// Refer to <see cref="M:GetDirectorySize(String, String, SearchOption)"/> for an example.
        /// </example>
        public static ByteSize GetDirectorySize ( string path, string searchPattern )
        {
            return GetDirectorySize(path, searchPattern, SearchOption.TopDirectoryOnly);
        }

        /// <summary>Gets the size of a directory and its children.</summary>
        /// <param name="path">The path to retrieve the size for.</param>
        /// <param name="options">The search options.</param>
        /// <returns>The size of the directory in bytes.</returns>
        /// <remarks>
        /// Only files in the current directory matching the pattern are included.
        /// </remarks>
        /// <example>
        /// Refer to <see cref="M:GetDirectorySize(String, String, SearchOption)"/> for an example.
        /// </example>
        public static ByteSize GetDirectorySize ( string path, SearchOption options )
        {
            return GetDirectorySize(path, null, options);
        }

        /// <summary>Gets the size of a directory and its children.</summary>
        /// <param name="path">The path to retrieve the size for.</param>
        /// <param name="searchPattern">The optional search pattern to use.  Can be <see langword="null"/> or empty for all files.</param>
        /// <param name="options">The search options.</param>
        /// <returns>The size of the directory in bytes.</returns>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( )
        ///			{
        ///				Console.WriteLine("System32 without children: {0} bytes", DirectoryExtensions.GetDirectorySize(@"C:\Windows\System32", "*.*", SearchOption.TopDirectoryOnly));
        ///				Console.WriteLine("System32 with children: {0} bytes", DirectoryExtensions.GetDirectorySize(@"C:\Windows\System32", "*.*", SearchOption.AllDirectories));
        ///			}
        ///		}
        /// 
        ///		/* Typical output
        /// 
        ///			System32 without children: 509273362 bytes
        ///			System32 with children: 1060451977 bytes
        ///		 */
        /// </code>
        /// </example>
        public static ByteSize GetDirectorySize ( string path, string searchPattern, SearchOption options )
        {
            if (String.IsNullOrEmpty(path))
                return ByteSize.Zero;

            return new DirectoryInfo(path).GetSize(searchPattern, options);            
        }
        #endregion

        #region SafeEnumerateDirectories

        /// <summary>Gets the directories contained in a directory.</summary>
        /// <param name="path">The directory to search.</param>
        /// <remarks>
        /// This method is similar to <see cref="O:Directory.EnumerateDirectories"/> except security exceptions are silently ignored allowing
        /// the retrieval to ignore directories that normally could not be processed.        
        /// <para />
        /// Only the specified directory is searched.  The full path is returned.
        /// </remarks>
        /// <returns>The list of directories.</returns>        
        public static IEnumerable<string> SafeEnumerateDirectories ( string path )
        {
            return SafeEnumerateDirectories(path, null, SearchOption.TopDirectoryOnly);
        }

        /// <summary>Gets the directories contained in a directory.</summary>
        /// <param name="path">The directory to search.</param>
        /// <param name="searchPattern">The search pattern to use.</param>        
        /// <remarks>
        /// This method is similar to <see cref="O:Directory.EnumerateDirectories"/> except security exceptions are silently ignored allowing
        /// the retrieval to ignore directories that normally could not be processed.
        /// <para />
        /// Only the specified directory is searched.  The full path is returned.
        /// </remarks>
        /// <returns>The list of directories.</returns>        
        public static IEnumerable<string> SafeEnumerateDirectories ( string path, string searchPattern )
        {
            return SafeEnumerateDirectories(path, searchPattern, SearchOption.TopDirectoryOnly);
        }

        /// <summary>Gets the directories contained in a directory.</summary>
        /// <param name="path">The directory to search.</param>        
        /// <param name="options">The search options.</param>
        /// <remarks>
        /// This method is similar to <see cref="O:Directory.EnumerateDirectories"/> except security exceptions are silently ignored allowing
        /// the retrieval to ignore directories that normally could not be processed.
        /// <para />
        /// The full path is returned.
        /// </remarks>
        /// <returns>The list of directories.</returns>        
        public static IEnumerable<string> SafeEnumerateDirectories ( string path, SearchOption options )
        {
            return SafeEnumerateDirectories(path, null, options);
        }

        /// <summary>Gets the directories contained in a directory.</summary>
        /// <param name="path">The directory to search.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <param name="options">The search options.</param>
        /// <remarks>
        /// This method is similar to <see cref="O:Directory.EnumerateDirectories"/> except security exceptions are silently ignored allowing
        /// the retrieval to ignore directories that normally could not be processed.
        /// <para />
        /// The full path is returned.
        /// </remarks>
        /// <returns>The list of directories.</returns>        
        public static IEnumerable<string> SafeEnumerateDirectories ( string path, string searchPattern, SearchOption options )
        {
            if (String.IsNullOrEmpty(path))
                return Enumerable.Empty<string>();

            var dir = new DirectoryInfo(path);
            var children = dir.SafeEnumerateDirectories(searchPattern, options);

            return (from c in children
                    select c.FullName);
        }
        #endregion

        #region SafeEnumerateFiles

        /// <summary>Gets the files contained in a directory.</summary>
        /// <param name="path">The directory to search.</param>
        /// <remarks>
        /// This method is similar to <see cref="O:Directory.EnumerateFiles"/> except security exceptions are silently ignored allowing
        /// the retrieval to ignore directories that normally could not be processed.        
        /// <para />
        /// Only the specified directory is searched.  The full path is returned.
        /// </remarks>
        /// <returns>The list of files.</returns>        
        public static IEnumerable<string> SafeEnumerateFiles ( string path )
        {            
            return SafeEnumerateFiles(path, null, SearchOption.TopDirectoryOnly);
        }

        /// <summary>Gets the files contained in a directory.</summary>
        /// <param name="path">The directory to search.</param>
        /// <param name="searchPattern">The search pattern to use.</param>        
        /// <remarks>
        /// This method is similar to <see cref="O:Directory.EnumerateFiles"/> except security exceptions are silently ignored allowing
        /// the retrieval to ignore directories that normally could not be processed.
        /// <para />
        /// Only the specified directory is searched.  The full path is returned.
        /// </remarks>
        /// <returns>The list of files.</returns>        
        public static IEnumerable<string> SafeEnumerateFiles ( string path, string searchPattern )
        {
            return SafeEnumerateFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
        }

        /// <summary>Gets the files contained in a directory.</summary>
        /// <param name="path">The directory to search.</param>
        /// <param name="options">The search options.</param>
        /// <remarks>
        /// This method is similar to <see cref="O:Directory.EnumerateFiles"/> except security exceptions are silently ignored allowing
        /// the retrieval to ignore directories that normally could not be processed.
        /// <para />
        /// The full path is returned.
        /// </remarks>
        /// <returns>The list of files.</returns>        
        public static IEnumerable<string> SafeEnumerateFiles ( string path, SearchOption options )
        {
            return SafeEnumerateFiles(path, null, options);
        }

        /// <summary>Gets the files contained in a directory.</summary>
        /// <param name="path">The directory to search.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <param name="options">The search options.</param>
        /// <remarks>
        /// This method is similar to <see cref="O:Directory.EnumerateFiles"/> except security exceptions are silently ignored allowing
        /// the retrieval to ignore directories that normally could not be processed.
        /// <para />
        /// The full path is returned.
        /// </remarks>
        /// <returns>The list of files.</returns>        
        public static IEnumerable<string> SafeEnumerateFiles ( string path, string searchPattern, SearchOption options )
        {
            if (String.IsNullOrEmpty(path))
                return Enumerable.Empty<string>();
                        
            var dir = new DirectoryInfo(path);
            var children = dir.SafeEnumerateFiles(searchPattern, options);

            return (from c in children
                    select c.FullName);
        }
        #endregion
    }
}
