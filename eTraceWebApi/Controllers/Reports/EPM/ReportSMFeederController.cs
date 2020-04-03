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
    public class ReportSMFeederController : ServerBaseController<ReportSMFeederController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportSMFeederController()
        {

        }

        /// <summary>
        /// DownLoadSMFeederDatas
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownLoadSMFeederDatas([FromBody] DownLoadSMFeederRequest requestData)
        {
            try
            {
                HttpResponseMessage fullResponse;
                ReportSMFeederHeaderModelQuery queryModel = GetModelQuery<DownLoadSMFeederRequest, ReportSMFeederHeaderModelQuery>(requestData);

                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     SMFeederBLL.Instance.SMFeederDataGetRowCount,
                                                     SMFeederBLL.Instance.GetSMFeederHeaderDatas,
                                                       "SMFeeder");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get SMFeeder Header Datas
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportSMFeederHeaderResponse GetSMFeederHeaderDatas([FromBody] ReportSMFeederRequest requestData)
        {
            ReportSMFeederHeaderResponse response = GetBusinessResponseDataInited<ReportSMFeederHeaderResponse>();
            try
            {
                ReportSMFeederHeaderModelQuery queryModel = GetModelQuery<ReportSMFeederRequest, ReportSMFeederHeaderModelQuery>(requestData);

                ReportSMFeederHeaderModel dbDatas = SMFeederBLL.Instance.GetSMFeederHeaderDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMFeederHeaderResponse.Item();
                            data.CopyModelValueFrom<ReportSMFeederHeaderResponse.Item, ReportSMFeederHeaderModel.Header>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMFeederHeaderResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        /// Get SMFeeder PMHeader Datas
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportSMFeederPMHeaderResponse GetSMFeederPMHeaderDatas([FromBody] ReportSMFeederRequest requestData)
        {
            ReportSMFeederPMHeaderResponse response = GetBusinessResponseDataInited<ReportSMFeederPMHeaderResponse>();
            try
            {
                ReportSMFeederHeaderModelQuery queryModel = GetModelQuery<ReportSMFeederRequest, ReportSMFeederHeaderModelQuery>(requestData);

                ReportSMFeederPMHeaderModel dbDatas = SMFeederBLL.Instance.GetSMFeederPMHeaderDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMFeederPMHeaderResponse.Item();
                            data.CopyModelValueFrom<ReportSMFeederPMHeaderResponse.Item, ReportSMFeederPMHeaderModel.PMHeader>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMFeederPMHeaderResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        /// Get SMFeeder PMHeader Item Datas
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportSMFeederPMHeaderItemResponse GetSMFeederPMHeaderItemDatas([FromBody] ReportSMFeederRequest requestData)
        {
            ReportSMFeederPMHeaderItemResponse response = GetBusinessResponseDataInited<ReportSMFeederPMHeaderItemResponse>();
            try
            {
                ReportSMFeederHeaderModelQuery queryModel = GetModelQuery<ReportSMFeederRequest, ReportSMFeederHeaderModelQuery>(requestData);

                ReportSMFeederPMHeaderItemModel dbDatas = SMFeederBLL.Instance.GetSMFeederPMHeaderItemDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMFeederPMHeaderItemResponse.Item();
                            data.CopyModelValueFrom<ReportSMFeederPMHeaderItemResponse.Item, ReportSMFeederPMHeaderItemModel.PMHeaderItem>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMFeederPMHeaderItemResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        /// Get SMFeeder PMHeader Mat Datas
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportSMFeederPMHeaderMatResponse GetSMFeederPMHeaderMatDatas([FromBody] ReportSMFeederRequest requestData)
        {
            ReportSMFeederPMHeaderMatResponse response = GetBusinessResponseDataInited<ReportSMFeederPMHeaderMatResponse>();
            try
            {
                ReportSMFeederHeaderModelQuery queryModel = GetModelQuery<ReportSMFeederRequest, ReportSMFeederHeaderModelQuery>(requestData);

                ReportSMFeederPMHeaderMatModel dbDatas = SMFeederBLL.Instance.GetSMFeederPMHeaderMatDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMFeederPMHeaderMatResponse.Item();
                            data.CopyModelValueFrom<ReportSMFeederPMHeaderMatResponse.Item, ReportSMFeederPMHeaderMatModel.PMHeaderMat>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMFeederPMHeaderMatResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        /// Get SMFeeder RepairHeader Datas
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportSMFeederRepairHeaderResponse GetSMFeederRepairHeaderDatas([FromBody] ReportSMFeederRequest requestData)
        {
            ReportSMFeederRepairHeaderResponse response = GetBusinessResponseDataInited<ReportSMFeederRepairHeaderResponse>();
            try
            {
                ReportSMFeederHeaderModelQuery queryModel = GetModelQuery<ReportSMFeederRequest, ReportSMFeederHeaderModelQuery>(requestData);

                ReportSMFeederRepairHeaderModel dbDatas = SMFeederBLL.Instance.GetSMFeederRepairHeaderDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMFeederRepairHeaderResponse.Item();
                            data.CopyModelValueFrom<ReportSMFeederRepairHeaderResponse.Item, ReportSMFeederRepairHeaderModel.RepairHeader>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMFeederRepairHeaderResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        /// Get SMFeeder RepairHeader Mat Datas
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportSMFeederRepairHeaderMatResponse GetSMFeederRepairHeaderMatDatas([FromBody] ReportSMFeederRequest requestData)
        {
            ReportSMFeederRepairHeaderMatResponse response = GetBusinessResponseDataInited<ReportSMFeederRepairHeaderMatResponse>();
            try
            {
                ReportSMFeederHeaderModelQuery queryModel = GetModelQuery<ReportSMFeederRequest, ReportSMFeederHeaderModelQuery>(requestData);

                ReportSMFeederRepairHeaderMatModel dbDatas = SMFeederBLL.Instance.GetSMFeederRepairHeaderMatDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMFeederRepairHeaderMatResponse.Item();
                            data.CopyModelValueFrom<ReportSMFeederRepairHeaderMatResponse.Item, ReportSMFeederRepairHeaderMatModel.RepairHeaderMat>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMFeederRepairHeaderMatResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
