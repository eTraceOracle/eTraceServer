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
    public class LabelInfoDAL : DALBase, ILabelInfoDAL
    {                                                    
        #region corts

        public LabelInfoDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public LabelInfoDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields

        private const string sql_LabelInfo_Detail_select = @"SELECT OrgCode,CLID,BoxID, MaterialNo,MaterialRevision AS Revision,MaterialDesc,QtyBaseUOM,BaseUOM,SLOC as SubInv, StorageBin as Locator, AddlText, ISNULL(StorageType,'') as StorageType, StockType,DateCode,LotNo,CountryofOrigin as COO, ExpDate, RecDocNo,  RTLot,RecDate,RoHS, PurOrdNo, PurOrdItem, VendorID, VendorName, VendorPN,Manufacturer, ManufacturerPN, QMLStatus, NextReviewDate, ReviewStatus,ReviewedOn, ReviewedBy,CreatedOn,CreatedBy,ChangedOn,ChangedBy ,LastTransaction, MSL, InvoiceNo,StatusCode FROM T_CLMaster WITH (nolock) where 1=1 ";   // " ORDER BY CLID, MaterialNo";

        private const string sql_LabelInfo_Summary_select = @"SELECT A.OrgCode, A.MaterialNo, A.Revision, A.SubInv, A.StorageType, A.Locator, sum(A.QtyBaseUOM) AS QtyBaseUOM, A.BaseUOM, A.RTLot FROM (SELECT OrgCode,MaterialNo,MaterialRevision AS Revision ,SLOC as SubInv, ISNULL(StorageType,'') as StorageType, StorageBin as Locator, QtyBaseUOM, BaseUOM, RTLot from T_CLMaster with(nolock) where 1=1 "; // " ORDER BY MaterialNo, SLOC, StorageType, StorageBin,RTLot"

        private const string sql_LabelInfo_Summary_Group = @") AS A GROUP BY OrgCode, SubInv, StorageType, Locator, MaterialNo, BaseUOM, RTLot, Revision";


        private const string sql_LabelInfo_ePurge_Detail = @"SELECT OrgCode,CLID,MaterialNo,QtyBaseUOM,BaseUOM,SLOC as SubInv, StorageBin as Locator,StockType,DateCode,LotNo,ExpDate, RecDocNo,RTLot,RecDate,VendorID, VendorName, VendorPN,Manufacturer, ManufacturerPN, ChangedOn,ChangedBy, LastTransaction FROM T_CLMaster WITH (nolock) where 1=1 ";  // " ORDER BY CLID, MaterialNo"

        private const string sql_LabelInfo_ePurge_Summary = @"SELECT OrgCode,SLOC as SubInv,MaterialNo,Manufacturer, ManufacturerPN,DateCode,LotNo,sum(QtyBaseUOM) AS Qty, ActualReturnedQty='', DeviationQty='', Remark='' from T_CLMaster with(nolock) where 1=1 ";     // " ORDER BY OrgCode, SLOC, MaterialNo, Manufacturer, ManufacturerPN,DateCode,LotNo"

        private const string sql_LabelInfo_ePurgeSM_Group = @" GROUP BY OrgCode, SLOC, MaterialNo, Manufacturer, ManufacturerPN, DateCode, LotNo";


        #endregion

        #region methods


        #region methods GetLabelInfoDatas
        /// <summary>
        /// 获取 CLID Master Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportLabelInfoDetailModel GetLabelInfoDetailData(ReportLabelInfoModelQuery query)
        {
            ReportLabelInfoDetailModel result = new ReportLabelInfoDetailModel();
            result.Data = new List<ReportLabelInfoDetailModel.Detail>();
            string sql = sql_LabelInfo_Detail_select;

            #region Conditions
            sql += GetReportSqlCondition(query);
            #endregion

            string ordercol = " CLID, MaterialNo";
            PageSql pSql = GetPagerSQL(query, sql, "", ordercol);

            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportLabelInfoDetailModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        public ReportLabelInfoSummaryModel GetLabelInfoSummaryData(ReportLabelInfoModelQuery query)
        {
            ReportLabelInfoSummaryModel result = new ReportLabelInfoSummaryModel();
            result.Data = new List<ReportLabelInfoSummaryModel.Summary>();
            string sql = sql_LabelInfo_Summary_select;

            #region Conditions
            sql += GetReportSqlCondition(query);
            #endregion

            string ordercol = " MaterialNo, SubInv, StorageType, Locator, RTLot";
            PageSql pSql = GetPagerSQL(query, sql, "", ordercol);

            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportLabelInfoSummaryModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        public ReportLabelInfoePurgeDTModel GetLabelInfoePurgeDTData(ReportLabelInfoModelQuery query)
        {
            ReportLabelInfoePurgeDTModel result = new ReportLabelInfoePurgeDTModel();
            result.Data = new List<ReportLabelInfoePurgeDTModel.Detail>();
            string sql = sql_LabelInfo_ePurge_Detail;

            #region Conditions
            sql += GetReportSqlCondition(query);
            #endregion

            string ordercol = " CLID, MaterialNo";
            PageSql pSql = GetPagerSQL(query, sql, "", ordercol);

            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportLabelInfoePurgeDTModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        public ReportLabelInfoePurgeSMModel GetLabelInfoePurgeSMData(ReportLabelInfoModelQuery query)
        {
            ReportLabelInfoePurgeSMModel result = new ReportLabelInfoePurgeSMModel();
            result.Data = new List<ReportLabelInfoePurgeSMModel.Summary>();
            string sql = sql_LabelInfo_ePurge_Summary;

            #region Conditions
            sql += GetReportSqlCondition(query);
            #endregion

            string ordercol = " OrgCode, SLOC, MaterialNo, Manufacturer, ManufacturerPN, DateCode, LotNo";
            PageSql pSql = GetPagerSQL(query, sql, "", ordercol);

            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportLabelInfoePurgeSMModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        /// <summary>
        /// 获取物料Summary Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long LabelInfoDataGetRowCount(ReportLabelInfoModelQuery query)
        {
            string sql = sql_LabelInfo_Detail_select;                  //Default Type: Detail

            if (query.ReportType.Equals("Summary"))
            {
                sql = sql_LabelInfo_Summary_select;
            }
            else if (query.ReportType.Equals("ePurgeSM"))
            {
                sql = sql_LabelInfo_ePurge_Summary;
            }
            else if (query.ReportType.Equals("ePurgeDT"))
            {
                sql = sql_LabelInfo_ePurge_Detail;
            }


            #region Conditions
            sql += GetReportSqlCondition(query);
            #endregion

            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        private string GetReportSqlCondition(ReportLabelInfoModelQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.OrgCode))
                {
                    sql += string.Format(" and OrgCode = '{0}' ", query.OrgCode); //"and " + "OrgCode= '" + query.OrgCode + "' ";
                }

                if (!string.IsNullOrEmpty(query.BoxID))
                {
                    if (query.BoxID.Contains("*"))
                        sql += " and BoxID like '" + query.BoxID.Replace("*", "%") + "' ";
                    else if (query.BoxID.Contains(","))
                        sql += " and BoxID in ('" + query.BoxID.Replace(",", "','") + "') ";
                    else
                        sql += string.Format(" and BoxID = '{0}' ", query.BoxID); 
                }


                if (!string.IsNullOrEmpty(query.SubInv))
                {
                    if (query.SubInv.Contains("*"))
                        sql += " and SLOC like '" + query.SubInv.Replace("*", "%") + "' ";
                    else if (query.SubInv.Contains(","))
                        sql += " and SLOC in ('" + query.SubInv.Replace(",", "','") + "') ";
                    else
                        sql += string.Format(" and SLOC = '{0}' ", query.SubInv);
                }

                if (!string.IsNullOrEmpty(query.Locator))
                {
                    if (query.Locator.Contains("*"))
                        sql += " and StorageBin like '" + query.Locator.Replace("*", "%") + "' ";
                    else if (query.Locator.Contains(","))
                        sql += " and StorageBin in ('" + query.Locator.Replace(",", "','") + "') ";
                    else
                        sql += string.Format(" and StorageBin = '{0}' ", query.Locator);
                }

                if (!string.IsNullOrEmpty(query.MaterialNo))
                {
                    if (query.MaterialNo.Contains("*"))
                        sql += " and MaterialNo like '" + query.MaterialNo.Replace("*", "%") + "' ";
                    else if (query.MaterialNo.Contains(","))
                        sql += " and MaterialNo in ('" + query.MaterialNo.Replace(",", "','") + "') ";
                    else
                        sql += string.Format(" and MaterialNo = '{0}' ", query.MaterialNo);
                }

                // RT NO
                if (!string.IsNullOrEmpty(query.RecDocNo))
                {
                    if (query.RecDocNo.Contains("*"))
                        sql += " and RecDocNo like '" + query.RecDocNo.Replace("*", "%") + "' ";
                    else if (query.RecDocNo.Contains(","))
                        sql += " and RecDocNo  in ('" + query.RecDocNo.Replace(",", "','") + "') ";
                    else
                        sql += string.Format(" and RecDocNo = '{0}' ", query.RecDocNo);
                }

                if (!string.IsNullOrEmpty(query.VendorID))
                {
                    if (query.VendorID.Contains("*"))
                        sql += " and VendorID like '" + query.VendorID.Replace("*", "%") + "' ";
                    else if (query.VendorID.Contains(","))
                        sql += " and VendorID in ('" + query.VendorID.Replace(",", "','") + "') ";
                    else
                        sql += string.Format(" and VendorID = '{0}' ", query.VendorID);
                }

                //if (!string.IsNullOrEmpty(query.DateCode))
                //{
                //    if (query.DateCode.Contains("*"))
                //        sql += " and DateCode like '" + query.DateCode.Replace("*", "%") + "' ";
                //    else
                //        sql += " and DateCode  in ('" + query.DateCode.Replace(",", "','") + "') ";
                //}

                //if (!string.IsNullOrEmpty(query.LotNo))
                //{
                //    if (query.LotNo.Contains("*"))
                //        sql += " and LotNo like '" + query.LotNo.Replace("*", "%") + "' ";
                //    else
                //        sql += " and LotNo in ('" + query.LotNo.Replace(",", "','") + "') ";
                //}
                if (!string.IsNullOrEmpty(query.DateCode))
                {
                    sql += string.Format(" and DateCode = '{0}' ", query.DateCode);
                }

                if (!string.IsNullOrEmpty(query.LotNo))
                {
                    sql += string.Format(" and LotNo = '{0}' ", query.LotNo);
                }

                if (!string.IsNullOrEmpty(query.RTLot))
                {
                    if (query.RTLot.Contains("*"))
                        sql += " and RTLot like '" + query.RTLot.Replace("*", "%") + "' ";
                    else if (query.RTLot.Contains(","))
                        sql += " and RTLot in ('" + query.RTLot.Replace(",", "','") + "') ";
                    else
                        sql += string.Format(" and RTLot = '{0}' ", query.RTLot);
                }

                if (!string.IsNullOrEmpty(query.Manufacturer))
                {
                    if (query.Manufacturer.Contains("*"))
                        sql += " and Manufacturer like '" + query.Manufacturer.Replace("*", "%") + "' ";
                    else
                        sql += " and Manufacturer in ('" + query.Manufacturer.Replace(",", "','") + "') ";
                }

                if (!string.IsNullOrEmpty(query.ManufacturerPN))
                {
                    if (query.ManufacturerPN.Contains("*"))
                        sql += " and ManufacturerPN like '" + query.ManufacturerPN.Replace("*", "%") + "' ";
                    else
                        sql += " and ManufacturerPN in ('" + query.ManufacturerPN.Replace(",", "','") + "') ";
                }


                //if (query.RTFrom.HasValue)
                //{
                //    sql += string.Format(" and RecDate>='{0}'", query.RTFrom.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                //}
                //if (query.RTTo.HasValue)
                //{
                //    sql += string.Format(" and RecDate<'{0}'", query.RTTo.Value.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"));
                //}

                if (!query.RTFrom.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), RecDate, 23) >='{0}'", query.RTFrom.ToString("yyyy-MM-dd"));
                }
                if (!query.RTTo.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), RecDate, 23) <='{0}'", query.RTTo.ToString("yyyy-MM-dd"));
                }


                if (!string.IsNullOrEmpty(query.CLID))
                {
                    if (query.CLID.Contains("*"))
                        sql += " and CLID like '" + query.CLID.Replace("*", "%") + "' ";
                    else if (query.CLID.Contains(","))
                        sql += " and CLID in ('" + query.CLID.Replace(",", "','") + "') ";
                    else
                        sql += string.Format(" and CLID = '{0}' ", query.CLID);
                }

                string sqlStatusCode = "";

                if (query.StatusCode == string.Empty)
                    sqlStatusCode = " and StatusCode in ('1','4','6','9') ";
                else if (query.StatusCode == "1")
                    sqlStatusCode = " and StatusCode in ('1') ";
                else if (query.StatusCode == "4")
                    sqlStatusCode = " and StatusCode in ('4') ";
                else if (query.StatusCode == "6")
                    sqlStatusCode = " and StatusCode in ('6') ";
                else if (query.StatusCode == "9")
                    sqlStatusCode = " and StatusCode in ('9') ";

                if (!string.IsNullOrEmpty(query.LastTransaction))
                {
                    if (query.LastTransaction.Contains("*"))
                        sql += " and LastTransaction like '" + query.LastTransaction.Replace("*", "%") + "' ";
                    else if (query.LastTransaction.Contains(","))
                        sql += " and LastTransaction in ('" + query.LastTransaction.Replace(",", "','") + "') ";
                    else
                        sql += string.Format(" and LastTransaction = '{0}' ", query.LastTransaction);
                }


                sql = sqlStatusCode + sql;   

                //For Search Summary, need to add Group by column
                if (query.ReportType.Equals("Summary"))
                {
                    sql += sql_LabelInfo_Summary_Group;
                }
                else if (query.ReportType.Equals("ePurgeSM"))
                {
                    sql += sql_LabelInfo_ePurgeSM_Group;
                }

            }
            return sql;
        }

        private ReportLabelInfoDetailModel.Detail GetReportLabelInfoDetailModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportLabelInfoDetailModel.Detail data = new ReportLabelInfoDetailModel.Detail

            {
                OrgCode = DBConvert.DB2String(dr["OrgCode"]),
                CLID = DBConvert.DB2String(dr["CLID"]),
                BoxID = DBConvert.DB2String(dr["BoxID"]),
                MaterialNo = DBConvert.DB2String(dr["MaterialNo"]),
                Revision = DBConvert.DB2String(dr["Revision"]),
                MaterialDesc = DBConvert.DB2String(dr["MaterialDesc"]),
                QtyBaseUOM = DBConvert.DB2Decimal(dr["QtyBaseUOM"]),
                BaseUOM = DBConvert.DB2String(dr["BaseUOM"]),
                SubInv = DBConvert.DB2String(dr["SubInv"]),
                Locator = DBConvert.DB2String(dr["Locator"]),
                StorageType = DBConvert.DB2String(dr["StorageType"]),
                AddlText = DBConvert.DB2String(dr["AddlText"]),
                StockType = DBConvert.DB2String(dr["StockType"]),
                DateCode = DBConvert.DB2String(dr["DateCode"]),
                LotNo = DBConvert.DB2String(dr["LotNo"]),
                COO = DBConvert.DB2String(dr["COO"]),
                ExpDate = DBConvert.DB2DatetimeString(dr["ExpDate"], "yyyy-MM-dd"),
                RecDocNo = DBConvert.DB2String(dr["RecDocNo"]),
                RTLot = DBConvert.DB2String(dr["RTLot"]),
                RecDate = DBConvert.DB2DatetimeNull(dr["RecDate"]),
                RoHS = DBConvert.DB2String(dr["RoHS"]),
                PurOrdNo = DBConvert.DB2String(dr["PurOrdNo"]),
                PurOrdItem = DBConvert.DB2String(dr["PurOrdItem"]),
                VendorID = DBConvert.DB2String(dr["VendorID"]),
                VendorName = DBConvert.DB2String(dr["VendorName"]),
                VendorPN = DBConvert.DB2String(dr["VendorPN"]),
                Manufacturer = DBConvert.DB2String(dr["Manufacturer"]),
                ManufacturerPN = DBConvert.DB2String(dr["ManufacturerPN"]),
                QMLStatus = DBConvert.DB2String(dr["QMLStatus"]),
                NextReviewDate = DBConvert.DB2DatetimeString(dr["NextReviewDate"], "yyyy-MM-dd"),
                ReviewStatus = DBConvert.DB2String(dr["ReviewStatus"]),
                ReviewedOn = DBConvert.DB2DatetimeString(dr["ReviewedOn"], "yyyy-MM-dd"),
                ReviewedBy = DBConvert.DB2String(dr["ReviewedBy"]),
                CreatedOn = DBConvert.DB2DatetimeNull(dr["CreatedOn"]),
                CreatedBy = DBConvert.DB2String(dr["CreatedBy"]),
                ChangedOn = DBConvert.DB2DatetimeNull(dr["ChangedOn"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                LastTransaction = DBConvert.DB2String(dr["LastTransaction"]),
                MSL = DBConvert.DB2String(dr["MSL"]),
                InvoiceNo = DBConvert.DB2String(dr["InvoiceNo"]),
                StatusCode = DBConvert.DB2String(dr["StatusCode"]),

            };
            return data;
        }


        private ReportLabelInfoSummaryModel.Summary GetReportLabelInfoSummaryModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportLabelInfoSummaryModel.Summary data = new ReportLabelInfoSummaryModel.Summary

            {
                OrgCode = DBConvert.DB2String(dr["OrgCode"]),
                MaterialNo = DBConvert.DB2String(dr["MaterialNo"]),
                Revision = DBConvert.DB2String(dr["Revision"]),
                SubInv = DBConvert.DB2String(dr["SubInv"]),
                Locator = DBConvert.DB2String(dr["Locator"]),
                StorageType = DBConvert.DB2String(dr["StorageType"]),
                QtyBaseUOM = DBConvert.DB2Decimal(dr["QtyBaseUOM"]),
                BaseUOM = DBConvert.DB2String(dr["BaseUOM"]),
                RTLot = DBConvert.DB2String(dr["RTLot"]),

            };
            return data;
        }


        private ReportLabelInfoePurgeDTModel.Detail GetReportLabelInfoePurgeDTModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportLabelInfoePurgeDTModel.Detail data = new ReportLabelInfoePurgeDTModel.Detail

            {
                OrgCode = DBConvert.DB2String(dr["OrgCode"]),
                CLID = DBConvert.DB2String(dr["CLID"]),
                MaterialNo = DBConvert.DB2String(dr["MaterialNo"]),
                QtyBaseUOM = DBConvert.DB2Decimal(dr["QtyBaseUOM"]),
                BaseUOM = DBConvert.DB2String(dr["BaseUOM"]),
                SubInv = DBConvert.DB2String(dr["SubInv"]),
                Locator = DBConvert.DB2String(dr["Locator"]),
                StockType = DBConvert.DB2String(dr["StockType"]),
                DateCode = DBConvert.DB2String(dr["DateCode"]),
                LotNo = DBConvert.DB2String(dr["LotNo"]),
                ExpDate = DBConvert.DB2DatetimeString(dr["ExpDate"], "yyyy-MM-dd"),
                RecDocNo = DBConvert.DB2String(dr["RecDocNo"]),
                RTLot = DBConvert.DB2String(dr["RTLot"]),
                RecDate = DBConvert.DB2DatetimeNull(dr["RecDate"]),
                VendorID = DBConvert.DB2String(dr["VendorID"]),
                VendorName = DBConvert.DB2String(dr["VendorName"]),
                VendorPN = DBConvert.DB2String(dr["VendorPN"]),
                Manufacturer = DBConvert.DB2String(dr["Manufacturer"]),
                ManufacturerPN = DBConvert.DB2String(dr["ManufacturerPN"]),
                ChangedOn = DBConvert.DB2DatetimeNull(dr["ChangedOn"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                LastTransaction = DBConvert.DB2String(dr["LastTransaction"]),
            };
            return data;
        }


        private ReportLabelInfoePurgeSMModel.Summary GetReportLabelInfoePurgeSMModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportLabelInfoePurgeSMModel.Summary data = new ReportLabelInfoePurgeSMModel.Summary

            {
                OrgCode = DBConvert.DB2String(dr["OrgCode"]),
                SubInv = DBConvert.DB2String(dr["SubInv"]),
                MaterialNo = DBConvert.DB2String(dr["MaterialNo"]),
                Manufacturer = DBConvert.DB2String(dr["Manufacturer"]),
                ManufacturerPN = DBConvert.DB2String(dr["ManufacturerPN"]),
                DateCode = DBConvert.DB2String(dr["DateCode"]),
                LotNo = DBConvert.DB2String(dr["LotNo"]),
                Qty = DBConvert.DB2Decimal(dr["Qty"]),
                ActualReturnedQty = DBConvert.DB2String(dr["ActualReturnedQty"]),
                DeviationQty = DBConvert.DB2String(dr["DeviationQty"]),
                Remark = DBConvert.DB2String(dr["Remark"]),

            };
            return data;
        }

        #endregion

        #endregion
    }
}
