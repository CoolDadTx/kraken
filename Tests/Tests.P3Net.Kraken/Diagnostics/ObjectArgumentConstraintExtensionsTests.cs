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
    public class ObjectArgumentConstraintExtensionsTests : UnitTest
    {
        #region IsNotNull
        
        [TestMethod]
        public void IsNotNull_IsTrue ()
        {
            var target = new ArgumentConstraint<object>(new Argument<object>("a", "Hello"));

            target.IsNotNull();
        }

        [TestMethod]
        public void IsNotNull_IsFalse ()
        {
            var target = new ArgumentConstraint<object>(new Argument<object>("a", null));

            Action work = () => target.IsNotNull();

            work.ShouldThrowArgumentNullException();
        }

        [TestMethod]
        public void IsNotNull_IsFalse_WithMessage ()
        {
            var target = new ArgumentConstraint<object>(new Argument<object>("a", null));

            Action work = () => target.IsNotNull("Testing");

            work.ShouldThrowArgumentNullException().ContainingMessage("Testing");
        }

        [TestMethod]
        public void IsNotNull_IsTrue_WithMessage ()
        {
            var target = new ArgumentConstraint<object>(new Argument<object>("a", "Hello"));

            target.IsNotNull("Testing");
        }
        #endregion

        #region IsNull

        [TestMethod]
        public void IsNull_IsTrue ()
        {
            var target = new ArgumentConstraint<object>(new Argument<object>("a", null));

            target.IsNull();
        }

        [TestMethod]
        public void IsNull_IsFalse ()
        {
            var target = new ArgumentConstraint<object>(new Argument<object>("a", "Hello"));

            Action work = () => target.IsNull();

            work.ShouldThrowArgumentException();
        }

        [TestMethod]
        public void IsNull_IsFalse_WithMessage ()
        {
            var target = new ArgumentConstraint<object>(new Argument<object>("a", "Hello"));

            Action work = () => target.IsNull("Testing");

            work.ShouldThrowArgumentException().ContainingMessage("Testing");
        }
        #endregion
    }
}
