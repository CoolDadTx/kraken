/*
 * Copyright © 2013 Michael Taylor
 * All Rights Reserved
 * 
 * From code copyright Federation of State Medical Boards
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace P3Net.Kraken.Diagnostics
{
    /// <summary>Provides extension methods for <see cref="ArgumentConstraint{T}"/> of <see cref="IComparable{T}"/> types.</summary>
    public static class ComparableArgumentConstraintExtensions
    {
        #region IsBetween

        /// <summary>Verifies the argument is between two values, inclusive.</summary>
        /// <param name="source">The source.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not between the two values.</exception>
        public static AndArgumentConstraint<T> IsBetween<T> ( this ArgumentConstraint<T> source, T minValue, T maxValue ) where T : IComparable<T>
        {
            return IsBetween(source, minValue, maxValue, "Value must be greater than or equal to {0} and less than or equal to {1}.", minValue, maxValue);
        }

        /// <summary>Verifies the argument is between two values, inclusive.</summary>
        /// <param name="source">The source.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not between the two values.</exception>
        public static AndArgumentConstraint<T> IsBetween<T> ( this ArgumentConstraint<T> source, T minValue, T maxValue, 
                                                       string message, params object[] messageArgs ) where T : IComparable<T>
        {
            var value = source.Argument.Value;

            if (value.CompareTo(minValue) >= 0 && value.CompareTo(maxValue) <= 0)
                return new AndArgumentConstraint<T>(source);

            throw new ArgumentOutOfRangeException(source.Argument.Name, String.Format(message, messageArgs));
        }
        #endregion

        #region IsBetweenExclusive

        /// <summary>Verifies the argument is between two values, exclusive.</summary>
        /// <param name="source">The source.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not between the two values.</exception>
        public static AndArgumentConstraint<T> IsBetweenExclusive<T> ( this ArgumentConstraint<T> source, T minValue, T maxValue ) where T : IComparable<T>
        {
            return IsBetweenExclusive(source, minValue, maxValue, "Value must be greater than {0} and less than {1}.", minValue, maxValue);
        }

        /// <summary>Verifies the argument is between two values, exclusive.</summary>
        /// <param name="source">The source.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not between the two values.</exception>
        public static AndArgumentConstraint<T> IsBetweenExclusive<T> ( this ArgumentConstraint<T> source, T minValue, T maxValue,
                                                       string message, params object[] messageArgs ) where T : IComparable<T>
        {
            var value = source.Argument.Value;

            if (value.CompareTo(minValue) > 0 && value.CompareTo(maxValue) < 0)
                return new AndArgumentConstraint<T>(source);

            throw new ArgumentOutOfRangeException(source.Argument.Name, String.Format(message, messageArgs));
        }
        #endregion

        #region IsEqualTo

        /// <summary>Verifies the argument is equal to a value.</summary>
        /// <param name="source">The source value.</param>
        /// <param name="value">The value to compare against.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not equal to the value.</exception>        
        public static AndArgumentConstraint<T> IsEqualTo<T> ( this ArgumentConstraint<T> source, T value ) where T : IComparable<T>
        {
            return IsEqualTo<T>(source, value, "Value must be equal to {0}", value);
        }

        /// <summary>Verifies the argument is equal to a value.</summary>
        /// <param name="source">The source value.</param>
        /// <param name="value">The value to compare against.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not equal to the value.</exception>        
        public static AndArgumentConstraint<T> IsEqualTo<T> ( this ArgumentConstraint<T> source, T value,
                                                                  string message, params object[] messageArgs ) where T : IComparable<T>
        {
            if (source.Argument.Value.CompareTo(value) == 0)
                return new AndArgumentConstraint<T>(source);

            throw new ArgumentOutOfRangeException(source.Argument.Name, String.Format(message, messageArgs));
        }
        #endregion

        #region IsGreaterThan

        /// <summary>Verifies the argument is greater than a value.</summary>
        /// <param name="source">The source value.</param>
        /// <param name="value">The value to compare against.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than the value.</exception>        
        public static AndArgumentConstraint<T> IsGreaterThan<T> ( this ArgumentConstraint<T> source, T value ) where T : IComparable<T>
        {
            return IsGreaterThan<T>(source, value, "Value must be greater than {0}", value);
        }

        /// <summary>Verifies the argument is greater than a value.</summary>
        /// <param name="source">The source value.</param>
        /// <param name="value">The value to compare against.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than the value.</exception>        
        public static AndArgumentConstraint<T> IsGreaterThan<T> ( this ArgumentConstraint<T> source, T value,
                                                                  string message, params object[] messageArgs ) where T : IComparable<T>
        {
            if (source.Argument.Value.CompareTo(value) > 0)
                return new AndArgumentConstraint<T>(source);

            throw new ArgumentOutOfRangeException(source.Argument.Name, String.Format(message, messageArgs));
        }
        #endregion

        #region IsGreaterThanOrEqualTo

        /// <summary>Verifies the argument is greater than or equal to a value.</summary>
        /// <param name="source">The source value.</param>
        /// <param name="value">The value to compare against.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than or equal to the value.</exception>        
        public static AndArgumentConstraint<T> IsGreaterThanOrEqualTo<T> ( this ArgumentConstraint<T> source, T value ) where T : IComparable<T>
        {
            return IsGreaterThanOrEqualTo<T>(source, value, "Value must be greater than or equal to {0}", value);
        }

        /// <summary>Verifies the argument is greater than or equal to a value.</summary>
        /// <param name="source">The source value.</param>
        /// <param name="value">The value to compare against.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than or equal to the value.</exception>        
        public static AndArgumentConstraint<T> IsGreaterThanOrEqualTo<T> ( this ArgumentConstraint<T> source, T value,
                                                                  string message, params object[] messageArgs ) where T : IComparable<T>
        {
            if (source.Argument.Value.CompareTo(value) >= 0)
                return new AndArgumentConstraint<T>(source);

            throw new ArgumentOutOfRangeException(source.Argument.Name, String.Format(message, messageArgs));
        }
        #endregion

        #region IsLessThan

        /// <summary>Verifies the argument is less than a value.</summary>
        /// <param name="source">The source value.</param>
        /// <param name="value">The value to compare against.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than the value.</exception>        
        public static AndArgumentConstraint<T> IsLessThan<T> ( this ArgumentConstraint<T> source, T value ) where T : IComparable<T>
        {
            return IsLessThan<T>(source, value, "Value must be less than {0}", value);
        }

        /// <summary>Verifies the argument is less than a value.</summary>
        /// <param name="source">The source value.</param>
        /// <param name="value">The value to compare against.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than the value.</exception>        
        public static AndArgumentConstraint<T> IsLessThan<T> ( this ArgumentConstraint<T> source, T value,
                                                                  string message, params object[] messageArgs ) where T : IComparable<T>
        {
            if (source.Argument.Value.CompareTo(value) < 0)
                return new AndArgumentConstraint<T>(source);

            throw new ArgumentOutOfRangeException(source.Argument.Name, String.Format(message, messageArgs));
        }
        #endregion

        #region IsLessThanOrEqualTo

        /// <summary>Verifies the argument is less than or equal to a value.</summary>
        /// <param name="source">The source value.</param>
        /// <param name="value">The value to compare against.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than or equal to the value.</exception>        
        public static AndArgumentConstraint<T> IsLessThanOrEqualTo<T> ( this ArgumentConstraint<T> source, T value ) where T : IComparable<T>
        {
            return IsLessThanOrEqualTo<T>(source, value, "Value must be less than or equal to {0}", value);
        }

        /// <summary>Verifies the argument is less than or equal to a value.</summary>
        /// <param name="source">The source value.</param>
        /// <param name="value">The value to compare against.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than or equal to the value.</exception>        
        public static AndArgumentConstraint<T> IsLessThanOrEqualTo<T> ( this ArgumentConstraint<T> source, T value,
                                                                  string message, params object[] messageArgs ) where T : IComparable<T>
        {
            if (source.Argument.Value.CompareTo(value) <= 0)
                return new AndArgumentConstraint<T>(source);

            throw new ArgumentOutOfRangeException(source.Argument.Name, String.Format(message, messageArgs));
        }
        #endregion

        #region IsNotEqualTo

        /// <summary>Verifies the argument is not equal to a value.</summary>
        /// <param name="source">The source value.</param>
        /// <param name="value">The value to compare against.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is equal to the value.</exception>        
        public static AndArgumentConstraint<T> IsNotEqualTo<T> ( this ArgumentConstraint<T> source, T value ) where T : IComparable<T>
        {
            return IsNotEqualTo<T>(source, value, "Value must not be equal to {0}", value);
        }

        /// <summary>Verifies the argument is not equal to a value.</summary>
        /// <param name="source">The source value.</param>
        /// <param name="value">The value to compare against.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is equal to the value.</exception>        
        public static AndArgumentConstraint<T> IsNotEqualTo<T> ( this ArgumentConstraint<T> source, T value,
                                                                  string message, params object[] messageArgs ) where T : IComparable<T>
        {
            if (source.Argument.Value.CompareTo(value) != 0)
                return new AndArgumentConstraint<T>(source);

            throw new ArgumentOutOfRangeException(source.Argument.Name, String.Format(message, messageArgs));
        }
        #endregion
    }
}