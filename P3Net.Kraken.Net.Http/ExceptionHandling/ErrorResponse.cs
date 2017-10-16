/*
 * Copyright © 2016 Michael Taylor
 * All Rights Reserved
 * 
 * Portions copyright Federation of State Medical Boards
 */
using System;
using System.Collections.Generic;

namespace P3Net.Kraken.Net.Http.ExceptionHandling
{
    /// <summary>Provides a general purpose error object for returning errors from API calls.</summary>
    /// <preliminary />
    public class ErrorResponse
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="ErrorResponse"/> class.</summary>
        public ErrorResponse ()
        { }

        /// <summary>Initializes an instance of the <see cref="ErrorResponse"/> class.</summary>
        /// <param name="exception">The exception to wrap.</param>
        public ErrorResponse ( Exception exception )
        {
            if (exception != null)
            {
                Message = exception.Message;
                Code = exception.GetType().Name;
                LogId = exception.GetLogId();
                
                if (exception.InnerException != null)
                    InnerError = new ErrorResponse(exception.InnerException);
            };
        }
        #endregion

        // -- These properties are in the order we'd like to see them in the JSON

		/// <summary>Gets or sets the error code.</summary>
	    public string Code { get; set; }
        /// <summary>Gets or sets the error message.</summary>
        public string Message { get; set; }        

		/// <summary>Gets or sets the target of the error, if any.</summary>
        public string Target { get; set; }

		/// <summary>Gets or sets the unique ID of any log entry associated with the error.</summary>
        public Guid LogId { get; set; }

        /// <summary>Gets or sets a relate error response.</summary>
        public ErrorResponse InnerError { get; set; }

        /// <summary>Gets or sets optional data to associate with the error.</summary>
        public IDictionary<string, string> Data { get; set; }                        
    }
}