/*
 * Copyright © 2005 Michael Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;

using P3Net.Kraken.Collections;
using P3Net.Kraken.Diagnostics;
#endregion

namespace P3Net.Kraken.Data.Common
{
    /// <summary>Represents a data transaction.</summary>
    public class DataTransaction : IDisposable
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="DataTransaction"/> class.</summary>
        /// <param name="innerTransaction">The inner transaction object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="innerTransaction"/> is <see langword="null"/>.</exception>
        public DataTransaction ( DbTransaction innerTransaction ) : this(innerTransaction, true)
        { /* Do nothing */ }

        /// <summary>Initializes an instance of the <see cref="DataTransaction"/> class.</summary>
        /// <param name="innerTransaction">The inner transaction object.</param>
        /// <param name="closeConnection"><see langword="true"/> to close the connection when the transaction is completed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="innerTransaction"/> is <see langword="null"/>.</exception>
        public DataTransaction ( DbTransaction innerTransaction, bool closeConnection )
        {
            Verify.Argument("innerTransaction", innerTransaction).IsNotNull();

            m_inner = innerTransaction;
            m_autoClose = closeConnection;
        }
        #endregion

        #region Public Members

        #region Events

        /// <summary>Occurs after the transaction is committed successfully.</summary>
        public event EventHandler Committed;

        /// <summary>Occurs after the transaction is rolled back.</summary>
        public event EventHandler RolledBack;
        #endregion

        #region Attributes

        /// <summary>Gets the isolation level of the transaction.</summary>
        public IsolationLevel IsolationLevel
        {
            get { return (m_inner != null) ? m_inner.IsolationLevel : IsolationLevel.Unspecified; }
        }
        #endregion

        #region Methods

        /// <summary>Commits the transaction.</summary>
        /// <exception cref="InvalidOperationException">The transaction has already been closed.</exception>
        /// <example>
        /// <code lang="C#">
        ///    public void UpdateData ( )
        ///    {
        ///       using(DataTransaction trans = ConnectionManager.BeginTransaction())
        ///       {
        ///          ...
        ///          trans.Commit();
        ///       };
        ///    }
        /// </code>
        /// </example>
        public void Commit ( )
        {
            CommitBase();
        }

        /// <summary>Disposes of the object.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Rolls back the transaction.</summary>
        /// <exception cref="InvalidOperationException">The transaction has already been closed.</exception>
        /// <example>
        /// <code lang="C#">
        ///    public void UpdateData ( )
        ///    {
        ///       DataTransaction trans = null;
        ///       try
        ///       {
        ///          trans = ConnectionManager.BeginTransaction();		
        ///          ...		
        ///          trans.Commit();
        ///       } catch (Exception e)
        ///       {
        ///          if (trans != null)
        ///             trans.Rollback();
        ///       };
        ///    }
        /// </code>
        /// </example>
        public void Rollback ( )
        {
            RollbackBase();
        }
        #endregion

        #endregion 

        #region Private Members

        #region Attributes

        [ExcludeFromCodeCoverage]
        internal DbTransaction InnerTransaction
        {
            get { return m_inner; }
        }
        #endregion

        #region Methods

        private void AutoClose ( DbTransaction trans )
        {
            if (m_autoClose && (trans.Connection != null) && (trans.Connection.State == ConnectionState.Open))
                trans.Connection.Close();
        }

        private void CommitBase ()
        {
            DbTransaction trans = Interlocked.Exchange<DbTransaction>(ref m_inner, null);
            if (trans == null)
                throw new InvalidOperationException("The transaction is already closed.");

            trans.Commit();

            try
            {
                //Raise event as needed
                EventHandler hldr = Committed;
                if (hldr != null)
                    hldr(this, EventArgs.Empty);
            } finally
            {
                AutoClose(trans);
            };
        }

        private void Dispose ( bool disposing )
        {
            if (disposing)
            {
                DbTransaction trans = Interlocked.Exchange<DbTransaction>(ref m_inner, null);
                if (trans != null)
                    InternalRollback(trans);
            };
        }

        private void InternalRollback ( DbTransaction trans )
        {
            try
            {
                trans.Rollback();

                //Raise event as needed
                EventHandler hldr = RolledBack;
                if (hldr != null)
                    hldr(this, EventArgs.Empty);
            } finally
            {
                AutoClose(trans);
            };
        }

        private void RollbackBase ()
        {
            DbTransaction trans = Interlocked.Exchange<DbTransaction>(ref m_inner, null);
            if (trans == null)
                throw new InvalidOperationException("The transaction is already closed.");

            InternalRollback(trans);
        }
        #endregion

        #region Data

        private DbTransaction m_inner;
        private bool m_autoClose;
        #endregion

        #endregion
    }
}
