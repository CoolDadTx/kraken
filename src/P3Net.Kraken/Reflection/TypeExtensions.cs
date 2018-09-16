/*
 * Copyright © 2004 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.Reflection
{
    /// <summary>Provides extension methods for types.</summary>
    public static class TypeExtensions
    {
        #region GetFriendlyName

        /// <summary>Gets the C# friendly name of a type, if any.</summary>
        /// <param name="source">The type.</param>
        /// <returns>The type name.</returns>
        /// <remarks>
        /// This method uses the <see cref="CSharpTypeNameProvider"/> for getting the name.
        /// </remarks>
        public static string GetFriendlyName ( this Type source )
        {
            return GetFriendlyName(source, new CSharpTypeNameProvider());
        }

        /// <summary>Gets the C# friendly name of a type, if any.</summary>
        /// <param name="source">The type.</param>
        /// <param name="includeNamespace"><see langword="true"/> to include the namespace in the type.</param>
        /// <returns>The type name.</returns>
        /// <remarks>
        /// This method uses the <see cref="CSharpTypeNameProvider"/> for getting the name.
        /// </remarks>
        public static string GetFriendlyName ( this Type source, bool includeNamespace )
        {
            return GetFriendlyName(source, new CSharpTypeNameProvider { IncludeNamespace = includeNamespace });
        }
        
        /// <summary>Gets the C# friendly name of a type, if any.</summary>
        /// <param name="source">The type.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>The type name.</returns>        
        /// <exception cref="ArgumentNullException"><paramref name="provider"/> is <see langword="null"/>.</exception>
        public static string GetFriendlyName ( this Type source, TypeNameProvider provider )
        {
            return provider.GetTypeName(source);
        }
        #endregion

        #region GetMethod

        /// <summary>Gets the method with the given name.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="name">The name of the member.</param>
        /// <param name="ignoreCase"><see langword="true"/> to ignore case.</param>
        /// <remarks>
        /// All methods are searched.
        /// </remarks>
        /// <returns>The method, if found, or <see langword="null"/>  otherwise.</returns>
        /// <seealso cref="O:GetPublicMethod"/>
        /// <seealso cref="O:GetPublicOrProtectedMethod"/>
        public static MethodInfo GetMethod ( this Type source, string name, bool ignoreCase )
        {
            if (String.IsNullOrEmpty(name))
                return null;

            var flags = BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            if (ignoreCase)
                flags |= BindingFlags.IgnoreCase;

            return source.GetMethod(name, flags);
        }

        /// <summary>Gets the public method with the given name.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="name">The name of the member.</param>
        /// <remarks>
        /// Case is sensitive.
        /// </remarks>
        /// <returns>The method, if found, or <see langword="null"/>  otherwise.</returns>
        /// <seealso cref="O:GetPublicOrProtectedMethod"/>
        public static MethodInfo GetPublicMethod ( this Type source, string name)
        {
            return GetPublicMethod(source, name, false);
        }

        /// <summary>Gets the public method with the given name.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="name">The name of the member.</param>
        /// <param name="ignoreCase"><see langword="true"/> to ignore case.</param>
        /// <returns>The method, if found, or <see langword="null"/>  otherwise.</returns>
        /// <seealso cref="O:GetPublicOrProtectedMethod"/>
        public static MethodInfo GetPublicMethod ( this Type source, string name, bool ignoreCase )
        {
            if (String.IsNullOrEmpty(name))
                return null;

            var flags = BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
            if (ignoreCase)
                flags |= BindingFlags.IgnoreCase;
               
            return source.GetMethod(name, flags);
        }

        /// <summary>Gets the public method with the given name.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="name">The name of the member.</param>
        /// <remarks>
        /// Case is sensitive.
        /// </remarks>
        /// <returns>The method, if found, or <see langword="null"/>  otherwise.</returns>
        /// <seealso cref="O:GetPublicMethod"/>
        public static MethodInfo GetPublicOrProtectedMethod ( this Type source, string name )
        {
            return GetPublicOrProtectedMethod(source, name, false);
        }

        /// <summary>Gets the public method with the given name.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="name">The name of the member.</param>
        /// <param name="ignoreCase"><see langword="true"/> to ignore case.</param>
        /// <returns>The method, if found, or <see langword="null"/>  otherwise.</returns>
        /// <seealso cref="O:GetPublicMethod"/>
        public static MethodInfo GetPublicOrProtectedMethod ( this Type source, string name, bool ignoreCase )
        {
            if (String.IsNullOrEmpty(name))
                return null;

            var flags = BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            if (ignoreCase)
                flags |= BindingFlags.IgnoreCase;

            var result = source.GetMethod(name, flags);
            return ((result != null) && (result.IsFamily || result.IsPublic)) ? result : null;
        }
        #endregion

        #region GetProperty

        /// <summary>Gets the property with the given name.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="name">The name of the member.</param>
        /// <param name="ignoreCase"><see langword="true"/> to ignore case.</param>
        /// <remarks>
        /// All properties are searched.
        /// </remarks>
        /// <returns>The property, if found, or <see langword="null"/>  otherwise.</returns>
        /// <seealso cref="O:GetPublicProperty"/>
        /// <seealso cref="O:GetPublicOrProtectedProperty"/>
        public static PropertyInfo GetProperty ( this Type source, string name, bool ignoreCase )
        {
            if (String.IsNullOrEmpty(name))
                return null;

            var flags = BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            if (ignoreCase)
                flags |= BindingFlags.IgnoreCase;

            return source.GetProperty(name, flags);
        }

        /// <summary>Gets the public property with the given name.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="name">The name of the member.</param>
        /// <remarks>
        /// Case is sensitive.
        /// </remarks>
        /// <returns>The property, if found, or <see langword="null"/>  otherwise.</returns>
        /// <seealso cref="O:GetPublicOrProtectedProperty"/>
        public static PropertyInfo GetPublicProperty ( this Type source, string name )
        {
            return GetPublicProperty(source, name, false);
        }

        /// <summary>Gets the public property with the given name.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="name">The name of the member.</param>
        /// <param name="ignoreCase"><see langword="true"/> to ignore case.</param>
        /// <returns>The property, if found, or <see langword="null"/>  otherwise.</returns>
        /// <seealso cref="O:GetPublicOrProtectedProperty"/>
        public static PropertyInfo GetPublicProperty ( this Type source, string name, bool ignoreCase )
        {
            if (String.IsNullOrEmpty(name))
                return null;

            var flags = BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
            if (ignoreCase)
                flags |= BindingFlags.IgnoreCase;

            return source.GetProperty(name, flags);
        }

        /// <summary>Gets the public property with the given name.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="name">The name of the member.</param>
        /// <remarks>
        /// Case is sensitive.
        /// </remarks>
        /// <returns>The property, if found, or <see langword="null"/>  otherwise.</returns>
        /// <seealso cref="O:GetPublicProperty"/>
        public static PropertyInfo GetPublicOrProtectedProperty ( this Type source, string name )
        {
            return GetPublicOrProtectedProperty(source, name, false);
        }

        /// <summary>Gets the public property with the given name.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="name">The name of the member.</param>
        /// <param name="ignoreCase"><see langword="true"/> to ignore case.</param>
        /// <returns>The property, if found, or <see langword="null"/>  otherwise.</returns>
        /// <seealso cref="O:GetPublicProperty"/>
        public static PropertyInfo GetPublicOrProtectedProperty ( this Type source, string name, bool ignoreCase )
        {
            if (String.IsNullOrEmpty(name))
                return null;

            var flags = BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            if (ignoreCase)
                flags |= BindingFlags.IgnoreCase;

            var result = source.GetProperty(name, flags);            
            if (result == null)
                return null;

            //Accessibility is determined by accessors
            var getter = result.GetGetMethod(true);
            if (getter != null)
            {
                if (getter.IsPublic || getter.IsFamily)
                    return result;
            };
            var setter = result.GetSetMethod(true);
            if (setter != null)
            {
                if (setter.IsPublic || setter.IsFamily)
                    return result;
            };

            return null;
        }
        #endregion

        #region ImplementsInterface

        /// <summary>Determines if the given type implements the specified interface.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="interfaceName">The interface to find.</param>
        /// <returns><see langword="true"/> if the interface is implemented or <see langword="false"/> otherwise.</returns>
        /// <remarks>
        /// The interface name is case-insensitive.
        /// </remarks>
        /// <example>
        /// <code lang="C#">		
        ///		public class App : IApplication
        ///		{
        ///			public static void Main ( )
        ///			{
        ///				App app = new App();
        /// 
        ///				if (app.GetType().ImplementsInterface("IApplication"))
        ///					Console.WriteLine("App implements IApplication.");
        ///			}
        ///		}
        /// </code>
        /// </example>
        public static bool ImplementsInterface ( this Type source, string interfaceName )
        {
            return ImplementsInterface(source, interfaceName, true);
        }

        /// <summary>Determines if the given type implements the specified interface.</summary>
        /// <param name="source">The source object.</param>
        /// <param name="interfaceName">The interface to find.</param>
        /// <param name="ignoreCase"><see langword="true"/> to ignore the case.</param>
        /// <returns><see langword="true"/> if the interface is implemented or <see langword="false"/> otherwise.</returns>
        /// <example>
        /// <code lang="C#">
        ///		public class App : IApplication
        ///		{
        ///			public static void Main ( )
        ///			{
        ///				App app = new App();
        /// 
        ///				if (app.GetType().ImplementsInterface("IApplication", true))
        ///					Console.WriteLine("App implements IApplication.");
        ///			}
        ///		}
        /// </code>
        /// </example>
        public static bool ImplementsInterface ( this Type source, string interfaceName, bool ignoreCase )
        {
            if (String.IsNullOrEmpty(interfaceName))
                return false;
            
            return source.GetInterface(interfaceName, ignoreCase) != null;
        }
        #endregion
    }
}
