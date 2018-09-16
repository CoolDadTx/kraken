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
    /// <summary>Specifies a value uses the ANSI character set.</summary>   
    /// <seealso cref="CharSetAttribute"/>
    /// <seealso cref="UnicodeAttribute"/>    
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes")]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = false)]
    public class AnsiAttribute : CharSetAttribute
    {
        /// <summary>Initializes an instance of the <see cref="AnsiAttribute"/> class.</summary>
        public AnsiAttribute () : base(CharSet.Ansi)
        {
        }
    }
}
