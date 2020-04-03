using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
        public class ReportEquipmentPMPlanRequest : ServerRequestBase<ReportEquipmentPMPlanRequestItem>
        {

        }

        public class DownLoadEquipmentPMPlanRequest : ReportDownloadRequestBase<ReportEquipmentPMPlanRequestItem>
        {

        }


        public class ReportEquipmentPMPlanRequestItem
        {
            public string Category { get; set; }
            public string SubCategory { get; set; }
            public string Frequency { get; set; }       
        }
        
}

