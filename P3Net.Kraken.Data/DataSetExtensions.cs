/*
 * Copyright © 2012 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace P3Net.Kraken.Data
{
    /// <summary>Provides extension methods for <see cref="DataSet"/>.</summary>
    public static class DataSetExtensions
    {        
        /// <summary>Determines if the set has any tables.</summary>
        /// <param name="source">The source.</param>
        /// <returns><see langword="true"/> if the set has any tables.</returns>
        public static bool HasTables ( this DataSet source )
        {
            return source.Tables.Count > 0;
        }

        /// <summary>Gets the number of tables in the dataset.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The number of tables.</returns>
        public static int TableCount ( this DataSet source )
        {
            return source.Tables.Count;
        }

        /// <summary>Gets the enumerable list of tables.</summary>
        /// <param name="source">The source.</param>
        /// <returns>An enumerable list of tables.</returns>
        public static IEnumerable<DataTable> TablesAsEnumerable ( this DataSet source )
        {
            return source.Tables.OfType<DataTable>();
        }
    }
}
