using eTrace.Model.V2.Report;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
        public class ReportSMFeederRequest : ServerRequestBase<ReportSMFeederRequestItem>
        {

        }

        public class DownLoadSMFeederRequest : ReportDownloadRequestBase<ReportSMFeederRequestItem>
        {

        }


        public class ReportSMFeederRequestItem
        {
            public string EquipmentID { get; set; }
            public string Description { get; set; }
            public string Model { get; set; }
            public string Status { get; set; }
            public int FailedTimes { get; set; }
            public string PMItemPMID { get; set; }
            public string PMMatPMID { get; set; }
            public string RepairRepID { get; set; }

    }
        
}

