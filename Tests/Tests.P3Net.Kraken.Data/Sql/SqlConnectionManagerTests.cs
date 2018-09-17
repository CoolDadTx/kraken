using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken;
using P3Net.Kraken.Data.Configuration;
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

        [TestMethod]
        public void Ctor_WithConnectionStringNameWorks ()
        {
            var connName = "Name1";
            var expectedString = "Server=server1;Database=db1";

            var mockProvider = new MemoryDataConfigurationProvider();
            mockProvider.ConnectionStrings[connName] = expectedString;

            var target = new SqlConnectionManager(connName, mockProvider);

            target.ConnectionString.Should().Be(expectedString);
        }

        [TestMethod]
        public void Ctor_WithConnectionStringNameDoesNotExistFails ()
        {
            var connName = "Name1";

            Action a = () => new SqlConnectionManager(connName);

            a.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void Ctor_WithConnectionStringNameAndIsEmptyFails ()
        {
            var connName = "Name1";

            var mockProvider = new MemoryDataConfigurationProvider();
            mockProvider.ConnectionStrings[connName] = "";

            Action a = () => new SqlConnectionManager(connName, mockProvider);

            a.Should().Throw<Exception>();
        }
        #endregion
    }
}
