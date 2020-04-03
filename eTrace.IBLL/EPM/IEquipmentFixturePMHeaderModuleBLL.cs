using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface IEquipmentFixturePMHeaderModuleBLL
    {
        ReportEquipmentFixturePMHeaderModel GetSMEquipmentFixturePMHeaderDatas(ReportEquipmentFixturePMHeaderQuery query);

        ReportEquipmentFixturePMDetailModel GetSMEquipmentFixturePMDetailDatas(ReportEquipmentFixturePMHeaderQuery query);

        ReportEquipmentFixturePMItemModel GetSMEquipmentFixturePMItemDatas(ReportEquipmentFixturePMHeaderQuery query);
        ReportEquipmentFixturePMMatModel GetSMEquipmentFixturePMMatDatas(ReportEquipmentFixturePMHeaderQuery query);
        //Initialization Equipment Status List
        List<string> GetPMFrequency();

        long EquipmentFixturePMHeaderDataGetRowCount(ReportEquipmentFixturePMHeaderQuery query);
        long EquipmentFixturePMDetailDataGetRowCount(ReportEquipmentFixturePMHeaderQuery query);

    }
}
