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
    public class SMTQCDataBLL : eTraceBLLBase<SMTQCDataBLL, ISMTQCDataModuleBLL>, ISMTQCDataModuleBLL
    {
        //private ISMTQCDataDAL tdSMTQCDataV2ModuleDal = null;
        private ISMTQCDataDAL tdSMTQCDataRptModuleDal = null;
        public SMTQCDataBLL()
        {
            tdSMTQCDataRptModuleDal = DBManager.Instance.GetSMTQCDataModuleDAL(EmDBType.eTraceConnection);
        }

        public ReportSMTQCDataModel GetSMTQCDataData(ReportSMTQCDataQuery query)
        {

            return tdSMTQCDataRptModuleDal.GetSMTQCDataData(query);
        }

        public long SMTQCDataDataGetRowCount(ReportSMTQCDataQuery query)
        {
            return tdSMTQCDataRptModuleDal.SMTQCDataDataGetRowCount(query);
        }

    }
}
