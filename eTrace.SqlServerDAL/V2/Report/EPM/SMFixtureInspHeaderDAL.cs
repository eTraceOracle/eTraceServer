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
    public class SMFixtureInspHeaderDAL : DALBase, ISMFixtureInspHeaderDAL
    {                                                    
        #region corts

        public SMFixtureInspHeaderDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public SMFixtureInspHeaderDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields


        private const string sql_SMFixtureInsp_Header_Datas_select = @" SELECT DISTINCT A.InspID, A.InspType, A.FixtureID, B.ItemNo, A.InspSpecID, dateadd(hour,8,A.InspectedOn) as InspectedOn, A.InspectedBy, A.InspResult, dateadd(hour,8,A.ChangedOn) as ChangedOn, A.ChangedBy, A.Remarks, B.BatchID, C.Store
                                                                    FROM T_SMFixtureInspHeader AS A WITH (nolock) 
                                                                    INNER JOIN T_SMFixture AS B WITH(NOLOCK) ON A.FixtureID = B.FixtureID 
                                                                    Left outer join T_SMLoc as C WITH(NOLOCK) ON C.LocID = B.StorageLocation
                                                                    WHERE 1=1  ";

        private const string sql_SMFixtureInsp_Item_Datas_select = @" SELECT InspID, InspItem, InspCondition, LowerLimit, UpperLimit, InspValue, Unit, InspResult, Remarks
                                                                            FROM T_SMFixtureInspItem WITH (nolock) WHERE 1=1 ";


        #endregion

        #region methods


        #region methods GetSMFixtureInspHeaderDatas 
        /// <summary>
        /// GetSMFixtureInspHeaderDatas
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportSMFixtureInspHeaderModel GetSMFixtureInspHeaderDatas(ReportSMFixtureInspHeaderModelQuery query)
        {
            ReportSMFixtureInspHeaderModel result = new ReportSMFixtureInspHeaderModel();
            result.Data = new List<ReportSMFixtureInspHeaderModel.Item>();
            string sql = sql_SMFixtureInsp_Header_Datas_select;
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
                result.Data.Add(GetReportSMFixtureInspHeaderModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        public ReportSMFixtureInspItemModel GetSMFixtureInspItemDatas(ReportSMFixtureInspHeaderModelQuery query)
        {
            ReportSMFixtureInspItemModel result = new ReportSMFixtureInspItemModel();
            result.Data = new List<ReportSMFixtureInspItemModel.Item>();
            string sql = sql_SMFixtureInsp_Item_Datas_select;
            #region Conditions
            sql += GetReportSMFixtureInspItemSqlCondition(query);
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
                result.Data.Add(GetReportSMFixtureInspItemModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        /// <summary>
        /// SMFixtureInspHeaderDataGetRowCount
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public long SMFixtureInspHeaderDataGetRowCount(ReportSMFixtureInspHeaderModelQuery query)
        {
            string sql = sql_SMFixtureInsp_Header_Datas_select;
            #region Conditions
            sql += GetReportEquipmentSqlCondition(query);
            #endregion
            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        private string GetReportEquipmentSqlCondition(ReportSMFixtureInspHeaderModelQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.InspID))
                {
                    sql += string.Format(" and InspID='{0}'", query.InspID);
                }
                if (!string.IsNullOrEmpty(query.InspType))
                {
                    sql += string.Format(" and A.InspType='{0}'", query.InspType);
                }
                if (!string.IsNullOrEmpty(query.InspSpecID))
                {
                    sql += string.Format(" and A.InspSpecID='{0}'", query.InspSpecID);
                }
                if (!string.IsNullOrEmpty(query.FixtureID))
                {
                    sql += string.Format(" and A.FixtureID='{0}'", query.FixtureID);
                }
                if (!string.IsNullOrEmpty(query.ItemNO))
                {
                    sql += string.Format(" and b.ItemNO='{0}'", query.ItemNO);
                }
                if (!string.IsNullOrEmpty(query.BatchID))
                {
                    sql += string.Format(" and B.BatchID='{0}'", query.BatchID);
                }
                if (!string.IsNullOrEmpty(query.Store))
                {
                    sql += string.Format(" and c.Store='{0}'", query.Store);
                }
                if (!string.IsNullOrEmpty(query.Owner))
                {
                    sql += string.Format(" and b.Owner='{0}'", query.Owner);
                }
                if (!query.InspectedFrom.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), A.InspectedOn, 23)>='{0}'", query.InspectedFrom.ToString("yyyy-MM-dd"));
                }
                if (!query.InspectedTo.ToString().Equals("1/1/0001 12:00:00 AM"))
                {
                    sql += string.Format(" and CONVERT(varchar(100), A.InspectedOn, 23)<='{0}'", query.InspectedTo.ToString("yyyy-MM-dd"));
                }
            }
            return sql;
        }

        private string GetReportSMFixtureInspItemSqlCondition(ReportSMFixtureInspHeaderModelQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.InspID))
                {
                    sql += string.Format(" and InspID='{0}'", query.InspID);
                }
            }
            return sql;
        }

        private ReportSMFixtureInspHeaderModel.Item GetReportSMFixtureInspHeaderModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMFixtureInspHeaderModel.Item data = new ReportSMFixtureInspHeaderModel.Item
            {

                InspID = DBConvert.DB2String(dr["InspID"]),
                InspType = DBConvert.DB2String(dr["InspType"]),
                FixtureID = DBConvert.DB2String(dr["FixtureID"]),
                ItemNo = DBConvert.DB2String(dr["ItemNo"]),
                InspSpecID = DBConvert.DB2String(dr["InspSpecID"]),
                InspectedOn = DBConvert.DB2Datetime(dr["InspectedOn"]),
                InspectedBy = DBConvert.DB2String(dr["InspectedBy"]),
                InspResult = DBConvert.DB2String(dr["InspResult"]),
                BatchID = DBConvert.DB2String(dr["BatchID"]),
                Store = DBConvert.DB2String(dr["Store"]),
                ChangedOn = DBConvert.DB2Datetime(dr["ChangedOn"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),
            };
            return data;
        }

        private ReportSMFixtureInspItemModel.Item GetReportSMFixtureInspItemModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportSMFixtureInspItemModel.Item data = new ReportSMFixtureInspItemModel.Item
            {
                InspID = DBConvert.DB2String(dr["InspID"]),
                InspItem = DBConvert.DB2String(dr["InspItem"]),
                InspCondition = DBConvert.DB2String(dr["InspCondition"]),
                LowerLimit = DBConvert.DB2String(dr["LowerLimit"]),
                UpperLimit = DBConvert.DB2String(dr["UpperLimit"]),
                InspValue = DBConvert.DB2String(dr["InspValue"]),
                Unit = DBConvert.DB2String(dr["Unit"]),
                InspResult = DBConvert.DB2String(dr["InspResult"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),
            };
            return data;
        }

        #endregion

        #endregion
    }
}
