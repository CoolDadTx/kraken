/*
 * Copyright © 2004 Michael Taylor
 * All rights reserved.
 */
#region Imports

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

using P3Net.Kraken.Collections;
using P3Net.Kraken.Diagnostics;
using P3Net.Kraken.Text;
#endregion

namespace P3Net.Kraken
{
    /// <summary>Provides extension methods for strings.</summary>
    public static class StringExtensions
    {
        #region Public Members

        /// <summary>Gets the string value unless it is <see langword="null"/> or empty in which case it returns <see langword="null"/>.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The string value or <see langword="null"/> if it is <see langword="null"/> or empty.</returns>
        public static string AsNullIfEmpty ( this string source )
        {
            return !String.IsNullOrEmpty(source) ? source : null;
        }

        #region Coalesce

        /// <summary>Returns the first value that is not <see langword="null"/>.</summary>
        /// <param name="values">The values to examine.</param>
        /// <returns>The first value that is not <see langword="null"/>.  If all values are <see langword="null"/> then 
        /// <see langword="null"/> is returned.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/>.</exception>
        public static string Coalesce ( params string[] values )
        {
            return Coalesce(StringCoalesceOptions.None, values as IEnumerable<string>);
        }

        /// <summary>Returns the first value that is not <see langword="null"/>.</summary>
        /// <param name="options">The options to use.</param>
        /// <param name="values">The values to examine.</param>
        /// <returns>The first value that is not <see langword="null"/>.  If all values are <see langword="null"/> then 
        /// <see langword="null"/> is returned.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/>.</exception>
        public static string Coalesce ( StringCoalesceOptions options, params string[] values )
        {
            return Coalesce(options, values as IEnumerable<string>);
        }

        /// <summary>Returns the first value that is not <see langword="null"/>.</summary>
        /// <param name="values">The values to examine.</param>
        /// <returns>The first value that is not <see langword="null"/>.  If all values are <see langword="null"/> then 
        /// <see langword="null"/> is returned.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/>.</exception>
        public static string Coalesce ( IEnumerable<string> values )
        {
            return Coalesce(StringCoalesceOptions.None, values);
        }

        /// <summary>Returns the first value that is not <see langword="null"/>.</summary>
        /// <param name="options">The options to use.</param>
        /// <param name="values">The values to examine.</param>
        /// <returns>The first value that is not <see langword="null"/>.  If all values are <see langword="null"/> then 
        /// <see langword="null"/> is returned.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/>.</exception>
        public static string Coalesce ( StringCoalesceOptions options, IEnumerable<string> values )
        {
            Verify.Argument("values", values).IsNotNull();

            IEnumerable<string> query;
            if (EnumExtensions.IsAny(options, StringCoalesceOptions.SkipEmpty))
                query = values.Where(x => !String.IsNullOrEmpty(x));
            else
                query = values.Where(x => x != null);

            return query.FirstOrDefault();
        }
        #endregion

        #region Combine
        
        /// <summary>Combines a set of strings using the given separator.</summary>
        /// <param name="separator">The separator to use.</param>        
        /// <param name="values">The list of strings to combine.</param>
        /// <returns>The combined string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="separator"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// The separator is only inserted between values when the separator does not appear at the end of the left obj or the beginning of the right obj.  
        /// If one of the strings is <see langword="null"/> or empty then it is ignored.  This differs from how <see cref="O:String.Join"/> behaves.
        /// </remarks>
        /// <seealso cref="O:String.Join"/>
        /// <example>
        /// <code lang="C#">		
        ///		Console.WriteLine(StringExtensions.Combine(@"\", new[] { @"C:\Program Files", "App" } ));	
        ///		Console.WriteLine(StringExtensions.Combine(@"\", new[] { @"C:\Windows\", "System32" } ));	
        ///		Console.WriteLine(StringExtensions.Combine(@"\", new[] { @"C:\Windows\", @"\Inf" } ));		
        ///		Console.WriteLine(StringExtensions.Combine(@"\", new[] { @"C:\Temp", "" } ));				
        ///		
        ///           //Prints
        ///           //   C:\Program Files\App
        ///           //   C:\Windows\System32
        ///          //    C:\Windows\Inf
        ///          //    C:\Temp
        ///  </code>
        ///  </example>
        public static string Combine ( string separator, IEnumerable<string> values )
        {
            Verify.Argument("separator", separator).IsNotNull();
            if (values == null)
                return "";
                          
            return InternalCombine(separator, values);  
        }

        /// <summary>Combines a set of strings using the given separator.</summary>
        /// <param name="separator">The separator to use.</param>        
        /// <param name="values">The list of strings to combine.</param>
        /// <returns>The combined string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="separator"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// The separator is only inserted between values when the separator does not appear at the end of the left obj or the beginning of the right obj.  
        /// If one of the strings is <see langword="null"/> or empty then it is ignored.  This differs from how <see cref="O:String.Join"/> behaves.
        /// </remarks>
        /// <seealso cref="O:String.Join"/>
        /// <example>
        /// <code lang="C#">		
        ///		Console.WriteLine(StringExtensions.Combine(@"\", @"C:\Program Files", "App" ));	
        ///		Console.WriteLine(StringExtensions.Combine(@"\", @"C:\Windows\", "System32" ));	
        ///		Console.WriteLine(StringExtensions.Combine(@"\", @"C:\Windows\", @"\Inf" ));		
        ///		Console.WriteLine(StringExtensions.Combine(@"\", @"C:\Temp", "" ));				
        ///		
        ///           //Prints
        ///           //   C:\Program Files\App
        ///           //   C:\Windows\System32
        ///          //    C:\Windows\Inf
        ///          //    C:\Temp
        ///  </code>
        ///  </example>
        public static string Combine ( string separator, params string[] values )
        {
            Verify.Argument("separator", separator).IsNotNull();
            if (values.IsNullOrEmpty())
                return "";

            return InternalCombine(separator, values);             
        }
        #endregion

        #region EnsureEndsWith

        /// <summary>Ensures a string ends with a specific delimiter.</summary>
        /// <param name="source">The source.</param>
        /// <param name="delimiter">The delimiter to look for.</param>
        /// <returns>The string with the delimiter added.  If the source is <see langword="null"/> or empty then the delimiter is returned.</returns>               
        /// <remarks>
        /// The comparison is done using the current culture's case sensitive comparison.
        /// </remarks>
        public static string EnsureEndsWith ( this string source, char delimiter )
        {
            return EnsureEndsWith(source, delimiter.ToString());
        }

        /// <summary>Ensures a string ends with a specific delimiter.</summary>
        /// <param name="source">The source.</param>
        /// <param name="delimiter">The delimiter to look for.</param>
        /// <param name="comparison">The comparison to perform.</param>
        /// <returns>The string with the delimiter added.  If the source is <see langword="null"/> or empty then the delimiter is returned.</returns>                 
        public static string EnsureEndsWith ( this string source, char delimiter, StringComparison comparison )
        {
            return EnsureEndsWith(source, delimiter.ToString(), comparison);
        }

        /// <summary>Ensures a string ends with a specific delimiter.</summary>
        /// <param name="source">The source.</param>
        /// <param name="delimiter">The delimiter to look for.</param>
        /// <param name="ignoreCase"><see langword="true"/> to ignore case.</param>
        /// <param name="culture">The culture to use.  If <see langword="null"/> then the current culture is used.</param>
        /// <returns>The string with the delimiter added.  If the source is <see langword="null"/> or empty then the delimiter is returned.</returns>                 
        public static string EnsureEndsWith ( this string source, char delimiter, bool ignoreCase, CultureInfo culture )
        {
            return EnsureEndsWith(source, delimiter.ToString(), ignoreCase, culture);
        }

