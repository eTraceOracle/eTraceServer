using eTrace.Common;
using eTrace.Report.IDAL;
using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using System.Configuration;
using System.Data;

namespace eTrace.Report.SqlServerDAL.V2.Report
{
    public class iProAMLvseTraceDAL : DALBase, IiProAMLvseTraceDAL
    {                                                    
        #region corts

        public iProAMLvseTraceDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public iProAMLvseTraceDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields

        private const string sql_iProAMLvseTrace_select = @" ";


        #endregion

        #region rowCount
        private long rowCount;
        #endregion

        #region methods

        #region methods GetiProAMLvseTraceDatas
        /// <summary>
        /// 获取 CLID Master Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportiProAMLvseTraceModel GetiProAMLvseTraceData(ReportiProAMLvseTraceModelQuery query)
        {
            ReportiProAMLvseTraceModel result = new ReportiProAMLvseTraceModel();
            result.Data = new List<ReportiProAMLvseTraceModel.Item>();

            try
            {
                DataSet ds = eTraceWS.Report_AMLIProVSeTrace2(query.OrgCode,query.SubInv,query.RTDateFrom,query.RTDateTo,query.AMLStatus,query.CLIDStatus);
                result.Data = dbHelper.DataSetToList<ReportiProAMLvseTraceModel.Item>(ds, 0);
                result.Pager = new ModelPager();

                if (result.Data != null)
                {
                    if (result.Data.Count > 0)
                    {
                        result.Pager.TotalCount = result.Data.Count;
                        rowCount = result.Data.Count;
                    }
                    else
                    {
                        result.Pager.TotalCount = 0;
                        rowCount = 0;
                    }
                }
                else
                {
                    result.Pager.TotalCount = 0;
                    rowCount = 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        /// <summary>
        /// 获取物料Summary Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long iProAMLvseTraceDataGetRowCount(ReportiProAMLvseTraceModelQuery query)
        {
            string sql = sql_iProAMLvseTrace_select;
            #region Conditions
            sql += GetReportSqlCondition(query);
            #endregion
            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        private string GetReportSqlCondition(ReportiProAMLvseTraceModelQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.OrgCode))
                {
                    sql += string.Format(" and OrgCode = '{0}' ", query.OrgCode); //"and " + "OrgCode= '" + query.OrgCode + "' ";
                }
              
                //if (sql.Length > 4000)
                //{
                //    Page.Response.Write("Your condition is too long to get the result. Input again");
                //    return;
                //}

                //sql = sql.Replace("'", "''") + "' ";
                //sql += "' ";

            }
            return sql;
        }

        private ReportiProAMLvseTraceModel.Item GetReportiProAMLvseTraceModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportiProAMLvseTraceModel.Item data = new ReportiProAMLvseTraceModel.Item

            {
                OrgCode = DBConvert.DB2String(dr["OrgCode"]),               
            };
            return data;
        }



        #endregion

        #endregion
    }
}
