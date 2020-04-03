using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
    public class ReportProductDataArchiveRequest : ServerRequestBase<ReportProductDataArchiveRequest.Item>
    {
        public class Item
        {
            public string Model { get; set; }
            public string SaleOrder { get; set; }
            public string DiscreteJob { get; set; }
            public string DeliveryNote { get; set; }
            public string FlatFile { get; set; }
            public string SendBy { get; set; }
            public string PalletID { get; set; }
            public string BoxID { get; set; }
            public string ProductSN { get; set; }
            public string TVANo { get; set; }
            public bool IsTracePreViousSN { get; set; }
            [JsonIgnore]
            public DateTime? CreatedOnFrom
            {
                get
                {
                    return strCreatedOnFrom.GetDateTime("yyyy-MM-dd 00:00:00");
                }
            }
            [JsonProperty("CreatedOnFrom")]
            public string strCreatedOnFrom { get; set; }
            [JsonIgnore]
            public DateTime? CreatedOnTo
            {
                get
                {
                    return strCreatedOnTo.GetDateTime("yyyy-MM-dd 00:00:00");
                }
            }
            [JsonProperty("CreatedOnTo")]
            public string strCreatedOnTo { get; set; }
        }
    }

    public class ReportDownloadProductDataArchiveRequest : ReportDownloadRequestBase<ReportProductDataArchiveRequest.Item>
    {
       
    }



}