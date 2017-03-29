/*
 * Copyright © 2007 Michael Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.Diagnostics.CodeAnalysis;

#endregion

namespace P3Net.Kraken.Data.Common
{
    /// <summary>Defines the schema of a database.</summary>
    [ExcludeFromCodeCoverage]
    public sealed class SchemaInformation
    {
        /// <summary>Determines if a parameter name prefix is defined.</summary>
        public bool HasParameterPrefix
        {
            get { return !String.IsNullOrEmpty(m_parameterFormatPrefix); }
        }

        /// <summary>Determines if a parameter name suffix is defined.</summary>
        public bool HasParameterSuffix
        {
            get { return !String.IsNullOrEmpty(m_parameterFormatSuffix); }
        }

        /// <summary>Gets or sets the format of parameter names.</summary>
        public string ParameterFormat
        {
            get { return ParameterFormatPrefix + "{0}" + ParameterFormatSuffix; }
            set
            {
                value = (value != null) ? value.Trim() : "";

                int nPos = value.IndexOf('{');
                if (nPos >= 0)
                {
                    m_parameterFormatPrefix = value.Left(nPos);
                    m_parameterFormatSuffix = value.RightOf("}");
                };
            }
        }

        /// <summary>Gets the parameter name prefix.</summary>
        public string ParameterFormatPrefix
        {
            get { return m_parameterFormatPrefix ?? ""; }
        }

        /// <summary>Gets the parameter name suffix.</summary>
        public string ParameterFormatSuffix
        {
            get { return m_parameterFormatSuffix ?? ""; }
        }

        #region Private Members

        private string m_parameterFormatPrefix;
        private string m_parameterFormatSuffix;
        #endregion
    }
}
