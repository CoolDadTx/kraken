/*
 * Copyright © 2007 Michael Taylor
 * All Rights Reserved
 */
using System;

using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.Data.Common
{
    /// <summary>Simple factory for creating an output parameter.</summary>
    public class OutputParameter
    {
        #region Construction

        private OutputParameter(string name)
        {
            Verify.Argument("name", name).IsNotNullOrEmpty();

            m_name = name;
        }
        #endregion

        /// <summary>Creates a new input parameter with the provided name.</summary>
        /// <param name="name">The name of the parameter.</param>
        /// <returns>The new parameter.</returns>
        public static OutputParameter Named(string name)
        {
            return new OutputParameter(name);
        }        

        /// <summary>Gets the typed parameter.</summary>
        public OutputParameter<T> OfType<T>()
        {
            return new OutputParameter<T>(m_name);
        }

        #region Private Members

        private readonly string m_name;
        #endregion
    }
}
