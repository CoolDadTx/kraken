/*
 * Copyright © 2006 Michael Taylor
 * All rights reserved.
 */
#region Imports

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using COM = System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Text;

using Microsoft.Win32;

#endregion

namespace P3Net.Kraken.Net
{
    //Native methods for WinINET API.
    [SuppressUnmanagedCodeSecurity]
    internal static class WinINetNative
    {
        #region Error Codes

        public const uint ERROR_NO_MORE_FILES			= 18;

        public const uint INTERNET_ERROR_BASE = 12000;

        public const uint ERROR_INTERNET_OUT_OF_HANDLES = (INTERNET_ERROR_BASE + 1);
        public const uint ERROR_INTERNET_TIMEOUT = (INTERNET_ERROR_BASE + 2);
        public const uint ERROR_INTERNET_EXTENDED_ERROR = (INTERNET_ERROR_BASE + 3);
        public const uint ERROR_INTERNET_INTERNAL_ERROR = (INTERNET_ERROR_BASE + 4);
        public const uint ERROR_INTERNET_INVALID_URL = (INTERNET_ERROR_BASE + 5);
        public const uint ERROR_INTERNET_UNRECOGNIZED_SCHEME = (INTERNET_ERROR_BASE + 6);
        public const uint ERROR_INTERNET_NAME_NOT_RESOLVED = (INTERNET_ERROR_BASE + 7);
        public const uint ERROR_INTERNET_PROTOCOL_NOT_FOUND = (INTERNET_ERROR_BASE + 8);
        public const uint ERROR_INTERNET_INVALID_OPTION = (INTERNET_ERROR_BASE + 9);
        public const uint ERROR_INTERNET_BAD_OPTION_LENGTH = (INTERNET_ERROR_BASE + 10);
        public const uint ERROR_INTERNET_OPTION_NOT_SETTABLE = (INTERNET_ERROR_BASE + 11);
        public const uint ERROR_INTERNET_SHUTDOWN = (INTERNET_ERROR_BASE + 12);
        public const uint ERROR_INTERNET_INCORRECT_USER_NAME = (INTERNET_ERROR_BASE + 13);
        public const uint ERROR_INTERNET_INCORRECT_PASSWORD = (INTERNET_ERROR_BASE + 14);
        public const uint ERROR_INTERNET_LOGIN_FAILURE = (INTERNET_ERROR_BASE + 15);
        public const uint ERROR_INTERNET_INVALID_OPERATION = (INTERNET_ERROR_BASE + 16);
        public const uint ERROR_INTERNET_OPERATION_CANCELLED = (INTERNET_ERROR_BASE + 17);
        public const uint ERROR_INTERNET_INCORRECT_HANDLE_TYPE = (INTERNET_ERROR_BASE + 18);
        public const uint ERROR_INTERNET_INCORRECT_HANDLE_STATE = (INTERNET_ERROR_BASE + 19);
        public const uint ERROR_INTERNET_NOT_PROXY_REQUEST = (INTERNET_ERROR_BASE + 20);
        public const uint ERROR_INTERNET_REGISTRY_VALUE_NOT_FOUND = (INTERNET_ERROR_BASE + 21);
        public const uint ERROR_INTERNET_BAD_REGISTRY_PARAMETER = (INTERNET_ERROR_BASE + 22);
        public const uint ERROR_INTERNET_NO_DIRECT_ACCESS = (INTERNET_ERROR_BASE + 23);
        public const uint ERROR_INTERNET_NO_CONTEXT = (INTERNET_ERROR_BASE + 24);
        public const uint ERROR_INTERNET_NO_CALLBACK = (INTERNET_ERROR_BASE + 25);
        public const uint ERROR_INTERNET_REQUEST_PENDING = (INTERNET_ERROR_BASE + 26);
        public const uint ERROR_INTERNET_INCORRECT_FORMAT = (INTERNET_ERROR_BASE + 27);
        public const uint ERROR_INTERNET_ITEM_NOT_FOUND = (INTERNET_ERROR_BASE + 28);
        public const uint ERROR_INTERNET_CANNOT_CONNECT = (INTERNET_ERROR_BASE + 29);
        public const uint ERROR_INTERNET_CONNECTION_ABORTED = (INTERNET_ERROR_BASE + 30);
        public const uint ERROR_INTERNET_CONNECTION_RESET = (INTERNET_ERROR_BASE + 31);
        public const uint ERROR_INTERNET_FORCE_RETRY = (INTERNET_ERROR_BASE + 32);
        public const uint ERROR_INTERNET_INVALID_PROXY_REQUEST = (INTERNET_ERROR_BASE + 33);
        public const uint ERROR_INTERNET_NEED_UI = (INTERNET_ERROR_BASE + 34);
        public const uint ERROR_INTERNET_HANDLE_EXISTS = (INTERNET_ERROR_BASE + 36);
        public const uint ERROR_INTERNET_SEC_CERT_DATE_INVALID = (INTERNET_ERROR_BASE + 37);
        public const uint ERROR_INTERNET_SEC_CERT_CN_INVALID = (INTERNET_ERROR_BASE + 38);
        public const uint ERROR_INTERNET_HTTP_TO_HTTPS_ON_REDIR = (INTERNET_ERROR_BASE + 39);
        public const uint ERROR_INTERNET_HTTPS_TO_HTTP_ON_REDIR = (INTERNET_ERROR_BASE + 40);
        public const uint ERROR_INTERNET_MIXED_SECURITY		= (INTERNET_ERROR_BASE + 41);
        public const uint ERROR_INTERNET_CHG_POST_IS_NON_SECURE = (INTERNET_ERROR_BASE + 42);
        public const uint ERROR_INTERNET_POST_IS_NON_SECURE = (INTERNET_ERROR_BASE + 43);
        public const uint ERROR_INTERNET_CLIENT_AUTH_CERT_NEEDED = (INTERNET_ERROR_BASE + 44);
        public const uint ERROR_INTERNET_INVALID_CA = (INTERNET_ERROR_BASE + 45);
        public const uint ERROR_INTERNET_CLIENT_AUTH_NOT_SETUP = (INTERNET_ERROR_BASE + 46);
        public const uint ERROR_INTERNET_ASYNC_THREAD_FAILED = (INTERNET_ERROR_BASE + 47);
        public const uint ERROR_INTERNET_REDIRECT_SCHEME_CHANGE = (INTERNET_ERROR_BASE + 48);
        public const uint ERROR_INTERNET_DIALOG_PENDING = (INTERNET_ERROR_BASE + 49);
        public const uint ERROR_INTERNET_RETRY_DIALOG = (INTERNET_ERROR_BASE + 50);
        public const uint ERROR_INTERNET_HTTPS_HTTP_SUBMIT_REDIR = (INTERNET_ERROR_BASE + 52);
        public const uint ERROR_INTERNET_INSERT_CDROM = (INTERNET_ERROR_BASE + 53);
        public const uint ERROR_INTERNET_FORTEZZA_LOGIN_NEEDED = (INTERNET_ERROR_BASE + 54);
        public const uint ERROR_INTERNET_SEC_CERT_ERRORS = (INTERNET_ERROR_BASE + 55);
        public const uint ERROR_INTERNET_SEC_CERT_NO_REV = (INTERNET_ERROR_BASE + 56);
        public const uint ERROR_INTERNET_SEC_CERT_REV_FAILED = (INTERNET_ERROR_BASE + 57);

