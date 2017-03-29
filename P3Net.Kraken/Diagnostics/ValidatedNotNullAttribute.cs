/*
 * Copyright © 2011 Microsoft
 * All Rights Reserved
 */
#region Imports

using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace P3Net.Kraken.Diagnostics
{
    /// <summary>Provides an attribute that identifies a parameter that is already validated.</summary>
    /// <remarks>
    /// This attribute is only for resolving the code analysis warning CA1062.  Apply this attribute to parameters only if checking
    /// for <see langword="null"/> does not make sense such as in guard classes or in extension methods.
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// public static class Guard
    /// {
    ///    public static void VerifyIsNotNull ( [ValidatedNotNull] object value )
    ///    {
    ///       if (value == null)
    ///           throw new ArgumentNullException(value, "value");
    ///    }
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class ValidatedNotNullAttribute : Attribute
    {
    }
}
