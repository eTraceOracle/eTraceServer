using eTrace.Common;
using eTrace.Model.V2.Report;
using eTrace.Model.V2.Report.DailyRepairList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.SqlServerDAL.V2.Report
{ 
    public class WipInWipOutDAL : DALBase, eTrace.Report.IDAL.DailyRepairList.IWipInWipOutDAL
    {
        private const string C_BASESQL = @"select 
                                        (case when a.TestStation is null then b.TestStation else a.TestStation end) as Station,
                                        (case when a.TestStationCount is null then 0 else a.TestStationCount end) as WipinQty,
                                        (case when b.TestStationCount is null then 0 else b.TestStationCount end) as WipoutQty
                                        from
	                                        (select t_repitem.TestStation, count(t_repitem.TestStation) as TestStationCount 
	                                        from t_repitem with (nolock) inner join t_repheader with (nolock) 
	                                        on t_repitem.repid = t_repheader.repid 
	                                        where t_repitem.teststation <> 'QC4'
	                                        and (t_repitem.RepairDate between '{0}' and '{1}') 
	                                        group by t_repitem.TestStation) as a
	                                        full outer join
	                                        (select t_repitem.TestStation, count(t_repitem.TestStation) as TestStationCount 
	                                        from t_repitem with (nolock) inner join t_repheader with (nolock) 
	                                        on t_repitem.repid = t_repheader.repid 
	                                        where t_repitem.teststation <> 'QC4' 
	                                        AND EXISTS (select 'x' from T_RepReplacement as rr with(nolock) 
				                                        where rr.repid=t_repitem.repid and rr.item=t_repitem.item
				                                        and rr.DefectCode<>'')
	                                        and (t_repitem.ChangedOn between '{0}' and '{1}') 
	                                        group by t_repitem.TestStation) as b
	                                        on a.TestStation=b.TestStation ";
        private const string C_ORDERBY = @" Station";
        public WipInWipOutDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public WipInWipOutDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        /// <summary>
        /// 获取查询结果行数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long GetTotalCount(RequestItem query)
        {
            string sql = GetSQLCount( string.Format(C_BASESQL, query.DateFrom, query.DateTo));

            long rowCount = dbHelper.GetCount(sql);
            return rowCount;
        }

        /// <summary>
        /// 按照条件查找锁数据，不分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<GetWipInWipOutModel.Item> GetData(GetWipInWipOutQuery query)
        {


            string sql =  string.Format( C_BASESQL, query.Data.DateFrom,query.Data.DateTo);
            string orderBy = string.Empty;


            string sqlData = GetSQLData(sql, C_ORDERBY);
            return dbHelper.GetList<GetWipInWipOutModel.Item>(sqlData, GetItemFromDataReader);


        }
        /// <summary>
        /// 按照条件查找锁数据，分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<GetWipInWipOutModel.Item> GetDataByPage(GetWipInWipOutQuery query)
        {

            //' order by   Status , LockedOn desc'
            string sql = string.Format(C_BASESQL, query.Data.DateFrom, query.Data.DateTo);
            if (query.Pager!=null)
            {
                query.Pager.Order = C_ORDERBY;
            }
             string sqlDataByPage=GetSQLDataByPage(sql, query.Pager);
            return    dbHelper.GetList<GetWipInWipOutModel.Item>(sqlDataByPage, GetItemFromDataReader);
                   
        }
        /// <summary>
        /// 从dataReader读取数据到 Item
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private GetWipInWipOutModel.Item GetItemFromDataReader(System.Data.SqlClient.SqlDataReader dr)
        {
            try
            {
                GetWipInWipOutModel.Item data = new GetWipInWipOutModel.Item
                {

                    Station = DBConvert.DB2String(dr["Station"]),
                    WipinQty = DBConvert.DB2Int(dr["WipinQty"]),
                    WipoutQty = DBConvert.DB2Int(dr["WipoutQty"])
                };
                return data;
            }
            catch (Exception ex)
            {

                return null;
            }
          
        }
        private string GetSqlCondition(RequestItem queryItem)
        {
            string sql = string.Empty;
            if (queryItem != null)
            {
                if (queryItem.DateFrom.HasValue)
                {
                    sql += string.Format(" and DateFrom>='{0}'", queryItem.DateFrom.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (queryItem.DateTo.HasValue)
                {
                    sql += string.Format(" and DateTime<'{0}'", queryItem.DateTo.Value.ToString("yyyy-MM-dd HH:mm:ss"));
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
