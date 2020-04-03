using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
        public class ReportSMLocRequest : ServerRequestBase<ReportSMLocRequestItem>
        {

        }

        public class DownLoadSMLocRequest : ReportDownloadRequestBase<ReportSMLocRequestItem>
        {

        }


        public class ReportSMLocRequestItem
        {
            public string Category { get; set; }
            public string SubCategory { get; set; }
            public string LocID { get; set; }
            public string Store { get; set; }
        }
        
}

