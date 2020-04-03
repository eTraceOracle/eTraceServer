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
    /// ReportRawMatPakStatusController
    /// </summary>
    public class ReportRawMatPakStatusController : ServerBaseController<ReportRawMatPakStatusController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportRawMatPakStatusController()
        {
        }

        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetRawMatPakStatusTotalCount(DownLoadRawMatPakStatusRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();

            ReportRawMatPakStatusModelQuery queryModel = GetModelQuery<DownLoadRawMatPakStatusRequest, ReportRawMatPakStatusModelQuery>(requestData);

            long rowCount = RawMatPakStatusBLL.Instance.RawMatPakStatusDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }

        /// <summary>
        /// 获取下载 CLID Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadRawMatPakStatusData([FromBody] DownLoadRawMatPakStatusRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportRawMatPakStatusModelQuery queryModel = GetModelQuery<DownLoadRawMatPakStatusRequest, ReportRawMatPakStatusModelQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     //RawMatPakStatusBLL.Instance.RawMatPakStatusDataGetRowCount,
                                                     RawMatPakStatusBLL.Instance.GetRawMatPakStatusData,
                                                       "RawMatPakStatus");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }



        /// <summary>
        /// 获取 CLID Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportRawMatPakStatusResponse GetRawMatPakStatusData([FromBody] ReportRawMatPakStatusRequest requestData)
        {
            ReportRawMatPakStatusResponse response = GetBusinessResponseDataInited<ReportRawMatPakStatusResponse>();
            try
            {
                ReportRawMatPakStatusModelQuery queryModel = GetModelQuery<ReportRawMatPakStatusRequest, ReportRawMatPakStatusModelQuery>(requestData);

                ReportRawMatPakStatusModel dbDatas = RawMatPakStatusBLL.Instance.GetRawMatPakStatusData(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportRawMatPakStatusResponse.Item();
                            data.CopyModelValueFrom<ReportRawMatPakStatusResponse.Item, ReportRawMatPakStatusModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportRawMatPakStatusResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
                    }
                    if (dbDatas.Pager != null)
                    {
                        response.Pager = new ResponsePager()
                        {
                            TotalCount = dbDatas.Pager.TotalCount,
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                return GetResponseError<ReportRawMatPakStatusResponse>(ref response, ex, EmBussinesCodeType.ReportInternalError);
            }
            return response;
        }




    }
}
