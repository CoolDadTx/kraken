#region Imports

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken;
using P3Net.Kraken.UnitTesting;

#endregion

namespace Tests.P3Net.Kraken
{
    [TestClass]
    public class EventHandlerExensionsTests : UnitTest
    {
        #region Tests

        #region Raise (with argument)
        
        [TestMethod]
        public void Raise_WithArguments_EventNotNull ()
        {
            object eventSender = null;
            int eventId = 0;
            int expected = 10;

            var target = new SomeType();
            target.SomeEvent += (o,e) => {
                eventSender = o;
                eventId = e.Id; 
            };

            //Act
            target.Notify(expected);
            
            //Assert
            eventSender.Should().Be(target);
            eventId.Should().Be(expected);
        }

        [TestMethod]
        public void Raise_WithArguments_EventIsNull ()
        {
            int expected = 10;

            var target = new SomeType();

            //Act
            target.Notify(expected);

            //Assert
            //Doesn't crash
        }
        #endregion

        #region Raise (with function)

        [TestMethod]
        public void Raise_WithFunction_EventNotNull ()
        {
            object eventSender = null;
            int eventId = 0;
            int expected = 10;

            var target = new SomeType();
            target.SomeEvent += (o,e) => {
                eventSender = o;
                eventId = e.Id; 
            };

            //Act
            var actual = target.NotifyWithFunction(expected);
            
            //Assert
            actual.Should().BeTrue();

            eventSender.Should().Be(target);
            eventId.Should().Be(expected);            
        }

        [TestMethod]
        public void Raise_WithFunction_EventIsNull ()
        {
            int expected = 10;

            var target = new SomeType();

            //Act
            var actual = target.NotifyWithFunction(expected);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #endregion

        #region Private Members

        private sealed class SomeType
        {
            public event EventHandler<SomeEventArgs> SomeEvent;

            public void Notify ( int id )
            {
                SomeEvent.Raise(this, new SomeEventArgs(id));
            }

            public bool NotifyWithFunction ( int id )
            {
                bool createdArg = false;

                SomeEvent.Raise(this, () => { createdArg = true; return new SomeEventArgs(id); });

                return createdArg;
            }
        }

        private sealed class SomeEventArgs : EventArgs
        {
            public SomeEventArgs ( int id )
            {
                Id = id;
            }

            public int Id { get; private set; }
        }
        #endregion
    }
}
