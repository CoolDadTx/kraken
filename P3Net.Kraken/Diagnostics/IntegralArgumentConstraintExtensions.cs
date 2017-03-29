/*
 * Copyright © 2013 Michael Taylor
 * All Rights Reserved
 */  
using System;
using System.Collections.Generic;
using System.Linq;

namespace P3Net.Kraken.Diagnostics
{
    /// <summary>Provides extension methods for integral types of <see cref="ArgumentConstraint{T}"/>.</summary>
    public static class IntegralArgumentConstraintExtensions
    {      
        #region SByte

        #region IsGreaterThanOrEqualToZero

        /// <summary>Verifies the argument is greater than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than or equal to zero.</exception>
        [CLSCompliant(false)]
        public static AndArgumentConstraint<sbyte> IsGreaterThanOrEqualToZero ( this ArgumentConstraint<sbyte> source )
        {
            return IsGreaterThanOrEqualToZero(source, "Value must be greater than or equal to zero.");
        }

        /// <summary>Verifies the argument is greater than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than or equal to zero.</exception>
        [CLSCompliant(false)]
        public static AndArgumentConstraint<sbyte> IsGreaterThanOrEqualToZero ( this ArgumentConstraint<sbyte> source, string message, params object[] messageArgs )
        {
            return source.IsGreaterThanOrEqualTo(default(sbyte), message, messageArgs);
        }
        #endregion

        #region IsGreaterThanZero

        /// <summary>Verifies the argument is greater than zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than zero.</exception>
        [CLSCompliant(false)]
        public static AndArgumentConstraint<sbyte> IsGreaterThanZero ( this ArgumentConstraint<sbyte> source  )
        {
            return IsGreaterThanZero(source, "Value must be greater than zero.");
        }

        /// <summary>Verifies the argument is greater than zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than zero.</exception>
        [CLSCompliant(false)]
        public static AndArgumentConstraint<sbyte> IsGreaterThanZero ( this ArgumentConstraint<sbyte> source, string message, params object[] messageArgs )
        {
            return source.IsGreaterThan(default(sbyte), message, messageArgs);
        }
        #endregion

        #region IsLessThanOrEqualToZero

        /// <summary>Verifies the argument is less than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than or equal to zero.</exception>
        [CLSCompliant(false)]
        public static AndArgumentConstraint<sbyte> IsLessThanOrEqualToZero ( this ArgumentConstraint<sbyte> source )
        {
            return IsLessThanOrEqualToZero(source, "Value must be less than or equal to zero.");
        }

        /// <summary>Verifies the argument is less than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than or equal to zero.</exception>
        [CLSCompliant(false)]
        public static AndArgumentConstraint<sbyte> IsLessThanOrEqualToZero ( this ArgumentConstraint<sbyte> source, string message, params object[] messageArgs )
        {
            return source.IsLessThanOrEqualTo(default(sbyte), message, messageArgs);
        }
        #endregion

        #region IsLessThanZero

        /// <summary>Verifies the argument is less than zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than zero.</exception>
        [CLSCompliant(false)]
        public static AndArgumentConstraint<sbyte> IsLessThanZero ( this ArgumentConstraint<sbyte> source )
        {
            return IsLessThanZero(source, "Value must be less than zero.");
        }

        /// <summary>Verifies the argument is less than zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than zero.</exception>
        [CLSCompliant(false)]
        public static AndArgumentConstraint<sbyte> IsLessThanZero ( this ArgumentConstraint<sbyte> source, string message, params object[] messageArgs )
        {
            return source.IsLessThan(default(sbyte), message, messageArgs);
        }
        #endregion

        #region IsNotZero

        /// <summary>Verifies the argument is not zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is zero.</exception>
        [CLSCompliant(false)]
        public static AndArgumentConstraint<sbyte> IsNotZero ( this ArgumentConstraint<sbyte> source )
        {
            return IsNotZero(source, "Value cannot be zero.");
        }

        /// <summary>Verifies the argument is not zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is zero.</exception>
        [CLSCompliant(false)]
        public static AndArgumentConstraint<sbyte> IsNotZero ( this ArgumentConstraint<sbyte> source, string message, params object[] messageArgs )
        {
            return source.IsNotEqualTo(default(sbyte), message, messageArgs);
        }
        #endregion

        #region IsZero

