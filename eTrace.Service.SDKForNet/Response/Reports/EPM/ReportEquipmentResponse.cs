using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    public class ReportEquipmentResponse : ServerResponseBase<List<ReportEquipmentResponse.Item>>
    {
        public class Item
        {
            public int Seq { get; set; }
            public string Category { get; set; }
            public string SubCategory { get; set; }
            public string EquipmentID { get; set; }
            public string Description { get; set; }
            public string Model { get; set; }
            public string Spec { get; set; }
            public DateTime MfrDate { get; set; }
            public string CurrProdLine { get; set; }
            public string FixedAssessID { get; set; }
            public string Manufacturer { get; set; }
            public string ManufacturerSN { get; set; }
            public DateTime AcqDate { get; set; }
            public string Department { get; set; }
            public string SeqOnLine { get; set; }
            public string Owner { get; set; }
            public string Status { get; set; }
            public DateTime ChangedOn { get; set; }
            public string ChangedBy { get; set; }
            public string Remarks { get; set; }
        }
    }
}
