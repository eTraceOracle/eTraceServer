using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportSMFixtureVerificationModel : ModelBase<List<ReportSMFixtureVerificationModel.Item>>
    {
        public class Item
        {
            public string FixtureID { get; set; }
            public string ActualLocation { get; set; }
            public string SystemLocation { get; set; }
            public string Status { get; set; }
            public string Result { get; set; }
            public DateTime TransactionDate { get; set; }
        }
    }

    public class ReportSMFixtureVerificationModelQuery : ModelQueryBase
    {
        public string FixtureID { get; set; }
        public string ItemNO { get; set; }      
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string BatchID { get; set; }
        public string Status { get; set; }
        public string ReasonCode { get; set; }
        public string Owner { get; set; }
        public DateTime TransactionDateFrom { get; set; }
        public DateTime TransactionDateTo { get; set; }
    }
}
