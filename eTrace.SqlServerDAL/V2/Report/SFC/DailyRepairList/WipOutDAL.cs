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
    public class WipOutDAL : DALBase, eTrace.Report.IDAL.DailyRepairList.IWipOutDAL
    {
        private const string C_BASESQL = @"declare @RepairDefectCodeTemp table([DefectCode] VARCHAR (50),[Qty] int)
                                            Insert @RepairDefectCodeTemp
                                            select T_RepReplacement.DefectCode, count(T_RepReplacement.DefectCode) as Qty from t_repitem with (nolock) 
                                            inner join t_repheader with (nolock) on t_repitem.repid = t_repheader.repid 
                                            inner join T_RepReplacement with(nolock) on T_RepReplacement.repid=t_repitem.repid and T_RepReplacement.item=t_repitem.item
                                            where t_repitem.teststation <> 'QC4' AND (T_RepReplacement.DefectCode <> '')
                                            and (t_repitem.RepairDate between '{0}' and '{1}') 
                                            group by T_RepReplacement.DefectCode

                                            Select a.[DefectCode] AS Code,a.[Qty],cast(a.[Qty]*100.0/b.[Qty] as numeric(8,2)) as [Percent] 
                                            from @RepairDefectCodeTemp as a,(select sum([Qty]) as [Qty] from @RepairDefectCodeTemp) as b
                                             ";
        private const string C_ORDERBY = @" [Percent] desc ";
        public WipOutDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public WipOutDAL(EmDBType dbType)
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
            var sqlCountBase=  @"declare @RepairDefectCodeTemp table([DefectCode] VARCHAR (50),[Qty] int)
                                            Insert @RepairDefectCodeTemp
                                            select T_RepReplacement.DefectCode, count(T_RepReplacement.DefectCode) as Qty from t_repitem with (nolock) 
                                            inner join t_repheader with (nolock) on t_repitem.repid = t_repheader.repid 
                                            inner join T_RepReplacement with(nolock) on T_RepReplacement.repid=t_repitem.repid and T_RepReplacement.item=t_repitem.item
                                            where t_repitem.teststation <> 'QC4' AND (T_RepReplacement.DefectCode <> '')
                                            and (t_repitem.RepairDate between '{0}' and '{1}') 
                                            group by T_RepReplacement.DefectCode

                                            Select count(1)
                                            from @RepairDefectCodeTemp as a,(select sum([Qty]) as [Qty] from @RepairDefectCodeTemp) as b
                                             ";
            string sql =string.Format(sqlCountBase, query.DateFrom, query.DateTo); ;

            long rowCount = dbHelper.GetCount(sql);
            return rowCount;
        }

        /// <summary>
        /// 按照条件查找锁数据，不分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<GetWipOutModel.Item> GetData(GetWipOutQuery query)
        {


            string sql = string.Format(C_BASESQL, query.Data.DateFrom, query.Data.DateTo);
            string orderBy = string.Empty;


            string sqlData = GetSQLData(sql, C_ORDERBY);
            return dbHelper.GetList<GetWipOutModel.Item>(sqlData, GetItemFromDataReader);


        }
        /// <summary>
        /// 按照条件查找锁数据，分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<GetWipOutModel.Item> GetDataByPage(GetWipOutQuery query)
        {

            //' order by   Status , LockedOn desc'
            string sql = string.Format(C_BASESQL, query.Data.DateFrom, query.Data.DateTo);
            if (query.Pager != null)
            {
                query.Pager.Order = C_ORDERBY;
            }
            string sqlDataByPage = GetSQLDataByPage(sql, query.Pager);
            return dbHelper.GetList<GetWipOutModel.Item>(sqlDataByPage, GetItemFromDataReader);

        }
        /// <summary>
        /// 从dataReader读取数据到 Item
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private GetWipOutModel.Item GetItemFromDataReader(System.Data.SqlClient.SqlDataReader dr)
        {
            GetWipOutModel.Item data = new GetWipOutModel.Item
            {
                Code = DBConvert.DB2String(dr["Code"]),
                Percent = DBConvert.DB2Decimal(dr["Percent"]),
                Qty = DBConvert.DB2Int(dr["Qty"])
            };
            return data;
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
            }
            return sql;
        }



    }
}
