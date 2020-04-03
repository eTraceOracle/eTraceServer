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
    /// ReportSMJobDataRecordsController
    /// </summary>
    public class ReportSMJobDataRecordsController : ServerBaseController<ReportSMJobDataRecordsController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportSMJobDataRecordsController()
        {

        }

        /// <summary>
        /// 获取下载Job数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadSMJobDataRecordsJobDatas([FromBody] DownLoadSMJobDataRecordsJobRequest requestData)
        {
            try
            {
                HttpResponseMessage fullResponse;
                ReportSMJobDataRecordsQuery queryModel = GetModelQuery<DownLoadSMJobDataRecordsJobRequest, ReportSMJobDataRecordsQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     SMJobDataRecordsBLL.Instance.SMJobDataRecordsJobGetRowCount,
                                                     SMJobDataRecordsBLL.Instance.GetSMJobDataRecordsJobDatas,
                                                       "");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// 获取下载Equipment数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadSMJobDataRecordsEquipmentDatas([FromBody] DownLoadSMJobDataRecordsEquipmentRequest requestData)
        {
            try
            {
                HttpResponseMessage fullResponse;
                ReportSMJobDataRecordsQuery queryModel = GetModelQuery<DownLoadSMJobDataRecordsEquipmentRequest, ReportSMJobDataRecordsQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     SMJobDataRecordsBLL.Instance.SMJobDataRecordsEquipmentGetRowCount,
                                                     SMJobDataRecordsBLL.Instance.GetSMJobDataRecordsEquipmentDatas,
                                                       "");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// 获取下载SPG数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadSMJobDataRecordsSPGDatas([FromBody] DownLoadSMJobDataRecordsSPGRequest requestData)
        {
            try
            {
                HttpResponseMessage fullResponse;
                ReportSMJobDataRecordsQuery queryModel = GetModelQuery<DownLoadSMJobDataRecordsSPGRequest, ReportSMJobDataRecordsQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     SMJobDataRecordsBLL.Instance.SMJobDataRecordsSPGGetRowCount,
                                                     SMJobDataRecordsBLL.Instance.GetSMJobDataRecordsSPGDatas,
                                                       "SMJobDataRecords");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// /Check for Job Download
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetJobDataTotalCount(DownLoadSMJobDataRecordsJobRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
            ReportSMJobDataRecordsQuery queryModel = GetModelQuery<DownLoadSMJobDataRecordsJobRequest, ReportSMJobDataRecordsQuery>(requestData);
            long rowCount = SMJobDataRecordsBLL.Instance.SMJobDataRecordsJobGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
            //response.BussinesCode = EmBussinesCodeType.Success;
            //response.LessThanCheckDownloadRowCount = (rowCount <= CheckDownloadRowCount);
            //response.RowCount = rowCount;
            //return response;
        }

        /// <summary>
        /// /Check for Equipment Download
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetEquipmentDataTotalCount(DownLoadSMJobDataRecordsEquipmentRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
            ReportSMJobDataRecordsQuery queryModel = GetModelQuery<DownLoadSMJobDataRecordsEquipmentRequest, ReportSMJobDataRecordsQuery>(requestData);
            long rowCount = SMJobDataRecordsBLL.Instance.SMJobDataRecordsEquipmentGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
            //response.BussinesCode = EmBussinesCodeType.Success;
            //response.LessThanCheckDownloadRowCount = (rowCount <= CheckDownloadRowCount);
            //response.RowCount = rowCount;
            //return response;
        }

        /// <summary>
        /// /Check for SPG Download
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetSPGDataTotalCount(DownLoadSMJobDataRecordsSPGRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
            ReportSMJobDataRecordsQuery queryModel = GetModelQuery<DownLoadSMJobDataRecordsSPGRequest, ReportSMJobDataRecordsQuery>(requestData);
            long rowCount = SMJobDataRecordsBLL.Instance.SMJobDataRecordsSPGGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
            //response.BussinesCode = EmBussinesCodeType.Success;
            //response.LessThanCheckDownloadRowCount = (rowCount <= CheckDownloadRowCount);
            //response.RowCount = rowCount;
            //return response;
        }


        /// <summary>
        /// Get Job Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportSMJobDataRecordsJobResponse GetSMJobDataRecordsJobDatas([FromBody] ReportSMJobDataRecordsRequest requestData)
        {
            ReportSMJobDataRecordsJobResponse response = GetBusinessResponseDataInited<ReportSMJobDataRecordsJobResponse>();
            try
            {
                ReportSMJobDataRecordsQuery queryModel = GetModelQuery<ReportSMJobDataRecordsRequest, ReportSMJobDataRecordsQuery>(requestData);

                ReportSMJobDataRecordsJobModel dbDatas = SMJobDataRecordsBLL.Instance.GetSMJobDataRecordsJobDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMJobDataRecordsJobResponse.Job();
                            data.CopyModelValueFrom<ReportSMJobDataRecordsJobResponse.Job, ReportSMJobDataRecordsJobModel.Job>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMJobDataRecordsJobResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        /// Get Equipment Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportSMJobDataRecordsEquipmentResponse GetSMJobDataRecordsEquipmentDatas([FromBody] ReportSMJobDataRecordsRequest requestData)
        {
            ReportSMJobDataRecordsEquipmentResponse response = GetBusinessResponseDataInited<ReportSMJobDataRecordsEquipmentResponse>();
            try
            {
                ReportSMJobDataRecordsQuery queryModel = GetModelQuery<ReportSMJobDataRecordsRequest, ReportSMJobDataRecordsQuery>(requestData);

                ReportSMJobDataRecordsEquipmentModel dbDatas = SMJobDataRecordsBLL.Instance.GetSMJobDataRecordsEquipmentDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMJobDataRecordsEquipmentResponse.Equipment();
                            data.CopyModelValueFrom<ReportSMJobDataRecordsEquipmentResponse.Equipment, ReportSMJobDataRecordsEquipmentModel.Equipment>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMJobDataRecordsEquipmentResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        /// Get SPG Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportSMJobDataRecordsSPGResponse GetSMJobDataRecordsSPGDatas([FromBody] ReportSMJobDataRecordsRequest requestData)
        {
            ReportSMJobDataRecordsSPGResponse response = GetBusinessResponseDataInited<ReportSMJobDataRecordsSPGResponse>();
            try
            {
                ReportSMJobDataRecordsQuery queryModel = GetModelQuery<ReportSMJobDataRecordsRequest, ReportSMJobDataRecordsQuery>(requestData);

                ReportSMJobDataRecordsSPGModel dbDatas = SMJobDataRecordsBLL.Instance.GetSMJobDataRecordsSPGDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMJobDataRecordsSPGResponse.SPG();
                            data.CopyModelValueFrom<ReportSMJobDataRecordsSPGResponse.SPG, ReportSMJobDataRecordsSPGModel.SPG>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMJobDataRecordsSPGResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