        #endregion

        #region Types and Data

        //For FptFindFirstFile
        [Flags]
        public enum InternetFindFlagType : uint
        {
            Hyperlink = 0x00000400,
            NeedFile = 0x00000010,
            NoCacheWrite = 0x04000000,
            Reload = 0x80000000,
            Resynchronize = 0x00000800,
        }

        //For InternetOpen
        [Flags]
        public enum InternetFlagType : uint
        {
            FromCache	= 0x01000000,
            Async		= 0x10000000,
            Offline		= InternetFlagType.FromCache,
        }

        //For InternetOpen
        public enum InternetOpenType : uint
        {
            Preconfig				= 0,
            Direct					= 1,
            Proxy					= 3,			
            PreconfigWithNoAutoProxy= 4,
        }

        //For InternetConnect
        public enum InternetPortType : ushort
        {
            Invalid = 0,
            FTP		= 21,
            Gopher	= 70,
            HTTP	= 80,
            HTTPS	= 443,
            SOCKS	= 1080,
        }

        //For InternetConnect
        public enum InternetServiceType : uint
        {
            FTP		= 1,
            Gopher	= 2,
            HTTP	= 3,
        }

        //For Find...
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct WIN32_FIND_DATA
        {
            public uint dwFileAttributes;

            public COM.FILETIME ftCreationTime;
            public COM.FILETIME ftLastAccessTime;
            public COM.FILETIME ftLastWriteTime;

            public uint nFileSizeHigh;
            public uint nFileSizeLow;

            public uint nReserved0;
            public uint nReserved1;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string cFileName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternateFileName;
        }
        #endregion

