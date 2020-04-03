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
using System.IO;

namespace eTrace.Report.SqlServerDAL.V2.Report
{
    public class ProductArchiveDAL : DALBase, IProductArvhiveDAL 
    {
        #region corts

        public ProductArchiveDAL(DBHelper dbHelper)
            : base(dbHelper)
        {
        }
        public ProductArchiveDAL(EmDBType dbType)
            : base(dbType)
        {
        }
        #endregion

        #region sql fields

        private const string sql_tdHeader_select = @"select * from T_TDHeader with(nolock) where 1=1 ";
        //private const string sql_product_testdataSummary_select = @"SELECT ExtSerialNo, CartonID, IntSerialNo, ProcessName, 
        //                                            SeqNo, PO, T_TDHeader.Model, PCBA, ProdDate, WIPIn, Result, OperatorName, 
        //                                TesterNo, ProgramName, ProgramRevision, IPSNo, IPSRevision, Remark FROM T_TDHeader WITH (nolock) 
        //                                inner join T_Shippment with (nolock) on T_TDHeader.ExtSerialNo = T_Shippment.ProductSerialNo where 1=1 ";


        private const string sql_temp_table = @"insert into #productTestDataDownload(
		[seqNO],
		[InputCondition] ,
		[Processname],
		[TDID],
		[TestStep],
		[TestID],
		[testName],
		[Datetime],
		[Result],
		[Lowlimit],
		[Highlimit],
		[Unit],
		[SystemNo],
		[ExtSerialNo],
		[status]) 
	SELECT V_TDHeader.seqno,V_TDItem.InputCondition,V_TDHeader.processname,V_TDItem.TDID,V_TDItem.TestStep,V_TDItem.TestID,testName,V_TDHeader.ProdDate AS Datetime, V_TDItem.Result, V_TDItem.LowerLimit AS Lowlimit, V_TDItem.UpperLimit AS Highlimit, 
	V_TDItem.Unit, V_TDHeader.TesterNo AS SystemNo, V_TDHeader.ExtSerialNo ,status 
	FROM V_TDHeader WITH (nolock) INNER JOIN V_TDItem WITH (nolock) ON V_TDHeader.TDID = V_TDItem.TDID  WHERE 1=1 {0} order by V_TDHeader.ExtSerialNo,V_TDHeader.Proddate, V_TDHeader.seqNO, CONVERT(int, V_TDItem.TestStep) ,V_TDItem.testID";

        private const string sql_product_testdata_select = @"SELECT  a.ExtSerialNo,a.ProcessName,a.SeqNo,
b.TestStep as TestStep,b.TestID,b.TestName,b.InputCondition,a.ProdDate as DateTime,
b.LowerLimit as LowLimit,b.[Result],b.UpperLimit as Highlimit,b.Unit,a.TesterNo as SystemNo,b.Status from T_TDHeader a with(nolock)
INNER JOIN T_TDItem b with(nolock)
on a.TDID=b.TDID 
INNER JOIN T_Shippment c with(nolock)
on a.ExtSerialNo=c.ProductSerialNo
where 1=1 "; 

        private const string sql_product_data_select = @"SELECT ProductSerialNo, PalletID,CartonID,SaleOrder,DeliveryNote,ProdOrder,
     Model, CustomerPN, CustomerRev, SerialNo2,SerialNo3,SerialNo4,FlatFile,
     SentBy, ProductionLine,CreatedOn, CreatedBy, ChangedOn, ChangedBy ,TVA ,
     0 as DJSize, '''' as preSN
     FROM V_Shippment as a with(nolock)  where 1=1 ";
        private const string sql_product_data_select_PreSN = @"SELECT ProductSerialNo, PalletID,CartonID,SaleOrder,DeliveryNote,ProdOrder,
     Model, CustomerPN, CustomerRev, SerialNo2,SerialNo3,SerialNo4,FlatFile,
     SentBy, ProductionLine,CreatedOn, CreatedBy, ChangedOn, ChangedBy ,TVA ,
     0 as DJSize, '''' as preSN
     FROM V_Shippment as a with(nolock)  where 1=1 ";

        #endregion

        #region methods
        #region methods TDHeader- 产品工序数据

