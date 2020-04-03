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
    public class SearchCLIDDAL : DALBase, ISearchCLIDDAL
    {                                                    
        #region corts

        public SearchCLIDDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public SearchCLIDDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields

        private const string sql_SearchCLID_select = @"SELECT OrgCode,CLID,MaterialNo,MaterialRevision,MaterialDesc,QtyBaseUOM,BaseUOM,SLOC as SubInv,StorageBin as Locator,
                      StorageType,CreatedOn,CreatedBy,ChangedOn,ChangedBy,DateCode,LotNo,CountryOfOrigin as COO,ExpDate,RecDocNo,RecDocItem,RTLot,RecDate,RoHS,PurOrdNo,PurOrdItem,
                      DeliveryType,VendorID,VendorName,VendorPN,InvoiceNo,BillofLading,DN,HeaderText,ProdDate,ReasonCode,Operator,StockType,
                      ItemText,MatSuffix1,MatSuffix2,MatSuffix3,AddlText,ReferenceCLID,BoxID,Manufacturer,ManufacturerPN,QMLStatus,NextReviewDate,
                      ReviewStatus,ReviewedOn,ReviewedBy,SampleSize,AddlData,Stemp,MSL,SupplyType,LastDJ,LastTransaction,StatusCode
                      FROM T_CLMaster with (nolock) where 1=1 ";

        private const string sql_Archive_select = @"SELECT OrgCode,CLID,MaterialNo,MaterialRevision,MaterialDesc,QtyBaseUOM,BaseUOM,SLOC as SubInv,StorageBin as Locator,
                      StorageType,CreatedOn,CreatedBy,ChangedOn,ChangedBy,DateCode,LotNo,CountryOfOrigin as COO,ExpDate,RecDocNo,RecDocItem,RTLot,RecDate,RoHS,PurOrdNo,PurOrdItem,
                      DeliveryType,VendorID,VendorName,VendorPN,InvoiceNo,BillofLading,DN,HeaderText,ProdDate,ReasonCode,Operator,StockType,
                      ItemText,MatSuffix1,MatSuffix2,MatSuffix3,AddlText,ReferenceCLID,BoxID,Manufacturer,ManufacturerPN,QMLStatus,NextReviewDate,
                      ReviewStatus,ReviewedOn,ReviewedBy,SampleSize,AddlData,Stemp,MSL,SupplyType,LastDJ,LastTransaction,StatusCode
                      FROM V_CLMaster with (nolock) where 1=1 ";

        #endregion

        #region methods

        #region methods GetSearchCLIDDatas

        /// <summary>
        /// 获取 CLID Master Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportSearchCLIDModel GetSearchCLIDData(ReportSearchCLIDModelQuery query)
        {
            ReportSearchCLIDModel result = new ReportSearchCLIDModel();
            result.Data = new List<ReportSearchCLIDModel.Item>();

            string sql = sql_SearchCLID_select;               //Default Type: Current
            if (query.ReportType.Equals("Archive"))
            {
                sql = sql_Archive_select;
            }

            #region Conditions
            sql += GetReportSqlCondition(query);
            #endregion

            string ordercol = " CLID";
            PageSql pSql = GetPagerSQL(query, sql, "", ordercol);

            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount, 1200);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportSearchCLIDModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }


        /// <summary>
        /// 获取物料Summary Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long SearchCLIDDataGetRowCount(ReportSearchCLIDModelQuery query)
        {
            string sql = sql_SearchCLID_select;
            if (query.ReportType.Equals("Archive"))
            {
                sql = sql_Archive_select;
            }

            #region Conditions
            sql += GetReportSqlCondition(query);
            #endregion

            long rowCount = dbHelper.GetCount(GetSQLCount(sql), 1200);
            return rowCount;
        }

        private string GetReportSqlCondition(ReportSearchCLIDModelQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.OrgCode))
                {
                    sql += string.Format(" and OrgCode = '{0}' ", query.OrgCode); //"and " + "OrgCode= '" + query.OrgCode + "' ";
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
                        sql = sql + "and " + "MaterialNo like '" + query.MaterialNo.Replace("*", "%") + "' ";
                    else if (query.MaterialNo.Contains(","))
                        sql += " and MaterialNo in ('" + query.MaterialNo.Replace(",", "','") + "') ";
                    else
                        sql += string.Format(" and MaterialNo = '{0}' ", query.MaterialNo);
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

                if (!string.IsNullOrEmpty(query.DateCode))
                {
                    sql += string.Format(" and DateCode = '{0}' ", query.DateCode);
                }

                if (!string.IsNullOrEmpty(query.LotNo))
                {
                    sql += string.Format(" and LotNo = '{0}' ", query.LotNo);
                }

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

                if (!string.IsNullOrEmpty(query.ManufacturerPN))
                {
                    sql += string.Format(" and ManufacturerPN = '{0}' ", query.ManufacturerPN);
                }

                if (!string.IsNullOrEmpty(query.PODJNo))
                {
                    if (query.PODJNo.Contains("*"))
                        sql += " and PurOrdNo like '" + query.PODJNo.Replace("*", "%") + "' ";
                    else if (query.PODJNo.Contains(","))
                        sql += " and PurOrdNo in ('" + query.PODJNo.Replace(",", "','") + "') ";
                    else
                        sql += string.Format(" and PurOrdNo = '{0}' ", query.PODJNo);
                }


                if (!string.IsNullOrEmpty(query.QMLStatus))
                {
                    sql += string.Format(" and QMLStatus = '{0}' ", query.QMLStatus);
                }

                if (!string.IsNullOrEmpty(query.StatusCode))
                {
                    sql += string.Format(" and StatusCode = '{0}' ", query.StatusCode);
                }


                //if (sql.Length > 4000)
                //{
                //    Page.Response.Write("Your condition is too long to get the result. Input again");
                //    return;
                //}

            }
            return sql;
        }

        private ReportSearchCLIDModel.Item GetReportSearchCLIDModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSearchCLIDModel.Item data = new ReportSearchCLIDModel.Item

            {
                OrgCode = DBConvert.DB2String(dr["OrgCode"]),
                CLID = DBConvert.DB2String(dr["CLID"]),
                MaterialNo = DBConvert.DB2String(dr["MaterialNo"]),
                MaterialRevision = DBConvert.DB2String(dr["MaterialRevision"]),
                MaterialDesc = DBConvert.DB2String(dr["MaterialDesc"]),
                QtyBaseUOM = DBConvert.DB2Decimal(dr["QtyBaseUOM"]),
                BaseUOM = DBConvert.DB2String(dr["BaseUOM"]),
                SubInv = DBConvert.DB2String(dr["SubInv"]),                
                Locator = DBConvert.DB2String(dr["Locator"]),          
                StorageType = DBConvert.DB2String(dr["StorageType"]),
                CreatedOn = DBConvert.DB2DatetimeNull(dr["CreatedOn"]),
                CreatedBy = DBConvert.DB2String(dr["CreatedBy"]),
                ChangedOn = DBConvert.DB2DatetimeNull(dr["ChangedOn"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                DateCode = DBConvert.DB2String(dr["DateCode"]),
                LotNo = DBConvert.DB2String(dr["LotNo"]),
                COO = DBConvert.DB2String(dr["COO"]),         
                ExpDate = DBConvert.DB2DatetimeString(dr["ExpDate"], "yyyy-MM-dd"),
                RecDocNo = DBConvert.DB2String(dr["RecDocNo"]),
                RecDocItem = DBConvert.DB2String(dr["RecDocItem"]),
                RTLot = DBConvert.DB2String(dr["RTLot"]),
                RecDate = DBConvert.DB2DatetimeNull(dr["RecDate"]),
                RoHS = DBConvert.DB2String(dr["RoHS"]),
                PurOrdNo = DBConvert.DB2String(dr["PurOrdNo"]),
                PurOrdItem = DBConvert.DB2String(dr["PurOrdItem"]),
                DeliveryType = DBConvert.DB2String(dr["DeliveryType"]),
                VendorID = DBConvert.DB2String(dr["VendorID"]),
                VendorName = DBConvert.DB2String(dr["VendorName"]),
                VendorPN = DBConvert.DB2String(dr["VendorPN"]),
                InvoiceNo = DBConvert.DB2String(dr["InvoiceNo"]),
                BillofLading = DBConvert.DB2String(dr["BillofLading"]),
                DN = DBConvert.DB2String(dr["DN"]),
                HeaderText = DBConvert.DB2String(dr["HeaderText"]),
                ProdDate = DBConvert.DB2DatetimeString(dr["ProdDate"], "yyyy-MM-dd"),
                ReasonCode = DBConvert.DB2String(dr["ReasonCode"]),
                Operator = DBConvert.DB2String(dr["Operator"]),
                StockType = DBConvert.DB2String(dr["StockType"]),
                ItemText = DBConvert.DB2String(dr["ItemText"]),
                MatSuffix1 = DBConvert.DB2String(dr["MatSuffix1"]),
                MatSuffix2 = DBConvert.DB2String(dr["MatSuffix2"]),
                MatSuffix3 = DBConvert.DB2String(dr["MatSuffix3"]),
                AddlText = DBConvert.DB2String(dr["AddlText"]),
                ReferenceCLID = DBConvert.DB2String(dr["ReferenceCLID"]),
                BoxID = DBConvert.DB2String(dr["BoxID"]),
                Manufacturer = DBConvert.DB2String(dr["Manufacturer"]),
                ManufacturerPN = DBConvert.DB2String(dr["ManufacturerPN"]),
                QMLStatus = DBConvert.DB2String(dr["QMLStatus"]),
                NextReviewDate = DBConvert.DB2DatetimeString(dr["NextReviewDate"], "yyyy-MM-dd"),
                ReviewStatus = DBConvert.DB2String(dr["ReviewStatus"]),
                ReviewedOn = DBConvert.DB2DatetimeString(dr["ReviewedOn"], "yyyy-MM-dd"),
                ReviewedBy = DBConvert.DB2String(dr["ReviewedBy"]),
                SampleSize = DBConvert.DB2String(dr["SampleSize"]),
                AddlData = DBConvert.DB2String(dr["AddlData"]),
                Stemp = DBConvert.DB2String(dr["Stemp"]),
                MSL = DBConvert.DB2String(dr["MSL"]),
                SupplyType = DBConvert.DB2String(dr["SupplyType"]),
                LastDJ = DBConvert.DB2String(dr["LastDJ"]),
                LastTransaction = DBConvert.DB2String(dr["LastTransaction"]),
                StatusCode = DBConvert.DB2String(dr["StatusCode"]),

            };
            return data;
        }


        #endregion

        #endregion
    }
}
