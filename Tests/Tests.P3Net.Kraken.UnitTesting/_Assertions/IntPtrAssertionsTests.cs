using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.UnitTesting
{
    [TestClass]
    public class IntPtrAssertionsTests : UnitTest
    {
        #region Be

        #region Int32

        [TestMethod]
        public void Be_Int32_Matches ()
        {
            var expected = 12345678;

            var target = new IntPtr(expected);

            target.Should().Be(expected);
        }

        [TestMethod]
        public void Be_Int32_DoesNotMatch ()
        {
            var target = new IntPtr(12345678);

            Action action = () => target.Should().Be(14);
            
            action.ShouldThrow<AssertFailedException>();
        }

        [TestMethod]
        public void Be_Int32_DoesNotMatch_WithCustomMessage ()
        {
            var target = new IntPtr(12345678);
            var expectedMessage = "Should fail";

            Action action = () => target.Should().Be(14, expectedMessage);

            action.ShouldThrow<AssertFailedException>().ContainingMessage(expectedMessage);
        }
        #endregion       
 
        #region Int64

        [TestMethod]
        public void Be_Int64_Matches ()
        {
            var expected = 1234567890L;

            var target = new IntPtr(expected);

            target.Should().Be(expected);
        }

        [TestMethod]
        public void Be_Int64_DoesNotMatch ()
        {
            var target = new IntPtr(1234567890L);

            Action action = () => target.Should().Be(14);

            action.ShouldThrow<AssertFailedException>();
        }

        [TestMethod]
        public void Be_Int64_DoesNotMatch_WithCustomMessage ()
        {
            var target = new IntPtr(1234567890L);
            var expectedMessage = "Should fail";                

            Action action = () => target.Should().Be(14, expectedMessage);

            action.ShouldThrow<AssertFailedException>().ContainingMessage(expectedMessage);
        }
        #endregion  

        #region IntPtr

        [TestMethod]
        public void Be_IntPtr_Matches ()
        {
            var expectedValue = 19141412;

            var target = new IntPtr(expectedValue);

            target.Should().Be(new IntPtr(expectedValue));
        }

        [TestMethod]
        public void Be_IntPtr_DoesNotMatch ()
        {
            var target = new IntPtr(12345678);

            Action action = () => target.Should().Be(new IntPtr(53522));

            action.ShouldThrow<AssertFailedException>();
        }

        [TestMethod]
        public void Be_IntPtr_DoesNotMatch_WithCustomMessage ()
        {
            var target = new IntPtr(12345678);
            var expectedMessage = "Should fail";

            Action action = () => target.Should().Be(new IntPtr(342423), expectedMessage);

            action.ShouldThrow<AssertFailedException>().ContainingMessage(expectedMessage);
        }
        #endregion  

        #endregion

        #region BeZero

        [TestMethod]
        public void BeZero_IsTrue ()
        {
            var target = new IntPtr();

            target.Should().BeZero();
        }

        [TestMethod]
        public void BeZero_IsFalse ()
        {
            var target = new IntPtr(4532452);

            Action action = () => target.Should().BeZero();

            action.ShouldThrow<AssertFailedException>();
        }

        [TestMethod]
        public void BeZero_WithMessage ()
        {
            var target = new IntPtr(3453434);
            var expectedMessage = "Should fail";

            Action action = () => target.Should().BeZero(expectedMessage);

            action.ShouldThrow<AssertFailedException>().ContainingMessage(expectedMessage);
        }
        #endregion

        #region NotBeZero

        [TestMethod]
        public void NotBeZero_IsTrue ()
        {
            var target = new IntPtr(45454);

            target.Should().NotBeZero();
        }

        [TestMethod]
        public void NotBeZero_IsFalse ()
        {
            var target = new IntPtr();

            Action action = () => target.Should().NotBeZero();

            action.ShouldThrow<AssertFailedException>();
        }

        [TestMethod]
        public void NotBeZero_WithMessage ()
        {
            var target = new IntPtr();
            var expectedMessage = "Should fail";

            Action action = () => target.Should().NotBeZero(expectedMessage);

            action.ShouldThrow<AssertFailedException>().ContainingMessage(expectedMessage);
        }
        #endregion
    }
}
