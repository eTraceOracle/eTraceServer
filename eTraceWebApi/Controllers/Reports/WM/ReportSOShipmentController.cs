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
    /// ReportSOShipmentController
    /// </summary>
    public class ReportSOShipmentController : ServerBaseController<ReportSOShipmentController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportSOShipmentController()
        {
        }


        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetSOShipmentDetailTotalCount(DownLoadSOShipmentDetailRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();

            ReportSOShipmentModelQuery queryModel = GetModelQuery<DownLoadSOShipmentDetailRequest, ReportSOShipmentModelQuery>(requestData);
            queryModel.ReportType = "Detail";

            long rowCount = SOShipmentBLL.Instance.SOShipmentDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }

        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetSOShipmentSummaryTotalCount(DownLoadSOShipmentSummaryRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();

            ReportSOShipmentModelQuery queryModel = GetModelQuery<DownLoadSOShipmentSummaryRequest, ReportSOShipmentModelQuery>(requestData);
            queryModel.ReportType = "Summary";

            long rowCount = SOShipmentBLL.Instance.SOShipmentDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }


        /// <summary>
        /// 获取下载 Detail Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadSOShipmentDetailData([FromBody] DownLoadSOShipmentDetailRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportSOShipmentModelQuery queryModel = GetModelQuery<DownLoadSOShipmentDetailRequest, ReportSOShipmentModelQuery>(requestData);
                queryModel.ReportType = "Detail";

                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     //SOShipmentBLL.Instance.SOShipmentDataGetRowCount,
                                                     SOShipmentBLL.Instance.GetSOShipmentDetailData,
                                                       "SOShipmentDetail");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }


        /// <summary>
        /// 获取下载 Summary Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadSOShipmentSummaryData([FromBody] DownLoadSOShipmentSummaryRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportSOShipmentModelQuery queryModel = GetModelQuery<DownLoadSOShipmentSummaryRequest, ReportSOShipmentModelQuery>(requestData);
                queryModel.ReportType = "Summary";

                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     //SOShipmentBLL.Instance.SOShipmentDataGetRowCount,
                                                     SOShipmentBLL.Instance.GetSOShipmentSummaryData,
                                                       "SOShipmentSummary");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }


        /// <summary>
        /// 获取 Detail Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportSOShipmentDetailResponse GetSOShipmentDetailData([FromBody] ReportSOShipmentRequest requestData)
        {
            ReportSOShipmentDetailResponse response = GetBusinessResponseDataInited<ReportSOShipmentDetailResponse>();
            try
            {
                ReportSOShipmentModelQuery queryModel = GetModelQuery<ReportSOShipmentRequest, ReportSOShipmentModelQuery>(requestData);
                queryModel.ReportType = "Detail";

                ReportSOShipmentDetailModel dbDatas = SOShipmentBLL.Instance.GetSOShipmentDetailData(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSOShipmentDetailResponse.Detail();
                            data.CopyModelValueFrom<ReportSOShipmentDetailResponse.Detail, ReportSOShipmentDetailModel.Detail>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSOShipmentDetailResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
                return GetResponseError<ReportSOShipmentDetailResponse>(ref response, ex, EmBussinesCodeType.ReportInternalError);
            }
            return response;
        }


        /// <summary>
        /// 获取 Summary Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportSOShipmentSummaryResponse GetSOShipmentSummaryData([FromBody] ReportSOShipmentRequest requestData)
        {
            ReportSOShipmentSummaryResponse response = GetBusinessResponseDataInited<ReportSOShipmentSummaryResponse>();
            try
            {
                ReportSOShipmentModelQuery queryModel = GetModelQuery<ReportSOShipmentRequest, ReportSOShipmentModelQuery>(requestData);
                queryModel.ReportType = "Summary";

                ReportSOShipmentSummaryModel dbDatas = SOShipmentBLL.Instance.GetSOShipmentSummaryData(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSOShipmentSummaryResponse.Summary();
                            data.CopyModelValueFrom<ReportSOShipmentSummaryResponse.Summary, ReportSOShipmentSummaryModel.Summary>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSOShipmentSummaryResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
                return GetResponseError<ReportSOShipmentSummaryResponse>(ref response, ex, EmBussinesCodeType.ReportInternalError);
            }
            return response;
        }


    }
}
