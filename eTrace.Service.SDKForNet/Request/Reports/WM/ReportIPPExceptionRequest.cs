using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
   public class ReportIPPExceptionRequest : ServerRequestBase<ReportIPPExceptionRequestItem>
     {

     }

   public class DownLoadIPPExceptionRequest : ReportDownloadRequestBase<ReportIPPExceptionRequestItem>
     {

     }


    public class ReportIPPExceptionRequestItem
     {
        public string OrgCode { get; set; }
        public string Floor { get; set; }
        public string DJ { get; set; }
        public string Model { get; set; }
        public Boolean ExceptionOnly { get; set; }

    }

}

