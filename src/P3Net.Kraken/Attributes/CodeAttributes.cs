/*
 * Copyright © 2005 Michael Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.Diagnostics.CodeAnalysis;

#endregion

namespace P3Net.Kraken
{
    /// <summary>This class represents the base class for code attributes.</summary>
    [AttributeUsage(AttributeTargets.All)]
    [ExcludeFromCodeCoverage]
    public abstract class CodingAttribute : Attribute
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="CodingAttribute"/> class.</summary>
        protected CodingAttribute ( ) : this("")
        { }

        /// <summary>Initializes an instance of the <see cref="CodingAttribute"/> class.</summary>
        /// <param name="message">The message to associate with the attribute.</param>
        protected CodingAttribute ( string message )
        {
            Message = message ?? "";
        }

        #endregion

        #region Public Members
         
        /// <summary>Gets the message associated with the coding attribute.</summary>
        public string Message { get; private set; }
        
        #endregion 
    }

    /// <summary>This attribute identifies a type or member that has not been through code analysis.</summary>
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Constructor|AttributeTargets.Delegate|
                AttributeTargets.Enum|AttributeTargets.Event|AttributeTargets.Interface|AttributeTargets.Method|
                AttributeTargets.Property|AttributeTargets.Struct)]
    [ExcludeFromCodeCoverage]
    public sealed class CodeNotAnalyzedAttribute : CodingAttribute
    {	
        #region Construction

        /// <summary>Initializes an instance of the <see cref="CodeNotAnalyzedAttribute"/> class.</summary>
        public CodeNotAnalyzedAttribute ( ) : base("The code has not been analyzed.")
        { }

        /// <summary>Initializes an instance of the <see cref="CodeNotAnalyzedAttribute"/> class.</summary>
        /// <param name="message">The message to associate with the attribute.</param>
        public CodeNotAnalyzedAttribute(string message) : base(message)
        { }

        #endregion
    }

    /// <summary>This attribute identifies a type or member that has not been completely documented yet.</summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Delegate |
                AttributeTargets.Enum | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Method |
                AttributeTargets.Property | AttributeTargets.Struct)]
    [ExcludeFromCodeCoverage]
    public sealed class CodeNotDocumentedAttribute : CodingAttribute
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="CodeNotDocumentedAttribute"/> class.</summary>
        public CodeNotDocumentedAttribute ( ) : base("The code has not been documented.")
        { }

        /// <summary>Initializes an instance of the <see cref="CodeNotDocumentedAttribute"/> class.</summary>
        /// <param name="message">The message to associate with the attribute.</param>
        public CodeNotDocumentedAttribute ( string message ) : base(message)
        { }

        #endregion
    }

    /// <summary>This attribute identifies a type or member that has not been tested.</summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Delegate |
                AttributeTargets.Enum | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Method |
                AttributeTargets.Property | AttributeTargets.Struct)]
    [ExcludeFromCodeCoverage]
    public sealed class CodeNotTestedAttribute : CodingAttribute
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="CodeNotTestedAttribute"/> class.</summary>
        public CodeNotTestedAttribute ( ) : base("The code has not been tested.")
        { }

        /// <summary>Initializes an instance of the <see cref="CodeNotTestedAttribute"/> class.</summary>
        /// <param name="message">The message to associate with the attribute.</param>
        public CodeNotTestedAttribute ( string message ) : base(message)
        { }

        #endregion
    }
}
