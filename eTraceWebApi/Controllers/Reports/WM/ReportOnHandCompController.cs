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
    /// ReportOnHandCompController
    /// </summary>
    public class ReportOnHandCompController : ServerBaseController<ReportOnHandCompController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportOnHandCompController()
        {

        }

        /// <summary>
        /// 下载 Onhand Inventory Oracle vs eTrace Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadOnHandCompData([FromBody] DownLoadOnHandCompRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportOnHandCompModelQuery queryModel = GetModelQuery<DownLoadOnHandCompRequest, ReportOnHandCompModelQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     OnHandCompBLL.Instance.GetOnHandCompData,
                                                     "OH");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }



        /// <summary>
        /// 获取 Onhand Inventory Oracle vs eTrace Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportOnHandCompResponse GetOnHandCompData([FromBody] ReportOnHandCompRequest requestData)
        {
            ReportOnHandCompResponse response = GetBusinessResponseDataInited<ReportOnHandCompResponse>();
            try
            {
                ReportOnHandCompModelQuery queryModel = GetModelQuery<ReportOnHandCompRequest, ReportOnHandCompModelQuery>(requestData);

                ReportOnHandCompModel dbDatas = OnHandCompBLL.Instance.GetOnHandCompData(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportOnHandCompResponse.Item();
                            data.CopyModelValueFrom<ReportOnHandCompResponse.Item, ReportOnHandCompModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportOnHandCompResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
