/*
 * Copyright © 2007 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.Data.Common
{
    /// <summary>Represents a parameter in a <see cref="DataCommand"/>.</summary>
    /// <remarks>
    /// Parameter names should not be formatted based upon the database requirements.  The formatting is applied automatically as needed.
    /// </remarks>
    public class DataParameter
    {
        #region Construction

        private DataParameter ()
        {
            IsNullable = true;
            SourceVersion = DataRowVersion.Current;
        }
        
        /// <summary>Initializes an instance of the <see cref="DataParameter"/> class.</summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="name"/> is empty.</exception>
        public DataParameter ( string name, DbType type ) : this(name, type, ParameterDirection.Input)
        {	
        }

        /// <summary>Initializes an instance of the <see cref="DataParameter"/> class.</summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="direction">The direction of the parameter.</param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="name"/> is empty.</exception>
        public DataParameter ( string name, DbType type, ParameterDirection direction ) : this()
        {
            Name = name;
            DbType = type;
            Direction = direction;
        }
        #endregion
                
        /// <summary>Gets the type of the parameter.</summary>
        [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member")]
        public DbType DbType { get; private set; }

        /// <summary>Gets the direction of the parameter.</summary>
        public ParameterDirection Direction { get; private set; }

        /// <summary>Determines if <see cref="Precision"/> has been set.</summary>
        public bool HasPrecision
        {
            get { return m_precision >= 0; }
        }

        /// <summary>Determines if <see cref="Scale"/> has been set.</summary>
        public bool HasScale
        {
            get { return m_scale >= 0; }
        }

        /// <summary>Determines if <see cref="Size"/> has been set.</summary>
        public bool HasSize
        {
            get { return m_size >= 0; }
        }

        /// <summary>Gets or sets whether the parameter can be NULL.</summary>
        public bool IsNullable { get; set; }

        /// <summary>Gets or sets the name of the parameter.</summary>
        /// <exception cref="ArgumentNullException">When setting the property and the value is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">When setting the property and the value is empty.</exception>        
        public string Name
        {
            get { return m_name; }
            set
            {
                Verify.Argument("value", value).IsNotNullOrEmpty();

                m_name = value.Trim();
            }
        }

        /// <summary>Gets or sets the precision of the type.</summary>
        /// <exception cref="ArgumentOutOfRangeException">When setting the property and the precision is greater than what
        /// is supported by the type or less than 0.</exception>
        public int Precision
        {
            get { return (m_precision > 0) ? m_precision : 0; }

            set
            {
                switch (DbType)
                {
                    case DbType.Single: Verify.Argument("value", value).IsBetween(0, 24); break;
                    case DbType.Double: Verify.Argument("value", value).IsBetween(0, 53); break;
                    default: Verify.Argument("value", value).IsBetween(0, 38); break;
                };

                m_precision = value;
            }
        }

        /// <summary>Gets or sets the scale of the type.</summary>
        /// <exception cref="ArgumentOutOfRangeException">When setting the property and the value is &lt; 0.</exception>
        public int Scale
        {
            get { return (m_scale > 0) ? m_scale : 0; }
            set
            {
                Verify.Argument("value", value).IsGreaterThanOrEqualToZero();

                m_scale = value;
            }
        }

        /// <summary>Gets or sets the size of the type.</summary>
        /// <remarks>
        /// The size is only used for binary and string types.  Data is truncated if
        /// it is too large.  The size will only be set either when querying for
        /// parameter information or when explicitly set in code.
        /// <para/>
        /// If size is -1 then it represents the maximal size allowed by the type.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">When setting the property and the value is &lt; 0.</exception>
        public int Size
        {
            get { return m_size; }
            set
            {
                Verify.Argument("value", value).IsGreaterThanOrEqualTo(-1);
                
                m_size = value;
            }
        }

        /// <summary>Gets or sets the command column associated with the parameter.</summary>
        /// <remarks>
        /// The command column is used to map a <see cref="DataSet"/> column to the parameter.
        /// </remarks>
        public string SourceColumn
        {
            get { return m_sourceColumn; }
            set
            {
                m_sourceColumn = (value ?? "").Trim();
            }
        }

        /// <summary>Gets or sets the row version to load when setting the <see cref="Value"/> property.</summary>
        /// <remarks>
        /// This property is used when updating a dataset.
        /// </remarks>
        public DataRowVersion SourceVersion { get; set; }

        /// <summary>Gets or sets the value of the parameter.</summary>
        public object Value
        {
            get { return m_value; }
            set 
            {
                m_value = value;
            }
        }

        #region Methods
        
        /// <summary>Gets a string representation of the class.</summary>
        /// <returns>A string representing the class.</returns>
        public override string ToString ()
        {
            return Name;
        }
        #endregion
                
        #region Private Members

        private string m_name = "";

        //Initialize to null otherwise user's get a "missing parameter" error if they don't specify a value
        private object m_value = DBNull.Value;

        private int m_precision = -1;
        private int m_scale = -1;
        private int m_size = -1;

        private string m_sourceColumn = "";
        
        #endregion
    }
}
