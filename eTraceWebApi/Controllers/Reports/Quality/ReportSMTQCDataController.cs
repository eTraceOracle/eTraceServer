﻿using eTrace.Report.BLL;
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
    /// ReportSMTQCDataController
    /// </summary>
    public class ReportSMTQCDataController : ServerBaseController<ReportSMTQCDataController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportSMTQCDataController()
        {

        }

        /// <summary>
        /// 获取下载保养数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadSMTQCDataDatas([FromBody] DownLoadSMTQCDataDatasRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportSMTQCDataQuery queryModel = GetModelQuery<DownLoadSMTQCDataDatasRequest, ReportSMTQCDataQuery>(requestData);
                //fullResponse = DownloadReportResponse(queryModel,
                //                                     requestData.TableHeaders,
                //                                     ProductModuleBLL.Instance.ProductTestDataGetRowCount,
                //                                     ProductModuleBLL.Instance.GetProductTestData,
                //                                       "");
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     SMTQCDataBLL.Instance.SMTQCDataDataGetRowCount,
                                                     SMTQCDataBLL.Instance.GetSMTQCDataData,
                                                       "SMTQCData");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// check row count for Download
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetSMTQCDataTotalCount(DownLoadSMTQCDataDatasRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
            ReportSMTQCDataQuery queryModel = GetModelQuery<DownLoadSMTQCDataDatasRequest, ReportSMTQCDataQuery>(requestData);
            long rowCount = SMTQCDataBLL.Instance.SMTQCDataDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
            //response.BussinesCode = EmBussinesCodeType.Success;
            //response.LessThanCheckDownloadRowCount = (rowCount <= CheckDownloadRowCount);
            //response.RowCount = rowCount;
            //return response;
        }

        /// <summary>
        /// 获取保养数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportSMTQCDataResponse GetSMTQCDataDatas([FromBody] ReportSMTQCDataRequest requestData)
        {
            ReportSMTQCDataResponse response = GetBusinessResponseDataInited<ReportSMTQCDataResponse>();
            try
            {
                ReportSMTQCDataQuery queryModel = GetModelQuery<ReportSMTQCDataRequest, ReportSMTQCDataQuery>(requestData);

                ReportSMTQCDataModel dbDatas = SMTQCDataBLL.Instance.GetSMTQCDataData(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMTQCDataResponse.Item();
                            data.CopyModelValueFrom<ReportSMTQCDataResponse.Item, ReportSMTQCDataModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMTQCDataResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
