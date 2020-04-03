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
    public class RawMatPakStatusDAL : DALBase, IRawMatPakStatusDAL
    {                                                    
        #region corts

        public RawMatPakStatusDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public RawMatPakStatusDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields

        private const string sql_RawMatPakStatus_select = @"SELECT A.OrgCode, A.SLOC as SubInv, A.StorageBin as Locator, A.CLID, B.BoxID, B.PalletID, A.MaterialNo as ItemNo, A.QtyBaseUOM, B.CreatedBy as PackedBy, B.CreatedOn as PackedOn, A.ChangedBy, A.ChangedOn "
                             + "FROM T_CLMaster as A WITH (nolock) left outer join T_CLMaster_Packing as B on A.CLID = B.CLID where StatusCode = '1' ";


        #endregion

        #region methods

        #region methods GetRawMatPakStatusDatas
        /// <summary>
        /// 获取 CLID Master Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportRawMatPakStatusModel GetRawMatPakStatusData(ReportRawMatPakStatusModelQuery query)
        {
            ReportRawMatPakStatusModel result = new ReportRawMatPakStatusModel();
            result.Data = new List<ReportRawMatPakStatusModel.Item>();
            string sql = sql_RawMatPakStatus_select;
            #region Conditions
            sql += GetReportSqlCondition(query);
            #endregion

            string ordercol = " SubInv, Locator, ChangedOn ";
            PageSql pSql = GetPagerSQL(query, sql, "", ordercol);

            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportRawMatPakStatusModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }


        /// <summary>
        /// 获取物料Summary Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long RawMatPakStatusDataGetRowCount(ReportRawMatPakStatusModelQuery query)
        {
            string sql = sql_RawMatPakStatus_select;
            #region Conditions
            sql += GetReportSqlCondition(query);
            #endregion
            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        private string GetReportSqlCondition(ReportRawMatPakStatusModelQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.OrgCode))
                {
                    sql += string.Format(" and A.OrgCode = '{0}' ", query.OrgCode);
                }

                if (!string.IsNullOrEmpty(query.SubInv))
                {
                    if (query.SubInv.Contains("*"))
                        sql += " and A.SLOC like '" + query.SubInv.Replace("*", "%") + "' ";
                    else if (query.SubInv.Contains(","))
                        sql += " and A.SLOC in ('" + query.SubInv.Replace(",", "','") + "') ";
                    else
                        sql += string.Format(" and A.SLOC = '{0}' ", query.SubInv);

                }

                if (!string.IsNullOrEmpty(query.LocatorFrom))
                    sql += " and A.StorageBin >= '" + query.LocatorFrom.Trim() + "' ";

                if (!string.IsNullOrEmpty(query.LocatorTo))
                    sql += " and A.StorageBin <= '" + query.LocatorTo.Trim() + "' ";

                if (!string.IsNullOrEmpty(query.ItemNo))
                {
                    if (query.ItemNo.Contains("*"))
                        sql += " and A.MaterialNo like '" + query.ItemNo.Replace("*", "%") + "' ";
                    else if (query.ItemNo.Contains(","))
                        sql += " and A.MaterialNo in ('" + query.ItemNo.Replace(",", "','") + "') ";
                    else
                        sql += string.Format(" and A.MaterialNo = '{0}' ", query.ItemNo);
                }


            }
            return sql;
        }

        private ReportRawMatPakStatusModel.Item GetReportRawMatPakStatusModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportRawMatPakStatusModel.Item data = new ReportRawMatPakStatusModel.Item

            {
                OrgCode = DBConvert.DB2String(dr["OrgCode"]),
                SubInv = DBConvert.DB2String(dr["SubInv"]),
                Locator = DBConvert.DB2String(dr["Locator"]),
                CLID = DBConvert.DB2String(dr["CLID"]),
                BoxID = DBConvert.DB2String(dr["BoxID"]),
                PalletID = DBConvert.DB2String(dr["PalletID"]),
                ItemNo = DBConvert.DB2String(dr["ItemNo"]),
                QtyBaseUOM = DBConvert.DB2Decimal(dr["QtyBaseUOM"]),
                PackedBy = DBConvert.DB2String(dr["PackedBy"]),
                PackedOn = DBConvert.DB2DatetimeNull(dr["PackedOn"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                ChangedOn = DBConvert.DB2DatetimeNull(dr["ChangedOn"]),
            };

            return data;
        }



        #endregion

        #endregion
    }
}
