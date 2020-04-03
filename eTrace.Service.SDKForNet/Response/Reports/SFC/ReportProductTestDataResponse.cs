using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    public class ReportProductTestDataResponse 
        : ServerResponseBase<List<ReportProductTestDataResponse.Item>>
    {
        public class Item
        {
            public string ExtSerialNo { get; set; }
            public string ProcessName { get; set; }
            public int SeqNo { get; set; }
            public string TestStep { get; set; }
            public string TestId { get; set; }
            public string TestName { get; set; }
            public string InputCondition { get; set; }
            public string TestNameAndInputCondition {
                get;
                set;
            }
            public DateTime DateTime { get; set; }
            public double LowLimit { get; set; }
            public double Result { get; set; }
            public double HighLimit { get; set; }
            public string Unit { get; set; }
            public string SystemNo { get; set; }
            public string Status { get; set; }
            public int Seq { get; set; }
        }
    }
}
