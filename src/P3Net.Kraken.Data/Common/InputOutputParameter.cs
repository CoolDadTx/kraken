/*
 * Copyright © 2007 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Data;
using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.Data.Common
{
    /// <summary>Simple factory for creating input/output parameters.</summary>
    public class InputOutputParameter
    {
        #region Construction

        private InputOutputParameter (string name)
        {
            Verify.Argument(nameof(name)).WithValue(name).IsNotNullOrEmpty();

            _name = name;
        }
        #endregion

        /// <summary>Creates a new input parameter with the provided name.</summary>
        /// <param name="name">The name of the parameter.</param>
        /// <returns>The new parameter.</returns>
        public static InputOutputParameter Named (string name) => new InputOutputParameter(name);

        /// <summary>Creates an <see cref="InputParameter{T}"/> for the given type.</summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <returns>The typed parameter.</returns>
        /// <remarks>
        /// This method should be used when <see cref="WithValue"/> cannot be used.
        /// </remarks>
        public InputOutputParameter<T> OfType<T> () => new InputOutputParameter<T>(_name);

        /// <summary>Sets the value of the parameter.</summary>
        /// <param name="value">The parameter value.</param>
        /// <returns>The updated parameter.</returns>
        public InputOutputParameter<T> WithValue<T>(T value) => new InputOutputParameter<T>(_name).WithValue(value);

        #region Private members

        private readonly string _name;
        #endregion
    }
}
