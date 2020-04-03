using eTrace.Report.BLL.Business;
using eTrace.Model;
using eTrace.Model.V2.Report;
using eTrace.Service.SDKForNet.Request.Reports;
using eTrace.Service.SDKForNet.Response.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using eTrace.Common;
using eTrace.Service.SDKForNet;
using eTrace.Service.SDKForNet.Request.Reports.SFC;
using eTrace.Service.SDKForNet.Response.Reports.SFC;
using System.Data;
using eTrace.Report.BLL;
using eTrace.Model.V2.Report.DailyRepairList;
using System.IO;
using System.Net.Http.Headers;

namespace eTraceWebApi.Controllers.Reports
{
    public class ReportRepairController : ServerBaseController<ReportWIPProductController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportRepairController()
        {

        }


        /// <summary>
        /// 下载 维修记录报表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadListOfRepairData(DownloadListOfRepairDataRequest request)
        {
            try
            {
                var queryModel = GetModelQueryData<DownloadListOfRepairDataRequest,
                                                   GetListOfRepairDataQuery,
                                                   GetListOfRepairDataQuery.Item>(request);
                HttpResponseMessage fullResponse = null;
                long rowCount = RepairBLL.Instance.GetListOfRepairDataTotalCount(queryModel);
                if (rowCount > 0 && rowCount < MaxDownloadRowCount)
                {
                    queryModel.Pager = null;
                    var dataList = RepairBLL.Instance.GetListOfRepairData(queryModel);
                    fullResponse = GetDownloadExcelResponse(dataList, request.TableHeaders, "ListOfRepairData");
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
        public ReportCheckRowsCountResponse GetListOfRepairDataRowCount(DownloadListOfRepairDataRequest request)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
            var queryModel = GetModelQueryData<DownloadListOfRepairDataRequest,
                                                  GetListOfRepairDataQuery,
                                                  GetListOfRepairDataQuery.Item>(request);
            long rowCount = RepairBLL.Instance.GetListOfRepairDataTotalCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }


        /// <summary>
        /// 获取维修记录
        /// </summary>
        [HttpPost]
        public GetListOfRepairDataResponse GetListOfRepairData(GetListOfRepairDataRequest request)
        {
            var response = GetBusinessResponseDataInited<GetListOfRepairDataResponse>();
            try
            {
                var queryModel = GetModelQueryData<GetListOfRepairDataRequest,
                                                GetListOfRepairDataQuery,
                                                GetListOfRepairDataQuery.Item>(request);
                GetListOfRepairDataModel dbDatas = RepairBLL.Instance.GetListOfRepairDataByPage(queryModel);
                return GetReportQueryResponse<GetListOfRepairDataResponse,
                                GetListOfRepairDataResponseItem,
                                GetListOfRepairDataModel.Item>(dbDatas);
            }
            catch (Exception ex)
            {

                response = GetResponseError(ref response, EmBussinesCodeType.ReportUnKnowError);
            }
    

            return response;
        }


        /// <summary>
        /// 获取WipInWipOut记录
        /// </summary>
        [HttpPost]
        public GetWipInWipOutDataResponse GetWipInWipOutData(GetWipInWipOutRequest request)
        {
            var response = GetBusinessResponseDataInited<GetWipInWipOutDataResponse>();
            try
            {
                var queryModel = GetModelQueryData<GetWipInWipOutRequest,
                                                GetWipInWipOutQuery,
                                                RequestItem>(request);
                GetWipInWipOutModel dbDatas = RepairBLL.Instance.GetWipInWipOutDataByPage(queryModel);
                return GetReportQueryResponse<GetWipInWipOutDataResponse,
                                GetWipInWipOutDataResponseItem,
                                GetWipInWipOutModel.Item>(dbDatas);
            }
            catch (Exception ex)
            {

                response = GetResponseError(ref response, EmBussinesCodeType.ReportUnKnowError);
            }


            return response;
        }


        /// <summary>
        /// 获取WipOut记录
        /// </summary>
        [HttpPost]
        public GetWipOutDataResponse GetWipOutData(GetWipOutRequest request)
        {
            var response = GetBusinessResponseDataInited<GetWipOutDataResponse>();
            try
            {
                var queryModel = GetModelQueryData<GetWipOutRequest,
                                                GetWipOutQuery,
                                                RequestItem>(request);
                GetWipOutModel dbDatas = RepairBLL.Instance.GetWipOutDataByPage(queryModel);
                return GetReportQueryResponse<GetWipOutDataResponse,
                                GetWipOutDataResponseItem,
                                GetWipOutModel.Item>(dbDatas);
            }
            catch (Exception ex)
            {

                response = GetResponseError(ref response, EmBussinesCodeType.ReportUnKnowError);
            }


            return response;
        }
        /// <summary>
        /// 获取MoreThanOne记录
        /// </summary>
        [HttpPost]
        public GetMoreThanOneDataResponse GetMoreThanOneData(GetMoreThanOneRequest request)
        {
            var response = GetBusinessResponseDataInited<GetMoreThanOneDataResponse>();
            try
            {
                var queryModel = GetModelQueryData<GetMoreThanOneRequest,
                                                GetMoreThanOneQuery,
                                                RequestItem>(request);
                GetMoreThanOneModel dbDatas = RepairBLL.Instance.GetMoreThanOneDataByPage(queryModel);
                return GetReportQueryResponse<GetMoreThanOneDataResponse,
                                GetMoreThanOneDataResponseItem,
                                GetMoreThanOneModel.Item>(dbDatas);
            }
            catch (Exception ex)
            {

                response = GetResponseError(ref response, EmBussinesCodeType.ReportUnKnowError);
            }


            return response;
        }

        /// <summary>
        /// 获取TopTenComonent记录
        /// </summary>
        [HttpPost]
        public GetTopTenComonentDataResponse GetTopTenComonentData(GetTopTenComonentRequest request)
        {
            var response = GetBusinessResponseDataInited<GetTopTenComonentDataResponse>();
            try
            {
                var queryModel = GetModelQueryData<GetTopTenComonentRequest,
                                                GetTopTenComonentQuery,
                                                RequestItem>(request);
                GetTopTenComonentModel dbDatas = RepairBLL.Instance.GetTopTenComonentDataByPage(queryModel);
                return GetReportQueryResponse<GetTopTenComonentDataResponse,
                                GetTopTenComonentDataResponseItem,
                                GetTopTenComonentModel.Item>(dbDatas);
            }
            catch (Exception ex)
            {

                response = GetResponseError(ref response, EmBussinesCodeType.ReportUnKnowError);
            }


            return response;
        }

        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetDailyRepairListRowCount(DownloadDailyRepairListRequest request)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
            var queryTopTenComonent = GetModelQueryData<DownloadDailyRepairListRequest,
                                                 GetTopTenComonentQuery,
                                                 RequestItem>(request);

            var queryWipInWipOut = GetModelQueryData<DownloadDailyRepairListRequest,
                                            GetWipInWipOutQuery,
                                            RequestItem>(request);

            var queryWipOut = GetModelQueryData<DownloadDailyRepairListRequest,
                                            GetWipOutQuery,
                                            RequestItem>(request);

            var queryMoreThanOne = GetModelQueryData<DownloadDailyRepairListRequest,
                                           GetMoreThanOneQuery,
                                           RequestItem>(request);


            HttpResponseMessage fullResponse = null;
            long rowCount = 0;
            rowCount = RepairBLL.Instance.GetTopTenComonentDataTotalCount(queryTopTenComonent);
            if (rowCount > CheckDownloadRowCount)
            {
                //over max row limit

                return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
            }
            rowCount = RepairBLL.Instance.GetWipInWipOutDataTotalCount(queryWipInWipOut);
            if (rowCount > CheckDownloadRowCount)
            {
                return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
            }
            rowCount = RepairBLL.Instance.GetWipOutDataTotalCount(queryWipOut);
            if (rowCount > CheckDownloadRowCount)
            {
                return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
            }
            rowCount = RepairBLL.Instance.GetMoreThanOneDataTotalCount(queryMoreThanOne);
            if (rowCount > CheckDownloadRowCount)
            { 
                  return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount); ;
            }
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }

