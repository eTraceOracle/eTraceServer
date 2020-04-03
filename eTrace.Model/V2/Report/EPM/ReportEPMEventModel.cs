using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportEPMEventModel : ModelBase<List<ReportEPMEventModel.Item>>
    {
        public class Item
        {
            public string EventID { get; set; }
            public string Category { get; set; }
            public string Desc { get; set; }
            public DateTime CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public string Remarks { get; set; }
        }
    }

    public class ReportEPMEventItemModel : ModelBase<List<ReportEPMEventItemModel.EventItem>>
    {
        public class EventItem
        {
            public string EventID { get; set; }
            public string Item { get; set; }
            public string Value { get; set; }
            public string Lookup { get; set; }
            public string ErrMessage { get; set; }
        }

    }

    public class ReportEPMEventModelQuery : ModelQueryBase
    {
        public string EventID { get; set; }
        public string Category { get; set; }
        public string Desc { get; set; }
    }
}
