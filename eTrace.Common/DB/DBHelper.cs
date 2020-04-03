using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Configuration;

namespace eTrace.Common
{
    public class SQLEventArgs : EventArgs
    {
        public string Sql { get; private set; }
        public double? ExcutedMilliseconds { get; private set; }
        public SQLEventArgs(string sql, double? excutedMilliseconds = null)
        {
            this.Sql = sql;
            this.ExcutedMilliseconds = excutedMilliseconds;
        }
        public SQLEventArgs(string sql, SqlParameter[] cmdParms, double? excutedMilliseconds = null)
        {
            this.Sql = GetSQL(sql, cmdParms);
            this.ExcutedMilliseconds = excutedMilliseconds;
        }
        public SQLEventArgs(string sql, IDataParameter[] cmdParms, double? excutedMilliseconds = null)
        {
            this.Sql = GetSQL(sql, cmdParms);
            this.ExcutedMilliseconds = excutedMilliseconds;
        }

        private string GetSQL(string sql, SqlParameter[] cmdParms)
        {
            return string.Format("{0}  args: {1}", sql, string.Join(",", (from q in cmdParms
                                                                          select string.Format("{0}-{1}", q.ParameterName, q.Value)).ToList()));
        }
        private string GetSQL(string sql, IDataParameter[] cmdParms)
        {
            return string.Format("{0}  args: {1}", sql, string.Join(",", (from q in cmdParms
                                                                          select string.Format("{0}-{1}", q.ParameterName, q.Value)).ToList()));
        }
        public SQLEventArgs(IList<string> sqls, double? excutedMilliseconds = null)
        {
            this.Sql = string.Join(Environment.NewLine, sqls);
            this.ExcutedMilliseconds = excutedMilliseconds;
        }
        public SQLEventArgs(ArrayList sqls, double? excutedMilliseconds = null)
        {
            this.Sql = string.Join(Environment.NewLine, sqls);
            this.ExcutedMilliseconds = excutedMilliseconds;
        }

        public SQLEventArgs(List<Tuple<string, SqlParameter[]>> sqlCmdParms, double? excutedMilliseconds = null)
        {
            this.Sql = string.Join(Environment.NewLine, (from q in sqlCmdParms
                                                         select GetSQL(q.Item1, q.Item2)).ToList());
            this.ExcutedMilliseconds = excutedMilliseconds;
        }
    }

    public class DumpSqlTicker
    {
        private Stopwatch stopWatch = null;
        public DumpSqlTicker()
        {
            stopWatch = new Stopwatch();
        }

        public void Start()
        {
            if (stopWatch != null)
                stopWatch.Start();
        }

        public void Stop()
        {
            if (stopWatch != null)
                stopWatch.Stop();
        }

        public double? GetMilliseconds()
        {

            if (stopWatch != null)
                return stopWatch.ElapsedMilliseconds;
            else
                return null;
        }
    }

    public class DBHelper
    {
        public EventHandler<SQLEventArgs> DumpSqlEvent;

