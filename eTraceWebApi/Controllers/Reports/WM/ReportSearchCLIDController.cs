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
    /// ReportSearchCLIDController
    /// </summary>
    public class ReportSearchCLIDController : ServerBaseController<ReportSearchCLIDController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportSearchCLIDController()
        {
        }

        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetSearchCLIDTotalCount(DownLoadSearchCLIDRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();

            ReportSearchCLIDModelQuery queryModel = GetModelQuery<DownLoadSearchCLIDRequest, ReportSearchCLIDModelQuery>(requestData);
            queryModel.ReportType = "Current";

            long rowCount = SearchCLIDBLL.Instance.SearchCLIDDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }

        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetSearchCLIDArchiveTotalCount(DownLoadSearchCLIDRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();

            ReportSearchCLIDModelQuery queryModel = GetModelQuery<DownLoadSearchCLIDRequest, ReportSearchCLIDModelQuery>(requestData);
            queryModel.ReportType = "Archive";

            long rowCount = SearchCLIDBLL.Instance.SearchCLIDDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }


        /// <summary>
        /// 获取下载 CLID Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadSearchCLIDData([FromBody] DownLoadSearchCLIDRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportSearchCLIDModelQuery queryModel = GetModelQuery<DownLoadSearchCLIDRequest, ReportSearchCLIDModelQuery>(requestData);
                queryModel.ReportType = "Current";

                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     //SearchCLIDBLL.Instance.SearchCLIDDataGetRowCount,
                                                     SearchCLIDBLL.Instance.GetSearchCLIDData,
                                                       "SearchCLIDs");

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
        public IHttpActionResult DownloadSearchCLIDArchiveData([FromBody] DownLoadSearchCLIDRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportSearchCLIDModelQuery queryModel = GetModelQuery<DownLoadSearchCLIDRequest, ReportSearchCLIDModelQuery>(requestData);
                queryModel.ReportType = "Archive";

                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     //SearchCLIDBLL.Instance.SearchCLIDDataGetRowCount,
                                                     SearchCLIDBLL.Instance.GetSearchCLIDData,
                                                       "SearchCLIDs_Archive");

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
        public ReportSearchCLIDResponse GetSearchCLIDData([FromBody] ReportSearchCLIDRequest requestData)
        {
            ReportSearchCLIDResponse response = GetBusinessResponseDataInited<ReportSearchCLIDResponse>();
            try
            {
                ReportSearchCLIDModelQuery queryModel = GetModelQuery<ReportSearchCLIDRequest, ReportSearchCLIDModelQuery>(requestData);
                queryModel.ReportType = "Current";

                ReportSearchCLIDModel dbDatas = SearchCLIDBLL.Instance.GetSearchCLIDData(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSearchCLIDResponse.Item();
                            data.CopyModelValueFrom<ReportSearchCLIDResponse.Item, ReportSearchCLIDModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSearchCLIDResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        public ReportSearchCLIDResponse GetSearchCLIDArchiveData([FromBody] ReportSearchCLIDRequest requestData)
        {
            ReportSearchCLIDResponse response = GetBusinessResponseDataInited<ReportSearchCLIDResponse>();
            try
            {
                ReportSearchCLIDModelQuery queryModel = GetModelQuery<ReportSearchCLIDRequest, ReportSearchCLIDModelQuery>(requestData);
                queryModel.ReportType = "Archive";

                ReportSearchCLIDModel dbDatas = SearchCLIDBLL.Instance.GetSearchCLIDData(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSearchCLIDResponse.Item();
                            data.CopyModelValueFrom<ReportSearchCLIDResponse.Item, ReportSearchCLIDModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSearchCLIDResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
