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
    /// ReportFGAgingController
    /// </summary>
    public class ReportFGAgingController : ServerBaseController<ReportFGAgingController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportFGAgingController()
        {
        }


        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetFGAgingDetailTotalCount(DownLoadFGAgingDetailRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();

            ReportFGAgingModelQuery queryModel = GetModelQuery<DownLoadFGAgingDetailRequest, ReportFGAgingModelQuery>(requestData);
            queryModel.ReportType = "Detail";

            long rowCount = FGAgingBLL.Instance.FGAgingDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }

        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetFGAgingSummaryTotalCount(DownLoadFGAgingSummaryRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();

            ReportFGAgingModelQuery queryModel = GetModelQuery<DownLoadFGAgingSummaryRequest, ReportFGAgingModelQuery>(requestData);
            queryModel.ReportType = "Summary";

            long rowCount = FGAgingBLL.Instance.FGAgingDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }


        /// <summary>
        /// 获取下载 Detail Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadFGAgingDetailData([FromBody] DownLoadFGAgingDetailRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportFGAgingModelQuery queryModel = GetModelQuery<DownLoadFGAgingDetailRequest, ReportFGAgingModelQuery>(requestData);
                queryModel.ReportType = "Detail";

                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     //FGAgingBLL.Instance.FGAgingDataGetRowCount,
                                                     FGAgingBLL.Instance.GetFGAgingDetailData,
                                                       "FGAgingDetail");

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
        public IHttpActionResult DownloadFGAgingSummaryData([FromBody] DownLoadFGAgingSummaryRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportFGAgingModelQuery queryModel = GetModelQuery<DownLoadFGAgingSummaryRequest, ReportFGAgingModelQuery>(requestData);
                queryModel.ReportType = "Summary";

                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     //FGAgingBLL.Instance.FGAgingDataGetRowCount,
                                                     FGAgingBLL.Instance.GetFGAgingSummaryData,
                                                       "FGAgingSummary");

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
        public ReportFGAgingDetailResponse GetFGAgingDetailData([FromBody] ReportFGAgingRequest requestData)
        {
            ReportFGAgingDetailResponse response = GetBusinessResponseDataInited<ReportFGAgingDetailResponse>();
            try
            {
                ReportFGAgingModelQuery queryModel = GetModelQuery<ReportFGAgingRequest, ReportFGAgingModelQuery>(requestData);
                queryModel.ReportType = "Detail";

                ReportFGAgingDetailModel dbDatas = FGAgingBLL.Instance.GetFGAgingDetailData(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportFGAgingDetailResponse.Detail();
                            data.CopyModelValueFrom<ReportFGAgingDetailResponse.Detail, ReportFGAgingDetailModel.Detail>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportFGAgingDetailResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
                return GetResponseError<ReportFGAgingDetailResponse>(ref response, ex, EmBussinesCodeType.ReportInternalError);
            }
            return response;
        }


        /// <summary>
        /// 获取 Summary Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportFGAgingSummaryResponse GetFGAgingSummaryData([FromBody] ReportFGAgingRequest requestData)
        {
            ReportFGAgingSummaryResponse response = GetBusinessResponseDataInited<ReportFGAgingSummaryResponse>();
            try
            {
                ReportFGAgingModelQuery queryModel = GetModelQuery<ReportFGAgingRequest, ReportFGAgingModelQuery>(requestData);
                queryModel.ReportType = "Summary";

                ReportFGAgingSummaryModel dbDatas = FGAgingBLL.Instance.GetFGAgingSummaryData(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportFGAgingSummaryResponse.Summary();
                            data.CopyModelValueFrom<ReportFGAgingSummaryResponse.Summary, ReportFGAgingSummaryModel.Summary>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportFGAgingSummaryResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
                return GetResponseError<ReportFGAgingSummaryResponse>(ref response, ex, EmBussinesCodeType.ReportInternalError);
            }
            return response;
        }

 
    }
}
