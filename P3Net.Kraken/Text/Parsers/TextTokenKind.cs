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
    /// <summary>Defines the different kinds of tokens.</summary>
    public enum TextTokenKind
    {
        /// <summary>Regular text.</summary>
        Text,

        /// <summary>Text that was delimited.</summary>
        DelimitedText,
    }
}

