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

    public class ReportSMMatInvController : ServerBaseController<ReportSMMatInvController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportSMMatInvController()
        {

        }

        /// <summary>
        /// 获取下载数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownLoadSMMatInvDatas([FromBody] DownLoadSMMatInvRequest requestData)
        {
            try
            {
                HttpResponseMessage fullResponse;
                ReportSMMatInvModelQuery queryModel = GetModelQuery<DownLoadSMMatInvRequest, ReportSMMatInvModelQuery>(requestData);

                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     SMMatInvBLL.Instance.SMMatInvDataGetRowCount,
                                                     SMMatInvBLL.Instance.GetSMMatInvDatas,
                                                       "SMMatInv");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// 获取MatInv数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportSMMatInvResponse GetSMMatInvDatas([FromBody] ReportSMMatInvRequest requestData)
        {
            ReportSMMatInvResponse response = GetBusinessResponseDataInited<ReportSMMatInvResponse>();
            try
            {
                ReportSMMatInvModelQuery queryModel = GetModelQuery<ReportSMMatInvRequest, ReportSMMatInvModelQuery>(requestData);

                ReportSMMatInvModel dbDatas = SMMatInvBLL.Instance.GetSMMatInvDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMMatInvResponse.Item();
                            data.CopyModelValueFrom<ReportSMMatInvResponse.Item, ReportSMMatInvModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMMatInvResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
