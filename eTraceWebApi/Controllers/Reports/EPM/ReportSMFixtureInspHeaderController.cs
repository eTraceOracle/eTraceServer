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
    public class ReportSMFixtureInspHeaderController : ServerBaseController<ReportSMFixtureInspHeaderController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportSMFixtureInspHeaderController()
        {

        }

        /// <summary>
        /// 获取下载保养规范数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownLoadSMFixtureInspHeaderDatas([FromBody] DownLoadSMFixtureInspHeaderRequest requestData)
        {
            try
            {
                HttpResponseMessage fullResponse;
                ReportSMFixtureInspHeaderModelQuery queryModel = GetModelQuery<DownLoadSMFixtureInspHeaderRequest, ReportSMFixtureInspHeaderModelQuery>(requestData);

                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     SMFixtureInspHeaderBLL.Instance.SMFixtureInspHeaderDataGetRowCount,
                                                     SMFixtureInspHeaderBLL.Instance.GetSMFixtureInspHeaderDatas,
                                                       "SMFixtureInspHeader");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// 获取Header数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportSMFixtureInspHeaderResponse GetSMFixtureInspHeaderDatas([FromBody] ReportSMFixtureInspHeaderRequest requestData)
        {
            ReportSMFixtureInspHeaderResponse response = GetBusinessResponseDataInited<ReportSMFixtureInspHeaderResponse>();
            try
            {
                ReportSMFixtureInspHeaderModelQuery queryModel = GetModelQuery<ReportSMFixtureInspHeaderRequest, ReportSMFixtureInspHeaderModelQuery>(requestData);

                ReportSMFixtureInspHeaderModel dbDatas = SMFixtureInspHeaderBLL.Instance.GetSMFixtureInspHeaderDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMFixtureInspHeaderResponse.Item();
                            data.CopyModelValueFrom<ReportSMFixtureInspHeaderResponse.Item, ReportSMFixtureInspHeaderModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMFixtureInspHeaderResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
                throw ex; ;
            }
            return response;
        }

        /// <summary>
        /// 获取Item数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportSMFixtureInspHeaderPMItemResponse GetSMFixtureInspItemDatas([FromBody] ReportSMFixtureInspHeaderRequest requestData)
        {
            ReportSMFixtureInspHeaderPMItemResponse response = GetBusinessResponseDataInited<ReportSMFixtureInspHeaderPMItemResponse>();
            try
            {
                ReportSMFixtureInspHeaderModelQuery queryModel = GetModelQuery<ReportSMFixtureInspHeaderRequest, ReportSMFixtureInspHeaderModelQuery>(requestData);

                ReportSMFixtureInspItemModel dbDatas = SMFixtureInspHeaderBLL.Instance.GetSMFixtureInspItemDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMFixtureInspHeaderPMItemResponse.Item();
                            data.CopyModelValueFrom<ReportSMFixtureInspHeaderPMItemResponse.Item, ReportSMFixtureInspItemModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMFixtureInspHeaderPMItemResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
                throw ex; ;
            }
            return response;
        }

    }
}
