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
    public class SMMatTransDAL : DALBase, ISMMatTransDAL
    {                                                    
        #region corts

        public SMMatTransDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public SMMatTransDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields


        private const string sql_SMMatTransDatas_select = @" SELECT distinct a.TransID, a.[TO], a.Material, a.Qty, a.UOM, a.UnitCost, a.Currency, a.MovementType, a.FromLocID, a.ToLocID, a.PO, dateadd(hour,8,a.ChangedOn) as ChangedOn, a.ChangedBy, a.Remarks as PNRemarks,  
                                                                  b.Store as fromStore, c.Store as toStore ,d.Vendor,f.CostCenter,f.RequestedBy,d.Remarks as TORemarks,e.Description  
                                                                  FROM T_SMMatTrans as a WITH (nolock) left outer join T_SMLoc as b with(nolock) on a.FromLocID = b.LocID  
                                                                  left outer join T_SMLoc as c with(nolock) on a.ToLocID = c.LocID  
                                                                  left outer join T_SMTOHeader as d with(nolock) on a.[TO] =d.[TO]  
                                                                  left outer join T_SMTOItem as f with(nolock) on d.[TO] =f.[TO] and a.Material =f.Material and a.Qty =f.Qty and a.FromLocID =f.FromLocID and a.ToLocID =f.ToLocID  
                                                                  left outer join T_SMMaterial e with(nolock) on a.Material =e.Material  
                                                                  where 1=1 ";

        private const string sql_SMMatTrans_FromLocID_select = @" select distinct FromLocID from T_SMMatTrans order by FromLocID asc ";

        private const string sql_SMMatTrans_ToLocID_select = @" select distinct ToLocID from T_SMMatTrans order by ToLocID asc ";

        private const string sql_SMMatTrans_MovementType_select = @" select distinct MovementType from T_SMMatMovementType order by MovementType asc ";

        #endregion

        #region methods

        /// <summary>
        /// 获取备件交易数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportSMMatTransModel GetSMMatTransDatas(ReportSMMatTransModelQuery query)
        {
            ReportSMMatTransModel result = new ReportSMMatTransModel();
            result.Data = new List<ReportSMMatTransModel.Item>();
            string sql = sql_SMMatTransDatas_select;
            #region Conditions
            sql += GetReportpmentSqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportSMMatTransModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }
        /// <summary>
        /// 获取维修列表总行数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long SMMatTransDataGetRowCount(ReportSMMatTransModelQuery query)
        {
            string sql = sql_SMMatTransDatas_select;
            #region Conditions
            sql += GetReportpmentSqlCondition(query);
            #endregion
            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        private string GetReportpmentSqlCondition(ReportSMMatTransModelQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Material))
                {
                    sql += string.Format(" and a.Material='{0}'", query.Material);
                }
                if (!string.IsNullOrEmpty(query.TO))
                {
                    sql += string.Format(" and a.[TO]='{0}'", query.TO);
                }
                if (!string.IsNullOrEmpty(query.FromLocID))
                {
                    sql += string.Format(" and a.FromLocID= '{0}'", query.FromLocID);
                }
                if (!string.IsNullOrEmpty(query.ToLocID))
                {
                    sql += string.Format(" and a.ToLocID= '{0}'", query.ToLocID);
                }
                if (!string.IsNullOrEmpty(query.MovementType))
                {
                    sql += string.Format(" and a.MovementType= '{0}'", query.MovementType);
                }
            }
            return sql;
        }

        private ReportSMMatTransModel.Item GetReportSMMatTransModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMMatTransModel.Item data = new ReportSMMatTransModel.Item
            {
                TransID = DBConvert.DB2String(dr["TransID"]),
                TO = DBConvert.DB2String(dr["TO"]),
                Material = DBConvert.DB2String(dr["Material"]),
                Qty = DBConvert.DB2String(dr["Qty"]),
                UOM = DBConvert.DB2String(dr["UOM"]),
                UnitCost = DBConvert.DB2String(dr["UnitCost"]),
                Currency = DBConvert.DB2String(dr["Currency"]),
                MovementType = DBConvert.DB2String(dr["MovementType"]),
                FromLocID = DBConvert.DB2String(dr["FromLocID"]),
                ToLocID = DBConvert.DB2String(dr["ToLocID"]),
                PO = DBConvert.DB2String(dr["PO"]),
                ChangedOn = DBConvert.DB2Datetime(dr["ChangedOn"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                PNRemarks = DBConvert.DB2String(dr["PNRemarks"]),
                fromStore = DBConvert.DB2String(dr["fromStore"]),
                toStore = DBConvert.DB2String(dr["toStore"]),
                Vendor = DBConvert.DB2String(dr["Vendor"]),
                CostCenter = DBConvert.DB2String(dr["CostCenter"]),
                RequestedBy = DBConvert.DB2String(dr["RequestedBy"]),
                TORemarks = DBConvert.DB2String(dr["TORemarks"]),
                Description = DBConvert.DB2String(dr["Description"]),
            };
            return data;
        }


        public List<string> GetSMMatTransFromLocID()
        {
            string sql = sql_SMMatTrans_FromLocID_select;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Add(DBConvert.DB2String(dr["FromLocID"]));

            });

            return result;
        }

        public List<string> GetSMMatTransToLocID()
        {
            string sql = sql_SMMatTrans_ToLocID_select;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Add(DBConvert.DB2String(dr["ToLocID"]));
            });

            return result;
        }

        public List<string> GetMovementType()
        {
            string sql = sql_SMMatTrans_MovementType_select;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Add(DBConvert.DB2String(dr["MovementType"]));
            });

            return result;
        }


        #endregion
    }
}
