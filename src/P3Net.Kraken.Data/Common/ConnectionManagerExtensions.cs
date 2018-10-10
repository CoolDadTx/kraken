/*
 * Copyright © 2018 Michael Taylor
 * All Rights Reserved
 */
using System;

using P3Net.Kraken.Diagnostics;

#if NET_FRAMEWORK

using System.Configuration;
#endif

namespace P3Net.Kraken.Data.Common
{
    /// <summary>Provides extension methods for <see cref="ConnectionManager"/>.</summary>
    public static class ConnectionManagerExtensions
    {
        /// <summary>Sets the connection string of the <see cref="ConnectionManager"/> to a specific value.</summary>
        /// <typeparam name="T">The type of connection manager.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="connectionString">The connection string to use.</param>
        /// <returns>The connection manager.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="connectionString"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="connectionString"/> is empty.</exception>
        public static T WithConnectionString<T> ( this T source, string connectionString ) where T : ConnectionManager
        {
            source.ConnectionString = connectionString;

            return source;
        }

#if NET_FRAMEWORK

        /// <summary>Sets the connection string of the <see cref="ConnectionManager"/> given the name in the configuration file.</summary>
        /// <typeparam name="T">The type of connection manager.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="name">The name of the connection string.</param>
        /// <returns>The connection manager.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="name"/> is empty.</exception>
        public static T WithConnectionStringName<T> ( this T source, string name ) where T : ConnectionManager
        {
            Verify.Argument(nameof(name)).WithValue(name).IsNotNullOrEmpty();

            var settings = ConfigurationManager.ConnectionStrings[name];
            if (settings == null)
                throw new ItemNotFoundException("Connection string not found.", name);

            source.ConnectionString = settings.ConnectionString;

            return source;
        }
#endif
    }
}
