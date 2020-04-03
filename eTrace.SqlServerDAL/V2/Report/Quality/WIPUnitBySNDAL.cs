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
using System.Data;

namespace eTrace.Report.SqlServerDAL.V2.Report
{
    public class WIPUnitBySNDAL : DALBase, IWIPUnitBySNDAL
    {
        #region corts

        public WIPUnitBySNDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public WIPUnitBySNDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields

        private const string sql_WIPUnitBySN_WIPID_select = "select  WIPID from T_WIPHeader with (nolock) where 1=1 ";              

        //private const string sql_tdHeader_select = @"select * from T_TDHeader with(nolock) where 1=1 ";

        //private const string sql_WIPUnitBySN_select = @"SELECT EquipmentID,Category, SubCategory, Description, Model, Spec, 
        //                                                    CONVERT(varchar(10),MfrDate,120) as MfrDate, CurrProdLine, FixedAssessID, Manufacturer, 
        //                                                    ManufacturerSN, CONVERT(varchar(10),AcqDate,120) as AcqDate, Department, SeqOnLine, Owner, 
        //                                                    Status,ChangedOn,ChangedBy,Remarks FROM T_SMEquipment a  WITH (nolock) where 1=1 ";

        private const string sql_WIPUnitBySN_select1 = @"WITH subqry AS (SELECT WIPID, IntSN, Model, PCBA, MotherBoardSN, CurrentProcess FROM T_WIPHeader WITH (nolock) WHERE WIPID = '";
					
        private const string sql_WIPUnitBySN_select2 = @" UNION ALL SELECT w.WIPID, w.IntSN, w.Model, w.PCBA, w.MotherBoardSN, w.CurrentProcess 
                                           FROM T_WIPHeader AS w WITH (nolock) CROSS JOIN subqry AS s WHERE (w.MotherBoardSN = s.WIPID)) 
                                           SELECT  tt.IntSN, tt.Model, tt.PCBA, m.IntSN AS MotherBoardSN, tt.CurrentProcess,c.process, c.CircuitCode, c.Component, c.CLID, c.JobID, c.ChangedOn, c.ChangedBy, c.Remarks, 
                                           l.DateCode, l.LotNo, l.VendorID, l.VendorName, l.VendorPN, l.Manufacturer, l.ManufacturerPN, l.RecDocNo 
                                           FROM subqry as tt LEFT OUTER JOIN T_WIPHeader AS m WITH (nolock) ON tt.MotherBoardSN = m.WIPID inner join t_sfcomptrace as c with(nolock) on tt.wipid=c.wipid 
                                           inner join t_clmaster as l with(nolock) on l.CLID = c.CLID ";

        #endregion

        #region methods


        #region methods WIPUnitBySNData- 设备数据

