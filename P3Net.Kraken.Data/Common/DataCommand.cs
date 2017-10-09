/*
 * Copyright © 2005 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Data;

using P3Net.Kraken.Collections;
using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.Data.Common
{
    /// <summary>Represents a command to be executed against a database.</summary>
    public class DataCommand 
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="DataCommand"/> class.</summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="type">The type of command.</param>
        /// <exception cref="ArgumentNullException"><paramref name="commandText"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="commandText"/> is empty.</exception>
        public DataCommand ( string commandText, CommandType type )
        {
            Verify.Argument(nameof(commandText)).WithValue(commandText).IsNotNullOrEmpty();

            CommandText = commandText;
            CommandType = type;

            Parameters = new DataParameterCollection();
        }        
        #endregion
                
        /// <summary>Gets the command to execute.</summary>
        public string CommandText { get; private set; }

        /// <summary>Gets or sets the timeout for the command.</summary>
        /// <exception cref="ArgumentOutOfRangeException">The timeout is less than zero.</exception>
        /// <value>The default is zero.</value>
        public TimeSpan CommandTimeout
        {
            get => _commandTimeout;
            set 
            {
                Verify.Argument(nameof(value)).WithValue(value).IsGreaterThan(TimeSpan.Zero);
                
                _commandTimeout = value; 
            }
        }

        /// <summary>Gets the type of command to execute.</summary>
        public CommandType CommandType { get; private set; }

        /// <summary>Gets the parameters associated with the command.</summary>
        public DataParameterCollection Parameters { get; private set; }


        /// <summary>Gets the return value after the stored procedure has been run.</summary>
        /// <value>This property is only used if <see cref="SupportsReturnValue"/> is <see langword="true"/>.</value>
        public int ReturnValue { get; protected internal set; }

        /// <summary>Determines if the command supports having a return value.</summary>
        public bool SupportsReturnValue { get; protected set; }

        /// <summary>Gets or sets how results are applied to a <see cref="DataRow"/> for Update commands.</summary>
        public UpdateRowSource UpdatedRowSource { get; set; }

        /// <summary>Gets a string representation of the class.</summary>
        /// <returns>A string representing the class.</returns>
        public override string ToString () => CommandText;

        /// <summary>Adds parameters to the command.</summary>
        /// <param name="parameters">The parameters to add.</param>
        /// <returns>The updated command.</returns>
        public DataCommand WithParameters ( params DataParameter[] parameters )
        {
            if (parameters != null)
                Parameters.AddRange(parameters);

            return this;
        }
        
        #region Private Members

        private TimeSpan _commandTimeout;

        #endregion 
    }

    /// <summary>Represents a stored procedure command.</summary>
    public class StoredProcedure : DataCommand
    {
        /// <summary>Initializes an instance of the <see cref="StoredProcedure"/> class.</summary>
        /// <param name="name">The name of the stored procedure.</param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="name"/> is empty.</exception>
        public StoredProcedure ( string name ) : base(name, CommandType.StoredProcedure)
        {
            SupportsReturnValue = true;
        }
    }

    /// <summary>Represents an adhoc query command.</summary>
    public class AdhocQuery : DataCommand
    {
        #region Construction
        
        /// <summary>Initializes an instance of the <see cref="AdhocQuery"/> class.</summary>
        /// <param name="commandText">The query text.</param>
        /// <exception cref="ArgumentNullException"><paramref name="commandText"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="commandText"/> is empty.</exception>
        public AdhocQuery ( string commandText ) : base(commandText, CommandType.Text)
        {
        }
        #endregion
    }
}