using System;
using System.Data;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Data.Common;
using P3Net.Kraken.UnitTesting;

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

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Ctor_CommandTextIsEmpty ()
        {
            Action action = () => new DataCommand("", CommandType.Text);

            action.Should().Throw<ArgumentException>();
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

            action.Should().Throw<ArgumentOutOfRangeException>();
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
    }
}
