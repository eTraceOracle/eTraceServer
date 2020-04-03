using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
        public class ReportSMFixtureTransRequest : ServerRequestBase<ReportSMFixtureTransRequestItem>
        {

        }

        public class DownLoadSMFixtureTransRequest : ReportDownloadRequestBase<ReportSMFixtureTransRequestItem>
        {

        }


        public class ReportSMFixtureTransRequestItem
        {
            public string FixtureID { get; set; }
            public string ItemNo { get; set; }
            public string Category { get; set; }
            public string SubCategory { get; set; }
            public string BatchID { get; set; }
            public string Status { get; set; }
            public string TransactionType { get; set; }
            public string Owner { get; set; }
    }
        
}

