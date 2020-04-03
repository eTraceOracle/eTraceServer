using eTrace.Report.BLL;
using eTrace.Report.IBLL;
using eTrace.Model;
using eTrace.Model.V2.Report;
using eTrace.Service.SDKForNet;
using eTrace.Service.SDKForNet.Request;
using eTrace.Service.SDKForNet.Request.Reports;
using eTrace.Service.SDKForNet.Response;
using eTrace.Service.SDKForNet.Response.Reports;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using eTrace.Common;
using System.IO;
using System.Net.Http.Headers;
using eTrace.Report.BLL.Business;
using System.Data;
using System.Data.SqlClient;
using RptQualityData;


namespace eTraceWebApi.Controllers
{
    /// <summary>
    /// ReportProductModuleController
    /// </summary>
    public class ReportYieldReportFPYController : ServerBaseController<ReportYieldReportFPYController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportYieldReportFPYController()
        {

        }

        string sdf = string.Empty;
        string sdt = string.Empty;

        /// <summary>
        /// 获取下载Equipment数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadYieldReportFPYDatas([FromBody] DownloadYieldReportFPYDatasRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportYieldReportFPYQuery queryModel = GetModelQuery<DownloadYieldReportFPYDatasRequest, ReportYieldReportFPYQuery>(requestData);
               
                DataTable dtFPY;
                dtFPY = null;

                if (!Validation(requestData))
                    dtFPY = null;
                else
                    dtFPY = DailyProcess(requestData);

                //fullResponse = DownloadReportResponse(queryModel,
                //                                         requestData.TableHeaders,
                //                                         WIPUnitByDJBLL.Instance.WIPUnitByDJDataGetRowCount,
                //                                         WIPUnitByDJBLL.Instance.GetWIPUnitByDJData,
                //                                           "");

                //****** need make sure all columns in datatable  can match those defined in ***Response.cs file, and make sure the datatype is correct  ******//

                long rowCount = dtFPY.Rows.Count;
                if (rowCount > 0 && rowCount < MaxDownloadRowCount)
                {
                    queryModel.Pager = null;

                    //var dbDatas = GetDataFunc(queryModel);

                    fullResponse = GetDownloadExcelResponse(dtFPY, requestData.TableHeaders, "YieldReportFPY");

                    if (fullResponse == null)
                    {
                        fullResponse = Request.CreateResponse(HttpStatusCode.InternalServerError);
                    }
                }
                else if (rowCount == 0)
                {
                    //not available data
                    fullResponse = Request.CreateResponse(HttpStatusCode.NoContent);
                }
                else
                {
                    //over max row limit
                    fullResponse = Request.CreateResponse(HttpStatusCode.RequestEntityTooLarge);
                }

                return ResponseMessage(fullResponse);                                  

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// check row count for Download
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetYieldReportFPYDataTotalCount(DownloadYieldReportFPYDatasRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();

            ReportYieldReportFPYQuery queryModel = GetModelQuery<DownloadYieldReportFPYDatasRequest, ReportYieldReportFPYQuery>(requestData);
            DataTable dtFPY;
            dtFPY = null;
            if (!Validation(requestData))
               dtFPY = null;
            else
               dtFPY = DailyProcess(requestData);
            long rowCount = dtFPY.Rows.Count;
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }

        private class ReportYieldReportFPYQuery : ModelQueryBase
        {
            public string Station { get; set; }
            public string Model { get; set; }
            public DateTime? PDF { get; set; }
            public DateTime? PDT { get; set; }
        }

