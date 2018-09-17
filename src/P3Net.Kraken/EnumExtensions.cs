/*
 * Copyright © 2009 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using P3Net.Kraken.Diagnostics;
using P3Net.Kraken.Reflection;

namespace P3Net.Kraken
{
    /// <summary>Provides support for strongly typed enumerations.</summary>        
    public static class EnumExtensions
    {
        /// <summary>Formats the value.</summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="value">The value to formatString.</param>
        /// <param name="formatString">The formatString string to use.</param>
        /// <returns>The formatted value.</returns>        
        /// <example>
        /// <code lang="C#">
        /// public string GetEmployeeStatus ( EmployeeStatus status )
        /// {
        ///    return EnumExtensions.Format(status, "g");
        /// }
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException"><paramref name="formatString"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        /// <typeparamref name="T"/> is not an enumeration.
        /// <para>-or-</para>
        /// <paramref name="value"/> is not a valid value.
        /// </exception>
        /// <exception cref="FormatException"><paramref name="formatString"/> contains an invalid value.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]        
        public static string Format<T> ( T value, string formatString ) where T : struct
        {            
            return Enum.Format(typeof(T), value, formatString);
        }

        /// <summary>Gets a pair matching a formatted string name to an enumeration value.</summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <returns>An array of pairs mapping formatted names to their enumerated value.</returns>
        /// <exception cref="ArgumentException"><typeparamref name="T"/> is not an enumerated type.</exception>
        /// <remarks>
        /// Unlike the <see cref="GetNames"/> method, this method generates a formatted name suitable for
        /// display for each enumeration value.  If the enumeration type defines a <see cref="TypeConverter"/>
        /// then the converter is used for conversion.  Otherwise the following rules apply.
        /// <list type="bullet">
        ///    <item>Underscores are converted to spaces. ("Minimum_Altitude" becomes "Minimum Altitude")</item>
        ///    <item>A space is inserted between each lowercase letter followed by an uppercase letter unless a
        /// space already exists. ("MinimumAltitude" becomes "Minimum Altitude")</item>
        ///    <item>When more than one uppercase letter appears together then a space is inserted between the
        /// second to last letter and the last letter. ("ICAOCode" becomes "ICAO Code")</item>
        ///    <item>Numbers are always grouped together and separated by spaces on each side. ("My1Choice" becomes "My 1 Choice")</item>
        /// </list>
        /// </remarks>
        /// <seealso cref="GetNames"/>
        /// <example>
        /// <code lang="C#">
        ///    public void PopulateDropDown ( ComboBox ctrl )
        ///    {
        ///       ctrl.DataSource = EnumExtensions.GetFormattedNames&lt;JobTitle&gt;();
        ///       ctrl.DisplayMember = "Item1";
        ///       ctrl.ValueMember = "Item2";
        ///    }
        /// </code>
        /// </example>
        public static Tuple<T, string>[] GetFormattedNames<T> () where T: struct
        {
            var values = GetValues<T>();
            var pairs = new Tuple<T, string>[values.Length];

            //Determine if a type converter exists (ignore the default converter)
            var conv = TypeDescriptor.GetConverter(typeof(T));
            if (conv is EnumConverter)
                conv = null;

            //Enumerate the values
            for (int nIdx = 0; nIdx < values.Length; ++nIdx)
            {
                T value = values[nIdx];

                //Determine the name
                string name = (conv != null) ? conv.ConvertToString(value) : GetName(value).ToUserFriendly(true);

                //Build the pair
                var pair = new Tuple<T, string>(value, name);
                pairs[nIdx] = pair;                
            };

            return pairs;
        }
        
        /// <summary>Gets the named constant represented by the value.</summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <returns>The named value if found or <see langword="null"/> otherwise.</returns>
        /// <exception cref="ArgumentException"><typeparamref name="T"/> is not an enumeration.</exception>
        /// <remarks>
        /// Flag enumerations will return <see langword="null"/> if multiple values are specified.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// public string GetEmployeeStatus ( EmployeeStatus status )
        /// {
        ///    return EnumExtensions.GetName(status);
        /// }
        /// </code>
        /// </example>
        public static string GetName<T> ( T value ) where T: struct
        {
            return Enum.GetName(typeof(T), value);
        }

        /// <summary>Gets the names of the values in the enumeration.</summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <returns>The array of names for the enumerated values.</returns>
        /// <exception cref="ArgumentException"><typeparamref name="T"/> is not an enumeration.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static string[] GetNames<T> ( ) where T: struct
        {        
            return Enum.GetNames(typeof(T));
        }

        /// <summary>Gets the underlying type of the enumeration.</summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <returns>The base type for the enumeration.</returns>
        /// <exception cref="ArgumentException"><typeparamref name="T"/> is not an enumeration.</exception>        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public static Type GetUnderlyingType<T> ( ) where T: struct
        {
            return Enum.GetUnderlyingType(typeof(T));
        }

        /// <summary>Gets the defined values.</summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <returns>The array of values for the enumeration.</returns>
        /// <exception cref="ArgumentException"><typeparamref name="T"/> is not an enumeration.</exception>
        public static T[] GetValues<T> ( ) where T: struct
        {
            var values = new List<T>();
            foreach (T value in Enum.GetValues(typeof(T)))
                values.Add(value);

            return values.ToArray();
        }

        #region Is...

        /// <summary>Determines if a value is in a list of values.</summary>
        /// <param name="source">The source.</param>
        /// <param name="values">The values to check.</param>
        /// <returns><see langword="true"/> if the value contains all the given values <see langword="false"/> otherwise.</returns>
        /// <remarks>
        /// This method is only useful for enumerations that represent flags.
        /// </remarks>
        public static bool IsAll ( this Enum source, params Enum[] values )
        {
            if (values == null || values.Length == 0)
                return false;

            foreach (var value in values)
            {
                if (!source.HasFlag(value))
                    return false;
            };

            return true;
        }

        /// <summary>Determines if a value is in a list of values.</summary>
        /// <param name="source">The source.</param>
        /// <param name="values">The values to check.</param>
        /// <returns><see langword="true"/> if the value is in the given list or <see langword="false"/> otherwise.</returns>
        public static bool IsAny ( this Enum source, params Enum[] values )
        {
            if (values == null || values.Length == 0)
                return false;

            //Flag enums compare by looking for any value in the set 
            var type = source.GetType();
            if (type.HasAttribute<FlagsAttribute>())
            {
                //Check for the individual flag values
                foreach (var value in values)
                {
                    if (source.HasFlag(value))
                        return true;
                };
            } else
            {
                foreach (var value in values)
                {
                    if (source.Equals(value))
                        return true;
                };
            };

            return false;
        }
        #endregion        

        #region IsDefined

        /// <summary>Determines if the given value is defined for the type.</summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <returns><see langword="true"/> if it is defined.</returns>
        /// <remarks>
        /// Only the named constant is supported.  Numeric values will return <see langword="false"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// public bool IsValidEmployeeStatus ( string status )
        /// {
        ///    return EnumExtensions.IsDefined&lt;EmployeeStatus&gt;(status);
        /// }
        /// </code>
        /// </example>
        /// <exception cref="ArgumentException"><typeparamref name="T"/> is not an enumeration.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static bool IsDefined<T>( string value ) where T : struct
        {
            return Enum.IsDefined(typeof(T), value);
        }

        /// <summary>Determines if the given value is defined for the type.</summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <returns><see langword="true"/> if it is defined.</returns>
        /// <example>
        /// Refer to <see cref="EnumExtensions.IsDefined{T}(String)"/> for an example.
        /// </example>
        /// <exception cref="ArgumentException"><typeparamref name="T"/> is not an enumeration.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static bool IsDefined<T>( int value ) where T : struct
        {
            return Enum.IsDefined(typeof(T), value);
        }

        /// <summary>Determines if the given value is defined for the type.</summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <returns><see langword="true"/> if it is defined.</returns>
        /// <example>        
        /// Refer to <see cref="EnumExtensions.IsDefined{T}(String)"/> for an example.
        /// </example>
        /// <exception cref="ArgumentException"><typeparamref name="T"/> is not an enumeration.</exception>
        public static bool IsDefined<T>( T value ) where T : struct
        {
            return Enum.IsDefined(typeof(T), value);
        }
        #endregion

        #region Parse

        /// <summary>Parses a string into an enumerated value.</summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        /// <remarks>
        /// Unlike <see cref="O:Enum.Parse"/> this method will use a <see cref="TypeConverter"/> if one is defined
        /// for the type and fall back to the base implementation otherwise.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        /// <typeparamref name="T"/> is not an enumeration.
        /// <para>-or-</para>
        /// <paramref name="value"/> is an empty string.
        /// <para>-or-</para>
        /// <paramref name="value"/> is not a valid enumerated value.
        /// </exception>
        public static T Parse<T> ( string value ) where T : struct
        {            
            return Parse<T>(value, false);
        }

        /// <summary>Parses a string into an enumerated value.</summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <param name="ignoreCase"><see langword="true"/> to ignore the case of the string.</param>
        /// <returns>The converted value.</returns>
        /// <remarks>
        /// Unlike <see cref="O:Enum.Parse"/> this method will use a <see cref="TypeConverter"/> if one is defined
        /// for the type and fall back to the base implementation otherwise.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        /// <typeparamref name="T"/> is not an enumeration.
        /// <para>-or-</para>
        /// <paramref name="value"/> is an empty string.
        /// <para>-or-</para>
        /// <paramref name="value"/> is not a valid enumerated value.
        /// </exception>
        public static T Parse<T> ( string value, bool ignoreCase ) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Type is not an enumeration.");

            Verify.Argument("value", value).IsNotNullOrWhiteSpace();

            //Get the converter (Enums always have one)
            var converter = TypeDescriptor.GetConverter(typeof(T));
            var obj = converter.ConvertFromString(value);
            if (obj != null)
                return (T)obj;

            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }
        #endregion

        #region ToEnum

        /// <summary>Converts the value to the enumerated type.</summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        /// <remarks>
        /// The returned enumerated value is not necessarily defined.
        /// </remarks>
        /// <exception cref="ArgumentException"><typeparamref name="T"/> is not an enumeration.</exception>
        /// <example>Refer to <see cref="EnumExtensions.ToEnum{T}(Object)"/> for an example.</example>
        [CLSCompliant(false)]
        public static T ToEnum<T>( sbyte value ) where T : struct
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        /// <summary>Converts the value to the enumerated type.</summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        /// <remarks>
        /// The returned enumerated value is not necessarily defined.
        /// </remarks>
        /// <exception cref="ArgumentException"><typeparamref name="T"/> is not an enumeration.</exception>
        /// <example>Refer to <see cref="EnumExtensions.ToEnum{T}(Object)"/> for an example.</example>
        public static T ToEnum<T>( short value ) where T : struct
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        /// <summary>Converts the value to the enumerated type.</summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        /// <exception cref="ArgumentException"><typeparamref name="T"/> is not an enumeration.</exception>
        /// <remarks>
        /// The returned enumerated value is not necessarily defined.
        /// </remarks>
        /// <example>Refer to <see cref="EnumExtensions.ToEnum{T}(Object)"/> for an example.</example>
        public static T ToEnum<T>( int value ) where T : struct
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        /// <summary>Converts the value to the enumerated type.</summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        /// <exception cref="ArgumentException"><typeparamref name="T"/> is not an enumeration.</exception>
        /// <remarks>
        /// The returned enumerated value is not necessarily defined.
        /// </remarks>
        /// <example>Refer to <see cref="EnumExtensions.ToEnum{T}(Object)"/> for an example.</example>
        public static T ToEnum<T>( long value ) where T : struct
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        /// <summary>Converts the value to the enumerated type.</summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        /// <remarks>
        /// The returned enumerated value is not necessarily defined.
        /// </remarks>
        /// <exception cref="ArgumentException"><typeparamref name="T"/> is not an enumeration.</exception>
        /// <example>Refer to <see cref="EnumExtensions.ToEnum{T}(Object)"/> for an example.</example>
        public static T ToEnum<T>( byte value ) where T : struct
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        /// <summary>Converts the value to the enumerated type.</summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        /// <remarks>
        /// The returned enumerated value is not necessarily defined.
        /// </remarks>
        /// <exception cref="ArgumentException"><typeparamref name="T"/> is not an enumeration.</exception>
        /// <example>Refer to <see cref="EnumExtensions.ToEnum{T}(Object)"/> for an example.</example>
        [CLSCompliant(false)]
        public static T ToEnum<T>( ushort value ) where T : struct
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        /// <summary>Converts the value to the enumerated type.</summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        /// <exception cref="ArgumentException"><typeparamref name="T"/> is not an enumeration.</exception>
        /// <remarks>
        /// The returned enumerated value is not necessarily defined.
        /// </remarks>
        /// <example>Refer to <see cref="EnumExtensions.ToEnum{T}(Object)"/> for an example.</example>
        [CLSCompliant(false)]
        public static T ToEnum<T>( uint value ) where T : struct
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        /// <summary>Converts the value to the enumerated type.</summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        /// <exception cref="ArgumentException"><typeparamref name="T"/> is not an enumeration.</exception>
        /// <remarks>
        /// The returned enumerated value is not necessarily defined.
        /// </remarks>
        /// <example>Refer to <see cref="EnumExtensions.ToEnum{T}(Object)"/> for an example.</example>
        [CLSCompliant(false)]
        public static T ToEnum<T>( ulong value ) where T : struct
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        /// <summary>Converts the value to the enumerated type.</summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        /// <exception cref="ArgumentException"><typeparamref name="T"/> is not an enumeration.</exception>
        /// <remarks>
        /// The returned enumerated value is not necessarily defined.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// public SomeEnum GetValueOrDefault ( object value )
        /// {
        ///    try
        ///    {
        ///       return EnumExtensions.ToEnum&lt;SomeEnum&gt;(value);
        ///    } catch (ArgumentException)
        ///    {
        ///       return SomeEnum.Unknown;
        ///    };
        /// }
        /// </code>
        /// </example>
        /// <exception cref="ArgumentException"><paramref name="value"/> is not a numeric type.</exception>
        public static T ToEnum<T>( object value ) where T : struct
        {
            return (T)Enum.ToObject(typeof(T), value);
        }
        #endregion

        /// <summary>Converts an enum value to a string.</summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <returns>The string value.</returns>
        /// <remarks>
        /// Unlike <see cref="O:Enum.ToString"/> this method will use a <see cref="TypeConverter"/> if one is 
        /// defined for the type and fall back to the base implementation otherwise.
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// <paramref name="value"/> is not a valid enumerated value.
        /// <para>-or-</para>
        /// <typeparamref name="T"/> is not an enumerated type.
        /// </exception>
        public static string ToString<T> ( T value ) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Type is not an enumeration.");

            //Get the converter (Enums always have one)                        
            var converter = TypeDescriptor.GetConverter(typeof(T));
            return converter.ConvertToString(value);
        }

        /// <summary>Tries to parse a string to an enum value.</summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The string value.</param>
        /// <param name="result">The resulting value.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        /// <exception cref="ArgumentException"><typeparamref name="T"/> is not an enumerated type.</exception>
        /// <remarks>
        /// Unlike <see cref="O:Enum.TryParse{T}"/> this method will use a <see cref="TypeConverter"/> if one is defined
        /// for the type and fall back to the base implementation otherwise.
        /// </remarks>
        public static bool TryParse<T> ( string value, out T result ) where T : struct
        {
            return TryParse<T>(value, false, out result);
        }

        /// <summary>Tries to parse a string to an enum value.</summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The string value.</param>
        /// <param name="ignoreCase"><see langword="true"/> to ignore case.</param>
        /// <param name="result">The resulting value.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        /// <remarks>
        /// Unlike <see cref="O:Enum.TryParse{T}"/> this method will use a <see cref="TypeConverter"/> if one is defined
        /// for the type and fall back to the base implementation otherwise.
        /// </remarks>
        /// <exception cref="ArgumentException"><typeparamref name="T"/> is not an enumerated type.</exception>
        public static bool TryParse<T> ( string value, bool ignoreCase, out T result ) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Type is not an enumeration.");

            //Get the converter
            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter != null)
            {
                try
                {
                    var obj = converter.ConvertFromString(value);
                    if (obj != null)
                    {
                        result = (T)obj;
                        return true;
                    };
                } catch (FormatException)
                {
                } catch (ArgumentException)
                {
                } catch (NotSupportedException) //EnumConverter throws this
                { };
            };

            return Enum.TryParse<T>(value, ignoreCase, out result);
        }
    }
}
