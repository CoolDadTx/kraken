using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using P3Net.Kraken.ComponentModel.DataAnnotations;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.ComponentModel.DataAnnotations
{
    [TestClass]
    public class RequiredIfAttributeTests : UnitTest
    {
        #region Ctor

        [TestMethod]
        public void Ctor_WithName ()
        {
            var expected = "Name";
            var target = new RequiredIfAttribute(expected);

            //Assert
            target.PropertyName.Should().Be(expected);
        }

        [TestMethod]
        public void Ctor_WithNull ()
        {
            Action action = () => new RequiredIfAttribute(null);

            action.Should().Throw<ArgumentNullException>();
        }
        #endregion

        #region IsValid

        [TestMethod]
        public void IsValid_IsTrue ()
        {
            var target = new TestData() { DoValidation = true, RequiredProperty = "Test" };
                        
            var context = new ValidationContext(target, null, null);
            var results = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(target, context, results);
            
            //Assert            
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsValid_ConditionIsFalse ()
        {
            var target = new TestData() { DoValidation = false, RequiredProperty = "Test" };

            var context = new ValidationContext(target, null, null);
            var results = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(target, context, results);

            //Assert            
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsValid_ValueIsNotSet ()
        {
            var target = new TestData() { DoValidation = true };

            var context = new ValidationContext(target, null, null);
            var results = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(target, context, results);

            //Assert            
            actual.Should().BeFalse();
            results.Should().HaveCount(1);
        }

        [TestMethod]
        public void IsValid_ValueIsNotSetButConditionIsFalse ()
        {
            var target = new TestData() { DoValidation = false };

            var context = new ValidationContext(target, null, null);
            var results = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(target, context, results);

            //Assert            
            actual.Should().BeTrue();
        }

        [TestMethod]        
        public void IsValid_PropertyIsBad ()
        {
            var target = new TestDataWithBadProperty() { };

            var context = new ValidationContext(target, null, null);
            var results = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(target, context, results);

            //Assert            
            actual.Should().BeFalse();
            results.Should().HaveCount(1);
        }

        [TestMethod]
        public void IsValid_WithCustomValueTrue ()
        {
            var target = new TestDataWithCustomValue() { Kind = 2, RequiredProperty = "set" };

            var context = new ValidationContext(target, null, null);
            var results = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(target, context, results);

            //Assert            
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsValid_WithCustomValueFalse ()
        {
            var target = new TestDataWithCustomValue() { Kind = 1, RequiredProperty = "set" };

            var context = new ValidationContext(target, null, null);
            var results = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(target, context, results);

            //Assert            
            actual.Should().BeTrue();        
        }

        [TestMethod]
        public void IsValid_WithCustomValueTrueAndPropertyNotSet ()
        {
            var target = new TestDataWithCustomValue() { Kind = 2 };

            var context = new ValidationContext(target, null, null);
            var results = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(target, context, results);

            //Assert            
            actual.Should().BeFalse();
            results.Should().HaveCount(1);
        }

        [TestMethod]
        public void IsValid_WithNegativeBooleanCondition ()
        {
            var target = new TestDataWithNegativeBoolean() { DoNotValidate=true };

            var context = new ValidationContext(target, null, null);
            var results = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(target, context, results);

            //Assert            
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsValid_WithStringCustomValue ()
        {
            var target = new TestDataWithStringCustomValue() { Kind = 2, RequiredProperty = "set" };

            var context = new ValidationContext(target, null, null);
            var results = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(target, context, results);

            //Assert            
            actual.Should().BeTrue();
        }
        #endregion

        #region Private Members

        private sealed class TestData
        {
            [RequiredIf("DoValidation")]
            public string RequiredProperty { get; set; }

            public bool DoValidation { get; set; }
        }

        private sealed class TestDataWithNegativeBoolean
        {
            [RequiredIf("DoNotValidate", Value=false)]
            public string RequiredProperty { get; set; }

            public bool DoNotValidate { get; set; }
        }

        private sealed class TestDataWithBadProperty
        {
            [RequiredIf("DontValidate")]
            public string RequiredWithBadProperty { get; set; }
        }

        private sealed class TestDataWithCustomValue
        {
            public int Kind { get; set; }

            [RequiredIf("Kind", Value = 2)]
            public string RequiredProperty { get; set; }
        }

        private sealed class TestDataWithStringCustomValue
        {
            public int Kind { get; set; }

            [RequiredIf("Kind", Value = "2")]
            public string RequiredProperty { get; set; }
        }
        #endregion
    }
}
