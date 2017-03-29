/*
 * Copyright © Michael Taylor
 * All Rights Reserved
 */
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace P3Net.Kraken
{
    [SuppressUnmanagedCodeSecurity]
    internal static class SafeNativeMethods
    {
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        public static extern int PathCommonPrefixW ( string pszFile1, string pszFile2, System.Text.StringBuilder pszPath );

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PathCompactPathExW ( System.Text.StringBuilder pszOut, string pszSrc, uint cchMax, uint dwFlags );

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PathIsRelativeW ( string lpszPath );

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PathIsUNCW ( string pszPath );     

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PathRelativePathToW ( System.Text.StringBuilder pszPath, string pszFrom, FileAttributes dwAttrFrom, string pszTo, FileAttributes dwAttrTo );     
    }
}
