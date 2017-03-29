/*
 * Copyright © 2012 Michael Taylor
 * All Rights Reserved
 * 
 * From code copyright Federation of State Medical Boards
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace P3Net.Kraken.Collections
{
    /// <summary>Provides extension methods for object dictionaries.</summary>
    public static class ObjectDictionaryExtensions
    {
        #region GetValueAs...

        /// <summary>Gets a value from the dictionary as a boolean.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value as a string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        /// <exception cref="KeyNotFoundException">The key was not found.</exception>        
        /// <remarks>
        /// A <see langword="null"/> value is returned as the default type value.
        /// </remarks>
        public static bool GetValueAsBoolean<TKey> ( this IDictionary<TKey, object> source, TKey key )
        {
            return TypeConversion.ToBooleanOrDefault(source[key]);            
        }

        /// <summary>Gets a value from the dictionary as a byte.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value as a string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        /// <exception cref="KeyNotFoundException">The key was not found.</exception>        
        /// <remarks>
        /// A <see langword="null"/> value is returned as the default type value.
        /// </remarks>
        public static byte GetValueAsByte<TKey> ( this IDictionary<TKey, object> source, TKey key )
        {
            return TypeConversion.ToByteOrDefault(source[key]);
        }

        /// <summary>Gets a value from the dictionary as a character.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value as a string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        /// <exception cref="KeyNotFoundException">The key was not found.</exception>        
        /// <remarks>
        /// A <see langword="null"/> value is returned as the default type value.
        /// </remarks>
        public static char GetValueAsChar<TKey> ( this IDictionary<TKey, object> source, TKey key )
        {
            return TypeConversion.ToCharOrDefault(source[key]);
        }

        /// <summary>Gets a value from the dictionary as a <see cref="DateTime"/>.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value as a string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        /// <exception cref="KeyNotFoundException">The key was not found.</exception>        
        /// <remarks>
        /// A <see langword="null"/> value is returned as <see cref="DateTime.MinValue"/>.
        /// </remarks>
        public static DateTime GetValueAsDateTime<TKey> ( this IDictionary<TKey, object> source, TKey key )
        {
            return TypeConversion.ToDateTimeOrDefault(source[key]);
        }

        /// <summary>Gets a value from the dictionary as a decimal.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value as a string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        /// <exception cref="KeyNotFoundException">The key was not found.</exception>        
        /// <remarks>
        /// A <see langword="null"/> value is returned as the default type value.
        /// </remarks>
        public static decimal GetValueAsDecimal<TKey> ( this IDictionary<TKey, object> source, TKey key )
        {
            return TypeConversion.ToDecimalOrDefault(source[key]);
        }

        /// <summary>Gets a value from the dictionary as a double.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value as a string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        /// <exception cref="KeyNotFoundException">The key was not found.</exception>        
        /// <remarks>
        /// A <see langword="null"/> value is returned as the default type value.
        /// </remarks>
        public static double GetValueAsDouble<TKey> ( this IDictionary<TKey, object> source, TKey key )
        {
            return TypeConversion.ToDoubleOrDefault(source[key]);
        }

        /// <summary>Gets a value from the dictionary as a signed 16-byte integer.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value as a string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        /// <exception cref="KeyNotFoundException">The key was not found.</exception>        
        /// <remarks>
        /// A <see langword="null"/> value is returned as the default type value.
        /// </remarks>
        public static short GetValueAsInt16<TKey> ( this IDictionary<TKey, object> source, TKey key )
        {
            return TypeConversion.ToInt16OrDefault(source[key]);
        }

        /// <summary>Gets a value from the dictionary as a signed 32-byte integer.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value as a string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        /// <exception cref="KeyNotFoundException">The key was not found.</exception>        
        /// <remarks>
        /// A <see langword="null"/> value is returned as the default type value.
        /// </remarks>
        public static int GetValueAsInt32<TKey> ( this IDictionary<TKey, object> source, TKey key )
        {
            return TypeConversion.ToInt32OrDefault(source[key]);
        }

        /// <summary>Gets a value from the dictionary as a signed 64-byte integer.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value as a string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        /// <exception cref="KeyNotFoundException">The key was not found.</exception>        
        /// <remarks>
        /// A <see langword="null"/> value is returned as the default type value.
        /// </remarks>
        public static long GetValueAsInt64<TKey> ( this IDictionary<TKey, object> source, TKey key )
        {
            return TypeConversion.ToInt64OrDefault(source[key]);
        }

        /// <summary>Gets a value from the dictionary as a signed byte.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value as a string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        /// <exception cref="KeyNotFoundException">The key was not found.</exception>        
        /// <remarks>
        /// A <see langword="null"/> value is returned as the default type value.
        /// </remarks>
        [CLSCompliant(false)]
        public static sbyte GetValueAsSByte<TKey> ( this IDictionary<TKey, object> source, TKey key )
        {
            return TypeConversion.ToSByteOrDefault(source[key]);
        }

        /// <summary>Gets a value from the dictionary as a float.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value as a string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        /// <exception cref="KeyNotFoundException">The key was not found.</exception>        
        /// <remarks>
        /// A <see langword="null"/> value is returned as the default type value.
        /// </remarks>
        public static float GetValueAsSingle<TKey> ( this IDictionary<TKey, object> source, TKey key )
        {
            return TypeConversion.ToSingleOrDefault(source[key]);
        }

        /// <summary>Gets a value from the dictionary as a string.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value as a string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        /// <exception cref="KeyNotFoundException">The key was not found.</exception>        
        /// <remarks>
        /// A <see langword="null"/> value is returned as an empty string.
        /// </remarks>
        public static string GetValueAsString<TKey> ( this IDictionary<TKey, object> source, TKey key )
        {
            object value = source[key];

            return (value != null) ? value.ToString() : "";
        }

        /// <summary>Gets a value from the dictionary as an unsigned 16-byte integer.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value as a string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        /// <exception cref="KeyNotFoundException">The key was not found.</exception>        
        /// <remarks>
        /// A <see langword="null"/> value is returned as the default type value.
        /// </remarks>
        [CLSCompliant(false)]
        public static ushort GetValueAsUInt16<TKey> ( this IDictionary<TKey, object> source, TKey key )
        {
            return TypeConversion.ToUInt16OrDefault(source[key]);
        }

        /// <summary>Gets a value from the dictionary as an unsigned 32-byte integer.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value as a string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        /// <exception cref="KeyNotFoundException">The key was not found.</exception>        
        /// <remarks>
        /// A <see langword="null"/> value is returned as the default type value.
        /// </remarks>
        [CLSCompliant(false)]
        public static uint GetValueAsUInt32<TKey> ( this IDictionary<TKey, object> source, TKey key )
        {
            return TypeConversion.ToUInt32OrDefault(source[key]);
        }

        /// <summary>Gets a value from the dictionary as an unsigned 64-byte integer.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value as a string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        /// <exception cref="KeyNotFoundException">The key was not found.</exception>        
        /// <remarks>
        /// A <see langword="null"/> value is returned as the default type value.
        /// </remarks>
        [CLSCompliant(false)]
        public static ulong GetValueAsUInt64<TKey> ( this IDictionary<TKey, object> source, TKey key )
        {
            return TypeConversion.ToUInt64OrDefault(source[key]);
        }
        #endregion

        #region TryGetValueAs...

        /// <summary>Tries to get a value as a boolean.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key to retrieve.</param>
        /// <param name="value">The value.</param>
        /// <returns><see langword="true"/> if the value is returned or <see langword="false"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>        
        public static bool TryGetValueAsBoolean<TKey> ( this IDictionary<TKey, object> source, TKey key, out bool value )
        {
            string result;
            if (TryGetValueAsString(source, key, out result))
            {
                if (!String.IsNullOrEmpty(result) && Boolean.TryParse(result, out value))
                    return true;
            };
            
            value = false;
            return false;
        }

        /// <summary>Tries to get a value as a byte.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key to retrieve.</param>
        /// <param name="value">The value.</param>
        /// <returns><see langword="true"/> if the value is returned or <see langword="false"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>   

        public static bool TryGetValueAsByte<TKey> ( this IDictionary<TKey, object> source, TKey key, out byte value )
        {
            string result;
            if (TryGetValueAsString(source, key, out result))
            {
                if (!String.IsNullOrEmpty(result) && Byte.TryParse(result, out value))
                    return true;
            };

            value = 0;
            return false;
        }

        /// <summary>Tries to get a value as a character.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key to retrieve.</param>
        /// <param name="value">The value.</param>
        /// <returns><see langword="true"/> if the value is returned or <see langword="false"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>        
        public static bool TryGetValueAsChar<TKey> ( this IDictionary<TKey, object> source, TKey key, out char value )
        {
            string result;
            if (TryGetValueAsString(source, key, out result))
            {
                if (!String.IsNullOrEmpty(result) && Char.TryParse(result, out value))
                    return true;
            };

            value = default(char);
            return false;
        }

        /// <summary>Tries to get a value as a <see cref="DateTime"/>.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key to retrieve.</param>
        /// <param name="value">The value.</param>
        /// <returns><see langword="true"/> if the value is returned or <see langword="false"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>        
        /// <remarks>
        /// If the key exists but the value is <see langword="null"/> then the default value for the type is returned.
        /// </remarks>
        public static bool TryGetValueAsDateTime<TKey> ( this IDictionary<TKey, object> source, TKey key, out DateTime value )
        {
            string result;
            if (TryGetValueAsString(source, key, out result))
            {
                if (!String.IsNullOrEmpty(result) && DateTime.TryParse(result, out value))
                    return true;
            };

            value = DateTime.MinValue;
            return false;
        }

        /// <summary>Tries to get a value as a decimal.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key to retrieve.</param>
        /// <param name="value">The value.</param>
        /// <returns><see langword="true"/> if the value is returned or <see langword="false"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>        
        public static bool TryGetValueAsDecimal<TKey> ( this IDictionary<TKey, object> source, TKey key, out decimal value )
        {
            string result;
            if (TryGetValueAsString(source, key, out result))
            {
                if (!String.IsNullOrEmpty(result) && Decimal.TryParse(result, out value))
                    return true;
            };

            value = 0;
            return false;
        }

        /// <summary>Tries to get a value as a double.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key to retrieve.</param>
        /// <param name="value">The value.</param>
        /// <returns><see langword="true"/> if the value is returned or <see langword="false"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>        
        public static bool TryGetValueAsDouble<TKey> ( this IDictionary<TKey, object> source, TKey key, out double value )
        {
            string result;
            if (TryGetValueAsString(source, key, out result))
            {
                if (!String.IsNullOrEmpty(result) && Double.TryParse(result, out value))
                    return true;
            };

            value = 0;
            return false;
        }

        /// <summary>Tries to get a value as a signed 16-bit integer.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key to retrieve.</param>
        /// <param name="value">The value.</param>
        /// <returns><see langword="true"/> if the value is returned or <see langword="false"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>        
        public static bool TryGetValueAsInt16<TKey> ( this IDictionary<TKey, object> source, TKey key, out short value )
        {
            string result;
            if (TryGetValueAsString(source, key, out result))
            {
                if (!String.IsNullOrEmpty(result) && Int16.TryParse(result, out value))
                    return true;
            };

            value = 0;
            return false;
        }

        /// <summary>Tries to get a value as a signed 32-bit integer.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key to retrieve.</param>
        /// <param name="value">The value.</param>
        /// <returns><see langword="true"/> if the value is returned or <see langword="false"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>        
        public static bool TryGetValueAsInt32<TKey> ( this IDictionary<TKey, object> source, TKey key, out int value )
        {
            string result;
            if (TryGetValueAsString(source, key, out result))
            {
                if (!String.IsNullOrEmpty(result) && Int32.TryParse(result, out value))
                    return true;
            };

            value = 0;
            return false;
        }

        /// <summary>Tries to get a value as a signed 64-bit integer.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key to retrieve.</param>
        /// <param name="value">The value.</param>
        /// <returns><see langword="true"/> if the value is returned or <see langword="false"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>        
        public static bool TryGetValueAsInt64<TKey> ( this IDictionary<TKey, object> source, TKey key, out long value )
        {
            string result;
            if (TryGetValueAsString(source, key, out result))
            {
                if (!String.IsNullOrEmpty(result) && Int64.TryParse(result, out value))
                    return true;
            };

            value = 0;
            return false;
        }

        /// <summary>Tries to get a value as a signed byte.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key to retrieve.</param>
        /// <param name="value">The value.</param>
        /// <returns><see langword="true"/> if the value is returned or <see langword="false"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>        
        [CLSCompliant(false)]
        public static bool TryGetValueAsSByte<TKey> ( this IDictionary<TKey, object> source, TKey key, out sbyte value )
        {
            string result;
            if (TryGetValueAsString(source, key, out result))
            {
                if (!String.IsNullOrEmpty(result) && SByte.TryParse(result, out value))
                    return true;
            };

            value = 0;
            return false;
        }

        /// <summary>Tries to get a value as a float.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key to retrieve.</param>
        /// <param name="value">The value.</param>
        /// <returns><see langword="true"/> if the value is returned or <see langword="false"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>        
        public static bool TryGetValueAsSingle<TKey> ( this IDictionary<TKey, object> source, TKey key, out float value )
        {
            string result;
            if (TryGetValueAsString(source, key, out result))
            {
                if (!String.IsNullOrEmpty(result) && Single.TryParse(result, out value))
                    return true;
            };

            value = 0;
            return false;
        }

        /// <summary>Tries to get a value as a string.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key to retrieve.</param>
        /// <param name="value">The value.</param>
        /// <returns><see langword="true"/> if the value is returned or <see langword="false"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>        
        public static bool TryGetValueAsString<TKey> ( this IDictionary<TKey, object> source, TKey key, out string value )
        {
            object result;
            if (source.TryGetValue(key, out result))
            {
                value = TypeConversion.ToStringOrEmpty(result);
                return true;
            };

            value = "";
            return false;
        }

        /// <summary>Tries to get a value as an unsigned 16-bit integer.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key to retrieve.</param>
        /// <param name="value">The value.</param>
        /// <returns><see langword="true"/> if the value is returned or <see langword="false"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>        
        [CLSCompliant(false)]
        public static bool TryGetValueAsUInt16<TKey> ( this IDictionary<TKey, object> source, TKey key, out ushort value )
        {
            string result;
            if (TryGetValueAsString(source, key, out result))
            {
                if (!String.IsNullOrEmpty(result) && UInt16.TryParse(result, out value))
                    return true;
            };

            value = 0;
            return false;
        }

        /// <summary>Tries to get a value as an unsigned 32-bit integer.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key to retrieve.</param>
        /// <param name="value">The value.</param>
        /// <returns><see langword="true"/> if the value is returned or <see langword="false"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>        
        [CLSCompliant(false)]
        public static bool TryGetValueAsUInt32<TKey> ( this IDictionary<TKey, object> source, TKey key, out uint value )
        {
            string result;
            if (TryGetValueAsString(source, key, out result))
            {
                if (!String.IsNullOrEmpty(result) && UInt32.TryParse(result, out value))
                    return true;
            };

            value = 0;
            return false;
        }

        /// <summary>Tries to get a value as an unsigned 64-bit integer.</summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="key">The key to retrieve.</param>
        /// <param name="value">The value.</param>
        /// <returns><see langword="true"/> if the value is returned or <see langword="false"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>        
        [CLSCompliant(false)]
        public static bool TryGetValueAsUInt64<TKey> ( this IDictionary<TKey, object> source, TKey key, out ulong value )
        {
            string result;
            if (TryGetValueAsString(source, key, out result))
            {
                if (!String.IsNullOrEmpty(result) && UInt64.TryParse(result, out value))
                    return true;
            };

            value = 0;
            return false;
        }
        #endregion
    }
}
