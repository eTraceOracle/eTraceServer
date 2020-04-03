using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
   public class ReportOnHandCompRequest : ServerRequestBase<ReportOnHandCompRequestItem>
     {

     }

   public class DownLoadOnHandCompRequest : ReportDownloadRequestBase<ReportOnHandCompRequestItem>
     {

     }


    public class ReportOnHandCompRequestItem
     {
        public string OrgCode { get; set; }
        public string SubInv { get; set; }
        public string Locator { get; set; }
        public string LotNo { get; set; }
        public string ItemNo { get; set; }
        public string DiffFlag { get; set; }
    }

}

