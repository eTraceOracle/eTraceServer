using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
    public class ReportSMTQCDataRequest : ServerRequestBase<ReportSMTQCDataRequestItem>
    {

    }
    public class DownLoadSMTQCDataDatasRequest : ReportDownloadRequestBase<ReportSMTQCDataRequestItem>
    {

    }

    public class ReportSMTQCDataRequestItem
        {
        public string Model { get; set; }
        public string Shift { get; set; }
        public string LineType { get; set; }
        public string Floor { get; set; }
        public DateTime? PDF { get; set; }
        public DateTime? PDT { get; set; }
    }

        
}

