using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface ISMMatInvDAL
    {

        /// <summary>
        /// GetSMMatInvDatas
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ReportSMMatInvModel GetSMMatInvDatas(ReportSMMatInvModelQuery query);
                                                                              
        long SMMatInvDataGetRowCount(ReportSMMatInvModelQuery query);
    }
}
