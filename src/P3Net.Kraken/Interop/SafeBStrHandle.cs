/*
 * Copyright © 2007 Michael L Taylor
 * All Rights Reserved
 * 
 * $Header: $
 */
#region Imports

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using P3Net.Kraken;
#endregion

namespace P3Net.Kraken.Interop
{
    /// <summary>Represents a pointer to memory allocated using <b>SysAllocString</b>.</summary>
    /// <remarks>
    /// This class provides a managed wrapper around an unmanaged memory pointer.  The memory is freed when the handle is destroyed.  It should be used 
    /// in lieu of <see cref="IntPtr"/> when allocating memory.
    /// </remarks>
    /// <preliminary/>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Str")]
    public sealed class SafeBStrHandle : SafeMemoryHandle
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="SafeBStrHandle"/> class.</summary>
        public SafeBStrHandle ( ) 
        { /* Do nothing */ }

        /// <summary>Initializes an instance of the <see cref="SafeBStrHandle"/> class.</summary>
        /// <param name="value">The pointer to wrap.</param>
        public SafeBStrHandle ( IntPtr value ) : base(value)
        { /* Do nothing */ }

        /// <summary>Initializes an instance of the <see cref="SafeBStrHandle"/> class.</summary>
        /// <param name="value">The pointer to wrap.</param>
        /// <param name="zeroFree"><see langword="true"/> to zero free the memory when needed.</param>
        public SafeBStrHandle ( IntPtr value, bool zeroFree ) : base(value)
        {
            m_zeroFree = zeroFree;
        }
        #endregion //Construction

        #region Public Members

        /// <summary>Attaches to a new pointer.</summary>
        /// <param name="value">The pointer to attach to.</param>
        /// <example>
        /// Refer to <see cref="SafeMemoryHandle.Attach"/> for an example.
        /// </example>
        public override void Attach ( IntPtr value )
        {
            base.Attach(value);

            m_zeroFree = false;
        }

        /// <summary>Attaches to a new pointer.</summary>
        /// <param name="value">The pointer to attach to.</param>
        /// <param name="zeroFree"><see langword="true"/> to zero free the memory when released.</param>
        /// <remarks>
        /// This overloaded method allows for attaching to a secure string that was allocated in unmanaged code.  If <paramref name="zeroFree"/> 
        /// is <see langword="true"/> then the unmanaged memory will be zeroed out when freed.
        /// </remarks>
        /// <example>
        /// Refer to <see cref="SafeMemoryHandle.Attach"/> for an example.
        /// </example>
        public void Attach ( IntPtr value, bool zeroFree )
        {
            base.Attach(value);

            m_zeroFree = zeroFree;
        }

        /// <summary>Creates a managed pointer with memory allocated for a BSTR.</summary>
        /// <param name="value">The string to copy.</param>
        /// <returns>The new pointer.</returns>
        /// <example>
        /// Refer to <see cref="SecureStringToBStr"/> for an equivalent example.
        /// </example>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Str")]
        public static SafeBStrHandle StringToBStr ( string value )
        {
            if (value == null)
                return new SafeBStrHandle();

            return new SafeBStrHandle(Marshal.StringToBSTR(value));
        }

        /// <summary>Creates a managed pointer with memory allocated for a BSTR.</summary>
        /// <param name="value">The string to copy.</param>
        /// <returns>The new pointer.</returns>
        /// <example>
        /// <code lang="C#">
        ///    public void SendCredentials ( SecureString userName, SecureString password )
        ///    {
        ///       using(SafeBStrHandle hdlUser = SafeBStrHandle.SecureStringToBStr(userName))
        ///       {
        ///          using(SafeBStrHandle hdlPwd = SafeBStrHandle.SecureStringToBStr(password))
        ///          {
        ///             SetUserCredentialsNative(hdlUser.Pointer, hdlPwd.Pointer);
        ///          };
        ///       };
        ///    }
        /// </code>
        /// </example>
        [Obsolete("Deprecated in 6.0. Do not use SecureString.")]
        public static SafeBStrHandle SecureStringToBStr ( System.Security.SecureString value)
        {
            if (value == null)
                return new SafeBStrHandle();

            return new SafeBStrHandle(Marshal.SecureStringToBSTR(value), true);
        }

        /// <summary>Gets the pointer as a string.</summary>
        /// <returns>The string.</returns>
        /// <remarks>
        /// If the handle will zero free memory then the returned string is empty..
        /// </remarks>
        public override string ToString ()
        {
            IntPtr ptr = Pointer;
            if ((ptr != IntPtr.Zero) && !m_zeroFree)
                return Marshal.PtrToStringBSTR(ptr);

            return "";
        }
        #endregion

        #region Protected Members

        /// <summary>Releases the given pointer.</summary>
        /// <param name="ptr">The pointer to release.</param>
        protected override void ReleasePointer ( IntPtr ptr )
        {
            if (ptr != IntPtr.Zero)
            {
                if (m_zeroFree)
                    Marshal.ZeroFreeBSTR(ptr);
                else
                    Marshal.FreeBSTR(ptr);
            };
        }
        #endregion

        #region Private Members

        private bool m_zeroFree;
        #endregion
    }
}
