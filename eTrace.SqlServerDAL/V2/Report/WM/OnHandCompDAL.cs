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
    public class OnHandCompDAL : DALBase, IOnHandCompDAL
    {                                                    
        #region corts

        public OnHandCompDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public OnHandCompDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields

        private const string sql_OnHandComp_select = @" ";


        #endregion

        #region rowCount
        private long rowCount;
        #endregion

        #region methods

        #region methods GetOnHandCompDatas
        /// <summary>
        /// 获取 CLID Master Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportOnHandCompModel GetOnHandCompData(ReportOnHandCompModelQuery query)
        {
            ReportOnHandCompModel result = new ReportOnHandCompModel();
            result.Data = new List<ReportOnHandCompModel.Item>();
            //string sql = sql_OnHandComp_select;
            //#region Conditions
            //sql += GetReportSqlCondition(query);
            //#endregion

            //string ordercol = " CLID";
            //PageSql pSql = GetPagerSQL(query, sql, "", ordercol);

            //if (!string.IsNullOrEmpty(pSql.SQLCount))
            //{
            //    result.Pager = new ModelPager();
            //    result.Pager.TotalCount = dbHelper.GetPageCount(pSql.SQLCount);
            //}
            //dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            //{
            //    result.Data.Add(GetReportOnHandCompModel(dr));
            //}, (isover) =>
            //{ result.IsOverMaxRow = isover; });

            string sessionid = Guid.NewGuid().ToString();
            IDataParameter[] parameters = new SqlParameter[7];
            SqlParameter[] parms = {
                new SqlParameter("@sessionid", SqlDbType.VarChar, 50),
                new SqlParameter("@orgcode", SqlDbType.VarChar, 50),
                new SqlParameter("@subinv", SqlDbType.VarChar, 50),
                new SqlParameter("@locator", SqlDbType.VarChar, 50),
                new SqlParameter("@lotnumber", SqlDbType.VarChar, 50),
                new SqlParameter("@itemnum", SqlDbType.VarChar, 1024),
                new SqlParameter("@showdiscrepancy", SqlDbType.VarChar, 50)
            };
            parms[0].Value = sessionid;
            parms[1].Value = query.OrgCode;
            parms[2].Value = query.SubInv;
            parms[3].Value = query.Locator;
            parms[4].Value = query.LotNo;
            parms[5].Value = query.ItemNo;
            parms[6].Value = query.DiffFlag;
            parameters = parms;
            DataSet ds = dbHelper.RunProcedureDS("sp_GetOnhandRept", parameters);
            result.Data = dbHelper.DataSetToList<ReportOnHandCompModel.Item>(ds, 0);
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
            return result;
        }


        /// <summary>
        /// 获取物料Summary Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long OnHandCompDataGetRowCount(ReportOnHandCompModelQuery query)
        {
            string sql = sql_OnHandComp_select;
            #region Conditions
            sql += GetReportSqlCondition(query);
            #endregion
            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        private string GetReportSqlCondition(ReportOnHandCompModelQuery query)
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

        private ReportOnHandCompModel.Item GetReportOnHandCompModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportOnHandCompModel.Item data = new ReportOnHandCompModel.Item

            {
                Org = DBConvert.DB2String(dr["OrgCode"]),               
            };
            return data;
        }



        #endregion

        #endregion
    }
}
