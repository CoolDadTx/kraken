/*
 * Copyright © 2012 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Diagnostics.CodeAnalysis;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

namespace TaylorSoft.Kraken.UnitTesting.Assertions
{
    /// <summary>Provides assertions for <see cref="Action"/>.</summary>
    public static class ActionAssertions
    {
        /// <summary>Verifies that an action throws an exception.</summary>
        /// <param name="source">The action to run.</param>
        public static void VerifyThrows ( this Action source )
        {
            VerifyThrows<Exception>(source, "Action did not throw exception.");
        }
        
        /// <summary>Verifies that an action throws an exception.</summary>
        /// <param name="source">The action to run.</param>
        /// <param name="message">The message to display.</param>
        /// <param name="arguments">The message arguments.</param>
        public static void VerifyThrows ( this Action source, string message, params object[] arguments )
        {
            VerifyThrows<Exception>(source, message, arguments);
        }

        /// <summary>Verifies that an action throws a specific exception.</summary>
        /// <typeparam name="T">The expected exception.</typeparam>
        /// <param name="source">The action to run.</param>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification="Type is for verification.")]
        public static void VerifyThrows<T> ( this Action source ) where T : Exception
        {
            VerifyThrows<T>(source, "Action did not throw exception.");
        }

        /// <summary>Verifies that an action throws a specific exception.</summary>
        /// <typeparam name="T">The expected exception.</typeparam>
        /// <param name="source">The action to run.</param>
        /// <param name="message">The message to display.</param>
        /// <param name="arguments">The message arguments.</param>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Type is for verification.")]
        public static void VerifyThrows<T> ( this Action source, string message, params object[] arguments ) where T : Exception
        {
            try
            {
                source();

                //Have to throw this explicitly because CC won't consider it otherwise
                throw new AssertFailedException(String.Format(message, arguments));
            } catch (AssertFailedException)
            {
                throw;
            } catch (Exception e)
            {
                if (e is T)
                    return;

                //Have to throw this explicitly because CC won't consider it otherwise
                throw new AssertFailedException(String.Format("Action threw exception of type '{0}' but was expected '{1}", e.GetType(), typeof(T)));
            };            
        }        
    }
}
