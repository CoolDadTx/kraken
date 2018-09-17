using System;
using System.Data.Common;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Data.Common;
using P3Net.Kraken.Data.Configuration;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.Data.Common
{
    [TestClass]
    public class ConnectionManagerTests : UnitTest
    {
        #region Ctor
        
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

        [TestMethod]
        public void Ctor_WithConnectionStringNameWorks ()
        {
            var connName = "Name1";
            var expectedString = "Server=server1;Database=db1";

            var mockProvider = new MemoryDataConfigurationProvider();
            mockProvider.ConnectionStrings[connName] = expectedString;

            var target = new TestConnectionManager(connName, mockProvider);

            target.ConnectionString.Should().Be(expectedString);
        }

        [TestMethod]
        public void Ctor_WithConnectionStringNameDoesNotExistFails ()
        {
            var connName = "Name1";

            Action a = () => new TestConnectionManager(connName);

            a.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void Ctor_WithConnectionStringNameAndIsEmptyFails ()
        {
            var connName = "Name1";

            var mockProvider = new MemoryDataConfigurationProvider();
            mockProvider.ConnectionStrings[connName] = "";

            Action a = () => new TestConnectionManager(connName, mockProvider);

            a.Should().Throw<Exception>();
        }
        #endregion

        #region ConnectionString

        [TestMethod]
        public void ConnectionString_SetToNullThrows ()
        {
            var target = new TestConnectionManager("Server=server1;Dabase=DB1");
            Action a = () => target.SetConnectionString(null);

            a.Should().Throw<ArgumentNullException>();            
        }

        [TestMethod]
        public void ConnectionString_SetToEmptyThrows ()
        {
            var target = new TestConnectionManager("Server=server1;Dabase=DB1");
            Action a = () => target.SetConnectionString("");

            a.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void ConnectionString_WithConnectionStringWorks ()
        {
            var expected = "Server=server2;Dabase=db2";

            var target = new TestConnectionManager("Server=server1;Dabase=DB1");
            target.SetConnectionString(expected);

            target.ConnectionString.Should().Be(expected);
        }

        [TestMethod]
        public void ConnectionString_WithConnectionStringNameWorks ()
        {
            var expected = "Server=server2;Dabase=db2";
            var connName = "conn1";

            var provider = new MemoryDataConfigurationProvider();
            provider.ConnectionStrings[connName] = expected;

            var target = new TestConnectionManager("Server=server1;Dabase=DB1", provider);
            target.SetConnectionString(connName);

            target.ConnectionString.Should().Be(expected);
        }
        #endregion

        #region Private Members

        private sealed class TestConnectionManager : ConnectionManager
        {
            public TestConnectionManager ( string connectionStringOrName ) : this(connectionStringOrName, null)
            {
            }

            public TestConnectionManager ( string connectionStringOrName, IDataConfigurationProvider configurationProvider ) 
                                    : base(connectionStringOrName, configurationProvider ?? new MemoryDataConfigurationProvider())
            {
            }

            public void SetConnectionString ( string value ) => ConnectionString = value;

            protected override DbCommand CreateCommandBase ( DataCommand command ) => throw new NotImplementedException();
            protected override DbConnection CreateConnectionBase ( string connectionString ) => throw new NotImplementedException();
            protected override DbDataAdapter CreateDataAdapterBase () => throw new NotImplementedException();            
        }        
        #endregion
    }
}
