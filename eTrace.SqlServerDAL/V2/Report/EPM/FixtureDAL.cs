using eTrace.Common;
using eTrace.Report.IDAL;
using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.SqlServerDAL.V2.Report
{
    public class FixtureDAL : DALBase, IFixtureDAL
    {
         #region corts

        public FixtureDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public FixtureDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields
        private const string sql_fixture_select = @" select SysFixtureID,Category,SubCategory,FixtureID,BatchID,Description,Spec,ItemNo,Status,StorageLocation,
                                                    CurrentJob,CurrentUseCount,TotalUseCount,ChangedOn,ChangedBy,PO,Vendor,OrderDate,UnitPrice,
                                                    Currency,RecDate,DeliveryNote,InvoiceNo,OrderReason,ProdReceiver,ProdRecDate,Remarks,
                                                    Dept,FileName,Attachment,Owner,Model,CurrProdLine,Rev from T_SMFixture
                                                    with (nolock) where 1=1 ";

        private const string sql_fixture_status_select = "select distinct status from  T_SMFixture with (nolock)";

        #endregion
        /// <summary>
        /// 获取工装数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportFixtureModel GetFixtures(ReportFixtureQuery query)
        {
            ReportFixtureModel result = new ReportFixtureModel();
            result.Data = new List<ReportFixtureModel.Item>();
            string sql = sql_fixture_select;
            sql += GetFixtureCondition(query);
            PageSql pSql = GetPagerSQL(query, sql);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportSMFixturetDataModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });

            return result;
        }

        private string GetFixtureCondition(ReportFixtureQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.FixtureID))
                {
                    sql += string.Format(" and FixtureID='{0}'", query.FixtureID);
                }
                if (!string.IsNullOrEmpty(query.ItemNO))
                {
                    sql += string.Format(" and ItemNo=N'{0}'", query.ItemNO);
                }
                if (!string.IsNullOrEmpty(query.Category))
                {
                    sql += string.Format(" and Category='{0}'", query.Category);
                }
                if (!string.IsNullOrEmpty(query.SubCategory))
                {
                    sql += string.Format(" and SubCategory=N'{0}'", query.SubCategory);
                }
                if (!string.IsNullOrEmpty(query.BatchID))
                {
                    sql += string.Format(" and BatchID='{0}'", query.BatchID);
                }
                if (query.Status!=null&&query.Status.Count != 0)
                {
                    sql += " and Status in (";
                    for (int i = 0; i < query.Status.Count; i++)
                    {
                        sql += "'" + query.Status[i] + "',";
                    }
                    sql=sql.Substring(0,sql.Length-1);
                    sql+=")";
                }
                if (!string.IsNullOrEmpty(query.Owner))
                {
                    sql += string.Format(" and Owner='{0}'", query.Owner);
                }
                if (!string.IsNullOrEmpty(query.Description))
                {
                    sql += string.Format(" and Description like N'%{0}%'", query.Description);
                }
                if (query.LastUseFrom.HasValue)
                {
                    sql += string.Format(" and LastUse>='{0}'", query.LastUseFrom.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (query.LastUseTo.HasValue)
                {
                    sql += string.Format(" and LastUse<'{0}'", query.LastUseTo.Value.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (query.LastReturnFrom.HasValue)
                {
                    sql += string.Format(" and LastReturn>='{0}'", query.LastReturnFrom.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (query.LastReturnTo.HasValue)
                {
                    sql += string.Format(" and LastReturn<'{0}'", query.LastReturnTo.Value.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (query.LastInspectFrom.HasValue)
                {
                    sql += string.Format(" and LastInspect>='{0}'", query.LastInspectFrom.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (query.LastInspectTo.HasValue)
                {
                    sql += string.Format(" and LastInspect<'{0}'", query.LastInspectTo.Value.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (!string.IsNullOrEmpty(query.Dept))
                {
                    sql += string.Format(" and Dept='{0}'", query.Dept);
                }
                if (!string.IsNullOrEmpty(query.CurrProdLine))
                {
                    sql += string.Format(" and CurrProdLine='{0}'", query.CurrProdLine);
                }
            }
            return sql;
        }

        private ReportFixtureModel.Item GetReportSMFixturetDataModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportFixtureModel.Item data = new ReportFixtureModel.Item() 
            {
                SysFixtureID = DBConvert.DB2String(dr["SysFixtureID"]),
                Category = DBConvert.DB2String(dr["Category"]),
                SubCategory = DBConvert.DB2String(dr["SubCategory"]),
                FixtureID = DBConvert.DB2String(dr["FixtureID"]),
                BatchID = DBConvert.DB2String(dr["BatchID"]),
                Description = DBConvert.DB2String(dr["Description"]),
                Spec = DBConvert.DB2String(dr["Spec"]),
                ItemNo = DBConvert.DB2String(dr["ItemNo"]),
                Status = DBConvert.DB2String(dr["Status"]),
                Location = DBConvert.DB2String(dr["StorageLocation"]),
                CurrentJob = DBConvert.DB2String(dr["CurrentJob"]),
                CurrentCount = DBConvert.DB2Int(dr["CurrentUseCount"]),
                TotalCount = DBConvert.DB2Int(dr["TotalUseCount"]),
                PO = DBConvert.DB2String(dr["PO"]),
                Vendor = DBConvert.DB2String(dr["Vendor"]),
                OrderDate = DBConvert.DB2Datetime(dr["OrderDate"]),
                UnitPrice = DBConvert.DB2Decimal(dr["UnitPrice"]),
                Currency = DBConvert.DB2String(dr["Currency"]),
                RecDate = DBConvert.DB2String(dr["RecDate"]),
                DeliveryNote = DBConvert.DB2String(dr["DeliveryNote"]),
                InvoiceNo = DBConvert.DB2String(dr["InvoiceNo"]),
                OrderReason = DBConvert.DB2String(dr["OrderReason"]),
                ProdReceiver = DBConvert.DB2String(dr["ProdReceiver"]),
                ProdRecDate = DBConvert.DB2Datetime(dr["ProdRecDate"]),               
                Dept = DBConvert.DB2String(dr["Dept"]),
                FileName = DBConvert.DB2String(dr["FileName"]),
                Attachment = DBConvert.DB2String(dr["Attachment"]),
                Owner = DBConvert.DB2String(dr["Owner"]),
                Model = DBConvert.DB2String(dr["Model"]),
                CurrProdLine = DBConvert.DB2String(dr["CurrProdLine"]),
                Rev = DBConvert.DB2String(dr["Rev"]),
                ChangedOn = DBConvert.DB2Datetime(dr["ChangedOn"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"]),
                Remarks = DBConvert.DB2String(dr["Remarks"]),

            };
            return data;
        }

        public long FixtureDataGetRowCount(ReportFixtureQuery query)
        {
            string sql = sql_fixture_select;
            #region Conditions
            sql += GetFixtureCondition(query);
            #endregion
            string countSql = GetSQLCount(sql);
            long rowCount = dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        /// <summary>
        /// 获取工装状态
        /// </summary>
        /// <returns></returns>
        public List<string> GeFixtureStatus()
        {
            string sql = sql_fixture_status_select;
            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Add(DBConvert.DB2String(dr["status"]));
            });

            return result;
        }
    }
}
