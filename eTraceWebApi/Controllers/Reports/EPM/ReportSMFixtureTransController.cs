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
    public class ReportSMFixtureTransController : ServerBaseController<ReportSMFixtureTransController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportSMFixtureTransController()
        {

        }

        /// <summary>
        /// 获取下载维修数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownLoadSMFixtureTransDatas([FromBody] DownLoadSMFixtureTransRequest requestData)
        {
            try
            {
                HttpResponseMessage fullResponse;
                ReportSMFixtureTransModelQuery queryModel = GetModelQuery<DownLoadSMFixtureTransRequest, ReportSMFixtureTransModelQuery>(requestData);

                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     SMFixtureTransBLL.Instance.SMFixtureTransDataGetRowCount,
                                                     SMFixtureTransBLL.Instance.GetSMFixtureTransDatas,
                                                       "SMFixtureTrans");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// 获取维修数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportSMFixtureTransResponse GetSMFixtureTransDatas([FromBody] ReportSMFixtureTransRequest requestData)
        {
            ReportSMFixtureTransResponse response = GetBusinessResponseDataInited<ReportSMFixtureTransResponse>();
            try
            {
                ReportSMFixtureTransModelQuery queryModel = GetModelQuery<ReportSMFixtureTransRequest, ReportSMFixtureTransModelQuery>(requestData);

                ReportSMFixtureTransModel dbDatas = SMFixtureTransBLL.Instance.GetSMFixtureTransDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMFixtureTransResponse.Item();
                            data.CopyModelValueFrom<ReportSMFixtureTransResponse.Item, ReportSMFixtureTransModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMFixtureTransResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        /// Get Category
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServerResponse<List<string>> GetSMFixtureTransCate()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = SMFixtureTransBLL.Instance.GetSMFixtureTransCate();
            response.Data = dbDatas;
            return response;
        }

        /// <summary>
        /// Get SubCategory
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServerResponse<List<string>> GetSMFixtureTransSubCate()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = SMFixtureTransBLL.Instance.GetSMFixtureTransSubCate();
            response.Data = dbDatas;
            return response;
        }

        /// <summary>
        /// Get Status
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServerResponse<List<string>> GetSMFixtureTransStatus()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = SMFixtureTransBLL.Instance.GetSMFixtureTransStatus();
            response.Data = dbDatas;
            return response;
        }

        /// <summary>
        /// Get TransactionType
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServerResponse<List<string>> GetSMFixtureTransTransactionType()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = SMFixtureTransBLL.Instance.GetSMFixtureTransTransactionType();
            response.Data = dbDatas;
            return response;
        }

    }
}
