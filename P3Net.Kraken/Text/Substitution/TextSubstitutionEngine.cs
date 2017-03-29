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
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

using P3Net.Kraken.Diagnostics;
using P3Net.Kraken.Text.Parsers;
#endregion

namespace P3Net.Kraken.Text.Substitution
{    
    /// <summary>Provides simple string templated expansion support.</summary>
    /// <remarks>
    /// Use the <see cref="O:Substitute"/> method to specify the substitution rules.  Substitution rules are used to match patterns to the 
    /// text to replacement them with.  Use <see cref="Process"/> to apply the substitution rules to an inputValue string.
    /// <para/>
    /// A substitution rule specifies the pattern it matches and the replacement text to use.  In the simplest case the rule maps one-to-one from
    /// one string to another.  More complex rules may use heuristics to determine whether they match a pattern and what the replacement 
    /// obj is.  Rules are processed in the order they are added so the first rule to match a pattern is used to process it.
    /// </remarks>    
    /// <example>
    /// <code lang="C#">
    /// public string GenerateEmail ( string body, Dictionary&lt;string, string&gt; values )
    /// {
    ///    var engine = new TextSubstitutionEngine();
    ///    
    ///    foreach (var obj in values)
    ///       engine.Rules.Substitute(obj.Key).With(obj.Value);
    ///       
    ///    return engine.Process(body);
    /// }
    /// </code>
    /// </example>
    public class TextSubstitutionEngine
    {
        #region Construction

        /// <summary>Creates an instance of the <see cref="TextSubstitutionEngine"/> class.</summary>
        /// <param name="startDelimiter">The start delimiter.</param>
        /// <param name="endDelimiter">The end delimiter.</param>        
        /// <exception cref="ArgumentNullException"><paramref name="startDelimiter"/> or <paramref name="endDelimiter"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="startDelimiter"/> or <paramref name="endDelimiter"/> is empty.</exception>
        public TextSubstitutionEngine ( string startDelimiter, string endDelimiter ) : this(startDelimiter, endDelimiter, StringComparison.CurrentCultureIgnoreCase)
        { }

        /// <summary>Creates an instance of the <see cref="TextSubstitutionEngine"/> class.</summary>
        /// <param name="startDelimiter">The start delimiter.</param>
        /// <param name="endDelimiter">The end delimiter.</param>
        /// <param name="defaultComparison">The default comparison to perform.</param>
        /// <exception cref="ArgumentNullException"><paramref name="startDelimiter"/> or <paramref name="endDelimiter"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="startDelimiter"/> or <paramref name="endDelimiter"/> is empty.</exception>
        public TextSubstitutionEngine ( string startDelimiter, string endDelimiter, StringComparison defaultComparison )
        {
            StartDelimiter = startDelimiter;
            EndDelimiter = endDelimiter;
            DefaultComparison = defaultComparison;

            Rules = new TextSubstitutionRuleCollection();            
        }
        #endregion

        #region Public Members

        #region Attributes
        
        /// <summary>Gets or sets the substitution options.</summary>
        /// <obj>The default is <see cref="StringComparison.CurrentCulture"/>.</obj>
        public StringComparison DefaultComparison
        {
            get { return m_options.Comparison; }
            set { m_options.Comparison = value; }
        }

        /// <summary>Gets or sets the end delimiter.</summary>
        /// <exception cref="ArgumentNullException">When setting the property and the obj is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">When setting the property and the obj is empty.</exception>
        public string EndDelimiter
        {
            get { return m_options.EndDelimiter; }
            set
            {
                Verify.Argument("value", value).IsNotNullOrEmpty();

                m_options.EndDelimiter = value;
            }
        }

        /// <summary>Gets the substitution rules.</summary>
        public TextSubstitutionRuleCollection Rules { get; private set; }

        /// <summary>Gets or sets the starting delimiter.</summary>
        /// <exception cref="ArgumentNullException">When setting the property and the obj is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">When setting the property and the obj is empty.</exception>
        public string StartDelimiter
        {
            get { return m_options.StartDelimiter; }
            set
            {
                Verify.Argument("value", value).IsNotNullOrEmpty();

                m_options.StartDelimiter = value;
            }
        }
        #endregion

        #region Methods

