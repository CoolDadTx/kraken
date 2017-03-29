/*
 * Copyright © 2007 Michael L Taylor
 * All Rights Reserved
 * 
 * $Header: $
 */
#region Imports

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

using P3Net.Kraken.Diagnostics;
#endregion

namespace P3Net.Kraken.Interop
{
    /// <summary>Represents a pointer to memory allocated using <b>CoTaskMemAlloc</b>.</summary>
    /// <remarks>
    /// This class provides a managed wrapper around an unmanaged memory pointer.  The memory is freed when the handle is destroyed.  It should be used 
    /// in lieu of <see cref="IntPtr"/> when allocating memory.
    /// </remarks>
    public sealed class SafeComMemoryHandle : SafeMemoryHandle
    {
        #region Construction

        private SafeComMemoryHandle ( IntPtr value, bool zeroFree, bool isUnicode ) : base(value)
        {
            m_zeroFree = zeroFree;
            m_isUnicode = isUnicode;
        }

        /// <summary>Initializes an instance of the <see cref="SafeComMemoryHandle"/> class.</summary>
        public SafeComMemoryHandle ( ) : this(IntPtr.Zero, false, false)
        { /* Do nothing */ }

        /// <summary>Initializes an instance of the <see cref="SafeComMemoryHandle"/> class.</summary>
        /// <param name="value">The pointer to wrap.</param>
        public SafeComMemoryHandle ( IntPtr value ) : this(value, false, false)
        { /* Do nothing */ }
          
        #endregion

        #region Public Members

        /// <summary>Creates a managed pointer with memory allocated using <b>CoTaskMemAlloc</b>.</summary>
        /// <param name="size">The size of the memory to allocate.</param>
        /// <returns>The new pointer.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="size"/> is less than or equal to zero.</exception>
        /// <example>
        /// <code lang="C#">
        ///    public string GetUserName ( )
        ///    {
        ///       int size = 0;
        ///       GetUserNameNative(IntPtr.Zero, ref size);
        ///       
        ///       using(SafeComMemoryHandle hdl = SafeComMemoryHandle.Allocate(size))
        ///       {
        ///          GetUserNameNative(hdl.Pointer, ref size);
        ///       };
        ///       
        ///       ...
        ///    }
        /// </code>
        /// <code lang="VB">
        ///    Public Function GetUserName ( ) As String
        ///       Dim size As Integer = 0
        ///       
        ///       GetUserNameNative(IntPtr.Zero, size)
        ///       
        ///       Using hdl As SafeComMemoryHandle = SafeComMemoryHandle.Allocate(size)
        ///           GetUserNameNative(hdl.Pointer, ref.size)
        ///       End Using
        ///       
        ///       ...
        ///    End Function
        /// </code>
        /// </example>
        public static SafeComMemoryHandle Allocate ( int size )
        {
            Verify.Argument("size", size).IsGreaterThanZero();

            return new SafeComMemoryHandle(Marshal.AllocCoTaskMem(size));
        }

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
        
        /// <summary>Creates a managed pointer for an ANSI string with memory allocated using <b>CoTaskMemAlloc</b>.</summary>
        /// <param name="value">The string to copy.</param>
        /// <returns>The new pointer.</returns>
        /// <remarks>
        /// The memory is zeroed out when the handle is closed.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///    public void SendCredentials ( SecureString userName, SecureString password )
        ///    {
        ///       using(SafeComMemoryHandle hdlUser = SafeComMemoryHandle.SecureStringToAnsi(userName))
        ///       {
        ///          using(SafeComMemoryHandle hdlPwd = SafeComMemoryHandle.SecureStringToAnsi(password))
        ///          {
        ///             SetUserCredentialsNative(hdlUser.Pointer, hdlPwd.Pointer);
        ///          };
        ///       };
        ///    }
        /// </code>
        /// <code lang="VB">
        ///    Public Sub SendCredentials ( userName As SecureString, password As SecureString )
        ///     
        ///       Using hdlUser As SafeComMemoryHandle = SafeComMemoryHandle.SecureStringToAnsi(userName)
        ///          Using hdlPwd As SafeComMemoryHandle = SafeComMemoryHandle.SecureStringToAnsi(password)
        ///          
        ///             SetUserCredentialsNative(hdlUser.Pointer, hdlPwd.Pointer)
        ///          End Using
        ///       End Using
        ///    End Sub
        /// </code>
        /// </example>
        public static SafeComMemoryHandle SecureStringToAnsi ( System.Security.SecureString value )
        {
            if (value == null)
                return new SafeComMemoryHandle();

            return new SafeComMemoryHandle(Marshal.SecureStringToCoTaskMemAnsi(value), true, false);
        }

