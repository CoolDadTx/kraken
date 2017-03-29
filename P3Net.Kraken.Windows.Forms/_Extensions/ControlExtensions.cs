/*
 * Copyright © 2014 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Windows.Forms;

using P3Net.Kraken.ComponentModel;

namespace P3Net.Kraken.WinForms
{
    /// <summary>Provides extension methods for <see cref="Control"/>.</summary>
    public static class ControlExtensions
    {
        /// <summary>Saves the current cursor for the control and restores it when the object is destroyed.</summary>
        /// <param name="source">The control to persist the cursor for.</param>
        /// <returns>An object that restores the cursor when it is disposed.</returns>
        /// <example>
        /// <code lang="C#">
        /// using (var obj = form.SaveCursor())
        /// {
        ///    form.Cursor = Cursors.WaitCursor;
        ///    ...
        /// }
        /// </code>
        /// </example>
        public static IDisposable SaveCursor ( this Control source )
        {
            return new SaveCursorState(source);
        }

        /// <summary>Saves the current cursor for the control and restores it when the object is destroyed.</summary>
        /// <param name="source">The control to persist the cursor for.</param>
        /// <param name="cursor">The new cursor.</param>
        /// <returns>An object that restores the cursor when it is disposed.</returns>
        /// <example>
        /// <code lang="C#">
        /// using (var obj = form.SaveCursor(Cursors.WaitCursor))
        /// {        
        ///    ...
        /// }
        /// </code>
        /// </example>
        public static IDisposable SaveCursor ( this Control source, Cursor cursor )
        {
            var obj = new SaveCursorState(source);
            source.Cursor = cursor;

            return obj;
        }

        #region Private Members

        private sealed class SaveCursorState : DisposableObject
        {
            public SaveCursorState ( Control parent )
            {
                m_parent = parent;
                m_previous = m_parent.Cursor;
            }

            protected override void Dispose ( bool disposing )
            {
                if (disposing && !m_parent.IsDisposed)
                {
                    if (!m_parent.InvokeRequired)
                        Restore();
                    else
                        m_parent.Invoke((MethodInvoker)Restore);
                };
            }

            private void Restore ()
            {
                m_parent.Cursor = m_previous;
            }

            private readonly Control m_parent;
            private readonly Cursor m_previous;
        }
        #endregion
    }
}
