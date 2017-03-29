/*
 * Copyright © 2008 Michael Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Collections;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken.Collections
{
    [TestClass]
    public class CollectionExtensionsTests : UnitTest
    {
        #region AddRange

        [TestMethod]
        public void AddRange_HasSomeItems ( )
        {
            var expected = new Collection<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var actual = new Collection<int>() { 1, 2, 3, 4, 5, 6 };

            //Act
            actual.AddRange(new int[] { 7, 8, 9 });

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void AddRange_NewItemsEmpty ()
        {
            var expected = new Collection<int>() { 1, 2, 3, 4, 5, 6 };
            var actual = new Collection<int>() { 1, 2, 3, 4, 5, 6 };

            //Act
            actual.AddRange(new int[] { });

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void AddRange_NewItemsNull ()
        {
            var actual = new Collection<int>() { 1, 2, 3, 4, 5, 6 };

            //Act
            Action action = () => actual.AddRange(null);

            action.ShouldThrowArgumentNullException();
        }
        #endregion
    }
}
