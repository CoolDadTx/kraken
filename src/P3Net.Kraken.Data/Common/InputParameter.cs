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
            Verify.Argument(nameof(name)).WithValue(name).IsNotNullOrEmpty();

            _name = name;
        }
        #endregion

        /// <summary>Creates a new input parameter with the provided name.</summary>
        /// <param name="name">The name of the parameter.</param>
        /// <returns>The new parameter.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="name"/> is empty.</exception>
        public static InputParameter Named ( string name ) => new InputParameter(name);

        /// <summary>Creates an <see cref="InputParameter{T}"/> for the given type.</summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <returns>The typed parameter.</returns>
        /// <remarks>
        /// This method should be used when <see cref="WithValue"/> cannot be used.
        /// </remarks>
        public InputParameter<T> OfType<T> () => new InputParameter<T>(_name);

        /// <summary>Sets the value of the parameter.</summary>
        /// <param name="value">The parameter value.</param>
        /// <returns>The updated parameter.</returns>
        public InputParameter<T> WithValue<T> ( T value ) => new InputParameter<T>(_name).WithValue(value);

        #region Private Members

        private readonly string _name;
        #endregion
    }
}
