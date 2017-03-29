/*
 * Copyright © 2006 Michael Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

#endregion

namespace P3Net.Kraken.Reflection
{
    /// <summary>Provides assembly details such as version, filename and copyright notice.</summary>
    /// <seealso cref="AssemblyExtensions"/>
    /// <threadsafety static="true" instance="true" />
    public sealed class AssemblyDetails
    {
        #region Construction

        internal AssemblyDetails ( Assembly assembly )
        {
            Debug.Assert(assembly != null, "Assembly is null.");

            m_assembly = assembly;
        }
        #endregion

        #region Public Members
        
        /// <summary>Gets the build date of the assembly.</summary>
        /// <value>The build date comes from the PE header information.</value>
        public DateTime BuildDate
        {
            get
            {
                DateTime value;
                if (!TryGetProperty("BuildDate", out value))                
                {
                    value = GetBuildDate();
                    SetProperty("BuildDate", value);                    
                };

                return value;
            }
        }

        /// <summary>Gets the company name.</summary>
        /// <value>The property comes from the corresponding assembly attribute.</value>
        public string CompanyName
        {
            get 
            {
                return GetAttributeProperty("CompanyName", "", typeof(AssemblyCompanyAttribute), "Company"); 
            }
        }

        /// <summary>Gets the configuration name.</summary>
        /// <value>The property comes from the corresponding assembly attribute.</value>
        public string Configuration
        {
            get 
            {
                return GetAttributeProperty("Configuration", "", typeof(AssemblyConfigurationAttribute), "Configuration"); 
            }
        }
        
        /// <summary>Gets the copyright notice.</summary>
        /// <value>The property comes from the corresponding assembly attribute.</value>
        public string Copyright
        {
            get
            {
                return GetAttributeProperty("Copyright", "", typeof(AssemblyCopyrightAttribute), "Copyright");
            }
        }
        
        /// <summary>Gets the description of the assembly.</summary>
        /// <value>The property comes from the corresponding assembly attribute.</value>
        public string Description
        {
            get
            {
                return GetAttributeProperty("Description", "", typeof(AssemblyDescriptionAttribute), "Description");
            }
        }

        /// <summary>Gets the filename of the assembly.</summary>
        /// <value>The filename includes the extension.</value>
        public string FileName
        {
            get
            {
                string value;
                if (!TryGetProperty("FileName", out value))
                {
                    value = Path.GetFileName(FullPath);
                    SetProperty("FileName", value);
                };

                return value;
            }
        }

        /// <summary>Gets the filename of the assembly.</summary>
        /// <value>The filename does not include the extension.</value>
        public string FileNameWithoutExtension
        {
            get
            {
                string value;
                if (!TryGetProperty("FileNameWithoutExtension", out value))
                {
                    value = Path.GetFileNameWithoutExtension(FullPath);
                    SetProperty("FileNameWithoutExtension", value);
                };

                return value;
            }
        }

        /// <summary>Gets the file version of the assembly.</summary>
        /// <value>The property comes from the corresponding assembly attribute.</value>
        [ExcludeFromCodeCoverage]
        public Version FileVersion
        {
            get
            {
                Version value;
                if (!TryGetProperty("FileVersion", out value))
                {                    
                    string str = GetAttributeProperty("FileVersion", "", typeof(AssemblyFileVersionAttribute), "Version");
                    value = String.IsNullOrEmpty(str) ? new Version(0, 0, 0, 0) : new Version(str);

                    SetProperty("FileVersion", value);
                };

                return value;
            }
        }

        /// <summary>Gets the file version as a string.</summary>
        /// <value>The property comes from the corresponding assembly attribute.</value>
        public string FileVersionString
        {
            get	{ return FileVersion.ToString(); }
        }

        /// <summary>Gets the full path to the assembly.</summary>
        public string FullPath
        {
            get { return m_assembly.Location;	}
        }

        /// <summary>Gets the informational version of the assembly.</summary>
        /// <value>The property comes from the corresponding assembly attribute.</value>
        public string InformationalVersion
        {
            get
            {
                return GetAttributeProperty("InformationalVersion", "", typeof(AssemblyInformationalVersionAttribute), "InformationalVersion");
            }
        }

        /// <summary>Determines if the assembly is CLS compliant.</summary>
        /// <value>The property comes from the corresponding assembly attribute.</value>
        public bool IsClsCompliant
        {
            get
            {
                return GetAttributeProperty("IsCLSCompliant", false, typeof(CLSCompliantAttribute), "IsCompliant");
            }
        }

        /// <summary>Determines if the assembly is visible to COM.</summary>
        /// <value>The property comes from the corresponding assembly attribute.</value>
        public bool IsComVisible
        {
            get
            {
                return GetAttributeProperty("IsComVisible", true, typeof(ComVisibleAttribute), "Value");
            }
        }

        /// <summary>Gets the product name of the assembly.</summary>
        /// <value>The property comes from the corresponding assembly attribute.</value>
        public string ProductName
        {
            get
            {
                return GetAttributeProperty("ProductName", "", typeof(AssemblyProductAttribute), "Product");
            }
        }

        /// <summary>Gets the title of the assembly.</summary>
        /// <value>The property comes from the corresponding assembly attribute.</value>
        public string Title
        {
            get
            {
                return GetAttributeProperty("Title", "", typeof(AssemblyTitleAttribute), "Title");
            }
        }

        /// <summary>Gets the trademark notice of the assembly.</summary>
        /// <value>The property comes from the corresponding assembly attribute.</value>
        public string Trademark
        {
            get
            {
                return GetAttributeProperty("Trademark", "", typeof(AssemblyTrademarkAttribute), "Trademark");
            }
        }

        /// <summary>Gets the version of the assembly.</summary>
        /// <value>The property comes from the corresponding assembly attribute.</value>
        [ExcludeFromCodeCoverage]
        public Version Version
        {
            get
            {
                Version value;
                if (!TryGetProperty("Version", out value))
                {
                    value = m_assembly.GetName().Version ?? new Version(0, 0, 0, 0);

                    SetProperty("Version", value);
                };

                return value;
            }
        }

        /// <summary>Gets the version as a string .</summary>
        /// <value>The property comes from the corresponding assembly attribute.</value>
        public string VersionString
        {
            get { return Version.ToString(); }
        }		
        #endregion

        #region Private Members

        [ExcludeFromCodeCoverage]
        private T GetAttributeProperty<T> ( string key, T defaultValue, Type attributeType, string propertyName )
        {
            T value;
            if (TryGetProperty(key, out value))
                return value;

            //Try to get the attribute value
            value = defaultValue;            
            var attribute = (from a in m_assembly.GetCustomAttributes(attributeType, false)
                             select a).FirstOrDefault();
            if (attribute != null)
            {
                var prop = attributeType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance);
                if (prop != null)
                {
                    object obj = prop.GetValue(attribute, null);
                    if (obj != null)
                        value = (T)obj;
                };
            };
            
            SetProperty(key, value);
                        
            return value;
        }

        private DateTime GetBuildDate ( )
        {
            //We have to stream the assembly into memory so we can pick apart the PE header (mini parser present)
            using (FileStream stream = new FileStream(m_assembly.Location, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                return InternalGetBuildDate(stream);                
            };
        }

        //Broke this guy out so we can disable CA on it because it should never fail
        [ExcludeFromCodeCoverage]
        private static DateTime InternalGetBuildDate ( FileStream stream )
        {
            //MS-DOS header contains offset to PE signature at 0x3C
            stream.Seek(0x3C, SeekOrigin.Begin);
            int offsetPESignature = ReadInt32(stream);

            //Jump to the PE signature and make sure it is valid
            stream.Seek(offsetPESignature, SeekOrigin.Begin);
            if ((stream.ReadByte() != (int)'P') || (stream.ReadByte() != (int)'E') ||
                (stream.ReadByte() != 0) || (stream.ReadByte() != 0))
                return DateTime.MinValue;

            //Next is the COFF header which contains what we want at offset 4
            stream.Seek(4, SeekOrigin.Current);
            int timeStamp = ReadInt32(stream);

            //The stamp is the # of seconds since 1/1/1970 so start there
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(timeStamp);
        }

        private bool TryGetProperty<T> ( string key, out T value )
        {
            object obj = null;
            if (m_properties.TryGetValue(key, out obj))
            {
                value = (T)obj;
                return true;
            };

            value = default(T);
            return false;
        }

        private static int ReadInt32 ( Stream stream )
        {
            byte[] buffer = new byte[4];

            stream.Read(buffer, 0, 4);
            return BitConverter.ToInt32(buffer, 0);
        }
        
        private void SetProperty( string key, object value )
        {
            //Ensure it is thread safe
            lock (m_properties)
            {
                m_properties[key] = value;
            };
        }
        #region Data

        private Assembly m_assembly;
        private Dictionary<string, object> m_properties = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase); 
        #endregion

        #endregion
    }
}
