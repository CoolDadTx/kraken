/*
 * Copyright © 2011 Michael Taylor
 * All Rights Reserved
 * 
 * From code copyright Federation of State Medical Boards
 */
#region Imports

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using P3Net.Kraken.Collections;
using P3Net.Kraken.Diagnostics;
using P3Net.Kraken.Text.Substitution;
#endregion

namespace P3Net.Kraken.Text.Substitution
{
    /// <summary>Represents a substitution rule where simple text is matched and replaced with simple text.</summary>
    public class SimpleTextSubstitutionRule : TextSubstitutionRule
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="SimpleTextSubstitutionRule"/> class.</summary>
        /// <param name="matchingText">The text to match.</param>
        /// <param name="replacementText">The replacement text.</param>
        /// <remarks>
        /// If <paramref name="matchingText"/> is <see langword="null"/> or empty then the rule never matches.  If 
        /// <paramref name="replacementText"/> is <see langword="null"/> or empty then the rule removes the text.
        /// </remarks>
        public SimpleTextSubstitutionRule ( string matchingText, string replacementText )
        {
            MatchingText = matchingText ?? "";
            ReplacementText = replacementText ?? "";
        }
        #endregion

        #region Public Members

        /// <summary>Gets the text matching rule.</summary>
        /// <remarks>
        /// If the property is <see langword="null"/> or empty then the rule never matches.
        /// </remarks>
        public string MatchingText { get; private set; }

        /// <summary>Gets the text replacement rule.</summary>
        /// <remarks>
        /// If the property is <see langword="null"/> or empty then the rule removes the text.
        /// </remarks>
        public string ReplacementText { get; private set; }
        #endregion

        #region Protected Members

        /// <summary>Determines if the rule can process the context.</summary>
        /// <param name="context">The context to check.</param>
        /// <returns><see langword="true"/> if the rule applies.</returns>
        protected override bool CanProcessCore ( TextSubstitutionContext context )
        {
            return String.Compare(context.Text, MatchingText, context.Options.Comparison) == 0;
        }

        /// <summary>Determines if a rule matches the context.</summary>
        /// <param name="context">The context to check.</param>
        /// <returns>The updated text.</returns>
        protected override string ProcessCore ( TextSubstitutionContext context )
        {
            return ReplacementText;
        }
        #endregion
    }
}

