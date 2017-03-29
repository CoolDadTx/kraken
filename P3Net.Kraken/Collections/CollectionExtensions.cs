/*
 * Copyright © 2008 Michael Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.Collections.Generic;
using System.Linq;

using P3Net.Kraken.Diagnostics;
#endregion

namespace P3Net.Kraken.Collections
{
    /// <summary>Provides extension methods for collections.</summary>
    public static class CollectionExtensions
    {		
        /// <summary>Adds a list of items to an existing collection.</summary>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="source">The source list to update.</param>
        /// <param name="items">The items to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="items"/> is <see langword="null"/>.</exception>
        /// <example>		
        /// <code lang="C#">
        /// static void Main ( )
        /// {
        ///    var values = new Collection&lt;int&gt;();
        ///    values.AddRange(new int[] { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 };
        /// }
        /// </code>
        /// <code lang="VB">
        /// Shared Sub Main ( )
        /// 
        ///    Dim values As New Collection(Of Integer)()
        ///    values.AddRange(New Integer() { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 }
        /// End Sub
        /// </code>
        /// </example>
        public static void AddRange<T> ( this ICollection<T> source, IEnumerable<T> items )
        {
            Verify.Argument("items", items).IsNotNull();

            if (!items.IsNullOrEmpty())
                items.ForEach(i => source.Add(i));
        }
    }
}
