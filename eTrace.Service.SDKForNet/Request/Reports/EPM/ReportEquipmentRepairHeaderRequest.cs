using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
        public class ReportEquipmentRepairHeaderRequest : ServerRequestBase<ReportEquipmentRepairHeaderRequestItem>
        {

        }

        public class DownLoadEquipmentRepairHeaderRequest : ReportDownloadRequestBase<ReportEquipmentRepairHeaderRequestItem>
        {

        }


        public class ReportEquipmentRepairHeaderRequestItem
        {
            public string EquipmentID { get; set; }
            public string RepairMan { get; set; }
            public string ProductionLine { get; set; }
            public string Department { get; set; }
            public string Status { get; set; }
            public string RepID { get; set; }
        [JsonIgnore]
            public DateTime? FailedFrom
            {
                get
                {
                    return strFailedFrom.GetDateTime("yyyy-MM-dd 00:00:00");
                }
            }
            [JsonProperty("FailedFrom")]
            public string strFailedFrom { get; set; }

            [JsonIgnore]
            public DateTime? FailedTo
            {
                get
                {
                    return strFailedTo.GetDateTime("yyyy-MM-dd 00:00:00");
                }
            }
            [JsonProperty("FailedTo")]
            public string strFailedTo { get; set; }

            [JsonIgnore]
            public DateTime? FixedFrom
            {
                get
                {
                    return strFixedFrom.GetDateTime("yyyy-MM-dd 00:00:00");
                }
            }
            [JsonProperty("FixedFrom")]
            public string strFixedFrom { get; set; }

            [JsonIgnore]
            public DateTime? FixedTo
            {
                get
                {
                    return strFixedTo.GetDateTime("yyyy-MM-dd 00:00:00");
                }
            }
            [JsonProperty("FixedTo")]
            public string strFixedTo { get; set; }
        }
        
}

