using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken.ComponentModel;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.ComponentModel
{
    [TestClass]
    public partial class DisposableCollectionTests : UnitTest
    {       
        #region Ctor

        [TestMethod]
        public void Ctor_DefaultWorks ()
        {
            //Act
            var target = new DisposableCollection();

            //Assert
            target.Should().BeEmpty();
        }

        [TestMethod]
        public void Ctor_ValidListWorks ()
        {
            var expected = new List<IDisposable>() {
                new TestDisposableObject(),
                new TestDisposableObject(),
                new TestDisposableObject()
            };

            //Act
            var target = new DisposableCollection(expected);

            //Assert
            target.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void Ctor_NullListIsEmpty ()
        {
            List<IDisposable> items = null;

            //Act
            var target = new DisposableCollection(items);

            //Assert
            target.Should().BeEmpty();
        }

        [TestMethod]
        public void Ctor_EmptyListIsEmpty ()
        {
            var expected = new List<IDisposable>();

            //Act
            var target = new DisposableCollection(expected);

            //Assert
            target.Should().BeEmpty();
        }

        [TestMethod]
        public void Ctor_ListWithNullsWorks ()
        {
            var expected = new List<IDisposable> {
                new TestDisposableObject(),
                null,
                new TestDisposableObject()
            };

            //Act
            var target = new DisposableCollection(expected);

            //Assert - Can't use Contains because it doesn't like nulls
            target[0].Should().Be(expected[0]);
            target[1].Should().BeNull();
            target[2].Should().Be(expected[2]);
        }
        #endregion
        
        #region Clear
        
        [TestMethod]
        public void Clear_ObjectsAreDisposed ()
        {
            var expected = new TestDisposableObject();
            var target = new DisposableCollection();
            target.Add(expected);

            //Act
            target.Clear();

            //Assert
            expected.IsDisposed.Should().BeTrue();
        }

        [TestMethod]
        public void Clear_ValidCollectionWithDisposedObjectsIsDisposed ()
        {
            var expected = new TestDisposableObject();
            var target = new DisposableCollection();
            target.Add(expected);

            //Act
            expected.Dispose();
            target.Clear();

            //Assert
            expected.IsDisposed.Should().BeTrue();
        }

        [TestMethod]
        public void Clear_ValidCollectionWithNoDisposeFlagIsNotDisposed ()
        {
            var expected = new TestDisposableObject();
            var target = new DisposableCollection();
            target.Add(expected);

            //Act
            target.Clear(false);

            //Assert
            expected.IsDisposed.Should().BeFalse();
        }
        #endregion

        #region CreateAndAdd

        [TestMethod]
        public void CreateAndAdd_WithValue ()
        {
            var expected = new TestDisposableObject();

            var target = new DisposableCollection();
            var actual = target.CreateAndAdd(expected);

            //Assert
            actual.Should().Be(expected);
            target.Should().HaveCount(1);
            target.Should().Contain(expected);
        }

        [TestMethod]
        public void CreateAndAdd_WithDelegate ()
        {
            var target = new DisposableCollection();
            var actual = target.CreateAndAdd(() => new TestDisposableObject());

            //Assert
            target.Should().HaveCount(1);
            target.Should().Contain(actual);
        }
        #endregion

        #region Detach

        [TestMethod]
        public void Detach_DoesNotDispose ()
        {
            var target = new DisposableCollection();
            var expected = new TestDisposableObject();
            target.Add(expected);

            //Act
            var actual = target.Detach(expected);

            //Assert
            actual.Should().BeTrue();
            expected.IsDisposed.Should().BeFalse();
        }
        #endregion

        #region Dispose

        [TestMethod]
        public void Dispose_UsingDisposesObjects ()
        {
            var expected = new TestDisposableObject();

            //Act
            using (var target = new DisposableCollection())
            {
                target.Add(expected);
            };

            //Assert
            expected.IsDisposed.Should().BeTrue();
        }
        
        [TestMethod]
        public void Dispose_DoubleDisposeWorks ()
        {
            var expected = new TestDisposableObject();

            //Act
            using (var target = new DisposableCollection())
            {
                target.Add(expected);

                target.Dispose();
            };

            //Assert
            expected.IsDisposed.Should().BeTrue();
        }
        #endregion
        
        #region Remove

        [TestMethod]
        public void Remove_DisposesOfItem ()
        {
            var target = new DisposableCollection();
            var item = new TestDisposableObject();

            //Act
            target.Add(item);
            var actual = target.Remove(item);

            //Assert
            actual.Should().BeTrue();
            item.IsDisposed.Should().BeTrue();
        }
        #endregion

        #region Set

        [TestMethod]
        public void ItemSet_DisposesOriginalItem ()
        {
            var target = new DisposableCollection();
            var originalItem = new TestDisposableObject();
            var newItem = new TestDisposableObject();

            //Act
            target.Add(originalItem);
            target[0] = newItem;

            //Assert            
            originalItem.IsDisposed.Should().BeTrue();
            newItem.IsDisposed.Should().BeFalse();
        }
        #endregion
     
        #region Private Members

        private class TestDisposableObject : DisposableObject
        {
            public TestDisposableObject ()
            {
                m_id = Interlocked.Increment(ref s_lastId);
            }

            public int DisposeCount { get; private set; }

            protected override void Dispose ( bool disposing )
            {
                DisposeCount += 1;

                base.Dispose(disposing);
            }

            public override bool Equals ( object obj )
            {
                return m_id == ((TestDisposableObject)obj).m_id;   
            }

            public override int GetHashCode ()
            {
                return m_id.GetHashCode();
            }

            private static int s_lastId = 0;

            private int m_id;
        }	
        #endregion
    }
}
