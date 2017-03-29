using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken.ServiceModel.Channels;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.ServiceModel.Channels
{
    [TestClass]
    public class InProcChannelFactoryTests : UnitTest
    {
        #region Ctor

        [TestMethod]
        public void Ctor_Default ()
        {
            Uri actual = null;
            using (var factory = new InProcChannelFactory())
            {
                actual = factory.BaseAddress;
            };

            actual.AbsoluteUri.Should().StartWith(InProcChannelFactory.DefaultBaseAddress);
        }

        [TestMethod]
        public void Ctor_WithEndpoint ()
        {
            //Act
            var expected = "Test123";
            Uri actual = null;
            using (var factory = new InProcChannelFactory(expected))
            {
                actual = factory.BaseAddress;
            };

            //Assert
            actual.AbsolutePath.Should().Contain("/" + expected);
        }
        #endregion

        //[TestMethod]
        //public void CreateInstance_NewService ( )
        //{
        //    var expected = 20;

        //    int actual;

        //    //Act
        //    using (var target = new InProcChannelFactory(""))
        //    {
        //        var instance = target.CreateInstance<TestWcfService, ITestWcfInterface>();

        //        actual = instance.Double(10);
        //    };  
          
        //    //Assert
        //    actual.Should().Be(expected);
        //}

        [TestMethod]
        public void Default_Get ()
        {
            var expected = InProcChannelFactory.Default;
            var actual = InProcChannelFactory.Default;

            actual.Should().Be(expected);
        }


        #region Private Members

        [ServiceContract]
        internal interface ITestWcfInterface
        {
            [OperationContract]
            int Double ( int value );
        }

        internal sealed class TestWcfService : ITestWcfInterface
        {
            public int Double ( int value ) { return value * 2; }
        }
        #endregion
    }
}
