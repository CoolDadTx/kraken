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
    public class FloatArgumentConstraintExtensionsTests : UnitTest
    {
        #region Float

        #region IsGreaterThanOrEqualToZero

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_Single_IsTrue ()
        {
            var target = new ArgumentConstraint<float>(new Argument<float>("a", 10));

            target.IsGreaterThanOrEqualToZero();
        }

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_Single_IsFalse ()
        {
            var target = new ArgumentConstraint<float>(new Argument<float>("a", -10));

            Action work = () => target.IsGreaterThanOrEqualToZero();

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_Single_IsZero ()
        {
            var target = new ArgumentConstraint<float>(new Argument<float>("a", 0));

            target.IsGreaterThanOrEqualToZero();
        }

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_Single_WithMessage ()
        {
            var target = new ArgumentConstraint<float>(new Argument<float>("a", -10));

            Action work = () => target.IsGreaterThanOrEqualToZero("Testing");

            work.Should().Throw<ArgumentOutOfRangeException>().ContainingMessage("Testing");
        }
        #endregion

        #region IsGreaterThanZero

        [TestMethod]
        public void IsGreaterThanZero_Single_IsTrue ()
        {
            var target = new ArgumentConstraint<float>(new Argument<float>("a", 10));

            target.IsGreaterThanZero();
        }

        [TestMethod]
        public void IsGreaterThanZero_Single_IsFalse ()
        {
            var target = new ArgumentConstraint<float>(new Argument<float>("a", -10));

            Action work = () => target.IsGreaterThanZero();

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsGreaterThanZero_Single_IsZero ()
        {
            var target = new ArgumentConstraint<float>(new Argument<float>("a", 0));

            Action work = () => target.IsGreaterThanZero();

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsGreaterThanZero_Single_WithMessage ()
        {
            var target = new ArgumentConstraint<float>(new Argument<float>("a", -10));

            Action work = () => target.IsGreaterThanZero("Testing");

            work.Should().Throw<ArgumentOutOfRangeException>().ContainingMessage("Testing");
        }
        #endregion

        #region IsLessThanOrEqualToZero

        [TestMethod]
        public void IsLessThanOrEqualToZero_Single_IsTrue ()
        {
            var target = new ArgumentConstraint<float>(new Argument<float>("a", -10));

            target.IsLessThanOrEqualToZero();
        }

        [TestMethod]
        public void IsLessThanOrEqualToZero_Single_IsFalse ()
        {
            var target = new ArgumentConstraint<float>(new Argument<float>("a", 10));

            Action work = () => target.IsLessThanOrEqualToZero();

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsLessThanOrEqualToZero_Single_IsZero ()
        {
            var target = new ArgumentConstraint<float>(new Argument<float>("a", 0));

            target.IsLessThanOrEqualToZero();
        }

        [TestMethod]
        public void IsLessThanOrEqualToZero_Single_WithMessage ()
        {
            var target = new ArgumentConstraint<float>(new Argument<float>("a", 10));

            Action work = () => target.IsLessThanOrEqualToZero("Testing");

            work.Should().Throw<ArgumentOutOfRangeException>().ContainingMessage("Testing");
        }
        #endregion

        #region IsLessThanZero

        [TestMethod]
        public void IsLessThanZero_Single_IsTrue ()
        {
            var target = new ArgumentConstraint<float>(new Argument<float>("a", -10));

            target.IsLessThanZero();
        }

        [TestMethod]
        public void IsLessThanZero_Single_IsFalse ()
        {
            var target = new ArgumentConstraint<float>(new Argument<float>("a", 10));

            Action work = () => target.IsLessThanZero();

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsLessThanZero_Single_IsZero ()
        {
            var target = new ArgumentConstraint<float>(new Argument<float>("a", 0));

            Action work = () => target.IsLessThanZero();

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsLessThanZero_Single_WithMessage ()
        {
            var target = new ArgumentConstraint<float>(new Argument<float>("a", 10));

            Action work = () => target.IsLessThanZero("Testing");

            work.Should().Throw<ArgumentOutOfRangeException>().ContainingMessage("Testing");
        }
        #endregion

        #region IsNotZero

        [TestMethod]
        public void IsNotZero_Single_IsTrue ()
        {
            var target = new ArgumentConstraint<float>(new Argument<float>("a", 10));

            target.IsNotZero();
        }

        [TestMethod]
        public void IsNotZero_Single_IsFalse ()
        {
            var target = new ArgumentConstraint<float>(new Argument<float>("a", 0));

            Action work = () => target.IsNotZero();

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsNotZero_Single_WithMessage ()
        {
            var target = new ArgumentConstraint<float>(new Argument<float>("a", 0));

            Action work = () => target.IsNotZero("Testing");

            work.Should().Throw<ArgumentOutOfRangeException>().ContainingMessage("Testing");
        }
        #endregion

        #region IsZero

        [TestMethod]
        public void IsZero_Single_IsTrue ()
        {
            var target = new ArgumentConstraint<float>(new Argument<float>("a", 0));

            target.IsZero();
        }

        [TestMethod]
        public void IsZero_Single_IsFalse ()
        {
            var target = new ArgumentConstraint<float>(new Argument<float>("a", 10));

            Action work = () => target.IsZero();

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsZero_Single_WithMessage ()
        {
            var target = new ArgumentConstraint<float>(new Argument<float>("a", 10));

            Action work = () => target.IsZero("Testing");

            work.Should().Throw<ArgumentOutOfRangeException>().ContainingMessage("Testing");
        }
        #endregion

        #endregion

        #region Double

        #region IsGreaterThanOrEqualToZero

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_Double_IsTrue ()
        {
            var target = new ArgumentConstraint<double>(new Argument<double>("a", 10));

            target.IsGreaterThanOrEqualToZero();
        }

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_Double_IsFalse ()
        {
            var target = new ArgumentConstraint<double>(new Argument<double>("a", -10));

            Action work = () => target.IsGreaterThanOrEqualToZero();

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_Double_IsZero ()
        {
            var target = new ArgumentConstraint<double>(new Argument<double>("a", 0));

            target.IsGreaterThanOrEqualToZero();
        }

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_Double_WithMessage ()
        {
            var target = new ArgumentConstraint<double>(new Argument<double>("a", -10));

            Action work = () => target.IsGreaterThanOrEqualToZero("Testing");

            work.Should().Throw<ArgumentOutOfRangeException>().ContainingMessage("Testing");
        }
        #endregion

        #region IsGreaterThanZero

        [TestMethod]
        public void IsGreaterThanZero_Double_IsTrue ()
        {
            var target = new ArgumentConstraint<double>(new Argument<double>("a", 10));

            target.IsGreaterThanZero();
        }

        [TestMethod]
        public void IsGreaterThanZero_Double_IsFalse ()
        {
            var target = new ArgumentConstraint<double>(new Argument<double>("a", -10));

            Action work = () => target.IsGreaterThanZero();

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsGreaterThanZero_Double_IsZero ()
        {
            var target = new ArgumentConstraint<double>(new Argument<double>("a", 0));

            Action work = () => target.IsGreaterThanZero();

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsGreaterThanZero_Double_WithMessage ()
        {
            var target = new ArgumentConstraint<double>(new Argument<double>("a", -10));

            Action work = () => target.IsGreaterThanZero("Testing");

            work.Should().Throw<ArgumentOutOfRangeException>().ContainingMessage("Testing");
        }
        #endregion

        #region IsLessThanOrEqualToZero

        [TestMethod]
        public void IsLessThanOrEqualToZero_Double_IsTrue ()
        {
            var target = new ArgumentConstraint<double>(new Argument<double>("a", -10));

            target.IsLessThanOrEqualToZero();
        }

        [TestMethod]
        public void IsLessThanOrEqualToZero_Double_IsFalse ()
        {
            var target = new ArgumentConstraint<double>(new Argument<double>("a", 10));

            Action work = () => target.IsLessThanOrEqualToZero();

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsLessThanOrEqualToZero_Double_IsZero ()
        {
            var target = new ArgumentConstraint<double>(new Argument<double>("a", 0));

            target.IsLessThanOrEqualToZero();
        }

        [TestMethod]
        public void IsLessThanOrEqualToZero_Double_WithMessage ()
        {
            var target = new ArgumentConstraint<double>(new Argument<double>("a", 10));

            Action work = () => target.IsLessThanOrEqualToZero("Testing");

            work.Should().Throw<ArgumentOutOfRangeException>().ContainingMessage("Testing");
        }
        #endregion

        #region IsLessThanZero

        [TestMethod]
        public void IsLessThanZero_Double_IsTrue ()
        {
            var target = new ArgumentConstraint<double>(new Argument<double>("a", -10));

            target.IsLessThanZero();
        }

        [TestMethod]
        public void IsLessThanZero_Double_IsFalse ()
        {
            var target = new ArgumentConstraint<double>(new Argument<double>("a", 10));

            Action work = () => target.IsLessThanZero();

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsLessThanZero_Double_IsZero ()
        {
            var target = new ArgumentConstraint<double>(new Argument<double>("a", 0));

            Action work = () => target.IsLessThanZero();

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsLessThanZero_Double_WithMessage ()
        {
            var target = new ArgumentConstraint<double>(new Argument<double>("a", 10));

            Action work = () => target.IsLessThanZero("Testing");

            work.Should().Throw<ArgumentOutOfRangeException>().ContainingMessage("Testing");
        }
        #endregion

        #region IsNotZero

        [TestMethod]
        public void IsNotZero_Double_IsTrue ()
        {
            var target = new ArgumentConstraint<double>(new Argument<double>("a", 10));

            target.IsNotZero();
        }

        [TestMethod]
        public void IsNotZero_Double_IsFalse ()
        {
            var target = new ArgumentConstraint<double>(new Argument<double>("a", 0));

            Action work = () => target.IsNotZero();

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsNotZero_Double_WithMessage ()
        {
            var target = new ArgumentConstraint<double>(new Argument<double>("a", 0));

            Action work = () => target.IsNotZero("Testing");

            work.Should().Throw<ArgumentOutOfRangeException>().ContainingMessage("Testing");
        }
        #endregion

        #region IsZero

        [TestMethod]
        public void IsZero_Double_IsTrue ()
        {
            var target = new ArgumentConstraint<double>(new Argument<double>("a", 0));

            target.IsZero();
        }

        [TestMethod]
        public void IsZero_Double_IsFalse ()
        {
            var target = new ArgumentConstraint<double>(new Argument<double>("a", 10));

            Action work = () => target.IsZero();

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsZero_Double_WithMessage ()
        {
            var target = new ArgumentConstraint<double>(new Argument<double>("a", 10));

            Action work = () => target.IsZero("Testing");

            work.Should().Throw<ArgumentOutOfRangeException>().ContainingMessage("Testing");
        }
        #endregion

        #endregion

        #region Decimal

        #region IsGreaterThanOrEqualToZero

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_Decimal_IsTrue ()
        {
            var target = new ArgumentConstraint<decimal>(new Argument<decimal>("a", 10));

            target.IsGreaterThanOrEqualToZero();
        }

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_Decimal_IsFalse ()
        {
            var target = new ArgumentConstraint<decimal>(new Argument<decimal>("a", -10));

            Action work = () => target.IsGreaterThanOrEqualToZero();

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_Decimal_IsZero ()
        {
            var target = new ArgumentConstraint<decimal>(new Argument<decimal>("a", 0));

            target.IsGreaterThanOrEqualToZero();
        }

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_Decimal_WithMessage ()
        {
            var target = new ArgumentConstraint<decimal>(new Argument<decimal>("a", -10));

            Action work = () => target.IsGreaterThanOrEqualToZero("Testing");

            work.Should().Throw<ArgumentOutOfRangeException>().ContainingMessage("Testing");
        }
        #endregion

        #region IsGreaterThanZero

        [TestMethod]
        public void IsGreaterThanZero_Decimal_IsTrue ()
        {
            var target = new ArgumentConstraint<decimal>(new Argument<decimal>("a", 10));

            target.IsGreaterThanZero();
        }

        [TestMethod]
        public void IsGreaterThanZero_Decimal_IsFalse ()
        {
            var target = new ArgumentConstraint<decimal>(new Argument<decimal>("a", -10));

            Action work = () => target.IsGreaterThanZero();

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsGreaterThanZero_Decimal_IsZero ()
        {
            var target = new ArgumentConstraint<decimal>(new Argument<decimal>("a", 0));

            Action work = () => target.IsGreaterThanZero();

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsGreaterThanZero_Decimal_WithMessage ()
        {
            var target = new ArgumentConstraint<decimal>(new Argument<decimal>("a", -10));

            Action work = () => target.IsGreaterThanZero("Testing");

            work.Should().Throw<ArgumentOutOfRangeException>().ContainingMessage("Testing");
        }
        #endregion

        #region IsLessThanOrEqualToZero

        [TestMethod]
        public void IsLessThanOrEqualToZero_Decimal_IsTrue ()
        {
            var target = new ArgumentConstraint<decimal>(new Argument<decimal>("a", -10));

            target.IsLessThanOrEqualToZero();
        }

        [TestMethod]
        public void IsLessThanOrEqualToZero_Decimal_IsFalse ()
        {
            var target = new ArgumentConstraint<decimal>(new Argument<decimal>("a", 10));

            Action work = () => target.IsLessThanOrEqualToZero();

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsLessThanOrEqualToZero_Decimal_IsZero ()
        {
            var target = new ArgumentConstraint<decimal>(new Argument<decimal>("a", 0));

            target.IsLessThanOrEqualToZero();
        }

        [TestMethod]
        public void IsLessThanOrEqualToZero_Decimal_WithMessage ()
        {
            var target = new ArgumentConstraint<decimal>(new Argument<decimal>("a", 10));

            Action work = () => target.IsLessThanOrEqualToZero("Testing");

            work.Should().Throw<ArgumentOutOfRangeException>().ContainingMessage("Testing");
        }
        #endregion

        #region IsLessThanZero

        [TestMethod]
        public void IsLessThanZero_Decimal_IsTrue ()
        {
            var target = new ArgumentConstraint<decimal>(new Argument<decimal>("a", -10));

            target.IsLessThanZero();
        }

        [TestMethod]
        public void IsLessThanZero_Decimal_IsFalse ()
        {
            var target = new ArgumentConstraint<decimal>(new Argument<decimal>("a", 10));

            Action work = () => target.IsLessThanZero();

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsLessThanZero_Decimal_IsZero ()
        {
            var target = new ArgumentConstraint<decimal>(new Argument<decimal>("a", 0));

            Action work = () => target.IsLessThanZero();

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsLessThanZero_Decimal_WithMessage ()
        {
            var target = new ArgumentConstraint<decimal>(new Argument<decimal>("a", 10));

            Action work = () => target.IsLessThanZero("Testing");

            work.Should().Throw<ArgumentOutOfRangeException>().ContainingMessage("Testing");
        }
        #endregion

        #region IsNotZero

        [TestMethod]
        public void IsNotZero_Decimal_IsTrue ()
        {
            var target = new ArgumentConstraint<decimal>(new Argument<decimal>("a", 10));

            target.IsNotZero();
        }

        [TestMethod]
        public void IsNotZero_Decimal_IsFalse ()
        {
            var target = new ArgumentConstraint<decimal>(new Argument<decimal>("a", 0));

            Action work = () => target.IsNotZero();

            work.Should().Throw<ArgumentOutOfRangeException>();
        }
        
        [TestMethod]
        public void IsNotZero_Decimal_WithMessage ()
        {
            var target = new ArgumentConstraint<decimal>(new Argument<decimal>("a", 0));

            Action work = () => target.IsNotZero("Testing");

            work.Should().Throw<ArgumentOutOfRangeException>().ContainingMessage("Testing");
        }
        #endregion

        #region IsZero

        [TestMethod]
        public void IsZero_Decimal_IsTrue ()
        {
            var target = new ArgumentConstraint<decimal>(new Argument<decimal>("a", 0));

            target.IsZero();
        }

        [TestMethod]
        public void IsZero_Decimal_IsFalse ()
        {
            var target = new ArgumentConstraint<decimal>(new Argument<decimal>("a", 10));

            Action work = () => target.IsZero();

            work.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsZero_Decimal_WithMessage ()
        {
            var target = new ArgumentConstraint<decimal>(new Argument<decimal>("a", 10));

            Action work = () => target.IsZero("Testing");

            work.Should().Throw<ArgumentOutOfRangeException>().ContainingMessage("Testing");
        }
        #endregion

        #endregion
    }
}
