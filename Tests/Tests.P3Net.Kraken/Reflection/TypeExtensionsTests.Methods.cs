using System;
using System.Collections.Generic;
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
        #region GetMethod

        [TestMethod]
        public void GetMethod_MethodExists ()
        {
            var target = typeof(List<int>);
            var expected = target.GetMethod("Add");

            //Act
            var actual = target.GetMethod(expected.Name, false);
            
            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetMethod_MethodDoesNotExist ()
        {
            var target = typeof(List<int>);
            
            //Act
            var actual = target.GetMethod("blah", false);

            //Assert
            actual.Should().BeNullFixed();
        }

        [TestMethod]
        public void GetMethod_NameIsNull ()
        {
            var target = typeof(List<int>);

            //Act
            var actual = target.GetMethod(null, false);

            //Assert
            actual.Should().BeNullFixed();
        }

        [TestMethod]
        public void GetMethod_NameIsEmpty ()
        {
            var target = typeof(List<int>);

            //Act
            var actual = target.GetMethod("", false);

            //Assert
            actual.Should().BeNullFixed();
        }

        [TestMethod]
        public void GetMethod_BadCaseWithSensitive ()
        {
            var target = typeof(List<int>);

            //Act
            var actual = target.GetMethod("ADD", false);

            //Assert
            actual.Should().BeNullFixed();
        }

        [TestMethod]
        public void GetMethod_BadCaseWithInsensitive ()
        {
            var target = typeof(List<int>);

            //Act
            var actual = target.GetMethod("ADD", true);

            var s = actual.Should();
            var r = Object.ReferenceEquals(s.Subject, null);

            //Assert
            actual.Should().NotBeNullFixed();
        }
        #endregion
        
        #region GetPublicMethod

        [TestMethod]
        public void GetPublicMethod_MethodExists ()
        {
            var target = typeof(List<int>);
            var expected = target.GetMethod("Add");

            //Act
            var actual = target.GetPublicMethod(expected.Name);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetPublicMethod_MethodDoesNotExist ()
        {
            var target = typeof(List<int>);

            //Act
            var actual = target.GetPublicMethod("blah");

            //Assert
            actual.Should().BeNullFixed();
        }

        [TestMethod]
        public void GetPublicMethod_NameIsNull ()
        {
            var target = typeof(List<int>);

            //Act
            var actual = target.GetPublicMethod(null);

            //Assert
            actual.Should().BeNullFixed();
        }

        [TestMethod]
        public void GetPublicMethod_NameIsEmpty ()
        {
            var target = typeof(List<int>);

            //Act
            var actual = target.GetPublicMethod("");

            //Assert
            actual.Should().BeNullFixed();
        }

        [TestMethod]
        public void GetPublicMethod_BadCaseWithSensitive ()
        {
            var target = typeof(List<int>);

            //Act
            var actual = target.GetPublicMethod("ADD");

            //Assert
            actual.Should().BeNullFixed();
        }

        [TestMethod]
        public void GetPublicMethod_BadCaseWithInsensitive ()
        {
            var target = typeof(List<int>);

            //Act
            var actual = target.GetPublicMethod("ADD", true);

            //Assert
            actual.Should().NotBeNullFixed();
        }

        [TestMethod]
        public void GetPublicMethod_MethodIsNotPublic ()
        {
            var target = typeof(List<int>);

            //Act
            var actual = target.GetPublicMethod("MemberwiseClone", true);

            //Assert
            actual.Should().BeNullFixed();
        }
        #endregion

        #region GetPublicOrProtectedMethod

        [TestMethod]
        public void GetPublicOrProtectedMethod_MethodExists ()
        {
            var target = typeof(List<int>);
            var expected = target.GetMethod("Add");

            //Act
            var actual = target.GetPublicOrProtectedMethod(expected.Name);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetPublicOrProtectedMethod_MethodDoesNotExist ()
        {
            var target = typeof(List<int>);

            //Act
            var actual = target.GetPublicOrProtectedMethod("blah");

            //Assert
            actual.Should().BeNullFixed();
        }

        [TestMethod]
        public void GetPublicOrProtectedMethod_NameIsNull ()
        {
            var target = typeof(List<int>);

            //Act
            var actual = target.GetPublicOrProtectedMethod(null);

            //Assert
            actual.Should().BeNullFixed();
        }

        [TestMethod]
        public void GetPublicOrProtectedMethod_NameIsEmpty ()
        {
            var target = typeof(List<int>);

            //Act
            var actual = target.GetPublicOrProtectedMethod("");

            //Assert
            actual.Should().BeNullFixed();
        }

        [TestMethod]
        public void GetPublicOrProtectedMethod_BadCaseWithSensitive ()
        {
            var target = typeof(List<int>);

            //Act
            var actual = target.GetPublicOrProtectedMethod("ADD");

            //Assert
            actual.Should().BeNullFixed();
        }

        [TestMethod]
        public void GetPublicOrProtectedMethod_BadCaseWithInsensitive ()
        {
            var target = typeof(List<int>);

            //Act
            var actual = target.GetPublicOrProtectedMethod("MemberwiseCLONE", true);

            //Assert
            actual.Should().NotBeNullFixed();
        }

        [TestMethod]
        public void GetPublicOrProtectedMethod_MethodIsProtected ()
        {
            var target = typeof(List<int>);
            var expected = target.GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase);

            //Act
            var actual = target.GetPublicOrProtectedMethod(expected.Name);

            //Assert
            actual.Should().Be(expected);
        }
        #endregion
    }
}
