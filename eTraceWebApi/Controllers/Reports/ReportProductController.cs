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

namespace eTraceWebApi.Controllers
{
    /// <summary>
    /// ReportProductModuleController
    /// </summary>
    public class ReportProductController : ServerBaseController<ReportProductController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportProductController()
        {

        }

        /// <summary>
        /// TDHeader
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportTDHeaderResponse GetTDHeaderDatas([FromBody] ReportTDHeaderRequest requestData)
        {
            ReportTDHeaderResponse response = GetBusinessResponseDataInited<ReportTDHeaderResponse>();
            try
            {
                ReportTDHeaderQuery queryModel = GetModelQuery<ReportTDHeaderRequest, ReportTDHeaderQuery>(requestData);
                var dbDatas = ProductBLL.Instance.GetTDHeaders(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportTDHeaderResponse.Item();
                            data.CopyModelValueFrom<ReportTDHeaderResponse.Item, ReportTDHeaderModel.Item>(item);
                            response.Data.Add(data);
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportTDHeaderResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        /// 获取产品测试数据详细资料
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportProductTestDataResponse GetProductTestDatas([FromBody] ReportGetProductTestDatasRequest requestData)
        {
            ReportProductTestDataQuery queryModel = GetModelQuery<ReportGetProductTestDatasRequest, ReportProductTestDataQuery>(requestData);
            ReportProductTestDataModel dbDatas = ProductBLL.Instance.GetProductTestData(queryModel);
            return GetReportQueryResponse<ReportProductTestDataResponse,
                                            ReportProductTestDataResponse.Item, 
                                            ReportProductTestDataModel.Item>(dbDatas); 
        }
        /// <summary>
        /// 获取产品测试数据汇总
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportProductTestDataSummaryResponse GetProductTestDatasSummary([FromBody] ReportGetProductTestDatasRequest requestData)
        {
            ReportProductTestDataQuery queryModel = GetModelQuery<ReportGetProductTestDatasRequest, ReportProductTestDataQuery>(requestData);
            ReportProductTestSummaryDataModel dbDatas = ProductBLL.Instance.GetProductTestDataSummary(queryModel);
            return GetReportQueryResponse<ReportProductTestDataSummaryResponse,
                                            ReportProductTestDataSummaryResponse.Item,
                                            ReportProductTestSummaryDataModel.Item>(dbDatas);
        }



        /// <summary>
        /// 获取产品测试数据详细资料
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportProductTestDataArchiveResponse GetProductTestDatasArchive([FromBody] ReportGetProductTestDatasArchiveRequest requestData)
        {
            ReportProductTestDataArchiveQuery queryModel = GetModelQuery<ReportGetProductTestDatasArchiveRequest, ReportProductTestDataArchiveQuery>(requestData);
            ReportProductTestDataArchiveModel dbDatas = ProductArchiveBLL.Instance.GetProductTestData(queryModel);
            return GetReportQueryResponse<ReportProductTestDataArchiveResponse,
                                            ReportProductTestDataArchiveResponse.Item,
                                            ReportProductTestDataArchiveModel.Item>(dbDatas);
        }
        /// <summary>
        /// 获取产品测试数据汇总
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportProductTestDataArchiveSummaryResponse GetProductTestDatasSummaryArchive([FromBody] ReportGetProductTestDatasArchiveSummaryRequest requestData)
        {
            ReportProductTestDataArchiveQuery queryModel = GetModelQuery<ReportGetProductTestDatasArchiveSummaryRequest, ReportProductTestDataArchiveQuery>(requestData);
            ReportProductTestSummaryDataArchiveModel dbDatas = ProductArchiveBLL .Instance.GetProductTestDataSummary(queryModel);
            return GetReportQueryResponse<ReportProductTestDataArchiveSummaryResponse,
                                            ReportProductTestDataArchiveSummaryResponse.Item,
                                            ReportProductTestSummaryDataArchiveModel.Item>(dbDatas);
        }
        /// <summary>
        /// 下载产品测试数据汇总
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadProductTestDatasSummary([FromBody] ReportDownloadProductTestDatasRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportProductTestDataQuery queryModel = GetModelQuery<ReportDownloadProductTestDatasRequest, ReportProductTestDataQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     ProductBLL.Instance.ProductTestDataSummaryGetRowCount,
                                                     ProductBLL.Instance.GetProductTestDataSummary,
                                                       "ProductTestDatasSummary");
                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        /// <summary>
        /// 下载产品测试数据汇总
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadProductTestDatasSummaryArchive([FromBody] ReportDownloadProductTestDatasArchiveSummaryRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportProductTestDataArchiveQuery queryModel = GetModelQuery<ReportDownloadProductTestDatasArchiveSummaryRequest, ReportProductTestDataArchiveQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     ProductArchiveBLL.Instance.ProductTestDataSummaryGetRowCount,
                                                     ProductArchiveBLL.Instance.GetProductTestDataSummary,
                                                       "ProductTestDatasSummaryArchive");
                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetProductTestDataSummaryRowCount(ReportDownloadProductTestDatasRequest request)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
            ReportProductTestDataQuery queryModel = GetModelQuery<ReportDownloadProductTestDatasRequest, ReportProductTestDataQuery>(request);
            long rowCount = ProductBLL.Instance.ProductTestDataSummaryGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }
        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetProductTestDataSummaryArchiveRowCount(ReportDownloadProductTestDatasArchiveSummaryRequest request)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
            ReportProductTestDataArchiveQuery queryModel = GetModelQuery<ReportDownloadProductTestDatasArchiveSummaryRequest, ReportProductTestDataArchiveQuery>(request);
            long rowCount = ProductArchiveBLL.Instance.ProductTestDataSummaryGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }

        //[HttpPost]
        //public ReportCheckRowsCountResponse GetTotalRowCount([FromBody] ReportDownloadProductTestDatasRequest requestData)
        //{
        //    var response   = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
        //    ReportProductTestDataQuery queryModel = GetModelQuery<ReportDownloadProductTestDatasRequest, ReportProductTestDataQuery>(requestData);

        //    long rowCount = ProductBLL.Instance.ProductTestDataGetRowCount(queryModel);
        //    if (rowCount==0)
        //    {
        //        response.BussinesCode = EmBussinesCodeType.ReportCheckDownloadRowCountEqualZero;
        //        response.Success = false;
        //    }
        //    if (rowCount > CheckDownloadRowCount)
        //    {
        //        response.BussinesCode = EmBussinesCodeType.ReportCheckDownloadRowCountEqualZero;
        //        response.Success = false;
        //    }

        //}

        /// <summary>
        /// 下载产品测试数据详细资料
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadProductTestDatas([FromBody] ReportDownloadProductTestDatasRequest requestData) 
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportProductTestDataQuery queryModel = GetModelQuery<ReportDownloadProductTestDatasRequest, ReportProductTestDataQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     ProductBLL.Instance.ProductTestDataGetRowCount,
                                                     ProductBLL.Instance.GetProductTestData,
                                                       "ProductTestDatasDetail");
                return ResponseMessage(fullResponse);
                //}

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        /// <summary>
        /// 下载产品测试数据详细资料
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadProductTestDatasArchive([FromBody] ReportDownloadProductTestDatasArchiveRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportProductTestDataArchiveQuery queryModel = GetModelQuery<ReportDownloadProductTestDatasArchiveRequest, ReportProductTestDataArchiveQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     ProductArchiveBLL.Instance.ProductTestDataGetRowCount,
                                                     ProductArchiveBLL.Instance.GetProductTestData,
                                                       "ProductTestDatasDetailArchive");
                return ResponseMessage(fullResponse);
                //}

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetProductTestDataRowCount(ReportDownloadProductTestDatasRequest request)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
            ReportProductTestDataQuery queryModel = GetModelQuery<ReportDownloadProductTestDatasRequest, ReportProductTestDataQuery>(request);

