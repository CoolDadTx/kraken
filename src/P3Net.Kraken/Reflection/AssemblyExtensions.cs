/*
 * Copyright © 2006 Michael Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

#endregion

namespace P3Net.Kraken.Reflection
{
    /// <summary>Provides extensions for <see cref="Assembly"/>.</summary>
    /// <example>
    /// <code lang="C#">
    ///  void PrintAssemblyInformation ( Assembly assembly )
    ///	{
    ///		Console.WriteLine("Product = {0}", assembly.GetProductName());
    ///		Console.WriteLine("Version = {0}", assembly.GetVersionString());
    ///		Console.WriteLine("Company = {0}", assembly.GetCompanyName());
    ///		Console.WriteLine("Description = {0}", assembly.GetDescription());
    ///	}
    /// </code>
    /// <code lang="VB">
    /// Sub PrintAssemblyInformation ( assembly As Assembly )
    /// 
    ///      Console.WriteLine("Product = {0}", assembly.GetProductName())
    ///      Console.WriteLine("Version = {0}", assembly.GetVersionString())
    /// 	    Console.WriteLine("Company = {0}", assembly.GetCompanyName())
    /// 	    Console.WriteLine("Description = {0}", assembly.GetDescription())
    /// End Sub
    /// </code>
    /// </example>
    public static class AssemblyExtensions
    {		
        #region Public Members

        /// <summary>Gets the details for an assembly.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The details of the assembly.</returns>        
        public static AssemblyDetails GetAssemblyDetails ( this Assembly source )
        {
            string key = source.FullName;

            //Look it up first
            AssemblyDetails details;
            if (s_cache.TryGetValue(key, out details))
                return details;

            //Insert into the cache
            lock (s_cache)
            {            
                details = new AssemblyDetails(source);
                s_cache[key] = details;

                return details;
            };
        }        
        #endregion

        #region Private Members

        private static Dictionary<string, AssemblyDetails> s_cache = new Dictionary<string, AssemblyDetails>(StringComparer.OrdinalIgnoreCase); 

        #endregion
    }
}
