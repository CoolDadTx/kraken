/*
 * Copyright © Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace P3Net.Kraken
{
    /// <summary>Represents currency values.</summary>
    public struct Money : IEquatable<Money>, IEquatable<decimal>, IComparable<Money>, IComparable<decimal>, IComparable, IFormattable
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="Money"/> structure.</summary>
        /// <param name="value">The monetary value.</param>
        public Money ( decimal value ) : this(value, null)
        {
        }

        /// <summary>Initializes an instance of the <see cref="Money"/> structure.</summary>
        /// <param name="value">The monetary value.</param>
        public Money ( double value ) : this((decimal)value, null)
        {        
        }

        /// <summary>Initializes an instance of the <see cref="Money"/> structure.</summary>
        /// <param name="value">The monetary value.</param>
        /// <param name="currency">The currency associated with the value.</param>
        public Money ( double value, RegionInfo currency ) : this((decimal)value, currency)
        {
        }

        /// <summary>Initializes an instance of the <see cref="Money"/> structure.</summary>
        /// <param name="value">The monetary value.</param>
        /// <param name="currency">The currency associated with the value.</param>
        public Money ( decimal value, RegionInfo currency ) : this()
        {
            Currency = currency;
            Value = value;
        }

        /// <summary>Converts from <see cref="Money"/> to <see cref="decimal"/>.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The converted value.</returns>
        public static explicit operator decimal ( Money value )
        {
            return value.Value;
        }

        /// <summary>Converts from a <see cref="decimal"/> to <see cref="Money"/>.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The converted value.</returns>
        public static implicit operator Money ( decimal value )
        {
            return new Money(value);
        }
        #endregion

        /// <summary>Gets the currency associated with the monetary, if any.</summary>
        public RegionInfo Currency { get; private set; }

        /// <summary>Get the numeric value.</summary>
        public decimal Value { get; private set; }

        #region Add

        /// <summary>Adds two monetary values.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The new value.</returns>
        /// <exception cref="MismatchedCurrencyException">Raised when the values have different currencies.</exception>
        public Money Add ( Money value )
        {
            var currency = VerifyMatchingCurrency(this, value);

            return new Money(Value + value.Value, currency);
        }

        /// <summary>Adds two monetary values.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The new value.</returns>
        public Money Add ( decimal value )
        {
            return new Money(Value + value, Currency);
        }

        /// <summary>Adds two monetary values.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The new value.</returns>
        public Money Add ( double value )
        {
            return new Money(Value + (decimal)value, Currency);
        }

        /// <summary>Adds two monetary values.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The new value.</returns>
        public Money Add ( long value )
        {
            return new Money(Value + (decimal)value, Currency);
        }

        /// <summary>Adds two monetary values.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The new value.</returns>
        /// <exception cref="MismatchedCurrencyException">Raised when the values have different currencies.</exception>
        public static Money operator+ ( Money left, Money right )
        {
            return left.Add(right);
        }

        /// <summary>Adds two monetary values.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The new value.</returns>
        public static Money operator+ ( Money left, decimal right )
        {
            return left.Add(right);
        }

        /// <summary>Adds two monetary values.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The new value.</returns>
        public static Money operator+ ( Money left, double right )
        {
            return left.Add(right);
        }

        /// <summary>Adds two monetary values.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The new value.</returns>
        public static Money operator+ ( Money left, long right )
        {
            return left.Add(right);
        }
        #endregion

        #region Subtract

        /// <summary>Subtracts two monetary values.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The new value.</returns>
        /// <exception cref="MismatchedCurrencyException">Raised when the values have different currencies.</exception>
        public Money Subtract ( Money value )
        {
            var currency = VerifyMatchingCurrency(this, value);

            return new Money(Value - value.Value, currency);
        }

        /// <summary>Subtracts two monetary values.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The new value.</returns>
        public Money Subtract ( decimal value )
        {
            return new Money(Value - value, Currency);
        }

        /// <summary>Subtracts two monetary values.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The new value.</returns>
        public Money Subtract ( double value )
        {
            return new Money(Value - (decimal)value, Currency);
        }

        /// <summary>Subtracts two monetary values.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The new value.</returns>
        public Money Subtract ( long value )
        {
            return new Money(Value - (decimal)value, Currency);
        }

        /// <summary>Subtracts two monetary values.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The new value.</returns>
        /// <exception cref="MismatchedCurrencyException">Raised when the values have different currencies.</exception>
        public static Money operator- ( Money left, Money right )
        {
            return left.Subtract(right);
        }

        /// <summary>Subtracts two monetary values.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The new value.</returns>
        public static Money operator- ( Money left, decimal right )
        {
            return left.Subtract(right);
        }

        /// <summary>Subtracts two monetary values.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The new value.</returns>
        public static Money operator- ( Money left, double right )
        {
            return left.Subtract(right);
        }

        /// <summary>Subtracts two monetary values.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The new value.</returns>
        public static Money operator- ( Money left, long right )
        {
            return left.Subtract(right);
        }
        #endregion

        #region Multiply        

        /// <summary>Multiplies two monetary values.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The new value.</returns>
        public Money Multiply ( double value )
        {
            return new Money(Value * (decimal)value, Currency);
        }

        /// <summary>Multiplies two monetary values.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The new value.</returns>
        public Money Multiply ( long value )
        {
            return new Money(Value * (decimal)value, Currency);
        }
        
        /// <summary>Multiplies two monetary values.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The new value.</returns>
        public static Money operator* ( Money left, double right )
        {
            return left.Multiply(right);
        }

        /// <summary>Multiplies two monetary values.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The new value.</returns>
        public static Money operator* ( Money left, long right )
        {
            return left.Multiply(right);
        }
        #endregion

        #region Divide

        /// <summary>Divides two monetary values.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The new value.</returns>
        /// <exception cref="MismatchedCurrencyException">Raised when the values have different currencies.</exception>
        public decimal Divide ( Money value )
        {
            var currency = VerifyMatchingCurrency(this, value);

            return Value / value.Value;
        }

        /// <summary>Divides two monetary values.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The new value.</returns>
        public Money Divide ( decimal value )
        {
            return new Money(Value / value, Currency);
        }

        /// <summary>Divides two monetary values.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The new value.</returns>
        public Money Divide ( double value )
        {
            return new Money(Value / (decimal)value, Currency);
        }

        /// <summary>Divides two monetary values.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The new value.</returns>
        public Money Divide ( long value )
        {
            return new Money(Value / (decimal)value, Currency);
        }

        /// <summary>Divides two monetary values.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The new value.</returns>
        /// <exception cref="MismatchedCurrencyException">Raised when the values have different currencies.</exception>
        public static decimal operator/ ( Money left, Money right )
        {
            return left.Divide(right);
        }

        /// <summary>Divides two monetary values.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The new value.</returns>
        public static Money operator/ ( Money left, decimal right )
        {
            return left.Divide(right);
        }

        /// <summary>Divides two monetary values.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The new value.</returns>
        public static Money operator/ ( Money left, double right )
        {
            return left.Divide(right);
        }

        /// <summary>Divides two monetary values.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The new value.</returns>
        public static Money operator/ ( Money left, long right )
        {
            return left.Divide(right);
        }
        #endregion

        #region IComparable Members

        /// <summary>Compares two values for sorting.</summary>
        /// <param name="other">The other value.</param>
        /// <returns>1 if the left side is greater, 0 if they are equal or -1 if the right side is greater.</returns>
        /// <exception cref="MismatchedCurrencyException">Raised when the values have different currencies.</exception>
        public int CompareTo ( Money other )
        {
            VerifyMatchingCurrency(this, other);

            return this.Value.CompareTo(other.Value);
        }

        /// <summary>Compares two values for sorting.</summary>
        /// <param name="other">The other value.</param>
        /// <returns>1 if the left side is greater, 0 if they are equal or -1 if the right side is greater.</returns>
        public int CompareTo ( decimal other )
        {
            return this.Value.CompareTo(other);
        }

        /// <summary>Compares two values for sorting.</summary>
        /// <param name="other">The other value.</param>
        /// <returns>1 if the left side is greater, 0 if they are equal or -1 if the right side is greater.</returns>
        public int CompareTo ( object other )
        {
            if (other is Money)
                return CompareTo((Money)other);

            if (other is decimal)
                return CompareTo((decimal)other);

            throw new ArgumentException("Value must be money.");
        }

        /// <summary>Compares two values for sorting.</summary>
        /// <param name="left">The left side.</param>
        /// <param name="right">The right side.</param>
        /// <returns><see langword="true"/> if the expression evaluates to true.</returns>
        public static bool operator> ( Money left, Money right )
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>Compares two values for sorting.</summary>
        /// <param name="left">The left side.</param>
        /// <param name="right">The right side.</param>
        /// <returns><see langword="true"/> if the expression evaluates to true.</returns>
        public static bool operator> ( Money left, decimal right )
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>Compares two values for sorting.</summary>
        /// <param name="left">The left side.</param>
        /// <param name="right">The right side.</param>
        /// <returns><see langword="true"/> if the expression evaluates to true.</returns>
        public static bool operator>= ( Money left, Money right )
        {
            return left.CompareTo(right) >= 0;
        }

        /// <summary>Compares two values for sorting.</summary>
        /// <param name="left">The left side.</param>
        /// <param name="right">The right side.</param>
        /// <returns><see langword="true"/> if the expression evaluates to true.</returns>
        public static bool operator>= ( Money left, decimal right )
        {
            return left.CompareTo(right) >= 0;
        }

        /// <summary>Compares two values for sorting.</summary>
        /// <param name="left">The left side.</param>
        /// <param name="right">The right side.</param>
        /// <returns><see langword="true"/> if the expression evaluates to true.</returns>
        public static bool operator< ( Money left, Money right )
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>Compares two values for sorting.</summary>
        /// <param name="left">The left side.</param>
        /// <param name="right">The right side.</param>
        /// <returns><see langword="true"/> if the expression evaluates to true.</returns>
        public static bool operator< ( Money left, decimal right )
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>Compares two values for sorting.</summary>
        /// <param name="left">The left side.</param>
        /// <param name="right">The right side.</param>
        /// <returns><see langword="true"/> if the expression evaluates to true.</returns>
        public static bool operator<= ( Money left, Money right )
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>Compares two values for sorting.</summary>
        /// <param name="left">The left side.</param>
        /// <param name="right">The right side.</param>
        /// <returns><see langword="true"/> if the expression evaluates to true.</returns>
        public static bool operator<= ( Money left, decimal right )
        {
            return left.CompareTo(right) <= 0;
        }
        #endregion

        #region IEquatable Members

        /// <summary>Determines if the two objects are equal.</summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if the values are equal.</returns>
        /// <exception cref="MismatchedCurrencyException">Raised when the values have different currencies.</exception>
        public static bool operator ==( Money left, Money right )
        {
            return left.Equals(right);
        }

        /// <summary>Determines if the two objects are equal.</summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if the values are equal.</returns>
        public static bool operator ==( Money left, decimal right )
        {
            return left.Equals(right);
        }

        /// <summary>Determines if the two objects are equal.</summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if the values are equal.</returns>
        /// <exception cref="MismatchedCurrencyException">Raised when the values have different currencies.</exception>
        public static bool operator !=( Money left, Money right )
        {
            return !left.Equals(right);
        }

        /// <summary>Determines if the two objects are equal.</summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns><see langword="true"/> if the values are equal.</returns>
        public static bool operator !=( Money left, decimal right )
        {
            return !left.Equals(right);
        }

        /// <summary>Determines if the two objects are equal.</summary>
        /// <param name="obj">The value to compare against.</param>
        /// <returns><see langword="true"/> if the values are equal.</returns>
        /// <exception cref="MismatchedCurrencyException">Raised when the values have different currencies.</exception>
        public override bool Equals ( object obj )
        {
            if (obj is Money)
                return Equals((Money)obj);

            if (obj is decimal)
                return Equals((decimal)obj);

            return false;
        }

        /// <summary>Determines if the two objects are equal.</summary>
        /// <param name="other">The value to compare against.</param>
        /// <returns><see langword="true"/> if the values are equal.</returns>
        /// <exception cref="MismatchedCurrencyException">Raised when the values have different currencies.</exception>
        public bool Equals ( Money other )
        {
            var currency = VerifyMatchingCurrency(this, other);

            return Value.Equals(other.Value);
        }

        /// <summary>Determines if the two objects are equal.</summary>
        /// <param name="other">The value to compare against.</param>
        /// <returns><see langword="true"/> if the values are equal.</returns>
        public bool Equals ( decimal other )
        {
            return Value.Equals(other);
        }

        /// <summary>Gets the hash code.</summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode ()
        {
            if (Currency != null)
                return Tuple.Create(Currency, Value).GetHashCode();

            return Value.GetHashCode();
        }
        #endregion

        #region IFormattable Members

        /// <summary>Gets the string representation.</summary>
        /// <returns>The string value.</returns>
        public override string ToString ()
        {
            return ToString("c", null);
        }

        /// <summary>Gets the string representation.</summary>
        /// <param name="format">The format string to use.</param>
        /// <returns>The string value.</returns>
        public string ToString ( string format )
        {
            return ToString(format, null);
        }

        /// <summary>Gets the string representation.</summary>
        /// <param name="format">The format string to use.</param>
        /// <param name="provider">The format provider to use.</param>
        /// <returns>The string value.</returns>
        public string ToString ( string format, IFormatProvider provider )
        {
            //Use currency
            if (String.IsNullOrEmpty(format))
                format = "c";

            //Use the provider, if any
            if (provider != null)
                return Value.ToString(format, provider);

            //If currency is provided then use it
            if (Currency != null)
            {
                var currencyName = Currency.Name;

                var culture = CultureInfo.GetCultures(CultureTypes.AllCultures).FirstOrDefault(c => c.Name == currencyName);
                if (culture == null)
                    culture = CultureInfo.GetCultureInfo(currencyName);

                return Value.ToString(format, culture);
            };

            return Value.ToString(format);
        }
        #endregion

        #region Private Members

        private static RegionInfo VerifyMatchingCurrency ( Money left, Money right )
        {            
            if (left.Currency == null)
                return right.Currency;

            if (right.Currency == null)
                return left.Currency;

            if (left.Currency.CurrencyEnglishName == right.Currency.CurrencyEnglishName)
                return left.Currency;

            throw new MismatchedCurrencyException();
        }
        #endregion
    }
}
