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
    /// ReportProductModuleController
    /// </summary>
    public class ReportEquipmentController : ServerBaseController<ReportEquipmentController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportEquipmentController()
        {

        }

        /// <summary>
        /// 获取下载Equipment数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadEquipmentDatas([FromBody] DownloadEquipmentDatasRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportEquipmentQuery queryModel = GetModelQuery<DownloadEquipmentDatasRequest, ReportEquipmentQuery>(requestData);
                //fullResponse = DownloadReportResponse(queryModel,
                //                                     requestData.TableHeaders,
                //                                     ProductModuleBLL.Instance.ProductTestDataGetRowCount,
                //                                     ProductModuleBLL.Instance.GetProductTestData,
                //                                       "");
                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     EquipmentBLL.Instance.EquipmentDataGetRowCount,
                                                     EquipmentBLL.Instance.GetEquipmentData,
                                                       "SMEquipment");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// 获取设备数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportEquipmentResponse GetEquipmentDatas([FromBody] ReportEquipmentRequest requestData)
        {
            ReportEquipmentResponse response = GetBusinessResponseDataInited<ReportEquipmentResponse>();
            try
            {
                ReportEquipmentQuery queryModel = GetModelQuery<ReportEquipmentRequest, ReportEquipmentQuery>(requestData);

                ReportEquipmentModel dbDatas = EquipmentBLL.Instance.GetEquipmentData(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportEquipmentResponse.Item();
                            data.CopyModelValueFrom<ReportEquipmentResponse.Item, ReportEquipmentModel.Item>(item);
                            data.Seq = counter;
                            response.Data.Add(data);
                            counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportEquipmentResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        public ServerResponse<List<string>> GetEquipmentStatus()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = EquipmentBLL.Instance.GetEquipmentStatus();
            response.Data = dbDatas;
            return response;
        }

        /// <summary>
        /// Get All Department
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServerResponse<List<string>> GetAllDepartment()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = EquipmentBLL.Instance.GetAllDepartment();
            response.Data = dbDatas;
            return response;
        }


    }
}
