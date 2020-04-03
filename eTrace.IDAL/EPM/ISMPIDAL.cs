using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface ISMPIDAL
    {

        /// <summary>
        /// 获取Equipment Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ReportSMPIHeaderModel GetSMPIHeaderDatas(ReportSMPIHeaderModelQuery query);

        ReportSMPIEquipmentsModel GetSMPIEquipmentsDatas(ReportSMPIHeaderModelQuery query);

        ReportSMPIMatsModel GetSMPIMatsDatas(ReportSMPIHeaderModelQuery query);

        ReportSMPIHistoryModel GetSMPIHistoryDatas(ReportSMPIHeaderModelQuery query);

        long SMPIDataGetRowCount(ReportSMPIHeaderModelQuery query);
    }
}
