using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

using P3Net.Kraken.Reflection;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.Reflection
{
    public partial class TypeExtensionsTests
    {
        #region GetProperty

        [TestMethod]
        public void GetProperty_Exists ()
        {
            var target = typeof(Collection<int>);
            var expected = target.GetProperty("Count");

            //Act
            var actual = target.GetProperty(expected.Name, false);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetProperty_DoesNotExist ()
        {
            var target = typeof(Collection<int>);
            
            //Act
            var actual = target.GetProperty("blah", false);

            //Assert
            actual.Should().BeNullFixed();
        }

        [TestMethod]
        public void GetProperty_NameIsNull ()
        {
            var target = typeof(Collection<int>);

            //Act
            var actual = target.GetProperty(null, false);

            //Assert
            actual.Should().BeNullFixed();
        }

        [TestMethod]
        public void GetProperty_NameIsEmpty ()
        {
            var target = typeof(Collection<int>);

            //Act
            var actual = target.GetProperty("", false);

            //Assert
            actual.Should().BeNullFixed();
        }

        [TestMethod]
        public void GetProperty_BadCaseWithSensitive ()
        {
            var target = typeof(Collection<int>);

            //Act
            var actual = target.GetProperty("COUNT", false);

            //Assert
            actual.Should().BeNullFixed();
        }

        [TestMethod]
        public void GetProperty_BadCaseWithInsensitive ()
        {
            var target = typeof(Collection<int>);

            //Act
            var actual = target.GetProperty("COUNT", true);

            //Assert
            actual.Should().NotBeNullFixed();
        }
        #endregion
        
        #region GetPublicProperty

        [TestMethod]
        public void GetPublicProperty_Exists ()
        {
            var target = typeof(Collection<int>);
            var expected = target.GetProperty("Count");

            //Act
            var actual = target.GetPublicProperty(expected.Name);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetPublicProperty_DoesNotExist ()
        {
            var target = typeof(Collection<int>);

            //Act
            var actual = target.GetPublicProperty("blah");

            //Assert
            actual.Should().BeNullFixed();
        }

        [TestMethod]
        public void GetPublicProperty_NameIsNull ()
        {
            var target = typeof(Collection<int>);

            //Act
            var actual = target.GetPublicProperty(null);

            //Assert
            actual.Should().BeNullFixed();
        }

        [TestMethod]
        public void GetPublicProperty_NameIsEmpty ()
        {
            var target = typeof(Collection<int>);

            //Act
            var actual = target.GetPublicProperty("");

            //Assert
            actual.Should().BeNullFixed();
        }

        [TestMethod]
        public void GetPublicProperty_BadCaseWithSensitive ()
        {
            var target = typeof(Collection<int>);

            //Act
            var actual = target.GetPublicProperty("COUNT", false);

            //Assert
            actual.Should().BeNullFixed();
        }

        [TestMethod]
        public void GetPublicProperty_BadCaseWithInsensitive ()
        {
            var target = typeof(Collection<int>);

            //Act
            var actual = target.GetPublicProperty("COUNT", true);

            //Assert
            actual.Should().NotBeNullFixed();
        }

        [TestMethod]
        public void GetPublicProperty_IsNotPublic ()
        {
            var target = typeof(Collection<int>);

            //Act
            var actual = target.GetPublicProperty("Items", true);

            //Assert
            actual.Should().BeNullFixed();
        }

        [TestMethod]
        public void GetPublicProperty_SetterIsPublic ()
        {
            var target = typeof(PublicSetterType);

            //Act
            var actual = target.GetPublicProperty("Item", true);

            //Assert
            actual.Should().NotBeNullFixed();
        }
        #endregion

        #region GetPublicOrProtectedProperty

        [TestMethod]
        public void GetPublicOrProtectedProperty_Exists ()
        {
            var target = typeof(Collection<int>);
            var expected = target.GetProperty("Count");

            //Act
            var actual = target.GetPublicOrProtectedProperty(expected.Name);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetPublicOrProtectedProperty_DoesNotExist ()
        {
            var target = typeof(Collection<int>);

            //Act
            var actual = target.GetPublicOrProtectedProperty("blah");

            //Assert
            actual.Should().BeNullFixed();
        }

        [TestMethod]
        public void GetPublicOrProtectedProperty_NameIsNull ()
        {
            var target = typeof(Collection<int>);

            //Act
            var actual = target.GetPublicOrProtectedProperty(null);

            //Assert
            actual.Should().BeNullFixed();
        }

        [TestMethod]
        public void GetPublicOrProtectedProperty_NameIsEmpty ()
        {
            var target = typeof(Collection<int>);

            //Act
            var actual = target.GetPublicOrProtectedProperty("");

            //Assert
            actual.Should().BeNullFixed();
        }

        [TestMethod]
        public void GetPublicOrProtectedProperty_BadCaseWithSensitive ()
        {
            var target = Type.GetType("System.Collections.ObjectModel.Collection`1");            

            //Act
            var actual = target.GetPublicOrProtectedProperty("ITEMS");

            //Assert
            actual.Should().BeNullFixed();
        }

        [TestMethod]
        public void GetPublicOrProtectedProperty_BadCaseWithInsensitive ()
        {
            var target = Type.GetType("System.Collections.ObjectModel.Collection`1");            

            //Act
            var actual = target.GetPublicOrProtectedProperty("ITEMS", true);

            //Assert
            actual.Should().NotBeNullFixed();
        }

        [TestMethod]
        public void GetPublicOrProtectedProperty_IsProtected ()
        {
            var target = Type.GetType("System.Collections.ObjectModel.Collection`1");            
            var expected = target.GetProperty("Items", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase);

            //Act
            var actual = target.GetPublicOrProtectedProperty(expected.Name);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetPublicOrProtectedProperty_SetterIsPublic ()
        {
            var target = typeof(PublicSetterType);

            //Act
            var actual = target.GetPublicOrProtectedProperty("Item", true);

            //Assert
            actual.Should().NotBeNullFixed();
        }

        [TestMethod]
        public void GetPublicOrProtectedProperty_IsPrivate ()
        {
            var target = typeof(PublicSetterType);

            //Act
            var actual = target.GetPublicOrProtectedProperty("Hidden", true);

            //Assert
            actual.Should().BeNullFixed();
        }
        #endregion

        #region Private Members

        private sealed class PublicSetterType
        {
            public int Item { private get; set; }
            private int Hidden { get; set; }
        }
        #endregion
    }
}
