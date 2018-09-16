/*
 * Copyright © 2006 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.Design
{
    /// <summary>Provides a type editor that displays its contents in a drop down list.</summary>
    [CodeNotTested]
    public class DropDownEditor : UITypeEditor
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="DropDownEditor"/> class.</summary>
        public DropDownEditor ( )
        { }

        /// <summary>Initializes an instance of the <see cref="DropDownEditor"/> class.</summary>
        /// <param name="sorted"><see langword="true"/> to sort the list.</param>
        public DropDownEditor ( bool sorted )
        {
            Sorted = sorted;
        }
        #endregion

        #region Public Members

        /// <summary>Gets or sets whether the list is sorted or not.</summary>
        public bool Sorted { get; set; }

        #region Methods

        /// <summary>Edits the property value.</summary>
        /// <param name="context">The context of the editor.</param>
        /// <param name="provider">The provider for the editor.</param>
        /// <param name="value">The current value.</param>
        /// <returns>The new value.</returns>		
        /// <exception cref="ArgumentNullException"><paramref name="provider"/> is <see langword="null"/>.</exception>
        public override object EditValue ( ITypeDescriptorContext context, IServiceProvider provider, object value )
        {
            Verify.Argument("provider", provider).IsNotNull();

            //Generate a drop-down list
            m_Svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            if (m_Svc != null)
            {
                try
                {
                    ListBox box = new ListBox();
                    box.Sorted = Sorted;
                    box.BorderStyle = BorderStyle.None;
                    box.Click += OnBoxClicked;

                    //Get the list of legal values
                    box.Items.AddRange(GetListValues(context, provider, value));
                    box.SelectedItem = value;
                    m_Svc.DropDownControl(box);

                    return box.SelectedItem ?? value;
                } finally
                {
                    m_Svc = null;
                };
            };

            return value;
        }

        /// <summary>Specifies the style of the editor.</summary>
        /// <param name="context">The context of the editor.</param>
        /// <returns>The style of the editor.</returns>
        public override UITypeEditorEditStyle GetEditStyle ( ITypeDescriptorContext context )
        {
            return UITypeEditorEditStyle.DropDown;
        }			
        #endregion

        #endregion 

        #region Protected Members

        /// <summary>Gets the list of value to display for the item.</summary>
        /// <param name="context">The context of the editor.</param>
        /// <param name="provider">The provider for the editor.</param>
        /// <param name="value">The current value.</param>
        /// <returns>An array of values for the object.</returns>
        protected virtual object[] GetListValues ( ITypeDescriptorContext context, IServiceProvider provider, object value )
        {
            if (value == null)
                return new object[0];

            Type type = value.GetType();
            if (type.IsEnum)
            {
                Array arr = Enum.GetValues(type);
                object[] values = new object[arr.Length];
                arr.CopyTo(values, 0);
                return values;
            };

            return new object[0];			
        }
        #endregion

        #region Private Members

        private void OnBoxClicked ( object sender, EventArgs e )
        {			
            ListBox box = sender as ListBox;
            if (box != null)
                m_Svc.CloseDropDown();
        }		
                
        private IWindowsFormsEditorService m_Svc;
        
        #endregion
    }
}
