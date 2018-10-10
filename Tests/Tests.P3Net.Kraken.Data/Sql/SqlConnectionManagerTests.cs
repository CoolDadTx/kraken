/*
 * Copyright © 2018 Federation of State Medical Boards
 * All Rights Reserved
 */
using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken.Data.Sql;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.Data.Sql
{
    [TestClass]
    public class SqlConnectionManagerTests : UnitTest
    {
        #region Ctor

        [TestMethod]
        public void Ctor_Default ()
        {
            var target = new SqlConnectionManager();

            target.ConnectionString.Should().BeEmpty();
        }

#pragma warning disable 618
        [TestMethod]
        public void Ctor_WithConnectionStringWorks ()
        {
            var expectedString = "Server=server1;Database=DB1";

            var target = new SqlConnectionManager(expectedString);

            target.ConnectionString.Should().Be(expectedString);
        }

        [TestMethod]
        public void Ctor_WithConnectionStringNullFails ()
        {
            Action a = () => new SqlConnectionManager(null);

            a.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Ctor_WithConnectionStringEmptyFails ()
        {
            Action a = () => new SqlConnectionManager("");

            a.Should().Throw<ArgumentException>();
        }
#pragma warning disable 618
        #endregion

        #region FormatParameterName

        [TestMethod]
        public void FormatParameterName_NotAlreadyFormatted ()
        {
            var name = "FirstName";
            var expected = $"@{name}";

            var target = new TestSqlConnectionManager();
            var actual = target.GetParameterName(name);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void FormatParameterName_AlreadyFormatted ()
        {
            var name = "@FirstName";
            var expected = name;

            var target = new TestSqlConnectionManager();
            var actual = target.GetParameterName(name);

            actual.Should().Be(expected);
        }
        #endregion

        #region Private Members

        private sealed class TestSqlConnectionManager : SqlConnectionManager
        {
            public TestSqlConnectionManager () : base(@"Server=localhost;Database=Master")
            { }

            public string GetParameterName ( string originalName ) => FormatParameterName(originalName);
        }
        #endregion
    }
}
