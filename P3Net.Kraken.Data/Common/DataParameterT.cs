/*
 * Copyright © 2007 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Data;
using System.Linq;

namespace P3Net.Kraken.Data.Common
{
    /// <summary>Represents a typed parameter in a <see cref="DataCommand"/>.</summary>    
    /// <typeparam name="T">The CLR type of the parameter.</typeparam>
    public class DataParameter<T> : DataParameter
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="DataParameter"/> class.</summary>
        /// <param name="name">The name of the parameter.</param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="name"/> is empty.</exception>
        public DataParameter ( string name ) : base(name, DbTypeMapper.ToDbType(typeof(T)), ParameterDirection.Input)
        { }

        /// <summary>Initializes an instance of the <see cref="DataParameter"/> class.</summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="direction">The direction of the parameter.</param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="name"/> is empty.</exception>
        public DataParameter ( string name, ParameterDirection direction ) : base(name, DbTypeMapper.ToDbType(typeof(T)), direction)
        { }
        #endregion

        /// <summary>Gets or sets the typed value.</summary>
        public T TypedValue
        {
            get
            {
                if (Convert.IsDBNull(Value) || (Value == null))
                    return default(T);

                return (T)Convert.ChangeType(Value, typeof(T));
            }

            set
            {
                Value = value;
            }
        }
    }
}
