/*
 * Copyright © 2007 Michael Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

#endregion

namespace P3Net.Kraken.Data.Common
{
    /// <summary>Provides connection information for classes deriving from <see cref="ConnectionManager"/>.</summary>
    [ExcludeFromCodeCoverage]
    public sealed class ConnectionData : IDisposable
    {
        #region Construction

        internal ConnectionData ( DbConnection connection )
        {
            m_connection = connection;
        }

        internal ConnectionData ( DbTransaction transaction )
        {
            m_transaction = transaction;
        }
        #endregion

        #region Public Members

        #region Attributes

        /// <summary>Gets the underlying connection.</summary>
        public DbConnection Connection
        {
            get { return m_connection ?? m_transaction.Connection; }
        }

        /// <summary>Gets the underlying transaction.</summary>
        public DbTransaction Transaction
        {
            get { return m_transaction; }
        }
        #endregion

        #region Methods

        /// <summary>Detaches the connection from the object so it won't be closed.</summary>
        /// <returns>The connection.</returns>
        public DbConnection Detach ()
        {
            var conn = m_connection;
            m_connection = null;

            return conn;
        }

        /// <summary>Disposes of the instance.</summary>
        public void Dispose ()
        {
            Dispose(true);
        }

        /// <summary>Opens the connection if it is not already opened.</summary>
        public void Open ()
        {
            if ((m_connection != null) && (m_connection.State == ConnectionState.Closed))
                m_connection.Open();
        }
        #endregion

        #endregion

        #region Private Members

        #region Methods

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void Dispose ( bool disposing )
        {
            if (disposing)
            {
                try
                {
                    if ((m_connection != null) && (m_connection.State != ConnectionState.Closed))
                        m_connection.Close();
                } catch
                { /* Ignore */
                } finally
                {
                    m_connection = null;
                };
            };
        }
        #endregion

        #region Data

        private DbTransaction m_transaction;
        private DbConnection m_connection;

        #endregion

        #endregion
    }
}
