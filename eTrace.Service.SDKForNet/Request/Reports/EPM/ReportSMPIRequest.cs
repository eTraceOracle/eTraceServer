using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
        public class ReportSMPIRequest : ServerRequestBase<ReportSMPIRequestItem>
        {

        }

        public class DownLoadSMPIRequest : ReportDownloadRequestBase<ReportSMPIRequestItem>
        {

        }


        public class ReportSMPIRequestItem
        {
            public string Assembly { get; set; }
            public string AssemblyRev { get; set; }
            public string ProdLine { get; set; }
            public string BaseLine { get; set; }
            public string ApprovalStatus { get; set; }
            public string EquipmentsPIID { get; set; }
            public string MatsPIID { get; set; }
            public string HistoryPIID { get; set; }
        }
        
}
