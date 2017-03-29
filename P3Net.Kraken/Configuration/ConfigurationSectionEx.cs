/*
 * Provides enhanced support for configuration sections.
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

using P3Net.Kraken;
#endregion

namespace P3Net.Kraken.Configuration
{
	/// <summary>Provides enhanced support for <see cref="ConfigurationSection"/> instances.</summary>
	/// <remarks>
	/// <see cref="ConfigurationSectionEx"/> provides support for setting up a configuration element
	/// and managing its properties without a lot of coding.  Configuration properties normally do not
	/// change during the lifetime of an application.  Therefore <see cref="ConfigurationSectionEx"/>
	/// stores configuration properties in static fields.  Each configuration element that derives from
	/// this class should define a type constructor that defines the configuration properties for the 
	/// element.  This ensures that the configuration properties are available before they are used.
	/// <para/>
	/// Configuration properties can be defined either programmatically or declaratively.  Declarative properties
	/// are easier to write but require reflection to load which can have a negative impact on performance.  Therefore
	/// <see cref="ConfigurationSectionEx"/>, by default, does not support declarative properties.  To include the
	/// declarative properties call <see cref="LoadProperties"/> in the constructor for the class.
	/// </remarks>
	/// <seealso cref="ConfigurationElementEx"/>
	/// <preliminary/>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase"), CodeNotAnalyzed]
	[CodeNotDocumented]
	[CodeNotTested]
	public abstract class ConfigurationSectionEx : ConfigurationSection
	{		
		#region Protected Members

		#region Attributes

		/// <summary>Gets the configuration properties defined for the element.</summary>
		protected override ConfigurationPropertyCollection Properties
		{
			[DebuggerStepThrough]
			get { return m_Cache.GetProperties(GetType()); }
		}
		#endregion

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
		protected static void AddProperty ( Type elementType, ConfigurationProperty property )
		{
			m_Cache.Add(elementType, property);
		}
				
		/// <summary>Adds a property to the property collection.</summary>
		/// <param name="property">The property to add.</param>
		/// <exception cref="ArgumentNullException"><paramref name="property"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">A property with the same name already exists.</exception>
		protected void AddProperty ( ConfigurationProperty property )
		{
			m_Cache.Add(GetType(), property);
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
		protected static bool ContainsProperty ( Type elementType, ConfigurationProperty property )
		{
			return m_Cache.Contains(elementType, (property != null) ? property.Name : null);
		}

		/// <summary>Determines if a property already exists.</summary>
		/// <param name="elementType">The type to check.</param>
		/// <param name="property">The name of the property to check.</param>
		/// <returns><see langword="true"/> if the property exists or <see langword="false"/> otherwise.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="elementType"/> is <see langword="null"/>.
		/// <para>-or-</para>
		/// <paramref name="property"/> is <see langword="null"/>.
		/// </exception>
		protected static bool ContainsProperty ( Type elementType, string property )
		{
			return m_Cache.Contains(elementType, property);
		}

		/// <summary>Determines if a property already exists.</summary>
		/// <param name="property">The name of the property to check.</param>
		/// <returns><see langword="true"/> if the property exists or <see langword="false"/> otherwise.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="property"/> is <see langword="null"/>.</exception>
		protected bool ContainsProperty ( ConfigurationProperty property )
		{
			return m_Cache.Contains(GetType(), (property != null) ? property.Name : null);
		}

		/// <summary>Determines if a property already exists.</summary>
		/// <param name="property">The name of the property to check.</param>
		/// <returns><see langword="true"/> if the property exists or <see langword="false"/> otherwise.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="property"/> is <see langword="null"/>.</exception>
		protected bool ContainsProperty ( string property )
		{
			return m_Cache.Contains(GetType(), property);
		}

		/// <summary>Gets the value of the given property.</summary>
		/// <param name="property">The property to retrieve the value of.</param>
		/// <returns>The value of the property or <see langword="null"/> if no value is found.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="property"/> is <see langword="null"/>.</exception>
		protected object GetPropertyValue ( ConfigurationProperty property )
		{
			if (property == null)
				throw new ArgumentNullException("property");

			return this[property];
		}

		/// <summary>Gets the value of the given property.</summary>
		/// <param name="property">The name of the property to retrieve the value of.</param>
		/// <returns>The value of the property or <see langword="null"/> if no value is found.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="property"/> is <see langword="null"/>.</exception>
		protected object GetPropertyValue ( string property )
		{
			if (property == null)
				throw new ArgumentNullException("property");

			return this[property];
		}

		/// <summary>Gets the value of the given property.</summary>
		/// <typeparam name="T">The type of the property value.</typeparam>
		/// <param name="property">The property to retrieve the value of.</param>
		/// <param name="defaultValue">The value to return if the property has not been set.</param>
		/// <returns>The value of the property or <paramref name="defaultValue"/> if no value is found.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="property"/> is <see langword="null"/>.</exception>
		protected T GetPropertyValue<T> ( ConfigurationProperty property, T defaultValue )
		{
			object obj = GetPropertyValue(property);
			return (obj != null) ? (T)obj : defaultValue;
		}

		/// <summary>Gets the value of the given property.</summary>
		/// <typeparam name="T">The type of the property value.</typeparam>
		/// <param name="property">The name of the property to retrieve the value of.</param>
		/// <param name="defaultValue">The value to return if the property has not been set.</param>
		/// <returns>The value of the property or <paramref name="defaultValue"/> if no value is found.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="property"/> is <see langword="null"/>.</exception>
		protected T GetPropertyValue<T> ( string property, T defaultValue )
		{
			object obj = GetPropertyValue(property);
			return (obj != null) ? (T)obj : defaultValue;
		}

		/// <summary>Loads all properties defined declaratively for an element.</summary>
		/// <param name="element">The element to load the properties of.</param>
		/// <remarks>
		/// This method can be called any number of times but will only load the properties the
		/// first time it is called.
		/// </remarks>
		protected static void LoadProperties ( ConfigurationSectionEx element )
		{
			m_Cache.LoadProperties(element, OnLoadProperties);			
		}

		/// <summary>Removes all properties from the element.</summary>
		/// <param name="elementType">The type to remove all the properties of.</param>
		/// <exception cref="ArgumentNullException"><paramref name="elementType"/> is <see langword="null"/>.</exception>
		protected static void RemoveAllProperties ( Type elementType )
		{
			m_Cache.Clear(elementType);
		}

		/// <summary>Removes all properties from the element.</summary>
		protected void RemoveAllProperties ( )
		{
			m_Cache.Clear(GetType());
		}
			
		/// <summary>Removes a property from the element.</summary>
		/// <param name="elementType">The type containing the property to remove.</param>
		/// <param name="property">The property to remove.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="elementType"/> is <see langword="null"/>.
		/// <para>-or-</para>
		/// <paramref name="property"/> is <see langword="null"/>.
		/// </exception>
		protected static void RemoveProperty ( Type elementType, ConfigurationProperty property )
		{
			m_Cache.Remove(elementType, (property != null) ? property.Name : null);
		}

		/// <summary>Removes a property from the element.</summary>
		/// <param name="elementType">The type containing the property to remove.</param>
		/// <param name="property">The name of the property to remove.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="elementType"/> is <see langword="null"/>.
		/// <para>-or-</para>
		/// <paramref name="property"/> is <see langword="null"/>.
		/// </exception>
		protected static void RemoveProperty ( Type elementType, string property )
		{
			m_Cache.Remove(elementType, property);
		}

		/// <summary>Removes a property from the element.</summary>
		/// <param name="property">The property to remove.</param>
		/// <exception cref="ArgumentNullException"><paramref name="property"/> is <see langword="null"/>.</exception>
		protected void RemoveProperty ( ConfigurationProperty property )
		{
			m_Cache.Remove(GetType(), (property != null) ? property.Name : null);
		}

		/// <summary>Removes a property from the element.</summary>
		/// <param name="property">The name of the property to remove.</param>
		/// <exception cref="ArgumentNullException"><paramref name="property"/> is <see langword="null"/>.</exception>
		protected void RemoveProperty ( string property )
		{
			m_Cache.Remove(GetType(), property);
		}
		
		/// <summary>Sets the value of a property.</summary>
		/// <param name="property">The property to set.</param>
		/// <param name="value">The value of the property.</param>
		/// <exception cref="ArgumentNullException"><paramref name="property"/> is <see langword="null"/>.</exception>		
		protected void SetPropertyValue<T> ( ConfigurationProperty property, T value )
		{
			if (property == null)
				throw new ArgumentNullException("property");

			this[property] = value;
		}

		/// <summary>Sets the value of a property.</summary>
		/// <param name="property">The name of the property to set.</param>
		/// <param name="value">The value of the property.</param>
		/// <exception cref="ArgumentNullException"><paramref name="property"/> is <see langword="null"/>.</exception>		
		protected void SetPropertyValue<T> ( string property, T value )
		{
			if (property == null)
				throw new ArgumentNullException("property");

			this[property] = value;
		}
		#endregion

		#endregion //Protected Members

		#region Private Members
		
		#region Methods

		private ConfigurationPropertyCollection GetBaseProperties ( )
		{
			return base.Properties;
		}

		//Called when the declarative properties of an element need to be loaded
		private static ConfigurationPropertyCollection OnLoadProperties ( object element )
		{
			Debug.Assert(element is ConfigurationSectionEx, "Element is not ConfigurationSectionEx.");

			return ((ConfigurationSectionEx)element).GetBaseProperties();
		}
		#endregion

		#region Data

		private static ConfigurationPropertyCache m_Cache = new ConfigurationPropertyCache();
		#endregion

		#endregion //Private Members
	}
}
