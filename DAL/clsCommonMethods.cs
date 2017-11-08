using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace DAL
{
    public struct strStoredProcedureParam
    {
        public string strParameterName;
        public SqlDbType dbtParameterType;
        public int maxLength;
        public override string ToString()
        {
            return strParameterName + " " + dbtParameterType.ToString();
        }
    }

    public struct strCRUD
    {
        public string strType;
        public enSqlCommandType commandType;
        public string strStoredProcedure;
        public override int GetHashCode()
        {
            return (strType + commandType + strStoredProcedure).GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return (strType + commandType + strStoredProcedure).Equals(obj);
        }
    }

    public enum enSqlCommandType
    {
        Insert, Delete, Update, Select, SelectAll, SelectFilter, Custom
    }

    public static class clsCommonMethods
    {
        private const string CATALOG = @"StudentenAdministratie";
        private static string DATASOURCE = @"172.16.99.153";
        private static string ip1 = "172.16.99.153"; //test
        private static string ip2 = "7.207.14.14"; //vpn test
        private static string ip3 = "192.168.18.145"; //live
        private static string ip4 = "7.223.184.255"; //vpn live
        private static string localh = "127.0.0.1";
        //private const string DATASOURCE = @"KURT-PC";
        //private static string DATASOURCE = @"7.207.14.14";
        //Provider=SQLNCLI11.1;Persist Security Info=False;User ID=Pcvogr4SQL;Initial Catalog=StudentenAdministratie;Data Source=172.16.99.153;Initial File Name="";Server SPN=""
        public static SqlConnection GetConnection
        {
            get { return new SqlConnection(ConnectionString); }
        }

        private static bool _HasConnection;

        public static bool HasConnection
        {
            get
            {
                _HasConnection = false;
                try
                {
                    using (SqlConnection sq = GetConnection)
                    {
                        sq.Open();

                        _HasConnection = sq.State == ConnectionState.Open;
                        sq.Close();
                    }
                }
                catch (SqlException sq)
                {
                    System.Console.Error.WriteLine(sq.Message);
                    Console.WriteLine(sq.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return _HasConnection;
            }
            set { _HasConnection = value; }
        }


        private static bool ping(string ip)
        {
            Ping pingsender = new Ping();
            IPAddress addr = IPAddress.Parse(ip);
            return pingsender.Send(addr, 200).Status == IPStatus.Success;
        }

        public static string ConnectionString
        {
            get
            {
                SqlConnectionStringBuilder sqlb = new SqlConnectionStringBuilder();
                
                sqlb.PersistSecurityInfo = false;
                sqlb.InitialCatalog = CATALOG;
                sqlb.IntegratedSecurity = false;
                sqlb.UserID = "Pcvogr4SQL";
                sqlb.Password = "Pcvogr4.";
                sqlb.ConnectTimeout = 5;

                DATASOURCE = ip3;
                sqlb.DataSource = DATASOURCE;
                return sqlb.ConnectionString;
            }
        }

        //caches
        private static Dictionary<string, List<strStoredProcedureParam>> dictStoredProceduresCache = new Dictionary<string, List<strStoredProcedureParam>>();
        private static HashSet<strCRUD> hsCRUDCache = new HashSet<strCRUD>();
        //check all => strsp = like 'usp' en dan de namen bijhouden
        /// <summary>
        /// Caches the parameters of the stored procedure to the adapter.
        /// </summary>
        /// <param name="strStoredProcedure"></param>
        public static void GetStoredProcedureParams(string strStoredProcedure)
        {
            SqlCommand cmd = new SqlCommand(@"select PARAMETER_NAME, DATA_TYPE, CASE WHEN CHARACTER_MAXIMUM_LENGTH IS NULL THEN 0 ELSE CHARACTER_MAXIMUM_LENGTH END AS CHARACTER_MAXIMUM_LENGTH from information_schema.parameters
where specific_name = '" + strStoredProcedure + "'", GetConnection);
            SqlParameter sp = new SqlParameter("sp", SqlDbType.NVarChar, 50);
            sp.Value = strStoredProcedure;
            //cmd.Parameters.Add(sp);
            cmd.CommandType = CommandType.Text;
            try
            {
                cmd.Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                cmd.Connection.Close();
                dictStoredProceduresCache.Add(strStoredProcedure, new List<strStoredProcedureParam>());
                foreach (DataRow dr in dt.Rows)
                {
                    strStoredProcedureParam p = new strStoredProcedureParam();
                    p.strParameterName = dr[0].ToString();
                    p.dbtParameterType = (SqlDbType)Enum.Parse(typeof(SqlDbType), dr[1].ToString(), true);
                    p.maxLength = Convert.ToInt32(dr[2]);
                    dictStoredProceduresCache[strStoredProcedure].Add(p);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }




        /// <summary>
        /// Converts the Properties of the object into the sql usp parameters
        /// </summary>
        /// <param name="spParams"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static List<SqlParameter> ConvertToSqlParameter(List<strStoredProcedureParam> spParams, List<Tuple<string, object>> parameters)
        {
            List<SqlParameter> sqlparams = new List<SqlParameter>();
            //List<SqlParameter> test = spParams.Where(x => parameters.FindIndex(p => p.Item1 == x.strParameterName.Replace("@", "")));
            if (!CheckParameters(spParams, parameters))
                throw new ArgumentException("Could not find all required parameters");
            for (int i = 0; i < spParams.Count; i++)
            {
                strStoredProcedureParam v = spParams[i];
                object o = parameters.Find(x => x.Item1.ToLower().Equals(v.strParameterName.Replace("@", "").ToLower())).Item2;
                SqlParameter para = new SqlParameter();
                para.ParameterName = v.strParameterName;
                para.SqlDbType = v.dbtParameterType;
                if (o == null)
                {
                    para.Value = DBNull.Value;
                }
                else
                {
                    if (v.dbtParameterType == SqlDbType.NVarChar)
                    {
                        para.Size = v.maxLength;
                        Console.WriteLine(spParams[i].strParameterName);
                        if (o is string)
                        {
                            if (((string)o).Length <= v.maxLength)
                            {
                                para.Value = o;
                            }
                            else
                            {
                                throw new ArgumentException("ConvertToSqlParameter, parameter " + v.strParameterName + " index " + i + " expected string with length " + v.maxLength + ", received" + ((string)o).Length);
                            }
                        }
                        else
                        {
                            //o is geen string, maar verwacht type is nvar
                            //throw new ArgumentException("ConvertToSqlParameter, parameter " + v.strParameterName + " index " + i + " expected string, received " + o.GetType().ToString());
                        }
                    }
                    else
                    if (v.dbtParameterType == SqlDbType.DateTime)
                    {
                        if (o is DateTime)
                        {
                            para.Value = (DateTime)o;
                        }

                    }
                    else
                    {
                        para.Value = o;
                    }

                }
                if (v.strParameterName == "@Returnvalue")
                {
                    para.Direction = ParameterDirection.Output;
                }
                sqlparams.Add(para);
            }
            return sqlparams;
        }
        /// <summary>
        /// Converts the supplied parameters values into the stored procedure parameters
        /// </summary>
        /// <param name="TargetList"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static List<SqlParameter> ConvertToSqlParameter(List<strStoredProcedureParam> TargetList, object[] values)
        {
            List<SqlParameter> sqlparams = new List<SqlParameter>();
            if (TargetList.Count != values.Length)
                throw new ArgumentException("ConvertToSqlParameter, expected " + TargetList.Count + " parameters. Received " + values.Length);
            for (int i = 0; i < TargetList.Count; i++)
            {
                strStoredProcedureParam v = TargetList[i];
                object o = values[i];
                SqlParameter para = new SqlParameter();
                para.ParameterName = v.strParameterName;
                para.SqlDbType = v.dbtParameterType;
                if (o == null)
                {
                    para.Value = DBNull.Value;
                }
                else
                {

                    if (v.dbtParameterType == SqlDbType.NVarChar)
                    {
                        para.Size = v.maxLength;
                        if (o is string)
                        {
                            if (((string)o).Length <= v.maxLength)
                            {
                                para.Value = o;
                            }
                            else
                            {
                                throw new ArgumentException("ConvertToSqlParameter, parameter " + v.strParameterName + " index " + i + " expected string with length " + v.maxLength + ", received" + ((string)o).Length);
                            }
                        }
                        else
                        {
                            //throw new ArgumentException("ConvertToSqlParameter, parameter " + v.strParameterName + " index " + i + " expected string, received " + o != null?o.GetType().ToString():"null");
                        }
                    }
                    else
                    if (v.dbtParameterType == SqlDbType.DateTime)
                    {
                        if (o is DateTime)
                        {
                            para.Value = (DateTime)o;
                        }

                    }
                    else
                    {
                        para.Value = o;
                    }
                }
                if (v.strParameterName == "@Returnvalue")
                {
                    para.Direction = ParameterDirection.Output;
                }
                sqlparams.Add(para);
            }
            return sqlparams;
        }

        private static bool CheckParameters(List<strStoredProcedureParam> spParams, List<Tuple<string, object>> Properties)
        {
            bool checkComplete = true;
            if (spParams.Count > 0)
                foreach (var v in spParams)
                {
                    if (!(Properties.FindIndex(x => x.Item1.ToLower().Equals(v.strParameterName.Replace("@", "").ToLower())) > -1))
                    {
                        checkComplete = false;
                        break;
                    }
                }
            return checkComplete;
        }

        /// <summary>
        /// Gets the datatable for object T
        /// </summary>
        /// <typeparam name="T">Type of the BO</typeparam>
        /// <param name="cmdType">Stored procedure type</param>
        /// <param name="parameters">List of the parameters that are filled in</param>
        /// <returns></returns>
        public static DataTable GetDataTable<T>(enSqlCommandType cmdType, List<Tuple<string, object>> parameters)
        {
            int TryAgainCount = 1;
            DataTable dt = new DataTable();
            bool success = false;
            do
            {
                string strSP = GetSP<T>(cmdType);
                List<strStoredProcedureParam> spParams = new List<strStoredProcedureParam>();

                if (!dictStoredProceduresCache.TryGetValue(strSP, out spParams))
                {
                    GetStoredProcedureParams(strSP);
                    spParams = dictStoredProceduresCache[strSP];
                }

                if (CheckParameters(spParams, parameters))
                {
                    using (SqlCommand sq = new SqlCommand(strSP, GetConnection))
                    {
                        try
                        {
                            sq.Parameters.AddRange(ConvertToSqlParameter(spParams, parameters).ToArray());
                            sq.CommandType = CommandType.StoredProcedure;
                            if (cmdType == enSqlCommandType.Delete)
                            {
                                sq.Connection.Open();
                                sq.ExecuteNonQuery();
                                success = true;
                            }
                            else
                            {
                                using (SqlDataAdapter da = new SqlDataAdapter(sq))
                                {
                                    using (DataSet ds = new DataSet())
                                    {
                                        sq.Connection.Open();
                                        da.Fill(ds);
                                        int count = ds.Tables.Count;
                                        if (count == 0)
                                            success = false;
                                        if (count == 2)
                                        {
                                            dt = ds.Tables[1];
                                            success = true;
                                        }
                                        else
                                        {
                                            dt = ds.Tables[0];
                                            success = true;
                                        }
                                    }
                                }
                            }
                        }
                        catch (SqlException e)
                        {
                            //clear stored procedure cache
                            Console.WriteLine(e);
                            TryAgainCount--;
                            dictStoredProceduresCache.Remove(strSP);
                        }
                    }
                }
                else
                {

                    throw new ArgumentException("Select " + strSP + "expected parametercount " + spParams.Count + " received " + parameters.Count);
                }
            } while (TryAgainCount >= 0 && !success);

            return dt;
        }




        /// <summary>
        /// Gets the Datatable from the specified sqltype and objectname, or from the storedprocedure name.
        /// BLL will parse the Datatable
        /// while loop is to check for changes stored procedures
        /// </summary>
        /// <typeparam name="T">Type of the BO</typeparam>
        /// <param name="cmdType">Command type of the procedure</param>
        /// <param name="strSP">string of the usp (if empty it wont be used)</param>
        /// <param name="parameters">List of parameters required by the stored procedure</param>
        /// <returns></returns>
        public static DataTable GetDataTableCustom<T>(enSqlCommandType cmdType, string strSP = "", params object[] parameters)
        {
            DataTable dt = new DataTable();
            int TryAgainCount = 1;
            bool success = false;
            if (string.IsNullOrEmpty(strSP))
            {
                strSP = GetSP<T>(cmdType);
            }
            do
            {
                List<strStoredProcedureParam> spParams = new List<strStoredProcedureParam>();

                if (!dictStoredProceduresCache.TryGetValue(strSP, out spParams))
                {
                    GetStoredProcedureParams(strSP);
                    spParams = dictStoredProceduresCache[strSP];
                }

                if (spParams.Count == parameters.Length)
                {
                    using (SqlCommand sq = new SqlCommand(strSP, GetConnection))
                    {
                        sq.Parameters.AddRange(ConvertToSqlParameter(spParams, parameters).ToArray());
                        sq.CommandType = CommandType.StoredProcedure;
                        try
                        {
                            if (cmdType == enSqlCommandType.Delete)
                            {
                                sq.Connection.Open();
                                sq.ExecuteNonQuery();
                                success = true;
                            }
                            else
                            {
                                using (SqlDataAdapter da = new SqlDataAdapter(sq))
                                {
                                    sq.Connection.Open();
                                    da.Fill(dt);
                                    success = true;
                                }
                            }
                        }
                        catch (SqlException)
                        {
                            //maybe stored procedure has been updated?
                            if (TryAgainCount > 0)
                            {
                                dictStoredProceduresCache.Remove(strSP);
                                TryAgainCount--;
                            }
                            else
                            {
                                throw;
                            }

                        }
                    }
                }
                else
                {
                    string parameterslist = "";
                    spParams.ToList().ForEach(p => parameterslist += p.strParameterName + " " + p.dbtParameterType.ToString() + Environment.NewLine);
                    throw new ArgumentException("Could not find required parameters:" + Environment.NewLine + parameterslist);
                }
            } while (TryAgainCount >= 0 && !success);

            return dt;
        }

        public static DataTable GetDataTableParams(enSqlCommandType cmdType, string strSP, params object[] parameters)
        {
            List<strStoredProcedureParam> spParams = new List<strStoredProcedureParam>();
            DataTable dt = new DataTable();
            if (!dictStoredProceduresCache.TryGetValue(strSP, out spParams))
            {
                GetStoredProcedureParams(strSP);
                spParams = dictStoredProceduresCache[strSP];
            }
            if (spParams.Count > 0)
            {
                if (spParams.Count == parameters.Length)
                {
                    using (SqlCommand sq = new SqlCommand(strSP, GetConnection))
                    {
                        sq.Parameters.AddRange(ConvertToSqlParameter(spParams, parameters).ToArray());
                        sq.CommandType = CommandType.StoredProcedure;
                        if (cmdType == enSqlCommandType.Delete) //ook bij update, create?
                        {
                            sq.Connection.Open();
                            if (sq.ExecuteNonQuery() > 0)
                            {
                                //success
                            }
                        }
                        else
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(sq))
                            {
                                sq.Connection.Open();
                                da.Fill(dt);
                            }
                        }
                    }
                }
                else
                {
                    string parameterslist = "";
                    spParams.ToList().ForEach(p => parameterslist += p.strParameterName + " " + p.dbtParameterType.ToString() + Environment.NewLine);
                    throw new ArgumentException("Could not find required parameters:" + Environment.NewLine + parameterslist);
                }
            }
            return dt;
        }

        /// <summary>
        /// SQLConsole
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="strSP"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable GetDataTableParams(string strSP)
        {
            DataTable dt = new DataTable();

            using (SqlCommand sq = new SqlCommand(strSP, GetConnection))
            {
                sq.CommandText = strSP;
                sq.CommandType = CommandType.Text;

                using (SqlDataAdapter da = new SqlDataAdapter(sq))
                {
                    sq.Connection.Open();
                    try
                    {
                        da.Fill(dt);
                    }
                    catch (Exception)
                    {

                        
                    }

                }

            }


            return dt;
        }

        public const string strUSP = "usp_";
        public static string GetSP<T>(enSqlCommandType commandType)
        {
            return string.Format("{0}{1}{2}", strUSP, typeof(T).ToString().Replace("StudentApplication.Model.cls", ""), commandType.ToString());
        }

    }
}
