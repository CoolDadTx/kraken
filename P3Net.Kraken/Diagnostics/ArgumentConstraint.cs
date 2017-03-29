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
    /// <summary>Represents an argument constraint to be validated.</summary>
    public class ArgumentConstraint<T>
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="ArgumentConstraint{T}"/> class.</summary>
        /// <param name="argument">The argument that the constraint applies to.</param>
        /// <exception cref="ArgumentNullException"><paramref name="argument"/> is <see langword="null"/>.</exception>
        public ArgumentConstraint ( Argument<T> argument )
        {
            if (argument == null)
                throw new ArgumentNullException("argument");

            m_argument = argument;
        }
        #endregion

        /// <summary>Gets the associated argument.</summary>
        public Argument<T> Argument
        {
            get { return m_argument; }
        }

        #region Is
        
        /// <summary>Verifies a condition is met.</summary>
        /// <param name="condition">The condition.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="condition"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The condition is not met.</exception>
        public AndArgumentConstraint<T> Is ( Func<T, bool> condition )
        {
            return Is(condition, "Argument is invalid.");
        }

        /// <summary>Verifies a condition is met.</summary>
        /// <param name="condition">The condition.</param>
        /// <param name="message">The error message.</param>
        /// <param name="args">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="condition"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The condition is not met.</exception>
        public AndArgumentConstraint<T> Is ( Func<T, bool> condition, string message, params object[] args )
        {
            if (condition == null)
                throw new ArgumentNullException("condition");

            if (!condition(m_argument.Value))
                throw new ArgumentException(String.Format(message, args), m_argument.Name);

            return new AndArgumentConstraint<T>(this);
        }
        #endregion

        #region IsNot
        
        /// <summary>Verifies a condition is not met.</summary>
        /// <param name="condition">The condition.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="condition"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The condition is met.</exception>
        public AndArgumentConstraint<T> IsNot ( Func<T, bool> condition )
        {
            return IsNot(condition, "Argument is invalid.");
        }

        /// <summary>Verifies a condition is not met.</summary>
        /// <param name="condition">The condition.</param>
        /// <param name="message">The error message.</param>        
        /// <param name="args">The message arguments.</param>
        /// <returns>The constraint.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="condition"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The condition is met.</exception>
        public AndArgumentConstraint<T> IsNot ( Func<T, bool> condition, string message, params object[] args )
        {
            if (condition == null)
                throw new ArgumentNullException("condition");

            if (condition(m_argument.Value))
                throw new ArgumentException(String.Format(message, args), m_argument.Name);

            return new AndArgumentConstraint<T>(this);
        }
        #endregion

        #region Private Members

        private readonly Argument<T> m_argument;
        #endregion
    }
}
