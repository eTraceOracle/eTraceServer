using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface IEPMEventModuleBLL
    {
        ReportEPMEventModel GetSMEPMEventDatas(ReportEPMEventModelQuery query);

        ReportEPMEventItemModel GetSMEPMEventItemDatas(ReportEPMEventModelQuery query);

        long EPMEventDataGetRowCount(ReportEPMEventModelQuery query);
        List<string> GetCategory();
    }
}
