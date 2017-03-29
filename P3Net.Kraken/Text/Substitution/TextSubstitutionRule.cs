/*
 * Copyright © 2011 Michael Taylor
 * All Rights Reserved
 * 
 * From code copyright Federation of State Medical Boards
 */
#region Imports

using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text;

using P3Net.Kraken.Diagnostics;
#endregion

namespace P3Net.Kraken.Text.Substitution
{    
    /// <summary>Provides a base class for text substitution rules.</summary>
    public abstract class TextSubstitutionRule
    {
        #region Public Members

        /// <summary>Determines if the rule can process the context.</summary>
        /// <param name="context">The context to check.</param>
        /// <returns><see langword="true"/> if the rule applies.</returns>
        public bool CanProcess ( TextSubstitutionContext context )
        {
            return CanProcessCore(context);
        }

        /// <summary>Processes the current context and returns the new text.</summary>
        /// <param name="context">The context to check.</param>
        /// <returns>The updated text.</returns>
        public string Process ( TextSubstitutionContext context )
        {
            return ProcessCore(context);
        }        
        #endregion

        #region Protected Members

        /// <summary>Determines if the rule can process the context.</summary>
        /// <param name="context">The context to check.</param>
        /// <returns><see langword="true"/> if the rule applies.</returns>
        protected abstract bool CanProcessCore ( TextSubstitutionContext context );

        /// <summary>Processes the current context and returns the new text.</summary>
        /// <param name="context">The context to check.</param>
        /// <returns>The updated text.</returns>
        protected abstract string ProcessCore ( TextSubstitutionContext context );
        
        #endregion
    }
}