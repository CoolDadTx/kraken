/*
 * Copyright © 2017 P3Net
 * All Rights Reserved
 */
using System;

using P3Net.Kraken.Collections;

namespace P3Net.Kraken.Data.Common
{
    /// <summary>Provides extension methods for <see cref="DataCommand"/> types.</summary>
    public static class DataCommandExtensions
    {
        /// <summary>Adds parameters to the command.</summary>
        /// <typeparam name="T">The type of command.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="parameters">The parameters to add.</param>
        /// <returns>The updated command.</returns>
        public static T WithParameters<T> ( this T source, params DataParameter[] parameters ) where T: DataCommand
        {
            if (parameters != null)
                source.Parameters.AddRange(parameters);

            return source;
        }
    }
}
