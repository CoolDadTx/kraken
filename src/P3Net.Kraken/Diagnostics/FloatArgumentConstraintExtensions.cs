/*
 * Copyright © 2013 Michael Taylor
 * All Rights Reserved
 */  
using System;
using System.Collections.Generic;
using System.Linq;

namespace P3Net.Kraken.Diagnostics
{
    /// <summary>Provides extension methods for floating point types of <see cref="ArgumentConstraint{T}"/>.</summary>
    public static class FloatArgumentConstraintExtensions
    {
        #region Float

        #region IsGreaterThanOrEqualToZero

        /// <summary>Verifies the argument is greater than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than or equal to zero.</exception>
        public static AndArgumentConstraint<float> IsGreaterThanOrEqualToZero ( this ArgumentConstraint<float> source )
        {
            return IsGreaterThanOrEqualToZero(source, "Value must be greater than or equal to zero.");
        }

        /// <summary>Verifies the argument is greater than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than or equal to zero.</exception>
        public static AndArgumentConstraint<float> IsGreaterThanOrEqualToZero ( this ArgumentConstraint<float> source, string message, params object[] messageArgs )
        {
            return source.IsGreaterThanOrEqualTo(default(float), message, messageArgs);
        }
        #endregion

        #region IsGreaterThanZero

        /// <summary>Verifies the argument is greater than zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than zero.</exception>
        public static AndArgumentConstraint<float> IsGreaterThanZero ( this ArgumentConstraint<float> source  )
        {
            return IsGreaterThanZero(source, "Value must be greater than zero.");
        }

        /// <summary>Verifies the argument is greater than zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than zero.</exception>
        public static AndArgumentConstraint<float> IsGreaterThanZero ( this ArgumentConstraint<float> source, string message, params object[] messageArgs )
        {
            return source.IsGreaterThan(default(float), message, messageArgs);
        }
        #endregion

        #region IsLessThanOrEqualToZero

        /// <summary>Verifies the argument is less than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than or equal to zero.</exception>
        public static AndArgumentConstraint<float> IsLessThanOrEqualToZero ( this ArgumentConstraint<float> source )
        {
            return IsLessThanOrEqualToZero(source, "Value must be less than or equal to zero.");
        }

        /// <summary>Verifies the argument is less than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than or equal to zero.</exception>
        public static AndArgumentConstraint<float> IsLessThanOrEqualToZero ( this ArgumentConstraint<float> source, string message, params object[] messageArgs )
        {
            return source.IsLessThanOrEqualTo(default(float), message, messageArgs);
        }
        #endregion

        #region IsLessThanZero

        /// <summary>Verifies the argument is less than zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than zero.</exception>
        public static AndArgumentConstraint<float> IsLessThanZero ( this ArgumentConstraint<float> source )
        {
            return IsLessThanZero(source, "Value must be less than zero.");
        }

        /// <summary>Verifies the argument is less than zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than zero.</exception>
        public static AndArgumentConstraint<float> IsLessThanZero ( this ArgumentConstraint<float> source, string message, params object[] messageArgs )
        {
            return source.IsLessThan(default(float), message, messageArgs);
        }
        #endregion

        #region IsNotZero

        /// <summary>Verifies the argument is not zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is zero.</exception>
        public static AndArgumentConstraint<float> IsNotZero ( this ArgumentConstraint<float> source )
        {
            return IsNotZero(source, "Value cannot be zero.");
        }

        /// <summary>Verifies the argument is not zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is zero.</exception>
        public static AndArgumentConstraint<float> IsNotZero ( this ArgumentConstraint<float> source, string message, params object[] messageArgs )
        {
            return source.IsNotEqualTo(default(float), message, messageArgs);
        }
        #endregion

        #region IsZero

        /// <summary>Verifies the argument is zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not zero.</exception>
        public static AndArgumentConstraint<float> IsZero ( this ArgumentConstraint<float> source )
        {
            return IsZero(source, "Value must be zero.");
        }

