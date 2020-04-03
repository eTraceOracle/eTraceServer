using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportComponentsUsedModel : ModelBase<List<ReportComponentsUsedModel.Item>>
    {
        public class Item
        {
            public string OrgCode { get; set; }
            public string MaterialNo { get; set; }
            public string MaterialRevision { get; set; }
            public string CLID { get; set; }
            public decimal CLIDQty { get; set; }
            public string SubInv { get; set; }
            public string Locator { get; set; }
            public string DJNumber { get; set; }
            public string Product { get; set; }
            public string Manufacturer { get; set; }
            public string ManufacturerPN { get; set; }
            public string VendorID { get; set; }
            public string VendorName { get; set; }
            public string PurOrdNo { get; set; }
            public string PurOrdItem { get; set; }
            public string RTLot { get; set; }
            public string MaterialDesc { get; set; }
            public string Safety { get; set; }
            public string DateCode { get; set; }
            public string LotNo { get; set; }
            public DateTime? RecDate { get; set; }
            public DateTime IssueDate { get; set; }
            public string ChangedBy { get; set; }
            public string ExpDate { get; set; }
            public string Remarks { get; set; }
            public string MSL { get; set; }
            public DateTime? ReturnDate { get; set; }

        }
    }

    public class ReportComponentsUsedTLAModel : ModelBase<List<ReportComponentsUsedTLAModel.Item>>
    {
        public class Item
        {
            public string TLA_DJ { get; set; }
            public string Assembly { get; set; }
            public string SMT_DJ { get; set; }
            public string Assembly_CLID { get; set; }
            public decimal Assembly_CLIDQty { get; set; }
            public DateTime? Assembly_IssueDate { get; set; }
            public string CLID { get; set; }
            public string MaterialNo { get; set; }
            public string DateCode { get; set; }
            public string LotNo { get; set; }
            public string Manufacturer { get; set; }
            public string ManufacturerPN { get; set; }
            public decimal CLIDQty { get; set; }
            public DateTime? IssueDate { get; set; }
            public DateTime? RecDate { get; set; }
            public string PurOrdNo { get; set; }
        }
    }

    public class ReportComponentsUsedModelQuery : ModelQueryBase
    {
        public string OrgCode { get; set; }
        public string DJNo { get; set; }
        public string MaterialNo { get; set; }
        public string CLID { get; set; }
        public string DateCode { get; set; }
        public string LotNo { get; set; }
        public DateTime IssueDateFrom { get; set; }
        public DateTime IssueDateTo { get; set; }
        public string PODJNo { get; set; }                   // PO/ SA DJ
        public string Manufacturer { get; set; }
        public string SubAssembly { get; set; }
        public string ReportType { get; set; }              // Current / Archive / TLADJ

    }
}
