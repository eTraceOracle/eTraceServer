using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface IEquipmentRepairHeaderModuleBLL
    {
        ReportEquipmentRepairHeaderModel GetSMRepairHeaderDatas(ReportEquipmentRepairHeaderModelQuery query);

        ReportEquipmentRepairMatModel GetSMRepairMatDatas(ReportEquipmentRepairHeaderModelQuery query);

        //Initialization Repair Status List
        List<string> GetSMRepairStatus();

        long EquipmentRepairHeaderDataGetRowCount(ReportEquipmentRepairHeaderModelQuery query);
    }
}
