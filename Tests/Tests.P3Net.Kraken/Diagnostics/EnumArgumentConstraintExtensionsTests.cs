using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

using P3Net.Kraken.Diagnostics;

namespace Tests.P3Net.Kraken.Diagnostics
{
    [TestClass]
    public class EnumArgumentConstraintExtensionsTests
    {
        #region IsValidEnum

        [TestMethod]
        public void IsValidEnum_IsTrue ()
        {
            var target = new ArgumentConstraint<SimpleEnum>(new Argument<SimpleEnum>("arg", SimpleEnum.First));

            target.IsValidEnum();
        }

        [TestMethod]
        public void IsValidEnum_IsFalse ()
        {
            var target = new ArgumentConstraint<SimpleEnum>(new Argument<SimpleEnum>("arg", (SimpleEnum)10));

            Action action = () => target.IsValidEnum();

            action.ShouldThrow<ArgumentOutOfRangeException>();
        }
        #endregion

        #region IsValidEnumAndNotZero

        [TestMethod]
        public void IsValidEnumAndNotZero_IsTrue ()
        {
            var target = new ArgumentConstraint<SimpleEnum>(new Argument<SimpleEnum>("arg", SimpleEnum.First));

            target.IsValidEnumAndNotZero();
        }

        [TestMethod]
        public void IsValidEnumAndNotZero_IsFalse ()
        {
            var target = new ArgumentConstraint<SimpleEnum>(new Argument<SimpleEnum>("arg", (SimpleEnum)10));

            Action action = () => target.IsValidEnumAndNotZero();

            action.ShouldThrow<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void IsValidEnumAndNotZero_IsZero ()
        {
            var target = new ArgumentConstraint<SimpleEnum>(new Argument<SimpleEnum>("arg", (SimpleEnum)0));

            Action action = () => target.IsValidEnumAndNotZero();

            action.ShouldThrow<ArgumentOutOfRangeException>();
        }
        #endregion

        #region IsIn

        [TestMethod]
        public void IsIn_IsInList ()
        {
            var target = new ArgumentConstraint<SimpleEnum>(new Argument<SimpleEnum>("arg", SimpleEnum.First));

            target.IsIn(SimpleEnum.First, SimpleEnum.Third);            
        }

        [TestMethod]
        public void IsIn_IsNotInList ()
        {
            var target = new ArgumentConstraint<SimpleEnum>(new Argument<SimpleEnum>("arg", SimpleEnum.First));

            Action action = () => target.IsIn(SimpleEnum.Second, SimpleEnum.Third);

            action.ShouldThrow<ArgumentOutOfRangeException>();
        }
        #endregion

        #region IsNotIn

        [TestMethod]
        public void IsNotIn_IsNotInList ()
        {
            var target = new ArgumentConstraint<SimpleEnum>(new Argument<SimpleEnum>("arg", SimpleEnum.First));

            target.IsNotIn(SimpleEnum.Second, SimpleEnum.Third);
        }

        [TestMethod]
        public void IsNotIn_IsInList ()
        {
            var target = new ArgumentConstraint<SimpleEnum>(new Argument<SimpleEnum>("arg", SimpleEnum.First));

            Action action = () => target.IsNotIn(SimpleEnum.First, SimpleEnum.Third);

            action.ShouldThrow<ArgumentOutOfRangeException>();
        }
        #endregion

        #region Private Members

        private enum SimpleEnum
        {
            First = 1,
            Second = 2,
            Third = 3,
        }
        #endregion
    }
}