        /// <summary>
        /// 获取DJ信息
        /// </summary>
        /// <returns></returns>
        public List<string> GetWIPID(string IntSN)
        {
            string sql = sql_WIPUnitBySN_WIPID_select;
            sql += string.Format(" and IntSN='{0}'", IntSN);

            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Add(DBConvert.DB2String(dr["WIPID"]));
            });

            return result;
        }
        
        public ReportWIPUnitBySNModel GetWIPUnitBySNData(ReportWIPUnitBySNQuery query)
        {
            ReportWIPUnitBySNModel result = new ReportWIPUnitBySNModel();
            result.Data = new List<ReportWIPUnitBySNModel.Item>();

            #region Conditions
            string WIPID = GetWIPID(query.IntSN)[0];
            string sql;
            sql = sql_WIPUnitBySN_select1;
            sql += WIPID;
            sql += "'";
            sql += sql_WIPUnitBySN_select2;
            #endregion

            string ordercol = " tt.intsn,c.process,c.CircuitCode";
            PageSql pSql = GetPagerSQL(query, sql, "", ordercol);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                //result.Pager.TotalCount = dbHelper.GetPageCount(pSql.SQLCount);

                DataTable dtSN;
                dtSN = dbHelper.GetTable(sql,60);
                result.Pager.TotalCount = dtSN.Rows.Count;
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportWIPUnitBySNModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }
        public long WIPUnitBySNDataGetRowCount(ReportWIPUnitBySNQuery query)
        {
            //string sql = sql_WIPUnitBySN_select;
            //#region Conditions
            //sql += GetReportWIPUnitBySNSqlCondition(query);
            //#endregion

            #region Conditions
            string WIPID = GetWIPID(query.IntSN)[0];
            string sql;
            sql = sql_WIPUnitBySN_select1;
            sql += WIPID;
            sql += "'";
            sql += sql_WIPUnitBySN_select2;
            #endregion

            string countSql = GetSQLCount( sql);
            //long rowCount=  dbHelper.GetPageCount(countSql);

            DataTable dtSN;
            dtSN = dbHelper.GetTable(sql, 60);
           long rowCount = dtSN.Rows.Count;

            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }
        public void GetWIPUnitBySNAction(ReportWIPUnitBySNQuery query, Action<ReportWIPUnitBySNModel.Item> action, Action actionEnd)
        {
            ReportWIPUnitBySNModel result = new ReportWIPUnitBySNModel();
            result.Data = new List<ReportWIPUnitBySNModel.Item>();

            //string sql = sql_WIPUnitBySN_select;
            //#region Conditions
            //sql += GetReportWIPUnitBySNSqlCondition(query);
            //#endregion

            #region Conditions
            string WIPID = GetWIPID(query.IntSN)[0];
            string sql;
            sql = sql_WIPUnitBySN_select1;
            sql += WIPID;
            sql += "'";
            sql += sql_WIPUnitBySN_select2;
            #endregion

            string ordercol = " tt.intsn,c.process,c.CircuitCode";
            PageSql pSql = GetPagerSQL(query, sql, "", ordercol);

            //PageSql pSql = GetPagerSQL(query, sql);

            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                //result.Pager.TotalCount = dbHelper.GetPageCount(pSql.SQLCount);

                DataTable dtSN;
                dtSN = dbHelper.GetTable(sql, 60);
                result.Pager.TotalCount = dtSN.Rows.Count;
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                if (action != null)
                {
                    action(GetReportWIPUnitBySNModel(dr));
                }
            });
            if (actionEnd != null)
            {
                actionEnd();
            }
        }

        private string GetReportWIPUnitBySNSqlCondition(ReportWIPUnitBySNQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.IntSN))
                {
                    sql += string.Format(" and a.IntSN='{0}'", query.IntSN);
                }
                
            }
            return sql;
        }

        private ReportWIPUnitBySNModel.Item GetReportWIPUnitBySNModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportWIPUnitBySNModel.Item data = new ReportWIPUnitBySNModel.Item
            {
                IntSN = DBConvert.DB2String(dr["IntSN"]),
                Model = DBConvert.DB2String(dr["Model"]),
                PCBA = DBConvert.DB2String(dr["PCBA"]),
                MotherBoardSN = DBConvert.DB2String(dr["MotherBoardSN"]),
                CurrentProcess = DBConvert.DB2String(dr["CurrentProcess"]),
                process = DBConvert.DB2String(dr["process"]),
                CircuitCode = DBConvert.DB2String(dr["CircuitCode"]),
                Component = DBConvert.DB2String(dr["Component"]),
                CLID = DBConvert.DB2String(dr["CLID"]),
                JobID = DBConvert.DB2String(dr["JobID"]),
                ChangedOn = DBConvert.DB2Datetime(dr["ChangedOn"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),
                DateCode = DBConvert.DB2String(dr["DateCode"]),
                LotNo = DBConvert.DB2String(dr["LotNo"]),
                VendorID = DBConvert.DB2String(dr["VendorID"]),
                VendorName = DBConvert.DB2String(dr["VendorName"]),
                VendorPN = DBConvert.DB2String(dr["VendorPN"]),
                Manufacturer = DBConvert.DB2String(dr["Manufacturer"]),
                ManufacturerPN = DBConvert.DB2String(dr["ManufacturerPN"]),
                RecDocNo = DBConvert.DB2String(dr["RecDocNo"])
            };
            return data;
        }      



        #endregion

        #endregion
    }
}
