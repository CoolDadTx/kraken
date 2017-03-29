/*
 * Provides support for configuration properties.
 *
 * Copyright © 2006 Michael L Taylor ($COMPANY$)
 * All Rights Reserved
 * 
 * $Header: $
 */
#region Imports

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
#endregion

namespace P3Net.Kraken.Configuration
{
	//Delegate for loading properties
	internal delegate ConfigurationPropertyCollection LoadPropertiesDelegate ( object element );

	//Provides support for configuration properties
	internal sealed class ConfigurationPropertyCache
	{		
		#region Public Members
		
		#region Methods

		/// <summary>Adds a property to the property collection.</summary>
		/// <param name="elementType">The type to add the property to.</param>
		/// <param name="property">The property to add.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="elementType"/> is <see langword="null"/>.
		/// <para>-or-</para>
		/// <paramref name="property"/> is <see langword="null"/>.
		/// </exception>
		/// <exception cref="ArgumentException">A property with the same name already exists.</exception>
		public void Add ( Type elementType, ConfigurationProperty property )
		{
			if (elementType == null)
				throw new ArgumentNullException("elementType");
			if (property == null)
				throw new ArgumentNullException("property");

			GetPropertyData(elementType).Properties.Add(property);
		}

		/// <summary>Removes all properties from the element.</summary>
		/// <param name="elementType">The type to remove all the properties of.</param>
		/// <exception cref="ArgumentNullException"><paramref name="elementType"/> is <see langword="null"/>.</exception>
		public void Clear ( Type elementType )
		{
			if (elementType == null)
				throw new ArgumentNullException("elementType");

			GetPropertyData(elementType).Properties.Clear();
		}
		
		/// <summary>Determines if a property already exists.</summary>
		/// <param name="elementType">The type to check.</param>
		/// <param name="property">The property to check.</param>
		/// <returns><see langword="true"/> if the property exists or <see langword="false"/> otherwise.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="elementType"/> is <see langword="null"/>.
		/// <para>-or-</para>
		/// <paramref name="property"/> is <see langword="null"/>.
		/// </exception>
		public bool Contains ( Type elementType, string property )
		{
			if (elementType == null)
				throw new ArgumentNullException("elementType");
			if (property == null)
				throw new ArgumentNullException("property");

			return GetPropertyData(elementType).Properties[property] != null;
		}

		/// <summary>Gets the properties associated with a type.</summary>
		/// <param name="elementType">The type to retrieve the properties of.</param>
		/// <returns>A collection of properties for the element.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="elementType"/> is <see langword="null"/>.</exception>
		public ConfigurationPropertyCollection GetProperties ( Type elementType )
		{
			if (elementType == null)
				throw new ArgumentNullException("elementType");

			return GetPropertyData(elementType).Properties;
		}

		/// <summary>Loads all properties defined declaratively for an element.</summary>
		/// <param name="element">The element to load the properties of.</param>
		/// <param name="handler">The handler to call if the properties must be loaded.</param>
		/// <remarks>
		/// This method can be called any number of times but will only load the properties the
		/// first time it is called.
		/// </remarks>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="element"/> is <see langword="null"/>.
		/// <para>-or-</para>
		/// <paramref name="handler"/> is <see langword="null"/>.
		/// </exception>
		public void LoadProperties ( object element, LoadPropertiesDelegate handler )
		{
			if (element == null)
				throw new ArgumentNullException("element");
			if (handler == null)
				throw new ArgumentNullException("handler");

			//If already loaded then skip it
			PropertyData data = GetPropertyData(element.GetType());
			if (data.PropertiesLoaded)
				return;

			//We'll use a shared lock because this shouldn't happen too often
			lock (m_PropertyCache)
			{
				//Double check
				if (data.PropertiesLoaded)
					return;

				//Load the declarative properties				
				ConfigurationPropertyCollection props = handler(element);
				foreach (ConfigurationProperty prop in props)
				{
					data.Properties.Add(prop);
				};

				//Mark as loaded
				data.PropertiesLoaded = true;
			};
		}
		
		/// <summary>Removes a property from the element.</summary>
		/// <param name="elementType">The type containing the property to remove.</param>
		/// <param name="property">The property to remove.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="elementType"/> is <see langword="null"/>.
		/// <para>-or-</para>
		/// <paramref name="property"/> is <see langword="null"/>.
		/// </exception>
		public void Remove ( Type elementType, string property )
		{
			if (elementType == null)
				throw new ArgumentNullException("elementType");
			if (property == null)
				throw new ArgumentNullException("property");

			GetPropertyData(elementType).Properties.Remove(property);
		}
		#endregion

		#endregion //Public Members

		#region Private Members

		#region Types

		private class PropertyData
		{
			public ConfigurationPropertyCollection Properties
			{
				[DebuggerStepThrough]
				get { return m_Properties; }
			}

			public bool PropertiesLoaded
			{
				[DebuggerStepThrough]
				get { return m_bLoaded; }

				[DebuggerStepThrough]
				set { m_bLoaded = value; }
			}

			private ConfigurationPropertyCollection m_Properties = new ConfigurationPropertyCollection();
			private bool m_bLoaded;
		}
		#endregion

		#region Methods

		// Gets the property data associated with a type.
		private PropertyData GetPropertyData ( Type elementType )
		{
			//Just look it up first
			PropertyData data;
			if (!m_PropertyCache.TryGetValue(elementType, out data))
			{
				//Lock (we don't do this very often so performance is not critical)
				lock (m_PropertyCache)
				{
					//Double check and add
					if (!m_PropertyCache.TryGetValue(elementType, out data))
						m_PropertyCache[elementType] = data = new PropertyData();
				};
			};

			return data;
		}		
		#endregion

		#region Data

		//Maps property collections to types
		private Dictionary<Type, PropertyData> m_PropertyCache = new Dictionary<Type, PropertyData>();		
		#endregion

		#endregion //Private Members
	}
}
