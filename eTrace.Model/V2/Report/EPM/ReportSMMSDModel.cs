using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportSMMSDModel : ModelBase<List<ReportSMMSDModel.Item>>
    {
        public class Item
        {
            public string CLID { get; set; }
            public string MaterialNo { get; set; }
            public string MSL { get; set; }
            public string FloorLife { get; set; }
            public string RemainingLife { get; set; }
            public string LastTransaction { get; set; }
            public string ProdLine { get; set; }
            public DateTime ChangedOn { get; set; }
            public string ChangedBy { get; set; }
            public string Remarks { get; set; }

        }
    }

    public class ReportSMMSDPMItemModel : ModelBase<List<ReportSMMSDPMItemModel.Item>>
    {
        public class Item
        {
            public string CLID { get; set; }
            public DateTime TransactDate { get; set; }
            public string Transaction { get; set; }
            public string FromStatus { get; set; }
            public string ToStatus { get; set; }
            public string Details { get; set; }
            public string TransactedBy { get; set; }
            public string Remarks { get; set; }

        }

    }

    public class ReportSMMSDModelQuery : ModelQueryBase
    {
        public string MaterialNo { get; set; }
        public string CLID { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string LastTransaction { get; set; }
    }
}
