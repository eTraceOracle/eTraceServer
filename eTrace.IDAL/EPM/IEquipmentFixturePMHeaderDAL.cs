using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface IEquipmentFixturePMHeaderDAL
    {

        /// <summary>
        /// 获取Equipment Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ReportEquipmentFixturePMHeaderModel GetSMEquipmentFixturePMHeaderDatas(ReportEquipmentFixturePMHeaderQuery query);

        ReportEquipmentFixturePMDetailModel GetSMEquipmentFixturePMDetailDatas(ReportEquipmentFixturePMHeaderQuery query);
        ReportEquipmentFixturePMItemModel GetSMEquipmentFixturePMItemDatas(ReportEquipmentFixturePMHeaderQuery query);
        ReportEquipmentFixturePMMatModel GetSMEquipmentFixturePMMatDatas(ReportEquipmentFixturePMHeaderQuery query);

        List<string> GetPMFrequency();

        long EquipmentFixturePMHeaderDataGetRowCount(ReportEquipmentFixturePMHeaderQuery query);
        long EquipmentFixturePMDetailDataGetRowCount(ReportEquipmentFixturePMHeaderQuery query);
        
    }
}
