/*
 * Copyright © 2005 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.Data.Common
{
    /// <summary>Provides a base class for data access layers available to applications.</summary>
    /// <remarks>
    /// This is the base class for all connection managers.  This class provides basic data access functionality.  Derived
    /// classes are created to implement database-specific functionality.  Applications should use this class indirectly through
    /// application-specific data access components.  An application can manage multiple, different databases at once using this model.
    /// <para/>
    /// To support a new database derive from this class.  The following methods must be
    /// implemented: <see cref="CreateCommandBase"/>, <see cref="CreateConnectionBase"/> and <see cref="CreateDataAdapterBase"/>.  
    /// <see cref="CreateTransactionBase"/> and <see cref="QueryParametersBase"/> can be implemented if needed.  When using
    /// <see cref="DbProviderFactory"/>-based classes use <see cref="DbProviderFactoryConnectionManager"/> instead as it will
    /// implement some of the methods automatically.
    /// <para/>
    /// <see cref="ConnectionManager"/> is guaranteed to close all connections in all but a couple of cases.
    /// <list type="numbered">
    ///		<item>Calling <see cref="M:BeginTransaction"/> will leave an open connection until the transaction object is disposed.</item>
    ///		<item><see cref="M:ExecuteReader"/> will leave an open connection until the reader is disposed.</item>
    /// </list>
    /// </remarks>
    public abstract class ConnectionManager
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="ConnectionManager"/> class.</summary>
        protected ConnectionManager ()
        { /* Do nothing */ }

        /// <summary>Initializes an instance of the <see cref="ConnectionManager"/> class.</summary>
        /// <param name="connectionString">The connection string to use.</param>
        protected ConnectionManager ( string connectionString )
        {
            if (connectionString != null)
                m_connString = connectionString.Trim();
        }
        #endregion

        #region Attributes

        /// <summary>Determines if the manager supports querying for parameter information.</summary>
        /// <value>The default value is <see langword="false"/>.</value>
        public bool SupportsQueryParameters { get; protected set; }

        /// <summary>Determines if the manager supports setting the user context.</summary>
        /// <value>The default is <see langword="false"/>.</value>
        public bool SupportsUserContext { get; protected set; }
        #endregion        

        #region BeginTransaction

        /// <summary>Begins a transaction to wrap a group of data access calls.</summary>
        /// <returns>The transaction to use for the calls.</returns>
        /// <remarks>
        /// The transaction is an isolated transaction.  Each call to this method will create a new, opened
        /// connection to the database.  Be sure to commit or roll back the transaction before it goes out of scope.  A
        /// <b>using</b> block is recommended.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///    public class Person
        ///    {
        ///       ...
        ///       private void Save ( ConnectionManager connMgr )
        ///       {
        ///		     using(var trans = connMgr.BeginTransaction())
        ///          {
        ///             ...
        ///             trans.Commit();
        ///          };
        ///       }
        ///    }
        /// </code>
        /// </example>
        public DataTransaction BeginTransaction ()
        {
            return BeginTransactionCore(IsolationLevel.ReadCommitted);
        }

        /// <summary>Begins a transaction to wrap a group of data access calls.</summary>
        /// <param name="level">The level of isolation for the transaction.</param>
        /// <returns>The transaction to use for the calls.</returns>
        /// <remarks>
        /// The transaction is an isolated transaction.  Each call to this method will create a new, opened
        /// connection to the database.  Be sure to commit or roll back the transaction before it goes out of scope.  A
        /// <b>using</b> block is recommended.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///    public class Person
        ///    {
        ///       ...
        ///       private void Save ( ConnectionManager connMgr )
        ///       {
        ///		     using(var trans = connMgr.BeginTransaction(IsolationLevel.ReadCommitted))
        ///          {
        ///             ...
        ///             trans.Commit();
        ///          };
        ///       }
        ///    }
        /// </code>
        /// </example>
        public DataTransaction BeginTransaction ( IsolationLevel level )
        {
            return BeginTransactionCore(level);
        }
        #endregion

        #region ExecuteDataSet

        /// <summary>Executes a command and returns the results as a <see cref="DataSet"/>.</summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>The results as a <see cref="DataSet"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="command"/> is <see langword="null"/>.</exception>
        /// <example>
        /// <code lang="C#">
        ///    private DataSet LoadData ( ConnectionManager connMgr )
        ///    {		
        ///       var cmd = new StoredProcedure(...);
        ///       return connMgr.ExecuteDataSet(cmd);
        ///    }
        /// </code>
        /// </example>
        public DataSet ExecuteDataSet ( DataCommand command )
        {
            return ExecuteDataSet(command, null);
        }

        /// <summary>Executes a command and returns the results as a <see cref="DataSet"/>.</summary>
        /// <param name="transaction">The transaction to execute within.  If it is <see langword="null"/> then no transaction is used.</param>
        /// <param name="command">The command to execute.</param>
        /// <returns>The results as a <see cref="DataSet"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="command"/> is <see langword="null"/>.</exception>
        /// <example>
        /// <code lang="C#">
        ///    private DataSet LoadData ( ConnectionManager connMgr )
        ///    {		
        ///       var cmd = new StoredProcedure(...);
        ///       DataSet ds = null;
        /// 
        ///       using(var trans = connMgr.BeginTransaction())
        ///	     {
        ///          ds = connMgr.ExecuteDataSet(cmd, trans);
        ///          ...
        ///          trans.Commit();
        ///      };
        /// 
        ///       return ds;
        ///    }
        /// </code>
        /// </example>
        public DataSet ExecuteDataSet ( DataCommand command, DataTransaction transaction )
        {
            Verify.Argument("command", command).IsNotNull();

            using (var conn = CreateConnectionData(transaction))
            {
                return ExecuteDataSetCore(conn, command);
            };
        }
        #endregion

        #region ExecuteNonQuery

        /// <summary>Executes a command and returns the number of affected rows.</summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>The number of rows affected.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="command"/> is <see langword="null"/>.</exception>
        /// <example>
        /// <code lang="C#">
        ///    private int LoadData ( ConnectionManager connMgr )
        ///    {		
        ///       var cmd = new StoredProcedure(...);
        ///       return connMgr.ExecuteNonQuery(cmd);
        ///    }
        /// </code>
        /// </example>
        public int ExecuteNonQuery ( DataCommand command )
        {
            return ExecuteNonQuery(command, null);
        }

        /// <summary>Executes a command and returns the number of affected rows.</summary>
        /// <param name="transaction">The transaction to execute within.  If it is <see langword="null"/> then no transaction is used.</param>
        /// <param name="command">The command to execute.</param>
        /// <returns>The number of rows affected.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="command"/> is <see langword="null"/>.</exception>
        /// <example>
        /// <code lang="C#">
        ///    private int LoadData ( ConnectionManager connMgr )
        ///    {		
        ///       var cmd = new StoredProcedure(...);
        ///       int result = 0;
        /// 
        ///       using(var trans = connMgr.BeginTransaction())
        ///		  {
        ///          result = connMgr.ExecuteNonQuery(cmd, trans);
        ///          ...
        ///          trans.Commit();
        ///       };
        /// 
        ///       return result;
        ///    }
        /// </code>
        /// </example>
        public int ExecuteNonQuery ( DataCommand command, DataTransaction transaction )
        {
            Verify.Argument("command", command).IsNotNull();

            using (var conn = CreateConnectionData(transaction))
            {
                return ExecuteNonQueryCore(conn, command);
            };
        }
        #endregion

        #region ExecuteQueryWithResult

        /// <summary>Executes a command, parses the first result and returns a strongly-typed object.</summary>
        /// <typeparam name="TResult">The type of the objects to return.</typeparam>
        /// <param name="command">The command to execute.</param>
        /// <param name="converter">The method to convert the row to an object.</param>
        /// <returns>An object containing the data that was parsed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="command"/> or <paramref name="converter"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method combines the functionality of <see cref="O:ExecuteReader"/> with the standard code used to load data
        /// from a reader into a business object.  <paramref name="converter"/> is called once for each row to build the list
        /// of data objects to return.
        /// </remarks>
        /// <seealso cref="O:ExecuteQueryWithResults{TResult}"/>
        /// <example>
        /// <code lang="C#">
        ///    public Person GetPerson ( ConnectionManager connMgr, int id )
        ///    {
        ///       var cmd = new StoredProcedure("GetPerson")
        ///                                  .WithParameters( 
        ///                                         InputParameter&lt;int&gt;.Named("@id").WithValue(id)
        ///                                           );
        /// 
        ///       return connMgr.ExecuteQueryWithResult&lt;Person&gt;(cmd, ParsePerson);
        ///    }
        /// 
        ///    private Person ParsePerson ( DbDataReader dr )
        ///    {
        ///       Person per = new Person(..);
        ///       ...
        /// 
        ///       return per;
        ///    }
        /// </code>
        /// </example>
        public TResult ExecuteQueryWithResult<TResult> ( DataCommand command, Func<DbDataReader, TResult> converter )
        {
            return ExecuteQueryWithResult<TResult>(command, converter, null);
        }

        /// <summary>Executes a command, parses the result and returns a strongly-typed object.</summary>
        /// <typeparam name="TResult">The type of the objects to return.</typeparam>
        /// <param name="transaction">The transaction to execute within.  If it is <see langword="null"/> then no transaction is used.</param>
        /// <param name="command">The command to execute.</param>
        /// <param name="converter">The method to convert the row to an object.</param>
        /// <returns>An object containing the data that was parsed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="command"/> or <paramref name="converter"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method combines the functionality of <see cref="O:ExecuteReader"/> with the standard code used to load data
        /// from a reader into a business object.  <paramref name="converter"/> is called once for each row to build the list
        /// of data objects to return.
        /// </remarks>
        /// <seealso cref="O:ExecuteQueryWithResults{TResult}"/>
        /// <example>Refer to <see cref="ExecuteQueryWithResult{T}(DataCommand, Func{DbDataReader,T})">ExecuteQueryWithResult</see> for an example.</example>
        public TResult ExecuteQueryWithResult<TResult> ( DataCommand command, Func<DbDataReader, TResult> converter, DataTransaction transaction )
        {
            Verify.Argument("converter", converter).IsNotNull();

            using (var dr = ExecuteReader(command, transaction))
            {
                if (dr.Read())
                    return converter(dr);
            };

            return default(TResult);
        }

        /// <summary>Executes a command, parses the first result and returns a strongly-typed object.</summary>
        /// <typeparam name="TResult">The type of the objects to return.</typeparam>
        /// <param name="command">The command to execute.</param>
        /// <param name="converter">The method to convert the row to an object.</param>
        /// <param name="data">The data to pass to the converter.</param>
        /// <returns>An object containing the data that was parsed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="command"/> or <paramref name="converter"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method combines the functionality of <see cref="O:ExecuteReader"/> with the standard code used to load data
        /// from a reader into a business object.  <paramref name="converter"/> is called once for each row to build the list
        /// of data objects to return.
        /// </remarks>
        /// <seealso cref="O:ExecuteQueryWithResults{TResult}"/>
        /// <example>Refer to <see cref="ExecuteQueryWithResult{T}(DataCommand, Func{DbDataReader,T})">ExecuteQueryWithResult</see> for an example.</example>
        public TResult ExecuteQueryWithResult<TResult> ( DataCommand command, Func<DbDataReader, object, TResult> converter, object data )
        {
            return ExecuteQueryWithResult<TResult>(command, converter, data, null);
        }

        /// <summary>Executes a command, parses the result and returns a strongly-typed object.</summary>
        /// <typeparam name="TResult">The type of the objects to return.</typeparam>
        /// <param name="transaction">The transaction to execute within.  If it is <see langword="null"/> then no transaction is used.</param>
        /// <param name="command">The command to execute.</param>
        /// <param name="converter">The method to convert the row to an object.</param>
        /// <param name="data">The data to pass to the converter.</param>
        /// <returns>An object containing the data that was parsed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="command"/> or <paramref name="converter"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method combines the functionality of <see cref="O:ExecuteReader"/> with the standard code used to load data
        /// from a reader into a business object.  <paramref name="converter"/> is called once for each row to build the list
        /// of data objects to return.
        /// </remarks>
        /// <seealso cref="O:ExecuteQueryWithResults{TResult}"/>
        /// <example>Refer to <see cref="ExecuteQueryWithResult{T}(DataCommand, Func{DbDataReader,T})">ExecuteQueryWithResult</see> for an example.</example>
        public TResult ExecuteQueryWithResult<TResult> ( DataCommand command, Func<DbDataReader, object, TResult> converter, object data, DataTransaction transaction )
        {
            Verify.Argument("converter", converter).IsNotNull();

            using (var dr = ExecuteReader(command, transaction))
            {
                if (dr.Read())
                    return converter(dr, data);
            };

            return default(TResult);
        }
        #endregion

        #region ExecuteQueryWithResults

        /// <summary>Executes a command, parses the results and returns a strongly-typed array of objects.</summary>
        /// <typeparam name="TResult">The type of the objects to return.</typeparam>
        /// <param name="command">The command to execute.</param>
        /// <param name="converter">The method used to convert a row to an object.</param>
        /// <returns>An array containing the objects that were parsed.  The array will never be <see langword="null"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="command"/> or <paramref name="converter"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method combines the functionality of <see cref="O:ExecuteReader"/> with the standard code used to load data
        /// from a reader into a business object.  <paramref name="converter"/> is called once for each row to build the list
        /// of data objects to return.
        /// </remarks>
        /// <seealso cref="O:ExecuteQueryWithResult{TResult}"/>
        /// <example>
        /// <code lang="C#">
        ///    public Person[] GetPerson ( ConnectionManager connMgr, int id )
        ///    {
        ///       var cmd = new StoredProcedure("GetPerson")
        ///                              .WithParameters(
        ///                                   InputParameter&lt;int&gt;.Named("id").WithValue(id)
        ///                                   );
        /// 
        ///       return connMgr.ExecuteQueryWithResults&lt;Person&gt;(cmd, ParsePerson);
        ///    }
        /// 
        ///    private Person ParsePerson ( DbDataReader dr )
        ///    {
        ///       Person per = new Person(..);
        ///       ...
        /// 
        ///       return per;
        ///    }
        /// </code>
        /// </example>
        public TResult[] ExecuteQueryWithResults<TResult> ( DataCommand command, Func<DbDataReader, TResult> converter )
        {
            Verify.Argument("converter", converter).IsNotNull();

            var items = new List<TResult>();
            using (var dr = ExecuteReader(command))
            {
                while (dr.Read())
                    items.Add(converter(dr));
            };

            return items.ToArray();
        }

        /// <summary>Executes a command, parses the results and returns a strongly-typed array of objects.</summary>
        /// <typeparam name="TResult">The type of the objects to return.</typeparam>
        /// <param name="command">The command to execute.</param>
        /// <param name="converter">The method used to convert a row to an object.</param>
        /// <param name="data">User-provided data to parse to the delegate.</param>
        /// <returns>An enumerable list containing the objects that were parsed.  The list will never be <see langword="null"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="command"/> or <paramref name="converter"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method combines the functionality of <see cref="O:ExecuteReader"/> with the standard code used to load data
        /// from a reader into a business object.  <paramref name="converter"/> is called once for each row to build the list
        /// of data objects to return.
        /// </remarks>
        /// <seealso cref="O:ExecuteQueryWithResult{TResult}"/>
        /// <example>Refer to <see cref="ExecuteQueryWithResult{T}(DataCommand,Func{DbDataReader,T})"></see> for an example.</example>
        public IEnumerable<TResult> ExecuteQueryWithResults<TResult> ( DataCommand command, Func<DbDataReader, object, TResult> converter, object data )
        {
            Verify.Argument("converter", converter).IsNotNull();

            var items = new List<TResult>();
            using (var dr = ExecuteReader(command))
            {
                while (dr.Read())
                    items.Add(converter(dr, data));
            };

            return items;
        }

        /// <summary>Executes a command, parses the results and returns a strongly-typed array of objects.</summary>
        /// <typeparam name="TResult">The type of the objects to return.</typeparam>
        /// <param name="transaction">The transaction to execute within.  If it is <see langword="null"/> then no transaction is used.</param>
        /// <param name="command">The command to execute.</param>
        /// <param name="converter">The method to use to convert the row to an object.</param>
        /// <returns>An enumerable list containing the objects that were parsed.  The list will never be <see langword="null"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="command"/> or <paramref name="converter"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method combines the functionality of <see cref="O:ExecuteReader"/> with the standard code used to load data
        /// from a reader into a business object.  <paramref name="converter"/> is called once for each row to build the list
        /// of data objects to return.
        /// </remarks>
        /// <seealso cref="O:ExecuteQueryWithResult{TResult}"/>
        /// <example>Refer to <see cref="ExecuteQueryWithResult{T}(DataCommand,Func{DbDataReader,T})"></see> for an example.</example>
        public IEnumerable<TResult> ExecuteQueryWithResults<TResult> ( DataCommand command, Func<DbDataReader, TResult> converter, DataTransaction transaction )
        {
            Verify.Argument("converter", converter).IsNotNull();

            var items = new List<TResult>();
            using (var dr = ExecuteReader(command, transaction))
            {
                while (dr.Read())
                    items.Add(converter(dr));
            };

            return items;
        }

        /// <summary>Executes a command, parses the results and returns a strongly-typed array of objects.</summary>
        /// <typeparam name="TResult">The type of the objects to return.</typeparam>
        /// <param name="transaction">The transaction to execute within.  If it is <see langword="null"/> then no transaction is used.</param>
        /// <param name="command">The command to execute.</param>
        /// <param name="converter">The method to use to convert the row to an object.</param>
        /// <param name="data">User-provided data to parse to the delegate.</param>
        /// <returns>An enumerable list containing the objects that were parsed.  The list will never be <see langword="null"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="command"/> or <paramref name="converter"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method combines the functionality of <see cref="O:ExecuteReader"/> with the standard code used to load data
        /// from a reader into a business object.  <paramref name="converter"/> is called once for each row to build the list
        /// of data objects to return.
        /// </remarks>
        /// <seealso cref="O:ExecuteQueryWithResult{TResult}"/>
        /// <example>Refer to <see cref="ExecuteQueryWithResult{T}(DataCommand,Func{DbDataReader,T})"></see> for an example.</example>
        public IEnumerable<TResult> ExecuteQueryWithResults<TResult> ( DataCommand command, Func<DbDataReader, Object, TResult> converter, object data, DataTransaction transaction )
        {
            Verify.Argument("converter", converter).IsNotNull();

            var items = new List<TResult>();
            using (var dr = ExecuteReader(command, transaction))
            {
                while (dr.Read())
                    items.Add(converter(dr, data));
            };

            return items;
        }
        #endregion

        #region ExecuteReader

        /// <summary>Executes a command and returns a data reader with the results.</summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>A <see cref="DbDataReader"/> containing the results.  The reader may be empty but it will
        /// never be <see langword="null"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="command"/> is <see langword="null"/>.</exception>
        /// <example>
        /// <code lang="C#">
        ///    private void LoadData ( ConnectionManager connMgr )
        ///    {		
        ///       var cmd = new StoredProcedure(...);
        ///       using(var dr = connMgr.ExecuteReader(cmd))
        ///       {
        ///          ...
        ///       };
        ///    }
        /// </code>
        /// </example>
        /// <seealso cref="O:ExecuteQueryWithResult{T}"/>
        /// <seealso cref="O:ExecuteQueryWithResults{T}"/>
        public DbDataReader ExecuteReader ( DataCommand command )
        {
            return ExecuteReader(command, null);
        }

        /// <summary>Executes a command and returns a data reader with the results.</summary>
        /// <param name="transaction">The transaction to execute within.  If it is <see langword="null"/> then no transaction is used.</param>
        /// <param name="command">The command to execute.</param>
        /// <returns>A <see cref="DbDataReader"/> containing the results.  The reader may be empty but it will
        /// never be <see langword="null"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="command"/> is <see langword="null"/>.</exception>
        /// <example>Refer to <see cref="ExecuteReader(DataCommand)">ExecuteReader</see> for an example.</example>
        /// <seealso cref="O:ExecuteQueryWithResult{T}"/>
        /// <seealso cref="O:ExecuteQueryWithResults{T}"/>
        public DbDataReader ExecuteReader ( DataCommand command, DataTransaction transaction )
        {
            Verify.Argument("command", command).IsNotNull();

            using (var conn = CreateConnectionData(transaction))
            {
                var dr = ExecuteReaderCore(conn, command);

                //The reader is now responsible for connection cleanup
                conn.Detach();

                return dr;
            };
        }
        #endregion

        #region ExecuteScalar

        /// <summary>Executes a command and returns the first result.</summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>The first element from the result set.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="command"/> is <see langword="null"/>.</exception>
        /// <example>
        /// <code lang="C#">
        ///    private object LoadData ( ConnectionManager connMgr )
        ///    {		
        ///       var cmd = new StoredProcedure(...);
        ///       return connMgr.ExecuteScalar(cmd);
        ///    }
        /// </code>
        /// </example>
        public object ExecuteScalar ( DataCommand command )
        {
            return ExecuteScalar(command, null);
        }

        /// <summary>Executes a command and returns the first result.</summary>
        /// <param name="transaction">The transaction to execute within.  If it is <see langword="null"/> then no transaction is used.</param>
        /// <param name="command">The command to execute.</param>
        /// <returns>The first element from the result set.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="command"/> is <see langword="null"/>.</exception>
        /// <example>Refer to <see cref="ExecuteScalar(DataCommand)">ExecuteScalar</see> for an example.</example>
        public object ExecuteScalar ( DataCommand command, DataTransaction transaction )
        {
            Verify.Argument("command", command).IsNotNull();

            using (var conn = CreateConnectionData(transaction))
            {
                return ExecuteScalarCore(conn, command);
            };
        }

        /// <summary>Executes a command and returns the first result.</summary>
        /// <typeparam name="T">The type of the value returned.</typeparam>
        /// <param name="command">The command to execute.</param>
        /// <returns>The first element from the result set.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="command"/> is <see langword="null"/>.</exception>
        /// <example>Refer to <see cref="ExecuteScalar(DataCommand)">ExecuteScalar</see> for an example.</example>
        public T ExecuteScalar<T> ( DataCommand command )
        {
            var result = ExecuteScalar(command);

            return ((result != null) && (result != DBNull.Value)) ? (T)Convert.ChangeType(result, typeof(T)) : default(T);
        }

        /// <summary>Executes a command and returns the first result.</summary>
        /// <typeparam name="T">The type of the value returned.</typeparam>
        /// <param name="transaction">The transaction to execute within.  If it is <see langword="null"/> then no transaction is used.</param>
        /// <param name="command">The command to execute.</param>
        /// <returns>The first element from the result set.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="command"/> is <see langword="null"/>.</exception>
        /// <example>Refer to <see cref="ExecuteScalar(DataCommand)">ExecuteScalar</see> for an example.</example>
        public T ExecuteScalar<T> ( DataCommand command, DataTransaction transaction )
        {
            var result = ExecuteScalar(command, transaction);

            return ((result != null) && (result != DBNull.Value)) ? (T)Convert.ChangeType(result, typeof(T)) : default(T);
        }
        #endregion

        #region FillDataSet

        /// <summary>Fills a data set with the results of a command.</summary>
        /// <param name="ds">The data set to fill.</param>
        /// <param name="command">The command to execute.</param>
        /// <exception cref="ArgumentNullException"><paramref name="ds"/> or <paramref name="command"/> is <see langword="null"/>.</exception>
        /// <example>
        /// <code lang="C#">
        ///    public void RefreshData ( ConnectionManager connMgr, DataSet ds )
        ///    {
        ///       var cmd = new StoredProcedure(...);
        ///       connMgr.FillDataSet(cmd, ds);
        ///    }
        /// </code>
        /// </example>
        public void FillDataSet ( DataCommand command, DataSet ds )
        {
            FillDataSet(command, ds, null, null);
        }

        /// <summary>Fills a data set with the results of a command.</summary>
        /// <param name="ds">The data set to fill.</param>
        /// <param name="tables">The tables to use for storing the results.</param>
        /// <param name="command">The command to execute.</param>
        /// <exception cref="ArgumentNullException"><paramref name="ds"/> or <paramref name="command"/> is <see langword="null"/>.</exception>
        /// <example>Refer to <see cref="FillDataSet(DataCommand, DataSet)">FillDataSet</see> for an example.</example>
        public void FillDataSet ( DataCommand command, DataSet ds, string[] tables )
        {
            FillDataSet(command, ds, tables, null);
        }

        /// <summary>Fills a data set with the results of a command.</summary>
        /// <param name="transaction">The transaction to execute within.  If it is <see langword="null"/> then no transaction is used.</param>
        /// <param name="ds">The data set to fill.</param>
        /// <param name="command">The command to execute.</param>
        /// <exception cref="ArgumentNullException"><paramref name="ds"/> or <paramref name="command"/> is <see langword="null"/>.</exception>
        /// <example>Refer to <see cref="FillDataSet(DataCommand, DataSet)">FillDataSet</see> for an example.</example>
        public void FillDataSet ( DataCommand command, DataSet ds, DataTransaction transaction )
        {
            FillDataSet(command, ds, null, transaction);
        }

        /// <summary>Fills a data set with the results of a command.</summary>
        /// <param name="transaction">The transaction to execute within.  If it is <see langword="null"/> then no transaction is used.</param>
        /// <param name="ds">The data set to fill.</param>
        /// <param name="tables">The tables to use for storing the results.</param>
        /// <param name="command">The command to execute.</param>
        /// <exception cref="ArgumentNullException"><paramref name="ds"/> or <paramref name="command"/> is <see langword="null"/>.</exception>
        /// <example>Refer to <see cref="FillDataSet(DataCommand, DataSet)">FillDataSet</see> for an example.</example>
        public void FillDataSet ( DataCommand command, DataSet ds, string[] tables, DataTransaction transaction )
        {
            Verify.Argument("ds", ds).IsNotNull();
            Verify.Argument("command", command).IsNotNull();

            using (var conn = CreateConnectionData(transaction))
            {
                FillDataSetCore(conn, ds, command, tables);
            };
        }
        #endregion

        #region QueryParameters

        /// <summary>Gets the parameters associated with a stored procedure.</summary>
        /// <param name="name">The name of the stored procedure.</param>
        /// <returns>An array of data parameters, in ordinal order.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="name"/> is empty.</exception>
        /// <exception cref="NotSupportedException">The manager does not support this method.</exception>
        /// <remarks>
        /// Not all managers support this method.  Use <see cref="SupportsQueryParameters"/> to determine if the
        /// manager supports this method.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "QueryParameters")]
        public DataParameter[] QueryParameters ( string name )
        {
            Verify.Argument("name", name).IsNotNullOrEmpty();
            if (!SupportsQueryParameters)
                throw new NotSupportedException("The manager does not support QueryParameters.");

            return this.QueryParametersBase(name);
        }
        #endregion

        /// <summary>Sets the user context.</summary>
        /// <param name="userContext">The user context.</param>
        /// <returns>The user context.</returns>
        /// <remarks>
        /// Not all connection will support a user context.
        /// </remarks>
        public ConnectionManager SetUserContext ( string userContext )
        {
            m_userContext = userContext;

            return this;
        }

        #region UpdateDataSet

        /// <summary>Updates a <see cref="DataSet"/>.</summary>
        /// <param name="insertCommand">Command to use for insertions.</param>
        /// <param name="deleteCommand">Command to use for deletions.</param>
        /// <param name="updateCommand">Command to use for updates.</param>
        /// <param name="ds"><see cref="DataSet"/> to use.</param>
        /// <param name="table">The table to use when updating the command.  If <see langword="null"/> or empty then the
        /// first table is used.</param>
        /// <remarks>
        /// To support transactions, precede this call with ExecuteScalar("begin transaction").  Failure to
        /// use transactions could cause failed updates to be partially applied.
        /// <para/>
        /// The command parameters can be <see langword="null"/> but the update will fail if the command is needed.		
        /// </remarks>		
        /// <exception cref="DBConcurrencyException">A concurrency error occurred.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="ds"/> is <see langword="null"/>.</exception>	
        /// <example>
        /// <code lang="C#">
        ///    public void UpdateData ( ConnectionManagerBase connMgr, DataSet ds )
        ///    {
        ///       var cmdInsert = new Query(...);
        ///       var cmdUpdate = new Query(...);
        ///       var cmdDelete = new Query(...);
        /// 
        ///       connMgr.UpdateDataSet(cmdInsert, cmdDelete, cmdUpdate, ds, null);
        ///    }
        /// </code>
        /// </example>
        public void UpdateDataSet ( DataCommand insertCommand, DataCommand deleteCommand,
                                    DataCommand updateCommand, DataSet ds, string table )
        {
            UpdateDataSet(insertCommand, deleteCommand, updateCommand, ds, table, null);
        }

        /// <summary>Updates a <see cref="DataSet"/>.</summary>
        /// <param name="transaction">The transaction to execute within.  If it is <see langword="null"/> then no transaction is used.</param>
        /// <param name="insertCommand">Command to use for insertions.</param>
        /// <param name="deleteCommand">Command to use for deletions.</param>
        /// <param name="updateCommand">Command to use for updates.</param>
        /// <param name="ds"><see cref="DataSet"/> to use.</param>
        /// <param name="table">The table to use when updating the command.  If <see langword="null"/> or empty then the
        /// first table is used.</param>
        /// <remarks>
        /// To support transactions, precede this call with ExecuteScalar("begin transaction").  Failure to
        /// use transactions could cause failed updates to be partially applied.
        /// <para/>
        /// The command parameters can be <see langword="null"/> but the update will fail if the command is needed.
        /// </remarks>		
        /// <exception cref="DBConcurrencyException">A concurrency error occurred.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="ds"/> is <see langword="null"/>.</exception>
        /// <example>Refer to <see cref="UpdateDataSet(DataCommand,DataCommand,DataCommand,DataSet,string)">UpdateDataSet</see> for an example.</example>
        public void UpdateDataSet ( DataCommand insertCommand, DataCommand deleteCommand,
                                    DataCommand updateCommand, DataSet ds, string table, DataTransaction transaction )
        {
            //Validate
            Verify.Argument("insertCommand", insertCommand).IsNotNull();
            Verify.Argument("deleteCommand", deleteCommand).IsNotNull();
            Verify.Argument("updateCommand", updateCommand).IsNotNull();
            Verify.Argument("ds", ds).IsNotNull();

            //Initialize table name as needed
            table = (table ?? "").Trim();
            if (String.IsNullOrEmpty(table) && (ds.Tables.Count > 0))
                table = ds.Tables[0].TableName;

            using (var conn = CreateConnectionData(transaction))
            {
                UpdateDataSetCore(conn, insertCommand, deleteCommand, updateCommand, ds, table);
            };
        }
        #endregion
        
        #region Protected Members

        #region Attributes

        /// <summary>Gets or sets the connection information.</summary>
        protected string ConnectionString
        {
            get { return m_connString; }
            set { m_connString = (value != null) ? value.Trim() : ""; }
        }
        #endregion

        #region Abstract/Virtual Methods

        /// <summary>Creates a command.</summary>
        /// <param name="command">The command.</param>
        /// <returns>The underlying command.</returns>
        protected abstract DbCommand CreateCommandBase ( DataCommand command );

        /// <summary>Creates a connection given a connection string.</summary>
        /// <param name="connectionString">The connection string to use.</param>
        /// <returns>The underlying connection object.</returns>
        protected abstract DbConnection CreateConnectionBase ( string connectionString );

        /// <summary>Creates a data adapter.</summary>
        /// <returns>The underlying data adapter.</returns>
        protected abstract DbDataAdapter CreateDataAdapterBase ();

        /// <summary>Creates a transaction.</summary>
        /// <param name="connection">The connection used for the transaction.</param>
        /// <param name="level">The isolation level to use.</param>
        /// <returns>The underlying transaction.</returns>
        protected virtual DbTransaction CreateTransactionBase ( DbConnection connection, IsolationLevel level )
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            return connection.BeginTransaction(level);
        }

        /// <summary>Queries for the parameters associated with a stored procedure.</summary>
        /// <param name="name">The name of the stored procedure to query.</param>
        /// <returns>An array of parameters.</returns>
        /// <exception cref="NotSupportedException">Always thrown.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "QueryParameters")]
        protected virtual DataParameter[] QueryParametersBase ( string name )
        {
            throw new NotSupportedException("QueryParameters is not supported.");
        }
        #endregion

        #region Methods

        /// <summary>Begins a transaction.</summary>
        /// <param name="level">The level of the transaction.</param>
        /// <returns>The underlying transaction.</returns>
        protected virtual DataTransaction BeginTransactionCore ( IsolationLevel level )
        {
            DbConnection conn = null;
            try
            {
                conn = CreateConnectionBase(ConnectionString);

                return new DataTransaction(CreateTransactionBase(conn, level), true);
            } catch (Exception)
            {
                if (conn != null)
                    conn.Dispose();

                throw;
            };
        }

        /// <summary>Populates a data set with data.</summary>
        /// <param name="conn">The connection information.</param>
        /// <param name="command">The command to execute.</param>
        /// <returns>The data set results.</returns>
        protected virtual DataSet ExecuteDataSetCore ( ConnectionData conn, DataCommand command )
        {
            DataSet ds = new DataSet();

            try
            {
                ds.Locale = CultureInfo.InvariantCulture;
                FillDataSetCore(conn, ds, command, null);
                return ds;
            } catch
            {
                ds.Dispose();
                throw;
            };
        }

        /// <summary>Executes a command and returns the results.</summary>
        /// <param name="conn">The connection information.</param>
        /// <param name="command">The command to execute.</param>
        /// <returns>The results of the command.</returns>
        protected virtual int ExecuteNonQueryCore ( ConnectionData conn, DataCommand command )
        {
            using (DbCommand cmd = PrepareCommandCore(conn, command))
            {
                conn.Open();

                int result = cmd.ExecuteNonQuery();

                //Copy the parameter values back
                UpdateParameterCore(cmd, command);
                return result;
            };
        }

        /// <summary>Executes a command and returns the results.</summary>
        /// <param name="conn">The connection information.</param>
        /// <param name="command">The command to execute.</param>
        /// <returns>The results of the command.</returns>
        protected virtual DbDataReader ExecuteReaderCore ( ConnectionData conn, DataCommand command )
        {
            using (DbCommand cmd = PrepareCommandCore(conn, command))
            {
                conn.Open();

                //Can't close the connection if it is associated with a transaction so do the check now
                var behavior = (conn.Transaction != null) ? CommandBehavior.Default : CommandBehavior.CloseConnection;

                //Create the reader
                DbDataReader dr = null;
                try
                {
                    dr = cmd.ExecuteReader(behavior);

                    //Copy the parameter values back
                    UpdateParameterCore(cmd, command);
                } catch
                {
                    if (dr != null)
                        dr.Close();

                    throw;
                };

                return dr;
            };
        }

        /// <summary>Executes a command and returns the results.</summary>
        /// <param name="conn">The connection information.</param>
        /// <param name="command">The command to execute.</param>
        /// <returns>The results of the command.</returns>
        protected virtual object ExecuteScalarCore ( ConnectionData conn, DataCommand command )
        {
            using (DbCommand cmd = PrepareCommandCore(conn, command))
            {
                conn.Open();

                object obj = cmd.ExecuteScalar();

                //Copy the parameter values back
                UpdateParameterCore(cmd, command);
                return obj;
            };
        }

        /// <summary>Fills a dataset with the results of a command.</summary>
        /// <param name="conn">The underlying connection data to use.</param>
        /// <param name="ds">The dataset to populate.</param>
        /// <param name="command">The command to execute.</param>
        /// <param name="tables">The table(s) to populate.</param>
        /// <remarks>
        /// The default implementation uses a <see cref="DbDataAdapter"/> to fill the data set using
        /// the specified command.
        /// </remarks>
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
        protected virtual void FillDataSetCore ( ConnectionData conn, DataSet ds, DataCommand command, string[] tables )
        {
            DbCommand cmdDb = null;

            try
            {
                conn.Open();

                //Create the adapter
                using (DbDataAdapter da = CreateDataAdapterBase())
                {
                    //Add the tables as needed
                    if ((tables != null) && (tables.Length > 0))
                    {
                        string strTable = "Table";
                        for (int nIdx = 0;
                             nIdx < tables.Length;
                             ++nIdx)
                        {
                            Verify.Argument("tables", tables[nIdx]).IsNotNullOrEmpty("One or more table entries are invalid");
                            da.TableMappings.Add(strTable, tables[nIdx]);
                            strTable = String.Concat("Table", (nIdx + 1).ToString());
                        };
                    };

                    //Execute
                    cmdDb = PrepareCommandCore(conn, command);
                    da.SelectCommand = cmdDb;
                    da.Fill(ds);

                    //Copy the parameter values back
                    UpdateParameterCore(cmdDb, command);
                };
            } finally
            {
                if (cmdDb != null)
                    cmdDb.Dispose();
            };
        }

        /// <summary>Updates a data set.</summary>
        /// <param name="conn">The connection information.</param>
        /// <param name="insertCommand">The command for inserting rows.</param>
        /// <param name="updateCommand">The command for updating rows.</param>
        /// <param name="deleteCommand">The command for deleting rows.</param>
        /// <param name="ds">The data set to update.</param>
        /// <param name="table">The table to update.</param>
        /// <remarks>
        /// The default implementation uses a <see cref="DbDataAdapter"/> to update the data set using
        /// the provided commands.
        /// </remarks>
        protected virtual void UpdateDataSetCore ( ConnectionData conn, DataCommand insertCommand, DataCommand updateCommand,
                                         DataCommand deleteCommand, DataSet ds, string table )
        {
            DbCommand cmdInsert = null, cmdUpdate = null, cmdDelete = null;

            try
            {
                conn.Open();

                //Create the adapter
                using (DbDataAdapter da = CreateDataAdapterBase())
                {
                    if (insertCommand != null)
                    {
                        cmdInsert = PrepareCommandCore(conn, insertCommand);
                        da.InsertCommand = cmdInsert;
                    };
                    if (updateCommand != null)
                    {
                        cmdUpdate = PrepareCommandCore(conn, updateCommand);
                        da.UpdateCommand = cmdUpdate;
                    };
                    if (deleteCommand != null)
                    {
                        cmdDelete = PrepareCommandCore(conn, deleteCommand);
                        da.DeleteCommand = cmdDelete;
                    };

                    //Update
                    da.Update(ds, table);

                    //Commit the changes
                    ds.AcceptChanges();
                };
            } finally
            {
                if (cmdInsert != null)
                    cmdInsert.Dispose();
                if (cmdUpdate != null)
                    cmdUpdate.Dispose();
                if (cmdDelete != null)
                    cmdDelete.Dispose();
            };
        }
        #endregion

        #endregion

        #region Private Members

        #region Methods

        private ConnectionData CreateConnectionData ( DataTransaction transaction )
        {
            ConnectionData data = null;

            try
            {
                if (transaction != null)
                    data = new ConnectionData(transaction.InnerTransaction);
                else
                    data = new ConnectionData(CreateConnectionBase(ConnectionString));

                PrepareConnectionCore(data);
                return data;
            } catch
            {
                if (data != null)
                    data.Dispose();

                throw;
            };
        }

        private DbCommand PrepareCommandCore ( ConnectionData conn, DataCommand command )
        {
            DataParameter addedParameter = null;

            //Create the underlying command
            DbCommand cmd = null;
            try
            {
                //Do we need to add a return value parameter?
                var addParameter = (command is StoredProcedure) && !command.Parameters.Any(p => p.Direction == ParameterDirection.ReturnValue);

                //Add it now so it'll be treated like every other parameter
                if (addParameter)
                {
                    addedParameter = new DataParameter("return", DbType.Int32, ParameterDirection.ReturnValue);
                    command.Parameters.Add(addedParameter);
                };

                cmd = CreateCommandBase(command);

                //Set any null parameter values to DBNull
                foreach (var parm in cmd.Parameters.OfType<DbParameter>())
                {
                    if (parm.Value == null)
                        parm.Value = DBNull.Value;
                };
                
                //Initialize the command with the connection information
                cmd.Connection = conn.Connection;
                if (conn.Transaction != null)
                    cmd.Transaction = conn.Transaction;
            } catch (Exception)
            {
                if (cmd != null)
                    cmd.Dispose();

                throw;
            } finally
            {
                //Remove the return value parameter if we added it
                if (addedParameter != null)
                    command.Parameters.Remove(addedParameter);
            };

            return cmd;
        }

        /// <summary>Prepares the connection after it has been opened.</summary>
        /// <param name="connection">The open connection.</param>
        /// <remarks>
        /// The default implementation checks to see if the connection manager supports user contexts.  If so then it calls
        /// <see cref="SetUserContext"/>.
        /// </remarks>
        protected virtual void PrepareConnectionCore ( ConnectionData connection )
        {
            if (SupportsUserContext)
            {
                SetUserContextCore(connection, m_userContext ?? "");
            };
        }

        /// <summary>Sets the user context.</summary>
        /// <param name="connection">The open connection.</param>
        /// <param name="userContext">The user context to use.</param>
        /// <remarks>
        /// The default implementation does nothing.
        /// </remarks>
        protected virtual void SetUserContextCore ( ConnectionData connection, string userContext )
        {
        }

        /// <summary>Updates the command with any output or input/output parameter values.</summary>
        /// <param name="command">The command to copy the values from.</param>
        /// <param name="target">The target to copy the values to.</param>
        /// <remarks>
        /// The default implementation will copy the value from any input/output, output or return value parameters back
        /// to the target.
        /// </remarks>
        private static void UpdateParameterCore ( DbCommand command, DataCommand target )
        {
            //If a parameter was added to store the return value
            var sproc = target as StoredProcedure;
            if (sproc != null)
            {
                //Get the return value from the call
                var returnParam = (from p in command.Parameters.Cast<DbParameter>()
                                   where p.Direction == ParameterDirection.ReturnValue
                                   select p).FirstOrDefault();
                if (returnParam != null)
                    sproc.ReturnValue = TypeConversion.ToInt32OrDefault(returnParam.Value);
            };

            for (int nIdx = 0; nIdx < target.Parameters.Count; ++nIdx)
            {
                switch (command.Parameters[nIdx].Direction)
                {
                    case ParameterDirection.InputOutput:
                    case ParameterDirection.Output:
                    case ParameterDirection.ReturnValue:
                    {
                        target.Parameters[nIdx].Value = command.Parameters[nIdx].Value;
                        break;
                    };
                };
            };
        }
        #endregion

        #region Data

        private string m_connString;
        private string m_userContext;

        #endregion

        #endregion 
    }
}
