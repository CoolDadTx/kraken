/* 
 * Copyright © 2009 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Data;

using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.Data
{
    /// <summary>Provides extension methods for <see cref="DataRow"/>.</summary>
    public static class DataRowExtensions
    {
        /// <summary>Determines if a column exists in the row.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column to check.</param>
        /// <returns><see langword="true"/> if the column exists or <see langword="false"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty.</exception>        
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        public static bool ColumnExists ( this DataRow source, string columnName )
        {
            Verify.Argument(nameof(columnName)).WithValue(columnName).IsNotNullOrEmpty();

            return CheckColumnExists(source, columnName);
        }

        #region GetValue...

        /// <summary>Gets the obj of a column as a boolean.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column obj.</returns>
        /// <remarks>
        /// The following values are treated as <see langword="true"/>.
        /// <list type="bullet">
        ///    <item>true (any case)</item>
        ///    <item>yes (any case)</item>
        ///    <item>1</item>
        /// </list>
        /// The following values are treated as <see langword="false"/>.
        /// <list type="bullet">
        ///    <item>false (any case)</item>
        ///    <item>no (any case)</item>
        ///    <item>0</item>
        ///    <item>null</item>
        ///    <item>empty string</item>
        /// </list>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>        
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetBooleanValue" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static bool GetBooleanValueOrDefault ( this DataRow source, string columnName )
        {
            return GetBooleanValueOrDefault(source, columnName, false);
        }

        /// <summary>Gets the obj of a column as a boolean.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The column obj.</returns>
        /// <remarks>
        /// The following values are treated as <see langword="true"/>.
        /// <list type="bullet">
        ///    <item>true (any case)</item>
        ///    <item>yes (any case)</item>
        ///    <item>1</item>
        /// </list>
        /// The following values are treated as <see langword="false"/>.
        /// <list type="bullet">
        ///    <item>false (any case)</item>
        ///    <item>no (any case)</item>
        ///    <item>0</item>
        ///    <item>null</item>
        ///    <item>empty string</item>
        /// </list>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>        
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetBooleanValue" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static bool GetBooleanValueOrDefault ( this DataRow source, string columnName, bool defaultValue )
        {
            VerifyColumnExists(source, columnName);

            return GetValueCore(source, columnName, BooleanTryParse, defaultValue);
        }

        /// <summary>Gets the obj of a column as an 8-bit unsigned integer.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column obj.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetByteValue" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static byte GetByteValueOrDefault ( this DataRow source, string columnName )
        {
            return GetByteValueOrDefault(source, columnName, 0);
        }

        /// <summary>Gets the obj of a column as an 8-bit unsigned integer.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The column obj.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetByteValue" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static byte GetByteValueOrDefault ( this DataRow source, string columnName, byte defaultValue )
        {
            VerifyColumnExists(source, columnName);

            return GetValueCore(source, columnName, Byte.TryParse, defaultValue);
        }

        /// <summary>Gets the obj of a column as a character.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column obj.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetCharValue" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static char GetCharValueOrDefault ( this DataRow source, string columnName )
        {
            return GetCharValueOrDefault(source, columnName, default(char));
        }

        /// <summary>Gets the obj of a column as a character.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The column obj.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetCharValue" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static char GetCharValueOrDefault ( this DataRow source, string columnName, char defaultValue )
        {
            VerifyColumnExists(source, columnName);

            return GetValueCore(source, columnName, Char.TryParse, defaultValue);
        }

        /// <summary>Gets the value of a column as a Date. Invalid values return the default.</summary>
        /// <param name="source">The source.</param>
        /// <param name="columnName">The colum name.</param>
        /// <returns>The value.</returns>
        public static Date GetDateValueOrDefault ( this DataRow source, string columnName )
        {
            return source.GetDateTimeValueOrDefault(columnName).ToDate();
        }

        /// <summary>Gets the value of a column as a Date. Invalid values return the default.</summary>
        /// <param name="source">The source.</param>
        /// <param name="columnName">The colum name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value.</returns>
        public static Date GetDateValueOrDefault ( this DataRow source, string columnName, Date defaultValue )
        {
            return source.GetDateTimeValueOrDefault(columnName, defaultValue).ToDate();
        }

        /// <summary>Gets the obj of a column as a DateTime.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column obj.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetDateTimeValue" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static DateTime GetDateTimeValueOrDefault ( this DataRow source, string columnName )
        {
            return GetDateTimeValueOrDefault(source, columnName, DateTime.MinValue);
        }

        /// <summary>Gets the obj of a column as a DateTime.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The column obj.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetDecimalValue" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static DateTime GetDateTimeValueOrDefault ( this DataRow source, string columnName, DateTime defaultValue )
        {
            VerifyColumnExists(source, columnName);

            return GetValueCore(source, columnName, DateTime.TryParse, defaultValue);
        }

        /// <summary>Gets the obj of a column as a decimal.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column obj.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetDecimalValue" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static decimal GetDecimalValueOrDefault ( this DataRow source, string columnName )
        {
            return GetDecimalValueOrDefault(source, columnName, 0);
        }

        /// <summary>Gets the obj of a column as a decimal.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The column obj.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetDecimalValue" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static decimal GetDecimalValueOrDefault ( this DataRow source, string columnName, decimal defaultValue )
        {
            VerifyColumnExists(source, columnName);

            return GetValueCore(source, columnName, Decimal.TryParse, defaultValue);
        }

        /// <summary>Gets the obj of a column as a double.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column obj.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetDoubleValue" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static double GetDoubleValueOrDefault ( this DataRow source, string columnName )
        {
            return GetDoubleValueOrDefault(source, columnName, 0);
        }

        /// <summary>Gets the obj of a column as a double.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The column obj.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetDoubleValue" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static double GetDoubleValueOrDefault ( this DataRow source, string columnName, double defaultValue )
        {
            VerifyColumnExists(source, columnName);

            return GetValueCore(source, columnName, Double.TryParse, defaultValue);
        }

        /// <summary>Gets the obj of a column as a 16-bit integer.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column obj.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>        
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetUInt16Value" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static short GetInt16ValueOrDefault ( this DataRow source, string columnName )
        {
            return GetInt16ValueOrDefault(source, columnName, 0);
        }

        /// <summary>Gets the obj of a column as a 16-bit integer.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The column obj.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>        
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetUInt16Value" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static short GetInt16ValueOrDefault ( this DataRow source, string columnName, short defaultValue )
        {
            VerifyColumnExists(source, columnName);

            return GetValueCore(source, columnName, Int16.TryParse, defaultValue);
        }

        /// <summary>Gets the obj of a column as a 32-bit integer.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column obj.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetInt32Value" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static int GetInt32ValueOrDefault ( this DataRow source, string columnName )
        {
            return GetInt32ValueOrDefault(source, columnName, 0);
        }

        /// <summary>Gets the obj of a column as a 32-bit integer.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The column obj.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetInt32Value" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static int GetInt32ValueOrDefault ( this DataRow source, string columnName, int defaultValue )
        {
            VerifyColumnExists(source, columnName);

            return GetValueCore(source, columnName, Int32.TryParse, defaultValue);
        }

        /// <summary>Gets the obj of a column as a 64-bit integer.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column obj.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetInt64Value" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static long GetInt64ValueOrDefault ( this DataRow source, string columnName )
        {
            return GetInt64ValueOrDefault(source, columnName, 0);
        }

        /// <summary>Gets the obj of a column as a 64-bit integer.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The column obj.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetInt64Value" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static long GetInt64ValueOrDefault ( this DataRow source, string columnName, long defaultValue )
        {
            VerifyColumnExists(source, columnName);

            return GetValueCore(source, columnName, Int64.TryParse, defaultValue);
        }

        /// <summary>Gets the obj of a column as an 8-bit signed integer.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column obj.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetSByteValue" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        [CLSCompliant(false)]
        public static sbyte GetSByteValueOrDefault ( this DataRow source, string columnName )
        {
            return GetSByteValueOrDefault(source, columnName, 0);
        }

        /// <summary>Gets the obj of a column as an 8-bit signed integer.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The column obj.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetSByteValue" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        [CLSCompliant(false)]
        public static sbyte GetSByteValueOrDefault ( this DataRow source, string columnName, sbyte defaultValue )
        {
            VerifyColumnExists(source, columnName);

            return GetValueCore(source, columnName, SByte.TryParse, defaultValue);
        }

        /// <summary>Gets the obj of a column as a float.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column obj.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetSingleValue" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static float GetSingleValueOrDefault ( this DataRow source, string columnName )
        {
            return GetSingleValueOrDefault(source, columnName, 0);
        }

        /// <summary>Gets the obj of a column as a float.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The column obj.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetSingleValue" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static float GetSingleValueOrDefault ( this DataRow source, string columnName, float defaultValue )
        {
            VerifyColumnExists(source, columnName);

            return GetValueCore(source, columnName, Single.TryParse, defaultValue);
        }

        /// <summary>Gets the obj of a column as a string.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column obj.  An empty string is returned as the default obj or if the column obj is NULL.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetStringValue" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static string GetStringValueOrDefault ( this DataRow source, string columnName )
        {
            return GetStringValueOrDefault(source, columnName, "");
        }

        /// <summary>Gets the obj of a column as a string.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The column obj.  An empty string is returned as the default obj or if the column obj is NULL.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetStringValue" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static string GetStringValueOrDefault ( this DataRow source, string columnName, string defaultValue )
        {
            VerifyColumnExists(source, columnName);

            if (source.IsNull(columnName))
                return defaultValue;

            string value;
            TryGetStringValue(source, columnName, out value);
            return value;
        }

        /// <summary>Gets the obj of a column as a 16-bit unsigned integer.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column obj.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetUInt16Value" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        [CLSCompliant(false)]
        public static ushort GetUInt16ValueOrDefault ( this DataRow source, string columnName )
        {
            return GetUInt16ValueOrDefault(source, columnName, 0);
        }

        /// <summary>Gets the obj of a column as a 16-bit unsigned integer.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The column obj.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetUInt16Value" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        [CLSCompliant(false)]
        public static ushort GetUInt16ValueOrDefault ( this DataRow source, string columnName, ushort defaultValue )
        {
            VerifyColumnExists(source, columnName);

            return GetValueCore(source, columnName, UInt16.TryParse, defaultValue);
        }

        /// <summary>Gets the obj of a column as a 32-bit unsigned integer.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column obj.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetUInt32Value" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        [CLSCompliant(false)]
        public static uint GetUInt32ValueOrDefault ( this DataRow source, string columnName )
        {
            return GetUInt32ValueOrDefault(source, columnName, 0);
        }

        /// <summary>Gets the obj of a column as a 32-bit unsigned integer.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The column obj.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetUInt32Value" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        [CLSCompliant(false)]
        public static uint GetUInt32ValueOrDefault ( this DataRow source, string columnName, uint defaultValue )
        {
            VerifyColumnExists(source, columnName);

            return GetValueCore(source, columnName, UInt32.TryParse, defaultValue);
        }

        /// <summary>Gets the obj of a column as a 64-bit unsigned integer.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column obj.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetUInt64Value" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        [CLSCompliant(false)]
        public static ulong GetUInt64ValueOrDefault ( this DataRow source, string columnName )
        {
            return GetUInt64ValueOrDefault(source, columnName, 0);
        }

        /// <summary>Gets the obj of a column as a 64-bit unsigned integer.  Invalid values return the default.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The column obj.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columnName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="columnName"/> is empty or does not exist.</exception>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="TryGetUInt64Value" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        [CLSCompliant(false)]
        public static ulong GetUInt64ValueOrDefault ( this DataRow source, string columnName, ulong defaultValue )
        {
            VerifyColumnExists(source, columnName);

            return GetValueCore(source, columnName, UInt64.TryParse, defaultValue);
        }
        #endregion

        #region TryGetValue...

        /// <summary>Tries to retrieve the obj of the column.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="result">The column obj.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        /// <remarks>
        /// The following values are treated as <see langword="true"/>.
        /// <list type="bullet">
        ///    <item>true (any case)</item>
        ///    <item>yes (any case)</item>
        ///    <item>1</item>
        /// </list>
        /// The following values are treated as <see langword="false"/>.
        /// <list type="bullet">
        ///    <item>false (any case)</item>
        ///    <item>no (any case)</item>
        ///    <item>0</item>
        ///    <item>null</item>
        ///    <item>empty string</item>
        /// </list>
        /// </remarks>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="O:GetBooleanValueOrDefault" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static bool TryGetBooleanValue ( this DataRow source, string columnName, out bool result )
        {
            return TryGetValueOrDefault(source, columnName, BooleanTryParse, out result);
        }

        /// <summary>Tries to retrieve the obj of the column.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="result">The column obj.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="O:GetByteValueOrDefault" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static bool TryGetByteValue ( this DataRow source, string columnName, out byte result )
        {
            return TryGetValueOrDefault(source, columnName, Byte.TryParse, out result);
        }

        /// <summary>Tries to retrieve the obj of the column.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="result">The column obj.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="O:GetCharValueOrDefault" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static bool TryGetCharValue ( this DataRow source, string columnName, out char result )
        {
            return TryGetValueOrDefault(source, columnName, Char.TryParse, out result);
        }

        /// <summary>Tries to retrieve the obj of the column.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="result">The column obj.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="O:GetDateValueOrDefault" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static bool TryGetDateValue ( this DataRow source, string columnName, out Date result )
        {
            return TryGetValueOrDefault(source, columnName, Date.TryParse, out result);
        }

        /// <summary>Tries to retrieve the obj of the column.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="result">The column obj.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="O:GetDateTimeValueOrDefault" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static bool TryGetDateTimeValue ( this DataRow source, string columnName, out DateTime result )
        {
            return TryGetValueOrDefault(source, columnName, DateTime.TryParse, out result);
        }


        /// <summary>Tries to retrieve the obj of the column.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="result">The column obj.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="O:GetDecimalValueOrDefault" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static bool TryGetDecimalValue ( this DataRow source, string columnName, out decimal result )
        {
            return TryGetValueOrDefault(source, columnName, Decimal.TryParse, out result);
        }

        /// <summary>Tries to retrieve the obj of the column.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="result">The column obj.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="O:GetDoubleValueOrDefault" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static bool TryGetDoubleValue ( this DataRow source, string columnName, out double result )
        {
            return TryGetValueOrDefault(source, columnName, Double.TryParse, out result);
        }

        /// <summary>Tries to retrieve the obj of the column.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="result">The column obj.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="O:GetUInt16ValueOrDefault" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static bool TryGetInt16Value ( this DataRow source, string columnName, out short result )
        {
            return TryGetValueOrDefault(source, columnName, Int16.TryParse, out result);
        }

        /// <summary>Tries to retrieve the obj of the column.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="result">The column obj.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="O:GetInt32ValueOrDefault" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static bool TryGetInt32Value ( this DataRow source, string columnName, out int result )
        {
            return TryGetValueOrDefault(source, columnName, Int32.TryParse, out result);
        }

        /// <summary>Tries to retrieve the obj of the column.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="result">The column obj.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="O:GetInt64ValueOrDefault" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static bool TryGetInt64Value ( this DataRow source, string columnName, out long result )
        {
            return TryGetValueOrDefault(source, columnName, Int64.TryParse, out result);
        }

        /// <summary>Tries to retrieve the obj of the column.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="result">The column obj.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="O:GetSByteValueOrDefault" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        [CLSCompliant(false)]
        public static bool TryGetSByteValue ( this DataRow source, string columnName, out sbyte result )
        {
            return TryGetValueOrDefault(source, columnName, SByte.TryParse, out result);
        }

        /// <summary>Tries to retrieve the obj of the column.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="result">The column obj.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="O:GetSingleValueOrDefault" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static bool TryGetSingleValue ( this DataRow source, string columnName, out float result )
        {
            return TryGetValueOrDefault(source, columnName, Single.TryParse, out result);
        }

        /// <summary>Tries to retrieve the obj of the column.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="result">The column obj.  An empty string is returned if the column is NULL.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="O:GetStringValueOrDefault" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        public static bool TryGetStringValue ( this DataRow source, string columnName, out string result )
        {
            try
            {
                var value = source[columnName];

                result = (value != DBNull.Value) && (value != null) ? value.ToString() : "";
                return true;
            } catch (ArgumentException)
            { /* Ignore*/
            };

            result = null;
            return false;
        }

        /// <summary>Tries to retrieve the obj of the column.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="result">The column obj.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="O:GetUInt16ValueOrDefault" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        [CLSCompliant(false)]
        public static bool TryGetUInt16Value ( this DataRow source, string columnName, out ushort result )
        {
            return TryGetValueOrDefault(source, columnName, UInt16.TryParse, out result);
        }

        /// <summary>Tries to retrieve the obj of the column.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="result">The column obj.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="O:GetUInt32ValueOrDefault" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        [CLSCompliant(false)]
        public static bool TryGetUInt32Value ( this DataRow source, string columnName, out uint result )
        {
            return TryGetValueOrDefault(source, columnName, UInt32.TryParse, out result);
        }

        /// <summary>Tries to retrieve the obj of the column.</summary>
        /// <param name="source">The source</param>
        /// <param name="columnName">The column name.</param>
        /// <param name="result">The column obj.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        /// <example>Refer to <see cref="DataRowExtensions"/> for an example.</example>
        /// <seealso cref="O:GetUInt64ValueOrDefault" />
        /// <seealso cref="O:DataRowExtensions.Field{T}" />
        [CLSCompliant(false)]
        public static bool TryGetUInt64Value ( this DataRow source, string columnName, out ulong result )
        {
            return TryGetValueOrDefault(source, columnName, UInt64.TryParse, out result);
        }
        #endregion
             
        #region Private Members

        private static bool BooleanTryParse ( string value, out bool result )
        {
            return TypeConversion.TryConvertToBoolean(value, out result);
        }

        private static T GetValueCore<T> ( DataRow source, string columnName, TryParseDelegate<T> parser, T defaultValue ) where T : struct
        {
            T value;
            TryGetValueOrDefault<T>(source, columnName, parser, out value, defaultValue);
            return value;
        }

        private delegate bool TryParseDelegate<T> ( string value, out T result );

        //Returns true if the column obj exists and is either null/empty or valid, returns false if the column does not exist or the obj cannot be converted
        //Result is the obj if it is converted or null otherwise
        private static bool TryGetValue<T> ( DataRow source, string columnName, TryParseDelegate<T> parser, out Nullable<T> result ) where T : struct
        {
            string value;
            if (TryGetStringValue(source, columnName, out value))
            {
                if (String.IsNullOrEmpty(value))
                {
                    //Return false because the obj could not be found
                    result = null;
                    return true;
                };

                T actualResult;
                if (parser(value, out actualResult))
                {
                    result = actualResult;
                    return true;
                };
            };

            result = null;
            return false;
        }

        private static bool TryGetValueOrDefault<T> ( DataRow source, string columnName, TryParseDelegate<T> parser, out T result, T defaultValue = default(T) ) where T : struct
        {
            Nullable<T> actualResult;
            if (TryGetValue(source, columnName, parser, out actualResult))
            {
                result = actualResult.HasValue ? actualResult.Value : defaultValue;
                return true;
            };

            result = defaultValue;
            return false;
        }

        private static bool CheckColumnExists ( [ValidatedNotNull] DataRow source, string columnName )
        {
            //If command is null then we want to throw so don't check for that...
            return source.Table.Columns.Contains(columnName);
        }

        private static void VerifyColumnExists ( [ValidatedNotNull] DataRow source, string columnName )
        {
            if (!CheckColumnExists(source, columnName))
                throw new ArgumentException("Column does not exist.", "columnName");
        }
        #endregion
    }
}