using System;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken;
using System.Globalization;

namespace Tests.P3Net.Kraken
{
    [TestClass]
    public class MoneyTests
    {
        #region ctor

        [TestMethod]
        public void Ctor_SimpleValue ()
        {
            var expected = 12.34M;

            var target = new Money(expected);
            
            target.Value.Should().Be(expected);
        }

        [TestMethod]
        public void Ctor_DoubleValue ()
        {
            var expected = 12.34;

            var target = new Money(expected);

            target.Value.Should().Be((decimal)expected);
        }

        [TestMethod]
        public void Ctor_NegativeAmount ()
        {
            var expected = -10.25M;
            var target = new Money(expected);

            target.Value.Should().Be(expected);
        }

        [TestMethod]
        public void Ctor_AmountWithCurrency ()
        {
            var expectedAmount = 1234.56M;
            var expectedRegion = new RegionInfo("en-GB");

            var target = new Money(expectedAmount, expectedRegion);

            target.Value.Should().Be(expectedAmount);
            target.Currency.Should().Be(expectedRegion);
        }

        [TestMethod]
        public void Ctor_DoubleAmountWithCurrency ()
        {
            var expectedAmount = 1234.56;
            var expectedRegion = new RegionInfo("en-GB");

            var target = new Money(expectedAmount, expectedRegion);

            target.Value.Should().Be((decimal)expectedAmount);
            target.Currency.Should().Be(expectedRegion);
        }

        [TestMethod]
        public void ConversionToDecimal ()
        {
            var target = new Money(45M);

            decimal actual = (decimal)target;

            actual.Should().Be(target.Value);
        }

        [TestMethod]
        public void ConversionFromDecimal ()
        {
            var target = 45M;

            Money actual = target;

            actual.Value.Should().Be(target);
        }	
        #endregion

        #region Add

        [TestMethod]
        public void Add_MoneyToMoney ()
        {
            var money1 = new Money(45.50);
            var money2 = new Money(100.50);

            var actual = money1.Add(money2);

            actual.Value.Should().Be(money1.Value + money2.Value);
        }
        
        [TestMethod]
        public void Add_MoneyWithMixedCurrency ()
        {
            var money1 = new Money(45.50, new RegionInfo("en-US"));
            var money2 = new Money(6.52, new RegionInfo("en-GB"));

            Action action = () => money1.Add(money2);

            action.ShouldThrow<MismatchedCurrencyException>();
        }

        [TestMethod]
        public void Add_MoneyWithNoCurrencyToCurrency ()
        {
            var money1 = new Money(45.50, new RegionInfo("en-US"));
            var money2 = new Money(6.52);

            var actual = money1.Add(money2);

            actual.Value.Should().Be(money1.Value + money2.Value);
            actual.Currency.Should().Be(money1.Currency);
        }

        [TestMethod]
        public void Add_MoneyToDecimal ()
        {
            var money1 = new Money(45.50);
            var money2 = 100.50M;

            var actual = money1.Add(money2);

            actual.Value.Should().Be(money1.Value + money2);
        }

        [TestMethod]
        public void Add_MoneyToInt ()
        {
            var money1 = new Money(45.50);
            int money2 = 100;

            var actual = money1.Add(money2);

            actual.Value.Should().Be(money1.Value + money2);
        }

        [TestMethod]
        public void Add_MoneyToNumericKeepsCurrency ()
        {
            var money1 = new Money(45.50, new RegionInfo("en-GB"));
            var money2 = 123.45M;

            var actual = money1.Add(money2);

            actual.Currency.Should().Be(money1.Currency);
        }

        [TestMethod]
        public void AddOp_MoneyToMoney ()
        {
            var money1 = new Money(45.50);
            var money2 = new Money(100.50);

            var actual = money1 + money2;

            actual.Value.Should().Be(money1.Value + money2.Value);
        }

