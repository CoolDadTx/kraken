/*
 * Copyright © 2013 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace P3Net.Kraken.Reflection
{
    /// <summary>Provides an implementation of <see cref="TypeNameProvider"/> for Visual Basic.</summary>
    public class VisualBasicTypeNameProvider : TypeNameProvider
    {
        #region Construction

        static VisualBasicTypeNameProvider ()
        {
            //Some types are included so that the namespace is left off when referenced
            s_aliasMappings = new Dictionary<Type, string>() {          
                                    { typeof(void), "" },
                                    { typeof(char), "Char" },                                                        
                                    { typeof(string), "String" },                                                        
                                    { typeof(bool), "Boolean" },                                                        
                                    { typeof(object), "Object" },                                                        

                                    { typeof(float), "Single" },                                                        
                                    { typeof(double), "Double" },                                                        
                                    { typeof(decimal), "Decimal" },                                                        

                                    { typeof(sbyte), "SByte" },                                                        
                                    { typeof(short), "Short" },                                                        
                                    { typeof(int), "Integer" },      
                                    { typeof(long), "Long" },      

                                    { typeof(byte), "Byte" },                                                        
                                    { typeof(ushort), "UShort" },                    
                                    { typeof(uint), "UInteger" },      
                                    { typeof(ulong), "ULong" },    
                                    { typeof(DateTime), "Date" },
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
            //Format => Type(,,)
            return String.Format("{0}({1})", GetTypeName(elementType), new string(',', dimensions - 1));
        }

        /// <summary>Formats a ByRef type.</summary>
        /// <param name="elementType">The element type.</param>
        /// <returns>The type name.</returns>
        protected override string FormatByRefType ( Type elementType )
        {
            //VB doesn't have a byref syntax for general types
            return GetTypeName(elementType);
        }

        /// <summary>Formats a closed generic type.</summary>
        /// <param name="baseType">The type.</param>
        /// <param name="typeArguments">The array of type arguments.</param>
        /// <returns>The type name.</returns>
        protected override string FormatClosedGenericType(Type baseType, Type[] typeArguments)
        {
            //Get the type arguments            
            var argStrings = String.Join(", Of ", from a in typeArguments select GetTypeName(a));

            //Format => Type(Of arg1, Of arg2, ...)
            return String.Format("{0}(Of {1})", RemoveTrailingGenericSuffix(GetTypeName(baseType)), argStrings);
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
            //VB doesn't have a pointer type
            return FormatSimpleType(elementType);
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
