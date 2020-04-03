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
    public class SMInspSpecDAL : DALBase, ISMInspSpecDAL
    {                                                    
        #region corts

        public SMInspSpecDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public SMInspSpecDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields


        private const string sql_SMInspSpecDatas_select = @"SELECT InspSpecID, Description, dateadd(hour,8,ChangedOn) as ChangedOn, ChangedBy, Remarks
                                                            FROM T_SMInspSpec WITH (nolock) WHERE 1=1";

        private const string sql_SMInspSpec_InspSpecID_select = @"select distinct InspSpecID from T_SMInspSpec WITH (nolock) WHERE 1=1";

        private const string sql_SMInspSpec_InspSpecIDItem_select = @"SELECT  [InspSpecID] ,[InspItem] ,[ItemDesc] ,[InspCondition] ,[Standard] ,[LowerLimit] ,[UpperLimit] ,[Unit] ,[Remarks] FROM [dbo].[T_SMInspSpecItem] WITH (nolock) WHERE 1=1";


        #endregion

        #region methods


        #region methods GetSMInspSpecDatas
        /// <summary>
        /// 获取主数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportSMInspSpecModel GetSMInspSpecDatas(ReportSMInspSpecModelQuery query)
        {
            ReportSMInspSpecModel result = new ReportSMInspSpecModel();
            result.Data = new List<ReportSMInspSpecModel.Item>();
            string sql = sql_SMInspSpecDatas_select;
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
                result.Data.Add(GetReportSMInspSpecModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        public ReportSMInspSpecModel GetSMInspSpecItemDatas(ReportSMInspSpecModelQuery query)
        {
            ReportSMInspSpecModel result = new ReportSMInspSpecModel();
            result.Data = new List<ReportSMInspSpecModel.Item>();
            string sql = sql_SMInspSpec_InspSpecIDItem_select;
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
                result.Data.Add(GetReportSMInspSpecItemModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }
        /// <summary>
        /// 获取维修列表总行数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long SMInspSpecDataGetRowCount(ReportSMInspSpecModelQuery query)
        {
            string sql = sql_SMInspSpecDatas_select;
            #region Conditions
            sql += GetReportEquipmentSqlCondition(query);
            #endregion
            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        private string GetReportEquipmentSqlCondition(ReportSMInspSpecModelQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.InspSpecID))
                {
                    sql += string.Format(" and InspSpecID='{0}'", query.InspSpecID);
                }

            }
            return sql;
        }

        private ReportSMInspSpecModel.Item GetReportSMInspSpecModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMInspSpecModel.Item data = new ReportSMInspSpecModel.Item
            {
                InspSpecID = DBConvert.DB2String(dr["InspSpecID"]),
                Description = DBConvert.DB2String(dr["Description"]),
                ChangedOn = DBConvert.DB2Datetime(dr["ChangedOn"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),
            };
            return data;
        }

        private ReportSMInspSpecModel.Item GetReportSMInspSpecItemModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMInspSpecModel.Item data = new ReportSMInspSpecModel.Item
            {
                InspSpecID = DBConvert.DB2String(dr["InspSpecID"]),
                InspItem = DBConvert.DB2String(dr["InspItem"]),
                ItemDesc = DBConvert.DB2String(dr["ItemDesc"]),
                InspCondition = DBConvert.DB2String(dr["InspCondition"]),
                Standard = DBConvert.DB2String(dr["Standard"]),
                LowerLimit = DBConvert.DB2String(dr["LowerLimit"]),
                UpperLimit = DBConvert.DB2String(dr["UpperLimit"]),
                Unit = DBConvert.DB2String(dr["Unit"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),
            };
            return data;
        }


        /// <summary>
        /// 获取维修状态
        /// </summary>
        /// <returns></returns>
        public List<string> GetSMInspSpecID()
        {
            string sql = sql_SMInspSpec_InspSpecID_select;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Add(DBConvert.DB2String(dr["InspSpecID"]));
                //result.Add(DBConvert.DB2String(dr["SubCategory"]));
            });

            return result;
        }

        #endregion

        #endregion
    }
}
