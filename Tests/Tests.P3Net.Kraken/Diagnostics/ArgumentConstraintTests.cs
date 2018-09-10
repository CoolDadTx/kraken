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
    public class ArgumentConstraintTests
    {
        #region Ctor
        
        [TestMethod]
        public void Ctor_WithArgument ()
        {
            var expected = new Argument<int>("Test", 10);

            var target = new ArgumentConstraint<int>(expected);

            target.Argument.Should().Be(expected);
        }

        [TestMethod]        
        public void Ctor_WithNullArgument ()
        {
            Action work = () => new ArgumentConstraint<int>(null);

            work.Should().Throw<ArgumentNullException>();
        }
        #endregion

        #region Is

        [TestMethod]
        public void Is_IsTrue ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("Test", 10));

            target.Is(x => x > 0);
        }

        [TestMethod]
        public void Is_IsFalse ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("Test", 10));

            Action work = () => target.Is(x => x < 0);

            work.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void Is_ConditionIsNull ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("Test", 10));

            Action work = () => target.Is(null);

            work.Should().Throw<ArgumentNullException>();
        }
        
        [TestMethod]
        public void Is_WithMessage_IsFalse ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("Test", 10));

            Action work = () => target.Is(x => x < 0, "Testing {0} {1}", 1, 2);

            work.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void Is_WithMessage_ConditionIsNull ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("Test", 10));

            Action work = () => target.Is(null, "Testing {0} {1}", 1, 2);

            work.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Is_WithMessage_IsTrue ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("Test", 10));

            target.Is(x => x > 0, "Testing {0} {1}", 1, 2);            
        }
        #endregion

        #region IsNot

        [TestMethod]
        public void IsNot_IsTrue ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("Test", 10));

            Action work = () => target.IsNot(x => x > 0);

            work.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void IsNot_IsFalse ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("Test", 10));

            target.IsNot(x => x < 0);
        }

        [TestMethod]        
        public void IsNot_ConditionIsNull ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("Test", 10));

            Action work = () => target.IsNot(null);

            work.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void IsNot_WithMessage_IsFalse ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("Test", 10));

            Action work = () => target.IsNot(x => x > 0, "Testing {0} {1}", 1, 2);

            work.Should().Throw<ArgumentException>().ContainingMessage("Testing 1 2");
        }

        [TestMethod]        
        public void IsNot_WithMessage_ConditionIsNull ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("Test", 10));

            Action work = () => target.IsNot(null, "Testing {0} {1}", 1, 2);

            work.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void IsNot_WithMessage_IsTrue ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("Test", 10));

            target.IsNot(x => x < 0, "Testing {0} {1}", 1, 2);            
        }
        #endregion
    }
}
