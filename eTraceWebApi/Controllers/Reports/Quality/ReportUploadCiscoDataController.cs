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
    /// ReportUploadCiscoDataController
    /// </summary>
    public class ReportUploadCiscoDataController : ServerBaseController<ReportUploadCiscoDataController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportUploadCiscoDataController()
        {

        }

        /// <summary>
        /// 获取下载保养数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadUploadCiscoDataDatas([FromBody] DownLoadUploadCiscoDataDatasRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportUploadCiscoDataQuery queryModel = GetModelQuery<DownLoadUploadCiscoDataDatasRequest, ReportUploadCiscoDataQuery>(requestData);
                //fullResponse = DownloadReportResponse(queryModel,
                //                                     requestData.TableHeaders,
                //                                     ProductModuleBLL.Instance.ProductTestDataGetRowCount,
                //                                     ProductModuleBLL.Instance.GetProductTestData,
                //                                       "");
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     UploadCiscoDataBLL.Instance.UploadCiscoDataDataGetRowCount,
                                                     UploadCiscoDataBLL.Instance.GetUploadCiscoDataData,
                                                       "CiscoDataUpload");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// check row count for Download
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetUploadCiscoDataTotalCount(DownLoadUploadCiscoDataDatasRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
            ReportUploadCiscoDataQuery queryModel = GetModelQuery<DownLoadUploadCiscoDataDatasRequest, ReportUploadCiscoDataQuery>(requestData);
            long rowCount = UploadCiscoDataBLL.Instance.UploadCiscoDataDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
            
        }

        /// <summary>
        /// 获取保养数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportUploadCiscoDataResponse GetUploadCiscoDataDatas([FromBody] ReportUploadCiscoDataRequest requestData)
        {
            ReportUploadCiscoDataResponse response = GetBusinessResponseDataInited<ReportUploadCiscoDataResponse>();
            try
            {
                ReportUploadCiscoDataQuery queryModel = GetModelQuery<ReportUploadCiscoDataRequest, ReportUploadCiscoDataQuery>(requestData);

                ReportUploadCiscoDataModel dbDatas = UploadCiscoDataBLL.Instance.GetUploadCiscoDataData(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportUploadCiscoDataResponse.Item();
                            data.CopyModelValueFrom<ReportUploadCiscoDataResponse.Item, ReportUploadCiscoDataModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportUploadCiscoDataResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
