using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface ISMTQCDataModuleBLL
    {
        ReportSMTQCDataModel GetSMTQCDataData(ReportSMTQCDataQuery query);

        long SMTQCDataDataGetRowCount(ReportSMTQCDataQuery query);
    }
}
