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
using System.Web;

namespace eTraceWebApi.Controllers
{
    /// <summary>
    /// ReportEquipmentFixturePMHeaderController
    /// </summary>
    public class ReportEquipmentFixturePMHeaderController : ServerBaseController<ReportEquipmentFixturePMHeaderController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportEquipmentFixturePMHeaderController()
        {

        }

        /// <summary>
        /// 获取下载保养数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadEquipmentFixturePMHeaderDatas([FromBody] DownLoadEquipmentFixturePMHeaderDatasRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportEquipmentFixturePMHeaderQuery queryModel = GetModelQuery<DownLoadEquipmentFixturePMHeaderDatasRequest, ReportEquipmentFixturePMHeaderQuery>(requestData);
                //fullResponse = DownloadReportResponse(queryModel,
                //                                     requestData.TableHeaders,
                //                                     ProductModuleBLL.Instance.ProductTestDataGetRowCount,
                //                                     ProductModuleBLL.Instance.GetProductTestData,
                //                                       "");
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     EquipmentFixturePMHeaderBLL.Instance.EquipmentFixturePMHeaderDataGetRowCount,
                                                     EquipmentFixturePMHeaderBLL.Instance.GetSMEquipmentFixturePMHeaderDatas,
                                                       "PMHeader");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }


        /// <summary>
        /// 获取下载保养数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadEquipmentFixturePMDetailDatas([FromBody] DownLoadEquipmentFixturePMDetailDatasRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportEquipmentFixturePMHeaderQuery queryModel = GetModelQuery<DownLoadEquipmentFixturePMDetailDatasRequest, ReportEquipmentFixturePMHeaderQuery>(requestData);
                //fullResponse = DownloadReportResponse(queryModel,
                //                                     requestData.TableHeaders,
                //                                     ProductModuleBLL.Instance.ProductTestDataGetRowCount,
                //                                     ProductModuleBLL.Instance.GetProductTestData,
                //                                       "");
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     EquipmentFixturePMHeaderBLL.Instance.EquipmentFixturePMHeaderDataGetRowCount,
                                                     EquipmentFixturePMHeaderBLL.Instance.GetSMEquipmentFixturePMDetailDatas,
                                                       "");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// /Check for PMHeader Download
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetPMSummaryDataTotalCount(DownLoadEquipmentFixturePMHeaderDatasRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
            ReportEquipmentFixturePMHeaderQuery queryModel = GetModelQuery<DownLoadEquipmentFixturePMHeaderDatasRequest, ReportEquipmentFixturePMHeaderQuery>(requestData);
            long rowCount = EquipmentFixturePMHeaderBLL.Instance.EquipmentFixturePMHeaderDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
            //response.BussinesCode = EmBussinesCodeType.Success;
            //response.LessThanCheckDownloadRowCount = (rowCount <= CheckDownloadRowCount);
            //response.RowCount = rowCount;
            //return response;
        }

        /// <summary>
        /// /Check for PMItem Download
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetPMDetailDataTotalCount(DownLoadEquipmentFixturePMDetailDatasRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
            ReportEquipmentFixturePMHeaderQuery queryModel = GetModelQuery<DownLoadEquipmentFixturePMDetailDatasRequest, ReportEquipmentFixturePMHeaderQuery>(requestData);
            long rowCount = EquipmentFixturePMHeaderBLL.Instance.EquipmentFixturePMDetailDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
            //response.BussinesCode = EmBussinesCodeType.Success;
            //response.LessThanCheckDownloadRowCount = (rowCount <= CheckDownloadRowCount);
            //response.RowCount = rowCount;
            //return response;
        }

        /// <summary>
        /// 获取保养数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportEquipmentFixturePMHeaderResponse GetSMEquipmentFixturePMHeaderDatas([FromBody] ReportEquipmentFixturePMHeaderRequest requestData)
        {
            ReportEquipmentFixturePMHeaderResponse response = GetBusinessResponseDataInited<ReportEquipmentFixturePMHeaderResponse>();
            try
            {
                ReportEquipmentFixturePMHeaderQuery queryModel = GetModelQuery<ReportEquipmentFixturePMHeaderRequest, ReportEquipmentFixturePMHeaderQuery>(requestData);

                ReportEquipmentFixturePMHeaderModel dbDatas = EquipmentFixturePMHeaderBLL.Instance.GetSMEquipmentFixturePMHeaderDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportEquipmentFixturePMHeaderResponse.Item();
                            data.CopyModelValueFrom<ReportEquipmentFixturePMHeaderResponse.Item, ReportEquipmentFixturePMHeaderModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportEquipmentFixturePMHeaderResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        /// 获取保养明细
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportEquipmentFixturePMDetailResponse GetSMEquipmentFixturePMDetailDatas([FromBody] ReportEquipmentFixturePMHeaderRequest requestData)
        {
            ReportEquipmentFixturePMDetailResponse response = GetBusinessResponseDataInited<ReportEquipmentFixturePMDetailResponse>();
            try
            {
                ReportEquipmentFixturePMHeaderQuery queryModel = GetModelQuery<ReportEquipmentFixturePMHeaderRequest, ReportEquipmentFixturePMHeaderQuery>(requestData);

                ReportEquipmentFixturePMDetailModel dbDatas = EquipmentFixturePMHeaderBLL.Instance.GetSMEquipmentFixturePMDetailDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportEquipmentFixturePMDetailResponse.Item();
                            data.CopyModelValueFrom<ReportEquipmentFixturePMDetailResponse.Item, ReportEquipmentFixturePMDetailModel.DetailItem>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportEquipmentFixturePMDetailResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        /// Get PM Item
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportEquipmentFixturePMItemResponse GetSMEquipmentFixturePMItemDatas([FromBody] ReportEquipmentFixturePMHeaderRequest requestData)
        {
            ReportEquipmentFixturePMItemResponse response = GetBusinessResponseDataInited<ReportEquipmentFixturePMItemResponse>();
            try
            {
                ReportEquipmentFixturePMHeaderQuery queryModel = GetModelQuery<ReportEquipmentFixturePMHeaderRequest, ReportEquipmentFixturePMHeaderQuery>(requestData);

                ReportEquipmentFixturePMItemModel dbDatas = EquipmentFixturePMHeaderBLL.Instance.GetSMEquipmentFixturePMItemDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportEquipmentFixturePMItemResponse.PMItem();
                            data.CopyModelValueFrom<ReportEquipmentFixturePMItemResponse.PMItem, ReportEquipmentFixturePMItemModel.PMItems>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportEquipmentFixturePMItemResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        /// Get PM Mat
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportEquipmentFixturePMMatResponse GetSMEquipmentFixturePMMatDatas([FromBody] ReportEquipmentFixturePMHeaderRequest requestData)
        {
            ReportEquipmentFixturePMMatResponse response = GetBusinessResponseDataInited<ReportEquipmentFixturePMMatResponse>();
            try
            {
                ReportEquipmentFixturePMHeaderQuery queryModel = GetModelQuery<ReportEquipmentFixturePMHeaderRequest, ReportEquipmentFixturePMHeaderQuery>(requestData);

                ReportEquipmentFixturePMMatModel dbDatas = EquipmentFixturePMHeaderBLL.Instance.GetSMEquipmentFixturePMMatDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportEquipmentFixturePMMatResponse.PMMat();
                            data.CopyModelValueFrom<ReportEquipmentFixturePMMatResponse.PMMat, ReportEquipmentFixturePMMatModel.PMMat>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportEquipmentFixturePMMatResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        /// 获取设备状态下拉
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServerResponse<List<string>> GetPMFrequency()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = EquipmentFixturePMHeaderBLL.Instance.GetPMFrequency();
            response.Data = dbDatas;
            return response;
        }

        /// <summary>
        /// Open PM Attachment
        /// </summary>
        [HttpGet]
        public HttpResponseMessage OpenPMAttachment(string fileName)
        {
            //string fileName = "报表模板.xlsx";
            //string filePath = HttpContext.Current.Server.MapPath("~/") + "FileRoot\\" + "ReportTemplate.xlsx";
            string filePath = System.Configuration.ConfigurationManager.AppSettings["AttachmentFolder"] + fileName;
            FileStream stream = new FileStream(filePath, FileMode.Open);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = HttpUtility.UrlEncode(fileName)
            };
            response.Headers.Add("Access-Control-Expose-Headers", "FileName");
            response.Headers.Add("FileName", HttpUtility.UrlEncode(fileName));
            return response;
        }

    }
}
