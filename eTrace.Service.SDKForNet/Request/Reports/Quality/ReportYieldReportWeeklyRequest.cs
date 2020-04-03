using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
    public class ReportYieldReportWeeklyRequest :
        ServerRequestBase<ReportYieldReportWeeklyDatasRequestItem>
        {

        }
        public class DownloadYieldReportWeeklyDatasRequest :
       ReportDownloadRequestBase<ReportYieldReportWeeklyDatasRequestItem>
        {

        }
        public class ReportYieldReportWeeklyDatasRequestItem
    {
        public string Station { get; set; }
        public string Model { get; set; }
        public DateTime? PDF { get; set; }
        public DateTime? PDT { get; set; }

    }


}