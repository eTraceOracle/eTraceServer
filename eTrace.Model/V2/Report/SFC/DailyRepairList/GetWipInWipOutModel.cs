using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report.DailyRepairList
{
     public class GetWipInWipOutModel : ModelBase<List<GetWipInWipOutModel.Item>>
    {
        public class Item
        {
            public string Station { get; set; }
            public int WipinQty { get; set; }
            public int WipoutQty { get; set; }
        }
    }
    
    public class GetWipInWipOutQuery : ModelQueryBase<RequestItem>
    {
       
    }
}
