using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response.Reports
{
    public class ProductStationResponse : ServerResponseBase<List<ProductStationResponse.Item>>
    {
        public class Item
        {
            public string Station { get; set; }
            public string Description { get; set; }
            public int Seq { get; set; }
        }
    }
}
