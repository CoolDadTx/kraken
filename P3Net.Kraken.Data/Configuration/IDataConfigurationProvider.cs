/*
 * Copyright © 2018 Michael Taylor
 * All Rights Reserved
 * 
 * Portions copyright Federation of State Medical Boards
 */
using System;

namespace P3Net.Kraken.Data.Configuration
{
    /// <summary>Provides data configuration information.</summary>
    public interface IDataConfigurationProvider
    {
        /// <summary>Gets a connection string given its name.</summary>
        /// <param name="name">The name of the connection string.</param>
        /// <returns>The connection string or <see langword="null"/> if the connection string is not found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="name"/> is empty.</exception>
        string GetConnectionString ( string name );
    }
}