        /// <summary>Verifies the argument is zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not zero.</exception>
        public static AndArgumentConstraint<float> IsZero ( this ArgumentConstraint<float> source, string message, params object[] messageArgs )
        {
            return source.IsEqualTo(default(float), message, messageArgs);
        }
        #endregion

        #endregion

        #region Double

        #region IsGreaterThanOrEqualToZero

        /// <summary>Verifies the argument is greater than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than or equal to zero.</exception>
        public static AndArgumentConstraint<double> IsGreaterThanOrEqualToZero ( this ArgumentConstraint<double> source )
        {
            return IsGreaterThanOrEqualToZero(source, "Value must be greater than or equal to zero.");
        }

        /// <summary>Verifies the argument is greater than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than or equal to zero.</exception>
        public static AndArgumentConstraint<double> IsGreaterThanOrEqualToZero ( this ArgumentConstraint<double> source, string message, params object[] messageArgs )
        {
            return source.IsGreaterThanOrEqualTo(default(double), message, messageArgs);
        }
        #endregion

        #region IsGreaterThanZero

        /// <summary>Verifies the argument is greater than zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than zero.</exception>
        public static AndArgumentConstraint<double> IsGreaterThanZero ( this ArgumentConstraint<double> source )
        {
            return IsGreaterThanZero(source, "Value must be greater than zero.");
        }

        /// <summary>Verifies the argument is greater than zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than zero.</exception>
        public static AndArgumentConstraint<double> IsGreaterThanZero ( this ArgumentConstraint<double> source, string message, params object[] messageArgs )
        {
            return source.IsGreaterThan(default(double), message, messageArgs);
        }
        #endregion

        #region IsLessThanOrEqualToZero

        /// <summary>Verifies the argument is less than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than or equal to zero.</exception>
        public static AndArgumentConstraint<double> IsLessThanOrEqualToZero ( this ArgumentConstraint<double> source )
        {
            return IsLessThanOrEqualToZero(source, "Value must be less than or equal to zero.");
        }

        /// <summary>Verifies the argument is less than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than or equal to zero.</exception>
        public static AndArgumentConstraint<double> IsLessThanOrEqualToZero ( this ArgumentConstraint<double> source, string message, params object[] messageArgs )
        {
            return source.IsLessThanOrEqualTo(default(double), message, messageArgs);
        }
        #endregion

        #region IsLessThanZero

        /// <summary>Verifies the argument is less than zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than zero.</exception>
        public static AndArgumentConstraint<double> IsLessThanZero ( this ArgumentConstraint<double> source )
        {
            return IsLessThanZero(source, "Value must be less than zero.");
        }

        /// <summary>Verifies the argument is less than zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than zero.</exception>
        public static AndArgumentConstraint<double> IsLessThanZero ( this ArgumentConstraint<double> source, string message, params object[] messageArgs )
        {
            return source.IsLessThan(default(double), message, messageArgs);
        }
        #endregion

        #region IsNotZero

        /// <summary>Verifies the argument is not zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is zero.</exception>
        public static AndArgumentConstraint<double> IsNotZero ( this ArgumentConstraint<double> source )
        {
            return IsNotZero(source, "Value cannot be zero.");
        }

        /// <summary>Verifies the argument is not zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is zero.</exception>
        public static AndArgumentConstraint<double> IsNotZero ( this ArgumentConstraint<double> source, string message, params object[] messageArgs )
        {
            return source.IsNotEqualTo(default(double), message, messageArgs);
        }
        #endregion

        #region IsZero

        /// <summary>Verifies the argument is zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not zero.</exception>
        public static AndArgumentConstraint<double> IsZero ( this ArgumentConstraint<double> source )
        {
            return IsZero(source, "Value must be zero.");
        }

        /// <summary>Verifies the argument is zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not zero.</exception>
        public static AndArgumentConstraint<double> IsZero ( this ArgumentConstraint<double> source, string message, params object[] messageArgs )
        {
            return source.IsEqualTo(default(double), message, messageArgs);
        }
        #endregion

