using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

using P3Net.Kraken.Data;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.Data
{
    public class DataSetExtensionsTests : UnitTest
    {       
        #region HasTables

        [TestMethod]
        public void HasTables_IsTrue ()
        {
            var target = new DataSet();
            target.Tables.Add(new DataTable());

            var actual = target.HasTables();

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void HasTables_IsFalse ()
        {
            var target = new DataSet();
            
            var actual = target.HasTables();

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void HasTables_IsNull ()
        {
            DataSet target = null;

            var actual = target.HasTables();

            actual.Should().BeFalse();
        }
        #endregion

        #region TableCount

        [TestMethod]
        public void TableCount_IsTrue ()
        {
            var target = new DataSet();
            target.Tables.Add(new DataTable());

            var actual = target.TableCount();

            actual.Should().Be(1);
        }

        [TestMethod]
        public void TableCount_IsFalse ()
        {
            var target = new DataSet();

            var actual = target.TableCount();

            actual.Should().Be(0);
        }

        [TestMethod]
        public void TableCount_IsNull ()
        {
            DataSet target = null;

            var actual = target.TableCount();

            actual.Should().Be(0);
        }
        #endregion

        #region TablesAsEnumerable

        [TestMethod]
        public void TablesAsEnumerable_HasTables ()
        {
            var target = new DataSet();
            var table = new DataTable();
            target.Tables.Add(table);

            var actual = target.TablesAsEnumerable();

            actual.Should().HaveCount(1);
        }

        [TestMethod]
        public void TablesAsEnumerable_NoTables ()
        {
            var target = new DataSet();

            var actual = target.TablesAsEnumerable();

            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void TablesAsEnumerable_IsNull ()
        {
            DataSet target = null;

            var actual = target.TablesAsEnumerable();

            actual.Should().BeEmpty();
        }
        #endregion
    }
}
