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
    public class ReportSMMSDController : ServerBaseController<ReportSMMSDController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportSMMSDController()
        {

        }

        [HttpPost]
        public IHttpActionResult DownLoadSMMSDDatas([FromBody] DownLoadSMMSDRequest requestData)
        {
            try
            {
                HttpResponseMessage fullResponse;
                ReportSMMSDModelQuery queryModel = GetModelQuery<DownLoadSMMSDRequest, ReportSMMSDModelQuery>(requestData);

                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     SMMSDBLL.Instance.SMMSDDataGetRowCount,
                                                     SMMSDBLL.Instance.GetSMMSDDatas,
                                                       "SMMSD");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public ReportSMMSDResponse GetSMMSDDatas([FromBody] ReportSMMSDRequest requestData)
        {
            ReportSMMSDResponse response = GetBusinessResponseDataInited<ReportSMMSDResponse>();
            try
            {
                ReportSMMSDModelQuery queryModel = GetModelQuery<ReportSMMSDRequest, ReportSMMSDModelQuery>(requestData);

                ReportSMMSDModel dbDatas = SMMSDBLL.Instance.GetSMMSDDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMMSDResponse.Item();
                            data.CopyModelValueFrom<ReportSMMSDResponse.Item, ReportSMMSDModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMMSDResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        public ReportSMMSDPMItemResponse GetSMMSDPMItemDatas([FromBody] ReportSMMSDRequest requestData)
        {
            ReportSMMSDPMItemResponse response = GetBusinessResponseDataInited<ReportSMMSDPMItemResponse>();
            try
            {
                ReportSMMSDModelQuery queryModel = GetModelQuery<ReportSMMSDRequest, ReportSMMSDModelQuery>(requestData);

                ReportSMMSDPMItemModel dbDatas = SMMSDBLL.Instance.GetSMMSDPMItemDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMMSDPMItemResponse.Item();
                            data.CopyModelValueFrom<ReportSMMSDPMItemResponse.Item, ReportSMMSDPMItemModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMMSDPMItemResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        public ServerResponse<List<string>> GetSMMSDLastTransaction()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = SMMSDBLL.Instance.GetSMMSDLastTransaction();
            response.Data = dbDatas;
            return response;
        }

    }
}
