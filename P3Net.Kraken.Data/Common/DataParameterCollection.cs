/*
 * Copyright © 2006 Michael Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using P3Net.Kraken.Collections;
using P3Net.Kraken.Diagnostics;
#endregion

namespace P3Net.Kraken.Data.Common
{
    /// <summary>Provides a collection of <see cref="DataParameter"/> objects.</summary>
    public sealed class DataParameterCollection : KeyedCollection<string, DataParameter>
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="DataParameterCollection"/> class.</summary>
        public DataParameterCollection ()
        { /* Do nothing */ }

        /// <summary>Initializes an instance of the <see cref="DataParameterCollection"/> class.</summary>
        /// <param name="items">The initial items to add.</param>
        public DataParameterCollection ( IEnumerable<DataParameter> items )
        {
            if (items != null)
                this.AddRange(items);
        }
        #endregion

        #region Protected Members

        /// <summary>Inserts an item.</summary>
        /// <param name="index">The index of the new item.</param>
        /// <param name="item">The item to add.</param>
        protected override void InsertItem ( int index, DataParameter item )
        {
            Verify.Argument("item", item).IsNotNull();

            base.InsertItem(index, item);
        }

        /// <summary>Sets an item.</summary>
        /// <param name="index">The index of the new item.</param>
        /// <param name="item">The item to add.</param>
        protected override void SetItem ( int index, DataParameter item )
        {
            Verify.Argument("item", item).IsNotNull();

            base.SetItem(index, item);
        }

        /// <summary>Gets the key of an item.</summary>
        /// <param name="item">The item.</param>
        /// <returns>The key.</returns>
        protected override string GetKeyForItem ( DataParameter item )
        {
            return item.Name;
        }
        #endregion
    }
}
