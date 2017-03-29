using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken
{    
    partial class StringExtensionsTests : UnitTest
    {
        #region Enumerable

        [TestMethod]
        public void Coalesce_Enumerable_FirstIsNotNull ()
        {
            var expected = "Test";
            var target = new List<string> { expected, "One", "Two" };
            var actual = StringExtensions.Coalesce(target);
            
            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Coalesce_Enumerable_FirstIsNull ()
        {
            var expected = "Test";
            var target = new List<string> { null, null, expected };
            var actual = StringExtensions.Coalesce(target);

            //Assert         
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Coalesce_Enumerable_AllAreNull ()
        {
            var target = new List<string> { null, null };
            var actual = StringExtensions.Coalesce(target);

            //Assert
            actual.Should().BeNull();
        }

        [TestMethod]
        public void Coalesce_Enumerable_IncludeEmpty ()
        {
            var target = new List<string> { null, "", "Test" };
            var actual = StringExtensions.Coalesce(target);

            //Assert         
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void Coalesce_EnumerableSkipEmpty_IncludeEmpty ()
        {
            var expected = "Test";
            var target = new List<string> { null, "", expected };
            var actual = StringExtensions.Coalesce(StringCoalesceOptions.SkipEmpty, target);

            //Assert         
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Coalesce_EnumerableSkipEmpty_AllAreEmpty ()
        {
            var target = new List<string> { "", "" };
            var actual = StringExtensions.Coalesce(StringCoalesceOptions.SkipEmpty, target);

            //Assert
            actual.Should().BeNull();
        }
        #endregion

        #region VarArgs

        [TestMethod]
        public void Coalesce_VarArgs_FirstIsNotNull ()
        {
            var expected = "Test";
            var actual = StringExtensions.Coalesce(expected, "One", "Two");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Coalesce_VarArgs_FirstIsNull ()
        {
            var expected = "Test";
            var actual = StringExtensions.Coalesce(null, null, expected);

            //Assert         
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Coalesce_VarArgs_AllAreNull ()
        {
            var actual = StringExtensions.Coalesce(null, null);

            //Assert
            actual.Should().BeNull();
        }

        [TestMethod]
        public void Coalesce_VarArgs_IncludeEmpty ()
        {
            var actual = StringExtensions.Coalesce(null, "", "Test");

            //Assert         
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void Coalesce_VarArgsSkipEmpty_IncludeEmpty ()
        {
            var expected = "Test";
            var actual = StringExtensions.Coalesce(StringCoalesceOptions.SkipEmpty, null, "", expected);

            //Assert         
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Coalesce_VarArgsSkipEmpty_AllAreEmpty ()
        {
            var actual = StringExtensions.Coalesce(StringCoalesceOptions.SkipEmpty, "", "");

            //Assert
            actual.Should().BeNull();
        }
        #endregion
    }
}
