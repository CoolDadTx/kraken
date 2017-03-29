#region Imports

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Data.Common;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken.Data.Common
{
    [TestClass]
    public class DataParameterCollectionTests : UnitTest
    {
        #region Ctor

        [TestMethod]
        public void Ctor_Empty ()
        {
            //Act
            var target = new DataParameterCollection();

            //Assert
            target.Should().BeEmpty();
        }

        [TestMethod]
        public void Ctor_WithNull ()
        {
            //Act
            var target = new DataParameterCollection(null);

            //Assert
            target.Should().BeEmpty();
        }

        [TestMethod]
        public void Ctor_WithParameters ()
        {
            var expected = new List<DataParameter>() {
                    new DataParameter("in1", DbType.Int32),
                    new DataParameter("out1", DbType.String)
            };

            //Act
            var target = new DataParameterCollection(expected);

            //Assert
            target.Should().ContainInOrder(expected);
        }
        #endregion

        #region Add

        [TestMethod]
        public void Add_Isvalid ()
        {
            var target = new DataParameterCollection();
            var expected = new DataParameter("p1", DbType.Int32);

            //Act
            target.Add(expected);
            var actual = target[expected.Name];

            //Assert            
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Add_NullValue ()
        {
            var target = new DataParameterCollection();
   
            Action action = () => target.Add(null);

            action.ShouldThrowArgumentNullException();
        }
        #endregion

        #region Set
        
        [TestMethod]        
        public void Set_NewValue ()
        {
            var target = new DataParameterCollection();
            var expected = new DataParameter("p2", DbType.String);
            target.Add(new DataParameter("p1", DbType.Int32));

            //Act
            target[0] = expected;

            //Assert
            target[0].Should().Be(expected);
        }

        [TestMethod]
        public void Set_SetNullValue ()
        {
            var target = new DataParameterCollection();
            target.Add(new DataParameter("p1", DbType.Int32));

            Action action = () => target[0] = null;

            action.ShouldThrowArgumentNullException();
        }
        #endregion
    }
}
