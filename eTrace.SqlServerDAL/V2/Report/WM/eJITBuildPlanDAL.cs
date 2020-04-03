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

using System.Reflection;
using System.Configuration;
using System.Data;

namespace eTrace.Report.SqlServerDAL.V2.Report
{
    public class eJITBuildPlanDAL : DALBase, IeJITBuildPlanDAL
    {                                                    
        #region corts

        public eJITBuildPlanDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public eJITBuildPlanDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields

        private const string sql_eJITBuildPlan_Search_Base = @"SELECT  T_BuildPlanItem.Productionfloor, T_BuildPlanHeader.CreateOn, T_BuildPlanHeader.CreateBy, T_BuildPlanItem.DJNO, T_BuildPlanItem.Model, 
                      T_BuildPlanItem.Subinventory, T_BuildPlanItem.RequireQty, T_BuildPlanItem.OrgCode,isnull(T_BuildPlanItem.WipQty,0) as WipQty, T_BuildPlanItem.DeliveryDate, T_BuildPlanItem.ShipInstruction
					  FROM T_BuildPlanHeader with (nolock) INNER JOIN T_BuildPlanItem with (nolock) ON T_BuildPlanHeader.BuildPlanId = T_BuildPlanItem.BuildPlanId ";

        private const string sql_eJITBuildPlan_Search_Join = @"INNER JOIN  T_KBPickingList with (nolock) ON T_BuildPlanHeader.BuildPlanId = T_KBPickingList.BuildPlanId ";

        private const string sql_eJITBuildPlan_Detail_Base = @"SELECT OrgCode,CLID,BoxID, MaterialNo,MaterialRevision AS Revision,MaterialDesc,QtyBaseUOM,BaseUOM,SLOC as SubInv, StorageBin as Locator, AddlText, ISNULL(StorageType,'') as StorageType, StockType,DateCode,LotNo,CountryofOrigin as COO, ExpDate, RecDocNo,  RTLot,RecDate,RoHS, PurOrdNo, PurOrdItem, VendorID, VendorName, VendorPN,Manufacturer, ManufacturerPN, QMLStatus, NextReviewDate, ReviewStatus,ReviewedOn, ReviewedBy,CreatedOn,CreatedBy,ChangedOn,ChangedBy ,LastTransaction, MSL, InvoiceNo,StatusCode FROM T_CLMaster WITH (nolock) where 1=1 ";   

        #endregion

        #region methods


        #region methods GeteJITBuildPlanDatas

        private string GetSqlCondition_Search(ReporteJITBuildPlanModelQuery query)
        {
            string sql = string.Empty;
            string condition1 = "";
            string condition2 = "";

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.OrgCode))
                {
                    condition1 += string.Format(" OrgCode = '{0}' ", query.OrgCode); 
                }

                if (!string.IsNullOrEmpty(query.Productionfloor))
                {
                    if (query.Productionfloor.Contains(","))
                      condition1 += " and  Productionfloor in  ('" + query.Productionfloor.Replace(",", "','") + "') ";
                    else
                      condition1 += string.Format(" and Productionfloor = '{0}' ", query.Productionfloor); 
               }

                if (!query.UploadFrom.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    if (query.UploadFrom.ToString().Contains("*") )
                    {
                        query.UploadFrom = DateTime.Now;
                    }
                   condition1 += string.Format(" and CONVERT(varchar(100), Createon, 23) >='{0}'", query.UploadFrom.ToString("yyyy-MM-dd"));
                }

                if (!query.UploadTo.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    if (query.UploadTo.ToString().Contains("*"))
                    {
                      query.UploadTo = DateTime.Now.AddDays(-15);
                    }

                    condition1 += string.Format(" and CONVERT(varchar(100), Createon, 23) <='{0}'", query.UploadTo.ToString("yyyy-MM-dd"));
                }

                if (!string.IsNullOrEmpty(query.UploadBy))
                {
                    condition1 += string.Format(" and createBy = '{0}' ", query.UploadBy);
                }


                if (!string.IsNullOrEmpty(query.ItemNO))
                {
                     sql += sql_eJITBuildPlan_Search_Join;
                    //condition2 += string.Format(" and ItemNO = '{0}' ", query.ItemNO);

                    if (query.ItemNO.Contains(","))
                        condition2 += " and ItemNO in  ('" + query.ItemNO.Replace(",", "','") + "') ";
                    else
                        condition2 += string.Format(" and ItemNO = '{0}' ", query.ItemNO);
                }


                sql += " WHERE " + condition1;


