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
    public class EquipmentPMSpecBLL : eTraceBLLBase<EquipmentPMSpecBLL, IEquipmentPMSpecModuleBLL>, IEquipmentPMSpecModuleBLL
    {
        private IEquipmentPMSpecDAL tdIEquipmentPMSpecDAL = null;
        public EquipmentPMSpecBLL()
        {
            tdIEquipmentPMSpecDAL = DBManager.Instance.GetEquipmentPMSpecModuleDAL(EmDBType.eTraceConnection);
        }

        public ReportEquipmentPMSpecModel GetSMEquipmentPMSpecDatas(ReportEquipmentPMSpecModelQuery query)
        {
            return tdIEquipmentPMSpecDAL.GetSMEquipmentPMSpecDatas(query);
        }

        public ReportEquipmentPMSpecPMItemModel GetSMEquipmentPMSpecPMItemDatas(ReportEquipmentPMSpecModelQuery query)
        {
            return tdIEquipmentPMSpecDAL.GetSMEquipmentPMSpecPMItemDatas(query);
        }

        public long EquipmentPMSpecDataGetRowCount(ReportEquipmentPMSpecModelQuery query)
        {
            return tdIEquipmentPMSpecDAL.EquipmentPMSpecDataGetRowCount (query);
        }

    }
}
