using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface IEquipmentPMSpecModuleBLL
    {
        ReportEquipmentPMSpecModel GetSMEquipmentPMSpecDatas(ReportEquipmentPMSpecModelQuery query);

        ReportEquipmentPMSpecPMItemModel GetSMEquipmentPMSpecPMItemDatas(ReportEquipmentPMSpecModelQuery query);

        long EquipmentPMSpecDataGetRowCount(ReportEquipmentPMSpecModelQuery query);
    }
}
