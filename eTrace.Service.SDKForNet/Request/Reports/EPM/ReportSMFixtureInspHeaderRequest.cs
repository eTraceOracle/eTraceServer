using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
        public class ReportSMFixtureInspHeaderRequest : ServerRequestBase<ReportSMFixtureInspHeaderRequestItem>
        {

        }

        public class DownLoadSMFixtureInspHeaderRequest : ReportDownloadRequestBase<ReportSMFixtureInspHeaderRequestItem>
        {

        }


        public class ReportSMFixtureInspHeaderRequestItem
        {
            public string InspID { get; set; }
            public string InspType { get; set; }
            public string InspSpecID { get; set; }
            public string FixtureID { get; set; }
            public string ItemNO { get; set; }
            public string BatchID { get; set; }
            public DateTime InspectedFrom { get; set; }
            public DateTime InspectedTo { get; set; }
            public string Store { get; set; }
            public string Owner { get; set; }
        }   
        
}

