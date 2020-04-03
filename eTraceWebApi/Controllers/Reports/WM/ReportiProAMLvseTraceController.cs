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
    /// ReportiProAMLvseTraceController
    /// </summary>
    public class ReportiProAMLvseTraceController : ServerBaseController<ReportiProAMLvseTraceController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportiProAMLvseTraceController()
        {

        }

        /// <summary>
        /// 下载 AML Ipro vs eTrace Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadiProAMLvseTraceData([FromBody] DownLoadiProAMLvseTraceRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportiProAMLvseTraceModelQuery queryModel = GetModelQuery<DownLoadiProAMLvseTraceRequest, ReportiProAMLvseTraceModelQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     iProAMLvseTraceBLL.Instance.GetiProAMLvseTraceData,
                                                     "AML");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }



        /// <summary>
        /// 获取 AML Ipro vs eTrace Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportiProAMLvseTraceResponse GetiProAMLvseTraceData([FromBody] ReportiProAMLvseTraceRequest requestData)
        {
            ReportiProAMLvseTraceResponse response = GetBusinessResponseDataInited<ReportiProAMLvseTraceResponse>();
            try
            {
                ReportiProAMLvseTraceModelQuery queryModel = GetModelQuery<ReportiProAMLvseTraceRequest, ReportiProAMLvseTraceModelQuery>(requestData);

                ReportiProAMLvseTraceModel dbDatas = iProAMLvseTraceBLL.Instance.GetiProAMLvseTraceData(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportiProAMLvseTraceResponse.Item();
                            data.CopyModelValueFrom<ReportiProAMLvseTraceResponse.Item, ReportiProAMLvseTraceModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportiProAMLvseTraceResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
