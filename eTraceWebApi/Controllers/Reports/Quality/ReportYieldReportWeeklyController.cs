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
    public class ReportYieldReportWeeklyController : ServerBaseController<ReportYieldReportWeeklyController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportYieldReportWeeklyController()
        {

        }

        /// <summary>
        /// 获取下载Equipment数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadYieldReportWeeklyDatas([FromBody] DownloadYieldReportWeeklyDatasRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportYieldReportWeeklyQuery queryModel = GetModelQuery<DownloadYieldReportWeeklyDatasRequest, ReportYieldReportWeeklyQuery>(requestData);

                DataTable dtWeekly;
                dtWeekly = null;

                DataTable tmp = WeeklyData(requestData);
                if (tmp == null)
                {
                    fullResponse = Request.CreateResponse(HttpStatusCode.NoContent);
                    return ResponseMessage(fullResponse);
                }

                DataTable statistic = GetWeeklyStatistic(tmp);
                DataTable pm = GetProductMasterData(requestData);
                if (statistic != null && pm != null)
                {
                    GetResult(ref statistic, pm);
                    statistic.DefaultView.Sort = "Model ASC";
                    dtWeekly = statistic.Copy();
                }


                //fullResponse = DownloadReportResponse(queryModel,
                //                                         requestData.TableHeaders,
                //                                         WIPUnitByDJBLL.Instance.WIPUnitByDJDataGetRowCount,
                //                                         WIPUnitByDJBLL.Instance.GetWIPUnitByDJData,
                //                                           "");

                long rowCount = dtWeekly.Rows.Count;
                if (rowCount > 0 && rowCount < MaxDownloadRowCount)
                {
                    queryModel.Pager = null;

                    //var dbDatas = GetDataFunc(queryModel);
                    dtWeekly.Columns.Remove("Year");
                    fullResponse = GetDownloadExcelResponse(dtWeekly, requestData.TableHeaders, "WeeklyYieldReport");

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
        public ReportCheckRowsCountResponse GetYieldReportWeeklyDataTotalCount(DownloadYieldReportWeeklyDatasRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
            ReportYieldReportWeeklyQuery queryModel = GetModelQuery<DownloadYieldReportWeeklyDatasRequest, ReportYieldReportWeeklyQuery>(requestData);
            long rowCount = 0;

            DataTable dtWeekly;
            dtWeekly = null;

            DataTable tmp = WeeklyData(requestData);
            if (tmp == null)
            {
                rowCount = 0;
            }
            else
            {
                DataTable statistic = GetWeeklyStatistic(tmp);
                DataTable pm = GetProductMasterData(requestData);
                if (statistic != null && pm != null)
                {
                    GetResult(ref statistic, pm);
                    statistic.DefaultView.Sort = "Model ASC";
                    dtWeekly = statistic.Copy();
                    rowCount = dtWeekly.Rows.Count;
                }
            }

            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }

        private class ReportYieldReportWeeklyQuery : ModelQueryBase
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
        public ReportYieldReportWeeklyResponse GetYieldReportWeeklyDatas([FromBody] ReportYieldReportWeeklyRequest requestData)
        {
            //List<string> data = new List<string>();
            //data.AsEnumerable()
            //data.Skip(10).Take()
            //requestData.Pager.

            ReportYieldReportWeeklyResponse response = new ReportYieldReportWeeklyResponse();

            response.BussinesCode = EmBussinesCodeType.Success;
            List<ReportYieldReportWeeklyResponse.Item> dataList = new List<ReportYieldReportWeeklyResponse.Item>();

            DataTable dtWeekly;
            dtWeekly = null;

            DataTable tmp = WeeklyData(requestData);
            if (tmp == null)
            {
                response = GetResponseError(ref response, EmBussinesCodeType.NoDataFound);
                return response;
            }

            DataTable statistic = GetWeeklyStatistic(tmp);
            DataTable pm = GetProductMasterData(requestData);
            if (statistic != null && pm != null)
            {
                GetResult(ref statistic, pm);
                statistic.DefaultView.Sort = "Model ASC";
                dtWeekly = statistic.Copy();
            }

            if (dtWeekly != null && dtWeekly.Rows.Count > 0)
            {
                int skipRow, returnRow;
                skipRow = (requestData.Pager.CurrentPage - 1) * requestData.Pager.PageSize;
                returnRow = (skipRow + requestData.Pager.PageSize) > dtWeekly.Rows.Count ? dtWeekly.Rows.Count : (skipRow + requestData.Pager.PageSize);

                decimal demo;
                //DateTime demoTime;

                for (int i = skipRow; i < returnRow; i++)
                {
                    dataList.Add(new ReportYieldReportWeeklyResponse.Item()
                    {
                        Model = dtWeekly.Rows[i]["Model"].ToString(),
                        Station = dtWeekly.Rows[i]["Station"].ToString(),
                        Total = decimal.TryParse(dtWeekly.Rows[i]["Total"].ToString(), out demo) ? demo : 0,
                        Success = decimal.TryParse(dtWeekly.Rows[i]["Success"].ToString(), out demo) ? demo : 0,
                        Failed = decimal.TryParse(dtWeekly.Rows[i]["Failed"].ToString(), out demo) ? demo : 0,
                        Yield = decimal.TryParse(dtWeekly.Rows[i]["Yield"].ToString(), out demo) ? demo : 0,
                        PPM = decimal.TryParse(dtWeekly.Rows[i]["PPM"].ToString(), out demo) ? demo : 0,
                        Week = dtWeekly.Rows[i]["Week"].ToString(),

                        Floor = dtWeekly.Rows[i]["Floor"].ToString(),
                        Description = dtWeekly.Rows[i]["Description"].ToString(),
                        BusinessUnit = dtWeekly.Rows[i]["BusinessUnit"].ToString(),
                        Power = dtWeekly.Rows[i]["Power"].ToString(),
                        VoltageType = dtWeekly.Rows[i]["VoltageType"].ToString()
                    });
                }
                response.Data = dataList;

                response.Pager = new ResponsePager()
                {
                    TotalCount = dtWeekly.Rows.Count,
                    //TotalCount = (int)Math.Ceiling((double)dtWeekly.Rows.Count / requestData.Pager.PageSize),
                };
            }
            else
            {
                response = GetResponseError(ref response, EmBussinesCodeType.NoDataFound);
            }
            return response;
        }

        private DataTable WeeklyData(ServerRequestBase<ReportYieldReportWeeklyDatasRequestItem> requestData)
        {
            DataSet result1 = WeeklyProdData(requestData);

            if (result1 == null || result1.Tables.Count == 0 || result1.Tables[0] == null)
                return null;
            DataSet result2 = WeeklyWIPData(requestData);
            if (result1 != null && result1.Tables.Count > 0 && result1.Tables[0] != null && result1.Tables[0].Rows.Count > 0 && result2 != null && result2.Tables.Count > 0 && result2.Tables[0] != null && result2.Tables[0].Rows.Count > 0)
            {
                DataTable dt = result1.Tables[0].Clone();
                DataRow row;

                foreach (DataRow row1 in result1.Tables[0].Rows)
                {
                    row = dt.NewRow();
                    row["Model"] = row1["Model"];
                    row["ProcessName"] = row1["ProcessName"];
                    row["SUMA"] = row1["SUMA"];
                    row["SUMB"] = row1["SUMB"];
                    row["YearA"] = row1["YearA"];
                    row["WeekA"] = row1["WeekA"];
                    row["ShopFloor"] = row1["ShopFloor"];
                    foreach (DataRow row2 in result2.Tables[0].Rows)
                    {
                        if (row["Model"].ToString() == row2["Model"].ToString() && row["ProcessName"].ToString() == row2["ProcessName"].ToString() && row["YearA"].ToString() == row2["YearA"].ToString() && row["WeekA"].ToString() == row2["WeekA"].ToString() && row["ShopFloor"].ToString() == row2["ShopFloor"].ToString())
                        {
                            int suma1 = 0;
                            int suma2 = 0;
                            if (!(object.Equals(row["SUMA"], null) || object.Equals(row["SUMA"], DBNull.Value)))
                                suma1 = System.Convert.ToInt32(row["SUMA"]);
                            if (!(object.Equals(row2["SUMA"], null) || object.Equals(row2["SUMA"], DBNull.Value)))
                                suma2 = System.Convert.ToInt32(row2["SUMA"]);
                            suma1 = suma1 + suma2;
                            row["SUMA"] = suma1.ToString();
                            break;
                        }
                    }
                    dt.Rows.Add(row);
                }
                foreach (DataRow row1 in result2.Tables[0].Rows)
                {
                    row = dt.NewRow();
                    row["Model"] = row1["Model"];
                    row["ProcessName"] = row1["ProcessName"];
                    row["SUMA"] = row1["SUMA"];
                    row["SUMB"] = row1["SUMB"];
                    row["YearA"] = row1["YearA"];
                    row["WeekA"] = row1["WeekA"];
                    row["ShopFloor"] = row1["ShopFloor"];
                    bool flag = false;
                    foreach (DataRow row2 in result1.Tables[0].Rows)
                    {
                        if (row["Model"].ToString() == row2["Model"].ToString() && row["ProcessName"].ToString() == row2["ProcessName"].ToString() && row["YearA"].ToString() == row2["YearA"].ToString() && row["WeekA"].ToString() == row2["WeekA"].ToString() && row["ShopFloor"].ToString() == row2["ShopFloor"].ToString())
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
            else if (result1 != null && result1.Tables.Count > 0 && result1.Tables[0] != null && result1.Tables[0].Rows.Count > 0)
                return result1.Tables[0];
            else if (result2 != null && result2.Tables.Count > 0 && result2.Tables[0] != null && result2.Tables[0].Rows.Count > 0)
                return result2.Tables[0];
            else
                return null;
        }

        private DataSet WeeklyProdData(ServerRequestBase<ReportYieldReportWeeklyDatasRequestItem> requestData)
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
                cond = cond + " and ProcessName in ('" + requestData.Data.Station.Replace(",", "','") + "') ";
                sql7 = " and ProcessName = '" + requestData.Data.Station.Replace(",", "','") + "' ";
                sql6 = " and T_RepItem.TestStation ='" + requestData.Data.Station.Replace(",", "','") + "' ";
            }
            if (requestData.Data.Model != null && requestData.Data.Model != "")
            {
                cond = cond + " and Model in ('" + requestData.Data.Model.Replace(",", "','") + "') ";
                sql2 = " and Model in ('" + requestData.Data.Model.Replace(",", "','") + "') ";
                sql1 = " and T_RepHeader.Model in ('" + requestData.Data.Model.Replace(",", "','") + "') ";
            }
            if (requestData.Data.PDF != null && requestData.Data.PDT != null)
            {
                string PDF, PDT;
                PDF = requestData.Data.PDF?.ToString("yyyy-MM-dd");
                PDT = requestData.Data.PDT?.ToString("yyyy-MM-dd");
                cond = cond + " and (ProdDate between '" + PDF + " 00:00:00 AM' and '" + PDT + " 11:59:59 PM')";
                sql5 = " and (T_RepItem.FailTime between '" + PDF + " 00:00:00 AM' and '" + PDT + " 11:59:59 PM')";
                sql4 = " and " + "(ProdDate between '" + PDF.Replace("/", "-") + "' and '" + PDT.Replace("/", "-") + "')";
            }

            cond = cond.Replace("'", "");

            var ds = new DataSet();
            var thisConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["eTraceConnection"]);
            var thisAdapter1 = new SqlDataAdapter(sql, thisConnection);

            try
            {
                thisAdapter1.SelectCommand.CommandType = CommandType.StoredProcedure;
                thisAdapter1.SelectCommand.CommandText = "dbo.sp_GetYieldReportRrt_v3";
                thisAdapter1.SelectCommand.CommandTimeout = 6000;
                thisAdapter1.SelectCommand.Parameters.Add("@DayType", SqlDbType.VarChar, 50);
                thisAdapter1.SelectCommand.Parameters["@DayType"].Value = "Weekly";
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

        private DataSet WeeklyWIPData(ServerRequestBase<ReportYieldReportWeeklyDatasRequestItem> requestData)
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
                cond = cond + " and ProcessName in ('" + requestData.Data.Station.Replace(",", "','") + "') ";
                sql7 = " and ProcessName = '" + requestData.Data.Station.Replace(",", "','") + "' ";
                sql6 = " and T_RepItem.TestStation ='" + requestData.Data.Station.Replace(",", "','") + "' ";
            }
            if (requestData.Data.Model != null && requestData.Data.Model != "")
            {
                cond = cond + " and Model in ('" + requestData.Data.Model.Replace(",", "','") + "') ";
                sql2 = " and Model in ('" + requestData.Data.Model.Replace(",", "','") + "') ";
                sql1 = " and T_RepHeader.Model in ('" + requestData.Data.Model.Replace(",", "','") + "') ";
            }
            if (requestData.Data.PDF.ToString() != null && requestData.Data.PDT.ToString() != null && requestData.Data.PDF.ToString() != "" && requestData.Data.PDT.ToString() != "")
            {
                string PDF, PDT;
                PDF = requestData.Data.PDF?.ToString("yyyy-MM-dd");
                PDT = requestData.Data.PDT?.ToString("yyyy-MM-dd");

                cond = cond + " and (ProdDate between '" + PDF + " 00:00:00 AM' and '" + PDT + " 11:59:59 PM')";
                sql5 = " and (T_RepItem.FailTime between '" + PDF + " 00:00:00 AM' and '" + PDT + " 11:59:59 PM')";
                sql4 = " and " + "(ProdDate between '" + PDF.Replace("/", "-") + "' and '" + PDT.Replace("/", "-") + "')";
            }

            cond = cond.Replace("'", "");
            // common.WriteReportLogs(sessionID, "Yield Report 3", "IP: " + Me.Request.UserHostAddress + "  ComputerName: " + System.Net.Dns.Resolve(Request.UserHostAddress).HostName, cond, "Daily")

            var ds = new DataSet();
            var thisConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["eTraceConnection"]);
            var thisAdapter1 = new SqlDataAdapter(sql, thisConnection);

            try
            {
                thisAdapter1.SelectCommand.CommandType = CommandType.StoredProcedure;
                thisAdapter1.SelectCommand.CommandText = "dbo.sp_GetYieldReportRrt_v4";
                thisAdapter1.SelectCommand.CommandTimeout = 6000;
                thisAdapter1.SelectCommand.Parameters.Add("@DayType", SqlDbType.VarChar, 50);
                thisAdapter1.SelectCommand.Parameters["@DayType"].Value = "Weekly";
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

        private DataTable GetWeeklyStatistic(DataTable dt)
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
                var dcWeek = new DataColumn("Week", Type.GetType("System.String"));
                dt2.Columns.Add(dcWeek);
                var dcYear = new DataColumn("Year", Type.GetType("System.String"));
                dt2.Columns.Add(dcYear);
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

                foreach (DataRow row in dt.Rows)
                {
                    int suma = 0;
                    int num = 0;
                    int total = 0;

                    DataRow dr = dt2.NewRow();
                    dr["Model"] = row["Model"];
                    dr["Station"] = row["ProcessName"];
                    if (!(object.Equals(row["SUMA"], null) || object.Equals(row["SUMA"], DBNull.Value)))
                        suma = int.Parse(row["SUMA"].ToString());
                    if (!(object.Equals(row["SUMB"], null) || object.Equals(row["SUMB"], DBNull.Value)))
                        num = int.Parse(row["SUMB"].ToString());
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
                    dr["Week"] = row["YearA"].ToString() + "-" + row["WeekA"].ToString();
                    dr["Floor"] = row["ShopFloor"];
                    dr["Description"] = string.Empty;
                    dr["BusinessUnit"] = string.Empty;
                    dr["Power"] = string.Empty;
                    dr["VoltageType"] = string.Empty;

                    dt2.Rows.Add(dr);
                }
                return dt2;
            }
        }

        private DataTable GetProductMasterData(ServerRequestBase<ReportYieldReportWeeklyDatasRequestItem> requestData)
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

    }
}
