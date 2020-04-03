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
    public class EquipmentPMSpecDAL : DALBase, IEquipmentPMSpecDAL
    {                                                    
        #region corts

        public EquipmentPMSpecDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public EquipmentPMSpecDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields


        private const string sql_SMEquipmentPMSpecDatas_select = @"SELECT [PMSpecID] ,[Description] ,[Frequency] ,[Status] ,[ChangedOn] ,[ChangedBy] ,[Remarks] FROM [dbo].[T_SMEquipPMSpec] where 1=1 ";

        private const string sql_SMEquipmentPMSpecPMItemDatas_select = @"SELECT [PMSpecID] ,[PMItem] ,[ItemDesc] ,[PMInstruction] ,[FileName] ,[Operator] ,[ChangedOn] ,[ChangedBy] ,[Remarks] FROM [dbo].[T_SMEquipPMSpecItem] where 1=1 ";


        #endregion

        #region methods


        #region methods GetSMEquipmentPMSpecDatas - Equipment /Fixture PM Plan Data 
        /// <summary>
        /// GetSMEquipmentPMSpecDatas
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportEquipmentPMSpecModel GetSMEquipmentPMSpecDatas(ReportEquipmentPMSpecModelQuery query)
        {
            ReportEquipmentPMSpecModel result = new ReportEquipmentPMSpecModel();
            result.Data = new List<ReportEquipmentPMSpecModel.Item>();
            string sql = sql_SMEquipmentPMSpecDatas_select;
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
                result.Data.Add(GetReportEquipmentPMSpecModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        public ReportEquipmentPMSpecPMItemModel GetSMEquipmentPMSpecPMItemDatas(ReportEquipmentPMSpecModelQuery query)
        {
            ReportEquipmentPMSpecPMItemModel result = new ReportEquipmentPMSpecPMItemModel();
            result.Data = new List<ReportEquipmentPMSpecPMItemModel.Item>();
            string sql = sql_SMEquipmentPMSpecPMItemDatas_select;
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
                result.Data.Add(GetReportEquipmentPMSpecPMItemModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        /// <summary>
        /// EquipmentPMSpecDataGetRowCount
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long EquipmentPMSpecDataGetRowCount(ReportEquipmentPMSpecModelQuery query)
        {
            string sql = sql_SMEquipmentPMSpecDatas_select;
            #region Conditions
            sql += GetReportEquipmentSqlCondition(query);
            #endregion
            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        private string GetReportEquipmentSqlCondition(ReportEquipmentPMSpecModelQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.PMSpecID))
                {
                    sql += string.Format(" and PMSpecID='{0}'", query.PMSpecID);
                }
                if (!string.IsNullOrEmpty(query.Frequency))
                {
                    sql += string.Format(" and Frequency ='{0}'", query.Frequency);
                }
            }
            return sql;
        }

        private ReportEquipmentPMSpecModel.Item GetReportEquipmentPMSpecModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportEquipmentPMSpecModel.Item data = new ReportEquipmentPMSpecModel.Item
            {
                PMSpecID = DBConvert.DB2String(dr["PMSpecID"]),
                Description = DBConvert.DB2String(dr["Description"]),
                Frequency = DBConvert.DB2String(dr["Frequency"]),
                Status = DBConvert.DB2String(dr["Status"]),
                ChangedOn = DBConvert.DB2Datetime(dr["ChangedOn"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),
            };
            return data;
        }

        private ReportEquipmentPMSpecPMItemModel.Item GetReportEquipmentPMSpecPMItemModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportEquipmentPMSpecPMItemModel.Item data = new ReportEquipmentPMSpecPMItemModel.Item
            {
                PMSpecID = DBConvert.DB2String(dr["PMSpecID"]),
                PMInstruction = DBConvert.DB2String(dr["PMInstruction"]),
                PMItem = DBConvert.DB2String(dr["PMItem"]),
                ItemDesc = DBConvert.DB2String(dr["ItemDesc"]),
                Operator = DBConvert.DB2String(dr["Operator"]),
                ChangedOn = DBConvert.DB2Datetime(dr["ChangedOn"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),
            };
            return data;
        }

        #endregion

        #endregion
    }
}
