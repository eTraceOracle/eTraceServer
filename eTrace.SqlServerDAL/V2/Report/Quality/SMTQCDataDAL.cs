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
    public class SMTQCDataDAL : DALBase, ISMTQCDataDAL
    {
        #region corts

        public SMTQCDataDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public SMTQCDataDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields

        private const string sql_SMTQCData_select = @"SELECT Model, ProdDate, Shift, [Floor], LineType, Line, seqNo, Revision, PO, 
			TlaPN, TlaDesc, TestStation, CauseCode, DefectCode, [Description] as DefectCodeDesc, RootCause, T_SMTQC.Category, DefectQty, CompQty, 
			OFD, OppQty, InspectQty, BuildQty, ProdSNs, CompPosition, Component, CompDesc, 
			CompSupplier, DateCode, LotNo, Marking, TVA, Comment, BoardStatus, TinStoveStatus, 
			QCEmpID , CreatedOn, CreatedBy, WScarrierNo, PinNo, SA AS PCBA FROM T_SMTQC WITH(NOLOCK) left outer join T_RepCodes with (nolock) on T_SMTQC.DefectCode = T_RepCodes.Code and T_RepCodes.Category = 'DEFECT' where 1=1 ";

        //private const string sql_SMTQCData_select1 = @"WITH subqry AS (SELECT WIPID, IntSN, Model, PCBA, MotherBoardSN, CurrentProcess FROM T_WIPHeader WITH (nolock) WHERE WIPID = '";

        //private const string sql_SMTQCData_select2 = @" UNION ALL SELECT w.WIPID, w.IntSN, w.Model, w.PCBA, w.MotherBoardSN, w.CurrentProcess 
        //                                   FROM T_WIPHeader AS w WITH (nolock) CROSS JOIN subqry AS s WHERE (w.MotherBoardSN = s.WIPID)) 
        //                                   SELECT  tt.IntSN, tt.Model, tt.PCBA, m.IntSN AS MotherBoardSN, tt.CurrentProcess,c.process, c.CircuitCode, c.Component, c.CLID, c.JobID, c.ChangedOn, c.ChangedBy, c.Remarks, 
        //                                   l.DateCode, l.LotNo, l.VendorID, l.VendorName, l.VendorPN, l.Manufacturer, l.ManufacturerPN, l.RecDocNo 
        //                                   FROM subqry as tt LEFT OUTER JOIN T_WIPHeader AS m WITH (nolock) ON tt.MotherBoardSN = m.WIPID inner join t_sfcomptrace as c with(nolock) on tt.wipid=c.wipid 
        //                                   inner join t_clmaster as l with(nolock) on l.CLID = c.CLID ";

        #endregion

        #region methods


        #region methods SMTQCDataData- 设备数据        

        public ReportSMTQCDataModel GetSMTQCDataData(ReportSMTQCDataQuery query)
        {
            ReportSMTQCDataModel result = new ReportSMTQCDataModel();
            result.Data = new List<ReportSMTQCDataModel.Item>();

            #region Conditions
            string sql;
            sql = sql_SMTQCData_select;
            sql += GetReportSMTQCDataSqlCondition(query);
            #endregion

            string ordercol = " ProdDate, Model, Shift, Floor, Line, seqNo";
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
                result.Data.Add(GetReportSMTQCDataModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }
        public long SMTQCDataDataGetRowCount(ReportSMTQCDataQuery query)
        {
            #region Conditions
            string sql;
            sql = sql_SMTQCData_select;
            sql += GetReportSMTQCDataSqlCondition(query);
            #endregion

            string countSql = GetSQLCount( sql);
            //long rowCount=  dbHelper.GetPageCount(countSql);

            DataTable dtSN;
            dtSN = dbHelper.GetTable(sql, 60);
           long rowCount = dtSN.Rows.Count;

            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }
        public void GetSMTQCDataAction(ReportSMTQCDataQuery query, Action<ReportSMTQCDataModel.Item> action, Action actionEnd)
        {
            ReportSMTQCDataModel result = new ReportSMTQCDataModel();
            result.Data = new List<ReportSMTQCDataModel.Item>();

            #region Conditions
            string sql;
            sql = sql_SMTQCData_select;
            sql += GetReportSMTQCDataSqlCondition(query);
            #endregion

            string ordercol = " ProdDate, Model, Shift, Floor, Line, seqNo";
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
                    action(GetReportSMTQCDataModel(dr));
                }
            });
            if (actionEnd != null)
            {
                actionEnd();
            }
        }

        private string GetReportSMTQCDataSqlCondition(ReportSMTQCDataQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Model))
                {
                    string models;
                    models = query.Model.Replace(",", "','");
                    if (models.Contains("，"))
                        models = models.Replace("，", "','");
                    sql +=  string.Format(" and Model IN ('{0}') ", models);
                }

                if (query.PDF != null)
                    sql += string.Format(" and ProdDate >= ('{0}')", query.PDF.ToString());

                if (query.PDT == null)
                    sql += string.Format(" and ProdDate <= ('{0}')", query.PDF.ToString());

                if (query.PDT != null)
                    sql += string.Format(" and ProdDate <= ('{0}')", query.PDT.ToString());

                if (!string.IsNullOrEmpty(query.Shift))
                    sql += string.Format(" and Shift = ('{0}')", query.Shift);

                if (!string.IsNullOrEmpty(query.LineType))
                    sql += string.Format(" and LineType = ('{0}')", query.LineType);

                if (!string.IsNullOrEmpty(query.Floor))
                    sql += string.Format(" and Floor = ('{0}')", query.Floor);

            }
            return sql;
        }

        private ReportSMTQCDataModel.Item GetReportSMTQCDataModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMTQCDataModel.Item data = new ReportSMTQCDataModel.Item
            {
                Model = DBConvert.DB2String(dr["Model"]),
                ProdDate = DBConvert.DB2String(dr["ProdDate"]),
                Shift = DBConvert.DB2String(dr["Shift"]),
                Floor = DBConvert.DB2String(dr["Floor"]),
                LineType = DBConvert.DB2String(dr["LineType"]),
                Line = DBConvert.DB2String(dr["Line"]),
                SeqNo = DBConvert.DB2Decimal(dr["SeqNo"]),
                Revision = DBConvert.DB2String(dr["Revision"]),
                PO = DBConvert.DB2String(dr["PO"]),
                TlaPN = DBConvert.DB2String(dr["TlaPN"]),
                TlaDesc = DBConvert.DB2String(dr["TlaDesc"]),
                TestStation = DBConvert.DB2String(dr["TestStation"]),
                CauseCode = DBConvert.DB2String(dr["CauseCode"]),
                DefectCode = DBConvert.DB2String(dr["DefectCode"]),
                DefectCodeDesc = DBConvert.DB2String(dr["DefectCodeDesc"]),
                RootCause = DBConvert.DB2String(dr["RootCause"]),
                Comment = DBConvert.DB2String(dr["Comment"]),
                DefectQty = DBConvert.DB2Decimal(dr["DefectQty"]),
                CompQty = DBConvert.DB2Decimal(dr["CompQty"]),
                OFD = DBConvert.DB2Decimal(dr["OFD"]),
                OppQty = DBConvert.DB2Decimal(dr["OppQty"]),
                InspectQty = DBConvert.DB2Decimal(dr["InspectQty"]),
                BuildQty = DBConvert.DB2Decimal(dr["BuildQty"]),
                ProdSNs = DBConvert.DB2String(dr["ProdSNs"]),
                CompPosition = DBConvert.DB2String(dr["CompPosition"]),
                Component = DBConvert.DB2String(dr["Component"]),
                CompDesc = DBConvert.DB2String(dr["CompDesc"]),
                CompSupplier = DBConvert.DB2String(dr["CompSupplier"]),
                DateCode = DBConvert.DB2String(dr["DateCode"]),
                LotNo = DBConvert.DB2String(dr["LotNo"]),
                Marking = DBConvert.DB2String(dr["Marking"]),
                TVA = DBConvert.DB2String(dr["TVA"]),
                BoardStatus = DBConvert.DB2String(dr["BoardStatus"]),
                TinStoveStatus = DBConvert.DB2String(dr["TinStoveStatus"]),
                QCEmpID = DBConvert.DB2String(dr["QCEmpID"]),
                CreatedOn = DBConvert.DB2Datetime(dr["CreatedOn"]),
                CreatedBy = DBConvert.DB2String(dr["CreatedBy"]),
                WScarrierNo = DBConvert.DB2String(dr["WScarrierNo"]),
                PinNo = DBConvert.DB2String(dr["PinNo"]),
                PCBA = DBConvert.DB2String(dr["PCBA"])                
            };
            return data;
        }      



        #endregion

        #endregion
    }
}
