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
    public class ReportEPMEventController : ServerBaseController<ReportEPMEventController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportEPMEventController()
        {

        }

        /// <summary>
        /// 获取下载保养规范数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownLoadEPMEventDatas([FromBody] DownLoadEPMEventRequest requestData)
        {
            try
            {
                HttpResponseMessage fullResponse;
                ReportEPMEventModelQuery queryModel = GetModelQuery<DownLoadEPMEventRequest, ReportEPMEventModelQuery>(requestData);

                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     EPMEventBLL.Instance.EPMEventDataGetRowCount,
                                                     EPMEventBLL.Instance.GetSMEPMEventDatas,
                                                       "EPMEvent");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// 获取保养规范数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportEPMEventResponse GetSMEPMEventDatas([FromBody] ReportEPMEventRequest requestData)
        {
            ReportEPMEventResponse response = GetBusinessResponseDataInited<ReportEPMEventResponse>();
            try
            {
                ReportEPMEventModelQuery queryModel = GetModelQuery<ReportEPMEventRequest, ReportEPMEventModelQuery>(requestData);

                ReportEPMEventModel dbDatas = EPMEventBLL.Instance.GetSMEPMEventDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportEPMEventResponse.Item();
                            data.CopyModelValueFrom<ReportEPMEventResponse.Item, ReportEPMEventModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportEPMEventResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        /// 获取保养规范项目数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportEPMEventPMItemResponse GetSMEPMEventItemDatas([FromBody] ReportEPMEventRequest requestData)
        {
            ReportEPMEventPMItemResponse response = GetBusinessResponseDataInited<ReportEPMEventPMItemResponse>();
            try
            {
                ReportEPMEventModelQuery queryModel = GetModelQuery<ReportEPMEventRequest, ReportEPMEventModelQuery>(requestData);

                ReportEPMEventItemModel dbDatas = EPMEventBLL.Instance.GetSMEPMEventItemDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportEPMEventPMItemResponse.Item();
                            data.CopyModelValueFrom<ReportEPMEventPMItemResponse.Item, ReportEPMEventItemModel.EventItem>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportEPMEventPMItemResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        /// Get Category
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServerResponse<List<string>> GetCategory()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = EPMEventBLL.Instance.GetCategory();
            response.Data = dbDatas;
            return response;
        }

    }
}