        /// <summary>Ensures a string ends with a specific delimiter.</summary>
        /// <param name="source">The source.</param>
        /// <param name="delimiter">The delimiter to look for.</param>
        /// <returns>The string with the delimiter added.  If the source is <see langword="null"/> or empty then the delimiter is returned.</returns>               
        /// <exception cref="ArgumentNullException"><paramref name="delimiter"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// The comparison is done using the current culture's case sensitive comparison.
        /// </remarks>
        public static string EnsureEndsWith ( this string source, string delimiter )
        {
            return EnsureEndsWith(source, delimiter, false, null);
        }

        /// <summary>Ensures a string ends with a specific delimiter.</summary>
        /// <param name="source">The source.</param>
        /// <param name="delimiter">The delimiter to look for.</param>
        /// <param name="comparison">The comparison to perform.</param>
        /// <returns>The string with the delimiter added.  If the source is <see langword="null"/> or empty then the delimiter is returned.</returns>               
        /// <exception cref="ArgumentNullException"><paramref name="delimiter"/> is <see langword="null"/>.</exception>
        public static string EnsureEndsWith ( this string source, string delimiter, StringComparison comparison )
        {
            if (String.IsNullOrEmpty(source))
                return delimiter;

            return source.EndsWith(delimiter, comparison) ? source : source + delimiter;
        }

        /// <summary>Ensures a string ends with a specific delimiter.</summary>
        /// <param name="source">The source.</param>
        /// <param name="delimiter">The delimiter to look for.</param>
        /// <param name="ignoreCase"><see langword="true"/> to ignore case.</param>
        /// <param name="culture">The culture to use.  If <see langword="null"/> then the current culture is used.</param>
        /// <returns>The string with the delimiter added.  If the source is <see langword="null"/> or empty then the delimiter is returned.</returns>               
        /// <exception cref="ArgumentNullException"><paramref name="delimiter"/> is <see langword="null"/>.</exception>
        public static string EnsureEndsWith ( this string source, string delimiter, bool ignoreCase, CultureInfo culture )
        {
            if (String.IsNullOrEmpty(source))
                return delimiter;

            return source.EndsWith(delimiter, ignoreCase, culture) ? source : source + delimiter;
        }
        #endregion

        #region EnsureStartsWith

        /// <summary>Ensures a string starts with a specific delimiter.</summary>
        /// <param name="source">The source.</param>
        /// <param name="delimiter">The delimiter to look for.</param>
        /// <returns>The string with the delimiter added.  If the source is <see langword="null"/> or empty then the delimiter is returned.</returns>               
        /// <exception cref="ArgumentNullException"><paramref name="delimiter"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// The comparison is done using the current culture's case sensitive comparison.
        /// </remarks>
        public static string EnsureStartsWith ( this string source, char delimiter )
        {
            return EnsureStartsWith(source, delimiter.ToString());
        }

        /// <summary>Ensures a string starts with a specific delimiter.</summary>
        /// <param name="source">The source.</param>
        /// <param name="delimiter">The delimiter to look for.</param>
        /// <param name="comparison">The comparison to do.</param>
        /// <returns>The string with the delimiter added.  If the source is <see langword="null"/> or empty then the delimiter is returned.</returns>               
        /// <exception cref="ArgumentNullException"><paramref name="delimiter"/> is <see langword="null"/>.</exception>
        public static string EnsureStartsWith ( this string source, char delimiter, StringComparison comparison )
        {
            return EnsureStartsWith(source, delimiter.ToString(), comparison);
        }

        /// <summary>Ensures a string starts with a specific delimiter.</summary>
        /// <param name="source">The source.</param>
        /// <param name="delimiter">The delimiter to look for.</param>
        /// <param name="ignoreCase"><see langword="true"/> to ignore case.</param>
        /// <param name="culture">The culture to use.  If <see langword="null"/> then the current culture is used.</param>
        /// <returns>The string with the delimiter added.  If the source is <see langword="null"/> or empty then the delimiter is returned.</returns>               
        /// <exception cref="ArgumentNullException"><paramref name="delimiter"/> is <see langword="null"/>.</exception>
        public static string EnsureStartsWith ( this string source, char delimiter, bool ignoreCase, CultureInfo culture )
        {
            return EnsureStartsWith(source, delimiter.ToString(), ignoreCase, culture);
        }

        /// <summary>Ensures a string starts with a specific delimiter.</summary>
        /// <param name="source">The source.</param>
        /// <param name="delimiter">The delimiter to look for.</param>
        /// <returns>The string with the delimiter added.  If the source is <see langword="null"/> or empty then the delimiter is returned.</returns>               
        /// <exception cref="ArgumentNullException"><paramref name="delimiter"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// The comparison is done using the current culture's case sensitive comparison.
        /// </remarks>
        public static string EnsureStartsWith ( this string source, string delimiter )
        {
            return EnsureStartsWith(source, delimiter, false, null);
        }

        /// <summary>Ensures a string starts with a specific delimiter.</summary>
        /// <param name="source">The source.</param>
        /// <param name="delimiter">The delimiter to look for.</param>
        /// <param name="comparison">The comparison to do.</param>
        /// <returns>The string with the delimiter added.  If the source is <see langword="null"/> or empty then the delimiter is returned.</returns>               
        /// <exception cref="ArgumentNullException"><paramref name="delimiter"/> is <see langword="null"/>.</exception>
        public static string EnsureStartsWith ( this string source, string delimiter, StringComparison comparison )
        {
            if (String.IsNullOrEmpty(source))
                return delimiter;

            return source.StartsWith(delimiter, comparison) ? source : delimiter + source;
        }

        /// <summary>Ensures a string starts with a specific delimiter.</summary>
        /// <param name="source">The source.</param>
        /// <param name="delimiter">The delimiter to look for.</param>
        /// <param name="ignoreCase"><see langword="true"/> to ignore case.</param>
        /// <param name="culture">The culture to use.  If <see langword="null"/> then the current culture is used.</param>
        /// <returns>The string with the delimiter added.  If the source is <see langword="null"/> or empty then the delimiter is returned.</returns>               
        /// <exception cref="ArgumentNullException"><paramref name="delimiter"/> is <see langword="null"/>.</exception>
        public static string EnsureStartsWith ( this string source, string delimiter, bool ignoreCase, CultureInfo culture )
        {
            if (String.IsNullOrEmpty(source))
                return delimiter;

            return source.StartsWith(delimiter, ignoreCase, culture) ? source : delimiter + source;
        }
        #endregion

        #region FormatWith

        /// <summary>Provides a shorthand for using <see cref="O:String.Format"/> on a string.</summary>
        /// <param name="source">The source.</param>
        /// <param name="arg">The argument.</param>
        /// <returns>The formatted string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method is identical to <see cref="O:String.Format"/> but is an instance method.
        /// </remarks>
        [Obsolete("Deprecated in 5.0. Use string interpolation.")]
        public static string FormatWith ( this string source, object arg )
        {
            return String.Format(source, arg);
        }

        /// <summary>Provides a shorthand for using <see cref="O:String.Format"/> on a string.</summary>
        /// <param name="source">The source.</param>
        /// <param name="arg1">The argument.</param>
        /// <param name="arg2">The argument.</param>
        /// <returns>The formatted string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method is identical to <see cref="O:String.Format"/> but is an instance method.
        /// </remarks>
        [Obsolete("Deprecated in 5.0. Use string interpolation.")]
        public static string FormatWith ( this string source, object arg1, object arg2 )
        {
            return String.Format(source, arg1, arg2);
        }

        /// <summary>Provides a shorthand for using <see cref="O:String.Format"/> on a string.</summary>
        /// <param name="source">The source.</param>
        /// <param name="arg1">The argument.</param>
        /// <param name="arg2">The argument.</param>
        /// <param name="arg3">The argument.</param>
        /// <returns>The formatted string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method is identical to <see cref="O:String.Format"/> but is an instance method.
        /// </remarks>
        [Obsolete("Deprecated in 5.0. Use string interpolation.")]
        public static string FormatWith ( this string source, object arg1, object arg2, object arg3 )
        {
            return String.Format(source, arg1, arg2, arg3);
        }

