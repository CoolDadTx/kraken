/*
 * Copyright © 2009 Michael L Taylor
 * All Rights Reserved
 * 
 * Portions of this file are © Microsoft from the Composite Application Library
 *
 * $Header: /Kraken/Source/P3Net.Kraken/Core/ExceptionExtensions.cs   1   2009-11-22 14:41:20-06:00   Michael $
 */
#region Imports

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using P3Net.Kraken.Diagnostics;
#endregion

namespace P3Net.Kraken
{
    /// <summary>Provides some extensions for <see cref="Exception"/> objects.</summary>
    /// <remarks>
    /// This class adds the ability to register framework exceptions.  Framework exceptions are
    /// exceptions that are used by the framework infrastructure but generally do not contain
    /// enough information to be useful to developers.  Often a framework exception contains
    /// an inner exception with the actual information.  By using <see cref="GetRootException"/> a
    /// developer can skip over the framework exceptions to get to the underlying issue.  The 
    /// following exceptions are automatically registered as framework exceptions.
    /// <list type="bullet">
    ///    <item><see cref="System.Reflection.TargetInvocationException"/></item>
    /// </list>	
    /// </remarks>
    [CodeNotAnalyzed]
    [CodeNotTested]
    public static class ExceptionExtensions
    {
        #region Construction

        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static ExceptionExtensions ( )
        {
            s_frameworkExceptions.Add(typeof(System.Reflection.TargetInvocationException));
        }
        #endregion

        #region Public Members

        /// <summary>Gets the root exception.</summary>
        /// <param name="value">The value to check.</param>
        /// <returns>The root exception.</returns>
        /// <remarks>
        /// The root exception is considered to be the first exception that is not
        /// a registered framework exception.  If all exceptions are registered then
        /// the original value is returned.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// static void Main ( )
        /// {
        ///    try
        ///    {
        ///       InvokeWebService();
        ///    } catch (Exception e)
        ///    {
        ///       e = e.GetRootException();
        ///       Console.WriteLine("ERROR: {0}", e.Message);
        ///    };
        /// }
        /// </code>
        /// <code lang="VB">
        /// Shared Sub Main ()
        /// 
        ///    Try
        ///       InvokeWebService()
        ///    Catch e As Exception
        ///    
        ///       Console.WriteLine("ERROR: {0}", e.GetRootException())
        ///    End Try 
        /// End Sub
        /// </code>
        /// </example>
        public static Exception GetRootException ( this Exception value )
        {
            Exception root = value;

            lock (s_frameworkExceptions)
            {
                while (root != null)
                {
                    if (!s_frameworkExceptions.Contains(root.GetType()))
                        return root;

                    root = root.InnerException;
                };
            };

            return value;
        }

        /// <summary>Determines if the given exception type is registered.</summary>
        /// <param name="exceptionType">The exception to check.</param>
        /// <returns><see langword="true"/> if it is registered or <see langword="false"/> otherwise.</returns>        
        public static bool IsFrameworkExceptionRegistered ( Type exceptionType )
        {
            if (exceptionType == null)
                return false;

            lock (s_frameworkExceptions)
            {
                return s_frameworkExceptions.Contains(exceptionType);
            };
        }

        /// <summary>Registers an exception as a framework exception.</summary>
        /// <param name="exceptionType">The exception to register.</param>
        /// <remarks>
        /// Framework exceptions are skipped by <see cref="GetRootException"/>.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="exceptionType"/> is <see langword="null"/>.</exception>
        /// <example>
        /// <code lang="C#">
        /// using System.IO;
        /// using System.Runtime.Serialization;
        /// using System.Runtime.Serialization.Formatters.Binary;
        /// 
        /// static void Main ( )
        /// {
        ///    ExceptionExtensions.RegisterFrameworkException(typeof(SerializationException));
        ///    try
        ///    {
        ///       BinaryFormatter formatter = new BinaryFormatter();
        ///       using (MemoryStream stream = new MemoryStream())
        ///       {
        ///          formatter.Serialize(stream, DateTime.Now);
        ///       };
        ///    } catch (Exception e)
        ///    {
        ///       Console.WriteLine("ERROR: {0}", e.GetRootException());
        ///    };		
        /// }
        /// </code>
        /// <code lang="VB">
        /// Imports System.IO
        /// Imports System.Runtime.Serialization
        /// Imports System.Runtime.Serialization.Formatters.Binary
        /// 
        /// Shared Sub Main ( )
        /// 
        ///    ExceptionExtensions.RegisterFrameworkException(GetType(SerializationException))
        ///    Try
        ///    
        ///       Dim formatter As New BinaryFormatter()
        ///       Using stream As New MemoryStream()
        ///       
        ///          formatter.Serialize(stream, DateTime.Now)
        ///       End Using
        ///    Catch e As Exception
        ///    
        ///       Console.WriteLine("ERROR: {0}", e.GetRootException())
        ///    End Try		
        /// End Sub
        /// </code>
        /// </example>
        public static void RegisterFrameworkException ( Type exceptionType )
        {
            Verify.Argument("exceptionType", exceptionType).IsNotNull();

            lock(s_frameworkExceptions)
            {
                if (!s_frameworkExceptions.Contains(exceptionType))
                    s_frameworkExceptions.Add(exceptionType);            
            };
        }
        #endregion //Public Members

        #region Private Members

        #region Data
        
        private static List<Type> s_frameworkExceptions = new List<Type>();      
        #endregion

        #endregion //Private Members
    }
}
