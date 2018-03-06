/*
 * Copyright © 2018 Michael Taylor
 * All Rights Reserved
 * 
 * Portions copyright Federation of State Medical Boards
 */
using System;
using System.Collections.Generic;

using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.Data.Configuration
{
    /// <summary>Provides an implementation of <see cref="IDataConfigurationProvider"/> using in memory objects.</summary>
    public class MemoryDataConfigurationProvider : IDataConfigurationProvider
    {
        /// <summary>Gets the connection strings defined for the provider.</summary>
        public IDictionary<string, string> ConnectionStrings { get; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>Gets a connection string given its name.</summary>
        /// <param name="name">The name of the connection string.</param>
        /// <returns>The connection string or <see langword="null"/> if the connection string is not found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="name"/> is empty.</exception>
        public string GetConnectionString ( string name )
        {
            Verify.Argument(nameof(name)).WithValue(name).IsNotNullOrEmpty();

            if (ConnectionStrings.TryGetValue(name, out var connString))
                return connString;

            return null;
        }
    }
}
