using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface IWIPUnitByDJDAL
    {

        /// <summary>
        /// 获取Equipment Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ReportWIPUnitByDJModel GetWIPUnitByDJData(ReportWIPUnitByDJQuery query);

        //List<string> GetDJ(ReportWIPUnitByDJQuery query);
        List<string> GetDJ(string IntSN);


        long WIPUnitByDJDataGetRowCount(ReportWIPUnitByDJQuery query);
    }
}
