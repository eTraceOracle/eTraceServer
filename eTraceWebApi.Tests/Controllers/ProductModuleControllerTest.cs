using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eTraceWebApi;
using eTraceWebApi.Controllers;
using System.Collections;
using eTrace.Service.SDKForNet.Request;
using eTrace.Service.SDKForNet.Response;
using eTrace.Core;
using eTrace.Service.SDKForNet;
using eTrace.Service.SDKForNet.Request.Reports;
using eTrace.Service.SDKForNet.Response.Reports;

namespace eTraceWebApi.Tests.Controllers
{
    [TestClass]
    public class ProductModuleControllerTest
    {
        [TestMethod]
        public void GetTDHeaderDatas()
        {
            //ReportProductController controller = new ReportProductController();
            //ReportTDHeaderRequest query = new ReportTDHeaderRequest();
            //query.Data = new ReportTDHeaderRequest.Item();
            //query.Data.IntSN = "dd";
            //ReportTDHeaderResponse result = controller.GetTDHeaderDatas(query);

            //// Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual(2, result.Count());
            //Assert.AreEqual("value1", result.ElementAt(0));
            //Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetProductTestDatas()
        {
            //ReportProductController controller = new ReportProductController();
            //ReportProductTestDataRequest query = new ReportProductTestDataRequest();
            //query.Data = new ReportProductTestDataRequest.Item();
            //query.Data.Model = "CST550AP-8";
            //query.Data.Station = "Burnin";
            //query.Data.DJ = "P2946835";
            //query.Data.strProductTimeStart =  "2019/04/07 00:00:00";
            //query.Data.strProductTimeEnd = "2019/05/01 00:00:00";
            //query.Pager = new  RequestPager();
            //query.Pager.CurrentPage = 1;
            //query.Pager.PageSize = 1000;
            //query.Pager.Order = "a.tdid";
            //string reqData = SerializationHelper.Serialize<ReportProductTestDataRequest>(query, EmFormatType.Json);

            //ReportProductTestDataResponse result = controller.GetProductTestDatas(query);

            //// Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual(2, result.Count());
            //Assert.AreEqual("value1", result.ElementAt(0));
            //Assert.AreEqual("value2", result.ElementAt(1));
        }
        [TestMethod]
        public void GetProductDatas()
        {
            ReportProductController controller = new ReportProductController();
            ReportProductDataRequest query = new ReportProductDataRequest();
            query.Data = new ReportProductDataRequest.Item();
            query.Data.Model = "700-014500-0000";
            //query.Data.CreatedOnFrom =  "2019/04/01 12:00:00";
            //query.Data.CreatedOnTo = "2019/04/05 12:00:00";
            query.Pager = new RequestPager();
            query.Pager.CurrentPage = 1;
            query.Pager.PageSize = 20;
            query.Pager.Order = "createdon";
            string reqData = SerializationHelper.Serialize<ReportProductDataRequest>(query, EmFormatType.Json);

            ReportProductDataResponse result = controller.GetProductDatas(query);

            //// Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual(2, result.Count());
            //Assert.AreEqual("value1", result.ElementAt(0));
            //Assert.AreEqual("value2", result.ElementAt(1));
        }
    }
}
