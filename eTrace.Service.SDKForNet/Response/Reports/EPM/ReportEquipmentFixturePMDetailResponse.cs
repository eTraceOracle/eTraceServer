using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    public class ReportEquipmentFixturePMDetailResponse : ServerResponseBase<List<ReportEquipmentFixturePMDetailResponse.Item>>
    {
        public class Item
        {
            public string EquipmentID { get; set; }
            public string PMStatus { get; set; }
            public DateTime PMSCheduledDate { get; set; }
            public DateTime PMStartDate { get; set; }
            public DateTime PMCompletionDate { get; set; }
            public string PMResult { get; set; }
            public string PMItem { get; set; }
            public string ItemDesc { get; set; }
            public string PMItemData { get; set; }
            public string Operator { get; set; }
            public string PMInstruction { get; set; }
            public string Attachment { get; set; }
            public string Remarks { get; set; }
                                
        }
    }
}
