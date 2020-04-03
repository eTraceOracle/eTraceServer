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
    public class SMFeederDAL : DALBase, ISMFeederDAL
    {                                                    
        #region corts

        public SMFeederDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public SMFeederDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields


        private const string sql_SMFeederHeader_select = @" SELECT   T_SMEquipment.EquipmentID, T_SMEquipment.Description, T_SMEquipment.Model, T_SMEquipment.Manufacturer, T_SMEquipment.ManufacturerSN, 
                                                            CONVERT(varchar(10), MfrDate, 120) AS MfrDate, T_SMEquipment.CurrProdLine, T_SMEquipment.Status, u.FailedTimes, T_SMEquipment.Owner , CONVERT(varchar(10),v.PMScheduledDate,120) as PMScheduledDate 
                                                            From            T_SMEquipment WITH (nolock) INNER JOIN 
                                                            (SELECT        T_SMEquipment_1.EquipmentID, COUNT(T_SMEquipRepairHeader.EquipmentID) AS FailedTimes 
                                                            FROM            T_SMEquipment AS T_SMEquipment_1 WITH (nolock) LEFT OUTER JOIN T_SMEquipRepairHeader WITH (nolock) ON T_SMEquipment_1.EquipmentID = T_SMEquipRepairHeader.EquipmentID 
                                                            GROUP BY T_SMEquipment_1.EquipmentID) AS u ON T_SMEquipment.EquipmentID = u.EquipmentID 
                                                            left outer join (select T_SMEquipPMHeader.EquipmentID, min(T_SMEquipPMHeader.PMScheduledDate) as PMScheduledDate from T_SMEquipPMHeader with (nolock) inner join T_SMEquipment with (nolock) on T_SMEquipPMHeader.EquipmentID = T_SMEquipment.EquipmentID where PMStatus = 'Scheduled' group by T_SMEquipPMHeader.EquipmentID ) as v on T_SMEquipment.EquipmentID = v.EquipmentID 
                                                            where 1=1 And T_SMEquipment.Category = 'Feeder' ";

        private const string sql_SMFeederPMHeader_select = @" SELECT A.PMID, A.EquipmentID, A.Frequency, A.PMStatus, CONVERT(varchar(10),A.PMScheduledDate,120) as PMScheduledDate, dateadd(hour,8,A.PMCompletionDate) as PMCompletionDate, A.PMTechnician, A.PMResult, A.CreatedBy
                                                                FROM T_SMEquipPMHeader as A WITH (nolock)  inner join T_SMEquipment with(nolock)  on A.EquipmentID = T_SMEquipment.EquipmentID WHERE 1=1 ";

        private const string sql_SMFeederPMHeaderItem_select = @"SELECT PMID, PMItem, ItemDesc, PMItemResult, Operator, InstFileName, AttFileName, PMItemData, Remarks
                                                        FROM T_SMEquipPMItem WITH (nolock) WHERE 1=1";

        private const string sql_SMFeederPMHeaderMat_select = @"SELECT Material ,Qty ,UOM ,UnitCost , dateadd(hour,8,ChangedOn) as ChangedOn ,ChangedBy ,Remarks FROM T_SMEquipPMMat with(nolock)
                                                        WHERE 1=1";


        private const string sql_SMFeederRepairHeader_select = @"SELECT a.RepID, a.EquipmentID, dateadd(hour,8,a.FailedTime) as FailedTime, dateadd(hour,8,a.FixedTime) as FixedTime, a.Symptom, a.Cause, a.CauseType, a.[Action], a.RepairCenter, a.Repairman
                                                                    FROM T_SMEquipRepairHeader as a WITH (nolock) inner join T_SMEquipment with(nolock)  on A.EquipmentID = T_SMEquipment.EquipmentID WHERE 1=1";

        private const string sql_SMFeederRepairHeaderMat_select = @"SELECT RepID, Material, Qty, UOM, UnitCost, Remarks
                                                            FROM T_SMEquipRepairMat WITH (nolock) WHERE 1=1";



        #endregion

        #region methods


        #region methods GetSMFeederHeaderDatas - Equipment /Fixture PM Plan Data 
        /// <summary>
        /// Feeder Header
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportSMFeederHeaderModel GetSMFeederHeaderDatas(ReportSMFeederHeaderModelQuery query)
        {
            ReportSMFeederHeaderModel result = new ReportSMFeederHeaderModel();
            result.Data = new List<ReportSMFeederHeaderModel.Header>();
            string sql = sql_SMFeederHeader_select;
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
                result.Data.Add(GetReportSMFeederHeaderModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        /// <summary>
        /// PM Header
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportSMFeederPMHeaderModel GetSMFeederPMHeaderDatas(ReportSMFeederHeaderModelQuery query)
        {
            ReportSMFeederPMHeaderModel result = new ReportSMFeederPMHeaderModel();
            result.Data = new List<ReportSMFeederPMHeaderModel.PMHeader>();
            string sql = sql_SMFeederPMHeader_select;
            #region Conditions
            sql += GetReportEquipmentSqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            pSql.SQLDatas = sql;
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportSMFeederPMHeaderModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        /// <summary>
        /// PMItem
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportSMFeederPMHeaderItemModel GetSMFeederPMHeaderItemDatas(ReportSMFeederHeaderModelQuery query)
        {
            ReportSMFeederPMHeaderItemModel result = new ReportSMFeederPMHeaderItemModel();
            result.Data = new List<ReportSMFeederPMHeaderItemModel.PMHeaderItem>();
            string sql = sql_SMFeederPMHeaderItem_select;
            #region Conditions
            sql += GetReportEquipmentSqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            pSql.SQLDatas = sql;
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportSMFeederPMHeaderItemModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        /// <summary>
        /// PMItem
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportSMFeederPMHeaderMatModel GetSMFeederPMHeaderMatDatas(ReportSMFeederHeaderModelQuery query)
        {
            ReportSMFeederPMHeaderMatModel result = new ReportSMFeederPMHeaderMatModel();
            result.Data = new List<ReportSMFeederPMHeaderMatModel.PMHeaderMat>();
            string sql = sql_SMFeederPMHeaderMat_select;
            #region Conditions
            sql += GetReportEquipmentSqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            pSql.SQLDatas = sql;
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportSMFeederPMHeaderMatModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        /// <summary>
        /// Repair Header
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportSMFeederRepairHeaderModel GetSMFeederRepairHeaderDatas(ReportSMFeederHeaderModelQuery query)
        {
            ReportSMFeederRepairHeaderModel result = new ReportSMFeederRepairHeaderModel();
            result.Data = new List<ReportSMFeederRepairHeaderModel.RepairHeader>();
            string sql = sql_SMFeederRepairHeader_select;
            #region Conditions
            sql += GetReportEquipmentSqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            pSql.SQLDatas = sql;
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportSMFeederRepairHeaderModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        /// <summary>
        /// Repair Header Mat
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportSMFeederRepairHeaderMatModel GetSMFeederRepairHeaderMatDatas(ReportSMFeederHeaderModelQuery query)
        {
            ReportSMFeederRepairHeaderMatModel result = new ReportSMFeederRepairHeaderMatModel();
            result.Data = new List<ReportSMFeederRepairHeaderMatModel.RepairHeaderMat>();
            string sql = sql_SMFeederRepairHeaderMat_select;
            #region Conditions
            sql += GetReportEquipmentSqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            pSql.SQLDatas = sql;
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportSMFeederRepairHeaderMatModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        /// <summary>
        /// SMFeederDataGetRowCount
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long SMFeederDataGetRowCount(ReportSMFeederHeaderModelQuery query)
        {
            string sql = sql_SMFeederHeader_select;
            #region Conditions
            sql += GetReportEquipmentSqlCondition(query);
            #endregion
            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        private string GetReportEquipmentSqlCondition(ReportSMFeederHeaderModelQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.EquipmentID))
                {
                    sql += string.Format(" and T_SMEquipment.EquipmentID='{0}'", query.EquipmentID);
                }
                if (!string.IsNullOrEmpty(query.Description))
                {
                    sql += string.Format(" and Description like N'%{0}%'", query.Description);
                }
                if (!string.IsNullOrEmpty(query.Model))
                {
                    sql += string.Format(" and Model='{0}'", query.Model);
                }
                if (!string.IsNullOrEmpty(query.Status))
                {
                    sql += string.Format(" and T_SMEquipment.Status='{0}'", query.Status);
                }
                if (query.FailedTimes >0 )
                {
                    sql += string.Format(" and FailedTimes>='{0}'", query.FailedTimes);
                }
                if (!string.IsNullOrEmpty(query.PMItemPMID))
                {
                    sql += string.Format(" and PMID ='{0}'", query.PMItemPMID);
                }
                if (!string.IsNullOrEmpty(query.PMMatPMID))
                {
                    sql += string.Format(" and PMID ='{0}'", query.PMMatPMID);
                }
                if (!string.IsNullOrEmpty(query.RepairRepID))
                {
                    sql += string.Format(" and RepID ='{0}'", query.RepairRepID);
                }

            }
            return sql;
        }
        //Feeder Header
        private ReportSMFeederHeaderModel.Header GetReportSMFeederHeaderModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMFeederHeaderModel.Header data = new ReportSMFeederHeaderModel.Header
            {
                EquipmentID = DBConvert.DB2String(dr["EquipmentID"]),
                Description = DBConvert.DB2String(dr["Description"]),
                Model = DBConvert.DB2String(dr["Model"]),
                Manufacturer = DBConvert.DB2String(dr["Manufacturer"]),
                ManufacturerSN = DBConvert.DB2String(dr["ManufacturerSN"]),
                MfrDate = DBConvert.DB2Datetime(dr["MfrDate"]),
                CurrProdLine = DBConvert.DB2String(dr["CurrProdLine"]),
                Status = DBConvert.DB2String(dr["Status"]),
                FailedTimes = DBConvert.DB2String(dr["FailedTimes"]),
                Owner = DBConvert.DB2String(dr["Owner"]),
                PMScheduledDate = DBConvert.DB2Datetime(dr["PMScheduledDate"]),
            };
            return data;
        }
        //PM Header
        private ReportSMFeederPMHeaderModel.PMHeader GetReportSMFeederPMHeaderModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMFeederPMHeaderModel.PMHeader data = new ReportSMFeederPMHeaderModel.PMHeader
            {
                PMID = DBConvert.DB2String(dr["PMID"]),
                EquipmentID = DBConvert.DB2String(dr["EquipmentID"]),
                Frequency = DBConvert.DB2String(dr["Frequency"]),
                PMStatus = DBConvert.DB2String(dr["PMStatus"]),
                PMScheduledDate = DBConvert.DB2Datetime(dr["PMScheduledDate"]),
                PMCompletionDate = DBConvert.DB2Datetime(dr["PMCompletionDate"]),
                PMTechnician = DBConvert.DB2String(dr["PMTechnician"]),
                PMResult = DBConvert.DB2String(dr["PMResult"]),
                CreatedBy = DBConvert.DB2String(dr["CreatedBy"]),
            };
            return data;
        }
        // PM Header Item
        private ReportSMFeederPMHeaderItemModel.PMHeaderItem GetReportSMFeederPMHeaderItemModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMFeederPMHeaderItemModel.PMHeaderItem data = new ReportSMFeederPMHeaderItemModel.PMHeaderItem
            {
                PMID = DBConvert.DB2String(dr["PMID"]),
                PMItem = DBConvert.DB2String(dr["PMItem"]),
                ItemDesc = DBConvert.DB2String(dr["ItemDesc"]),
                PMItemResult = DBConvert.DB2String(dr["PMItemResult"]),
                Operator = DBConvert.DB2String(dr["Operator"]),
                InstFileName = DBConvert.DB2String(dr["InstFileName"]),
                AttFileName = DBConvert.DB2String(dr["AttFileName"]),
                PMItemData = DBConvert.DB2String(dr["PMItemData"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),

            };
            return data;
        }

        // PM Header Mat
        private ReportSMFeederPMHeaderMatModel.PMHeaderMat GetReportSMFeederPMHeaderMatModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMFeederPMHeaderMatModel.PMHeaderMat data = new ReportSMFeederPMHeaderMatModel.PMHeaderMat
            {
                Material = DBConvert.DB2String(dr["Material"]),
                Qty = DBConvert.DB2String(dr["Qty"]),
                UOM = DBConvert.DB2String(dr["UOM"]),
                UnitCost = DBConvert.DB2String(dr["UnitCost"]),
                ChangedOn = DBConvert.DB2Datetime(dr["ChangedOn"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),

            };
            return data;
        }

        // Repair Header
        private ReportSMFeederRepairHeaderModel.RepairHeader GetReportSMFeederRepairHeaderModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMFeederRepairHeaderModel.RepairHeader data = new ReportSMFeederRepairHeaderModel.RepairHeader
            {
                RepID = DBConvert.DB2String(dr["RepID"]),
                EquipmentID = DBConvert.DB2String(dr["EquipmentID"]),
                FailedTime = DBConvert.DB2Datetime(dr["FailedTime"]),
                FixedTime = DBConvert.DB2Datetime(dr["FixedTime"]),
                Symptom = DBConvert.DB2String(dr["Symptom"]),
                Cause = DBConvert.DB2String(dr["Cause"]),
                CauseType = DBConvert.DB2String(dr["CauseType"]),
                Action = DBConvert.DB2String(dr["Action"]),
                RepairCenter = DBConvert.DB2String(dr["RepairCenter"]),
                Repairman = DBConvert.DB2String(dr["Repairman"]),

            };
            return data;
        }

        // Repair Header Mat
        private ReportSMFeederRepairHeaderMatModel.RepairHeaderMat GetReportSMFeederRepairHeaderMatModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMFeederRepairHeaderMatModel.RepairHeaderMat data = new ReportSMFeederRepairHeaderMatModel.RepairHeaderMat
            {
                RepID = DBConvert.DB2String(dr["RepID"]),
                Material = DBConvert.DB2String(dr["Material"]),
                Qty = DBConvert.DB2String(dr["Qty"]),
                UOM = DBConvert.DB2String(dr["UOM"]),
                UnitCost = DBConvert.DB2String(dr["UnitCost"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),
            };
            return data;
        }

        #endregion

        #endregion
    }
}
