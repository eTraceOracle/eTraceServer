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
    public class ReportSMMatTransController : ServerBaseController<ReportSMMatTransController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ReportSMMatTransController()
        {

        }

        /// <summary>
        /// 获取下载备件交易数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownLoadSMMatTransDatas([FromBody] DownLoadSMMatTransRequest requestData)
        {
            try
            {
                HttpResponseMessage fullResponse;
                ReportSMMatTransModelQuery queryModel = GetModelQuery<DownLoadSMMatTransRequest, ReportSMMatTransModelQuery>(requestData);

                fullResponse = DownloadReportResponse(queryModel,
                                                     requestData.TableHeaders,
                                                     SMMatTransBLL.Instance.SMMatTransDataGetRowCount,
                                                     SMMatTransBLL.Instance.GetSMMatTransDatas,
                                                       "SMMatTrans");

                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// 获取备件交易数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportSMMatTransResponse GetSMMatTransDatas([FromBody] ReportSMMatTransRequest requestData)
        {
            ReportSMMatTransResponse response = GetBusinessResponseDataInited<ReportSMMatTransResponse>();
            try
            {
                ReportSMMatTransModelQuery queryModel = GetModelQuery<ReportSMMatTransRequest, ReportSMMatTransModelQuery>(requestData);

                ReportSMMatTransModel dbDatas = SMMatTransBLL.Instance.GetSMMatTransDatas(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        //int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            var data = new ReportSMMatTransResponse.Item();
                            data.CopyModelValueFrom<ReportSMMatTransResponse.Item, ReportSMMatTransModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            //counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportSMMatTransResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        public ServerResponse<List<string>> GetSMMatTransFromLocID()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = SMMatTransBLL.Instance.GetSMMatTransFromLocID();
            response.Data = dbDatas;
            return response;
        }

        [HttpPost]
        public ServerResponse<List<string>> GetSMMatTransToLocID()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = SMMatTransBLL.Instance.GetSMMatTransToLocID();
            response.Data = dbDatas;
            return response;
        }

        [HttpPost]
        public ServerResponse<List<string>> GetMovementType()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = SMMatTransBLL.Instance.GetMovementType();
            response.Data = dbDatas;
            return response;
        }

    }
}
