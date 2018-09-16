/*
 * Copyright © 2013 Michael Taylor
 * All Rights Reserved
 * 
 * From code copyright Federation of State Medical Boards
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace P3Net.Kraken.ComponentModel
{
    /// <summary>Provides a generic implementation of <see cref="EnumConverter"/> that can handle custom conversion rules.</summary>
    /// <typeparam name="T">The enum type.</typeparam>
    public class EnumTypeConverter<T> : EnumConverter where T : struct
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="EnumTypeConverter{T}"/> class.</summary>
        public EnumTypeConverter () : base(typeof(T))
        {
        }
        #endregion

        #region Public Members
        
        /// <summary>Determines if the source type can be converted to the enumerated type.</summary>
        /// <param name="context">The context.</param>
        /// <param name="sourceType">The source type.</param>
        /// <returns><see langword="true"/> if the conversion is allowed or <see langword="false"/> otherwise.</returns>        
        public override bool CanConvertFrom ( ITypeDescriptorContext context, Type sourceType )
        {
            if (sourceType == typeof(string) || sourceType == typeof(T))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>Determines if the enumeration can be converted to another type.</summary>
        /// <param name="context">The context.</param>
        /// <param name="destinationType">The destination type.</param>
        /// <returns><see langword="true"/> if the conversion is allowed or <see langword="false"/> otherwise.</returns>
        public override bool CanConvertTo ( ITypeDescriptorContext context, Type destinationType )
        {
            if (destinationType == typeof(T) || destinationType == typeof(string))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>Converts from a valid type to the enum type.</summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="value">The value to convert.</param>
        /// <returns>The eumerated value.</returns>
        public override object ConvertFrom ( ITypeDescriptorContext context, CultureInfo culture, object value )
        {
            var str = value as string;
            if (str != null)
            {
                T data;
                if (FromString(context, culture, str, out data))
                    return data;
            } else if (value is T)
            {
                return (T)value;
            };

            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>Converts the enumerated value to another type.</summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="value">The enumerated value.</param>
        /// <param name="destinationType">The destination type.</param>
        /// <returns>The converted value.</returns>
        public override object ConvertTo ( ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType )
        {
            T typedValue = (T)value;

            if (destinationType == typeof(T))
                return typedValue;
            else if (destinationType == typeof(string))
            {
                string result;
                if (ToString(context, culture, typedValue, out result))
                    return result;
            };

            return base.ConvertTo(context, culture, value, destinationType);
        }
        #endregion

        #region Protected Members

        /// <summary>Converts from a string to the enum type.</summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="str">The string value.</param>
        /// <param name="result">The converted value.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        protected virtual bool FromString ( ITypeDescriptorContext context, CultureInfo culture, string str, out T result )
        {
            return Enum.TryParse<T>(str, out result);
        }

        /// <summary>Converts an enum type to a string.</summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="value">The value to convert.</param>
        /// <param name="result">The value as a string.</param>
        /// <returns><see langword="true"/> if successful.</returns>
        /// <remarks>
        /// The default calls <see cref="O:Enum.ToString"/>.
        /// </remarks>
        protected virtual bool ToString ( ITypeDescriptorContext context, CultureInfo culture, T value, out string result )
        {
            result = value.ToString();
            return true;
        }        
        #endregion
    }
}
