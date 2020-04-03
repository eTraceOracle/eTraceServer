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
    public class EquipmentFixturePMHeaderDAL : DALBase, IEquipmentFixturePMHeaderDAL
    {                                                    
        #region corts

        public EquipmentFixturePMHeaderDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public EquipmentFixturePMHeaderDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields


        private const string sql_EquipmentFixturePMHeader_select = @"SELECT distinct A.PMID, A.EquipmentID,A.Seq, B.Description, B.Model, D.ProdFloor as ProductionFloor, B.CurrProdLine as ProductionLine, B.Owner, A.Frequency, A.PMSpecID, A.PMStatus, CONVERT(varchar(10),A.PMScheduledDate,120) as PMScheduledDate, dateadd(hour,8,A.PMStartDate) as PMStartDate, dateadd(hour,8,A.PMCompletionDate) as PMCompletionDate, (case when (A.PMStartDate is NULL OR A.PMCompletionDate IS NULL) then NULL ELSE CONVERT(VARCHAR(20),CEILING(CONVERT(INT, DATEDIFF([Minute], PMStartDate, PMCompletionDate)))) END) AS PMUsedTime, A.PMTechnician, A.PMResult, A.Attachment,A.ChangedOn,A.ChangedBy,A.CreatedOn,A.CreatedBy,A.CurrProdLine,A.CurrLocation,A.CurrUseCount,A.Remarks
                                                                    FROM T_SMEquipPMHeader as A WITH (nolock)  
                                                                    inner join V_SMEquipmentFixture as B with(nolock)  on A.EquipmentID = B.EquipmentID 
                                                                    inner join T_SFProdLine as D with(nolock) on B.CurrProdLine = D.ProdLine 
                                                                    WHERE 1=1  ";

        private const string sql_EquipmentFixturePMItem_select = @"SELECT distinct A.PMID, PMItem, ItemDesc, PMItemResult, Operator, InstFileName, AttFileName, PMItemData, Remarks
                                                                    FROM T_SMEquipPMItem A WITH (nolock) WHERE 1=1";

        private const string sql_EquipmentFixturePMMat_select = @"SELECT distinct A.PMID,Material ,Qty ,UOM ,UnitCost , dateadd(hour,8,ChangedOn) as ChangedOn ,ChangedBy ,Remarks FROM T_SMEquipPMMat A with(nolock)
                                                                    WHERE 1=1";

        private const string sql_EquipmentFixturePMDetail_select = @"SELECT distinct A.PMID, A.EquipmentID as EquipmentID,A.PMStatus as PMStatus, CONVERT(varchar(10),A.PMScheduledDate,120) as PMScheduledDate, dateadd(hour,8,A.PMStartDate) as PMStartDate, dateadd(hour,8,A.PMCompletionDate) as PMCompletionDate, (case when (A.PMStartDate is NULL OR A.PMCompletionDate IS NULL) then NULL ELSE CONVERT(VARCHAR(20),CEILING(CONVERT(INT, DATEDIFF([Minute], PMStartDate, PMCompletionDate)))) END) AS PMUsedTime, PMItem, ItemDesc, PMResult,PMItemData, C.Operator, C.InstFileName AS PMInstruction, C.AttFileName as Attachment, C.Remarks
                                                                    FROM T_SMEquipPMHeader as A WITH(nolock)
                                                                    inner join T_SMEquipPMItem as C with(nolock) on A.PMID = C.PMID
                                                                    inner join V_SMEquipmentFixture as B with(nolock)  on A.EquipmentID = B.EquipmentID
                                                                    inner join T_SFProdLine as D on B.CurrProdLine = D.ProdLine
                                                                    WHERE 1=1";

        private const string sql_EquipmentFixturePMHeader_Frequency_select = "select PMFrequency from T_SMEquipPMFrequency with(nolock) order by PMFrequency asc";

        #endregion

        #region methods


        #region methods EquipmentFixturePMHeader- 设备、工装保养数据

        public ReportEquipmentFixturePMHeaderModel GetSMEquipmentFixturePMHeaderDatas(ReportEquipmentFixturePMHeaderQuery query)
        {
            ReportEquipmentFixturePMHeaderModel result = new ReportEquipmentFixturePMHeaderModel();
            result.Data = new List<ReportEquipmentFixturePMHeaderModel.Item>();
            string sql = sql_EquipmentFixturePMHeader_select;
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
                result.Data.Add(GetReportEquipmentFixturePMHeaderModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }


        public ReportEquipmentFixturePMDetailModel GetSMEquipmentFixturePMDetailDatas(ReportEquipmentFixturePMHeaderQuery query)
        {
            ReportEquipmentFixturePMDetailModel result = new ReportEquipmentFixturePMDetailModel();
            result.Data = new List<ReportEquipmentFixturePMDetailModel.DetailItem>();
            string sql = sql_EquipmentFixturePMDetail_select;
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
                result.Data.Add(GetReportEquipmentFixturePMDetailModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        public ReportEquipmentFixturePMItemModel GetSMEquipmentFixturePMItemDatas(ReportEquipmentFixturePMHeaderQuery query)
        {
            ReportEquipmentFixturePMItemModel result = new ReportEquipmentFixturePMItemModel();
            result.Data = new List<ReportEquipmentFixturePMItemModel.PMItems>();
            string sql = sql_EquipmentFixturePMItem_select;
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
                result.Data.Add(GetReportEquipmentFixturePMItemModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        public ReportEquipmentFixturePMMatModel GetSMEquipmentFixturePMMatDatas(ReportEquipmentFixturePMHeaderQuery query)
        {
            ReportEquipmentFixturePMMatModel result = new ReportEquipmentFixturePMMatModel();
            result.Data = new List<ReportEquipmentFixturePMMatModel.PMMat>();
            string sql = sql_EquipmentFixturePMMat_select;
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
                result.Data.Add(GetReportEquipmentFixturePMMatModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }



        public long EquipmentFixturePMHeaderDataGetRowCount(ReportEquipmentFixturePMHeaderQuery query)
        {
            string sql = sql_EquipmentFixturePMHeader_select;
            #region Conditions
            sql += GetReportEquipmentSqlCondition(query);
            #endregion
            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        public long EquipmentFixturePMDetailDataGetRowCount(ReportEquipmentFixturePMHeaderQuery query)
        {
            string sql = sql_EquipmentFixturePMDetail_select;
            #region Conditions
            sql += GetReportEquipmentSqlCondition(query);
            #endregion
            string countSql = GetSQLCount(sql);
            long rowCount = dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        private string GetReportEquipmentSqlCondition(ReportEquipmentFixturePMHeaderQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.EquipmentID))
                {
                    sql += string.Format(" and A.EquipmentID='{0}'", query.EquipmentID);
                }
                if (!string.IsNullOrEmpty(query.Category))
                {
                    sql += string.Format(" and B.Category='{0}'", query.Category);
                }
                if (!string.IsNullOrEmpty(query.SubCategory))
                {
                    sql += string.Format(" and B.SubCategory='{0}'", query.SubCategory);
                }
                if (!string.IsNullOrEmpty(query.Frequency))
                {
                    sql += string.Format(" and A.Frequency='{0}'", query.Frequency);
                }
                //if (!string.IsNullOrEmpty(query.ScheduledFrom.ToString()))
                //{
                //    sql += string.Format(" and a.PMScheduledDate='{0}'", query.ScheduledFrom);
                //}
                //if (!string.IsNullOrEmpty(query.ScheduledTo.ToString()))
                //{
                //    sql += string.Format(" and a.PMScheduledDate='{0}'", query.ScheduledTo);
                //}

                if (!query.ScheduledFrom.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), A.PMScheduledDate, 23) >='{0}'", query.ScheduledFrom.ToString("yyyy-MM-dd"));
                }
                if (!query.ScheduledTo.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), A.PMScheduledDate, 23) <='{0}'", query.ScheduledTo.ToString("yyyy-MM-dd"));
                }
                if (!query.PMCompletionFrom.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), A.PMCompletionDate, 23) >='{0}'", query.PMCompletionFrom.ToString("yyyy-MM-dd"));
                }
                if (!query.PMCompletionTo.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), A.PMCompletionDate, 23) <='{0}'", query.PMCompletionTo.ToString("yyyy-MM-dd"));
                }

                if (!string.IsNullOrEmpty(query.Department))
                {
                    sql += string.Format(" and B.Department='{0}'", query.Department);
                }
                if (!string.IsNullOrEmpty(query.PMStatus))
                {
                    sql += string.Format(" and A.PMStatus='{0}'", query.PMStatus);
                }
                if (!string.IsNullOrEmpty(query.ProductionLine))
                {
                    sql += string.Format(" and B.CurrProdLine='{0}'", query.ProductionLine);
                }
                if (!string.IsNullOrEmpty(query.ProductionFloor))
                {
                    sql += string.Format(" and D.ProdFloor='{0}'", query.ProductionFloor);
                }
                if (!string.IsNullOrEmpty(query.PMID))
                {
                    sql += string.Format(" and A.PMID='{0}'", query.PMID);
                }
            }
            return sql;
        }

        private ReportEquipmentFixturePMHeaderModel.Item GetReportEquipmentFixturePMHeaderModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportEquipmentFixturePMHeaderModel.Item data = new ReportEquipmentFixturePMHeaderModel.Item
            {
                EquipmentID = DBConvert.DB2String(dr["EquipmentID"]),
                Frequency = DBConvert.DB2String(dr["Frequency"]),
                Seq = DBConvert.DB2String(dr["Seq"]),
                Description = DBConvert.DB2String(dr["Description"]),
                Model = DBConvert.DB2String(dr["Model"]),               
                ProductionFloor = DBConvert.DB2String(dr["ProductionFloor"]),
                ProductionLine = DBConvert.DB2String(dr["ProductionLine"]),            
                Owner = DBConvert.DB2String(dr["Owner"]),
                PMSpecID = DBConvert.DB2String(dr["PMSpecID"]),
                PMStatus = DBConvert.DB2String(dr["PMStatus"]),
                PMSCheduledDate = DBConvert.DB2Datetime(dr["PMSCheduledDate"]),
                PMStartDate = DBConvert.DB2Datetime(dr["PMStartDate"]),
                PMCompletionDate = DBConvert.DB2Datetime(dr["PMCompletionDate"]),
                PMUsedTime = DBConvert.DB2String(dr["PMUsedTime"]),
                PMTechnician = DBConvert.DB2String(dr["PMTechnician"]),
                PMResult = DBConvert.DB2String(dr["PMResult"]),
                Attachment = DBConvert.DB2String(dr["Attachment"]),
                ChangedOn = DBConvert.DB2Datetime(dr["ChangedOn"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                CreatedOn = DBConvert.DB2Datetime(dr["CreatedOn"]),
                CreatedBy = DBConvert.DB2String(dr["CreatedBy"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),
                CurrProdLine = DBConvert.DB2String(dr["CurrProdLine"]),
                CurrLocation = DBConvert.DB2String(dr["CurrLocation"]),
                CurrUseCount = DBConvert.DB2String(dr["CurrUseCount"]),
                 PMID = DBConvert.DB2String(dr["PMID"]),
            };
            return data;
        }

        private ReportEquipmentFixturePMDetailModel.DetailItem GetReportEquipmentFixturePMDetailModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportEquipmentFixturePMDetailModel.DetailItem data = new ReportEquipmentFixturePMDetailModel.DetailItem
            {
                EquipmentID = DBConvert.DB2String(dr["EquipmentID"]),
                PMStatus = DBConvert.DB2String(dr["PMStatus"]),
                PMSCheduledDate = DBConvert.DB2Datetime(dr["PMSCheduledDate"]),
                PMStartDate = DBConvert.DB2Datetime(dr["PMStartDate"]),
                PMCompletionDate = DBConvert.DB2Datetime(dr["PMCompletionDate"]),
                PMItem = DBConvert.DB2String(dr["PMItem"]),
                PMResult = DBConvert.DB2String(dr["PMResult"]),
                ItemDesc = DBConvert.DB2String(dr["ItemDesc"]),
                PMItemData = DBConvert.DB2String(dr["PMItemData"]),
                Operator = DBConvert.DB2String(dr["Operator"]),
                PMInstruction = DBConvert.DB2String(dr["PMInstruction"]),
                Attachment = DBConvert.DB2String(dr["Attachment"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),
            };
            return data;
        }

        private ReportEquipmentFixturePMItemModel.PMItems GetReportEquipmentFixturePMItemModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportEquipmentFixturePMItemModel.PMItems data = new ReportEquipmentFixturePMItemModel.PMItems
            {
                PMItem = DBConvert.DB2String(dr["PMItem"]),
                ItemDesc = DBConvert.DB2String(dr["ItemDesc"]),
                PMItemResult = DBConvert.DB2String(dr["PMItemResult"]),
                Operator = DBConvert.DB2String(dr["Operator"]),
                InstFileName = DBConvert.DB2String(dr["InstFileName"]),
                AttFileName = DBConvert.DB2String(dr["AttFileName"]),
                PMItemData = DBConvert.DB2String(dr["PMItemData"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),
                PMID = DBConvert.DB2String(dr["PMID"]),
            };
            return data;
        }

        private ReportEquipmentFixturePMMatModel.PMMat GetReportEquipmentFixturePMMatModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportEquipmentFixturePMMatModel.PMMat data = new ReportEquipmentFixturePMMatModel.PMMat
            {
                Material = DBConvert.DB2String(dr["Material"]),
                Qty = DBConvert.DB2String(dr["Qty"]),
                UOM = DBConvert.DB2String(dr["UOM"]),
                UnitCost = DBConvert.DB2String(dr["UnitCost"]),
                ChangedOn = DBConvert.DB2Datetime(dr["ChangedOn"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),
                PMID = DBConvert.DB2String(dr["PMID"]),         
            };
            return data;
        }


        /// <summary>
        /// 获取保养频率
        /// </summary>
        /// <returns></returns>
        public List<string> GetPMFrequency()
        {
            string sql = sql_EquipmentFixturePMHeader_Frequency_select;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Add(DBConvert.DB2String(dr["PMFrequency"]));
            });

            return result;
        }


        #endregion

        #endregion
    }
}
