/*
 * Copyright © 2013 Michael Taylor
 * All Rights Reserved
 * 
 * From code copyright Federation of State Medical Boards
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace P3Net.Kraken.Diagnostics
{
    /// <summary>Represents an argument to be validated.</summary>
    public class Argument
    {
        #region Construction

        /// <summary>Sets the name of the argument.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The argument.</returns>
        public Argument ( string value )
        {
            m_name = value ?? "";
        }
        #endregion

        /// <summary>Gets the name of the argument.</summary>
        public string Name
        {
            get { return m_name; }
        }

        /// <summary>Associates a value with an argument.</summary>
        /// <typeparam name="T">The argument type.</typeparam>
        /// <param name="value">The argument value.</param>
        /// <returns>The constraint.</returns>
        public ArgumentConstraint<T> WithValue<T> ( T value )
        {
            return new ArgumentConstraint<T>(new Argument<T>(m_name, value));
        }
                
        #region Private Members

        private readonly string m_name;
        #endregion
    }

    /// <summary>Represents a typed argument to be validated.</summary>
    /// <typeparam name="T">The type of the argument.</typeparam>
    public class Argument<T>
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="Argument"/> class.</summary>
        /// <param name="name">The argument name.</param>
        /// <param name="value">The argument value.</param>
        public Argument ( string name, T value )
        {
            m_name = name ?? "";
            m_value = value;
        }
        #endregion
       
        /// <summary>Gets the name of the argument.</summary>
        public string Name 
        {
            get { return m_name; }
        }

        /// <summary>Gets the value of the argument.</summary>
        public T Value 
        {
            get { return m_value; }
        }

        #region Private Members

        private readonly string m_name;
        private readonly T m_value;
        #endregion
    }
}
