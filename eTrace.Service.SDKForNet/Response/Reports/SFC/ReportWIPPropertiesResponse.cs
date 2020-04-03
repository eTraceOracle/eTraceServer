using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    public class ReportWIPPropertiesResponse
        : ServerResponseBase<List<ReportWIPPropertiesResponse.Item>>
    {
        public class Item
        {
            public string IntSerialNo { get; set; }
            public int SeqNo { get; set; }
            public string ProcessName { get; set; }
            public string Model { get; set; }
            public string PCBA { get; set; }
            public string PropertyType { get; set; }
            public string PropertyName { get; set; }
            public string InputType { get; set; }
            public string PropertyValue { get; set; }
            public string MotherBoard { get; set; }
            public string MotherBoardSN { get; set; }
            public string ChangedBy { get; set; }
            public DateTime ChangedOn { get; set; }
        }
    }
}
