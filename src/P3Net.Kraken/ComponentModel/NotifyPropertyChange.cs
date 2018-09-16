/*
 * Copyright © 2007 Michael L Taylor
 * All Rights Reserved
 * 
 * $Header: /Kraken/Source/P3Net.Kraken/ComponentModel/NotifyPropertyChange.cs   1   2009-11-22 14:41:20-06:00   Michael $
 */
#region Imports

using System;
using System.ComponentModel;
using System.Diagnostics;

using P3Net.Kraken;
#endregion

namespace P3Net.Kraken.ComponentModel
{
	/// <summary>Provides a basic implementation for <see cref="INotifyPropertyChanged"/> and
	/// <see cref="INotifyPropertyChanging"/>.</summary>
	[CodeNotAnalyzed]
	public abstract class NotifyPropertyChange : INotifyPropertyChanged, INotifyPropertyChanging
	{
		#region Public Members
				
		/// <summary>Raised when a property on the object changes.</summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>Raised when a property is preparing to change.</summary>
		public event PropertyChangingEventHandler PropertyChanging;
		
		#endregion 

		#region Protected Members

		/// <summary>Raises the <see cref="PropertyChanged"/> event.</summary>
		/// <param name="propertyName">The name of the property that has changed.</param>
		protected virtual void OnPropertyChanged ( string propertyName )
		{
			PropertyChangedEventHandler hdlr = PropertyChanged;
			if (hdlr != null)
				hdlr(this, new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>Raises the <see cref="PropertyChanging"/> event.</summary>
		/// <param name="propertyName">The name of the property being changed.</param>
		protected virtual void OnPropertyChanging ( string propertyName )
		{
			PropertyChangingEventHandler hdlr = PropertyChanging;
			if (hdlr != null)
				hdlr(this, new PropertyChangingEventArgs(propertyName));
		}
		#endregion 
	}
}
