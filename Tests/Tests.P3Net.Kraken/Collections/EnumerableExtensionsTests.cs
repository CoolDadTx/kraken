using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Collections;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.Collections
{
    [TestClass]
    public class EnumerableExtensionsTests : UnitTest
    {
#pragma warning disable 618
        #region ForEach

        [TestMethod]
        public void ForEach_SimpleAction ()
        {
            var actual = new Collection<int>();
            var target = new Collection<int>() { 1, 2, 3, 4 };

            //Act
            target.ForEach(x => actual.Add(x));

            //Assert
            actual.Should().BeEquivalentTo(target);
        }

        [TestMethod]
        public void ForEach_ActionIsNull ()
        {
            var target = new Collection<int>() { 1, 2, 3, 4 };

            //Act
            Action action = () => target.ForEach(null);

            action.ShouldThrowArgumentNullException();
        }
        #endregion

        #region ForEachIf

        [TestMethod]
        public void ForEachIf_SimpleAction ()
        {
            var actual = new Collection<int>();
            var target = new Collection<int>() { 1, 2, 3, 4 };
            var expected = new Collection<int>() { 2, 4 };

            //Act
            target.ForEachIf(x => actual.Add(x), _ => _ % 2 == 0);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void ForEachIf_ActionIsNull ()
        {
            var target = new Collection<int>() { 1, 2, 3, 4 };

            //Act
            Action action = () => target.ForEachIf(null, _ => true);

            action.ShouldThrowArgumentNullException();
        }

        [TestMethod]
        public void ForEachIf_PredicateIsNull ()
        {
            var target = new Collection<int>() { 1, 2, 3, 4 };

            //Act
            Action action = () => target.ForEachIf(_ => { }, null);

            action.ShouldThrowArgumentNullException();
        }
        #endregion
#pragma warning restore 618

        #region GetValueOrEmpty

        [TestMethod]
        public void GetValueOrEmpty_HasItems ()
        {
            var expected = new List<int>() { 1, 2, 3 };

            //Act
            var actual = expected.GetValueOrEmpty();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void GetValueOrEmpty_IsEmpty ()
        {
            var expected = new List<int>();

            //Act
            var actual = expected.GetValueOrEmpty();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void GetValueOrEmpty_IsNull ()
        {
            IEnumerable<int> target = null;

            //Act
            var actual = target.GetValueOrEmpty();

            //Assert
            actual.Should().BeEmpty();
        }
        #endregion

        #region IsNullOrEmpty

        [TestMethod]
        public void IsNullOrEmpty_ListHasItems ( )
        {
            var target = new Collection<int>() { 1 };

            //Act
            var actual = target.IsNullOrEmpty();

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsNullOrEmpty_ListIsEmpty ()
        {
            var target = new Collection<int>();

            //Act
            var actual = target.IsNullOrEmpty();

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsNullOrEmpty_ListIsNull ()
        {
            List<int> target = null;

            //Act
            var actual = target.IsNullOrEmpty();

            //Assert
            actual.Should().BeTrue();
        }
        #endregion

        #region OrderThenBy

        [TestMethod]
        public void OrderThenBy_NotOrdered ()
        {
            var target = new[] { 3, 5, 1, 2, 8 };

            var actual = target.OrderThenBy(v => v);

            actual.Should().BeInAscendingOrder();
        }

        [TestMethod]
        public void OrderThenBy_Ordered ()
        {
            var target = new[]
            {
                new OrderTestData() { Field1 = 1, Field2 = 3 },
                new OrderTestData() { Field1 = 3, Field2 = 5 },
                new OrderTestData() { Field1 = 5, Field2 = 1 },
                new OrderTestData() { Field1 = 2, Field2 = 7 },
                new OrderTestData() { Field1 = 3, Field2 = 2 },
            };
            var expected = target.OrderBy(v => v.Field1).ThenBy(v => v.Field2);

            var actual = target.OrderBy(v => v.Field1).OrderThenBy(v => v.Field2);

            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod]
        public void OrderThenByDescending_NotOrdered ()
        {
            var target = new[] { 3, 5, 1, 2, 8 };

            var actual = target.OrderThenByDescending(v => v);

            actual.Should().BeInDescendingOrder();
        }

        [TestMethod]
        public void OrderThenByDescending_Ordered ()
        {
            var target = new[]
            {
                new OrderTestData() { Field1 = 1, Field2 = 3 },
                new OrderTestData() { Field1 = 3, Field2 = 5 },
                new OrderTestData() { Field1 = 5, Field2 = 1 },
                new OrderTestData() { Field1 = 2, Field2 = 7 },
                new OrderTestData() { Field1 = 3, Field2 = 2 },
            };
            var expected = target.OrderBy(v => v.Field1).ThenByDescending(v => v.Field2);

            var actual = target.OrderBy(v => v.Field1).OrderThenByDescending(v => v.Field2);

            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod]
        public void OrderThenByDirectionAscending_NotOrdered ()
        {
            var target = new[] { 3, 5, 1, 2, 8 };

            var actual = target.OrderThenByDirection(v => v, false);

            actual.Should().BeInAscendingOrder();
        }

        [TestMethod]
        public void OrderThenByDirectionAscending_Ordered ()
        {
            var target = new[]
            {
                new OrderTestData() { Field1 = 1, Field2 = 3 },
                new OrderTestData() { Field1 = 3, Field2 = 5 },
                new OrderTestData() { Field1 = 5, Field2 = 1 },
                new OrderTestData() { Field1 = 2, Field2 = 7 },
                new OrderTestData() { Field1 = 3, Field2 = 2 },
            };
            var expected = target.OrderBy(v => v.Field1).ThenBy(v => v.Field2);

            var actual = target.OrderBy(v => v.Field1).OrderThenByDirection(v => v.Field2, false);

            actual.ShouldAllBeEquivalentTo(expected);
        }

        [TestMethod]
        public void OrderThenByDirectionDescending_NotOrdered ()
        {
            var target = new[] { 3, 5, 1, 2, 8 };

            var actual = target.OrderThenByDirection(v => v, true);

            actual.Should().BeInDescendingOrder();
        }

        [TestMethod]
        public void OrderThenByDirectionDescending_Ordered ()
        {
            var target = new[]
            {
                new OrderTestData() { Field1 = 1, Field2 = 3 },
                new OrderTestData() { Field1 = 3, Field2 = 5 },
                new OrderTestData() { Field1 = 5, Field2 = 1 },
                new OrderTestData() { Field1 = 2, Field2 = 7 },
                new OrderTestData() { Field1 = 3, Field2 = 2 },
            };
            var expected = target.OrderBy(v => v.Field1).ThenByDescending(v => v.Field2);

            var actual = target.OrderBy(v => v.Field1).OrderThenByDirection(v => v.Field2, true);

            actual.ShouldAllBeEquivalentTo(expected);
        }
        #endregion

#pragma warning disable 618    
        #region RemoveIf

        [TestMethod]
        public void RemoveIf_Some ()
        {
            var target = new int[] { 1, 2, 3, 4, 5 };
            var expected = new int[] { 1, 3, 5 };

            var actual = target.RemoveIf(x => x % 2 == 0);

            actual.Should().HaveCount(expected.Count());
            actual.Should().Contain(expected);            
        }

        [TestMethod]
        public void RemoveIf_All ()
        {
            var target = new int[] { 1, 2, 3, 4, 5 };
            
            var actual = target.RemoveIf(x => true);

            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void RemoveIf_SourceIsEmpty ()
        {
            var target = new int[0];

            var actual = target.RemoveIf(x => false);

            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void RemoveIf_ConditionIsNull ()
        {
            Action action = () => new int[0].RemoveIf(null);

            action.ShouldThrowArgumentNullException();
        }

        [TestMethod]
        public void RemoveIf_RemoveNulls ()
        {
            var target = new string[] { "One", null, "Two", null, "Three" };
            var expected = new string[] { "One", "Two", "Three" };

            var actual = target.RemoveIf(x => x == null);

            actual.Should().HaveCount(expected.Count());
            actual.Should().Contain(expected);
        }

        [TestMethod]
        public void RemoveIf_SourceIsNull ()
        {
            IEnumerable<string> target = null;

            var actual = target.RemoveIf(x => false);

            actual.Should().BeEmpty();
        }
        #endregion
#pragma warning restore 618

        #region Private Members

        private sealed class OrderTestData
        {
            public int Field1 { get; set; }
            public int Field2 { get; set; }
        }
        #endregion
    }
}
