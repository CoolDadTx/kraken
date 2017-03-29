using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using P3Net.Kraken.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Tests.P3Net.Kraken.ComponentModel.DataAnnotations
{
    [TestClass]
    public class ValidationResultsTests : UnitTest
    {
        #region Ctor
        
        [TestMethod]
        public void Ctor_WithResults ()
        {
            var expected = new List<ValidationResult>() { new ValidationResult("Error") };

            var target = new ValidationResults(expected);

            target.Results.Should().HaveCount(1);
            target.Succeeded.Should().BeFalse();
        }

        [TestMethod]
        public void Ctor_WithNoResults ()
        {
            var target = new ValidationResults(null);

            target.Succeeded.Should().BeTrue();
        }
        #endregion
    }
}