        /// <summary>Creates a managed pointer for a Unicode string with memory allocated using <b>CoTaskMemAlloc</b>.</summary>
        /// <param name="value">The string to copy.</param>
        /// <returns>The new pointer.</returns>
        /// <remarks>
        /// The memory is zeroed out when the handle is closed.
        /// </remarks>
        /// <example>
        /// Refer to <see cref="SecureStringToAnsi"/> for an equivalent example.
        /// </example>
        public static SafeComMemoryHandle SecureStringToUnicode ( System.Security.SecureString value )
        {
            if (value == null)
                return new SafeComMemoryHandle();

            return new SafeComMemoryHandle(Marshal.SecureStringToCoTaskMemUnicode(value), true, true);
        }

        /// <summary>Creates a managed pointer for an ANSI string with memory allocated using <b>CoTaskMemAlloc</b>.</summary>
        /// <param name="value">The string to copy.</param>
        /// <returns>The new pointer.</returns>
        /// <example>
        /// Refer to <see cref="SecureStringToAnsi"/> for an equivalent example.
        /// </example>
        public static SafeComMemoryHandle StringToAnsi ( string value )
        {
            if (value == null)
                return new SafeComMemoryHandle();

            return new SafeComMemoryHandle(Marshal.StringToCoTaskMemAnsi(value));
        }

        /// <summary>Creates a managed pointer for an auto string with memory allocated using <b>CoTaskMemAlloc</b>.</summary>
        /// <param name="value">The string to copy.</param>
        /// <returns>The new pointer.</returns>
        /// <example>
        /// Refer to <see cref="SecureStringToAnsi"/> for an equivalent example.
        /// </example>
        public static SafeComMemoryHandle StringToAuto ( string value )
        {
            if (value == null)
                return new SafeComMemoryHandle();

            return new SafeComMemoryHandle(Marshal.StringToCoTaskMemAuto(value));
        }

        /// <summary>Creates a managed pointer for a Unicode string with memory allocated using <b>CoTaskMemAlloc</b>.</summary>
        /// <param name="value">The string to copy.</param>
        /// <returns>The new pointer.</returns>
        /// <example>
        /// Refer to <see cref="SecureStringToAnsi"/> for an equivalent example.
        /// </example>
        public static SafeComMemoryHandle StringToUnicode ( string value )
        {
            if (value == null)
                return new SafeComMemoryHandle();

            return new SafeComMemoryHandle(Marshal.StringToCoTaskMemUni(value));
        }
        #endregion

        #region Protected Members

        /// <summary>Releases the given pointer.</summary>
        /// <param name="value">The pointer to release.</param>
        protected override void ReleasePointer ( IntPtr value )
        {
            if (value != IntPtr.Zero)
            {
                if (m_zeroFree)
                {
                    if (m_isUnicode)
                        Marshal.ZeroFreeCoTaskMemUnicode(value);
                    else
                        Marshal.ZeroFreeCoTaskMemAnsi(value);

                    m_zeroFree = false;
                } else
                    Marshal.FreeCoTaskMem(value);
            };
        }
        #endregion

        #region Private Members

        private bool m_zeroFree;
        private bool m_isUnicode;
        #endregion
    }
}
