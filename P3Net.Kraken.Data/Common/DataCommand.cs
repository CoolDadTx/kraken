/*
 * Copyright © 2005 Michael Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;

using P3Net.Kraken.Collections;
using P3Net.Kraken.Diagnostics;
#endregion

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
            Verify.Argument("commandText", commandText).IsNotNullOrEmpty();

            CommandText = commandText;
            CommandType = type;

            Parameters = new DataParameterCollection();
        }        
        #endregion

        #region Public Members

        #region Attributes

        /// <summary>Gets the command to execute.</summary>
        public string CommandText { get; private set; }

        /// <summary>Gets or sets the timeout for the command.</summary>
        /// <exception cref="ArgumentOutOfRangeException">The timeout is less than zero.</exception>
        /// <value>The default is zero.</value>
        public TimeSpan CommandTimeout
        {
            get { return m_commandTimeout; }
            set 
            {
                Verify.Argument("value", value).IsGreaterThan(TimeSpan.Zero);
                
                m_commandTimeout = value; 
            }
        }

        /// <summary>Gets the type of command to execute.</summary>
        public CommandType CommandType { get; private set; }

        /// <summary>Gets the parameters associated with the command.</summary>
        public DataParameterCollection Parameters { get; private set; }

        /// <summary>Gets or sets how results are applied to a <see cref="DataRow"/> for Update commands.</summary>
        public UpdateRowSource UpdatedRowSource { get; set; }
        #endregion

        #region Methods
        
        /// <summary>Gets a string representation of the class.</summary>
        /// <returns>A string representing the class.</returns>
        public override string ToString()
        {
            return CommandText;
        }

        /// <summary>Adds parameters to the command.</summary>
        /// <param name="parameters">The parameters to add.</param>
        /// <returns>The updated command.</returns>
        public DataCommand WithParameters ( params DataParameter[] parameters )
        {
            if (parameters != null)
                Parameters.AddRange(parameters);

            return this;
        }
        #endregion

        #endregion
        
        #region Private Members

        private TimeSpan m_commandTimeout;

        #endregion 
    }

    /// <summary>Represents a stored procedure command.</summary>
    public class StoredProcedure : DataCommand
    {
        #region Construction
        
        /// <summary>Iniitializes an instance of the <see cref="StoredProcedure"/> class.</summary>
        /// <param name="name">The name of the stored procedure.</param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="name"/> is empty.</exception>
        public StoredProcedure ( string name ) : base(name, CommandType.StoredProcedure)
        {
        }
        #endregion

        #region Public Members

        /// <summary>Gets the return value after the stored procedure has been run.</summary>
        public int ReturnValue { get; internal set; }
        #endregion
    }

    /// <summary>Represents an adhoc query command.</summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Adhoc")]
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