using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
        public class ReportSMInspSpecRequest : ServerRequestBase<ReportSMInspSpecRequestItem>
        {

        }

        public class DownLoadSMInspSpecRequest : ReportDownloadRequestBase<ReportSMInspSpecRequestItem>
        {

        }


        public class ReportSMInspSpecRequestItem
        {
            public string InspSpecID { get; set; }     
        }
        
}

