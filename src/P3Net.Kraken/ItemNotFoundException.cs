/*
 * Copyright © 2018 Michael Taylor
 * All Rights Reserved
 * 
 * Portions copyright Federation of State Medical Boards
 */
using System;
using System.Runtime.Serialization;

namespace P3Net.Kraken
{
    /// <summary>Occurs when an item cannot be found.</summary>
    [Serializable]
    public class ItemNotFoundException : Exception
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="ItemNotFoundException"/> class.</summary>
        public ItemNotFoundException () : base("Item not found.")
        {
        }

        /// <summary>Initializes an instance of the <see cref="ItemNotFoundException"/> class.</summary>
        /// <param name="message">The exception message.</param>
        public ItemNotFoundException ( string message ) : base(message)
        {
        }

        /// <summary>Initializes an instance of the <see cref="ItemNotFoundException"/> class.</summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ItemNotFoundException ( string message, Exception innerException ) : base(message, innerException)
        {
        }

        /// <summary>Initializes an instance of the <see cref="ItemNotFoundException"/> class.</summary>
        /// <param name="message">The exception message.</param>
        /// <param name="key">The key of the item.</param>
        public ItemNotFoundException ( string message, object key ) : base(message)
        {
            m_key = (key != null) ? key.ToString() : null;
        }

        /// <summary>Initializes an instance of the <see cref="ItemNotFoundException"/> class.</summary>
        /// <param name="message">The exception message.</param>
        /// <param name="key">The key of the item.</param>
        /// <param name="innerException">The inner exception.</param>
        public ItemNotFoundException ( string message, object key, Exception innerException ) : base(message, innerException)
        {
            m_key = (key != null) ? key.ToString() : null;
        }

        /// <summary>Initializes an instance of the <see cref="ItemNotFoundException"/> class.</summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context.</param>
        protected ItemNotFoundException ( SerializationInfo info, StreamingContext context ) : base(info, context)
        {
            m_key = info.GetString("Key");
        }
        #endregion

        /// <summary>Gets the key of the item, if any.</summary>
        public string Key
        {
            get { return m_key ?? ""; }
        }

        #region Protected Members

        /// <summary>Serializes the object.</summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData ( SerializationInfo info, StreamingContext context )
        {
            base.GetObjectData(info, context);

            info.AddValue("Key", Key);
        }
        #endregion

        #region Private Members

        private readonly string m_key;
        #endregion
    }
}
