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
    public class SMMaterialDAL : DALBase, ISMMaterialDAL
    {                                                    
        #region corts

        public SMMaterialDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public SMMaterialDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields


        private const string sql_SMMaterialDatas_select = @"SELECT distinct Material, Description, UOM, Spec, '' as Image, PictureUrl, StdCost, Currency, MatType, Category, SubCategory, EquipCategory, EquipSubCategory, EquipModel, DefaultStore,SafetyStock, Status, dateadd(hour,8,ChangedOn) as ChangedOn, ChangedBy, Remarks
                                                                FROM T_SMMaterial WITH (nolock) WHERE 1=1";

        private const string sql_SMMaterial_Category_select = @"select distinct Category from T_SMMaterial WITH(nolock) order by Category asc";

        private const string sql_SMMaterial_SubCategory_select = @"select distinct SubCategory from T_SMMaterial WITH(nolock) order by SubCategory asc";

        private const string sql_SMMaterial_Store_select = @"select distinct SubCategory from T_SMMaterial WITH(nolock) order by SubCategory asc";

        private const string sql_SMMaterial_EquipCategory_select = @"select distinct EquipCategory from T_SMMaterial WITH(nolock) order by EquipCategory asc";

        private const string sql_SMMaterial_EquipSubCategory_select = @"select distinct EquipSubCategory from T_SMMaterial WITH(nolock) order by EquipSubCategory asc";

        private const string sql_SMMaterial_EquipModel_select = @"select distinct EquipModel from T_SMMaterial WITH(nolock) order by EquipModel asc";

        private const string sql_SMMaterial_DefaultStore_select = @"select distinct DefaultStore from T_SMMaterial WITH (nolock) order by DefaultStore asc";
        #endregion

        #region methods


        #region methods GetSMMaterialDatas - Material Data 
        /// <summary>
        /// 获取物料数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportSMMaterialModel GetSMMaterialDatas(ReportSMMaterialModelQuery query)
        {
            ReportSMMaterialModel result = new ReportSMMaterialModel();
            result.Data = new List<ReportSMMaterialModel.Item>();
            string sql = sql_SMMaterialDatas_select;
            #region Conditions
            sql += GetReportMaterialSqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportSMMaterialModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        public long SMMaterialDataGetRowCount(ReportSMMaterialModelQuery query)
        {
            string sql = sql_SMMaterialDatas_select;
            #region Conditions
            sql += GetReportMaterialSqlCondition(query);
            #endregion
            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        private string GetReportMaterialSqlCondition(ReportSMMaterialModelQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Category))
                {
                    sql += string.Format(" and Category= N'{0}'", query.Category);
                }
                if (!string.IsNullOrEmpty(query.SubCategory))
                {
                    sql += string.Format(" and SubCategory = N'{0}'", query.SubCategory);
                }
                if (!string.IsNullOrEmpty(query.EquipCategory))
                {
                    sql += string.Format(" and EquipCategory = N'{0}'", query.EquipCategory);
                }
                if (!string.IsNullOrEmpty(query.EquipSubCategory))
                {
                    sql += string.Format(" and EquipSubCategory = N'{0}'", query.EquipSubCategory);
                }
                if (!string.IsNullOrEmpty(query.Material))
                {
                    sql += string.Format(" and Material = N'{0}'", query.Material);
                }
                if (!string.IsNullOrEmpty(query.EquipModel))
                {
                    sql += string.Format(" and EquipModel= N'{0}'", query.EquipModel);
                }
                if (!string.IsNullOrEmpty(query.DefaultStore))
                {
                    sql += string.Format(" and DefaultStore= '{0}'", query.DefaultStore);
                }
            }
            return sql;
        }

        private ReportSMMaterialModel.Item GetReportSMMaterialModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMMaterialModel.Item data = new ReportSMMaterialModel.Item
            {
                Material = DBConvert.DB2String(dr["Material"]),
                Description = DBConvert.DB2String(dr["Description"]),
                UOM = DBConvert.DB2String(dr["UOM"]),
                Spec = DBConvert.DB2String(dr["Spec"]),
                Image = DBConvert.DB2String(dr["Image"]),
                PictureUrl = DBConvert.DB2String(dr["PictureUrl"]),
                StdCost = DBConvert.DB2String(dr["StdCost"]),
                Currency = DBConvert.DB2String(dr["Currency"]),
                MatType = DBConvert.DB2String(dr["MatType"]),
                Category = DBConvert.DB2String(dr["Category"]),
                SubCategory = DBConvert.DB2String(dr["SubCategory"]),
                EquipCategory = DBConvert.DB2String(dr["EquipCategory"]),
                EquipSubCategory = DBConvert.DB2String(dr["EquipSubCategory"]),
                EquipModel = DBConvert.DB2String(dr["EquipModel"]),
                DefaultStore = DBConvert.DB2String(dr["DefaultStore"]),
                SafetyStock = DBConvert.DB2String(dr["SafetyStock"]),
                Status = DBConvert.DB2String(dr["Status"]),
                ChangedOn = DBConvert.DB2Datetime(dr["ChangedOn"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),
            };
            return data;
        }



        public List<string> GetSMMaterialCategory()
        {
            string sql = sql_SMMaterial_Category_select;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Add(DBConvert.DB2String(dr["Category"]));
            });

            return result;
        }

        public List<string> GetSMMaterialSubCategory()
        {
            string sql = sql_SMMaterial_SubCategory_select;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Add(DBConvert.DB2String(dr["SubCategory"]));
            });

            return result;
        }

        public List<string> GetSMMaterialEquipCategory()
        {
            string sql = sql_SMMaterial_EquipCategory_select;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Add(DBConvert.DB2String(dr["EquipCategory"]));
            });

            return result;
        }

        public List<string> GetSMMaterialEquipSubCategory()
        {
            string sql = sql_SMMaterial_EquipSubCategory_select;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Add(DBConvert.DB2String(dr["EquipSubCategory"]));
            });

            return result;
        }

        public List<string> GetSMMaterialEquipModel()
        {
            string sql = sql_SMMaterial_EquipModel_select;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Add(DBConvert.DB2String(dr["EquipModel"]));
            });

            return result;
        }

        public List<string> GetSMMaterialDefaultStore()
        {
            string sql = sql_SMMaterial_DefaultStore_select;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Add(DBConvert.DB2String(dr["DefaultStore"]));
            });

            return result;
        }


        #endregion

        #endregion
    }
}
