/*
 * Copyright © 2009 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.Text
{
    /// <summary>Provides comparison support for characters.</summary>        
    public abstract class CharComparer : IComparer<char>, IEqualityComparer<char>, IComparer, IEqualityComparer
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="CharComparer"/> class.</summary>
        protected CharComparer ( )
        { }
        #endregion

        #region Attributes

        /// <summary>Gets a comparer for the current culture.</summary>
        public static CharComparer CurrentCulture
        {
            get { return new CultureAwareComparer(CultureInfo.CurrentCulture, false); }
        }

        /// <summary>Gets a comparer for the current culture.</summary>
        public static CharComparer CurrentCultureIgnoreCase
        {
            get { return new CultureAwareComparer(CultureInfo.CurrentCulture, true); }
        }

        /// <summary>Gets a comparer for the invariant culture.</summary>
        public static CharComparer InvariantCulture
        {
            get { return s_invariantCulture; }
        }

        /// <summary>Gets a comparer for the invariant culture.</summary>
        public static CharComparer InvariantCultureIgnoreCase
        {
            get { return s_invariantCultureIgnoreCase; }
        }

        /// <summary>Gets a comparer for ordinal comparisons.</summary>
        public static CharComparer Ordinal
        {
            get { return s_ordinal; }
        }

        /// <summary>Gets a comparer for ordinal comparisons.</summary>
        public static CharComparer OrdinalIgnoreCase
        {
            get { return s_ordinalIgnoreCase; }
        }
        #endregion
       
        /// <summary>Compares two objects for sorting.</summary>
        /// <param name="x">The first object.</param>
        /// <param name="y">The second object.</param>
        /// <returns>The results of the comparison.</returns>
        [ExcludeFromCodeCoverage]
        public abstract int Compare ( char x, char y );

        /// <summary>Compares two objects for equality.</summary>
        /// <param name="x">The first object.</param>
        /// <param name="y">The second object.</param>
        /// <returns><see langword="true"/> if they are equal or <see langword="false"/> otherwise.</returns>
        [ExcludeFromCodeCoverage]
        public new bool Equals ( object x, object y )
        {
            if (x == y)
                return true;
            if ((x == null) || (y == null))
                return false;

            if ((x is char) && (y is char))
                return Equals((char)x, (char)y);

            return x.Equals(y);
        }

        /// <summary>Compares two objects for equality.</summary>
        /// <param name="x">The first object.</param>
        /// <param name="y">The second object.</param>
        /// <returns><see langword="true"/> if they are equal or <see langword="false"/> otherwise.</returns>
        [ExcludeFromCodeCoverage]
        public abstract bool Equals ( char x, char y );

        /// <summary>Gets the character comparer given a string comparison type.</summary>
        /// <param name="comparison">The comparison to perform.</param>
        /// <returns>The equivalent comparer.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="comparison"/> is not a valid enumerated obj.</exception>
        public static CharComparer GetComparer ( StringComparison comparison )
        {
            switch (comparison)
            {
                case StringComparison.CurrentCulture: return CharComparer.CurrentCulture;
                case StringComparison.CurrentCultureIgnoreCase: return CharComparer.CurrentCultureIgnoreCase;
                case StringComparison.InvariantCulture: return CharComparer.InvariantCulture;
                case StringComparison.InvariantCultureIgnoreCase: return CharComparer.InvariantCultureIgnoreCase;

                case StringComparison.Ordinal: return CharComparer.Ordinal;
                case StringComparison.OrdinalIgnoreCase: return CharComparer.OrdinalIgnoreCase;
            };

            throw new ArgumentOutOfRangeException("comparison", "Comparison is invalid.");
        }
                
        /// <summary>Gets the hash code.</summary>
        /// <param name="obj">The object to hash.</param>
        /// <returns>The hash code.</returns>
        [ExcludeFromCodeCoverage]
        public abstract int GetHashCode ( char obj );

        /// <summary>Gets the equivalent <see cref="StringComparer"/> instance.</summary>
        /// <returns>The comparer.</returns>
        public abstract StringComparer ToStringComparer ();

        #region Interface Members

        [ExcludeFromCodeCoverage]
        int IComparer.Compare ( object x, object y )
        {
            if (x == y)
                return 0;
            else if (x == null)
                return -1;
            else if (y == null)
                return 1;

            if ((x is char) && (y is char))
                return Compare((char)x, (char)y);

            IComparable comp = x as IComparable;
            if (comp != null)
                return comp.CompareTo(y);

            throw new ArgumentException("The arguments do not implement IComparable.");
        }

        [ExcludeFromCodeCoverage]
        int IEqualityComparer.GetHashCode ( object value )
        {
            Verify.Argument("value", value).IsNotNull();

            if (value is char)
                return GetHashCode((char)value);

            return value.GetHashCode();
        }
        #endregion

        #region Private Members

        #region Types

        [ExcludeFromCodeCoverage]
        [Serializable]
        private sealed class CultureAwareComparer : CharComparer
        {
            public CultureAwareComparer ( CultureInfo culture, bool ignoreCase )
            {
                m_culture = culture;
                m_ignoreCase = ignoreCase;
                m_options = ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None;
            }

            public override int Compare ( char x, char y )
            {
                return m_culture.CompareInfo.Compare(x.ToString(), y.ToString(), m_options);
            }
                                    
            public override bool Equals ( object obj )            
            {
                CultureAwareComparer comp = obj as CultureAwareComparer;
                if (comp != null) // Don't need to check options because it follows ignoreCase
                    return (m_ignoreCase == comp.m_ignoreCase) && (m_culture == comp.m_culture);

                return false;
            }               

            public override bool Equals ( char x, char y )
            {
                return m_culture.CompareInfo.Compare(x.ToString(), y.ToString(), m_options) == 0;
            }

            public override int GetHashCode ( )
            {
                int code = m_culture.CompareInfo.GetHashCode();
                return m_ignoreCase ? ~code : code;
            }

            public override int GetHashCode ( char value )
            {
                return m_ignoreCase ? Char.ToUpper(value).GetHashCode() : value.GetHashCode();
            }

            public override StringComparer ToStringComparer ()
            {
                return StringComparer.Create(m_culture, m_ignoreCase);
            }

            private CultureInfo m_culture;
            private CompareOptions m_options;
            private bool m_ignoreCase;
        }

        [ExcludeFromCodeCoverage]
        [Serializable]
        private sealed class OrdinalComparer : CharComparer
        {
            public OrdinalComparer ( bool ignoreCase )
            {
                m_ignoreCase = ignoreCase;
                m_comparer = ignoreCase ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal;
            }

            public override int Compare ( char x, char y )
            {
                //Comparer doesn't always return +- 1 so adjust
                int result = m_comparer.Compare(x.ToString(), y.ToString());
                if (result == 0)
                    return 0;

                return (result < 0) ? -1 : 1;
            }

            public override bool Equals ( object obj )
            {
                OrdinalComparer comp = obj as OrdinalComparer;
                if (comp != null)  //Comparer follows ignoreCase
                    return m_ignoreCase == comp.m_ignoreCase;

                return false;
            }

            public override bool Equals ( char x, char y )
            {
                return m_comparer.Equals(x.ToString(), y.ToString());
            }

            public override int GetHashCode ( )
            {
                return m_ignoreCase.GetHashCode();
            }

            public override int GetHashCode ( char value )
            {
                return m_comparer.GetHashCode(value.ToString());
            }

            public override StringComparer ToStringComparer ()
            {
                return m_comparer;
            }

            private StringComparer m_comparer;
            private bool m_ignoreCase;
        }
        #endregion

        private static readonly CharComparer s_invariantCulture = new CultureAwareComparer(CultureInfo.InvariantCulture, false);
        private static readonly CharComparer s_invariantCultureIgnoreCase = new CultureAwareComparer(CultureInfo.InvariantCulture, true);
        private static readonly CharComparer s_ordinal = new OrdinalComparer(false);
        private static readonly CharComparer s_ordinalIgnoreCase = new OrdinalComparer(true);
        #endregion 
    }
}
