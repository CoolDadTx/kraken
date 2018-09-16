/*
 * Copyright © 2011 Michael Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace P3Net.Kraken.Collections
{
    /// <summary>Provides extension methods for arrays.</summary>
    public static class ArrayExtensions
    {
        /// <summary>Gets the enumerable value if it is not <see langword="null"/> or an empty list otherwise.</summary>
        /// <typeparam name="T">The type of the enumerable items.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>The source if it is not <see langword="null"/> or an empty list otherwise.</returns>
        /// <example>
        /// <code lang="C#">
        /// public void ProcessItems ( Item[] items )
        /// {
        ///    foreach (var item in items.GetValueOrEmpty())
        ///       ProcessItem(item);
        /// }
        /// </code>
        /// <code lang="VB">
        /// Sub ProcessItems ( items As Item() )
        /// 
        ///    For Each item In items.GetValueOrEmpty() 
        ///       ProcessItem(item)
        ///    Next
        /// End Sub
        /// </code>
        /// </example>
        public static T[] GetValueOrEmpty<T> ( this T[] source )
        {
            return source ?? new T[0];
        }

        /// <summary>Gets the index of the given item, if found.</summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="source">The source array.</param>
        /// <param name="item">The item to find.</param>
        /// <returns>The zero-based index or -1 if the item is not found.</returns>
        public static int IndexOf<T> ( this T[] source, T item )
        {
            return Array.IndexOf(source, item);
        }
    }
}
