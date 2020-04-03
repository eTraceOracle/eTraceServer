using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface ISMMSDDAL
    {

        /// <summary>
        /// 获取Equipment Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ReportSMMSDModel GetSMMSDDatas(ReportSMMSDModelQuery query);

        ReportSMMSDPMItemModel GetSMMSDPMItemDatas(ReportSMMSDModelQuery query);

        List<string> GetSMMSDLastTransaction();

        long SMMSDDataGetRowCount(ReportSMMSDModelQuery query);
    }
}
