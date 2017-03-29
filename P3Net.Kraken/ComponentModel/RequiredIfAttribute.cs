/*
 * Copyright © 2012 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.ComponentModel.DataAnnotations
{
    /// <summary>Validates that a value is set if another property is <see langword="true"/>.</summary>
    /// <remarks>
    /// The associated element is required only if a property evaluates to <see langword="true"/>.
    /// <para />
    /// When using this attribute on an MVC model note that model properties are validated when they are set.  Ensure the
    /// conditional property appears before the property being validated.
    /// </remarks>
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification="Is extensible.")]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]    
    public class RequiredIfAttribute : RequiredAttribute
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="RequiredIfAttribute"/> class.</summary>
        /// <param name="propertyName">The property name to check.</param>
        /// <exception cref="ArgumentNullException"><paramref name="propertyName"/> is <see langword="null"/>.</exception>
        public RequiredIfAttribute ( string propertyName ) : base()
        {
            Verify.Argument("propertyName", propertyName).IsNotNull();

            PropertyName = propertyName;
        }
        #endregion

        #region Public Members

        /// <summary>Gets the property name to evaluate.</summary>
        public string PropertyName { get; private set; }

        /// <summary>Gets or sets the optional value to compare against.</summary>
        /// <value>If not set then the property is compared against <see langword="true"/>.</value>
        public object Value { get; set; }
        #endregion

        #region Protected Members

        /// <summary>Performs the validation.</summary>
        /// <param name="value">The value to apply the validation to.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>The validation result.</returns>
        protected override ValidationResult IsValid ( object value, ValidationContext validationContext )
        {
            //Get the property value
            var property = validationContext.ObjectType.GetProperty(PropertyName);
            if (property == null)
                return new ValidationResult(String.Format("Property '{0}' not found.", PropertyName));

            var propertyValue = property.GetValue(validationContext.ObjectInstance, null);
            
            var checkBase = false;
            if (Value != null)
            {
                var convertedValue = Convert.ChangeType(Value, property.PropertyType);                                
                checkBase = Object.Equals(propertyValue, convertedValue);
            } else
                checkBase = Convert.ToBoolean(propertyValue);            

            if (checkBase)            
                return base.IsValid(value, validationContext);

            return ValidationResult.Success;
        }
        #endregion
    }
}
