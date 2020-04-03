using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportWIPUnitBySNModel : ModelBase<List<ReportWIPUnitBySNModel.Item>>
    {
        public class Item
        {
            public string IntSN { get; set; }
            public string Model { get; set; }
            public string PCBA { get; set; }
            public string MotherBoardSN { get; set; }
            public string CurrentProcess { get; set; }
            public string process { get; set; }
            public string CircuitCode { get; set; }
            public string Component { get; set; }
            public string CLID { get; set; }
            public string JobID { get; set; }
            public DateTime ChangedOn { get; set; }
            public string ChangedBy { get; set; }
            public string Remarks { get; set; }
            public string DateCode { get; set; }
            public string LotNo { get; set; }
            public string VendorID { get; set; }
            public string VendorName { get; set; }
            public string VendorPN { get; set; }
            public string Manufacturer { get; set; }
            public string ManufacturerPN { get; set; }
            public string RecDocNo { get; set; }
        }
    }

    public class ReportWIPUnitBySNQuery : ModelQueryBase
    {
        public string IntSN { get; set; }
    }
}
