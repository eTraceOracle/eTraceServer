using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportUploadCiscoDataModel : ModelBase<List<ReportUploadCiscoDataModel.Item>>
    {
        public class Item
        {
            public string ProductSerialNo { get; set; }
            public string Model { get; set; }
            public string CustomerPN { get; set; }
            public string Revision { get; set; }
            public string DeliveryNote { get; set; }
            public string CustomerPO { get; set; }
            public string Firmware { get; set; }
            public DateTime CreatedOn { get; set; }
            public string CreatedBy { get; set; }        }
    }

    public class ReportUploadCiscoDataQuery : ModelQueryBase
    {      
        public DateTime? createFrom { get; set; }
        public DateTime? createTo { get; set; }
    }
}
