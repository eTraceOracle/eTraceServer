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
    public class EquipmentRepairHeaderBLL : eTraceBLLBase<EquipmentRepairHeaderBLL, IEquipmentRepairHeaderModuleBLL>, IEquipmentRepairHeaderModuleBLL
    {
        private IEquipmentRepairHeaderDAL tdIEquipmentRepairHeaderDAL = null;
        public EquipmentRepairHeaderBLL()
        {
            tdIEquipmentRepairHeaderDAL = DBManager.Instance.GetEquipmentRepairHeaderModuleDAL(EmDBType.eTraceConnection);
        }

        public ReportEquipmentRepairHeaderModel GetSMRepairHeaderDatas(ReportEquipmentRepairHeaderModelQuery query)
        {
            return tdIEquipmentRepairHeaderDAL.GetSMRepairHeaderDatas(query);
        }

        public ReportEquipmentRepairMatModel GetSMRepairMatDatas(ReportEquipmentRepairHeaderModelQuery query)
        {
            return tdIEquipmentRepairHeaderDAL.GetSMRepairMatDatas(query);
        }

        public long EquipmentRepairHeaderDataGetRowCount(ReportEquipmentRepairHeaderModelQuery query)
        {
            return tdIEquipmentRepairHeaderDAL.EquipmentRepairHeaderDataGetRowCount(query);
        }

        public List<string> GetSMRepairStatus()
        {
            return tdIEquipmentRepairHeaderDAL.GetSMRepairStatus();
        }

    }
}
