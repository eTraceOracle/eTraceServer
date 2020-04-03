using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
   public class ReportInvOptimizationRequest : ServerRequestBase<ReportInvOptimizationRequestItem>
     {

     }

   public class DownLoadInvOptimizationRequest : ReportDownloadRequestBase<ReportInvOptimizationRequestItem>
     {

     }


    public class ReportInvOptimizationRequestItem
     {
        public string OrgCode { get; set; }
        public string ShortageItems { get; set; }
        public string DmdCutDate_Shortage { get; set; }
        public string POCutDate_Shortage { get; set; }
        public bool SafetyStock { get; set; }
        public string DmdCutDate_Excess { get; set; }
        public string POCutDate_Excess { get; set; }

    }

}

