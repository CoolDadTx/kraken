/*
 * Provides a parser for command line arguments.
 *
 * Copyright © 2006 Michael L Taylor ($COMPANY$)
 * All Rights Reserved
 * 
 * $Header: $
 */
#region Imports

using System;
using System.Collections.Generic;
using System.Diagnostics;

using P3Net.Kraken;
using P3Net.Kraken.Collections;
#endregion

namespace P3Net.Kraken.Console
{
	///// <summary>Provides a parser for command line arguments.</summary>
	///// <remarks>
	///// To use <see cref="CommandLineParser"/> add a <see cref="CommandLineOption"/> instance to the
	///// <see cref="Options"/> property for each supported command line option.  Then call the <see cref="Parse"/>
	///// method on the argument list to parse and validate the arguments.  <see cref="CommandLineParser"/> auto-generates
	///// help information based on the configured options.
	///// <para/>
	///// A class can be derived from <see cref="CommandLineParser"/> if the default behavior is not sufficient.
	///// </remarks>
	///// <preliminary/>
	//[CodeNotAnalyzed]
	//[CodeNotDocumented]
	//[CodeNotTested]
	//public class CommandLineParser
	//{
	//    #region Construction

	//    /// <summary>Initializes an instance of the <see cref="CommandLineParser"/> class.</summary>
	//    public CommandLineParser ( )
	//    {
	//        //Defaults
	//        m_Settings.IgnoreCase = true;

	//        m_Settings.OptionsDelimiters.Add('-');
	//        m_Settings.OptionsDelimiters.Add('/');

	//        m_Settings.SettingsDelimiters.Add(' ');
	//    }

	//    /// <summary>Initializes an instance of the <see cref="CommandLineParser"/> class.</summary>
	//    /// <param name="settings">The default settings to use.</param>
	//    public CommandLineParser ( CommandLineParserSettings settings )
	//    {
	//        m_Settings = settings;
	//    }
	//    #endregion //Construction

	//    #region Public Members

	//    #region Attributes

	//    /// <summary>Gets the options defined for the parser.</summary>
	//    public CollectionEx<CommandLineOption> Options
	//    {
	//        [DebuggerStepThrough]
	//        get { return m_Options; }
	//    }

	//    /// <summary>Gets the settings to use for the parser.</summary>
	//    /// <remarks>
	//    /// By default the following settings are used.
	//    /// <list type="bullet">
	//    ///		<item>IgnoreCase : True</item>
	//    ///		<item>OptionsDelimiters : '/', '-'</item>
	//    ///		<item>SettingsDelimiters : ' '</item>
	//    /// </list>
	//    /// </remarks>
	//    public CommandLineParserSettings Settings
	//    {
	//        [DebuggerStepThrough]
	//        get { return m_Settings; }
	//    }
	//    #endregion

	//    #region Methods

	//    /// <summary>Parses the string arguments.</summary>
	//    /// <param name="arguments">An array of arguments to parse.</param>
	//    /// <exception cref="Exception">An error occurred parsing the arguments.</exception>
	//    public void Parse ( string[] arguments )
	//    {
	//        Dictionary<string, CommandLineOption> options = PrepareOptions();
	//    }
	//    #endregion

	//    #endregion //Public Members

	//    #region Protected Members

	//    #region Methods

	//    /// <summary>Called when a setting does not have a value.</summary>
	//    /// <param name="setting">The setting that is missing a value.</param>
	//    /// <exception cref="Exception">No value was specified for a setting.</exception>
	//    /// <remarks>
	//    /// The default implementation throws an <see cref="Exception"/>.  Derived classes
	//    /// can override this behavior.
	//    /// </remarks>
	//    protected virtual void OnMissingSettingValue ( CommandLineOption setting )
	//    {
	//        throw new Exception(String.Format("No value specified for setting '{0}'.", setting.Name));
	//    }

	//    /// <summary>Called when an unknown option is found.</summary>
	//    /// <param name="argument">The argument that was being parsed.</param>
	//    /// <exception cref="Exception">An unknown option was found.</exception>
	//    /// <remarks>
	//    /// The default implementation throws an <see cref="Exception"/>.  Derived classes
	//    /// can override this behavior.
	//    /// </remarks>
	//    protected virtual void OnUnknownOption ( string argument )
	//    {
	//        throw new Exception(String.Format("An unknown option '{0}' was found.", argument));
	//    }
	//    #endregion

	//    #endregion //Protected Members

	//    #region Private Members

	//    #region Methods

	//    private Dictionary<string, CommandLineOption> PrepareOptions ( )
	//    {
	//        CollectionEx<CommandLineOption> opts = Options;
	//        Dictionary<string, CommandLineOption> dict;
	//        if (Settings.IgnoreCase)
	//            dict = new Dictionary<string, CommandLineOption>(StringComparer.InvariantCultureIgnoreCase);
	//        else
	//            dict = new Dictionary<string, CommandLineOption>();

	//        //Move the options to the dictionary
	//        foreach (CommandLineOption option in opts)
	//        {
	//            if (option != null)
	//            {
	//                if (dict.ContainsKey(option.Name))
	//                {
	//                    if (option.Name == "")
	//                        throw new Exception("A default option already exists.");
	//                    else
	//                        throw new Exception(String.Format("An option already exists with the name '{0}'.", option.Name));
	//                };

	//                dict[option.Name] = option;
	//            };
	//        };

	//        return dict;
	//    }
	//    #endregion

	//    #region Data

	//    private CommandLineParserSettings m_Settings;
	//    private CollectionEx<CommandLineOption> m_Options = new CollectionEx<CommandLineOption>();
	//    #endregion

	//    #endregion //Private Members
	//}
}
