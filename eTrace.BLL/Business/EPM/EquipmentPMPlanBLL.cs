using eTrace.Common;
using eTrace.Core;
using eTrace.Report.IBLL;
using eTrace.Report.IDAL;
using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.BLL.Business
{
    public class EquipmentPMPlanBLL : eTraceBLLBase<EquipmentPMPlanBLL, IEquipmentPMPlanModuleBLL>, IEquipmentPMPlanModuleBLL
    {
        private IEquipmentPMPlanDAL tdIEquipmentPMPlanDAL = null;
        public EquipmentPMPlanBLL()
        {
            tdIEquipmentPMPlanDAL = DBManager.Instance.GetEquipmentPMPlanModuleDAL(EmDBType.eTraceConnection);
        }

        public ReportEquipmentPMPlanModel GetSMEquipmentPMPlanDatas(ReportEquipmentPMPlanModelQuery query)
        {
            return tdIEquipmentPMPlanDAL.GetSMEquipmentPMPlanDatas(query);
        }

        public long EquipmentPMPlanDataGetRowCount(ReportEquipmentPMPlanModelQuery query)
        {
            return tdIEquipmentPMPlanDAL.EquipmentPMPlanDataGetRowCount (query);
        }

        public List<string> GetSMEquipmentPMPlanCate()
        {
            return tdIEquipmentPMPlanDAL.GetSMEquipmentPMPlanCate();
        }

        public List<string> GetSMEquipmentPMPlanSubCate()
        {
            return tdIEquipmentPMPlanDAL.GetSMEquipmentPMPlanSubCate();
        }

    }
}
