/*
 * Copyright © 2011 Michael Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Reflection;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken.Reflection
{
    [TestClass] 
    public class CustomAttributeProviderExtensionsTests : UnitTest
    {
        #region Tests

        #region GetAttribute
        
        [TestMethod]
        public void GetAttribute_AttributeExists ()
        {
            var target = typeof(TypeWithAttribute);
            var expected = "Base";

            //Act            
            var actual = target.GetAttribute<TestCustomAttribute>();

            //Assert
            actual.Should().NotBeNull();
            actual.Message.Should().Be(expected);
        }

        [TestMethod]
        public void GetAttribute_AttributeMising ()
        {
            var target = typeof(TypeWithNoAttribute);

            //Act            
            var actual = target.GetAttribute<TestCustomAttribute>();

            //Assert
            actual.Should().BeNull();
        }

        [TestMethod]
        public void GetAttribute_IncludeInherited_AttributeExistsOnBase ()
        {
            var target = typeof(TypeWithNoAttributeBaseWithAttribute);
            var expected = "Base";

            //Act            
            var actual = target.GetAttribute<TestCustomAttribute>(true);

            //Assert
            actual.Should().NotBeNull();
            actual.Message.Should().Be(expected);
        }

        [TestMethod]
        public void GetAttribute_IncludeInherited_AttributeExistsOnBaseAndDerived ()
        {
            var target = typeof(TypeWithAttributeBaseWithAttribute);
            var expected = "Derived";

            //Act            
            var actual = target.GetAttribute<TestCustomAttribute>(true);

            //Assert
            actual.Should().NotBeNull();
            actual.Message.Should().Be(expected);
        }

        [TestMethod]
        public void GetAttribute_IncludeInherited_AttributeNotExistsOnBaseAndExistsOnDerived ()
        {
            var target = typeof(TypeWithAttributeBaseWithNoAttribute);
            var expected = "Derived";

            //Act            
            var actual = target.GetAttribute<TestCustomAttribute>(true);

            //Assert
            actual.Should().NotBeNull();
            actual.Message.Should().Be(expected);
        }

        [TestMethod]
        public void GetAttribute_IncludeInherited_AttributeNotExistsOnBaseOrDerived ()
        {
            var target = typeof(TypeWithNoAttributeBaseWithNoAttribute);

            //Act            
            var actual = target.GetAttribute<TestCustomAttribute>(true);

            //Assert
            actual.Should().BeNull();
        }
        #endregion

        #region GetAttributes

        [TestMethod]
        public void GetAttributes_TypeHasNoAttributes ()
        {
            var target = typeof(TypeWithNoAttribute);

            //Act
            var actual = target.GetAttributes<TestCustomAttribute>();

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void GetAttributes_TypeWithMultipleAttributes ()
        {
            var target = typeof(TypeWithMultipleAttributesBaseWithNoAttribute);
            
            //Act
            var actual = target.GetAttributes<TestCustomAttribute>();

            //Assert
            actual.Should().HaveCount(2);
            actual.Should().Contain(x => x.Message == "Derived1");
            actual.Should().Contain(x => x.Message == "Derived2");
        }

        [TestMethod]
        public void GetAttributes_IncludeInherited_TypeWithMultipleAttributes ()
        {
            var target = typeof(TypeWithMultipleAttributesBaseWithNoAttribute);

            //Act
            var actual = target.GetAttributes<TestCustomAttribute>(true);

            //Assert
            actual.Should().HaveCount(2);
            actual.Should().Contain(x => x.Message == "Derived1");
            actual.Should().Contain(x => x.Message == "Derived2");
        }

        [TestMethod]
        public void GetAttributes_IncludeInherited_TypeWithMultipleAttributesAndBaseWithAttributes ()
        {
            var target = typeof(TypeWithMultipleAttributesBaseWithAttribute);

            //Act
            var actual = target.GetAttributes<TestCustomAttribute>(true);

            //Assert
            actual.Should().HaveCount(3);
            actual.Should().Contain(x => x.Message == "Derived1");
            actual.Should().Contain(x => x.Message == "Derived2");
            actual.Should().Contain(x => x.Message == "Base");
        }

        [TestMethod]
        public void GetAttributes_IncludeInherited_TypeWithMultipleAttributesAndBaseWithMultipleAttributes ()
        {
            var target = typeof(TypeWithMultipleAttributesBaseWithMultipleAttributes);

            //Act
            var actual = target.GetAttributes<TestCustomAttribute>(true);

            //Assert
            actual.Should().HaveCount(4);
            actual.Should().Contain(x => x.Message == "Derived1");
            actual.Should().Contain(x => x.Message == "Derived2");
            actual.Should().Contain(x => x.Message == "Base1");
            actual.Should().Contain(x => x.Message == "Base2");
        }

        [TestMethod]
        public void GetAttributes_IncludeInherited_TypeWithNoAttributesAndBaseWithMultipleAttributes ()
        {
            var target = typeof(TypeWithNoAttributeBaseWithMultipleAttributes);

            //Act
            var actual = target.GetAttributes<TestCustomAttribute>(true);

            //Assert
            actual.Should().HaveCount(2);
            actual.Should().Contain(x => x.Message == "Base1");
            actual.Should().Contain(x => x.Message == "Base2");
        }
        #endregion
        
        #region HasAttribute

        [TestMethod]
        public void HasAttribute_AttributeExists ()
        {
            var target = typeof(TypeWithAttribute);            

            //Act                        
            var actual = target.HasAttribute<TestCustomAttribute>();

            //Assert
            actual.Should().BeTrue();            
        }

        [TestMethod]
        public void HasAttribute_AttributeMising ()
        {
            var target = typeof(TypeWithNoAttribute);

            //Act            
            var actual = target.HasAttribute<TestCustomAttribute>();

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void HasAttribute_IncludeInherited_AttributeExistsOnBase ()
        {
            var target = typeof(TypeWithNoAttributeBaseWithAttribute);
            
            //Act            
            var actual = target.HasAttribute<TestCustomAttribute>(true);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void HasAttribute_IncludeInherited_AttributeExistsOnBaseAndDerived ()
        {
            var target = typeof(TypeWithAttributeBaseWithAttribute);

            //Act            
            var actual = target.HasAttribute<TestCustomAttribute>(true);

            //Assert
            actual.Should().BeTrue();
        }        
        #endregion

        #endregion

        #region Private Members

        [AttributeUsage(AttributeTargets.All, AllowMultiple=true, Inherited=true)]
        private class TestCustomAttribute : Attribute
        {        
            public TestCustomAttribute ( string message )
            { Message = message; }

            public string Message { get; private set; }
        }

        private class TypeWithNoAttribute 
        {             
        }

        [TestCustom("Base")]
        private class TypeWithAttribute
        {             
        }

        [TestCustom("Base1")]
        [TestCustom("Base2")]
        private class TypeWithMultipleAttributes
        {
        }

        private class TypeWithNoAttributeBaseWithNoAttribute : TypeWithNoAttribute
        { }

        private class TypeWithNoAttributeBaseWithAttribute : TypeWithAttribute
        { }

        [TestCustom("Derived")]
        private class TypeWithAttributeBaseWithNoAttribute : TypeWithNoAttribute
        { }

        [TestCustom("Derived")]
        private class TypeWithAttributeBaseWithAttribute : TypeWithAttribute
        { }

        [TestCustom("Derived1")]
        [TestCustom("Derived2")]
        private class TypeWithMultipleAttributesBaseWithNoAttribute : TypeWithNoAttribute
        {
        }

        [TestCustom("Derived1")]
        [TestCustom("Derived2")]
        private class TypeWithMultipleAttributesBaseWithAttribute : TypeWithAttribute
        {
        }

        private class TypeWithNoAttributeBaseWithMultipleAttributes : TypeWithMultipleAttributes
        {
        }

        [TestCustom("Derived1")]
        [TestCustom("Derived2")]
        private class TypeWithMultipleAttributesBaseWithMultipleAttributes : TypeWithMultipleAttributes
        {
        }
        #endregion
    }
}