        /// <summary>
        /// 获取设备数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportYieldReportFPYResponse GetYieldReportFPYDatas([FromBody] ReportYieldReportFPYRequest requestData)
        {          
            //List<string> data = new List<string>();
            //data.AsEnumerable()
            //data.Skip(10).Take()
            //requestData.Pager.

            ReportYieldReportFPYResponse response = new ReportYieldReportFPYResponse();

            response.BussinesCode = EmBussinesCodeType.Success;
            List<ReportYieldReportFPYResponse.Item> dataList = new List<ReportYieldReportFPYResponse.Item>();

            DataTable dtFPY;
            dtFPY = null;

            if (!Validation(requestData))
                dtFPY = null;
            else
                dtFPY = DailyProcess(requestData);

            if (dtFPY !=null && dtFPY.Rows.Count >0)
            {
                int skipRow, returnRow;
                skipRow = (requestData.Pager.CurrentPage - 1) * requestData.Pager.PageSize;
                returnRow = (skipRow + requestData.Pager.PageSize) > dtFPY.Rows.Count ? dtFPY.Rows.Count : (skipRow + requestData.Pager.PageSize);

                decimal demo;
                //DateTime demoTime;

                for (int i = skipRow; i < returnRow; i++)
                {
                    dataList.Add(new ReportYieldReportFPYResponse.Item()
                    {
                        Model = dtFPY.Rows[i]["Model"].ToString(),
                        DJ = dtFPY.Rows[i]["DJ"].ToString(),
                        Description = dtFPY.Rows[i]["Description"].ToString(),
                        Floor = dtFPY.Rows[i]["Floor"].ToString(),
                        BusinessUnit = dtFPY.Rows[i]["BusinessUnit"].ToString(),
                        SeqNo = decimal.TryParse(dtFPY.Rows[i]["SeqNo"].ToString(), out demo) ? demo : 0,
                        Station = dtFPY.Rows[i]["Station"].ToString(),
                        FirstTotal = decimal.TryParse(dtFPY.Rows[i]["FirstTotal"].ToString(), out demo) ? demo : 0,
                        FirstSuccess = decimal.TryParse(dtFPY.Rows[i]["FirstSuccess"].ToString(), out demo) ? demo : 0,
                        FirstFailed = decimal.TryParse(dtFPY.Rows[i]["FirstFailed"].ToString(), out demo) ? demo : 0,
                        FirstYield = decimal.TryParse(dtFPY.Rows[i]["FirstYield"].ToString(), out demo) ? demo : 0,
                        FinalTotal = decimal.TryParse(dtFPY.Rows[i]["FinalTotal"].ToString(), out demo) ? demo : 0,
                        FinalSuccess = decimal.TryParse(dtFPY.Rows[i]["FinalSuccess"].ToString(), out demo) ? demo : 0,
                        FinalFailed = decimal.TryParse(dtFPY.Rows[i]["FinalFailed"].ToString(), out demo) ? demo : 0,
                        FinalYield = decimal.TryParse(dtFPY.Rows[i]["FinalYield"].ToString(), out demo) ? demo : 0,
                        FinalWIP = decimal.TryParse(dtFPY.Rows[i]["FinalWIP"].ToString(), out demo) ? demo : 0,
                        VoltageType = dtFPY.Rows[i]["VoltageType"].ToString(),
                        ServerProdDateTime = dtFPY.Rows[i]["ServerProdDateTime"].ToString(),
                        LocalProdDateTime = dtFPY.Rows[i]["LocalProdDateTime"].ToString(),
                        Power = dtFPY.Rows[i]["Power"].ToString(),
                    });
                }
                response.Data = dataList;

                response.Pager = new ResponsePager()
                {
                    TotalCount = dtFPY.Rows.Count,
                    //TotalCount = (int)Math.Ceiling((double)dtFPY.Rows.Count / requestData.Pager.PageSize),
                };
            }
            else
            {
                response = GetResponseError(ref response, EmBussinesCodeType.NoDataFound);
            }
                     

            return response;


            //List<ReportWIPUnitByDJResponse.Item> dataList = new List<ReportWIPUnitByDJResponse.Item>();
            //for (int i = 0; i < length; i++)
            //{
            //    ReportWIPUnitByDJResponse.Item data = new ReportWIPUnitByDJResponse.Item();
            //    data.AddlText=

            //    dataList.Add(new ReportWIPUnitByDJResponse.Item()
            //    {
            //           DN=
            //    });
            //}
            //};

            //response = GetResponseError(ref  response, EmBussinesCodeType.DJNotFound );
        }

