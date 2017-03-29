using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

using P3Net.Kraken.Diagnostics;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.Diagnostics
{
    [TestClass]
    public class AndArgumentConstraintTests
    {
        [TestMethod]
        public void Ctor_WithArgument ()
        {
            var expectedArg = new Argument("test").WithValue(10);

            var target = new AndArgumentConstraint<int>(expectedArg);
            var actual = target.And;

            //We won't assume the arg or constraints objects are the same
            actual.Argument.Name.Should().Be(expectedArg.Argument.Name);
            actual.Argument.Value.Should().Be(expectedArg.Argument.Value);
        }

        [TestMethod]        
        public void Ctor_WithNullArgument ()
        {
            Action work = () => new AndArgumentConstraint<int>(null);

            work.ShouldThrowArgumentNullException();
        }
    }
}
