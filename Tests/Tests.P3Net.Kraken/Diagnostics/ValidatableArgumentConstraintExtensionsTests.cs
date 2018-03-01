using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

using P3Net.Kraken.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Tests.P3Net.Kraken.Diagnostics
{
    [TestClass]
    public class ValidatableArgumentConstraintExtensionsTests
    {
        [TestMethod]
        public void IsValid_IsTrue ()
        {
            var target = new ArgumentConstraint<IValidatableObject>(new Argument<IValidatableObject>("arg", new TestValidation(true)));

            target.IsValid();
        }

        [TestMethod]
        public void IsValid_IsFalse ()
        {
            var target = new ArgumentConstraint<IValidatableObject>(new Argument<IValidatableObject>("arg", new TestValidation(false)));

            Action action = () => target.IsValid();

            action.Should().Throw<ValidationException>();
        }

        #region Private Members

        private sealed class TestValidation : IValidatableObject
        {
            public TestValidation ( bool isValid )
            {
                m_isValid = isValid;
            }

            public IEnumerable<ValidationResult> Validate ( ValidationContext context )
            {
                if (!m_isValid)
                    yield return new ValidationResult("Validation failed.");
            }

            private readonly bool m_isValid;
        }
        #endregion
    }
}
