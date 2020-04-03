using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
    public class ReportSMFixtureVerificationRequest : ServerRequestBase<ReportSMFixtureVerificationRequestItem>
    {

    }

    public class DownLoadSMFixtureVerificationRequest : ReportDownloadRequestBase<ReportSMFixtureVerificationRequestItem>
    {

    }

    public class ReportSMFixtureVerificationRequestItem
    {
        public string FixtureID { get; set; }
        public string ItemNO { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string BatchID { get; set; }
        public string Status { get; set; }
        public string ReasonCode { get; set; }
        public string Owner { get; set; }
        public DateTime TransactionDateFrom { get; set; }
        public DateTime TransactionDateTo { get; set; }
    }
        
}

