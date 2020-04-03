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
    public class EquipmentPMPlanDAL : DALBase, IEquipmentPMPlanDAL
    {                                                    
        #region corts

        public EquipmentPMPlanDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public EquipmentPMPlanDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields


        private const string sql_SMEquipmentPMPlanDatas_select = @"SELECT Category, SubCategory, Frequency, Tolerance, PMSpecID, dateadd(hour,8,ChangedOn) as ChangedOn, ChangedBy, Remarks
                                                            FROM T_SMEquipPMPlan WITH (nolock) WHERE 1=1";

        private const string sql_SMEquipmentPMPlan_Cate_select = @"select distinct Category from V_SMEquipmentFixture WITH (nolock) order by Category asc";

        private const string sql_SMEquipmentPMPlan_SubCate_select = @"select distinct SubCategory from V_SMEquipmentFixture WITH (nolock) order by SubCategory asc";

        #endregion

        #region methods


        #region methods GetSMEquipmentPMPlanDatas - Equipment /Fixture PM Plan Data 
        /// <summary>
        /// 获取保养计划数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportEquipmentPMPlanModel GetSMEquipmentPMPlanDatas(ReportEquipmentPMPlanModelQuery query)
        {
            ReportEquipmentPMPlanModel result = new ReportEquipmentPMPlanModel();
            result.Data = new List<ReportEquipmentPMPlanModel.Item>();
            string sql = sql_SMEquipmentPMPlanDatas_select;
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
                result.Data.Add(GetReportEquipmentPMPlanModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }
        /// <summary>
        /// 获取维修列表总行数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long EquipmentPMPlanDataGetRowCount(ReportEquipmentPMPlanModelQuery query)
        {
            string sql = sql_SMEquipmentPMPlanDatas_select;
            #region Conditions
            sql += GetReportEquipmentSqlCondition(query);
            #endregion
            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        private string GetReportEquipmentSqlCondition(ReportEquipmentPMPlanModelQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Category))
                {
                    sql += string.Format(" and Category='{0}'", query.Category);
                }
                if (!string.IsNullOrEmpty(query.SubCategory))
                {
                    sql += string.Format(" and SubCategory ='{0}'", query.SubCategory);
                }
                if (!string.IsNullOrEmpty(query.Frequency))
                {
                    sql += string.Format(" and Frequency= '{0}'", query.Frequency);
                }
            }
            return sql;
        }

        private ReportEquipmentPMPlanModel.Item GetReportEquipmentPMPlanModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportEquipmentPMPlanModel.Item data = new ReportEquipmentPMPlanModel.Item
            {
                Category = DBConvert.DB2String(dr["Category"]),
                SubCategory = DBConvert.DB2String(dr["SubCategory"]),
                Frequency = DBConvert.DB2String(dr["Frequency"]),
                Tolerance = DBConvert.DB2String(dr["Tolerance"]),
                PMSpecID = DBConvert.DB2String(dr["PMSpecID"]),
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
        public List<string> GetSMEquipmentPMPlanCate()
        {
            string sql = sql_SMEquipmentPMPlan_Cate_select;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Add(DBConvert.DB2String(dr["Category"]));
                //result.Add(DBConvert.DB2String(dr["SubCategory"]));
            });

            return result;
        }

        public List<string> GetSMEquipmentPMPlanSubCate()
        {
            string sql = sql_SMEquipmentPMPlan_SubCate_select;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                //result.Add(DBConvert.DB2String(dr["Category"]));
                result.Add(DBConvert.DB2String(dr["SubCategory"]));
            });

            return result;
        }


        #endregion

        #endregion
    }
}
