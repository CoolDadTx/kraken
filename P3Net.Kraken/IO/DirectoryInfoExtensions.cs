/*
 * Copyright © 2005 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Security;

using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.IO
{
    /// <summary>Provides support for working with <see cref="DirectoryInfo"/>.</summary>
    public static class DirectoryInfoExtensions
    {
        #region Public Members
        
        #region GetSize

        /// <summary>Gets the size of a directory and its children.</summary>
        /// <param name="source">The source object.</param>        
        /// <returns>The size of the directory in bytes.</returns>
        /// <remarks>
        /// Only files in the directory are included.
        /// </remarks>
        /// <example>
        /// Refer to <see cref="M:GetSize(String, SearchOption)"/> for an example.
        /// </example>
        public static ByteSize GetSize ( this DirectoryInfo source )
        {
            return source.GetSize(null, SearchOption.TopDirectoryOnly);
        }

        /// <summary>Gets the size of a directory and its children.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="searchPattern">The optional search pattern to use.  Can be <see langword="null"/> or empty for all files.</param>
        /// <returns>The size of the directory in bytes.</returns>
        /// <remarks>
        /// Only files in the directory matching the pattern are included.
        /// </remarks>
        /// <example>
        /// Refer to <see cref="M:GetSize(String, SearchOption)"/> for an example.
        /// </example>
        public static ByteSize GetSize ( this DirectoryInfo source, string searchPattern )
        {
            return source.GetSize(searchPattern, SearchOption.TopDirectoryOnly);
        }

        /// <summary>Gets the size of a directory and its children.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="options">The search options.</param>
        /// <returns>The size of the directory in bytes.</returns>
        /// <remarks>
        /// All the files are included.
        /// </remarks>        
        /// <example>
        /// Refer to <see cref="M:GetSize(String, SearchOption)"/> for an example.
        /// </example>
        public static ByteSize GetSize ( this DirectoryInfo source, SearchOption options )
        {
            return source.GetSize(null, options);
        }

        /// <summary>Gets the size of a directory and its children.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="searchPattern">The optional search pattern to use.  Can be <see langword="null"/> or empty for all files.</param>
        /// <param name="options">The search options.</param>
        /// <returns>The size of the directory in bytes.</returns>
        /// <example>
        /// <code lang="C#">
        ///		public class App
        ///		{
        ///			static void Main ( )
        ///			{
        ///			   var dir = new DirectoryInfo(@"C:\Windows\System32");
        ///		          Console.WriteLine("System32 without children: {0} bytes", dir.GetSize());
        ///			   Console.WriteLine("System32 with children: {0} bytes", dir.GetSize("*.*", SearchOption.AllDirectories);
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
        public static ByteSize GetSize ( this DirectoryInfo source, string searchPattern, SearchOption options )
        {
            if (!source.Exists)
                return ByteSize.Zero;

            //Safely enumerate the files in the directory - wish there were a faster approach
            long size = 0;
            foreach (var file in source.SafeEnumerateFiles(searchPattern, options))
            {
                size += file.Length;
            };

            return new ByteSize(size);
        }
        #endregion

        #region SafeEnumerateDirectories

        /// <summary>Gets the directories contained in a directory.</summary>
        /// <param name="source">The source object.</param>
        /// <remarks>
        /// This method is similar to <see cref="O:DirectoryInfo.EnumerateDirectories"/> except security exceptions are silently ignored allowing
        /// the retrieval to ignore directories that normally could not be processed.        
        /// <para />
        /// Only the specified directory is searched.
        /// </remarks>
        /// <returns>The list of directories.</returns>        
        public static IEnumerable<DirectoryInfo> SafeEnumerateDirectories ( this DirectoryInfo source )
        {            
            return source.SafeEnumerateDirectories(null, SearchOption.TopDirectoryOnly);
        }

        /// <summary>Gets the directories contained in a directory.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="searchPattern">The search pattern to use.</param>        
        /// <remarks>
        /// This method is similar to <see cref="O:Directory.EnumerateDirectories"/> except security exceptions are silently ignored allowing
        /// the retrieval to ignore directories that normally could not be processed.
        /// <para />
        /// Only the specified directory is searched.
        /// </remarks>
        /// <returns>The list of directories.</returns>        
        public static IEnumerable<DirectoryInfo> SafeEnumerateDirectories ( this DirectoryInfo source, string searchPattern )
        {
            return source.SafeEnumerateDirectories(searchPattern, SearchOption.TopDirectoryOnly);
        }

        /// <summary>Gets the directories contained in a directory.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="options">The search options.</param>
        /// <remarks>
        /// This method is similar to <see cref="O:Directory.EnumerateDirectories"/> except security exceptions are silently ignored allowing
        /// the retrieval to ignore directories that normally could not be processed.
        /// </remarks>
        /// <returns>The list of directories.</returns>        
        public static IEnumerable<DirectoryInfo> SafeEnumerateDirectories ( this DirectoryInfo source, SearchOption options )
        {
            return source.SafeEnumerateDirectories(null, options);
        }

        /// <summary>Gets the directories contained in a directory.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <param name="options">The search options.</param>
        /// <remarks>
        /// This method is similar to <see cref="O:Directory.EnumerateDirectories"/> except security exceptions are silently ignored allowing
        /// the retrieval to ignore directories that normally could not be processed.
        /// </remarks>
        /// <returns>The list of directories.</returns>        
        public static IEnumerable<DirectoryInfo> SafeEnumerateDirectories ( this DirectoryInfo source, string searchPattern, SearchOption options )
        {
            if (!source.Exists)
                yield break;

            if (String.IsNullOrEmpty(searchPattern))
                searchPattern = "*";

            //Push the root onto the stack
            var stack = new Stack<DirectoryInfo>();
            stack.Push(source);

            bool includeChildren = options.HasFlag(SearchOption.AllDirectories);

            //Until we run out of directories (or we don't want all of them)
            do
            {
                //Pop the next directory
                var current = stack.Pop();

                //Get the child directories if possible
                var children = TryEnumerateDirectories(current, searchPattern);
                if (children != null)
                {
                    //For each child directory push it onto the stack and return it - we're ass
                    foreach (var child in children)
                    {
                        if (includeChildren)   
                            stack.Push(child);

                        yield return child;
                    };
                };
            } while (stack.Any());
        }
        #endregion

        #region SafeEnumerateFiles

        /// <summary>Gets the files contained in a directory.</summary>
        /// <param name="source">The source object.</param>
        /// <remarks>
        /// This method is similar to <see cref="O:Directory.EnumerateFiles"/> except security exceptions are silently ignored allowing
        /// the retrieval to ignore directories that normally could not be processed.        
        /// <para />
        /// Only the specified directory is searched.
        /// </remarks>
        /// <returns>The list of files.</returns>        
        public static IEnumerable<FileInfo> SafeEnumerateFiles ( this DirectoryInfo source )
        {            
            return source.SafeEnumerateFiles(null, SearchOption.TopDirectoryOnly);
        }

        /// <summary>Gets the files contained in a directory.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="searchPattern">The search pattern to use.</param>        
        /// <remarks>
        /// This method is similar to <see cref="O:Directory.EnumerateFiles"/> except security exceptions are silently ignored allowing
        /// the retrieval to ignore directories that normally could not be processed.
        /// <para />
        /// Only the specified directory is searched.
        /// </remarks>
        /// <returns>The list of files.</returns>        
        public static IEnumerable<FileInfo> SafeEnumerateFiles ( this DirectoryInfo source, string searchPattern )
        {
            return source.SafeEnumerateFiles(searchPattern, SearchOption.TopDirectoryOnly);
        }

        /// <summary>Gets the files contained in a directory.</summary>
        /// <param name="source">The source object.</param>        
        /// <param name="options">The search options.</param>
        /// <remarks>
        /// This method is similar to <see cref="O:Directory.EnumerateFiles"/> except security exceptions are silently ignored allowing
        /// the retrieval to ignore directories that normally could not be processed.
        /// </remarks>
        /// <returns>The list of files.</returns>        
        public static IEnumerable<FileInfo> SafeEnumerateFiles ( this DirectoryInfo source, SearchOption options )
        {
            return source.SafeEnumerateFiles(null, options);
        }

        /// <summary>Gets the files contained in a directory.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="searchPattern">The search pattern to use.</param>
        /// <param name="options">The search options.</param>
        /// <remarks>
        /// This method is similar to <see cref="O:Directory.EnumerateFiles"/> except security exceptions are silently ignored allowing
        /// the retrieval to ignore directories that normally could not be processed.
        /// </remarks>
        /// <returns>The list of files.</returns>        
        public static IEnumerable<FileInfo> SafeEnumerateFiles ( this DirectoryInfo source, string searchPattern, SearchOption options )
        {
            if (!source.Exists)
                yield break;

            if (String.IsNullOrEmpty(searchPattern))
                searchPattern = "*.*";

            //Enumerate the root files first
            var children = TryEnumerateFiles(source, searchPattern);
            if (children != null)
            {
                foreach (var child in children)
                    yield return child;
            };

            //If we want all directories then enumerate the children
            if (options.HasFlag(SearchOption.AllDirectories))
            {
                foreach (var dir in SafeEnumerateDirectories(source, null, options))
                {
                    children = TryEnumerateFiles(dir, searchPattern);
                    if (children != null)
                    {
                        foreach (var child in children)
                            yield return child;
                    };         
                };
            };
        }
        #endregion

        #endregion

        #region Private Members

        [ExcludeFromCodeCoverage]
        private static IEnumerable<DirectoryInfo> TryEnumerateDirectories ( DirectoryInfo dir, string searchPattern )
        {            
            try
            {                   
                //Relying on the fact that the returned enumor has already triggered the first fetch so if we didn't have 
                //read access it would fail here
                return dir.EnumerateDirectories(searchPattern, SearchOption.TopDirectoryOnly);
            } catch (UnauthorizedAccessException)
            { /* Ignore */
            } catch (SecurityException)
            { /* Ignore */ };

            return null;
        }

        [ExcludeFromCodeCoverage]
        private static IEnumerable<FileInfo> TryEnumerateFiles ( DirectoryInfo dir, string searchPattern )
        {
            try
            {
                //Relying on the fact that the returned enumor has already triggered the first fetch so if we didn't have 
                //read access it would fail here
                return dir.EnumerateFiles(searchPattern, SearchOption.TopDirectoryOnly);
            } catch (UnauthorizedAccessException)
            { /* Ignore */
            } catch (SecurityException)
            { /* Ignore */ };

            return null;
        }
        #endregion
    }
}
