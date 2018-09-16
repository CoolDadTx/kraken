/*
 * Copyright © 2007 Michael L Taylor
 * All Rights Reserved
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using P3Net.Kraken.Collections;
using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.ComponentModel
{
    /// <summary>Provides a collection of disposable objects.</summary>
    /// <remarks>
    /// This class is designed for cleaning up a group of disposable objects.  When dealing with certain areas of .NET a number of classes must be cleaned up
    /// when they are no longer used, such as WMI.  As the number of objects increase the code becomes more unreadable with nested <b>using</b> statements. 
    /// This class can be used to dispose of all the objects at once using a single <b>using</b> statement rather than nesting them.  Each item in the collection has its 
    /// <see cref="IDisposable.Dispose"/> method called when the collection is disposed.
    /// </remarks>
    public sealed class DisposableCollection : Collection<IDisposable>, IDisposable
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="DisposableCollection"/> class.</summary>
        public DisposableCollection ()
        { /* Do nothing */ }
        
        /// <summary>Initializes an instance of the <see cref="DisposableCollection"/> class.</summary>
        /// <paramref name="items">The items to add to the collection.</paramref>
        /// <remarks>
        /// Any <see langword="null"/> elements in the list are not added.
        /// </remarks>
        public DisposableCollection ( IEnumerable<IDisposable> items )
        {
            if (items != null)
                this.AddRange(items);
        }
        #endregion

        #region Public Members
        
        /// <summary>Clears the collection of items.</summary>
        /// <param name="disposeItems"><see langword="true"/> to dispose of all items in the collection or <see langword="false"/> otherwise.</param>
        /// <example>
        /// <code lang="C#">
        /// static void Main ( string[] args )
        /// {
        ///    using(DisposableCollection coll = new DisposableCollection())
        ///    {
        ///       ...
        /// 
        ///       //Items are disposed
        ///       coll.Clear(true);
        ///    }
        /// }
        /// </code>
        /// <code lang="VB">
        /// Shared Sub Main ( args() As String )
        ///    Using coll As New DisposableCollection()
        ///       ...
        /// 
        ///       'Items are disposed
        ///       coll.Clear(True)
        ///    End Using
        /// End Sub
        /// </code>
        /// </example>
        public void Clear ( bool disposeItems )
        {
            try
            {
                if (!disposeItems)
                    m_suppressDisposalOnRemove = true;

                Clear();
            } finally
            {
                m_suppressDisposalOnRemove = false;
            };
        }

        #region CreateAndAdd

        /// <summary>Creates and adds a disposable instance.</summary>
        /// <typeparam name="T">The type of the instance.</typeparam>
        /// <param name="instance">The instance to add.</param>
        /// <returns>The created instance.</returns>
        /// <remarks>
        /// This method allows for the combination of creating and adding an instance to the collection.
        /// </remarks>
        public T CreateAndAdd<T> ( T instance ) where T : IDisposable
        {
            Add(instance);

            return instance;
        }

        /// <summary>Creates and adds a disposable instance.</summary>
        /// <typeparam name="T">The type of the instance.</typeparam>
        /// <param name="createDelegate">The function to return the new instance.</param>
        /// <returns>The created instance.</returns>
        /// <remarks>
        /// This method allows for the combination of creating and adding an instance to the collection.
        /// </remarks>
        public T CreateAndAdd<T> ( Func<T> createDelegate ) where T : IDisposable
        {
            return CreateAndAdd(createDelegate());
        }
        #endregion
        
        /// <summary>Removes an item from the collection without disposing it.</summary>
        /// <param name="item">The item to be removed.</param>
        /// <returns><see langword="true"/> if the item was removed or <see langword="false"/> if it was not found.</returns>		
        /// <remarks>
        /// This method behaves similar to <see cref="Collection{T}.Remove"/> but it does not dispose of the object after it is removed.
        /// </remarks>
        public bool Detach ( IDisposable item )
        {
            try
            {
                m_suppressDisposalOnRemove = true;

                return Remove(item);
            } finally
            {
                m_suppressDisposalOnRemove = false;
            };
        }

        /// <summary>Disposes of the collection and any children.</summary>
        public void Dispose ( )
        {
            Clear(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Protected Members

        /// <summary>Called when the collection is cleared.</summary>
        protected override void ClearItems ()
        {
            //Unless suppressing of the disposal is enabled then dispose of each item first
            if (!m_suppressDisposalOnRemove)
            {
                foreach (var item in Items)
                {
                    if (item != null)
                        item.Dispose();
                };
            };

            base.ClearItems();
        }

        /// <summary>Removes an item from the collection.</summary>
        /// <param name="index">The zero-based index.</param>
        protected override void RemoveItem ( int index )
        {
            //Dispose of the item being removed unless suppress is enabled
            if (!m_suppressDisposalOnRemove)
            {
                var item = Items[index];
                if (item != null)
                    item.Dispose();
            };

            base.RemoveItem(index);
        }

        /// <summary>Replaces an item with another.</summary>
        /// <param name="index">The zero-based index.</param>
        /// <param name="item">The new item.</param>
        protected override void SetItem ( int index, IDisposable item )
        {
            //Dispose of the original item first
            var oldItem = Items[index];
            if (oldItem != null)
                oldItem.Dispose();

            base.SetItem(index, item);
        }
        #endregion

        #region Private Members

        private bool m_suppressDisposalOnRemove;

        #endregion        
    }
}
