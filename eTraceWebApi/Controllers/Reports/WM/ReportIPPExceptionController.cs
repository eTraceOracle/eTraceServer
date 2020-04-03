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
    /// ReportIPPExceptionController
    /// </summary>
    public class ReportIPPExceptionController : ServerBaseController<ReportIPPExceptionController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportIPPExceptionController()
        {
        }

        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetIPPExceptionTotalCount(DownLoadIPPExceptionRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();

            ReportIPPExceptionModelQuery queryModel = GetModelQuery<DownLoadIPPExceptionRequest, ReportIPPExceptionModelQuery>(requestData);

            long rowCount = IPPExceptionBLL.Instance.IPPExceptionDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }

        /// <summary>
        /// 获取下载 CLID Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadIPPExceptionData([FromBody] DownLoadIPPExceptionRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportIPPExceptionModelQuery queryModel = GetModelQuery<DownLoadIPPExceptionRequest, ReportIPPExceptionModelQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     //IPPExceptionBLL.Instance.IPPExceptionDataGetRowCount,
                                                     IPPExceptionBLL.Instance.GetIPPExceptionData,
                                                       "IPPException");

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
        public ReportIPPExceptionResponse GetIPPExceptionData([FromBody] ReportIPPExceptionRequest requestData)
        {
            ReportIPPExceptionResponse response = GetBusinessResponseDataInited<ReportIPPExceptionResponse>();
            try
            {
                ReportIPPExceptionModelQuery queryModel = GetModelQuery<ReportIPPExceptionRequest, ReportIPPExceptionModelQuery>(requestData);

                ReportIPPExceptionModel dbDatas = IPPExceptionBLL.Instance.GetIPPExceptionByPage(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportIPPExceptionResponse.Item();
                            data.CopyModelValueFrom<ReportIPPExceptionResponse.Item, ReportIPPExceptionModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportIPPExceptionResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
