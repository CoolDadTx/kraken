/*
 * Copyright © 2013 Federation of State Medical Boards
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Diagnostics;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.Diagnostics
{
    [TestClass]
    public class ComparableArgumentConstraintExtensionsTests : UnitTest
    {
        #region IsBetween
        
        [TestMethod]
        public void IsBetween_IsTrue ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 10));

            target.IsBetween(1, 10);
        }

        [TestMethod]
        public void IsBetween_IsTooSmall ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 19));

            Action work = () => target.IsBetween(20, 100);

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsBetween_IsTooLarge ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 10));

            Action work = () => target.IsBetween(1, 9);

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsBetween_WithMessage ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 10));

            Action work = () => target.IsBetween(20, 100, "Test {0} {1}", 20, 100);

            work.Should().Throw<ArgumentOutOfRangeException>().ContainingMessage("Test 20 100");
        }
        #endregion

        #region IsBetweenExclusive

        [TestMethod]
        public void IsBetweenExclusive_IsTrue ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 10));

            target.IsBetweenExclusive(1, 11);
        }

        [TestMethod]
        public void IsBetweenExclusive_IsTooSmall ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 10));

            Action work = () => target.IsBetweenExclusive(10, 100);

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsBetweenExclusive_IsTooLarge ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 10));

            Action work = () => target.IsBetweenExclusive(1, 10);

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsBetweenExclusive_WithMessage ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 10));

            Action work = () => target.IsBetweenExclusive(20, 100, "Test {0} {1}", 20, 100);

            work.Should().Throw<ArgumentOutOfRangeException>().ContainingMessage("Test 20 100");
        }
        #endregion

        #region IsEqualTo

        [TestMethod]
        public void IsEqualTo_IsTrue ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 10));

            target.IsEqualTo(10);
        }

        [TestMethod]
        public void IsEqualTo_IsFalse ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 19));

            Action work = () => target.IsEqualTo(20);

            work.Should().Throw<ArgumentOutOfRangeException>();
        }
        
        [TestMethod]
        public void IsEqualTo_WithMessage ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 10));

            Action work = () => target.IsEqualTo(20, "Test {0}", 20);

            work.Should().Throw<ArgumentOutOfRangeException>().ContainingMessage("Test 20");
        }
        #endregion

        #region IsGreaterThan

        [TestMethod]
        public void IsGreaterThan_IsTrue ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 10));

            target.IsGreaterThan(9);
        }

        [TestMethod]
        public void IsGreaterThan_IsFalse ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 20));

            Action work = () => target.IsGreaterThan(20);

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsGreaterThan_WithMessage ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 10));

            Action work = () => target.IsGreaterThan(20, "Test {0}", 20);

            work.Should().Throw<ArgumentOutOfRangeException>().ContainingMessage("Test 20");
        }
        #endregion

        #region IsGreaterThanOrEqualTo

        [TestMethod]
        public void IsGreaterThanOrEqualTo_IsTrue ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 10));

            target.IsGreaterThanOrEqualTo(9);
        }

        [TestMethod]
        public void IsGreaterThanOrEqualTo_IsFalse ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 20));

            Action work = () => target.IsGreaterThanOrEqualTo(25);

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsGreaterThanOrEqualTo_IsEqual ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 20));

            target.IsGreaterThanOrEqualTo(20);            
        }

        [TestMethod]
        public void IsGreaterThanOrEqualTo_WithMessage ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 10));

            Action work = () => target.IsGreaterThanOrEqualTo(20, "Test {0}", 20);

            work.Should().Throw<ArgumentOutOfRangeException>().ContainingMessage("Test 20");
        }
        #endregion

        #region IsLessThan

        [TestMethod]
        public void IsLessThan_IsTrue ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 10));

            target.IsLessThan(11);
        }

        [TestMethod]
        public void IsLessThan_IsFalse ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 20));

            Action work = () => target.IsLessThan(20);

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsLessThan_WithMessage ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 30));

            Action work = () => target.IsLessThan(20, "Test {0}", 20);

            work.Should().Throw<ArgumentOutOfRangeException>().ContainingMessage("Test 20");
        }
        #endregion

        #region IsLessThanOrEqualTo

        [TestMethod]
        public void IsLessThanOrEqualTo_IsTrue ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 5));

            target.IsLessThanOrEqualTo(10);
        }

        [TestMethod]
        public void IsLessThanOrEqualTo_IsFalse ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 20));

            Action work = () => target.IsLessThanOrEqualTo(19);

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsLessThanOrEqualTo_IsEqual ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 20));

            target.IsLessThanOrEqualTo(20);
        }

        [TestMethod]
        public void IsLessThanOrEqualTo_WithMessage ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 30));

            Action work = () => target.IsLessThanOrEqualTo(20, "Test {0}", 20);

            work.Should().Throw<ArgumentOutOfRangeException>().ContainingMessage("Test 20");
        }
        #endregion

        #region IsNotEqualTo

        [TestMethod]
        public void IsNotEqualTo_IsTrue ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 20));

            target.IsNotEqualTo(10);
        }

        [TestMethod]
        public void IsNotEqualTo_IsFalse ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 20));

            Action work = () => target.IsNotEqualTo(20);

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsNotEqualTo_WithMessage ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 20));

            Action work = () => target.IsNotEqualTo(20, "Test {0}", 20);

            work.Should().Throw<ArgumentOutOfRangeException>().ContainingMessage("Test 20");
        }
        #endregion
    }
}
