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
    public class ReportEquipmentPMPlanController : ServerBaseController<ReportEquipmentPMPlanController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportEquipmentPMPlanController()
        {

        }

        /// <summary>
        /// 获取下载维修数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownLoadEquipmentPMPlanDatas([FromBody] DownLoadEquipmentPMPlanRequest requestData)
        {
            try
            {
                HttpResponseMessage fullResponse;
                ReportEquipmentPMPlanModelQuery queryModel = GetModelQuery<DownLoadEquipmentPMPlanRequest, ReportEquipmentPMPlanModelQuery>(requestData);

                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     EquipmentPMPlanBLL.Instance.EquipmentPMPlanDataGetRowCount,
                                                     EquipmentPMPlanBLL.Instance.GetSMEquipmentPMPlanDatas,
                                                       "EquipmentPMPlan");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// 获取维修数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportEquipmentPMPlanResponse GetSMEquipmentPMPlanDatas([FromBody] ReportEquipmentPMPlanRequest requestData)
        {
            ReportEquipmentPMPlanResponse response = GetBusinessResponseDataInited<ReportEquipmentPMPlanResponse>();
            try
            {
                ReportEquipmentPMPlanModelQuery queryModel = GetModelQuery<ReportEquipmentPMPlanRequest, ReportEquipmentPMPlanModelQuery>(requestData);

                ReportEquipmentPMPlanModel dbDatas = EquipmentPMPlanBLL.Instance.GetSMEquipmentPMPlanDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportEquipmentPMPlanResponse.Item();
                            data.CopyModelValueFrom<ReportEquipmentPMPlanResponse.Item, ReportEquipmentPMPlanModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportEquipmentPMPlanResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        public ServerResponse<List<string>> GetSMEquipmentPMPlanCate()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = EquipmentPMPlanBLL.Instance.GetSMEquipmentPMPlanCate();
            response.Data = dbDatas;
            return response;
        }

        [HttpPost]
        public ServerResponse<List<string>> GetSMEquipmentPMPlanSubCate()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = EquipmentPMPlanBLL.Instance.GetSMEquipmentPMPlanSubCate();
            response.Data = dbDatas;
            return response;
        }

    }
}
