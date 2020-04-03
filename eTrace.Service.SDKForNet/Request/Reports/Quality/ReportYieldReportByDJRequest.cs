using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
    public class ReportYieldReportByDJRequest :
        ServerRequestBase<ReportYieldReportByDJDatasRequestItem>
        {

        }
        public class DownloadYieldReportByDJDatasRequest :
       ReportDownloadRequestBase<ReportYieldReportByDJDatasRequestItem>
        {

        }
        public class ReportYieldReportByDJDatasRequestItem
    {
        public string DJ { get; set; }
        public DateTime? PDF { get; set; }
        public DateTime? PDT { get; set; }

    }


}