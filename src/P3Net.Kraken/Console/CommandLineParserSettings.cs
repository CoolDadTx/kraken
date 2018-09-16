/*
 * Represents the settings for a command line parser.
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
using System.Diagnostics;
using System.Threading;

using P3Net.Kraken;
#endregion

namespace P3Net.Kraken.Console
{
	///// <summary>Represents the settings used for <see cref="CommandLineParser"/>.</summary>
	///// <preliminary/>	
	//[CodeNotAnalyzed]
	//[CodeNotDocumented]
	//[CodeNotTested]
	//public struct CommandLineParserSettings
	//{
	//    #region Public Members

	//    #region Attributes

	//    /// <summary>Gets or sets whether the case of options is ignored.</summary>
	//    public bool IgnoreCase
	//    {
	//        [DebuggerStepThrough]
	//        get { return m_bIgnoreCase; }

	//        [DebuggerStepThrough]
	//        set { m_bIgnoreCase = value; }
	//    }

	//    /// <summary>Gets the collection of delimiters to use for identifying options.</summary>
	//    public Collection<char> OptionsDelimiters
	//    {
	//        [DebuggerStepThrough]
	//        get 
	//        {
	//            if (m_OptionsDelimiters == null)
	//                Interlocked.CompareExchange(ref m_OptionsDelimiters, new Collection<char>(), null);

	//            return m_OptionsDelimiters; 
	//        }
	//    }

	//    /// <summary>Gets the collection of delimiters that separate a setting from its value.</summary>
	//    public Collection<char> SettingsDelimiters
	//    {
	//        [DebuggerStepThrough]
	//        get
	//        {
	//            if (m_SettingsDelimiters == null)
	//                Interlocked.CompareExchange(ref m_SettingsDelimiters, new Collection<char>(), null);

	//            return m_SettingsDelimiters;
	//        }
	//    }
	//    #endregion

	//    #endregion //Public Members

	//    #region Private Members

	//    #region Data

	//    private Collection<char> m_OptionsDelimiters;
	//    private Collection<char> m_SettingsDelimiters;
	//    private bool m_bIgnoreCase;
	//    #endregion

	//    #endregion //Private Members
	//}
}
