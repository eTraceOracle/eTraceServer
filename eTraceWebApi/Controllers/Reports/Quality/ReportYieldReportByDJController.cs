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
    public class ReportYieldReportByDJController : ServerBaseController<ReportYieldReportByDJController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportYieldReportByDJController()
        {

        }

        /// <summary>
        /// 获取下载Equipment数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadYieldReportByDJDatas([FromBody] DownloadYieldReportByDJDatasRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportYieldReportByDJQuery queryModel = GetModelQuery<DownloadYieldReportByDJDatasRequest, ReportYieldReportByDJQuery>(requestData);

                DataTable dtDaily;
                dtDaily = null;

                dtDaily = DailyProcess(requestData);

                //fullResponse = DownloadReportResponse(queryModel,
                //                                         requestData.TableHeaders,
                //                                         WIPUnitByDJBLL.Instance.WIPUnitByDJDataGetRowCount,
                //                                         WIPUnitByDJBLL.Instance.GetWIPUnitByDJData,
                //                                           "");

                long rowCount = dtDaily.Rows.Count;
                if (rowCount > 0 && rowCount < MaxDownloadRowCount)
                {
                    queryModel.Pager = null;

                    //var dbDatas = GetDataFunc(queryModel);
                    fullResponse = GetDownloadExcelResponse(dtDaily, requestData.TableHeaders, "YieldReportByDJ");

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
        public ReportCheckRowsCountResponse GetYieldReportByDJDataTotalCount(DownloadYieldReportByDJDatasRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();

            ReportYieldReportByDJQuery queryModel = GetModelQuery<DownloadYieldReportByDJDatasRequest, ReportYieldReportByDJQuery>(requestData);
            DataTable dtDaily;
            dtDaily = null;
            dtDaily = DailyProcess(requestData);
            long rowCount = dtDaily.Rows.Count;
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
            //response.BussinesCode = EmBussinesCodeType.Success;
            //response.LessThanCheckDownloadRowCount = (rowCount <= CheckDownloadRowCount);
            //response.RowCount = rowCount;
            //return response;
        }

        private class ReportYieldReportByDJQuery : ModelQueryBase
        {
            public string DJ { get; set; }
            public DateTime? PDF { get; set; }
            public DateTime? PDT { get; set; }
        }

        /// <summary>
        /// 获取设备数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportYieldReportByDJResponse GetYieldReportByDJDatas([FromBody] ReportYieldReportByDJRequest requestData)
        {
            //List<string> data = new List<string>();
            //data.AsEnumerable()
            //data.Skip(10).Take()
            //requestData.Pager.

            ReportYieldReportByDJResponse response = new ReportYieldReportByDJResponse();

            response.BussinesCode = EmBussinesCodeType.Success;
            List<ReportYieldReportByDJResponse.Item> dataList = new List<ReportYieldReportByDJResponse.Item>();

            DataTable dtDaily;
            dtDaily = null;

            dtDaily = DailyProcess(requestData);

            if (dtDaily != null && dtDaily.Rows.Count > 0)
            {
                int skipRow, returnRow;
                skipRow = (requestData.Pager.CurrentPage - 1) * requestData.Pager.PageSize;
                returnRow = (skipRow + requestData.Pager.PageSize) > dtDaily.Rows.Count ? dtDaily.Rows.Count : (skipRow + requestData.Pager.PageSize);

                decimal demo;
                //DateTime demoTime;

                for (int i = skipRow; i < returnRow; i++)
                {
                    dataList.Add(new ReportYieldReportByDJResponse.Item()
                    {
                        Model = dtDaily.Rows[i]["Model"].ToString(),
                        Station = dtDaily.Rows[i]["Station"].ToString(),
                        Total = decimal.TryParse(dtDaily.Rows[i]["Total"].ToString(), out demo) ? demo : 0,
                        Success = decimal.TryParse(dtDaily.Rows[i]["Success"].ToString(), out demo) ? demo : 0,
                        Failed = decimal.TryParse(dtDaily.Rows[i]["Failed"].ToString(), out demo) ? demo : 0,
                        Yield = decimal.TryParse(dtDaily.Rows[i]["Yield"].ToString(), out demo) ? demo : 0,
                        PPM = decimal.TryParse(dtDaily.Rows[i]["PPM"].ToString(), out demo) ? demo : 0,
                        ProdOrder = dtDaily.Rows[i]["ProdOrder"].ToString(),
                        POQty = decimal.TryParse(dtDaily.Rows[i]["POQty"].ToString(), out demo) ? demo : 0,
                        Floor = dtDaily.Rows[i]["Floor"].ToString(),
                        Description = dtDaily.Rows[i]["Description"].ToString(),
                        BusinessUnit = dtDaily.Rows[i]["BusinessUnit"].ToString(),
                        Power = dtDaily.Rows[i]["Power"].ToString(),
                        VoltageType = dtDaily.Rows[i]["VoltageType"].ToString(),
                        GenerateDate = dtDaily.Rows[i]["GenerateDate"].ToString()
                    });
                }
                response.Data = dataList;

                response.Pager = new ResponsePager()
                {
                    TotalCount = dtDaily.Rows.Count,
                    //TotalCount = (int)Math.Ceiling((double)dtWeekly.Rows.Count / requestData.Pager.PageSize),
                };
            }
            else
            {
                response = GetResponseError(ref response, EmBussinesCodeType.NoDataFound);
            }
            return response;
        }

        private DataTable DailyProcess(ServerRequestBase<ReportYieldReportByDJDatasRequestItem> requestData)
        {
            var msg = "";
            try
            {
                msg = " run GetDailyTestData ";
                DataTable dtTestData = GetDailyTestData(requestData, msg);
                msg = " run GetDailyWIPData ";
                DataTable dtWIPData = GetDailyWIPData(requestData, msg);
                msg = " run GetDailyRepairData ";
                DataTable dtRepairData = GetDailyRepairData(requestData, msg);
                msg = " run GetProductMasterData ";
                DataTable dtProductMasterData = GetProductMasterData();
                msg = " empty data ";
                if (dtTestData == null | dtWIPData == null | dtRepairData == null | dtProductMasterData == null)
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
                DataTable dtStatistic = RptQualityData.VBGetQualityData.GetDailyStatisticData(dtTestData);
                msg = " run GetDJQty ";
                DataTable dtDJQty = GetDJQty(requestData, msg);
                msg = " run GetDailyResultData ";
                DataTable result = RptQualityData.VBGetQualityData.GetDailyResultData(dtStatistic, dtRepairData, dtProductMasterData, dtDJQty);
                return result;
            }
            catch (Exception ex)
            {
                msg = msg + " - " + ex.Message;
                return null/* TODO Change to default(_) if this is not a reference type */;
            }
        }

        private DataTable GetDailyTestData(ServerRequestBase<ReportYieldReportByDJDatasRequestItem> requestData, string msg)
        {
            string cond1 = "";
            if (requestData.Data.DJ != null && requestData.Data.DJ != "")
            {
                cond1 = cond1 + " and " + "T1.PO = \'" + requestData.Data.DJ.Replace(",", "\',\'") + "\' ";
            }

            // If PDF.Text <> "" And PDT.Text <> "" Then
            //     cond1 = cond1 & " and " & "(T1.ProdDate between '" & PDF.Text & " 00:00:00 AM' and '" & PDT.Text & " 11:59:59 PM')"
            // End If
            if (requestData.Data.PDF != null)
            {
                string PDF;
                PDF = requestData.Data.PDF?.ToString("yyyy/MM/dd");
                cond1 = cond1 + " and (T1.ProdDate >= \'" + PDF + " 00:00:00 AM\')";
            }

            if ((requestData.Data.PDT != null))
            {
                string PDT;
                PDT = requestData.Data.PDT?.ToString("yyyy/MM/dd");
                cond1 = cond1 + " and (T1.ProdDate <= \'" + PDT + " 11:59:59 PM\')";
            }

            DataSet ds1 = new DataSet();
            string sql1 = " SELECT     A.Model, A.PO,  A.ProcessName, 1 AS ANumber, dbo.af_getZSFloor(B.ProductionLine) AS ShopFloor  FROM  (SELECT     T1.ExtSerialNo, T1.Model, T1.PO, MIN(T1.ProdDate) AS Proddate, T1.ProcessName  FROM  dbo.T_TDHeader AS T1 WITH (nolock index=IX_ProdOrder)  WHERE     (T1.SeqNo = 1)  AND EXISTS (SELECT 1 FROM T_PRODUCTSTRUCTURE as PS WITH(NOLOCK) WHERE PS.PCBA = T1.PCBA AND PS.IsTLA = 1 ) "
                        + cond1 + " GROUP BY T1.ExtSerialNo, T1.Model, T1.PO, T1.ProcessName) AS A  INNER JOIN dbo.T_Shippment AS B WITH (nolock) ON A.ExtSerialNo = B.ProductSerialNo ";

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
                msg = (msg + (" - " + ex.Message));
                return null;
            }
            return ds1.Tables["TestData"];
        }

        private DataTable GetDailyWIPData(ServerRequestBase<ReportYieldReportByDJDatasRequestItem> requestData, string msg)
        {
            string cond2 = "";
            if (requestData.Data.DJ != null && requestData.Data.DJ != "")
            {
                cond2 = cond2 + " and " + "T1.PO = \'" + requestData.Data.DJ.Replace(",", "\',\'") + "\' ";
            }

            // If PDF.Text <> "" And PDT.Text <> "" Then
            //     cond2 = cond2 & " and " & "(T1.ProdDate between '" & PDF.Text & " 00:00:00 AM' and '" & PDT.Text & " 11:59:59 PM')"
            // End If
            if (requestData.Data.PDF != null)
            {
                string PDF;
                PDF = requestData.Data.PDF?.ToString("yyyy/MM/dd");

                cond2 = cond2 + " and (T1.ProdDate >= \'" + PDF + " 00:00:00 AM\')";
            }

            if (requestData.Data.PDT != null)
            {
                string PDT;
                PDT = requestData.Data.PDT?.ToString("yyyy/MM/dd");

                cond2 = cond2 + " and (T1.ProdDate <= \'" + PDT + " 11:59:59 PM\')";
            }

            DataSet ds2 = new DataSet();
            string sql2 = " SELECT     T1.Model, T1.PO, T1.ProcessName, 1 AS ANumber, dbo.af_getZSFloor(D .ProdLine) AS ShopFloor FROM dbo.T_WIPTDHeader AS T1 WITH (nolock)  INNER JOIN dbo.T_WIPHeader AS D WITH (nolock) ON T1.WIPID = D.WIPID WHERE     (T1.SeqNo = 1)  AND EXISTS (SELECT 1 FROM T_PRODUCTSTRUCTURE as PS WITH(NOLOCK) WHERE PS.PCBA = D.PCBA AND PS.IsTLA = 1 ) " + cond2;

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
                msg = (msg + (" - " + ex.Message));
                return null;
            }
            return ds2.Tables["WIPData"];
        }

        private DataTable GetDailyRepairData(ServerRequestBase<ReportYieldReportByDJDatasRequestItem> requestData, string msg)
        {
            string cond3_1 = "";
            if (requestData.Data.DJ != null && requestData.Data.DJ != "")
            {
                cond3_1 = cond3_1 + " and " + "H1.ProdOrder = \'" + requestData.Data.DJ.Replace(",", "\',\'") + "\' ";
            }

            // If PDF.Text <> "" And PDT.Text <> "" Then
            //     cond3_1 = cond3_1 & " and " & "(I1.FailTime between '" & PDF.Text & " 00:00:00 AM' and '" & PDT.Text & " 11:59:59 PM')"
            // End If

            if (requestData.Data.PDF != null)
            {
                string PDF;
                PDF = requestData.Data.PDF?.ToString("yyyy/MM/dd");

                cond3_1 = cond3_1 + " and (I1.FailTime >= \'" + PDF + " 00:00:00 AM\')";
            }

            if (requestData.Data.PDT != null)
            {
                string PDT;
                PDT = requestData.Data.PDT?.ToString("yyyy/MM/dd");

                cond3_1 = cond3_1 + " and (I1.FailTime <= \'" + PDT + " 11:59:59 PM\')";
            }

            DataSet ds3 = new DataSet();
            string sql3 = " SELECT REP.Model, REP.ProdOrder, sum(REP.NUM) as NUMB, REP.TestStation, REP.shopFloor  FROM (  " + " SELECT DISTINCT 1 AS NUM, H1.Model, H1.ProdOrder, I1.TestStation, dbo.af_getZSFloor(H1.Floor) as shopFloor, H1.RepID  FROM  T_RepHeader AS H1  with(nolock) INNER JOIN T_RepItem AS I1 with(nolock) ON  H1.RepID = I1.RepID WHERE 1=1 "
                        + cond3_1 + " ) as REP   " + " GROUP BY  REP.Model, REP.ProdOrder, REP.TestStation, REP.shopFloor ";

            SqlConnection connection3 = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["eTraceConnection"]);
            SqlDataAdapter adapter3 = new SqlDataAdapter(sql3, connection3);
            try
            {
                adapter3.SelectCommand.CommandType = CommandType.Text;
                adapter3.SelectCommand.Connection = connection3;
                adapter3.SelectCommand.CommandTimeout = 999;
                adapter3.SelectCommand.CommandText = sql3;
                adapter3.SelectCommand.Connection.Open();
                adapter3.Fill(ds3, "RepairData");
                adapter3.SelectCommand.Connection.Close();
            }
            catch (Exception ex)
            {
                adapter3.SelectCommand.Connection.Close();
                msg = (msg + (" - " + ex.Message));
                return null;
            }
            return ds3.Tables["RepairData"];
        }

        private DataTable GetProductMasterData()
        {
            string cond4 = "";
            DataSet ds4 = new DataSet();
            string sql4 = " SELECT B.Model, B.Description, B.BusinessUnit, B.Power, B.VotageType AS VoltageType FROM V_ProductMaster AS B WITH (nolock)  WHERE 1=1 " + cond4;

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
                return null;
            }
            return ds4.Tables["ProductMasterData"];
        }

        private DataTable GetDJQty(ServerRequestBase<ReportYieldReportByDJDatasRequestItem> requestData, string msg)
        {
            string cond2 = "";
            if (requestData.Data.DJ != null && requestData.Data.DJ != "")
            {
                cond2 = " where PO = \'" + requestData.Data.DJ.Replace(",", "\',\'") + "\' ";
            }

            DataSet ds2 = new DataSet();
            string sql2 = (" SELECT distinct PO, POQty from T_POQty " + cond2);

            SqlConnection connection2 = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["eTraceConnection"]);
            SqlDataAdapter adapter2 = new SqlDataAdapter(sql2, connection2);
            try
            {
                adapter2.SelectCommand.CommandType = CommandType.Text;
                adapter2.SelectCommand.Connection = connection2;
                adapter2.SelectCommand.CommandTimeout = 999;
                adapter2.SelectCommand.CommandText = sql2;
                adapter2.SelectCommand.Connection.Open();
                adapter2.Fill(ds2, "DJQty");
                adapter2.SelectCommand.Connection.Close();
            }
            catch (Exception ex)
            {
                adapter2.SelectCommand.Connection.Close();
                msg = (msg + (" - " + ex.Message));
                return null;
            }
            return ds2.Tables["DJQty"];
        }


    }
}
