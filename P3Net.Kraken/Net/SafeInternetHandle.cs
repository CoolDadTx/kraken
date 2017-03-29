/*
 * Copyright © 2006 Michael L Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using Microsoft.Win32.SafeHandles;

using P3Net.Kraken;
#endregion

namespace P3Net.Kraken.Net
{
    /// <summary>Safe handle for HINTERNET resources.</summary>
    internal sealed class SafeInternetHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="SafeInternetHandle"/> class.</summary>
        private SafeInternetHandle ( ) : base(true)
        { /* Do nothing */ }

        /// <summary>Initializes an instance of the <see cref="SafeInternetHandle"/> class.</summary>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public SafeInternetHandle ( IntPtr preexistingHandle, bool ownsHandle ) : base(ownsHandle)			
        {
            base.SetHandle(preexistingHandle);
        }
        #endregion //Construction

        #region Protected Members
        
        /// <summary>Releases the underlying handle.</summary>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        protected override bool ReleaseHandle ( )
        {
            try
            {
                return WinINetNative.InternetCloseHandle(base.handle);
            } finally
            {
                base.SetHandleAsInvalid();
            };
        }
        #endregion
    }
}
