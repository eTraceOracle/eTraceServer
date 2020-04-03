using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report.DailyRepairList
{
     public class GetMoreThanOneModel : ModelBase<List<GetMoreThanOneModel.Item>>
    {
        public class Item
        {
            public string Model { get; set; }
            public string TestStation { get; set; }
            public string DefectCode { get; set; }
            public string Cause { get; set; }
            public string CompPN { get; set; }
            public string CompRefD { get; set; }
            public string Floor { get; set; }
            public int Qty { get; set; }
        }
    }
    
    public class GetMoreThanOneQuery : ModelQueryBase<RequestItem>
    {
       
    }
}
