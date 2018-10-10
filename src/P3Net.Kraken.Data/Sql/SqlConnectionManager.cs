/*
 * Copyright © 2005 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using P3Net.Kraken.Data.Common;

namespace P3Net.Kraken.Data.Sql
{
    /// <summary>Provides a <see cref="ConnectionManager"/> implementation for SQL Server.</summary>
    public class SqlConnectionManager : DbProviderFactoryConnectionManager
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="SqlConnectionManager"/> class.</summary>
        public SqlConnectionManager () : base(SqlClientFactory.Instance)
        {
            SupportsQueryParameters = true;
            SupportsUserContext = true;
        }

        /// <summary>Initializes an instance of the <see cref="SqlConnectionManager"/> class.</summary>
        /// <param name="connectionString">The connection string to use.</param>
        [Obsolete("Deprecated in 6.1. Use one of the With... extension methods instead.")]
        public SqlConnectionManager ( string connectionString ) : base(SqlClientFactory.Instance)
        {
            UseConnectionString(connectionString);
        }
        #endregion

        /// <summary>Formats the parameter name.</summary>
        /// <param name="originalName">The parameter name.</param>
        /// <returns>The formatted parameter name.</returns>
        protected override string FormatParameterName ( string originalName ) => originalName.EnsureStartsWith("@");

        /// <summary>Gets the schema information from the database.</summary>
        /// <returns>The schema information.</returns>
        protected override SchemaInformation LoadSchema ()
        {
            var schema = base.LoadSchema();

            //For some reason SQL does not return a properly formatted parameter name so fix it now
            schema.ParameterFormat = "@{0}";

            return schema;
        }

        /// <summary>Queries the database for the parameters to the given stored procedure.</summary>
        /// <param name="name">The name of the stored procedure.</param>
        /// <returns>An array of parameters.</returns>
        protected override DataParameter[] QueryParametersBase ( string name )
        {
            //Open a connection
            using (var conn = CreateConnectionBase(ConnectionString))
            {
                conn.Open();

                //Set up a restriction to get only the desired parameters
                //Currently there are 4 restrictions supported but we care about the third one
                var restrictions = BuildRestriction(name);
                var parms = new SortedList<int, DataParameter>();

                //Get the schema for the given stored procedure			
                var dt = conn.GetSchema("ProcedureParameters", restrictions);
                if (dt != null)
                {
                    //Allocate an array
                    int pos;

                    foreach (DataRow row in dt.Rows)
                    {
                        //Parse and insert in ordinal order
                        var parm = ParseParameter(row, out pos);
                        parms.Add(pos, parm);
                    };
                };

                return parms.Values.ToArray();
            };
        }

        /// <summary>Sets the user context.</summary>
        /// <param name="connection">The open connection.</param>
        /// <param name="userContext">The user context.</param>
        /// <remarks>
        /// The default implementation sets CONTEXT_INFO directly.
        /// </remarks>
        protected override void SetUserContextCore ( ConnectionData connection, string userContext )
        {
            base.SetUserContextCore(connection, userContext);

            if (String.IsNullOrEmpty(userContext))
                return;

            //Make sure the connection is open
            connection.Open();

            using (var cmd = new SqlCommand("SET CONTEXT_INFO @user", (SqlConnection)connection.Connection))
            {
                cmd.CommandType = CommandType.Text;
                var parm = cmd.Parameters.Add("@user", SqlDbType.VarBinary, 128);
                parm.Value = System.Text.Encoding.ASCII.GetBytes(userContext);

                cmd.ExecuteNonQuery();
            };
        }

        #region Private Members

        //Provides a mapping between SQL types and DbType
        private static Dictionary<string, DbType> MappedTypes => s_typeMapping.Value;

        private static string[] BuildRestriction ( string name )
        {
            var tokens = name.Split('.');

            //Format for parameter restrictions is: catalog, schema, procedure, parameter
            var restrictions = new string[4];
            switch (tokens.Length)
            {
                case 1:
                {
                    restrictions[2] = name;
                    break;
                };
                case 2:
                {
                    restrictions[1] = tokens[0];
                    restrictions[2] = tokens[1];
                    break;
                };
                case 3:
                {
                    restrictions[0] = tokens[0];
                    restrictions[1] = tokens[1];
                    restrictions[2] = tokens[2];
                    break;
                };
                default:
                {
                    restrictions[2] = name;
                    break;
                };
            };

            return restrictions;
        }

        private static DbType MapType ( string typeName )
        {
            if (MappedTypes.TryGetValue(typeName, out var type))
                return type;

            //Default
            return DbType.Object;
        }

        //Maps SQL types to their equivalent DbTypes
        private static Dictionary<string, DbType> MapTypes ()
        {
            var mappings = new Dictionary<string, DbType>(StringComparer.OrdinalIgnoreCase);

            mappings.Add("bigint", DbType.Int64);
            mappings.Add("binary", DbType.Binary);
            mappings.Add("bit", DbType.Boolean);
            mappings.Add("char", DbType.AnsiStringFixedLength);
            mappings.Add("datetime", DbType.DateTime);
            mappings.Add("decimal", DbType.Decimal);
            mappings.Add("float", DbType.Double);
            mappings.Add("image", DbType.Binary);
            mappings.Add("int", DbType.Int32);
            mappings.Add("money", DbType.Currency);
            mappings.Add("nchar", DbType.StringFixedLength);
            mappings.Add("ntext", DbType.String);
            mappings.Add("numeric", DbType.VarNumeric);
            mappings.Add("nvarchar", DbType.String);
            mappings.Add("real", DbType.Single);
            mappings.Add("smalldatetime", DbType.DateTime);
            mappings.Add("smallint", DbType.Int16);
            mappings.Add("smallmoney", DbType.Currency);
            mappings.Add("sql_variant", DbType.Object);
            mappings.Add("text", DbType.AnsiString);
            mappings.Add("timestamp", DbType.Binary);
            mappings.Add("tinyint", DbType.Byte);
            mappings.Add("uniqueidentifier", DbType.Guid);
            mappings.Add("varbinary", DbType.Binary);
            mappings.Add("varchar", DbType.AnsiString);
            mappings.Add("xml", DbType.Xml);

            return mappings;
        }

        private static DataParameter ParseParameter ( DataRow dr, out int position )
        {
            var name = "";
            var direction = ParameterDirection.Input;

            //Load the name and direction (they're tied together)
            if (!dr.IsNull("parameter_name"))
            {
                name = Convert.ToString(dr["parameter_name"]);

                //We ignore unknown values
                string strDir = dr["parameter_mode"].ToString();
                if (String.Compare(strDir, "in", StringComparison.OrdinalIgnoreCase) == 0)
                    direction = ParameterDirection.Input;
                else if (String.Compare(strDir, "out", StringComparison.OrdinalIgnoreCase) == 0)
                    direction = ParameterDirection.Output;
                else if (String.Compare(strDir, "inout", StringComparison.OrdinalIgnoreCase) == 0)
                    direction = ParameterDirection.InputOutput;
            } else
                direction = ParameterDirection.ReturnValue;

            var type = MapType(Convert.ToString(dr["data_type"]));

            var parm = new DataParameter(name, type, direction);

            //Precision, scale and size ohh my
            if (!dr.IsNull("numeric_precision"))
                parm.Precision = Convert.ToByte(dr["numeric_precision"]);
            if (!dr.IsNull("numeric_scale"))
                parm.Scale = Convert.ToByte(dr["numeric_scale"]);
            if (!dr.IsNull("character_maximum_length"))
                parm.Size = Convert.ToInt32(dr["character_maximum_length"]);

            //if (!dr.IsNull("is_nullable"))
            //parm.IsNullable = Convert.ToBoolean(dr["is_nullable"]);

            position = Convert.ToInt32(dr["ordinal_position"]);

            return parm;
        }

        private static readonly Lazy<Dictionary<string, DbType>> s_typeMapping = new Lazy<Dictionary<string, DbType>>(MapTypes);

        #endregion 
    }
}