        [TestMethod]
        public void AddOp_MoneyToDecimal ()
        {
            var money1 = new Money(45.50);
            var money2 = 100.50M;

            var actual = money1 + money2;

            actual.Value.Should().Be(money1.Value + money2);
        }

        [TestMethod]
        public void AddOp_MoneyToDouble ()
        {
            var money1 = new Money(45.50);
            double money2 = 100.50;

            var actual = money1 + money2;

            actual.Value.Should().Be(money1.Value + (decimal)money2);
        }

        [TestMethod]
        public void AddOp_MoneyToInt ()
        {
            var money1 = new Money(45.50);
            int money2 = 100;

            var actual = money1 + money2;

            actual.Value.Should().Be(money1.Value + (decimal)money2);
        }

        [TestMethod]
        public void AddOp_MoneyWithMixedCurrency ()
        {
            var money1 = new Money(45.50, new RegionInfo("en-US"));
            var money2 = new Money(6.52, new RegionInfo("en-GB"));

            Action action = () => { var result = money1 + money2; };

            action.ShouldThrow<MismatchedCurrencyException>();
        }

        [TestMethod]
        public void AddOp_MoneyWithNoCurrencyToCurrency ()
        {
            var money1 = new Money(45.50, new RegionInfo("en-US"));
            var money2 = new Money(6.52);

            var actual = money1 + money2;

            actual.Value.Should().Be(money1.Value + money2.Value);
            actual.Currency.Should().Be(money1.Currency);
        }
        #endregion

        #region Subtract

        [TestMethod]
        public void Subtract_MoneyToMoney ()
        {
            var money1 = new Money(45.50);
            var money2 = new Money(100.50);

            var actual = money1.Subtract(money2);

            actual.Value.Should().Be(money1.Value - money2.Value);
        }
        
        [TestMethod]
        public void Subtract_MoneyWithMixedCurrency ()
        {
            var money1 = new Money(45.50, new RegionInfo("en-US"));
            var money2 = new Money(6.52, new RegionInfo("en-GB"));

            Action action = () => money1.Subtract(money2);

            action.ShouldThrow<MismatchedCurrencyException>();
        }

        [TestMethod]
        public void Subtract_MoneyWithNoCurrencyToCurrency ()
        {
            var money1 = new Money(45.50, new RegionInfo("en-US"));
            var money2 = new Money(6.52);

            var actual = money1.Subtract(money2);

            actual.Value.Should().Be(money1.Value - money2.Value);
            actual.Currency.Should().Be(money1.Currency);
        }

        [TestMethod]
        public void Subtract_MoneyToDecimal ()
        {
            var money1 = new Money(45.50);
            var money2 = 100.50M;

            var actual = money1.Subtract(money2);

            actual.Value.Should().Be(money1.Value - money2);
        }

        [TestMethod]
        public void Subtract_MoneyToInt ()
        {
            var money1 = new Money(45.50);
            int money2 = 100;

            var actual = money1.Subtract(money2);

            actual.Value.Should().Be(money1.Value - money2);
        }

        [TestMethod]
        public void Subtract_MoneyToNumericKeepsCurrency ()
        {
            var money1 = new Money(45.50, new RegionInfo("en-GB"));
            var money2 = 123.45M;

            var actual = money1.Subtract(money2);

            actual.Currency.Should().Be(money1.Currency);
        }

        [TestMethod]
        public void SubtractOp_MoneyToMoney ()
        {
            var money1 = new Money(45.50);
            var money2 = new Money(100.50);

            var actual = money1 - money2;

            actual.Value.Should().Be(money1.Value - money2.Value);
        }

        [TestMethod]
        public void SubtractOp_MoneyToDecimal ()
        {
            var money1 = new Money(45.50);
            var money2 = 100.50M;

            var actual = money1 - money2;

            actual.Value.Should().Be(money1.Value - money2);
        }

        [TestMethod]
        public void SubtractOp_MoneyToDouble ()
        {
            var money1 = new Money(45.50);
            double money2 = 100.50;

            var actual = money1 - money2;

            actual.Value.Should().Be(money1.Value - (decimal)money2);
        }

