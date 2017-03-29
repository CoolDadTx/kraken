/*
 * Copyright © 2007 Michael L Taylor
 * All Rights Reserved
 */
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace P3Net.Kraken.ServiceProcess
{
	/// <summary>Defines a specific action to take if a service fails.</summary>
	[ExcludeFromCodeCoverage]
    public struct ServiceAction : IEquatable<ServiceAction>
	{
		#region Construction

		/// <summary>Initializes an instance of the <see cref="ServiceAction"/> structure.</summary>
		/// <param name="type">The type of action to take.</param>
		public ServiceAction ( ServiceActionMode type ) : this(type, new TimeSpan(0))
		{ /* Do nothing */ }

		/// <summary>Initializes an instance of the <see cref="ServiceAction"/> structure.</summary>
		/// <param name="type">The type of action to take.</param>
		/// <param name="delay">The delay, in milliseconds, before the action is performed.</param>
		public ServiceAction ( ServiceActionMode type, int delay ) : this(type, new TimeSpan(0, 0, 0, 0, delay))
		{ /* Do nothing */ }

		/// <summary>Initializes an instance of the <see cref="ServiceAction"/> structure.</summary>
		/// <param name="type">The type of action to take.</param>
		/// <param name="delay">The delay interval before the action is performed.</param>
		public ServiceAction ( ServiceActionMode type, TimeSpan delay ) : this()
		{
			ActionType = type;
			Delay = delay;
		}

		internal ServiceAction ( NativeMethods.SC_ACTION action ) : this()
		{
			ActionType = (ServiceActionMode)action.type;
			Delay = new TimeSpan(0, 0, 0, 0, (int)action.delay);
		}
		#endregion //Construction

		#region Public Members

		/// <summary>Gets the action to take.</summary>
		public ServiceActionMode ActionType { get; private set; }

		/// <summary>Gets the delay before the action is taken.</summary>
		public TimeSpan Delay { get; private set; }
        #endregion

		#region Infrastructure

		/// <summary>Compares two objects for equality.</summary>
		/// <param name="obj">The right-hand side.</param>		
		/// <returns><see langword="true"/> if they are equal or <see langword="false"/> otherwise.</returns>
		public override bool Equals ( object obj )
		{
			if (obj == null)
				return false;

			return this == ((ServiceAction)obj);
		}

        /// <summary>Compares two objects for equality.</summary>
        /// <param name="other">The right-hand side.</param>		
        /// <returns><see langword="true"/> if they are equal or <see langword="false"/> otherwise.</returns>
        public bool Equals ( ServiceAction other )
        {
            return this == other;
        }

		/// <summary>Gets the hash code for the object.</summary>
		/// <returns>The hash code.</returns>
		public override int GetHashCode ()
		{
			return (int)ActionType | Delay.GetHashCode();
		}

		/// <summary>Converts the object to a string.</summary>
		/// <returns>The object as a string.</returns>
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
		public override string ToString ()
		{
			return String.Format("{0:g} after {1}", ActionType, Delay);
		}

		/// <summary>Compares two objects for equality.</summary>
		/// <param name="left">The left-hand side.</param>
		/// <param name="right">The right-hand side.</param>
		/// <returns><see langword="true"/> if they are equal or <see langword="false"/> otherwise.</returns>
		public static bool operator == ( ServiceAction left, ServiceAction right )
		{
			return ((left.ActionType == right.ActionType) && (left.Delay == right.Delay));
		}

		/// <summary>Compares two objects for inequality.</summary>
		/// <param name="left">The left-hand side.</param>
		/// <param name="right">The right-hand side.</param>
		/// <returns><see langword="true"/> if they are unequal or <see langword="false"/> otherwise.</returns>
		public static bool operator != ( ServiceAction left, ServiceAction right )
		{
			return ((left.ActionType != right.ActionType) || (left.Delay != right.Delay));
		}
		#endregion
	}
}
