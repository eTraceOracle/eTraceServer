using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface ISMMatTransDAL
    {

        /// <summary>
        /// 获取Equipment Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ReportSMMatTransModel GetSMMatTransDatas(ReportSMMatTransModelQuery query);
                                                                               
        List<string> GetSMMatTransFromLocID();    

        List<string> GetSMMatTransToLocID();

        List<string> GetMovementType();

        long SMMatTransDataGetRowCount(ReportSMMatTransModelQuery query);
    }
}