        /// <summary>Verifies the argument is zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not zero.</exception>
        [CLSCompliant(false)]
        public static AndArgumentConstraint<sbyte> IsZero ( this ArgumentConstraint<sbyte> source )
        {
            return IsZero(source, "Value must be zero.");
        }

        /// <summary>Verifies the argument is zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not zero.</exception>
        [CLSCompliant(false)]
        public static AndArgumentConstraint<sbyte> IsZero ( this ArgumentConstraint<sbyte> source, string message, params object[] messageArgs )
        {
            return source.IsEqualTo(default(sbyte), message, messageArgs);
        }
        #endregion

        #endregion

        #region Int16

        #region IsGreaterThanOrEqualToZero

        /// <summary>Verifies the argument is greater than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than or equal to zero.</exception>
        public static AndArgumentConstraint<short> IsGreaterThanOrEqualToZero ( this ArgumentConstraint<short> source )
        {
            return IsGreaterThanOrEqualToZero(source, "Value must be greater than or equal to zero.");
        }

        /// <summary>Verifies the argument is greater than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than or equal to zero.</exception>
        public static AndArgumentConstraint<short> IsGreaterThanOrEqualToZero ( this ArgumentConstraint<short> source, string message, params object[] messageArgs )
        {
            return source.IsGreaterThanOrEqualTo(default(short), message, messageArgs);
        }
        #endregion

        #region IsGreaterThanZero

        /// <summary>Verifies the argument is greater than zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than zero.</exception>
        public static AndArgumentConstraint<short> IsGreaterThanZero ( this ArgumentConstraint<short> source )
        {
            return IsGreaterThanZero(source, "Value must be greater than zero.");
        }

        /// <summary>Verifies the argument is greater than zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than zero.</exception>
        public static AndArgumentConstraint<short> IsGreaterThanZero ( this ArgumentConstraint<short> source, string message, params object[] messageArgs )
        {
            return source.IsGreaterThan(default(short), message, messageArgs);
        }
        #endregion

        #region IsLessThanOrEqualToZero

        /// <summary>Verifies the argument is less than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than or equal to zero.</exception>
        public static AndArgumentConstraint<short> IsLessThanOrEqualToZero ( this ArgumentConstraint<short> source )
        {
            return IsLessThanOrEqualToZero(source, "Value must be less than or equal to zero.");
        }

        /// <summary>Verifies the argument is less than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than or equal to zero.</exception>
        public static AndArgumentConstraint<short> IsLessThanOrEqualToZero ( this ArgumentConstraint<short> source, string message, params object[] messageArgs )
        {
            return source.IsLessThanOrEqualTo(default(short), message, messageArgs);
        }
        #endregion

        #region IsLessThanZero

        /// <summary>Verifies the argument is less than zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than zero.</exception>
        public static AndArgumentConstraint<short> IsLessThanZero ( this ArgumentConstraint<short> source )
        {
            return IsLessThanZero(source, "Value must be less than zero.");
        }

        /// <summary>Verifies the argument is less than zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than zero.</exception>
        public static AndArgumentConstraint<short> IsLessThanZero ( this ArgumentConstraint<short> source, string message, params object[] messageArgs )
        {
            return source.IsLessThan(default(short), message, messageArgs);
        }
        #endregion

        #region IsNotZero

        /// <summary>Verifies the argument is not zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is zero.</exception>
        public static AndArgumentConstraint<short> IsNotZero ( this ArgumentConstraint<short> source )
        {
            return IsNotZero(source, "Value cannot be zero.");
        }

        /// <summary>Verifies the argument is not zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is zero.</exception>
        public static AndArgumentConstraint<short> IsNotZero ( this ArgumentConstraint<short> source, string message, params object[] messageArgs )
        {
            return source.IsNotEqualTo(default(short), message, messageArgs);
        }
        #endregion

        #region IsZero

        /// <summary>Verifies the argument is zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not zero.</exception>
        public static AndArgumentConstraint<short> IsZero ( this ArgumentConstraint<short> source )
        {
            return IsZero(source, "Value must be zero.");
        }

        /// <summary>Verifies the argument is zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not zero.</exception>
        public static AndArgumentConstraint<short> IsZero ( this ArgumentConstraint<short> source, string message, params object[] messageArgs )
        {
            return source.IsEqualTo(default(short), message, messageArgs);
        }
        #endregion