        /// <summary>Provides a shorthand for using <see cref="O:String.Format"/> on a string.</summary>
        /// <param name="source">The source.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The formatted string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method is identical to <see cref="O:String.Format"/> but is an instance method.
        /// </remarks>
        [Obsolete("Deprecated in 5.0. Use string interpolation.")]
        public static string FormatWith ( this string source, params object[] args )
        {
            return String.Format(source, args);
        }
        #endregion

        #region GetComparer

        /// <summary>Gets the equivalent comparer for <see cref="StringComparison"/>.</summary>
        /// <param name="comparison">The comparison.</param>
        /// <returns>The equivalent comparer.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="comparison"/> is not a valid enumeration other.</exception>
        public static StringComparer GetComparer ( StringComparison comparison )
        {
            switch (comparison)
            {
                case StringComparison.CurrentCulture: return StringComparer.CurrentCulture;
                case StringComparison.CurrentCultureIgnoreCase: return StringComparer.CurrentCultureIgnoreCase;

                case StringComparison.InvariantCulture: return StringComparer.InvariantCulture;
                case StringComparison.InvariantCultureIgnoreCase: return StringComparer.InvariantCultureIgnoreCase;

                case StringComparison.Ordinal: return StringComparer.Ordinal;
                case StringComparison.OrdinalIgnoreCase: return StringComparer.OrdinalIgnoreCase;
            };

            throw new ArgumentOutOfRangeException("comparison");
        }
        #endregion

        #region IndexOfNotIn

        /// <summary>Finds the index of the first character not in the given token list.</summary>
        /// <param name="source">The string to search.</param>
        /// <param name="tokens">The tokens to ignore.</param>
        /// <returns>The index of the first character that is not in <paramref name="tokens"/> or -1 if none are found.</returns>
        /// <remarks>
        /// If <paramref name="tokens"/> is empty or <paramref name="source"/> is empty then it returns -1.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///		Console.WriteLine("abc123def".IndexOfNotIn('a', 'b', 'c', 'd'));	// 3
        ///		Console.WriteLine("123456".IndexOfNotIn('1', '2', '3', '4', '5', '6'));	// -1
        ///		Console.WriteLine("abcd".IndexOfNotIn('1', '2', '3', '4'));		// 0
        /// </code>
        /// </example>
        public static int IndexOfNotIn ( this string source, params char[] tokens )
        {
            return IndexOfNotIn(source, tokens, 0);
        }

        /// <summary>Finds the index of the first character not in the given token list.</summary>
        /// <param name="source">The string to search.</param>
        /// <param name="tokens">The tokens to ignore.</param>
        /// <param name="startingIndex">The starting index to search.</param>
        /// <returns>The index of the first character that is not in <paramref name="tokens"/> or -1 if none are found.  The index will be at least
        /// as large as <paramref name="startingIndex"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="tokens"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startingIndex"/> is less than zero.</exception>
        /// <remarks>
        /// If <paramref name="tokens"/> is empty or <paramref name="source"/> is empty then it returns -1.
        /// </remarks>
        /// <example>Refer to <see cref="M:IndexOfNotIn(String,Char[])">IndexOfNotIn</see> for an example.</example>
        public static int IndexOfNotIn ( this string source, IEnumerable<char> tokens, int startingIndex )
        {
            Verify.Argument("tokens", tokens).IsNotNull();
            Verify.Argument("startingIndex", startingIndex).IsGreaterThanOrEqualToZero();

            if ((source.Length == 0) || (tokens.Count() == 0) || (startingIndex >= source.Length))
                return -1;

            for (int index = startingIndex; index < source.Length; ++index)
            {
                if (!tokens.Contains(source[index]))
                    return index;
            };

            return -1;
        }
        #endregion
        
        #region Is...

        /// <summary>Determines if a string consists of only alphabetic characters.</summary>
        /// <param name="source">The string to examine.</param>
        /// <returns><see langword="true"/> if the string consists of only alphabetic characters or <see langword="false"/> otherwise.</returns>
        /// <example>
        /// <code lang="C#">        
        ///            while (true)
        ///            {
        ///                string inputValue = Console.ReadLine();
        ///                Console.WriteLine("{0} :: Alpha={1}    AlphaNumeric={2}    Identifier={3}    Numeric={4}", inputValue
        ///                                                inputValue.IsAlpha(), inputValue.IsAlphaNumeric(), inputValue.IsIdentifier(), inputValue.IsNumeric());
        ///            };
        ///            
        ///            //Output
        ///            abcdefg :: Alpha=true    AlphaNumeric=true    Identifier=true    Numeric=false
        ///            abc def  :: Alpha=false    AlphaNumeric=false    Identifier=false    Numeric=false
        ///            abc1def :: Alpha=false    AlphaNumeric=true    Identifier=true    Numeric=false
        ///            1abcdef :: Alpha=false    AlphaNumeric=true    Identifier=false    Numeric=false
        ///            123456   :: Alpha=false    AlphaNumeric=true    Identifier=false    Numeric=true
        ///            abc_def :: Alpha=false    AlphaNumeric=false    Identifier=true    Numeric=false
        /// </code>
        /// </example>
        /// <seealso cref="IsAlphaNumeric"/>
        /// <seealso cref="IsIdentifier"/>
        /// <seealso cref="IsNumeric"/>
        public static bool IsAlpha ( this string source )
        {
            if (source.Length == 0)
                return false;

            foreach (char ch in source)
            {
                if (!Char.IsLetter(ch))
                    return false;
            };

            return true;
        }

        /// <summary>Determines if a string consists of only alphabetic and numeric characters.</summary>
        /// <param name="source">The string to examine.</param>
        /// <returns><see langword="true"/> if the string consists of only alphabetic and numeric characters or <see langword="false"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        /// <example>Refer to <see cref="IsAlpha"/> for an example.</example>
        /// <seealso cref="IsAlpha"/>
        /// <seealso cref="IsNumeric"/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "AlphaNumeric")]
        public static bool IsAlphaNumeric ( this string source )
        {
            if (source.Length == 0)
                return false;

            foreach (Char ch in source)
            {
                if (!Char.IsLetterOrDigit(ch))
                    return false;
            };

            return true;
        }

        /// <summary>Determines if a string is empty.</summary>
        /// <param name="source">Thje source.</param>
        /// <returns><see langword="true"/> if the string is empty, or <see langword="null"/>.</returns>
        public static bool IsEmpty ( this string source )
        {
            return String.IsNullOrEmpty(source);
        }

        /// <summary>Determines if a string is a valid identifier.</summary>
        /// <param name="source">The string to examine.</param>
        /// <returns><see langword="true"/> if the string is a valid identifier or <see langword="false"/> otherwise.</returns>
        /// <remarks>
        /// A valid identifier uses the C-style definition.  The string must consist of letters, digits and underscores.  The
        /// first character can not be a digit.  Multiple underscores are permitted.
        /// </remarks>
        /// <example>Refer to <see cref="IsAlpha"/> for an example.</example>
        /// <seealso cref="IsAlpha"/>		
        public static bool IsIdentifier ( this string source )
        {
            //FIrst character must be a letter or underscore by standard identifier rules
            if ((source.Length == 0) || (!Char.IsLetter(source, 0) && source[0] != '_'))
                return false;

            foreach (Char ch in source)
            {
                if (!Char.IsLetterOrDigit(ch) && (ch != '_'))
                    return false;
            };

            return true;
        }

        /// <summary>Determines if a string consists of only numeric characters.</summary>
        /// <param name="source">The string to examine.</param>
        /// <returns><see langword="true"/> if the string consists of only numeric characters or <see langword="false"/> otherwise.</returns>
        /// <remarks>
        /// Parseable strings are not necessarily numeric.
        /// </remarks>
        /// <example>Refer to <see cref="IsAlpha"/> for an example.</example>
        /// <seealso cref="IsAlpha"/>
        /// <seealso cref="IsAlphaNumeric"/>
        public static bool IsNumeric ( this string source )
        {
            if (source.Length == 0)
                return false;

            foreach (var ch in source)
            {
                if (!Char.IsNumber(ch))
                    return false;
            };

            return true;
        }

