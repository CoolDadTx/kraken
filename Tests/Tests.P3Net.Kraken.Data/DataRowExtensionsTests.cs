#region Imports

using System;
using System.Data;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Data;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken.Data
{
    [TestClass]
    public partial class DataRowExtensionsTests : UnitTest
    {
        #region Private Members

        //Helper members to create tables and rows
        private sealed class ColumnDefinition
        {
            public ColumnDefinition ( string name, Type type, object initialValue = null )
            {
                Name = name;
                Type = type;
                InitialValue = initialValue ?? DBNull.Value;
            }

            public object InitialValue { get; private set; }
            public string Name { get; private set; }
            public Type Type { get; private set; }
        }
        
        private static DataRow CreateRow ( params ColumnDefinition[] columns )
        {
            var table = new DataTable();
            foreach (var column in columns)
                table.Columns.Add(column.Name, column.Type);

            var row = table.NewRow();
            foreach (var column in columns)
                row[column.Name] = column.InitialValue;

            return row;
        }
        #endregion
    }
}
