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
    public class WIPUnitByDJDAL : DALBase, IWIPUnitByDJDAL
    {
        #region corts

        public WIPUnitByDJDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public WIPUnitByDJDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields

        private const string sql_WIPUnitByDJ_DJ_select = "SELECT DJ FROM T_WIPHEADER WITH (nolock) where 1=1 ";              

        //private const string sql_tdHeader_select = @"select * from T_TDHeader with(nolock) where 1=1 ";

        //private const string sql_WIPUnitByDJ_select = @"SELECT EquipmentID,Category, SubCategory, Description, Model, Spec, 
        //                                                    CONVERT(varchar(10),MfrDate,120) as MfrDate, CurrProdLine, FixedAssessID, Manufacturer, 
        //                                                    ManufacturerSN, CONVERT(varchar(10),AcqDate,120) as AcqDate, Department, SeqOnLine, Owner, 
        //                                                    Status,ChangedOn,ChangedBy,Remarks FROM T_SMEquipment a  WITH (nolock) where 1=1 ";

        private const string sql_WIPUnitByDJ_select1 = @"SELECT * FROM  (SELECT T_PO_CLID.PDTO as clid1,T_PO_CLID.PDTO, T_PO_CLID.PO, T_PO_CLID.CLIDQty, T_PO_CLID.IssueDate, T_PO_CLID.ReturnDate, 
          V_CLMaster.* FROM V_PO_CLID as T_PO_CLID with(nolock) INNER JOIN V_CLMaster with(nolock) ON T_PO_CLID.CLID = V_CLMaster.CLID INNER JOIN V_PDTO_PO AS T_PDTO_PO with(nolock) ON T_PO_CLID.PDTO = T_PDTO_PO.PDTO WHERE T_PDTO_PO.PO IN  ('";
					
        private const string sql_WIPUnitByDJ_select2 = @" UNION ALL SELECT T_PO_CLID_1.PDTO as clid1,T_PO_CLID_1.PDTO, T_PO_CLID_1.PO, T_PO_CLID_1.CLIDQty, T_PO_CLID_1.IssueDate, T_PO_CLID_1.ReturnDate, 
                                                    V_CLMaster_1.* FROM V_PO_CLID AS T_PO_CLID_1 with(nolock) INNER JOIN   V_CLMaster AS V_CLMaster_1 with(nolock) ON T_PO_CLID_1.CLID = V_CLMaster_1.CLID WHERE T_PO_CLID_1.PO IN ('";

        private const string sql_WIPUnitByDJ_select3 = " results ORDER BY results.MaterialNo, results.CLID";


        #endregion

        #region methods


        #region methods WIPUnitByDJData- 设备数据

        /// <summary>
        /// 获取DJ信息
        /// </summary>
        /// <returns></returns>
        public List<string> GetDJ(string IntSN)
        {
            string sql = sql_WIPUnitByDJ_DJ_select;
            sql += string.Format(" and IntSN='{0}'", IntSN);

            List<string> result = new List<string>();
            dbHelper.ExecuteReader(sql, (dr) =>
            {
                result.Add(DBConvert.DB2String(dr["DJ"]));
            });

            return result;
        }

        private string GetAllPos(string PO)
        {
            string resutl = "";

            // GetAllPos = ""

            string POTemp;
            SqlConnection thisConnection = new SqlConnection(dbHelper.ConnectionString);

            string sql = "";
            try
            {
                thisConnection.Open();

                SqlDataAdapter thisAdapter = new SqlDataAdapter(sql, thisConnection);

                SqlCommandBuilder cb = new SqlCommandBuilder(thisAdapter);

                DataSet ds = new DataSet();

                // sql = "SELECT distinct V_CLMaster.purordno FROM T_PO_CLID INNER JOIN V_CLMaster ON T_PO_CLID.CLID = V_CLMaster.CLID WHERE T_PO_CLID.PO = '" & PO & "' and T_PO_CLID.clid like 'FYP%'"

                sql = "exec sp_GetPOListRpt '" + PO + "'";

                thisAdapter = new SqlDataAdapter(sql, thisConnection);

                thisAdapter.Fill(ds, "POs");
                thisConnection.Close();

                foreach (DataRow dbRow in ds.Tables["POs"].Rows)
                {
                    // GetAllPos = GetAllPos & "///" & dbRow("purordno").ToString()
                    resutl = resutl + "','" + dbRow["purordno"].ToString();
                    POTemp = GetAllPos(dbRow["purordno"].ToString());
                    if (POTemp != "")
                        // GetAllPos = GetAllPos & "///" & GetAllPos
                        resutl = resutl + "','" + POTemp;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (thisConnection.State == ConnectionState.Open)
                    thisConnection.Close();
            }

            return resutl;
        }


        public ReportWIPUnitByDJModel GetWIPUnitByDJData(ReportWIPUnitByDJQuery query)
        {
            ReportWIPUnitByDJModel result = new ReportWIPUnitByDJModel();
            result.Data = new List<ReportWIPUnitByDJModel.Item>();           

            #region Conditions
            string dj = GetDJ(query.IntSN)[0];
            string POList;
            //POList = @"'" + dj + GetAllPos(dj) + "'";
            POList = @dj + GetAllPos(dj) ;
            string sql;
            sql = sql_WIPUnitByDJ_select1;
            sql += POList;
            sql += "')";
            sql += sql_WIPUnitByDJ_select2;
            sql += POList;
            sql += "') AND T_PO_CLID_1.PDTO IS NULL  ) results  ";

            //sql += GetReportWIPUnitByDJSqlCondition(query);

            #endregion
            string ordercol = " results.MaterialNo, results.CLID";
            PageSql pSql = GetPagerSQL(query, sql, "", ordercol);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportWIPUnitByDJModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }
        public long WIPUnitByDJDataGetRowCount(ReportWIPUnitByDJQuery query)
        {
            //string sql = sql_WIPUnitByDJ_select;
            //#region Conditions
            //sql += GetReportWIPUnitByDJSqlCondition(query);
            //#endregion

            #region Conditions

            string dj = GetDJ(query.IntSN)[0];
            string POList;
            //POList = @"'" + dj + GetAllPos(dj) + "'";
            POList = @dj + GetAllPos(dj);
            string sql;
            sql = sql_WIPUnitByDJ_select1;
            sql += POList;
            sql += "')";
            sql += sql_WIPUnitByDJ_select2;
            sql += POList;
            sql += "') AND T_PO_CLID_1.PDTO IS NULL  ) results  ";

            #endregion
            string countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }
        public void GetWIPUnitByDJAction(ReportWIPUnitByDJQuery query, Action<ReportWIPUnitByDJModel.Item> action, Action actionEnd)
        {
            ReportWIPUnitByDJModel result = new ReportWIPUnitByDJModel();
            result.Data = new List<ReportWIPUnitByDJModel.Item>();

            //string sql = sql_WIPUnitByDJ_select;
            //#region Conditions
            //sql += GetReportWIPUnitByDJSqlCondition(query);
            //#endregion

            #region Conditions

            string dj = GetDJ(query.IntSN)[0];
            string POList;
            //POList = @"'" + dj + GetAllPos(dj) + "'";
            POList = @dj + GetAllPos(dj);
            string sql;
            sql = sql_WIPUnitByDJ_select1;
            sql += POList;
            sql += "')";
            sql += sql_WIPUnitByDJ_select2;
            sql += POList;
            sql += "') AND T_PO_CLID_1.PDTO IS NULL  ) results  ";

            #endregion

            string ordercol = " results.MaterialNo, results.CLID";
            PageSql pSql = GetPagerSQL(query, sql,"",ordercol);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                if (action != null)
                {
                    action(GetReportWIPUnitByDJModel(dr));
                }
            });
            if (actionEnd != null)
            {
                actionEnd();
            }
        }

        private string GetReportWIPUnitByDJSqlCondition(ReportWIPUnitByDJQuery query)
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

        private ReportWIPUnitByDJModel.Item GetReportWIPUnitByDJModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportWIPUnitByDJModel.Item data = new ReportWIPUnitByDJModel.Item
            {
                MaterialNo = DBConvert.DB2String(dr["MaterialNo"]),
                MaterialDesc = DBConvert.DB2String(dr["MaterialDesc"]),
                MaterialRevision = DBConvert.DB2String(dr["MaterialRevision"]),
                CLIDQty = DBConvert.DB2Decimal(dr["CLIDQty"]),
                BaseUOM = DBConvert.DB2String(dr["BaseUOM"]),
                CLID = DBConvert.DB2String(dr["CLID"]),
                IssueDate = DBConvert.DB2Datetime(dr["IssueDate"]),
                DateCode = DBConvert.DB2String(dr["DateCode"]),
                LotNo = DBConvert.DB2String(dr["LotNo"]),
                StatusCode = DBConvert.DB2String(dr["StatusCode"]),
                ExpDate = DBConvert.DB2Datetime(dr["ExpDate"]),
                RecDocNo = DBConvert.DB2String(dr["RecDocNo"]),
                RecDate = DBConvert.DB2Datetime(dr["RecDate"]),
                ProcessID = DBConvert.DB2String(dr["ProcessID"]),
                RoHS = DBConvert.DB2String(dr["RoHS"]),
                PurOrdNo = DBConvert.DB2String(dr["PurOrdNo"]),
                PurOrdItem = DBConvert.DB2String(dr["PurOrdItem"]),
                VendorID = DBConvert.DB2String(dr["VendorID"]),
                VendorName = DBConvert.DB2String(dr["VendorName"]),
                VendorPN = DBConvert.DB2String(dr["VendorPN"]),
                BillofLading = DBConvert.DB2String(dr["BillofLading"]),
                DN = DBConvert.DB2String(dr["DN"]),
                HeaderText = DBConvert.DB2String(dr["HeaderText"]),
                ProdDate = DBConvert.DB2Datetime(dr["ProdDate"]),
                ReasonCode = DBConvert.DB2String(dr["ReasonCode"]),
                SLOC = DBConvert.DB2String(dr["SLOC"]),
                StorageBin = DBConvert.DB2String(dr["StorageBin"]),
                Operator = DBConvert.DB2String(dr["Operator"]),
                StockType = DBConvert.DB2String(dr["StockType"]),
                ItemText = DBConvert.DB2String(dr["ItemText"]),
                MatSuffix1 = DBConvert.DB2String(dr["MatSuffix1"]),
                MatSuffix2 = DBConvert.DB2String(dr["MatSuffix2"]),
                MatSuffix3 = DBConvert.DB2String(dr["MatSuffix3"]),
                Printed = DBConvert.DB2String(dr["Printed"]),

                IsTraceable = DBConvert.DB2String(dr["IsTraceable"]),
                AddlText = DBConvert.DB2String(dr["AddlText"]),
                ReferenceCLID = DBConvert.DB2String(dr["ReferenceCLID"]),
                BoxID = DBConvert.DB2String(dr["BoxID"]),
                Manufacturer = DBConvert.DB2String(dr["Manufacturer"]),
                ManufacturerPN = DBConvert.DB2String(dr["ManufacturerPN"]),
                QMLStatus = DBConvert.DB2String(dr["QMLStatus"]),
                NextReviewDate = DBConvert.DB2Datetime(dr["NextReviewDate"]),
                ReviewStatus = DBConvert.DB2String(dr["ReviewStatus"]),
                ReviewedOn = DBConvert.DB2Datetime(dr["ReviewedOn"]),
                ReviewedBy = DBConvert.DB2String(dr["ReviewedBy"]),
                SampleSize = DBConvert.DB2Int(dr["SampleSize"]),
                CreatedOn = DBConvert.DB2Datetime(dr["CreatedOn"]),
                CreatedBy = DBConvert.DB2String(dr["CreatedBy"]),
                ChangedOn = DBConvert.DB2Datetime(dr["ChangedOn"]),
                ChangedBy = DBConvert.DB2String(dr["ChangedBy"])
            };
            return data;
        }      



        #endregion

        #endregion
    }
}