        /// <summary>Determines if a string is all whitespace.</summary>
        /// <param name="source">Thje source.</param>
        /// <returns><see langword="true"/> if the string is whitespace, or <see langword="null"/>.</returns>
        public static bool IsWhitespace ( this string source )
        {
            return String.IsNullOrWhiteSpace(source);
        }
        #endregion

        #region Left

        /// <summary>Gets the leftmost <paramref name="count"/> characters from a string.</summary>
        /// <param name="source">The string to retrieve the substring from.</param>
        /// <param name="count">The number of characters to retrieve.</param>
        /// <returns>The substring.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is negative.</exception>
        /// <remarks>
        /// If <paramref name="count"/> is greater than the length of the string then the entire string is returned.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///		string strRes;
        /// 
        ///     strRes = "abcdefghij".Left(3);	//strRes = "abc"
        ///		strRes = "abcdefghij".Left(20);	//strRes = "abcdefghij"
        /// </code>
        /// </example>
        /// <seealso cref="O:LeftOf"/>
        /// <seealso cref="Right"/>
        /// <seealso cref="Mid"/>
        public static string Left ( this string source, int count )
        {
            Verify.Argument("count", count).IsGreaterThanOrEqualToZero();

            return (count < source.Length) ? source.Substring(0, count) : source;
        }
        #endregion

        #region LeftOf

        /// <summary>Gets the portion of the string to the left of any of the given tokens.</summary>
        /// <param name="source">The string to search.</param>
        /// <param name="tokens">The tokens to find.</param>
        /// <returns>All characters to the left of any of the tokens.</returns>
        /// <example>
        /// <code lang="C#">
        /// public static string GetEmailUser ( string emailAddress )
        /// {
        ///    return emailAddress.LeftOf('@');
        /// }
        /// </code>
        /// </example>
        /// <seealso cref="Left"/>
        /// <seealso cref="O:RightOf"/>
        public static string LeftOf ( this string source, params char[] tokens )
        {
            return LeftOf(source, (IList<char>)tokens);
        }

        /// <summary>Gets the portion of the string to the left of any of the given tokens.</summary>
        /// <param name="source">The string to search.</param>
        /// <param name="tokens">The tokens to find.</param>
        /// <returns>All characters to the left of any of the given characters.</returns>
        /// <remarks>
        /// If <paramref name="tokens"/> is <see langword="null"/> or empty then the entire string is returned.
        /// </remarks>
        /// <example>Refer to <see cref="LeftOf(String,Char[])">LeftOf</see> for an example.</example>
        /// <seealso cref="Left"/>
        /// <seealso cref="O:RightOf"/>
        public static string LeftOf ( this string source, IList<char> tokens )
        {            
            if ((tokens == null) || tokens.Count == 0)
                return source;

            //Find the token
            int index = source.IndexOfAny(tokens.ToArray());
            
            return (index >= 0) ? source.Substring(0, index) : source;
        }

        /// <summary>Gets the portion of the string to the left of the given token.</summary>
        /// <param name="source">The string to search.</param>
        /// <param name="token">The token to find.</param>
        /// <returns>All characters to the left of the given string, if found.</returns>
        /// <remarks>        
        /// If <paramref name="token"/> is <see langword="null"/> or empty then the entire string is returned.  The current culture is used.
        /// </remarks>
        /// <example>Refer to <see cref="LeftOf(String,Char[])">LeftOf</see> for an example.</example>
        /// <seealso cref="Left"/>
        /// <seealso cref="O:RightOf"/>
        public static string LeftOf ( this string source, string token )
        {
            return LeftOf(source, token, StringComparison.CurrentCulture);
        }

        /// <summary>Gets the portion of the string to the left of the given token.</summary>
        /// <param name="source">The string to search.</param>
        /// <param name="token">The token to find.</param>
        /// <param name="comparison">The type of comparison to do.</param>
        /// <returns>All characters to the left of the given string, if found.</returns>
        /// <remarks>
        /// If <paramref name="token"/> is <see langword="null"/> or empty then the entire string is returned.
        /// </remarks>
        /// <example>Refer to <see cref="LeftOf(String,Char[])">LeftOf</see> for an example.</example>
        /// <seealso cref="Left"/>
        /// <seealso cref="O:RightOf"/>
        public static string LeftOf ( this string source, string token, StringComparison comparison )
        {        
            //Empty source or token is easy
            if ((source.Length == 0) || String.IsNullOrEmpty(token))
                return source;
            
            //Find the token
            int index = source.IndexOf(token, comparison);
           
            return (index >= 0) ? source.Substring(0, index) : source;
        }
        #endregion

        #region Mid

        /// <summary>Gets the characters in the range <paramref name="startIndex"/> to <paramref name="endIndex"/> inclusive.</summary>
        /// <param name="source">The string to retrieve the substring from.</param>
        /// <param name="startIndex">The starting index of the string.</param>
        /// <param name="endIndex">The ending index of the string.  The character at the given index is returned.</param>
        /// <returns>The substring.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="startIndex"/> is negative.
        /// <para>-or-</para>
        /// <paramref name="endIndex"/> is less than <paramref name="startIndex"/>.
        /// </exception>
        /// <remarks>
        /// If <paramref name="startIndex"/> is larger than the length of the string then an empty string is returned.  If
        /// <paramref name="endIndex"/> is greater than the length of the string then the remainder of the string is returned.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///		Console.WriteLine("abcdefghij".Mid(3, 5));	// "def"
        ///		Console.WriteLine("abcdefghij".Mid(4, 20));	// "efghij"
        ///		Console.WriteLine("abcdefghij".Mid(5, 5));	// "f"
        /// </code>
        /// </example>
        /// <seealso cref="Left"/>
        /// <seealso cref="Right"/>
        public static string Mid ( this string source, int startIndex, int endIndex )
        {
            Verify.Argument("startIndex", startIndex).IsGreaterThanOrEqualToZero();
            Verify.Argument("endIndex", endIndex).IsGreaterThanOrEqualTo(startIndex);

            if (startIndex >= source.Length)
                return "";

            if (endIndex >= source.Length)
                endIndex = source.Length - 1;

            return source.Substring(startIndex, endIndex - startIndex + 1);
        }
        #endregion

        #region RemoveAll

        /// <summary>Removes all specified characters from a string.</summary>
        /// <param name="source">The string to modify.</param>
        /// <param name="value">The value to remove.</param>
        /// <returns>The updated string.</returns>
        public static string RemoveAll ( this string source, char value )
        {
            return RemoveAll(source, value.ToString(), 0, StringComparison.CurrentCulture);
        }

        /// <summary>Removes all specified characters from a string.</summary>
        /// <param name="source">The string to modify.</param>
        /// <param name="value">The value to remove.</param>
        /// <param name="startIndex">The index to start removing from.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is less than zero or larger than the length of the string.</exception>
        /// <returns>The updated string.</returns>
        public static string RemoveAll ( this string source, char value, int startIndex )
        {
            return RemoveAll(source, value.ToString(), startIndex, StringComparison.CurrentCulture);
        }

        /// <summary>Removes all specified characters from a string.</summary>
        /// <param name="source">The string to modify.</param>
        /// <param name="value">The value to remove.</param>
        /// <param name="comparison">The comparer to use.</param>
        /// <returns>The updated string.</returns>
        public static string RemoveAll ( this string source, char value, StringComparison comparison )
        {
            return RemoveAll(source, value.ToString(), 0, comparison);
        }

