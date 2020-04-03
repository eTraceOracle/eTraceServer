using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response.Reports
{
    public class ReportFixtureCatResponse : ServerResponseBase<List<ReportFixtureCatResponse.Item>>
    {
        public class Item
        {
            public String category { get; set; }

            public List<string> subCategory { get; set; }
        }
       
    }
}
