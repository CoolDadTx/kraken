/*
 * Copyright © 2016 Michael Taylor
 * All Rights Reserved
 * 
 * From code copyright Federation of State Medical Boards
 */
using System;
using System.ComponentModel.DataAnnotations;

using P3Net.Kraken.ComponentModel.DataAnnotations;

namespace P3Net.Kraken.Diagnostics
{
    /// <summary>Provides extension methods for <see cref="ArgumentConstraint{IValidatableObject}"/>.</summary>
    public static class ValidatableArgumentConstraintExtensions
    {
        /// <summary>Verifies the value is valid.</summary>
        /// <param name="source">The source value.</param>
        /// <returns>The new constraint.</returns>
        /// <remarks>
        /// The value must be valid.  If the value is <see langword="null"/> then it is considered valid.
        /// </remarks>
        public static AndArgumentConstraint<T> IsValid<T> ( this ArgumentConstraint<T> source ) where T : IValidatableObject
        {
            if (source.Argument.Value != null)
                ObjectValidator.ValidateFullObject(source.Argument.Value);

            return new AndArgumentConstraint<T>(source);
        }

        /// <summary>Verifies the value is not <see langword="null"/> and is valid.</summary>
        /// <param name="source">The source value.</param>
        /// <returns>The new constraint.</returns>
        /// <remarks>
        /// The value cannot be <see langword="null"/> and must be valid.
        /// </remarks>
        public static AndArgumentConstraint<T> IsNotNullAndValid<T> ( this ArgumentConstraint<T> source ) where T : class, IValidatableObject
        {
            source.IsNotNull();

            return source.IsValid();

        }
    }
}
