/*
 * Represents a type convert used for collections that displays the number of items in the collection
 * instead of just the name.
 *
 * Copyright © 2006 Michael L Taylor ($COMPANY$)
 * All Rights Reserved
 * 
 * $Header: $
 */
#region Imports

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;

using P3Net.Kraken;
#endregion

namespace P3Net.Kraken.Design
{
	/// <summary>Represents a collection type converter that displays the number of items in the collection.</summary>
	[CodeNotAnalyzed]
	[CodeNotDocumented]
	[CodeNotTested]	
	public class CountedCollectionConverter : CollectionConverter
	{
		#region Public Members

		#region Methods

		/// <summary>Converts the object to the given type.</summary>
		/// <param name="context">The conversion context.</param>
		/// <param name="culture">The culture being used.</param>
		/// <param name="value">The current value.</param>
		/// <param name="destinationType">The destination type.</param>
		/// <returns>The value as the specified type.</returns>
		public override object ConvertTo ( ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType )
		{
			//If converting from a collection to a string.
			//MODIFIED: MLT - 6/20/06 CA1800 Cast once
			if (destinationType == typeof(string))
			{
				ICollection coll = value as ICollection;
				if (coll != null)
					return GetCollectionDisplayName(coll);
			};

			return base.ConvertTo(context, culture, value, destinationType);
		}
		#endregion

		#endregion //Public Members

		#region Protected Members

		#region Methods

		/// <summary>Gets the string to display when the collection is converted to a string.</summary>
		/// <param name="value">The value for the collection.</param>
		/// <returns>The collection as a string.</returns>
		protected virtual string GetCollectionDisplayName ( ICollection value )
		{
			return (value != null) ? value.Count.ToString() : "0";
		}
		#endregion

		#endregion //Protected Members
	}
}
