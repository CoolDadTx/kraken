/*
 * Copyright © 2011 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Globalization;
using System.Linq;

using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken
{
    /// <summary>Provides support for converting between types.</summary>
    /// <remarks>
    /// This class provides equivalent functionality to <see cref="Convert"/> with some modifications to handle invalid values.  Unless otherwise
    /// specified <see langword="null"/>, empty strings, blank strings and <see cref="DBNull.Value"/> are mappsed to the type's default value.
    /// </remarks>
    public static class TypeConversion
    {
        #region Public Members

        #region Coerce

        /// <summary>Coerces a smaller numeric type to a larger type.</summary>
        /// <typeparam name="T">The larger type.</typeparam>
        /// <param name="value">The value to coerce.</param>
        /// <returns>The coerced value.</returns>
        /// <remarks>
        /// The standard type coercion rules used by most compilers are used to coerce the value.  The actual value is ignored and only
        /// the type rules are used unlike standard type conversion.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is not a primitive type.</exception>
        /// <exception cref="InvalidCastException">The value's type cannot be coerced to the desired type.</exception>
        public static T Coerce<T> ( object value ) where T : struct
        {
            Verify.Argument("value", value).IsNotNull();

            Type desiredType = typeof(T);
            Type valueType = value.GetType();

            if (desiredType == valueType)
                return (T)value;

            if (!IsNumericType(valueType))
                throw new ArgumentException("Value type is not a primitive.", "value");

            //Make sure the types are compatible first - doesn't seem to be any framework functionality to give us this information
            if (desiredType == typeof(decimal))
            {
                return (T)Convert.ChangeType(value, typeof(decimal));
            } else if (desiredType == typeof(double))
            {
                if (valueType != typeof(decimal))
                    return (T)Convert.ChangeType(value, typeof(double));
            } else if (desiredType == typeof(float))
            {
                if (!IsType(valueType, typeof(decimal), typeof(double)))
                    return (T)Convert.ChangeType(value, typeof(float));
            } else if (desiredType == typeof(ulong))
            {
                if (!IsType(valueType, typeof(decimal), typeof(double), typeof(float)))
                    return (T)Convert.ChangeType(value, typeof(ulong));
            } else if (desiredType == typeof(long))
            {
                if (!IsType(valueType, typeof(decimal), typeof(double), typeof(float), typeof(ulong)))
                    return (T)Convert.ChangeType(value, typeof(long));
            } else if (desiredType == typeof(uint))
            {
                if (IsType(valueType, typeof(int), typeof(ushort), typeof(short), typeof(byte), typeof(sbyte)))
                    return (T)Convert.ChangeType(value, typeof(uint));
            } else if (desiredType == typeof(int))
            {
                if (IsType(valueType, typeof(ushort), typeof(short), typeof(byte), typeof(sbyte)))
                    return (T)Convert.ChangeType(value, typeof(int));
            } else if (desiredType == typeof(ushort))
            {
                if (IsType(valueType, typeof(short), typeof(byte), typeof(sbyte)))
                    return (T)Convert.ChangeType(value, typeof(ushort));
            } else if (desiredType == typeof(short))
            {
                if (IsType(valueType, typeof(byte), typeof(sbyte)))
                    return (T)Convert.ChangeType(value, typeof(short));
            } else if (desiredType == typeof(byte))
            {
                if (valueType == typeof(sbyte))
                    return (T)Convert.ChangeType(value, typeof(byte));
            };

            throw new InvalidCastException(String.Format("Cannot coerce from '{0}' to '{1}'", valueType.Name, desiredType.Name));
        }
        #endregion

        #region ToBooleanOrDefault

        /// <summary>Converts a string to a boolean value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as a boolean.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <remarks>
        /// The following values are considered to be <see langword="true"/>.
        /// <list type="bullet">        
        ///     <item>true</item>
        ///     <item>Non-zero numeric values</item>
        ///  </list>
        /// The following values are considered to be <see langword="false"/>.
        /// <list type="bullet">
        ///     <item>0</item>
        ///     <item>false</item>
        ///     <item>(empty)</item>
        ///     <item>(null)</item>
        /// </list>
        /// All other values are passed to <see cref="Boolean.Parse"/> for conversion.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///		TypeConversion.ToBooleanOrDefault("1");		//true
        /// 
        ///		TypeConversion.ToBooleanOrDefault("false");	//false
        ///		TypeConversion.ToBooleanOrDefault(null);	//false
        /// 
        ///		TypeConversion.ToBooleanOrDefault("abc"):	//error
        /// </code>
        /// </example>
        public static bool ToBooleanOrDefault ( string value )
        {
            return ToBooleanOrDefault(value, false);
        }

        /// <summary>Converts a string to a boolean value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as a boolean.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <remarks>
        /// The following values are considered to be <see langword="true"/>.
        /// <list type="bullet">        
        ///     <item>true</item>
        ///     <item>Non-zero numeric values</item>
        ///  </list>
        /// The following values are considered to be <see langword="false"/>.
        /// <list type="bullet">
        ///     <item>0</item>
        ///     <item>false</item>
        ///     <item>(empty)</item>
        ///     <item>(null)</item>
        /// </list>
        /// All other values are passed to <see cref="Boolean.Parse"/> for conversion.
        /// </remarks>        
        public static bool ToBooleanOrDefault ( string value, bool defaultValue )
        {
            if (String.IsNullOrWhiteSpace(value))
                return defaultValue;

            bool result;
            if (Boolean.TryParse(value, out result))
                return result;

            //Convert.ToBooleanOrDefault doesn't do well here nor does Bool.Parse()...            
            long number;
            if (Int64.TryParse(value, out number))
                return number != 0;

            throw new FormatException("Value is not in a correct format.");
        }

        /// <summary>Converts an object to a boolean value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as a boolean.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        /// <remarks>
        /// If the value is a string then calls the string overload of this method.  If it is <see cref="DBNull.Value"/> then it returns <see langword="false"/>.
        /// Otherwise it calls <see cref="O:Convert.ToBooleanOrDefault"/>.
        /// </remarks>
        public static bool ToBooleanOrDefault ( object value )
        {
            return ToBooleanOrDefault(value, false);
        }

        /// <summary>Converts an object to a boolean value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as a boolean.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        /// <remarks>
        /// If the value is a string then calls the string overload of this method.  If it is <see cref="DBNull.Value"/> then it returns <see langword="false"/>.
        /// Otherwise it calls <see cref="O:Convert.ToBooleanOrDefault"/>.
        /// </remarks>
        public static bool ToBooleanOrDefault ( object value, bool defaultValue )
        {
            string str = value as string;
            if (str != null)
                return ToBooleanOrDefault(str, defaultValue);

            if (value == null || value == DBNull.Value)
                return defaultValue;

            return Convert.ToBoolean(value);
        }

        /// <summary>Attempts to convert a value to a boolean value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="result">The converted value.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> if conversion failed.</returns>
        /// <remarks>
        /// The following rules are used to handle the conversion.
        /// <list type="bullet">
        /// <item>If <paramref name="value"/> is <see langword="null"/> or <see cref="DBNull.Value"/> then return <see langword="false"/>.</item>
        /// <item>If <paramref name="value"/> is already a boolean then return the value.</item>
        /// <item>If <paramref name="value"/> is an empty string then return <see langword="false"/>.</item>
        /// <item>If <see cref="Boolean.TryParse"/> is successful then return the parsed value.</item>
        /// <item>If <paramref name="value"/> is "Yes" then return <see langword="true"/>. If <paramref name="value"/> is "No" then return <see langword="false"/>.</item>
        /// <item>If <paramref name="value"/> can be converted to <see cref="Int64"/> or <see cref="UInt64"/> then return <see langword="true"/> if the value is not zero otherwise return <see langword="false"/>.</item>
        /// </list>
        /// </remarks>
        public static bool TryConvertToBoolean ( object value, out bool result )
        {
            result = false;

            //Null or DBNull
            if (value == null || value == DBNull.Value)
                return false;

            //Boolean
            if (value is bool booleanResult)
            {
                result = booleanResult;
                return true;
            };

            //Empty string
            var str = value.ToString();
            if (String.IsNullOrEmpty(str))
                return false;

            //Try the standard parsing
            if (Boolean.TryParse(str, out result))
                return true;

            //Check for Yes/No
            if (String.Compare(str, "yes", StringComparison.OrdinalIgnoreCase) == 0)
            {
                result = true;
                return true;
            } else if (String.Compare(str, "no", StringComparison.OrdinalIgnoreCase) == 0)
                return true;

            //Try numerics
            if (Int64.TryParse(str, out long lresult))
            {
                result = lresult != 0;
                return true;
            }

            if (UInt64.TryParse(str, out ulong ulresult))
            {
                result = ulresult != 0;
                return true;
            };

            return false;
        }
        #endregion

        #region ToByteOrDefault

        /// <summary>Converts a string to a byte value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as a signed byte value.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>            
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        /// <example>
        /// <code lang="C#">
        ///		TypeConversion.ToByteOrDefault("0xF0");	//240
        ///		TypeConversion.ToByteOrDefault("10");		//10
        ///		TypeConversion.ToByteOrDefault(null);		//0
        /// </code>
        /// </example>
        public static byte ToByteOrDefault ( string value )
        {
            return ToByteOrDefault(value, 0);
        }

        /// <summary>Converts a string to a byte value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as a signed byte value.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>            
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        public static byte ToByteOrDefault ( string value, byte defaultValue )
        {
            if (String.IsNullOrWhiteSpace(value))
                return defaultValue;

            NumberStyles style = PrepareInt(ref value);
            return Byte.Parse(value, style);
        }

        /// <summary>Converts an object to a byte value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as a signed byte value.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>            
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        public static byte ToByteOrDefault ( object value )
        {
            return ToByteOrDefault(value, 0);
        }

        /// <summary>Converts an object to a byte value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as a signed byte value.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>            
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        public static byte ToByteOrDefault ( object value, byte defaultValue )
        {
            string str = value as string;
            if (str != null)
                return ToByteOrDefault(str, defaultValue);

            if (value == null || value == DBNull.Value)
                return defaultValue;

            return Convert.ToByte(value);
        }
        #endregion

        #region ToCharOrDefault

        /// <summary>Converts a string to a character value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as a character.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>            
        /// <example>
        /// <code lang="C#">
        ///		TypeConversion.ToCharOrDefault("c");		// c
        ///		TypeConversion.ToCharOrDefault("x");		// x
        ///		TypeConversion.ToCharOrDefault(null);		// Char.MinValue
        /// </code>
        /// </example>
        public static char ToCharOrDefault ( string value )
        {
            return ToCharOrDefault(value, default(char));
        }

        /// <summary>Converts a string to a character value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as a character.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>            
        public static char ToCharOrDefault ( string value, char defaultValue )
        {
            if (String.IsNullOrWhiteSpace(value))
                return defaultValue;

            return Char.Parse(value);
        }

        /// <summary>Converts an object to a character value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as a character.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>            
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        public static char ToCharOrDefault ( object value )
        {
            return ToCharOrDefault(value, default(char));
        }

        /// <summary>Converts an object to a character value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as a character.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>            
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        public static char ToCharOrDefault ( object value, char defaultValue )
        {
            string str = value as string;
            if (str != null)
                return ToCharOrDefault(str, defaultValue);

            if (value == null || value == DBNull.Value)
                return defaultValue;

            return Convert.ToChar(value);
        }
        #endregion

        #region ToDateTimeOrDefault

        /// <summary>Converts a string to a <see cref="DateTime"/> value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as a <see cref="DateTime"/>.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        /// <example>
        /// <code lang="C#">
        ///		TypeConversion.ToDateTimeOrDefault("12/04/2004");				// Dec 4, 2004
        ///		TypeConversion.ToDateTimeOrDefault("03/12/2000 15:31:30");	// Mar 12, 2000 3:31:30 PM
        /// </code>
        /// </example>
        public static DateTime ToDateTimeOrDefault ( string value )
        {
            return ToDateTimeOrDefault(value, DateTime.MinValue);
        }

        /// <summary>Converts a string to a <see cref="DateTime"/> value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as a <see cref="DateTime"/>.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        /// <example>
        /// <code lang="C#">
        ///		TypeConversion.ToDateTimeOrDefault("12/04/2004");				// Dec 4, 2004
        ///		TypeConversion.ToDateTimeOrDefault("03/12/2000 15:31:30");	// Mar 12, 2000 3:31:30 PM
        /// </code>
        /// </example>
        public static DateTime ToDateTimeOrDefault ( string value, DateTime defaultValue )
        {
            if (String.IsNullOrWhiteSpace(value))
                return defaultValue;

            return DateTime.Parse(value);
        }

        /// <summary>Converts a string to a <see cref="DateTime"/> value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="format">The format of the string to parse.</param>
        /// <returns><paramref name="value"/> as a <see cref="DateTime"/>.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <example>
        /// <code lang="C#">
        ///		TypeConversion.ToDateTimeOrDefault("12/04/2004", "g");			// Dec 4, 2004
        ///		TypeConversion.ToDateTimeOrDefault("03/12/2000 15:31:30", "f");	// Mar 12, 2000 3:31:30 PM
        /// </code>
        /// </example>		
        public static DateTime ToDateTimeOrDefault ( string value, string format )
        {
            return ToDateTimeOrDefault(value, format, DateTime.MinValue);
        }

        /// <summary>Converts a string to a <see cref="DateTime"/> value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="format">The format of the string to parse.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as a <see cref="DateTime"/>.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <example>
        /// <code lang="C#">
        ///		TypeConversion.ToDateTimeOrDefault("12/04/2004", "g");			// Dec 4, 2004
        ///		TypeConversion.ToDateTimeOrDefault("03/12/2000 15:31:30", "f");	// Mar 12, 2000 3:31:30 PM
        /// </code>
        /// </example>		
        public static DateTime ToDateTimeOrDefault ( string value, string format, DateTime defaultValue )
        {
            if (String.IsNullOrWhiteSpace(value))
                return defaultValue;

            return DateTime.ParseExact(value, format, DateTimeFormatInfo.CurrentInfo);
        }

        /// <summary>Converts an object to a <see cref="DateTime"/> value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as a <see cref="DateTime"/>.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>   
        public static DateTime ToDateTimeOrDefault ( object value )
        {
            return ToDateTimeOrDefault(value, DateTime.MinValue);
        }

        /// <summary>Converts an object to a <see cref="DateTime"/> value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as a <see cref="DateTime"/>.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>   
        public static DateTime ToDateTimeOrDefault ( object value, DateTime defaultValue )
        {
            string str = value as string;
            if (str != null)
                return ToDateTimeOrDefault(str, defaultValue);

            if (value == null || value == DBNull.Value)
                return defaultValue;

            return Convert.ToDateTime(value);
        }
        #endregion

        #region ToDecimalOrDefault

        /// <summary>Converts a string to a decimal value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as a decimal.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <example>
        /// <code lang="C#">
        ///		TypeConversion.ToDecimalOrDefault("1.2345");	//1.2345M
        ///		TypeConversion.ToDecimalOrDefault("");		//0.0M
        ///		TypeConversion.ToDecimalOrDefault(null);		//0.0M
        /// </code>
        /// </example>
        public static decimal ToDecimalOrDefault ( string value )
        {
            return ToDecimalOrDefault(value, 0);
        }

        /// <summary>Converts a string to a decimal value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as a decimal.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        public static decimal ToDecimalOrDefault ( string value, decimal defaultValue )
        {
            if (String.IsNullOrWhiteSpace(value))
                return defaultValue;

            return Decimal.Parse(value);
        }

        /// <summary>Converts an object to a decimal value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as a decimal.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        public static decimal ToDecimalOrDefault ( object value )
        {
            return ToDecimalOrDefault(value, 0);
        }

        /// <summary>Converts an object to a decimal value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as a decimal.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        public static decimal ToDecimalOrDefault ( object value, decimal defaultValue )
        {
            string str = value as string;
            if (str != null)
                return ToDecimalOrDefault(str, defaultValue);

            if (value == null || value == DBNull.Value)
                return defaultValue;

            return Convert.ToDecimal(value);
        }
        #endregion

        #region ToDoubleOrDefault

        /// <summary>Converts a string to a double value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as an 8-byte real value.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        public static double ToDoubleOrDefault ( string value )
        {
            return ToDoubleOrDefault(value, 0);
        }

        /// <summary>Converts a string to a double value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as an 8-byte real value.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        public static double ToDoubleOrDefault ( string value, double defaultValue )
        {
            if (String.IsNullOrWhiteSpace(value))
                return defaultValue;

            return Double.Parse(value);
        }

        /// <summary>Converts an object to a double value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as an 8-byte real value.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        public static double ToDoubleOrDefault ( object value )
        {
            return ToDoubleOrDefault(value, 0);
        }

        /// <summary>Converts an object to a double value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as an 8-byte real value.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        public static double ToDoubleOrDefault ( object value, double defaultValue )
        {
            string str = value as string;
            if (str != null)
                return ToDoubleOrDefault(str, defaultValue);

            if (value == null || value == DBNull.Value)
                return defaultValue;

            return Convert.ToDouble(value);
        }
        #endregion

        #region ToInt16OrDefault

        /// <summary>Converts a string to a short value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as a signed 16-bit integer.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>            
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        public static short ToInt16OrDefault ( string value )
        {
            return ToInt16OrDefault(value, 0);
        }

        /// <summary>Converts a string to a short value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as a signed 16-bit integer.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>            
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        public static short ToInt16OrDefault ( string value, short defaultValue )
        {
            if (String.IsNullOrWhiteSpace(value))
                return defaultValue;

            NumberStyles style = PrepareInt(ref value);
            return Int16.Parse(value, style);
        }

        /// <summary>Converts an object to a short value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as a signed 16-bit integer.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>            
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        public static short ToInt16OrDefault ( object value )
        {
            return ToInt16OrDefault(value, 0);
        }

        /// <summary>Converts an object to a short value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as a signed 16-bit integer.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>            
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        public static short ToInt16OrDefault ( object value, short defaultValue )
        {
            string str = value as string;
            if (str != null)
                return ToInt16OrDefault(str, defaultValue);

            if (value == null || value == DBNull.Value)
                return defaultValue;

            return Convert.ToInt16(value);
        }
        #endregion

        #region ToInt32OrDefault

        /// <summary>Converts a string to an int value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as a signed 32-bit integer.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        public static int ToInt32OrDefault ( string value )
        {
            return ToInt32OrDefault(value, 0);
        }

        /// <summary>Converts a string to an int value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as a signed 32-bit integer.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        public static int ToInt32OrDefault ( string value, int defaultValue )
        {
            if (String.IsNullOrWhiteSpace(value))
                return defaultValue;

            NumberStyles style = PrepareInt(ref value);
            return Int32.Parse(value, style);
        }

        /// <summary>Converts an object to an int value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as a signed 32-bit integer.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        public static int ToInt32OrDefault ( object value )
        {
            return ToInt32OrDefault(value, 0);
        }

        /// <summary>Converts an object to an int value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as a signed 32-bit integer.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        public static int ToInt32OrDefault ( object value, int defaultValue )
        {
            string str = value as string;
            if (str != null)
                return ToInt32OrDefault(str, defaultValue);

            if (value == null || value == DBNull.Value)
                return defaultValue;

            return Convert.ToInt32(value);
        }
        #endregion

        #region ToInt64OrDefault

        /// <summary>Converts a string to a long value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as a signed 64-bit integer.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        public static long ToInt64OrDefault ( string value )
        {
            return ToInt64OrDefault(value, 0);
        }

        /// <summary>Converts a string to a long value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as a signed 64-bit integer.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        public static long ToInt64OrDefault ( string value, long defaultValue )
        {
            if (String.IsNullOrWhiteSpace(value))
                return defaultValue;

            NumberStyles style = PrepareInt(ref value);
            return Int64.Parse(value, style);
        }

        /// <summary>Converts an object to a long value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as a signed 64-bit integer.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        public static long ToInt64OrDefault ( object value )
        {
            return ToInt64OrDefault(value, 0);
        }

        /// <summary>Converts an object to a long value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as a signed 64-bit integer.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        public static long ToInt64OrDefault ( object value, long defaultValue )
        {
            string str = value as string;
            if (str != null)
                return ToInt64OrDefault(str, defaultValue);

            if (value == null || value == DBNull.Value)
                return defaultValue;

            return Convert.ToInt64(value);
        }
        #endregion

        #region ToSByteOrDefault

        /// <summary>Converts a string to a signed byte value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as a signed byte value.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>            
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        [CLSCompliant(false)]
        public static sbyte ToSByteOrDefault ( string value )
        {
            return ToSByteOrDefault(value, 0);
        }

        /// <summary>Converts a string to a signed byte value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as a signed byte value.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>            
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        [CLSCompliant(false)]
        public static sbyte ToSByteOrDefault ( string value, sbyte defaultValue )
        {
            if (String.IsNullOrWhiteSpace(value))
                return defaultValue;

            NumberStyles style = PrepareInt(ref value);
            return SByte.Parse(value, style);
        }

        /// <summary>Converts an object to a signed byte value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as a signed byte value.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>            
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        [CLSCompliant(false)]
        public static sbyte ToSByteOrDefault ( object value )
        {
            return ToSByteOrDefault(value, 0);
        }

        /// <summary>Converts an object to a signed byte value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as a signed byte value.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>            
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        [CLSCompliant(false)]
        public static sbyte ToSByteOrDefault ( object value, sbyte defaultValue )
        {
            string str = value as string;
            if (str != null)
                return ToSByteOrDefault(str, defaultValue);

            if (value == null || value == DBNull.Value)
                return defaultValue;

            return Convert.ToSByte(value);
        }
        #endregion

        #region ToSingleOrDefault

        /// <summary>Converts a string to a single value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as a 4-byte real value.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        public static float ToSingleOrDefault ( string value )
        {
            return ToSingleOrDefault(value, 0);
        }

        /// <summary>Converts a string to a single value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as a 4-byte real value.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        public static float ToSingleOrDefault ( string value, float defaultValue )
        {
            if (String.IsNullOrWhiteSpace(value))
                return defaultValue;

            return Single.Parse(value);
        }

        /// <summary>Converts an object to a single value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as a 4-byte real value.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        public static float ToSingleOrDefault ( object value )
        {
            return ToSingleOrDefault(value, 0);
        }

        /// <summary>Converts an object to a single value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as a 4-byte real value.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        public static float ToSingleOrDefault ( object value, float defaultValue )
        {
            string str = value as string;
            if (str != null)
                return ToSingleOrDefault(str, defaultValue);

            if (value == null || value == DBNull.Value)
                return defaultValue;

            return Convert.ToSingle(value);
        }
        #endregion

        #region ToStringOrEmpty

        /// <summary>Converts an object to a single value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value as a string.</returns>
        /// <remarks>If <paramref name="value"/> is <see langword="null"/> then an empty string is returned.</remarks>
        public static string ToStringOrEmpty ( object value )
        {
            return ToStringOrEmpty(value, "");
        }

        /// <summary>Converts an object to a single value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>        
        /// <returns>The value as a string.</returns>
        /// <remarks>If <paramref name="value"/> is <see langword="null"/> then an empty string is returned.</remarks>
        public static string ToStringOrEmpty ( object value, string defaultValue )
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;

            return value.ToString();
        }
        #endregion

        #region ToUInt16OrDefault

        /// <summary>Converts a string to a ushort value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as an unsigned 16-bit integer.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        [CLSCompliant(false)]
        public static ushort ToUInt16OrDefault ( string value )
        {
            return ToUInt16OrDefault(value, 0);
        }

        /// <summary>Converts a string to a ushort value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as an unsigned 16-bit integer.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        [CLSCompliant(false)]
        public static ushort ToUInt16OrDefault ( string value, ushort defaultValue )
        {
            if (String.IsNullOrWhiteSpace(value))
                return defaultValue;

            NumberStyles style = PrepareInt(ref value);
            return UInt16.Parse(value, style);
        }

        /// <summary>Converts an object to a ushort value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as an unsigned 16-bit integer.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        [CLSCompliant(false)]
        public static ushort ToUInt16OrDefault ( object value )
        {
            return ToUInt16OrDefault(value, 0);
        }

        /// <summary>Converts an object to a ushort value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as an unsigned 16-bit integer.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        [CLSCompliant(false)]
        public static ushort ToUInt16OrDefault ( object value, ushort defaultValue )
        {
            string str = value as string;
            if (str != null)
                return ToUInt16OrDefault(str, defaultValue);

            if (value == null || value == DBNull.Value)
                return defaultValue;

            return Convert.ToUInt16(value);
        }
        #endregion

        #region ToUInt32OrDefault

        /// <summary>Converts a string to a uint value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as an unsigned 32-bit integer.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        [CLSCompliant(false)]
        public static uint ToUInt32OrDefault ( string value )
        {
            return ToUInt32OrDefault(value, 0);
        }

        /// <summary>Converts a string to a uint value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as an unsigned 32-bit integer.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        [CLSCompliant(false)]
        public static uint ToUInt32OrDefault ( string value, uint defaultValue )
        {
            if (String.IsNullOrWhiteSpace(value))
                return defaultValue;

            NumberStyles style = PrepareInt(ref value);
            return UInt32.Parse(value, style);
        }

        /// <summary>Converts an object to a uint value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as an unsigned 32-bit integer.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        [CLSCompliant(false)]
        public static uint ToUInt32OrDefault ( object value )
        {
            return ToUInt32OrDefault(value, 0);
        }

        /// <summary>Converts an object to a uint value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as an unsigned 32-bit integer.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        [CLSCompliant(false)]
        public static uint ToUInt32OrDefault ( object value, uint defaultValue )
        {
            string str = value as string;
            if (str != null)
                return ToUInt32OrDefault(str, defaultValue);

            if (value == null || value == DBNull.Value)
                return defaultValue;

            return Convert.ToUInt32(value);
        }
        #endregion

        #region ToUInt64OrDefault

        /// <summary>Converts a string to a ulong value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as an unsigned 64-bit integer.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        [CLSCompliant(false)]
        public static ulong ToUInt64OrDefault ( string value )
        {
            return ToUInt64OrDefault(value, 0);
        }

        /// <summary>Converts a string to a ulong value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as an unsigned 64-bit integer.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        [CLSCompliant(false)]
        public static ulong ToUInt64OrDefault ( string value, ulong defaultValue )
        {
            if (String.IsNullOrWhiteSpace(value))
                return defaultValue;

            NumberStyles style = PrepareInt(ref value);
            return UInt64.Parse(value, style);
        }

        /// <summary>Converts an object to a ulong value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><paramref name="value"/> as a unsign 64-bit integer.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        [CLSCompliant(false)]
        public static ulong ToUInt64OrDefault ( object value )
        {
            return ToUInt64OrDefault(value, 0);
        }

        /// <summary>Converts an object to a ulong value.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns><paramref name="value"/> as a unsign 64-bit integer.</returns>
        /// <exception cref="FormatException"><paramref name="value"/> is not in correct format for type.</exception>        
        /// <exception cref="InvalidCastException"><paramref name="value"/> can not be converted to the appropriate type.</exception>
        /// <exception cref="OverflowException">The resulting value is too large for the type.</exception>
        [CLSCompliant(false)]
        public static ulong ToUInt64OrDefault ( object value, ulong defaultValue )
        {
            string str = value as string;
            if (str != null)
                return ToUInt64OrDefault(str, defaultValue);

            if (value == null || value == DBNull.Value)
                return defaultValue;

            return Convert.ToUInt64(value);
        }
        #endregion

        #endregion

        #region Private Members

        private static bool IsNumericType ( Type type )
        {
            return IsType(type, typeof(decimal), typeof(double), typeof(float),
                               typeof(long), typeof(int), typeof(short), typeof(sbyte),
                               typeof(ulong), typeof(uint), typeof(ushort), typeof(byte));
        }

        private static bool IsType ( Type baseType, params Type[] possibleTypes )
        {
            return (from t in possibleTypes
                    where baseType == t
                    select t).Any();
        }

        private static NumberStyles PrepareInt ( ref string value )
        {
            NumberStyles styles = NumberStyles.Integer;
            if (value.StartsWith("0x") || value.StartsWith("0X"))
            {
                styles = NumberStyles.HexNumber;
                value = value.Substring(2);
            };

            return styles;
        }
        #endregion

    }
}