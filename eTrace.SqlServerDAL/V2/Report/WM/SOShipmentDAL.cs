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
    public class SOShipmentDAL : DALBase, ISOShipmentDAL
    {                                                    
        #region corts

        public SOShipmentDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public SOShipmentDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields

        private const string sql_SOShipment_Detail_select = @"SELECT A.OrgCode,  B.PO as DN,  A.SONo as SO, A.SOLine as SOLine, A.MaterialNo as Item, 
                                A.MaterialRevision as Revision, A.MaterialDesc as ItemDesc,  A.SLOC as SubInv,  A.StorageBin as Locator,
                                A.DateCode, A.LotNo,  B.CLIDQty,  A.CLID, A.boxid as PalletID, A.Manufacturer, A.ManufacturerPN, 
                                A.VendorID as VendorCode, A.VendorName,A.CountryofOrigin as COO, A.RTLot, A.PurordNo as DJNO, A.RecDate,  
                                B.IssueDate, DATEDIFF(day, A.RecDate, GETDATE()) AS StorageDays ,B.ChangedBy, A.ShipmentNo
                                FROM T_CLMaster AS A with(nolock)  INNER JOIN T_PO_CLID AS B with(nolock) 
                                ON A.CLID = B.CLID WHERE A.StatusCode = 0 AND (B.PO NOT LIKE '[a-z]%') AND A.ShipmentNo <> '' ";

        private const string sql_SOShipment_Summary_select = @"SELECT A.OrgCode, A.MaterialNo as ITEM,
                                A.MaterialRevision as Revision, A.SLOC as SubInv, sum(B.CLIDQty) as TotalQTY
                                FROM T_CLMaster AS A  with(nolock)  INNER JOIN T_PO_CLID AS B with(nolock) 
                                ON A.CLID = B.CLID
                                WHERE A.StatusCode = 0 AND (B.PO NOT LIKE '[a-z]%') AND A.ShipmentNo<> '' ";

        private const string sql_SOShipment_Summary_Group = @" GROUP BY A.OrgCode, A.MaterialNo, A.SLOC, A.MaterialRevision ";


        #endregion

        #region methods


        #region methods GetSOShipmentDatas
        /// <summary>
        /// 获取 CLID Master Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportSOShipmentDetailModel GetSOShipmentDetailData(ReportSOShipmentModelQuery query)
        {
            ReportSOShipmentDetailModel result = new ReportSOShipmentDetailModel();
            result.Data = new List<ReportSOShipmentDetailModel.Detail>();
            string sql = sql_SOShipment_Detail_select;

            #region Conditions
            sql += GetReportSqlCondition(query);
            #endregion

            string ordercol = " A.MaterialNo, A.LotNo ";
            PageSql pSql = GetPagerSQL(query, sql, "", ordercol);

            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportSOShipmentDetailModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        public ReportSOShipmentSummaryModel GetSOShipmentSummaryData(ReportSOShipmentModelQuery query)
        {
            ReportSOShipmentSummaryModel result = new ReportSOShipmentSummaryModel();
            result.Data = new List<ReportSOShipmentSummaryModel.Summary>();
            string sql = sql_SOShipment_Summary_select;

            #region Conditions
            sql += GetReportSqlCondition(query);
            #endregion

            string ordercol = " A.MaterialNo ";
            PageSql pSql = GetPagerSQL(query, sql, "", ordercol);

            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportSOShipmentSummaryModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        /// <summary>
        /// 获取物料Summary Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long SOShipmentDataGetRowCount(ReportSOShipmentModelQuery query)
        {
            string sql = sql_SOShipment_Detail_select;                    //Default Type: Detail
            if (query.ReportType.Equals("Summary"))
            {
                sql = sql_SOShipment_Summary_select;
            }

            #region Conditions
            sql += GetReportSqlCondition(query);
            #endregion

            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

         private string GetReportSqlCondition(ReportSOShipmentModelQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.OrgCode))
                {
                    sql += string.Format(" and A.OrgCode = '{0}' ", query.OrgCode);
                }

                if (!string.IsNullOrEmpty(query.DeliveryNo))
                {
                    if (query.DeliveryNo.Contains("*"))
                        sql += " and B.PO like '" + query.DeliveryNo.Replace("*", "%") + "' ";
                    else if (query.DeliveryNo.Contains(","))
                        sql += " and B.PO in ('" + query.DeliveryNo.Replace(",", "','") + "') ";
                    else
                        sql += string.Format(" and B.PO = '{0}' ", query.DeliveryNo);
                }

                if (!string.IsNullOrEmpty(query.Item))
                {
                    if (query.Item.Contains("*"))
                        sql += " and A.MaterialNo like '" + query.Item.Replace("*", "%") + "' ";
                    else if (query.Item.Contains(","))
                        sql += " and A.MaterialNo in ('" + query.Item.Replace(",", "','") + "') ";
                    else
                        sql += string.Format(" and A.MaterialNo = '{0}' ", query.Item);
                }

                if (!string.IsNullOrEmpty(query.DateCode))
                {
                    sql += string.Format(" and A.DateCode = '{0}' ", query.DateCode);
                }

                if (!string.IsNullOrEmpty(query.LotNo))
                {
                    sql += string.Format(" and A.LotNo = '{0}' ", query.LotNo);
                }


                if (!string.IsNullOrEmpty(query.DestSubInv))
                {
                    if (query.DestSubInv.Contains("*"))
                        sql += " and A.SLOC like '" + query.DestSubInv.Replace("*", "%") + "' ";
                    else if (query.DestSubInv.Contains(","))
                        sql += " and A.SLOC in ('" + query.DestSubInv.Replace(",", "','") + "') ";
                    else
                        sql += string.Format(" and A.SLOC = '{0}' ", query.DestSubInv);
                }

                if (!query.IssueDateFrom.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), B.IssueDate, 23) >='{0}'", query.IssueDateFrom.ToString("yyyy-MM-dd"));
                }
                    if (!query.IssueDateTo.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), B.IssueDate, 23) <='{0}'", query.IssueDateTo.ToString("yyyy-MM-dd"));
                }

                if (!string.IsNullOrEmpty(query.StorageDay.ToString()))
                {
                     if (query.ReportType.Equals("Detail"))
                    {    //DATEDIFF(datepart, startdate, enddate)
                        sql += " and (DATEDIFF(day, A.RecDate, GETDATE()) >= '" + query.StorageDay + "') ";
                      }
                     else   
                    {
                        sql += " and (DATEDIFF(day, A.RecDate, B.IssueDate) >= '" + query.StorageDay + "') ";
                    }
                }

                //For Search Summary, need to add Group by column
                if (query.ReportType.Equals("Summary"))
                {
                    sql += sql_SOShipment_Summary_Group;
                }

            }
            return sql;
        }

        private ReportSOShipmentDetailModel.Detail GetReportSOShipmentDetailModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSOShipmentDetailModel.Detail data = new ReportSOShipmentDetailModel.Detail

            {
                OrgCode = DBConvert.DB2String(dr["OrgCode"]),
                DN = DBConvert.DB2String(dr["DN"]),
                SO = DBConvert.DB2String(dr["SO"]),
                SOLine = DBConvert.DB2String(dr["SOLine"]),
                Item = DBConvert.DB2String(dr["Item"]),
                Revision = DBConvert.DB2String(dr["Revision"]),
                ItemDesc = DBConvert.DB2String(dr["ItemDesc"]),
                SubInv = DBConvert.DB2String(dr["SubInv"]),
                Locator = DBConvert.DB2String(dr["Locator"]),
                DateCode = DBConvert.DB2String(dr["DateCode"]),
                LotNo = DBConvert.DB2String(dr["LotNo"]),
                CLIDQty = DBConvert.DB2Decimal(dr["CLIDQty"]),
                CLID = DBConvert.DB2String(dr["CLID"]),
                PalletID = DBConvert.DB2String(dr["PalletID"]),
                Manufacturer = DBConvert.DB2String(dr["Manufacturer"]),
                ManufacturerPN = DBConvert.DB2String(dr["ManufacturerPN"]),
                VendorCode = DBConvert.DB2String(dr["VendorCode"]),
                VendorName = DBConvert.DB2String(dr["VendorName"]),
                COO = DBConvert.DB2String(dr["COO"]),
                RTLot = DBConvert.DB2String(dr["RTLot"]),
                DJNO = DBConvert.DB2String(dr["DJNO"]),
                RecDate = DBConvert.DB2DatetimeNull(dr["RecDate"]),
                IssueDate = DBConvert.DB2DatetimeNull(dr["IssueDate"]),
                StorageDays = DBConvert.DB2Int(dr["StorageDays"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                ShipmentNo = DBConvert.DB2String(dr["ShipmentNo"]),
            };
            return data;
        }


        private ReportSOShipmentSummaryModel.Summary GetReportSOShipmentSummaryModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSOShipmentSummaryModel.Summary data = new ReportSOShipmentSummaryModel.Summary

            {
                OrgCode = DBConvert.DB2String(dr["OrgCode"]),
                Item = DBConvert.DB2String(dr["Item"]),
                Revision = DBConvert.DB2String(dr["Revision"]),
                SubInv = DBConvert.DB2String(dr["SubInv"]),
                TotalQTY = DBConvert.DB2Decimal(dr["TotalQTY"]),
            };
            return data;
        }


        #endregion

        #endregion
    }
}
