using System;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Data.Common;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.Data.Common
{
    [TestClass]
    public class InputParameterTests : UnitTest
    {        
        #region Named

        [TestMethod]
        public void Named_WithValidValue ()
        {
            var expected = "@s1";

            var target = InputParameter.Named(expected).WithValue("");

            //Assert
            target.Name.Should().Be(expected);
        }

        [TestMethod]
        public void Named_WithNull ()
        {            
            Action action = () => InputParameter.Named(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Named_WithEmpty ()
        {
            Action action = () => InputParameter.Named("");

            action.Should().Throw<ArgumentException>();
        }
        #endregion

        #region WithValue

        [TestMethod]
        public void WithValue_IsValid ()
        {
            var expected = 40;

            var target = InputParameter.Named("@in1").WithValue(expected);

            //Assert
            target.Value.Should().Be(expected);
        }
        #endregion
    }
}
