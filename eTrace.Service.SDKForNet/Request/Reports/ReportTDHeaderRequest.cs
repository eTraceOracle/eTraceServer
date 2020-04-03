using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request
{
    public class ReportTDHeaderRequest : ServerRequestBase<ReportTDHeaderRequest.Item>
    {
        public class Item
        {
            public string DJ { get; set; } 
            public string IntSN { get; set; }
        }
    }
}
