/*
 * Copyright © 2013 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace P3Net.Kraken.Reflection
{
    /// <summary>Base class for implementing a type to string converter.</summary>
    public abstract class TypeNameProvider
    {
        /// <summary>Gets or sets whether the name should include the namespace.</summary>
        public bool IncludeNamespace { get; set; }

        /// <summary>Gets the language-friendly name of a type.</summary>
        /// <param name="type">The type to get the name of.</param>
        /// <returns>The friendly name.</returns>
        public string GetTypeName ( Type type )
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return GetTypeNameCore(type);
        }

        #region Protected Members

        /// <summary>Gets the language-friendly name of a type.</summary>
        /// <param name="type">The type to get the name of.</param>
        /// <returns>The friendly name.</returns>
        protected virtual string GetTypeNameCore ( Type type )
        {
            //Special types
            if (type.IsPointer)
                return ProcessPointerType(type);
            if (type.IsByRef)
                return ProcessByRefType(type);

            //Arrays
            if (type.IsArray)
                return ProcessArrayType(type);

            //Closed generic types
            if (type.IsGenericType && !type.IsGenericTypeDefinition)
                return ProcessClosedGenericType(type);

            return ProcessSimpleType(type);
        }
        
        /// <summary>Processes an array type.</summary>
        /// <param name="type">The type to process.</param>
        /// <returns>The friendly name.</returns>
        protected virtual string ProcessArrayType ( Type type )
        {
            //Multidimensional arrays are rectangular so they follow the form [,]
            //Jagged arrays come across as arrays of arrays they are single dimensional to us
            var elementType = type.GetElementType();
            var dimensions = type.GetArrayRank();

            return FormatArrayType(elementType, dimensions);
        }

        /// <summary>Processes a ByRef type.</summary>
        /// <param name="type">The type to process.</param>
        /// <returns>The friendly name.</returns>
        protected virtual string ProcessByRefType ( Type type )
        {
            var refType = type.GetElementType();

            return FormatByRefType(refType);
        }

        /// <summary>Processes a closed generic type.</summary>
        /// <param name="type">The type to process.</param>
        /// <returns>The friendly name.</returns>
        protected virtual string ProcessClosedGenericType ( Type type )
        {            
            var baseType = type.GetGenericTypeDefinition();

            var typeArgs = type.GetGenericArguments();

            //Handle nullable types special
            if (baseType.IsValueType && baseType.Name == "Nullable`1")
                return FormatNullableType(typeArgs[0]);

            return FormatClosedGenericType(baseType, typeArgs);
        }

        /// <summary>Processes a pointer type.</summary>
        /// <param name="type">The type to process.</param>
        /// <returns>The friendly name.</returns>
        protected virtual string ProcessPointerType ( Type type )
        {
            var pointerType = type.GetElementType();

            return FormatPointerType(pointerType);
        }

        /// <summary>Processes a simple type.</summary>
        /// <param name="type">The type to process.</param>
        /// <returns>The friendly name.</returns>
        protected virtual string ProcessSimpleType ( Type type )
        {
            return FormatSimpleType(type);
        }

        /// <summary>Removes any trailing generic suffix on a name.</summary>
        /// <param name="value">The name.</param>
        /// <returns>The trimmed string.</returns>
        protected static string RemoveTrailingGenericSuffix ( string value )
        {
            var tokens = value.Split(new char[] { '`' }, 2);

            return tokens[0];
        }
        #endregion

        #region Format Members

        /// <summary>Formats an array type.</summary>
        /// <param name="elementType">The element type.</param>
        /// <param name="dimensions">The number of dimensions.</param>
        /// <returns>The type name.</returns>
        protected abstract string FormatArrayType ( Type elementType, int dimensions );

        /// <summary>Formats a ByRef type.</summary>
        /// <param name="type">The type.</param>
        /// <returns>The type name.</returns>
        protected abstract string FormatByRefType ( Type type );

        /// <summary>Formats a closed generic type.</summary>
        /// <param name="baseType">The type.</param>
        /// <param name="typeArguments">The array of type arguments.</param>
        /// <returns>The type name.</returns>
        protected abstract string FormatClosedGenericType ( Type baseType, Type[] typeArguments );

        /// <summary>Formats a nullable type.</summary>
        /// <param name="type">The type.</param>
        /// <returns>The type name.</returns>
        protected abstract string FormatNullableType ( Type type );

        /// <summary>Formats a pointer type.</summary>
        /// <param name="type">The type.</param>
        /// <returns>The type name.</returns>
        protected abstract string FormatPointerType ( Type type );

        /// <summary>Formats a simple type.</summary>
        /// <param name="type">The type.</param>
        /// <returns>The type name.</returns>
        /// <remarks>
        /// The default implementation simply returns the type name.
        /// </remarks>
        protected virtual string FormatSimpleType ( Type type )
        {
            return IncludeNamespace ? type.FullName : type.Name;
        }
        #endregion
    }
}
