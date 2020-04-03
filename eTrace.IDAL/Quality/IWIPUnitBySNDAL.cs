using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface IWIPUnitBySNDAL
    {

        /// <summary>
        /// 获取Equipment Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ReportWIPUnitBySNModel GetWIPUnitBySNData(ReportWIPUnitBySNQuery query);

        //List<string> GetWIPID(ReportWIPUnitBySNQuery query);
        List<string> GetWIPID(string IntSN);


        long WIPUnitBySNDataGetRowCount(ReportWIPUnitBySNQuery query);
    }
}
