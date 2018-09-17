/*
 * Copyright © 2011 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;

#if NET_FRAMEWORK

using System.Data.Linq;
#endif

namespace P3Net.Kraken.Data
{
    /// <summary>Provides support for mapping between <see cref="DbType"/> values and CLR types.</summary>
    /// <remarks>
    /// This class is database agnostic.  It does not attempt to select the most appropriate, database specific type.
    /// </remarks>
    public static class DbTypeMapper
    {
        #region Construction

        static DbTypeMapper ( )
        {
            //Cache some standard mappings
            s_clrToDbMappings = new Dictionary<Type, DbType>() {
                {typeof(bool), DbType.Boolean},
                {typeof(byte), DbType.Byte},
                {typeof(short), DbType.Int16},
                {typeof(int), DbType.Int32},                    
                {typeof(long), DbType.Int64},  

                {typeof(sbyte), DbType.SByte},
                {typeof(ushort), DbType.UInt16},
                {typeof(uint), DbType.UInt32},                    
                {typeof(ulong), DbType.UInt64},  
                    
                {typeof(decimal), DbType.Decimal},
                {typeof(float), DbType.Single},
                {typeof(double), DbType.Double},
                    
                {typeof(char), DbType.String},      
                {typeof(char[]), DbType.String},
                {typeof(string), DbType.String},      

                {typeof(DateTime), DbType.DateTime},
                {typeof(DateTimeOffset), DbType.DateTimeOffset},
                {typeof(TimeSpan), DbType.Time},

                {typeof(Guid), DbType.Guid},

#if NET_FRAMEWORK
                {typeof(Binary), DbType.Binary},
#endif

                {typeof(byte[]), DbType.Binary},
                {typeof(XElement), DbType.Xml},

                {typeof(bool?), DbType.Boolean},
                {typeof(byte?), DbType.Byte},
                {typeof(short?), DbType.Int16},
                {typeof(int?), DbType.Int32},                    
                {typeof(long?), DbType.Int64},  

                {typeof(sbyte?), DbType.SByte},
                {typeof(ushort?), DbType.UInt16},
                {typeof(uint?), DbType.UInt32},                    
                {typeof(ulong?), DbType.UInt64},  
                    
                {typeof(decimal?), DbType.Decimal},
                {typeof(float?), DbType.Single},
                {typeof(double?), DbType.Double},
                    
                {typeof(char?), DbType.String}, 
                {typeof(DateTime?), DbType.DateTime},
                {typeof(DateTimeOffset?), DbType.DateTimeOffset},
                {typeof(TimeSpan?), DbType.Time},
                {typeof(Guid?), DbType.Guid },
                {typeof(Date), DbType.Date },
                {typeof(Date?), DbType.Date },

                {typeof(Money), DbType.Currency },
                {typeof(Money?), DbType.Currency }
            };

            s_dbToClrMappings = new Dictionary<DbType, Type>() {                
                {DbType.AnsiString, typeof(string)},
                {DbType.AnsiStringFixedLength, typeof(string)},
                {DbType.Binary, typeof(byte[])},
                {DbType.Byte, typeof(byte)},
                {DbType.Boolean, typeof(bool)},
                {DbType.Currency, typeof(Money)},
                {DbType.Date, typeof(Date)},
                {DbType.DateTime, typeof(DateTime)},
                {DbType.DateTime2, typeof(DateTime)},
                {DbType.DateTimeOffset, typeof(DateTimeOffset)},
                {DbType.Decimal, typeof(decimal)},
                {DbType.Double, typeof(double)},
                {DbType.Guid, typeof(Guid)},
                {DbType.Int16, typeof(short)},
                {DbType.Int32, typeof(int)},
                {DbType.Int64, typeof(long)},
                {DbType.Object, typeof(object)},
                {DbType.SByte, typeof(sbyte)},
                {DbType.Single, typeof(float)},
                {DbType.String, typeof(string)},
                {DbType.StringFixedLength, typeof(string)},
                {DbType.Time, typeof(TimeSpan)},
                {DbType.UInt16, typeof(ushort)},
                {DbType.UInt32, typeof(uint)},
                {DbType.UInt64, typeof(ulong)},
                {DbType.VarNumeric, typeof(decimal)},                               
                {DbType.Xml, typeof(XElement)},                
            };
        }
        #endregion

        #region Public Members

        /// <summary>Gets the <see cref="DbType"/> associated with a CLR type.</summary>
        /// <param name="type">The type to convert.</param>
        /// <returns>The associated DB type.  If no type is appropriate then it returns <see cref="DbType.Object"/>.</returns>
        /// <remarks>
        /// The mapping follows the standard LINQ-to-SQL mappings.
        /// <list type="table">
        ///    <listheader>
        ///       <term>CLR Type</term>
        ///       <description>DbType</description>
        ///    </listheader>        
        ///    <item>
        ///       <term>Boolean</term>
        ///       <description>Boolean</description>
        ///    </item>
        ///    <item>
        ///       <term>Byte</term>
        ///       <description>Byte</description>
        ///    </item>
        ///    <item>
        ///       <term>Byte[]</term>
        ///       <description>Binary</description>
        ///    </item>
        ///    <item>
        ///       <term>Char</term>
        ///       <description>String</description>
        ///    </item>
        ///    <item>
        ///       <term>Char[]</term>
        ///       <description>String</description>
        ///    </item>
        ///    <item>
        ///       <term><see cref="Date"/></term>
        ///       <description>Date</description>
        ///    </item>
        ///    <item>
        ///       <term>DateTime</term>
        ///       <description>DateTime</description>
        ///    </item>
        ///    <item>
        ///       <term>DateTimeOffset</term>
        ///       <description>DateTimeOffset</description>
        ///    </item>
        ///    <item>
        ///       <term>Decimal</term>
        ///       <description>Decimal</description>
        ///    </item>
        ///    <item>
        ///       <term>Double</term>
        ///       <description>Double</description>
        ///    </item>
        ///    <item>
        ///       <term>Guid</term>
        ///       <description>Guid</description>
        ///    </item>
        ///    <item>
        ///       <term>Int16</term>
        ///       <description>Int16</description>
        ///    </item>
        ///    <item>
        ///       <term>Int32</term>
        ///       <description>Int32</description>
        ///    </item>
        ///    <item>
        ///       <term>Int64</term>
        ///       <description>Int64</description>
        ///    </item>
        ///    <item>
        ///       <term><see cref="Money"/></term>
        ///       <description>Currency</description>
        ///    </item>
        ///    <item>
        ///       <term>SByte</term>
        ///       <description>SByte</description>
        ///    </item>
        ///    <item>
        ///       <term>Single</term>
        ///       <description>Single</description>
        ///    </item>
        ///    <item>
        ///       <term>String</term>
        ///       <description>String</description>
        ///    </item>
        ///    <item>
        ///       <term>System.Data.Linq.Binary</term>
        ///       <description>Binary</description>
        ///    </item>
        ///    <item>
        ///       <term>System.Xml.Linq.XElement</term>
        ///       <description>Xml</description>
        ///    </item>        
        ///    <item>
        ///       <term>TimeSpan</term>
        ///       <description>Time</description>
        ///    </item>
        ///    <item>
        ///       <term>UInt16</term>
        ///       <description>UInt16</description>
        ///    </item>
        ///    <item>
        ///       <term>UInt32</term>
        ///       <description>UInt32</description>
        ///    </item>
        ///    <item>
        ///       <term>UInt64</term>
        ///       <description>UInt64</description>
        ///    </item>
        /// </list>
        /// </remarks>
        public static DbType ToDbType ( Type type )
        {
            //Standard mappings
            DbType result;
            if (s_clrToDbMappings.TryGetValue(type, out result))
                return result;
            
            return DbType.Object;            
        }

        /// <summary>Gets the CLR type associated with a <see cref="DbType"/>.</summary>
        /// <param name="type">The type to convert.</param>
        /// <returns>The associated CLR type.  If no type is appropriate then it returns <see cref="Object"/>.</returns>
        /// <remarks>
        /// The mapping follows the standard LINQ-to-SQL mappings.
        /// <list type="table">
        ///    <listheader>
        ///       <term>DbType</term>
        ///       <description>CLR Type</description>
        ///    </listheader>        
        ///    <item>
        ///       <term>AnsiString</term>
        ///       <description>String</description>
        ///    </item>
        ///    <item>
        ///       <term>AnsiStringFixedLength</term>
        ///       <description>String</description>
        ///    </item>
        ///    <item>
        ///       <term>Binary</term>
        ///       <description>Byte[]</description>
        ///    </item>
        ///    <item>
        ///       <term>Byte</term>
        ///       <description>Byte</description>
        ///    </item>
        ///    <item>
        ///       <term>Boolean</term>
        ///       <description>Boolean</description>
        ///    </item>
        ///    <item>
        ///       <term>Currency</term>
        ///       <description>Money</description>
        ///    </item>
        ///    <item>
        ///       <term>Date</term>
        ///       <description>Date</description>
        ///    </item>
        ///    <item>
        ///       <term>DateTime</term>
        ///       <description>DateTime</description>
        ///    </item>
        ///    <item>
        ///       <term>DateTime2</term>
        ///       <description>DateTime</description>
        ///    </item>
        ///    <item>
        ///       <term>DateTimeOffset</term>
        ///       <description>DateTimeOffset</description>
        ///    </item>
        ///    <item>
        ///       <term>Decimal</term>
        ///       <description>Decimal</description>
        ///    </item>
        ///    <item>
        ///       <term>Double</term>
        ///       <description>Double</description>
        ///    </item>
        ///    <item>
        ///       <term>Guid</term>
        ///       <description>Guid</description>
        ///    </item>
        ///    <item>
        ///       <term>Int16</term>
        ///       <description>Int16</description>
        ///    </item>
        ///    <item>
        ///       <term>Int32</term>
        ///       <description>Int32</description>
        ///    </item>
        ///    <item>
        ///       <term>Int64</term>
        ///       <description>Int64</description>
        ///    </item>
        ///    <item>
        ///       <term>Object</term>
        ///       <description>Object</description>
        ///    </item>
        ///    <item>
        ///       <term>SByte</term>
        ///       <description>SByte</description>
        ///    </item>
        ///    <item>
        ///       <term>Single</term>
        ///       <description>Single</description>
        ///    </item>
        ///    <item>
        ///       <term>String</term>
        ///       <description>String</description>
        ///    </item>
        ///    <item>
        ///       <term>StringFixedLength</term>
        ///       <description>String</description>
        ///    </item>
        ///    <item>
        ///       <term>Time</term>
        ///       <description>TimeSpan</description>
        ///    </item>
        ///    <item>
        ///       <term>UInt16</term>
        ///       <description>UInt16</description>
        ///    </item>
        ///    <item>
        ///       <term>UInt32</term>
        ///       <description>UInt32</description>
        ///    </item>
        ///    <item>
        ///       <term>UInt64</term>
        ///       <description>UInt64</description>
        ///    </item>
        ///    <item>
        ///       <term>VarNumeric</term>
        ///       <description>Decimal</description>
        ///    </item>        
        ///    <item>
        ///       <term>Xml</term>
        ///       <description>System.Xml.Linq.XElement</description>
        ///    </item>
        /// </list>
        /// </remarks>
        public static Type ToClrType ( DbType type )
        {
            Type result;
            if (s_dbToClrMappings.TryGetValue(type, out result))
                return result;
                           
            return typeof(object);
        }
        #endregion

        #region Private Members

        private static readonly Dictionary<Type, DbType> s_clrToDbMappings;
        private static readonly Dictionary<DbType, Type> s_dbToClrMappings;
        #endregion
    }
}
