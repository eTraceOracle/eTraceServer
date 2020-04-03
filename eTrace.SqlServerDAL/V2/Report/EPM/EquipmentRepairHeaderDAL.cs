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
    public class EquipmentRepairHeaderDAL : DALBase, IEquipmentRepairHeaderDAL
    {                                                    
        #region corts

        public EquipmentRepairHeaderDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public EquipmentRepairHeaderDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields


        private const string sql_SMRepairHeader_select = @"SELECT a.RepID, a.EquipmentID,B.CurrProdLine as ProductionLine,B.Department, dateadd(hour,8,a.FailedTime) as FailedTime, 
                                                                    dateadd(hour,8,a.FixedTime) as FixedTime, a.RepairCenter, a.Repairman, a.Status,a.Category,a.Symptom, 
                                                                    a.Cause, a.CauseType, a.[Action], dateadd(hour,8,a.EndorsedOn) as EndorsedOn, a.EndorsedBy, a.Attachment, 
                                                                    a.FileName, dateadd(hour,8,a.ChangedOn) as ChangedOn, a.ChangedBy, a.Remarks,Convert(decimal(10,2),
                                                                    (cast(DATEDIFF( minute, a.FailedTime, a.EndorsedOn) as decimal(10,2)) / 60) ) AS Duration, 
                                                                    DowntimeHour, DowntimeCostUSD ,c.Model,c.ManufacturerSN
                                                                    FROM T_SMEquipRepairHeader as a WITH (nolock) inner join V_SMEquipmentFixture as B with(nolock)  on A.EquipmentID = B.EquipmentID 
                                                                    left join T_SMEquipment as c with(nolock) on a.EquipmentID =c.EquipmentID 
                                                                    WHERE 1=1";

        private const string sql_SMRepairHeader_Mat_select = "select Material,Qty,UOM,UnitCost,ChangedOn,ChangedBy,Remarks from T_SMEquipRepairMat with(nolock) where 1=1";


        private const string sql_SMRepairHeader_Status_select = "select distinct Code as Status from T_SMLOVItem with(nolock) where Name ='Equipment Repair - Status' and Status ='1'";

        #endregion

        #region methods


        #region methods GetSMRepairHeaderDatas - Equipment /Fixture Repair Data 
        /// <summary>
        /// 获取维修数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportEquipmentRepairHeaderModel GetSMRepairHeaderDatas(ReportEquipmentRepairHeaderModelQuery query)
        {
            ReportEquipmentRepairHeaderModel result = new ReportEquipmentRepairHeaderModel();
            result.Data = new List<ReportEquipmentRepairHeaderModel.Item>();
            string sql = sql_SMRepairHeader_select;
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
                result.Data.Add(GetReportEquipmentRepairHeaderModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        /// <summary>
        /// Get Repair Mat
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportEquipmentRepairMatModel GetSMRepairMatDatas(ReportEquipmentRepairHeaderModelQuery query)
        {
            ReportEquipmentRepairMatModel result = new ReportEquipmentRepairMatModel();
            result.Data = new List<ReportEquipmentRepairMatModel.Mat>();
            string sql = sql_SMRepairHeader_Mat_select;
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
                result.Data.Add(GetReportEquipmentRepairMatModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        /// <summary>
        /// 获取维修列表总行数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long EquipmentRepairHeaderDataGetRowCount(ReportEquipmentRepairHeaderModelQuery query)
        {
            string sql = sql_SMRepairHeader_select;
            #region Conditions
            sql += GetReportEquipmentSqlCondition(query);
            #endregion
            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        private string GetReportEquipmentSqlCondition(ReportEquipmentRepairHeaderModelQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.EquipmentID))
                {
                    sql += string.Format(" and a.EquipmentID='{0}'", query.EquipmentID);
                }
                if (!string.IsNullOrEmpty(query.RepairMan))
                {
                    sql += string.Format(" and a.RepairMan like N'%{0}%'", query.RepairMan);
                }
                if (!string.IsNullOrEmpty(query.ProductionLine))
                {
                    sql += string.Format(" and B.CurrProdLine= '{0}'", query.ProductionLine);
                }
                if (!query.FailedFrom.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), a.FailedTime, 23) >= '{0}' ", query.FailedFrom.ToString("yyyy-MM-dd"));
                }                
                if (!query.FailedTo.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), a.FailedTime, 23) <= '{0}'", query.FailedTo.ToString("yyyy-MM-dd"));
                }
                if (!query.FixedFrom.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), a.FixedTime, 23) >= '{0}' ", query.FixedFrom.ToString("yyyy-MM-dd"));
                }                
                if (!query.FixedTo.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), a.FixedTime, 23) <= '{0}' ", query.FixedTo.ToString("yyyy-MM-dd"));
                }
                if (!string.IsNullOrEmpty(query.Department))
                {
                    sql += string.Format(" and B.Department='{0}'", query.Department);
                }
                if (!string.IsNullOrEmpty(query.Status))
                {
                    sql += string.Format(" and a.Status='{0}'", query.Status);
                }
                if (!string.IsNullOrEmpty(query.RepID))
                {
                    sql += string.Format(" and RepID='{0}'", query.RepID);
                }
            }
            return sql;
        }

        private ReportEquipmentRepairHeaderModel.Item GetReportEquipmentRepairHeaderModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportEquipmentRepairHeaderModel.Item data = new ReportEquipmentRepairHeaderModel.Item
            {
                EquipmentID = DBConvert.DB2String(dr["EquipmentID"]),
                RepairMan = DBConvert.DB2String(dr["RepairMan"]),
                Department = DBConvert.DB2String(dr["Department"]),
                ProductionLine = DBConvert.DB2String(dr["ProductionLine"]),
                FailedTime = DBConvert.DB2Datetime(dr["FailedTime"]),
                FixedTime = DBConvert.DB2Datetime(dr["FixedTime"]),
                RepairCenter = DBConvert.DB2String(dr["RepairCenter"]),
                Status = DBConvert.DB2String(dr["Status"]),
                Category = DBConvert.DB2String(dr["Category"]),
                Symptom = DBConvert.DB2String(dr["Symptom"]),
                Cause = DBConvert.DB2String(dr["Cause"]),
                CauseType = DBConvert.DB2String(dr["CauseType"]),
                Action = DBConvert.DB2String(dr["Action"]),
                EndorsedOn = DBConvert.DB2Datetime(dr["EndorsedOn"]),
                EndorsedBy = DBConvert.DB2String(dr["EndorsedBy"]),
                FileName = DBConvert.DB2String(dr["FileName"]),
                Duration = DBConvert.DB2String(dr["Duration"]),
                DowntimeHour = DBConvert.DB2Decimal(dr["DowntimeHour"]),
                DowntimeCostUSD = DBConvert.DB2Decimal(dr["DowntimeCostUSD"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),
                RepID = DBConvert.DB2String(dr["RepID"]),
            };
            return data;
        }

        private ReportEquipmentRepairMatModel.Mat GetReportEquipmentRepairMatModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportEquipmentRepairMatModel.Mat data = new ReportEquipmentRepairMatModel.Mat 
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


        /// <summary>
        /// 获取维修状态
        /// </summary>
        /// <returns></returns>
        public List<string> GetSMRepairStatus()
        {
            string sql = sql_SMRepairHeader_Status_select;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Add(DBConvert.DB2String(dr["Status"]));
            });

            return result;
        }


        #endregion

        #endregion
    }
}
