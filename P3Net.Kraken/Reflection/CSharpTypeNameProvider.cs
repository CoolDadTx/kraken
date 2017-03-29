/*
 * Copyright © 2013 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace P3Net.Kraken.Reflection
{
    /// <summary>Provides an implementation of <see cref="TypeNameProvider"/> for C#.</summary>
    public class CSharpTypeNameProvider : TypeNameProvider
    {
        #region Construction

        static CSharpTypeNameProvider()
        {
            s_aliasMappings = new Dictionary<Type, string>() {          
                                    { typeof(void), "void" },
                                    { typeof(char), "char" },  
                                    { typeof(string), "string" },  
                                    { typeof(bool), "bool" },
                                    { typeof(object), "object" },

                                    { typeof(float), "float" },                    
                                    { typeof(double), "double" },                    
                                    { typeof(decimal), "decimal" },                    

                                    { typeof(sbyte), "sbyte" },                    
                                    { typeof(short), "short" },                    
                                    { typeof(int), "int" },      
                                    { typeof(long), "long" },      
                                    { typeof(byte), "byte" },                    
                                    { typeof(ushort), "ushort" },                    
                                    { typeof(uint), "uint" },      
                                    { typeof(ulong), "ulong" },    
                                };
        }
        #endregion  

        #region Protected Members

        /// <summary>Formats an array type.</summary>
        /// <param name="elementType">The element type.</param>
        /// <param name="dimensions">The number of dimensions.</param>
        /// <returns>The type name.</returns>
        protected override string FormatArrayType ( Type elementType, int dimensions )
        {
            //Format => Type[,,,]           
            return String.Format("{0}[{1}]", GetTypeName(elementType), new string(',', dimensions - 1));
        }

        /// <summary>Formats a ByRef type.</summary>
        /// <param name="elementType">The element type.</param>
        /// <returns>The type name.</returns>
        protected override string FormatByRefType ( Type elementType )
        {
            //C# doesn't have a byref syntax for general types
            return GetTypeName(elementType);
        }

        /// <summary>Formats a closed generic type.</summary>
        /// <param name="baseType">The type.</param>
        /// <param name="typeArguments">The array of type arguments.</param>
        /// <returns>The type name.</returns>
        protected override string FormatClosedGenericType(Type baseType, Type[] typeArguments)
        {
            //Get the type arguments            
            var argStrings = String.Join(", ", from a in typeArguments select GetTypeName(a));

            //Format => Type<arg1, arg2, ...>
            return String.Format("{0}<{1}>", RemoveTrailingGenericSuffix(GetTypeName(baseType)), argStrings);
        }

        /// <summary>Formats a nullable type.</summary>
        /// <param name="type">The type.</param>
        /// <returns>The type name.</returns>
        protected override string FormatNullableType ( Type type )
        {
            //Format => Type?   
            return GetTypeName(type) + "?";
        }

        /// <summary>Formats a pointer type.</summary>
        /// <param name="elementType">The element type.</param>
        /// <returns>The type name.</returns>
        protected override string FormatPointerType ( Type elementType )
        {
            //Format => Type*
            return GetTypeName(elementType) + "*";
        }

        /// <summary>Processes a simple type.</summary>
        /// <param name="type">The type.</param>
        /// <returns>The type name.</returns>
        protected override string ProcessSimpleType ( Type type )
        {
            string alias;
            if (s_aliasMappings.TryGetValue(type, out alias))
                return alias;

            return FormatSimpleType(type);
        }
        #endregion

        #region Private Members

        private static readonly Dictionary<Type, string> s_aliasMappings;
        #endregion
    }
}
