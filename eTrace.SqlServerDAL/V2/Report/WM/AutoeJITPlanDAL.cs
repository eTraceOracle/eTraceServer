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

namespace eTrace.Report.SqlServerDAL.V2.Report
{
    public class AutoeJITPlanDAL : DALBase, IAutoeJITPlanDAL
    {                                                    
        #region corts

        public AutoeJITPlanDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public AutoeJITPlanDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields

        private const string sql_AutoeJITPlan_select = @"SELECT distinct ORG_CODE as OrgCode,ITEM_NO,SUBINV,LOCATOR,EJIT_REQ_QTY,ORDER_QTY,EJIT_TRIGGER_TYPE,DESCRIPTION,NEED_BY_DATE,Creation_date,Plan_Arrival_date,GAP,PRIMARY_UNIT_OF_MEASURE,ITEM_TYPE,FREQUENCY,LEAD_TIME,MPQ,VENDOR_NAME "
                             + " FROM T_AutoeJITPlan WITH (nolock) where 1=1 ";   


        #endregion

        #region methods

        #region methods GetAutoeJITPlanDatas
        /// <summary>
        /// 获取 CLID Master Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportAutoeJITPlanModel GetAutoeJITPlanData(ReportAutoeJITPlanModelQuery query)
        {
            ReportAutoeJITPlanModel result = new ReportAutoeJITPlanModel();
            result.Data = new List<ReportAutoeJITPlanModel.Item>();
            string sql = sql_AutoeJITPlan_select;
            #region Conditions
            sql += GetReportSqlCondition(query);
            #endregion

            string ordercol = " LOCATOR ASC, NEED_BY_DATE ASC, ITEM_NO ASC ";
            PageSql pSql = GetPagerSQL(query, sql, "", ordercol);

            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportAutoeJITPlanModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }


        /// <summary>
        /// 获取物料Summary Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long AutoeJITPlanDataGetRowCount(ReportAutoeJITPlanModelQuery query)
        {
            string sql = sql_AutoeJITPlan_select;
            #region Conditions
            sql += GetReportSqlCondition(query);
            #endregion
            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        private string GetReportSqlCondition(ReportAutoeJITPlanModelQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.OrgCode))
                {
                    sql += string.Format(" and ORG_CODE = '{0}' ", query.OrgCode); 
                }

                // Locator
                if (!string.IsNullOrEmpty(query.Locator))
                {
                    if (query.Locator.Contains("*"))
                        sql += " and LOCATOR like '" + query.Locator.Replace("*", "%") + "' ";
                    else if (query.Locator.Contains(","))
                        sql += " and LOCATOR in ('" + query.Locator.Replace(",", "','") + "') ";
                    else
                        sql += string.Format(" and LOCATOR = '{0}' ", query.Locator);
                }


                if (!query.NeedFrom.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), NEED_BY_DATE, 23) >='{0}'", query.NeedFrom.ToString("yyyy-MM-dd"));
                }
                if (!query.NeedTo.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), NEED_BY_DATE, 23) <='{0}'", query.NeedTo.ToString("yyyy-MM-dd"));
                }


                if (!query.PlanFrom.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), Plan_Arrival_date, 23) >='{0}'", query.PlanFrom.ToString("yyyy-MM-dd"));
                }
                if (!query.PlanTo.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), Plan_Arrival_date, 23) <='{0}'", query.PlanTo.ToString("yyyy-MM-dd"));
                }


                if (!query.CreationFrom.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), Creation_date, 23) >='{0}'", query.CreationFrom.ToString("yyyy-MM-dd"));
                }
                if (!query.CreationTo.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), Creation_date, 23) <='{0}'", query.CreationTo.ToString("yyyy-MM-dd"));
                }

            }
            return sql;
        }

        private ReportAutoeJITPlanModel.Item GetReportAutoeJITPlanModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportAutoeJITPlanModel.Item data = new ReportAutoeJITPlanModel.Item

            {
                OrgCode = DBConvert.DB2String(dr["OrgCode"]),
                ITEM_NO = DBConvert.DB2String(dr["ITEM_NO"]),
                SUBINV = DBConvert.DB2String(dr["SUBINV"]),
                LOCATOR = DBConvert.DB2String(dr["LOCATOR"]),
                EJIT_REQ_QTY = DBConvert.DB2Decimal(dr["EJIT_REQ_QTY"]),
                ORDER_QTY = DBConvert.DB2Decimal(dr["ORDER_QTY"]),
                EJIT_TRIGGER_TYPE = DBConvert.DB2String(dr["EJIT_TRIGGER_TYPE"]),
                DESCRIPTION = DBConvert.DB2String(dr["DESCRIPTION"]),
                NEED_BY_DATE = DBConvert.DB2DatetimeString(dr["NEED_BY_DATE"], "yyyy-MM-dd"),
                Creation_date = DBConvert.DB2DatetimeString(dr["Creation_date"], "yyyy-MM-dd"),
                Plan_Arrival_date = DBConvert.DB2DatetimeString(dr["Plan_Arrival_date"], "yyyy-MM-dd"),
                GAP = DBConvert.DB2String(dr["GAP"]),
                PRIMARY_UNIT_OF_MEASURE = DBConvert.DB2String(dr["PRIMARY_UNIT_OF_MEASURE"]),
                ITEM_TYPE = DBConvert.DB2String(dr["ITEM_TYPE"]),
                FREQUENCY = DBConvert.DB2String(dr["FREQUENCY"]),
                LEAD_TIME = DBConvert.DB2String(dr["LEAD_TIME"]),
                MPQ = DBConvert.DB2String(dr["MPQ"]),
                VENDOR_NAME = DBConvert.DB2String(dr["VENDOR_NAME"]),
            };
            return data;
        }



        #endregion

        #endregion
    }
}
