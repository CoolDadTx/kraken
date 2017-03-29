using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.UnitTesting._Assertions
{
    [TestClass]
    public class ExceptionAssertionsExtensionsTests : UnitTest
    {
        #region ContainingMessage

        [TestMethod]
        public void ContainingMessage_Works ()
        {
            var expectedMessage = "Hello";

            Action action = () => { throw new Exception(expectedMessage); };

            action.ShouldThrow<Exception>().ContainingMessage(expectedMessage);
        }

        [TestMethod]
        public void ContainingMessage_WithReason ()
        {
            var expectedMessage = "Hello";
            
            Action action = () => { throw new Exception(expectedMessage); };

            action.ShouldThrow<Exception>().ContainingMessage(expectedMessage, "Fail");
        }
        #endregion

        #region WithParameter

        [TestMethod]
        public void WithParameter_Works ()
        {
            var expectedName = "Parm1";

            Action action = () => { throw new ArgumentException("Message", expectedName);  };

            action.ShouldThrow<ArgumentException>().WithParameter(expectedName);
        }

        [TestMethod]
        public void WithParameter_WithReason ()
        {
            var expectedName = "Parm1";

            Action action = () => { throw new ArgumentException("Message", expectedName); };

            action.ShouldThrow<ArgumentException>().WithParameter(expectedName, "Failed");
        }
        #endregion
    }
}
