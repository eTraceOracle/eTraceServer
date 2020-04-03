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
    /// ReporteJITFrequencyController
    /// </summary>
    public class ReporteJITFrequencyController : ServerBaseController<ReporteJITFrequencyController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReporteJITFrequencyController()
        {
        }

        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GeteJITFrequencyDetailTotalCount(DownLoadeJITFrequencyRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();

            ReporteJITFrequencyModelQuery queryModel = GetModelQuery<DownLoadeJITFrequencyRequest, ReporteJITFrequencyModelQuery>(requestData);

            long rowCount = eJITFrequencyBLL.Instance.eJITFrequencyDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }

        /// <summary>
        /// 获取下载 CLID Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadeJITFrequencyData([FromBody] DownLoadeJITFrequencyRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReporteJITFrequencyModelQuery queryModel = GetModelQuery<DownLoadeJITFrequencyRequest, ReporteJITFrequencyModelQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     //eJITFrequencyBLL.Instance.eJITFrequencyDataGetRowCount,
                                                     eJITFrequencyBLL.Instance.GeteJITFrequencyData,
                                                       "eJITDeliveryFrequency");

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
        public ReporteJITFrequencyResponse GeteJITFrequencyData([FromBody] ReporteJITFrequencyRequest requestData)
        {
            ReporteJITFrequencyResponse response = GetBusinessResponseDataInited<ReporteJITFrequencyResponse>();
            try
            {
                ReporteJITFrequencyModelQuery queryModel = GetModelQuery<ReporteJITFrequencyRequest, ReporteJITFrequencyModelQuery>(requestData);

                ReporteJITFrequencyModel dbDatas = eJITFrequencyBLL.Instance.GeteJITFrequencyData(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReporteJITFrequencyResponse.Item();
                            data.CopyModelValueFrom<ReporteJITFrequencyResponse.Item, ReporteJITFrequencyModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReporteJITFrequencyResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
