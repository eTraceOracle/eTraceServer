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
    public class SMFixtureVerificationDAL : DALBase, ISMFixtureVerificationDAL
    {                                                    
        #region corts

        public SMFixtureVerificationDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public SMFixtureVerificationDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields


        private const string sql_SMFixtureVerificationDatas_select = @"SELECT distinct ft.FixtureID,ft.FromLoc as [ActualLocation],ft.ToLoc as [SystemLocation] , f.Status, ft.ReasonCode as Result, dateadd(hour,8,ft.TransactionDate) as  TransactionDate FROM T_SMFixtureTrans as ft with(nolock) LEFT OUTER JOIN T_SMFixture AS f with(nolock) ON (ft.FixtureID = f.FixtureID or ft.FixtureID = f.BatchID)  
                                                                        where 1=1 and TransactionType='Verification' ";
        #endregion

        #region methods


        #region methods GetSMFixtureVerificationDatas - Equipment /Fixture PM Plan Data 
        /// <summary>
        /// 获取保养计划数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportSMFixtureVerificationModel GetSMFixtureVerificationDatas(ReportSMFixtureVerificationModelQuery query)
        {
            ReportSMFixtureVerificationModel result = new ReportSMFixtureVerificationModel();
            result.Data = new List<ReportSMFixtureVerificationModel.Item>();
            string sql = sql_SMFixtureVerificationDatas_select;
            #region Conditions
            sql += GetReportEquipmentSqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportSMFixtureVerificationModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }
        /// <summary>
        /// 获取维修列表总行数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long SMFixtureVerificationDataGetRowCount(ReportSMFixtureVerificationModelQuery query)
        {
            string sql = sql_SMFixtureVerificationDatas_select;
            #region Conditions
            sql += GetReportEquipmentSqlCondition(query);
            #endregion
            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        private string GetReportEquipmentSqlCondition(ReportSMFixtureVerificationModelQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.FixtureID))
                {
                    sql += string.Format(" and f.FixtureID= '{0}'", query.FixtureID);
                }
                if (!string.IsNullOrEmpty(query.ItemNO))
                {
                    sql += string.Format(" and f.ItemNO= '{0}'", query.ItemNO);
                }
                if (!string.IsNullOrEmpty(query.Category))
                {
                    sql += string.Format(" and f.Category= '{0}'", query.Category);
                }
                if (!string.IsNullOrEmpty(query.SubCategory))
                {
                    sql += string.Format(" and f.SubCategory= N'{0}'", query.SubCategory);
                }
                if (!string.IsNullOrEmpty(query.BatchID))
                {
                    sql += string.Format(" and f.BatchID='{0}'", query.BatchID);
                }
                if (!string.IsNullOrEmpty(query.Status))
                {
                    sql += string.Format(" and f.Status='{0}'", query.Status);
                }
                if (!string.IsNullOrEmpty(query.ReasonCode))
                {
                    sql += string.Format(" and ft.ReasonCode='{0}'", query.ReasonCode);
                }
                if (!string.IsNullOrEmpty(query.Owner))
                {
                    sql += string.Format(" and f.Owner='{0}'", query.Owner);
                }
                if (!query.TransactionDateFrom.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), ft.TransactionDate, 23) >='{0}'", query.TransactionDateFrom.ToString("yyyy-MM-dd"));
                }
                if (!query.TransactionDateTo.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), ft.TransactionDate, 23) <='{0}'", query.TransactionDateTo.ToString("yyyy-MM-dd"));
                }
            }
            return sql;
        }

        private ReportSMFixtureVerificationModel.Item GetReportSMFixtureVerificationModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMFixtureVerificationModel.Item data = new ReportSMFixtureVerificationModel.Item
            {
                FixtureID = DBConvert.DB2String(dr["FixtureID"]),
                ActualLocation = DBConvert.DB2String(dr["ActualLocation"]),
                SystemLocation = DBConvert.DB2String(dr["SystemLocation"]),
                Status = DBConvert.DB2String(dr["Status"]),
                Result = DBConvert.DB2String(dr["Result"]),
                TransactionDate = DBConvert.DB2Datetime(dr["TransactionDate"]),
            };
            return data;
        }
        #endregion

        #endregion
    }
}
