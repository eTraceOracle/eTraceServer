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
    public class MoreThanOneDAL : DALBase, eTrace.Report.IDAL.DailyRepairList.IMoreThanOneDAL
    {
        private const string C_BASESQL = @"SELECT A.Model, A.TestStation, A.DefectCode, A.Cause, A.CompPN, A.CompRefD, A.Floor, A.Qty
                                            FROM
                                            (
                                            SELECT RH.Model, RI.TestStation, RR.Cause as DefectCode, RR.DefectCode as Cause, RR.CompPN, RR.CompRefD, RH.Floor, Count(1) as Qty
                                            FROM T_RepHeader AS RH with(nolock) INNER JOIN T_RepItem AS RI with(nolock) ON RH.RepID=RI.RepID
                                            INNER JOIN T_RepReplacement AS RR with(nolock) ON RI.RepID=RR.RepID AND RI.Item=RR.Item
                                            WHERE RI.RepairDate between '{0}' and '{1}'
                                            AND (RI.TestStation  <> 'QC4')
                                            GROUP BY RH.Model, RI.TestStation, RR.Cause,  RR.DefectCode, RR.CompPN, RR.CompRefD, RH.Floor
                                            ) AS A
                                            where ((A.TestStation in ('Hipot', 'Vibration')  and A.Qty >= 1) or (A.DefectCode in ('MECD-C') and A.Qty >= 1) or ( A.CompPN = '' and A.Qty >= 3) )
                                           ";
        private const string C_ORDERBY = @" A.Qty DESC";
        public MoreThanOneDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public MoreThanOneDAL(EmDBType dbType)
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
            string sql = GetSQLCount(string.Format(C_BASESQL, query.DateFrom, query.DateTo)); ;

            long rowCount = dbHelper.GetCount(sql);
            return rowCount;
        }

        /// <summary>
        /// 按照条件查找锁数据，不分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<GetMoreThanOneModel.Item> GetData(GetMoreThanOneQuery query)
        {


            string sql = string.Format(C_BASESQL, query.Data.DateFrom, query.Data.DateTo);
            string orderBy = string.Empty;


            string sqlData = GetSQLData(sql, C_ORDERBY);
            return dbHelper.GetList<GetMoreThanOneModel.Item>(sqlData, GetItemFromDataReader);


        }
        /// <summary>
        /// 按照条件查找锁数据，分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<GetMoreThanOneModel.Item> GetDataByPage(GetMoreThanOneQuery query)
        {

            //' order by   Status , LockedOn desc'
            string sql = string.Format(C_BASESQL, query.Data.DateFrom, query.Data.DateTo);
            if (query.Pager != null)
            {
                query.Pager.Order = C_ORDERBY;
            }
            string sqlDataByPage = GetSQLDataByPage(sql, query.Pager);
            return dbHelper.GetList<GetMoreThanOneModel.Item>(sqlDataByPage, GetItemFromDataReader);

        }
        /// <summary>
        /// 从dataReader读取数据到 Item
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private GetMoreThanOneModel.Item GetItemFromDataReader(System.Data.SqlClient.SqlDataReader dr)
        {
            GetMoreThanOneModel.Item data = new GetMoreThanOneModel.Item
            {
                Cause = DBConvert.DB2String(dr["Cause"]),
                CompPN = DBConvert.DB2String(dr["CompPN"]),
                CompRefD = DBConvert.DB2String(dr["CompRefD"]),
                DefectCode = DBConvert.DB2String(dr["DefectCode"]),
                Floor = DBConvert.DB2String(dr["Floor"]),
                Model = DBConvert.DB2String(dr["Model"]),
                Qty = DBConvert.DB2Int(dr["Qty"]),
                TestStation = DBConvert.DB2String(dr["TestStation"])
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
