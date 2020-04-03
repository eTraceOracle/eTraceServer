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
    public class WIPUnitBySNBLL : eTraceBLLBase<WIPUnitBySNBLL, IWIPUnitBySNModuleBLL>, IWIPUnitBySNModuleBLL
    {
        //private IWIPUnitBySNDAL tdWIPUnitBySNV2ModuleDal = null;
        private IWIPUnitBySNDAL tdWIPUnitBySNRptModuleDal = null;
        public WIPUnitBySNBLL()
        {
            tdWIPUnitBySNRptModuleDal = DBManager.Instance.GetWIPUnitBySNModuleDAL(EmDBType.eTraceConnection);
        }

        public ReportWIPUnitBySNModel GetWIPUnitBySNData(ReportWIPUnitBySNQuery query)
        {

            return tdWIPUnitBySNRptModuleDal.GetWIPUnitBySNData(query);
        }

        public long WIPUnitBySNDataGetRowCount(ReportWIPUnitBySNQuery query)
        {
            return tdWIPUnitBySNRptModuleDal.WIPUnitBySNDataGetRowCount(query);
        }

        public List<string> GetWIPID(string IntSN)
        {
            return tdWIPUnitBySNRptModuleDal.GetWIPID(IntSN);
        }

    }
}
