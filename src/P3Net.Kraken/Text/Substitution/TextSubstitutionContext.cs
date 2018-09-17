/*
 * Copyright © 2011 Michael Taylor
 * All Rights Reserved
 * 
 * From code copyright Federation of State Medical Boards
 */
#region Imports

using System;
using System.Diagnostics.CodeAnalysis;
#endregion

namespace P3Net.Kraken.Text.Substitution
{
    /// <summary>Provides contextual information for <see cref="TextSubstitutionEngine"/>.</summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")]
    [ExcludeFromCodeCoverage]
    public struct TextSubstitutionContext
    {
        #region Construction
        
        internal TextSubstitutionContext ( TextSubstitutionOptions options, string originalText ) : this()
        {
            Options = options;

            OriginalText = originalText ?? "";
        }
        #endregion

        #region Public Members

        /// <summary>Gets the full, original delimited text.</summary>
        public string DelimitedText
        {
            get { return Options.StartDelimiter + OriginalText + Options.EndDelimiter; }
        }

        /// <summary>Gets the substitution options.</summary>
        public TextSubstitutionOptions Options { get; private set; }

        /// <summary>Gets the original token text.</summary>
        public string OriginalText { get; private set; }
        
        /// <summary>Gets the token text with any leading and trailing spaces cleaned up.</summary>
        public string Text 
        {
            get { return OriginalText.Trim(); }
        }        
        #endregion
    }

    /// <summary>Represents options for text substitution.</summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")]
    [ExcludeFromCodeCoverage]
    public struct TextSubstitutionOptions
    {
        #region Public Members
        
        /// <summary>Gets or sets the comparison to use.</summary>
        public StringComparison Comparison 
        {
            get { return m_comparison.HasValue ? m_comparison.Value : StringComparison.CurrentCultureIgnoreCase; }
            set { m_comparison = value; }
        }

        /// <summary>Gets or sets the end delimiter.</summary>
        public string EndDelimiter
        {
            get { return m_endDelimiter ?? ""; }
            set { m_endDelimiter = value; }
        }

        /// <summary>Gets or sets the start delimiter.</summary>
        public string StartDelimiter 
        {
            get { return m_startDelimiter ?? ""; }
            set { m_startDelimiter = value; }
        }
        #endregion

        #region Private Members

        private StringComparison? m_comparison;

        private string m_startDelimiter;
        private string m_endDelimiter;
        #endregion
    }
}
