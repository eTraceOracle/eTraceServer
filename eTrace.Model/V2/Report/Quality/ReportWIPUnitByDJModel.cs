using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportWIPUnitByDJModel : ModelBase<List<ReportWIPUnitByDJModel.Item>>
    {
        public class Item
        {
            public string MaterialNo { get; set; }
            public string MaterialDesc { get; set; }
            public string MaterialRevision { get; set; }
            public decimal CLIDQty { get; set; }
            public string BaseUOM { get; set; }
            public string CLID { get; set; }
            public DateTime IssueDate { get; set; }
            public string DateCode { get; set; }
            public string LotNo { get; set; }
            public string StatusCode { get; set; }
            public DateTime ExpDate { get; set; }
            public string RecDocNo { get; set; }
            public DateTime RecDate { get; set; }
            public string ProcessID { get; set; }
            public string RoHS { get; set; }
            public string PurOrdNo { get; set; }
            public string PurOrdItem { get; set; }
            public string VendorID { get; set; }
            public string VendorName { get; set; }
            public string VendorPN { get; set; }
            public string BillofLading { get; set; }
            public string DN { get; set; }
            public string HeaderText { get; set; }
            public DateTime ProdDate { get; set; }
            public string ReasonCode { get; set; }
            public string SLOC { get; set; }
            public string StorageBin { get; set; }
            public string Operator { get; set; }
            public string StockType { get; set; }
            public string ItemText { get; set; }
            public string MatSuffix1 { get; set; }
            public string MatSuffix2 { get; set; }
            public string MatSuffix3 { get; set; }
            public string Printed { get; set; }
            public string IsTraceable { get; set; }
            public string AddlText { get; set; }
            public string ReferenceCLID { get; set; }
            public string BoxID { get; set; }
            public string Manufacturer { get; set; }
            public string ManufacturerPN { get; set; }
            public string QMLStatus { get; set; }
            public DateTime NextReviewDate { get; set; }
            public string ReviewStatus { get; set; }
            public DateTime ReviewedOn { get; set; }
            public string ReviewedBy { get; set; }
            public int SampleSize { get; set; }
            public DateTime CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public DateTime ChangedOn { get; set; }
            public string ChangedBy { get; set; }
        }
    }

    public class ReportWIPUnitByDJQuery : ModelQueryBase
    {
        public string IntSN { get; set; }
    }
}
