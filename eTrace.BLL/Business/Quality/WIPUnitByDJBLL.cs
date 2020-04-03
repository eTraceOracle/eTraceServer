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
    public class WIPUnitByDJBLL : eTraceBLLBase<WIPUnitByDJBLL, IWIPUnitByDJModuleBLL>, IWIPUnitByDJModuleBLL
    {
        //private IWIPUnitByDJDAL tdWIPUnitByDJV2ModuleDal = null;
        private IWIPUnitByDJDAL tdWIPUnitByDJRptModuleDal = null;
        public WIPUnitByDJBLL()
        {
            tdWIPUnitByDJRptModuleDal = DBManager.Instance.GetWIPUnitByDJModuleDAL(EmDBType.eTraceConnection);
        }

        public ReportWIPUnitByDJModel GetWIPUnitByDJData(ReportWIPUnitByDJQuery query)
        {

            return tdWIPUnitByDJRptModuleDal.GetWIPUnitByDJData(query);
        }

        public long WIPUnitByDJDataGetRowCount(ReportWIPUnitByDJQuery query)
        {
            return tdWIPUnitByDJRptModuleDal.WIPUnitByDJDataGetRowCount(query);
        }

        public List<string> GetDJ(string IntSN)
        {
            return tdWIPUnitByDJRptModuleDal.GetDJ(IntSN);
        }

    }
}