        /// <summary>
        /// 获取设备状态下拉
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpPost]
        public ServerResponse<List<string>> GetStation([FromBody] ReportYieldReportFPYRequest requestData)  //ReportYieldReportFPYRequest requestData
        {
            //ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            ServerResponse<List<string>> response = GetBusinessResponseDataInited<ServerResponse<List<string>>>();
            var dbDatas = Get_Station(requestData);
            response.Data = dbDatas;
            response.BussinesCode = EmBussinesCodeType.Success;
            return response;
        }

        public List<string> Get_Station(ReportYieldReportFPYRequest requestData)
        {
            string sql = "SELECT Process as Station FROM T_ProductProcess with (nolock)";

            DataSet ds1 = new DataSet();
            SqlConnection connection1 = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["eTraceConnection"]);
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql, connection1);
            try
            {
                adapter1.SelectCommand.CommandType = CommandType.Text;
                adapter1.SelectCommand.Connection = connection1;
                adapter1.SelectCommand.CommandTimeout = 6000;
                adapter1.SelectCommand.CommandText = sql;

                adapter1.SelectCommand.Connection.Open();
                adapter1.Fill(ds1, "Station");
                ds1.Tables["Station"].Rows.Add("");
                adapter1.SelectCommand.Connection.Close();
            }
            catch (Exception ex)
            {
                adapter1.SelectCommand.Connection.Close();
                return null/* TODO Change to default(_) if this is not a reference type */;
            }           

            List<string> result = new List<string>();

            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                result.Add(DBConvert.DB2String(ds1.Tables[0].Rows[i]["Station"]));
            }

            //dbHelper.ExecuteReader(sql, (dr) =>
            //{
            //    result.Add(DBConvert.DB2String(dr["Station"]));
            //});

            return result;
        }

        private bool Validation(ServerRequestBase<ReportYieldReportFPYDatasRequestItem> requestData)
        {
            if (requestData.Data.DJ != "")
            {
                return true;
            }
            else if (requestData.Data.PDF == null || requestData.Data.PDT == null)
            {
                return false;
            }

            sdf = DateTime.Parse(requestData.Data.PDF.ToString()).AddHours(-8).ToString("yyyy/MM/dd HH:mm:ss");
            sdt = DateTime.Parse(requestData.Data.PDT.ToString()).AddHours(-8).ToString("yyyy/MM/dd HH:mm:ss");
            return true;
        }

        private DataTable DailyProcess(ServerRequestBase<ReportYieldReportFPYDatasRequestItem> requestData)
        {
            var msg = "";
            try
            {
                msg = " run GetDailyTestData ";
                DataTable dtTestData = GetDailyTestData(requestData, msg);
                msg = " run GetDailyWIPData ";
                DataTable dtWIPData = GetDailyWIPData(requestData, msg);
                msg = " run GetProductMasterData ";
                DataTable dtProductMasterData = GetProductMasterData(requestData, msg);
                msg = " empty data ";
                if ((dtTestData == null && dtWIPData == null) || dtProductMasterData == null)
                {
                    msg = msg + " - " + " dataset is null ";
                    return null/* TODO Change to default(_) if this is not a reference type */;
                }

                DataRow dr;
                for (int i = 0; i <= dtWIPData.Rows.Count - 1; i++)
                {
                    dr = dtTestData.NewRow();
                    dr.ItemArray = dtWIPData.Rows[i].ItemArray;
                    dtTestData.Rows.Add(dr);
                }
                msg = " run GetDailyStatisticData ";
                DataTable dtStatistic = GetDailyStatisticData(dtTestData);
                msg = " run GetDailyResultData ";
                DataTable result = GetDailyResultData(requestData,dtStatistic, dtProductMasterData);
                DataTable calFinalWip = GetFinalWipData(result);
                return result;
            }
            catch (Exception ex)
            {
                msg = msg + " - " + ex.Message;
                return null/* TODO Change to default(_) if this is not a reference type */;
            }
        }

        private DataTable GetDailyTestData(ServerRequestBase<ReportYieldReportFPYDatasRequestItem> requestData, string msg)
        {
            string cond1 = "";

            if (sdf != "" && sdt != "")
                cond1 = cond1 + " and " + "(T1.ProdDate between '" + sdf + "' and '" + sdt + "')";

            if (requestData.Data.Model != null && requestData.Data.Model != "")
                cond1 = cond1 + " and " + "T1.Model in ('" + requestData.Data.Model.Replace(",", "','") + "') ";
            if (requestData.Data.Station != null && requestData.Data.Station != "")
                cond1 = cond1 + " and " + "T1.ProcessName in ('" + requestData.Data.Station.Replace(",", "','") + "') ";

            bool hasDJ = false;
            if (requestData.Data.DJ != null && requestData.Data.DJ != "")
            {
                cond1 = cond1 + " and " + "T1.PO in ('" + requestData.Data.DJ.Replace(",", "','") + "') ";
                hasDJ = true;
            }

            DataSet ds1 = new DataSet();


            string sql1 = "";

            if (hasDJ)
                sql1 = " SELECT   A.Model, A.PO, '1990-01-01' AS ProdDate, upper(A.ProcessName) as ProcessName, A.Result, A.SequenceMin, A.SequenceMax, "
              + " '' AS ShopFloor, R.SeqNo  "
              + " FROM "
              + " (select     T1.ExtSerialNo, T1.Model, T1.PO, t1.Proddate, T1.PCBA, T1.ProcessName, T1.Result "
              + " ,ROW_NUMBER() OVER(PARTITION BY T1.ExtSerialNo, T1.Model, T1.PO, T1.PCBA, T1.ProcessName ORDER BY T1.SeqNo ASC ) as SequenceMin "
              + " ,ROW_NUMBER() OVER(PARTITION BY T1.ExtSerialNo, T1.Model, T1.PO, T1.PCBA, T1.ProcessName ORDER BY T1.SeqNo DESC ) as SequenceMax "
              + " FROM         dbo.T_TDHeader AS T1 WITH (nolock index=IX_ProdOrder index=ix_proddate index=ix_model) "
              + " WHERE     1=1 "
              + cond1
              + " ) AS A "
              + " INNER JOIN dbo.T_Shippment AS B WITH (nolock) ON A.ExtSerialNo = B.ProductSerialNo "
              + " LEFT OUTER JOIN dbo.T_ProductRouting AS R WITH(NOLOCK) ON R.Model = A.Model AND R.PCBA = A.PCBA AND upper(R.Process) = upper(A.ProcessName) "
              + " WHERE 1=1 "
              + " AND (A.SequenceMin = 1 OR A.SequenceMax = 1) "
              + " AND EXISTS (SELECT 1 FROM T_PRODUCTSTRUCTURE as PS WITH(NOLOCK) WHERE PS.PCBA = A.PCBA AND PS.IsTLA = 1 ) ";
            else
                sql1 = " SELECT   A.Model, A.PO, '1990-01-01' AS ProdDate, upper(A.ProcessName) as ProcessName, A.Result, A.SequenceMin, A.SequenceMax, "
                            + " '' AS ShopFloor, R.SeqNo  "
                            + " FROM "
                            + " (select     T1.ExtSerialNo, T1.Model, T1.PO, t1.Proddate, T1.PCBA, T1.ProcessName, T1.Result "
                            + " ,ROW_NUMBER() OVER(PARTITION BY T1.ExtSerialNo, T1.Model, T1.PO, T1.PCBA, T1.ProcessName ORDER BY T1.SeqNo ASC ) as SequenceMin "
                            + " ,ROW_NUMBER() OVER(PARTITION BY T1.ExtSerialNo, T1.Model, T1.PO, T1.PCBA, T1.ProcessName ORDER BY T1.SeqNo DESC ) as SequenceMax "
                            + " FROM         dbo.T_TDHeader AS T1 WITH (nolock index=ix_proddate index=IX_ProdOrder index=ix_model) "
                            + " WHERE     1=1 "
                            + cond1
                            + " ) AS A "
                            + " INNER JOIN dbo.T_Shippment AS B WITH (nolock) ON A.ExtSerialNo = B.ProductSerialNo "
                            + " LEFT OUTER JOIN dbo.T_ProductRouting AS R WITH(NOLOCK) ON R.Model = A.Model AND R.PCBA = A.PCBA AND upper(R.Process) = upper(A.ProcessName) "
                            + " WHERE 1=1 "
                            + " AND (A.SequenceMin = 1 OR A.SequenceMax = 1) "
                            + " AND EXISTS (SELECT 1 FROM T_PRODUCTSTRUCTURE as PS WITH(NOLOCK) WHERE PS.PCBA = A.PCBA AND PS.IsTLA = 1 ) ";


            SqlConnection connection1 = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["eTraceConnection"]);
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, connection1);
            try
            {
                adapter1.SelectCommand.CommandType = CommandType.Text;
                adapter1.SelectCommand.Connection = connection1;
                adapter1.SelectCommand.CommandTimeout = 6000;
                adapter1.SelectCommand.CommandText = sql1;

                adapter1.SelectCommand.Connection.Open();
                adapter1.Fill(ds1, "TestData");
                adapter1.SelectCommand.Connection.Close();
            }
            catch (Exception ex)
            {
                adapter1.SelectCommand.Connection.Close();
                msg = msg + " - " + ex.Message;
                return null/* TODO Change to default(_) if this is not a reference type */;
            }

            return ds1.Tables["TestData"];
        }

        private DataTable GetDailyWIPData(ServerRequestBase<ReportYieldReportFPYDatasRequestItem> requestData, string msg)
        {
            string cond2 = "";

            if (sdf != "" && sdt != "")
                cond2 = cond2 + " and " + "(T1.ProdDate between '" + sdf + "' and '" + sdt + "')";

            if (requestData.Data.Model != null && requestData.Data.Model != "")
                cond2 = cond2 + " and " + "T1.Model in ('" + requestData.Data.Model.Replace(",", "','") + "') ";
            if (requestData.Data.Station != null && requestData.Data.Station != "")
                cond2 = cond2 + " and " + "T1.ProcessName in ('" + requestData.Data.Station.Replace(",", "','") + "') ";
            if (requestData.Data.DJ != null && requestData.Data.DJ != "")
                cond2 = cond2 + " and " + "T1.PO in ('" + requestData.Data.DJ.Replace(",", "','") + "') ";

            DataSet ds2 = new DataSet();

            string sql2 = " SELECT   A.Model, A.PO, '1990-01-01' AS ProdDate, upper(A.ProcessName) as ProcessName, A.Result, A.SequenceMin, A.SequenceMax, "
                                + " '' AS ShopFloor, R.SeqNo  "
                                + " FROM "
                                + " (select  T1.WIPID, T1.Model, T1.PO,  T1.PCBA, T1.ProcessName, T1.Result "
                                + " ,ROW_NUMBER() OVER(PARTITION BY T1.WIPID, T1.Model, T1.PO, T1.PCBA, T1.ProcessName ORDER BY T1.SeqNo ASC ) as SequenceMin "
                                + " ,ROW_NUMBER() OVER(PARTITION BY T1.WIPID, T1.Model, T1.PO, T1.PCBA, T1.ProcessName ORDER BY T1.SeqNo DESC ) as SequenceMax "
                                + " FROM dbo.T_WIPTDHeader AS T1 WITH (nolock) INNER JOIN dbo.T_WIPHeader AS D WITH (nolock) ON T1.WIPID = D.WIPID "
                                + " WHERE     1=1 "
                                + cond2
                                + " ) AS A "
                                + " LEFT OUTER JOIN dbo.T_ProductRouting AS R WITH(NOLOCK) ON R.Model = A.Model AND R.PCBA = A.PCBA AND upper(R.Process) = upper(A.ProcessName) "
                                + " WHERE 1=1 "
                                + " AND (A.SequenceMin = 1 OR A.SequenceMax = 1) "
                                + " AND EXISTS (SELECT 1 FROM T_PRODUCTSTRUCTURE as PS WITH(NOLOCK) WHERE PS.PCBA = A.PCBA AND PS.IsTLA = 1 ) ";

            SqlConnection connection2 = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["eTraceConnection"]);
            SqlDataAdapter adapter2 = new SqlDataAdapter(sql2, connection2);
            try
            {
                adapter2.SelectCommand.CommandType = CommandType.Text;
                adapter2.SelectCommand.Connection = connection2;
                adapter2.SelectCommand.CommandTimeout = 999;
                adapter2.SelectCommand.CommandText = sql2;

                adapter2.SelectCommand.Connection.Open();
                adapter2.Fill(ds2, "WIPData");
                adapter2.SelectCommand.Connection.Close();
            }
            catch (Exception ex)
            {
                adapter2.SelectCommand.Connection.Close();
                msg = msg + " - " + ex.Message;
                return null/* TODO Change to default(_) if this is not a reference type */;
            }
            return ds2.Tables["WIPData"];
        }       

        private DataTable GetProductMasterData(ServerRequestBase<ReportYieldReportFPYDatasRequestItem> requestData, string msg)
        {
            string cond4 = "";

            if (requestData.Data.Model != null && requestData.Data.Model != "")
                cond4 = cond4 + " and " + "B.Model in ('" + requestData.Data.Model.Replace(",", "','") + "') ";

            DataSet ds4 = new DataSet();
            string sql4 = " SELECT B.Model, B.Description, B.BusinessUnit, B.Power, B.VotageType FROM V_ProductMaster AS B WITH (nolock) "
                                + " WHERE 1=1 " + cond4;

            SqlConnection connection4 = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["eTraceConnection"]);
            SqlDataAdapter adapter4 = new SqlDataAdapter(sql4, connection4);
            try
            {
                adapter4.SelectCommand.CommandType = CommandType.Text;
                adapter4.SelectCommand.Connection = connection4;
                adapter4.SelectCommand.CommandTimeout = 999;
                adapter4.SelectCommand.CommandText = sql4;

                adapter4.SelectCommand.Connection.Open();
                adapter4.Fill(ds4, "ProductMasterData");
                adapter4.SelectCommand.Connection.Close();
            }
            catch (Exception ex)
            {
                adapter4.SelectCommand.Connection.Close();
                msg = msg + " - " + ex.Message;
                return null/* TODO Change to default(_) if this is not a reference type */;
            }

            return ds4.Tables["ProductMasterData"];
        }

        private DataTable GetDailyStatisticData(DataTable dt)
        {
            DataTable dt1 = new DataTable("dtStatistic");
            DataColumn dcModel = new DataColumn("Model", System.Type.GetType("System.String"));
            dt1.Columns.Add(dcModel);
            DataColumn dcDJ = new DataColumn("DJ", System.Type.GetType("System.String"));
            dt1.Columns.Add(dcDJ);
            DataColumn dcseq = new DataColumn("SeqNo", System.Type.GetType("System.Int32"));
            dt1.Columns.Add(dcseq);
            DataColumn dcProcessName = new DataColumn("ProcessName", System.Type.GetType("System.String"));
            dt1.Columns.Add(dcProcessName);
            DataColumn dcProdDate = new DataColumn("ProdDate", System.Type.GetType("System.String"));
            dt1.Columns.Add(dcProdDate);
            DataColumn dcShopFloor = new DataColumn("ShopFloor", System.Type.GetType("System.String"));
            dt1.Columns.Add(dcShopFloor);
            DataColumn dc1 = new DataColumn("FPYTotal", System.Type.GetType("System.Int32"));
            dt1.Columns.Add(dc1);
            DataColumn dc2 = new DataColumn("FPYPass", System.Type.GetType("System.Int32"));
            dt1.Columns.Add(dc2);
            DataColumn dc3 = new DataColumn("FPYFail", System.Type.GetType("System.Int32"));
            dt1.Columns.Add(dc3);
            DataColumn dc4 = new DataColumn("LPYTotal", System.Type.GetType("System.Int32"));
            dt1.Columns.Add(dc4);
            DataColumn dc5 = new DataColumn("LPYPass", System.Type.GetType("System.Int32"));
            dt1.Columns.Add(dc5);
            DataColumn dc6 = new DataColumn("LPYFail", System.Type.GetType("System.Int32"));
            dt1.Columns.Add(dc6);

            string modelVal = "";
            string djVal = "";
            string proddateVal = "";
            string processVal = "";
            string shopVal = "";
            string resultVal = "";
            int first = 0;
            int last = 0;
            int SeqNo = 0;

            for (int i = 0; i <= dt.Rows.Count - 1; i += 1)
            {
                modelVal = dt.Rows[i]["Model"].ToString();
                djVal = dt.Rows[i]["PO"].ToString();
                proddateVal = dt.Rows[i]["ProdDate"].ToString();
                processVal = dt.Rows[i]["ProcessName"].ToString();
                shopVal = dt.Rows[i]["ShopFloor"].ToString();
                resultVal = dt.Rows[i]["Result"].ToString().ToUpper();
                first = int.Parse(dt.Rows[i]["SequenceMin"].ToString());
                last = int.Parse(dt.Rows[i]["SequenceMax"].ToString());

                if (dt.Rows[i]["SeqNo"].ToString() == "")
                    SeqNo = 0;
                else
                    SeqNo = int.Parse(dt.Rows[i]["SeqNo"].ToString());

                // 赋值或新加行
                DataRow aRow = GetDataRowInStatistic(dt1, modelVal, djVal, processVal, shopVal);

                if (aRow == null)
                {
                    // 新加
                    int FPYTotal = 0;
                    int FPYPass = 0;
                    int FPYFail = 0;
                    int LPYTotal = 0;
                    int LPYPass = 0;
                    int LPYFail = 0;
                    if (first == 1)
                    {
                        // 如果是first yield
                        FPYTotal = FPYTotal + 1;
                        if (resultVal == "PASS")
                            FPYPass = FPYPass + 1;
                        else
                            FPYFail = FPYFail + 1;
                    }

                    if (last == 1)
                    {
                        // 如果是last yield
                        LPYTotal = LPYTotal + 1;
                        if (resultVal == "PASS")
                            LPYPass = LPYPass + 1;
                        else
                            LPYFail = LPYFail + 1;
                    }

                    DataRow dr = dt1.NewRow();
                    dr["Model"] = modelVal;
                    dr["DJ"] = djVal;
                    dr["ProcessName"] = processVal;
                    dr["ProdDate"] = proddateVal;
                    dr["ShopFloor"] = shopVal;
                    dr["FPYTotal"] = FPYTotal;
                    dr["FPYPass"] = FPYPass;
                    dr["FPYFail"] = FPYFail;
                    dr["LPYTotal"] = LPYTotal;
                    dr["LPYPass"] = LPYPass;
                    dr["LPYFail"] = LPYFail;
                    dr["SeqNo"] = SeqNo;
                    dt1.Rows.Add(dr);
                }
                else
                {
                    // 修改
                    DataRow dr = aRow;

                    int FPYTotal = int.Parse(dr["FPYTotal"].ToString());
                    int FPYPass = int.Parse(dr["FPYPass"].ToString());
                    int FPYFail = int.Parse(dr["FPYFail"].ToString());
                    int LPYTotal = int.Parse(dr["LPYTotal"].ToString());
                    int LPYPass = int.Parse(dr["LPYPass"].ToString());
                    int LPYFail = int.Parse(dr["LPYFail"].ToString());
                    if (first == 1)
                    {
                        // 如果是first yield
                        FPYTotal = FPYTotal + 1;
                        if (resultVal == "PASS")
                            FPYPass = FPYPass + 1;
                        else
                            FPYFail = FPYFail + 1;
                    }

                    if (last == 1)
                    {
                        // 如果是last yield
                        LPYTotal = LPYTotal + 1;
                        if (resultVal == "PASS")
                            LPYPass = LPYPass + 1;
                        else
                            LPYFail = LPYFail + 1;
                    }


                    dr["FPYTotal"] = FPYTotal;
                    dr["FPYPass"] = FPYPass;
                    dr["FPYFail"] = FPYFail;
                    dr["LPYTotal"] = LPYTotal;
                    dr["LPYPass"] = LPYPass;
                    dr["LPYFail"] = LPYFail;
                }
            }
            return dt1;
        }

        private DataRow GetDataRowInStatistic(DataTable dt1, string model, string dj, string process, string shopfloor)
        {
            for (int i = dt1.Rows.Count - 1; i >= 0; i += -1)
            {
                DataRow row = dt1.Rows[i];
                if (row["Model"].ToString() == model && row["DJ"].ToString() == dj && row["ProcessName"].ToString() == process && row["ShopFloor"].ToString() == shopfloor)
                    return row;
            }
            return null/* TODO Change to default(_) if this is not a reference type */;
        }

        private DataTable GetDailyResultData(ServerRequestBase<ReportYieldReportFPYDatasRequestItem> requestData,DataTable dtStatistic, DataTable dtProductMaster)
        {
            var query1 = from statistic in dtStatistic.AsEnumerable()
                         join prodMaster in dtProductMaster.AsEnumerable() on statistic.Field<string>("Model").ToUpper() equals prodMaster.Field<string>("Model").ToUpper() into g1
                         from prodMaster in g1.DefaultIfEmpty()
                         select new
                         {
                             Model = statistic.Field<string>("Model"),
                             DJ = statistic.Field<string>("DJ"),
                             SeqNo = statistic.Field<int>("SeqNo"),
                             ProcessName = statistic.Field<string>("ProcessName"),
                             FPYTotal = statistic.Field<int>("FPYTotal"),
                             FPYPass = statistic.Field<int>("FPYPass"),
                             FPYFail = statistic.Field<int>("FPYFail"),
                             LPYTotal = statistic.Field<int>("LPYTotal"),
                             LPYPass = statistic.Field<int>("LPYPass"),
                             LPYFail = statistic.Field<int>("LPYFail"),
                             ProdDate = statistic.Field<string>("ProdDate"),
                             ShopFloor = statistic.Field<string>("ShopFloor"),
                             Description = prodMaster == null ? "" : prodMaster.Field<string>("Description"),
                             BusinessUnit = prodMaster == null ? "" : prodMaster.Field<string>("BusinessUnit"),
                             Power = prodMaster == null ? "" : prodMaster.Field<string>("Power"),
                             VotageType = prodMaster == null ? "" : prodMaster.Field<string>("VotageType")
                         };

            var dt2 = new DataTable("Daily");
            var dcModel = new DataColumn("Model", Type.GetType("System.String"));
            dt2.Columns.Add(dcModel);
            var dcDJ = new DataColumn("DJ", Type.GetType("System.String"));
            dt2.Columns.Add(dcDJ);
            var dcDescription = new DataColumn("Description", Type.GetType("System.String"));
            dt2.Columns.Add(dcDescription);
            var dcshopfloor = new DataColumn("Floor", Type.GetType("System.String"));
            dt2.Columns.Add(dcshopfloor);
            var dcBusinessUnit = new DataColumn("BusinessUnit", Type.GetType("System.String"));
            dt2.Columns.Add(dcBusinessUnit);
            var dcSeq = new DataColumn("SeqNo", Type.GetType("System.Int32"));
            dt2.Columns.Add(dcSeq);
            var dcStation = new DataColumn("Station", Type.GetType("System.String"));
            dt2.Columns.Add(dcStation);
            var dcTotal = new DataColumn("FirstTotal", Type.GetType("System.String"));
            dt2.Columns.Add(dcTotal);
            var dcSuccess = new DataColumn("FirstSuccess", Type.GetType("System.String"));
            dt2.Columns.Add(dcSuccess);
            var dcFailed = new DataColumn("FirstFailed", Type.GetType("System.String"));
            dt2.Columns.Add(dcFailed);
            var dcYield = new DataColumn("FirstYield", Type.GetType("System.String"));
            dt2.Columns.Add(dcYield);
            var dcTotal2 = new DataColumn("FinalTotal", Type.GetType("System.String"));
            dt2.Columns.Add(dcTotal2);
            var dcSuccess2 = new DataColumn("FinalSuccess", Type.GetType("System.String"));
            dt2.Columns.Add(dcSuccess2);
            var dcFailed2 = new DataColumn("FinalFailed", Type.GetType("System.String"));
            dt2.Columns.Add(dcFailed2);
            var dcYield2 = new DataColumn("FinalYield", Type.GetType("System.String"));
            dt2.Columns.Add(dcYield2);
            var dcFinalWip = new DataColumn("FinalWIP", Type.GetType("System.String"));
            dt2.Columns.Add(dcFinalWip);
            var dcVotageType = new DataColumn("VoltageType", Type.GetType("System.String"));
            dt2.Columns.Add(dcVotageType);
            var dcSDate = new DataColumn("ServerProdDateTime", Type.GetType("System.String"));
            dt2.Columns.Add(dcSDate);
            var dcLDate = new DataColumn("LocalProdDateTime", Type.GetType("System.String"));
            dt2.Columns.Add(dcLDate);
            var dcPower = new DataColumn("Power", Type.GetType("System.String"));
            dt2.Columns.Add(dcPower);

            foreach (var dailyItem in query1)
            {
                DataRow dr = dt2.NewRow();
                dr["Model"] = dailyItem.Model;
                dr["DJ"] = dailyItem.DJ;
                dr["Description"] = dailyItem.Description;
                dr["Floor"] = dailyItem.ShopFloor;
                dr["BusinessUnit"] = dailyItem.BusinessUnit;
                dr["SeqNo"] = dailyItem.SeqNo;
                dr["Station"] = dailyItem.ProcessName;

                dr["FirstTotal"] = dailyItem.FPYTotal;
                dr["FirstSuccess"] = dailyItem.FPYPass;
                dr["FirstFailed"] = dailyItem.FPYFail;
                if (dailyItem.FPYTotal == 0)
                    dr["FirstYield"] = double.Parse(double.Parse("0").ToString("0.####")) * 100;
                else
                    dr["FirstYield"] = double.Parse((double.Parse(dailyItem.FPYPass.ToString()) / double.Parse(dailyItem.FPYTotal.ToString())).ToString("0.####")) * 100;

                dr["FinalTotal"] = dailyItem.LPYTotal;
                dr["FinalSuccess"] = dailyItem.LPYPass;
                dr["FinalFailed"] = dailyItem.LPYFail;

                if (dailyItem.LPYTotal == 0)
                    dr["FinalYield"] = double.Parse(double.Parse("0").ToString("0.####")) * 100; 
                else
                    dr["FinalYield"] = double.Parse((double.Parse(dailyItem.LPYPass.ToString()) / double.Parse(dailyItem.LPYTotal.ToString())).ToString("0.####")) * 100;

                dr["VoltageType"] = dailyItem.VotageType;
                dr["LocalProdDateTime"] = string.Format("{0} - {1}", requestData.Data.PDF?.ToString("yyyy/MM/dd HH:mm:ss"), requestData.Data.PDT?.ToString("yyyy/MM/dd HH:mm:ss"));
                dr["ServerProdDateTime"] = string.Format("{0} - {1}", sdf, sdt);
                dr["Power"] = dailyItem.Power;
                dr["FinalWIP"] = dr["FinalTotal"];
                dt2.Rows.Add(dr);
            }

            DataView dataView = dt2.DefaultView;
            dataView.Sort = "Model asc, dj asc, SeqNo asc, Station asc";
            dt2 = dataView.ToTable();

            return dt2;
        }

        private DataTable GetFinalWipData(DataTable data)
        {
            DataTable copy = data.Copy();

            for (int i = 0, loopTo = data.Rows.Count - 1; i <= loopTo; i += 1)
            {
                DataRow row = data.Rows[i];
                if (row["SeqNo"].ToString() != "0")
                {
                    int seq = int.Parse(row["SeqNo"].ToString());
                    string model = row["Model"].ToString();
                    string dj = row["DJ"].ToString();
                    string floor = row["Floor"].ToString();
                    string station = row["Station"].ToString();

                    DataRow r = GetDataRowInFinalWipData(copy, seq, model, dj, station, floor);

                    if (r != null)
                    {
                        int nextft = int.Parse(r["FinalTotal"].ToString());
                        int ft = int.Parse(row["FinalTotal"].ToString());
                        row["FinalWIP"] = ft - nextft;
                    }
                }
            }
            return data;
        }

        private DataRow GetDataRowInFinalWipData(DataTable dt1, int seq, string model, string dj, string station, string floor)
        {
            DataRow[] rows = dt1.Select("Model='" + model + "' and DJ='" + dj + "' and Floor='" + floor + "' and SeqNo > " + seq.ToString() + "", "SeqNo asc");

            if (rows.Count() > 0)
                return rows[0];
            else
                return null;
        }

    }
}
