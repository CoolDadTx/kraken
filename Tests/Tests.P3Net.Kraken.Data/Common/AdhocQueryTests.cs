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
    public class AdhocQueryTests : UnitTest
    {
        [TestMethod]
        public void Ctor_AdhocQuery_CommandTextValid ()
        {            
            //Act
            var actual = new AdhocQuery("SELECT * FROM Tables");

            //Assert
            actual.CommandText.Should().Be("SELECT * FROM Tables");
            actual.CommandType.Should().Be(CommandType.Text);
        }

        [TestMethod]
        public void Ctor_AdhocQueryCommandTextIsNull ()
        {
            Action action = () => new AdhocQuery(null);

            action.ShouldThrowArgumentNullException();
        }

        [TestMethod]
        public void Ctor_AdhocQueryCommandTextIsEmpty ()
        {
            Action action = () => new AdhocQuery("");

            action.ShouldThrowArgumentException();
        }
    }
}
