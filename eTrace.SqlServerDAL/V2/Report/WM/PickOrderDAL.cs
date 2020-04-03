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
    public class PickOrderDAL : DALBase, IPickOrderDAL
    {                                                    
        #region corts

        public PickOrderDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public PickOrderDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields

        private const string sql_PickOrder_select = @"select * from v_openPickOrder Where 1=1 ";

        private const string sql_SupplyType_select = @"SELECT DefaultValue FROM T_ProcessProperties WHERE (ProcessID = 51) AND (Property = 'SupplyType') AND (ValidationType = '') ";

        private const string sql_Locators_select = @"exec sp_getOnhandLocators ";

        private string SourceInv = string.Empty;


        #endregion

        #region methods

        #region methods GetPickOrderDatas
        /// <summary>
        /// 获取 CLID Master Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportPickOrderModel GetPickOrderData(ReportPickOrderModelQuery query)
        {
            ReportPickOrderModel result = new ReportPickOrderModel();
            result.Data = new List<ReportPickOrderModel.Item>();
            string sql = sql_PickOrder_select;
            #region Conditions
            sql += GetReportSqlCondition(query);
            #endregion

            string ordercol = " pickorder# ";
            PageSql pSql = GetPagerSQL(query, sql, "", ordercol);

            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportPickOrderModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }


        /// <summary>
        /// 获取物料Summary Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long PickOrderDataGetRowCount(ReportPickOrderModelQuery query)
        {
            string sql = sql_PickOrder_select;
            #region Conditions
            sql += GetReportSqlCondition(query);
            #endregion
            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        private string GetReportSqlCondition(ReportPickOrderModelQuery query)
        {
            string sql = string.Empty;
            //string SourceInv = string.Empty;

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.OrgCode))
                {
                    sql += string.Format(" and OrgCode = '{0}' ", query.OrgCode); 
                }

                // Floor
                if (!string.IsNullOrEmpty(query.Floor))
                {
                    if (query.Floor.Contains("*"))
                        sql += " and prodfloor like '" + query.Floor.Replace("*", "%") + "' ";
                    else if (query.Floor.Contains(","))
                        sql += " and prodfloor in ('" + query.Floor.Replace(",", "','") + "') ";
                    else
                        sql += string.Format(" and prodfloor = '{0}' ", query.Floor);
                }

                if (!string.IsNullOrEmpty(query.DJNo))
                {
                    if (query.DJNo.Contains("*"))
                        sql += " and DJ like '" + query.DJNo.Replace("*", "%") + "' ";
                    else if (query.DJNo.Contains(","))
                        sql += " and DJ in ('" + query.DJNo.Replace(",", "','") + "') ";
                    else
                        sql += string.Format(" and DJ = '{0}' ", query.DJNo);
                }

                if (!string.IsNullOrEmpty(query.PickOrderNo))
                {
                    if (query.PickOrderNo.Contains("*"))
                        sql += " and pickorder# like '" + query.PickOrderNo.Replace("*", "%") + "' ";
                    else if (query.PickOrderNo.Contains(","))
                        sql += " and pickorder# in ('" + query.PickOrderNo.Replace(",", "','") + "') ";
                    else
                        sql += string.Format(" and pickorder# = '{0}' ", query.PickOrderNo);
                }

                if (!string.IsNullOrEmpty(query.SourceSubInv))
                {
                    SourceInv = query.SourceSubInv;
                }


                if (!string.IsNullOrEmpty(query.SupplyType))
                {
                    sql += string.Format(" and SupplyType = '{0}' ", query.SupplyType);
                }


                if (!string.IsNullOrEmpty(query.Status))
                {
                    sql += string.Format(" and status = '{0}' ", query.Status);
                }


                if (!query.CreationFrom.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), createdon, 23) >='{0}'", query.CreationFrom.ToString("yyyy-MM-dd"));
                }
                if (!query.CreationTo.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), createdon, 23) <='{0}'", query.CreationTo.ToString("yyyy-MM-dd"));
                }


            }
            return sql;
        }

        private ReportPickOrderModel.Item GetReportPickOrderModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportPickOrderModel.Item data = new ReportPickOrderModel.Item

            {
                OrgCode = DBConvert.DB2String(dr["OrgCode"]),
                PickOrder = DBConvert.DB2String(dr["PickOrder#"]),
                DJ = DBConvert.DB2String(dr["DJ"]),
                Product = DBConvert.DB2String(dr["Product"]),
                BuildQty = DBConvert.DB2Decimal(dr["BuildQty"]),
                ProdFloor = DBConvert.DB2String(dr["ProdFloor"]),
                ProdLine = DBConvert.DB2String(dr["ProdLine"]),
                ETA = DBConvert.DB2String(dr["ETA"]),
                SupplyType = DBConvert.DB2String(dr["SupplyType"]),
                DestSubInv = DBConvert.DB2String(dr["DestSubInv"]),
                DestLocator = DBConvert.DB2String(dr["DestLocator"]),
                ReasonCode = DBConvert.DB2String(dr["ReasonCode"]),
                ItemNo = DBConvert.DB2String(dr["Item"]),
                ReqQty = DBConvert.DB2Decimal(dr["ReqQty"]),
                PickedQty = DBConvert.DB2Decimal(dr["PickedQty"]),
                OpenQty = DBConvert.DB2Decimal(dr["OpenQty"]),
                Status = DBConvert.DB2String(dr["Status"]),
                OnhandLocators = DBConvert.DB2String(dr["OnhandLocators"]),
                CreatedBy = DBConvert.DB2String(dr["CreatedBy"]),
                CreatedOn = DBConvert.DB2DatetimeNull(dr["CreatedOn"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                ChangedOn = DBConvert.DB2DatetimeNull(dr["ChangedOn"]),
            };

            if (!string.IsNullOrEmpty(SourceInv))
                {
                   data.OnhandLocators = GetLocators(data.ItemNo, SourceInv);
                };


            return data;
        }


        /// <summary>
        /// 获取SupplyType
        /// </summary>
        /// <returns></returns>
        public List<string> GetSupplyType()
        {
            string myType = string.Empty;
            string sql = sql_SupplyType_select;

            List<string> result = new List<string>();
            result.Add(string.Empty);

            dbHelper.ExecuteReader(sql, (dr) =>
            {
                myType = DBConvert.DB2String(dr["DefaultValue"]);
            });

            string[] sArray = myType.Split('~');
            foreach (string i in sArray)
            {
                result.Add(i.ToString());
            }

           return result;
        }


        public string GetLocators(string Items, string SLOC)
        {
            string result = "";
            string sql = sql_Locators_select + " '" + Items + "','" + SLOC + "' ";

            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result += DBConvert.DB2String(dr["StorageBin"]).ToString() + "/" + DBConvert.DB2String(dr["QtyBaseUOM"]).ToString() + ",";
           });

            if ((result != ""))
                result = result.Substring(0, result.LastIndexOf(","));

            return result;
        }


        #endregion

        #endregion
    }
}
