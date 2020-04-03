using eTrace.Common;
using eTrace.Core;
using eTrace.Report.IBLL;
using eTrace.Report.IDAL;
using eTrace.Model.V2.Report;

namespace eTrace.Report.BLL.Business
{
    public class MobileEquipmentBLL : eTraceBLLBase<MobileEquipmentBLL, IMobileEquipmentBLL>, IMobileEquipmentBLL
    {
        private IMobileEquipmentDAL tdEquipmentV2ModuleDal = null;
        public MobileEquipmentBLL()
        {
            tdEquipmentV2ModuleDal = DBManager.Instance.GetMobileEquipmentModuleDAL(EmDBType.eTraceConnection);
        }

        public ReportMobileEquipmentModel GetMobileEquipmentData(ReportMobileEquipmentQuery query)
        {
            return tdEquipmentV2ModuleDal.GetMobileEquipmentData(query);
        }

        public long EquipmentDataGetRowCount(ReportMobileEquipmentQuery query)
        {
            return tdEquipmentV2ModuleDal.EquipmentDataGetRowCount(query);
        }
        public bool InsertMobileItem(ReportMobileEquipmentQuery reportEquipmentMobile)
        {
            return tdEquipmentV2ModuleDal.InsertMobileItem(reportEquipmentMobile);
        }


    }
}
