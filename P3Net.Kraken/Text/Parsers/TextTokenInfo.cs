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
    /// <summary>Represents the information returned by the parser for each token.</summary>    
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")]
    [ExcludeFromCodeCoverage]
    public struct TextTokenInfo
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="TextTokenInfo"/> structure.</summary>
        /// <param name="kind">The type of token.</param>
        /// <param name="text">The token text.</param>
        /// <param name="position">The position within the text where the token appears.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="position"/> is less than zero.</exception>
        public TextTokenInfo ( TextTokenKind kind, string text, int position )
            : this()
        {
            Verify.Argument("position", position).IsGreaterThanOrEqualToZero();

            Kind = kind;
            OriginalText = text ?? "";
            Position = position;
        }
        #endregion

        #region Public Members

        /// <summary>Gets the type of token.</summary>
        public TextTokenKind Kind { get; private set; }

        /// <summary>Gets the original text of the token, including any spaces, but without the delimiters.</summary>
        public string OriginalText { get; private set; }

        /// <summary>Gets the position within the string where the token starts.</summary>
        /// <remarks>
        /// For delimited text the position indicates where the delimiter starts.
        /// </remarks>
        public int Position { get; private set; }

        /// <summary>Gets the text of the token with the text trimmed.</summary>
        public string Text
        {
            get { return OriginalText.Trim(); }
        }

        #endregion
    }
}