        /// <summary>
        /// TDHeader数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ReportTDHeaderModel GetTDHeaders(ReportTDHeaderQuery query)
        {
            ReportTDHeaderModel result = new ReportTDHeaderModel();
            result.Data = new List<ReportTDHeaderModel.Item>();
            string sql = sql_tdHeader_select;
            #region Conditions
            sql += GetTDHeaderSqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetTDHeaderModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            return result;
        }

        private string GetTDHeaderSqlCondition(ReportTDHeaderQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.ExtserialNo))
                {
                    sql += string.Format(" and ExtSerialNo='{0}'", query.ExtserialNo);
                }
                if (!string.IsNullOrEmpty(query.IntSerialNo))
                {
                    sql += string.Format(" and IntSerialNo='{0}'", query.IntSerialNo);
                }
            }
            return sql;
        }

        private ReportTDHeaderModel.Item GetTDHeaderModel(System.Data.SqlClient.SqlDataReader dr)
        {
            ReportTDHeaderModel.Item data = new ReportTDHeaderModel.Item
            {
                ExtSerialNo = DBConvert.DB2String(dr["ExtSerialNo"]),
                IntSerialNo = DBConvert.DB2String(dr["IntSerialNo"]),
                IPSNo = DBConvert.DB2String(dr["IPSNo"]),
                IPSRevision = DBConvert.DB2String(dr["IPSRevision"]),
                OperatorName = DBConvert.DB2String(dr["OperatorName"]),
                PO = DBConvert.DB2String(dr["PO"]),
                ProcessName = DBConvert.DB2String(dr["ProcessName"]),
                ProdDate = DBConvert.DB2Datetime(dr["ProdDate"]),
                ProgramName = DBConvert.DB2String(dr["ProgramName"]),
                ProgramRevision = DBConvert.DB2String(dr["ProgramRevision"]),
                Remark = DBConvert.DB2String(dr["Remark"]),
                TDID = DBConvert.DB2String(dr["TDID"]),
                TesterNo = DBConvert.DB2String(dr["TesterNo"]),
                WIPIn = DBConvert.DB2Datetime(dr["WIPIn"]),
                Model = DBConvert.DB2String(dr["Model"]),
                PCBA = DBConvert.DB2String(dr["PCBA"]),
                SeqNo = DBConvert.DB2Int(dr["SeqNo"]),
            };
            return data;
        }
        #endregion

        #region methods ProductTestData- 产品测试数据

        public ReportProductTestDataArchiveModel GetProductTestData(ReportProductTestDataArchiveQuery query)
        {
            ReportProductTestDataArchiveModel result = new ReportProductTestDataArchiveModel();
            result.Data = new List<ReportProductTestDataArchiveModel.Item>();
            string sql = string.Empty;
            string sqlCount = string.Empty;
            string strConditon = GetReportProductTestDataSqlCondition(query);
            string sqlInsertWithCondition = string.Format(sql_temp_table, strConditon);
            if (query.IsCustomerReport )
            {
                string sql_temp_Header = string.Format ( @"SELECT V_TDHeader.seqno,V_TDItem.InputCondition,V_TDHeader.processname,V_TDItem.TDID,V_TDItem.TestStep,V_TDItem.TestID,testName,V_TDHeader.ProdDate AS Datetime, V_TDItem.Result, V_TDItem.LowerLimit AS Lowlimit, V_TDItem.UpperLimit AS Highlimit, 
	V_TDItem.Unit, V_TDHeader.TesterNo AS SystemNo, V_TDHeader.ExtSerialNo ,status 
	FROM V_TDHeader WITH (nolock) INNER JOIN V_TDItem WITH (nolock) ON V_TDHeader.TDID = V_TDItem.TDID  WHERE 1=1 {0} "
                        ,strConditon );
                string sql_temp_process = string.Format(@"SELECT   ExtSerialNo,Processname, MAX(seqNO) AS seqno  
			                                FROM           ({0} ) as processseq
		                                GROUP BY ExtSerialNo,Processname", sql_temp_Header);
                sql = string.Format(@"select 
			        a.[Processname],
			        a.[TestStep],
			        a.[TestID],
			        a.[testName],
			        a.[Datetime],
			        a.[Result],
			        a.[Lowlimit],
			        a.[Highlimit],
			        a.[Unit],
			        a.[SystemNo],
			        a.[ExtSerialNo],
			        a.[seqNO],
			        a.[InputCondition],
			        a.[status] from ({0})  AS a CROSS JOIN ({1}) AS b
		        WHERE        (a.Processname = b.Processname) AND (a.seqNO = b.seqno) and a.ExtSerialNo=b.ExtSerialNo",
                sql_temp_Header,
                sql_temp_process);
                //order by V_TDHeader.ExtSerialNo,V_TDHeader.Proddate, V_TDHeader.seqNO, CONVERT(int, V_TDItem.TestStep) ,V_TDItem.testID
                if (query.Pager != null)
                {
                    query.Pager.Order = " ExtSerialNo, Datetime, SeqNo, CONVERT(int, TestStep), TestID";
                }
            }
            else
            {
                string sql_temp = @"SELECT     V_TDItem.TestStep,V_TDItem.TestID, V_TDItem.TestName,V_TDHeader.ProdDate AS Datetime,V_TDItem.Result,V_TDItem.LowerLimit AS Lowlimit,
					 V_TDItem.UpperLimit AS Highlimit, V_TDItem.Unit, V_TDHeader.TesterNo AS SystemNo,V_TDHeader.ExtSerialNo,V_TDItem.Status, V_TDHeader.SeqNo,
					  V_TDItem.InputCondition, V_TDHeader.ProcessName
	     
					FROM            V_TDHeader WITH (nolock) INNER JOIN
                         V_TDItem WITH (nolock) ON V_TDHeader.TDID = V_TDItem.TDID
					WHERE 1=1 {0}  ";
                if (query.Pager != null)
                {
                    query.Pager.Order = "V_TDHeader.ExtSerialNo, Datetime, V_TDHeader.SeqNo, CONVERT(int, V_TDItem.TestStep), V_TDItem.TestID";
                }
                sql = string.Format(sql_temp, strConditon); 
            } 
            #region Conditions 
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                result.Data.Add(GetReportProductTestDataModel(dr));
            }, (isover) =>
            { result.IsOverMaxRow = isover; });
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            return result;
        }
        public long ProductTestDataGetRowCount(ReportProductTestDataArchiveQuery query)
        {
            string sql = string.Empty;
            string sqlCount = string.Empty;
            string strConditon = GetReportProductTestDataSqlCondition(query);
            string sqlInsertWithCondition = string.Format(sql_temp_table, strConditon);
            if (query.IsCustomerReport)
            {
                string sql_temp_Header = string.Format(@"SELECT V_TDHeader.seqno,V_TDItem.InputCondition,V_TDHeader.processname,V_TDItem.TDID,V_TDItem.TestStep,V_TDItem.TestID,testName,V_TDHeader.ProdDate AS Datetime, V_TDItem.Result, V_TDItem.LowerLimit AS Lowlimit, V_TDItem.UpperLimit AS Highlimit, 
	V_TDItem.Unit, V_TDHeader.TesterNo AS SystemNo, V_TDHeader.ExtSerialNo ,status 
	FROM V_TDHeader WITH (nolock) INNER JOIN V_TDItem WITH (nolock) ON V_TDHeader.TDID = V_TDItem.TDID  WHERE 1=1 {0} "
                        , strConditon);
                string sql_temp_process = string.Format(@"SELECT   ExtSerialNo,Processname, MAX(seqNO) AS seqno  
			                                FROM           ({0} ) as processseq
		                                GROUP BY ExtSerialNo,Processname", sql_temp_Header);
                sql = string.Format(@"select 
			        a.[Processname],
			        a.[TestStep],
			        a.[TestID],
			        a.[testName],
			        a.[Datetime],
			        a.[Result],
			        a.[Lowlimit],
			        a.[Highlimit],
			        a.[Unit],
			        a.[SystemNo],
			        a.[ExtSerialNo],
			        a.[seqNO],
			        a.[InputCondition],
			        a.[status] from ({0})  AS a CROSS JOIN ({1}) AS b
		        WHERE        (a.Processname = b.Processname) AND (a.seqNO = b.seqno) and a.ExtSerialNo=b.ExtSerialNo",
                sql_temp_Header,
                sql_temp_process);
                //order by V_TDHeader.ExtSerialNo,V_TDHeader.Proddate, V_TDHeader.seqNO, CONVERT(int, V_TDItem.TestStep) ,V_TDItem.testID

                //query.Pager.Order = " ExtSerialNo, Datetime, SeqNo, CONVERT(int, TestStep), TestID";
            }
            else
            {
                string sql_temp = @"SELECT     V_TDItem.TestStep,V_TDItem.TestID, V_TDItem.TestName,V_TDHeader.ProdDate AS Datetime,V_TDItem.Result,V_TDItem.LowerLimit AS Lowlimit,
					 V_TDItem.UpperLimit AS Highlimit, V_TDItem.Unit, V_TDHeader.TesterNo AS SystemNo,V_TDHeader.ExtSerialNo,V_TDItem.Status, V_TDHeader.SeqNo,
					  V_TDItem.InputCondition, V_TDHeader.ProcessName
	     
					FROM            V_TDHeader WITH (nolock) INNER JOIN
                         V_TDItem WITH (nolock) ON V_TDHeader.TDID = V_TDItem.TDID
					WHERE 1=1 {0}  ";
                //query.Pager.Order = "V_TDHeader.ExtSerialNo, Datetime, V_TDHeader.SeqNo, CONVERT(int, V_TDItem.TestStep), V_TDItem.TestID";
                sql = string.Format(sql_temp, strConditon);
            } 
            #endregion
            string  countSql = GetSQLCount( sql);
            long rowCount=  dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        public long ProductDataGetRowCount(ReportProductArchiveDataQuery query)
        {
            string sql = string.Empty;

            if (query.IsTracePreViousSN )
            {
                sql = sql_product_data_select_PreSN ;
            }
            else
            {
                sql = sql_product_data_select;
            }
            #region Conditions
            sql += GetProductDataSqlCondition(query);
            #endregion
            string countSql = GetSQLCount(sql);
            long rowCount = dbHelper.GetCount(countSql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }
        public void GetProductTestDataAction(ReportProductTestDataArchiveQuery query, Action<ReportProductTestDataArchiveModel.Item> action, Action actionEnd)
        {
            ReportProductTestDataArchiveModel result = new ReportProductTestDataArchiveModel();
            result.Data = new List<ReportProductTestDataArchiveModel.Item>();
            string sql = sql_product_testdata_select;
            #region Conditions
            sql += GetReportProductTestDataSqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            {
                if (action != null)
                {
                    action(GetReportProductTestDataModel(dr));
                }
            });
            if (actionEnd != null)
            {
                actionEnd();
            }
        }

        private string GetReportProductTestDataSqlCondition(ReportProductTestDataArchiveQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.IntSN))
                {
                    sql += string.Format(" and = V_TDHeader.IntSerialNo in ('{0}')", query.IntSN.Replace(",", "','"));
                }
                if (!string.IsNullOrEmpty(query.DJ))
                {
                    sql += string.Format(" and V_TDHeader.PO='{0}'", query.DJ);
                }
                if (!string.IsNullOrEmpty(query.Model))
                {
                    sql += string.Format(" and Model='{0}'", query.Model);
                }
                if (!string.IsNullOrEmpty(query.ProductSN))
                {
                    sql += string.Format(" and V_TDHeader.ExtSerialNo in ('{0}')", query.ProductSN.Replace(",","','"));
                }
                if (!string.IsNullOrEmpty(query.Station))
                {
                    sql += string.Format(" and V_TDHeader.ProcessName='{0}'", query.Station);
                }
                if (!string.IsNullOrEmpty(query.TestId))
                {
                    sql += string.Format(" and TestId='{0}'", query.TestId);
                }
                if (!string.IsNullOrEmpty(query.TestStep))
                {
                    sql += string.Format(" and TestStep='{0}'", query.TestStep);
                }
                if (query.ProductTimeStart.HasValue)
                {
                    sql += string.Format(" and ProdDate>='{0}'", query.ProductTimeStart.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (query.ProductTimeEnd.HasValue)
                {
                    sql += string.Format(" and ProdDate<'{0}'", query.ProductTimeEnd.Value.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }
            return sql;
        }
        private string GetReportProductTestDataSummarySqlCondition(ReportProductTestDataArchiveQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.IntSN))
                {
                    sql += string.Format(" and IntSerialNo in ('{0}')", query.IntSN.Trim().Replace(",", "','"));
                }
                if (!string.IsNullOrEmpty(query.ProductSN ))
                {
                    sql += string.Format(" and ExtSerialNo in ('{0}')", query.ProductSN.Trim().Replace(",", "','"));
                }
                //if (!string.IsNullOrEmpty(query.Station ))
                //{
                //    sql += string.Format(" and ProcessName = '{0}'", query.Station.Trim());
                //}
            }
            return sql;
        }
        private ReportProductTestDataArchiveModel.Item GetReportProductTestDataModel(System.Data.SqlClient.SqlDataReader dr)
        {
            try
            {
                ReportProductTestDataArchiveModel.Item data = new ReportProductTestDataArchiveModel.Item
                {
                    ExtSerialNo = DBConvert.DB2String(dr["ExtSerialNo"]),
                    DateTime = DBConvert.DB2Datetime(dr["DateTime"]),
                    HighLimit = DBConvert.DB2Double(dr["HighLimit"]),
                    InputCondition = DBConvert.DB2String(dr["InputCondition"]),
                    LowLimit = DBConvert.DB2Double(dr["LowLimit"]),
                    ProcessName = DBConvert.DB2String(dr["ProcessName"]),
                    Result = DBConvert.DB2Double(dr["Result"]),
                    SeqNo = DBConvert.DB2Int(dr["SeqNo"]),
                    TestStep = DBConvert.DB2String(dr["TestStep"]),
                    Status = DBConvert.DB2String(dr["Status"]),
                    SystemNo = DBConvert.DB2String(dr["SystemNo"]),
                    TestId = DBConvert.DB2String(dr["TestId"]),
                    TestName = DBConvert.DB2String(dr["TestName"]),
                    Unit = DBConvert.DB2String(dr["Unit"]),
                };

                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
           
        }



  
        #endregion

        #region methods ProductData- 产品数据

        public ReportProductArchiveDataModel GetProductDatasArchive(ReportProductArchiveDataQuery query)
        {
            ReportProductArchiveDataModel result = new ReportProductArchiveDataModel();
            result.Data = new List<ReportProductArchiveDataModel.Item>();
            string sql = string.Empty;
            if (query.IsTracePreViousSN)
            {
                sql = sql_product_data_select_PreSN;
            }
            else
            {
                sql = sql_product_data_select;
            }
            #region Conditions
            sql += GetProductDataSqlCondition(query);
            #endregion
            PageSql pSql = GetPagerSQL(query, sql,"", " Model,ProductSerialNo");
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            result.Data = dbHelper.GetList<ReportProductArchiveDataModel.Item>(pSql.SQLDatas, GetProductDataModel);
  
            return result;
        }

        private string GetProductDataSqlCondition(ReportProductArchiveDataQuery query)
        {
            string sql = string.Empty;
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.ProductSN))
                {
                    //sql += string.Format(" and a.ProductSerialNo='{0}'", query.ProductSN);
                    string serialno1;
                    string serialno2;
                    if(query.ProductSN.Contains("~"))
                    {
                        serialno1 = query.ProductSN.Substring(1, query.ProductSN.LastIndexOf("~") - 1);
                        serialno2 = query.ProductSN.Substring(1, query.ProductSN.LastIndexOf("~") + 1);
                        sql += " and " + "(ProductSerialNo between '" + serialno1 + " 'and '" + serialno2 + " ')";
                    }
                    else
                    {
                        sql += " and " + "ProductSerialNo in ('" + query.ProductSN.Replace(",", "','") + "') ";
                    }

                }
                if (!string.IsNullOrEmpty(query.SaleOrder))
                {
                    sql += string.Format(" and a.SaleOrder='{0}'", query.SaleOrder);
                }
                if (!string.IsNullOrEmpty(query.SendBy))
                {
                    sql += string.Format(" and a.SendBy='{0}'", query.SendBy);
                }
                //if (!string.IsNullOrEmpty(query.SerialNo2))
                //{
                //    //sql += string.Format(" and a.SerialNo2='{0}'", query.SerialNo2);
                //    sql += "and a.SerialNo2 in ('" + query.SerialNo2.Replace(",", "','") + "') ";
                //}
                //if (!string.IsNullOrEmpty(query.SerialNo3))
                //{
                //    sql += "and a.SerialNo3 in ('" + query.SerialNo3.Replace(",", "','") + "') ";
                //}
                //if (!string.IsNullOrEmpty(query.SerialNo4))
                //{
                //    sql += "and a.SerialNo4 in ('" + query.SerialNo4.Replace(",", "','") + "') ";
                //}
                if (!string.IsNullOrEmpty(query.BoxID))
                {
                    sql += string.Format(" and a.CartonID='{0}'", query.BoxID);
                }
                if (!string.IsNullOrEmpty(query.DeliveryNote))
                {
                    sql += string.Format(" and a.DeliveryNote='{0}'", query.DeliveryNote);
                }
                if (!string.IsNullOrEmpty(query.DiscreteJob))
                {
                    if (query.DiscreteJob.Contains("*"))
                    {
                        sql += "and a.ProdOrder like '" + query.DiscreteJob.Replace("*", "%") + "' ";
                    }
                    else
                    {
                        sql += "and a.ProdOrder in ('" + query.DiscreteJob.Replace(",", "','") + "') ";
                    }
                    
                }
                if (!string.IsNullOrEmpty(query.FlatFile))
                {
                    if (query.FlatFile.Contains("*"))
                    {
                        sql += "and a.FlatFile like '" + query.FlatFile.Replace("*", "%") + "' ";
                    }
                    else
                    {
                        sql += "and a.FlatFile in ('" + query.FlatFile.Replace(",", "','") + "') ";
                    }
                } 
                if (!string.IsNullOrEmpty(query.Model))
                {
                    if (query.Model.Contains("*"))
                    {
                        sql += "and a.Model like '" + query.Model.Replace("*", "%") + "' ";
                    }
                    else
                    {
                        sql += "and a.Model in ('" + query.Model.Replace(",", "','") + "') ";
                    }
                }
                if (!string.IsNullOrEmpty(query.PalletID))
                {
                    if (query.Model.Contains("*"))
                    {
                        sql += "and a.PalletID like '" + query.PalletID.Replace("*", "%") + "' ";
                    }
                    else
                    {
                        sql += "and a.PalletID in ('" + query.PalletID.Replace(",", "','") + "') ";
                    }
                }
                if (!string.IsNullOrEmpty(query.TVANo))
                {
                    
                    if (query.TVANo.Contains("*"))
                    {
                        sql += "and a.TVA like '" + query.TVANo.Replace("*", "%") + "' ";
                    }
                    else
                    {
                        sql += "and a.TVA in ('" + query.TVANo.Replace(",", "','") + "') ";
                    }
                }
                if (query.CreatedOnFrom.HasValue)
                {
                    sql += string.Format(" and a.CreatedOn>='{0}'", query.CreatedOnFrom.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (query.CreatedOnTo.HasValue)
                {
                    sql += string.Format(" and a.CreatedOn<'{0}'", query.CreatedOnTo.Value.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }
            return sql;
        }

        private ReportProductArchiveDataModel.Item GetProductDataModel(System.Data.SqlClient.SqlDataReader dr)
        {
            try
            {
                ReportProductArchiveDataModel.Item data = new ReportProductArchiveDataModel.Item
                {
                    BoxID = DBConvert.DB2String(dr["CartonID"]),
                    ChangedOn = DBConvert.DB2Datetime(dr["ChangedOn"]),
                    CreatedBy = DBConvert.DB2String(dr["CreatedBy"]),
                    CreatedOn = DBConvert.DB2Datetime(dr["CreatedOn"]),
                    CustomerPN = DBConvert.DB2String(dr["CustomerPN"]),
                    CustomerRev = DBConvert.DB2String(dr["CustomerRev"]),
                    DeliveryNote = DBConvert.DB2String(dr["DeliveryNote"]),
                    DiscreteJob = DBConvert.DB2String(dr["ProdOrder"]),
                    DJQty = DBConvert.DB2Int(dr["DJSize"]),
                    FlatFile = DBConvert.DB2String(dr["FlatFile"]),
                    Model = DBConvert.DB2String(dr["Model"]),
                    PalletID = DBConvert.DB2String(dr["PalletID"]),
                    PreSN = DBConvert.DB2String(dr["PreSN"]),
                    ProductionLine = DBConvert.DB2String(dr["ProductionLine"]),
                    ProductSerialNo = DBConvert.DB2String(dr["ProductSerialNo"]),
                    SaleOrder = DBConvert.DB2String(dr["SaleOrder"]),
                    SentBy = DBConvert.DB2String(dr["SentBy"]),
                    SerialNo2 = DBConvert.DB2String(dr["SerialNo2"]),
                    SerialNo3 = DBConvert.DB2String(dr["SerialNo3"]),
                    SerialNo4 = DBConvert.DB2String(dr["SerialNo4"]),
                    TVA = DBConvert.DB2String(dr["TVA"]),
                };
                return data;
            }
            catch (Exception ex)
            {

                return null;
            }
          
        }
        private ReportProductTestSummaryDataArchiveModel.Item GetProductTestDataSummaryModel(System.Data.SqlClient.SqlDataReader dr)
        {
            try
            {
                var model = new ReportProductTestSummaryDataArchiveModel.Item()
                {
                    CartonID = "",
                    ExtSerialNo = DBConvert.DB2String(dr["ExtSerialNo"]),
                    IntSerialNo= DBConvert.DB2String(dr["IntSerialNo"]),
                    IPSNo = DBConvert.DB2String(dr["IPSNo"]),
                    IPSRevision = DBConvert.DB2String(dr["IPSRevision"]),
                    Model = DBConvert.DB2String(dr["Model"]),
                    OperatorName = DBConvert.DB2String(dr["OperatorName"]),
                    PCBA = DBConvert.DB2String(dr["PCBA"]),
                    PO = DBConvert.DB2String(dr["PO"]),
                    ProcessName = DBConvert.DB2String(dr["ProcessName"]),
                    ProdDate = DBConvert.DB2Datetime(dr["ProdDate"]),
                    ProgramName = DBConvert.DB2String(dr["ProgramName"]),
                    ProgramRevision = DBConvert.DB2String(dr["ProgramRevision"]),
                    Remark = DBConvert.DB2String(dr["Remark"]),
                    Result = DBConvert.DB2String(dr["Result"]),
                    SeqNo = DBConvert.DB2Int(dr["SeqNo"]),
                    TesterNo = DBConvert.DB2String(dr["TesterNo"]),
                };
                return model;
            }
            catch (Exception ex)
            {

                return null;
            }

        }
        public ReportProductTestSummaryDataArchiveModel GetProductTestDataSummary(ReportProductTestDataArchiveQuery query)
        {
            ReportProductTestSummaryDataArchiveModel result = new ReportProductTestSummaryDataArchiveModel();
            result.Data = new List<ReportProductTestSummaryDataArchiveModel.Item>();
            string sql = @"SELECT ExtSerialNo, IntSerialNo, ProcessName, SeqNo, PO, Model, PCBA, ProdDate, WIPIn, Result, OperatorName, TesterNo, ProgramName, ProgramRevision, IPSNo, IPSRevision, Remark FROM V_TDHeader WITH (nolock) where 1=1 " ;
            #region Conditions
            sql += GetReportProductTestDataSummarySqlCondition(query);
            #endregion
            if (query.Pager!=null )
            {

                query.Pager.Order = " ProdDate ";
            }
            PageSql pSql = GetPagerSQL(query, sql);
            if (!string.IsNullOrEmpty(pSql.SQLCount))
            {
                result.Pager = new ModelPager();
                result.Pager.TotalCount = dbHelper.GetCount(pSql.SQLCount);
            }
            result.Data = dbHelper.GetList<ReportProductTestSummaryDataArchiveModel.Item>(pSql.SQLDatas, GetProductTestDataSummaryModel);
            //dbHelper.ExecuteReader(pSql.SQLDatas, (dr) =>
            //{
            //    result.Data.Add(GetReportProductTestDataModel(dr));
            //}, (isover) =>
            //{ result.IsOverMaxRow = isover; });
            return result;
        }

        public long ProductTestDataSummaryGetRowCount(ReportProductTestDataArchiveQuery query)
        {
            string sql = @"SELECT count(1) FROM V_TDHeader WITH (nolock) where 1=1 ";
            sql += GetReportProductTestDataSummarySqlCondition(query);
            long rowCount = dbHelper.GetCount(sql);
            //long maxRowCount = eTrace.Common.LocalConfiguration.Instance.ReportDownloadMaxRowCount();
            return rowCount;
        }

        #endregion 
    }
}
