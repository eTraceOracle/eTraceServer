using eTrace.Common;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.SqlServerDAL.V2.Report
{
    public class WIPLockDAL : DALBase, eTrace.Report.IDAL.IWIPLockDAL
    {
        private const string C_BASESQL = @"Select T_WIPLock.*, 
                                    T_RepItem.RepairerID, 
                                    convert(varchar(12),T_RepItem.RepairDate,101) as RepairDate,
                                    convert(varchar(12),T_RepItem.RepairDate,108) as RepairTime,
                                    T_RepReplacement.DefectCode,
                                    T_RepReplacement.Cause,
                                    T_RepReplacement.CompRefD,
                                    T_RepReplacement.CompPN  
                    from T_WIPLock  with(nolock)  INNER JOIN T_RepItem with(nolock) on
                        T_WIPLock.TriggeringWIPID = T_RepItem.RepID and 
                        T_WIPLock.RDCItem = T_RepItem.Item 
                            INNER JOIN T_RepReplacement with(nolock) on T_WIPLock.TriggeringWIPID = T_RepReplacement.RepId and 
                            T_WIPLock.RDCItem = T_RepReplacement.Item
                             where T_RepReplacement.PriDefect = 1 ";
        public WIPLockDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public WIPLockDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        /// <summary>
        /// 获取查询结果行数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long GetTotalCount(GetWipLockDataModelQuery.Item query)
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
        public List<GetWipLockDataResultModel.Item> GetWipLoackData(GetWipLockDataModelQuery query)
        {
            string sql = C_BASESQL + GetSqlCondition(query.Data);
            string orderBy = string.Empty;


            string sqlData = GetSQLData(sql, " Status , LockedOn desc");
            return dbHelper.GetList<GetWipLockDataResultModel.Item>(sqlData, GetItemFromDataReader);


        }
        /// <summary>
        /// 按照条件查找锁数据，分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<GetWipLockDataResultModel.Item> GetWipLoackDataByPage(GetWipLockDataModelQuery query)
        {

            //' order by   Status , LockedOn desc'
            string sql = C_BASESQL + GetSqlCondition(query.Data); 
             string sqlDataByPage=GetSQLDataByPage(sql, query.Pager);
            return    dbHelper.GetList<GetWipLockDataResultModel.Item>(sqlDataByPage, GetItemFromDataReader);
                   
        }
        /// <summary>
        /// 从dataReader读取数据到 Item
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private GetWipLockDataResultModel.Item GetItemFromDataReader(System.Data.SqlClient.SqlDataReader dr)
        {
            GetWipLockDataResultModel.Item data = new GetWipLockDataResultModel.Item
            {
                Cause= DBConvert.DB2String(dr["Cause"]),
                CauseCode= DBConvert.DB2String(dr["CauseCode"]),
                CompPN= DBConvert.DB2String(dr["CompPN"]),
                CompRefD= DBConvert.DB2String(dr["CompRefD"]),
                DefectCode= DBConvert.DB2String(dr["DefectCode"]),
                Details= DBConvert.DB2String(dr["Details"]),
                FailedTestStep= DBConvert.DB2String(dr["FailedTestStep"]),
                FA_Others= DBConvert.DB2String(dr["FA_Others"]),
                FA_PE= DBConvert.DB2String(dr["FA_PE"]),
                FA_PQE= DBConvert.DB2String(dr["FA_PQE"]),
                FA_TE= DBConvert.DB2String(dr["FA_TE"]),
                LockedOn= DBConvert.DB2Datetime(dr["LockedOn"]),
                LockType= DBConvert.DB2String(dr["LockType"]),
                Model= DBConvert.DB2String(dr["Model"]),
                PBR= DBConvert.DB2String(dr["PBR"]),
                PCBA= DBConvert.DB2String(dr["PCBA"]),
                Process= DBConvert.DB2String(dr["Process"]),
                ProdLine= DBConvert.DB2String(dr["ProdLine"]),
                Remarks= DBConvert.DB2String(dr["Remarks"]),
                RepairDate= DBConvert.DB2String(dr["RepairDate"]),
                RepairerID= DBConvert.DB2String(dr["RepairerID"]),
                RepairTime= DBConvert.DB2DatetimeNull(dr["RepairTime"]),
                Status = DBConvert.DB2String(dr["Status"]),
                UnlockedBy = DBConvert.DB2String(dr["UnlockedBy"]),
                UnlockedOn = DBConvert.DB2Datetime(dr["UnlockedOn"])


                //ExtSerialNo = DBConvert.DB2String(dr["ExtSerialNo"]),
                //IntSerialNo = DBConvert.DB2String(dr["IntSerialNo"]),
                //IPSNo = DBConvert.DB2String(dr["IPSNo"]),
                //IPSRevision = DBConvert.DB2String(dr["IPSRevision"]),
                //OperatorName = DBConvert.DB2String(dr["OperatorName"]),
                //PO = DBConvert.DB2String(dr["PO"]),
                //ProcessName = DBConvert.DB2String(dr["ProcessName"]),
                //ProdDate = DBConvert.DB2Datetime(dr["ProdDate"]),
                //ProgramName = DBConvert.DB2String(dr["ProgramName"]),
                //ProgramRevision = DBConvert.DB2String(dr["ProgramRevision"]),
                //Remark = DBConvert.DB2String(dr["Remark"]),
                //TDID = DBConvert.DB2String(dr["TDID"]),
                //TesterNo = DBConvert.DB2String(dr["TesterNo"]),
                //WIPIn = DBConvert.DB2Datetime(dr["WIPIn"]),
                //Model = DBConvert.DB2String(dr["Model"]),
                //PCBA = DBConvert.DB2String(dr["PCBA"]),
                //SeqNo = DBConvert.DB2Int(dr["SeqNo"]),
            };
            return data;
        }
        private string GetSqlCondition(eTrace.Model.V2.Report.GetWipLockDataModelQuery.Item queryItem)
        {
            string sql = string.Empty;
            if (queryItem != null)
            {
                if (queryItem.StartTime.HasValue)
                {
                    sql += string.Format(" and LockedOn>='{0}'", queryItem.StartTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (queryItem.EndTime.HasValue)
                {
                    sql += string.Format(" and LockedOn<'{0}'", queryItem.EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (!string.IsNullOrEmpty(queryItem.ProdLine))
                {
                    sql += string.Format(" and ProdLine='{0}'", queryItem.ProdLine);
                }
                if (!string.IsNullOrEmpty(queryItem.Process))
                {
                    sql += string.Format(" and Process='{0}'", queryItem.Process);
                }
                if (!string.IsNullOrEmpty(queryItem.Model))
                {
                    sql += string.Format(" and Model='{0}'", queryItem.Model);
                }
                if (!string.IsNullOrEmpty(queryItem.PCBA))
                {
                    sql += string.Format(" and PCBA='{0}'", queryItem.PCBA);
                }
            }
            return sql;
        }



    }
}
