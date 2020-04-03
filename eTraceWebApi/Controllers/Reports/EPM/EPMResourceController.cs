using eTrace.Report.BLL.Business;
using eTrace.Model;
using eTrace.Model.V2.Report;
using eTrace.Service.SDKForNet;
using eTrace.Service.SDKForNet.Response.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eTraceWebApi.Controllers.Reports
{
    public class EPMResourceController : ServerBaseController<EPMResourceController>
    {
        public EPMResourceController()
        {

        }


        public string Test()
        {
            return "";
        }

        [HttpPost]
        public ReportFixtureCatResponse GetFixtureCategory()
        {
            ReportFixtureCatResponse response = GetBusinessResponseDataInited<ReportFixtureCatResponse>();
            FixtureCategoryModel dbDatas = FixtureCategoryBLL.Instance.GetCategorys();
            try{
            if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {

                        foreach (var item in dbDatas.Data)
                        {
                            response.Data.Add(new ReportFixtureCatResponse.Item
                            {
                               category=item.category,
                               subCategory=item.subCategory
                            });
                           
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ReportFixtureCatResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
        public ServerResponse<List<string>> GetFixtureStatus()
        {
            ServerResponse<List<string>> response = new ServerResponse<List<string>>();
            var dbDatas = SMFixtureInspHeaderModuleBLL.Instance.GeFixtureStatus();
            response.Data = dbDatas;
            return response;
        }
    }
}
