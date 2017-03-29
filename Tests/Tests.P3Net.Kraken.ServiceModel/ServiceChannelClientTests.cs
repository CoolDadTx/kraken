using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using P3Net.Kraken.UnitTesting;
using System.ServiceModel;

using P3Net.Kraken.ServiceModel;

namespace Tests.P3Net.Kraken.ServiceModel
{
    [TestClass]
    public class ServiceChannelClientTests : UnitTest
    {
        #region HasOpenChannel

        [TestMethod]
        public void HasOpenChannel_IsFalse ()
        {
            var target = new TestServiceClient();

            target.HasOpenChannelTest.Should().BeFalse();
        }

        [TestMethod]
        public void HasOpenChannel_IsTrue ()
        {
            var target = new TestServiceClient();
            var process = target as ISupportsPersistentChannel;
            process.Open();

            target.HasOpenChannelTest.Should().BeTrue();
        }
        #endregion

        #region CloseChannel

        [TestMethod]
        public void CloseChannel_Successful ()
        {
            var target = new TestServiceClient();
            target.OpenChannelTest();

            target.HasOpenChannelTest.Should().BeTrue();
            target.CloseChannelTest();
            
            target.HasOpenChannelTest.Should().BeFalse();
        }

        [TestMethod]
        public void CloseChannel_AlreadyClosedIsIgnored ()
        {
            var target = new TestServiceClient();
            target.OpenChannelTest();

            target.HasOpenChannelTest.Should().BeTrue();
            target.CloseChannelTest();
            target.CloseChannelTest();

            target.HasOpenChannelTest.Should().BeFalse();
        }
        #endregion

        #region CreateInstance

        //Need to figure out how to unit test this easily
        //[TestMethod]
        //public void CreateInstance_IsCalledWhenInvoking ()
        //{
        //    var target = new TestServiceClient();
        //    target.Foo();

        //    target.CreateInstanceCounter.Should().Be(1);
        //    target.FooCounter.Should().Be(1);
        //}

        //[TestMethod]
        //public void CreateInstance_IsCalledOnceWhenChannelOpen ()
        //{
        //    var target = new TestServiceClient();
        //    target.OpenChannel();

        //    target.Foo();
        //    target.Foo();

        //    target.CreateInstanceCounter.Should().Be(1);
        //    target.FooCounter.Should().Be(2);
        //}

        //[TestMethod]
        //public void CreateInstance_IsCalledWhenInvokingFunction ()
        //{
        //    var target = new TestServiceClient();
        //    target.Bar();

        //    target.CreateInstanceCounter.Should().Be(1);
        //    target.BarCounter.Should().Be(1);
        //}

        //[TestMethod]
        //public void CreateInstance_IsCalledOnceWhenChannelOpenFunction ()
        //{
        //    var target = new TestServiceClient();
        //    target.OpenChannel();

        //    target.Bar();
        //    target.Bar();

        //    target.CreateInstanceCounter.Should().Be(1);
        //    target.BarCounter.Should().Be(2);
        //}
        #endregion

        #region OpenChannel

        [TestMethod]
        public void OpenChannel_Successful ()
        {
            var target = new TestServiceClient();
            target.OpenChannelTest();

            target.HasOpenChannelTest.Should().BeTrue();
        }

        [TestMethod]
        public void OpenChannel_AlreadyOpenIsIgnored ()
        {
            var target = new TestServiceClient();
            target.OpenChannelTest();                        
            target.OpenChannelTest();

            target.HasOpenChannelTest.Should().BeTrue();
        }
        #endregion

        #region ISupportsPersistentChannel

        [TestMethod]
        public void ISupportsPersistentChannel_Close ()
        {
            var instance = new TestServiceClient();
            instance.OpenChannelTest();

            var target = instance as ISupportsPersistentChannel;
            target.Close();

            instance.HasOpenChannelTest.Should().BeFalse();
        }

        [TestMethod]
        public void ISupportsPersistentChannel_Open ()
        {
            var instance = new TestServiceClient();
            
            var target = instance as ISupportsPersistentChannel;
            target.Open();

            instance.HasOpenChannelTest.Should().BeTrue();
        }
        #endregion

        #region Private Types

        private sealed class TestServiceClient : ServiceChannelClient<ITestInterface>, ITestInterface
        {
            public int CreateInstanceCounter { get; private set; }
            public int FooCounter { get; private set; }
            public int BarCounter { get; private set; }

            public bool HasOpenChannelTest
            {
                get { return this.HasOpenChannel; }
            }

            public void CloseChannelTest ()
            {
                this.CloseChannel();
            }

            public void OpenChannelTest ()
            {
                this.OpenChannel();
            }            

            public void Foo ()
            {
                InvokeMethod(x => x.Foo());
            }

            public string Bar ()
            {
                return InvokeMethod(x => x.Bar());
            }

            protected override ServiceClientWrapper<ITestInterface> CreateInstance ()
            {
                var mock = new Mock<ITestInterface>();
                mock.Setup(x => x.Foo()).Callback(() => FooCounter++);
                mock.Setup(x => x.Bar()).Callback(() => BarCounter++).Returns("Test");

                return new TestServiceClientWrapper(() => { CreateInstanceCounter++; return mock.Object; });
            }
        }

        [ServiceContract]
        public interface ITestInterface
        {
            [OperationContract]
            void Foo ();

            [OperationContract]
            string Bar ();
        }

        private sealed class TestServiceClientWrapper : ServiceClientWrapper<ITestInterface>
        {
            public TestServiceClientWrapper ( Func<ITestInterface> channel ) : base(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost/Test"))
            {
                m_channel = channel;
            }

            protected override ITestInterface CreateChannel ()
            {
                return m_channel();
            }

            private Func<ITestInterface> m_channel;
        }        
        #endregion
    }
}

