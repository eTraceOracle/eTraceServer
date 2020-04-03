using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportFGAgingDetailModel : ModelBase<List<ReportFGAgingDetailModel.Detail>>
    {
        public class Detail
        {
            public string OrgCode { get; set; }
            public string Item { get; set; }
            public string SubInv  { get; set; }
            public string Locator { get; set; }
            public string CLID { get; set; }
            public string PalletID_BOXID { get; set; }
            public decimal QtyBaseUOM { get; set; }
            public string DateCode { get; set; }
            public string LotNo { get; set; }
            public string SalesOrderNo { get; set; }
            public string SalesOrderLine { get; set; }
            public string DJNo { get; set; }
            public string RT { get; set; }
            public string RoHS { get; set; }
            public string Rev { get; set; }
            public DateTime? DJCompletionDate { get; set; }
            public int StorageDays { get; set; }
            public string CreatedBy { get; set; }
            public DateTime? CreatedOn { get; set; }
            public string ChangedBy { get; set; }
            public DateTime? ChangedOn { get; set; }
        }
    }


    public class ReportFGAgingSummaryModel : ModelBase<List<ReportFGAgingSummaryModel.Summary>>
    {
        public class Summary
        {  
            public string OrgCode { get; set; }
            public string Item { get; set; }
            public string SubInv { get; set; }
            public string Locator { get; set; }
            public decimal QTY { get; set; }
        }
    }



    public class ReportFGAgingModelQuery : ModelQueryBase
    {
        public string OrgCode { get; set; }
        public string SubInv { get; set; }
        public string Item { get; set; }
        public int AgingDays { get; set; }
        public DateTime RTDateFrom { get; set; }
        public DateTime RTDateTo { get; set; }
        public string DayOperator { get; set; }
        public string ReportType { get; set; }     // Detail / Summary
    }
}
