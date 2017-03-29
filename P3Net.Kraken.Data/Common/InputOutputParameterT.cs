/*
 * Copyright © 2007 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Data;

namespace P3Net.Kraken.Data.Common
{
    /// <summary>Represents an input/output parameter.</summary>
    /// <typeparam name="T">The CLR type of the parameter.</typeparam>
    /// <seealso cref="DbTypeMapper"/>
    public class InputOutputParameter<T> : DataParameter<T>
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="InputOutputParameter{T}"/> class.</summary>
        /// <param name="name">The name of the parameter.</param>
        public InputOutputParameter(string name) : base(name, ParameterDirection.InputOutput)
        { }
        #endregion

        /// <summary>Gets the current typed value.</summary>
        /// <returns>The typed parameter value.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public T GetValue()
        {
            return TypedValue;
        }

        /// <summary>Sets the value of the parameter.</summary>
        /// <param name="value">The parameter value.</param>
        /// <returns>The updated parameter.</returns>
        public InputOutputParameter<T> WithValue(T value)
        {
            Value = value;
            return this;
        }
    }
}
