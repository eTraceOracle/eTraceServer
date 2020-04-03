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
    public class ReportYieldReportDailyController : ServerBaseController<ReportYieldReportDailyController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportYieldReportDailyController()
        {

        }

        /// <summary>
        /// 获取下载Equipment数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadYieldReportDailyDatas([FromBody] DownloadYieldReportDailyDatasRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportYieldReportDailyQuery queryModel = GetModelQuery<DownloadYieldReportDailyDatasRequest, ReportYieldReportDailyQuery>(requestData);

                bool isFastWay = ValidationFastWay(requestData);
                bool isFastWay2 = ValidationFastWay2(requestData);
                DataTable dtDaily;
                dtDaily = null;

                if (isFastWay || isFastWay2)
                {
                    dtDaily = DailyProcess(requestData);
                }
                else
                {
                    DataTable tmp = DailyData(requestData);
                    if (tmp == null)
                    {
                        fullResponse = Request.CreateResponse(HttpStatusCode.NoContent);
                        return ResponseMessage(fullResponse);
                    }

                    DataTable statistic = GetDailyStatistic(tmp);
                    DataTable pm = GetProductMasterData(requestData);
                    if (statistic != null && pm != null)
                    {
                        GetResult(ref statistic, pm);
                        statistic.DefaultView.Sort = "Model ASC";
                        dtDaily = statistic.Copy();
                    }
                }

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

                    fullResponse = GetDownloadExcelResponse(dtDaily, requestData.TableHeaders, "DailyYieldReport");

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
        public ReportCheckRowsCountResponse GetYieldReportDailyDataTotalCount(DownloadYieldReportDailyDatasRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
            ReportYieldReportDailyQuery queryModel = GetModelQuery<DownloadYieldReportDailyDatasRequest, ReportYieldReportDailyQuery>(requestData);
            long rowCount = 0;

            bool isFastWay = ValidationFastWay(requestData);
            bool isFastWay2 = ValidationFastWay2(requestData);
            DataTable dtDaily;
            dtDaily = null;

            if (isFastWay || isFastWay2)
            {
                dtDaily = DailyProcess(requestData);
                rowCount = dtDaily.Rows.Count;
            }
            else
            {
                DataTable tmp = DailyData(requestData);
                if (tmp == null)
                {
                    rowCount = 0;
                }
                else
                {
                    DataTable statistic = GetDailyStatistic(tmp);
                    DataTable pm = GetProductMasterData(requestData);
                    if (statistic != null && pm != null)
                    {
                        GetResult(ref statistic, pm);
                        statistic.DefaultView.Sort = "Model ASC";
                        dtDaily = statistic.Copy();
                        rowCount = dtDaily.Rows.Count;
                    }
                }
            }
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
            //response.BussinesCode = EmBussinesCodeType.Success;
            //response.LessThanCheckDownloadRowCount = (rowCount <= CheckDownloadRowCount);
            //response.RowCount = rowCount;
            //return response;
        }

        private class ReportYieldReportDailyQuery : ModelQueryBase
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
        public ReportYieldReportDailyResponse GetYieldReportDailyDatas([FromBody] ReportYieldReportDailyRequest requestData)
        {
            //List<string> data = new List<string>();
            //data.AsEnumerable()
            //data.Skip(10).Take()
            //requestData.Pager.

            ReportYieldReportDailyResponse response = new ReportYieldReportDailyResponse();

            response.BussinesCode = EmBussinesCodeType.Success;
            List<ReportYieldReportDailyResponse.Item> dataList = new List<ReportYieldReportDailyResponse.Item>();

            bool isFastWay = ValidationFastWay(requestData);
            bool isFastWay2 = ValidationFastWay2(requestData);
            DataTable dtDaily;
            dtDaily = null;

            if (isFastWay || isFastWay2)
            {
                dtDaily = DailyProcess(requestData);
            }
            else
            {
                DataTable tmp = DailyData(requestData);
                if (tmp == null)
                {
                    response = GetResponseError(ref response, EmBussinesCodeType.NoDataFound);
                    return response;
                }

                DataTable statistic = GetDailyStatistic(tmp);
                DataTable pm = GetProductMasterData(requestData);
                if (statistic != null && pm != null)
                {
                    GetResult(ref statistic, pm);
                    statistic.DefaultView.Sort = "Model ASC";
                    dtDaily = statistic.Copy();
                }
            }

            if (dtDaily != null && dtDaily.Rows.Count > 0)
            {
                int skipRow, returnRow;
                skipRow = (requestData.Pager.CurrentPage - 1) * requestData.Pager.PageSize;
                returnRow = (skipRow + requestData.Pager.PageSize) > dtDaily.Rows.Count ? dtDaily.Rows.Count : (skipRow + requestData.Pager.PageSize);

                decimal demo;
                //DateTime demoTime;

                for (int i = skipRow; i < returnRow; i++)
                {
                    dataList.Add(new ReportYieldReportDailyResponse.Item()
                    {
                        Model = dtDaily.Rows[i]["Model"].ToString(),
                        Station = dtDaily.Rows[i]["Station"].ToString(),
                        Total = decimal.TryParse(dtDaily.Rows[i]["Total"].ToString(), out demo) ? demo : 0,
                        Success = decimal.TryParse(dtDaily.Rows[i]["Success"].ToString(), out demo) ? demo : 0,
                        Failed = decimal.TryParse(dtDaily.Rows[i]["Failed"].ToString(), out demo) ? demo : 0,
                        Yield = decimal.TryParse(dtDaily.Rows[i]["Yield"].ToString(), out demo) ? demo : 0,
                        PPM = decimal.TryParse(dtDaily.Rows[i]["PPM"].ToString(), out demo) ? demo : 0,
                        //ProdDate = DateTime.TryParse(dtDaily.Rows[i]["ProdDate"].ToString(), out demoTime) ? demoTime : DateTime.Parse("1900-01-01"),
                        ProdDate = dtDaily.Rows[i]["ProdDate"].ToString(),

                        Floor = dtDaily.Rows[i]["Floor"].ToString(),
                        Description = dtDaily.Rows[i]["Description"].ToString(),
                        BusinessUnit = dtDaily.Rows[i]["BusinessUnit"].ToString(),
                        Power = dtDaily.Rows[i]["Power"].ToString(),
                        VoltageType = dtDaily.Rows[i]["VoltageType"].ToString()
                    });
                }
                response.Data = dataList;

                response.Pager = new ResponsePager()
                {
                    TotalCount = dtDaily.Rows.Count,
                    //TotalCount = (int)Math.Ceiling((double)dtDaily.Rows.Count / requestData.Pager.PageSize),
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
        public ServerResponse<List<string>> GetStation([FromBody] ReportYieldReportDailyRequest requestData)  //ReportYieldReportDailyRequest requestData
        {
            //ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            ServerResponse<List<string>> response = GetBusinessResponseDataInited<ServerResponse<List<string>>>();
            var dbDatas = Get_Station(requestData);
            response.Data = dbDatas;
            response.BussinesCode = EmBussinesCodeType.Success;
            return response;
        }

        public List<string> Get_Station(ReportYieldReportDailyRequest requestData)
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

                //ds1.Tables["Station"].Rows.Add("");

                DataRow newBlankRow = ds1.Tables["Station"].NewRow();
                newBlankRow["Station"] = "";
                ds1.Tables["Station"].Rows.InsertAt(newBlankRow, 0);

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

        private bool ValidationFastWay(ServerRequestBase<ReportYieldReportDailyDatasRequestItem> requestData)
        {
            string md = requestData.Data.Model;
            if ((md == null))
            {
                return false;
            }
            else
            {
                string[] split = md.Split(',');
                if ((split.Length > 5))
                {
                    return false;
                }

            }

            DateTime? df = requestData.Data.PDF;
            DateTime? dt = requestData.Data.PDT;
            TimeSpan? ts = (dt - df);
            if ((ts?.Days > 7))
            {
                return false;
            }

            return true;
        }

        private bool ValidationFastWay2(ServerRequestBase<ReportYieldReportDailyDatasRequestItem> requestData)
        {
            DateTime? df = requestData.Data.PDF;
            DateTime? dt = requestData.Data.PDT;
            TimeSpan? ts = (dt - df);
            if ((ts?.Days > 2))
            {
                return false;
            }

            return true;
        }

        //private DataTable DailyData(ServerRequestBase<ReportYieldReportDailyDatasRequestItem> requestData)
        //{
        //    DataSet result1 = DailyProdData(requestData);
        //    if ((result1 == null) || (result1.Tables.Count == 0) || (result1.Tables[0] == null))
        //    {
        //        return null;
        //    }

        //    DataSet result2 = DailyWIPData(requestData);
        //    if ((!(result1 == null) && !(result1.Tables.Count == 0) && !(result1.Tables[0] == null)) && (!(result2 == null) && !(result2.Tables.Count == 0) && !(result2.Tables[0] == null)))
        //    {
        //        DataTable dt = result1.Tables[0].Clone();
        //        DataRow row;
        //        DataRow row1;
        //        DataRow row2;
        //        foreach (DataRow row1 in result1.Tables[0].Rows)
        //        {
        //            row = dt.NewRow();
        //            row["Model"] = row1["Model"];
        //            row["ProcessName"] = row1["ProcessName"];
        //            row["SUMA"] = row1["SUMA"];
        //            row["NUM"] = row1["NUM"];
        //            row["DayA"] = row1["DayA"];
        //            row["ShopFloor"] = row1["ShopFloor"];
        //            foreach (DataRow row2 in result2.Tables[0].Rows)
        //            {
        //                if ((row["Model"] == row2["Model"]) && (row["ProcessName"] == row2["ProcessName"]) && (row["DayA"] == row2["DayA"]) && (row["ShopFloor"] == row2["ShopFloor"]))
        //                {
        //                    int suma1 = 0;
        //                    int suma2 = 0;
        //                    if (!Equals(row["SUMA"], System.DBNull.Value))
        //                    {
        //                        suma1 = ((int)(row["SUMA"]));
        //                    }
        //                    if (!Equals(row2["SUMA"], System.DBNull.Value))
        //                    {
        //                        suma2 = ((int)(row2["SUMA"]));
        //                    }
        //                    suma1 = suma1 + suma2;
        //                    row["SUMA"] = suma1.ToString();
        //                    break;
        //                }

        //                Equals(row2["SUMA"], System.DBNull.Value);
        //                suma2 = ((int)(row2["SUMA"]));
        //                if ((suma1
        //                            == (suma1 + suma2)))
        //                {
        //                    row["SUMA"] = suma1.ToString;
        //                    break;
        //                }

        //            }

        //            dt.Rows.Add(row);
        //        }

        //        foreach (row1 in result2.Tables[0].Rows)
        //        {
        //            row = dt.NewRow();
        //            row["Model"] = row1["Model"];
        //            row["ProcessName"] = row1["ProcessName"];
        //            row["SUMA"] = row1["SUMA"];
        //            row["NUM"] = row1["NUM"];
        //            row["DayA"] = row1["DayA"];
        //            row["ShopFloor"] = row1["ShopFloor"];
        //            bool flag = false;
        //            foreach (row2 in result1.Tables[0].Rows)
        //            {
        //                if (((row["Model"] == row2["Model"])
        //                            && ((row["ProcessName"] == row2["ProcessName"])
        //                            && ((row["DayA"] == row2["DayA"])
        //                            && (row["ShopFloor"] == row2["ShopFloor"])))))
        //                {
        //                    flag = true;
        //                    break;
        //                }

        //            }

        //            if ((flag == false))
        //            {
        //                dt.Rows.Add(row);
        //            }
        //        }
        //        return dt;
        //    }
        //    else if (!(result1 == null) && !(result1.Tables.Count == 0) && !(result1.Tables[0] == null))
        //    { 
        //        return result1.Tables[0];
        //    }
        //    else if (!(result2 == null) && !(result2.Tables.Count == 0) && !(result2.Tables[0] == null))
        //    { 
        //        return result2.Tables[0];
        //    }
        //    else
        //    {
        //        return null;
        //    }

        //}

        private DataTable DailyData(ServerRequestBase<ReportYieldReportDailyDatasRequestItem> requestData)
        {
            DataSet result1 = DailyProdData(requestData);
            if ((result1 == null || result1.Tables.Count == 0 || result1.Tables[0] == null))
                return null/* TODO Change to default(_) if this is not a reference type */;
            DataSet result2 = DailyWIPData(requestData);
            if ((result1 != null && result1.Tables.Count > 0 && result1.Tables[0] != null && result1.Tables[0].Rows.Count > 0) && (result2 != null && result2.Tables.Count > 0 && result2.Tables[0] != null && result2.Tables[0].Rows.Count > 0))
            {
                DataTable dt = result1.Tables[0].Clone();
                DataRow row;
                DataRow row1;
                DataRow row2;
                foreach (DataRow dr in result1.Tables[0].Rows)
                {
                    row = dt.NewRow();
                    row["Model"] = dr["Model"];
                    row["ProcessName"] = dr["ProcessName"];
                    row["SUMA"] = dr["SUMA"];
                    row["NUM"] = dr["NUM"];
                    row["DayA"] = dr["DayA"];
                    row["ShopFloor"] = dr["ShopFloor"];
                    foreach (DataRow dr2 in result2.Tables[0].Rows)
                    {
                        if (row["Model"].ToString() == dr2["Model"].ToString() && row["ProcessName"].ToString() == dr2["ProcessName"].ToString() && row["DayA"].ToString() == dr2["DayA"].ToString() && row["ShopFloor"].ToString() == dr2["ShopFloor"].ToString())
                        {
                            int suma1 = 0;
                            int suma2 = 0;
                            if (!(object.Equals(row["SUMA"], null) || object.Equals(row["SUMA"], System.DBNull.Value)))
                                suma1 = System.Convert.ToInt32(row["SUMA"]);
                            if (!(object.Equals(dr2["SUMA"], null) || object.Equals(dr2["SUMA"], System.DBNull.Value)))
                                suma2 = System.Convert.ToInt32(dr2["SUMA"]);
                            suma1 = suma1 + suma2;
                            row["SUMA"] = suma1.ToString();
                            break;
                        }
                    }
                    dt.Rows.Add(row);
                }
                foreach (DataRow dr in result2.Tables[0].Rows)
                {
                    row = dt.NewRow();
                    row["Model"] = dr["Model"];
                    row["ProcessName"] = dr["ProcessName"];
                    row["SUMA"] = dr["SUMA"];
                    row["NUM"] = dr["NUM"];
                    row["DayA"] = dr["DayA"];
                    row["ShopFloor"] = dr["ShopFloor"];
                    bool flag = false;
                    foreach (DataRow dr2 in result1.Tables[0].Rows)
                    {
                        if (row["Model"].ToString() == dr2["Model"].ToString() && row["ProcessName"].ToString() == dr2["ProcessName"].ToString() && row["DayA"].ToString() == dr2["DayA"].ToString() && row["ShopFloor"].ToString() == dr2["ShopFloor"].ToString())
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag == false)
                        dt.Rows.Add(row);
                }
                return dt;
            }
            else if ((result1 != null && result1.Tables.Count > 0 && result1.Tables[0] != null && result1.Tables[0].Rows.Count > 0))
                return result1.Tables[0];
            else if ((result2 != null && result2.Tables.Count > 0 && result2.Tables[0] != null && result2.Tables[0].Rows.Count > 0))
                return result2.Tables[0];
            else
                return null;
        }

        private DataSet DailyProdData(ServerRequestBase<ReportYieldReportDailyDatasRequestItem> requestData)
        {
            string cond = "";
            string sql = "";
            string sql1 = "";
            string sql2 = "";
            string sql3 = "";
            string sql4 = "";
            string sql5 = "";
            string sql6 = "";
            string sql7 = "";
            string sql8 = "";

            if (requestData.Data.Station != null && requestData.Data.Station != "")
            {
                cond = cond + " and VT.ProcessName in ('" + requestData.Data.Station.Replace(",", "','") + "') ";
                sql7 = " and VT.ProcessName = '" + requestData.Data.Station.Replace(",", "','") + "' ";
                sql6 = " and T_RepItem.TestStation ='" + requestData.Data.Station.Replace(",", "','") + "' ";
            }
            if (requestData.Data.Model != null && requestData.Data.Model != "")
            {
                cond = cond + " and VT.Model in ('" + requestData.Data.Model.Replace(",", "','") + "') ";
                sql2 = " and VT.Model in ('" + requestData.Data.Model.Replace(",", "','") + "') ";
                sql1 = " and T_RepHeader.Model in ('" + requestData.Data.Model.Replace(",", "','") + "') ";
            }
            if (requestData.Data.PDF != null && requestData.Data.PDT != null)
            {
                string PDF, PDT;
                PDF = requestData.Data.PDF?.ToString("yyyy-MM-dd");
                PDT = requestData.Data.PDT?.ToString("yyyy-MM-dd");

                cond = cond + " and (VT.ProdDate between '" + PDF + " 00:00:00 AM' and '" + PDT + " 11:59:59 PM')";
                sql5 = " and (T_RepItem.FailTime between '" + PDF + " 00:00:00 AM' and '" + PDT + " 11:59:59 PM')";
                sql4 = " and " + "(VT.ProdDate between '" + PDF.Replace("/", "-") + "' and '" + PDT.Replace("/", "-") + "')";
            }

            cond = cond.Replace("'", "");

            var ds = new DataSet();
            var thisConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["eTraceConnection"]);
            var thisAdapter1 = new SqlDataAdapter(sql, thisConnection);

            //DataSet ds = new DataSet();
            //SqlConnection thisConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("eTraceConnection").ConnectionString);
            //SqlDataAdapter thisAdapter1 = new SqlDataAdapter(sql, thisConnection);

            try
            {
                thisAdapter1.SelectCommand.CommandType = CommandType.StoredProcedure;
                thisAdapter1.SelectCommand.CommandText = "dbo.sp_GetYieldReportRrt_v3";
                thisAdapter1.SelectCommand.CommandTimeout = 6000;
                thisAdapter1.SelectCommand.Parameters.Add("@DayType", SqlDbType.VarChar, 50);
                thisAdapter1.SelectCommand.Parameters["@DayType"].Value = "Daily";
                thisAdapter1.SelectCommand.Parameters.Add("@condition1", SqlDbType.VarChar, 500);
                thisAdapter1.SelectCommand.Parameters.Add("@condition2", SqlDbType.VarChar, 500);
                thisAdapter1.SelectCommand.Parameters.Add("@condition3", SqlDbType.VarChar, 500);
                thisAdapter1.SelectCommand.Parameters.Add("@condition4", SqlDbType.VarChar, 500);
                thisAdapter1.SelectCommand.Parameters.Add("@condition5", SqlDbType.VarChar, 500);
                thisAdapter1.SelectCommand.Parameters.Add("@condition6", SqlDbType.VarChar, 50);
                thisAdapter1.SelectCommand.Parameters.Add("@condition7", SqlDbType.VarChar, 500);
                thisAdapter1.SelectCommand.Parameters.Add("@condition8", SqlDbType.VarChar, 500);
                thisAdapter1.SelectCommand.Parameters["@condition1"].Value = sql1;
                thisAdapter1.SelectCommand.Parameters["@condition2"].Value = sql2;
                thisAdapter1.SelectCommand.Parameters["@condition3"].Value = sql3;
                thisAdapter1.SelectCommand.Parameters["@condition4"].Value = sql4;
                thisAdapter1.SelectCommand.Parameters["@condition5"].Value = sql5;
                thisAdapter1.SelectCommand.Parameters["@condition6"].Value = sql6;
                thisAdapter1.SelectCommand.Parameters["@condition7"].Value = sql7;
                thisAdapter1.SelectCommand.Parameters["@condition8"].Value = sql8;
                thisAdapter1.SelectCommand.Connection.Open();
                thisAdapter1.Fill(ds, "DataSource");
                thisAdapter1.SelectCommand.Connection.Close();
            }
            catch (Exception e)
            {
                thisAdapter1.SelectCommand.Connection.Close();
                return null;
            }
            if (thisConnection.State != ConnectionState.Closed)
                thisConnection.Close();

            return ds;
        }

        private DataSet DailyWIPData(ServerRequestBase<ReportYieldReportDailyDatasRequestItem> requestData)
        {
            string cond = "";
            string sql = "";
            string sql1 = "";
            string sql2 = "";
            string sql3 = "";
            string sql4 = "";
            string sql5 = "";
            string sql6 = "";
            string sql7 = "";
            string sql8 = "";

            if (requestData.Data.Station != null && requestData.Data.Station != "")
            {
                cond = cond + " and VT.ProcessName in ('" + requestData.Data.Station.Replace(",", "','") + "') ";
                sql7 = " and VT.ProcessName = '" + requestData.Data.Station.Replace(",", "','") + "' ";
                sql6 = " and T_RepItem.TestStation ='" + requestData.Data.Station.Replace(",", "','") + "' ";
            }
            if (requestData.Data.Model != null && requestData.Data.Model != "")
            {
                cond = cond + " and VT.Model in ('" + requestData.Data.Model.Replace(",", "','") + "') ";
                sql2 = " and VT.Model in ('" + requestData.Data.Model.Replace(",", "','") + "') ";
                sql1 = " and T_RepHeader.Model in ('" + requestData.Data.Model.Replace(",", "','") + "') ";
            }
            if (requestData.Data.PDF.ToString() != null && requestData.Data.PDT.ToString() != null && requestData.Data.PDF.ToString() != "" && requestData.Data.PDT.ToString() != "")
            {
                string PDF, PDT;
                PDF = requestData.Data.PDF?.ToString("yyyy-MM-dd");
                PDT = requestData.Data.PDT?.ToString("yyyy-MM-dd");

                cond = cond + " and (VT.ProdDate between '" + PDF + " 00:00:00 AM' and '" + PDT + " 11:59:59 PM')";
                sql5 = " and (T_RepItem.FailTime between '" + PDF + " 00:00:00 AM' and '" + PDT + " 11:59:59 PM')";
                sql4 = " and " + "(VT.ProdDate between '" + PDF.Replace("/", "-") + "' and '" + PDT.Replace("/", "-") + "')";
            }

            cond = cond.Replace("'", "");
            // common.WriteReportLogs(sessionID, "Yield Report 3", "IP: " + Me.Request.UserHostAddress + "  ComputerName: " + System.Net.Dns.Resolve(Request.UserHostAddress).HostName, cond, "Daily")

            var ds = new DataSet();
            var thisConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["eTraceConnection"]);
            var thisAdapter1 = new SqlDataAdapter(sql, thisConnection);

            //DataSet ds = new DataSet();
            //SqlConnection thisConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("eTraceConnection").ConnectionString);
            //SqlDataAdapter thisAdapter1 = new SqlDataAdapter(sql, thisConnection);

            try
            {
                thisAdapter1.SelectCommand.CommandType = CommandType.StoredProcedure;
                thisAdapter1.SelectCommand.CommandText = "dbo.sp_GetYieldReportRrt_v4";
                thisAdapter1.SelectCommand.CommandTimeout = 6000;
                thisAdapter1.SelectCommand.Parameters.Add("@DayType", SqlDbType.VarChar, 50);
                thisAdapter1.SelectCommand.Parameters["@DayType"].Value = "Daily";
                thisAdapter1.SelectCommand.Parameters.Add("@condition1", SqlDbType.VarChar, 500);
                thisAdapter1.SelectCommand.Parameters.Add("@condition2", SqlDbType.VarChar, 500);
                thisAdapter1.SelectCommand.Parameters.Add("@condition3", SqlDbType.VarChar, 500);
                thisAdapter1.SelectCommand.Parameters.Add("@condition4", SqlDbType.VarChar, 500);
                thisAdapter1.SelectCommand.Parameters.Add("@condition5", SqlDbType.VarChar, 500);
                thisAdapter1.SelectCommand.Parameters.Add("@condition6", SqlDbType.VarChar, 50);
                thisAdapter1.SelectCommand.Parameters.Add("@condition7", SqlDbType.VarChar, 500);
                thisAdapter1.SelectCommand.Parameters.Add("@condition8", SqlDbType.VarChar, 500);
                thisAdapter1.SelectCommand.Parameters["@condition1"].Value = sql1;
                thisAdapter1.SelectCommand.Parameters["@condition2"].Value = sql2;
                thisAdapter1.SelectCommand.Parameters["@condition3"].Value = sql3;
                thisAdapter1.SelectCommand.Parameters["@condition4"].Value = sql4;
                thisAdapter1.SelectCommand.Parameters["@condition5"].Value = sql5;
                thisAdapter1.SelectCommand.Parameters["@condition6"].Value = sql6;
                thisAdapter1.SelectCommand.Parameters["@condition7"].Value = sql7;
                thisAdapter1.SelectCommand.Parameters["@condition8"].Value = sql8;
                thisAdapter1.SelectCommand.Connection.Open();
                thisAdapter1.Fill(ds, "DataSource");
                thisAdapter1.SelectCommand.Connection.Close();
            }
            catch (Exception e)
            {
                thisAdapter1.SelectCommand.Connection.Close();
            }
            if (thisConnection.State != ConnectionState.Closed)
                thisConnection.Close();
            return ds;
        }

        private DataTable GetDailyStatistic(DataTable dt)
        {
            if (dt == null)
                return null;
            else
            {
                var dt2 = new DataTable("Daily");
                var dcModel = new DataColumn("Model", Type.GetType("System.String"));
                dt2.Columns.Add(dcModel);
                var dcStation = new DataColumn("Station", Type.GetType("System.String"));
                dt2.Columns.Add(dcStation);
                var dcTotal = new DataColumn("Total", Type.GetType("System.String"));
                dt2.Columns.Add(dcTotal);
                var dcSuccess = new DataColumn("Success", Type.GetType("System.String"));
                dt2.Columns.Add(dcSuccess);
                var dcFailed = new DataColumn("Failed", Type.GetType("System.String"));
                dt2.Columns.Add(dcFailed);
                var dcYield = new DataColumn("Yield", Type.GetType("System.String"));
                dt2.Columns.Add(dcYield);
                var dcPPM = new DataColumn("PPM", Type.GetType("System.String"));
                dt2.Columns.Add(dcPPM);
                var dcDate = new DataColumn("ProdDate", Type.GetType("System.String"));
                dt2.Columns.Add(dcDate);
                var dcFloor = new DataColumn("Floor", Type.GetType("System.String"));
                dt2.Columns.Add(dcFloor);
                var dcDescription = new DataColumn("Description", Type.GetType("System.String"));
                dt2.Columns.Add(dcDescription);
                var dcBusinessUnit = new DataColumn("BusinessUnit", Type.GetType("System.String"));
                dt2.Columns.Add(dcBusinessUnit);
                var dcPower = new DataColumn("Power", Type.GetType("System.String"));
                dt2.Columns.Add(dcPower);
                var dcVoltageType = new DataColumn("VoltageType", Type.GetType("System.String"));
                dt2.Columns.Add(dcVoltageType);

                foreach (DataRow drow in dt.Rows)
                {
                    int suma = 0;
                    int num = 0;
                    int total = 0;

                    DataRow dr = dt2.NewRow();
                    dr["Model"] = drow["Model"];
                    dr["Station"] = drow["ProcessName"];
                    if (!(object.Equals(drow["SUMA"], null) || object.Equals(drow["SUMA"], DBNull.Value)))
                        suma = int.Parse(drow["SUMA"].ToString());
                    if (!(object.Equals(drow["NUM"], null) || object.Equals(drow["NUM"], DBNull.Value)))
                        num = int.Parse(drow["NUM"].ToString());

                    if (num > suma)
                    {
                        dr["Total"] = num;
                        dr["Success"] = num - num;
                        dr["Failed"] = num;
                        dr["PPM"] = double.Parse(double.Parse("1").ToString("0.####")) * 1000000;
                    }
                    else
                    {
                        dr["Total"] = suma;
                        dr["Success"] = suma - num;
                        dr["Failed"] = num;
                        if (num == 0)
                            dr["PPM"] = double.Parse(double.Parse("0").ToString("0.####")) * 1000000;
                        else
                            dr["PPM"] = double.Parse((double.Parse(num.ToString()) / double.Parse(suma.ToString())).ToString("0.####")) * 1000000;
                    }

                    dr["Yield"] = double.Parse((double.Parse(dr["Success"].ToString()) / double.Parse(dr["Total"].ToString())).ToString("0.####")) * 100;

                    dr["ProdDate"] = drow["DayA"];
                    dr["Floor"] = drow["ShopFloor"];
                    dr["Description"] = string.Empty;
                    dr["BusinessUnit"] = string.Empty;
                    dr["Power"] = string.Empty;
                    dr["VoltageType"] = string.Empty;

                    dt2.Rows.Add(dr);
                }

                return dt2;
            }
        }

        private DataTable GetProductMasterData(ServerRequestBase<ReportYieldReportDailyDatasRequestItem> requestData)
        {
            string cond4 = "";
            if (requestData.Data.Model != null && requestData.Data.Model != "")
            {
                cond4 = (cond4 + (" and " + ("B.Model in (\'" + (requestData.Data.Model.Replace(",", "\',\'") + "\') "))));
            }

            DataSet ds4 = new DataSet();
            string sql4 = (" SELECT B.Model, B.Description, B.BusinessUnit, B.Power, B.VotageType AS VoltageType FROM V_ProductMa" +
            "ster AS B WITH (nolock) " + (" WHERE 1=1 " + cond4));
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

        private void GetResult(ref DataTable dtStatistic, DataTable dtProductMaster)
        {
            int i = 0;
            string lastModel = "xxx";
            string description = String.Empty;
            string bussinessUnit = String.Empty;
            string power = String.Empty;
            string voltageType = String.Empty;

            for (i = 0; (i <= (dtStatistic.Rows.Count - 1)); i++)
            {
                if ((dtStatistic.Rows[i]["Model"].ToString() != lastModel))
                {
                    this.GetPMData(dtStatistic.Rows[i]["Model"].ToString(), dtProductMaster, ref description, ref bussinessUnit, ref power, ref voltageType);
                    dtStatistic.Rows[i]["Description"] = description;
                    dtStatistic.Rows[i]["BusinessUnit"] = bussinessUnit;
                    dtStatistic.Rows[i]["Power"] = power;
                    dtStatistic.Rows[i]["VoltageType"] = voltageType;
                    lastModel = dtStatistic.Rows[i]["Model"].ToString();
                }
                else
                {
                    dtStatistic.Rows[i]["Description"] = description;
                    dtStatistic.Rows[i]["BusinessUnit"] = bussinessUnit;
                    dtStatistic.Rows[i]["Power"] = power;
                    dtStatistic.Rows[i]["VoltageType"] = voltageType;
                }
            }
        }

        private void GetPMData(string model, DataTable dt, ref string description, ref string bussinessUnit, ref string power, ref string voltageType)
        {
            description = String.Empty;
            bussinessUnit = String.Empty;
            power = String.Empty;
            voltageType = String.Empty;
            foreach (DataRow drow in dt.Rows)
            {
                if ((drow["Model"].ToString() == model))
                {
                    description = drow["Description"].ToString();
                    bussinessUnit = drow["BusinessUnit"].ToString();
                    power = drow["Power"].ToString();
                    voltageType = drow["VoltageType"].ToString();
                    break;
                }
            }
        }

        private DataTable DailyProcess(ServerRequestBase<ReportYieldReportDailyDatasRequestItem> requestData)
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
                DataTable dtProductMasterData = GetProductMasterData(requestData, msg);
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
                DataTable dtStatistic = GetDailyStatisticData(dtTestData);
                msg = " run GetDailyResultData ";
                DataTable result = RptQualityData.VBGetQualityData.GetDailyResultData(dtStatistic, dtRepairData, dtProductMasterData);
                return result;
            }
            catch (Exception ex)
            {
                msg = msg + " - " + ex.Message;
                return null/* TODO Change to default(_) if this is not a reference type */;
            }
        }

        private DataTable GetDailyTestData(ServerRequestBase<ReportYieldReportDailyDatasRequestItem> requestData, string msg)
        {
            string cond1 = "";
            if (requestData.Data.Model != null && requestData.Data.Model != "")
                cond1 = cond1 + " and " + "T1.Model in ('" + requestData.Data.Model.Replace(",", "','") + "') ";
            if (requestData.Data.PDF != null && requestData.Data.PDT != null)
            {
                string PDF, PDT;
                PDF = requestData.Data.PDF?.ToString("yyyy/MM/dd");
                PDT = requestData.Data.PDT?.ToString("yyyy/MM/dd");
                cond1 = cond1 + " and " + "(T1.ProdDate between '" + PDF + " 00:00:00 AM' and '" + PDT + " 11:59:59 PM')";
            }
            if (requestData.Data.Station != null && requestData.Data.Station != "")
                cond1 = cond1 + " and " + "T1.ProcessName in ('" + requestData.Data.Station.Replace(",", "','") + "') ";


            DataSet ds1 = new DataSet();

            string sql1 = " SELECT     A.Model, A.PO, CONVERT(varchar(10), A.Proddate, 120) AS ProdDate, A.PCBA, A.ProcessName, 1 AS ANumber, "
                                + " dbo.af_getZSFloor(B.ProductionLine) AS ShopFloor "
                                + " FROM "
                                + " (SELECT     T1.ExtSerialNo, T1.Model, T1.PO, MIN(T1.ProdDate) AS Proddate, T1.PCBA, T1.ProcessName "
                                + " FROM         dbo.T_TDHeader AS T1 WITH (nolock index=ix_proddate index=ix_model) "
                                + " WHERE     (T1.SeqNo = 1) "
                                + cond1
                                + " GROUP BY T1.ExtSerialNo, T1.Model, T1.PO, T1.PCBA, T1.ProcessName) AS A "
                                + " INNER JOIN dbo.T_Shippment AS B WITH (nolock) ON A.ExtSerialNo = B.ProductSerialNo "
                                + " WHERE 1=1 AND EXISTS (SELECT 1 FROM T_PRODUCTSTRUCTURE as PS WITH(NOLOCK) WHERE PS.PCBA = A.PCBA AND PS.IsTLA = 1 ) ";

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

        private DataTable GetDailyWIPData(ServerRequestBase<ReportYieldReportDailyDatasRequestItem> requestData, string msg)
        {
            string cond2 = "";
            if (requestData.Data.Model != null && requestData.Data.Model != "")
                cond2 = cond2 + " and " + "T1.Model in ('" + requestData.Data.Model.Replace(",", "','") + "') ";
            if (requestData.Data.PDF != null && requestData.Data.PDT != null)
            {
                string PDF, PDT;
                PDF = requestData.Data.PDF?.ToString("yyyy/MM/dd");
                PDT = requestData.Data.PDT?.ToString("yyyy/MM/dd");
                cond2 = cond2 + " and " + "(T1.ProdDate between '" + PDF + " 00:00:00 AM' and '" + PDT + " 11:59:59 PM')";
            }
            if (requestData.Data.Station != null && requestData.Data.Station != "")
                cond2 = cond2 + " and " + "T1.ProcessName in ('" + requestData.Data.Station.Replace(",", "','") + "') ";

            DataSet ds2 = new DataSet();

            string sql2 = " SELECT     T1.Model, T1.PO, CONVERT(varchar(10), T1.ProdDate, 120) AS ProdDate, T1.PCBA, T1.ProcessName, 1 AS ANumber, "
                                + " dbo.af_getZSFloor(D .ProdLine) AS ShopFloor "
                                + " FROM         dbo.T_WIPTDHeader AS T1 WITH (nolock) "
                                + " INNER JOIN dbo.T_WIPHeader AS D WITH (nolock) ON T1.WIPID = D.WIPID "
                                + " WHERE     (T1.SeqNo = 1) "
                                + "  AND EXISTS (SELECT 1 FROM T_PRODUCTSTRUCTURE as PS WITH(NOLOCK) WHERE PS.PCBA = T1.PCBA AND PS.IsTLA = 1 ) "
                                + cond2;

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

        private DataTable GetDailyRepairData(ServerRequestBase<ReportYieldReportDailyDatasRequestItem> requestData, string msg)
        {
            string cond3_1 = "";
            string cond3_2 = "";

            if (requestData.Data.Model != null && requestData.Data.Model != "")
                cond3_1 = cond3_1 + " and " + "T_RepHeader.Model in ('" + requestData.Data.Model.Replace(",", "','") + "') ";
            if (requestData.Data.PDF != null && requestData.Data.PDT != null)
            {
                string PDF, PDT;
                PDF = requestData.Data.PDF?.ToString("yyyy/MM/dd");
                PDT = requestData.Data.PDT?.ToString("yyyy/MM/dd");
                cond3_1 = cond3_1 + " and " + "(T_RepItem.FailTime between '" + PDF + " 00:00:00 AM' and '" + PDT + " 11:59:59 PM')";
            }
            if (requestData.Data.Station != null && requestData.Data.Station != "")
                cond3_1 = cond3_1 + " and " + "T_RepItem.TestStation in ('" + requestData.Data.Station.Replace(",", "','") + "') ";


            DataSet ds3 = new DataSet();

            string sql3 = " SELECT A.NUM, A.Model, A.TestStation, A.DayB, A.Floor "
                                + " FROM (SELECT COUNT(*) AS NUM, T_RepHeader.Model, T_RepItem.TestStation, CONVERT(char(10), T_RepItem.FailTime, 120) AS DayB, "
                                + " dbo.af_getZSFloor(Floor) as Floor "
                                + " FROM  T_RepHeader WITH (nolock) INNER JOIN T_RepItem WITH (nolock) ON T_RepHeader.RepID = T_RepItem.RepID "
                                + " WHERE 1=1 "
                                + cond3_1
                                + " AND NOT EXISTS "
                                + " (SELECT 'x' FROM T_RepItem AS ri WITH (nolock) WHERE (ri.RepID = T_RepItem.RepID) AND (T_RepItem.Item > ri.Item) AND (T_RepItem.TestStation = ri.TestStation)) "
                                + " GROUP BY T_RepHeader.Model, T_RepItem.TestStation, "
                                + " dbo.af_getZSFloor(Floor), "
                                + " CONVERT(char(10), T_RepItem.FailTime, 120)) AS A "
                                + " WHERE 1 = 1 " + cond3_2;

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
                msg = msg + " - " + ex.Message;
                return null/* TODO Change to default(_) if this is not a reference type */;
            }

            return ds3.Tables["RepairData"];
        }

        private DataTable GetProductMasterData(ServerRequestBase<ReportYieldReportDailyDatasRequestItem> requestData, string msg)
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
            DataRow[] findrows;

            findrows = dt.AsEnumerable().ToArray();

            //            var query = _
            //                From product In findrows _
            //                Group product By PP = New With {
            //                Key.model = product.Field(Of String)("Model").ToUpper(), Key.processname = product.Field(Of String)("ProcessName").ToUpper(), _
            //                Key.proddate = product.Field(Of String)("ProdDate").ToUpper(), Key.shopfloor = product.Field(Of String)("ShopFloor").ToUpper()}
            //            Into g = Group _
            //Select New With {.Model = PP.model, .Proddate = PP.proddate, .processName = PP.processname, .ShopFloor = PP.shopfloor, _
            //             .SUMA = g.Sum(Function(product) product.Field(Of Integer)("ANumber"))}
            var query = from product in dt.AsEnumerable()
                        group product by new { Model = product.Field<string>("Model"), ProcessName = product.Field<string>("ProcessName"), ProdDate = product.Field<string>("ProdDate"), ShopFloor = product.Field<string>("ShopFloor") } into m
                        select new
                        {
                            Model = m.Key.Model,
                            ProcessName = m.Key.ProcessName,
                            ProdDate = m.Key.ProdDate,
                            ShopFloor = m.Key.ShopFloor,
                            SUMA = m.Sum(n => n.Field<int>("ANumber"))
                        };

            DataTable dt1 = new DataTable("dtStatistic");
            DataColumn dcModel = new DataColumn("Model", System.Type.GetType("System.String"));
            dt1.Columns.Add(dcModel);
            DataColumn dcProcessName = new DataColumn("ProcessName", System.Type.GetType("System.String"));
            dt1.Columns.Add(dcProcessName);
            DataColumn dcProdDate = new DataColumn("ProdDate", System.Type.GetType("System.String"));
            dt1.Columns.Add(dcProdDate);
            DataColumn dcShopFloor = new DataColumn("ShopFloor", System.Type.GetType("System.String"));
            dt1.Columns.Add(dcShopFloor);
            DataColumn dcNumber = new DataColumn("SUMA", System.Type.GetType("System.Int32"));
            dt1.Columns.Add(dcNumber);

            foreach (var item in query)
            {
                DataRow dr = dt1.NewRow();
                dr["Model"] = item.Model;
                dr["ProcessName"] = item.ProcessName;
                dr["ProdDate"] = item.ProdDate;
                dr["ShopFloor"] = item.ShopFloor;
                dr["SUMA"] = item.SUMA;
                dt1.Rows.Add(dr);
            }

            return dt1;
        }
    }
}
