using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReporteJITFrequencyModel : ModelBase<List<ReporteJITFrequencyModel.Item>>
    {
        public class Item
        {
            public string ITEM { get; set; }
            public string VENDOR_NAME { get; set; }
            public string VENDOR_SITE { get; set; }
            public string PULL_FREQ { get; set; }
            public decimal TRANSIT_LT { get; set; }
            public string ETA_FREQ { get; set; }
        }
    }



    public class ReporteJITFrequencyModelQuery : ModelQueryBase
    {

    }
}
