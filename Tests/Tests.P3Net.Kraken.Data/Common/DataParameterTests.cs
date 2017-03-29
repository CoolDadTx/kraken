#region Imports

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using P3Net.Kraken.Data.Common;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken.Data.Common
{
    [TestClass]
    public class DataParameterTests : UnitTest
    {
        #region Ctor
        
        [TestMethod]
        public void Ctor_WithName ()
        {
            //Act
            var target = new DataParameter("@p1", DbType.Int32);

            //Assert
            target.Name.Should().Be("@p1");
            target.DbType.Should().Be(DbType.Int32);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_NameIsEmpty ()
        {
            //Act
            new DataParameter("", DbType.Int32);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_NameIsNull ()
        {
            //Act
            new DataParameter(null, DbType.Int32);
        }
        #endregion

        #region Name
        
        [TestMethod]
        public void Name_SetValid ()
        {
            var expected = "in3";

            //Act
            var target = new DataParameter("p1", DbType.Int32);
            target.Name = expected;

            //Assert
            target.Name.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Name_SetNull ()
        {
            var target = new DataParameter("p1", DbType.Int32);
            target.Name = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Name_SetEmpty ()
        {
            var target = new DataParameter("p1", DbType.Int32);
            target.Name = "";
        }

        [TestMethod]
        public void Name_FormattingRemains ()
        {
            var expected = "@p1";

            //Act
            var target = new DataParameter(expected, DbType.String);

            //Assert
            target.Name.Should().Be(expected);
        }
        #endregion

        #region Precision

        [TestMethod]
        public void Precision_SetValidSingle ()
        {
            int expected = 12;

            //Act
            var target = new DataParameter("p1", DbType.Single);
            target.Precision = expected;

            //Assert
            target.HasPrecision.Should().BeTrue();
            target.Precision.Should().Be(expected);            
        }

        [TestMethod]
        public void Precision_SetValidDouble ()
        {
            int expected = 40;

            //Act
            var target = new DataParameter("p1", DbType.Double);
            target.Precision = expected;

            //Assert
            target.HasPrecision.Should().BeTrue();
            target.Precision.Should().Be(expected);
        }

        [TestMethod]
        public void Precision_SetValidOther ()
        {
            int expected = 30;

            //Act
            var target = new DataParameter("p1", DbType.Int64);
            target.Precision = expected;

            //Assert
            target.HasPrecision.Should().BeTrue();
            target.Precision.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Precision_SetSingleOutOfRange ()
        {
            int expected = 40;

            //Act
            var target = new DataParameter("p1", DbType.Single);
            target.Precision = expected;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Precision_SetDoubleOutOfRange ()
        {
            int expected = 100;

            //Act
            var target = new DataParameter("p1", DbType.Double);
            target.Precision = expected;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Precision_SetOtherOutOfRange ()
        {
            int expected = 40;

            //Act
            var target = new DataParameter("p1", DbType.Int64);
            target.Precision = expected;
        }

        [TestMethod]
        public void Precision_NotSet ()
        {
            var target = new DataParameter("p1", DbType.Int32);

            //Assert
            target.HasPrecision.Should().BeFalse();
            target.Precision.Should().Be(0);
        }
        #endregion

        #region Scale

        [TestMethod]
        public void Scale_SetValid ()
        {
            var expected = 10;

            //Act
            var target = new DataParameter("p1", DbType.Int32);
            target.Scale = expected;

            //Assert
            target.HasScale.Should().BeTrue();
            target.Scale.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Scale_SetOutOfRange ()
        {
            //Act
            var target = new DataParameter("p1", DbType.Int32);
            target.Scale = -1;
        }

        [TestMethod]
        public void Scale_NotSet ()
        {
            var target = new DataParameter("p1", DbType.Int32);

            //Assert
            target.HasScale.Should().BeFalse();
            target.Scale.Should().Be(0);
        }
        #endregion

        #region Size

        [TestMethod]
        public void Size_SetValid ()
        {
            var expected = 100;

            //Act
            var target = new DataParameter("p1", DbType.String);
            target.Size = expected;

            //Assert
            target.HasSize.Should().BeTrue();
            target.Size.Should().Be(expected);
        }

        [TestMethod]
        public void Size_SetMaximum ()
        {
            var expected = -1;

            //Act
            var target = new DataParameter("p1", DbType.String);
            target.Size = expected;

            //Assert
            target.Size.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Size_SetOutOfRange ()
        {
            //Act
            var target = new DataParameter("p1", DbType.String);
            target.Size = -2;
        }

        [TestMethod]
        public void Size_NotSet ()
        {
            var target = new DataParameter("p1", DbType.Int32);

            //Assert
            target.HasSize.Should().BeFalse();
            target.Size.Should().Be(-1);
        }
        #endregion

        #region SourceColumn

        [TestMethod]
        public void SourceColumn_IsValid ()
        {
            var expected = "command";

            //Act
            var target = new DataParameter("p1", DbType.Int32);
            target.SourceColumn = expected;

            //Assert
            target.SourceColumn.Should().Be(expected);
        }

        [TestMethod]
        public void SourceColumn_IsNull ()
        {            
            //Act
            var target = new DataParameter("p1", DbType.Int32);
            target.SourceColumn = null;

            //Assert
            target.SourceColumn.Should().BeEmpty();
        }

        [TestMethod]
        public void SourceColumn_NotSet ()
        {
            //Act
            var target = new DataParameter("p1", DbType.Int32);
            
            //Assert
            target.SourceColumn.Should().BeEmpty();
        }
        #endregion

        #region ToString

        [TestMethod]
        public void ToString_IsValid ()
        {
            var expected = "@p1";

            //Act
            var target = new DataParameter(expected, DbType.Int32);
            var actual = target.ToString();

            //Assert
            actual.Should().Be(expected);
        }
        #endregion
    }
}
