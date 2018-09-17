/*
 * Copyright © 2007 Michael L Taylor
 * All Rights Reserved
 * 
 * $Header: $
 */
#region Imports

using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using P3Net.Kraken;
#endregion

namespace P3Net.Kraken.ComponentModel
{
    /// <summary>Provides an <see cref="IDisposable"/> base implementation.</summary>
    /// <remarks>
    /// This class provides a base implementation of the <see cref="IDisposable"/> interface.
    /// Derived classes should override <see cref="M:DisposableBase.Dispose(bool)"/>	 to clean up any resources.
    /// <para/>
    /// This class defines a finalizer.  If an object wrapped by this class is finalized rather than being disposed and debugging is enabled then a trace statement is 
    /// generated with the line where the object was created.
    /// <para/>
    /// To implement <see cref="IDisposable"/> without a finalizer use <see cref="DisposableObject"/> instead.
    /// </remarks>
    /// <seealso cref="DisposableObject"/>
    public abstract class DisposableObjectWithFinalizer : DisposableObject
    {		
        #region Construction

        /// <summary>Initializes an instance of the <see cref="DisposableObjectWithFinalizer"/> class.</summary>
        protected DisposableObjectWithFinalizer ()
        {		
            CaptureStack();
        }

        /// <summary>Finalizes the object.</summary>
        [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
        ~DisposableObjectWithFinalizer ()
        {
            if (!IsDisposed)
            {
                Dispose(false);

                //Dump the stack information
                ReportStack();
            };
        }
        #endregion
                
        #region Protected Members

        /// <summary>Disposes of the object.</summary>
        /// <param name="disposing"><see langword="true"/> if disposing of the object.</param>
        protected override void Dispose ( bool disposing )
        {
            base.Dispose(disposing);

            string output;
            s_finalizerStack.TryRemove(m_finalizerId, out output);
        }
        #endregion 

        #region Private Members

        #region Methods

        [Conditional("DEBUG")]
        [ExcludeFromCodeCoverage]
        private void CaptureStack ()
        {
            m_finalizerId = base.GetHashCode();

            var stack = new StackTrace(2, true).ToString();

            s_finalizerStack.TryAdd(m_finalizerId, stack);
        }

        [Conditional("DEBUG")]
        [ExcludeFromCodeCoverage]
        private void ReportStack ()
        {
            string stack;
            if (s_finalizerStack.TryGetValue(m_finalizerId, out stack))
            { 		
                var msg = "Object was finalized.  Check for missing using or Dispose statements: " + stack;
                Trace.WriteLine(msg);
            };
        }
        #endregion

        #region Data

        private int m_finalizerId;
        private static ConcurrentDictionary<int, string> s_finalizerStack = new ConcurrentDictionary<int, string>();
        #endregion

        #endregion
    }
}