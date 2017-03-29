/*
 * Copyright © 2016 Michael Taylor
 * All Rights Reserved
 * 
 * From code copyright Federation of State Medical Boards
 */
using System;

namespace P3Net.Kraken.Diagnostics
{
    /// <summary>Provides extension methods for <see cref="ArgumentConstraint{T}"/> where the type is an enumeration.</summary>
    public static class EnumArgumentConstraintExtensions
    {
        /// <summary>Verifies the value is a valid enum.</summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="source">The source value.</param>
        /// <returns>The new constraint.</returns>
        public static AndArgumentConstraint<T> IsValidEnum<T> ( this ArgumentConstraint<T> source ) where T : struct
        {
            if (!EnumExtensions.IsDefined(source.Argument.Value))
                throw new ArgumentOutOfRangeException(source.Argument.Name, "Undefined enumeration value.");

            return new AndArgumentConstraint<T>(source);
        }

        /// <summary>Verifies the value is a valid enum except zero (the standard default).</summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="source">The source value.</param>
        /// <returns>The new constraint.</returns>
        public static AndArgumentConstraint<T> IsValidEnumAndNotZero<T>( this ArgumentConstraint<T> source ) where T : struct, IComparable
        {
            source.IsValidEnum();

            if (source.Argument.Value.CompareTo(default(T)) == 0)
                throw new ArgumentOutOfRangeException(source.Argument.Name, "Invalid enumeration value.");

            return new AndArgumentConstraint<T>(source);
        }

        /// <summary>Verifies the value is one of given set of values.</summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="validValues">The list of valid values.</param>
        /// <returns>The new constraint.</returns>
        public static AndArgumentConstraint<T> IsIn<T>( this ArgumentConstraint<T> source, params T[] validValues ) where T : struct, IComparable
        {
            foreach (var validValue in validValues)
            {
                if (source.Argument.Value.CompareTo(validValue) == 0)
                    return new AndArgumentConstraint<T>(source);                
            };

            throw new ArgumentOutOfRangeException(source.Argument.Name, "Invalid enumeration value.");
        }

        /// <summary>Verifies the value is not in the given list.</summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="source">The source value.</param>
        /// <param name="invalidValues">The list of invalid values.</param>
        /// <returns>The new constraint.</returns>
        public static AndArgumentConstraint<T> IsNotIn<T>( this ArgumentConstraint<T> source, params T[] invalidValues ) where T : struct, IComparable
        {
            foreach (var invalidValue in invalidValues)
            {
                if (source.Argument.Value.CompareTo(invalidValue) == 0)
                    throw new ArgumentOutOfRangeException(source.Argument.Name, "Invalid enumeration value.");
            };

            return new AndArgumentConstraint<T>(source);
        }
    }
}
