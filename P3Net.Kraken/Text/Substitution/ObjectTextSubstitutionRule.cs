/*
 * Copyright © 2011 Michael Taylor
 * All Rights Reserved
 * 
 * From code copyright Federation of State Medical Boards
 */
#region Imports

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using P3Net.Kraken.Diagnostics;
#endregion

namespace P3Net.Kraken.Text.Substitution
{
    /// <summary>Provides a text substitution rule that uses an object's properties.</summary>
    /// <typeparam name="T">The type of the object being matched.</typeparam>
    /// <remarks>
    /// This rule maps text that matches the name of a property to the property's value.   Only public properties and fields on the type (or
    /// base types) are allowed.  
    /// </remarks>
    public class ObjectTextSubstitutionRule<T> : TextSubstitutionRule where T : class
    {
        #region Construction
        
        /// <summary>Initializes an instance of the <see cref="ObjectTextSubstitutionRule{T}"/> class.</summary>
        /// <param name="value">The value to use for matching.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public ObjectTextSubstitutionRule ( T value )
        {
            Verify.Argument("value", value).IsNotNull();

            Value = value;
        }
        #endregion

        #region Public Members
                
        /// <summary>Gets or sets the value to use for retrieving values.</summary>
        public T Value { get; private set; }
        
        #endregion

        #region Protected Members

        /// <summary>Determines if the rule can process the context.</summary>
        /// <param name="context">The context to check.</param>
        /// <returns><see langword="true"/> if the rule applies.</returns>
        protected override bool CanProcessCore ( TextSubstitutionContext context )
        {
            EnsureCacheInitialized(context.Options);

            //See if the context exists as a member
            m_member = FindMember(context.Text);
            
            return m_member != null;
        }

        /// <summary>Processes the current context and returns the new text.</summary>
        /// <param name="context">The context to check.</param>
        /// <returns>The updated text.</returns>
        protected override string ProcessCore ( TextSubstitutionContext context )
        {
            EnsureCacheInitialized(context.Options);

            //Just in case somebody calls Process directly we'll ignore the request
            m_member  = m_member ?? FindMember(context.Text);
            if (m_member == null)
                return null;
            
            //Get the string value
            return GetMemberValue(m_member);
        }
        #endregion

        #region Private Members

        [ExcludeFromCodeCoverage]
        private void EnsureCacheInitialized ( TextSubstitutionOptions options )
        {
            if ((m_cache == null) || (m_comparison != options.Comparison))
            {
                m_cache = new Dictionary<string, Func<string>>(StringExtensions.GetComparer(options.Comparison));
                m_comparison = options.Comparison;
            };
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private Func<string> FindMember ( string memberText )
        {
            //Check the cache first
            Func<string> memberFunction = null;
            if (!m_cache.TryGetValue(memberText, out memberFunction))
            {                
                //Ideally we'd use LINQ expressions but LINQ doesn't currently do case insensitve searches so we have to use reflection
                var expression = GetLinqExpressionFromMemberList(memberText);
                if (expression != null)
                {
                    //Make sure the expression returns a string
                    expression = Expression.Call(expression, "ToString", null);
                    memberFunction = Expression.Lambda<Func<string>>(expression).Compile();
                };

                //Cache for later
                m_cache[memberText] = memberFunction;
            };

            return memberFunction;
        }

        private static string GetMemberValue ( Func<string> member )
        {
            try
            {
                return member();
            } catch (NullReferenceException)
            {
                return "";
            };
        }

        private Expression GetLinqExpressionFromMemberList ( string memberText )
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            switch (m_comparison)
            {
                case StringComparison.CurrentCultureIgnoreCase:
                case StringComparison.InvariantCultureIgnoreCase:
                case StringComparison.OrdinalIgnoreCase: flags |= BindingFlags.IgnoreCase; break;
            };

            //Ideally we'd just use LINQ but LINQ is case sensitive so we have to convert the members manually.
            Expression expr = Expression.Constant(Value);
            string memberName;            
            var targetType = typeof(T);
            foreach (var member in memberText.Split('.'))
            {
                //Find the member - start with properties
                var prop = targetType.GetProperty(member, flags);
                if (prop != null)
                {
                    memberName = prop.Name;
                    targetType = prop.PropertyType;
                } else
                {
                    //Try fields
                    var field = targetType.GetField(member, flags);
                    if (field != null)
                    {
                        memberName = field.Name;
                        targetType = field.FieldType;
                    } else  //not found
                        return null;
                };

                //Add to the LINQ expression
                expr = Expression.PropertyOrField(expr, memberName);
            };

            return expr;
        }

        private Dictionary<string, Func<string>> m_cache;
        private StringComparison m_comparison;

        //Transient value used between matching the rule and processing it
        private Func<string> m_member;
        #endregion
    }
}
