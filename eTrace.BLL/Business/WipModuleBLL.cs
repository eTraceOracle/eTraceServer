using eTrace.Common;
using eTrace.Core;
using eTrace.IBLL;
using eTrace.IDAL;
using eTrace.Model.V2.Report;
using eTrace.Report.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.BLL.Business
{
    public class WipModuleBLL : eTraceBLLBase<WipModuleBLL, IWipModuleBLL>, IWipModuleBLL
    {
        private IWipModuleDAL wipModuleDal = null;
        public WipModuleBLL()
        {
            wipModuleDal = DBManager.Instance.GetWipModuleDAL(EmDBType.eTraceV2Connection);
        }

        public IList<WipFlowDB> GetWipFlowDBs(WipFlowQuery query)
        {
            return wipModuleDal.GetWipFlowDBs(query);
        }
    }
}
