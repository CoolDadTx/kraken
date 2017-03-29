/*
 * Copyright © 2012 Michael Taylor
 * All Rights Reserved
 * 
 * From code copyright  Federation of State Medical Boards
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace P3Net.Kraken.ComponentModel.DataAnnotations
{
    /// <summary>Provides support for validating objects.</summary>
    /// <remarks>
    /// This class provides additional functionality for <see cref="Validator"/>.
    /// </remarks>    
    public static class ObjectValidator
    {
        #region TryValidateFullObject

        /// <summary>Attempts to validate an object.</summary>
        /// <param name="instance">The instance to validate.</param>
        /// <returns>The validation results.</returns>
        /// <remarks>
        /// The full object, including any data annotations, is validated.
        /// </remarks>
        public static ValidationResults TryValidateFullObject ( object instance )
        {
            return TryValidateFullObject(instance, new ValidationContext(instance, null, null));
        }

        /// <summary>Attempts to validate an object.</summary>
        /// <param name="instance">The instance to validate.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>The validation results.</returns>
        /// <remarks>
        /// The full object, including any data annotations, is validated.
        /// </remarks>
        public static ValidationResults TryValidateFullObject ( object instance, ValidationContext validationContext )
        {
            return TryValidateObject(instance, validationContext, true);
        }        
        #endregion
        
        #region TryValidateObject

        /// <summary>Attempts to validate an object.</summary>
        /// <param name="instance">The instance to validate.</param>
        /// <returns>The validation results.</returns>
        /// <remarks>
        /// Only the required annotations and <see cref="IValidatableObject.Validate"/> are run.
        /// </remarks>
        public static ValidationResults TryValidateObject ( object instance )
        {
            return TryValidateObject(instance, new ValidationContext(instance, null, null), false);
        }

        /// <summary>Attempts to validate an object.</summary>
        /// <param name="instance">The instance to validate.</param>
        /// <param name="validateAllProperties"><see langword="true"/> to validate all property annotations.</param>
        /// <returns>The validation results.</returns>
        /// <remarks>
        /// If <paramref name="validateAllProperties"/> is <see langword="false"/> then only the required annotations
        /// and <see cref="IValidatableObject.Validate"/> are run.
        /// </remarks>
        public static ValidationResults TryValidateObject ( object instance, bool validateAllProperties )
        {
            return TryValidateObject(instance, new ValidationContext(instance, null, null), validateAllProperties);
        }

        /// <summary>Attempts to validate an object.</summary>
        /// <param name="instance">The instance to validate.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>The validation results.</returns>
        /// <remarks>
        /// Only the required annotations and <see cref="IValidatableObject.Validate"/> are run.
        /// </remarks>
        public static ValidationResults TryValidateObject ( object instance, ValidationContext validationContext )
        {
            return TryValidateObject(instance, validationContext, false);
        }

        /// <summary>Attempts to validate an object.</summary>
        /// <param name="instance">The instance to validate.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <param name="validateAllProperties"><see langword="true"/> to validate all property annotations.</param>
        /// <returns>The validation results.</returns>
        /// <remarks>
        /// If <paramref name="validateAllProperties"/> is <see langword="false"/> then only the required annotations
        /// and <see cref="IValidatableObject.Validate"/> are run.
        /// </remarks>
        public static ValidationResults TryValidateObject ( object instance, ValidationContext validationContext, bool validateAllProperties )
        {
            var results = new List<ValidationResult>();

            Validator.TryValidateObject(instance, validationContext, results, validateAllProperties);

            return new ValidationResults(results);
        }
        #endregion
                
        #region ValidateFullObject

        /// <summary>Validates an object.</summary>
        /// <param name="instance">The instance to validate.</param>
        /// <remarks>
        /// Validates the object including all its data annotations.  This differs from <see cref="O:ValidateObject"/> which optionally
        /// validates only the required annotations.
        /// </remarks>
        /// <exception cref="ValidationException">One or more validation exceptions occurred.</exception>
        public static void ValidateFullObject ( object instance )
        {
            ValidateFullObject(instance, new ValidationContext(instance, null, null));
        }

        /// <summary>Validates an object.</summary>
        /// <param name="instance">The instance to validate.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <remarks>
        /// Validates the object including all its data annotations.  This differs from <see cref="O:ValidateObject"/> which optionally
        /// validates only the required annotations.
        /// </remarks>
        /// <exception cref="ValidationException">One or more validation exceptions occurred.</exception>
        public static void ValidateFullObject ( object instance, ValidationContext validationContext )
        {
            Validator.ValidateObject(instance, validationContext, true);
        }
        #endregion

        #region ValidateObject

        /// <summary>Validates an object.</summary>
        /// <param name="instance">The instance to validate.</param>
        /// <remarks>
        /// This method is identical to <see cref="O:TryValidateObject"/> except it uses default values for most of the parameters.
        /// <para />
        /// Only required annotations and the <see cref="IValidatableObject.Validate"/> method are called.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="instance"/> is <see langword="null"/>.</exception>
        /// <exception cref="ValidationException">One or more validation exceptions occurred.</exception>
        public static void ValidateObject ( object instance )
        {
            ValidateObject(instance, false);
        }
        
        /// <summary>Validates an object.</summary>
        /// <param name="instance">The instance to validate.</param>
        /// <param name="validateAllProperties"><see langword="true"/> to validate all annotations.</param>
        /// <remarks>
        /// This method is identical to <see cref="O:TryValidateObject"/> except it uses default values for most of the parameters.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="instance"/> is <see langword="null"/>.</exception>
        /// <exception cref="ValidationException">One or more validation exceptions occurred.</exception>
        public static void ValidateObject ( object instance, bool validateAllProperties )
        {
            Validator.ValidateObject(instance, new ValidationContext(instance, null, null), validateAllProperties);
        }
        #endregion        
    }
}
