using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
    public class ReportUploadCiscoDataRequest : ServerRequestBase<ReportUploadCiscoDataRequestItem>
    {

    }
    public class DownLoadUploadCiscoDataDatasRequest : ReportDownloadRequestBase<ReportUploadCiscoDataRequestItem>
    {

    }

    public class ReportUploadCiscoDataRequestItem
        {        
        public DateTime? createFrom { get; set; }
        public DateTime? createTo { get; set; }
    }

        
}

