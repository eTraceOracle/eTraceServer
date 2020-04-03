using eTrace.Report.BLL.Business;
using eTrace.Model;
using eTrace.Model.V2.Report;
using eTrace.Service.SDKForNet.Request.Reports;
using eTrace.Service.SDKForNet.Response;
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

namespace eTraceWebApi.Controllers.Reports
{
    public class ReportWIPProductController : ServerBaseController<ReportWIPProductController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportWIPProductController()
        {

        }

        /// <summary>
        /// 获取工序站位数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public WIPUnitFlowResponse GetWIPFlowData([FromBody] WIPUnitFlowRequest requestData)
        {
            WIPUnitFlowQuery queryModel = GetModelQuery<WIPUnitFlowRequest, WIPUnitFlowQuery>(requestData);
            ReportWIPProductModel dbDatas = WIPProductBLL.Instance.GetWIPUnitFlow(queryModel);
            return GetReportQueryResponse<WIPUnitFlowResponse,
                                            WIPUnitFlowResponse.Item,
                                            ReportWIPProductModel.Item>(dbDatas);
            //WIPUnitFlowResponse response = GetBusinessResponseDataInited<WIPUnitFlowResponse>();
            //try
            //{
            //    WIPUnitFlowQuery queryModel = GetModelQuery<WIPUnitFlowRequest, WIPUnitFlowQuery>(requestData);
            //    ReportWIPProductModel dbDatas = WIPProductBLL.Instance.GetWIPUnitFlow(queryModel);

            //    if (dbDatas != null)
            //    {
            //        if (dbDatas.Data != null)
            //        {
            //            int counter = 1;
            //            foreach (var item in dbDatas.Data)
            //            {
            //                var data = new WIPUnitFlowResponse.Item();
            //                data.CopyModelValueFrom<WIPUnitFlowResponse.Item, ReportWIPProductModel.Item>(item);
            //                data.Seq = counter;
            //                response.Data.Add(data);
            //                counter++;
            //            }

            //        }
            //        //if the records number is a huge number
            //        if (dbDatas.IsOverMaxRow)
            //        {
            //            return GetResponseError<WIPUnitFlowResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
            //        }
            //        if (dbDatas.Pager != null)
            //        {
            //            response.Pager = new ResponsePager()
            //            {
            //                TotalCount = dbDatas.Pager.TotalCount,
            //            };
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    return GetResponseError<WIPUnitFlowResponse>(ref response, ex, EmBussinesCodeType.ReportProductGetProcessError);
            //}
            //return response;
        }

