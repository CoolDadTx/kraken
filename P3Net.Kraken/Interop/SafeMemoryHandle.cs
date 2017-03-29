/*
 * Copyright © 2007 Michael L Taylor
 * All Rights Reserved 
 */
#region Imports

using System;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

using P3Net.Kraken;
#endregion

namespace P3Net.Kraken.Interop
{
    /// <summary>Provides a wrapper around an unmanaged memory pointer.</summary>
    /// <remarks>
    /// This class provides a managed wrapper around an unmanaged memory pointer.  The 
    /// memory is freed when the handle is destroyed.  It should be used in lieu of
    /// <see cref="IntPtr"/> when allocating memory.
    /// </remarks>
    /// <seealso cref="SafeHGlobalHandle"/> 
    /// <seealso cref="SafeComMemoryHandle"/>
    /// <seealso cref="SafeBStrHandle"/>
    public abstract class SafeMemoryHandle : SafeHandle
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="SafeMemoryHandle"/> class.</summary>
        protected SafeMemoryHandle ( ) : base(IntPtr.Zero, true)
        { /* Do nothing */ }

        /// <summary>Initializes an instance of the <see cref="SafeMemoryHandle"/> class.</summary>
        /// <param name="value">The pointer to manage.</param>
        protected SafeMemoryHandle ( IntPtr value ) : base(IntPtr.Zero, true)
        {
            handle = value;
        }
        #endregion 

        #region Public Members

        #region Attributes

        /// <summary>Determines if the handle is invalid.</summary>		
        public override bool IsInvalid
        {
            [SecurityCritical]            
            get { return handle == IntPtr.Zero; }
        }

        /// <summary>Gets the memory pointer.</summary>
        public IntPtr Pointer
        {
            get { return handle; }
        }
        #endregion

        #region Methods

        /// <summary>Attaches a pointer to the handle.</summary>
        /// <param name="value">The pointer to attach to.</param>
        /// <remarks>
        /// Any existing memory held by the handle is freed.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///    public SafeMemoryHandle GetUserGroup ( string user )
        ///    {
        ///       //Get the buffer size
        ///       IntPtr ptr = IntPtr.Zero;
        ///       int size = 0;
        ///       GetUserGroupW(user, ptr, ref size);
        ///       
        ///       //Allocate
        ///       ptr = Marshal.AllocHGlobal(size);
        ///       GetUserGroupW(user, ptr, ref size);
        ///       
        ///       //Cache it
        ///       SafeMemoryHandle mptr = new SafeMemoryHandle();
        ///       mptr.Attach(ptr);
        ///       return mptr;
        ///    }
        /// </code>
        /// <code lang="VB">
        ///    Public Function GetUserGroup ( user As String ) As SafeMemoryHandle
        ///    
        ///       'Get the buffer size
        ///       Dim ptr As IntPtr = IntPtr.Zero
        ///       Dim size As Integer = 0
        ///       GetUserGroupW(user, ptr, size)
        ///       
        ///       'Allocate
        ///       ptr = Marshal.AllocHGlobal(size)
        ///       GetUserGroupW(user, ptr, size)
        ///       
        ///       'Cache it
        ///       Dim mptr As New ManagedPointer()
        ///       mptr.Attach(ptr)
        ///       
        ///       Return mptr
        ///    End Function
        /// </code>
        /// </example>
        public virtual void Attach ( IntPtr value )
        {
            IntPtr ptrOld = Interlocked.Exchange(ref handle, value);
            if ((ptrOld != IntPtr.Zero) && (ptrOld != value))
                ReleasePointer(ptrOld);
        }

        /// <summary>Detaches the pointer from the handle without freeing it.</summary>
        /// <returns>The detached pointer.</returns>
        /// <remarks>
        /// The pointer must be freed explicitly.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///    public IntPtr GetUserGroup ( string user )
        ///    {
        ///       //Get the buffer size
        ///       int size = 0;
        ///       GetUserGroupW(user, IntPtr.Zero, ref size);
        ///       
        ///       //Allocate
        ///       using (SafeMemoryHandle ptr = SafeHGlobalHandle.Alloc(size))
        ///       {
        ///          GetUserGroupW(user, ptr.handle, ref size);
        ///       
        ///          return ptr.Detach();
        ///       };
        ///    }
        /// </code>
        /// <code lang="VB">
        ///    Public Function GetUserGroup ( user As String ) As IntPtr
        ///    
        ///       'Get the buffer size
        ///       Dim size As Integer = 0
        ///       GetUserGroupW(user, IntPtr.Zero, size)
        ///       
        ///       'Allocate
        ///       Using ptr As SafeMemoryHandle = SafeHGlobalHandle.Alloc(size)
        ///       
        ///          GetUserGroupW(user, ptr.handle, size)
        ///       
        ///          Return ptr.Detach()
        ///       End Using
        ///    End Function
        /// </code>
        /// </example>
        public virtual IntPtr Detach ()
        {
            return Interlocked.Exchange(ref handle, IntPtr.Zero);
        }
        #endregion
            
        #endregion 

        #region Protected Members

        /// <summary>Releases the handle.</summary>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        [SecurityCritical]
        protected override bool ReleaseHandle ()
        {
            IntPtr ptr = Interlocked.Exchange(ref handle, IntPtr.Zero);
            if (ptr != IntPtr.Zero)
                ReleasePointer(ptr);

            return true;
        }

        /// <summary>Releases the specified pointer.</summary>
        /// <param name="value">The pointer to release.</param>
        protected abstract void ReleasePointer ( IntPtr value );
        #endregion
    }
}
