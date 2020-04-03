using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface IWIPUnitBySNModuleBLL
    {
        ReportWIPUnitBySNModel GetWIPUnitBySNData(ReportWIPUnitBySNQuery query);

        //Check if DJ exist for IntSN
        List<string> GetWIPID(string IntSN);

        long WIPUnitBySNDataGetRowCount(ReportWIPUnitBySNQuery query);
    }
}