        /// <summary>
        /// 下载 维修统计记录报表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadDailyRepairList(DownloadDailyRepairListRequest request)
        {
            try
            {
                #region 检查是否超过最大行数
                var queryTopTenComonent = GetModelQueryData<DownloadDailyRepairListRequest,
                                                GetTopTenComonentQuery,
                                                RequestItem>(request);

                var queryWipInWipOut = GetModelQueryData<DownloadDailyRepairListRequest,
                                                GetWipInWipOutQuery,
                                                RequestItem>(request);

                var queryWipOut = GetModelQueryData<DownloadDailyRepairListRequest,
                                                GetWipOutQuery,
                                                RequestItem>(request);

                var queryMoreThanOne = GetModelQueryData<DownloadDailyRepairListRequest,
                                               GetMoreThanOneQuery,
                                               RequestItem>(request);


                HttpResponseMessage fullResponse = null;
                long rowCount = 0;
                 rowCount = RepairBLL.Instance.GetTopTenComonentDataTotalCount(queryTopTenComonent);
                if (rowCount> MaxDownloadRowCount)
                {
                    //over max row limit
                    fullResponse = Request.CreateResponse(HttpStatusCode.RequestEntityTooLarge);
                    return ResponseMessage(fullResponse);
                }
                 rowCount = RepairBLL.Instance.GetWipInWipOutDataTotalCount(queryWipInWipOut);
                if (rowCount > MaxDownloadRowCount)
                {
                    //over max row limit
                    fullResponse = Request.CreateResponse(HttpStatusCode.RequestEntityTooLarge);
                    return ResponseMessage(fullResponse);
                }
                 rowCount = RepairBLL.Instance.GetWipOutDataTotalCount(queryWipOut);
                if (rowCount > MaxDownloadRowCount)
                {
                    //over max row limit
                    fullResponse = Request.CreateResponse(HttpStatusCode.RequestEntityTooLarge);
                    return ResponseMessage(fullResponse);
                }
                 rowCount = RepairBLL.Instance.GetMoreThanOneDataTotalCount(queryMoreThanOne);
                if (rowCount > MaxDownloadRowCount)
                {
                    //over max row limit
                    fullResponse = Request.CreateResponse(HttpStatusCode.RequestEntityTooLarge);
                    return ResponseMessage(fullResponse);
                }
                #endregion
                //查询
                var wipInWipOutList = RepairBLL.Instance.GetWipInWipOutData (queryWipInWipOut);
                var wipOutList = RepairBLL.Instance.GetWipOutData (queryWipOut);
                var topTenComponentList = RepairBLL.Instance.GetTopTenComonentData(queryTopTenComonent);
                var moreThanOneList = RepairBLL.Instance.GetMoreThanOneData(queryMoreThanOne);
                List<ExcelHelper.SaveMultiSheetModel> modelList = new List<ExcelHelper.SaveMultiSheetModel>();
                modelList.Add(GetSaveMultiSheetModel(request.WipInWipOutHeaders,
                                                    wipInWipOutList.Select(x => (object)x).ToList(),
                                                    "WipInWipOutReport"));
                modelList.Add(GetSaveMultiSheetModel(request.WipOutHeaders,
                                                    wipOutList.Select(x => (object)x).ToList(),
                                                    "WipOutReport"));
                modelList.Add(GetSaveMultiSheetModel(request.TopTenComponentHeaders,
                                                    topTenComponentList.Select(x => (object)x).ToList(),
                                                    "TopTenComponentReport"));
                modelList.Add(GetSaveMultiSheetModel(request.MoreThanOneHeaders,
                                                    moreThanOneList.Select(x => (object)x).ToList(),
                                                    "MoreThanOneReport"));

                MemoryStream stream = ExcelHelper.Instance.SaveMultiSheet(modelList);
                if (stream != null)
                {
                    fullResponse = Request.CreateResponse(HttpStatusCode.OK);
                    fullResponse.Content = new StreamContent(stream);
                    //define file Name
                    var excelHelper = new ExcelHelper();
                    string fileFullName = excelHelper.GetFileName("StatisticRepairData");
                    string contentDisposition = string.Format("attachment;filename={0}", fileFullName);
                    fullResponse.Content.Headers.ContentDisposition = ContentDispositionHeaderValue.Parse(contentDisposition);
                    fullResponse.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                    return ResponseMessage(fullResponse);
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.InternalServerError);
                }


            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// 下载 WipInWipOut记录报表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadWipInWipOutReport(DownloadWipInWipOutRequest request)
        {
            try
            {
                var queryModel = GetModelQueryData<DownloadWipInWipOutRequest,
                                                   GetWipInWipOutQuery,
                                                    RequestItem>(request);
                HttpResponseMessage fullResponse = null;
                long rowCount = RepairBLL.Instance.GetWipInWipOutDataTotalCount(queryModel);
                if (rowCount > 0 && rowCount < MaxDownloadRowCount)
                {
                    queryModel.Pager = null;
                    var dataList = RepairBLL.Instance.GetWipInWipOutData(queryModel);
                    fullResponse = GetDownloadExcelResponse(dataList, request.TableHeaders, "WipInWipOutData");
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
                return ResponseMessage(fullResponse);
                //}

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// 下载 WipOut记录报表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadWipOutReport(DownloadWipOutRequest request)
        {
            try
            {
                var queryModel = GetModelQueryData<DownloadWipOutRequest,
                                                   GetWipOutQuery,
                                                    RequestItem>(request);
                HttpResponseMessage fullResponse = null;
                long rowCount = RepairBLL.Instance.GetWipOutDataTotalCount(queryModel);
                if (rowCount > 0 && rowCount < MaxDownloadRowCount)
                {
                    queryModel.Pager = null;
                    var dataList = RepairBLL.Instance.GetWipOutData(queryModel);
                    fullResponse = GetDownloadExcelResponse(dataList, request.TableHeaders, "WipOutData");
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
                return ResponseMessage(fullResponse);
                //}

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// 下载 TopTenComonent记录报表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadTopTenComonentReport(DownloadTopTenComonentRequest  request)
        {
            try
            {
                var queryModel = GetModelQueryData<DownloadTopTenComonentRequest,
                                                   GetTopTenComonentQuery,
                                                    RequestItem>(request);
                HttpResponseMessage fullResponse = null;
                long rowCount = RepairBLL.Instance.GetTopTenComonentDataTotalCount(queryModel);
                if (rowCount > 0 && rowCount < MaxDownloadRowCount)
                {
                    queryModel.Pager = null;
                    var dataList = RepairBLL.Instance.GetTopTenComonentData(queryModel);
                    fullResponse = GetDownloadExcelResponse(dataList, request.TableHeaders, "TopTenComonentData");
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
                return ResponseMessage(fullResponse);
                //}

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// 下载 MoreThanOne记录报表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadMoreThanOneReport(DownloadMoreThanOneRequest   request)
        {
            try
            {
                var queryModel = GetModelQueryData<DownloadMoreThanOneRequest,
                                                   GetMoreThanOneQuery,
                                                    RequestItem>(request);
                HttpResponseMessage fullResponse = null;
                long rowCount = RepairBLL.Instance.GetMoreThanOneDataTotalCount(queryModel);
                if (rowCount > 0 && rowCount < MaxDownloadRowCount)
                {
                    queryModel.Pager = null;
                    var dataList = RepairBLL.Instance.GetMoreThanOneData(queryModel);
                    fullResponse = GetDownloadExcelResponse(dataList, request.TableHeaders, "MoreThanOneData");
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
                return ResponseMessage(fullResponse);
                //}

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}