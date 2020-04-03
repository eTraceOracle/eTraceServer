using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports.SFC
{
    public class GetSMTAOIDataRequest : ServerRequestBase<GetSMTAOIDataRequestItem>
    {


    }
    public class DownloadSMTAOIDataRequest : ReportDownloadRequestBase<GetSMTAOIDataRequestItem>
    {

    }
    public class GetSMTAOIDataRequestItem
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Model { get; set; }
        public string EquipmentID { get; set; }
        public string Result { get; set; }
        public string DiscreteJob{ get; set; }
        public string AOIBarcode { get; set; } 
    }
}
