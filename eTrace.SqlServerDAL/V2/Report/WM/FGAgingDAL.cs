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
    public class FGAgingDAL : DALBase, IFGAgingDAL
    {                                                    
        #region corts

        public FGAgingDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public FGAgingDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields

        private const string sql_FGAging_Detail_select = @"SELECT * FROM V_AssemblyAging where 1=1 ";

        private const string sql_FGAging_Summary_select = @"Select Org ,Item,Subinv,Locator,Sum(QtyBaseUOM) as QTY from V_AssemblyAging where 1=1 ";                                                                                                                                                                                                                                                                                                                                                    //            sql = "SELECT A.OrgCode, A.MaterialNo, A.MaterialRevision, A.SLOC, A.StorageType, A.StorageBin,sum(A.QtyBaseUOM) AS QtyBaseUOM, A.BaseUOM, A.RTLot FROM (SELECT OrgCode,MaterialNo,MaterialRevision,SLOC, ISNULL(StorageType,'') as StorageType, StorageBin, QtyBaseUOM, BaseUOM,RTLot from T_CLMaster with(nolock) where 1=1 " & sqlStatusCode & " and " & Mid(sql, 4) & ") AS A GROUP BY OrgCode,SLOC, StorageType, StorageBin, MaterialNo, BaseUOM,RTLot,MaterialRevision" & " ORDER BY MaterialNo, SLOC, StorageType, StorageBin,RTLot"

        private const string sql_FGAging_Summary_Group = @" GROUP BY Org, SubInv, Locator, Item ";

        #endregion

        #region methods


        #region methods GetFGAgingDatas
        /// <summary>
        /// 获取 CLID Master Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportFGAgingDetailModel GetFGAgingDetailData(ReportFGAgingModelQuery query)
        {
            ReportFGAgingDetailModel result = new ReportFGAgingDetailModel();
            result.Data = new List<ReportFGAgingDetailModel.Detail>();
            string sql = sql_FGAging_Detail_select;

             #region Conditions
            sql += GetReportSqlCondition(query);
            #endregion

            string ordercol = " Org, Item, DateCode, LotNo ";
            PageSql pSql = GetPagerSQL(query, sql, "", ordercol);

            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportFGAgingDetailModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        public ReportFGAgingSummaryModel GetFGAgingSummaryData(ReportFGAgingModelQuery query)
        {
            ReportFGAgingSummaryModel result = new ReportFGAgingSummaryModel();
            result.Data = new List<ReportFGAgingSummaryModel.Summary>();
            string sql = sql_FGAging_Summary_select;

            #region Conditions
            sql += GetReportSqlCondition(query);
            #endregion

            string ordercol = " SubInv, Locator, Item ";
            PageSql pSql = GetPagerSQL(query, sql, "", ordercol);

            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportFGAgingSummaryModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }


        /// <summary>
        /// 获取物料Summary Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long FGAgingDataGetRowCount(ReportFGAgingModelQuery query)
        {
            string sql = sql_FGAging_Detail_select;                 //Default Type: Detail
            if (query.ReportType.Equals("Summary"))
            {
                sql = sql_FGAging_Summary_select;
            }

            #region Conditions
            sql += GetReportSqlCondition(query);
            #endregion

            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        private string GetReportSqlCondition(ReportFGAgingModelQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.OrgCode))
                {
                    sql += string.Format(" and Org = '{0}' ", query.OrgCode); 
                }

                if (!string.IsNullOrEmpty(query.SubInv))
                {
                    if (query.SubInv.Contains("*"))
                        sql += " and Subinv like '" + query.SubInv.Replace("*", "%") + "' ";
                    else if (query.SubInv.Contains(","))
                        sql += " and Subinv in ('" + query.SubInv.Replace(",", "','") + "') ";
                    else
                        sql += string.Format(" and SubInv = '{0}' ", query.SubInv);
                }

                if (!string.IsNullOrEmpty(query.Item))
                {
                    if (query.Item.Contains("*"))
                        sql += " and Item like '" + query.Item.Replace("*", "%") + "' ";
                    else if (query.Item.Contains(","))
                        sql += " and Item in ('" + query.Item.Replace(",", "','") + "') ";
                    else
                        sql += string.Format(" and Item = '{0}' ", query.Item);
                }

                if (!string.IsNullOrEmpty(query.AgingDays.ToString()))
                {
                    sql += " and StorageDays  " + query.DayOperator + " " + query.AgingDays;
                }


                //For Search Detail only, we will consider input column: RTDateFrom / RTDateTo 
                if (query.ReportType.Equals("Detail"))
                {
                    if (!query.RTDateFrom.ToString().Equals("1/1/0001 12:00:00 AM"))
                    {
                        sql += string.Format(" and CONVERT(varchar(100), DJCompletionDate, 23) >='{0}'", query.RTDateFrom.ToString("yyyy-MM-dd"));
                    }
                    if (!query.RTDateTo.ToString().Equals("1/1/0001 12:00:00 AM"))
                    {
                        sql += string.Format(" and CONVERT(varchar(100), DJCompletionDate, 23) <='{0}'", query.RTDateTo.ToString("yyyy-MM-dd"));
                    }
                }
                else   //For Search Summary, need to add Group by column
                {
                    sql += sql_FGAging_Summary_Group;   
                }

            }
            return sql;
        }

        private ReportFGAgingDetailModel.Detail GetReportFGAgingDetailModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportFGAgingDetailModel.Detail data = new ReportFGAgingDetailModel.Detail

            {
                OrgCode = DBConvert.DB2String(dr["Org"]),
                Item = DBConvert.DB2String(dr["Item"]),
                SubInv = DBConvert.DB2String(dr["SubInv"]),
                Locator = DBConvert.DB2String(dr["Locator"]),
                CLID = DBConvert.DB2String(dr["CLID"]),
                PalletID_BOXID = DBConvert.DB2String(dr["PalletID_BOXID"]),
                QtyBaseUOM = DBConvert.DB2Decimal(dr["QtyBaseUOM"]),
                DateCode = DBConvert.DB2String(dr["DateCode"]),
                LotNo = DBConvert.DB2String(dr["LotNo"]),
                SalesOrderNo = DBConvert.DB2String(dr["SalesOrderNo"]),
                SalesOrderLine = DBConvert.DB2String(dr["SalesOrderLine"]),
                DJNo = DBConvert.DB2String(dr["DJNo"]),
                RT = DBConvert.DB2String(dr["RT"]),
                RoHS = DBConvert.DB2String(dr["RoHS"]),
                Rev = DBConvert.DB2String(dr["Rev"]),
                DJCompletionDate = DBConvert.DB2DatetimeNull(dr["DJCompletionDate"]),
                StorageDays = DBConvert.DB2Int(dr["StorageDays"]),
                CreatedBy = DBConvert.DB2String(dr["CreatedBy"]),
                CreatedOn = DBConvert.DB2DatetimeNull(dr["CreatedOn"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                ChangedOn = DBConvert.DB2DatetimeNull(dr["ChangedOn"]),
            };
            return data;
        }


        private ReportFGAgingSummaryModel.Summary GetReportFGAgingSummaryModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportFGAgingSummaryModel.Summary data = new ReportFGAgingSummaryModel.Summary

            {
                OrgCode = DBConvert.DB2String(dr["Org"]),
                Item = DBConvert.DB2String(dr["Item"]),
                SubInv = DBConvert.DB2String(dr["SubInv"]),
                Locator = DBConvert.DB2String(dr["Locator"]),
                QTY = DBConvert.DB2Decimal(dr["QTY"]),

            };
            return data;
        }



        #endregion

        #endregion
    }
}