        [TestMethod]
        public void SubtractOp_MoneyToInt ()
        {
            var money1 = new Money(45.50);
            int money2 = 100;

            var actual = money1 - money2;

            actual.Value.Should().Be(money1.Value - (decimal)money2);
        }

        [TestMethod]
        public void SubtractOp_MoneyWithMixedCurrency ()
        {
            var money1 = new Money(45.50, new RegionInfo("en-US"));
            var money2 = new Money(6.52, new RegionInfo("en-GB"));

            Action action = () => { var result = money1 - money2; };

            action.ShouldThrow<MismatchedCurrencyException>();
        }

        [TestMethod]
        public void SubtractOp_MoneyWithNoCurrencyToCurrency ()
        {
            var money1 = new Money(45.50, new RegionInfo("en-US"));
            var money2 = new Money(6.52);

            var actual = money1 - money2;

            actual.Value.Should().Be(money1.Value - money2.Value);
            actual.Currency.Should().Be(money1.Currency);
        }
        #endregion

        #region Multiply

        [TestMethod]
        public void Multiply_MoneyToDouble ()
        {
            var money1 = new Money(45.50);
            var money2 = 100.50;

            var actual = money1.Multiply(money2);

            actual.Value.Should().Be(money1.Value * (decimal)money2);
        }

        [TestMethod]
        public void Multiply_MoneyToInt ()
        {
            var money1 = new Money(45.50);
            int money2 = 100;

            var actual = money1.Multiply(money2);

            actual.Value.Should().Be(money1.Value * money2);
        }

        [TestMethod]
        public void Multiply_MoneyToNumericKeepsCurrency ()
        {
            var money1 = new Money(45.50, new RegionInfo("en-GB"));
            var money2 = 123.45;

            var actual = money1.Multiply(money2);

            actual.Currency.Should().Be(money1.Currency);
        }        
        
        [TestMethod]
        public void MultiplyOp_MoneyToDouble ()
        {
            var money1 = new Money(45.50);
            double money2 = 100.50;

            var actual = money1 * money2;

            actual.Value.Should().Be(money1.Value * (decimal)money2);
        }

        [TestMethod]
        public void MultiplyOp_MoneyToInt ()
        {
            var money1 = new Money(45.50);
            int money2 = 100;

            var actual = money1 * money2;

            actual.Value.Should().Be(money1.Value * (decimal)money2);
        }               
        #endregion

        #region Divide

        [TestMethod]
        public void Divide_MoneyToMoney ()
        {
            var money1 = new Money(45.50);
            var money2 = new Money(100.50);

            var actual = money1.Divide(money2);

            actual.Should().Be(money1.Value / money2.Value);
        }
        
        [TestMethod]
        public void Divide_MoneyWithMixedCurrency ()
        {
            var money1 = new Money(45.50, new RegionInfo("en-US"));
            var money2 = new Money(6.52, new RegionInfo("en-GB"));

            Action action = () => money1.Divide(money2);

            action.ShouldThrow<MismatchedCurrencyException>();
        }

        [TestMethod]
        public void Divide_MoneyWithNoCurrencyToCurrency ()
        {
            var money1 = new Money(45.50, new RegionInfo("en-US"));
            var money2 = new Money(6.52);

            var actual = money1.Divide(money2);

            actual.Should().Be(money1.Value / money2.Value);
        }

        [TestMethod]
        public void Divide_MoneyToDecimal ()
        {
            var money1 = new Money(45.50);
            var money2 = 100.50M;

            var actual = money1.Divide(money2);

            actual.Value.Should().Be(money1.Value / money2);
        }

        [TestMethod]
        public void Divide_MoneyToInt ()
        {
            var money1 = new Money(45.50);
            int money2 = 100;

            var actual = money1.Divide(money2);

            actual.Value.Should().Be(money1.Value / money2);
        }

