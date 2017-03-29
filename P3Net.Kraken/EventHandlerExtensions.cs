/*
 * Copyright © 2009 Michael L. Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using P3Net.Kraken;
#endregion

namespace P3Net.Kraken
{
    /// <summary>Provides extension methods for event handlers.</summary>
    /// <preliminary/>
    [CodeNotAnalyzed]	
    [CodeNotTested]
    public static class EventHandlerExtensions
    {
        #region EventHandler<T>

        /// <summary>Raises an event with the specified arguments.</summary>
        /// <typeparam name="T">The event argument type.</typeparam>
        /// <param name="handler">The event handler to raise.</param>
        /// <param name="sender">The object raising the event.</param>
        /// <param name="arguments">The argument to pass to the handler.</param>
        /// <remarks>
        /// The method verifies the handler is not <see langword="null"/> and then raises the event.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// public class MyClass
        /// {
        ///    public event EventHandler&lt;EventArgs&gt; MyEvent;
        ///    
        ///    protected virtual OnMyEvent ( )
        ///    {
        ///       MyEvent.Raise(this, EventArgs.Empty);
        ///    }
        /// }
        /// </code>
        /// <code lang="VB">
        /// Public Class MyClass
        ///    Public Event MyEvent As EventHandler(Of EventArgs)
        ///    
        ///    Protected CanOverride Sub OnMyEvent ( )
        ///       MyEvent.Raise(Me, EventArgs.Empty)
        ///    End Sub
        /// End Class
        /// </code>
        /// </example>
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        public static void Raise<T> ( this EventHandler<T> handler, object sender, T arguments ) where T : EventArgs
        {			
            if (handler != null)
                handler(sender, arguments);
        }

        /// <summary>Raises an event with the specified arguments.</summary>
        /// <typeparam name="T">The event argument type.</typeparam>
        /// <param name="handler">The event handler to raise.</param>
        /// <param name="sender">The object raising the event.</param>
        /// <param name="result">The function to call to get the argument to pass to the handler, if necessary.</param>
        /// <remarks>
        /// The method verifies the handler is not <see langword="null"/>, calls a function to generate the argument
        /// and then raises the event.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// public class MyClass
        /// {
        ///    public event EventHandler&lt;ComplexEventArgs&gt; MyEvent;
        ///    
        ///    protected virtual OnMyEvent ( )
        ///    {
        ///       MyEvent.Raise(this, () => new ComplexEventArgs() );
        ///    }
        /// }
        /// </code>
        /// <code lang="VB">
        /// Public Class MyClass
        ///    Public Event MyEvent As EventHandler(Of EventArgs)
        ///    
        ///    Protected CanOverride Sub OnMyEvent ( )
        ///       MyEvent.Raise(Me, Function() New ComplexEventArgs() )
        ///    End Sub
        /// End Class
        /// </code>
        /// </example>
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        public static void Raise<T> ( this EventHandler<T> handler, object sender, Func<T> result ) where T : EventArgs
        {
            if (handler != null)
                handler(sender, result());
        }
        #endregion				
    }
}
