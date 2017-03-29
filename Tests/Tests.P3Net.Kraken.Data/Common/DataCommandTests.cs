#region Imports

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using P3Net.Kraken.Data.Common;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken.Data.Common
{
    [TestClass]
    public class DataCommandTests : UnitTest
    {
        #region Ctor

        [TestMethod]
        public void Ctor_CommandTextValid ()
        {
            //Act
            var target = new DataCommand("sproc", CommandType.StoredProcedure);

            //Assert
            target.CommandText.Should().Be("sproc");
            target.CommandType.Should().Be(CommandType.StoredProcedure);
        }

        [TestMethod]
        public void Ctor_CommandTextIsNull ()
        {
            Action action = () => new DataCommand(null, CommandType.StoredProcedure);

            action.ShouldThrowArgumentNullException();
        }

        [TestMethod]
        public void Ctor_CommandTextIsEmpty ()
        {
            Action action = () => new DataCommand("", CommandType.Text);

            action.ShouldThrowArgumentException();
        }
        #endregion

        #region CommandTimeout

        [TestMethod]
        public void CommandTimeout_IsValid ()
        {
            var expected = new TimeSpan(1, 2, 3);

            //Act
            var target = new DataCommand("sproc", CommandType.StoredProcedure);
            target.CommandTimeout = expected;

            //Assert
            target.CommandTimeout.Should().Be(expected);
        }

        [TestMethod]
        public void CommandTimeout_IsNegative ()
        {
            var target = new DataCommand("sproc", CommandType.StoredProcedure);
            Action action = () => target.CommandTimeout = new TimeSpan(0, 0, -1);

            action.ShouldThrowArgumentOutOfRangeException();
        }
        #endregion

        #region ToString

        [TestMethod]
        public void ToString_IsCorrect ()
        {
            var expected = "SELECT * FROM Tables";
            var target = new DataCommand(expected, CommandType.Text);

            //Act
            var actual = target.ToString();

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

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

            //Act
            target.WithParameters(expected);

            //Assert
            target.Parameters.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void WithParameters_NoParameters ()
        {
            var target = new DataCommand("SELECT * FROM Tables", CommandType.Text);

            //Act
            target.WithParameters();

            //Assert
            target.Parameters.Should().BeEmpty();
        }
        #endregion
    }
}