        /// <summary>Removes all specified characters from a string.</summary>
        /// <param name="source">The string to modify.</param>
        /// <param name="value">The value to remove.</param>
        /// <param name="startIndex">The index to start removing from.</param>
        /// <param name="comparison">The comparer to use.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is less than zero or larger than the length of the string.</exception>
        /// <returns>The updated string.</returns>
        public static string RemoveAll ( this string source, char value, int startIndex, StringComparison comparison )
        {
            return RemoveAll(source, value.ToString(), startIndex, comparison);
        }

        /// <summary>Removes all specified characters from a string.</summary>
        /// <param name="source">The string to modify.</param>
        /// <param name="values">The values to remove.</param>
        /// <param name="startIndex">The index to start removing from.</param>
        /// <param name="comparison">The comparer to use.</param>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is less than zero or larger than the length of the string.</exception>
        /// <returns>The updated string.</returns>
        public static string RemoveAll ( this string source, IEnumerable<char> values, int startIndex, StringComparison comparison )
        {
            Verify.Argument("values", values).IsNotNull();

            var bldr = new StringBuilder(source.Length);
            if (startIndex != 0)
                bldr.Append(source.Left(startIndex));

            var comparer = CharComparer.GetComparer(comparison);
            foreach (var ch in source.Skip(startIndex))
            {
                if (!values.Any(x => comparer.Compare(ch, x) == 0))
                    bldr.Append(ch);
            };

            return bldr.ToString();
        }

        /// <summary>Removes the specified value from a string.</summary>
        /// <param name="source">The string to modify.</param>
        /// <param name="value">The value to remove.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is empty.</exception>
        /// <returns>The updated string.</returns>
        public static string RemoveAll ( this string source, string value )
        {
            return RemoveAll(source, value, 0, StringComparison.CurrentCulture);
        }

        /// <summary>Removes the specified value from a string.</summary>
        /// <param name="source">The string to modify.</param>
        /// <param name="value">The value to remove.</param>
        /// <param name="startIndex">The index to start removing from.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is less than zero or larger than the length of the string.</exception>
        /// <returns>The updated string.</returns>
        public static string RemoveAll ( this string source, string value, int startIndex )
        {
            return RemoveAll(source, value, startIndex, StringComparison.CurrentCulture);
        }

        /// <summary>Removes the specified value from a string.</summary>
        /// <param name="source">The string to modify.</param>
        /// <param name="value">The value to remove.</param>
        /// <param name="comparison">The comparer to use.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is empty.</exception>
        /// <returns>The updated string.</returns>
        public static string RemoveAll ( this string source, string value, StringComparison comparison )
        {
            return RemoveAll(source, value, 0, comparison);
        }

        /// <summary>Removes the specified value from a string.</summary>
        /// <param name="source">The string to modify.</param>
        /// <param name="value">The value to remove.</param>
        /// <param name="startIndex">The index to start removing from.</param>
        /// <param name="comparison">The comparer to use.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is less than zero or larger than the length of the string.</exception>
        /// <returns>The updated string.</returns>
        public static string RemoveAll ( this string source, string value, int startIndex, StringComparison comparison )
        {
            Verify.Argument("value", value).IsNotNullOrEmpty();

            return RemoveAll(source, new[] { value }, startIndex, comparison);
        }

        /// <summary>Removes all the specified values from a string.</summary>
        /// <param name="source">The string to modify.</param>
        /// <param name="values">The values to remove.</param>
        /// <param name="startIndex">The index to start removing from.</param>
        /// <param name="comparison">The comparer to use.</param>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is less than zero or larger than the length of the string.</exception>
        /// <returns>The updated string.</returns>
        public static string RemoveAll ( this string source, IEnumerable<string> values, int startIndex, StringComparison comparison )
        {
            Verify.Argument("values", values).IsNotNull();

            values = values.Where(x => !String.IsNullOrEmpty(x));

            var str = source;
            foreach (var value in values)
            {
                int index = str.IndexOf(value, startIndex, comparison);
                while (index >= 0)
                {
                    str = str.Remove(index, value.Length);

                    index = str.IndexOf(value, index, comparison);
                };
            };

            return str;
        }
        #endregion

        #region ReplaceAll

        /// <summary>Replaces a group of values with another obj.</summary>
        /// <param name="source">The string to modify.</param>
        /// <param name="valuesToReplace">The list of values to replace.</param>
        /// <param name="newValue">The new obj to use.</param>
        /// <returns>The updated string.</returns>
        /// <remarks>
        /// Each element in <paramref name="valuesToReplace"/> is replaced by <paramref name="newValue"/> in the returned string.  The values are replaced in the
        /// order in which they appear.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="valuesToReplace"/> is <see langword="null"/>.</exception>        
        /// <example>Refer to <see cref="ReplaceAll(String, IEnumerable{String},String)">ReplaceAll</see> for an example.</example>
        public static string ReplaceAll ( this string source, char[] valuesToReplace, char newValue )
        {
            Verify.Argument("valuesToReplace", valuesToReplace).IsNotNull();

            if (source.Length == 0 || valuesToReplace.Length == 0)
                return source;

            var bldr = new StringBuilder();
            foreach (var ch in source)
            {
                if (valuesToReplace.Contains(ch))
                    bldr.Append(newValue);
                else
                    bldr.Append(ch);
            };

            return bldr.ToString();
        }

        /// <summary>Replaces a group of values with another obj.</summary>
        /// <param name="source">The string to modify.</param>
        /// <param name="valuesToReplace">The list of values to replace.</param>
        /// <param name="replacementValues">The list of replacement values.</param>
        /// <returns>The updated string.</returns>
        /// <remarks>
        /// <paramref name="replacementValues"/> must be at least as large as <paramref name="valuesToReplace"/>.  Each element in <paramref name="valuesToReplace"/>
        /// is replaced in the string with the corresponding obj in <paramref name="replacementValues"/>.  The values are replaced in the order they appear in <paramref name="valuesToReplace"/>.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="valuesToReplace"/> or <paramref name="replacementValues"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="replacementValues"/> is not at least as long as <paramref name="valuesToReplace"/>.</exception>
        /// <example>
        /// <code lang="C#">
        ///		string strRes;
        ///		
        ///		char[] old1 = new char[] { '/', '\\' };
        ///		char[] new1 = new char[] { '-', '-' );
        ///		strRes = @"123/45\6789".ReplaceAll(old1, new1);	//strRes = "123-45-6789"
        /// </code>
        /// </example>
        public static string ReplaceAll ( this string source, char[] valuesToReplace, char[] replacementValues )
        {
            Verify.Argument("valuesToReplace", valuesToReplace).IsNotNull();
            Verify.Argument("replacementValues", replacementValues).IsNotNull();

            if (replacementValues.Length < valuesToReplace.Length)
                throw new ArgumentException("Replacement values is smaller than values to replace.", "replacementValues");

            if ((source.Length == 0) || (valuesToReplace.Length == 0))
                return source;

            var bldr = new StringBuilder();
            foreach (var ch in source)
            {
                int index = valuesToReplace.IndexOf(ch);
                if (index >= 0)
                    bldr.Append(replacementValues[index]);
                else
                    bldr.Append(ch);
            };

            return bldr.ToString();
        }

        /// <summary>Replaces a group of values with another obj.</summary>
        /// <param name="source">The string to modify.</param>
        /// <param name="valuesToReplace">The list of values to replace.</param>
        /// <param name="newValue">The new obj to use.</param>
        /// <returns>The updated string.</returns>
        /// <remarks>
        /// Each element in <paramref name="valuesToReplace"/> is replaced by <paramref name="newValue"/> in the returned string.  The values are replaced in the
        /// order in which they appear.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="valuesToReplace"/>.</exception>        
        /// <example>
        /// <code lang="C#">
        ///		Console.WriteLine(@"123/45\6789".ReplaceAll(new[] { "/", "@"\", "-"));	   // "123-45-6789"
        /// 
        ///		var oldValues = new string[] { "\r", "\n", "\r\n" };
        ///		Console.WriteLine(@"This\ris\r\na\ntest.".ReplaceAll(oldValues, "\r\n"));	// "This\r\nis\r\na\r\ntest."
        /// </code>
        /// </example>
        public static string ReplaceAll ( this string source, IEnumerable<string> valuesToReplace, string newValue )
        {
            Verify.Argument("valuesToReplace", valuesToReplace).IsNotNull();

            if (source.Length == 0 || valuesToReplace.Count() == 0)
                return source;

            newValue = newValue ?? "";

            StringBuilder bldr = new StringBuilder(source);
            foreach (var value in valuesToReplace)
            {
                if (!String.IsNullOrEmpty(value))
                    bldr.Replace(value, newValue);
            };

            return bldr.ToString();
        }

