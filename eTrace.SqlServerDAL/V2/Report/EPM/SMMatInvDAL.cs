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
    public class SMMatInvDAL : DALBase, ISMMatInvDAL
    {                                                    
        #region corts

        public SMMatInvDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public SMMatInvDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields


        private const string sql_SMMatInvDatas_select = @"SELECT distinct a.Material,b.Description,d.VendorPN,b.MatType,b.Category,b.SubCategory,b.Spec, a.Qty, b.SafetyStock, a.UnitCost, b.Currency,b.UOM,c.Store,a.LocID, c.Rack, c.Bin, dateadd(hour,8,a.ChangedOn) as ChangedOn, a.ChangedBy, a.Remarks
                                                        FROM T_SMMatInv as a WITH (nolock)  
                                                        INNER JOIN T_SMMaterial as b WITH(Nolock) on a.Material = b.Material  
                                                        LEFT JOIN T_SMLoc as c WITH(nolock) ON c.LocID = a.LocID  
                                                        LEFT JOIN T_SMMaterialVPN as d with(nolock) on b.Material =d.Material  
                                                        WHERE 1=1 ";


        #endregion

        #region methods


        #region methods GetSMMatInvDatas - MatInv /Fixture PM Plan Data 
        /// <summary>
        /// 获取保养计划数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportSMMatInvModel GetSMMatInvDatas(ReportSMMatInvModelQuery query)
        {
            ReportSMMatInvModel result = new ReportSMMatInvModel();
            result.Data = new List<ReportSMMatInvModel.Item>();
            string sql = sql_SMMatInvDatas_select;
            #region Conditions
            sql += GetReportMatInvSqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportSMMatInvModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }
        /// <summary>
        /// 获取维修列表总行数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long SMMatInvDataGetRowCount(ReportSMMatInvModelQuery query)
        {
            string sql = sql_SMMatInvDatas_select;
            #region Conditions
            sql += GetReportMatInvSqlCondition(query);
            #endregion
            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        private string GetReportMatInvSqlCondition(ReportSMMatInvModelQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Material))
                {
                    sql += string.Format(" and a.Material='{0}'", query.Material);
                }
                if (!string.IsNullOrEmpty(query.LocID))
                {
                    sql += string.Format(" and a.LocID='{0}'", query.LocID);
                }
                if (!string.IsNullOrEmpty(query.Category))
                {
                    sql += string.Format(" and b.Category = N'{0}'", query.Category);
                }
                if (!string.IsNullOrEmpty(query.SubCategory))
                {
                    sql += string.Format(" and b.SubCategory = N'{0}'", query.SubCategory);
                }
                if (!string.IsNullOrEmpty(query.EquipCategory))
                {
                    sql += string.Format(" and b.EquipCategory = N'{0}'", query.EquipCategory);
                }
                if (!string.IsNullOrEmpty(query.EquipSubCategory))
                {
                    sql += string.Format(" and b.EquipSubCategory = N'{0}'", query.EquipSubCategory);
                }
                if (!string.IsNullOrEmpty(query.EquipModel))
                {
                    sql += string.Format(" and b.EquipModel = N'{0}'", query.EquipModel);
                }
                if (!string.IsNullOrEmpty(query.Store))
                {
                    sql += string.Format(" and c.Store ='{0}'", query.Store);
                }

            }
            return sql;
        }

        private ReportSMMatInvModel.Item GetReportSMMatInvModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMMatInvModel.Item data = new ReportSMMatInvModel.Item
            {
                Material = DBConvert.DB2String(dr["Material"]),
                Description = DBConvert.DB2String(dr["Description"]),
                VendorPN = DBConvert.DB2String(dr["VendorPN"]),
                MatType = DBConvert.DB2String(dr["MatType"]),
                Category = DBConvert.DB2String(dr["Category"]),
                SubCategory = DBConvert.DB2String(dr["SubCategory"]),
                Spec = DBConvert.DB2String(dr["Spec"]),
                Qty = DBConvert.DB2String(dr["Qty"]),
                SafetyStock = DBConvert.DB2String(dr["SafetyStock"]),
                UnitCost = DBConvert.DB2String(dr["UnitCost"]),
                Currency = DBConvert.DB2String(dr["Currency"]),
                UOM = DBConvert.DB2String(dr["UOM"]),
                Store = DBConvert.DB2String(dr["Store"]),
                LocID = DBConvert.DB2String(dr["LocID"]),
                Rack = DBConvert.DB2String(dr["Rack"]),
                Bin = DBConvert.DB2String(dr["Bin"]),
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
