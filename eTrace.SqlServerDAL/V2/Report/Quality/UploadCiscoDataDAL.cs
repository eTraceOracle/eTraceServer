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
    public class UploadCiscoDataDAL : DALBase, IUploadCiscoDataDAL
    {
        #region corts

        public UploadCiscoDataDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public UploadCiscoDataDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields

        private const string sql_UploadCiscoData_select1 = @"select a.ProductSerialNo,a.Model,a.CustomerPN,a.Revision,a.DeliveryNote,a.CustomerPO,a.Firmware,a.CreatedOn,a.CreatedBy  from (Select * from T_FlatFileSN where 1=1 ";
        private const string sql_UploadCiscoData_select2 = @" ) a  inner join T_ProductCPN b on a.Model = b.Model where b.Customer = 'Cisco' ";


        //private const string sql_UploadCiscoData_select1 = @"WITH subqry AS (SELECT WIPID, IntSN, Model, PCBA, MotherBoardSN, CurrentProcess FROM T_WIPHeader WITH (nolock) WHERE WIPID = '";

        //private const string sql_UploadCiscoData_select2 = @" UNION ALL SELECT w.WIPID, w.IntSN, w.Model, w.PCBA, w.MotherBoardSN, w.CurrentProcess 
        //                                   FROM T_WIPHeader AS w WITH (nolock) CROSS JOIN subqry AS s WHERE (w.MotherBoardSN = s.WIPID)) 
        //                                   SELECT  tt.IntSN, tt.Model, tt.PCBA, m.IntSN AS MotherBoardSN, tt.CurrentProcess,c.process, c.CircuitCode, c.Component, c.CLID, c.JobID, c.ChangedOn, c.ChangedBy, c.Remarks, 
        //                                   l.DateCode, l.LotNo, l.VendorID, l.VendorName, l.VendorPN, l.Manufacturer, l.ManufacturerPN, l.RecDocNo 
        //                                   FROM subqry as tt LEFT OUTER JOIN T_WIPHeader AS m WITH (nolock) ON tt.MotherBoardSN = m.WIPID inner join t_sfcomptrace as c with(nolock) on tt.wipid=c.wipid 
        //                                   inner join t_clmaster as l with(nolock) on l.CLID = c.CLID ";

        #endregion

        #region methods


        #region methods UploadCiscoDataData- 设备数据        

        public ReportUploadCiscoDataModel GetUploadCiscoDataData(ReportUploadCiscoDataQuery query)
        {
            ReportUploadCiscoDataModel result = new ReportUploadCiscoDataModel();
            result.Data = new List<ReportUploadCiscoDataModel.Item>();

            #region Conditions
            string sql;
            sql = sql_UploadCiscoData_select1;
            sql += GetReportUploadCiscoDataSqlCondition(query);
            sql += sql_UploadCiscoData_select2;
            #endregion

            string ordercol = " a.CreatedOn ASC ";
            PageSql pSql = GetPagerSQL(query, sql, "", ordercol);
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
                result.Data.Add(GetReportUploadCiscoDataModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }
        public long UploadCiscoDataDataGetRowCount(ReportUploadCiscoDataQuery query)
        {
            #region Conditions
            string sql;
            sql = sql_UploadCiscoData_select1;
            sql += GetReportUploadCiscoDataSqlCondition(query);
            sql += sql_UploadCiscoData_select2;
            #endregion

            string countSql = GetSQLCount(sql);
            //long rowCount=  dbHelper.GetPageCount(countSql);

            DataTable dtSN;
            dtSN = dbHelper.GetTable(sql, 60);
            long rowCount = dtSN.Rows.Count;

            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }
        public void GetUploadCiscoDataAction(ReportUploadCiscoDataQuery query, Action<ReportUploadCiscoDataModel.Item> action, Action actionEnd)
        {
            ReportUploadCiscoDataModel result = new ReportUploadCiscoDataModel();
            result.Data = new List<ReportUploadCiscoDataModel.Item>();

            #region Conditions
            string sql;
            sql = sql_UploadCiscoData_select1;
            sql += GetReportUploadCiscoDataSqlCondition(query);
            sql += sql_UploadCiscoData_select2;
            #endregion

            string ordercol = " a.CreatedOn ASC ";
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
                    action(GetReportUploadCiscoDataModel(dr));
                }
            });
            if (actionEnd != null)
            {
                actionEnd();
            }
        }

        private string GetReportUploadCiscoDataSqlCondition(ReportUploadCiscoDataQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (query.createFrom != null && query.createTo != null)
                    sql += string.Format(" and CreatedOn BETWEEN '{0}' and '{1}' ", query.createFrom?.ToString("yyyy-MM-dd").Substring(0, 10) + " 00:00:00 AM", query.createTo?.ToString("yyyy-MM-dd").Substring(0, 10) + " 11:59:59 PM");

                if (query.createFrom != null && query.createTo == null)
                    sql += string.Format(" and CreatedOn >= '{0}'", query.createFrom?.ToString("yyyy-MM-dd").Substring(0, 10) + " 00:00:00 AM");

                if (query.createFrom == null && query.createTo != null)
                    sql += string.Format(" and CreatedOn <= '{0}'", query.createTo?.ToString("yyyy-MM-dd").Substring(0, 10) + " 11:59:59 PM");
            }
            return sql;
        }

        private ReportUploadCiscoDataModel.Item GetReportUploadCiscoDataModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportUploadCiscoDataModel.Item data = new ReportUploadCiscoDataModel.Item
            {
                ProductSerialNo = DBConvert.DB2String(dr["ProductSerialNo"]),
                Model = DBConvert.DB2String(dr["Model"]),
                CustomerPN = DBConvert.DB2String(dr["CustomerPN"]),
                Revision = DBConvert.DB2String(dr["Revision"]),
                DeliveryNote = DBConvert.DB2String(dr["DeliveryNote"]),
                CustomerPO = DBConvert.DB2String(dr["CustomerPO"]),
                Firmware = DBConvert.DB2String(dr["Firmware"]),
                CreatedOn = DBConvert.DB2Datetime(dr["CreatedOn"]),
                CreatedBy = DBConvert.DB2String(dr["CreatedBy"])
            };
            return data;
        }



        #endregion

        #endregion
    }
}
