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
    public class SMMSDDAL : DALBase, ISMMSDDAL
    {                                                    
        #region corts

        public SMMSDDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public SMMSDDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields


        private const string sql_SMMSDDatas_select = @" select distinct a.CLID, a.MaterialNo, a.MSL, c.FloorLife, a.RemainingLife, a.LastTransaction, a.ProdLine, dateadd(hour,8,a.ChangedOn) as ChangedOn, a.ChangedBy, a.Remarks
                                                        from T_SMCLID as a with(nolock) inner join T_SMStandardMSD as b with(nolock) on a.MSL = b.MSL inner join T_SMStandard as c with(nolock) on b.MSL = c.Item
                                                        where 1=1 and c.Category in ('MSD') ";

        private const string sql_SMMSDPMItemDatas_select = @" SELECT  CLID, dateadd(hour,8,TransactionDate) as TransactDate, [Transaction], FromStatus, ToStatus, Details, TransactedBy, Remarks from T_SMCLIDTrans where 1=1 ";

        private const string sql_SMMSD_select_LastTransaction = @" select distinct LastTransaction from T_SMCLID with(nolock) where ISNULL(MSL, '') <> '' AND LEFT(MSL,1) > 1 ";



        #endregion

        #region methods


        #region methods GetSMMSDDatas - Equipment /Fixture PM Plan Data 
        /// <summary>
        /// GetSMMSDDatas
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportSMMSDModel GetSMMSDDatas(ReportSMMSDModelQuery query)
        {
            ReportSMMSDModel result = new ReportSMMSDModel();
            result.Data = new List<ReportSMMSDModel.Item>();
            string sql = sql_SMMSDDatas_select;
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
                result.Data.Add(GetReportSMMSDModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        public ReportSMMSDPMItemModel GetSMMSDPMItemDatas(ReportSMMSDModelQuery query)
        {
            ReportSMMSDPMItemModel result = new ReportSMMSDPMItemModel();
            result.Data = new List<ReportSMMSDPMItemModel.Item>();
            string sql = sql_SMMSDPMItemDatas_select;
            #region Conditions
            sql += GetReportEquipmentSqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            pSql.SQLDatas = sql;
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportSMMSDPMItemModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        public List<string> GetSMMSDLastTransaction()
        {
            string sql = sql_SMMSD_select_LastTransaction;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Add(DBConvert.DB2String(dr["LastTransaction"]));
                //result.Add(DBConvert.DB2String(dr["SubCategory"]));
            });

            return result;
        }

        /// <summary>
        /// SMMSDDataGetRowCount
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long SMMSDDataGetRowCount(ReportSMMSDModelQuery query)
        {
            string sql = sql_SMMSDDatas_select;
            #region Conditions
            sql += GetReportEquipmentSqlCondition(query);
            #endregion
            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        private string GetReportEquipmentSqlCondition(ReportSMMSDModelQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.MaterialNo))
                {
                    sql += string.Format(" and a.MaterialNO in ('"+ query.MaterialNo.ToString().Trim().Replace(",", "','") + "') ");
                }
                if (!string.IsNullOrEmpty(query.CLID))
                {
                    sql += string.Format(" and CLID in ('" + query.CLID.ToString().Trim().Replace(",", "','") + "') ");
                }
                if (!query.DateFrom.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), a.ChangedOn, 23) >='{0}'", query.DateFrom.ToString("yyyy-MM-dd"));
                }
                if (!query.DateTo.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), a.ChangedOn, 23) <='{0}'", query.DateTo.ToString("yyyy-MM-dd"));
                }
                if (!string.IsNullOrEmpty(query.LastTransaction))
                {
                    sql += string.Format(" and a.LastTransaction ='{0}' ",query.LastTransaction);
                }
            }
            return sql;
        }

        private ReportSMMSDModel.Item GetReportSMMSDModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMMSDModel.Item data = new ReportSMMSDModel.Item
            {
                CLID = DBConvert.DB2String(dr["CLID"]),
                MaterialNo = DBConvert.DB2String(dr["MaterialNo"]),
                MSL = DBConvert.DB2String(dr["MSL"]),
                FloorLife = DBConvert.DB2String(dr["FloorLife"]),
                RemainingLife = DBConvert.DB2String(dr["RemainingLife"]),
                LastTransaction = DBConvert.DB2String(dr["LastTransaction"]),
                ProdLine = DBConvert.DB2String(dr["ProdLine"]),
                ChangedOn = DBConvert.DB2Datetime(dr["ChangedOn"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),
            };
            return data;
        }

        private ReportSMMSDPMItemModel.Item GetReportSMMSDPMItemModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMMSDPMItemModel.Item data = new ReportSMMSDPMItemModel.Item
            {
                CLID = DBConvert.DB2String(dr["CLID"]),
                TransactDate = DBConvert.DB2Datetime(dr["TransactDate"]),
                Transaction = DBConvert.DB2String(dr["Transaction"]),
                FromStatus = DBConvert.DB2String(dr["FromStatus"]),
                ToStatus = DBConvert.DB2String(dr["ToStatus"]),
                Details = DBConvert.DB2String(dr["Details"]),
                TransactedBy = DBConvert.DB2String(dr["TransactedBy"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),
            };
            return data;
        }

        #endregion

        #endregion
    }
}
