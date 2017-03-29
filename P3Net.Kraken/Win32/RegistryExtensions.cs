/*
 * Copyright © 2009 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Diagnostics.CodeAnalysis;

using Microsoft.Win32;

using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.Win32
{
	/// <summary>Provides extension methods for working with the registry.</summary>
	public static class RegistryExtensions
	{
		/// <summary>Determines if a key contains a subkey.</summary>
        /// <param name="source">The source.</param>
		/// <param name="name">The subkey to look for.</param>
		/// <returns><see langword="true"/> if the subkey exists or <see langword="false"/> otherwise.</returns>
		/// <remarks>
		/// <paramref name="name"/> can consist of multiple subkeys.
		/// </remarks>
		/// <example>
		/// <code lang="C#">
		/// static void Main ( )
		/// {
		///    using (var key = Registry.LocalMachine.OpenSubKey("Software", false))
		///    {
		///       bool initProduct = true;
		///       
		///       if (key.ContainsSubkey(@"My Company\My Product"))
		///       {
		///          using (var product = key.OpenSubKey(@"My Company\My Product", false))
		///          {
		///             if (product.ContainsValue("Initialized"))
		///                initProduct = false;   
		///          };
		///       };
		///       
		///       if (initProduct)
		///          LoadDefaultValues();
		///    };
		/// }
		/// </code>
		/// </example>
        public static bool ContainsSubkey ( this RegistryKey source, string name )
		{
            if (String.IsNullOrEmpty(name))
                return false;

			//Just try and open it - might be fooled by security though
			using (var child = source.OpenSubKey(name, false))
			{
				return child != null;
			};
		}

		/// <summary>Determines if a key contains a value.</summary>
		/// <param name="source">The source.</param>
		/// <param name="name">The value to look for.</param>
		/// <returns><see langword="true"/> if the value exists or <see langword="false"/> otherwise.</returns>
		/// <example>
		/// Refer to <see cref="ContainsSubkey"/> for an example.
		/// </example>
		public static bool ContainsValue ( this RegistryKey source, string name )
		{
			object value = source.GetValue(name ?? "");
			return (value != null);
		}

        #region GetValue...

        /// <summary>Gets the value of a key as an 8-byte float.</summary>
		/// <param name="source">The source.</param>
		/// <param name="name">The name of the value.</param>
		/// <returns>The converted value or the default value if the value does not exist.</returns>        
        /// <exception cref="FormatException">The value could not be converted to the correct type.</exception>
		public static double GetValueAsDouble ( this RegistryKey source, string name )
		{
            return GetValueAsDouble(source, name, 0);
		}

        /// <summary>Gets the value of a key as an 8-byte float.</summary>
        /// <param name="source">The source.</param>
        /// <param name="name">The name of the value.</param>
        /// <param name="defaultValue">The default value to return.</param>
        /// <returns>The converted value or the default value if the value does not exist.</returns>
        /// <exception cref="FormatException">The value could not be converted to the correct type.</exception>
        public static double GetValueAsDouble ( this RegistryKey source, string name, double defaultValue )
        {
            var result = source.GetValue(name);
            if (result != null)
                return Double.Parse(result.ToString());

            return defaultValue;
        }

        /// <summary>Gets the value of a key as a 32-bit signed integer.</summary>
        /// <param name="source">The source.</param>
        /// <param name="name">The name of the value.</param>
        /// <returns>The converted value or the default value if the value does not exist.</returns>
        /// <exception cref="FormatException">The value could not be converted to the correct type.</exception>        
        public static int GetValueAsInt32 ( this RegistryKey source, string name )
        {
            return GetValueAsInt32(source, name, 0);   
        }

        /// <summary>Gets the value of a key as a 32-bit signed integer.</summary>
        /// <param name="source">The source.</param>
		/// <param name="name">The name of the value.</param>
        /// <param name="defaultValue">The default value to return.</param>
        /// <returns>The converted value or the default value if the value does not exist.</returns>
        /// <exception cref="FormatException">The value could not be converted to the correct type.</exception>
		/// <example>
		/// <code lang="C#">
		/// static void Main ( )
		/// {
		///    using (var key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion"))
		///    {
		///       foreach (string child in key.GetValueNames())
		///       {
		///          switch (key.GetValueKind(child))
		///          {
		///             case RegistryValueKind.DWord: Console.WriteLine("{0}=[DWORD] {1}", child, key.GetValueAsInt32(child)); break;
		///             
		///             case RegistryValueKind.ExpandString:
		///             case RegistryValueKind.MultiString:
		///             case RegistryValueKind.String: Console.WriteLine("{0}=[STRING] {1}", child, key.GetValueAsString(child)); break;
		///             
		///             case RegistryValueKind.QWord: Console.WriteLine("{0}=[QWORD] {1}", child, key.GetValueAsUInt64(child)); break;
		///             
		///             default: Console.WriteLine("{0}=[BINARY] {1}", child, key.GetValueAsString(child)); break;					
		///          };
		///       };
		///    };
		/// }
		/// </code>
		/// </example>
		public static int GetValueAsInt32 ( this RegistryKey source, string name, int defaultValue )
		{
            var result = source.GetValue(name);
            if (result != null)
                return Int32.Parse(result.ToString());

            return defaultValue;
		}

		/// <summary>Gets the value of a key as a 64-bit signed integer.</summary>
        /// <param name="source">The source.</param>
		/// <param name="name">The name of the value.</param>
        /// <returns>The converted value or the default value if the value does not exist.</returns>
        /// <exception cref="FormatException">The value could not be converted to the correct type.</exception>
		/// <example>
		/// Refer to <see cref="GetValueAsInt32(RegistryKey,string,Int32)">GetValueAsInt32</see> for an example.
		/// </example>
		public static long GetValueAsInt64 ( this RegistryKey source, string name )
		{
            return GetValueAsInt64(source, name, 0);
		}

        /// <summary>Gets the value of a key as a 64-bit signed integer.</summary>
        /// <param name="source">The source.</param>
        /// <param name="name">The name of the value.</param>
        /// <param name="defaultValue">The default value to return.</param>
        /// <returns>The converted value or the default value if the value does not exist.</returns>
        /// <exception cref="FormatException">The value could not be converted to the correct type.</exception>
        /// <example>
        /// Refer to <see cref="GetValueAsInt32(RegistryKey,string,Int32)">GetValueAsInt32</see> for an example.
        /// </example>
        public static long GetValueAsInt64 ( this RegistryKey source, string name, long defaultValue )
        {
            var result = source.GetValue(name);
            if (result != null)
                return Int64.Parse(result.ToString());

            return defaultValue;
        }

        /// <summary>Gets the value of a key as a string.</summary>
        /// <param name="source">The source.</param>
        /// <param name="name">The name of the value.</param>
        /// <returns>The converted value or an empty string if the value does not exist.</returns>
        /// <exception cref="FormatException">The value could not be converted to the correct type.</exception>
        /// <example>
        /// Refer to <see cref="GetValueAsInt32(RegistryKey,string,Int32)">GetValueAsInt32</see> for an example.
        /// </example>
        public static string GetValueAsString ( this RegistryKey source, string name )
        {
            return GetValueAsString(source, name, "");
        }

		/// <summary>Gets the value of a key as a string.</summary>
        /// <param name="source">The source.</param>
		/// <param name="name">The name of the value.</param>
        /// <param name="defaultValue">The default value to return.</param>
        /// <returns>The converted value or the default value if the value does not exist.</returns>
		/// <example>
		/// Refer to <see cref="GetValueAsInt32(RegistryKey,string, Int32)">GetValueAsInt32</see> for an example.
		/// </example>
		public static string GetValueAsString ( this RegistryKey source, string name, string defaultValue )
		{
            var result = source.GetValue(name);

            return (result != null) ? result.ToString() : defaultValue;
		}
        #endregion

        #region TryGetValue...

        /// <summary>Gets the value of a key as an 8-byte float.</summary>
        /// <param name="source">The source.</param>
        /// <param name="name">The name of the value.</param>
        /// <param name="value">The converted value.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        public static bool TryGetValueAsDouble ( this RegistryKey source, string name, out double value )
        {
            var result = source.GetValue(name);
            if (result != null)
            {
                if (Double.TryParse(result.ToString(), out value))
                    return true;
            };

            value = 0;
            return false;
        }

        /// <summary>Gets the value of a key as a 32-bit signed integer.</summary>
        /// <param name="source">The source.</param>
        /// <param name="name">The name of the value.</param>
        /// <param name="value">The converted value.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        public static bool TryGetValueAsInt32 ( this RegistryKey source, string name, out int value )
        {
            var result = source.GetValue(name);
            if (result != null)
            {
                if (Int32.TryParse(result.ToString(), out value))
                    return true;
            };

            value = 0;
            return false;
        }

        /// <summary>Gets the value of a key as a 64-bit signed integer.</summary>
        /// <param name="source">The source.</param>
        /// <param name="name">The name of the value.</param>
        /// <param name="value">The converted value.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        public static bool TryGetValueAsInt64 ( this RegistryKey source, string name, out long value )
        {
            var result = source.GetValue(name);

            if (result != null)
            {
                if (Int64.TryParse(result.ToString(), out value))
                    return true;
            };

            value = 0;
            return false;
        }

        /// <summary>Gets the value of a key as a string.</summary>
        /// <param name="source">The source.</param>
        /// <param name="name">The name of the value.</param>
        /// <param name="value">The converted value.</param>
        /// <returns><see langword="true"/> if successful or <see langword="false"/> otherwise.</returns>
        public static bool TryGetValueAsString ( this RegistryKey source, string name, out string value )
        {
            var result = source.GetValue(name);
            if (result != null)
            {
                value = result.ToString();
                return true;
            };

            value = null;
            return false;
        }
        #endregion
    }
}
