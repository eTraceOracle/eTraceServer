using eTrace.Service.SDKForNet;
using eTrace.Service.SDKForNet.Request.Reports.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace eTraceWebApi.Controllers
{
    public class HomeController : ApiController
    {
        [Route(""), HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public HttpResponseMessage Index()
        {
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.Found);
            httpResponseMessage.Headers.Location = new Uri("/swagger/ui/index", UriKind.Relative);
            return httpResponseMessage;
        }
     
    }
}