        /// <summary>
        /// 获取SMT AOI数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public GetSMTAOIDataResponse GetSMTAOIData(GetSMTAOIDataRequest request)
        {
            GetSMTAOIDataResponse response = new GetSMTAOIDataResponse();
            using (SMTServiceReference.SMTDataServiceSoapClient client =new SMTServiceReference.SMTDataServiceSoapClient())
            {
               var smtDatset=    client.getAOIBoardData(request.Data.StartTime.ToString(),
                                        request.Data.EndTime.ToString(), 
                                        request.Data.Model, 
                                        request.Data.EquipmentID,
                                        request.Data.Result, 
                                        request.Data.DiscreteJob,
                                        request.Data.AOIBarcode);
                if (smtDatset.Tables.Count>0)
                {
                    DataTable dataTable = smtDatset.Tables[0];
                    response.Pager = new ResponsePager()
                    {
                        TotalCount = dataTable.Rows.Count
                    };
                    response.Data= dataTable.AsEnumerable().Select<DataRow, GetSMTAOIDataResponseItem>(x =>
                         new GetSMTAOIDataResponseItem()
                        {
                            AOIBarcode = x.Field<string>("AOIBarcode"),
                            BoardIndex = x.Field<int>("BoardIndex"),
                            Checked = x.Field<int>("Checked"),
                            EquipmentID = x.Field<string>("EquipmentID"),
                            FailComp = x.Field<int>("FailComp"),
                            IntSN = x.Field<string>("intSN"),
                            OperatorName= x.Field<string>("OperatorName"),
                            PanelID= x.Field<string>("PanelID"),
                            ProdDate= x.Field<DateTime>("ProdDate"),
                            ProgramName= x.Field<string>("ProgramName"),
                            Remark= x.Field<string>("Remark"),
                            Result= x.Field<string>("Result"),
                            TestCount= x.Field<int>("TestCount"),
                            TopBtm= x.Field<string>("TopBtm")
                         }
                    ).Skip(request.Pager.PageSize-1* request.Pager.CurrentPage)
                    .Take(request.Pager.PageSize) .ToList();
                   ;
                }
            }
            response.BussinesCode = EmBussinesCodeType.Success;
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadSMTAOIData(DownloadSMTAOIDataRequest request)
        {
            HttpResponseMessage response=null;
            using (SMTServiceReference.SMTDataServiceSoapClient client = new SMTServiceReference.SMTDataServiceSoapClient())
            {
                var smtDatset = client.getAOIBoardData(request.Data.StartTime.ToString(),
                                         request.Data.EndTime.ToString(),
                                         request.Data.Model,
                                         request.Data.EquipmentID,
                                         request.Data.Result,
                                         request.Data.DiscreteJob,
                                         request.Data.AOIBarcode);
                if (smtDatset.Tables.Count > 0)
                {
                    DataTable dataTable = smtDatset.Tables[0];
                    if (dataTable.Rows.Count > MaxDownloadRowCount)
                    {
                        response = Request.CreateResponse(HttpStatusCode.RequestEntityTooLarge);
                    }
                    else if (dataTable.Rows.Count == 0)
                    {
                        response = Request.CreateResponse(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        response = GetDownloadExcelResponse(dataTable, request.TableHeaders);
                    }

                }
            }
            return ResponseMessage(response); 
        }

        /// <summary>
        /// 下载 锁记录报表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadWipLockData(DownloadWipLockDataRequest request)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                GetWipLockDataModelQuery queryModel = GetModelQueryData<DownloadWipLockDataRequest, GetWipLockDataModelQuery, GetWipLockDataModelQuery.Item>(request);
                fullResponse = DownloadReportResponse(queryModel,
                                                     request.TableHeaders,
                                                     WIPProductBLL.Instance.GetWipLockDataTotalCount ,
                                                     WIPProductBLL.Instance.GetWipLockData,
                                                       "WipLockData");
                return ResponseMessage(fullResponse);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }


        /// <summary>
        /// check row count for DownloadProductTestDatasSummary
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetWipLockDataTotalCount(DownloadWipLockDataRequest request)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
            GetWipLockDataModelQuery queryModel = GetModelQueryData<DownloadWipLockDataRequest, GetWipLockDataModelQuery, GetWipLockDataModelQuery.Item>(request);
            long rowCount = WIPProductBLL.Instance.GetWipLockDataTotalCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }
        /// <summary>
        /// 获取产线锁记录
        /// </summary>
        [HttpPost]
        public GetWipLockDataResponse GetWipLockData(GetWipLockDataRequest request)
        {
            GetWipLockDataResponse response = GetBusinessResponseDataInited<GetWipLockDataResponse>();
            try
            {
                GetWipLockDataModelQuery queryModel = new GetWipLockDataModelQuery();
                queryModel.Data = request.Data;
                queryModel.Pager = new QueryPager();
                queryModel.Pager.CopyModelValueFrom(request.Pager);
                GetWipLockDataResultModel dbDatas = WIPProductBLL.Instance.GetWipLockData(queryModel);
                response.Data = dbDatas.Data;
                response.Pager = new ResponsePager()
                {
                    TotalCount = dbDatas.Pager.TotalCount
                };
                response.BussinesCode = EmBussinesCodeType.Success;
            }
            catch (Exception ex)
            {

                response = GetResponseError(ref response, EmBussinesCodeType.ReportUnKnowError);
            }
    

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public GetWIPInfoResponse GetWIPInfo(GetWIPInfoRequest request)
        {
            GetWIPInfoResponse response = GetBusinessResponseDataInited<GetWIPInfoResponse>();
            var wipInfo=   WIPProductBLL.Instance.GetWipInfo(request.Data.IntSN);
            response.Data = new GetWIPInfoResponseItem()
            {
                HeaderInfo=new GetWIPInfoResponseItem.Header() {
                    ChangedBy = wipInfo.WIPHeader.ChangedBy,
                    ChangedOn = wipInfo.WIPHeader.ChangedOn,
                    CurrentProcess = wipInfo.WIPHeader.CurrentProcess,
                    DJ = wipInfo.WIPHeader.DJ,
                    IntSN = wipInfo.WIPHeader.IntSN,
                    InvOrg = wipInfo.WIPHeader.InvOrg,
                    JobID = wipInfo.WIPHeader.JobID,
                    Model = wipInfo.WIPHeader.Model,
                    MotherBoardSN = wipInfo.WIPHeader.MotherBoardSN,
                    PCBA = wipInfo.WIPHeader.PCBA,
                    Result = wipInfo.WIPHeader.Result,
                },
                FlowList = wipInfo.WIPFlowList.ConvertAll(x =>
                 {
                     var flow = new GetWIPInfoResponseItem.Flow();
                     flow.CopyModelValue(x);
                     return flow;
                 }),
                PropertyList = wipInfo.WIPPropertyList.ConvertAll(x =>
                 {
                     var flow = new GetWIPInfoResponseItem.Property();
                     flow.CopyModelValue(x);
                     return flow;
                 }),
                TestDataList = wipInfo.WIPTestDataList.ConvertAll(x =>
               {
                   var flow = new GetWIPInfoResponseItem.TestData();
                   flow.CopyModelValue(x);
                   return flow;
               })

            };
            return response;
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
        /// check row count for DownloadProductTestDatasSummary
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetProductTestDatasSummaryTotalCount(ReportDownloadProductTestDatasRequest request)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
            ReportProductTestDataQuery queryModel = GetModelQuery<ReportDownloadProductTestDatasRequest, ReportProductTestDataQuery>(request); ;
            long rowCount = ProductBLL.Instance.ProductTestDataSummaryGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }

        /// <summary>
        /// 获取产品测试数据详细资料
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportWIPTDHeaderResponse GetWIPTDHeader([FromBody] ReportWIPTDHeaderRequest requestData)
        {
            ReportWIPTestDataQuery queryModel = GetModelQuery<ReportWIPTDHeaderRequest, ReportWIPTestDataQuery>(requestData);
            ReportWIPTDHeaderModel dbDatas = WIPProductBLL.Instance.GetWIPTDHeader(queryModel);
            return GetReportQueryResponse<ReportWIPTDHeaderResponse,
                                            ReportWIPTDHeaderResponse.Item,
                                            ReportWIPTDHeaderModel.Item>(dbDatas);
        }


        /// <summary>
        /// get Wip Satus Detail data by page
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public GetWipStatusDataDetailResponse GetWipStatusDataDetail(GetWipStatusDataRequest request)
        {

            var queryModel = GetModelQueryData<GetWipStatusDataRequest,
                                                    GetWipStatusQuery,
                                                    GetWipStatusQuery.Item>(request);

            WipStatusDetailModel dbDatas = WIPProductBLL.Instance.GetWipStatusDetailByPage(queryModel);
            return GetReportQueryResponse<GetWipStatusDataDetailResponse,
                                            GetWipStatusDataDetailResponseItem,
                                                WipStatusDetailItem>(dbDatas);


        }

        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetWipStatusDetailTotalCount(DownloadWipStatusDataRequest request)
        { 
            GetWipStatusQuery queryModel = GetModelQueryData<DownloadWipStatusDataRequest, GetWipStatusQuery, GetWipStatusQuery.Item>(request);
            long rowCount = WIPProductBLL.Instance.GetWipStatusDetailTotalCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }
        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetWipStatusSummaryTotalCount(DownloadWipStatusDataRequest request)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
            GetWipStatusQuery queryModel = GetModelQueryData<DownloadWipStatusDataRequest, GetWipStatusQuery, GetWipStatusQuery.Item>(request);
            long rowCount = WIPProductBLL.Instance.GetWipStatusSummaryTotalCount (queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }
        /// <summary>
        /// download WipStatusDataDetail
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadWipStatusDataDetail(DownloadWipStatusDataRequest request)
        {
            try
            {
                GetWipStatusQuery queryModel = GetModelQueryData<DownloadWipStatusDataRequest, GetWipStatusQuery, GetWipStatusQuery.Item>(request);
                HttpResponseMessage fullResponse = null;
                long rowCount = WIPProductBLL.Instance.GetWipStatusDetailTotalCount(queryModel);
                if (rowCount > 0 && rowCount < MaxDownloadRowCount)
                {
                    queryModel.Pager = null;
                    var dataList = WIPProductBLL.Instance.GetWipStatusDetail(queryModel);
                    fullResponse = GetDownloadExcelResponse(dataList, request.TableHeaders, "WipStatusReportDetail");
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
        /// 分页获取 Wip Satus Summary 数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public GetWipStatusDataSummaryResponse GetWipStatusDataSummary(GetWipStatusDataRequest request)
        {

            var queryModel = GetModelQueryData<GetWipStatusDataRequest,
                                                    GetWipStatusQuery,
                                                    GetWipStatusQuery.Item>(request);

            WipStatusSummaryModel dbDatas = WIPProductBLL.Instance.GetWipStatusSummaryByPage(queryModel);
            var response= GetReportQueryResponse<GetWipStatusDataSummaryResponse,
                                            GetWipStatusDataSummaryResponseItem,
                                            WipStatusSummaryItem>(dbDatas);
            response.SummaryCount = dbDatas.SummaryCount;
            return response;


        }

        /// <summary>
        /// 下载 WipStatusDataSummary
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadWipStatusDataSummary(DownloadWipStatusDataRequest request)
        {
            try
            {
                GetWipStatusQuery queryModel = GetModelQueryData<DownloadWipStatusDataRequest, GetWipStatusQuery, GetWipStatusQuery.Item>(request);
                HttpResponseMessage fullResponse = null;
                long rowCount = WIPProductBLL.Instance.GetWipStatusSummaryTotalCount(queryModel);
                if (rowCount > 0 && rowCount < MaxDownloadRowCount)
                {
                    queryModel.Pager = null;
                    var dataList = WIPProductBLL.Instance.GetWipStatusSummary(queryModel);
                    fullResponse = GetDownloadExcelResponse(dataList, request.TableHeaders, "WipStatusReportSummary");
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
        /// 下载产品测试数据汇总
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadWIPTDHeader([FromBody] ReportDownloadWIPTDHeaderRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportWIPTestDataQuery queryModel = GetModelQuery<ReportDownloadWIPTDHeaderRequest, ReportWIPTestDataQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     WIPProductBLL.Instance.ReportWIPTDHeaderGetRowCount,
                                                     WIPProductBLL.Instance.GetWIPTDHeader,
                                                       "WIPProductTDHeader");
                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        /// <summary>
        /// check row count for GetWIPTDHeaderRowCount
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetWIPTDHeaderRowCount(ReportDownloadWIPTDHeaderRequest request)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
            ReportWIPTestDataQuery queryModel = GetModelQuery<ReportDownloadWIPTDHeaderRequest, ReportWIPTestDataQuery>(request);
            long rowCount = WIPProductBLL.Instance.ReportWIPTDHeaderGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }

        /// <summary>
        /// 获取产品测试数据详细资料
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportWIPTestDataResponse GetWIPTestData([FromBody] ReportWIPTestDataRequest requestData)
        {
            ReportWIPTestDataQuery queryModel = GetModelQuery<ReportWIPTestDataRequest, ReportWIPTestDataQuery>(requestData);
            ReportWIPTestDataModel dbDatas = WIPProductBLL.Instance.GetWIPTestData(queryModel);
            return GetReportQueryResponse<ReportWIPTestDataResponse,
                                            ReportWIPTestDataResponse.Item,
                                            ReportWIPTestDataModel.Item>(dbDatas);
        }


        /// <summary>
        /// 下载产品测试数据汇总
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadWIPTestData([FromBody] ReportDownloadWIPTestDataRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportWIPTestDataQuery queryModel = GetModelQuery<ReportDownloadWIPTestDataRequest, ReportWIPTestDataQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     WIPProductBLL.Instance.ReportWIPTestDataGetRowCount,
                                                     WIPProductBLL.Instance.GetWIPTestData,
                                                       "WIPProductTDItem");
                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// check row count for DownloadWIPTestData
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetWIPTDHeaderRowCount(ReportDownloadWIPTestDataRequest request)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
            ReportWIPTestDataQuery queryModel = GetModelQuery<ReportDownloadWIPTestDataRequest, ReportWIPTestDataQuery>(request);
            long rowCount = WIPProductBLL.Instance.ReportWIPTDHeaderGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }

        /// <summary>
        /// 获取产品属性资料
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportWIPPropertiesResponse GetWIPProperties([FromBody] ReportWIPTestDataRequest requestData)
        {
            ReportWIPTestDataQuery queryModel = GetModelQuery<ReportWIPTestDataRequest, ReportWIPTestDataQuery>(requestData);
            ReportWIPPropertiesModel dbDatas = WIPProductBLL.Instance.GetWIPProperties(queryModel);
            return GetReportQueryResponse<ReportWIPPropertiesResponse,
                                            ReportWIPPropertiesResponse.Item,
                                            ReportWIPPropertiesModel.Item>(dbDatas);
        }


     
    }
}