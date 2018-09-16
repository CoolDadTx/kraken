/*
 * Copyright © 2013 Michael Taylor
 * All Rights Reserved
 */  
using System;
using System.Collections.Generic;
using System.Linq;

namespace P3Net.Kraken.Diagnostics
{
    /// <summary>Provides extensions for <see cref="ArgumentConstraint{String}"/>.</summary>
    public static class StringArgumentConstraintExtensions
    {
        #region HasLengthBetween

        /// <summary>Verifies the source is within a certain length.</summary>
        /// <param name="source">The source.</param>
        /// <param name="minLength">The minimum length.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentException">String is not within length.</exception>
        public static AndArgumentConstraint<string> HasLengthBetween ( this ArgumentConstraint<string> source, int minLength, int maxLength )
        {
            return HasLengthBetween(source, minLength, maxLength, "String must be between {0} and {1} characters long.", minLength, maxLength);
        }

        /// <summary>Verifies the source is within a certain length.</summary>
        /// <param name="source">The source.</param>
        /// <param name="minLength">The minimum length.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentException">String is not within length.</exception>
        public static AndArgumentConstraint<string> HasLengthBetween ( this ArgumentConstraint<string> source, int minLength, int maxLength, string message, params object[] args )
        {
            var len = (source.Argument.Value != null) ? source.Argument.Value.Length : 0;

            if (len < minLength || len > maxLength)
                throw new ArgumentException(String.Format(message, args), source.Argument.Name);

            return new AndArgumentConstraint<string>(source);
        }
        #endregion

        #region HasMaximumLength

        /// <summary>Verifies the source is a maximum length.</summary>
        /// <param name="source">The source.</param>
        /// <param name="value">The maximum length.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentException">String is longer than <paramref name="value"/>.</exception>
        public static AndArgumentConstraint<string> HasMaximumLength ( this ArgumentConstraint<string> source, int value )
        {
            return HasMaximumLength(source, value, "String must be at most {0} characters long.", value);
        }

        /// <summary>Verifies the source is a maximum length.</summary>
        /// <param name="source">The source.</param>
        /// <param name="value">The maximum length.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentException">String is longer than <paramref name="value"/>.</exception>
        public static AndArgumentConstraint<string> HasMaximumLength ( this ArgumentConstraint<string> source, int value, string message, params object[] args )
        {
            var len = (source.Argument.Value != null) ? source.Argument.Value.Length : 0;

            if (len > value)
                throw new ArgumentException(String.Format(message, args), source.Argument.Name);

            return new AndArgumentConstraint<string>(source);
        }
        #endregion

        #region HasMinimumLength

        /// <summary>Verifies the source is a minimum length.</summary>
        /// <param name="source">The source.</param>
        /// <param name="value">The minimum length.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentException">String is less than <paramref name="value"/>.</exception>
        public static AndArgumentConstraint<string> HasMinimumLength ( this ArgumentConstraint<string> source, int value )
        {
            return HasMinimumLength(source, value, "String must be at least {0} characters long.", value);
        }

        /// <summary>Verifies the source is a minimum length.</summary>
        /// <param name="source">The source.</param>
        /// <param name="value">The minimum length.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentException">String is less than <paramref name="value"/>.</exception>
        public static AndArgumentConstraint<string> HasMinimumLength ( this ArgumentConstraint<string> source, int value, string message, params object[] args )
        {
            var len = (source.Argument.Value != null) ? source.Argument.Value.Length : 0;

            if (len < value)
                throw new ArgumentException(String.Format(message, args), source.Argument.Name);

            return new AndArgumentConstraint<string>(source);
        }
        #endregion        

        #region IsAlpha

        /// <summary>Verifies the source contains only letters.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentException">The string contains non-letters.</exception>
        public static AndArgumentConstraint<string> IsAlpha ( this ArgumentConstraint<string> source )
        {
            if (!source.Argument.Value.IsAlpha())
                throw new ArgumentException("String must consist of only letters.", source.Argument.Name);

            return new AndArgumentConstraint<string>(source);
        }

        /// <summary>Verifies the source contains only letters.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentException">The string contains non-letters.</exception>
        public static AndArgumentConstraint<string> IsAlpha ( this ArgumentConstraint<string> source, string message, params object[] args )
        {
            if (!source.Argument.Value.IsAlpha())
                throw new ArgumentException(String.Format(message, args), source.Argument.Name);

            return new AndArgumentConstraint<string>(source);
        }
        #endregion

