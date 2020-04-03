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
    public class eJITFrequencyDAL : DALBase, IeJITFrequencyDAL
    {                                                    
        #region corts

        public eJITFrequencyDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public eJITFrequencyDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields

        private const string sql_eJITFrequency_select = @"select row_number() over(order by a.vendor_name) ITEM, a.*
                      from(select distinct VENDOR_NAME , VENDOR_SITE , len(frequency) PULL_FREQ,LEAD_TIME TRANSIT_LT, FREQUENCY ETA_FREQ from T_eJITDelivery ) a  ";


        #endregion

        #region methods

        #region methods GeteJITFrequencyDatas
        /// <summary>
        /// 获取 eJITFrequency Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReporteJITFrequencyModel GeteJITFrequencyData(ReporteJITFrequencyModelQuery query)
        {
            ReporteJITFrequencyModel result = new ReporteJITFrequencyModel();
            result.Data = new List<ReporteJITFrequencyModel.Item>();
            string sql = sql_eJITFrequency_select;

            //#region Conditions
            //sql += GetReportSqlCondition(query);
            //#endregion

            string ordercol = " ITEM";
            PageSql pSql = GetPagerSQL(query, sql, "", ordercol);

            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReporteJITFrequencyModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }


        /// <summary>
        /// 获取eJITFrequency row count Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long eJITFrequencyDataGetRowCount(ReporteJITFrequencyModelQuery query)
        {
            string sql = sql_eJITFrequency_select;

            //#region Conditions
            //sql += GetReportSqlCondition(query);
            //#endregion

            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        private string GetReportSqlCondition(ReporteJITFrequencyModelQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
            }
            return sql;
        }

        private ReporteJITFrequencyModel.Item GetReporteJITFrequencyModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReporteJITFrequencyModel.Item data = new ReporteJITFrequencyModel.Item

            {
                ITEM = DBConvert.DB2String(dr["ITEM"]),
                VENDOR_NAME = DBConvert.DB2String(dr["VENDOR_NAME"]),
                VENDOR_SITE = DBConvert.DB2String(dr["VENDOR_SITE"]),
                PULL_FREQ = DBConvert.DB2String(dr["PULL_FREQ"]),
                TRANSIT_LT = DBConvert.DB2Decimal(dr["TRANSIT_LT"]),
                ETA_FREQ = DBConvert.DB2String(dr["ETA_FREQ"]),
            };
            return data;
        }



        #endregion

        #endregion
    }
}
