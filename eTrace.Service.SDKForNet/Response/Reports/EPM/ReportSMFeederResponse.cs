using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    public class ReportSMFeederHeaderResponse : ServerResponseBase<List<ReportSMFeederHeaderResponse.Item>>
    {
        public class Item: ReportSMFeederHeaderModel.Header
        {

        }
    }
    //PM Header
    public class ReportSMFeederPMHeaderResponse : ServerResponseBase<List<ReportSMFeederPMHeaderResponse.Item>>
    {
        public class Item: ReportSMFeederPMHeaderModel.PMHeader
        {

        }
    }
    //PM Header Item
    public class ReportSMFeederPMHeaderItemResponse : ServerResponseBase<List<ReportSMFeederPMHeaderItemResponse.Item>>
    {
        public class Item : ReportSMFeederPMHeaderItemModel.PMHeaderItem
        {

        }
    }
    //PM Header Mat
    public class ReportSMFeederPMHeaderMatResponse : ServerResponseBase<List<ReportSMFeederPMHeaderMatResponse.Item>>
    {
        public class Item : ReportSMFeederPMHeaderMatModel.PMHeaderMat
        {

        }
    }

    //Repair Heaader
    public class ReportSMFeederRepairHeaderResponse : ServerResponseBase<List<ReportSMFeederRepairHeaderResponse.Item>>
    {
        public class Item : ReportSMFeederRepairHeaderModel.RepairHeader
        {

        }
    }
    //Repair Heaader Mat
    public class ReportSMFeederRepairHeaderMatResponse : ServerResponseBase<List<ReportSMFeederRepairHeaderMatResponse.Item>>
    {
        public class Item : ReportSMFeederRepairHeaderMatModel.RepairHeaderMat
        {

        }
    }

}
