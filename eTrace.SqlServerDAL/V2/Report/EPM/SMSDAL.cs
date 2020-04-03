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
    public class SMSDAL : DALBase, ISMSDAL
    {                                                    
        #region corts

        public SMSDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public SMSDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region methods


        #region methods GetSMSDatas

        private string GetSqlCondition(ReportSMSModelQuery query, string actiontype,string GetRowCount = "N")
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.DateFrom))
                {
                    if (!query.DateFrom.Contains ("00:00:00"))
                    {
                        query.DateFrom = query.DateFrom + " 00:00:00";
                    }                  
                }

                if (!string.IsNullOrEmpty(query.DateTo))
                {
                    if (!query.DateTo.Contains("23:59:59"))
                    {
                        query.DateTo = query.DateTo + " 23:59:59";
                    }                        
                }

                int CurrPage = 0;
                int PageSize = 0;
                if (query.Pager != null)
                {
                    CurrPage = (query.Pager.CurrentPage - 1) * query.Pager.PageSize;
                    PageSize = query.Pager.PageSize;
                }             

                sql = string.Format("exec sp_GetSMSReport  '{0}','{1}','{2}',{3},{4},'{5}' ", query.DateFrom, query.DateTo, actiontype, CurrPage, PageSize, GetRowCount);

            }
            return sql;
        }
        private ReportSMSSummaryModel.Summary GetReportSMSSummaryModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMSSummaryModel.Summary data = new ReportSMSSummaryModel.Summary

            {
                Dept = DBConvert.DB2String(dr["Dept"]),
                Mon = DBConvert.DB2String(dr["Mon"]),                         //Not NULL
                SMScount = DBConvert.DB2Int(dr["SMScount"]),
                SMSPercent = DBConvert.DB2Decimal(dr["SMSPercent"]),
               
            };
            return data;
        }

        private ReportSMSDetailModel.Detail GetReportSMSDetailModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMSDetailModel.Detail data = new ReportSMSDetailModel.Detail

            {
                SMScount = DBConvert.DB2Int(dr["SMScount"]),
                SMSID = DBConvert.DB2Int(dr["SMSID"]),
                Dept = DBConvert.DB2String(dr["Dept"]),
                Application = DBConvert.DB2String(dr["Application"]),
                Category = DBConvert.DB2String(dr["Category"]),
                Message = DBConvert.DB2String(dr["Message"]),
                SentOn = DBConvert.DB2Datetime(dr["SentOn"]),
                ResultCode = DBConvert.DB2String(dr["ResultCode"]),
                ResultMsg = DBConvert.DB2String(dr["ResultMsg"]),                
            };
            return data;
        }


        /// <summary>
        /// 按照条件查找Search Data，在存储过程中分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<ReportSMSSummaryModel.Summary> GetSMSSummaryData(ReportSMSModelQuery query)
        {
            string sql = GetSqlCondition(query, "summary");
            return dbHelper.GetList<ReportSMSSummaryModel.Summary>(sql, GetReportSMSSummaryModel);
        }


        /// <summary>
        /// 按照条件查找Search Data，分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<ReportSMSSummaryModel.Summary> GetSMSSummaryByPage(ReportSMSModelQuery query)
        {
            string sql = GetSqlCondition(query, "summary");
            return dbHelper.GetList<ReportSMSSummaryModel.Summary>(sql, GetReportSMSSummaryModel);
        }


        /// <summary>
        /// 获取查询结果行数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long SMSSummaryDataGetRowCount(ReportSMSModelQuery query)
        {
            string GetRowCount = "Y";
            string sql = GetSqlCondition(query, "summary", GetRowCount);
            long rowCount = dbHelper.GetCount(sql);
            return rowCount;
        }


        /// <summary>
        /// 按照条件查找Detail Data，不分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<ReportSMSDetailModel.Detail> GetSMSDetailData(ReportSMSModelQuery query)
        { 
            string sql = GetSqlCondition(query, "detail");
            return dbHelper.GetList<ReportSMSDetailModel.Detail>(sql, GetReportSMSDetailModel);
        }


        /// <summary>
        /// 按照条件查找Detail Data，分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<ReportSMSDetailModel.Detail> GetSMSDetailByPage(ReportSMSModelQuery query)
        {
            string sql = GetSqlCondition(query, "detail");
            return dbHelper.GetList<ReportSMSDetailModel.Detail>(sql, GetReportSMSDetailModel);
        }


        /// <summary>
        /// 获取查询结果行数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long SMSDetailDataGetRowCount(ReportSMSModelQuery query)
        {
            string GetRowCount = "Y";
            string sql = GetSqlCondition(query, "detail", GetRowCount);
            long rowCount = dbHelper.GetCount(sql); 

            return rowCount;
        }
        #endregion

        #endregion
    }
}
