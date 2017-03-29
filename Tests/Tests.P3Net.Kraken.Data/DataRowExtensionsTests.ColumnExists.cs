#region Imports

using System;
using System.Data;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Data;
#endregion

namespace Tests.P3Net.Kraken.Data
{
    public partial class DataRowExtensionsTests
    {
        [TestMethod]
        public void ColumnExists_ColumnDoesExist ()
        {
            //Arrange
            var table = new DataTable();
            table.Columns.Add("Column1", typeof(int));
            table.Columns.Add("Column2", typeof(string));

            var target = table.NewRow();

            //Act
            var actual = target.ColumnExists("Column2");

            //Assert
            actual.Should().BeTrue();            
        }

        [TestMethod]
        public void ColumnExists_ColumnDoesNotExist ()
        {
            //Arrange
            var table = new DataTable();
            table.Columns.Add("Column1", typeof(int));

            var target = table.NewRow();

            //Act
            var actual = target.ColumnExists("Column2");

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ColumnExists_ColumnIsNull ()
        {
            //Arrange
            var table = new DataTable();            
            var target = table.NewRow();

            //Act
            var actual = target.ColumnExists(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ColumnExists_ColumnIsEmpty ()
        {
            //Arrange
            var table = new DataTable();
            var target = table.NewRow();

            //Act
            var actual = target.ColumnExists("");
        }

        [TestMethod]
        public void ColumnExists_ColumnExistsWithDifferentCase ()
        {
            //Arrange
            var table = new DataTable();
            table.Columns.Add("Column1", typeof(int));

            var target = table.NewRow();

            //Act
            var actual = target.ColumnExists("cOlUmN1");

            //Assert
            actual.Should().BeTrue();
        }
    }
}
