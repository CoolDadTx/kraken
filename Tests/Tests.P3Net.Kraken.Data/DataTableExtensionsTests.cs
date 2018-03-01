using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Data;
using P3Net.Kraken.UnitTesting;


namespace Tests.P3Net.Kraken.Data
{    
    [TestClass]
    public class DataTableExtensionsTests : UnitTest
    {
        #region AddColumn

        [TestMethod]
        public void AddColumn_Successful ()
        {
            var expectedName = "expectedColumn1";
            var expectedType = typeof(long);

            var target = new DataTable();

            var actual = target.AddColumn(expectedName, expectedType);

            //Assert
            actual.Should().Be(target);
            actual.Columns[0].ColumnName.Should().Be(expectedName);
            actual.Columns[0].DataType.Should().Be(expectedType);
        }
        #endregion

        #region HasRows

        [TestMethod]
        public void HasRows_IsTrue ()
        {
            var target = new DataTable();
            var row = target.NewRow();
            target.Rows.Add(row);

            var actual = target.HasRows();

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void HasRows_IsFalse ()
        {
            var target = new DataTable();

            var actual = target.HasRows();

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void HasRows_IsNull ()
        {
            DataTable target = null;

            Action action = () => target.HasRows();

            action.Should().Throw<NullReferenceException>();
        }
        #endregion

        #region InsertRow

        [TestMethod]
        public void InsertRow_Successful ()
        {
            var expectedId = 1;
            var expectedName = "First";

            var target = new DataTable();
            target.Columns.Add("Id", typeof(int));
            target.Columns.Add("Name", typeof(string));

            var actual = target.InsertRow(expectedId, expectedName);

            //Assert
            actual.Should().Be(target);
            actual.Rows[0][0].Should().Be(expectedId);
            actual.Rows[0][1].Should().Be(expectedName);
        }

        [TestMethod]
        public void InsertRow_EmptyRow ()
        {
            var target = new DataTable();
            target.Columns.Add("Id", typeof(int));
            target.Columns.Add("Name", typeof(string));

            var actual = target.InsertRow();

            //Assert
            actual.Should().Be(actual);

            actual.Rows[0][0].Should().Be(DBNull.Value);
            actual.Rows[0][1].Should().Be(DBNull.Value);
        }
        #endregion
               
        #region RowCount

        [TestMethod]
        public void RowCount_IsTrue ()
        {
            var target = new DataTable();
            var row = target.NewRow();
            target.Rows.Add(row);

            var actual = target.RowCount();

            actual.Should().Be(1);
        }

        [TestMethod]
        public void RowCount_IsFalse ()
        {
            var target = new DataTable();

            var actual = target.RowCount();

            actual.Should().Be(0);
        }

        [TestMethod]
        public void RowCount_IsNull ()
        {
            DataTable target = null;

            Action action = () => target.RowCount();

            action.Should().Throw<NullReferenceException>();
        }
        #endregion

        #region RowsAsEnumerable

        [TestMethod]
        public void RowsAsEnumerable_HasTables ()
        {
            var target = new DataTable();
            var row = target.NewRow();
            target.Rows.Add(row);

            var actual = target.RowsAsEnumerable();

            actual.Should().HaveCount(1);
        }

        [TestMethod]
        public void RowsAsEnumerable_NoTables ()
        {
            var target = new DataTable();

            var actual = target.RowsAsEnumerable();

            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void RowsAsEnumerable_IsNull ()
        {
            DataTable target = null;

            Action action = () => target.RowsAsEnumerable();

            action.Should().Throw<NullReferenceException>();
        }
        #endregion
    }
}
