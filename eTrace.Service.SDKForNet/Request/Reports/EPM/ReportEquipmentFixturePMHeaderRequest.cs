using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
    public class ReportEquipmentFixturePMHeaderRequest : ServerRequestBase<ReportEquipmentFixturePMHeaderRequestItem>
    {

    }
    public class DownLoadEquipmentFixturePMHeaderDatasRequest : ReportDownloadRequestBase<ReportEquipmentFixturePMHeaderRequestItem>
    {

    }

    public class DownLoadEquipmentFixturePMDetailDatasRequest : ReportDownloadRequestBase<ReportEquipmentFixturePMHeaderRequestItem>
    {

    }

    public class ReportEquipmentFixturePMHeaderRequestItem
    {
            public string PMID { get; set; }
            public string EquipmentID { get; set; }
            public string Category { get; set; }
            public string SubCategory { get; set; }
            public string Frequency { get; set; }
            public string Department { get; set; }
            public string PMStatus { get; set; }
            public string ProductionLine { get; set; }
            public string ProductionFloor { get; set; }
            [JsonIgnore]
            public DateTime? ScheduledFrom
            {
                get
                {
                    return strScheduledFrom.GetDateTime("yyyy-MM-dd 00:00:00");
                }
            }
            [JsonProperty("ScheduledFrom")]
            public string strScheduledFrom { get; set; }
            [JsonIgnore]
            public DateTime? ScheduledTo
            {
                get
                {
                    return strScheduledTo.GetDateTime("yyyy-MM-dd 00:00:00");
                }
            }
            [JsonProperty("ScheduledTo")]
            public string strScheduledTo { get; set; }
            [JsonIgnore]
            public DateTime? PMCompletionFrom
            {
                get
                {
                    return strPMCompletionFrom.GetDateTime("yyyy-MM-dd 00:00:00");
                }
            }
            [JsonProperty("PMCompletionFrom")]
            public string strPMCompletionFrom { get; set; }
            [JsonIgnore]
            public DateTime? PMCompletionTo
            {
                get
                {
                    return strPMCompletionTo.GetDateTime("yyyy-MM-dd 00:00:00");
                }
            }
            [JsonProperty("PMCompletionTo")]
            public string strPMCompletionTo { get; set; }
        }

        
}

