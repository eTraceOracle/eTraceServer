using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
    public class DownloadFixtureDatasRequest : ReportDownloadRequestBase<ReportFixtureRequest.Item>
    {

    }
    public class ReportFixtureRequest : ServerRequestBase<ReportFixtureRequest.Item>
    {
        public class Item
        {
            public string FixtureID { get; set; }
            public string ItemNO { get; set; }
            public string Category { get; set; }
            public string SubCategory { get; set; }
            public string BatchID { get; set; }
            public List<string> Status { get; set; }
            public string Owner { get; set; }
            public string Description { get; set; }
            [JsonIgnore]
            public DateTime? LastUseFrom
            {
                get
                {
                    return strLastUseFrom.GetDateTime("yyyy-MM-dd 00:00:00");
                }
            }
            [JsonProperty("LastUseFrom")]
            public string strLastUseFrom { get; set; }
            [JsonIgnore]
            public DateTime? LastUseTo
            {
                get
                {
                    return strLastUseTo.GetDateTime("yyyy-MM-dd 00:00:00");
                }
            }
            [JsonProperty("LastUseTo")]
            public string strLastUseTo { get; set; }

            [JsonIgnore]
            public DateTime? LastReturnFrom
            {
                get
                {
                    return strLastReturnFrom.GetDateTime("yyyy-MM-dd 00:00:00");
                }
            }
            [JsonProperty("LastReturnFrom")]
            public string strLastReturnFrom { get; set; }
            [JsonIgnore]
            public DateTime? LastReturnTo
            {
                get
                {
                    return strLastReturnTo.GetDateTime("yyyy-MM-dd 00:00:00");
                }
            }
            [JsonProperty("LastReturnTo")]
            public string strLastReturnTo { get; set; }

            [JsonIgnore]
            public DateTime? LastInspectFrom
            {
                get
                {
                    return strLastInspectFrom.GetDateTime("yyyy-MM-dd 00:00:00");
                }
            }
            [JsonProperty("LastInspectFrom")]
            public string strLastInspectFrom { get; set; }
            [JsonIgnore]
            public DateTime? LastInspectTo
            {
                get
                {
                    return strLastInspectTo.GetDateTime("yyyy-MM-dd 00:00:00");
                }
            }
            [JsonProperty("LastInspectTo")]
            public string strLastInspectTo { get; set; }
            public string Dept { get; set; }
            public string CurrProdLine { get; set; }

        }
    }
}
