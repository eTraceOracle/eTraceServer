using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface IEquipmentPMPlanDAL
    {

        /// <summary>
        /// 获取Equipment Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ReportEquipmentPMPlanModel GetSMEquipmentPMPlanDatas(ReportEquipmentPMPlanModelQuery query);
                                                                               
        List<string> GetSMEquipmentPMPlanCate();

        List<string> GetSMEquipmentPMPlanSubCate();

        long EquipmentPMPlanDataGetRowCount(ReportEquipmentPMPlanModelQuery query);
    }
}
