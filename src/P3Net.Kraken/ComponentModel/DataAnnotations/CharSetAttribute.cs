/*
 * Copyright © 2014 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace P3Net.Kraken.ComponentModel.DataAnnotations
{
    /// <summary>Specifies the character set of a value.</summary>   
    /// <seealso cref="AnsiAttribute"/>
    /// <seealso cref="UnicodeAttribute"/>    
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes")]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = false)]
    public class CharSetAttribute : Attribute
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="CharSetAttribute"/> class.</summary>
        /// <param name="charSet">The character set of the value.</param>
        public CharSetAttribute ( CharSet charSet)
        {
            CharSet = charSet;
        }
        #endregion

        /// <summary>Determines the character set.</summary>
        public CharSet CharSet { get; private set; }
    }
}
