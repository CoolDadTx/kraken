/*
 * Copyright © 2008 Michael Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Collections;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken.Collections
{
    [TestClass]
    public class ArrayExtensionsTests : UnitTest
    {
        #region GetValueOrEmpty

        [TestMethod]
        public void GetValueOrEmpty_HasItems ()
        {
            var expected = new int[] { 1, 2, 3 };

            //Act
            var actual = expected.GetValueOrEmpty();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void GetValueOrEmpty_IsEmpty ()
        {
            var expected = new int[0];

            //Act
            var actual = expected.GetValueOrEmpty();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void GetValueOrEmpty_IsNull ()
        {
            int[] target = null;

            //Act
            var actual = target.GetValueOrEmpty();

            //Assert
            actual.Should().BeEmpty();
        }        
        #endregion

        #region IndexOf

        [TestMethod]
        public void IndexOf_ItemExistsInMiddle ( )
        {
            //Act
            var actual = new int[] { 1, 2, 3, 4, 5 }.IndexOf(3);

            //Assert    
            actual.Should().Be(2);
        }

        [TestMethod]
        public void IndexOf_ItemExistsAtStart ()
        {
            //Act
            var actual = new int[] { 1, 2, 3, 4, 5 }.IndexOf(1);

            //Assert    
            actual.Should().Be(0);
        }

        [TestMethod]
        public void IndexOf_ItemExistsAtEnd ()
        {
            //Act
            var actual = new int[] { 1, 2, 3, 4, 5 }.IndexOf(5);

            //Assert    
            actual.Should().Be(4);
        }

        [TestMethod]
        public void IndexOf_ItemDoesNotExist ()
        {
            //Act
            var actual = new int[] { 1, 2, 3, 4, 5 }.IndexOf(10);

            //Assert    
            actual.Should().Be(-1);
        }
        #endregion
    }
}
