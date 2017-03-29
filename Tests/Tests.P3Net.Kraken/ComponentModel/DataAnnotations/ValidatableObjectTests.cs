using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.ComponentModel.DataAnnotations;


namespace Tests.P3Net.Kraken.ComponentModel.DataAnnotations
{
    [TestClass]
    public class ValidatableObjectTests
    {
        [TestMethod]
        public void Validate_IsValid ()
        {
            var target = new TestValidatableObject();
            target.ValidationAction = ctx => null;

            var actual = target.Validate(new ValidationContext(target, null, null));

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void Validate_IsInvalid ()
        {
            var expectedResult = new ValidationResult("Some error");

            var target = new TestValidatableObject();
            target.ValidationAction = ctx => Enumerable.Repeat(expectedResult, 1);

            var actual = target.Validate(new ValidationContext(target, null, null));

            //Assert
            actual.Should().HaveCount(1);
            actual.First().ErrorMessage.Should().Be(expectedResult.ErrorMessage);
        }

        [TestMethod]
        public void Validate_CallsValidateCore ()
        {
            var target = new TestValidatableObject();
            
            var actual = target.Validate(new ValidationContext(target, null, null));

            //Assert
            actual.Should().HaveCount(0);
        }

        #region Private Members

        private sealed class TestValidatableObject : ValidatableObject
        {
            public Func<ValidationContext, IEnumerable<ValidationResult>> ValidationAction { get; set; }

            protected override IEnumerable<ValidationResult> ValidateCore ( ValidationContext context )
            {
                if (ValidationAction != null)
                    return ValidationAction(context);
                
                return base.ValidateCore(context);
            }
        }
        #endregion
    }
}
