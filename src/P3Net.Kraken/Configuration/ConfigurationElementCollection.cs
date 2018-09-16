/*
 * Copyright © 2017 P3Net
 * All Rights Reserved
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace P3Net.Kraken.Configuration
{
    /// <summary>Provides a generic collection for configuration elements.</summary>
    /// <typeparam name="TElement">The type of element.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public abstract class ConfigurationElementCollection<TKey, TElement> : ConfigurationElementCollection, ICollection<TElement>
                            where TElement: ConfigurationElement, new()
    {
        #region ICollection Members

        /// <summary>Adds a new element.</summary>
        /// <param name="item"></param>
        public void Add ( TElement item ) => BaseAdd(item);

        /// <summary>Removes all elements.</summary>
        public void Clear () => BaseClear();

        /// <summary>Determines if an element exists.</summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains ( TElement item ) => base.BaseIndexOf(item) >= 0;

        void ICollection<TElement>.CopyTo ( TElement[] array, int arrayIndex ) => base.CopyTo(array, arrayIndex);

        /// <summary>Gets an enumerator.</summary>
        /// <returns></returns>
        public new IEnumerator<TElement> GetEnumerator () => this.OfType<TElement>().GetEnumerator();
        
        /// <summary>Removes an element.</summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove ( TElement item )
        {
            var index = BaseIndexOf(item);
            if (index >= 0)
            {
                BaseRemoveAt(index);
                return true;
            };

            return false;
        }

        /// <summary>Removes an element.</summary>
        /// <param name="key"></param>
        public void Remove ( TKey key ) => BaseRemove(key);

        bool ICollection<TElement>.IsReadOnly => this.IsReadOnly();
        
        /// <summary>Gets an element by its key.</summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TElement this[TKey key] => BaseGet(key) as TElement;
        #endregion

        /// <summary>Creates a new element.</summary>
        /// <returns>The new element.</returns>
        protected override ConfigurationElement CreateNewElement () => new TElement();

        /// <summary>Gets the key of an element.</summary>
        /// <param name="element">The element.</param>
        /// <returns>The key, if any.</returns>
        protected override object GetElementKey ( ConfigurationElement element )
        {
            var value = element as TElement;
            if (value == null)
                return null;

            return GetElementKey(value);
        }

        /// <summary>Gets the key of an element.</summary>
        /// <param name="element">The element.</param>
        protected abstract TKey GetElementKey ( TElement element );        
    }    
}
