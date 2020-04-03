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
    public class ComponentsUsedDAL : DALBase, IComponentsUsedDAL
    {                                                    
        #region corts

        public ComponentsUsedDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public ComponentsUsedDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields

        private const string sql_base_select = @"SELECT A.OrgCode, A.MaterialNo, A.MaterialRevision, A.CLID, A.CLIDQty, A.SubInv, A.Locator, A.DJNumber, A.Product,
			   A.Manufacturer, A.ManufacturerPN, A.VendorID, A.VendorName, A.PurOrdNo, A.PurOrdItem, A.RTLot, A.MaterialDesc, 
			   CASE ISNULL(CHARINDEX('S', A.AddlData), 0) WHEN 0 THEN '' ELSE 'S' END AS Safety,
			   A.DateCode, A.LotNo, A.RecDate, A.IssueDate, A.ChangedBy, A.ExpDate, A.Remarks, A.MSL, A.ReturnDate
	      FROM (
	           SELECT  T_CLMaster.MaterialNo,T_CLMaster.MaterialRevision,T_CLMaster.CLID, T_CLMaster.Orgcode,T_CLMaster.SLOC as SubInv,T_CLMaster.StorageBin as Locator,T_CLMaster.Manufacturer,T_CLMaster.ManufacturerPN,T_CLMaster.VendorID,T_CLMaster.VendorName,T_CLMaster.PurOrdNo,T_CLMaster.RTLot,T_CLMaster.RecDate,
               T_CLMaster.MaterialDesc, T_CLMaster.DateCode, T_CLMaster.LotNo, T_PDTO_PO.PO as DJNumber, T_PO_CLID.CLIDQty, T_PO_CLID.IssueDate,T_PO_CLID.ChangedBy, T_CLMaster.AddlData, T_CLMaster.PurOrdItem, T_PDTO_PO.Product,T_CLMaster.ExpDate, T_PO_CLID.Remarks,T_CLMaster.MSL, T_PO_CLID.ReturnDate
               FROM  T_CLMaster with (nolock) INNER JOIN T_PO_CLID with (nolock) 
			   ON T_CLMaster.CLID = T_PO_CLID.CLID INNER JOIN T_PDTO_PO with (nolock) ON T_PO_CLID.PDTO = T_PDTO_PO.PDTO WHERE T_PO_CLID.PDTO IS NOT NULL ";

        private const string sql_select2 = @" union 
               SELECT T_CLMaster_1.MaterialNo, T_CLMaster_1.MaterialRevision, T_CLMaster_1.CLID, T_CLMaster_1.Orgcode, T_CLMaster_1.SLOC as SubInv, T_CLMaster_1.StorageBin as Locator, T_CLMaster_1.Manufacturer, T_CLMaster_1.ManufacturerPN, T_CLMaster_1.VendorID, T_CLMaster_1.VendorName, T_CLMaster_1.PurOrdNo, T_CLMaster_1.RTLot, T_CLMaster_1.RecDate,
               T_CLMaster_1.MaterialDesc, T_CLMaster_1.DateCode, T_CLMaster_1.LotNo,
               T_PO_CLID_1.PO as DJNumber, T_PO_CLID_1.CLIDQty, T_PO_CLID_1.IssueDate, T_PO_CLID_1.ChangedBy, T_CLMaster_1.AddlData, T_CLMaster_1.PurOrdItem, T_PO_CLID_1.Product, T_CLMaster_1.ExpDate, T_PO_CLID_1.Remarks, T_CLMaster_1.MSL, T_PO_CLID_1.ReturnDate
               FROM T_CLMaster AS T_CLMaster_1 with (nolock) INNER JOIN T_PO_CLID AS T_PO_CLID_1 with(nolock)
               ON T_CLMaster_1.CLID = T_PO_CLID_1.CLID
               WHERE  T_PO_CLID_1.PDTO IS NULL";

        private const string sql_Archive_select = @"SELECT A.OrgCode, A.MaterialNo, A.MaterialRevision, A.CLID, A.CLIDQty, A.SubInv, A.Locator, A.DJNumber, A.Product,
			   A.Manufacturer, A.ManufacturerPN, A.VendorID, A.VendorName, A.PurOrdNo, A.PurOrdItem, A.RTLot, A.MaterialDesc, 
			   CASE ISNULL(CHARINDEX('S', A.AddlData), 0) WHEN 0 THEN '' ELSE 'S' END AS Safety,
			   A.DateCode, A.LotNo, A.RecDate, A.IssueDate, A.ChangedBy, A.ExpDate, A.Remarks, A.MSL, A.ReturnDate
	      FROM (
	           SELECT  T_CLMaster.MaterialNo,T_CLMaster.MaterialRevision,T_CLMaster.CLID, T_CLMaster.Orgcode,T_CLMaster.SLOC as SubInv,T_CLMaster.StorageBin as Locator,T_CLMaster.Manufacturer,T_CLMaster.ManufacturerPN,T_CLMaster.VendorID,T_CLMaster.VendorName,T_CLMaster.PurOrdNo,T_CLMaster.RTLot,T_CLMaster.RecDate,
               T_CLMaster.MaterialDesc, T_CLMaster.DateCode, T_CLMaster.LotNo, T_PDTO_PO.PO as DJNumber, T_PO_CLID.CLIDQty, T_PO_CLID.IssueDate,T_PO_CLID.ChangedBy, T_CLMaster.AddlData, T_CLMaster.PurOrdItem, T_PDTO_PO.Product,T_CLMaster.ExpDate, '' AS Remarks, T_CLMaster.MSL, T_PO_CLID.ReturnDate
               FROM  V_CLMaster as T_CLMaster with (nolock) INNER JOIN V_PO_CLID as T_PO_CLID with (nolock) 
			   ON T_CLMaster.CLID = T_PO_CLID.CLID INNER JOIN V_PDTO_PO as T_PDTO_PO with (nolock) ON T_PO_CLID.PDTO = T_PDTO_PO.PDTO WHERE T_PO_CLID.PDTO IS NOT NULL ";

        private const string sql_Archive_select2 = @" union 
               SELECT T_CLMaster_1.MaterialNo, T_CLMaster_1.MaterialRevision, T_CLMaster_1.CLID, T_CLMaster_1.Orgcode, T_CLMaster_1.SLOC as SubInv, T_CLMaster_1.StorageBin as Locator, T_CLMaster_1.Manufacturer, T_CLMaster_1.ManufacturerPN, T_CLMaster_1.VendorID, T_CLMaster_1.VendorName, T_CLMaster_1.PurOrdNo, T_CLMaster_1.RTLot, T_CLMaster_1.RecDate,
               T_CLMaster_1.MaterialDesc, T_CLMaster_1.DateCode, T_CLMaster_1.LotNo,
               T_PO_CLID_1.PO as DJNumber, T_PO_CLID_1.CLIDQty, T_PO_CLID_1.IssueDate, T_PO_CLID_1.ChangedBy, T_CLMaster_1.AddlData, T_CLMaster_1.PurOrdItem, T_PO_CLID_1.Product, T_CLMaster_1.ExpDate, '' AS Remarks, T_CLMaster_1.MSL, T_PO_CLID_1.ReturnDate
			   FROM V_CLMaster AS T_CLMaster_1 with (nolock) INNER JOIN V_PO_CLID AS T_PO_CLID_1 with (nolock)
			   ON T_CLMaster_1.CLID = T_PO_CLID_1.CLID
		       WHERE  T_PO_CLID_1.PDTO IS NULL ";

        //private const string sql_ComponentsUsed_ordercol = @" ) A ORDER BY A.MaterialNo";
        private const string sql_end = @" ) A ";

        private const string sql_AllPos_select = @"exec sp_GetPOListRpt ";

        #endregion


        #region methods

        #region methods GetComponentsUsedDatas


        private string GetAllPos(string PO, int k)
        {
            string result = "";

            if (k == 200)
                return result;

            string POTemp;
            string sql = sql_AllPos_select + " '" + PO + "' ";

            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result = result + "///" + DBConvert.DB2String(dr["purordno"]).ToString();
                POTemp = GetAllPos(DBConvert.DB2String(dr["purordno"]).ToString(), k + 1);
                if (POTemp != "")
                    result = result + "///" + POTemp;
            });

            return result;
        }


        private string GetReportSqlCondition(ReportComponentsUsedModelQuery query)
        {
            int k = 0;
            string POList = string.Empty;

            if (query.ReportType.Equals("Archive"))
            {
                POList = "'" + query.DJNo + GetAllPos(query.DJNo, k) + "'";
            }

            string sql = string.Empty;
            string sql1 = string.Empty;
            string sql2 = string.Empty;

            if (query != null)
            {

                if ((!string.IsNullOrEmpty(query.OrgCode)) && (!query.ReportType.Equals("Archive")))
                {
                    sql1 = sql1 + " and T_CLMaster.OrgCode='" + query.OrgCode + "' ";
                    sql2 = sql2 + " and T_CLMaster_1.OrgCode='" + query.OrgCode + "' ";
                }

                if (!string.IsNullOrEmpty(query.DJNo))
                {
                    if (query.ReportType.Equals("Archive"))
                    {
                        sql1 = sql1 + "and T_PDTO_PO.PO in (" + POList.Replace("///", "','") + ") ";
                        sql2 = sql2 + "and T_PO_CLID_1.PO in (" + POList.Replace("///", "','") + ") ";
                    }
                    else
                    {
                       if (query.DJNo.Contains(","))
                       {
                          sql1 = sql1 + " and T_PDTO_PO.PO in ('" + query.DJNo.Replace(",", "','") + "') ";
                          sql2 = sql2 + " and T_PO_CLID_1.PO in ('" + query.DJNo.Replace(",", "','") + "') ";
                       }
                       else
                       {
                          sql1 = sql1 + "and T_PDTO_PO.PO = '" + query.DJNo + "' ";
                          sql2 = sql2 + "and T_PO_CLID_1.PO ='" + query.DJNo + "' ";
                       }
                   }
                }

                if (!string.IsNullOrEmpty(query.MaterialNo))
                {
                    if (query.MaterialNo.Contains("%"))
                    {
                        sql1 = sql1 + " and T_CLMaster.MaterialNo like '" + query.MaterialNo + "' ";
                        sql2 = sql2 + " and T_CLMaster_1.MaterialNo like '" + query.MaterialNo + "' ";
                    }
                    else if (query.MaterialNo.Contains(","))
                    {
                        sql1 = sql1 + " and T_CLMaster.MaterialNo in ('" + query.MaterialNo.Replace(",", "','") + "') ";
                        sql2 = sql2 + " and T_CLMaster_1.MaterialNo in ('" + query.MaterialNo.Replace(",", "','") + "') ";
                    }
                    else
                    {
                        sql1 = sql1 + " and T_CLMaster.MaterialNo = '" + query.MaterialNo + "' ";
                        sql2 = sql2 + " and T_CLMaster_1.MaterialNo = '" + query.MaterialNo + "' ";
                    }
                }

                if (!string.IsNullOrEmpty(query.CLID))
                {
                    if (query.CLID.Contains(","))
                    {
                        sql1 = sql1 + "and T_CLMaster.CLID in ('" + query.CLID.Replace(",", "','") + "') ";
                        sql2 = sql2 + "and T_CLMaster_1.CLID in ('" + query.CLID.Replace(",", "','") + "') ";
                    }
                    else
                    {
                        sql1 = sql1 + "and T_CLMaster.CLID = '" + query.CLID + "' ";
                        sql2 = sql2 + "and T_CLMaster_1.CLID ='" + query.CLID + "' ";
                    }
                }

                if (!string.IsNullOrEmpty(query.DateCode))
                {
                    sql1 = sql1 + " and T_CLMaster.DateCode = '" + query.DateCode + "' ";
                    sql2 = sql2 + " and T_CLMaster_1.DateCode = '" + query.DateCode + "' ";
                }

                if (!string.IsNullOrEmpty(query.LotNo))
                {
                    sql1 = sql1 + "and T_CLMaster.LotNo = '" + query.LotNo + "' ";
                    sql2 = sql2 + "and T_CLMaster_1.LotNo = '" + query.LotNo + "' ";
                }

                
                //if (!string.IsNullOrEmpty(query.IssueDateFrom) & !string.IsNullOrEmpty(query.IssueDateTo))
                //{
                //    sql1 = sql1 + "and  (T_PO_CLID.IssueDate between '" + query.IssueDateFrom + " 00:00:00 AM' and '" + query.IssueDateTo + " 11:59:59 PM' )";
                //    sql2 = sql2 + "and (T_PO_CLID_1.IssueDate between '" + query.IssueDateFrom + " 00:00:00 AM' and '" + query.IssueDateTo + " 11:59:59 PM')";
                //}

                if (!query.IssueDateFrom.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql1 += string.Format(" and CONVERT(varchar(100), T_PO_CLID.IssueDate, 23) >='{0}'", query.IssueDateFrom.ToString("yyyy-MM-dd"));
                    sql2 += string.Format(" and CONVERT(varchar(100), T_PO_CLID_1.IssueDate, 23) >='{0}'", query.IssueDateFrom.ToString("yyyy-MM-dd"));
                }
                if (!query.IssueDateTo.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql1 += string.Format(" and CONVERT(varchar(100), T_PO_CLID.IssueDate, 23) <='{0}'", query.IssueDateTo.ToString("yyyy-MM-dd"));
                    sql2 += string.Format(" and CONVERT(varchar(100), T_PO_CLID_1.IssueDate, 23) <='{0}'", query.IssueDateTo.ToString("yyyy-MM-dd"));
                }

                if (!string.IsNullOrEmpty(query.PODJNo))
                {
                    if (query.PODJNo.Contains(","))
                    {
                        sql1 = sql1 + "and T_CLMaster.PurOrdNo in ('" + query.PODJNo.Replace(",", "','") + "') ";
                        sql2 = sql2 + "and T_CLMaster_1.PurOrdNo in ('" + query.PODJNo.Replace(",", "','") + "') ";
                    }
                    else
                    {
                        sql1 = sql1 + "and T_CLMaster.PurOrdNo = '" + query.PODJNo + "' ";
                        sql2 = sql2 + "and T_CLMaster_1.PurOrdNo = '" + query.PODJNo + "' ";
                    }
                }

                if (!string.IsNullOrEmpty(query.Manufacturer))
                {
                    sql1 = sql1 + "and T_CLMaster.Manufacturer = '" + query.Manufacturer + "' ";
                    sql2 = sql2 + "and T_CLMaster_1.Manufacturer ='" + query.Manufacturer + "' ";
                }


                if (sql1 != "" & sql2 != "")
                {
                    if (query.ReportType.Equals("Archive"))
                    {
                        sql = sql1 + sql_Archive_select2 + sql2 + sql_end;
                    }
                    else
                    {
                         sql = sql1 + sql_select2 + sql2 + sql_end;
                   }
                }

            }
            return sql;
        }

        private ReportComponentsUsedModel.Item GetReportComponentsUsedModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportComponentsUsedModel.Item data = new ReportComponentsUsedModel.Item

            {
                OrgCode = DBConvert.DB2String(dr["OrgCode"]),
                MaterialNo = DBConvert.DB2String(dr["MaterialNo"]),
                MaterialRevision = DBConvert.DB2String(dr["MaterialRevision"]),
                CLID = DBConvert.DB2String(dr["CLID"]),
                CLIDQty = DBConvert.DB2Decimal(dr["CLIDQty"]),
                SubInv = DBConvert.DB2String(dr["SubInv"]),
                Locator = DBConvert.DB2String(dr["Locator"]),
                DJNumber = DBConvert.DB2String(dr["DJNumber"]),
                Product = DBConvert.DB2String(dr["Product"]),
                Manufacturer = DBConvert.DB2String(dr["Manufacturer"]),
                ManufacturerPN = DBConvert.DB2String(dr["ManufacturerPN"]),
                VendorID = DBConvert.DB2String(dr["VendorID"]),
                VendorName = DBConvert.DB2String(dr["VendorName"]),
                PurOrdNo = DBConvert.DB2String(dr["PurOrdNo"]),
                PurOrdItem = DBConvert.DB2String(dr["PurOrdItem"]),
                RTLot = DBConvert.DB2String(dr["RTLot"]),
                MaterialDesc = DBConvert.DB2String(dr["MaterialDesc"]),
                Safety = DBConvert.DB2String(dr["Safety"]),
                DateCode = DBConvert.DB2String(dr["DateCode"]),
                LotNo = DBConvert.DB2String(dr["LotNo"]),
                RecDate = DBConvert.DB2DatetimeNull(dr["RecDate"]),
                IssueDate = DBConvert.DB2Datetime(dr["IssueDate"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                ExpDate = DBConvert.DB2DatetimeString(dr["ExpDate"], "yyyy-MM-dd"),
                Remarks = DBConvert.DB2String(dr["Remarks"]),
                MSL = DBConvert.DB2String(dr["MSL"]),
                ReturnDate = DBConvert.DB2DatetimeNull(dr["ReturnDate"]),
            };

            return data;
        }


        #endregion


        /// <summary>
        /// 按照条件查找数据，不分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<ReportComponentsUsedModel.Item> GetComponentsUsedData(ReportComponentsUsedModelQuery query)
        {
            //string sql = sql_base_select + GetReportSqlCondition(query);            
            string sql = sql_base_select;                                      //Default Type: Current
            if (query.ReportType.Equals("Archive"))
            {
                sql = sql_Archive_select;
            }

            #region Conditions
            sql += GetReportSqlCondition(query);
            #endregion

            string orderBy = " A.MaterialNo ";
            string sqlData = GetSQLData(sql, orderBy);
            return dbHelper.GetList<ReportComponentsUsedModel.Item>(sqlData, GetReportComponentsUsedModel);
        }


        /// <summary>
        /// 按照条件查找数据，分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<ReportComponentsUsedModel.Item> GetComponentsUsedByPage(ReportComponentsUsedModelQuery query)
        {
            //string sql = sql_base_select + GetReportSqlCondition(query);
            string sql = sql_base_select;                                      //Default Type: Current
            if (query.ReportType.Equals("Archive"))
            {
                sql = sql_Archive_select;
            }

            #region Conditions
            sql += GetReportSqlCondition(query);
            #endregion

            string orderBy = " A.MaterialNo ";
            query.Pager.Order = orderBy;

            string sqlDataByPage = GetSQLDataByPage(sql, query.Pager);
            return dbHelper.GetList<ReportComponentsUsedModel.Item>(sqlDataByPage, GetReportComponentsUsedModel);
        }


        /// <summary>
        /// 获取查询结果行数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long ComponentsUsedDataGetRowCount(ReportComponentsUsedModelQuery query)
        {
            //string sql = sql_base_select + GetReportSqlCondition(query);            
            //#region Conditions
            //sql += GetReportSqlCondition(query);
            //#endregion   
                     
            string sql = sql_base_select;                    //Default Type: Current
            if (query.ReportType.Equals("TLADetail"))
            {
                sql = GetReportTLASqlString(query);          //Report Type: TLADetail
            }
            else
            {
               if (query.ReportType.Equals("Archive"))
               {
                   sql = sql_Archive_select;
               }
               sql += GetReportSqlCondition(query);
            }

            long rowCount = dbHelper.GetCount(GetSQLCount(sql), 1200);
            return rowCount;
        }


        /// <summary>
        /// 按照条件查找TLA数据，不分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<ReportComponentsUsedTLAModel.Item> GetComponentsUsedTLAData(ReportComponentsUsedModelQuery query)
        {
            string sql = GetReportTLASqlString(query);                                      //Default Type: TLADetail

            string orderBy = " tmp.Assembly, tmp.TLA_DJ, tmp.SMT_DJ ";
            string sqlData = GetSQLData(sql, orderBy);
            return dbHelper.GetList<ReportComponentsUsedTLAModel.Item>(sqlData, GetReportComponentsUsedTLAModel);
        }


        private string GetReportTLASqlString(ReportComponentsUsedModelQuery query)
        {

            string sql = string.Empty;
            string sql1 = string.Empty;
            string sql2 = string.Empty;

            if (query != null)
            {
                //if (!string.IsNullOrEmpty(query.OrgCode))
                //{
                //    sql1 = sql1 + " and vc.OrgCode='" + query.OrgCode + "' ";
                //}

                if (!string.IsNullOrEmpty(query.MaterialNo))
                {
                   sql2 = " vc.MaterialNo in ('" + query.MaterialNo.Replace(",", "','") + "') ";
                }

                if (!string.IsNullOrEmpty(query.SubAssembly))
                {
                    sql1 = " c.MaterialNo in ('" + query.SubAssembly.Replace(",", "','") + "') ";
                }

                if (sql1 == string.Empty)
                    sql1 = " 1 = 1 ";



                sql = @" select distinct tmp.* from ( "
                + " ( "
                //+ " select t1.po as TLA_DJ, t1.materialno as [Assembly], vp.PO as SMT_DJ, t1.clid as suba_clid,t1.suba_clidqty,t1.suba_issueDate,vc.clid,vc.materialno,vc.datecode, vc.lotno, vc.manufacturer,vc.manufacturerpn, vp.clidqty, vp.issueDate, vc.RecDate, vc.purOrdNo "
                + " select t1.po as TLA_DJ, t1.materialno as [Assembly], vp.PO as SMT_DJ, t1.clid as Assembly_CLID,t1.suba_clidqty as Assembly_CLIDQty,t1.suba_issueDate as Assembly_IssueDate, vc.CLID,vc.MaterialNo, vc.DateCode, vc.LotNo, vc.Manufacturer, vc.ManufacturerPN, vp.CLIDQty, vp.IssueDate, vc.RecDate, vc.PurOrdNo "
                + " from "
                + " v_po_clid as vp with(nolock) "
                + " inner join v_clmaster as vc with(nolock) on vc.clid=vp.clid "
                + " inner join  "
                + " (		 "
                + " select distinct c.clid, c.purOrdNo, p.po, c.materialno, p.clidqty as suba_clidqty, p.issueDate as suba_issueDate  "
                + " from v_po_clid as p with(nolock)  "
                + " inner join v_clmaster as c with(nolock) on p.clid=c.clid "
                + " where " + sql1 + " "
                + " union  "
                + " select distinct c.clid, c.purOrdNo, o.po, c.materialno, p.clidqty as suba_clidqty, p.issueDate as suba_issueDate  "
                + " from v_po_clid as p with(nolock) 	 "
                + " inner join v_pdto_po as o with(nolock) on p.pdto = o.pdto		 "
                + " inner join v_clmaster as c with(nolock) on p.clid=c.clid	 "
                + " where " + sql1 + " "
                + " and p.pdto is not null	 "
                + " ) as t1					 "
                + " on t1.purordno = vp.po		 "
                + " where " + sql2 + " "
                + " and t1.purordno <> ''	 "
                + " ) "
                + " union all "
                + " ( "
                + " select t1.po as TLA_DJ, t1.materialno as [Assembly], pd.PO as SMT_DJ, t1.clid as suba_clid,t1.suba_clidqty,t1.suba_issueDate,vc.clid,vc.materialno,vc.datecode, vc.lotno, vc.manufacturer,vc.manufacturerpn, vp.clidqty, vp.issueDate, vc.RecDate, vc.purOrdNo "
                + " from v_pdto_po as pd with(nolock) "
                + " inner join v_po_clid as vp with(nolock) on pd.pdto=vp.pdto	 "
                + " inner join v_clmaster as vc with(nolock) on vc.clid=vp.clid	 "
                + " inner join  "
                + " (		 "
                + " select distinct c.clid, c.purOrdNo, p.po, c.materialno, p.clidqty as suba_clidqty, p.issueDate as suba_issueDate "
                + " from v_po_clid as p with(nolock) 	 "
                + " inner join v_clmaster as c with(nolock) on p.clid=c.clid	 "
                + " where " + sql1 + " "
                + " union "
                + " select distinct c.clid, c.purOrdNo, o.po, c.materialno, p.clidqty as suba_clidqty, p.issueDate as suba_issueDate "
                + " from v_po_clid as p with(nolock) 	 "
                + " inner join v_pdto_po as o with(nolock) on p.pdto = o.pdto	 "
                + " inner join v_clmaster as c with(nolock) on p.clid=c.clid "
                + " where " + sql1 + " "
                + " and p.pdto is not null		 "
                + " ) as t1	 "
                + " on t1.purordno = pd.po		 "
                + " where " + sql2 + " "
                + " and t1.purordno <> ''		 "
                + " and pd.pdto is not null	 "
                + " ) "
                + " ) as tmp "
                + " where(tmp.clidqty > 0) ";
            //+ " order by tmp.Assembly,tmp.TLA_DJ, tmp.SMT_DJ ";

            }

            return sql;
        }

        private ReportComponentsUsedTLAModel.Item GetReportComponentsUsedTLAModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportComponentsUsedTLAModel.Item data = new ReportComponentsUsedTLAModel.Item

            {
                TLA_DJ = DBConvert.DB2String(dr["TLA_DJ"]),
                Assembly = DBConvert.DB2String(dr["Assembly"]),
                SMT_DJ = DBConvert.DB2String(dr["SMT_DJ"]),
                Assembly_CLID = DBConvert.DB2String(dr["Assembly_CLID"]),
                Assembly_CLIDQty = DBConvert.DB2Decimal(dr["Assembly_CLIDQty"]),
                Assembly_IssueDate = DBConvert.DB2Datetime(dr["Assembly_IssueDate"]),
                CLID = DBConvert.DB2String(dr["CLID"]),
                MaterialNo = DBConvert.DB2String(dr["MaterialNo"]),
                DateCode = DBConvert.DB2String(dr["DateCode"]),
                LotNo = DBConvert.DB2String(dr["LotNo"]),
                Manufacturer = DBConvert.DB2String(dr["Manufacturer"]),
                ManufacturerPN = DBConvert.DB2String(dr["ManufacturerPN"]),
                CLIDQty = DBConvert.DB2Decimal(dr["CLIDQty"]),
                IssueDate = DBConvert.DB2Datetime(dr["IssueDate"]),
                RecDate = DBConvert.DB2DatetimeNull(dr["RecDate"]),
                PurOrdNo = DBConvert.DB2String(dr["PurOrdNo"]),
            };

            return data;
        }

        #endregion
    }
}



