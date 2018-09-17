/*
 *  Copyright © 2006 Michael Taylor 
 *  All Rights Reserved 
 */
using System;
using System.Diagnostics;
using System.Linq;

namespace P3Net.Kraken.Diagnostics
{    
    /// <summary>Provides support for verifying expressions.</summary>
    [DebuggerStepThrough]
    public static class Verify
    {
        /// <summary>Creates an argument that can be verified.</summary>
        /// <param name="name">The argument name.</param>
        /// <returns>The argument.</returns>
        public static Argument Argument ( string name )
        {
            return new Argument(name);
        }

        /// <summary>Creates an argument that can be verified.</summary>
        /// <typeparam name="T">The type of the argument.</typeparam>
        /// <param name="name">The argument name.</param>
        /// <param name="value">The argument value.</param>
        /// <returns>The argument constraint.</returns>
        public static ArgumentConstraint<T> Argument<T> ( string name, T value )
        {
            return new ArgumentConstraint<T>(new Argument<T>(name, value));
        }                        
    }
}
