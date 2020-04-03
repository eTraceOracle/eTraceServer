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
    public class ReportEquipmentPMSpecController : ServerBaseController<ReportEquipmentPMSpecController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportEquipmentPMSpecController()
        {

        }

        /// <summary>
        /// 获取下载保养规范数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownLoadEquipmentPMSpecDatas([FromBody] DownLoadEquipmentPMSpecRequest requestData)
        {
            try
            {
                HttpResponseMessage fullResponse;
                ReportEquipmentPMSpecModelQuery queryModel = GetModelQuery<DownLoadEquipmentPMSpecRequest, ReportEquipmentPMSpecModelQuery>(requestData);

                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     EquipmentPMSpecBLL.Instance.EquipmentPMSpecDataGetRowCount,
                                                     EquipmentPMSpecBLL.Instance.GetSMEquipmentPMSpecDatas,
                                                       "EquipmentPMSpec");

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
        public ReportEquipmentPMSpecResponse GetSMEquipmentPMSpecDatas([FromBody] ReportEquipmentPMSpecRequest requestData)
        {
            ReportEquipmentPMSpecResponse response = GetBusinessResponseDataInited<ReportEquipmentPMSpecResponse>();
            try
            {
                ReportEquipmentPMSpecModelQuery queryModel = GetModelQuery<ReportEquipmentPMSpecRequest, ReportEquipmentPMSpecModelQuery>(requestData);

                ReportEquipmentPMSpecModel dbDatas = EquipmentPMSpecBLL.Instance.GetSMEquipmentPMSpecDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportEquipmentPMSpecResponse.Item();
                            data.CopyModelValueFrom<ReportEquipmentPMSpecResponse.Item, ReportEquipmentPMSpecModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportEquipmentPMSpecResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        public ReportEquipmentPMSpecPMItemResponse GetSMEquipmentPMSpecPMItemDatas([FromBody] ReportEquipmentPMSpecRequest requestData)
        {
            ReportEquipmentPMSpecPMItemResponse response = GetBusinessResponseDataInited<ReportEquipmentPMSpecPMItemResponse>();
            try
            {
                ReportEquipmentPMSpecModelQuery queryModel = GetModelQuery<ReportEquipmentPMSpecRequest, ReportEquipmentPMSpecModelQuery>(requestData);

                ReportEquipmentPMSpecPMItemModel dbDatas = EquipmentPMSpecBLL.Instance.GetSMEquipmentPMSpecPMItemDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportEquipmentPMSpecPMItemResponse.Item();
                            data.CopyModelValueFrom<ReportEquipmentPMSpecPMItemResponse.Item, ReportEquipmentPMSpecPMItemModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportEquipmentPMSpecPMItemResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
