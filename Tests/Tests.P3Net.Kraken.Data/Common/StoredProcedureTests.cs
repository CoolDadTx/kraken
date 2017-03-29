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
    public class StoredProcedureTests : UnitTest
    {       
        [TestMethod]
        public void Ctor_CommandTextValid ()
        {
            //Act
            var actual = new StoredProcedure("sproc");

            //Assert
            actual.CommandText.Should().Be("sproc");
            actual.CommandType.Should().Be(CommandType.StoredProcedure);
        }

        [TestMethod]
        public void Ctor_CommandTextIsNull ()
        {
            Action action = () => new StoredProcedure(null);

            action.ShouldThrowArgumentNullException();
        }

        [TestMethod]
        public void Ctor_CommandTextIsEmpty ()
        {
            Action action = () => new StoredProcedure("");

            action.ShouldThrowArgumentException();
        }
    }
}
