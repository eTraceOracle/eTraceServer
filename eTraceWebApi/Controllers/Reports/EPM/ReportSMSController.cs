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
    /// ReportSMSController
    /// </summary>
    public class ReportSMSController : ServerBaseController<ReportSMSController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportSMSController()
        {

        }

        /// <summary>
        /// 获取下载 SMS Summary Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadSMSSearchData([FromBody] DownLoadSMSSearchRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportSMSModelQuery queryModel = GetModelQuery<DownLoadSMSSearchRequest, ReportSMSModelQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     SMSBLL.Instance.SMSSummaryDataGetRowCount,
                                                     SMSBLL.Instance.GetSMSSummaryData,
                                                     "SMSReport_Sum");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }



        /// <summary>
        /// 获取 SMS Summary Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportSMSSearchResponse GetSMSSearchData([FromBody] ReportSMSRequest requestData)
        {
            ReportSMSSearchResponse response = GetBusinessResponseDataInited<ReportSMSSearchResponse>();
            try
            {
                ReportSMSModelQuery queryModel = GetModelQuery<ReportSMSRequest, ReportSMSModelQuery>(requestData);

                ReportSMSSummaryModel dbDatas = SMSBLL.Instance.GetSMSSummaryData(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMSSearchResponse.Item();
                            data.CopyModelValueFrom<ReportSMSSearchResponse.Item, ReportSMSSummaryModel.Summary>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMSSearchResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
                throw ex;
                //return GetResponseError<ReportSMSSearchResponse>(ref response, ex, EmBussinesCodeType.ReportInternalError);
            }
            return response;
        }



        /// <summary>
        /// 获取下载 SMS Detail Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadSMSDetailData([FromBody] DownLoadSMSDetailRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportSMSModelQuery queryModel = GetModelQuery<DownLoadSMSDetailRequest, ReportSMSModelQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     SMSBLL.Instance.SMSDetailDataGetRowCount,
                                                     SMSBLL.Instance.GetSMSDetailData,
                                                     "SMSReport_Detail");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }



        /// <summary>
        /// 获取 SMS Detail Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportSMSDetailResponse GetSMSDetailData([FromBody] ReportSMSRequest requestData)
        {
            ReportSMSDetailResponse response = GetBusinessResponseDataInited<ReportSMSDetailResponse>();
            try
            {
                ReportSMSModelQuery queryModel = GetModelQuery<ReportSMSRequest, ReportSMSModelQuery>(requestData);

                ReportSMSDetailModel dbDatas = SMSBLL.Instance.GetSMSDetailData(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMSDetailResponse.Item();
                            data.CopyModelValueFrom<ReportSMSDetailResponse.Item, ReportSMSDetailModel.Detail>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMSDetailResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
                throw ex;
                //return GetResponseError<ReportSMSDetailResponse>(ref response, ex, EmBussinesCodeType.ReportInternalError);
            }
            return response;
        }


    }
}
