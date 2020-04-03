using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportSOShipmentDetailModel : ModelBase<List<ReportSOShipmentDetailModel.Detail>>
    {
        public class Detail
        {
            public string OrgCode { get; set; }
            public string DN { get; set; }
            public string SO { get; set; }
            public string SOLine { get; set; }
            public string Item { get; set; }
            public string Revision { get; set; }
            public string ItemDesc { get; set; }
            public string SubInv { get; set; }
            public string Locator { get; set; }
            public string DateCode { get; set; }
            public string LotNo { get; set; }
            public decimal CLIDQty { get; set; }
            public string CLID { get; set; }
            public string PalletID { get; set; }
            public string Manufacturer { get; set; }
            public string ManufacturerPN { get; set; }
            public string VendorCode { get; set; }
            public string VendorName { get; set; }
            public string COO { get; set; }
            public string RTLot { get; set; }
            public string DJNO { get; set; }
            public DateTime? RecDate { get; set; }
            public DateTime? IssueDate { get; set; }
            public int StorageDays { get; set; }
            public string ChangedBy { get; set; }
            public string ShipmentNo { get; set; }
        }
    }


    public class ReportSOShipmentSummaryModel : ModelBase<List<ReportSOShipmentSummaryModel.Summary>>
    {
        public class Summary
        { 
            public string OrgCode { get; set; }
            public string Item { get; set; }
            public string Revision { get; set; }
            public string SubInv { get; set; }
            public decimal TotalQTY { get; set; }
        }
    }


 
    public class ReportSOShipmentModelQuery : ModelQueryBase
    {
        public string OrgCode { get; set; }
        public string DeliveryNo { get; set; }
        public string Item { get; set; }
        public int StorageDay { get; set; }
        public string DateCode { get; set; }
        public string LotNo { get; set; }
        public DateTime IssueDateFrom { get; set; }
        public DateTime IssueDateTo { get; set; }
        public string DestSubInv { get; set; }
        public string ReportType { get; set; }     // Detail / Summary

    }
}
