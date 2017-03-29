#region Imports

using System;
using System.ServiceModel;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.ServiceModel;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken.ServiceModel
{
    [TestClass]    
    public class ServiceClientFactoryTests : UnitTest
    {
        #region Tests 

        #region CreateAndWrap

        [TestMethod]        
        public void CreateAndWrap_DefaultSucceeds ( )
        {
            ////Act
            //using (var client = ServiceClientFactory.CreateAndWrap<ITestClient>())
            //{
            //    //Assert
            //    client.State.Should().Be(CommunicationState.Opened);
            //};            
            Assert.Inconclusive("Not implemented");        
        }
        #endregion

        #region InvokeMethod

        [TestMethod]        
        public void InvokeMethod_SimpleMethodSucceeds ( )
        {
            ////Act
            //ServiceClientFactory.InvokeMethod<ITestClient>(x => x.NotifyCalled("Called"));

            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void InvokeMethod_NullMethodFails ()
        {
            ////Act
            //ServiceClientFactory.InvokeMethod<ITestClient>(null);  
            Assert.Inconclusive("Not implemented");
        }
        #endregion

        #region InvokeMethodWithResult

        [TestMethod]
        public void InvokeMethodWithResult_SimpleMethodSucceeds ()
        {
            ////Act
            //var actual = ServiceClientFactory.InvokeMethod<ITestClient, int>(x => x.StringToInt32("10"));

            ////Assert
            //actual.Should().Be(10);
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void InvokeMethodWithResult_NullMethodFails ()
        {
            ////Act
            //ServiceClientFactory.InvokeMethod<ITestClient, int>(null);            
            Assert.Inconclusive("Not implemented");
        }
        #endregion

        #endregion

        #region Private Members

        [ServiceContract]
        public interface ITestClient
        {
            [OperationContract]
            int StringToInt32 ( string value );

            [OperationContract]
            void NotifyCalled ( string value );
        }        
        #endregion
    }
}
