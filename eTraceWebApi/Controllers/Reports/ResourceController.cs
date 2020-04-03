using eTrace.Report.BLL;
using eTrace.Report.BLL.Business;
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
namespace eTraceWebApi.Controllers.Reports
{
    /// <summary>
    /// 资源类
    /// </summary>
    public class ResourceController : ServerBaseController<ResourceController>
    {
        /// <summary>
        /// 
        /// </summary>
        public ResourceController()
        {

        }

        /// <summary>
        /// 获取工序站位数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [HttpPost]
        public ProductStationResponse GetProductStationATE([FromBody] ProductStationRequest requestData)
        {
            ProductStationResponse response = GetBusinessResponseDataInited<ProductStationResponse>();
            try
            {
                ProductStationQuery queryModel = GetModelQuery<ProductStationRequest, ProductStationQuery>(requestData);
                queryModel.ProcessType = "ATE";
                queryModel.Status = "ACTIVE";

                ProductStationModel dbDatas = ResourceBLL.Instance.GetProductStation(queryModel);
                if (dbDatas != null)
                {
                    if (dbDatas.Data != null)
                    {
                        int counter = 1;
                        foreach (var item in dbDatas.Data)
                        {
                            response.Data.Add(new ProductStationResponse.Item
                            {
                                Seq = counter,
                                Description = item.Description,
                                Station = item.Process
                            });
                            counter++;
                        }
                    }
                    if (dbDatas.IsOverMaxRow)
                    {
                        return GetResponseError<ProductStationResponse>(ref response, EmBussinesCodeType.IsOverMaxRow);
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
