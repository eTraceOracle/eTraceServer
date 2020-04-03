using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface ISMSolderPasteGlueModuleBLL
    {
        ReportSMSolderPasteGlueModel GetSMSolderPasteGlueDatas(ReportSMSolderPasteGlueModelQuery query);

        //Initialization Repair Status List
        List<string> GetSMSolderPasteGlueLastTransaction();
         
        long SMSolderPasteGlueDataGetRowCount(ReportSMSolderPasteGlueModelQuery query);
    }
}
