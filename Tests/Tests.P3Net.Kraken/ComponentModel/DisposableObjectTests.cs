/*
 * Copyright (c) 2007 by Michael L. Taylor
 * All rights reserved.
 */
#region Imports

using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken.ComponentModel;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken.ComponentModel
{
    [TestClass]
    public class DisposableObjectTests : UnitTest
    {
        #region Tests

        [TestMethod]
        public void Dispose_CalledWithUsing ()
        {
            //Act
            TestDisposableObject target = null;
            using (target = new TestDisposableObject())
            {
            };

            //Assert            
            target.IsDisposed.Should().BeTrue();
        }

        [TestMethod]
        public void Dispose_CalledExplicitly ()
        {
            //Act
            TestDisposableObject target = new TestDisposableObject();
            target.Dispose();

            //Assert            
            target.IsDisposed.Should().BeTrue();
        }

        [TestMethod]
        public void Dispose_CalledExplicitlyAndWithUsing ()
        {
            //Act
            TestDisposableObject target = null;
            using (target = new TestDisposableObject())
            {
                target.Dispose();
            };

            //Assert            
            target.IsDisposed.Should().BeTrue();
            target.DisposeCount.Should().Be(1);
        }
        #endregion

        #region Private Members

        private class TestDisposableObject : DisposableObject
        {
            public int DisposeCount { get; private set; }            

            protected override void Dispose ( bool disposing )
            {
                DisposeCount += 1;

                base.Dispose(disposing);
            }
        }		
        #endregion
    }
}

