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
    public class ReportSMPIController : ServerBaseController<ReportSMPIController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportSMPIController()
        {

        }

        /// <summary>
        /// 获取下载保养规范数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownLoadSMPIDatas([FromBody] DownLoadSMPIRequest requestData)
        {
            try
            {
                HttpResponseMessage fullResponse;
                ReportSMPIHeaderModelQuery queryModel = GetModelQuery<DownLoadSMPIRequest, ReportSMPIHeaderModelQuery>(requestData);

                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     SMPIBLL.Instance.SMPIDataGetRowCount,
                                                     SMPIBLL.Instance.GetSMPIHeaderDatas,
                                                       "SMPI");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// 获取PI主数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportSMPIHeaderResponse GetSMPIHeaderDatas([FromBody] ReportSMPIRequest requestData)
        {
            ReportSMPIHeaderResponse response = GetBusinessResponseDataInited<ReportSMPIHeaderResponse>();
            try
            {
                ReportSMPIHeaderModelQuery queryModel = GetModelQuery<ReportSMPIRequest, ReportSMPIHeaderModelQuery>(requestData);

                ReportSMPIHeaderModel dbDatas = SMPIBLL.Instance.GetSMPIHeaderDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMPIHeaderResponse.Header();
                            data.CopyModelValueFrom<ReportSMPIHeaderResponse.Header, ReportSMPIHeaderModel.Header>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMPIHeaderResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        /// 获取PI Equipments数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>

        [HttpPost]
        public ReportSMPIEquipmentsResponse GetSMPIEquipmentsDatas([FromBody] ReportSMPIRequest requestData)
        {
            ReportSMPIEquipmentsResponse response = GetBusinessResponseDataInited<ReportSMPIEquipmentsResponse>();
            try
            {
                ReportSMPIHeaderModelQuery queryModel = GetModelQuery<ReportSMPIRequest, ReportSMPIHeaderModelQuery>(requestData);

                ReportSMPIEquipmentsModel dbDatas = SMPIBLL.Instance.GetSMPIEquipmentsDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMPIEquipmentsResponse.Equipments();
                            data.CopyModelValueFrom<ReportSMPIEquipmentsResponse.Equipments, ReportSMPIEquipmentsModel.Equipments>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMPIEquipmentsResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        /// 获取PI Mats数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>

        [HttpPost]
        public ReportSMPIMatsResponse GetSMPIMatsDatas([FromBody] ReportSMPIRequest requestData)
        {
            ReportSMPIMatsResponse response = GetBusinessResponseDataInited<ReportSMPIMatsResponse>();
            try
            {
                ReportSMPIHeaderModelQuery queryModel = GetModelQuery<ReportSMPIRequest, ReportSMPIHeaderModelQuery>(requestData);

                ReportSMPIMatsModel dbDatas = SMPIBLL.Instance.GetSMPIMatsDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMPIMatsResponse.Mats();
                            data.CopyModelValueFrom<ReportSMPIMatsResponse.Mats, ReportSMPIMatsModel.Mats>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMPIMatsResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        /// 获取PI History数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>

        [HttpPost]
        public ReportSMPIHistoryResponse GetSMPIHistoryDatas([FromBody] ReportSMPIRequest requestData)
        {
            ReportSMPIHistoryResponse response = GetBusinessResponseDataInited<ReportSMPIHistoryResponse>();
            try
            {
                ReportSMPIHeaderModelQuery queryModel = GetModelQuery<ReportSMPIRequest, ReportSMPIHeaderModelQuery>(requestData);

                ReportSMPIHistoryModel dbDatas = SMPIBLL.Instance.GetSMPIHistoryDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMPIHistoryResponse.History();
                            data.CopyModelValueFrom<ReportSMPIHistoryResponse.History, ReportSMPIHistoryModel.History>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMPIHistoryResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
