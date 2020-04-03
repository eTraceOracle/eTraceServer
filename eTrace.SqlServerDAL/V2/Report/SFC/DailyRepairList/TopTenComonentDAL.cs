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
    public class TopTenComonentDAL : DALBase, eTrace.Report.IDAL.DailyRepairList.ITopTenComonentDAL
    {
        private const string C_BASESQL = @"SELECT D.*
                                            FROM
                                            (
                                            SELECT H.Model, R.CompPN, H.Floor,
                                                I.TestStation, R.Cause as DefectCode, R.DefectCode as Cause,R.CompRefD,
                                                Count(1) as Qty
                                            FROM T_RepHeader AS H with(nolock) INNER JOIN T_RepItem AS I with(nolock) ON H.RepID=I.RepID
                                            INNER JOIN T_RepReplacement AS R with(nolock) ON I.RepID=R.RepID AND I.Item=R.Item
                                            WHERE I.RepairDate between '{0}' and '{1}'
                                            AND (R.Cause is not null and R.Cause <> '' and R.DefectCode is not null and R.DefectCode <> '')
                                            AND (R.CompPN is not null and R.CompPN <> '') and I.TestStation  <> 'QC4'
                                            GROUP BY H.Model, R.CompPN, H.Floor,
                                            I.TestStation, R.Cause, R.DefectCode,R.CompRefD
                                            ) as D,
                                            (
                                            SELECT A.Model, A.CompPN, A.Floor
                                            FROM
                                            (
                                            SELECT RH.Model, RR.CompPN, RH.Floor, Count(1) as Qty
                                            FROM T_RepHeader AS RH with(nolock) INNER JOIN T_RepItem AS RI with(nolock) ON RH.RepID=RI.RepID
                                            INNER JOIN T_RepReplacement AS RR with(nolock) ON RI.RepID=RR.RepID AND RI.Item=RR.Item
                                            WHERE RI.RepairDate between'{0}' and '{1}'
                                            AND (RR.Cause is not null and RR.Cause <> '' and RR.DefectCode is not null and RR.DefectCode <> '')
                                            AND (RR.CompPN is not null and RR.CompPN <> '') and RI.TestStation  <> 'QC4'
                                            GROUP BY RH.Model, RR.CompPN, RH.Floor
                                            ) AS A
                                            where (A.Qty >= 3)
                                            ) AS B
                                            WHERE D.Model = B.Model and D.CompPN = B.CompPN and D.Floor = B.Floor";
        private const string C_ORDERBY = @"  D.Model, D.CompPN, D.Floor, D.Qty desc, D.TestStation";
        public TopTenComonentDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public TopTenComonentDAL(EmDBType dbType)
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
        public List<GetTopTenComonentModel.Item> GetData(GetTopTenComonentQuery query)
        {


            string sql = string.Format(C_BASESQL, query.Data.DateFrom, query.Data.DateTo);
            string orderBy = string.Empty;


            string sqlData = GetSQLData(sql, C_ORDERBY);
            return dbHelper.GetList<GetTopTenComonentModel.Item>(sqlData, GetItemFromDataReader);


        }
        /// <summary>
        /// 按照条件查找锁数据，分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<GetTopTenComonentModel.Item> GetDataByPage(GetTopTenComonentQuery query)
        {

            //' order by   Status , LockedOn desc'
            string sql = string.Format(C_BASESQL, query.Data.DateFrom, query.Data.DateTo);
            if (query.Pager != null)
            {
                query.Pager.Order = C_ORDERBY;
            }
            string sqlDataByPage = GetSQLDataByPage(sql, query.Pager);
            return dbHelper.GetList<GetTopTenComonentModel.Item>(sqlDataByPage, GetItemFromDataReader);

        }
        /// <summary>
        /// 从dataReader读取数据到 Item
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private GetTopTenComonentModel.Item GetItemFromDataReader(System.Data.SqlClient.SqlDataReader dr)
        {
            GetTopTenComonentModel.Item data = new GetTopTenComonentModel.Item
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