        /// <summary>Replaces a group of values with another obj.</summary>
        /// <param name="source">The string to modify.</param>
        /// <param name="valuesToReplace">The list of values to replace.</param>
        /// <param name="replacementValues">The list of replacement values.</param>
        /// <returns>The updated string.</returns>
        /// <remarks>
        /// <paramref name="replacementValues"/> must be at least as large as <paramref name="valuesToReplace"/>.  Each element in <paramref name="valuesToReplace"/>
        /// is replaced in the string with the corresponding obj in <paramref name="replacementValues"/>.  The values are replaced in the order they appear in <paramref name="valuesToReplace"/>.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="valuesToReplace"/> or <paramref name="replacementValues"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="replacementValues"/> is not at least as long as <paramref name="valuesToReplace"/>.</exception>
        /// <example>
        /// <code lang="C#">
        ///		string strRes;
        ///		
        ///		string[] old1 = new string[] { "/", @"\" };
        ///		string[] new1 = new string[] { "-", "-" );
        ///		strRes = @"123/45\6789".ReplaceAll(old1, new1);	//strRes = "123-45-6789"
        /// 
        ///		string[] old2 = new string[] { "\r\n", "\n", "\r" };
        ///		string[] new2 = new string[] { "\r", "\r", "\r\n" );
        ///		strRes = @"This\ris\r\na\ntest.".ReplaceAll(old2, new2);	//strRes = "This\r\nis\r\na\r\ntest."
        /// </code>
        /// </example>
        public static string ReplaceAll ( this string source, IEnumerable<string> valuesToReplace, IEnumerable<string> replacementValues )
        {
            Verify.Argument("valuesToReplace", valuesToReplace).IsNotNull();
            Verify.Argument("replacementValues", replacementValues).IsNotNull();

            if (replacementValues.Count() < valuesToReplace.Count())
                throw new ArgumentException("Replacement values is smaller than values to replace.", "replacementValues");

            if (source.Length == 0)
                return "";

            var bldr = new StringBuilder(source);

            int index = 0;
            foreach (var oldValue in valuesToReplace)
            {
                if (!String.IsNullOrEmpty(oldValue))
                    bldr.Replace(oldValue, replacementValues.ElementAt(index) ?? "");
                ++index;
            };

            return bldr.ToString();
        }
        #endregion

        #region Right

        /// <summary>Gets the rightmost <paramref name="count"/> characters from a string.</summary>
        /// <param name="source">The string to retrieve the substring from.</param>
        /// <param name="count">The number of characters to retrieve.</param>
        /// <returns>The substring.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is negative.</exception>
        /// <remarks>
        /// If <paramref name="count"/> is greater than the length of the string then the entire string is returned.  If <paramref name="count"/> is 
        /// zero then none of the string is returned.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        ///		string strRes;
        /// 
        ///         strRes = "abcdefghij".Right(3);	//strRes = "hij"
        ///		strRes = "abcdefghij".Right(20);	//strRes = "abcdefghij"
        /// </code>
        /// </example>
        /// <seealso cref="Left"/>
        /// <seealso cref="Mid"/>
        /// <seealso cref="O:RightOf"/>
        public static string Right ( this string source, int count )
        {
            Verify.Argument("count", count).IsGreaterThanOrEqualToZero();

            return (count < source.Length) ? source.Substring(source.Length - count) : source;
        }
        #endregion

        #region RightOf

        /// <summary>Gets the portion of the string to the right of any of the given tokens.</summary>
        /// <param name="source">The string to search.</param>
        /// <param name="tokens">The tokens to find.</param>
        /// <returns>All characters to the right of any of the given tokens or an empty string if the token is not found.</returns>
        /// <example>
        /// <code lang="C#">
        /// public static string GetEmailDomain ( string emailAddress )
        /// {
        ///    return emailAddress.RightOf('@');
        /// }
        /// </code>
        /// </example>
        /// <seealso cref="O:LeftOf"/>
        /// <seealso cref="Right"/>
        public static string RightOf ( this string source, params char[] tokens )
        {
            return RightOf(source, (IList<char>)tokens);
        }

        /// <summary>Gets the portion of the string to the right of any of the given tokens.</summary>
        /// <param name="source">The string to search.</param>
        /// <param name="tokens">The tokens to find.</param>
        /// <returns>All characters to the right of the first found token or an empty string if the tokens are not found.</returns>
        /// <remarks>
        /// If <paramref name="tokens"/> is <see langword="null"/> or empty then an empty string is returned.  
        /// </remarks>
        /// <example>Refer to <see cref="RightOf(String,Char[])">RightOf</see> for an example.</example>
        /// <seealso cref="O:LeftOf"/>
        /// <seealso cref="Right"/>
        public static string RightOf ( this string source, IList<char> tokens )
        {
            if ((source.Length == 0) || (tokens == null) || (tokens.Count == 0))
                return source;

            //Find it
            int index = source.IndexOfAny(tokens.ToArray());

            return (index >= 0) && (index < source.Length - 1) ? source.Substring(index + 1) : "";
        }
        
        /// <summary>Gets the portion of the string to the right of the given token.</summary>
        /// <param name="source">The string to search.</param>
        /// <param name="token">The token to find.</param>
        /// <returns>All characters to the right of the given token or an empty string if the token is not found.</returns>
        /// <remarks>
        /// If <paramref name="token"/> is <see langword="null"/> or empty then an empty string is returned.  The current culture is used.
        /// </remarks>
        /// <example>Refer to <see cref="RightOf(String,Char[])">RightOf</see> for an example.</example>
        /// <seealso cref="O:LeftOf"/>
        public static string RightOf ( this string source, string token )
        {
            return RightOf(source, token, StringComparison.CurrentCulture);
        }

        /// <summary>Gets the portion of the string to the right of the given token.</summary>
        /// <param name="source">The string to search.</param>
        /// <param name="token">The token to find.</param>
        /// <param name="comparisonType">The type of comparison to do.</param>
        /// <returns>All characters to the right of the given token or an empty string if the token is not found.</returns>
        /// <remarks>
        /// If <paramref name="token"/> is <see langword="null"/> or empty then an empty string is returned.
        /// </remarks>
        /// <example>Refer to <see cref="RightOf(String,Char[])">RightOf</see> for an example.</example>
        /// <seealso cref="O:LeftOf"/>
        public static string RightOf ( this string source, string token, StringComparison comparisonType )
        {
            if ((source.Length == 0) || String.IsNullOrEmpty(token))
                return source;

            //Find it
            int index = source.IndexOf(token, comparisonType);

            int start = index + token.Length;
            return (index >= 0) && (start < source.Length - 1) ? source.Substring(start) : "";
        }
        #endregion

        #region Strip

        #region Strip...

        /// <summary>Strips all non-digits characters from the string. </summary>
        /// <param name="source">The source value.</param>
        /// <remarks>The new value.</remarks>
        public static string StripNonDigits ( this string source )
        {
            return new string(source.Where(c => Char.IsDigit(c)).ToArray());
        }

        /// <summary>Strips all non-alphanumeric characters from the string. </summary>
        /// <param name="source">The source value.</param>
        /// <remarks>The new value.</remarks>
        public static string StripNonLetterOrDigits ( this string source )
        {
            return new string(source.Where(c => Char.IsLetterOrDigit(c)).ToArray());
        }
        #endregion

