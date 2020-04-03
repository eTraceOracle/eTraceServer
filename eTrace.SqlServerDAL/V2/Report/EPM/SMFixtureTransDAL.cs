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
    public class SMFixtureTransDAL : DALBase, ISMFixtureTransDAL
    {                                                    
        #region corts

        public SMFixtureTransDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public SMFixtureTransDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields


        //private const string sql_SMFixtureTransDatas_select = @"SELECT distinct ft.FixtureID,f.ItemNO,ft.TransactionType ,ft.FromLoc ,ft.ToLoc ,ft.FromStatus,ft.ToStatus, ft.JobID, ft.Usage, dateadd(hour,8,ft.TransactionDate) as  TransactionDate,ft.TransactedBy ,ft.Remarks FROM T_SMFixtureTrans as ft with(nolock) LEFT OUTER JOIN T_SMFixture AS f with(nolock) ON (ft.FixtureID = f.FixtureID or ft.FixtureID = f.BatchID) where 1=1 ";

        private const string sql_SMFixtureTransDatas_select = @"SELECT distinct ft.FixtureID,f.ItemNO,ft.TransactionType ,ft.ReasonCode,ft.FromLoc ,ft.ToLoc ,ft.FromStatus,ft.ToStatus, ft.JobID, ft.Usage, dateadd(hour,8, ft.TransactionDate) as  TransactionDate,ft.TransactedBy ,ft.Remarks FROM T_SMFixtureTrans as ft with(nolock) left JOIN T_SMFixture AS f with(nolock) ON ft.FixtureID = f.FixtureID where 1=1";

        private const string sql_SMFixtureTrans_Cate_select = @"SELECT distinct Category FROM T_SMFixture WITH (nolock) order by Category asc";

        private const string sql_SMFixtureTrans_SubCate_select = @"SELECT distinct SubCategory FROM T_SMFixture WITH (nolock) order by SubCategory asc";

        private const string sql_SMFixtureTrans_Status_select = @"SELECT distinct Status FROM T_SMFixture WITH (nolock) order by Status asc";

        private const string sql_SMFixtureTrans_TransactionType_select = @"SELECT distinct TransactionType FROM T_SMFixtureTrans WITH (nolock) order by TransactionType asc";

        #endregion

        #region methods


        #region methods GetSMFixtureTransDatas - Equipment /Fixture PM Plan Data 
        /// <summary>
        /// 获取保养计划数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportSMFixtureTransModel GetSMFixtureTransDatas(ReportSMFixtureTransModelQuery query)
        {
            ReportSMFixtureTransModel result = new ReportSMFixtureTransModel();
            result.Data = new List<ReportSMFixtureTransModel.Item>();
            string sql = sql_SMFixtureTransDatas_select;
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
                result.Data.Add(GetReportSMFixtureTransModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }
        /// <summary>
        /// 获取维修列表总行数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long SMFixtureTransDataGetRowCount(ReportSMFixtureTransModelQuery query)
        {
            string sql = sql_SMFixtureTransDatas_select;
            #region Conditions
            sql += GetReportEquipmentSqlCondition(query);
            #endregion
            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        private string GetReportEquipmentSqlCondition(ReportSMFixtureTransModelQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.FixtureID))
                {
                    sql += string.Format(" and f.FixtureID='{0}'", query.FixtureID);
                }
                if (!string.IsNullOrEmpty(query.ItemNo))
                {
                    sql += string.Format(" and f.ItemNo='{0}'", query.ItemNo);
                }
                if (!string.IsNullOrEmpty(query.Category))
                {
                    sql += string.Format(" and f.Category='{0}'", query.Category);
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
                if (!string.IsNullOrEmpty(query.TransactionType))
                {
                    sql += string.Format(" and ft.TransactionType='{0}'", query.TransactionType);
                }
                if (!string.IsNullOrEmpty(query.Owner))
                {
                    sql += string.Format(" and f.Owner='{0}'", query.Owner);
                }
            }
            return sql;
        }

        private ReportSMFixtureTransModel.Item GetReportSMFixtureTransModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMFixtureTransModel.Item data = new ReportSMFixtureTransModel.Item
            {
                FixtureID = DBConvert.DB2String(dr["FixtureID"]),
                ItemNo = DBConvert.DB2String(dr["ItemNo"]),
                TransactionType = DBConvert.DB2String(dr["TransactionType"]),
                ReasonCode = DBConvert.DB2String(dr["ReasonCode"]),
                FromLoc = DBConvert.DB2String(dr["FromLoc"]),
                ToLoc = DBConvert.DB2String(dr["ToLoc"]),
                FromStatus = DBConvert.DB2String(dr["FromStatus"]),
                ToStatus = DBConvert.DB2String(dr["ToStatus"]),
                JobID = DBConvert.DB2String(dr["JobID"]),
                Usage = DBConvert.DB2String(dr["Usage"]),
                TransactionDate = DBConvert.DB2Datetime(dr["TransactionDate"]),
                TransactedBy = DBConvert.DB2String(dr["TransactedBy"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),

            };
            return data;
        }

        public List<string> GetSMFixtureTransCate()
        {
            string sql = sql_SMFixtureTrans_Cate_select;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Add(DBConvert.DB2String(dr["Category"]));
                //result.Add(DBConvert.DB2String(dr["SubCategory"]));
            });

            return result;
        }

        public List<string> GetSMFixtureTransSubCate()
        {
            string sql = sql_SMFixtureTrans_SubCate_select;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                //result.Add(DBConvert.DB2String(dr["Category"]));
                result.Add(DBConvert.DB2String(dr["SubCategory"]));
            });

            return result;
        }

        public List<string> GetSMFixtureTransStatus()
        {
            string sql = sql_SMFixtureTrans_Status_select;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                //result.Add(DBConvert.DB2String(dr["Category"]));
                result.Add(DBConvert.DB2String(dr["Status"]));
            });

            return result;
        }

        public List<string> GetSMFixtureTransTransactionType()
        {
            string sql = sql_SMFixtureTrans_TransactionType_select;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                //result.Add(DBConvert.DB2String(dr["Category"]));
                result.Add(DBConvert.DB2String(dr["TransactionType"]));
            });

            return result;
        }


        #endregion

        #endregion
    }
}
