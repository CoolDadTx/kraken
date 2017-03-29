using System;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

using P3Net.Kraken;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken
{
    [TestClass]
    public partial class ByteSizeTests : UnitTest
    {
        #region CompareTo

        [TestMethod]
        public void CompareTo_ValueIsSmaller ()
        {
            var leftValue = new ByteSize(1000);
            var rightValue = new ByteSize(1001);

            //Act
            var actual = leftValue.CompareTo(rightValue);

            //Assert
            actual.Should().BeLessThan(0);
        }

        [TestMethod]
        public void CompareTo_ValueIsLarger ()
        {
            var leftValue = new ByteSize(1002);
            var rightValue = new ByteSize(1001);

            //Act
            var actual = leftValue.CompareTo(rightValue);

            //Assert
            actual.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void CompareTo_ValuesAreEqual ()
        {
            var leftValue = new ByteSize(1000);
            var rightValue = new ByteSize(1000);

            //Act
            var actual = leftValue.CompareTo(rightValue);

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void LessThan_Simple ()
        {
            var leftValue = new ByteSize(1000);
            var rightValue = new ByteSize(1001);

            //Act
            var actual = leftValue < rightValue;

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void GreaterThan_Simple ()
        {
            var leftValue = new ByteSize(1000);
            var rightValue = new ByteSize(1001);

            //Act
            var actual = leftValue > rightValue;

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void LessThanOrEqual_Simple ()
        {
            var leftValue = new ByteSize(1000);
            var rightValue = new ByteSize(1000);
        
            //Act
            var actual = leftValue <= rightValue;

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void GreaterThanOrEqual_Simple ()
        {
            var leftValue = new ByteSize(1000);
            var rightValue = new ByteSize(1001);

            //Act
            var actual = leftValue >= rightValue;

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region Conversion...

        [TestMethod]
        public void ToBytes_WithValue ()
        {
            var expected = 1234;

            //Act
            var target = new ByteSize(expected);
            var actual = target.Bytes;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToGigaBytes_WithValue ()
        {
            var expected = 100;

            //Act
            var target = ByteSize.FromGigabytes(expected);
            var actual = target.Gigabytes;

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        public void ToKiloBytes_WithValue ()
        {
            var expected = 100;

            //Act
            var target = ByteSize.FromKilobytes(expected);
            var actual = target.Kilobytes;

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        public void ToMegaBytes_WithValue ()
        {
            var expected = 100;

            //Act
            var target = ByteSize.FromMegabytes(expected);
            var actual = target.Megabytes;

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        public void ToTeraBytes_WithValue ()
        {
            var expected = 100;

            //Act
            var target = ByteSize.FromTerabytes(expected);
            var actual = target.Terabytes;

            //Assert
            actual.Should().BeApproximately(expected);
        }
        #endregion

        #region From...

        [TestMethod]
        public void FromBytes_WithValue ()
        {
            var expected = 1000;

            //Act
            var target = ByteSize.FromBytes(expected);
            var actual = target.Bytes;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void FromKiloBytes_WithValue ()
        {
            var expected = 1234;

            //Act
            var target = ByteSize.FromKilobytes(expected);
            var actual = target.Bytes;

            //Assert
            actual.Should().Be((long)(expected * ByteSize.BytesInKilobytes));
        }

        [TestMethod]
        public void FromGigaBytes_WithValue ()
        {
            var expected = 1234;

            //Act
            var target = ByteSize.FromGigabytes(expected);
            var actual = target.Bytes;

            //Assert
            actual.Should().Be((long)(expected * ByteSize.BytesInGigabytes));
        }

        [TestMethod]
        public void FromMegaBytes_WithValue ()
        {
            var expected = 1234;

            //Act
            var target = ByteSize.FromMegabytes(expected);
            var actual = target.Bytes;

            //Assert
            actual.Should().Be((long)(expected * ByteSize.BytesInMegabytes));
        }

        [TestMethod]
        public void FromTeraBytes_WithValue ()
        {
            var expected = 1234;

            //Act
            var target = ByteSize.FromTerabytes(expected);
            var actual = target.Bytes;

            //Assert
            actual.Should().Be((long)(expected * ByteSize.BytesInTerabytes));
        }
        #endregion

        #region Infrastructure

        [TestMethod]
        public void Equality_WithSameValues ()
        {
            var target1 = new ByteSize(1000);
            var target2 = new ByteSize(1000);

            //Act
            var actual = target1 == target2;

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Equality_WithDifferentValues ()
        {
            var target1 = new ByteSize(1000);
            var target2 = new ByteSize(1024);

            //Act
            var actual = target1 == target2;

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void EqualityMethod_WithSameValues ()
        {
            var target1 = new ByteSize(1000);
            var target2 = new ByteSize(1000);

            //Act
            var actual = target1.Equals(target2);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Inequality_WithSameValues ()
        {
            var target1 = new ByteSize(1000);
            var target2 = new ByteSize(1000);

            //Act
            var actual = target1 != target2;

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void Inequality_WithDifferentValues ()
        {
            var target1 = new ByteSize(1000);
            var target2 = new ByteSize(1024);

            //Act
            var actual = target1 != target2;

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void GetHashCode_WithSameValues ()
        {
            var target1 = new ByteSize(1000);
            var target2 = new ByteSize(1000);

            //Act
            var actual = target1.GetHashCode() == target2.GetHashCode();

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void GetHashCode_WithDifferentValues ()
        {
            var target1 = new ByteSize(1000);
            var target2 = new ByteSize(1024);

            //Act
            var actual = target1.GetHashCode() != target2.GetHashCode();

            //Assert
            actual.Should().BeTrue();
        }
        #endregion

        #region Math

        #region Addition
        
        [TestMethod]
        public void Add_Value ()
        {
            var expected = 1025;
            var left = new ByteSize(1000);
            var right = new ByteSize(25);
            
            //Act
            var updated = left.Add(right);
            var actual = updated.Bytes;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AddBytes_Value ()
        {
            var expected = 1025;
            var left = new ByteSize(1000);
            var right = 25;

            //Act
            var updated = left.AddBytes(right);
            var actual = updated.Bytes;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AddGigaBytes_Value ()
        {
            var expected = (1024 * 1024 * 1024) + 1000;
            var left = new ByteSize(1000);
            var right = 1;

            //Act
            var updated = left.AddGigabytes(right);
            var actual = updated.Bytes;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AddKiloBytes_Value ()
        {
            var expected = 2024;
            var left = new ByteSize(1000);
            var right = 1;

            //Act
            var updated = left.AddKilobytes(right);
            var actual = updated.Bytes;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AddMegaBytes_Value ()
        {
            var expected = (1024 * 1024) + 1000;
            var left = new ByteSize(1000);
            var right = 1;

            //Act
            var updated = left.AddMegabytes(right);
            var actual = updated.Bytes;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void AddTeraBytes_Value ()
        {
            var expected = (1024L * 1024 * 1024 * 1024) + 1000;
            var left = new ByteSize(1000);
            var right = 1;

            //Act
            var updated = left.AddTerabytes(right);
            var actual = updated.Bytes;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void OpAdd_Value ()
        {
            var expected = 1025;
            var left = new ByteSize(1000);
            var right = new ByteSize(25);

            //Act
            var updated = left + right;
            var actual = updated.Bytes;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void OpAdd_ValueIsInteger ()
        {
            var expected = 1025;
            var left = new ByteSize(1000);
            var right = 25;

            //Act
            var updated = left + right;
            var actual = updated.Bytes;

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region Subtraction

        [TestMethod]
        public void Subtract_Value ()
        {
            var expected = 975;
            var left = new ByteSize(1000);
            var right = new ByteSize(25);

            //Act
            var updated = left.Subtract(right);
            var actual = updated.Bytes;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void SubtractBytes_Value ()
        {
            var expected = 975;
            var left = new ByteSize(1000);
            var right = 25;

            //Act
            var updated = left.SubtractBytes(right);
            var actual = updated.Bytes;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void SubtractGigaBytes_Value ()
        {
            var expected = (1024L * 1024 * 1024) * 9;
            var left = ByteSize.FromGigabytes(10);
            var right = 1;

            //Act
            var updated = left.SubtractGigabytes(right);
            var actual = updated.Bytes;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void SubtractKiloBytes_Value ()
        {
            var expected = 1;
            var left = new ByteSize(1025);
            var right = 1;

            //Act
            var updated = left.SubtractKilobytes(right);
            var actual = updated.Bytes;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void SubtractMegaBytes_Value ()
        {
            var expected = (1024 * 1024) * 9;
            var left = ByteSize.FromMegabytes(10);
            var right = 1;

            //Act
            var updated = left.SubtractMegabytes(right);
            var actual = updated.Bytes;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void SubtractTeraBytes_Value ()
        {
            var expected = (1024L * 1024 * 1024 * 1024) * 9;
            var left = ByteSize.FromTerabytes(10);
            var right = 1;

            //Act
            var updated = left.SubtractTerabytes(right);
            var actual = updated.Bytes;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void OpSubtract_Value ()
        {
            var expected = 975;
            var left = new ByteSize(1000);
            var right = new ByteSize(25);

            //Act
            var updated = left - right;
            var actual = updated.Bytes;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void OpSubtract_ValueIsInteger ()
        {
            var expected = 975;
            var left = new ByteSize(1000);
            var right = 25;

            //Act
            var updated = left - right;
            var actual = updated.Bytes;

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #endregion
    }
}
