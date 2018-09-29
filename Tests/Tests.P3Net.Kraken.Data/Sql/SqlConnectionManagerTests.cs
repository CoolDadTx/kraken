using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken;
using P3Net.Kraken.Data.Sql;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.Data.Sql
{
    [TestClass]
    public class SqlConnectionManagerTests : UnitTest
    {
        #region Ctor
        
        [TestMethod]
        public void Ctor_WithConnectionStringWorks ()
        {
            var expectedString = "Server=server1;Database=DB1";

            var target = new SqlConnectionManager(expectedString);

            target.ConnectionString.Should().Be(expectedString);
        }

        [TestMethod]
        public void Ctor_WithConnectionStringNullFails ()
        {
            Action a = () => new SqlConnectionManager(null);

            a.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Ctor_WithConnectionStringEmptyFails ()
        {
            Action a = () => new SqlConnectionManager("");

            a.Should().Throw<ArgumentException>();
        }
        #endregion
    }
}
