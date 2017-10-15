using System;
using System.Data;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Data.Common;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.Data.Common
{
    [TestClass]
    public class DataCommandExtensionsTests : UnitTest
    {
        #region WithParameters

        [TestMethod]
        public void WithParameters_SomeParameters ()
        {
            var target = new DataCommand("sproc", CommandType.StoredProcedure);
            var expected = new DataParameter[] {
                new DataParameter("@in1", DbType.Int32),
                new DataParameter("@out1", DbType.String),
                new DataParameter("@inout1", DbType.DateTime)
            };

            DataCommandExtensions.WithParameters(target, expected);

            target.Parameters.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void WithParameters_NoParameters ()
        {
            var target = new DataCommand("SELECT * FROM Tables", CommandType.Text);

            DataCommandExtensions.WithParameters(target);

            target.Parameters.Should().BeEmpty();
        }
        #endregion
    }
}
