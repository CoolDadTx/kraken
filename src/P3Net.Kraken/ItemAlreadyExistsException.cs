/*
 * Copyright © 2018 Michael Taylor
 * All Rights Reserved
 * 
 * From code copyright Federation of State Medical Boards
 */
using System;
using System.Runtime.Serialization;

namespace P3Net.Kraken
{
    /// <summary>Occurs when an item already exists.</summary>
    [Serializable]
    public class ItemAlreadyExistsException : Exception
    {
        /// <summary>Initializes an instance of the <see cref="ItemAlreadyExistsException"/> class.</summary>
        public ItemAlreadyExistsException () : base("Item already exists.")
        {
        }

        /// <summary>Initializes an instance of the <see cref="ItemAlreadyExistsException"/> class.</summary>
        /// <param name="message">The exception message.</param>
        public ItemAlreadyExistsException ( string message ) : base(message)
        {
        }

        /// <summary>Initializes an instance of the <see cref="ItemAlreadyExistsException"/> class.</summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ItemAlreadyExistsException ( string message, Exception innerException ) : base(message, innerException)
        {
        }

        /// <summary>Initializes an instance of the <see cref="ItemAlreadyExistsException"/> class.</summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context.</param>
        protected ItemAlreadyExistsException ( SerializationInfo info, StreamingContext context ) : base(info, context)
        {
        }
    }
}
