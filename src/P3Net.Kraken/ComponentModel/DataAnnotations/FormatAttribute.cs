/*
 * Copyright © 2016 Michael Taylor
 * All Rights Reserved
 * 
 * From code copyright © 2016 Federation of State Medical Boards
 */
using System;
using System.ComponentModel;

using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.ComponentModel.DataAnnotations
{
    /// <summary>Defines the format of a value.</summary>
    /// <remarks>
    /// This attribute can be used in conjunction with <see cref="DescriptionAttribute"/> to specify the format of a value.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FormatAttribute : Attribute
    {
        /// <summary>Initializes an instance of the <see cref="FormatAttribute"/> class.</summary>
        /// <param name="format">The format.</param>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="format"/> is empty.</exception>
        public FormatAttribute ( string format )
        {
            Verify.Argument(nameof(format)).WithValue(format).IsNotNullOrEmpty();

            Format = format;
        }

        /// <summary>Gets or sets an example of the format.</summary>
        public string Example
        {
            get { return _example ?? ""; }
            set { _example = value; }
        }

        /// <summary>Gets the format.</summary>
        public string Format { get; private set; }

        #region Private Members

        private string _example;
        #endregion
    }
}
