using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface ISMLocDAL
    {

        /// <summary>
        /// Get SMLoc Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ReportSMLocModel GetSMLocDatas(ReportSMLocModelQuery query);

        ReportSMLocModel GetSMLocCate();


        //List<string> GetSMLocCate();
        List<string> GetSMLocCategory();
        List<string> GetSMLocSubCategory();
        List<string> GetSMLocStore();

        long SMLocDataGetRowCount(ReportSMLocModelQuery query);
    }
}
