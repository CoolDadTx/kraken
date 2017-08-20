/*
 * Copyright © 2011 Michael Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
#endregion


namespace P3Net.Kraken.Data
{
    /// <summary>Extension methods for data tables.</summary>
    public static class DataTableExtensions
    {
        /// <summary>Adds a column to a table.</summary>
        /// <param name="source">The source.</param>
        /// <param name="name">The column name.</param>
        /// <param name="type">The column type.</param>
        /// <returns>The updated table.</returns>
        public static DataTable AddColumn ( this DataTable source, string name, Type type )
        {
            var column = new DataColumn(name, type);
            source.Columns.Add(column);

            return source;
        }

        /// <summary>Adds a new row to the table.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The new row.</returns>
        public static DataRow AddNewRow ( this DataTable source )
        {
            var row = source.NewRow();
            source.Rows.Add(row);

            return row;
        }
                
        /// <summary>Determines if the table has any rows.</summary>
        /// <param name="source">The source.</param>
        /// <returns><see langword="true"/> if the table has rows.</returns>
        public static bool HasRows ( this DataTable source )
        {
            return source.Rows.Count > 0;
        }

        /// <summary>Creates and inserts a row into a table.</summary>
        /// <param name="source">The source.</param>
        /// <param name="values">The row values.</param>
        /// <returns>The updated table.</returns>
        public static DataTable InsertRow ( this DataTable source, params object[] values )
        {
            var row = source.NewRow();

            if (values != null)
                for (int index = 0; index < values.Length; ++index)
                    row[index] = values[index];

            source.Rows.Add(row);
            return source;
        }

        /// <summary>Determines how many rows are in the table.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The number of rows.</returns>
        public static int RowCount ( this DataTable source )
        {
            return source.Rows.Count;
        }

        /// <summary>Gets the rows as an enumerable list.</summary>
        /// <param name="source">The source.</param>
        /// <returns>An enumerabe list.</returns>
        public static IEnumerable<DataRow> RowsAsEnumerable ( this DataTable source )
        {
            return source.Rows.OfType<DataRow>();
        }
    };
}
