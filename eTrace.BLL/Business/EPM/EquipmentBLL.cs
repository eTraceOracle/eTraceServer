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
    public class EquipmentBLL : eTraceBLLBase<EquipmentBLL, IEquipmentModuleBLL>, IEquipmentModuleBLL
    {
        private IEquipmentDAL tdEquipmentV2ModuleDal = null;
        public EquipmentBLL()
        {
            tdEquipmentV2ModuleDal = DBManager.Instance.GetEquipmentModuleDAL(EmDBType.eTraceConnection);
        }

        public ReportEquipmentModel GetEquipmentData(ReportEquipmentQuery query)
        {
            return tdEquipmentV2ModuleDal.GetEquipmentData(query);
        }

        public long EquipmentDataGetRowCount(ReportEquipmentQuery query)
        {
            return tdEquipmentV2ModuleDal.EquipmentDataGetRowCount(query);
        }

        public List<string> GetEquipmentStatus()
        {
            return tdEquipmentV2ModuleDal.GetEquipmentStatus();
        }

        public List<string> GetAllDepartment()
        {
            return tdEquipmentV2ModuleDal.GetAllDepartment();
        }

    }
}
