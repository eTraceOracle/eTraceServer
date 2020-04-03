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
    public class InvOptimizationDAL : DALBase, IInvOptimizationDAL
    {
        #region corts

        public InvOptimizationDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public InvOptimizationDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields

        private const string sql_InvOptimization_select = @" ";


        #endregion

        #region rowCount
        private long rowCount;
        #endregion

        #region methods

        #region methods GetInvOptimizationDatas
        /// <summary>
        /// 获取 Inventory Optimization Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportInvOptimizationModel GetInvOptimizationData(ReportInvOptimizationModelQuery query)
        {
            ReportInvOptimizationModel result = new ReportInvOptimizationModel();
            result.Data = new List<ReportInvOptimizationModel.Item>();

            //string sql = sql_InvOptimization_select;
            //#region Conditions
            //sql += GetReportSqlCondition(query);
            //#endregion

            //string ordercol = " CLID";
            //PageSql pSql = GetPagerSQL(query, sql, "", ordercol);

            //if (!string.IsNullOrEmpty(pSql.SQLCount))
            //{
            //    result.Pager = new ModelPager();
            //    result.Pager.TotalCount = dbHelper.GetPageCount(pSql.SQLCount);
            //}

            // 方法1
            //dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            //{
            //    result.Data.Add(GetReportInvOptimizationModel(dr));
            //}, (isover) =>
            //{ result.IsOverMaxRow = isover; });

            // 方法2
            //IDataParameter[] parameters = new SqlParameter[1];
            //SqlParameter parms = new SqlParameter("@CLID", SqlDbType.VarChar, 100);
            //parms.Value = query.CLID;
            //parameters[0] = parms;
            //DataSet ds = dbHelper.RunProcedureDS("A_GetCLID_Test", parameters);
            //result.Data = dbHelper.DataSetToList<ReportInvOptimizationModel.Item>(ds, 0);

            // 方法3
            InvOptReport_InputData.OrgCode = query.OrgCode;
            InvOptReport_InputData.DmdCutDate_Shortage = query.DmdCutDate_Shortage;
            InvOptReport_InputData.POCutDate_Shortage = query.POCutDate_Shortage;
            InvOptReport_InputData.DmdCutDate_Excess = query.DmdCutDate_Excess;
            InvOptReport_InputData.POCutDate_Excess = query.POCutDate_Excess;
            InvOptReport_InputData.SafetyStock = query.SafetyStock;
            InvOptReport_InputData.ShortageItems = query.ShortageItems;

            try
            {
                DataSet ds = eTraceWS.GetMRPData(InvOptReport_InputData);
                result.Data = dbHelper.DataSetToList<ReportInvOptimizationModel.Item>(ds, 0);
                result.Pager = new ModelPager();
                if (result.Data != null)
                {
                if (result.Data.Count > 0)
                {
                    result.Pager.TotalCount = result.Data.Count;
                    rowCount = result.Data.Count;
                    }
                    else
                    {
                        result.Pager.TotalCount = 0;
                        rowCount = 0;
                    }
                }
                else
                {
                    result.Pager.TotalCount = 0;
                    rowCount = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return result;
        }


        /// <summary>
        /// 获取物料Summary Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long InvOptimizationDataGetRowCount(ReportInvOptimizationModelQuery query)
        {
            return 1;
        }

        private string GetReportSqlCondition(ReportInvOptimizationModelQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.OrgCode))
                {
                    sql += string.Format(" and OrgCode = '{0}' ", query.OrgCode); //"and " + "OrgCode= '" + query.OrgCode + "' ";
                }   
            }
            return sql;
        }

        private ReportInvOptimizationModel.Item GetReportInvOptimizationModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportInvOptimizationModel.Item data = new ReportInvOptimizationModel.Item

            {
                OrgCode = DBConvert.DB2String(dr["OrgCode"]),
            };
            return data;
        }



        #endregion

        #endregion
    }
}
