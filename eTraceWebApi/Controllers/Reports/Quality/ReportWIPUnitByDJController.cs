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

namespace eTraceWebApi.Controllers
{
    /// <summary>
    /// ReportProductModuleController
    /// </summary>
    public class ReportWIPUnitByDJController : ServerBaseController<ReportWIPUnitByDJController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportWIPUnitByDJController()
        {

        }

        /// <summary>
        /// 获取下载Equipment数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadComponentUsedRptByDJ([FromBody] DownloadWIPUnitByDJDatasRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportWIPUnitByDJQuery queryModel = GetModelQuery<DownloadWIPUnitByDJDatasRequest, ReportWIPUnitByDJQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,
                                                         requestData.TableHeaders,
                                                         WIPUnitByDJBLL.Instance.WIPUnitByDJDataGetRowCount,
                                                         WIPUnitByDJBLL.Instance.GetWIPUnitByDJData,
                                                           "WIPUnitByDJ");

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
        public ReportCheckRowsCountResponse GetWIPUnitByDJTotalCount(DownloadWIPUnitByDJDatasRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
            ReportWIPUnitByDJQuery queryModel = GetModelQuery<DownloadWIPUnitByDJDatasRequest, ReportWIPUnitByDJQuery>(requestData);
            long rowCount = WIPUnitByDJBLL.Instance.WIPUnitByDJDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }

        /// <summary>
        /// 获取设备数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportWIPUnitByDJResponse GetWIPUnitByDJData([FromBody] ReportWIPUnitByDJRequest requestData)
        {          
            //List<string> data = new List<string>();
            //data.AsEnumerable()
            //data.Skip(10).Take()
            //requestData.Pager.

            ReportWIPUnitByDJResponse response = new ReportWIPUnitByDJResponse();
            ReportWIPUnitByDJQuery queryModel = GetModelQuery<ReportWIPUnitByDJRequest, ReportWIPUnitByDJQuery>(requestData);

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
            //response = new ReportWIPUnitByDJResponse() {
            //    BussinesCode = EmBussinesCodeType.Success,
            //    Data = new List<ReportWIPUnitByDJResponse.Item>();

            //};

            List<string> dbDJ = WIPUnitByDJBLL.Instance.GetDJ(queryModel.IntSN);
            if (dbDJ.Count>0)
            {
                ReportWIPUnitByDJModel dbDatas = WIPUnitByDJBLL.Instance.GetWIPUnitByDJData(queryModel);
                response= GetReportQueryResponse<ReportWIPUnitByDJResponse,
                                                ReportWIPUnitByDJResponse.Item,
                                                ReportWIPUnitByDJModel.Item>(dbDatas);
            }
            else
            {
                response = GetResponseError(ref  response, EmBussinesCodeType.DJNotFound );
            }


            return response;
        }

        /// <summary>
        /// 获取设备状态下拉
        /// </summary>
        /// <returns></returns>
        /// 

        //[HttpGet]
        //public ServerResponse<List<string>> GetDJ( string IntSN)   //ReportWIPUnitByDJRequest requestData
        //{
        //    //ServerResponse<List<string>> response = new ServerResponse<List<string>>();
        //    ServerResponse<List<string>> response = GetBusinessResponseDataInited<ServerResponse<List<string>>>();
        //    var dbDatas = WIPUnitByDJBLL.Instance.GetDJ(IntSN);
        //    response.Data = dbDatas;
        //    response.BussinesCode = EmBussinesCodeType.Success;
        //    return response;
        //}

        [HttpPost]
        public ServerResponse<List<string>> GetDJ([FromBody] ReportWIPUnitByDJRequest requestData)  //ReportWIPUnitByDJRequest requestData
        {
            //ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            ServerResponse<List<string>> response = GetBusinessResponseDataInited<ServerResponse<List<string>>>();
            var dbDatas = WIPUnitByDJBLL.Instance.GetDJ(requestData.Data.IntSN);
            response.Data = dbDatas;
            response.BussinesCode = EmBussinesCodeType.Success;
            return response;
        }

    }
}
