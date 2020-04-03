using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportFixtureModel : ModelBase<List<ReportFixtureModel.Item>>
    {
        public class Item
        {
            public string SysFixtureID { get; set; }
            public string Category { get; set; }
            public string SubCategory { get; set; }
            public string FixtureID { get; set; }
            public string BatchID { get; set; }            
            public string Description { get; set; }
            public string Spec { get; set; }
            public string ItemNo { get; set; }
            public string Status { get; set; }
            public string Location { get; set; }
            public string CurrentJob { get; set; }
            public int CurrentCount { get; set; }
            public int TotalCount { get; set; }
            public string PO { get; set; }
            public string Vendor { get; set; }
            public DateTime OrderDate { get; set; }
            public decimal UnitPrice { get; set; }
            public string Currency { get; set; }
            public string RecDate { get; set; }
            public string DeliveryNote { get; set; }
            public string InvoiceNo { get; set; }
            public string OrderReason { get; set; }
            public string ProdReceiver { get; set; }
            public DateTime ProdRecDate { get; set; }
            public string Dept { get; set; }
            public string FileName { get; set; }
            public string Attachment { get; set; }
            public string Owner { get; set; }
            public string Model { get; set; }
            public string CurrProdLine { get; set; }
            public string Rev { get; set; }
            public DateTime ChangedOn { get; set; }
            public string ChangedBy { get; set; }
            public string Remarks { get; set; }
        }
    }

    public class ReportFixtureQuery : ModelQueryBase
    {
        public string FixtureID { get; set; }
        public string ItemNO { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string BatchID { get; set; }
        public List<string> Status { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }
        public DateTime? LastUseFrom { get; set; }
        public DateTime? LastUseTo { get; set; }
        public DateTime? LastReturnFrom { get; set; }
        public DateTime? LastReturnTo { get; set; }
        public DateTime? LastInspectFrom { get; set; }
        public DateTime? LastInspectTo { get; set; }
        public string Dept { get; set; }
        public string CurrProdLine { get; set; }

    }
}
