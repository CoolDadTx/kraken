/*
 * Copyright © 2007 Michael L Taylor
 * All Rights Reserved
 * 
 * $Header: $
 */
#region Imports

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
#endregion

namespace P3Net.Kraken.Web.UI
{
	/// <summary>Provides access to the properties of a page and its base class.</summary>
	/// <remarks>
	/// The property name can be any public or protected property of the page.  If a property can
	/// not be found then the base class of the page is searched.  This continues until the property
	/// is found or the base <see cref="Control"/> class is reached.
	/// </remarks>
	/// <preliminary/>
	[AspNetHostingPermission(System.Security.Permissions.SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
	[CodeNotAnalyzed]
	[CodeNotTested]
	public class PageParameter : Parameter
	{
		#region Construction

		/// <summary>Initializes a new instance of the <see cref="PageParameter"/> class.</summary>
		public PageParameter ()
		{ /* Do nothing */ }

		/// <summary>Initializes a new instance of the <see cref="PageParameter"/> class.</summary>
		/// <param name="name">The name of the property.</param>
		/// <param name="value">The value of the parameter.</param>
		/// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
		public PageParameter ( string name, string value ) : base(name)
		{
			PropertyName = value;
		}

		/// <summary>Initializes a new instance of the <see cref="PageParameter"/> class.</summary>
		/// <param name="name">The name of the property.</param>
		/// <param name="code">The type of the parameter.</param>
		/// <param name="value">The value of the parameter.</param>
		/// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
		public PageParameter ( string name, TypeCode code, string value ) : base(name, code)
		{
			PropertyName = value;
		}

		/// <summary>Initializes a new instance of the <see cref="PageParameter"/> class.</summary>
		/// <param name="original">The original parameter to copy.</param>
		protected PageParameter ( PageParameter original ) : base(original)
		{
			PropertyName = original.PropertyName;
		}
		#endregion //Construction

		#region Public Members

		#region Attributes

		/// <summary>Gets or sets the property to be retrieved.</summary>		
		[Description("The name of the property to be retrieved.")]
		public string PropertyName
		{
			[DebuggerStepThrough]
			get
			{
				string str = ViewState["PropertyName"] as string;
				return (str != null) ? str : "";
			}

			[DebuggerStepThrough]
			set
			{
				value = (value != null) ? value.Trim() : "";
				ViewState["PropertyName"] = value;
				OnParameterChanged();
			}
		}
		#endregion

		#endregion //Public Members

		#region Protected Members

		#region Methods

		/// <summary>Clones the parameter value.</summary>
		/// <returns>The cloned parameter.</returns>
		protected override Parameter Clone ()
		{
			return new PageParameter(this);
		}

		/// <summary>Evaluates the property and returns the parameter value.</summary>
		/// <param name="context">The context of the request.</param>
		/// <param name="control">The associated control, if any.</param>
		/// <returns>The value of the parameter.</returns>
		/// <exception cref="ArgumentException">
		/// The property name was not specified.
		/// <para>-or-</para>
		/// The property was not found.
		/// </exception>		
		protected override object Evaluate ( HttpContext context, Control control )
		{
			if (PropertyName.Length == 0)
				throw new ArgumentException("PropertyName was not specified.");
			if ((control == null) || (control.Page == null))
				throw new ArgumentNullException("control");

			//Search for the property starting with the base page class
			BindingFlags flags = BindingFlags.FlattenHierarchy |
								BindingFlags.NonPublic | BindingFlags.Public |  
								BindingFlags.Instance | BindingFlags.Static;
			Type typeCtrl = control.Page.GetType();
			while(typeCtrl != null)
			{
				PropertyInfo prop = typeCtrl.GetProperty(PropertyName, flags);
				if (prop != null)
					return prop.GetValue(control.Page, null);

				if (typeCtrl != typeof(Control))
					typeCtrl = typeCtrl.BaseType;
				else
					typeCtrl = null;
			};

			//If we didn't find it then throw
			throw new ArgumentException("Property not found.");			
		}		
		#endregion

		#endregion //Protected Members			
	}
}