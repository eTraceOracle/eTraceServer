using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
        public class ReportEPMEventRequest : ServerRequestBase<ReportEPMEventRequestItem>
        {

        }

        public class DownLoadEPMEventRequest : ReportDownloadRequestBase<ReportEPMEventRequestItem>
        {

        }


        public class ReportEPMEventRequestItem
        {
            public string Category { get; set; }
            public string Desc { get; set; }
            public string EventID { get; set; }
        }
        
}

