using eTrace.Common;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.SqlServerDAL.V2.Report
{ 
    public class PorductErrorLogDAL : DALBase, eTrace.Report.IDAL.IProductErrorLogDAL
    {
        private const string C_BASESQL = @"SELECT DateTime, ErrorMsg, Module, UserName, Remarks FROM T_ErrorLog with(nolock) where 1=1 ";
        private const string C_ORDERBY = @" DateTime";
        public PorductErrorLogDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public PorductErrorLogDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        /// <summary>
        /// 获取查询结果行数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long GetTotalCount(GetProductErrorLogQuery.Item query)
        {
            string sql = GetSQLCount( C_BASESQL + GetSqlCondition(query));

            long rowCount = dbHelper.GetCount(sql);
            return rowCount;
        }

        /// <summary>
        /// 按照条件查找锁数据，不分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<GetProductErrorLogModel.Item> GetProductionErrorLog(GetProductErrorLogQuery query)
        {
            string sql = C_BASESQL + GetSqlCondition(query.Data);
            string orderBy = string.Empty;


            string sqlData = GetSQLData(sql, C_ORDERBY);
            return dbHelper.GetList<GetProductErrorLogModel.Item>(sqlData, GetItemFromDataReader);


        }
        /// <summary>
        /// 按照条件查找锁数据，分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<GetProductErrorLogModel.Item> GetProductionErrorLogByPage(GetProductErrorLogQuery query)
        {

            //' order by   Status , LockedOn desc'
            string sql = C_BASESQL + GetSqlCondition(query.Data);
            if (query.Pager!=null)
            {
                query.Pager.Order = C_ORDERBY;
            }
             string sqlDataByPage=GetSQLDataByPage(sql, query.Pager);
            return    dbHelper.GetList<GetProductErrorLogModel.Item>(sqlDataByPage, GetItemFromDataReader);
                   
        }
        /// <summary>
        /// 从dataReader读取数据到 Item
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private GetProductErrorLogModel.Item GetItemFromDataReader(System.Data.SqlClient.SqlDataReader dr)
        {
            GetProductErrorLogModel.Item data = new GetProductErrorLogModel.Item
            {
                DateTime= DBConvert.DB2Datetime(dr["DateTime"]),
                ErrorMsg= DBConvert.DB2String(dr["ErrorMsg"]),
                Module = DBConvert.DB2String(dr["Module"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),
                UserName = DBConvert.DB2String(dr["UserName"])
            };
            return data;
        }
        private string GetSqlCondition(eTrace.Model.V2.Report.GetProductErrorLogQuery.Item queryItem)
        {
            string sql = string.Empty;
            if (queryItem != null)
            {
                if (queryItem.ErrorDateFrom.HasValue)
                {
                    sql += string.Format(" and DateTime>='{0}'", queryItem.ErrorDateFrom.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (queryItem.ErrorDateTo.HasValue)
                {
                    sql += string.Format(" and DateTime<'{0}'", queryItem.ErrorDateTo.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (!string.IsNullOrEmpty(queryItem.Module))
                {
                    sql += string.Format(" and Module='{0}'", queryItem.Module);
                }
                //if (!string.IsNullOrEmpty(queryItem.Process))
                //{
                //    sql += string.Format(" and Process='{0}'", queryItem.Process);
                //}
                //if (!string.IsNullOrEmpty(queryItem.Model))
                //{
                //    sql += string.Format(" and Model='{0}'", queryItem.Model);
                //}
                //if (!string.IsNullOrEmpty(queryItem.PCBA))
                //{
                //    sql += string.Format(" and PCBA='{0}'", queryItem.PCBA);
                //}
            }
            return sql;
        }



    }
}
