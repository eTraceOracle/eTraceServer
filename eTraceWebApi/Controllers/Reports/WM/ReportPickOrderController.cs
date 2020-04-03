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
    /// ReportPickOrderController
    /// </summary>
    public class ReportPickOrderController : ServerBaseController<ReportPickOrderController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportPickOrderController()
        {
        }

        /// <summary>
        /// /Check for Download
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetPickOrderTotalCount(DownLoadPickOrderRequest requestData)
        {
            //var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();

            ReportPickOrderModelQuery queryModel = GetModelQuery<DownLoadPickOrderRequest, ReportPickOrderModelQuery>(requestData);

            long rowCount = PickOrderBLL.Instance.PickOrderDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>(rowCount);
        }


        /// <summary>
        /// 获取下载 CLID Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadPickOrderData([FromBody] DownLoadPickOrderRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportPickOrderModelQuery queryModel = GetModelQuery<DownLoadPickOrderRequest, ReportPickOrderModelQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     //PickOrderBLL.Instance.PickOrderDataGetRowCount,
                                                     PickOrderBLL.Instance.GetPickOrderData,
                                                       "PickOrderStatus");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }



        /// <summary>
        /// 获取 CLID Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportPickOrderResponse GetPickOrderData([FromBody] ReportPickOrderRequest requestData)
        {
            ReportPickOrderResponse response = GetBusinessResponseDataInited<ReportPickOrderResponse>();
            try
            {
                ReportPickOrderModelQuery queryModel = GetModelQuery<ReportPickOrderRequest, ReportPickOrderModelQuery>(requestData);

                ReportPickOrderModel dbDatas = PickOrderBLL.Instance.GetPickOrderData(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportPickOrderResponse.Item();
                            data.CopyModelValueFrom<ReportPickOrderResponse.Item, ReportPickOrderModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportPickOrderResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
                return GetResponseError<ReportPickOrderResponse>(ref response, ex, EmBussinesCodeType.ReportInternalError);
            }
            return response;
        }



        /// <summary>
        /// 获取SupplyType下拉
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServerResponse<List<string>> GetSupplyType()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = PickOrderBLL.Instance.GetSupplyType();
            response.Data = dbDatas;
            return response;
        }


    }
}
