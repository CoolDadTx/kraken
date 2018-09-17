/*
 * Copyright © 2012 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;

namespace P3Net.Kraken
{ 
    /// <summary>Represents the formatting options for a byte size.</summary>    
    internal static class ByteSizeFormat
    {
        #region Public Members

        /// <summary>The default format string.
        /// 
        /// </summary>
        public const string DefaultFormatString = ",fff UU";

        /// <summary>Formats an object given the object, format string and provider.</summary>
        /// <param name="format">The format string.</param>
        /// <param name="value">The value to format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>The string representation.</returns>
        public static string Format ( string format, ByteSize value, IFormatProvider formatProvider )
        {
            //Use the default format specifier if one is not provided
            if (String.IsNullOrWhiteSpace(format))
                format = DefaultFormatString;

            //Use the provider or the current UI culture's number info                                                    
            return InternalFormat(value, format, formatProvider);
        }
        #endregion

        #region Private Members

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        private static string InternalFormat ( ByteSize value, string format, IFormatProvider formatProvider )
        {
            var bldr = new StringBuilder();
            Tuple<double, ByteSizeUnit> bestUnit = null;
            bool includeComma = false;
                                        
            //What unit do we need?
            int increment;
            for (int index = 0; index < format.Length; index += increment)
            {
                increment = 1;

                char current = format[index];
                switch (current)
                {
                    //Quoted strings
                    case '\'':
                    case '"': increment = ParseQuotedString(format, index, bldr); break;

                    case ',': includeComma = true; break;

                    //Escape sequence
                    case '\\':
                    {
                        //Subtract one because we have to fetch another character
                        if (index >= (format.Length - 1))
                            throw new FormatException("Escape sequence is missing token.");

                        //Skip the escaped char and output the next one
                        bldr.Append(format[index + 1]);
                        increment = 2;
                        break;
                    };

                    //Values
                    case 'b':
                    case 'B': increment = ProcessValue(format, index, current, value.Bytes, includeComma, bldr, formatProvider); break;

                    case 'k':
                    case 'K': increment = ProcessValue(format, index, current, value.Kilobytes, includeComma, bldr, formatProvider); break;

                    case 'm':
                    case 'M': increment = ProcessValue(format, index, current, value.Megabytes, includeComma, bldr, formatProvider); break;

                    case 'g':
                    case 'G': increment = ProcessValue(format, index, current, value.Gigabytes, includeComma, bldr, formatProvider); break;

                    case 't':
                    case 'T': increment = ProcessValue(format, index, current, value.Terabytes, includeComma, bldr, formatProvider); break;

                    //Units
                    case 'u':
                    case 'U':
                    {
                        if (bestUnit == null)
                            bestUnit = DetermineBestUnit(value);

                        increment = ParseRepeatPattern(format, index, current);
                        GenerateUnit(increment, bestUnit.Item2, Char.IsUpper(current), bldr);
                        break;
                    };

                    //Best fit
                    case 'f':
                    case 'F':
                    {
                        if (bestUnit == null)
                            bestUnit = DetermineBestUnit(value);

                        increment = ProcessValue(format, index, current, bestUnit.Item1, includeComma, bldr, formatProvider);
                        break;
                    };

                    default: bldr.Append(current); break;
                };
            };

            return bldr.ToString();
        }

        private static Tuple<double, ByteSizeUnit> DetermineBestUnit ( ByteSize value )
        {
            if (value.Bytes < ByteSize.BytesInTerabytes)
                if (value.Bytes < ByteSize.BytesInGigabytes)
                    if (value.Bytes < ByteSize.BytesInMegabytes)
                        if (value.Bytes < ByteSize.BytesInKilobytes)
                            return Tuple.Create((double)value.Bytes, ByteSizeUnit.Bytes);
                        else
                            return Tuple.Create(value.Kilobytes, ByteSizeUnit.Kilobytes);
                    else
                        return Tuple.Create(value.Megabytes, ByteSizeUnit.Megabytes);
                else
                    return Tuple.Create(value.Gigabytes, ByteSizeUnit.Gigabytes);

            return Tuple.Create(value.Terabytes, ByteSizeUnit.Terabytes);
        }

        private static void GenerateUnit ( int count, ByteSizeUnit unit, bool isUpper, StringBuilder output )
        {
            var text = s_unitText[unit];

            switch (count)
            {
                case 1: output.Append(isUpper ? text.UpperText1 : text.LowerText1); break;
                case 2: output.Append(isUpper ? text.UpperText2 : text.LowerText2); break;
                case 3: output.Append(isUpper ? text.UpperText3 : text.LowerText3); break;
            };
        }

        private static void GenerateValue ( double value, int count, bool useComma, StringBuilder output, IFormatProvider formatProvider )
        {
            //#,#.  
            //#,#.#
            //#,#.##
            string formatString = "{0:" + (useComma ? "#,#." : "#.") + ((count == 3) ? "##" : (count == 2) ? "#" : "") + "}";

            //Get the value            
            var str = String.Format(formatProvider, formatString, value);

            //Handle the case where 0 values return an empty string
            if (str.Length == 0)
                str = "0";

            output.Append(str);
        }

        private static int ParseQuotedString ( string format, int currentIndex, StringBuilder output )
        {
            int startPos = currentIndex;
            char startChar = format[currentIndex++];
            int len = format.Length;
            bool foundCloseQuote = false;

            //Until we get to the matching char or the end of the format, keep going
            while (currentIndex < len)
            {
                char currentChar = format[currentIndex++];

                if (currentChar == startChar)
                {
                    foundCloseQuote = true;
                    break;
                };

                if (currentChar == '\\')
                {
                    if (currentIndex >= len)
                        throw new FormatException("Escape sequence is missing token.");

                    //Append the escaped character
                    output.Append(format[currentIndex++]);
                } else
                    output.Append(currentChar);
            };

            //If we didn't find the closing quote then it is an error
            if (!foundCloseQuote)
                throw new FormatException("Ending quote missing from format string.");

            //Return the # of characters processed
            return currentIndex - startPos;
        }

        private static int ParseRepeatPattern ( string format, int currentIndex, char pattern )
        {
            int len = format.Length;
            int endPos = currentIndex + 1;

            //We don't care about case in the repeat pattern
            pattern = Char.ToLower(pattern);

            while ((endPos < len) && (Char.ToLower(format[endPos]) == pattern))
                ++endPos;

            return endPos - currentIndex;
        }

        private static int ProcessValue ( string format, int index, char current, double value, bool includeComma, StringBuilder output, IFormatProvider formatProvider )
        {
            int increment = ParseRepeatPattern(format, index, current);
            GenerateValue(value, increment, includeComma, output, formatProvider);

            return increment;
        }

        #region Data

        private enum ByteSizeUnit { Bytes, Kilobytes, Megabytes, Gigabytes, Terabytes };

        [ExcludeFromCodeCoverage]
        private struct UnitText
        {
            public UnitText ( string lowerText1, string upperText1,
                              string lowerText2, string upperText2,
                              string lowerText3, string upperText3 )
            {
                LowerText1 = lowerText1;
                UpperText1 = upperText1;

                LowerText2 = lowerText2;
                UpperText2 = upperText2;

                LowerText3 = lowerText3;
                UpperText3 = upperText3;
            }

            public string LowerText1;
            public string UpperText1;
            public string LowerText2;
            public string UpperText2;
            public string LowerText3;
            public string UpperText3;
        }

        private static readonly Dictionary<ByteSizeUnit, UnitText> s_unitText = new Dictionary<ByteSizeUnit, UnitText>()
        {
            { ByteSizeUnit.Bytes, new UnitText("b", "B", "by", "By", "bytes", "Bytes") },
            { ByteSizeUnit.Kilobytes, new UnitText("k", "K", "kb", "KB", "kilobytes", "Kilobytes") },
            { ByteSizeUnit.Megabytes, new UnitText("m", "M", "mb", "MB", "megabytes", "Megabytes") },
            { ByteSizeUnit.Gigabytes, new UnitText("g", "G", "gb", "GB", "gigabytes", "Gigabytes") },
            { ByteSizeUnit.Terabytes, new UnitText("t", "T", "tb", "TB", "terabytes", "Terabytes") },
        };
        #endregion

        #endregion
    }
}
