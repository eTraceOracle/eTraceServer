using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response.Reports
{
    /// <summary>
    /// Response Object For API GetWIPInfo 
    /// </summary>
    public  class GetWIPInfoResponse : ServerResponseBase<GetWIPInfoResponseItem>
    {
    }

    public class GetWIPInfoResponseItem
    {

        
        public Header HeaderInfo { get; set; }
        public List<Flow> FlowList { get; set; }
        public List<TestData> TestDataList { get; set; }
        public List<Property> PropertyList { get; set; }



        public class Header
        {
            public string IntSN { get; set; }
            public string Model { get; set; }
            public string PCBA { get; set; }
            public string DJ { get; set; }
            public string InvOrg { get; set; }
            public string CurrentProcess { get; set; }
            public string Result { get; set; }
            public string MotherBoardSN { get; set; }
            public string JobID { get; set; }
            public DateTime ChangedOn { get; set; }
            public string ChangedBy { get; set; }
        }
        public  class Flow
        {
            public int  SeqNo { get; set; }
            public string Process  { get; set; } 
            public string Status { get; set; } 
            public int TestRound { get; set; } 
            public int  FailedTest { get; set; } 
            public string LastResult { get; set; } 
            public int MaxTestRound { get; set; } 
            public int MaxFailure { get; set; }
        }
        public class Property
        {
            public int SeqNo { get; set; }
            public string PropertyType { get; set; }
            public string PropertyName { get; set; }
            public string InputType { get; set; }
            public string PropertyValue { get; set; }
            public DateTime ChangedOn { get; set; }
            public string ChangedBy { get; set; }
        }
        public class TestData
        {
            public string ProcessName { get; set; }
            public int  SeqNo { get; set; }
            public DateTime ProdDate { get; set; }
            public string Result { get; set; }
            public string OperatorName { get; set; }

        }
    }
}
