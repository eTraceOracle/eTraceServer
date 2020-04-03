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
    public class ReportEquipmentRepairHeaderController : ServerBaseController<ReportEquipmentRepairHeaderController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportEquipmentRepairHeaderController()
        {

        }

        /// <summary>
        /// 获取下载维修数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadEquipmentRepairHeaderDatas([FromBody] DownLoadEquipmentRepairHeaderRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportEquipmentRepairHeaderModelQuery queryModel = GetModelQuery<DownLoadEquipmentRepairHeaderRequest, ReportEquipmentRepairHeaderModelQuery>(requestData);
                //fullResponse = DownloadReportResponse(queryModel,
                //                                     requestData.TableHeaders,
                //                                     ProductModuleBLL.Instance.ProductTestDataGetRowCount,
                //                                     ProductModuleBLL.Instance.GetProductTestData,
                //                                       "");
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     EquipmentRepairHeaderBLL.Instance.EquipmentRepairHeaderDataGetRowCount,
                                                     EquipmentRepairHeaderBLL.Instance.GetSMRepairHeaderDatas,
                                                       "EquipmentRepairHeader");

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
        public ReportEquipmentRepairHeaderResponse GetSMRepairHeaderDatas([FromBody] ReportEquipmentRepairHeaderRequest requestData)
        {
            ReportEquipmentRepairHeaderResponse response = GetBusinessResponseDataInited<ReportEquipmentRepairHeaderResponse>();
            try
            {
                ReportEquipmentRepairHeaderModelQuery queryModel = GetModelQuery<ReportEquipmentRepairHeaderRequest, ReportEquipmentRepairHeaderModelQuery>(requestData);

                ReportEquipmentRepairHeaderModel dbDatas = EquipmentRepairHeaderBLL.Instance.GetSMRepairHeaderDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportEquipmentRepairHeaderResponse.Item();
                            data.CopyModelValueFrom<ReportEquipmentRepairHeaderResponse.Item, ReportEquipmentRepairHeaderModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportEquipmentRepairHeaderResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        /// get Repair Mat
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportEquipmentRepairMatResponse GetSMRepairMatDatas([FromBody] ReportEquipmentRepairHeaderRequest requestData)
        {
            ReportEquipmentRepairMatResponse response = GetBusinessResponseDataInited<ReportEquipmentRepairMatResponse>();
            try
            {
                ReportEquipmentRepairHeaderModelQuery queryModel = GetModelQuery<ReportEquipmentRepairHeaderRequest, ReportEquipmentRepairHeaderModelQuery>(requestData);

                ReportEquipmentRepairMatModel dbDatas = EquipmentRepairHeaderBLL.Instance.GetSMRepairMatDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportEquipmentRepairMatResponse.Mat();
                            data.CopyModelValueFrom<ReportEquipmentRepairMatResponse.Mat, ReportEquipmentRepairMatModel.Mat>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportEquipmentRepairMatResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        public ServerResponse<List<string>> GetSMRepairStatus()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = EquipmentRepairHeaderBLL.Instance.GetSMRepairStatus();
            response.Data = dbDatas;
            return response;
        }

    }
}
