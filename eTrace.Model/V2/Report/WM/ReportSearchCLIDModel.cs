using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportSearchCLIDModel : ModelBase<List<ReportSearchCLIDModel.Item>>
    {
        public class Item
        {
            public string OrgCode { get; set; }
            public string CLID { get; set; }
            public string MaterialNo { get; set; }
            public string MaterialRevision { get; set; }
            public string MaterialDesc { get; set; }
            public decimal QtyBaseUOM { get; set; }
            public string BaseUOM { get; set; }
            public string SubInv { get; set; }
            public string Locator { get; set; }
            public string StorageType { get; set; }
            public DateTime? CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public DateTime? ChangedOn { get; set; }
            public string ChangedBy { get; set; }
            public string DateCode { get; set; }
            public string LotNo { get; set; }
            public string COO { get; set; }
            public string ExpDate { get; set; }
            public string RecDocNo { get; set; }
            public string RecDocItem { get; set; }
            public string RTLot { get; set; }
            public DateTime? RecDate { get; set; }
            public string RoHS { get; set; }
            public string PurOrdNo { get; set; }
            public string PurOrdItem { get; set; }
            public string DeliveryType { get; set; }
            public string VendorID { get; set; }
            public string VendorName { get; set; }
            public string VendorPN { get; set; }
            public string InvoiceNo { get; set; }
            public string BillofLading { get; set; }
            public string DN { get; set; }
            public string HeaderText { get; set; }
            public string ProdDate { get; set; }
            public string ReasonCode { get; set; }
            public string Operator { get; set; }
            public string StockType { get; set; }
            public string ItemText { get; set; }
            public string MatSuffix1 { get; set; }
            public string MatSuffix2 { get; set; }
            public string MatSuffix3 { get; set; }
            public string AddlText { get; set; }
            public string ReferenceCLID { get; set; }
            public string BoxID { get; set; }
            public string Manufacturer { get; set; }
            public string ManufacturerPN { get; set; }
            public string QMLStatus { get; set; }
            public string NextReviewDate { get; set; }
            public string ReviewStatus { get; set; }
            public string ReviewedOn { get; set; }
            public string ReviewedBy { get; set; }
            public string SampleSize { get; set; }
            public string AddlData { get; set; }
            public string Stemp { get; set; }
            public string MSL { get; set; }
            public string SupplyType { get; set; }
            public string LastDJ { get; set; }
            public string LastTransaction { get; set; }
            public string StatusCode { get; set; }

        }
    }

    

    public class ReportSearchCLIDModelQuery : ModelQueryBase
    {
        public string OrgCode { get; set; }
        public string RecDocNo { get; set; }
        public string SubInv { get; set; }
        public string VendorID { get; set; }
        public string Locator { get; set; }
        public string MaterialNo { get; set; }
        public string DateCode { get; set; }
        public string LotNo { get; set; }
        public DateTime RTFrom { get; set; }
        public DateTime RTTo { get; set; }
        public string CLID { get; set; }
        public string ManufacturerPN { get; set; }             // MPN
        public string PODJNo { get; set; }                   // PO/ SA DJ
        public string QMLStatus { get; set; }                  // QMLStatus
        public string StatusCode { get; set; }
        public string ReportType { get; set; }
    }
}
