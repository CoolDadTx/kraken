/*
 * Copyright (c) 2007 by Michael L. Taylor
 * All rights reserved.
 */
#region Imports

using System;
using System.Collections.Generic;
using System.ComponentModel;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken.ComponentModel;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken.ComponentModel
{
    [TestClass]
    public class NotifyPropertyChangeTests : UnitTest
    {
        #region Tests

        #region PropertyChanged
        
        [TestMethod]
        public void PropertyChanged_RaisedWithNoHandler ()
        {
            var target = new TestNotifyPropertyChange();

            //Act
            target.Value = 4;
            
            //Assert
            target.PropertyChangedWasRaised("Value").Should().BeTrue();
        }

        [TestMethod]
        public void PropertyChanged_CallsHandler ()
        {
            var target = new TestNotifyPropertyChange();

            //Act
            bool actual = false;
            target.PropertyChanged += ( o, e ) =>
            {
                if (e.PropertyName == "Value")
                    actual = true;
            };
            target.Value = 4;

            //Assert
            actual.Should().BeTrue();
        }
        #endregion

        #region PropertyChanging

        [TestMethod]
        public void PropertyChanging_RaisedWithNoHandler ()
        {
            var target = new TestNotifyPropertyChange();

            //Act
            target.Value = 4;

            //Assert
            target.PropertyChangingWasRaised("Value").Should().BeTrue();
        }

        [TestMethod]
        public void PropertyChanging_CallsHandler ()
        {
            var target = new TestNotifyPropertyChange();

            //Act
            bool actual = false;
            target.PropertyChanging += ( o, e ) =>
            {
                if (e.PropertyName == "Value")
                    actual = true;
            };
            target.Value = 4;

            //Assert
            actual.Should().BeTrue();
        }
        #endregion
        
        #endregion
        
        #region Private Members

        private class TestNotifyPropertyChange : NotifyPropertyChange
        {
            public int Value
            {
                get { return m_value; }
                set
                {
                    this.OnPropertyChanging("Value");
                    m_value = value;
                    this.OnPropertyChanged("Value");
                }
            }

            public bool PropertyChangingWasRaised ( string propertyName )
            {
                return m_propertyChanging.Contains(propertyName);
            }

            public bool PropertyChangedWasRaised ( string propertyName )
            {
                return m_propertyChanged.Contains(propertyName);
            }

            protected override void OnPropertyChanged ( string propertyName )
            {
                base.OnPropertyChanged(propertyName);

                m_propertyChanged.Add(propertyName);
            }

            protected override void OnPropertyChanging ( string propertyName )
            {
                base.OnPropertyChanging(propertyName);

                m_propertyChanging.Add(propertyName);
            }

            private int m_value;
            private List<string> m_propertyChanged = new List<string>();
            private List<string> m_propertyChanging = new List<string>();
        }
        #endregion
    }
}