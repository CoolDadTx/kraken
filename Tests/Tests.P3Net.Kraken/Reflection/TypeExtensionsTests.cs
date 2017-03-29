using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

using P3Net.Kraken.Reflection;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.Reflection
{
    [TestClass]
    public partial class TypeExtensionsTests : UnitTest
    {
        #region GetFriendlyName

        [TestMethod]
        public void GetFriendlyName_DefaultWorks ()
        {
            var actual = this.GetType().GetFriendlyName();

            actual.Should().Be("TypeExtensionsTests");
        }

        [TestMethod]
        public void GetFriendlyName_IncludeNamespace ()
        {
            var actualType = this.GetType();
            var actual = actualType.GetFriendlyName(true);

            actual.Should().Be(actualType.Namespace + "." + actualType.Name);
        }

        [TestMethod]
        public void GetFriendlyName_IgnoreNamespace ()
        {
            var actual = this.GetType().GetFriendlyName(false);

            actual.Should().Be("TypeExtensionsTests");
        }

        [TestMethod]
        public void GetFriendlyName_WithProvider ()
        {
            var actual = this.GetType().GetFriendlyName(new UpperCaseProvider());

            actual.Should().Be(this.GetType().Name.ToUpper());
        }
        #endregion

        #region ImplementsInterface

        [TestMethod]
        public void ImplementsInterface_InterfaceImplemented ()
        {
            var target = typeof(List<int>);
            var expected = typeof(IList<int>);
            
            //Act
            var actual = target.ImplementsInterface(expected.Name);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void ImplementsInterface_InterfaceNotImplemented ()
        {
            var target = typeof(List<int>);
            var expected = typeof(IComparable);

            //Act
            var actual = target.ImplementsInterface(expected.Name);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void ImplementsInterface_InterfaceIsBad ()
        {
            var target = typeof(List<int>);

            //Act
            var actual = target.ImplementsInterface("abc");

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void ImplementsInterface_NameIsNull ()
        {
            var target = typeof(List<int>);

            //Act
            var actual = target.ImplementsInterface(null);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void ImplementsInterface_NameIsEmpty ()
        {
            var target = typeof(List<int>);

            //Act
            var actual = target.ImplementsInterface("");

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void ImplementsInterface_BadCaseWithDefault ()
        {
            var target = typeof(List<int>);

            //Act
            var actual = target.ImplementsInterface("ILIST<int>");

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void ImplementsInterface_BadCaseWithInsensitive ()
        {
            var target = typeof(List<int>);

            //Act
            var actual = target.ImplementsInterface("ILIST", true);

            //Assert
            actual.Should().BeTrue();
        }
        #endregion

        #region Private Members

        private sealed class UpperCaseProvider : TypeNameProvider
        {
            protected override string FormatSimpleType ( Type type )
            {
                return type.Name.ToUpper();
            }

            protected override string FormatArrayType ( Type elementType, int dimensions )
            {
                throw new NotImplementedException();
            }

            protected override string FormatByRefType ( Type type )
            {
                throw new NotImplementedException();
            }

            protected override string FormatClosedGenericType ( Type baseType, Type[] typeArguments )
            {
                throw new NotImplementedException();
            }

            protected override string FormatNullableType ( Type type )
            {
                throw new NotImplementedException();
            }

            protected override string FormatPointerType ( Type type )
            {
                throw new NotImplementedException();
            }
        }
        #endregion
    }
}