        //数据库连接字符串(web.config来配置)
        //<add key="ConnectionString" value="server=127.0.0.1;database=DATABASE;uid=sa;pwd=" />
        //Configer.Singleton.DBConnection 
        public string ConnectionString
        {
            get;
            private set;
        }
        public DBHelper(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        #region 公用方法

        public int GetMaxID(string FieldName, string TableName)
        {
            int result = 1;
            string strSql = "select max(" + FieldName + ")+1 from " + TableName + " with(nolock)";

            if (DumpSqlEvent != null)
            {
                DumpSqlEvent(this, new SQLEventArgs(strSql));
            }
            object obj = GetSingle(strSql);
            if (obj != null)
            {
                result = int.Parse(obj.ToString());
            }
            return result;
        }
        public bool Exists(string strSql, params SqlParameter[] cmdParms)
        {
            if (DumpSqlEvent != null)
            {
                DumpSqlEvent(this, new SQLEventArgs(strSql, cmdParms));
            }
            object obj = GetSingle(strSql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 获取数据根据sql语句 带参数 的
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetTable(string sql, int timeOut = 30)
        {
            if (DumpSqlEvent != null)
            {
                DumpSqlEvent(this, new SQLEventArgs(sql));
            }
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        if (timeOut == 0)
                        {
                            timeOut = LocalConfiguration.Instance.eTraceCommandTimeout();            //default is 120 seconds
                        }
                        cmd.CommandTimeout = timeOut;

                        connection.Open();
                        //if (timeOut > 0)
                        //{
                        //    cmd.CommandTimeout = timeOut;
                        //}

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                    }
                    catch (System.Data.SqlClient.SqlException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                }
            }
            return ds.Tables[0] ?? new DataTable();
        }

        /// <summary>
        /// SqlBulkCopy批量插入数据
        /// </summary>
        /// <param name="connectionStr">链接字符串</param>
        /// <param name="dataTableName">表名</param>
        /// <param name="sourceDataTable">数据源</param>
        /// <param name="batchSize">一次事务插入的行数</param>
        public void SqlBulkCopyByDataTable(string dataTableName, DataTable sourceDataTable, int batchSize = 100000, int timeOut = 30)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(ConnectionString, SqlBulkCopyOptions.KeepNulls))
                {
                    try
                    {
                        sqlBulkCopy.BulkCopyTimeout = timeOut;
                        sqlBulkCopy.DestinationTableName = dataTableName;
                        sqlBulkCopy.BatchSize = batchSize;
                        for (int i = 0; i < sourceDataTable.Columns.Count; i++)
                        {
                            sqlBulkCopy.ColumnMappings.Add(sourceDataTable.Columns[i].ColumnName, sourceDataTable.Columns[i].ColumnName);
                        }
                        sqlBulkCopy.WriteToServer(sourceDataTable);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
        #endregion

        #region  执行简单SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string SQLString)
        {
            if (DumpSqlEvent != null)
            {
                DumpSqlEvent(this, new SQLEventArgs(SQLString));
            }
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        cmd.CommandTimeout = LocalConfiguration.Instance.eTraceCommandTimeout();            //default is 120 seconds
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                }
            }
        }


        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string SQLString, int timeOut)
        {
            if (DumpSqlEvent != null)
            {
                DumpSqlEvent(this, new SQLEventArgs(SQLString));
            }
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        //if (timeOut > 0)
                        //{
                        //    cmd.CommandTimeout = timeOut;
                        //}
                        if (timeOut == 0)
                        {
                            timeOut = LocalConfiguration.Instance.eTraceCommandTimeout();            //default is 120 seconds
                        }
                        cmd.CommandTimeout = timeOut;

                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>  
        public void ExecuteSqlTran(ArrayList SQLStringList)
        {
            if (DumpSqlEvent != null)
            {
                DumpSqlEvent(this, new SQLEventArgs(SQLStringList));
            }
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    tx.Rollback();
                    throw new Exception(E.Message);
                }
            }
        }
        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string SQLString, string content)
        {
            if (DumpSqlEvent != null)
            {
                DumpSqlEvent(this, new SQLEventArgs(SQLString));
            }
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(SQLString, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    cmd.CommandTimeout = LocalConfiguration.Instance.eTraceCommandTimeout();            //default is 120 seconds

                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(strSQL, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@fs", SqlDbType.Image);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try
                {
                    cmd.CommandTimeout = LocalConfiguration.Instance.eTraceCommandTimeout();            //default is 120 seconds
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public long GetCount(string SQLString, int timeOut = 0)
        {
            if (DumpSqlEvent != null)
            {
                DumpSqlEvent(this, new SQLEventArgs(SQLString));
            }
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        //if (timeOut > 0)
                        //{
                        //    cmd.CommandTimeout = timeOut;
                        //}
                        if (timeOut == 0)
                        {
                            timeOut = LocalConfiguration.Instance.eTraceCommandTimeout();            //default is 120 seconds
                        }
                        cmd.CommandTimeout = timeOut;

                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return 0;
                        }
                        else
                        {
                            return DBConvert.DB2Long(obj);
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public object GetSingle(string SQLString, int timeOut = 0)
        {
            if (DumpSqlEvent != null)
            {
                DumpSqlEvent(this, new SQLEventArgs(SQLString));
            }
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        //if (timeOut > 0)
                        //{
                        //    cmd.CommandTimeout = timeOut;
                        //}
                        if (timeOut == 0)
                        {
                            timeOut = LocalConfiguration.Instance.eTraceCommandTimeout();            //default is 120 seconds
                        }
                        cmd.CommandTimeout = timeOut;
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
            }
        }
        ///// <summary>
        ///// 执行查询语句，返回SqlDataReader
        ///// </summary>
        ///// <param name="strSQL">查询语句</param>
        ///// <returns>SqlDataReader</returns>
        //public SqlDataReader ExecuteReader(string strSQL)
        //{
        //    if (DumpSqlEvent != null)
        //    {
        //        DumpSqlEvent(this, new SQLEventArgs(strSQL));
        //    }
        //    SqlConnection connection = new SqlConnection(ConnectionString);
        //    SqlCommand cmd = new SqlCommand(strSQL, connection);
        //    try
        //    {
        //        connection.Open();
        //        SqlDataReader myReader = cmd.ExecuteReader();
        //        return myReader;
        //    }
        //    catch (System.Data.SqlClient.SqlException e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        /// <summary>
        /// 执行查询语句，返回SqlDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public void ExecuteReader(string strSQL, Action<SqlDataReader> dr, Action<bool> isOver)
        {
            if (DumpSqlEvent != null)
            {
                DumpSqlEvent(this, new SQLEventArgs(strSQL));
            }
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(strSQL, connection))
                {
                    try
                    {
                        cmd.CommandTimeout = LocalConfiguration.Instance.eTraceCommandTimeout();            //default is 120 seconds
                        connection.Open();
                        if (dr != null)
                        {
                            using (SqlDataReader myReader = cmd.ExecuteReader())
                            {
                                long counter = 1;
                                while (myReader.Read())
                                {
                                    if (counter > eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount())
                                    {
                                        cmd.Cancel();
                                        break;
                                    }
                                    dr(myReader);
                                    counter++;
                                }
                                if (isOver != null)
                                {
                                    isOver(counter > eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount() ? true : false);
                                }
                            }
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="isThrowEx"></param>
        /// <returns></returns>
        public  T GetOject<T>  (SqlDataReader reader,bool isThrowEx=false ) where T:new ()
        {
            T t = new T();
            System.Reflection.PropertyInfo[] PropertyList = t.GetType().GetProperties();
            foreach (var prop in PropertyList)
            {
                try
                {
                    prop.SetValue(t, reader[prop.Name]);
                }
                catch (Exception ex)
                {
                    if (isThrowEx)
                    {
                        throw ex;
                    }
                    continue;
                }
            }
            return t;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strSQL"></param>
        /// <param name="GetTypeObject">显式指明转换方式速度会更快</param>
        /// <returns></returns>
        public List<T> GetList<T>(string strSQL, Func<SqlDataReader, T> GetTypeObject) where T : new()
        {
            List<T> dataList = new List<T>();
            if (DumpSqlEvent != null)
            {
                DumpSqlEvent(this, new SQLEventArgs(strSQL));
            }
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(strSQL, connection))
                {
                    try
                    {
                        cmd.CommandTimeout = 1200;
                        connection.Open();
                        using (SqlDataReader myReader = cmd.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                dataList.Add(GetTypeObject(myReader));
                            }
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
            }
            return dataList;
        }
        //public List<T> GetList<T>(string strSQL) where T : new()
        //{
        //    List<T> dataList = new List<T>();
        //    if (DumpSqlEvent != null)
        //    {
        //        DumpSqlEvent(this, new SQLEventArgs(strSQL));
        //    }
        //    using (SqlConnection connection = new SqlConnection(ConnectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(strSQL, connection))
        //        {
        //            try
        //            {
        //                connection.Open();
        //                using (SqlDataReader myReader = cmd.ExecuteReader())
        //                {
        //                    while (myReader.Read())
        //                    {
        //                        dataList.Add(GetOject<T>(myReader));
        //                    }
        //                }
        //            }
        //            catch (System.Data.SqlClient.SqlException e)
        //            {
        //                throw new Exception(e.Message);
        //            }
        //            finally
        //            {
        //                if (connection.State == ConnectionState.Open)
        //                    connection.Close();
        //            }
        //        }
        //    }
        //    return dataList;
        //}
        public void ExecuteReader(string strSQL, Action<SqlDataReader> dr)
        {
            if (DumpSqlEvent != null)
            {
                DumpSqlEvent(this, new SQLEventArgs(strSQL));
            }
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(strSQL, connection))
                {
                    try
                    {
                        cmd.CommandTimeout = LocalConfiguration.Instance.eTraceCommandTimeout();            //default is 120 seconds
                        connection.Open();
                        if (dr != null)
                        {
                            using (SqlDataReader myReader = cmd.ExecuteReader())
                            {
                                while (myReader.Read())
                                {
                                    dr(myReader);
                                }
                            }
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回SqlDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public void ExecuteReader(string strSQL, SqlParameter[] cmdParms, Action<SqlDataReader> dr)
        {
            if (DumpSqlEvent != null)
            {
                DumpSqlEvent(this, new SQLEventArgs(strSQL, cmdParms));
            }
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(strSQL, connection))
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, strSQL, cmdParms);
                        if (dr != null)
                        {
                            cmd.CommandTimeout = LocalConfiguration.Instance.eTraceCommandTimeout();            //default is 120 seconds
                            using (SqlDataReader myReader = cmd.ExecuteReader())
                            {
                                while (myReader.Read())
                                    dr(myReader);
                            }
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        if (DumpSqlEvent != null)
                        {
                            DumpSqlEvent(this, new SQLEventArgs(strSQL));
                        }
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string SQLString)
        {
            if (DumpSqlEvent != null)
            {
                DumpSqlEvent(this, new SQLEventArgs(SQLString));
            }
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }


        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string SQLString, params SqlParameter[] cmdParms)
        {
            if (DumpSqlEvent != null)
            {
                DumpSqlEvent(this, new SQLEventArgs(SQLString, cmdParms));
            }
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException E)
                    {
                        throw new Exception(E.Message);
                    }
                }
            }
        }


        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public void ExecuteSqlTran(List<Tuple<string, SqlParameter[]>> SQLStringList)
        {
            if (DumpSqlEvent != null)
            {
                DumpSqlEvent(this, new SQLEventArgs(SQLStringList));
            }
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        //循环
                        foreach (var myDE in SQLStringList)
                        {
                            string cmdText = myDE.Item1;
                            SqlParameter[] cmdParms = myDE.Item2;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }


        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public object GetSingle(string SQLString, params SqlParameter[] cmdParms)
        {
            if (DumpSqlEvent != null)
            {
                DumpSqlEvent(this, new SQLEventArgs(SQLString, cmdParms));
            }
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        ///// <summary>
        ///// 执行查询语句，返回SqlDataReader
        ///// </summary>
        ///// <param name="strSQL">查询语句</param>
        ///// <returns>SqlDataReader</returns>
        //public SqlDataReader ExecuteReader(string SQLString, params SqlParameter[] cmdParms)
        //{
        //    if (DumpSqlEvent != null)
        //    {
        //        DumpSqlEvent(this, new SQLEventArgs(SQLString, cmdParms));
        //    }
        //    SqlConnection connection = new SqlConnection(ConnectionString);
        //    SqlCommand cmd = new SqlCommand();
        //    try
        //    {
        //        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
        //        SqlDataReader myReader = cmd.ExecuteReader();
        //        cmd.Parameters.Clear();
        //        return myReader;
        //    }
        //    catch (System.Data.SqlClient.SqlException e)
        //    {
        //        throw new Exception(e.Message);
        //    }

        //}

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string SQLString, params SqlParameter[] cmdParms)
        {
            if (DumpSqlEvent != null)
            {
                DumpSqlEvent(this, new SQLEventArgs(SQLString, cmdParms));
            }
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }


        private void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        #endregion

        #region 存储过程操作

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            if (DumpSqlEvent != null)
            {
                DumpSqlEvent(this, new SQLEventArgs(storedProcName, parameters));
            }
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlDataReader returnReader;
                connection.Open();
                SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
                command.CommandType = CommandType.StoredProcedure;
                returnReader = command.ExecuteReader();
                return returnReader;
            }
        }


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            if (DumpSqlEvent != null)
            {
                DumpSqlEvent(this, new SQLEventArgs(storedProcName, parameters));
            }
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }

        public DataSet RunProcedureDS(string storedProcName, IDataParameter[] parameters)
        {
            if (DumpSqlEvent != null)
            {
                DumpSqlEvent(this, new SQLEventArgs(storedProcName, parameters));
            }
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.SelectCommand.CommandTimeout = LocalConfiguration.Instance.eTraceCommandTimeout();            //default is 120 seconds

                sqlDA.Fill(dataSet);
                connection.Close();
                return dataSet;
            }
        }
        public List<T> GetList<T>(string sql, params SqlParameter[] parameter)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (SqlParameter par in parameter)
                    {
                        cmd.Parameters.Add(par);
                    }
                    DataSet ds = new DataSet();
                    using (SqlDataAdapter dapter = new SqlDataAdapter(cmd))
                    {
                        dapter.Fill(ds);
                        return DataSetToList<T>(ds, 0);
                    }
                }
            }
        }
        public List<T> DataSetToList<T>(DataSet ds, int tableIndext)
        {
            if (ds == null || ds.Tables.Count <= 0 || tableIndext < 0)
            {
                return null;
            }
            DataTable dt = ds.Tables[tableIndext]; //取得DataSet里的一个下标为tableIndext的表，然后赋给dt
            IList<T> list = new List<T>();  //实例化一个list
            PropertyInfo[] tMembersAll = typeof(T).GetProperties();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T t = Activator.CreateInstance<T>();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    foreach (PropertyInfo tMember in tMembersAll)
                    {
                        if (dt.Columns[j].ColumnName.ToUpper().Equals(tMember.Name.ToUpper()))
                        {
                            if (dt.Rows[i][j] != DBNull.Value)
                            {
                                tMember.SetValue(t, dt.Rows[i][j], null);
                            }
                            else
                            {
                                tMember.SetValue(t, null, null);
                            }
                        }
                    }
                }
                list.Add(t);
            }
            return list.ToList();
        }

        /// <summary>
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            if (DumpSqlEvent != null)
            {
                DumpSqlEvent(this, new SQLEventArgs(storedProcName, parameters));
            }
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            return command;
        }

        /// <summary>
        /// 执行存储过程，返回影响的行数  
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            if (DumpSqlEvent != null)
            {
                DumpSqlEvent(this, new SQLEventArgs(storedProcName, parameters));
            }
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                int result;
                connection.Open();
                SqlCommand command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                result = (int)command.Parameters["ReturnValue"].Value;
                //Connection.Close();
                return result;
            }
        }

        /// <summary>
        /// 创建 SqlCommand 对象实例(用来返回一个整数值) 
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand 对象实例</returns>
        private SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            if (DumpSqlEvent != null)
            {
                DumpSqlEvent(this, new SQLEventArgs(storedProcName, parameters));
            }
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue",
             SqlDbType.Int, 4, ParameterDirection.ReturnValue,
             false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        #endregion
    }
}
