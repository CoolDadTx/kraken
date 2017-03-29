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
    /// <summary>Specifies a value uses the Unicode character set.</summary>   
    /// <seealso cref="AnsiAttribute"/>
    /// <seealso cref="CharSetAttribute"/>    
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes")]    
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = false)]
    public class UnicodeAttribute : CharSetAttribute
    {
        /// <summary>Initializes an instance of the <see cref="UnicodeAttribute"/> class.</summary>
        public UnicodeAttribute () : base(CharSet.Unicode)
        {
        }
    }
}
