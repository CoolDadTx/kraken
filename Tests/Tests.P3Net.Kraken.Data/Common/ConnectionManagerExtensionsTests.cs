using System;
using System.Data.Common;
using FluentAssertions;

using P3Net.Kraken.Data.Common;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.P3Net.Kraken.Data.Common
{
    [TestClass]
    public class ConnectionManagerExtensionsTests
    {
        #region WithConnectionString

        [TestMethod]
        public void WithConnectionString_IsValid ()
        {
            var expected = @"Server=(localdb);Database=Master;Integrated Security=SSPI";
            var target = new TestConnectionManager();

            target.WithConnectionString(expected);

            target.ConnectionString.Should().Be(expected);
        }

        [TestMethod]
        public void WithConnectionString_IsEmpty ()
        {
            var target = new TestConnectionManager();

            Action action = () => target.WithConnectionString("");

            action.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void WithConnectionString_IsNull ()
        {
            var target = new TestConnectionManager();

            Action action = () => target.WithConnectionString(null);

            action.Should().Throw<ArgumentException>();
        }
        #endregion

        #region Private Members

        private sealed class TestConnectionManager : ConnectionManager
        {
            protected override DbCommand CreateCommandBase ( DataCommand command ) => throw new NotImplementedException();
            protected override DbConnection CreateConnectionBase ( string connectionString ) => throw new NotImplementedException();
            protected override DbDataAdapter CreateDataAdapterBase () => throw new NotImplementedException();
        }
        #endregion
    }
}
