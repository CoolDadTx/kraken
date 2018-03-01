using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Data.Common;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.Data.Common
{
    [TestClass]
    public class OutputParameterTests : UnitTest
    {
        #region Named

        [TestMethod]
        public void Named_WithValidValue ()
        {
            var expected = "@s1";

            var target = OutputParameter.Named(expected).OfType<string>();

            //Assert
            target.Name.Should().Be(expected);
        }

        [TestMethod]
        public void Named_WithNull ()
        {            
            Action action = () => OutputParameter.Named(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Named_WithEmpty ()
        {                    
            Action action = () => OutputParameter.Named("");

            action.Should().Throw<ArgumentException>();
        }
        #endregion

        #region OfType

        [TestMethod]
        public void OfType_IsValid ()
        {         
            var target = OutputParameter.Named("@int1").OfType<int>();

            //Assert
            target.Should().BeOfType(typeof(OutputParameter<int>));
        }
        #endregion
    }
}
