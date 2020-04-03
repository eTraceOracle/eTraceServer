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
    /// ReportAutoeJITPlanController
    /// </summary>
    public class ReportAutoeJITPlanController : ServerBaseController<ReportAutoeJITPlanController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportAutoeJITPlanController()
        {
        }


        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetAutoeJITPlanTotalCount(DownLoadAutoeJITPlanRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();

            ReportAutoeJITPlanModelQuery queryModel = GetModelQuery<DownLoadAutoeJITPlanRequest, ReportAutoeJITPlanModelQuery>(requestData);

            long rowCount = AutoeJITPlanBLL.Instance.AutoeJITPlanDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }


        /// <summary>
        /// 获取下载 CLID Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadAutoeJITPlanData([FromBody] DownLoadAutoeJITPlanRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportAutoeJITPlanModelQuery queryModel = GetModelQuery<DownLoadAutoeJITPlanRequest, ReportAutoeJITPlanModelQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     //AutoeJITPlanBLL.Instance.AutoeJITPlanDataGetRowCount,
                                                     AutoeJITPlanBLL.Instance.GetAutoeJITPlanData,
                                                       "eJITDeliveryPlan");

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
        public ReportAutoeJITPlanResponse GetAutoeJITPlanData([FromBody] ReportAutoeJITPlanRequest requestData)
        {
            ReportAutoeJITPlanResponse response = GetBusinessResponseDataInited<ReportAutoeJITPlanResponse>();
            try
            {
                ReportAutoeJITPlanModelQuery queryModel = GetModelQuery<ReportAutoeJITPlanRequest, ReportAutoeJITPlanModelQuery>(requestData);

                ReportAutoeJITPlanModel dbDatas = AutoeJITPlanBLL.Instance.GetAutoeJITPlanData(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportAutoeJITPlanResponse.Item();
                            data.CopyModelValueFrom<ReportAutoeJITPlanResponse.Item, ReportAutoeJITPlanModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportAutoeJITPlanResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