        #region IsAlphaNumeric

        /// <summary>Verifies the source contains only letters or digits.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentException">The string contains non-letters or digits.</exception>
        public static AndArgumentConstraint<string> IsAlphanumeric ( this ArgumentConstraint<string> source )
        {
            if (!source.Argument.Value.IsAlphaNumeric())
                throw new ArgumentException("String must consist of only letters and digits.", source.Argument.Name);

            return new AndArgumentConstraint<string>(source);
        }

        /// <summary>Verifies the source contains only letters or digits.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentException">The string contains non-letters or digits.</exception>
        public static AndArgumentConstraint<string> IsAlphanumeric ( this ArgumentConstraint<string> source, string message, params object[] args )
        {
            if (!source.Argument.Value.IsAlphaNumeric())
                throw new ArgumentException(String.Format(message, args), source.Argument.Name);

            return new AndArgumentConstraint<string>(source);
        }
        #endregion

        #region IsNotNullOrEmpty

        /// <summary>Verifies the source is not <see langword="null"/> or empty.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentNullException">The string is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The string is empty.</exception>
        public static AndArgumentConstraint<string> IsNotNullOrEmpty ( this ArgumentConstraint<string> source )
        {
            if (source.Argument.Value == null)
                throw new ArgumentNullException(source.Argument.Name);

            if (source.Argument.Value.Length == 0)
                throw new ArgumentException("String cannot be empty.", source.Argument.Name);

            return new AndArgumentConstraint<string>(source);
        }

        /// <summary>Verifies the source is not <see langword="null"/> or empty.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentNullException">The string is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The string is empty.</exception>
        public static AndArgumentConstraint<string> IsNotNullOrEmpty ( this ArgumentConstraint<string> source, string message, params object[] args )
        {
            if (source.Argument.Value == null)
                throw new ArgumentNullException(source.Argument.Name, String.Format(message, args));

            if (source.Argument.Value.Length == 0)
                throw new ArgumentException(source.Argument.Name, String.Format(message, args));

            return new AndArgumentConstraint<string>(source);
        }
        #endregion

        #region IsNotNullOrWhiteSpace

        /// <summary>Verifies the source is not <see langword="null"/> or only contains whitespace.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentNullException">The string is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The string is empty.</exception>
        public static AndArgumentConstraint<string> IsNotNullOrWhiteSpace ( this ArgumentConstraint<string> source )
        {
            if (source.Argument.Value == null)
                throw new ArgumentNullException(source.Argument.Name);

            if (String.IsNullOrWhiteSpace(source.Argument.Value))
                throw new ArgumentException("String cannot be empty.", source.Argument.Name);

            return new AndArgumentConstraint<string>(source);
        }

        /// <summary>Verifies the source is not <see langword="null"/> or only contains whitespace.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentNullException">The string is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The string is empty.</exception>
        public static AndArgumentConstraint<string> IsNotNullOrWhiteSpace ( this ArgumentConstraint<string> source, string message, params object[] args )
        {
            if (source.Argument.Value == null)
                throw new ArgumentNullException(source.Argument.Name, String.Format(message, args));

            if (String.IsNullOrWhiteSpace(source.Argument.Value))
                throw new ArgumentException(String.Format(message, args), source.Argument.Name);

            return new AndArgumentConstraint<string>(source);
        }
        #endregion        

        #region IsNumeric

        /// <summary>Verifies the source contains only digits.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentException">The string contains non-digits.</exception>
        public static AndArgumentConstraint<string> IsNumeric ( this ArgumentConstraint<string> source )
        {
            if (!source.Argument.Value.IsNumeric())
                throw new ArgumentException("String must consist of only digits.", source.Argument.Name);

            return new AndArgumentConstraint<string>(source);
        }

        /// <summary>Verifies the source contains only digits.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentException">The string contains non-digits.</exception>
        public static AndArgumentConstraint<string> IsNumeric ( this ArgumentConstraint<string> source, string message, params object[] args )
        {
            if (!source.Argument.Value.IsNumeric())
                throw new ArgumentException(String.Format(message, args), source.Argument.Name);

            return new AndArgumentConstraint<string>(source);
        }
        #endregion
    }
}
