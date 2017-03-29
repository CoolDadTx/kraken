/*
 * Copyright © 2007 Michael Taylor
 * All Rights Reserved
 */
using System;

using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.Data.Common
{
    /// <summary>Provides a simple factory for creating input parameters.</summary>
    public class InputParameter
    {
        #region Construction

        private InputParameter ( string name )
        {
            Verify.Argument("name", name).IsNotNullOrEmpty();

            m_name = name;
        }
        #endregion

        /// <summary>Creates a new input parameter with the provided name.</summary>
        /// <param name="name">The name of the parameter.</param>
        /// <returns>The new parameter.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="name"/> is empty.</exception>
        public static InputParameter Named ( string name )
        {
            return new InputParameter(name);
        }

        /// <summary>Sets the value of the parameter.</summary>
        /// <param name="value">The parameter value.</param>
        /// <returns>The updated parameter.</returns>
        public InputParameter<T> WithValue<T> ( T value )
        {
            return new InputParameter<T>(m_name).WithValue(value);
        }

        #region Private Members

        private readonly string m_name;
        #endregion
    }
}
