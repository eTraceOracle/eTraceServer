using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface ISMFeederModuleBLL
    {
        ReportSMFeederHeaderModel GetSMFeederHeaderDatas(ReportSMFeederHeaderModelQuery query);

        ReportSMFeederPMHeaderModel GetSMFeederPMHeaderDatas(ReportSMFeederHeaderModelQuery query);
        ReportSMFeederPMHeaderItemModel GetSMFeederPMHeaderItemDatas(ReportSMFeederHeaderModelQuery query);
        ReportSMFeederPMHeaderMatModel GetSMFeederPMHeaderMatDatas(ReportSMFeederHeaderModelQuery query);
        ReportSMFeederRepairHeaderModel GetSMFeederRepairHeaderDatas(ReportSMFeederHeaderModelQuery query);
        ReportSMFeederRepairHeaderMatModel GetSMFeederRepairHeaderMatDatas(ReportSMFeederHeaderModelQuery query);
        long SMFeederDataGetRowCount(ReportSMFeederHeaderModelQuery query);
    }
}
