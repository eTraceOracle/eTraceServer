using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
    public class ReportYieldReportFPYRequest :
        ServerRequestBase<ReportYieldReportFPYDatasRequestItem>
        {

        }
        public class DownloadYieldReportFPYDatasRequest :
       ReportDownloadRequestBase<ReportYieldReportFPYDatasRequestItem>
        {

        }
        public class ReportYieldReportFPYDatasRequestItem
    {
        public string Model { get; set; }
        public string Station { get; set; }
        public string DJ { get; set; }
        public DateTime? PDF { get; set; }
        public DateTime? PDT { get; set; }

    }


}