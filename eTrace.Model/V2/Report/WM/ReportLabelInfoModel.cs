using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportLabelInfoDetailModel : ModelBase<List<ReportLabelInfoDetailModel.Detail>>
    {
        public class Detail
        {
            public string OrgCode { get; set; }
            public string CLID { get; set; }
            public string BoxID { get; set; }
            public string MaterialNo { get; set; }
            public string Revision { get; set; }
            public string MaterialDesc { get; set; }
            public decimal QtyBaseUOM { get; set; }
            public string BaseUOM { get; set; }
            public string SubInv { get; set; }
            public string Locator { get; set; }
            public string StorageType { get; set; }
            public string AddlText { get; set; }
            public string StockType { get; set; }
            public string DateCode { get; set; }
            public string LotNo { get; set; }
            public string COO { get; set; }
            public string ExpDate { get; set; }                //only return Date string
            public string RecDocNo { get; set; }
            public string RTLot { get; set; }
            public DateTime? RecDate { get; set; }
            public string RoHS { get; set; }
            public string PurOrdNo { get; set; }
            public string PurOrdItem { get; set; }
            public string VendorID { get; set; }
            public string VendorName { get; set; }
            public string VendorPN { get; set; }
            public string Manufacturer { get; set; }
            public string ManufacturerPN { get; set; }
            public string QMLStatus { get; set; }
            public string NextReviewDate { get; set; }
            public string ReviewStatus { get; set; }
            public string ReviewedOn { get; set; }
            public string ReviewedBy { get; set; }
            public DateTime? CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public DateTime? ChangedOn { get; set; }
            public string ChangedBy { get; set; }
            public string LastTransaction { get; set; }
            public string MSL { get; set; }
            public string InvoiceNo { get; set; }
            public string StatusCode { get; set; }

        }
    }


    public class ReportLabelInfoSummaryModel : ModelBase<List<ReportLabelInfoSummaryModel.Summary>>
    {
        public class Summary
        {  //Org	ItemNo	MaterialRevision	SubInv	Locator	SLOT	QtyBaseUOM	BaseUOM	Rtlot
            public string OrgCode { get; set; }
            public string MaterialNo { get; set; }
            public string Revision { get; set; }
            public string SubInv { get; set; }
            public string Locator { get; set; }
            public string StorageType { get; set; }
            public decimal QtyBaseUOM { get; set; }
            public string BaseUOM { get; set; }
            public string RTLot { get; set; }
        }
    }


    public class ReportLabelInfoePurgeDTModel : ModelBase<List<ReportLabelInfoePurgeDTModel.Detail>>
    {
        public class Detail
        {
            public string OrgCode { get; set; }
            public string CLID { get; set; }
            public string MaterialNo { get; set; }
            public decimal QtyBaseUOM { get; set; }
            public string BaseUOM { get; set; }
            public string SubInv { get; set; }
            public string Locator { get; set; }
            public string StockType { get; set; }
            public string DateCode { get; set; }
            public string LotNo { get; set; }
            public string ExpDate { get; set; }
            public string RecDocNo { get; set; }
            public string RTLot { get; set; }
            public DateTime? RecDate { get; set; }
            public string VendorID { get; set; }
            public string VendorName { get; set; }
            public string VendorPN { get; set; }
            public string Manufacturer { get; set; }
            public string ManufacturerPN { get; set; }
            public DateTime? ChangedOn { get; set; }
            public string ChangedBy { get; set; }
            public string LastTransaction { get; set; }

        }
    }


    public class ReportLabelInfoePurgeSMModel : ModelBase<List<ReportLabelInfoePurgeSMModel.Summary>>
    {
        public class Summary
        {  
            public string OrgCode { get; set; }
            public string SubInv { get; set; }
            public string MaterialNo { get; set; }
            public string Manufacturer { get; set; }
            public string ManufacturerPN { get; set; }
            public string DateCode { get; set; }
            public string LotNo { get; set; }
            public decimal Qty { get; set; }
            public string ActualReturnedQty { get; set; }
            public string DeviationQty { get; set; }
            public string Remark { get; set; }
        }
    }


    public class ReportLabelInfoModelQuery : ModelQueryBase
    {
        public string OrgCode { get; set; }
        public string RecDocNo { get; set; }
        public string SubInv { get; set; }
        public string VendorID { get; set; }
        public string Locator { get; set; }
        public string MaterialNo { get; set; }
        public string DateCode { get; set; }
        public string LotNo { get; set; }
        public string Manufacturer { get; set; }
        public string ManufacturerPN { get; set; }
        public DateTime RTFrom { get; set; }
        public DateTime RTTo { get; set; }
        public string CLID { get; set; }
        public string BoxID { get; set; }
        public string RTLot { get; set; }
        public string StatusCode { get; set; }
        public string LastTransaction { get; set; }
        public string ReportType { get; set; }     // Detail / Summary


    }
}
