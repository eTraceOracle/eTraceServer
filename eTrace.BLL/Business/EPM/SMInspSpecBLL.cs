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
    public class SMInspSpecBLL : eTraceBLLBase<SMInspSpecBLL, ISMInspSpecModuleBLL>, ISMInspSpecModuleBLL
    {
        private ISMInspSpecDAL tdISMInspSpecDAL = null;
        public SMInspSpecBLL()
        {
            tdISMInspSpecDAL = DBManager.Instance.GetSMInspSpecModuleDAL(EmDBType.eTraceConnection);
        }

        public ReportSMInspSpecModel GetSMInspSpecDatas(ReportSMInspSpecModelQuery query)
        {
            return tdISMInspSpecDAL.GetSMInspSpecDatas(query);
        }

        public ReportSMInspSpecModel GetSMInspSpecItemDatas(ReportSMInspSpecModelQuery query)
        {
            return tdISMInspSpecDAL.GetSMInspSpecItemDatas(query);
        }

        public long SMInspSpecDataGetRowCount(ReportSMInspSpecModelQuery query)
        {
            return tdISMInspSpecDAL.SMInspSpecDataGetRowCount (query);
        }

        public List<string> GetSMInspSpecID()
        {
            return tdISMInspSpecDAL.GetSMInspSpecID();
        }

    }
}
