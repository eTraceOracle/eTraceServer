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
    public class RawMatPackingDAL : DALBase, IRawMatPackingDAL
    {                                                    
        #region corts

        public RawMatPackingDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public RawMatPackingDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields

        private const string sql_RawMatPacking_select = @"SELECT A.OrgCode, PalletWeight, A.PalletID, A.BoxID, A.CLID,  MaterialNo as ItemNo, QtyBaseUOM, BaseUOM, SLOC as SubInv, StorageBin as Locator, DateCode, LotNo, CountryOfOrigin as COO, ExpDate, "
                                 + "RTLot, Manufacturer, ManufacturerPN, B.ChangedOn, B.ChangedBy, LastTransaction "
                                 + "FROM T_CLMaster_Packing as A WITH (nolock) inner join T_CLMaster as B WITH (nolock) on A.CLID = B.CLID where 1=1 ";


        #endregion

        #region methods

        #region methods GetRawMatPackingDatas
        /// <summary>
        /// 获取 CLID Master Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportRawMatPackingModel GetRawMatPackingData(ReportRawMatPackingModelQuery query)
        {
            ReportRawMatPackingModel result = new ReportRawMatPackingModel();
            result.Data = new List<ReportRawMatPackingModel.Item>();
            string sql = sql_RawMatPacking_select;
            #region Conditions
            sql += GetReportSqlCondition(query);
            #endregion

            string ordercol = " A.PalletID, A.BoxID, A.CLID ";
            PageSql pSql = GetPagerSQL(query, sql, "", ordercol);

            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportRawMatPackingModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }


        /// <summary>
        /// 获取物料Summary Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long RawMatPackingDataGetRowCount(ReportRawMatPackingModelQuery query)
        {
            string sql = sql_RawMatPacking_select;
            #region Conditions
            sql += GetReportSqlCondition(query);
            #endregion
            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        private string GetReportSqlCondition(ReportRawMatPackingModelQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.OrgCode))
                {
                    sql += string.Format(" and A.OrgCode = '{0}' ", query.OrgCode); 
                }

                if (!string.IsNullOrEmpty(query.ItemNo))
                {
                    if (query.ItemNo.Contains("*"))
                        sql += " and B.MaterialNo like '" + query.ItemNo.Replace("*", "%") + "' ";
                    else if (query.ItemNo.Contains(","))
                        sql += " and B.MaterialNo in ('" + query.ItemNo.Replace(",", "','") + "') ";
                    else
                        sql += string.Format(" and B.MaterialNo = '{0}' ", query.ItemNo);
                }

                if (!string.IsNullOrEmpty(query.BoxIDFrom))
                    sql += " and A.BoxID >= '" + query.BoxIDFrom + "' ";

                if (!string.IsNullOrEmpty(query.BoxIDTo))
                    sql += " and A.BoxID <= '" + query.BoxIDTo + "' ";

                if (!string.IsNullOrEmpty(query.PalletIDFrom))
                    sql += " and A.PalletID >= '" + query.PalletIDFrom + "' ";

                if (!string.IsNullOrEmpty(query.PalletIDTo))
                    sql += " and A.PalletID <= '" + query.PalletIDTo + "' ";

                if (!string.IsNullOrEmpty(query.CLID))
                {
                    if (query.CLID.Contains("*"))
                        sql += " and A.CLID like '" + query.CLID.Replace("*", "%") + "' ";
                    else if (query.CLID.Contains(","))
                        sql += " and A.CLID in ('" + query.CLID.Replace(",", "','") + "') ";
                    else
                        sql += string.Format(" and A.CLID = '{0}' ", query.CLID);
                }

            }
            return sql;
        }

        private ReportRawMatPackingModel.Item GetReportRawMatPackingModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportRawMatPackingModel.Item data = new ReportRawMatPackingModel.Item

            {
                OrgCode = DBConvert.DB2String(dr["OrgCode"]),
                PalletWeight = DBConvert.DB2String(dr["PalletWeight"]),
                PalletID = DBConvert.DB2String(dr["PalletID"]),
                BoxID = DBConvert.DB2String(dr["BoxID"]),
                CLID = DBConvert.DB2String(dr["CLID"]),
                ItemNo = DBConvert.DB2String(dr["ItemNo"]),
                QtyBaseUOM = DBConvert.DB2Decimal(dr["QtyBaseUOM"]),
                BaseUOM = DBConvert.DB2String(dr["BaseUOM"]),
                SubInv = DBConvert.DB2String(dr["SubInv"]),
                Locator = DBConvert.DB2String(dr["Locator"]),
                DateCode = DBConvert.DB2String(dr["DateCode"]),
                LotNo = DBConvert.DB2String(dr["LotNo"]),
                COO = DBConvert.DB2String(dr["COO"]),
                ExpDate = DBConvert.DB2DatetimeString(dr["ExpDate"], "yyyy-MM-dd"),
                RTLot = DBConvert.DB2String(dr["RTLot"]),
                Manufacturer = DBConvert.DB2String(dr["Manufacturer"]),
                ManufacturerPN = DBConvert.DB2String(dr["ManufacturerPN"]),
                ChangedOn = DBConvert.DB2DatetimeNull(dr["ChangedOn"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                LastTransaction = DBConvert.DB2String(dr["LastTransaction"]),
            };

            return data;
        }



        #endregion

        #endregion
    }
}
