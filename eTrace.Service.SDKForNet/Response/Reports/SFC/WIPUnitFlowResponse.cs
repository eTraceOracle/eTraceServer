using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response.Reports
{
    public class WIPUnitFlowResponse : ServerResponseBase<List<WIPUnitFlowResponse.Item>>
    {
        public class Item
        {
            public int Seq { get; set; }
            public int SeqNo { get; set; }
            public string IntSN { get; set; }
            public string PCBA { get; set; }
            public string DJ { get; set; }
            public string ProdLine { get; set; }
            public string Process { get; set; }
            public string Status { get; set; }
            public string LastResult { get; set; }
            public int TestRound { get; set; }
            public int FailedTest { get; set; }
            public int MaxTestRound { get; set; }
            public int MaxFailure { get; set; }
        }
    }
}
