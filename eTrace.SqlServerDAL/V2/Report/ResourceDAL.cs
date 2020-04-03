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
    public class ResourceDAL : DALBase, IResourceDAL
    {
        #region corts

        public ResourceDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public ResourceDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields

        private const string sql_product_process_select = @"SELECT Process, Description, ProcessType, Status, TFS, ProdLineType, Equipment, CompTrace, ChangedOn, ChangedBy, Remarks
FROM T_ProductProcess where 1=1 ";
        #endregion

        #region methods

        #region methods product Process- 工序配置

        public ProductStationModel GetProductStation(ProductStationQuery query)
        {
            ProductStationModel result = new ProductStationModel();
            result.Data = new List<ProductStationModel.Item>();
            string sql = sql_product_process_select;
            #region Conditions
            sql += GetProductStationSqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetProductStationModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        private string GetProductStationSqlCondition(ProductStationQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.ProcessType))
                {
                    sql += string.Format(" and ProcessType='{0}'", query.ProcessType);
                }
                if (!string.IsNullOrEmpty(query.Status))
                {
                    sql += string.Format(" and Status='{0}'", query.Status);
                }
            }
            return sql;
        }

        private ProductStationModel.Item GetProductStationModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ProductStationModel.Item data = new ProductStationModel.Item
            {
                Process = DBConvert.DB2StringUpper(dr["Process"]),
                Description = DBConvert.DB2String(dr["Description"]),
                ProcessType = DBConvert.DB2String(dr["ProcessType"]),
                Status = DBConvert.DB2StringUpper(dr["Status"]), 
            };
            return data;
        }
        #endregion
        #endregion
    }
}
