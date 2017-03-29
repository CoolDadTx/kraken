/*
 * Represents an option that can appear on the command line.
 *
 * Copyright © 2006 Michael L Taylor ($COMPANY$)
 * All Rights Reserved
 * 
 * $Header: $
 */
#region Imports

using System;
using System.Diagnostics;

using P3Net.Kraken;
#endregion

namespace P3Net.Kraken.Console
{
	///// <summary>Represents an option that can appear on the command line.</summary>
	///// <remarks>
	///// Each option has a name which specifies the argument that will map to the option.  The name
	///// does not include any argument switches or symbols.  One option may have no name, in this case
	///// all arguments that are not either an option or immediately following a setting are
	///// considered to be part of the Each option can be associated with an optional
	///// category.  The category is used to combine related options under a single header in the help description.	
	///// <para/>
	///// An option is either a switch or a setting.  The presences of a switch is sufficient to indicate
	///// some meaning (such as verbose output or to recursively perform an operation).  A setting requires
	///// an additional argument in order to have meaning.
	///// </remarks>
	///// <preliminary/>	
	//[CodeNotAnalyzed]
	//[CodeNotDocumented]
	//[CodeNotTested]
	//public class CommandLineOption
	//{
	//    #region Construction

	//    /// <summary>Initializes an instance of the <see cref="CommandLineOption"/> class.</summary>
	//    public CommandLineOption ( )
	//    { /* Do nothing */ }

	//    /// <summary>Initializes an instance of the <see cref="CommandLineOption"/> class.</summary>
	//    /// <param name="name">The name of the option.</param>
	//    /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
	//    public CommandLineOption ( string name ) : this(name, null, null, CommandLineOptionFlags.None)
	//    { /* Do nothing */ }

	//    /// <summary>Initializes an instance of the <see cref="CommandLineOption"/> class.</summary>
	//    /// <param name="name">The name of the option.</param>
	//    /// <param name="flags">Flags that represent the option.</param>
	//    /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
	//    public CommandLineOption ( string name, CommandLineOptionFlags flags ) : this(name, null, null, flags)
	//    { /* Do nothing */ }

	//    /// <summary>Initializes an instance of the <see cref="CommandLineOption"/> class.</summary>
	//    /// <param name="name">The name of the option.</param>
	//    /// <param name="helpText">Help text describing the option.</param>
	//    /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
	//    public CommandLineOption ( string name, string helpText ) : this(name, helpText, null, CommandLineOptionFlags.None)
	//    { /* Do nothing */ }

	//    /// <summary>Initializes an instance of the <see cref="CommandLineOption"/> class.</summary>
	//    /// <param name="name">The name of the option.</param>
	//    /// <param name="helpText">Help text describing the option.</param>
	//    /// <param name="flags">Flags that represent the option.</param>
	//    /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
	//    public CommandLineOption ( string name, string helpText, CommandLineOptionFlags flags )	
	//            : this(name, helpText, null, flags)
	//    { /* Do nothing */ }

	//    /// <summary>Initializes an instance of the <see cref="CommandLineOption"/> class.</summary>
	//    /// <param name="name">The name of the option.</param>
	//    /// <param name="helpText">Help text describing the option.</param>
	//    /// <param name="category">The category associated with the option.</param>
	//    /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
	//    public CommandLineOption ( string name, string helpText, string category )
	//                            : this(name, helpText, category, CommandLineOptionFlags.None)
	//    { /* Do nothing */ }

	//    /// <summary>Initializes an instance of the <see cref="CommandLineOption"/> class.</summary>
	//    /// <param name="name">The name of the option.</param>
	//    /// <param name="helpText">Help text describing the option.</param>
	//    /// <param name="category">The category associated with the option.</param>
	//    /// <param name="flags">Flags that represent the option.</param>
	//    /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
	//    public CommandLineOption ( string name, string helpText, string category, CommandLineOptionFlags flags )
	//    {
	//        ValidationHelper.ThrowIfArgumentNull(name, "name");
	//        Name = name;

	//        HelpText = helpText;
	//        Category = category;
	//        m_Flags = flags;
	//    }
	//    #endregion //Construction

