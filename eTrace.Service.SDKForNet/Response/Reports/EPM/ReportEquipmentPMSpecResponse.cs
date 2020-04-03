using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    public class ReportEquipmentPMSpecResponse : ServerResponseBase<List<ReportEquipmentPMSpecResponse.Item>>
    {
        public class Item
        {
            public string PMSpecID { get; set; }
            public string Description { get; set; }
            public string Frequency { get; set; }
            public string Status { get; set; }
            public string ChangedBy { get; set; }
            public DateTime ChangedOn { get; set; }
            public string Remarks { get; set; }
        }
    }

    public class ReportEquipmentPMSpecPMItemResponse : ServerResponseBase<List<ReportEquipmentPMSpecPMItemResponse.Item>>
    {
        public class Item
        {
            public string PMSpecID { get; set; }
            public string PMItem { get; set; }
            public string ItemDesc { get; set; }
            public string PMInstruction { get; set; }
            public string Operator { get; set; }
            public DateTime ChangedOn { get; set; }
            public string ChangedBy { get; set; }
            public string Remarks { get; set; }
        }
    }

}
