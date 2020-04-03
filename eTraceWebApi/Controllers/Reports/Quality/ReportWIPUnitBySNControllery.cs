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
    public class ReportWIPUnitBySNController : ServerBaseController<ReportWIPUnitBySNController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportWIPUnitBySNController()
        {

        }

        /// <summary>
        /// 获取下载Equipment数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadComponentUsedRptBySN([FromBody] DownloadWIPUnitBySNDatasRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportWIPUnitBySNQuery queryModel = GetModelQuery<DownloadWIPUnitBySNDatasRequest, ReportWIPUnitBySNQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,
                                                         requestData.TableHeaders,
                                                         WIPUnitBySNBLL.Instance.WIPUnitBySNDataGetRowCount,
                                                         WIPUnitBySNBLL.Instance.GetWIPUnitBySNData,
                                                           "WIPUnitBySN");

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
        public ReportCheckRowsCountResponse GetWIPUnitBySNTotalCount(DownloadWIPUnitBySNDatasRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
            ReportWIPUnitBySNQuery queryModel = GetModelQuery<DownloadWIPUnitBySNDatasRequest, ReportWIPUnitBySNQuery>(requestData);
            long rowCount = WIPUnitBySNBLL.Instance.WIPUnitBySNDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }

        /// <summary>
        /// 获取设备数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportWIPUnitBySNResponse GetWIPUnitBySNData([FromBody] ReportWIPUnitBySNRequest requestData)
        {
            ReportWIPUnitBySNResponse response = new ReportWIPUnitBySNResponse();
            ReportWIPUnitBySNQuery queryModel = GetModelQuery<ReportWIPUnitBySNRequest, ReportWIPUnitBySNQuery>(requestData);

            List<string> dbDJ = WIPUnitBySNBLL.Instance.GetWIPID(queryModel.IntSN);
            if (dbDJ.Count>0)
            {
                ReportWIPUnitBySNModel dbDatas = WIPUnitBySNBLL.Instance.GetWIPUnitBySNData(queryModel);
                response= GetReportQueryResponse<ReportWIPUnitBySNResponse,
                                                ReportWIPUnitBySNResponse.Item,
                                                ReportWIPUnitBySNModel.Item>(dbDatas);            
            }
            else
            {
                response = GetResponseError(ref  response, EmBussinesCodeType.NoDataFound );
            }

            return response;
        }

        /// <summary>
        /// 获取设备状态下拉
        /// </summary>
        /// <returns></returns>
        /// 

        //[HttpGet]
        //public ServerResponse<List<string>> GetWIPID( string IntSN)   //ReportWIPUnitBySNRequest requestData
        //{
        //    //ServerResponse<List<string>> response = new ServerResponse<List<string>>();
        //    ServerResponse<List<string>> response = GetBusinessResponseDataInited<ServerResponse<List<string>>>();
        //    var dbDatas = WIPUnitBySNBLL.Instance.GetWIPID(IntSN);
        //    response.Data = dbDatas;
        //    response.BussinesCode = EmBussinesCodeType.Success;
        //    return response;
        //}

        [HttpPost]
        public ServerResponse<List<string>> GetWIPID([FromBody] ReportWIPUnitBySNRequest requestData)  //ReportWIPUnitBySNRequest requestData
        {
            //ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            ServerResponse<List<string>> response = GetBusinessResponseDataInited<ServerResponse<List<string>>>();
            var dbDatas = WIPUnitBySNBLL.Instance.GetWIPID(requestData.Data.IntSN);
            response.Data = dbDatas;
            response.BussinesCode = EmBussinesCodeType.Success;
            return response;
        }

    }
}
