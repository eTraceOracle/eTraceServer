using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report.DailyRepairList
{
    /// <summary>
    /// public RequestItem for DailyRepairList Interface
    /// </summary>
    public class RequestItem
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }

}
