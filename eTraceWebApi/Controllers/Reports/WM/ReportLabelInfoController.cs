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
    /// ReportLabelInfoController
    /// </summary>
    public class ReportLabelInfoController : ServerBaseController<ReportLabelInfoController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportLabelInfoController()
        {
        }


        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetLabelInfoDetailTotalCount(DownLoadLabelInfoDetailRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();

            ReportLabelInfoModelQuery queryModel = GetModelQuery<DownLoadLabelInfoDetailRequest, ReportLabelInfoModelQuery>(requestData);
            queryModel.ReportType = "Detail";

            long rowCount = LabelInfoBLL.Instance.LabelInfoDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }

        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetLabelInfoSummaryTotalCount(DownLoadLabelInfoSummaryRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();

            ReportLabelInfoModelQuery queryModel = GetModelQuery<DownLoadLabelInfoSummaryRequest, ReportLabelInfoModelQuery>(requestData);
            queryModel.ReportType = "Summary";

            long rowCount = LabelInfoBLL.Instance.LabelInfoDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }

        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="ePurgeData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetLabelInfoePurgeTotalCount(DownLoadLabelInfoePurgeRequest ePurgeData)
        {
            var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();

            ReportLabelInfoModelQuery queryModel = GetModelQuery<DownLoadLabelInfoePurgeRequest, ReportLabelInfoModelQuery>(ePurgeData);

            //Reset ReportType as ePurge Summary here
            queryModel.ReportType = "ePurgeSM";
            long rowCountSM = LabelInfoBLL.Instance.LabelInfoDataGetRowCount(queryModel);
            long rowCount = rowCountSM;


            //Reset ReportType as ePurge Detail here
            queryModel.ReportType = "ePurgeDT";
            long rowCountDT = LabelInfoBLL.Instance.LabelInfoDataGetRowCount(queryModel);

            if (rowCount < rowCountDT)
            {
                rowCount = rowCountDT;
            }
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }


        /// <summary>
        /// 获取下载 Detail Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadLabelInfoDetailData([FromBody] DownLoadLabelInfoDetailRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportLabelInfoModelQuery queryModel = GetModelQuery<DownLoadLabelInfoDetailRequest, ReportLabelInfoModelQuery>(requestData);
                queryModel.ReportType = "Detail";

                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     //LabelInfoBLL.Instance.LabelInfoDataGetRowCount,
                                                     LabelInfoBLL.Instance.GetLabelInfoDetailData,
                                                       "MaterialLabelDetail");

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
        public IHttpActionResult DownloadLabelInfoSummaryData([FromBody] DownLoadLabelInfoSummaryRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportLabelInfoModelQuery queryModel = GetModelQuery<DownLoadLabelInfoSummaryRequest, ReportLabelInfoModelQuery>(requestData);
                queryModel.ReportType = "Summary";

                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     //LabelInfoBLL.Instance.LabelInfoDataGetRowCount,
                                                     LabelInfoBLL.Instance.GetLabelInfoSummaryData,
                                                       "MaterialLabelSummary");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }



        /// <summary>
        /// 获取下载 ePurge Data
        /// </summary>
        /// <param name="ePurgeData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadLabelInfoePurgeData([FromBody] DownLoadLabelInfoePurgeRequest ePurgeData)
        {
            HttpResponseMessage fullResponse;

            // Download ePurge Data
            ReportLabelInfoModelQuery queryModel = GetModelQuery<DownLoadLabelInfoePurgeRequest, ReportLabelInfoModelQuery>(ePurgeData);

            //Reset ReportType as ePurge Summary here
            queryModel.ReportType = "ePurgeSM";

            //调用查询方法 
            ReportLabelInfoePurgeSMModel SMdbDatas = LabelInfoBLL.Instance.GetLabelInfoePurgeSMData(queryModel);

            //构造参数
            ExcelHelper.SaveMultiSheetModel SMsaveModel = new ExcelHelper.SaveMultiSheetModel()
            {
                ClumnsHeaderMapperList = ePurgeData.SMTableHeaders
                    .Select(x => new ExcelHelper.ClumnsHeaderMapper()
                    {
                        HeaderOrder = x.HeaderOrder,
                        OriginalName = x.ColumnName,
                        NewName = x.HeaderLabel
                    }).ToList(),
                DataList = SMdbDatas.Data.Select(x => (object)x).ToList(),
                SheetName = "Summary"
            };
            List<ExcelHelper.SaveMultiSheetModel> modelList = new List<ExcelHelper.SaveMultiSheetModel>();
            modelList.Add(SMsaveModel);


            //Reset ReportType as ePurge Detail here
            queryModel.ReportType = "ePurgeDT";

             //调用查询方法 
            ReportLabelInfoePurgeDTModel DTdbDatas = LabelInfoBLL.Instance.GetLabelInfoePurgeDTData(queryModel);
            //构造参数
            ExcelHelper.SaveMultiSheetModel DTsaveModel = new ExcelHelper.SaveMultiSheetModel()
            {
                ClumnsHeaderMapperList = ePurgeData.DTTableHeaders
                    .Select(x => new ExcelHelper.ClumnsHeaderMapper()
                    {
                        HeaderOrder = x.HeaderOrder,
                        OriginalName = x.ColumnName,
                        NewName = x.HeaderLabel
                    }).ToList(),
                DataList = DTdbDatas.Data.Select(x => (object)x).ToList(),
                SheetName = "Detail"
            };
            modelList.Add(DTsaveModel);

            MemoryStream stream =ExcelHelper.Instance.SaveMultiSheet(modelList);
            if (stream != null)
            {
                fullResponse = Request.CreateResponse(HttpStatusCode.OK);
                fullResponse.Content = new StreamContent(stream);
                //define file Name
                var excelHelper = new ExcelHelper();
                string fileFullName = excelHelper.GetFileName("MaterialLabelePurge");
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


        /// <summary>
        /// 获取 Detail Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportLabelInfoDetailResponse GetLabelInfoDetailData([FromBody] ReportLabelInfoRequest requestData)
        {
            ReportLabelInfoDetailResponse response = GetBusinessResponseDataInited<ReportLabelInfoDetailResponse>();
            try
            {
                ReportLabelInfoModelQuery queryModel = GetModelQuery<ReportLabelInfoRequest, ReportLabelInfoModelQuery>(requestData);
                queryModel.ReportType = "Detail";

                ReportLabelInfoDetailModel dbDatas = LabelInfoBLL.Instance.GetLabelInfoDetailData(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportLabelInfoDetailResponse.Detail();
                            data.CopyModelValueFrom<ReportLabelInfoDetailResponse.Detail, ReportLabelInfoDetailModel.Detail>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportLabelInfoDetailResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        /// 获取 Summary Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportLabelInfoSummaryResponse GetLabelInfoSummaryData([FromBody] ReportLabelInfoRequest requestData)
        {
            ReportLabelInfoSummaryResponse response = GetBusinessResponseDataInited<ReportLabelInfoSummaryResponse>();
            try
            {
                ReportLabelInfoModelQuery queryModel = GetModelQuery<ReportLabelInfoRequest, ReportLabelInfoModelQuery>(requestData);
                queryModel.ReportType = "Summary";

                ReportLabelInfoSummaryModel dbDatas = LabelInfoBLL.Instance.GetLabelInfoSummaryData(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportLabelInfoSummaryResponse.Summary();
                            data.CopyModelValueFrom<ReportLabelInfoSummaryResponse.Summary, ReportLabelInfoSummaryModel.Summary>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportLabelInfoSummaryResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        /// 获取 ePurge Detail Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportLabelInfoePurgeDTResponse GetLabelInfoePurgeDTData([FromBody] ReportLabelInfoRequest requestData)
        {
            ReportLabelInfoePurgeDTResponse response = GetBusinessResponseDataInited<ReportLabelInfoePurgeDTResponse>();
            try
            {
                ReportLabelInfoModelQuery queryModel = GetModelQuery<ReportLabelInfoRequest, ReportLabelInfoModelQuery>(requestData);

                //Reset ReportType as ePurge Detail here
                queryModel.ReportType = "ePurgeDT";

                ReportLabelInfoePurgeDTModel dbDatas = LabelInfoBLL.Instance.GetLabelInfoePurgeDTData(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportLabelInfoePurgeDTResponse.Detail();
                            data.CopyModelValueFrom<ReportLabelInfoePurgeDTResponse.Detail, ReportLabelInfoePurgeDTModel.Detail>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportLabelInfoePurgeDTResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        /// 获取 ePurge Summary Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportLabelInfoePurgeSMResponse GetLabelInfoePurgeSMData([FromBody] ReportLabelInfoRequest requestData)
        {
            ReportLabelInfoePurgeSMResponse response = GetBusinessResponseDataInited<ReportLabelInfoePurgeSMResponse>();
            try
            {
                ReportLabelInfoModelQuery queryModel = GetModelQuery<ReportLabelInfoRequest, ReportLabelInfoModelQuery>(requestData);

                //Reset ReportType as ePurge Summary here
                queryModel.ReportType = "ePurgeSM";

                ReportLabelInfoePurgeSMModel dbDatas = LabelInfoBLL.Instance.GetLabelInfoePurgeSMData(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportLabelInfoePurgeSMResponse.Summary();
                            data.CopyModelValueFrom<ReportLabelInfoePurgeSMResponse.Summary, ReportLabelInfoePurgeSMModel.Summary>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportLabelInfoePurgeSMResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
