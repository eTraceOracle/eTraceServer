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
    public class SMLocDAL : DALBase, ISMLocDAL
    {                                                    
        #region corts

        public SMLocDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public SMLocDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields


        private const string sql_SMLocData_select = @"SELECT distinct A.LocID, B.FixtureID, B.ItemNo, A.Description, A.Category, A.SubCategory, A.MaxStorage, A.Store, A.Rack, A.Bin, dateadd(hour,8,A.ChangedOn) as ChangedOn, A.ChangedBy, A.Remarks FROM T_SMLoc AS A WITH (nolock) 
                                                  LEFT OUTER JOIN T_SMFixture AS B with(nolock) 
                                                  ON A.LocID = B.StorageLocation where 1=1 ";

        //private const string sql_SMLocCategory_select = @"select distinct Category,SubCategory  from T_SMLoc with(nolock) order by Category,SubCategory";

        private const string sql_SMLocCategory_select = @"select distinct Category  from T_SMLoc with(nolock) order by Category";

        private const string sql_SMLocSubCategory_select = @"select distinct SubCategory  from T_SMLoc with(nolock) order by SubCategory";


        private const string sql_SMLocStore_select = @"select distinct Store  from T_SMLoc with(nolock) order by Store";

        #endregion

        #region methods


        #region methods GetSMLocDatas
        /// <summary>
        /// 获取保养计划数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportSMLocModel GetSMLocDatas(ReportSMLocModelQuery query)
        {
            ReportSMLocModel result = new ReportSMLocModel();
            result.Data = new List<ReportSMLocModel.Item>();
            string sql = sql_SMLocData_select;
            #region Conditions
            sql += GetReportSMLocSqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportSMLocModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }


        //public ReportSMLocModel GetSMLocCate(ReportSMLocModelQuery query)
        //{
        //    ReportSMLocModel result = new ReportSMLocModel();
        //    result.Data = new List<ReportSMLocModel.Item>();
        //    string sql = sql_SMLocData_select;
        //    #region Conditions
        //    sql += GetReportSMLocSqlCondition(query);
        //    #endregion
        //    PageSql pSql = GetPagerSQL(query, sql);
        //    if (!string.IsNullOrEmpty(pSql.SQLCount))
        //    {
        //        result.Pager = new ModelPager();
        //        result.Pager.TotalCount = dbHelper.GetPageCount(pSql.SQLCount);
        //    }
        //    dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
        //    {
        //        result.Data.Add(GetReportSMLocModel(dr));
        //    }, (isover) =>
        //    { result.IsOverMaxRow = isover; });
        //    return result;
        //}

        /// <summary>
        /// Get Table Row Count
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long SMLocDataGetRowCount(ReportSMLocModelQuery query)
        {
            string sql = sql_SMLocData_select;
            #region Conditions
            sql += GetReportSMLocSqlCondition(query);
            #endregion
            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        private string GetReportSMLocSqlCondition(ReportSMLocModelQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Category))
                {
                    sql += string.Format(" and A.Category=N'{0}'", query.Category);
                }
                if (!string.IsNullOrEmpty(query.SubCategory))
                {
                    sql += string.Format(" and A.SubCategory =N'{0}'", query.SubCategory);
                }
                if (!string.IsNullOrEmpty(query.LocID))
                {
                    sql += string.Format(" and A.LocID= '{0}'", query.LocID);
                }
                if (!string.IsNullOrEmpty(query.Store))
                {
                    sql += string.Format(" and A.Store= N'{0}'", query.Store);
                }
            }
            return sql;
        }

        private ReportSMLocModel.Item GetReportSMLocModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMLocModel.Item data = new ReportSMLocModel.Item
            {
                LocID = DBConvert.DB2String(dr["LocID"]),
                FixtureID = DBConvert.DB2String(dr["FixtureID"]),
                ItemNo = DBConvert.DB2String(dr["ItemNo"]),
                Description = DBConvert.DB2String(dr["Description"]),
                Category = DBConvert.DB2String(dr["Category"]),
                SubCategory = DBConvert.DB2String(dr["SubCategory"]),              
                MaxStorage = DBConvert.DB2String(dr["MaxStorage"]),
                Store = DBConvert.DB2String(dr["Store"]),
                Rack = DBConvert.DB2String(dr["Rack"]),
                Bin = DBConvert.DB2String(dr["Bin"]),
                ChangedOn = DBConvert.DB2Datetime(dr["ChangedOn"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),
            };
            return data;
        }


        /// <summary>
        /// 获取类别
        /// </summary>
        /// <returns></returns>
        public ReportSMLocModel GetSMLocCate()
        {
            ReportSMLocModel result = new ReportSMLocModel();
            result.Data = new List<ReportSMLocModel.Item>();
            string sql = sql_SMLocCategory_select;
            //List<string> result = new List<string>();
            //dbHelper.ExecuteReader(sql, (dr) =>
            //{
            //    result.Data.Add(GetSMLocCateModel(dr));

            //}, (isover) =>
            //{ result.IsOverMaxRow = isover; });

            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Data.Add(GetSMLocCateModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;

        }

        private ReportSMLocModel.Item GetSMLocCateModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMLocModel.Item data = new ReportSMLocModel.Item
            {
                Category = DBConvert.DB2String(dr["Category"]),
                SubCategory = DBConvert.DB2String(dr["SubCategory"]),
            };
            return data;
        }

        public List<string> GetSMLocCategory()
        {
            string sql = sql_SMLocCategory_select;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Add(DBConvert.DB2String(dr["Category"]));
            });

            return result;
        }

        public List<string> GetSMLocSubCategory()
        {
            string sql = sql_SMLocSubCategory_select;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Add(DBConvert.DB2String(dr["SubCategory"]));
            });

            return result;
        }


        public List<string> GetSMLocStore()
        {
            string sql = sql_SMLocStore_select;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Add(DBConvert.DB2String(dr["Store"]));
            });

            return result;
        }


        #endregion

        #endregion
    }
}
