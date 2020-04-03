using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
   public  class WIPUnitFlowRequest : ServerRequestBase<WIPUnitFlowRequest.Item>
    {
        public class Item
        {
            public string IntSN { get; set; }
        }
    }
}