using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface ISMSolderPasteGlueDAL
    {

        /// <summary>
        /// 获取Equipment Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ReportSMSolderPasteGlueModel GetSMSolderPasteGlueDatas(ReportSMSolderPasteGlueModelQuery query);
                                                                               
        List<string> GetSMSolderPasteGlueLastTransaction();

        long SMSolderPasteGlueDataGetRowCount(ReportSMSolderPasteGlueModelQuery query);
    }
}
