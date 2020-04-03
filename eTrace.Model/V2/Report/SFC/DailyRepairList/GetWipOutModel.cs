using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report.DailyRepairList
{
     public class GetWipOutModel : ModelBase<List<GetWipOutModel.Item>>
    {
        public class Item
        {
            public string Code { get; set; }
            public int Qty   { get; set; }
            public decimal Percent { get; set; }
        }
    }
    
    public class GetWipOutQuery : ModelQueryBase<RequestItem>
    {
       
    }
}
