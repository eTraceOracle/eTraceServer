using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface ISMPIModuleBLL
    {
        ReportSMPIHeaderModel GetSMPIHeaderDatas(ReportSMPIHeaderModelQuery query);
        ReportSMPIEquipmentsModel GetSMPIEquipmentsDatas(ReportSMPIHeaderModelQuery query);
        ReportSMPIMatsModel GetSMPIMatsDatas(ReportSMPIHeaderModelQuery query);
        ReportSMPIHistoryModel GetSMPIHistoryDatas(ReportSMPIHeaderModelQuery query);
        long SMPIDataGetRowCount(ReportSMPIHeaderModelQuery query);
    }
}