        #endregion

        #region Int32

        #region IsGreaterThanOrEqualToZero

        /// <summary>Verifies the argument is greater than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than or equal to zero.</exception>
        public static AndArgumentConstraint<int> IsGreaterThanOrEqualToZero ( this ArgumentConstraint<int> source )
        {
            return IsGreaterThanOrEqualToZero(source, "Value must be greater than or equal to zero.");
        }

        /// <summary>Verifies the argument is greater than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than or equal to zero.</exception>
        public static AndArgumentConstraint<int> IsGreaterThanOrEqualToZero ( this ArgumentConstraint<int> source, string message, params object[] messageArgs )
        {
            return source.IsGreaterThanOrEqualTo(0, message, messageArgs);
        }
        #endregion

        #region IsGreaterThanZero

        /// <summary>Verifies the argument is greater than zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than zero.</exception>
        public static AndArgumentConstraint<int> IsGreaterThanZero ( this ArgumentConstraint<int> source )
        {
            return IsGreaterThanZero(source, "Value must be greater than zero.");
        }

        /// <summary>Verifies the argument is greater than zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than zero.</exception>
        public static AndArgumentConstraint<int> IsGreaterThanZero ( this ArgumentConstraint<int> source, string message, params object[] messageArgs )
        {
            return source.IsGreaterThan(0, message, messageArgs);
        }
        #endregion

        #region IsLessThanOrEqualToZero

        /// <summary>Verifies the argument is less than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than or equal to zero.</exception>
        public static AndArgumentConstraint<int> IsLessThanOrEqualToZero ( this ArgumentConstraint<int> source )
        {
            return IsLessThanOrEqualToZero(source, "Value must be less than or equal to zero.");
        }

        /// <summary>Verifies the argument is less than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than or equal to zero.</exception>
        public static AndArgumentConstraint<int> IsLessThanOrEqualToZero ( this ArgumentConstraint<int> source, string message, params object[] messageArgs )
        {
            return source.IsLessThanOrEqualTo(0, message, messageArgs);
        }
        #endregion

        #region IsLessThanZero

        /// <summary>Verifies the argument is less than zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than zero.</exception>
        public static AndArgumentConstraint<int> IsLessThanZero ( this ArgumentConstraint<int> source )
        {
            return IsLessThanZero(source, "Value must be less than zero.");
        }

        /// <summary>Verifies the argument is less than zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than zero.</exception>
        public static AndArgumentConstraint<int> IsLessThanZero ( this ArgumentConstraint<int> source, string message, params object[] messageArgs )
        {
            return source.IsLessThan(0, message, messageArgs);
        }
        #endregion

        #region IsNotZero

        /// <summary>Verifies the argument is not zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is zero.</exception>
        public static AndArgumentConstraint<int> IsNotZero ( this ArgumentConstraint<int> source )
        {
            return IsNotZero(source, "Value cannot be zero.");
        }

        /// <summary>Verifies the argument is not zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is zero.</exception>
        public static AndArgumentConstraint<int> IsNotZero ( this ArgumentConstraint<int> source, string message, params object[] messageArgs )
        {
            return source.IsNotEqualTo(0, message, messageArgs);
        }
        #endregion

        #region IsZero

        /// <summary>Verifies the argument is zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not zero.</exception>
        public static AndArgumentConstraint<int> IsZero ( this ArgumentConstraint<int> source )
        {
            return IsZero(source, "Value must be zero.");
        }

        /// <summary>Verifies the argument is zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not zero.</exception>
        public static AndArgumentConstraint<int> IsZero ( this ArgumentConstraint<int> source, string message, params object[] messageArgs )
        {
            return source.IsEqualTo(0, message, messageArgs);
        }
        #endregion

        #endregion

        #region Int64

        #region IsGreaterThanOrEqualToZero

        /// <summary>Verifies the argument is greater than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constralong.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than or equal to zero.</exception>
        public static AndArgumentConstraint<long> IsGreaterThanOrEqualToZero ( this ArgumentConstraint<long> source )
        {
            return IsGreaterThanOrEqualToZero(source, "Value must be greater than or equal to zero.");
        }