        /// <summary>Processes the inputValue string and returns the string with the substitution rules applied.</summary>
        /// <param name="inputValue">The string to process.</param>
        /// <returns>The processed string.</returns>
        /// <remarks>
        /// If <paramref name="inputValue"/> is <see langword="null"/> or empty then an empty string is returned.
        /// </remarks>
        /// <example>Refer to <see cref="TextSubstitutionEngine"/> for an example.</example>
        public string Process ( string inputValue )
        {
            if (String.IsNullOrWhiteSpace(inputValue))
                return "";
            
            var context = new TextSubstitutionProcessContext(m_options, inputValue);
            BeginProcess(context);
            
            //Enumerate the tokens
            var parser = new TextTokenParser(StartDelimiter, EndDelimiter);            
            foreach (var tokenInfo in parser.Parse(inputValue))
            {
                ProcessToken(context, tokenInfo);                
            };

            return EndProcess(context);
        }        
        #endregion

        #endregion

        #region Protected Members

        #region Types

        /// <summary>Contains the processing context while processing a string.</summary>
        [ExcludeFromCodeCoverage]
        protected sealed class TextSubstitutionProcessContext
        {
            internal TextSubstitutionProcessContext ( TextSubstitutionOptions options, string inputString )
            {
                InputString = inputString;

                Options = options;

                Output = new StringBuilder();
            }

            /// <summary>Gets or sets the string to be processed.</summary>
            public string InputString 
            {
                get { return m_inputString; }
                set { m_inputString = value ?? ""; }
            }

            /// <summary>Gets the substitution options being used.</summary>
            public TextSubstitutionOptions Options { get; private set; }

            /// <summary>Gets the output string.</summary>
            public StringBuilder Output { get; private set; }

            private string m_inputString;
        }
        #endregion

        /// <summary>Begins processing of the inputValue string.</summary>
        /// <param name="context">The processing context.</param>
        /// <remarks>
        /// The default implementation does nothing.  Derived classes can adjust the context before processing begins.
        /// </remarks>
        protected virtual void BeginProcess ( TextSubstitutionProcessContext context )
        { }

        /// <summary>Ends the processing of the output string.</summary>
        /// <param name="context">The processing context.</param>
        /// <returns>The string to return.</returns>
        /// <remarks>
        /// The default implementation returns the final output.
        /// </remarks>
        protected virtual string EndProcess ( TextSubstitutionProcessContext context )
        {
            return context.Output.ToString();
        }
        
        /// <summary>Called when no matching rule can be found for a token.</summary>
        /// <param name="context">The processing context.</param>
        /// <param name="tokenInfo">The token being processed.</param>
        /// <returns>The text to use.</returns>
        /// <remarks>
        /// The default implementation returns the original token text inside the delimiters.
        /// </remarks>
        protected virtual string OnHandleRuleNotFound ( TextSubstitutionProcessContext context, TextTokenInfo tokenInfo )
        {
            return context.Options.StartDelimiter + tokenInfo.OriginalText + context.Options.EndDelimiter;
        }

        /// <summary>Called to process delimited text.</summary>
        /// <param name="context">The processing context.</param>
        /// <param name="tokenInfo">The token being processed.</param>
        /// <remarks>
        /// The default implementation returns the replacement obj from the corresponding substitution rule.
        /// </remarks>
        protected virtual void OnProcessDelimitedText ( TextSubstitutionProcessContext context, TextTokenInfo tokenInfo )
        {
            //Process the template and output whatever results
            var subContext = new TextSubstitutionContext(context.Options, tokenInfo.OriginalText);

            //Find the applicable rule
            var rule = (from r in Rules
                        where r.CanProcess(subContext)
                        select r).FirstOrDefault();

            //Get the new text
            var text = (rule != null) ? rule.Process(subContext) : OnHandleRuleNotFound(context, tokenInfo);

            //Output
            if (!String.IsNullOrEmpty(text))
                context.Output.Append(text);
        }

        /// <summary>Called to process text that is not delimited.</summary>
        /// <param name="context">The processing context.</param>
        /// <param name="tokenInfo">The token being processed.</param>
        /// <remarks>
        /// The default implementation returns the original text.
        /// </remarks>
        protected virtual void OnProcessText ( TextSubstitutionProcessContext context, TextTokenInfo tokenInfo )
        {
            //Just append to the output
            context.Output.Append(tokenInfo.OriginalText);             
        }        
        #endregion

        #region Private Members
        
        private void ProcessToken ( TextSubstitutionProcessContext context, TextTokenInfo tokenInfo )
        {
            switch (tokenInfo.Kind)
            {                
                case TextTokenKind.Text: OnProcessText(context, tokenInfo); break;                                
                case TextTokenKind.DelimitedText: OnProcessDelimitedText(context, tokenInfo); break;
            };
        }

        private TextSubstitutionOptions m_options = new TextSubstitutionOptions();
        #endregion
    }
}
