using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Data.Common;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.Data.Common
{
    [TestClass]
    public class OutputParameterTTests : UnitTest
    {
        #region Ctor
        
        [TestMethod]
        public void Ctor_WithName ()
        {
            var target = new OutputParameter<int>("@p1");

            //Assert
            target.Name.Should().Be("@p1");
            target.DbType.Should().Be(DbType.Int32);
        }

        [TestMethod]
        public void Ctor_NameIsEmpty ()
        {
            Action action = () => new OutputParameter<int>("");

            action.ShouldThrowArgumentException();
        }

        [TestMethod]
        public void Ctor_NameIsNull ()
        {
            Action action = () => new OutputParameter<int>(null);

            action.ShouldThrowArgumentNullException();
        }

        [TestMethod]
        public void Ctor_WithUnknownType ()
        {
            var target = new OutputParameter<Tuple<string>>("@p1");

            //Assert
            target.DbType.Should().Be(DbType.Object);
        }
        #endregion

        #region GetValue

        [TestMethod]
        public void GetValue_IsValid ()
        {
            var expected = 40;

            var target = new OutputParameter<int>("@in1");
            target.Value = expected;
            var actual = target.GetValue();
                                
            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetValue_IsDBNull ()
        {
            var target = new OutputParameter<int>("@in1");
            target.Value = DBNull.Value;
            var actual = target.GetValue();

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void GetValue_IsNull ()
        {
            var target = new OutputParameter<int>("@in1");
            var actual = target.GetValue();

            //Assert
            actual.Should().Be(0);
        }
        #endregion
    }
}