        /// <summary>Verifies the argument is greater than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constralong.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than or equal to zero.</exception>
        public static AndArgumentConstraint<long> IsGreaterThanOrEqualToZero ( this ArgumentConstraint<long> source, string message, params object[] messageArgs )
        {
            return source.IsGreaterThanOrEqualTo(default(long), message, messageArgs);
        }
        #endregion

        #region IsGreaterThanZero

        /// <summary>Verifies the argument is greater than zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constralong.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than zero.</exception>
        public static AndArgumentConstraint<long> IsGreaterThanZero ( this ArgumentConstraint<long> source )
        {
            return IsGreaterThanZero(source, "Value must be greater than zero.");
        }

        /// <summary>Verifies the argument is greater than zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constralong.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not greater than zero.</exception>
        public static AndArgumentConstraint<long> IsGreaterThanZero ( this ArgumentConstraint<long> source, string message, params object[] messageArgs )
        {
            return source.IsGreaterThan(default(long), message, messageArgs);
        }
        #endregion

        #region IsLessThanOrEqualToZero

        /// <summary>Verifies the argument is less than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constralong.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than or equal to zero.</exception>
        public static AndArgumentConstraint<long> IsLessThanOrEqualToZero ( this ArgumentConstraint<long> source )
        {
            return IsLessThanOrEqualToZero(source, "Value must be less than or equal to zero.");
        }

        /// <summary>Verifies the argument is less than or equal to zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constralong.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than or equal to zero.</exception>
        public static AndArgumentConstraint<long> IsLessThanOrEqualToZero ( this ArgumentConstraint<long> source, string message, params object[] messageArgs )
        {
            return source.IsLessThanOrEqualTo(default(long), message, messageArgs);
        }
        #endregion

        #region IsLessThanZero

        /// <summary>Verifies the argument is less than zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constralong.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than zero.</exception>
        public static AndArgumentConstraint<long> IsLessThanZero ( this ArgumentConstraint<long> source )
        {
            return IsLessThanZero(source, "Value must be less than zero.");
        }

        /// <summary>Verifies the argument is less than zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constralong.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not less than zero.</exception>
        public static AndArgumentConstraint<long> IsLessThanZero ( this ArgumentConstraint<long> source, string message, params object[] messageArgs )
        {
            return source.IsLessThan(default(long), message, messageArgs);
        }
        #endregion

        #region IsNotZero

        /// <summary>Verifies the argument is not zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constralong.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is zero.</exception>
        public static AndArgumentConstraint<long> IsNotZero ( this ArgumentConstraint<long> source )
        {
            return IsNotZero(source, "Value cannot be zero.");
        }

        /// <summary>Verifies the argument is not zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constralong.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is zero.</exception>
        public static AndArgumentConstraint<long> IsNotZero ( this ArgumentConstraint<long> source, string message, params object[] messageArgs )
        {
            return source.IsNotEqualTo(default(long), message, messageArgs);
        }
        #endregion

        #region IsZero

        /// <summary>Verifies the argument is zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constralong.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not zero.</exception>
        public static AndArgumentConstraint<long> IsZero ( this ArgumentConstraint<long> source )
        {
            return IsZero(source, "Value must be zero.");
        }

        /// <summary>Verifies the argument is zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constralong.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not zero.</exception>
        public static AndArgumentConstraint<long> IsZero ( this ArgumentConstraint<long> source, string message, params object[] messageArgs )
        {
            return source.IsEqualTo(default(long), message, messageArgs);
        }
        #endregion

        #endregion

        #region Byte        
                
        #region IsNotZero

        /// <summary>Verifies the argument is not zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is zero.</exception>
        public static AndArgumentConstraint<byte> IsNotZero ( this ArgumentConstraint<byte> source )
        {
            return IsNotZero(source, "Value cannot be zero.");
        }

        /// <summary>Verifies the argument is not zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is zero.</exception>
        public static AndArgumentConstraint<byte> IsNotZero ( this ArgumentConstraint<byte> source, string message, params object[] messageArgs )
        {
            return source.IsNotEqualTo(default(byte), message, messageArgs);
        }
        #endregion

        #region IsZero

        /// <summary>Verifies the argument is zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not zero.</exception>
        public static AndArgumentConstraint<byte> IsZero ( this ArgumentConstraint<byte> source )
        {
            return IsZero(source, "Value must be zero.");
        }

