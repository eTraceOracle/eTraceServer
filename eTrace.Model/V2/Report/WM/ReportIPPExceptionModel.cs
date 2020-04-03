using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportIPPExceptionModel : ModelBase<List<ReportIPPExceptionModel.Item>>
    {
        public class Item
        {
            public string OrgCode { get; set; }
            public string ProdFloor { get; set; }
            public string DJ { get; set; }
            public string Model { get; set; }
            public decimal Start_Qty { get; set; }
            public decimal Sum_IPPQty { get; set; }
            public decimal Difference_Qty { get; set; }
        }
    }



    public class ReportIPPExceptionModelQuery : ModelQueryBase
    {
        public string OrgCode { get; set; }
        public string Floor { get; set; }
        public string DJ { get; set; }
        public string Model { get; set; }
        public Boolean ExceptionOnly { get; set; }

    }
}
