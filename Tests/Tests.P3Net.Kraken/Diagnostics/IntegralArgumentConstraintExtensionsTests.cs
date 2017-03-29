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
    public class IntegralArgumentConstraintExtensionsTests : UnitTest
    {
        #region SByte

        #region IsGreaterThanOrEqualToZero

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_SByte_IsTrue ()
        {
            var target = new ArgumentConstraint<sbyte>(new Argument<sbyte>("a", 10));

            target.IsGreaterThanOrEqualToZero();
        }

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_SByte_IsFalse ()
        {
            var target = new ArgumentConstraint<sbyte>(new Argument<sbyte>("a", -10));

            Action work = () => target.IsGreaterThanOrEqualToZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_SByte_IsZero ()
        {
            var target = new ArgumentConstraint<sbyte>(new Argument<sbyte>("a", 0));

            target.IsGreaterThanOrEqualToZero();
        }

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_SByte_WithMessage ()
        {
            var target = new ArgumentConstraint<sbyte>(new Argument<sbyte>("a", -10));

            Action work = () => target.IsGreaterThanOrEqualToZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #region IsGreaterThanZero

        [TestMethod]
        public void IsGreaterThanZero_SByte_IsTrue ()
        {
            var target = new ArgumentConstraint<sbyte>(new Argument<sbyte>("a", 10));

            target.IsGreaterThanZero();
        }

        [TestMethod]
        public void IsGreaterThanZero_SByte_IsFalse ()
        {
            var target = new ArgumentConstraint<sbyte>(new Argument<sbyte>("a", -10));

            Action work = () => target.IsGreaterThanZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsGreaterThanZero_SByte_IsZero ()
        {
            var target = new ArgumentConstraint<sbyte>(new Argument<sbyte>("a", 0));

            Action work = () => target.IsGreaterThanZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsGreaterThanZero_SByte_WithMessage ()
        {
            var target = new ArgumentConstraint<sbyte>(new Argument<sbyte>("a", -10));

            Action work = () => target.IsGreaterThanZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #region IsLessThanOrEqualToZero

        [TestMethod]
        public void IsLessThanOrEqualToZero_SByte_IsTrue ()
        {
            var target = new ArgumentConstraint<sbyte>(new Argument<sbyte>("a", -10));

            target.IsLessThanOrEqualToZero();
        }

        [TestMethod]
        public void IsLessThanOrEqualToZero_SByte_IsFalse ()
        {
            var target = new ArgumentConstraint<sbyte>(new Argument<sbyte>("a", 10));

            Action work = () => target.IsLessThanOrEqualToZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsLessThanOrEqualToZero_SByte_IsZero ()
        {
            var target = new ArgumentConstraint<sbyte>(new Argument<sbyte>("a", 0));

            target.IsLessThanOrEqualToZero();
        }

        [TestMethod]
        public void IsLessThanOrEqualToZero_SByte_WithMessage ()
        {
            var target = new ArgumentConstraint<sbyte>(new Argument<sbyte>("a", 10));

            Action work = () => target.IsLessThanOrEqualToZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #region IsLessThanZero

        [TestMethod]
        public void IsLessThanZero_SByte_IsTrue ()
        {
            var target = new ArgumentConstraint<sbyte>(new Argument<sbyte>("a", -10));

            target.IsLessThanZero();
        }

        [TestMethod]
        public void IsLessThanZero_SByte_IsFalse ()
        {
            var target = new ArgumentConstraint<sbyte>(new Argument<sbyte>("a", 10));

            Action work = () => target.IsLessThanZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsLessThanZero_SByte_IsZero ()
        {
            var target = new ArgumentConstraint<sbyte>(new Argument<sbyte>("a", 0));

            Action work = () => target.IsLessThanZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsLessThanZero_SByte_WithMessage ()
        {
            var target = new ArgumentConstraint<sbyte>(new Argument<sbyte>("a", 10));

            Action work = () => target.IsLessThanZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #region IsNotZero

        [TestMethod]
        public void IsNotZero_SByte_IsTrue ()
        {
            var target = new ArgumentConstraint<sbyte>(new Argument<sbyte>("a", 10));

            target.IsNotZero();
        }

        [TestMethod]
        public void IsNotZero_SByte_IsFalse ()
        {
            var target = new ArgumentConstraint<sbyte>(new Argument<sbyte>("a", 0));

            Action work = () => target.IsNotZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsNotZero_SByte_WithMessage ()
        {
            var target = new ArgumentConstraint<sbyte>(new Argument<sbyte>("a", 0));

            Action work = () => target.IsNotZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #region IsZero

        [TestMethod]
        public void IsZero_SByte_IsTrue ()
        {
            var target = new ArgumentConstraint<sbyte>(new Argument<sbyte>("a", 0));

            target.IsZero();
        }

        [TestMethod]
        public void IsZero_SByte_IsFalse ()
        {
            var target = new ArgumentConstraint<sbyte>(new Argument<sbyte>("a", 10));

            Action work = () => target.IsZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsZero_SByte_WithMessage ()
        {
            var target = new ArgumentConstraint<sbyte>(new Argument<sbyte>("a", 10));

            Action work = () => target.IsZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #endregion

        #region Int16

        #region IsGreaterThanOrEqualToZero

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_Int16_IsTrue ()
        {
            var target = new ArgumentConstraint<short>(new Argument<short>("a", 10));

            target.IsGreaterThanOrEqualToZero();
        }

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_Int16_IsFalse ()
        {
            var target = new ArgumentConstraint<short>(new Argument<short>("a", -10));

            Action work = () => target.IsGreaterThanOrEqualToZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_Int16_IsZero ()
        {
            var target = new ArgumentConstraint<short>(new Argument<short>("a", 0));

            target.IsGreaterThanOrEqualToZero();
        }

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_Int16_WithMessage ()
        {
            var target = new ArgumentConstraint<short>(new Argument<short>("a", -10));

            Action work = () => target.IsGreaterThanOrEqualToZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #region IsGreaterThanZero

        [TestMethod]
        public void IsGreaterThanZero_Int16_IsTrue ()
        {
            var target = new ArgumentConstraint<short>(new Argument<short>("a", 10));

            target.IsGreaterThanZero();
        }

        [TestMethod]
        public void IsGreaterThanZero_Int16_IsFalse ()
        {
            var target = new ArgumentConstraint<short>(new Argument<short>("a", -10));

            Action work = () => target.IsGreaterThanZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsGreaterThanZero_Int16_IsZero ()
        {
            var target = new ArgumentConstraint<short>(new Argument<short>("a", 0));

            Action work = () => target.IsGreaterThanZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsGreaterThanZero_Int16_WithMessage ()
        {
            var target = new ArgumentConstraint<short>(new Argument<short>("a", -10));

            Action work = () => target.IsGreaterThanZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #region IsLessThanOrEqualToZero

        [TestMethod]
        public void IsLessThanOrEqualToZero_Int16_IsTrue ()
        {
            var target = new ArgumentConstraint<short>(new Argument<short>("a", -10));

            target.IsLessThanOrEqualToZero();
        }

        [TestMethod]
        public void IsLessThanOrEqualToZero_Int16_IsFalse ()
        {
            var target = new ArgumentConstraint<short>(new Argument<short>("a", 10));

            Action work = () => target.IsLessThanOrEqualToZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsLessThanOrEqualToZero_Int16_IsZero ()
        {
            var target = new ArgumentConstraint<short>(new Argument<short>("a", 0));

            target.IsLessThanOrEqualToZero();
        }

        [TestMethod]
        public void IsLessThanOrEqualToZero_Int16_WithMessage ()
        {
            var target = new ArgumentConstraint<short>(new Argument<short>("a", 10));

            Action work = () => target.IsLessThanOrEqualToZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #region IsLessThanZero

        [TestMethod]
        public void IsLessThanZero_Int16_IsTrue ()
        {
            var target = new ArgumentConstraint<short>(new Argument<short>("a", -10));

            target.IsLessThanZero();
        }

        [TestMethod]
        public void IsLessThanZero_Int16_IsFalse ()
        {
            var target = new ArgumentConstraint<short>(new Argument<short>("a", 10));

            Action work = () => target.IsLessThanZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsLessThanZero_Int16_IsZero ()
        {
            var target = new ArgumentConstraint<short>(new Argument<short>("a", 0));

            Action work = () => target.IsLessThanZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsLessThanZero_Int16_WithMessage ()
        {
            var target = new ArgumentConstraint<short>(new Argument<short>("a", 10));

            Action work = () => target.IsLessThanZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #region IsNotZero

        [TestMethod]
        public void IsNotZero_Int16_IsTrue ()
        {
            var target = new ArgumentConstraint<short>(new Argument<short>("a", 10));

            target.IsNotZero();
        }

        [TestMethod]
        public void IsNotZero_Int16_IsFalse ()
        {
            var target = new ArgumentConstraint<short>(new Argument<short>("a", 0));

            Action work = () => target.IsNotZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsNotZero_Int16_WithMessage ()
        {
            var target = new ArgumentConstraint<short>(new Argument<short>("a", 0));

            Action work = () => target.IsNotZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #region IsZero

        [TestMethod]
        public void IsZero_Int16_IsTrue ()
        {
            var target = new ArgumentConstraint<short>(new Argument<short>("a", 0));

            target.IsZero();
        }

        [TestMethod]
        public void IsZero_Int16_IsFalse ()
        {
            var target = new ArgumentConstraint<short>(new Argument<short>("a", 10));

            Action work = () => target.IsZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsZero_Int16_WithMessage ()
        {
            var target = new ArgumentConstraint<short>(new Argument<short>("a", 10));

            Action work = () => target.IsZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #endregion

        #region Int32

        #region IsGreaterThanOrEqualToZero

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_Int32_IsTrue ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 10));

            target.IsGreaterThanOrEqualToZero();
        }

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_Int32_IsFalse ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", -10));

            Action work = () => target.IsGreaterThanOrEqualToZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_Int32_IsZero ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 0));

            target.IsGreaterThanOrEqualToZero();
        }

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_Int32_WithMessage ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", -10));

            Action work = () => target.IsGreaterThanOrEqualToZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #region IsGreaterThanZero

        [TestMethod]
        public void IsGreaterThanZero_Int32_IsTrue ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 10));

            target.IsGreaterThanZero();
        }

        [TestMethod]
        public void IsGreaterThanZero_Int32_IsFalse ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", -10));

            Action work = () => target.IsGreaterThanZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsGreaterThanZero_Int32_IsZero ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 0));

            Action work = () => target.IsGreaterThanZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsGreaterThanZero_Int32_WithMessage ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", -10));

            Action work = () => target.IsGreaterThanZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #region IsLessThanOrEqualToZero

        [TestMethod]
        public void IsLessThanOrEqualToZero_Int32_IsTrue ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", -10));

            target.IsLessThanOrEqualToZero();
        }

        [TestMethod]
        public void IsLessThanOrEqualToZero_Int32_IsFalse ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 10));

            Action work = () => target.IsLessThanOrEqualToZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsLessThanOrEqualToZero_Int32_IsZero ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 0));

            target.IsLessThanOrEqualToZero();
        }

        [TestMethod]
        public void IsLessThanOrEqualToZero_Int32_WithMessage ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 10));

            Action work = () => target.IsLessThanOrEqualToZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #region IsLessThanZero

        [TestMethod]
        public void IsLessThanZero_Int32_IsTrue ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", -10));

            target.IsLessThanZero();
        }

        [TestMethod]
        public void IsLessThanZero_Int32_IsFalse ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 10));

            Action work = () => target.IsLessThanZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsLessThanZero_Int32_IsZero ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 0));

            Action work = () => target.IsLessThanZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsLessThanZero_Int32_WithMessage ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 10));

            Action work = () => target.IsLessThanZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #region IsNotZero

        [TestMethod]
        public void IsNotZero_Int32_IsTrue ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 10));

            target.IsNotZero();
        }

        [TestMethod]
        public void IsNotZero_Int32_IsFalse ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 0));

            Action work = () => target.IsNotZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }
        
        [TestMethod]
        public void IsNotZero_Int32_WithMessage ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 0));

            Action work = () => target.IsNotZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #region IsZero

        [TestMethod]
        public void IsZero_Int32_IsTrue ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 0));

            target.IsZero();
        }

        [TestMethod]
        public void IsZero_Int32_IsFalse ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 10));

            Action work = () => target.IsZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsZero_Int32_WithMessage ()
        {
            var target = new ArgumentConstraint<int>(new Argument<int>("a", 10));

            Action work = () => target.IsZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #endregion

        #region Int64

        #region IsGreaterThanOrEqualToZero

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_Int64_IsTrue ()
        {
            var target = new ArgumentConstraint<long>(new Argument<long>("a", 10));

            target.IsGreaterThanOrEqualToZero();
        }

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_Int64_IsFalse ()
        {
            var target = new ArgumentConstraint<long>(new Argument<long>("a", -10));

            Action work = () => target.IsGreaterThanOrEqualToZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_Int64_IsZero ()
        {
            var target = new ArgumentConstraint<long>(new Argument<long>("a", 0));

            target.IsGreaterThanOrEqualToZero();
        }

        [TestMethod]
        public void IsGreaterThanOrEqualToZero_Int64_WithMessage ()
        {
            var target = new ArgumentConstraint<long>(new Argument<long>("a", -10));

            Action work = () => target.IsGreaterThanOrEqualToZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #region IsGreaterThanZero

        [TestMethod]
        public void IsGreaterThanZero_Int64_IsTrue ()
        {
            var target = new ArgumentConstraint<long>(new Argument<long>("a", 10));

            target.IsGreaterThanZero();
        }

        [TestMethod]
        public void IsGreaterThanZero_Int64_IsFalse ()
        {
            var target = new ArgumentConstraint<long>(new Argument<long>("a", -10));

            Action work = () => target.IsGreaterThanZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsGreaterThanZero_Int64_IsZero ()
        {
            var target = new ArgumentConstraint<long>(new Argument<long>("a", 0));

            Action work = () => target.IsGreaterThanZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsGreaterThanZero_Int64_WithMessage ()
        {
            var target = new ArgumentConstraint<long>(new Argument<long>("a", -10));

            Action work = () => target.IsGreaterThanZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #region IsLessThanOrEqualToZero

        [TestMethod]
        public void IsLessThanOrEqualToZero_Int64_IsTrue ()
        {
            var target = new ArgumentConstraint<long>(new Argument<long>("a", -10));

            target.IsLessThanOrEqualToZero();
        }

        [TestMethod]
        public void IsLessThanOrEqualToZero_Int64_IsFalse ()
        {
            var target = new ArgumentConstraint<long>(new Argument<long>("a", 10));

            Action work = () => target.IsLessThanOrEqualToZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsLessThanOrEqualToZero_Int64_IsZero ()
        {
            var target = new ArgumentConstraint<long>(new Argument<long>("a", 0));

            target.IsLessThanOrEqualToZero();
        }

        [TestMethod]
        public void IsLessThanOrEqualToZero_Int64_WithMessage ()
        {
            var target = new ArgumentConstraint<long>(new Argument<long>("a", 10));

            Action work = () => target.IsLessThanOrEqualToZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #region IsLessThanZero

        [TestMethod]
        public void IsLessThanZero_Int64_IsTrue ()
        {
            var target = new ArgumentConstraint<long>(new Argument<long>("a", -10));

            target.IsLessThanZero();
        }

        [TestMethod]
        public void IsLessThanZero_Int64_IsFalse ()
        {
            var target = new ArgumentConstraint<long>(new Argument<long>("a", 10));

            Action work = () => target.IsLessThanZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsLessThanZero_Int64_IsZero ()
        {
            var target = new ArgumentConstraint<long>(new Argument<long>("a", 0));

            Action work = () => target.IsLessThanZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsLessThanZero_Int64_WithMessage ()
        {
            var target = new ArgumentConstraint<long>(new Argument<long>("a", 10));

            Action work = () => target.IsLessThanZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #region IsNotZero

        [TestMethod]
        public void IsNotZero_Int64_IsTrue ()
        {
            var target = new ArgumentConstraint<long>(new Argument<long>("a", 10));

            target.IsNotZero();
        }

        [TestMethod]
        public void IsNotZero_Int64_IsFalse ()
        {
            var target = new ArgumentConstraint<long>(new Argument<long>("a", 0));

            Action work = () => target.IsNotZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsNotZero_Int64_WithMessage ()
        {
            var target = new ArgumentConstraint<long>(new Argument<long>("a", 0));

            Action work = () => target.IsNotZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #region IsZero

        [TestMethod]
        public void IsZero_Int64_IsTrue ()
        {
            var target = new ArgumentConstraint<long>(new Argument<long>("a", 0));

            target.IsZero();
        }

        [TestMethod]
        public void IsZero_Int64_IsFalse ()
        {
            var target = new ArgumentConstraint<long>(new Argument<long>("a", 10));

            Action work = () => target.IsZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsZero_Int64_WithMessage ()
        {
            var target = new ArgumentConstraint<long>(new Argument<long>("a", 10));

            Action work = () => target.IsZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #endregion

        #region Byte

        #region IsNotZero

        [TestMethod]
        public void IsNotZero_Byte_IsTrue ()
        {
            var target = new ArgumentConstraint<byte>(new Argument<byte>("a", 10));

            target.IsNotZero();
        }

        [TestMethod]
        public void IsNotZero_Byte_IsFalse ()
        {
            var target = new ArgumentConstraint<byte>(new Argument<byte>("a", 0));

            Action work = () => target.IsNotZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsNotZero_Byte_WithMessage ()
        {
            var target = new ArgumentConstraint<byte>(new Argument<byte>("a", 0));

            Action work = () => target.IsNotZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #region IsZero

        [TestMethod]
        public void IsZero_Byte_IsTrue ()
        {
            var target = new ArgumentConstraint<byte>(new Argument<byte>("a", 0));

            target.IsZero();
        }

        [TestMethod]
        public void IsZero_Byte_IsFalse ()
        {
            var target = new ArgumentConstraint<byte>(new Argument<byte>("a", 10));

            Action work = () => target.IsZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsZero_Byte_WithMessage ()
        {
            var target = new ArgumentConstraint<byte>(new Argument<byte>("a", 10));

            Action work = () => target.IsZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #endregion

        #region UInt16

        #region IsNotZero

        [TestMethod]
        public void IsNotZero_UInt16_IsTrue ()
        {
            var target = new ArgumentConstraint<ushort>(new Argument<ushort>("a", 10));

            target.IsNotZero();
        }

        [TestMethod]
        public void IsNotZero_UInt16_IsFalse ()
        {
            var target = new ArgumentConstraint<ushort>(new Argument<ushort>("a", 0));

            Action work = () => target.IsNotZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsNotZero_UInt16_WithMessage ()
        {
            var target = new ArgumentConstraint<ushort>(new Argument<ushort>("a", 0));

            Action work = () => target.IsNotZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #region IsZero

        [TestMethod]
        public void IsZero_UInt16_IsTrue ()
        {
            var target = new ArgumentConstraint<ushort>(new Argument<ushort>("a", 0));

            target.IsZero();
        }

        [TestMethod]
        public void IsZero_UInt16_IsFalse ()
        {
            var target = new ArgumentConstraint<ushort>(new Argument<ushort>("a", 10));

            Action work = () => target.IsZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsZero_UInt16_WithMessage ()
        {
            var target = new ArgumentConstraint<ushort>(new Argument<ushort>("a", 10));

            Action work = () => target.IsZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #endregion

        #region UInt32

        #region IsNotZero

        [TestMethod]
        public void IsNotZero_UInt32_IsTrue ()
        {
            var target = new ArgumentConstraint<uint>(new Argument<uint>("a", 10));

            target.IsNotZero();
        }

        [TestMethod]
        public void IsNotZero_UInt32_IsFalse ()
        {
            var target = new ArgumentConstraint<uint>(new Argument<uint>("a", 0));

            Action work = () => target.IsNotZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsNotZero_UInt32_WithMessage ()
        {
            var target = new ArgumentConstraint<uint>(new Argument<uint>("a", 0));

            Action work = () => target.IsNotZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #region IsZero

        [TestMethod]
        public void IsZero_UInt32_IsTrue ()
        {
            var target = new ArgumentConstraint<uint>(new Argument<uint>("a", 0));

            target.IsZero();
        }

        [TestMethod]
        public void IsZero_UInt32_IsFalse ()
        {
            var target = new ArgumentConstraint<uint>(new Argument<uint>("a", 10));

            Action work = () => target.IsZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsZero_UInt32_WithMessage ()
        {
            var target = new ArgumentConstraint<uint>(new Argument<uint>("a", 10));

            Action work = () => target.IsZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #endregion

        #region UInt64

        #region IsNotZero

        [TestMethod]
        public void IsNotZero_UInt64_IsTrue ()
        {
            var target = new ArgumentConstraint<ulong>(new Argument<ulong>("a", 10));

            target.IsNotZero();
        }

        [TestMethod]
        public void IsNotZero_UInt64_IsFalse ()
        {
            var target = new ArgumentConstraint<ulong>(new Argument<ulong>("a", 0));

            Action work = () => target.IsNotZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsNotZero_UInt64_WithMessage ()
        {
            var target = new ArgumentConstraint<ulong>(new Argument<ulong>("a", 0));

            Action work = () => target.IsNotZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #region IsZero

        [TestMethod]
        public void IsZero_UInt64_IsTrue ()
        {
            var target = new ArgumentConstraint<ulong>(new Argument<ulong>("a", 0));

            target.IsZero();
        }

        [TestMethod]
        public void IsZero_UInt64_IsFalse ()
        {
            var target = new ArgumentConstraint<ulong>(new Argument<ulong>("a", 10));

            Action work = () => target.IsZero();

            work.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IsZero_UInt64_WithMessage ()
        {
            var target = new ArgumentConstraint<ulong>(new Argument<ulong>("a", 10));

            Action work = () => target.IsZero("Testing");

            work.ShouldThrowArgumentOutOfRangeException().ContainingMessage("Testing");
        }
        #endregion

        #endregion
    }
}