               if (!string.IsNullOrEmpty(condition2))
                {
                    sql += condition2;
                }

            }
            return sql;
        }

        private string GetSqlCondition_Detail(ReporteJITBuildPlanModelQuery query, string GetRowCount = "N")
        {
            string sql = string.Empty;
            string condition1 = "";
            string condition2 = "";

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.OrgCode))
                {
                    condition1 += string.Format(" and OrgCode = '{0}' ", query.OrgCode);
                }

                if (!string.IsNullOrEmpty(query.Productionfloor))
                {
                    if (query.Productionfloor.Contains(","))
                        condition1 += " and  Productionfloor in  ('" + query.Productionfloor.Replace(",", "','") + "') ";
                    else
                        condition1 += string.Format(" and Productionfloor = '{0}' ", query.Productionfloor);
                }

                if (!query.UploadFrom.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    if (query.UploadFrom.ToString().Contains("*"))
                    {
                        query.UploadFrom = DateTime.Now;
                    }
                    condition2 += string.Format(" and CONVERT(varchar(100), Createon, 23) >='{0}'", query.UploadFrom.ToString("yyyy-MM-dd"));
                }

                if (!query.UploadTo.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    if (query.UploadTo.ToString().Contains("*"))
                    {
                        query.UploadTo = DateTime.Now.AddDays(-7);
                    }

                    condition2 += string.Format(" and CONVERT(varchar(100), Createon, 23) <='{0}'", query.UploadTo.ToString("yyyy-MM-dd"));
                }

                if (!string.IsNullOrEmpty(query.ItemNO))
                {
                    if (query.ItemNO.Contains(","))
                        condition2 += " and ItemNO in  ('" + query.ItemNO.Replace(",", "','") + "') ";
                    else
                        condition2 += string.Format(" and ItemNO = '{0}' ", query.ItemNO);
                }

                if (!string.IsNullOrEmpty(query.UploadBy))
                {
                    condition2 += string.Format(" and createBy = '{0}' ", query.UploadBy);
                }

                if (query.DisplayOpen)   //  True
                {
                    condition2 += string.Format(" and shortage <> recqty ");
                }

                condition1 = condition1.Replace("'", "''");
                condition2 = condition2.Replace("'", "''");

                int CurrPage = 0;
                int PageSize = 0;
                if (query.Pager != null)
                  {
                     CurrPage = (query.Pager.CurrentPage - 1) * query.Pager.PageSize;
                     PageSize = query.Pager.PageSize;
                  }

                sql = string.Format("exec sp_GeteJitBuildPlanDetail_V2  '{0}','{1}','{2}','{3}','{4}' ", condition1, condition2, CurrPage, PageSize, GetRowCount);


            }
            return sql;
        }


        private ReporteJITBuildPlanSearchModel.Item GetReporteJITBuildPlanSearchModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReporteJITBuildPlanSearchModel.Item data = new ReporteJITBuildPlanSearchModel.Item

            {
                Productionfloor = DBConvert.DB2String(dr["Productionfloor"]),
                CreateOn = DBConvert.DB2Datetime(dr["CreateOn"]),                         //Not NULL
                CreateBy = DBConvert.DB2String(dr["CreateBy"]),
                DJNO = DBConvert.DB2String(dr["DJNO"]),
                Model = DBConvert.DB2String(dr["Model"]),
                Subinventory = DBConvert.DB2String(dr["Subinventory"]),
                RequireQty = DBConvert.DB2Decimal(dr["RequireQty"]),
                OrgCode = DBConvert.DB2String(dr["OrgCode"]),
                WipQty = DBConvert.DB2Decimal(dr["WipQty"]),
                DeliveryDate = DBConvert.DB2String(dr["DeliveryDate"]),                 //Not NULL
                ShipInstruction = DBConvert.DB2String(dr["ShipInstruction"]),
            };
            return data;
        }


        private ReporteJITBuildPlanDetailModel.Item GetReporteJITBuildPlanDetailModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReporteJITBuildPlanDetailModel.Item data = new ReporteJITBuildPlanDetailModel.Item

            {
                Productionfloor = DBConvert.DB2String(dr["Productionfloor"]),
                CreateOn = DBConvert.DB2Datetime(dr["CreateOn"]),
                CreateBy = DBConvert.DB2String(dr["CreateBy"]),
                SubInventory = DBConvert.DB2String(dr["SubInventory"]),
                OrgCode = DBConvert.DB2String(dr["OrgCode"]),
                ItemNo = DBConvert.DB2String(dr["ItemNo"]),
                ItemRequiredQty = DBConvert.DB2Decimal(dr["ItemRequiredQty"]),
                OrderQTy = DBConvert.DB2Decimal(dr["OrderQTy"]),
                DeliveryDate = DBConvert.DB2String(dr["DeliveryDate"]),
                OrderNumber = DBConvert.DB2String(dr["OrderNumber"]),
                RecQty = DBConvert.DB2Decimal(dr["RecQty"]),
                RecBy = DBConvert.DB2String(dr["RecBy"]),
                RecON = DBConvert.DB2String(dr["RecON"]),
                eJITID = DBConvert.DB2String(dr["eJITID"]),
                Status = DBConvert.DB2String(dr["Status"]),
                ShipInstruction = DBConvert.DB2String(dr["ShipInstruction"]),
            };


            //Call eTrace Web Service function to get Valid Reqline Status
            string myDNPO = "";
            string InputType = "";
            string OrderNo = data.OrderNumber;

            try
            {
                if (!string.IsNullOrEmpty(OrderNo))
                {
                    if (data.RecBy == "" & OrderNo.IndexOf("/") > -1)
                     {
                         InputType = "IR/PR";
                         myDNPO = OrderNo.Substring(0, OrderNo.IndexOf("/"));
                     }
                    else if (data.RecBy != "" & OrderNo.IndexOf("/") == -1)
                    {
                      if (OrderNo.Substring(0, 3) == "130")
                      {
                          InputType = "PO";
                          myDNPO = OrderNo;
                      }
                      else
                      {
                          InputType = "DN";
                          myDNPO = OrderNo;
                      }
                    };

                    int myeJITID = DBConvert.DB2Int(data.eJITID);
                    data.Status = eTraceWS.ValidReqlineStatus(data.OrgCode, myDNPO, InputType, myeJITID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return data;
        }



        /// <summary>
        /// 按照条件查找Search Data，不分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<ReporteJITBuildPlanSearchModel.Item> GeteJITBuildPlanSearchData(ReporteJITBuildPlanModelQuery query)
        {
            string sql = sql_eJITBuildPlan_Search_Base + GetSqlCondition_Search(query);
            string orderBy = " Productionfloor ";

            string sqlData = GetSQLData(sql, orderBy);
            return dbHelper.GetList<ReporteJITBuildPlanSearchModel.Item>(sqlData, GetReporteJITBuildPlanSearchModel);
        }


        /// <summary>
        /// 按照条件查找Search Data，分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<ReporteJITBuildPlanSearchModel.Item> GeteJITBuildPlanSearchByPage(ReporteJITBuildPlanModelQuery query)
        {
            string sql = sql_eJITBuildPlan_Search_Base + GetSqlCondition_Search(query);

            string orderBy = " Productionfloor ";
            query.Pager.Order = orderBy;

            string sqlDataByPage = GetSQLDataByPage(sql, query.Pager);
            return dbHelper.GetList<ReporteJITBuildPlanSearchModel.Item>(sqlDataByPage, GetReporteJITBuildPlanSearchModel);
        }


        /// <summary>
        /// 获取查询结果行数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long eJITBuildPlanSearchDataGetRowCount(ReporteJITBuildPlanModelQuery query)
        {
            string sql = sql_eJITBuildPlan_Search_Base + GetSqlCondition_Search(query);
            long rowCount = dbHelper.GetCount(GetSQLCount(sql));
            return rowCount;
        }


        /// <summary>
        /// 按照条件查找Detail Data，不分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<ReporteJITBuildPlanDetailModel.Item> GeteJITBuildPlanDetailData(ReporteJITBuildPlanModelQuery query)
        { 
            string sql = GetSqlCondition_Detail(query);
            return dbHelper.GetList<ReporteJITBuildPlanDetailModel.Item>(sql, GetReporteJITBuildPlanDetailModel);
        }


        /// <summary>
        /// 按照条件查找Detail Data，分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<ReporteJITBuildPlanDetailModel.Item> GeteJITBuildPlanDetailByPage(ReporteJITBuildPlanModelQuery query)
        {
            string sql = GetSqlCondition_Detail(query);
            return dbHelper.GetList<ReporteJITBuildPlanDetailModel.Item>(sql, GetReporteJITBuildPlanDetailModel);
        }


        /// <summary>
        /// 获取查询结果行数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long eJITBuildPlanDetailDataGetRowCount(ReporteJITBuildPlanModelQuery query)
        {
            string GetRowCount = "Y";
            string sql = GetSqlCondition_Detail(query, GetRowCount);
            //long rowCount = dbHelper.GetPageCount(GetSQLCount(sql));

            long rowCount = dbHelper.GetCount(sql); 

            return rowCount;
        }
        #endregion

        #endregion
    }
}