            long rowCount = ProductBLL.Instance.ProductTestDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }
        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetProductTestDataArchiveRowCount(ReportDownloadProductTestDatasArchiveRequest request)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
            ReportProductTestDataArchiveQuery queryModel = GetModelQuery<ReportDownloadProductTestDatasArchiveRequest, ReportProductTestDataArchiveQuery>(request);
            long rowCount = ProductArchiveBLL.Instance.ProductTestDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }

        /// <summary>
        /// 检查是否查过最大行数
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        //[HttpPost]
        //public ReportCheckRowsCountResponse ProductTestDataCheckRowCount([FromBody] ReportProductTestDataRequest requestData)
        //{
        //    ReportCheckRowsCountResponse response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
        //    try
        //    {
        //        ReportProductTestDataQuery queryModel = GetModelQuery<ReportProductTestDataRequest, ReportProductTestDataQuery>(requestData);
        //        response.IsOverMaxRowCount = ProductModuleBLL.Instance.ProductTestDataGetRowCount(queryModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        return GetResponseError<ReportCheckRowsCountResponse>(ref response, ex, EmBussinesCodeType.ReportProductGetTDHeaderDatasError);
        //    }
        //    return response;
        //}

        /// <summary>
        /// 下载产品包装数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadProductDatas([FromBody] ReportDownloadProductDataRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportProductDataQuery queryModel = GetModelQuery<ReportDownloadProductDataRequest, ReportProductDataQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     ProductBLL.Instance.ProductDataGetRowCount,
                                                     ProductBLL.Instance.GetProductDatas,
                                                       "ProductData");
                return ResponseMessage(fullResponse);
                //}

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        /// <summary>
        /// 下载产品包装数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadProductDatasArchive([FromBody] ReportDownloadProductDataArchiveRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportProductArchiveDataQuery queryModel = GetModelQuery<ReportDownloadProductDataArchiveRequest, ReportProductArchiveDataQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     ProductArchiveBLL.Instance.ProductDataGetRowCount,
                                                     ProductArchiveBLL.Instance.GetProductDatasArchive,
                                                       "ProductData");
                return ResponseMessage(fullResponse);
                //}

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetProductDatasRowCount(ReportDownloadProductDataRequest request)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
            ReportProductDataQuery queryModel = GetModelQuery<ReportDownloadProductDataRequest, ReportProductDataQuery>(request);
            long rowCount = ProductBLL.Instance.ProductDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }
        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetProductDatasArchiveRowCount(ReportDownloadProductDataArchiveRequest   request)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
            ReportProductArchiveDataQuery queryModel = GetModelQuery<ReportDownloadProductDataArchiveRequest, ReportProductArchiveDataQuery  >(request);
            long rowCount = ProductArchiveBLL.Instance.ProductDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }
        /// <summary>
        /// 获取产品包装数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportProductDataResponse GetProductDatas([FromBody] ReportProductDataRequest requestData)
        {
            ReportProductDataResponse response = GetBusinessResponseDataInited<ReportProductDataResponse>();
            try
            {
                ReportProductDataQuery queryModel = GetModelQuery<ReportProductDataRequest, ReportProductDataQuery>(requestData);
                ReportProductDataModel dbDatas = ProductBLL.Instance.GetProductDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportProductDataResponse.Item();
                            data.CopyModelValueFrom<ReportProductDataResponse.Item, ReportProductDataModel.Item>(item);
                            data.Seq = counter;
                            response.Data.Add(data);
                            counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportProductDataResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
                //return GetResponseError<ReportProductDataResponse>(ref response, ex, EmBussinesCodeType.ReportInternalError);
            }
            return response;
        }
        /// <summary>
        /// 获取产品包装数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportProductDataArchiveResponse GetProductDatasArchive([FromBody] ReportProductDataArchiveRequest requestData)
        {
            ReportProductDataArchiveResponse response = GetBusinessResponseDataInited<ReportProductDataArchiveResponse>();
            try
            {
                ReportProductArchiveDataQuery queryModel = GetModelQuery<ReportProductDataArchiveRequest, ReportProductArchiveDataQuery>(requestData);
                ReportProductArchiveDataModel dbDatas = ProductArchiveBLL.Instance.GetProductDatasArchive(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportProductDataArchiveResponse.Item();
                            data.CopyModelValueFrom<ReportProductDataArchiveResponse.Item, ReportProductArchiveDataModel.Item>(item);
                            data.Seq = counter;
                            response.Data.Add(data);
                            counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportProductDataArchiveResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
                //return GetResponseError<ReportProductDataResponse>(ref response, ex, EmBussinesCodeType.ReportInternalError);
            }
            return response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public GetProductionErrorLogMisMatchResponse GetProductionErrorLogMisMatch(GetProductErrorLogRequest request)
        {
            //ReportProductTestDataQuery queryModel = GetModelQuery<GetProductErrorLogRequest, ReportProductTestDataQuery>(request);
            //ReportProductTestDataModel dbDatas = ProductBLL.Instance.GetProductTestData(queryModel);
            //return GetReportQueryResponse<ReportProductTestDataResponse,
            //                                ReportProductTestDataResponse.Item,
            //                                ReportProductTestDataModel.Item>(dbDatas);
            //return null;

            var queryModel = GetModelQueryData<GetProductErrorLogRequest,
                                                    GetProductErrorLogQuery, 
                                                    GetProductErrorLogQuery.Item>(request);
 
            GetProductErrorLogModel dbDatas = ProductErrorLogBLL.Instance.GetProductErrorLogByPage(queryModel);
            return GetReportQueryResponse<GetProductionErrorLogMisMatchResponse,
                                            GetProductErrorLogResponseItem,
                                            GetProductErrorLogModel.MissMatchItem>(dbDatas);


        }

        /// <summary>
        /// 下载ProductErrorlO
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadProductErrorLogMisMatch(DownloadProductErrorLogRequest request)
        {

            try
            {
                var queryModel = GetModelQueryData<DownloadProductErrorLogRequest,
                                                   GetProductErrorLogQuery,
                                                   GetProductErrorLogQuery.Item>(request);
                HttpResponseMessage fullResponse=null;
                long rowCount = ProductErrorLogBLL.Instance.GetProductErrorLogTotalCount(queryModel);
                if (rowCount > 0 && rowCount < MaxDownloadRowCount)
                {
                    queryModel.Pager = null;
                    var dataList = ProductErrorLogBLL.Instance.GetProductErrorLog(queryModel);
                    fullResponse = GetDownloadExcelResponse(dataList, request.TableHeaders, "ProductErrorLog");
                    if (fullResponse == null)
                    {
                        fullResponse = Request.CreateResponse(HttpStatusCode.InternalServerError);
                    }
                }
                else if (rowCount == 0)
                {
                    //not available data
                    fullResponse = Request.CreateResponse(HttpStatusCode.NoContent);
                }
                else
                {
                    //over max row limit
                    fullResponse = Request.CreateResponse(HttpStatusCode.RequestEntityTooLarge);
                }
                return    ResponseMessage(fullResponse);
                //}

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

        }


        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetProductErrorLogTotalCount(DownloadProductErrorLogRequest request)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
            var queryModel = GetModelQueryData<DownloadProductErrorLogRequest,
                                                  GetProductErrorLogQuery,
                                                  GetProductErrorLogQuery.Item>(request);

            long rowCount = ProductErrorLogBLL.Instance.GetProductErrorLogTotalCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }
    }
}