	//    #region Public Members

	//    #region Attributes

	//    /// <summary>Gets or sets whether an option can appear multiple times.</summary>
	//    /// <value>The default value is <see langword="false"/>.</value>
	//    public bool AllowMultiple
	//    {
	//        [DebuggerStepThrough]
	//        get { return BitFlags.IsSet((long)m_Flags, (long)CommandLineOptionFlags.AllowMultiple); }

	//        [DebuggerStepThrough]
	//        set 
	//        {
	//            m_Flags = (CommandLineOptionFlags)(value ?
	//                BitFlags.Set((long)m_Flags, (long)CommandLineOptionFlags.AllowMultiple) :
	//                BitFlags.Clear((long)m_Flags, (long)CommandLineOptionFlags.AllowMultiple));
	//        }
	//    }

	//    /// <summary>Gets or sets the category of the option.</summary>
	//    /// <value>Unless otherwise specified the category defaults to <b>Options</b>.</value>
	//    public string Category
	//    {
	//        [DebuggerStepThrough]
	//        get { return m_strCategory ?? "Options"; }

	//        [DebuggerStepThrough]
	//        set { m_strCategory = (value != null) ? value.Trim() : null; }
	//    }

	//    /// <summary>Gets or sets the help text to associate with the option.</summary>
	//    public string HelpText
	//    {
	//        [DebuggerStepThrough]
	//        get { return m_strHelp ?? ""; }

	//        [DebuggerStepThrough]
	//        set { m_strHelp = (value != null) ? value.Trim() : null; }
	//    }

	//    /// <summary>Gets or sets whether an option is required or not.</summary>
	//    /// <value>The default value is <see langword="false"/>.</value>
	//    public bool IsRequired
	//    {
	//        [DebuggerStepThrough]
	//        get { return BitFlags.IsSet((long)m_Flags, (long)CommandLineOptionFlags.IsRequired); }

	//        [DebuggerStepThrough]
	//        set 
	//        {
	//            m_Flags = (CommandLineOptionFlags)(value ?
	//                BitFlags.Set((long)m_Flags, (long)CommandLineOptionFlags.IsRequired) :
	//                BitFlags.Clear((long)m_Flags, (long)CommandLineOptionFlags.IsRequired));
	//        }
	//    }

	//    /// <summary>Gets or sets whether the option represents a setting.</summary>
	//    /// <value>The default value is <see langword="false"/>.</value>
	//    public bool IsSetting
	//    {
	//        [DebuggerStepThrough]
	//        get { return BitFlags.IsSet((long)m_Flags, (long)CommandLineOptionFlags.IsSetting); }

	//        [DebuggerStepThrough]
	//        set 
	//        {
	//            m_Flags = (CommandLineOptionFlags)(value ?
	//                BitFlags.Set((long)m_Flags, (long)CommandLineOptionFlags.IsSetting) :
	//                BitFlags.Clear((long)m_Flags, (long)CommandLineOptionFlags.IsSetting));
	//        }
	//    }

	//    /// <summary>Gets or sets the name of the option.</summary>
	//    /// <exception cref="ArgumentNullException">When setting the property and the value is <see langword="null"/>.</exception>
	//    /// <exception cref="ArgumentException">When setting the property and the value is empty.</exception>
	//    /// <value>The name does not include any argument delimiters such as '/' or '-'.</value>
	//    public string Name
	//    {
	//        [DebuggerStepThrough]
	//        get { return m_strName ?? ""; }

	//        [DebuggerStepThrough]
	//        set
	//        {
	//            ValidationHelper.ThrowIfArgumentStringEmptyOrNull(value, "Name");
	//            m_strName = value.Trim();
	//        }
	//    }
	//    #endregion

	//    #endregion //Public Members

	//    #region Private Members

	//    #region Data

	//    private CommandLineOptionFlags m_Flags = CommandLineOptionFlags.None;
		
	//    private string m_strName;
	//    private string m_strCategory;
	//    private string m_strHelp;

	//    #endregion

	//    #endregion //Private Members
	//}	
}