        #region Methods

        //MODIFIED: MLT - 2/4/06 CA1822 - Make static
        //Builds an exception around GetLastError()
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "Fortezza")]
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public static Exception GetLastError ( )
        {
            uint nErr = (uint)Marshal.GetLastWin32Error();

            //The Win32Exception class can't handle the INet errors so parse them 
            string strMsg = "";
            switch (nErr)
            {
                case ERROR_INTERNET_OUT_OF_HANDLES: strMsg = "Out of handles."; break;
                case ERROR_INTERNET_TIMEOUT: strMsg = "Timeout expired."; break;
                case ERROR_INTERNET_INTERNAL_ERROR: strMsg = "Internal error."; break;
                case ERROR_INTERNET_INVALID_URL: strMsg = "Invalid URL."; break;
                case ERROR_INTERNET_UNRECOGNIZED_SCHEME: strMsg = "Unrecognized scheme."; break;
                case ERROR_INTERNET_NAME_NOT_RESOLVED: strMsg = "Name not resolved."; break;
                case ERROR_INTERNET_PROTOCOL_NOT_FOUND: strMsg = "Protocol not found."; break;
                case ERROR_INTERNET_INVALID_OPTION: strMsg = "Invalid option."; break;
                case ERROR_INTERNET_BAD_OPTION_LENGTH: strMsg = "Bad option length."; break;
                case ERROR_INTERNET_OPTION_NOT_SETTABLE: strMsg = "Option not settable."; break;
                case ERROR_INTERNET_SHUTDOWN: strMsg = "Shutdown in progress."; break;
                case ERROR_INTERNET_INCORRECT_USER_NAME:
                case ERROR_INTERNET_INCORRECT_PASSWORD:
                case ERROR_INTERNET_LOGIN_FAILURE: strMsg = "Bad username or password."; break;
                case ERROR_INTERNET_INVALID_OPERATION: return new InvalidOperationException("Invalid operation.");
                case ERROR_INTERNET_OPERATION_CANCELLED: strMsg = "Operation cancelled."; break;
                case ERROR_INTERNET_INCORRECT_HANDLE_TYPE: strMsg = "Incorrect handle type."; break;
                case ERROR_INTERNET_INCORRECT_HANDLE_STATE: strMsg = "Incorrect handle state."; break;
                case ERROR_INTERNET_NOT_PROXY_REQUEST: strMsg = "Not a proxy request."; break;
                case ERROR_INTERNET_REGISTRY_VALUE_NOT_FOUND: strMsg = "Registry value not found."; break;
                case ERROR_INTERNET_BAD_REGISTRY_PARAMETER: strMsg = "Bad registry parameter."; break;
                case ERROR_INTERNET_NO_DIRECT_ACCESS: strMsg = "No direct access allowed."; break;
                case ERROR_INTERNET_NO_CONTEXT: strMsg = "No context available."; break;
                case ERROR_INTERNET_NO_CALLBACK: strMsg = "Callback not supported."; break;
                case ERROR_INTERNET_REQUEST_PENDING: strMsg = "Request pending."; break;
                case ERROR_INTERNET_INCORRECT_FORMAT: strMsg = "Incorrect format."; break;
                case ERROR_INTERNET_ITEM_NOT_FOUND: strMsg = "Item not found."; break;
                case ERROR_INTERNET_CANNOT_CONNECT: strMsg = "Connection failed."; break;
                case ERROR_INTERNET_CONNECTION_ABORTED: strMsg = "Connection aborted."; break;
                case ERROR_INTERNET_CONNECTION_RESET: strMsg = "Connection reset."; break;
                case ERROR_INTERNET_FORCE_RETRY: strMsg = "Forceful retry."; break;
                case ERROR_INTERNET_INVALID_PROXY_REQUEST: strMsg = "Invalid proxy request."; break;
                case ERROR_INTERNET_NEED_UI: strMsg = "UI needed."; break;
                case ERROR_INTERNET_HANDLE_EXISTS: strMsg = "Handle already exists."; break;
                case ERROR_INTERNET_SEC_CERT_DATE_INVALID: strMsg = "Security certificate date is invalid."; break;
                case ERROR_INTERNET_SEC_CERT_CN_INVALID: strMsg = "Security certificate is invalid."; break;
                case ERROR_INTERNET_HTTP_TO_HTTPS_ON_REDIR: strMsg = "Redirected to HTTPS server."; break;
                case ERROR_INTERNET_HTTPS_TO_HTTP_ON_REDIR: strMsg = "Redirected to HTTP server."; break;
                case ERROR_INTERNET_MIXED_SECURITY: strMsg = "Mixed security error."; break;
                case ERROR_INTERNET_CHG_POST_IS_NON_SECURE: strMsg = "Change post is not secure."; break;
                case ERROR_INTERNET_POST_IS_NON_SECURE: strMsg = "POst is not secure."; break;
                case ERROR_INTERNET_CLIENT_AUTH_CERT_NEEDED: strMsg = "Authentication required."; break;
                case ERROR_INTERNET_INVALID_CA:
                case ERROR_INTERNET_CLIENT_AUTH_NOT_SETUP: strMsg = "Invalid authentication."; break;
                case ERROR_INTERNET_ASYNC_THREAD_FAILED: strMsg = "Thread failure."; break;
                case ERROR_INTERNET_REDIRECT_SCHEME_CHANGE: strMsg = "Redirect scheme change."; break;
                case ERROR_INTERNET_DIALOG_PENDING: strMsg = "Dialog pending."; break;
                case ERROR_INTERNET_RETRY_DIALOG: strMsg = "Retry dialog."; break;
                case ERROR_INTERNET_HTTPS_HTTP_SUBMIT_REDIR: strMsg = "HTTPS to HTTP submit redirection."; break;
                case ERROR_INTERNET_INSERT_CDROM: strMsg = "Insert CD-ROM."; break;
                case ERROR_INTERNET_FORTEZZA_LOGIN_NEEDED: strMsg = "Fortezza login needed."; break;
                case ERROR_INTERNET_SEC_CERT_ERRORS:
                case ERROR_INTERNET_SEC_CERT_NO_REV:
                case ERROR_INTERNET_SEC_CERT_REV_FAILED: strMsg = "Certificate error."; break;

                case ERROR_INTERNET_EXTENDED_ERROR:
                    {
                        uint nSize = 1024;
                        StringBuilder msg = new StringBuilder((int)nSize);
                        if (InternetGetLastResponseInfo(ref nErr, msg, ref nSize))
                            strMsg = msg.ToString();

                        break;
                    };
            };

            return new Win32Exception((int)nErr, strMsg);
        }		
        #endregion

        #region Imports

        [DllImport("WinInet.dll", SetLastError=true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool InternetCloseHandle ( IntPtr hInternet );

        [DllImport("WinInet.dll", CharSet=CharSet.Auto, SetLastError=true)]
        public static extern IntPtr InternetConnect ( IntPtr hInternet, string lpstrServerName, 
                                                       int nServerPort, string lpszUserName, 
                                                       string lpszPassword, InternetServiceType dwService,
                                                       uint dwFlags, ref uint dwContext );

        [DllImport("WinInet.dll", CharSet=CharSet.Auto, SetLastError=true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool InternetFindNextFile ( IntPtr hFind, ref WIN32_FIND_DATA lpvFindData );
        
        [DllImport("WinInet.dll", CharSet=CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool InternetGetLastResponseInfo ( ref uint lpdwError, StringBuilder lpszBuffer,
                                                                 ref uint lpdwBufferLength );

        [DllImport("WinInet.dll", CharSet=CharSet.Auto, SetLastError=true)]
        public static extern IntPtr InternetOpen ( string lpszAgent, InternetOpenType dwAccessType, 
                                                    string lpszProxyName, string lpszProxyBypass, uint dwFlags );
        #endregion 
    }

}
