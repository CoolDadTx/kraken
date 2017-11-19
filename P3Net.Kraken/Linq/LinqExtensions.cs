/*
 * Copyright © 2017 P3Net
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P3Net.Kraken.Linq
{
    /// <summary>Provides some LINQ extensions.</summary>
    public static class LinqExtensions
    {
        /// <summary>Returns the first or default item from the query.</summary>
        /// <typeparam name="T">The type returned by the query.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>The first item or default if it is empty.</returns>
        public static Task<T> FirstOrDefaultAsync<T> ( this IEnumerable<T> source )
                                    => FirstOrDefaultAsync(source, CancellationToken.None);

        /// <summary>Returns the first or default item from the query.</summary>
        /// <typeparam name="T">The type returned by the query.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The first item or default if it is empty.</returns>
        public static Task<T> FirstOrDefaultAsync<T> ( this IEnumerable<T> source, CancellationToken cancellationToken )
                                    => Task.Run(() => source.FirstOrDefault());

        /// <summary>Returns the first or default item from the query.</summary>
        /// <typeparam name="T">The type returned by the query.</typeparam>        
        /// <param name="source">The source.</param>
        /// <param name="predicate">The predicate to apply.</param>
        /// <returns>The first item or default if it is empty.</returns>
        public static Task<T> FirstOrDefaultAsync<T> ( this IEnumerable<T> source, Func<T, bool> predicate )
                                    => FirstOrDefaultAsync(source, predicate, CancellationToken.None);

        /// <summary>Returns the first or default item from the query.</summary>
        /// <typeparam name="T">The type returned by the query.</typeparam>        
        /// <param name="source">The source.</param>
        /// <param name="predicate">The predicate to apply.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The first item or default if it is empty.</returns>
        public static Task<T> FirstOrDefaultAsync<T> ( this IEnumerable<T> source, Func<T, bool> predicate, CancellationToken cancellationToken )
                                    => Task.Run(() => source.FirstOrDefault(predicate));

        /// <summary>Returns the last or default item from the query.</summary>
        /// <typeparam name="T">The type returned by the query.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>The last item or default if it is empty.</returns>
        public static Task<T> LastOrDefaultAsync<T> ( this IEnumerable<T> source )
                                    => LastOrDefaultAsync(source, CancellationToken.None);

        /// <summary>Returns the last or default item from the query.</summary>
        /// <typeparam name="T">The type returned by the query.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The last item or default if it is empty.</returns>
        public static Task<T> LastOrDefaultAsync<T> ( this IEnumerable<T> source, CancellationToken cancellationToken )
                                    => Task.Run(() => source.LastOrDefault());

        /// <summary>Returns the last or default item from the query.</summary>
        /// <typeparam name="T">The type returned by the query.</typeparam>        
        /// <param name="source">The source.</param>
        /// <param name="predicate">The predicate to apply.</param>
        /// <returns>The last item or default if it is empty.</returns>
        public static Task<T> LastOrDefaultAsync<T> ( this IEnumerable<T> source, Func<T, bool> predicate )
                                    => LastOrDefaultAsync(source, predicate, CancellationToken.None);

        /// <summary>Returns the last or default item from the query.</summary>
        /// <typeparam name="T">The type returned by the query.</typeparam>        
        /// <param name="source">The source.</param>
        /// <param name="predicate">The predicate to apply.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The last item or default if it is empty.</returns>
        public static Task<T> LastOrDefaultAsync<T> ( this IEnumerable<T> source, Func<T, bool> predicate, CancellationToken cancellationToken )
                                    => Task.Run(() => source.LastOrDefault(predicate));

        /// <summary>Returns the query as an array.</summary>
        /// <typeparam name="T">The type returned by the query.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>The array of items.</returns>
        public static Task<T[]> ToArrayAsync<T> ( this IEnumerable<T> source ) => ToArrayAsync(source, CancellationToken.None);

        /// <summary>Returns the query as an array.</summary>
        /// <typeparam name="T">The type returned by the query.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The array of items.</returns>
        public static Task<T[]> ToArrayAsync<T> ( this IEnumerable<T> source, CancellationToken cancellationToken )
        {
            return Task.Run(() => Convert());

            T[] Convert ()
            {
                var items = new T[source.Count()];
                var index = 0;

                foreach (var item in source)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    items[index++] = item;
                };

                return items;
            }
        }

        /// <summary>Returns the query as a list.</summary>
        /// <typeparam name="T">The type returned by the query.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>The list of items.</returns>
        public static Task<List<T>> ToListAsync<T> ( this IEnumerable<T> source ) => ToListAsync(source, CancellationToken.None);

        /// <summary>Returns the query as a list.</summary>
        /// <typeparam name="T">The type returned by the query.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The list of items.</returns>
        public static Task<List<T>> ToListAsync<T> ( this IEnumerable<T> source, CancellationToken cancellationToken )
        {
            return Task.Run(() => Convert());

            List<T> Convert ()
            {
                var items = new List<T>(source.Count());

                foreach (var item in source)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    items.Add(item);
                };

                return items;
            }
        }
    }
}
