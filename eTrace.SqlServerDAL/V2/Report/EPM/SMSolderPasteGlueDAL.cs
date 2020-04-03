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
    public class SMSolderPasteGlueDAL : DALBase, ISMSolderPasteGlueDAL
    {                                                    
        #region corts

        public SMSolderPasteGlueDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public SMSolderPasteGlueDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields


        private const string sql_SMSolderPasteGlueDatas_select = @" select distinct b.Category, a.CLID, a.MaterialNo, a.Qty, a.UOM, a.LocID, CONVERT(varchar(10),a.ExpDate,120) as ExpDate, a.RemainingLife, dateadd(hour,8,a.WarmUpST) as WarmUpST, dateadd(hour,8, a.WarmUpET) as WarmUpET, dateadd(hour,8,a.StirringST) as StirringST, dateadd(hour,8,a.StirringET) as StirringET, dateadd(hour,8,a.FloorLifeST) as FloorLifeST, dateadd(hour,8,a.FloorLifeET) as FloorLifeET, dateadd(hour,8,a.FloorLifeST2) as FloorLifeST2, dateadd(hour,8,a.FloorLifeET2) as FloorLifeET2, dateadd(hour,8,a.OnStencilLifeST) as OnStencilLifeST, dateadd(hour,8,a.OnStencilLifeET) as OnStencilLifeET, a.LastTransaction, a.RefCLID, a.RefCLID2, dateadd(hour,8,a.ChangedOn) as ChangedOn, a.ChangedBy, a.Remarks 
                                                                    from T_SMCLID as a with(nolock) inner join T_SMStandard as b with(nolock) on a.MaterialNo = b.Item
                                                                    where 1=1 and b.Category in ('Glue','Solder Paste')  
                                                                    and lasttransaction <> 'Pending Recycle' ";

        private const string sql_SMSolderPasteGlue_LastTransaction_select = @"select distinct a.LastTransaction from T_SMCLID a with(nolock) inner join T_SMStandard as b with(nolock) on a.MaterialNo = b.Item 
                                                                            where b.Category in ('Glue','Solder Paste')
                                                                            and lasttransaction <> 'Pending Recycle'
                                                                            order by a.LastTransaction asc";


        #endregion

        #region methods


        #region methods GetSMSolderPasteGlueDatas - Equipment /Fixture PM Plan Data 
        /// <summary>
        /// 获取保养计划数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportSMSolderPasteGlueModel GetSMSolderPasteGlueDatas(ReportSMSolderPasteGlueModelQuery query)
        {
            ReportSMSolderPasteGlueModel result = new ReportSMSolderPasteGlueModel();
            result.Data = new List<ReportSMSolderPasteGlueModel.Item>();
            string sql = sql_SMSolderPasteGlueDatas_select;
            #region Conditions
            sql += GetReportEquipmentSqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportSMSolderPasteGlueModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }
        /// <summary>
        /// 获取维修列表总行数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long SMSolderPasteGlueDataGetRowCount(ReportSMSolderPasteGlueModelQuery query)
        {
            string sql = sql_SMSolderPasteGlueDatas_select;
            #region Conditions
            sql += GetReportEquipmentSqlCondition(query);
            #endregion
            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        private string GetReportEquipmentSqlCondition(ReportSMSolderPasteGlueModelQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.MaterialNo))
                {
                    sql += string.Format(" and a.MaterialNo in ('" + query.MaterialNo.ToString().Trim().Replace(",", "','") + "')");
                }
                if (!string.IsNullOrEmpty(query.CLID))
                {
                    sql += string.Format(" and a.CLID in ('" + query.CLID.ToString().Trim().Replace(",", "','") + "') ");
                }
                if (!query.DateFrom.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and a.ChangedOn >= '{0}' ", query.DateFrom.ToString("yyyy-MM-dd"));
                }
                if (!query.DateTo.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and a.ChangedOn <= '{0}' ", query.DateTo.ToString("yyyy-MM-dd"));
                }
                if (!string.IsNullOrEmpty(query.LastTransaction))
                {
                    sql += string.Format(" and a.LastTransaction= '{0}'", query.LastTransaction);
                }

                //false
                if (query.Validation)
                {
                    sql += string.Format(" and not a.RefCLID2 is null and a.RefCLID2 <> '' ");
                }
                else
                {
                    sql += string.Format(" and (a.RefCLID2 = '' or a.RefCLID2 is null) ");                
                }

            }
            return sql;
        }

        private ReportSMSolderPasteGlueModel.Item GetReportSMSolderPasteGlueModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMSolderPasteGlueModel.Item data = new ReportSMSolderPasteGlueModel.Item
            {
                Category = DBConvert.DB2String(dr["Category"]),
                CLID = DBConvert.DB2String(dr["CLID"]),
                MaterialNo = DBConvert.DB2String(dr["MaterialNo"]),
                Qty = DBConvert.DB2String(dr["Qty"]),
                UOM = DBConvert.DB2String(dr["UOM"]),
                LocID = DBConvert.DB2String(dr["LocID"]),
                ExpDate = DBConvert.DB2Datetime(dr["ExpDate"]),
                RemainingLife = DBConvert.DB2String(dr["RemainingLife"]),
                WarmUpST = DBConvert.DB2Datetime(dr["WarmUpST"]),
                WarmUpET = DBConvert.DB2Datetime(dr["WarmUpET"]),
                StirringST = DBConvert.DB2Datetime(dr["StirringST"]),
                StirringET = DBConvert.DB2Datetime(dr["StirringET"]),
                FloorLifeST = DBConvert.DB2Datetime(dr["FloorLifeST"]),
                FloorLifeET = DBConvert.DB2Datetime(dr["FloorLifeET"]),
                FloorLifeST2 = DBConvert.DB2Datetime(dr["FloorLifeST2"]),
                FloorLifeET2 = DBConvert.DB2Datetime(dr["FloorLifeET2"]),
                OnStencilLifeST = DBConvert.DB2Datetime(dr["OnStencilLifeST"]),
                OnStencilLifeET = DBConvert.DB2Datetime(dr["OnStencilLifeET"]),
                LastTransaction = DBConvert.DB2String(dr["LastTransaction"]),
                RefCLID = DBConvert.DB2String(dr["RefCLID"]),
                RefCLID2 = DBConvert.DB2String(dr["RefCLID2"]),
                ChangedOn = DBConvert.DB2Datetime(dr["ChangedOn"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),
            };
            return data;
        }


        /// <summary>
        /// Get LastTransaction
        /// </summary>
        /// <returns></returns>
        public List<string> GetSMSolderPasteGlueLastTransaction()
        {
            string sql = sql_SMSolderPasteGlue_LastTransaction_select;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Add(DBConvert.DB2String(dr["LastTransaction"]));
                //result.Add(DBConvert.DB2String(dr["SubCategory"]));
            });

            return result;
        }

        #endregion

        #endregion
    }
}
