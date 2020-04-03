using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportPickOrderModel : ModelBase<List<ReportPickOrderModel.Item>>
    {
        public class Item
        {
            public string OrgCode { get; set; }
            public string PickOrder { get; set; } 
            public string DJ { get; set; }
            public string Product { get; set; }
            public decimal BuildQty { get; set; }
            public string ProdFloor { get; set; }
            public string ProdLine { get; set; }
            public string ETA { get; set; }
            public string SupplyType { get; set; }
            public string DestSubInv { get; set; }
            public string DestLocator { get; set; }
            public string ReasonCode { get; set; }
            public string ItemNo { get; set; }
            public decimal ReqQty { get; set; }
            public decimal PickedQty { get; set; }
            public decimal OpenQty { get; set; }
            public string Status { get; set; }
            public string OnhandLocators { get; set; }
            public string CreatedBy { get; set; }
            public DateTime? CreatedOn { get; set; }
            public string ChangedBy { get; set; }
            public DateTime? ChangedOn { get; set; }
        }
    }

    

    public class ReportPickOrderModelQuery : ModelQueryBase
    {
        public string OrgCode { get; set; }
        public string Floor { get; set; }
        public string DJNo { get; set; }
        public string PickOrderNo { get; set; }
        public string SourceSubInv { get; set; }
        public string SupplyType { get; set; }
        public DateTime CreationFrom { get; set; }
        public DateTime CreationTo { get; set; }
        public string Status { get; set; }
    }
}
