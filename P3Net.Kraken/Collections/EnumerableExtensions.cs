/*
 * Copyright © 2008 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Linq;

using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.Collections
{
    /// <summary>Provides extension methods for enumerable lists.</summary>
    public static class EnumerableExtensions
    {        
        /// <summary>Performs an action on each item in the list.</summary>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="source">The source to enumerate.</param>
        /// <param name="action">The action to perform on each item.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// The action is performed immediately on each item in the source.
        /// </remarks>
        /// <returns>The original list after the action has been performed.</returns>
        /// <seealso cref="ForEachIf{T}" />
        public static IEnumerable<T> ForEach<T> ( this IEnumerable<T> source, Action<T> action )
        {
            Verify.Argument("action", action).IsNotNull();

            foreach (T item in source)
            {
                action(item);
            };

            return source;
        }

        /// <summary>Performs an action on each item in the list.</summary>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="source">The source to enumerate.</param>
        /// <param name="action">The action to perform on each item.</param>
        /// <param name="predicate">The predicate to check.</param>
        /// <exception cref="ArgumentNullException"><paramref name="action"/> or <paramref name="predicate"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// <paramref name="action"/> is performed on each item in the list if <paramref name="predicate"/>
        /// returns <see langword="true"/> for the item.  The action is performed immediately
        /// on each item.
        /// </remarks>		
        /// <returns>The original list after the action has been performed.</returns>
        /// <seealso cref="ForEach{T}"/>
        public static IEnumerable<T> ForEachIf<T> ( this IEnumerable<T> source, Action<T> action, Predicate<T> predicate )
        {
            Verify.Argument("action", action).IsNotNull();
            Verify.Argument("predicate", predicate).IsNotNull();

            source.ForEach(i =>
            {
                if (predicate(i))
                    action(i);
            });

            return source;
        }

        /// <summary>Gets the enumerable value if it is not <see langword="null"/> or an empty list otherwise.</summary>
        /// <typeparam name="T">The type of the enumerable items.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>The source if it is not <see langword="null"/> or an empty list otherwise.</returns>
        public static IEnumerable<T> GetValueOrEmpty<T> ( this IEnumerable<T> source )
        {
            return source ?? new T[0];
        }

        /// <summary>Determines if the source is <see langword="null"/> or empty.</summary>
        /// <typeparam name="T">The type of items being enumerated.</typeparam>
        /// <param name="source">The source object.</param>
        /// <returns><see langword="true"/> if the source is <see langword="null"/> or empty.</returns>
        public static bool IsNullOrEmpty<T> ( this IEnumerable<T> source )
        {
            return (source == null) || !source.Any();
        }

        /// <summary>Orders a list of items using the given selector.</summary>
        /// <typeparam name="TItem">The items being ordered.</typeparam>
        /// <typeparam name="TKey">The property being ordered by.</typeparam>
        /// <param name="source">The list of items.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The ordered list.</returns>
        /// <remarks>
        /// This method calls <see cref="O:Enumerable.OrderBy">OrderBy</see> or <see cref="O:Enumerable.ThenBy">ThenBy</see> depending upon whether the list is
        /// already ordered.
        /// </remarks>
        public static IOrderedEnumerable<TItem> OrderThenBy<TItem, TKey> ( this IEnumerable<TItem> source, Func<TItem, TKey> selector )
        {
            return OrderThenByDirection(source, selector, false);
        }

        /// <summary>Orders a list of items using the given selector in descending order.</summary>
        /// <typeparam name="TItem">The items being ordered.</typeparam>
        /// <typeparam name="TKey">The property being ordered by.</typeparam>
        /// <param name="source">The list of items.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The ordered list.</returns>
        /// <remarks>
        /// This method calls <see cref="O:Enumerable.OrderByDescending">OrderByDescending</see> or <see cref="O:Enumerable.ThenByDescending">ThenByDescending</see> depending upon whether the list is
        /// already ordered.
        /// </remarks>
        public static IOrderedEnumerable<TItem> OrderThenByDescending<TItem, TKey> ( this IEnumerable<TItem> source, Func<TItem, TKey> selector )
        {
            return OrderThenByDirection(source, selector, true);
        }

        /// <summary>Orders a list of items using the given selector.</summary>
        /// <typeparam name="TItem">The items being ordered.</typeparam>
        /// <typeparam name="TKey">The property being ordered by.</typeparam>
        /// <param name="source">The list of items.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="isDescending"><see langword="true"/> to order descending.</param>
        /// <returns>The ordered list.</returns>
        /// <remarks>
        /// This method calls <see cref="O:Enumerable.OrderBy">OrderBy</see> or <see cref="O:Enumerable.ThenBy">ThenBy</see> depending upon whether the list is
        /// already ordered.
        /// </remarks>
        public static IOrderedEnumerable<TItem> OrderThenByDirection<TItem, TKey> ( this IEnumerable<TItem> source, Func<TItem, TKey> selector, bool isDescending )
        {
            var ordered = source as IOrderedEnumerable<TItem>;
            if (ordered == null)
            {
                if (isDescending)
                    return source.OrderByDescending(selector);
                else
                    return source.OrderBy(selector);
            } else if (isDescending)
            {
                return ordered.ThenByDescending(selector);
            } else
                return ordered.ThenBy(selector);
        }

        /// <summary>Removes items from the list if a condition is met.</summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="condition">The condition to check.</param>
        /// <returns>The filtered list.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="condition"/> is <see langword="null"/>.</exception>
        public static IEnumerable<T> RemoveIf<T> ( this IEnumerable<T> source, Func<T, bool> condition )
        {
            Verify.Argument("condition", condition).IsNotNull();

            if (source != null)
                return source.Where(x => !condition(x));

            return Enumerable.Empty<T>();
        }
    }
}
