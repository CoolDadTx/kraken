/*
 * Copyright © 2014 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Linq;

using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.Algorithms
{
    /// <summary>Provides an implementation of the Luhn-10 algorithm.</summary>
    public static class Luhn
    {
        /// <summary>Calculates the check digit given a value.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The check digit (between 0 and 9).</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is empty or invalid.</exception>
        public static int CalculateCheckDigit ( string value )
        {
            Verify.Argument("value", value).IsNotNullOrEmpty().And.IsNumeric();
                        
            //Sum digits
            var sum = GetChecksum(value + '0');
            
            var mod = sum % 10;            
            return (mod == 0) ? (int)0 : (int)(10 - mod);
        }

        /// <summary>Verifies a string is valid for the algorithm.</summary>
        /// <param name="value">The string.</param>
        /// <returns><see langword="true"/> if valid or <see langword="false"/> otherwise.</returns>        
        /// <remarks>       
        /// A <see langword="null"/> or empty string is considered invalid because there is no check digit.
        /// </remarks>
        public static bool IsValid ( string value )
        {
            if (String.IsNullOrEmpty(value))
                return false;

            if (!value.IsNumeric())
                return false;

            var sum = GetChecksum(value);

            return sum % 10 == 0;
        }        

        #region Private Members
        
        private static long GetChecksum ( string value )
        {
            var deltas = new int[] { 0, 1, 2, 3, 4, -4, -3, -2, -1, 0 };

            //Enumerate the digits
            long checksum = 0;
            for (int index = value.Length - 1; index >= 0; --index)
            {
                var digit = ((int)value[index]) - 48;
                checksum += digit;
                
                if (((index - value.Length) % 2) == 0)
                    checksum += deltas[digit];
            };

            return checksum;
        }
        #endregion
    }
}
