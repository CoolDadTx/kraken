/*
 * Copyright (c) 2007 by Michael L. Taylor
 * All rights reserved.
 */
#region Imports

using System;
using System.Collections.Concurrent;
using System.Diagnostics;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken.ComponentModel;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken.ComponentModel
{
    [TestClass]
    public class DisposableObjectWithFinalizerTests : UnitTest
    {
        #region Tests

        [TestMethod]
        public void Dispose_WithUsingWasNotFinalized ()
        {
            //Act
            var id = Guid.NewGuid();
            var actual = new DisposableData();
            m_tracker.TryAdd(id, actual);
            using (var target = new TestDisposableObjectWithFinalizer(id))
            {
            };
            
            //Assert
            actual.WasDisposed.Should().BeTrue();
            actual.WasFinalized.Should().BeFalse();
        }

        [TestMethod]
        public void Dispose_WithExplicitCallWasNotFinalized ()
        {
            //Arrange
            var id = Guid.NewGuid();
            var actual = new DisposableData();
            m_tracker.TryAdd(id, actual);

            //Act
            var target = new TestDisposableObjectWithFinalizer(id);
            target.Dispose();

            //Assert
            actual.WasDisposed.Should().BeTrue();
            actual.WasFinalized.Should().BeFalse();
        }

        [TestMethod]
        public void Dispose_NoDisposeWasFinalized ()
        {
            //Arrange
            var id = Guid.NewGuid();
            var actual = new DisposableData();
            m_tracker.TryAdd(id, actual);

            //Act
            {
                var target = new TestDisposableObjectWithFinalizer(id);
                target = null;
            };
            GC.Collect();            
            GC.WaitForPendingFinalizers();
          
            //Assert            
            actual.WasDisposed.Should().BeFalse("Was disposed");
            actual.WasFinalized.Should().BeTrue("Not finalized");
        }
        #endregion

        #region Private Members

        private sealed class DisposableData
        {
            public bool WasDisposed { get; set; }
            public bool WasFinalized { get; set; }
        }

        private class TestDisposableObjectWithFinalizer : DisposableObjectWithFinalizer
        {
            public TestDisposableObjectWithFinalizer ( Guid id )
            {
                Id = id;
            }

            public Guid Id { get; private set; }

            protected override void Dispose ( bool disposing )
            {
                base.Dispose(disposing);

                DisposableData data;
                m_tracker.TryGetValue(Id, out data);

                if (disposing)
                    data.WasDisposed = true;
                else
                    data.WasFinalized = true;
            }
        }

        private static ConcurrentDictionary<Guid, DisposableData> m_tracker = new ConcurrentDictionary<Guid,DisposableData>();
        #endregion
    }
}

