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
    /// ReportRawMatPackingController
    /// </summary>
    public class ReportRawMatPackingController : ServerBaseController<ReportRawMatPackingController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportRawMatPackingController()
        {
        }

        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetRawMatPackingTotalCount(DownLoadRawMatPackingRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();

            ReportRawMatPackingModelQuery queryModel = GetModelQuery<DownLoadRawMatPackingRequest, ReportRawMatPackingModelQuery>(requestData);

            long rowCount = RawMatPackingBLL.Instance.RawMatPackingDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }

        /// <summary>
        /// 获取下载 CLID Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadRawMatPackingData([FromBody] DownLoadRawMatPackingRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportRawMatPackingModelQuery queryModel = GetModelQuery<DownLoadRawMatPackingRequest, ReportRawMatPackingModelQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     //RawMatPackingBLL.Instance.RawMatPackingDataGetRowCount,
                                                     RawMatPackingBLL.Instance.GetRawMatPackingData,
                                                       "RawMatPacking");

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
        public ReportRawMatPackingResponse GetRawMatPackingData([FromBody] ReportRawMatPackingRequest requestData)
        {
            ReportRawMatPackingResponse response = GetBusinessResponseDataInited<ReportRawMatPackingResponse>();
            try
            {
                ReportRawMatPackingModelQuery queryModel = GetModelQuery<ReportRawMatPackingRequest, ReportRawMatPackingModelQuery>(requestData);

                ReportRawMatPackingModel dbDatas = RawMatPackingBLL.Instance.GetRawMatPackingData(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportRawMatPackingResponse.Item();
                            data.CopyModelValueFrom<ReportRawMatPackingResponse.Item, ReportRawMatPackingModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportRawMatPackingResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
                return GetResponseError<ReportRawMatPackingResponse>(ref response, ex, EmBussinesCodeType.ReportInternalError);
            }
            return response;
        }




    }
}
