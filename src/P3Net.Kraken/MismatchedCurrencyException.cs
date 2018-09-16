/*
 * Copyright © Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Runtime.Serialization;


namespace P3Net.Kraken
{
    /// <summary>Raised when <see cref="Money"/> currencies do not match.</summary>
    [Serializable]
    public sealed class MismatchedCurrencyException : Exception
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="MismatchedCurrencyException"/> class.</summary>
        public MismatchedCurrencyException () : base("Currency must be the same.")
        { }

        /// <summary>Initializes an instance of the <see cref="MismatchedCurrencyException"/> class.</summary>
        /// <param name="message">The exception message.</param>
        public MismatchedCurrencyException ( string message ) : base(message)
        { }

        /// <summary>Initializes an instance of the <see cref="MismatchedCurrencyException"/> class.</summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public MismatchedCurrencyException ( string message, Exception innerException ) : base(message, innerException)
        { }

        /// <summary>Initializes an instance of the <see cref="MismatchedCurrencyException"/> class.</summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context.</param>
        private MismatchedCurrencyException ( SerializationInfo info, StreamingContext context ) : base(info, context)
        { }
        #endregion
    }
}
