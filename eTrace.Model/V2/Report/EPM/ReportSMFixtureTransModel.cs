using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportSMFixtureTransModel : ModelBase<List<ReportSMFixtureTransModel.Item>>
    {
        public class Item
        {
            public string FixtureID { get; set; }
            public string ItemNo { get; set; }
            public string TransactionType { get; set; }
            public string ReasonCode { get; set;}
            public string FromLoc { get; set; }
            public string ToLoc { get; set; }
            public string FromStatus { get; set; }
            public string ToStatus { get; set; }
            public string JobID { get; set; }
            public string Usage { get; set; }
            public DateTime TransactionDate { get; set; }
            public string TransactedBy { get; set; }
            public string Remarks { get; set; }

        }
    }

    public class ReportSMFixtureTransModelQuery : ModelQueryBase
    {
        public string FixtureID { get; set; }
        public string ItemNo { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string BatchID { get; set; }
        public string Status { get; set; }
        public string TransactionType { get; set; }
        public string Owner { get; set; }
    }
}
