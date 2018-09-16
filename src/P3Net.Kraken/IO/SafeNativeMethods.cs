/*
 * Copyright © Michael Taylor
 * All Rights Reserved
 */
using System;
using System.IO;

#if NET_FRAMEWORK

using System.Runtime.InteropServices;
#endif

using System.Security;
using System.Text;

using P3Net.Kraken;

namespace P3Net.Kraken.IO
{ 
    [SuppressUnmanagedCodeSecurity]
    internal static class SafeNativeMethods
    {
        public static string PathCompactPathEx ( string sourcePath, int maximumLength )
        {
#if NET_FRAMEWORK

            var bufferLength = maximumLength + 1;
            var output = new StringBuilder(bufferLength);
            PathCompactPathExW(output, sourcePath, (uint)bufferLength, 0);

            return output.ToString();
#else
            var pathPrefix = "..." + s_directorySeparator;

            //Special case a length that is too small for even the starter characters
            if (maximumLength <= 4)
                return pathPrefix.Left(maximumLength);

            //Algorithm - based upon current Windows behavior
            // If path length < max length return it
            // Get the filename (everything after last slash, if any, irrelevant of whether it is a filename or not)
            // If the filename length + ...\ greater than max length then trim filename down to fit with ellipsis on end
            // Calculate remaining space on front of string minus the ellipsis and concat the values
            var filename = Path.GetFileName(sourcePath);
            if (filename.Length + 4 > maximumLength)
            {
                //Trim the filename
                return pathPrefix + filename.Left(Math.Max(maximumLength - 7, 1)) + "...";
            };

            //The beginning of the path is limited to whatever space is left
            var remainingSpace = Math.Max(maximumLength - (filename.Length + 4), 0);
            return sourcePath.Left(remainingSpace) + pathPrefix + filename;
#endif
        }

        public static string PathCommonPrefix ( string path1, string path2 )
        {
#if NET_FRAMEWORK
            var commonPath = new StringBuilder(PathExtensions.MaximumPath);
            if (PathCommonPrefixW(path1, path2, commonPath) == 0)
                return "";

            return commonPath.ToString();
#else
            //Algorithm - based upon Windows behavior
            // Ensure path1 is the shortest path so we can simplify the enumeration
            // Enumerate the characters
            //    If we find a separator then this marks the next valid point in a common path
            //    If we find a mismatch then everything up to the last separator is the common path
            // If we run out of characters and there are still characters in the longer path and we aren't at a directory then return the last separator
            // Special case a drive letter so it returns X:\ instead of just X:
            // Treat relative paths the same as full paths
            if (path1.Length > path2.Length)
            {
                var tmp = path1;
                path1 = path2;
                path2 = tmp;
            };

            var current = 0;
            var lastSeparator = 0;
            while (current < path1.Length)
            {
                //Keep comparing characters until we find a difference
                if (Char.ToLowerInvariant(path1[current]) != Char.ToLowerInvariant(path2[current]))
                {
                    //They don't match so everything up to the last slash is common
                    return EnsureVolumeFormatted(path1.Left(lastSeparator));
                } else if (path1[current] == Path.DirectorySeparatorChar)
                {
                    //We've reached a sequence point so store it for later
                    lastSeparator = current;
                };

                ++current;
            };

            //We got to the end of the shortest path, if the longest path doesn't have a slash here then we were partially in a path 
            if (current < path2.Length && path2[current] != Path.DirectorySeparatorChar)
                return EnsureVolumeFormatted(path1.Left(lastSeparator));

            return EnsureVolumeFormatted(path1);
#endif
        }

        public static string PathRelativePathTo ( string basePath, bool baseIsFile, string pathName )
        {
#if NET_FRAMEWORK

            //Determine if we are dealing with files or directories (use heuristics)
            string file = Path.GetFileName(basePath);
            var fromAttribute = file.Contains(".") ? FileAttributes.Normal : FileAttributes.Directory;

            file = Path.GetFileName(pathName);
            var toAttribute = file.Contains(".") ? FileAttributes.Normal : FileAttributes.Directory;

            var bldr = new StringBuilder(PathExtensions.MaximumPath);
            if (!PathRelativePathToW(bldr, basePath, fromAttribute, pathName, toAttribute))
                return pathName;

            return bldr.ToString();
#else

            //Algorithm - based upon behavior of Windows PathRelativePathToW
            // Both paths must share a common prefix but don't have to be full paths
            // Find common path, if none then return path name
            //   If common path is not entire base path then prefix .. until back to common path
            //   If common path is base path then prefix .
            //   Append path name without common path

            //If the base path is a file then strip off the filename as it won't matter
            if (baseIsFile)
                basePath = Path.GetDirectoryName(basePath);
            basePath = basePath.EnsureEndsWith(s_directorySeparator);

            var commonPath = PathCommonPrefix(basePath, pathName);
            if (String.IsNullOrEmpty(commonPath))
                return pathName;

            //Normalize the paths
            commonPath = commonPath.EnsureEndsWith(s_directorySeparator);

            //If the base path is the common path
            var relativePath = "";
            if (String.Compare(commonPath, basePath, true) == 0)
                relativePath = ".";
            else
            {
                //Count the directories after the prefix (we added slash earlier so we'll be skipping the first one)
                var buffer = new StringBuilder();
                var directoryIndice = basePath.IndexOfAll(Path.DirectorySeparatorChar, commonPath.Length);
                foreach (var directoryIndex in directoryIndice)
                    buffer.Append(".." + s_directorySeparator);

                relativePath = buffer.ToString();
            };

            //Strip off the common path from the path name and append to relative path
            return relativePath.EnsureEndsWith(s_directorySeparator) + pathName.Substring(commonPath.Length);
#endif
        }

        public static bool PathIsRelative ( string path )
        {
#if NET_FRAMEWORK
            return PathIsRelativeW(path);
#else 
            // Algorithm - based upon the Windows PathIsRelativeW function
            // A path isn't relative if it has any of the following conditions
            //   Contains a volume separator
            //   Starts with a directory separator
            return !path.StartsWith(s_directorySeparator) && !path.Contains(Path.VolumeSeparatorChar.ToString());
#endif
        }

        public static bool PathIsUNC ( string path )
        {
#if NET_FRAMEWORK
            return PathIsUNCW(path);
#else
            return path[0] == '\\' && path[1] == '\\';
#endif
        }

        #region Private Members

        private static readonly string s_directorySeparator = Path.DirectorySeparatorChar.ToString();

        private static string EnsureVolumeFormatted ( string value )
        {
            if (value?.Length <= 1)
                return value;

            if (value[value.Length - 1] == Path.VolumeSeparatorChar)
                return value + s_directorySeparator;

            return value;
        }        

#if NET_FRAMEWORK
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        private static extern int PathCommonPrefixW ( string pszFile1, string pszFile2, System.Text.StringBuilder pszPath );

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PathCompactPathExW ( System.Text.StringBuilder pszOut, string pszSrc, uint cchMax, uint dwFlags );

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PathIsRelativeW ( string lpszPath );

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PathIsUNCW ( string pszPath );

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PathRelativePathToW ( System.Text.StringBuilder pszPath, string pszFrom, FileAttributes dwAttrFrom, string pszTo, FileAttributes dwAttrTo );
#endif

        #endregion
    }
}