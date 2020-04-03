using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface IEPMEventDAL
    {

        /// <summary>
        /// 获取Equipment Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ReportEPMEventModel GetSMEPMEventDatas(ReportEPMEventModelQuery query);

        ReportEPMEventItemModel GetSMEPMEventItemDatas(ReportEPMEventModelQuery query);

        long EPMEventDataGetRowCount(ReportEPMEventModelQuery query);

        List<string> GetCategory();
    }
}
