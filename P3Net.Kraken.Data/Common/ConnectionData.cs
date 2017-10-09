/*
 * Copyright © 2007 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using P3Net.Kraken.ComponentModel;

namespace P3Net.Kraken.Data.Common
{
    /// <summary>Provides connection information for classes deriving from <see cref="ConnectionManager"/>.</summary>
    [ExcludeFromCodeCoverage]
    public sealed class ConnectionData : DisposableObject
    {
        #region Construction

        internal ConnectionData ( DbConnection connection )
        {
            _connection = connection;
        }

        internal ConnectionData ( DbTransaction transaction )
        {
            _transaction = transaction;
        }
        #endregion

        /// <summary>Gets the underlying connection.</summary>
        public DbConnection Connection => _connection ?? _transaction.Connection;        

        /// <summary>Gets the underlying transaction.</summary>
        public DbTransaction Transaction => _transaction;
        
        /// <summary>Detaches the connection from the object so it won't be closed.</summary>
        /// <returns>The connection.</returns>
        public DbConnection Detach () => Interlocked.Exchange(ref _connection, null);
        
        /// <summary>Opens the connection if it is not already opened.</summary>
        public void Open ()
        {
            if (_connection?.State == ConnectionState.Closed)
                _connection.Open();
        }

        /// <summary>Opens the connection if it is not already opened.</summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        public Task OpenAsync ( CancellationToken cancellationToken )
        {
            if (_connection?.State == ConnectionState.Closed)
                return _connection.OpenAsync(cancellationToken);

            return Task.CompletedTask;
        }

        /// <summary>Disposes of the object.</summary>
        /// <param name="disposing"><see langword="true"/> if disposing.</param>

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        protected override void Dispose ( bool disposing )
        {
            if (disposing)
            {
                try
                {
                    var conn = Detach();
                    if ((conn?.State != ConnectionState.Closed))
                        conn.Close();
                } catch
                { /* Ignore */ };
            };
        }

        #region Private Members

        private readonly DbTransaction _transaction;
        private DbConnection _connection;

        #endregion
    }
}
