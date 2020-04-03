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
    /// ReportComponentsUsedController
    /// </summary>
    public class ReportComponentsUsedController : ServerBaseController<ReportComponentsUsedController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportComponentsUsedController()
        {
        }

        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetComponentsUsedTotalCount(DownLoadComponentsUsedRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();

            ReportComponentsUsedModelQuery queryModel = GetModelQuery<DownLoadComponentsUsedRequest, ReportComponentsUsedModelQuery>(requestData);
            queryModel.ReportType = "Current";

            long rowCount = ComponentsUsedBLL.Instance.ComponentsUsedDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }

        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetComponentsUsedArchiveTotalCount(DownLoadComponentsUsedRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();

            ReportComponentsUsedModelQuery queryModel = GetModelQuery<DownLoadComponentsUsedRequest, ReportComponentsUsedModelQuery>(requestData);
            queryModel.ReportType = "Archive";

            long rowCount = ComponentsUsedBLL.Instance.ComponentsUsedDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }

        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetComponentsUsedTLATotalCount(DownLoadComponentsUsedRequest requestData)
        {
            var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();

            ReportComponentsUsedModelQuery queryModel = GetModelQuery<DownLoadComponentsUsedRequest, ReportComponentsUsedModelQuery>(requestData);
            queryModel.ReportType = "TLADetail";

            long rowCount = ComponentsUsedBLL.Instance.ComponentsUsedDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }

        /// <summary>
        /// 获取下载 CLID Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadComponentsUsedData([FromBody] DownLoadComponentsUsedRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportComponentsUsedModelQuery queryModel = GetModelQuery<DownLoadComponentsUsedRequest, ReportComponentsUsedModelQuery>(requestData);
                queryModel.ReportType = "Current";

                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     //ComponentsUsedBLL.Instance.ComponentsUsedDataGetRowCount,
                                                     ComponentsUsedBLL.Instance.GetComponentsUsedData,
                                                       "ComponentsUsedInDJ");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }


        /// <summary>
        /// 获取下载 Archive Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadComponentsUsedArchiveData([FromBody] DownLoadComponentsUsedRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportComponentsUsedModelQuery queryModel = GetModelQuery<DownLoadComponentsUsedRequest, ReportComponentsUsedModelQuery>(requestData);
                queryModel.ReportType = "Archive";

                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     //ComponentsUsedBLL.Instance.ComponentsUsedDataGetRowCount,
                                                     ComponentsUsedBLL.Instance.GetComponentsUsedData,
                                                       "ComponentsUsedInDJ_Archive");

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
        public ReportComponentsUsedResponse GetComponentsUsedData([FromBody] ReportComponentsUsedRequest requestData)
        {
            ReportComponentsUsedResponse response = GetBusinessResponseDataInited<ReportComponentsUsedResponse>();
            try
            {
                ReportComponentsUsedModelQuery queryModel = GetModelQuery<ReportComponentsUsedRequest, ReportComponentsUsedModelQuery>(requestData);
                queryModel.ReportType = "Current";

                ReportComponentsUsedModel dbDatas = ComponentsUsedBLL.Instance.GetComponentsUsedByPage(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportComponentsUsedResponse.Item();
                            data.CopyModelValueFrom<ReportComponentsUsedResponse.Item, ReportComponentsUsedModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportComponentsUsedResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        /// 获取 CLID Archive Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportComponentsUsedResponse GetComponentsUsedArchiveData([FromBody] ReportComponentsUsedRequest requestData)
        {
            ReportComponentsUsedResponse response = GetBusinessResponseDataInited<ReportComponentsUsedResponse>();
            try
            {
                ReportComponentsUsedModelQuery queryModel = GetModelQuery<ReportComponentsUsedRequest, ReportComponentsUsedModelQuery>(requestData);
                queryModel.ReportType = "Archive";

                ReportComponentsUsedModel dbDatas = ComponentsUsedBLL.Instance.GetComponentsUsedByPage(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportComponentsUsedResponse.Item();
                            data.CopyModelValueFrom<ReportComponentsUsedResponse.Item, ReportComponentsUsedModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportComponentsUsedResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        /// 下载 Components Used TLA Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadComponentsUsedTLAData([FromBody] DownLoadComponentsUsedRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportComponentsUsedModelQuery queryModel = GetModelQuery<DownLoadComponentsUsedRequest, ReportComponentsUsedModelQuery>(requestData);
                queryModel.ReportType = "TLADetail";

                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     //ComponentsUsedBLL.Instance.ComponentsUsedDataGetRowCount,
                                                     ComponentsUsedBLL.Instance.GetComponentsUsedTLAData,
                                                       "ComponentsUsedInDJ_TLADetail");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }


    }
}