        #endregion

        #region ToCamel

        /// <summary>Returns a copy of the string camel-cased.</summary>
        /// <param name="source">The obj to convert.</param>
        /// <returns>The camel cased string.</returns>        
        /// <remarks>
        /// A camel-cased string lowercases the first letter of the first word.  Subsequent words are uppercased.  This method does not detect word boundaries 
        /// so only the first character is lower cased.
        /// </remarks>
        /// <example>Refer to <see cref="ToCamel(String,CultureInfo)">ToCamel</see> for an example.
        /// </example>
        /// <seealso cref="O:ToPascal"/>
        /// <seealso cref="O:ToUserFriendly"/>
        /// <seealso cref="O:String.ToUpper"/>
        /// <seealso cref="O:String.ToLower"/>		
        public static string ToCamel ( this string source )
        {
            return ToCamel(source, null);
        }

        /// <summary>Returns a copy of the string camel-cased.</summary>
        /// <param name="source">The obj to convert.</param>
        /// <param name="culture">The culture to use for the conversion.</param>
        /// <returns>The camel cased string.</returns>        
        /// <remarks>
        /// A camel-cased string lowercases the first letter of the first word.  Subsequent words are uppercased.  This method does not detect word boundaries 
        /// so only the first character is lower cased.
        /// </remarks>
        /// <example>
        /// <code language="C#">
        ///    static void Main ( )
        ///    {
        ///       Console.WriteLine("MyVariable".ToCamel(), CultureInfo.CurrentCulture);   //Prints myVariable
        ///       Console.WriteLine("yourVariable".ToCamel(), CultureInfo.CurrentCulture); //Prints yourVariable
        ///       Console.WriteLine("_Variable".ToCamel(), CultureInfo.CurrentCulture);    //Prints _Variable
        ///    }
        /// </code>
        /// </example>
        /// <seealso cref="O:ToPascal"/>
        /// <seealso cref="O:ToUserFriendly"/>
        /// <seealso cref="O:String.ToUpper"/>
        /// <seealso cref="O:String.ToLower"/>		
        public static string ToCamel ( this string source, CultureInfo culture )
        {
            culture = culture ?? CultureInfo.CurrentCulture;

            if (source.Length == 0)
                return "";

            if (Char.IsLower(source, 0))
                return source;

            return Char.ToLower(source[0], culture) + source.Substring(1);
        }
        #endregion
                
        #region ToPascal

        /// <summary>Returns a copy of the string Pascal-cased.</summary>
        /// <param name="source">The obj to convert.</param>
        /// <returns>The Pascal cased string.</returns>
        /// <remarks>
        /// A Pascal cased string uppercases the first letter of each word.  This method does not detect word boundaries so only the first character
        /// is upper cased.
        /// </remarks>
        /// <example>
        /// <code language="C#">
        ///    static void Main ( )
        ///    {
        ///       Console.WriteLine("MyVariable".ToPascal());   //Prints MyVariable
        ///       Console.WriteLine("yourVariable".ToPascal()); //Prints YourVariable
        ///       Console.WriteLine("_Variable".ToPascal());    //Prints _Variable
        ///    }
        /// </code>
        /// </example>
        /// <seealso cref="O:ToCamel"/>
        /// <seealso cref="O:ToUserFriendly"/>
        /// <seealso cref="O:String.ToUpper"/>
        /// <seealso cref="O:String.ToLower"/>		
        public static string ToPascal ( this string source )
        {
            return ToPascal(source, null);
        }

        /// <summary>Returns a copy of the string Pascal-cased.</summary>
        /// <param name="source">The obj to convert.</param>
        /// <param name="culture">The culture to use for the conversion.</param>
        /// <returns>The Pascal cased string.</returns>
        /// <remarks>
        /// A Pascal cased string uppercases the first letter of each word.  This method does not detect word boundaries so only the first character
        /// is upper cased.
        /// </remarks>
        /// <example>Refer to <see cref="ToPascal(String)">ToPascal</see> for an example.</example>
        /// <seealso cref="O:ToCamel"/>
        /// <seealso cref="O:ToUserFriendly"/>
        /// <seealso cref="O:String.ToUpper"/>
        /// <seealso cref="O:String.ToLower"/>		
        public static string ToPascal ( this string source, CultureInfo culture )
        {
            culture = culture ?? CultureInfo.CurrentCulture;

            if (source.Length == 0)
                return "";

            if (Char.IsUpper(source, 0))
                return source;

            return Char.ToUpper(source[0], culture) + source.Substring(1);
        }
        #endregion		

        #region ToTitleCase

        /// <summary>Title cases a string.</summary>
        /// <param name="source">The source value.</param>
        /// <returns>The string title cased using the current culture.</returns>        
        public static string ToTitleCase ( this string source )
        {
            return ToTitleCase(source, null);
        }

        /// <summary>Title cases a string.</summary>
        /// <param name="source">The source value.</param>
        /// <param name="culture">The culture to use.  If <see langword="null"/> then the current culture is used.</param>
        /// <returns>The string title cased.</returns>        
        public static string ToTitleCase ( this string source, CultureInfo culture )
        {
            culture = culture ?? CultureInfo.CurrentUICulture;

            return culture.TextInfo.ToTitleCase(source.ToLower());
        }
        #endregion

        #region ToUserFriendly

        /// <summary>Returns a copy of the string split into multiple words.</summary>
        /// <param name="source">The string to split.</param>
        /// <returns>A formatted string.</returns>
        /// <remarks>
        /// The source is converted with uppercasing on word boundaries.
        /// <para />
        /// Refer to <see cref="ToUserFriendly(String,Boolean)">ToUserFriendly</see> for a complete description.
        /// </remarks>
        /// <example>Refer to <see cref="ToUserFriendly(String,Boolean)">ToUserFriendly</see> for an example.
        /// </example>
        /// <seealso cref="O:ToCamel"/>
        /// <seealso cref="O:ToPascal"/>
        /// <seealso cref="O:String.ToUpper"/>
        /// <seealso cref="O:String.ToLower"/>
        public static string ToUserFriendly ( this string source )
        {
            return ToUserFriendly(source, true);
        }

