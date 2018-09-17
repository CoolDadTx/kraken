/*
 * Copyright © 2018 Michael Taylor
 * All Rights Reserved
 * 
 * Portions copyright Federation of State Medical Boards
 */
using System;
using System.Configuration;

using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.Data.Configuration
{
    /// <summary>Provides a <see cref="IDataConfigurationProvider"/> implementation using <see cref="ConfigurationManager"/>.</summary>
    public class ConfigurationManagerDataConfigurationProvider : IDataConfigurationProvider
    {
        /// <summary>Provides a default instance of the provider.</summary>
        public static ConfigurationManagerDataConfigurationProvider Default = new ConfigurationManagerDataConfigurationProvider();

        /// <summary>Gets a connection string given its name.</summary>
        /// <param name="name">The name of the connection string.</param>
        /// <returns>The connection string or <see langword="null"/> if the connection string is not found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="name"/> is empty.</exception>
        public string GetConnectionString ( string name )
        {
            Verify.Argument(nameof(name)).WithValue(name).IsNotNullOrEmpty();

            var settings = ConfigurationManager.ConnectionStrings[name];
            
            return settings?.ConnectionString ?? null;
        }
    }
}
