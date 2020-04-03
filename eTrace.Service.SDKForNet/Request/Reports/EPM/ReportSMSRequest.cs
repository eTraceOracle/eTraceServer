using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
   public class ReportSMSRequest : ServerRequestBase<ReportSMSRequestItem>
     {

     }

   public class DownLoadSMSDetailRequest : ReportDownloadRequestBase<ReportSMSRequestItem>
     {

     }

    public class DownLoadSMSSearchRequest : ReportDownloadRequestBase<ReportSMSRequestItem>
    {

    }



    public class ReportSMSRequestItem
     {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
    }

}

