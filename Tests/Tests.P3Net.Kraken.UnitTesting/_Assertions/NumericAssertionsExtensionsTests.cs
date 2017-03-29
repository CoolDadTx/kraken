using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.UnitTesting
{
    [TestClass]
    public class NumericAssertionsExtensionsTests : UnitTest
    {
        #region BeApproximately

        #region Decimal

        [TestMethod]
        public void BeApproximately_Decimal_WithinRange ()
        {
            var target = 456.789M;

            target.Should().BeApproximately(target);
        }

        [TestMethod]
        public void BeApproximately_Decimal_NotInRange ()
        {
            var target = 456.789M;

            Action action = () => target.Should().BeApproximately(target - 1);
            
            action.ShouldThrow<AssertFailedException>();
        }

        [TestMethod]
        public void BeApproximately_Decimal_NotInRangeWithCustomMessage ()
        {
            var target = 456.789M;
            var expectedMessage = "Should fail.";

            Action action = () => target.Should().BeApproximately(target - 1, expectedMessage);

            action.ShouldThrow<AssertFailedException>().ContainingMessage(expectedMessage);
        }
        #endregion

        #region Double

        [TestMethod]
        public void BeApproximately_Double_WithinRange ()
        {
            var target = 123456.78901;

            target.Should().BeApproximately(target);
        }

        [TestMethod]
        public void BeApproximately_Double_NotInRange ()
        {
            var target = 123456.78901;

            Action action = () => target.Should().BeApproximately(target - 1);

            action.ShouldThrow<AssertFailedException>();
        }

        [TestMethod]
        public void BeApproximately_Double_NotInRangeWithCustomMessage ()
        {
            var target = 123456.78901;            
            var expectedMessage = "Should fail.";

            Action action = () => target.Should().BeApproximately(target - 1, expectedMessage);

            action.ShouldThrow<AssertFailedException>().ContainingMessage(expectedMessage);
        }
        #endregion

        #region Single

        [TestMethod]
        public void BeApproximately_Single_WithinRange ()
        {
            var target = 456.789F;

            target.Should().BeApproximately(target);
        }

        [TestMethod]
        public void BeApproximately_Single_NotInRange ()
        {
            var target = 456.789F;

            Action action = () => target.Should().BeApproximately(target - 1);
            
            action.ShouldThrow<AssertFailedException>();
        }

        [TestMethod]
        public void BeApproximately_Single_NotInRangeWithCustomMessage ()
        {
            var target = 456.789F;
            var expectedMessage = "Should fail.";

            Action action = () => target.Should().BeApproximately(target - 1, expectedMessage);

            action.ShouldThrow<AssertFailedException>().ContainingMessage(expectedMessage);
        }
        #endregion

        #endregion

        #region BeExactly

        #region Decimal

        [TestMethod]
        public void BeExactly_Decimal_WithinRange ()
        {
            var target = 456M;

            target.Should().BeExactly((int)target);
        }

        [TestMethod]
        public void BeExactly_Decimal_NotInRange ()
        {
            var target = 456.0001M;

            Action action = () => target.Should().BeExactly((int)target);

            action.ShouldThrow<AssertFailedException>();
        }

        [TestMethod]
        public void BeExactly_Decimal_NotInRangeWithCustomMessage ()
        {
            var target = 456.0001M;
            var expectedMessage = "Should fail.";

            Action action = () => target.Should().BeExactly((int)target, expectedMessage);

            action.ShouldThrow<AssertFailedException>().ContainingMessage(expectedMessage);
        }
        #endregion

        #region Double

        [TestMethod]
        public void BeExactly_Double_WithinRange ()
        {
            var target = 123456.0;

            target.Should().BeExactly((int)target);
        }

        [TestMethod]
        public void BeExactly_Double_NotInRange ()
        {
            var target = 123456.00001;

            Action action = () => target.Should().BeExactly((int)target);

            action.ShouldThrow<AssertFailedException>();
        }

        [TestMethod]
        public void BeExactly_Double_NotInRangeWithCustomMessage ()
        {
            var target = 123456.000001;
            var expectedMessage = "Should fail.";

            Action action = () => target.Should().BeExactly((int)target, expectedMessage);

            action.ShouldThrow<AssertFailedException>().ContainingMessage(expectedMessage);
        }
        #endregion

        #region Single

        [TestMethod]
        public void BeExactly_Single_WithinRange ()
        {
            var target = 456F;

            target.Should().BeExactly((int)target);
        }

        [TestMethod]
        public void BeExactly_Single_NotInRange ()
        {
            var target = 456.001F;

            Action action = () => target.Should().BeExactly((int)target);

            action.ShouldThrow<AssertFailedException>();
        }

        [TestMethod]
        public void BeExactly_Single_NotInRangeWithCustomMessage ()
        {
            var target = 456.001F;
            var expectedMessage = "Should fail.";

            Action action = () => target.Should().BeExactly((int)target, expectedMessage);

            action.ShouldThrow<AssertFailedException>().ContainingMessage(expectedMessage);
        }
        #endregion

        #endregion
    }
}
