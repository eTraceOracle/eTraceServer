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
    /// ReportEquipmentRepairHeaderController
    /// </summary>
    public class ReportSMFixtureVerificationController : ServerBaseController<ReportSMFixtureVerificationController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportSMFixtureVerificationController()
        {

        }

        /// <summary>
        /// DownLoadSMFixtureVerificationDatas
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownLoadSMFixtureVerificationDatas([FromBody] DownLoadSMFixtureVerificationRequest requestData)
        {
            try
            {
                HttpResponseMessage fullResponse;
                ReportSMFixtureVerificationModelQuery queryModel = GetModelQuery<DownLoadSMFixtureVerificationRequest, ReportSMFixtureVerificationModelQuery>(requestData);

                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     SMFixtureVerificationBLL.Instance.SMFixtureVerificationDataGetRowCount,
                                                     SMFixtureVerificationBLL.Instance.GetSMFixtureVerificationDatas,
                                                       "SMFixtureVerification");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// GetSMFixtureVerificationDatas
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportSMFixtureVerificationResponse GetSMFixtureVerificationDatas([FromBody] ReportSMFixtureVerificationRequest requestData)
        {
            ReportSMFixtureVerificationResponse response = GetBusinessResponseDataInited<ReportSMFixtureVerificationResponse>();
            try
            {
                ReportSMFixtureVerificationModelQuery queryModel = GetModelQuery<ReportSMFixtureVerificationRequest, ReportSMFixtureVerificationModelQuery>(requestData);

                ReportSMFixtureVerificationModel dbDatas = SMFixtureVerificationBLL.Instance.GetSMFixtureVerificationDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMFixtureVerificationResponse.Item();
                            data.CopyModelValueFrom<ReportSMFixtureVerificationResponse.Item, ReportSMFixtureVerificationModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMFixtureVerificationResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
