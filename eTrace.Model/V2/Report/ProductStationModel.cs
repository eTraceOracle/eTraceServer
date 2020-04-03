using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ProductStationModel : ModelBase<List<ProductStationModel.Item>>
    {
        public class Item
        {
            public string Process { get; set; }
            public string Description { get; set; }
            public string ProcessType { get; set; }
            public string Status { get; set; }
        }
    }

    public class ProductStationQuery : ModelQueryBase
    {
        public string ProcessType { get; set; }
        public string Status { get; set; }
    }
}
