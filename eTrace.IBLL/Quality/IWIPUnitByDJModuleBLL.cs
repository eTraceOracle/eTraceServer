using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface IWIPUnitByDJModuleBLL
    {
        ReportWIPUnitByDJModel GetWIPUnitByDJData(ReportWIPUnitByDJQuery query);

        //Check if DJ exist for IntSN
        List<string> GetDJ(string IntSN);

        long WIPUnitByDJDataGetRowCount(ReportWIPUnitByDJQuery query);
    }
}