        /// <summary>Returns a copy of the string split into multiple words.</summary>
        /// <param name="source">The string to split.</param>
        /// <param name="uppercaseBoundary"><see langword="true"/> to force uppercase on word boundaries even if the original word
        /// was lowercase.  The default is <see langword="true"/>.</param>
        /// <returns>A formatted string.</returns>
        /// <remarks>
        /// This method is primarily useful for splitting strings that are chosen for language or identifier purposes but are to be displayed to users.  
        /// Enumerations are a good example.  Any characters other than letters, digits, underscores and underscores results in an undefined 
        /// string being returned.
        /// <para/>
        /// The string is split on calculated word boundaries.  Each word is separated from the next by a space.  Word boundaries are assumed 
        /// to start at uppercase letters or digits. Multiple digits are combined into a single word.  Multiple uppercase letters are combined into 
        /// a single word except for the last letter when followed by a lowercase letter.
        /// <para/>
        /// An underscore is replaced by a space in all cases.  Chains of spaces are trimmed to one in the final string.
        /// <para />
        /// When <paramref name="uppercaseBoundary"/> is <see langword="true"/>, the default, then lowercase letters are uppercased when 
        /// they start a new word.  This has no impact on the detection of subsequent word boundaries.
        /// <para/>
        /// <list type="table">
        ///    <listheader>
        ///       <item>Value</item>
        ///       <description>Returns</description>
        ///    </listheader>
        ///    <item>
        ///       <term>RemoteUsers</term>
        ///       <description>Remote Users</description>
        ///    </item>
        ///    <item>
        ///       <term>Part1Name</term>
        ///       <description>Part 1 Name</description>
        ///    </item>
        ///    <item>
        ///       <term>Part1name</term>
        ///       <description>Part 1 Name (when <paramref name="uppercaseBoundary"/> is <see langword="true"/>) or
        ///       Part 1 name (when it is <see langword="false"/>).</description>
        ///    </item>
        ///    <item>
        ///       <term>Aircraft_Type</term>
        ///       <description>Aircraft Type</description>
        ///    </item>
        ///    <item>
        ///       <term>Aircraft_type</term>
        ///       <description>Aircraft Type (when <paramref name="uppercaseBoundary"/> is <see langword="true"/>) or
        ///       Aircraft type (when it is <see langword="false"/>).</description>
        ///    </item>
        ///    <item>
        ///       <term>ICAOCode</term>
        ///       <description>ICAO Code</description>
        ///    </item>
        /// </list>
        /// </remarks>
        /// <example>
        /// <code language="C#">
        ///    static void Main ( )
        ///    {
        ///       Console.WriteLine("Type2code".ToUserFriendly(true));   //Prints Type 2 Code
        ///       Console.WriteLine("Result_code".ToUserFriendly(true)); //Prints Result Code
        ///    }
        /// </code>
        /// </example>
        /// <seealso cref="O:ToCamel"/>
        /// <seealso cref="O:ToPascal"/>
        /// <seealso cref="O:String.ToUpper"/>
        /// <seealso cref="O:String.ToLower"/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public static string ToUserFriendly ( this string source, bool uppercaseBoundary)
        {
            if (source.Length == 0)
                return source;

            StringBuilder bldr = new StringBuilder();
            StringBuilder buffer = new StringBuilder();

            //Init the state machine with the first character: 0 = init, 1 = upper,
            // 2 = lower, 3 = space, 4 = digit
            short state = 0, oldState = 0;
            char ch = source[0];
            if (UserFriendlySeparators.Contains(ch))
            {                
                //Ignore leading spaces
                state = oldState = 3;
            } else if (Char.IsUpper(ch))
            {
                state = oldState = 1;
                buffer.Append(ch);
            } else if (Char.IsDigit(ch))
            {
                state = oldState = 4;
                buffer.Append(ch);
            } else
            {
                state = oldState = 2;

                //If we should uppercase on word boundaries
                if (uppercaseBoundary && Char.IsLower(ch))
                    buffer.Append(Char.ToUpper(ch));
                else
                    buffer.Append(ch);
            };

            //Enumerate the characters			
            for (int index = 1; index < source.Length; ++index)
            {
                ch = source[index];

                //Separator
                if (UserFriendlySeparators.Contains(ch))
                {
                    //If we are already processing a separator then ignore this one so only 1 space gets inserted
                    if (state == 3)
                        continue;

                    state = 3;
                    ch = ' ';
                } else if (Char.IsUpper(ch))
                {
                    //What state are we in
                    switch (state)
                    {
                        //Upper to upper - keep going
                        case 1: break;

                        //Lower to upper - insert space and switch states
                        case 2:
                        {
                            buffer.Append(' ');
                            state = 1;
                            break;
                        };

                        //Space to upper - switch states
                        case 3: state = 1; break;

                        //Number to upper - add space and switch states
                        case 4:
                        {
                            buffer.Append(' ');
                            state = 1;
                            break;
                        };
                    };
                } else if (Char.IsDigit(ch))
                {
                    //What state are we in
                    switch (state)
                    {
                        //Upper to number - add space and switch states
                        //Lower to number
                        case 1:
                        case 2:
                        {
                            buffer.Append(' ');
                            state = 4;
                            break;
                        };

                        //Space to number - switch states
                        case 3: state = 4; break;

                        //Number to number - keep going
                        case 4: break;
                    };
                } else //Lowercase
                {
                    //What state are we in
                    switch (state)
                    {
                        //Upper to lower - switch states
                        case 1:
                        {
                            //If we had a group of 2 or more uppers then insert
                            //a space between the last two uppers
                            if (buffer.Length > 1)
                                buffer.Insert(buffer.Length - 1, ' ');

                            state = 2;
                            break;
                        };

                        //Lower to lower - keeping going
                        case 2: break;

                        //Space to lower - switch states
                        case 3:
                        {
                            state = 2;

                            //If we should uppercase on word boundaries
                            if (uppercaseBoundary && Char.IsLower(ch))
                                ch = Char.ToUpper(ch);

                            break;
                        };

                        //Number to lower - add space and switch states
                        case 4:
                        {
                            buffer.Append(' ');
                            state = 2;

                            //If we should uppercase on word boundaries
                            if (uppercaseBoundary && Char.IsLower(ch))
                                ch = Char.ToUpper(ch);

                            break;
                        };
                    };
                };

                //If the state changes then flush the buffer
                if (state != oldState)
                {
                    if (buffer.Length > 0)
                    {
                        bldr.Append(buffer.ToString());
                        buffer.Remove(0, buffer.Length);
                    };
                    oldState = state;
                };

                //Append to the buffer
                buffer.Append(ch);
            };

            //Flush the buffer
            if (buffer.Length > 0)
                bldr.Append(buffer.ToString());

            return bldr.ToString();
        }
        #endregion

        #region TrimmedValueOrEmpty - ValueOrEmpty

        /// <summary>Gets the string obj or an empty string if the string is <see langword="null"/>.</summary>
        /// <param name="source">The source string.</param>
        /// <returns>The string or an empty string if the string is <see langword="null"/>.</returns>
        /// <example>Refer to <see cref="ValueOrEmpty"/> for an example.</example>
        /// <seealso cref="ValueOrEmpty"/>
        public static string TrimmedValueOrEmpty ( this string source )
        {
            return (source != null) ? source.Trim() : "";
        }

        /// <summary>Gets the string obj or an empty string if the string is <see langword="null"/>.</summary>
        /// <param name="source">The source string.</param>
        /// <returns>The string or an empty string if the string is <see langword="null"/>.</returns>
        /// <example>
        /// <code lang="C#">
        /// public string GetSortableName ( string firstName, string middleName, string lastName )
        /// {
        ///    return String.Format({0}, {1} {2}, lastName.ValueOrEmpty(), firstName.ValueOrEmpty(), middleName.ValueOrEmpty());
        /// }
        /// 
        /// var sortableName = GetSortableName("Bill", null, "Smith");   //Returns Smith, Bill
        /// </code>
        /// </example>
        /// <seealso cref="ValueOrEmpty"/>
        public static string ValueOrEmpty ( this string source )
        {
            return source ?? "";
        }
        #endregion

        #endregion

        #region Private Members

        private static string InternalCombine ( string separator, IEnumerable<string> values )
        {
            //Special case an empty separator
            if (separator.Length == 0)
                return String.Join("", values);

            StringBuilder bldr = new StringBuilder();
            bool endsWithSeparator = true;   //Default to true so we won't add one initially

            foreach (var value in values)
            {
                if (!String.IsNullOrEmpty(value))
                {
                    bool valueHasDelimiter = value.StartsWith(separator);

                    //Add separator as needed
                    if (!endsWithSeparator && !valueHasDelimiter)
                        bldr.Append(separator);
                    else if (endsWithSeparator && valueHasDelimiter && bldr.Length > 0)
                        bldr.Remove(bldr.Length - 1, 1);

                    //Add the next obj
                    bldr.Append(value);

                    endsWithSeparator = value.EndsWith(separator);
                };
            };

            return bldr.ToString();
        }
        
        private static readonly char[] UserFriendlySeparators = new char[] { '_', ' ', '.' };
        #endregion 
    }

    /// <summary>Defines options for string coalesce.</summary>
    /// <seealso cref="O:StringExtensions.Coalesce"/>
    [Flags]
    public enum StringCoalesceOptions
    {
        /// <summary>No options.</summary>
        None = 0,

        /// <summary>Skip empty strings as well.</summary>
        SkipEmpty = 1,
    }
}
