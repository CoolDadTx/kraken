/*
 * Copyright © 2004 Michael Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

using P3Net.Kraken.Diagnostics;
#endregion

namespace P3Net.Kraken.Reflection
{
    /// <summary>Provides extension methods for custom attribute providers.</summary>    
    public static class CustomAttributeProviderExtensions
    {
        #region GetAttribute
                
        /// <summary>Gets the specified attribute from the source.</summary>
        /// <typeparam name="T">The source of the attribute to retrieve.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>The associated attribute if it exists or <see langword="null"/> otherwise.</returns>
        /// <remarks>
        /// Only attributes supplied by the source are included.  If multiple attributes are defined then only the first one is returned.
        /// </remarks>
        /// <example>Refer to <see cref="GetAttribute{T}(ICustomAttributeProvider,bool)"/> for an example.</example>
        /// <seealso cref="O:GetAttributes" />
        /// <seealso cref="O:HasAttribute" />
        public static T GetAttribute<T> ( this ICustomAttributeProvider source ) where T : Attribute
        {
            return GetAttribute<T>(source, false);
        }

        /// <summary>Gets the specified attribute from the source.</summary>
        /// <typeparam name="T">The source of the attribute to retrieve.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="includeInherited"><see langword="true"/> to include inherited attributes.</param>
        /// <remarks>
        /// If multiple attributes are defined then only the first one is returned.
        /// </remarks>
        /// <returns>The associated attribute if it exists or <see langword="null"/> otherwise.</returns>
        /// <example>
        /// <code lang="C#">
        /// public string GetProduct ( )
        /// {
        ///    var assembly = Assembly.GetEntryAssembly();
        ///    var attr = assembly.GetAttribute&lt;AssemblyProductAttribute&gt;();
        ///    
        ///    return (attr != null) ? attr.Product : "";
        /// }
        /// </code>
        /// <code lang="VB">
        /// Function GetProduct ( ) As String
        /// 
        ///    Dim assembly As Assembly = Assembly.GetEntryAssembly()
        ///    Dim attr As AssemblyProductAttribute = assembly.GetAttribute(Of AssemblyProductAttribute)()
        ///    
        ///    If attr Is Not Nothing Then
        ///       Return attr.Product
        ///    Else
        ///       Return ""
        ///    End If
        /// End Function
        /// </code>
        /// </example>
        /// <seealso cref="O:GetAttributes" />
        /// <seealso cref="O:HasAttribute" />
        public static T GetAttribute<T> ( this ICustomAttributeProvider source, bool includeInherited ) where T : Attribute
        {
            return (from a in source.GetCustomAttributes(typeof(T), includeInherited)
                    select a).Cast<T>().FirstOrDefault();       
        }                     
        #endregion
        
        #region GetAttributes
                
        /// <summary>Gets the specified attributes from the source.</summary>
        /// <typeparam name="T">The source of the attribute to retrieve.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>The associated attributes or an empty array.</returns>
        /// <remarks>
        /// Only attributes supplied by the source are included.
        /// </remarks>
        /// <example>Refer to <see cref="GetAttributes{T}(ICustomAttributeProvider,bool)"/> for an example.</example>
        /// <seealso cref="O:GetAttribute" />
        public static T[] GetAttributes<T> ( this ICustomAttributeProvider source ) where T : Attribute
        {
            return GetAttributes<T>(source, false);
        }

        /// <summary>Gets the specified attributes from the source.</summary>
        /// <typeparam name="T">The source of the attribute to retrieve.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="includeInherited"><see langword="true"/> to include inherited attributes.</param>
        /// <returns>The associated attributes or an empty array.</returns>
        /// <example>
        /// <code lang="C#">
        /// public string[] GetXamlNamespaces ( Assembly assembly )
        /// {
        ///    var attrs = assembly.GetAttributes&lt;XmlnsDefinitionAttribute&gt;();
        ///    
        ///    return (from a in attrs
        ///               select a.XmlNamespace).ToArray();
        /// }
        /// </code>
        /// <code lang="VB">
        /// Function GetXamlNamespaces ( assembly As Assembly ) as String()
        /// 
        ///    Dim attrs As XmlnsDefinitionAttribute() = assembly.GetAttributes(Of XmlnsDefinitionAttribute)()
        ///    
        ///    Return (From a In attrs Select a.XmlNamespace).ToArray()
        /// End Function
        /// </code>
        /// </example>
        /// <seealso cref="O:GetAttribute" />        
        public static T[] GetAttributes<T> ( this ICustomAttributeProvider source, bool includeInherited ) where T : Attribute
        {
            return (from a in source.GetCustomAttributes(typeof(T), includeInherited)
                    select a).Cast<T>().ToArray();
        }                     
        #endregion
        
        #region HasAttribute
                
        /// <summary>Determines if the source has the given attribute.</summary>
        /// <typeparam name="T">The source of the attribute to check.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns><see langword="true"/> if the source has the attribute or <see langword="false"/> otherwise.</returns>
        /// <remarks>
        /// Only attributes supplied by the source are included.
        /// </remarks>
        /// <example>Refer to <see cref="HasAttribute{T}(ICustomAttributeProvider,bool)"/> for an example.</example>
        /// <seealso cref="O:GetAttribute" />
        /// <seealso cref="O:HasAttribute" />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static bool HasAttribute<T> ( this ICustomAttributeProvider source ) where T : Attribute
        {
            return GetAttribute<T>(source) != null;
        }

        /// <summary>Determines if the source has the given attribute.</summary>
        /// <typeparam name="T">The source of the attribute to check.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="includeInherited"><see langword="true"/> to include inherited attributes.</param>
        /// <returns><see langword="true"/> if the source has the attribute or <see langword="false"/> otherwise.</returns>        
        /// <example>
        /// <code lang="C#">
        /// public string GetProduct ( )
        /// {
        ///    var assembly = Assembly.GetEntryAssembly();
        ///    
        ///    if (assembly.HasAttribute&lt;AssemblyProductAttribute&gt;(false))
        ///       return "";
        ///       
        ///    return assembly.GetAttribute&lt;AssemblyProductAttribute&gt;().Product;
        /// }
        /// </code>
        /// <code lang="VB">
        /// Function GetProduct ( ) As String
        /// 
        ///    Dim assembly As Assembly = Assembly.GetEntryAssembly()
        ///    
        ///    If assembly.HasAttribute(Of AssemblyProductAttribute)(False) Then
        ///       Return ""
        ///     End If
        ///       
        ///    Return assembly.GetAttribute(Of AssemblyProductAttribute)().Product
        /// End Function
        /// </code>
        /// </example>
        /// <seealso cref="O:GetCustomAttribute" />
        /// <seealso cref="O:HasAttribute" />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static bool HasAttribute<T> ( this ICustomAttributeProvider source, bool includeInherited ) where T : Attribute
        {
            return GetAttribute<T>(source, includeInherited) != null;
        }                     
        #endregion
    }
}
