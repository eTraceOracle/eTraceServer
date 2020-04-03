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
    public class SMPIDAL : DALBase, ISMPIDAL
    {                                                    
        #region corts

        public SMPIDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public SMPIDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields


        private const string sql_SMPI_Header = @"SELECT distinct PIID, Assembly, AssemblyRev, OtherAssy, Side, ProdLine, BaseLine, PanelType, PanelSize, PanelDrawingNo, PanelDrawingRev, Model, ModelRev, PCB,PCBRev, OtherPCB, CONVERT(varchar(10),ValidTo,120) as ValidTo, Status, dateadd(hour,8,ChangedOn) as ChangedOn, ChangedBy, Remarks, ApprovalLevel, ApprovalStatus 
                                                FROM T_SMPI WITH (nolock)  WHERE 1=1";

        private const string sql_SMPI_Equipments = @"SELECT distinct a.PIID, a.EquipmentID, a.SeqNo, a.ProgramName, a.Remarks, 
                                                b.Item, b.ItemType, b.Category, 
                                                b.Standard, b.Unit 
                                                FROM T_SMPIEquip a WITH (nolock) INNER JOIN T_SMPIEquipItem  b WITH (nolock) ON a.PIID = b.PIID and a.EquipmentID = b.EquipmentID 
                                                WHERE 1=1 ";

        private const string sql_SMPI_Mats = @"SELECT distinct PIID, ItemNo, MatType, CircuitCode, '' as Image, Remarks 
                                                FROM T_SMPIMat WITH (nolock) 
                                                WHERE 1=1 ";

        private const string sql_SMPI_Historys = @"SELECT distinct KeyValue, Level, Result, SendBy, SendOn, SendTo, CopyTo, Remarks 
                                                FROM T_SysWFLog WITH (nolock) 
                                                WHERE 1=1 ";


        #endregion

        #region methods


        #region methods GetSMPIHeaderDatas - Equipment /Fixture PM Plan Data 
        /// <summary>
        /// GetSMPIHeaderDatas
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportSMPIHeaderModel GetSMPIHeaderDatas(ReportSMPIHeaderModelQuery query)
        {
            ReportSMPIHeaderModel result = new ReportSMPIHeaderModel();
            result.Data = new List<ReportSMPIHeaderModel.Header>();
            string sql = sql_SMPI_Header;
            #region Conditions
            sql += GetReportSMPISqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportSMPIHeaderModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        public ReportSMPIEquipmentsModel GetSMPIEquipmentsDatas(ReportSMPIHeaderModelQuery query)
        {
            ReportSMPIEquipmentsModel result = new ReportSMPIEquipmentsModel();
            result.Data = new List<ReportSMPIEquipmentsModel.Equipments>();
            string sql = sql_SMPI_Equipments;
            #region Conditions
            sql += GetReportSMPISqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportSMPIEquipmentsModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        public ReportSMPIMatsModel GetSMPIMatsDatas(ReportSMPIHeaderModelQuery query)
        {
            ReportSMPIMatsModel result = new ReportSMPIMatsModel();
            result.Data = new List<ReportSMPIMatsModel.Mats>();
            string sql = sql_SMPI_Mats;
            #region Conditions
            sql += GetReportSMPISqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportSMPIMatsModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        public ReportSMPIHistoryModel GetSMPIHistoryDatas(ReportSMPIHeaderModelQuery query)
        {
            ReportSMPIHistoryModel result = new ReportSMPIHistoryModel();
            result.Data = new List<ReportSMPIHistoryModel.History>();
            string sql = sql_SMPI_Historys;
            #region Conditions
            sql += GetReportSMPISqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportSMPIHistoryModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        /// <summary>
        /// SMPIDataGetRowCount
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long SMPIDataGetRowCount(ReportSMPIHeaderModelQuery query)
        {
            string sql = sql_SMPI_Header;
            #region Conditions
            sql += GetReportSMPISqlCondition(query);
            #endregion
            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        private string GetReportSMPISqlCondition(ReportSMPIHeaderModelQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Assembly))
                {
                    sql += string.Format(" and Assembly='{0}'", query.Assembly);
                }
                if (!string.IsNullOrEmpty(query.AssemblyRev))
                {
                    sql += string.Format(" and AssemblyRev ='{0}'", query.AssemblyRev);
                }
                if (!string.IsNullOrEmpty(query.ProdLine))
                {
                    sql += string.Format(" and ProdLine='{0}'", query.ProdLine);
                }
                if (!string.IsNullOrEmpty(query.BaseLine))
                {
                    sql += string.Format(" and BaseLine ='{0}'", query.BaseLine);
                }
                if (!string.IsNullOrEmpty(query.ApprovalStatus))
                {
                    sql += string.Format(" and ApprovalStatus='{0}'", query.ApprovalStatus);
                }
                if (!string.IsNullOrEmpty(query.EquipmentsPIID))
                {
                    sql += string.Format(" and a.PIID='{0}'", query.EquipmentsPIID);
                }
                if (!string.IsNullOrEmpty(query.MatsPIID))
                {
                    sql += string.Format(" and PIID='{0}'", query.MatsPIID);
                }
                if (!string.IsNullOrEmpty(query.HistoryPIID))
                {
                    sql += string.Format(" and KeyValue='{0}'", query.HistoryPIID);
                }
            }
            return sql;
        }

        private ReportSMPIHeaderModel.Header GetReportSMPIHeaderModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMPIHeaderModel.Header data = new ReportSMPIHeaderModel.Header
            {

                PIID = DBConvert.DB2String(dr["PIID"]),
                Assembly = DBConvert.DB2String(dr["Assembly"]),
                AssemblyRev = DBConvert.DB2String(dr["AssemblyRev"]),
                OtherAssy = DBConvert.DB2String(dr["OtherAssy"]),
                Side = DBConvert.DB2String(dr["Side"]),
                ProdLine = DBConvert.DB2String(dr["ProdLine"]),
                BaseLine = DBConvert.DB2String(dr["BaseLine"]),
                PanelType = DBConvert.DB2String(dr["PanelType"]),
                PanelSize = DBConvert.DB2String(dr["PanelSize"]),
                PanelDrawingNo = DBConvert.DB2String(dr["PanelDrawingNo"]),
                PanelDrawingRev = DBConvert.DB2String(dr["PanelDrawingRev"]),
                Model = DBConvert.DB2String(dr["Model"]),
                ModelRev = DBConvert.DB2String(dr["ModelRev"]),
                PCB = DBConvert.DB2String(dr["PCB"]),
                PCBRev = DBConvert.DB2String(dr["PCBRev"]),
                OtherPCB = DBConvert.DB2String(dr["OtherPCB"]),
                ValidTo = DBConvert.DB2String(dr["ValidTo"]),
                Status = DBConvert.DB2String(dr["Status"]),
                ChangedOn = DBConvert.DB2Datetime(dr["ChangedOn"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),
                ApprovalLevel = DBConvert.DB2String(dr["ApprovalLevel"]),
                ApprovalStatus = DBConvert.DB2String(dr["ApprovalStatus"]),

            };
            return data;
        }

        private ReportSMPIEquipmentsModel.Equipments GetReportSMPIEquipmentsModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMPIEquipmentsModel.Equipments data = new ReportSMPIEquipmentsModel.Equipments
            {
                PIID = DBConvert.DB2String(dr["PIID"]),
                EquipmentID = DBConvert.DB2String(dr["EquipmentID"]),
                SeqNo = DBConvert.DB2String(dr["SeqNo"]),
                ProgramName = DBConvert.DB2String(dr["ProgramName"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),
                Item = DBConvert.DB2String(dr["Item"]),
                ItemType = DBConvert.DB2String(dr["ItemType"]),
                Category = DBConvert.DB2String(dr["Category"]),
                Standard = DBConvert.DB2String(dr["Standard"]),
                Unit = DBConvert.DB2String(dr["Unit"]),
            };
            return data;
        }

        private ReportSMPIMatsModel.Mats GetReportSMPIMatsModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMPIMatsModel.Mats data = new ReportSMPIMatsModel.Mats
            {
                PIID = DBConvert.DB2String(dr["PIID"]),
                ItemNo = DBConvert.DB2String(dr["ItemNo"]),
                MatType = DBConvert.DB2String(dr["MatType"]),
                CircuitCode = DBConvert.DB2String(dr["CircuitCode"]),
                Image = DBConvert.DB2String(dr["Image"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),
            };
            return data;
        }

        private ReportSMPIHistoryModel.History GetReportSMPIHistoryModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMPIHistoryModel.History data = new ReportSMPIHistoryModel.History
            {
                KeyValue = DBConvert.DB2String(dr["KeyValue"]),
                Level = DBConvert.DB2String(dr["Level"]),
                Result = DBConvert.DB2String(dr["Result"]),
                SendBy = DBConvert.DB2String(dr["SendBy"]),
                SendTo = DBConvert.DB2String(dr["SendTo"]),
                CopyTo = DBConvert.DB2String(dr["CopyTo"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),
            };
            return data;
        }

        #endregion

        #endregion
    }
}
