/*
 * Copyright © 2007 Michael L Taylor
 * All Rights Reserved
 * 
 * $Header: $
 */
#region Imports

using System;
using System.Diagnostics.CodeAnalysis;

using P3Net.Kraken;
#endregion

namespace P3Net.Kraken.ComponentModel
{
    /// <summary>Provides an <see cref="IDisposable"/> base implementation.</summary>
    /// <remarks>
    /// This class provides a base implementation of the <see cref="IDisposable"/> interface.
    /// Derived classes should override <see cref="M:Dispose(bool)"/>  to clean up any resources.
    /// <para/>
    /// This implementation does not define a finalizer.  If you need finalization support then
    /// use <see cref="DisposableObjectWithFinalizer"/> instead.
    /// </remarks>
    /// <seealso cref="DisposableObjectWithFinalizer"/>
    public abstract class DisposableObject : IDisposable
    {
        #region Public Members

        /// <summary>Determines if the object is disposed.</summary>
        public bool IsDisposed { get; private set; }
        
        /// <summary>Disposes of the object.</summary>
        [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
        public void Dispose ()
        {
            if (IsDisposed)
                return;
                        
            try
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            } finally
            {
                //We don't really want anybody calling this more than once
                //so try to prevent it here
                IsDisposed = true;
            };
        }		
        #endregion Public Members
        
        #region Protected Members
                
        /// <summary>Called when the object is disposed.</summary>
        /// <param name="disposing"><see langword="true"/> if the object is being disposed explicitly or <see langword="false"/> otherwise.</param>
        /// <remarks>
        /// The default implementation does nothing.
        /// </remarks>
        protected virtual void Dispose ( bool disposing )
        {
        }
        #endregion
    }
}