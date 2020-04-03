using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    public class ReportWIPTestDataResponse 
        : ServerResponseBase<List<ReportWIPTestDataResponse.Item>>
    {
        public class Item
        {
            public string IntSerialNo { get; set; }
            public string ProcessName { get; set; }
            public int SeqNo { get; set; }
            public string TestStep { get; set; }
            public string Model { get; set; }
            public string PCBA { get; set; }
            public DateTime ProdDate { get; set; }
            public string TesterNo { get; set; }
            public string TestName { get; set; }
            public string ProgramName { get; set; }
            public string ProgramRevision { get; set; }
            public string InputCondition { get; set; }
            public string OutputLoading { get; set; }
            public string OutputName { get; set; }
            public string TestID { get; set; }
            public string OperatorName { get; set; }
            public string IPSReference { get; set; }
            public double LowerLimit { get; set; }
            public double Result { get; set; }
            public double UpperLimit { get; set; }
            public string Unit { get; set; }
            public string Status { get; set; }
        }
    }
}
