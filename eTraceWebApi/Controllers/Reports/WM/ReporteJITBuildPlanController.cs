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
    /// ReporteJITBuildPlanController
    /// </summary>
    public class ReporteJITBuildPlanController : ServerBaseController<ReporteJITBuildPlanController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReporteJITBuildPlanController()
        {
        }

        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GeteJITBuildPlanDetailTotalCount(DownLoadeJITBuildPlanDetailRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();

            ReporteJITBuildPlanModelQuery queryModel = GetModelQuery<DownLoadeJITBuildPlanDetailRequest, ReporteJITBuildPlanModelQuery>(requestData);

            long rowCount = eJITBuildPlanBLL.Instance.eJITBuildPlanDetailDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);

        }

        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GeteJITBuildPlanSearchTotalCount(DownLoadeJITBuildPlanSearchRequest requestData)
        {
            var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();

            ReporteJITBuildPlanModelQuery queryModel = GetModelQuery<DownLoadeJITBuildPlanSearchRequest, ReporteJITBuildPlanModelQuery>(requestData);

            long rowCount = eJITBuildPlanBLL.Instance.eJITBuildPlanSearchDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }


        /// <summary>
        /// 获取下载 Detail Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadeJITBuildPlanDetailData([FromBody] DownLoadeJITBuildPlanDetailRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReporteJITBuildPlanModelQuery queryModel = GetModelQuery<DownLoadeJITBuildPlanDetailRequest, ReporteJITBuildPlanModelQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     //eJITBuildPlanBLL.Instance.eJITBuildPlanDetailDataGetRowCount,
                                                     eJITBuildPlanBLL.Instance.GeteJITBuildPlanDetailData,
                                                       "eJITBuildPlanDetail");

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
        public IHttpActionResult DownloadeJITBuildPlanSearchData([FromBody] DownLoadeJITBuildPlanSearchRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReporteJITBuildPlanModelQuery queryModel = GetModelQuery<DownLoadeJITBuildPlanSearchRequest, ReporteJITBuildPlanModelQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     //eJITBuildPlanBLL.Instance.eJITBuildPlanSearchDataGetRowCount,
                                                     eJITBuildPlanBLL.Instance.GeteJITBuildPlanSearchData,
                                                       "eJITBuildPlanSummary");

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
        public ReporteJITBuildPlanSearchResponse GeteJITBuildPlanSearchData([FromBody] ReporteJITBuildPlanRequest requestData)
        {
            ReporteJITBuildPlanSearchResponse response = GetBusinessResponseDataInited<ReporteJITBuildPlanSearchResponse>();
            try
            {
                ReporteJITBuildPlanModelQuery queryModel = GetModelQuery<ReporteJITBuildPlanRequest, ReporteJITBuildPlanModelQuery>(requestData);

                ReporteJITBuildPlanSearchModel dbDatas = eJITBuildPlanBLL.Instance.GeteJITBuildPlanSearchByPage(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReporteJITBuildPlanSearchResponse.Item();
                            data.CopyModelValueFrom<ReporteJITBuildPlanSearchResponse.Item, ReporteJITBuildPlanSearchModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReporteJITBuildPlanSearchResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
                //pocceed in GlobleExceptionFilter
                throw ex;
            }
            return response;
        }

        


        /// <summary>
        /// 获取 CLID Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReporteJITBuildPlanDetailResponse GeteJITBuildPlanDetailData([FromBody] ReporteJITBuildPlanRequest requestData)
        {
            ReporteJITBuildPlanDetailResponse response = GetBusinessResponseDataInited<ReporteJITBuildPlanDetailResponse>();
            try
            {
                ReporteJITBuildPlanModelQuery queryModel = GetModelQuery<ReporteJITBuildPlanRequest, ReporteJITBuildPlanModelQuery>(requestData);

                ReporteJITBuildPlanDetailModel dbDatas = eJITBuildPlanBLL.Instance.GeteJITBuildPlanDetailByPage(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReporteJITBuildPlanDetailResponse.Item();
                            data.CopyModelValueFrom<ReporteJITBuildPlanDetailResponse.Item, ReporteJITBuildPlanDetailModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReporteJITBuildPlanDetailResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
                //pocceed in GlobleExceptionFilter
                throw ex;
            }
            return response;
        }


    }
}
