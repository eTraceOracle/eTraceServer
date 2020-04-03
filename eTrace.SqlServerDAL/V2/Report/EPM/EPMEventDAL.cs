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
    public class EPMEventDAL : DALBase, IEPMEventDAL
    {                                                    
        #region corts

        public EPMEventDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public EPMEventDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields


        private const string sql_SMEPMEventDatas_select = @" SELECT [EventID] ,[Category] ,[Desc] ,[CreatedOn] ,[CreatedBy] ,[Remarks] FROM [dbo].[T_SMEventHeader] with(nolock) where 1=1 ";

        private const string sql_SMEPMEventPMItemDatas_select = @"SELECT TOP 1000 [EventID] ,[Item] ,[Value] ,[Lookup] ,[ErrMessage] FROM [dbo].[T_SMEventItem] with(nolock) where 1=1 ";

        private const string sql_GetCategory_select = @"select distinct EventCategory from T_SMEventDict with(nolock) ";


        #endregion

        #region methods


        #region methods GetSMEPMEventDatas - Equipment /Fixture PM Plan Data 
        /// <summary>
        /// GetSMEPMEventDatas
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportEPMEventModel GetSMEPMEventDatas(ReportEPMEventModelQuery query)
        {
            ReportEPMEventModel result = new ReportEPMEventModel();
            result.Data = new List<ReportEPMEventModel.Item>();
            string sql = sql_SMEPMEventDatas_select;
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
                result.Data.Add(GetReportEPMEventModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        public ReportEPMEventItemModel GetSMEPMEventItemDatas(ReportEPMEventModelQuery query)
        {
            ReportEPMEventItemModel result = new ReportEPMEventItemModel();
            result.Data = new List<ReportEPMEventItemModel.EventItem>();
            string sql = sql_SMEPMEventPMItemDatas_select;
            #region Conditions
            sql += GetReportEquipmentSqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            pSql.SQLDatas = sql;
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportEPMEventItemModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        /// <summary>
        /// EPMEventDataGetRowCount
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long EPMEventDataGetRowCount(ReportEPMEventModelQuery query)
        {
            string sql = sql_SMEPMEventDatas_select;
            #region Conditions
            sql += GetReportEquipmentSqlCondition(query);
            #endregion
            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        private string GetReportEquipmentSqlCondition(ReportEPMEventModelQuery query)
        {
            string sql = string.Empty;
            if (query != null) 
            {
                if (!string.IsNullOrEmpty(query.EventID))
                {
                    sql += string.Format(" and EventID='{0}'", query.EventID);
                }
                if (!string.IsNullOrEmpty(query.Category))
                {
                    sql += string.Format(" and Category='{0}'", query.Category);
                }
                if (!string.IsNullOrEmpty(query.Desc))
                {
                    sql += string.Format(" and Desc like N'%{0}%'", query.Desc);
                }
            }
            return sql;
        }

        private ReportEPMEventModel.Item GetReportEPMEventModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportEPMEventModel.Item data = new ReportEPMEventModel.Item
            {
                EventID = DBConvert.DB2String(dr["EventID"]),
                Category = DBConvert.DB2String(dr["Category"]),
                Desc = DBConvert.DB2String(dr["Desc"]),
                CreatedOn = DBConvert.DB2Datetime(dr["CreatedOn"]),
                CreatedBy = DBConvert.DB2String(dr["CreatedBy"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),
            };
            return data;
        }

        private ReportEPMEventItemModel.EventItem GetReportEPMEventItemModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportEPMEventItemModel.EventItem data = new ReportEPMEventItemModel.EventItem
            {
                EventID = DBConvert.DB2String(dr["EventID"]),
                Item = DBConvert.DB2String(dr["Item"]),
                Value = DBConvert.DB2String(dr["Value"]),
                Lookup = DBConvert.DB2String(dr["Lookup"]),
                ErrMessage = DBConvert.DB2String(dr["ErrMessage"]),
            };
            return data;
        }

        public List<string> GetCategory()
        {
            string sql = sql_GetCategory_select;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Add(DBConvert.DB2String(dr["EventCategory"]));
            });

            return result;
        }

        #endregion

        #endregion
    }
}
