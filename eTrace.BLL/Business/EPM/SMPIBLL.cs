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
    public class SMPIBLL : eTraceBLLBase<SMPIBLL, ISMPIModuleBLL>, ISMPIModuleBLL
    {
        private ISMPIDAL tdISMPIDAL = null;
        public SMPIBLL()
        {
            tdISMPIDAL = DBManager.Instance.GetSMPIModuleDAL(EmDBType.eTraceConnection);
        }

        public ReportSMPIHeaderModel GetSMPIHeaderDatas(ReportSMPIHeaderModelQuery query)
        {
            return tdISMPIDAL.GetSMPIHeaderDatas(query);
        }

        public ReportSMPIEquipmentsModel GetSMPIEquipmentsDatas(ReportSMPIHeaderModelQuery query)
        {
            return tdISMPIDAL.GetSMPIEquipmentsDatas(query);
        }

        public ReportSMPIMatsModel GetSMPIMatsDatas(ReportSMPIHeaderModelQuery query)
        {
            return tdISMPIDAL.GetSMPIMatsDatas(query);
        }

        public ReportSMPIHistoryModel GetSMPIHistoryDatas(ReportSMPIHeaderModelQuery query)
        {
            return tdISMPIDAL.GetSMPIHistoryDatas(query);
        }

        public long SMPIDataGetRowCount(ReportSMPIHeaderModelQuery query)
        {
            return tdISMPIDAL.SMPIDataGetRowCount (query);
        }

    }
}
