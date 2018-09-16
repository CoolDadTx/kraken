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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

using P3Net.Kraken.Diagnostics;
#endregion

namespace P3Net.Kraken.Text.Parsers
{
    /// <summary>Provides a parser for tokens within text.</summary>
    /// <remarks>
    /// The parser groups text that is delimited separately from text that is not delimited.  The delimiters
    /// are not returned as part of the result.
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// public string[] GetXmlComments ( string input )
    /// {
    ///    var comments = new List&lt;string&gt;();
    ///    
    ///    var parser = new TextTokenParser("&lt;!--", "--&gt;");
    ///    foreach(var token in parser.Parse(input))
    ///    {
    ///       if (token.Kind == TextTokenKind.DelimitedText)
    ///          comments.Add(token.Text);
    ///    };
    ///    
    ///    return comments.ToArray();
    /// }
    /// </code>
    /// </example>
    public sealed class TextTokenParser
    {
        #region Construction
        
        /// <summary>Initializes an instance of the <see cref="TextTokenParser"/> class.</summary>
        /// <param name="startDelimiter">The starting delimiter.</param>
        /// <param name="endDelimiter">The ending delimiter.</param>
        /// <exception cref="ArgumentNullException"><paramref name="startDelimiter"/> or <paramref name="endDelimiter"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="startDelimiter"/> or <paramref name="endDelimiter"/> is empty.</exception>
        public TextTokenParser ( string startDelimiter, string endDelimiter )
        {
            StartDelimiter = startDelimiter;
            EndDelimiter = endDelimiter;
        }
        #endregion

        #region Public Members

        #region Attributes

        /// <summary>Gets or sets the end delimiter.</summary>
        /// <exception cref="ArgumentNullException">When setting the property and the obj is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">When setting the property and the obj is empty.</exception>
        public string EndDelimiter
        {
            get { return m_endDelimiter; }
            set
            {
                Verify.Argument("value", value).IsNotNullOrEmpty();

                m_endDelimiter = value;
            }
        }

        /// <summary>Gets or sets the start delimiter.</summary>
        /// <exception cref="ArgumentNullException">When setting the property and the obj is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">When setting the property and the obj is empty.</exception>
        public string StartDelimiter
        {
            get { return m_startDelimiter; }
            set 
            {
                Verify.Argument("value", value).IsNotNullOrEmpty();

                m_startDelimiter = value;
            }
        }
        #endregion

        #region Methods

        /// <summary>Gets the tokens within the string.</summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>The list of tokens.</returns>
        /// <example>Refer to <see cref="TextTokenParser"/> for an example.</example>
        public IEnumerable<TextTokenInfo> Parse ( string value )
        {
            value = value.ValueOrEmpty();

            int inputLength = value.Length;
            int startDelimiterLength = StartDelimiter.Length;
            int endDelimiterLength = EndDelimiter.Length;
            int currentPos = 0;   

            //Until we run out of string
            while (currentPos < inputLength)
            {
                //Find the next starting delimiter, if any
                int nextDelimiter = value.IndexOf(StartDelimiter, currentPos);
                if (nextDelimiter < 0)
                {
                    //No more delimiters so return the final text - if the logic is right then we'll always have at least some
                    //text to return otherwise we would have exited the loop already
                    var text = value.Substring(currentPos);
                    yield return new TextTokenInfo(TextTokenKind.Text, text, currentPos);                    
                    yield break;
                };
                
                //Find the end of the delimited text - we want to make sure this is actually delimited text so we can return the correct text
                int tempStartAt = nextDelimiter + startDelimiterLength;
                int endOfDelimiter = (tempStartAt < inputLength) ? value.IndexOf(EndDelimiter, tempStartAt) : -1;
                if (endOfDelimiter < 0)
                {
                    //We had a start delimiter with no end so return the remainder of the string as text
                    var text = value.Substring(currentPos);
                    yield return new TextTokenInfo(TextTokenKind.Text, text, currentPos);
                    yield break;
                };
                
                //We found a delimiter so return the non-delimited text first, if any
                if (currentPos != nextDelimiter)
                {
                    var normalText = value.Mid(currentPos, nextDelimiter - 1);
                    yield return new TextTokenInfo(TextTokenKind.Text, normalText, currentPos);
                };

                //Now return the delimited text, note that it could be empty
                var delimitedText = (endOfDelimiter > tempStartAt) ? value.Mid(tempStartAt, endOfDelimiter - 1) : "";
                yield return new TextTokenInfo(TextTokenKind.DelimitedText, delimitedText, nextDelimiter);

                //Update the current position
                currentPos = endOfDelimiter + endDelimiterLength;                
            };
        }    
        #endregion

        #endregion

        #region Private Members

        private string m_endDelimiter;
        private string m_startDelimiter;
        #endregion
    }
}