        [TestMethod]
        public void Divide_MoneyToNumericKeepsCurrency ()
        {
            var money1 = new Money(45.50, new RegionInfo("en-GB"));
            var money2 = 123.45M;

            var actual = money1.Divide(money2);

            actual.Currency.Should().Be(money1.Currency);
        }

        [TestMethod]
        public void DivideOp_MoneyToMoney ()
        {
            var money1 = new Money(45.50);
            var money2 = new Money(100.50);

            var actual = money1 / money2;

            actual.Should().Be(money1.Value / money2.Value);
        }

        [TestMethod]
        public void DivideOp_MoneyToDecimal ()
        {
            var money1 = new Money(45.50);
            var money2 = 100.50M;

            var actual = money1 / money2;

            actual.Value.Should().Be(money1.Value / money2);
        }

        [TestMethod]
        public void DivideOp_MoneyToDouble ()
        {
            var money1 = new Money(45.50);
            double money2 = 100.50;

            var actual = money1 / money2;

            actual.Value.Should().Be(money1.Value / (decimal)money2);
        }

        [TestMethod]
        public void DivideOp_MoneyToInt ()
        {
            var money1 = new Money(45.50);
            int money2 = 100;

            var actual = money1 / money2;

            actual.Value.Should().Be(money1.Value / (decimal)money2);
        }

        [TestMethod]
        public void DivideOp_MoneyWithMixedCurrency ()
        {
            var money1 = new Money(45.50, new RegionInfo("en-US"));
            var money2 = new Money(6.52, new RegionInfo("en-GB"));

            Action action = () => { var result = money1 / money2; };

            action.ShouldThrow<MismatchedCurrencyException>();
        }

        [TestMethod]
        public void DivideOp_MoneyWithNoCurrencyToCurrency ()
        {
            var money1 = new Money(45.50, new RegionInfo("en-US"));
            var money2 = new Money(6.52);

            var actual = money1 / money2;

            actual.Should().Be(money1.Value / money2.Value);
        }
        #endregion

        #region IComparable<Money>, IComparable<decimal>, IComparable

        [TestMethod]
        public void CompareTo_Money_AreEqual ()
        {
            var target = new Money(123M);
            var other = new Money(123M);

            var actual = target.CompareTo(other);

            actual.Should().Be(0);
        }

        [TestMethod]
        public void CompareTo_Money_IsLess ()
        {
            var target = new Money(123M);
            var other = new Money(456M);

            var actual = target.CompareTo(other);

            actual.Should().BeLessThan(0);
        }

