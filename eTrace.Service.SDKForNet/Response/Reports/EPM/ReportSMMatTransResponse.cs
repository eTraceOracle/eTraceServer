using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    public class ReportSMMatTransResponse : ServerResponseBase<List<ReportSMMatTransResponse.Item>>
    {
        public class Item
        {
            public string TransID { get; set; }
            public string TO { get; set; }
            public string Material { get; set; }
            public string Qty { get; set; }
            public string UOM { get; set; }
            public string UnitCost { get; set; }
            public string Currency { get; set; }
            public string MovementType { get; set; }
            public string FromLocID { get; set; }
            public string ToLocID { get; set; }
            public string PO { get; set; }
            public DateTime ChangedOn { get; set; }
            public string ChangedBy { get; set; }
            public string PNRemarks { get; set; }
            public string fromStore { get; set; }
            public string toStore { get; set; }
            public string Vendor { get; set; }
            public string CostCenter { get; set; }
            public string RequestedBy { get; set; }
            public string TORemarks { get; set; }
            public string Description { get; set; }
        }
    }
}
