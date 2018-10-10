using System;
using System.Data.Common;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Data.Common;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.Data.Common
{
    [TestClass]
    public class ConnectionManagerTests : UnitTest
    {
        #region Ctor

        [TestMethod]
        public void Ctor_Default ()
        {
            var target = new TestConnectionManager();

            target.ConnectionString.Should().BeEmpty();
        }

        [TestMethod]
        public void Ctor_WithConnectionStringWorks ()
        {
            var expectedString = "Server=server1;Database=DB1";

            var target = new TestConnectionManager(expectedString);

            target.ConnectionString.Should().Be(expectedString);
        }

        [TestMethod]
        public void Ctor_WithConnectionStringNullFails ()
        {
            Action a = () => new TestConnectionManager(null);

            a.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Ctor_WithConnectionStringEmptyFails ()
        {
            Action a = () => new TestConnectionManager("");

            a.Should().Throw<ArgumentException>();
        }
        #endregion

        #region ConnectionString

        [TestMethod]
        public void ConnectionString_SetToNullThrows ()
        {
            var target = new TestConnectionManager();
            Action a = () => target.ConnectionString = null;

            a.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConnectionString_SetToEmptyThrows ()
        {
            var target = new TestConnectionManager();
            Action a = () => target.ConnectionString = "";

            a.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void ConnectionString_WithConnectionStringWorks ()
        {
            var expected = "Server=server2;Dabase=db2";

            var target = new TestConnectionManager();
            target.ConnectionString = expected;

            target.ConnectionString.Should().Be(expected);
        }
        #endregion

        #region Private Members

        private sealed class TestConnectionManager : ConnectionManager
        {
            public TestConnectionManager ()
            {
            }

#pragma warning disable 618        
            public TestConnectionManager ( string connectionStringOrName ) : base(connectionStringOrName)
            {
            }
#pragma warning disable 618

            protected override DbCommand CreateCommandBase ( DataCommand command ) => throw new NotImplementedException();
            protected override DbConnection CreateConnectionBase ( string connectionString ) => throw new NotImplementedException();
            protected override DbDataAdapter CreateDataAdapterBase () => throw new NotImplementedException();
        }
        #endregion
    }
}