        #endregion

        #region Decimal

        #region IsGreaterThanOrEqualToZero

        /// <summary>Verifies the argument is greater than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than or equal to zero.</exception>
        public static AndArgumentConstraint<decimal> IsGreaterThanOrEqualToZero ( this ArgumentConstraint<decimal> source )
        {
            return IsGreaterThanOrEqualToZero(source, "Value must be greater than or equal to zero.");
        }

        /// <summary>Verifies the argument is greater than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than or equal to zero.</exception>
        public static AndArgumentConstraint<decimal> IsGreaterThanOrEqualToZero ( this ArgumentConstraint<decimal> source, string message, params object[] messageArgs )
        {
            return source.IsGreaterThanOrEqualTo(default(decimal), message, messageArgs);
        }
        #endregion

        #region IsGreaterThanZero

        /// <summary>Verifies the argument is greater than zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than zero.</exception>
        public static AndArgumentConstraint<decimal> IsGreaterThanZero ( this ArgumentConstraint<decimal> source )
        {
            return IsGreaterThanZero(source, "Value must be greater than zero.");
        }

        /// <summary>Verifies the argument is greater than zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than zero.</exception>
        public static AndArgumentConstraint<decimal> IsGreaterThanZero ( this ArgumentConstraint<decimal> source, string message, params object[] messageArgs )
        {
            return source.IsGreaterThan(default(decimal), message, messageArgs);
        }
        #endregion

        #region IsLessThanOrEqualToZero

        /// <summary>Verifies the argument is less than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than or equal to zero.</exception>
        public static AndArgumentConstraint<decimal> IsLessThanOrEqualToZero ( this ArgumentConstraint<decimal> source )
        {
            return IsLessThanOrEqualToZero(source, "Value must be less than or equal to zero.");
        }

        /// <summary>Verifies the argument is less than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than or equal to zero.</exception>
        public static AndArgumentConstraint<decimal> IsLessThanOrEqualToZero ( this ArgumentConstraint<decimal> source, string message, params object[] messageArgs )
        {
            return source.IsLessThanOrEqualTo(default(decimal), message, messageArgs);
        }
        #endregion

        #region IsLessThanZero

        /// <summary>Verifies the argument is less than zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than zero.</exception>
        public static AndArgumentConstraint<decimal> IsLessThanZero ( this ArgumentConstraint<decimal> source )
        {
            return IsLessThanZero(source, "Value must be less than zero.");
        }

        /// <summary>Verifies the argument is less than zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than zero.</exception>
        public static AndArgumentConstraint<decimal> IsLessThanZero ( this ArgumentConstraint<decimal> source, string message, params object[] messageArgs )
        {
            return source.IsLessThan(default(decimal), message, messageArgs);
        }
        #endregion

        #region IsNotZero

        /// <summary>Verifies the argument is not zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is zero.</exception>
        public static AndArgumentConstraint<decimal> IsNotZero ( this ArgumentConstraint<decimal> source )
        {
            return IsNotZero(source, "Value cannot be zero.");
        }

        /// <summary>Verifies the argument is not zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is zero.</exception>
        public static AndArgumentConstraint<decimal> IsNotZero ( this ArgumentConstraint<decimal> source, string message, params object[] messageArgs )
        {
            return source.IsNotEqualTo(default(decimal), message, messageArgs);
        }
        #endregion

        #region IsZero

        /// <summary>Verifies the argument is zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not zero.</exception>
        public static AndArgumentConstraint<decimal> IsZero ( this ArgumentConstraint<decimal> source )
        {
            return IsZero(source, "Value must be zero.");
        }

        /// <summary>Verifies the argument is zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not zero.</exception>
        public static AndArgumentConstraint<decimal> IsZero ( this ArgumentConstraint<decimal> source, string message, params object[] messageArgs )
        {
            return source.IsEqualTo(default(decimal), message, messageArgs);
        }
        #endregion

        #endregion
    }
}