        /// <summary>Verifies the argument is zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not zero.</exception>
        public static AndArgumentConstraint<byte> IsZero ( this ArgumentConstraint<byte> source, string message, params object[] messageArgs )
        {
            return source.IsEqualTo(default(byte), message, messageArgs);
        }
        #endregion

        #endregion

        #region UInt16

        #region IsNotZero

        /// <summary>Verifies the argument is not zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is zero.</exception>
        [CLSCompliant(false)]
        public static AndArgumentConstraint<ushort> IsNotZero ( this ArgumentConstraint<ushort> source )
        {
            return IsNotZero(source, "Value cannot be zero.");
        }

        /// <summary>Verifies the argument is not zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is zero.</exception>
        [CLSCompliant(false)]
        public static AndArgumentConstraint<ushort> IsNotZero ( this ArgumentConstraint<ushort> source, string message, params object[] messageArgs )
        {
            return source.IsNotEqualTo(default(ushort), message, messageArgs);
        }
        #endregion

        #region IsZero

        /// <summary>Verifies the argument is zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not zero.</exception>
        [CLSCompliant(false)]
        public static AndArgumentConstraint<ushort> IsZero ( this ArgumentConstraint<ushort> source )
        {
            return IsZero(source, "Value must be zero.");
        }

        /// <summary>Verifies the argument is zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not zero.</exception>
        [CLSCompliant(false)]
        public static AndArgumentConstraint<ushort> IsZero ( this ArgumentConstraint<ushort> source, string message, params object[] messageArgs )
        {
            return source.IsEqualTo(default(ushort), message, messageArgs);
        }
        #endregion

        #endregion

        #region Uint32

        #region IsNotZero

        /// <summary>Verifies the argument is not zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constrauint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is zero.</exception>
        [CLSCompliant(false)]
        public static AndArgumentConstraint<uint> IsNotZero ( this ArgumentConstraint<uint> source )
        {
            return IsNotZero(source, "Value cannot be zero.");
        }

        /// <summary>Verifies the argument is not zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constrauint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is zero.</exception>
        [CLSCompliant(false)]
        public static AndArgumentConstraint<uint> IsNotZero ( this ArgumentConstraint<uint> source, string message, params object[] messageArgs )
        {
            return source.IsNotEqualTo(default(uint), message, messageArgs);
        }
        #endregion

        #region IsZero

        /// <summary>Verifies the argument is zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constrauint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not zero.</exception>
        [CLSCompliant(false)]
        public static AndArgumentConstraint<uint> IsZero ( this ArgumentConstraint<uint> source )
        {
            return IsZero(source, "Value must be zero.");
        }

        /// <summary>Verifies the argument is zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constrauint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not zero.</exception>
        [CLSCompliant(false)]
        public static AndArgumentConstraint<uint> IsZero ( this ArgumentConstraint<uint> source, string message, params object[] messageArgs )
        {
            return source.IsEqualTo(default(uint), message, messageArgs);
        }
        #endregion

        #endregion

        #region UInt64

        #region IsNotZero

        /// <summary>Verifies the argument is not zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraulong.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is zero.</exception>
        [CLSCompliant(false)]
        public static AndArgumentConstraint<ulong> IsNotZero ( this ArgumentConstraint<ulong> source )
        {
            return IsNotZero(source, "Value cannot be zero.");
        }

        /// <summary>Verifies the argument is not zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraulong.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is zero.</exception>
        [CLSCompliant(false)]
        public static AndArgumentConstraint<ulong> IsNotZero ( this ArgumentConstraint<ulong> source, string message, params object[] messageArgs )
        {
            return source.IsNotEqualTo(default(ulong), message, messageArgs);
        }
        #endregion

        #region IsZero

        /// <summary>Verifies the argument is zero.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The constraulong.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not zero.</exception>
        [CLSCompliant(false)]
        public static AndArgumentConstraint<ulong> IsZero ( this ArgumentConstraint<ulong> source )
        {
            return IsZero(source, "Value must be zero.");
        }

        /// <summary>Verifies the argument is zero.</summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The message arguments.</param>
        /// <returns>The constraulong.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The argument is not zero.</exception>
        [CLSCompliant(false)]
        public static AndArgumentConstraint<ulong> IsZero ( this ArgumentConstraint<ulong> source, string message, params object[] messageArgs )
        {
            return source.IsEqualTo(default(ulong), message, messageArgs);
        }
        #endregion

        #endregion
    }
}
