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
    public class ReportSMLocController : ServerBaseController<ReportSMLocController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportSMLocController()
        {

        }

        /// <summary>
        /// Get SMLoc DownLoad Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownLoadSMLocDatas([FromBody] DownLoadSMLocRequest requestData)
        {
            try
            {
                HttpResponseMessage fullResponse;
                ReportSMLocModelQuery queryModel = GetModelQuery<DownLoadSMLocRequest, ReportSMLocModelQuery>(requestData);

                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     SMLocBLL.Instance.SMLocDataGetRowCount,
                                                     SMLocBLL.Instance.GetSMLocDatas,
                                                       "SMLoc");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get SMLoc Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportSMLocResponse GetSMLocDatas([FromBody] ReportSMLocRequest requestData)
        {
            ReportSMLocResponse response = GetBusinessResponseDataInited<ReportSMLocResponse>();
            try
            {
                ReportSMLocModelQuery queryModel = GetModelQuery<ReportSMLocRequest, ReportSMLocModelQuery>(requestData);

                ReportSMLocModel dbDatas = SMLocBLL.Instance.GetSMLocDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMLocResponse.Item();
                            data.CopyModelValueFrom<ReportSMLocResponse.Item, ReportSMLocModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMLocResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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


        [HttpPost]
        public ServerResponse<List<string>> GetSMLocCategory()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = SMLocBLL.Instance.GetSMLocCategory();
            response.Data = dbDatas;
            return response;
        }

        [HttpPost]
        public ServerResponse<List<string>> GetSMLocSubCategory()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = SMLocBLL.Instance.GetSMLocSubCategory();
            response.Data = dbDatas;
            return response;
        }

        /// <summary>
        /// Get Loc Store
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServerResponse<List<string>> GetSMLocStore()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = SMLocBLL.Instance.GetSMLocStore();
            response.Data = dbDatas;
            return response;
        }

    }
}