        [TestMethod]
        public void CompareTo_Money_IsGreater ()
        {
            var target = new Money(123M);
            var other = new Money(45M);

            var actual = target.CompareTo(other);

            actual.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void CompareTo_Decimal_AreEqual ()
        {
            var target = new Money(123M);
            var other = 123M;

            var actual = target.CompareTo(other);

            actual.Should().Be(0);
        }

        [TestMethod]
        public void CompareTo_Decimal_IsLess ()
        {
            var target = new Money(123M);
            var other = 456M;

            var actual = target.CompareTo(other);

            actual.Should().BeLessThan(0);
        }

        [TestMethod]
        public void CompareTo_Decimal_IsGreater ()
        {
            var target = new Money(123M);
            var other = 45M;

            var actual = target.CompareTo(other);

            actual.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void CompareTo_MoneyWithSameCurrency ()
        {
            var target = new Money(123M, new RegionInfo("en-US"));
            var other = new Money(3M, new RegionInfo("en-US"));

            var actual = target.CompareTo(other);

            actual.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void CompareTo_MoneyWithDifferentCurrency ()
        {
            var target = new Money(123M, new RegionInfo("en-US"));
            var other = new Money(123M, new RegionInfo("en-GB"));

            Action action = () => target.CompareTo(other);

            action.ShouldThrow<MismatchedCurrencyException>();
        }

        [TestMethod]
        public void CompareTo_ObjectIsMoney ()
        {
            var target = new Money(123M);
            object other = new Money(456M);

            var actual = target.CompareTo(other);

            actual.Should().BeLessThan(0);
        }

        [TestMethod]
        public void CompareTo_ObjectIsDecimal ()
        {
            var target = new Money(123M);
            object other = 456M;

            var actual = target.CompareTo(other);

            actual.Should().BeLessThan(0);
        }

        [TestMethod]
        public void CompareTo_ObjectIsInvalid ()
        {
            var target = new Money(123M);
            object other = "bad";

            Action action = () => target.CompareTo(other);

            action.ShouldThrow<ArgumentException>();
        }
        
        [TestMethod]
        public void LessThanOp_Money_IsTrue ()
        {
            var left = new Money(123M);
            var right = new Money(423M);

            var actual = left < right;

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void LessThanOp_Money_IsFalse ()
        {
            var left = new Money(1223M);
            var right = new Money(423M);

            var actual = left < right;

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void LessThanOp_Money_AreEqual ()
        {
            var left = new Money(123M);
            var right = new Money(123M);

            var actual = left < right;

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void LessThanOp_Decimal_IsTrue ()
        {
            var left = new Money(123M);
            var right = 423M;

            var actual = left < right;

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void LessThanOp_Decimal_IsFalse ()
        {
            var left = new Money(1223M);
            var right = 423M;

            var actual = left < right;

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void LessThanOp_Decimal_AreEqual ()
        {
            var left = new Money(123M);
            var right = 123M;

            var actual = left < right;

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void LessThanOrEqualToOp_Money_IsTrue ()
        {
            var left = new Money(123M);
            var right = new Money(423M);

            var actual = left <= right;

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void LessThanOrEqualToOp_Money_IsFalse ()
        {
            var left = new Money(1223M);
            var right = new Money(423M);

            var actual = left <= right;

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void LessThanOrEqualToOp_Money_AreEqual ()
        {
            var left = new Money(123M);
            var right = new Money(123M);

            var actual = left <= right;

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void LessThanOrEqualToOp_Decimal_IsTrue ()
        {
            var left = new Money(123M);
            var right = 423M;

            var actual = left <= right;

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void LessThanOrEqualToOp_Decimal_IsFalse ()
        {
            var left = new Money(1223M);
            var right = 423M;

            var actual = left <= right;

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void LessThanOrEqualToOp_Decimal_AreEqual ()
        {
            var left = new Money(123M);
            var right = 123M;

            var actual = left <= right;

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void GreaterThanOp_Money_IsTrue ()
        {
            var left = new Money(123M);
            var right = new Money(43M);

            var actual = left > right;

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void GreaterThanOp_Money_IsFalse ()
        {
            var left = new Money(13M);
            var right = new Money(423M);

            var actual = left > right;

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void GreaterThanOp_Money_AreEqual ()
        {
            var left = new Money(123M);
            var right = new Money(123M);

            var actual = left > right;

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void GreaterThanOp_Decimal_IsTrue ()
        {
            var left = new Money(123M);
            var right = 42M;

            var actual = left > right;

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void GreaterThanOp_Decimal_IsFalse ()
        {
            var left = new Money(123M);
            var right = 423M;

            var actual = left > right;

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void GreaterThanOp_Decimal_AreEqual ()
        {
            var left = new Money(123M);
            var right = 123M;

            var actual = left > right;

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void GreaterThanOrEqualToOp_Money_IsTrue ()
        {
            var left = new Money(123M);
            var right = new Money(43M);

            var actual = left >= right;

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void GreaterThanOrEqualToOp_Money_IsFalse ()
        {
            var left = new Money(123M);
            var right = new Money(423M);

            var actual = left >= right;

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void GreaterThanOrEqualToOp_Money_AreEqual ()
        {
            var left = new Money(123M);
            var right = new Money(123M);

            var actual = left >= right;

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void GreaterThanOrEqualToOp_Decimal_IsTrue ()
        {
            var left = new Money(123M);
            var right = 43M;

            var actual = left >= right;

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void GreaterThanOrEqualToOp_Decimal_IsFalse ()
        {
            var left = new Money(123M);
            var right = 423M;

            var actual = left >= right;

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void GreaterThanOrEqualToOp_Decimal_AreEqual ()
        {
            var left = new Money(123M);
            var right = 123M;

            var actual = left >= right;

            actual.Should().BeTrue();
        }
        #endregion

        #region IEquatable<Money>, IEquatable<decimal>
                    
        [TestMethod]
        public void Equal_MoneyToMoney_IsTrue ()
        {
            var left = new Money(123M);
            var right = new Money(123M);

            var actual = left.Equals(right);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Equal_MoneyToMoney_IsFalse ()
        {
            var left = new Money(123M);
            var right = new Money(456M);

            var actual = left.Equals(right);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void Equal_MoneyToDecimal_IsTrue ()
        {
            var left = new Money(123M);
            var right = 123M;

            var actual = left.Equals(right);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Equal_MoneyToDecimal_IsFalse ()
        {
            var left = new Money(123M);
            var right = 553M;

            var actual = left.Equals(right);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void Equal_MoneyToMoneyWithCurrency_IsTrue ()
        {
            var left = new Money(123M, new RegionInfo("en-US"));
            var right = new Money(123M);

            var actual = left.Equals(right);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Equal_MoneyToMoneyWithCurrency_IsFalse ()
        {
            var left = new Money(123M, new RegionInfo("en-US"));
            var right = new Money(542M);

            var actual = left.Equals(right);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void Equal_MoneyToMoneyWithMixedCurrency ()
        {
            var left = new Money(123M, new RegionInfo("en-US"));
            var right = new Money(123M, new RegionInfo("en-GB"));

            Action action = () => left.Equals(right);

            action.ShouldThrow<MismatchedCurrencyException>();
        }

        public void EqualOp_MoneyToMoney_IsTrue ()
        {
            var left = new Money(123M);
            var right = new Money(123M);

            var actual = left == right;

            actual.Should().BeTrue();
        }
        
        [TestMethod]
        public void EqualOp_MoneyToDecimal_IsTrue ()
        {
            var left = new Money(123M);
            var right = 123M;

            var actual = left == right;

            actual.Should().BeTrue();
        }

        public void NotEqualOp_MoneyToMoney_IsTrue ()
        {
            var left = new Money(123M);
            var right = new Money(456M);

            var actual = left != right;

            actual.Should().BeTrue();
        }
        
        [TestMethod]
        public void NotEqualOp_MoneyToDecimal_IsFalse ()
        {
            var left = new Money(123M);
            var right = 123M;

            var actual = left != right;

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void EqualOp_MoneyToObjectMoney ()
        {
            var left = new Money(123M);
            object right = new Money(123M);

            var actual = left.Equals(right);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void EqualOp_MoneyToObjectDecimal ()
        {
            var left = new Money(123M);
            object right = 123M;

            var actual = left.Equals(right);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void GetHashCode_IsValue ()
        {
            var target = new Money(123M);

            var actual = target.GetHashCode();

            actual.Should().Be(target.Value.GetHashCode());
        }
        #endregion

        #region IFormattable

        [TestMethod]
        public void ToString_NoFormat ()
        {
            var target = new Money(123M);

            var actual = target.ToString();

            actual.Should().Be("$123.00");
        }

        [TestMethod]
        public void ToString_Format ()
        {
            var target = new Money(123M);

            var actual = target.ToString("g");

            actual.Should().Be("123");
        }

        [TestMethod]
        public void ToString_WithCurrency ()
        {
            var target = new Money(123M, new RegionInfo("en-GB"));

            var actual = target.ToString();

            actual.Should().Be("£123.00");
        }
        #endregion
    }
}
