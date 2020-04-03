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
    /// ReportInvOptimizationController
    /// </summary>
    public class ReportInvOptimizationController : ServerBaseController<ReportInvOptimizationController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportInvOptimizationController()
        {

        }

        /// <summary>
        /// 下载 Inventory Optimization Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadInvOptimizationData([FromBody] DownLoadInvOptimizationRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                //string excelfile = "InventoryReport" + string.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
                ReportInvOptimizationModelQuery queryModel = GetModelQuery<DownLoadInvOptimizationRequest, ReportInvOptimizationModelQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     InvOptimizationBLL.Instance.GetInvOptimizationData,
                                                     "InventoryReport");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }



        /// <summary>
        /// 获取 Inventory Optimization Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportInvOptimizationResponse GetInvOptimizationData([FromBody] ReportInvOptimizationRequest requestData)
        {
            ReportInvOptimizationResponse response = GetBusinessResponseDataInited<ReportInvOptimizationResponse>();
            //try
            //{
                ReportInvOptimizationModelQuery queryModel = GetModelQuery<ReportInvOptimizationRequest, ReportInvOptimizationModelQuery>(requestData);

                ReportInvOptimizationModel dbDatas = InvOptimizationBLL.Instance.GetInvOptimizationData(queryModel);
                
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportInvOptimizationResponse.Item();
                            data.CopyModelValueFrom<ReportInvOptimizationResponse.Item, ReportInvOptimizationModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportInvOptimizationResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
                    }
                    if (dbDatas.Pager != null)
                    {
                        response.Pager = new ResponsePager()
                        {
                            TotalCount = dbDatas.Pager.TotalCount,
                        };
                    }
                }

            //}
            //catch (Exception ex)
            //{
            //    return GetResponseError<ReportInvOptimizationResponse>(ref response, ex, EmBussinesCodeType.ReportProductGetTDHeaderDatasError);
            //}
            return response;
        }




    }
}
