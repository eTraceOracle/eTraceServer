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
    public class ReportSMMaterialController : ServerBaseController<ReportSMMaterialController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportSMMaterialController()
        {

        }

        /// <summary>
        /// 获取下载物料数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownLoadSMMaterialDatas([FromBody] DownLoadSMMaterialRequest requestData)
        {
            try
            {
                HttpResponseMessage fullResponse;
                ReportSMMaterialModelQuery queryModel = GetModelQuery<DownLoadSMMaterialRequest, ReportSMMaterialModelQuery>(requestData);

                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     SMMaterialBLL.Instance.SMMaterialDataGetRowCount,
                                                     SMMaterialBLL.Instance.GetSMMaterialDatas,
                                                       "SMMaterial");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// 获取物料数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportSMMaterialResponse GetSMMaterialDatas([FromBody] ReportSMMaterialRequest requestData)
        {
            ReportSMMaterialResponse response = GetBusinessResponseDataInited<ReportSMMaterialResponse>();
            try
            {
                ReportSMMaterialModelQuery queryModel = GetModelQuery<ReportSMMaterialRequest, ReportSMMaterialModelQuery>(requestData);

                ReportSMMaterialModel dbDatas = SMMaterialBLL.Instance.GetSMMaterialDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMMaterialResponse.Item();
                            data.CopyModelValueFrom<ReportSMMaterialResponse.Item, ReportSMMaterialModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMMaterialResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        public ServerResponse<List<string>> GetSMMaterialCategory()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = SMMaterialBLL.Instance.GetSMMaterialCategory();
            response.Data = dbDatas;
            return response;
        }

        [HttpPost]
        public ServerResponse<List<string>> GetSMMaterialSubCategory()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = SMMaterialBLL.Instance.GetSMMaterialSubCategory();
            response.Data = dbDatas;
            return response;
        }

        [HttpPost]
        public ServerResponse<List<string>> GetSMMaterialEquipCategory()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = SMMaterialBLL.Instance.GetSMMaterialEquipCategory();
            response.Data = dbDatas;
            return response;
        }

        [HttpPost]
        public ServerResponse<List<string>> GetSMMaterialEquipSubCategory()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = SMMaterialBLL.Instance.GetSMMaterialEquipSubCategory();
            response.Data = dbDatas;
            return response;
        }

        [HttpPost]
        public ServerResponse<List<string>> GetSMMaterialEquipModel()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = SMMaterialBLL.Instance.GetSMMaterialEquipModel();
            response.Data = dbDatas;
            return response;
        }

        [HttpPost]
        public ServerResponse<List<string>> GetSMMaterialDefaultStore()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = SMMaterialBLL.Instance.GetSMMaterialDefaultStore();
            response.Data = dbDatas;
            return response;
        }

    }
}
