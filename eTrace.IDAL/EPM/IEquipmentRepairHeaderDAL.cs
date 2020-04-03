using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface IEquipmentRepairHeaderDAL
    {

        /// <summary>
        /// 获取Equipment Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ReportEquipmentRepairHeaderModel GetSMRepairHeaderDatas(ReportEquipmentRepairHeaderModelQuery query);

        ReportEquipmentRepairMatModel GetSMRepairMatDatas(ReportEquipmentRepairHeaderModelQuery query);

        List<string> GetSMRepairStatus();

        long EquipmentRepairHeaderDataGetRowCount(ReportEquipmentRepairHeaderModelQuery query);
    }
}
