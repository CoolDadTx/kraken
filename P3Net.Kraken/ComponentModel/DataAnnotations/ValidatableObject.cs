/*
 * Copyright © 2012 Michael Taylor
 * All Rights Reserved
 * 
 * From code copyright Federation of State Medical Boards
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace P3Net.Kraken.ComponentModel.DataAnnotations
{
    /// <summary>Provides a base implementation of <see cref="IValidatableObject"/>.</summary>
    public abstract class ValidatableObject : IValidatableObject
    {
        #region Construction
        
        /// <summary>Initializes an instance of the <see cref="ValidatableObject"/> class.</summary>
        protected ValidatableObject ()
        { }
        #endregion

        #region Public Members

        /// <summary>Validates the object.</summary>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>The validation results.</returns>
        public IEnumerable<ValidationResult> Validate ( ValidationContext validationContext )
        {
            return ValidateCore(validationContext) ?? Enumerable.Empty<ValidationResult>();
        }
        #endregion

        #region Protected Members

        /// <summary>Validates the object.</summary>
        /// <param name="context">The validation context.</param>
        /// <remarks>
        /// The base implementation does nothing.
        /// </remarks>        
        protected virtual IEnumerable<ValidationResult> ValidateCore ( ValidationContext context )
        {
            return Enumerable.Empty<ValidationResult>();
        }
        #endregion
    }    
}
