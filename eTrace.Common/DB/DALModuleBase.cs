using eTrace.Common;
using eTrace.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Common
{
    public class eTraceWebService
    {
        protected eTraceOracleERP.eTraceOracleERPSoapClient eTraceWS = new eTraceOracleERP.eTraceOracleERPSoapClient();  
        protected eTraceOracleERP.InputData InvOptReport_InputData = new eTraceOracleERP.InputData();
    }
    public abstract class DALBase: eTraceWebService
    {
        protected DBHelper dbHelper = null;
        protected EmDBType dbType;
        private DALBase()
        {
        }
        public DALBase(DBHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public DALBase(EmDBType dbType)
        {
            this.dbType = dbType;
            this.dbHelper = DBManagerConfig.Instance.GetDbHelper(dbType);
        }

        /// <summary>
        /// If Pager is Null, then we use optional param ordercol to set column order, as download report does not need pager
        /// </summary>
        /// <param name="query"></param>
        /// <param name="sql"></param>
        /// <param name="sqlCount"></param>
        /// <param name="defualOrderCol">
        /// optinal, for report download, we don't need pager, and set column order here 
        /// if query.Pager.Order is empty ,use defualOrderCol, such as " Status , LockedOn desc"
        /// </param>
        /// <returns></returns>
        public PageSql GetPagerSQL(ModelQueryBase query, string sql, string sqlCount = "", string defualOrderCol = "")
        {
            PageSql pSQL = new PageSql();
            pSQL.SQLDatas = sql; 
            if (sqlCount == "")
            {
                pSQL.SQLCount = GetSQLCount(sql);
            }
            else
            {
                pSQL.SQLCount = sqlCount;
            }
            if (query != null)
            {

                if (query.Pager != null)
                {
                    if (string.IsNullOrEmpty(query.Pager.Order))
                    {
                        query.Pager.Order = defualOrderCol;
                    }
                    //pSQL.SQLDatas = string.Format(sqlPageDataTemp,
                    //    sql,
                    //    string.IsNullOrEmpty(query.Pager.Order) ? string.Empty : string.Format("ORDER BY {0}", query.Pager.Order),
                    //    (query.Pager.CurrentPage - 1) * query.Pager.PageSize,
                    //      query.Pager.PageSize);
                    pSQL.SQLDatas = GetSQLDataByPage(sql, query.Pager);

                }
                else
                {
                    pSQL.SQLDatas = GetSQLData( sql, defualOrderCol);

                }

            }
            return pSQL;
        }
        /// <summary>
        /// get sql to select count
        /// </summary>
        /// <param name="query"></param>
        /// <param name="sql">Main SQL</param>
        /// <returns></returns>
        public string GetSQLCount( string sql)
        {
            string sqlTotalCountTemp = @"select count(1) from ({0}) as t";
            return  string.Format(sqlTotalCountTemp, sql);
        }

        /// <summary>
        /// 获取数据不分页
        /// </summary>
        /// <param name="sql">baseSql</param>
        /// <param name="orderCol">order by string</param>
        /// <returns></returns>
        public string GetSQLData(string sql,string orderCol="")
        {
            if (!string.IsNullOrEmpty(orderCol))
            {
                return string.Format(@"{0} ORDER BY {1}", sql, orderCol);
            }else
            {
                return sql;
            } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql">sql without order by,order by must be define in Pager</param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public string GetSQLDataByPage(string sql, QueryPager pager)
        {
            string sqlPageDataTemp = @"{0} {1}
                      OFFSET {2} ROWS
                FETCH NEXT {3} ROWS ONLY ";
            return string.Format(sqlPageDataTemp,
                        sql,
                        string.IsNullOrEmpty(pager.Order) ? string.Empty : string.Format("ORDER BY {0}", pager.Order),
                        (pager.CurrentPage - 1) * pager.PageSize,
                          pager.PageSize);
        }
    }

    public class PageSql
    {
        public string SQLCount { get; set; }
        public string SQLDatas { get; set; }
    }
}
