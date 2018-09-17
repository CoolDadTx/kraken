using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Text.Substitution;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.Text.Substitution
{
    [TestClass]
    public class ObjectTextSubstitutionRuleTests : UnitTest
    {
        #region Tests

        #region Ctor

        [TestMethod]
        public void Ctor_UsingNullObject ()
        {
            Action action = () => new ObjectTextSubstitutionRule<SimpleType>(null);

            action.Should().Throw<ArgumentNullException>();
        }
        #endregion

        #region CanProcess

        [TestMethod]
        public void CanProcess_SimpleTypeValidMember ()
        {
            //Arrange
            var value = new SimpleType();
            var target = new ObjectTextSubstitutionRule<SimpleType>(value);
            var context = new TextSubstitutionContext(new TextSubstitutionOptions(), "Int32Value");

            //Act
            var actual = target.CanProcess(context);
            
            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void CanProcess_SimpleTypeNoSuchMember ()
        {
            //Arrange
            var value = new SimpleType();
            var target = new ObjectTextSubstitutionRule<SimpleType>(value);
            var context = new TextSubstitutionContext(new TextSubstitutionOptions(), "NonValue");

            //Act
            var actual = target.CanProcess(context);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void CanProcess_SimpleTypeWithField ()
        {
            //Arrange
            var value = new SimpleType();
            var target = new ObjectTextSubstitutionRule<SimpleType>(value);
            var context = new TextSubstitutionContext(new TextSubstitutionOptions(), "DoubleField");

            //Act
            var actual = target.CanProcess(context);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void CanProcess_NestedTypeValidMember ()
        {
            //Arrange
            var value = new NestedType();
            var target = new ObjectTextSubstitutionRule<NestedType>(value);
            var context = new TextSubstitutionContext(new TextSubstitutionOptions(), "SimpleValue.StringValue");

            //Act
            var actual = target.CanProcess(context);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void CanProcess_NestedTypeNoSuchMember ()
        {
            //Arrange
            var value = new SimpleType();
            var target = new ObjectTextSubstitutionRule<SimpleType>(value);
            var context = new TextSubstitutionContext(new TextSubstitutionOptions(), "SimpleValue.NonValue");

            //Act
            var actual = target.CanProcess(context);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void CanProcess_NestedTypeWithField ()
        {
            //Arrange
            var value = new NestedType();
            var target = new ObjectTextSubstitutionRule<NestedType>(value);
            var context = new TextSubstitutionContext(new TextSubstitutionOptions(), "SimpleValue.DoubleField");

            //Act
            var actual = target.CanProcess(context);

            //Assert
            actual.Should().BeTrue();
        }
        #endregion

        #region Process

        [TestMethod]
        public void Process_UsingSimpleTypeWithString ()
        {
            //Arrange
            var value = new SimpleType() { Int32Value = 1, StringValue = "First" };
            var expected = "First";
            var context = new TextSubstitutionContext(new TextSubstitutionOptions(), "StringValue");
            var target = new ObjectTextSubstitutionRule<SimpleType>(value);

            //Act
            var actual = target.Process(context);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Process_UsingSimpleTypeWithNonstring ()
        {
            //Arrange
            var value = new SimpleType() { Int32Value = 1, StringValue = "First" };
            var expected = "1";
            var context = new TextSubstitutionContext(new TextSubstitutionOptions(), "Int32Value");
            var target = new ObjectTextSubstitutionRule<SimpleType>(value);

            //Act
            var actual = target.Process(context);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Process_UsingSimpleTypeWithWrongCaseShouldWork ()
        {
            //Arrange
            var value = new SimpleType() { Int32Value = 1, StringValue = "First" };
            var expected = "1";
            var context = new TextSubstitutionContext(new TextSubstitutionOptions(), "int32VALUE");
            var target = new ObjectTextSubstitutionRule<SimpleType>(value);

            //Act
            var actual = target.Process(context);

            //Assert
            actual.Should().Be(expected);
        }
        
        [TestMethod]
        public void Process_UsingNestedType ()
        {
            //Arrange
            var expected = "Second";
            var value = new NestedType()
            {
                SimpleValue = new SimpleType() { StringValue = expected, Int32Value = 2 },
                DateTimeValue = new DateTime(2011, 10, 25, 1, 2, 3)
            };
            var context = new TextSubstitutionContext(new TextSubstitutionOptions(), "SimpleValue.StringValue");
            var target = new ObjectTextSubstitutionRule<NestedType>(value);

            //Act
            var actual = target.Process(context);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Process_UsingNestedTypeNonString ()
        {
            //Arrange
            var expected = 2011;
            var value = new NestedType()
            {
                SimpleValue = new SimpleType() { StringValue = "second", Int32Value = 2 },
                DateTimeValue = new DateTime(expected, 10, 25, 1, 2, 3)
            };
            var context = new TextSubstitutionContext(new TextSubstitutionOptions(), "DateTimeValue.Year");
            var target = new ObjectTextSubstitutionRule<NestedType>(value);

            //Act
            var actual = target.Process(context);

            //Assert
            actual.Should().Be(expected.ToString());
        }

        [TestMethod]
        public void Process_UsingNestedTypeWithWrongCaseShouldWork ()
        {
            //Arrange
            var expected = 2011;
            var value = new NestedType()
            {
                SimpleValue = new SimpleType() { StringValue = "second", Int32Value = 2 },
                DateTimeValue = new DateTime(expected, 10, 25, 1, 2, 3)
            };
            var context = new TextSubstitutionContext(new TextSubstitutionOptions(), "DateTimeValue.yEAR");
            var target = new ObjectTextSubstitutionRule<NestedType>(value);

            //Act
            var actual = target.Process(context);

            //Assert
            actual.Should().Be(expected.ToString());
        }

        [TestMethod]
        public void Process_UsingNestedTypeWithNullMemberReturnsEmpty ()
        {
            //Arrange
            var value = new NestedType();
            var context = new TextSubstitutionContext(new TextSubstitutionOptions(), "SimpleValue.StringValue");
            var target = new ObjectTextSubstitutionRule<NestedType>(value);

            //Act
            var actual = target.Process(context);

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void Process_UsingSimpleTypeWithBadMember ()
        {
            //Arrange
            var value = new SimpleType();
            var context = new TextSubstitutionContext(new TextSubstitutionOptions(), "BogusValue");
            var target = new ObjectTextSubstitutionRule<SimpleType>(value);

            //Act
            var actual = target.Process(context);

            //Assert
            actual.Should().BeNull();
        }

        [TestMethod]
        public void Process_ComplexTypeAsFinalValueCallsToString ()
        {
            //Arrange
            var value = new NestedType() { SimpleValue = new SimpleType() };
            var context = new TextSubstitutionContext(new TextSubstitutionOptions(), "SimpleValue");
            var target = new ObjectTextSubstitutionRule<NestedType>(value);

            //Act
            var actual = target.Process(context);

            //Assert
            actual.Should().Be(SimpleType.SimpleTypeStringName);
        }

        [TestMethod]
        public void Process_ComplexTypeWithNullReferenceMemberReturnsEmptyString ()
        {
            //Arrange
            var value = new NestedType();
            var context = new TextSubstitutionContext(new TextSubstitutionOptions(), "SimpleValue");
            var target = new ObjectTextSubstitutionRule<NestedType>(value);

            //Act
            var actual = target.Process(context);

            //Assert
            actual.Should().BeEmpty();
        }
        #endregion
        
        #endregion

        #region Private Members

        public class SimpleType
        {
            public string StringValue { get; set; }
            public int Int32Value { get; set; }

            public Double DoubleField;

            public const string SimpleTypeStringName = "A Simple Type";

            public override string ToString ()
            {
                return SimpleTypeStringName;
            }
        }

        public class NestedType
        {
            public SimpleType SimpleValue { get; set; }
            public DateTime DateTimeValue { get; set; }

            public bool BooleanField;
        }        
        #endregion
    }
}
