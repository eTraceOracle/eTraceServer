using eTrace.Report.BLL.Business;
using eTrace.Model;
using eTrace.Model.V2.Report;
using eTrace.Service.SDKForNet.Request.Reports;
using eTrace.Service.SDKForNet.Response.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using eTrace.Common;
using eTrace.Service.SDKForNet;

namespace eTraceWebApi.Controllers.Reports
{

    public class ReportEPMController : ServerBaseController<ReportEPMController>
    {
        public ReportEPMController() 
        { 
        }

        /// <summary>
        /// Get DownLoad Fixture Data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DownloadFixtureDatas([FromBody] DownloadFixtureDatasRequest requestData)
        {
            //ReportProductTestDataResponse response = GetBusinessResponseDataInited<ReportProductTestDataResponse>();
            try
            {
                HttpResponseMessage fullResponse;
                ReportFixtureQuery queryModel = GetModelQuery<DownloadFixtureDatasRequest, ReportFixtureQuery>(requestData);
                fullResponse = DownloadReportResponse(queryModel,requestData.TableHeaders,
                                                     SMFixtureInspHeaderModuleBLL.Instance.FixtureDataGetRowCount,
                                                     SMFixtureInspHeaderModuleBLL.Instance.GeFixtures,
                                                       "SMFixture");
                return ResponseMessage(fullResponse);

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }


        /// <summary>
        /// GetFixtureData
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ReoportFixtureResponse GetFixtureData([FromBody] ReportFixtureRequest requestData)
        {
            ReoportFixtureResponse response = GetBusinessResponseDataInited<ReoportFixtureResponse>();
            try
            {
                ReportFixtureQuery queryModel = GetModelQuery<ReportFixtureRequest, ReportFixtureQuery>(requestData);
                var dbDatas = SMFixtureInspHeaderModuleBLL.Instance.GeFixtures(queryModel);
                if (dbDatas.Data != null) 
                {
                    int counter = 1;
                    foreach (var item in dbDatas.Data)
                    {
                            var data = new ReoportFixtureResponse.Item();
                            data.CopyModelValueFrom<ReoportFixtureResponse.Item, ReportFixtureModel.Item>(item);
                            //data.Seq = counter;
                            response.Data.Add(data);
                            counter++;
                    }
                }
                if (dbDatas.IsOverMaxRow)
                {
                    return GetResponseError<ReoportFixtureResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
                }
                if (dbDatas.Pager != null)
                {
                    response.Pager = new ResponsePager()
                    {
                        TotalCount = dbDatas.Pager.TotalCount,
                    };
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
        /// /Check for Download
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ReportCheckRowsCountResponse GetFixtureDataTotalCount(DownloadFixtureDatasRequest requestData)
        {
            var response = GetBusinessResponseDataInited<ReportCheckRowsCountResponse>();
            ReportFixtureQuery queryModel = GetModelQuery<DownloadFixtureDatasRequest, ReportFixtureQuery>(requestData);
            long rowCount = SMFixtureInspHeaderModuleBLL.Instance.FixtureDataGetRowCount(queryModel);
            return GetCheckRowCountResponse<ReportCheckRowsCountResponse>( rowCount);
          
        }
    }
}
