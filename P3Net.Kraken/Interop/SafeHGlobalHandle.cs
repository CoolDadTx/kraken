/*
 * Copyright © 2007 Michael L Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

using P3Net.Kraken.Diagnostics;
#endregion

namespace P3Net.Kraken.Interop
{
    /// <summary>Represents a handle (pointer) to memory allocated using <b>LocalAlloc</b>.</summary>
    /// <remarks>
    /// This class provides a managed wrapper around an unmanaged memory pointer.  The 
    /// memory is freed when the handle is destroyed.  It should be used in lieu of
    /// <see cref="IntPtr"/> when allocating memory.
    /// </remarks>
    public sealed class SafeHGlobalHandle : SafeMemoryHandle
    {
        #region Construction

        private SafeHGlobalHandle ( IntPtr value, bool zeroFree, bool isUnicode ) : base(value)
        {
            m_zeroFree = zeroFree;
            m_isUnicode = isUnicode;
        }

        /// <summary>Initializes an instance of the <see cref="SafeMemoryHandle"/> class.</summary>
        public SafeHGlobalHandle ( ) : this(IntPtr.Zero, false, false)
        { /* Do nothing */ }

        /// <summary>Initializes an instance of the <see cref="SafeMemoryHandle"/> class.</summary>
        /// <param name="value">The pointer to wrap.</param>
        public SafeHGlobalHandle ( IntPtr value ) : this(value, false, false)
        { /* Do nothing */ }

        #endregion 

        #region Public Members
    
        /// <summary>Creates a new managed pointer with memory allocated using <b>LocalAlloc</b>.</summary>
        /// <param name="size">The number of bytes to allocate.</param>
        /// <returns>The new pointer.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="size"/> is less than or equal to zero.</exception>
        /// <example>
        /// <code lang="C#">
        ///    public string GetUserName ( )
        ///    {
        ///       int size = 0;
        ///       GetUserNameNative(IntPtr.Zero, ref size);
        ///       
        ///       using(SafeHGlobalHandle hdl = SafeHGlobalHandle.Allocate(size))
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
        ///       Using hdl As SafeHGlobalHandle = SafeHGlobalHandle.Allocate(size)
        ///           GetUserNameNative(hdl.Pointer, ref.size)
        ///       End Using
        ///       
        ///       ...
        ///    End Function
        /// </code>
        /// </example>
        public static SafeHGlobalHandle Allocate ( int size )
        {
            Verify.Argument("size", size).IsGreaterThanZero();

            return new SafeHGlobalHandle(Marshal.AllocHGlobal(size));
        }

        /// <summary>Creates a managed pointer with memory allocated using <b>LocalAlloc</b>.</summary>
        /// <param name="size">The number of bytes to allocate.</param>
        /// <returns>The new pointer.</returns>
        /// <example>
        /// Refer to <see cref="Allocate(System.Int32)"/> for an example.
        /// </example>
        public static SafeHGlobalHandle Allocate ( IntPtr size )
        {
            return new SafeHGlobalHandle(Marshal.AllocHGlobal(size));
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
                
        /// <summary>Creates a handle for a secure ANSI string allocated in global memory</summary>
        /// <param name="value">The string to copy.</param>
        /// <returns>The new pointer.</returns>
        /// <remarks>
        /// The memory is zeroed out when the handle is closed.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///    public void SendCredentials ( SecureString userName, SecureString password )
        ///    {
        ///       using(SafeHGlobalHandle hdlUser = SafeHGlobalHandle.SecureStringToAnsi(userName))
        ///       {
        ///          using(SafeHGlobalHandle hdlPwd = SafeHGlobalHandle.SecureStringToAnsi(password))
        ///          {
        ///             SetUserCredentialsNative(hdlUser.Pointer, hdlPwd.Pointer);
        ///          };
        ///       };
        ///    }
        /// </code>
        /// <code lang="VB">
        ///    Public Sub SendCredentials ( userName As SecureString, password As SecureString )
        ///     
        ///       Using hdlUser As SafeHGlobalHandle = SafeHGlobalHandle.SecureStringToAnsi(userName)
        ///          Using hdlPwd As SafeHGlobalHandle = SafeHGlobalHandle.SecureStringToAnsi(password)
        ///          
        ///             SetUserCredentialsNative(hdlUser.Pointer, hdlPwd.Pointer)
        ///          End Using
        ///       End Using
        ///    End Sub
        /// </code>
        /// </example>
        public static SafeHGlobalHandle SecureStringToAnsi ( System.Security.SecureString value )
        {
            if (value == null)
                return new SafeHGlobalHandle();

            return new SafeHGlobalHandle(Marshal.SecureStringToGlobalAllocAnsi(value), true, false);
        }

        /// <summary>Creates a handle  for a secure Unicode string allocated in global memory.</summary>
        /// <param name="value">The string to copy.</param>
        /// <remarks>
        /// The memory is zeroed out when the handle is closed.
        /// </remarks>
        /// <returns>The new pointer.</returns>
        /// <example>
        /// Refer to <see cref="SecureStringToAnsi"/> for an equivalent example.
        /// </example>
        public static SafeHGlobalHandle SecureStringToUnicode ( System.Security.SecureString value )
        {
            if (value == null)
                return new SafeHGlobalHandle();

            return new SafeHGlobalHandle(Marshal.SecureStringToGlobalAllocUnicode(value), true, true);
        }

        /// <summary>Creates a handler for an ANSI string allocated in global memory.</summary>
        /// <param name="value">The string to copy.</param>
        /// <returns>The new pointer.</returns>
        /// <example>
        /// Refer to <see cref="SecureStringToAnsi"/> for an equivalent example.
        /// </example>
        public static SafeHGlobalHandle StringToAnsi ( string value )
        {
            if (value == null)
                return new SafeHGlobalHandle();

            return new SafeHGlobalHandle(Marshal.StringToHGlobalAnsi(value));
        }

        /// <summary>Creates a handler for a string allocated in global memory.</summary>
        /// <param name="value">The string to copy.</param>
        /// <returns>The new pointer.</returns>
        /// <example>
        /// Refer to <see cref="SecureStringToAnsi"/> for an equivalent example.
        /// </example>
        public static SafeHGlobalHandle StringToAuto ( string value )
        {
            if (value == null)
                return new SafeHGlobalHandle();

            return new SafeHGlobalHandle(Marshal.StringToHGlobalAuto(value));
        }

        /// <summary>Creates a handler for a Unicode string allocated in global memory.</summary>
        /// <param name="value">The string to copy.</param>
        /// <returns>The new pointer.</returns>
        /// <example>
        /// Refer to <see cref="SecureStringToAnsi"/> for an equivalent example.
        /// </example>
        public static SafeHGlobalHandle StringToUnicode ( string value )
        {
            if (value == null)
                return new SafeHGlobalHandle();

            return new SafeHGlobalHandle(Marshal.StringToHGlobalUni(value));
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
                        Marshal.ZeroFreeGlobalAllocUnicode(value);
                    else
                        Marshal.ZeroFreeGlobalAllocAnsi(value);

                    m_zeroFree = false;
                } else
                    Marshal.FreeHGlobal(value);				
            };
        }
        #endregion

        #region Private Members

        private bool m_zeroFree;
        private bool m_isUnicode;
        #endregion
    }
}
