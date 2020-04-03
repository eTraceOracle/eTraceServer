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
    public class ReportSMInspSpecController : ServerBaseController<ReportSMInspSpecController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportSMInspSpecController()
        {

        }

        /// <summary>
        /// 获取下载数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownLoadSMInspSpecDatas([FromBody] DownLoadSMInspSpecRequest requestData)
        {
            try
            {
                HttpResponseMessage fullResponse;
                ReportSMInspSpecModelQuery queryModel = GetModelQuery<DownLoadSMInspSpecRequest, ReportSMInspSpecModelQuery>(requestData);

                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     SMInspSpecBLL.Instance.SMInspSpecDataGetRowCount,
                                                     SMInspSpecBLL.Instance.GetSMInspSpecDatas,
                                                       "SMInspSpec");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// GetSMInspSpecDatas
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportSMInspSpecResponse GetSMInspSpecDatas([FromBody] ReportSMInspSpecRequest requestData)
        {
            ReportSMInspSpecResponse response = GetBusinessResponseDataInited<ReportSMInspSpecResponse>();
            try
            {
                ReportSMInspSpecModelQuery queryModel = GetModelQuery<ReportSMInspSpecRequest, ReportSMInspSpecModelQuery>(requestData);

                ReportSMInspSpecModel dbDatas = SMInspSpecBLL.Instance.GetSMInspSpecDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMInspSpecResponse.Item();
                            data.CopyModelValueFrom<ReportSMInspSpecResponse.Item, ReportSMInspSpecModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMInspSpecResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        /// GetSMInspSpecItemDatas
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportSMInspSpecResponse GetSMInspSpecItemDatas([FromBody] ReportSMInspSpecRequest requestData)
        {
            ReportSMInspSpecResponse response = GetBusinessResponseDataInited<ReportSMInspSpecResponse>();
            try
            {
                ReportSMInspSpecModelQuery queryModel = GetModelQuery<ReportSMInspSpecRequest, ReportSMInspSpecModelQuery>(requestData);

                ReportSMInspSpecModel dbDatas = SMInspSpecBLL.Instance.GetSMInspSpecItemDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMInspSpecResponse.Item();
                            data.CopyModelValueFrom<ReportSMInspSpecResponse.Item, ReportSMInspSpecModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMInspSpecResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        public ServerResponse<List<string>> GetSMInspSpecID()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = SMInspSpecBLL.Instance.GetSMInspSpecID();
            response.Data = dbDatas;
            return response;
        }

    }
}
