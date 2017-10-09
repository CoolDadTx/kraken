/*
 * Copyright © 2017 P3Net
 * All Rights Reserved
 * 
 * Portions copyright Federation of State Medical Boards
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken
{
    /// <summary>Provides extensions for <see cref="Exception"/>.</summary>
    /// <remarks>
    /// This class adds the ability to register framework exceptions.  Framework exceptions are
    /// exceptions that are used by the framework infrastructure but generally do not contain
    /// enough information to be useful to developers.  Often a framework exception contains
    /// an inner exception with the actual information.  By using <see cref="GetRootException"/> a
    /// developer can skip over the framework exceptions to get to the underlying issue.  The 
    /// following exceptions are automatically registered as framework exceptions.
    /// <list type="bullet">
    ///    <item><see cref="TargetInvocationException"/></item>
    ///    <item><see cref="AggregateException"/></item>
    /// </list>	
    /// </remarks>
    /// <preliminary />
    public static class Exceptions
    {
        #region Construction

        static Exceptions ()
        {
            s_frameworkExceptions.Add(typeof(TargetInvocationException));
            s_frameworkExceptions.Add(typeof(AggregateException));
        }
        #endregion

        /// <summary>Builds a string of messages for an exception and its children.</summary>
        /// <param name="source">The source.</param>
        /// <param name="delimiter">The delimiter to use between exception messages.</param>
        /// <param name="skipRootExceptions"><see langword="true"/> to skip root exceptions.</param>
        /// <returns>The list of exception messages.</returns>
        public static string BuildMessage ( this Exception source, string delimiter = ", ", bool skipRootExceptions = true )
        {
            //Build the list of exceptions to analyze
            var exceptions = Exceptions.EnumerateExceptionChain(source, skipRootExceptions);
            if (exceptions.Count() == 1)
                return exceptions.First().Message;

            return String.Join(delimiter, exceptions.Select(e => e.Message));
        }

        /// <summary>Enumerates the exception chain.</summary>
        /// <param name="source">The source.</param>
        /// <param name="skipRootExceptions"><see langword="true"/> to not report root exceptions.</param>
        /// <returns>The list of exceptions including any inner and aggregate children.</returns>
        public static IEnumerable<Exception> EnumerateExceptionChain ( this Exception source, bool skipRootExceptions = true )
        {
            if (source != null)
            {                
                if (source is AggregateException agg)
                {
                    //Return the aggregate exception if not skipping exceptions
                    if (!skipRootExceptions)
                        yield return source;

                    //For each nested exception
                    foreach (var child in agg.InnerExceptions)
                    {
                        //Return the exception chain from there
                        foreach (var result in EnumerateExceptionChain(child, skipRootExceptions))
                            yield return result;
                    };
                } else
                {
                    //Get the root exception if necessary
                    if (skipRootExceptions)
                        source = source.GetRootException();

                    //Return this exception
                    yield return source;

                    //Enumerate any child exceptions and enumerate their chains
                    if (source.InnerException != null)
                        foreach (var result in EnumerateExceptionChain(source.InnerException, skipRootExceptions))
                            yield return result;
                };
            };
        }

        /// <summary>Gets the Log Id associated with an exception, if any.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The Log Id, if any.</returns>
        /// <remarks>
        /// The Log Id can be set by the logging infrastructure. It can be used to correlate a log entry with the 
        /// error that caused it.
        /// </remarks>
        public static Guid GetLogId ( this Exception source )
        {
            var data = source?.Data;

            if (data.Contains(s_logIdProperty))
                return (Guid)data[s_logIdProperty];

            return Guid.Empty;
        }

        /// <summary>Gets the root exception.</summary>
        /// <param name="value">The value to check.</param>
        /// <returns>The root exception.</returns>
        /// <remarks>
        /// The root exception is considered to be the first exception that is not
        /// a registered framework exception.  If all exceptions are registered then
        /// the original value is returned.
        /// </remarks>
        public static Exception GetRootException ( this Exception value )
        {
            var root = value;

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

        /// <summary>Sets the Log Id associated with an exception.</summary>
        /// <param name="source">The source.</param>
        /// <param name="logId">The log ID.</param>
        /// <remarks>
        /// The Log Id can be set by the logging infrastructure. It can be used to correlate a log entry with the 
        /// error that caused it.
        /// </remarks>
        public static void SetLogId ( this Exception source, Guid logId )
        {
            source.Data[s_logIdProperty] = logId;
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

        /// <summary>Determines if the given exception type is registered.</summary>
        /// <typeparam name="T">The exception type.</typeparam>
        /// <returns><see langword="true"/> if it is registered or <see langword="false"/> otherwise.</returns>        
        public static bool IsFrameworkExceptionRegistered<T> () where T : Exception
        {
            lock (s_frameworkExceptions)
            {
                return s_frameworkExceptions.Contains(typeof(T));
            };
        }

        /// <summary>Registers an exception as a framework exception.</summary>
        /// <param name="exceptionType">The exception to register.</param>
        /// <remarks>
        /// Framework exceptions are skipped by <see cref="GetRootException"/>.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="exceptionType"/> is <see langword="null"/>.</exception>
        public static void RegisterFrameworkException ( Type exceptionType )
        {
            Verify.Argument(nameof(exceptionType)).WithValue(exceptionType).IsNotNull();

            lock (s_frameworkExceptions)
            {
                if (!s_frameworkExceptions.Contains(exceptionType))
                    s_frameworkExceptions.Add(exceptionType);
            };
        }

        /// <summary>Registers an exception as a framework exception.</summary>
        /// <typeparam name="T">The exception to register.</typeparam>
        /// <remarks>
        /// Framework exceptions are skipped by <see cref="GetRootException"/>.
        /// </remarks>        
        public static void RegisterFrameworkException<T> () where T : Exception
        {
            lock (s_frameworkExceptions)
            {
                var exceptionType = typeof(T);
                if (!s_frameworkExceptions.Contains(exceptionType))
                    s_frameworkExceptions.Add(exceptionType);
            };
        }

        #region Private Members

        private static List<Type> s_frameworkExceptions = new List<Type>();

        private const string s_logIdProperty = "P3Net.LogId";
        #endregion
    }
}
