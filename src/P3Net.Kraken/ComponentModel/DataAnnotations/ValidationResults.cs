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
    /// <summary>Provides information about the results of validation.</summary>
    /// <see cref="ObjectValidator"/>
    public class ValidationResults
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="ValidationResults"/> class.</summary>
        /// <param name="results">The results.</param>
        public ValidationResults ( IEnumerable<ValidationResult> results )
        {
            m_results = results ?? Enumerable.Empty<ValidationResult>();
        }
        #endregion

        #region Public Members

        /// <summary>Gets the validation results.</summary>
        public IEnumerable<ValidationResult> Results
        {
            get { return m_results; }
        }

        /// <summary>Determines if the validation was successful.</summary>
        public bool Succeeded
        {
            get { return Results.Count() == 0; }
        }
        #endregion

        #region Boolean Members

        /// <summary>Converts an instance to a boolean value.</summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The value of <see cref="Succeeded"/>.</returns>
        [SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates")]
        [ExcludeFromCodeCoverage]
        public static implicit operator bool ( ValidationResults instance )
        {
            return instance.Succeeded;
        }
        #endregion

        #region Private Members

        private readonly IEnumerable<ValidationResult> m_results;
        #endregion 
    }
}
