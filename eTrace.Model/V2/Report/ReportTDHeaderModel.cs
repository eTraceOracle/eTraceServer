using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model
{
    public class ReportTDHeaderModel : ModelBase<List<ReportTDHeaderModel.Item>>
    {
        public class Item
        {
            public string TDID { get; set; }
            public string ExtSerialNo { get; set; }
            public string IntSerialNo { get; set; }
            public string ProcessName { get; set; }
            public int SeqNo { get; set; }
            public string PO { get; set; }
            public string Model { get; set; }
            public string PCBA { get; set; }
            public DateTime ProdDate { get; set; }
            public DateTime WIPIn { get; set; }
            public string Result { get; set; }
            public string OperatorName { get; set; }
            public string TesterNo { get; set; }
            public string ProgramName { get; set; }
            public string ProgramRevision { get; set; }
            public string IPSNo { get; set; }
            public string IPSRevision { get; set; }
            public string Remark { get; set; }
        }
    }

    public class ReportTDHeaderQuery : ModelQueryBase
    {
        public string ExtserialNo { get; set; }
        public string IntSerialNo { get; set; }
    }
}
