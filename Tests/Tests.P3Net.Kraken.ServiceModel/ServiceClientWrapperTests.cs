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
    public class ServiceClientWrapperTests : UnitTest
    {
        #region Tests

        #region Ctor

        [TestMethod]
        public void Ctor_DefaultSucceeds ( )
        {
            ////Arrange
            //using (var client = new ServiceClientWrapper<ITestClient>())
            //{
            //    //Act
            //    client.Open();

            //    //Assert
            //    client.State.Should().Be(CommunicationState.Opened);
            //};
            Assert.Inconclusive("Not implemented");
        }
        #endregion

        #region Close

        [TestMethod]
        public void Close_UnopenedClientSucceeds ( )
        {
            ////Arrange
            //ICommunicationObject commObject = null;
            //using (var target = new ServiceClientWrapper<ITestClient>())
            //{
            //    commObject = target as ICommunicationObject;

            //    //Act                
            //    target.Close();                
            //};

            ////Assert
            //commObject.State.Should().Be(CommunicationState.Closed);
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void Close_OpenedClientSucceeds ()
        {
            ////Arrange
            //ICommunicationObject commObject = null;
            //using (var target = new ServiceClientWrapper<ITestClient>())
            //{
            //    commObject = target as ICommunicationObject;

            //    //Act               
            //    var actual = target.Client.StringToInt32("10");                                 
            //};

            ////Assert
            //commObject.State.Should().Be(CommunicationState.Closed);
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void Close_FaultedClientSucceeds ()
        {
            ////Arrange
            //ICommunicationObject commObject = null;
            //using (var target = new ServiceClientWrapper<ITestClient>())
            //{
            //    commObject = target as ICommunicationObject;

            //    //Act       
            //    target.Client.FaultClient();                                         
            //};

            ////Assert
            //commObject.State.Should().Be(CommunicationState.Closed);
            Assert.Inconclusive("Not implemented");
        }
        #endregion
        
        #region Type Operator
        
        [TestMethod]
        public void TypeOperator_OpenClientSucceeds ( )
        {
            ////Arrange
            //int actual = 0;
            //ITestClient realClient = null;
            //using (var target = new ServiceClientWrapper<ITestClient>())
            //{
            //    //Act       
            //    realClient = target.Client;
            //    actual = realClient.StringToInt32("20");
            //};

            ////Assert
            //actual.Should().Be(20);
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void TypeOperator_ClosedClientFails ()
        {
            ////Arrange
            //ITestClient realClient = null;
            //using (var target = new ServiceClientWrapper<ITestClient>())
            //{
            //    //Act       
            //    target.Close();
            //    realClient = target.Client;                
            //};

            ////Assert
            //realClient.Should().BeNull();
            Assert.Inconclusive("Not implemented");
        }
        #endregion

        #endregion

        #region Private Members

        //Test interface for WCF
        [ServiceContract]
        private interface ITestClient
        {
            [OperationContract]
            int StringToInt32 ( string value );

            [OperationContract]
            void FaultClient ();
        }

        #endregion
    }
}