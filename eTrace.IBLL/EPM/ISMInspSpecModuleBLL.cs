using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface ISMInspSpecModuleBLL
    {
        ReportSMInspSpecModel GetSMInspSpecDatas(ReportSMInspSpecModelQuery query);

        ReportSMInspSpecModel GetSMInspSpecItemDatas(ReportSMInspSpecModelQuery query);

        //Initialization Repair Status List
        List<string> GetSMInspSpecID();


        long SMInspSpecDataGetRowCount(ReportSMInspSpecModelQuery query);
    }
}
